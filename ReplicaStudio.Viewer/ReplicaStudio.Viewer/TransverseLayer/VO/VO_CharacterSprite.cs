using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using Microsoft.Xna.Framework;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Shared.DatasLayer;
using System.Threading;
using ReplicaStudio.Viewer.TransverseLayer.Interfaces;
using ReplicaStudio.Viewer.PresentationLayer;
using System.Collections;
using ReplicaStudio.Viewer.TransverseLayer.Algorithms;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    public class VO_CharacterSprite : VO_Character, IEntity
    {
        #region Members
        private static Mutex mut = new Mutex();

        /// <summary>
        /// Liste d'animations
        /// </summary>
        List<VO_AnimatedSprite[]> _AnimationsStanding;

        /// <summary>
        /// Liste d'animations
        /// </summary>
        List<VO_AnimatedSprite[]> _AnimationsWalking;

        /// <summary>
        /// Liste d'animations
        /// </summary>
        List<VO_AnimatedSprite[]> _AnimationsTalking;

        /// <summary>
        /// Niveau de zoom
        /// </summary>
        Vector2 _Scale;

        /// <summary>
        /// Nombre de directions
        /// </summary>
        int _NbrDirections;

        /// <summary>
        /// Coordonnées de la Face
        /// </summary>
        Vector2 _FaceCoords;

        /// <summary>
        /// Type courant d'animation
        /// </summary>
        Guid _CurrentAnimationType;

        #region Threads
        /// <summary>
        /// Thread du pathfinder du joueur
        /// </summary>
        Thread _PlayerPathFinder;
        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Ancien Standing utilisé dans le cas d'un changement d'animation non loop
        /// </summary>
        public Guid OldStanding { get; set; }

        /// <summary>
        /// Ancien Walking utilisé dans le cas d'un changement d'animation non loop
        /// </summary>
        public Guid OldWalking { get; set; }

        /// <summary>
        /// Ancien Talking utilisé dans le cas d'un changement d'animation non loop
        /// </summary>
        public Guid OldTalking { get; set; }

        /// <summary>
        /// Member is talking
        /// </summary>
        public bool IsTalking { get; set; }

        /// <summary>
        /// Déplacement courant
        /// 
        /// </summary>
        public List<Point> CurrentPath { get; set; }

        /// <summary>
        /// Type d'animation courante
        /// </summary>
        public Enums.CharacterAnimationType CurrentCharacterAnimationType
        {
            get
            {
                if (IsTalking)
                    return Enums.CharacterAnimationType.Talking;
                if (CurrentPath != null)
                    return Enums.CharacterAnimationType.Walking;
                return Enums.CharacterAnimationType.Standing;
            }
        }

        /// <summary>
        /// Sprite à afficher
        /// </summary>
        public VO_AnimatedSprite Sprites
        {
            get
            {
                if (AllDirectionsSprites == null)
                    return null;

                VO_AnimatedSprite animatedSprite = AllDirectionsSprites[(int)CurrentDirection];
                if(animatedSprite.CurrentSpriteIndex == animatedSprite.SpritesCount - 1)
                {
                    if (IsTalking && this.OldTalking != Guid.Empty)
                    {
                        this.TalkingAnim = this.OldTalking;
                        this.OldTalking = Guid.Empty;
                        this.CharacterTalk();
                    }
                    else if (this.CurrentPath != null && this.OldWalking != Guid.Empty)
                    {
                        this.WalkingAnim = this.OldWalking;
                        this.OldWalking = Guid.Empty;
                        this.CharacterWalk();
                    }
                    else if (!IsTalking && this.CurrentPath == null && this.OldStanding != Guid.Empty)
                    {
                        this.StandingAnim = this.OldStanding;
                        this.OldStanding = Guid.Empty;
                        this.CharacterStand();
                    }
                }
                VO_AnimatedSprite animSprite = AllDirectionsSprites[(int)CurrentDirection];
                VO_Animation anim = GameCore.Instance.GetCharAnimationById(this.CharacterId, animSprite.AnimationId);

                int scaledW = (int)((float)anim.OriginPoint.X * _Scale.X);
                int scaledH = (int)((float)anim.OriginPoint.Y * _Scale.X);
                animSprite.SetPosition(this.Location.X - scaledW, this.Location.Y - scaledH);

                return AllDirectionsSprites[(int)CurrentDirection];
            }
        }

        /// <summary>
        /// Série des directions du sprite en cours
        /// </summary>
        public VO_AnimatedSprite[] AllDirectionsSprites
        {
            get;
            set;
        }

        /// <summary>
        /// Animation de la face courante
        /// </summary>
        public VO_AnimatedSprite FaceAnim
        {
            get;
            set;
        }

        /// <summary>
        /// Page courante executée
        /// </summary>
        public int CurrentExecutingPage { get; set; }

        /// <summary>
        /// Type courant d'animation
        /// </summary>
        public Guid CurrentAnimationType
        {
            get { return _CurrentAnimationType; }
        }

        /// <summary>
        /// Id character
        /// </summary>
        public Guid CharacterId { get; set; }

        /// <summary>
        /// Location du character
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Direction actuelle
        /// </summary>
        public Enums.Movement CurrentDirection { get; set; }

        /// <summary>
        /// Zoom
        /// </summary>
        public Vector2 Scale { get { return _Scale; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur pour pnj
        /// </summary>
        public VO_CharacterSprite(VO_StageCharacter source)
        {
            _AnimationsStanding = new List<VO_AnimatedSprite[]>();
            _AnimationsWalking = new List<VO_AnimatedSprite[]>();
            _AnimationsTalking = new List<VO_AnimatedSprite[]>();
            VO_Character character = GameCore.Instance.GetCharacterById(source.CharacterId);

            this.Face = character.Face;
            this.CharacterId = character.Id;
            this.Id = source.Id;
            this.DialogColor = character.DialogColor;
            if (character.Face != Guid.Empty)
                FaceAnim = new VO_AnimatedSprite(character.Face, Guid.Empty, Enums.AnimationType.CharacterFace, 0, 0, Constants.ViewerEnums.ImageResourceType.Screen);
            this.Speed = character.Speed;
            this.StandingAnim = character.StandingAnim;
            this.TalkingAnim = character.TalkingAnim;
            this.Title = source.Title;
            this.WalkingAnim = character.WalkingAnim;

            _NbrDirections = GameCore.Instance.Game.Project.MovementDirections;

            foreach (VO_Animation anim in character.Animations)
            {
                VO_AnimatedSprite[] animationsStanding = new VO_AnimatedSprite[GameCore.Instance.Game.Project.MovementDirections];
                VO_AnimatedSprite[] animationsWalking = new VO_AnimatedSprite[GameCore.Instance.Game.Project.MovementDirections];
                VO_AnimatedSprite[] animationsTalking = new VO_AnimatedSprite[GameCore.Instance.Game.Project.MovementDirections];
                for (int i = 0; i < GameCore.Instance.Game.Project.MovementDirections; i++)
                {
                    animationsStanding[i] = new VO_AnimatedSprite(anim.Id, source.CharacterId, Enums.AnimationType.CharacterAnimation, 0, 0, Constants.ViewerEnums.ImageResourceType.Screen, i);
                    animationsWalking[i] = new VO_AnimatedSprite(anim.Id, source.CharacterId, Enums.AnimationType.CharacterAnimation, 0, 0, Constants.ViewerEnums.ImageResourceType.Screen, i);
                    animationsTalking[i] = new VO_AnimatedSprite(anim.Id, source.CharacterId, Enums.AnimationType.CharacterAnimation, 0, 0, Constants.ViewerEnums.ImageResourceType.Screen, i);
                }
                _AnimationsStanding.Add(animationsStanding);
                _AnimationsWalking.Add(animationsWalking);
                _AnimationsTalking.Add(animationsTalking);
            }
            _Scale = new Vector2(1, 1);

            CurrentExecutingPage = -1;

            VO_Animation anima = GameCore.Instance.GetCharAnimationById(this.CharacterId, this.StandingAnim);
            SetPosition(source.Location.X + anima.OriginPoint.X, source.Location.Y + anima.OriginPoint.Y);

            SetCurrentAnimation(Enums.CharacterAnimationType.Standing, this.StandingAnim);
        }

        /// <summary>
        /// Constructeur pour joueur
        /// </summary>
        public VO_CharacterSprite(VO_Character source, Enums.Movement startingPos, VO_Coords coords)
        {
            _AnimationsStanding = new List<VO_AnimatedSprite[]>();
            _AnimationsWalking = new List<VO_AnimatedSprite[]>();
            _AnimationsTalking = new List<VO_AnimatedSprite[]>();

            this.Face = source.Face;
            this.CharacterId = source.Id;
            this.Id = source.Id;
            this.DialogColor = source.DialogColor;
            if (source.Face != Guid.Empty)
                FaceAnim = new VO_AnimatedSprite(source.Face, Guid.Empty, Enums.AnimationType.CharacterFace, 0, 0, Constants.ViewerEnums.ImageResourceType.Permanent);
            this.Speed = source.Speed;
            this.StandingAnim = source.StandingAnim;
            this.TalkingAnim = source.TalkingAnim;
            this.Title = source.Title;
            this.WalkingAnim = source.WalkingAnim;

            _NbrDirections = GameCore.Instance.Game.Project.MovementDirections;

            foreach (VO_Animation anim in source.Animations)
            {
                VO_AnimatedSprite[] animationsStanding = new VO_AnimatedSprite[GameCore.Instance.Game.Project.MovementDirections];
                VO_AnimatedSprite[] animationsWalking = new VO_AnimatedSprite[GameCore.Instance.Game.Project.MovementDirections];
                VO_AnimatedSprite[] animationsTalking = new VO_AnimatedSprite[GameCore.Instance.Game.Project.MovementDirections];
                for (int i = 0; i < GameCore.Instance.Game.Project.MovementDirections; i++)
                {
                    animationsStanding[i] = new VO_AnimatedSprite(anim.Id, source.Id, Enums.AnimationType.CharacterAnimation, 0, 0, Constants.ViewerEnums.ImageResourceType.Permanent, i);
                    animationsWalking[i] = new VO_AnimatedSprite(anim.Id, source.Id, Enums.AnimationType.CharacterAnimation, 0, 0, Constants.ViewerEnums.ImageResourceType.Permanent, i);
                    animationsTalking[i] = new VO_AnimatedSprite(anim.Id, source.Id, Enums.AnimationType.CharacterAnimation, 0, 0, Constants.ViewerEnums.ImageResourceType.Permanent, i);
                }
                _AnimationsStanding.Add(animationsStanding);
                _AnimationsWalking.Add(animationsWalking);
                _AnimationsTalking.Add(animationsTalking);
            }
            _Scale = new Vector2(1, 1);

            SetPosition(coords.Location.X, coords.Location.Y);
            CurrentExecutingPage = -1;
            CurrentDirection = startingPos;
            SetCurrentAnimation(Enums.CharacterAnimationType.Standing, this.StandingAnim);
        }
        #endregion

        #region Methods
        #region Methodes standard d'animation
        /// <summary>
        /// Passer à standing
        /// </summary>
        public void CharacterStand()
        {
            if (!IsTalking && CurrentPath == null)
                SetCurrentAnimation(Enums.CharacterAnimationType.Standing, StandingAnim);
        }

        /// <summary>
        /// Passer à walking
        /// </summary>
        public void CharacterWalk()
        {
            IsTalking = false;
            SetCurrentAnimation(Enums.CharacterAnimationType.Walking, WalkingAnim);
        }

        /// <summary>
        /// Passer à talk
        /// </summary>
        public void CharacterTalk()
        {
            if (CurrentPath == null)
            {
                SetCurrentAnimation(Enums.CharacterAnimationType.Talking, TalkingAnim);
                IsTalking = true;
            }
        }

        /// <summary>
        /// Stop talk
        /// </summary>
        public void CharacterStopTalking()
        {
            IsTalking = false;
        }
        #endregion

        /// <summary>
        /// Move perso
        /// </summary>
        /// <param name="location">Locations</param>
        public void MoveCharacter(Point location)
        {
            if (_PlayerPathFinder != null && _PlayerPathFinder.IsAlive)
                _PlayerPathFinder.Join();
            this.StopPath();
            if (location.X == this.Location.X && location.Y == this.Location.Y)
                return;
            _PlayerPathFinder = new Thread(new ParameterizedThreadStart(FindPath));
            _PlayerPathFinder.Start(new VO_FindPathCoords(this.Location, new Point(location.X, location.Y)));
        }

        /// <summary>
        /// Freeze une animation dans toutes les directions
        /// </summary>
        /// <param name="animation"></param>
        public void FreezeAnimation(Guid animation)
        {
            foreach (VO_AnimatedSprite[] anims in _AnimationsStanding)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.Frozen = true;
                    }
                }
            }

            foreach (VO_AnimatedSprite[] anims in _AnimationsWalking)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.Frozen = true;
                    }
                }
            }

            foreach (VO_AnimatedSprite[] anims in _AnimationsTalking)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.Frozen = true;
                    }
                }
            }
        }

        /// <summary>
        /// Défreeze une animation dans toutes les directions
        /// </summary>
        /// <param name="animation"></param>
        public void FreeAnimation(Guid animation)
        {
            foreach (VO_AnimatedSprite[] anims in _AnimationsStanding)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.Frozen = false;
                    }
                }
            }

            foreach (VO_AnimatedSprite[] anims in _AnimationsWalking)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.Frozen = false;
                    }
                }
            }

            foreach (VO_AnimatedSprite[] anims in _AnimationsTalking)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.Frozen = false;
                    }
                }
            }
        }

        /// <summary>
        /// Change la fréquence d'une anim
        /// </summary>
        /// <param name="animation"></param>
        public void SetAnimationFrequency(Guid animation, int frequency)
        {
            foreach (VO_AnimatedSprite[] anims in _AnimationsStanding)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.SetFrequency(frequency);
                    }
                }
            }

            foreach (VO_AnimatedSprite[] anims in _AnimationsWalking)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.SetFrequency(frequency);
                    }
                }
            }

            foreach (VO_AnimatedSprite[] anims in _AnimationsTalking)
            {
                if (anims[0].AnimationId == animation)
                {
                    foreach (VO_AnimatedSprite anim in anims)
                    {
                        anim.SetFrequency(frequency);
                    }
                }
            }
        }

        #region PathFinder
        /// <summary>
        /// Cherche un pathfinder
        /// </summary>
        /// <param name="start">Point d'entrée</param>
        /// <param name="end">Point d'arrivée</param>
        /// <returns>List des noeuds du chemin résolu</returns>
        public List<PathFinderNode> FindPath(Point start, Point end, int matrixPrecision)
        {
            start = new Point(start.X / matrixPrecision, start.Y / matrixPrecision);
            end = new Point(end.X / matrixPrecision, end.Y / matrixPrecision);
            return MatrixManager.CurrentStage.WalkAlgo.FindPath(start, end);
        }

        /// <summary>
        /// PathFinder
        /// </summary>
        /// <param name="findpath"></param>
        private void FindPath(object findpath)
        {
            mut.WaitOne();
            VO_FindPathCoords coords = (VO_FindPathCoords)findpath;
            List<PathFinderNode> path = FindPath(coords.Start, coords.End, GameCore.Instance.Game.Project.Resolution.MatrixPrecision);
            if (path != null)
            {
                this.StartPath(path, GameCore.Instance.Game.Project.Resolution.MatrixPrecision);
            }
            mut.ReleaseMutex();
            return;
        }
        #endregion

        /// <summary>
        /// Setter l'animation courante
        /// </summary>
        /// <param name="anim"></param>
        public void SetCurrentAnimation(Enums.CharacterAnimationType type, Guid anim)
        {
            if (_CurrentAnimationType != anim)
            {
                switch(type)
                {
                    case Enums.CharacterAnimationType.Standing:
                        AllDirectionsSprites = _AnimationsStanding.Find(p => p[0].AnimationId == anim);
                        break;
                    case Enums.CharacterAnimationType.Walking:
                        AllDirectionsSprites = _AnimationsWalking.Find(p => p[0].AnimationId == anim);
                        break;
                    case Enums.CharacterAnimationType.Talking:
                        AllDirectionsSprites = _AnimationsTalking.Find(p => p[0].AnimationId == anim);
                        break;
                }
                if (AllDirectionsSprites == null)
                    _CurrentAnimationType = StandingAnim;
                else
                    _CurrentAnimationType = anim;
                if (AllDirectionsSprites != null)
                {
                    foreach (VO_AnimatedSprite animSprite in AllDirectionsSprites)
                    {
                        int scaledW = (int)((float)animSprite.Width * _Scale.X);
                        int scaledH = (int)((float)animSprite.Height * _Scale.X);
                        animSprite.SetPosition(Location.X - scaledW / 2, Location.Y - scaledH / 2);
                    }
                }
            }
        }

        /// <summary>
        /// Surcharger la fréquence
        /// </summary>
        /// <param name="frequency"></param>
        public void SetCurrentAnimationFrequency(int frequency)
        {
            if (AllDirectionsSprites != null)
            {
                foreach (VO_AnimatedSprite anim in AllDirectionsSprites)
                {
                    if (anim != null)
                    {
                        if (anim.Frequency == frequency)
                            break;
                        anim.SetFrequency(frequency);
                    }
                }
            }
        }

        /// <summary>
        /// Vérifie qu'un point touche le perso
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool PointIsInCharacter(Point point)
        {
            VO_Animation anim = GameCore.Instance.GetCharAnimationById(this.CharacterId, this.CurrentAnimationType);
            Point animPos = GetAnimPosition();
            Rectangle sizeChar = new Rectangle(animPos.X, animPos.Y, (int)((float)anim.SpriteWidth * _Scale.X), ((int)((float)anim.SpriteHeight * _Scale.Y)));
            if (sizeChar.Contains(point))
            {
                VO_Sprite sprite = this.AllDirectionsSprites[(int)this.CurrentDirection].Sprite;
                if(sprite != null)
                {
                    /*Point relativePoint = new Point(point.X - animPos.X, point.Y - animPos.Y);
                    Rectangle source = new Rectangle(sprite.Source.X + relativePoint.X, sprite.Source.Y + relativePoint.Y, 1, 1);
                    Color[] colors1D = new Color[1];
                    sprite.Image.GetData(0, source, colors1D, 0, 1);

                    Color color = colors1D[0];
                    if (color.A != 0 || color.R != 0 || color.G != 0 || color.B != 0)
                    {
                        return true;
                    }

                    Sprite*/
                    return true;

                    /*for (int x = 0; x < sprite.Width; x++)
                    {
                        if (relativePoint.X == x)
                        {
                            for (int y = 0; y < sprite.Height; y++)
                            {
                                if (relativePoint.Y == y)
                                {
                                    Color color = colors1D[x + y * sprite.Width];
                                    if (color.A != 255)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }*/
                }
            }
            return false;
        }

        /// <summary>
        /// Changer la direction
        /// </summary>
        /// <param name="i"></param>
        public void ChangeDirection(Enums.Movement i)
        {
            CurrentDirection = i;
        }

        /// <summary>
        /// Changer la position de l'animation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            Location = new Point(x, y);
        }

        /// <summary>
        /// Récupérer la position en 0 de l'anim
        /// </summary>
        public Point GetAnimPosition()
        {
            if (AllDirectionsSprites != null)
            {
                foreach (VO_AnimatedSprite animSprite in AllDirectionsSprites)
                {
                    VO_Animation anim = GameCore.Instance.GetCharAnimationById(this.CharacterId, animSprite.AnimationId);

                    int scaledW = (int)((float)anim.OriginPoint.X * _Scale.X);
                    int scaledH = (int)((float)anim.OriginPoint.Y * _Scale.X);

                    return new Point(this.Location.X - scaledW, this.Location.Y - scaledH);
                }
            }
            return Location;
        }

        /// <summary>
        /// Change le Scale de l'animation
        /// </summary>
        /// <param name="value"></param>
        public void SetScale(Vector2 value)
        {
            _Scale = value;
            foreach (VO_AnimatedSprite[] animArray in _AnimationsStanding)
                foreach (VO_AnimatedSprite anim in animArray)
                    anim.SetScale(value);
            foreach (VO_AnimatedSprite[] animArray in _AnimationsWalking)
                foreach (VO_AnimatedSprite anim in animArray)
                    anim.SetScale(value);
            foreach (VO_AnimatedSprite[] animArray in _AnimationsTalking)
                foreach (VO_AnimatedSprite anim in animArray)
                    anim.SetScale(value);
        }

        /// <summary>
        /// Changer la couleur
        /// </summary>
        /// <param name="color">Objet transformations de couleur</param>
        public void SetColor(VO_ColorTransformation color)
        {
            foreach (VO_AnimatedSprite[] animArray in _AnimationsStanding)
                foreach (VO_AnimatedSprite anim in animArray)
                    anim.SetColor(color);
            foreach (VO_AnimatedSprite[] animArray in _AnimationsWalking)
                foreach (VO_AnimatedSprite anim in animArray)
                    anim.SetColor(color);
            foreach (VO_AnimatedSprite[] animArray in _AnimationsTalking)
                foreach (VO_AnimatedSprite anim in animArray)
                    anim.SetColor(color);
        }

        /// <summary>
        /// Commence à suivre un chemin
        /// </summary>
        /// <param name="path"></param>
        public void StartPath(List<PathFinderNode> path, int matrixPrecision)
        {
            List<Point> points = new List<Point>();

            bool first = true;
            int previousX = 0;
            int previousY = 0;

            if (matrixPrecision == 1)
            {
                foreach (PathFinderNode node in path)
                {
                    points.Add(((Point)(new Point(node.X, node.Y))));
                }
            }
            else
            {
                foreach (PathFinderNode node in path)
                {
                    int x = node.PX * matrixPrecision;
                    int y = node.PY * matrixPrecision;
                    if (first)
                    {
                        first = false;
                        previousX = x;
                        previousY = y;
                        points.Add(new Point(x, y));
                    }
                    else
                    {
                        IEnumerable enumerable = Tools.RenderLine(new Point(previousX, previousY), new Point(x, y));
                        IEnumerator enumerator = enumerable.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            points.Add(((Point)(enumerator.Current)));
                        }
                        //points.Add(new Point(x, y));
                        previousX = x;
                        previousY = y;
                    }
                }
            }


            CurrentPath = points;
            CharacterWalk();
        }

        /// <summary>
        /// Arrête un mouvement
        /// </summary>
        public void StopPath()
        {
            CurrentPath = null;
            CharacterStand();
        }

        /// <summary>
        /// Reset l'index de l'animation courante
        /// </summary>
        public void ResetAnimationIndex()
        {
            foreach (VO_AnimatedSprite sprite in AllDirectionsSprites)
            {
                sprite.CurrentSpriteIndex = 0;
            }
        }

        /// <summary>
        /// Récupère la prochaine direction du pathfinder
        /// </summary>
        public void GetNextPosition()
        {
            if (CurrentPath != null && CurrentPath.Count > 0)
            {
                if (CurrentAnimationType != WalkingAnim)
                    SetCurrentAnimation(Enums.CharacterAnimationType.Walking, this.WalkingAnim);

                //Nouveau déplacement
                int move = this.Speed;
                if (CurrentPath.Count < move)
                    move = CurrentPath.Count;
                Point node = CurrentPath[CurrentPath.Count - move];

                //Direction
                int movX = node.X - Location.X;
                int movY = node.Y - Location.Y;
                if (movX >= 1)
                {
                    if (movY >= 1 && _NbrDirections == 8)
                    {
                        CurrentDirection = Enums.Movement.DownRight;
                    }
                    else if (movY <= -1 && _NbrDirections == 8)
                    {
                        CurrentDirection = Enums.Movement.UpRight;
                    }
                    else
                    {
                        CurrentDirection = Enums.Movement.Right;
                    }
                }
                else if (movX <= -1)
                {
                    if (movY >= 1 && _NbrDirections == 8)
                    {
                        CurrentDirection = Enums.Movement.DownLeft;
                    }
                    else if (movY <= -1 && _NbrDirections == 8)
                    {
                        CurrentDirection = Enums.Movement.UpLeft;
                    }
                    else
                    {
                        CurrentDirection = Enums.Movement.Left;
                    }
                }
                else
                {
                    if (movY >= 1)
                    {
                        CurrentDirection = Enums.Movement.Down;
                    }
                    else if (movY <= -1)
                    {
                        CurrentDirection = Enums.Movement.Up;
                    }
                }

                //Setter le positionement
                SetPosition(node.X, node.Y);
                for (int i = 0; i < move; i++)
                {
                    CurrentPath.RemoveAt(CurrentPath.Count - 1);
                    if (CurrentPath.Count == 0)
                    {
                        CurrentPath = null;
                        SetCurrentAnimation(Enums.CharacterAnimationType.Standing, this.StandingAnim);
                        break;
                    }
                }
            }
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Détruire l'objet
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }
}
