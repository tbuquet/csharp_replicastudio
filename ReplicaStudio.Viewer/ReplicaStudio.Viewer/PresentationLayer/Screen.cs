using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.ServiceLayer;
using Microsoft.Xna.Framework;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using Microsoft.Xna.Framework.Graphics;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer;
using Microsoft.Xna.Framework.Input;

namespace ReplicaStudio.Viewer.PresentationLayer
{
    /// <summary>
    /// Classe de base Screen
    /// </summary>
    public class Screen : DrawableGameComponent
    {
        #region Members
        /// <summary>
        /// Référence au business de base des écrans
        /// </summary>
        protected static ScreenService _ScreenBusiness;

        /// <summary>
        /// Données projet
        /// </summary>
        protected static VO_Project _ProjectData;

        /// <summary>
        /// Données Terminology
        /// </summary>
        protected static VO_Terminology _TerminologyData;

        /// <summary>
        /// Données menu
        /// </summary>
        protected static VO_Menu _MenuData;

        /// <summary>
        /// Référence musique
        /// </summary>
        protected string _Music;

        /// <summary>
        /// Référence fréquence musique
        /// </summary>
        protected float _MusicFrequency = 100.0f;

        /// <summary>
        /// SpriteBatch
        /// </summary>
        protected SpriteBatch _SpriteBatch;
        #endregion

        #region Delegates
        /// <summary>
        /// Delegate ChangeScreen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ChangeScreenEventHandler(object sender, GameScreenEventArgs e);
        #endregion

        #region Events
        /// <summary>
        /// Changement de screen
        /// </summary>
        public event ChangeScreenEventHandler ChangingScreen;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="game"></param>
        public Screen(Game game, SpriteBatch spriteBatch) 
            : base(game)
        {
            _SpriteBatch = spriteBatch;

            //1 - Couche métier
            _ScreenBusiness = new ScreenService();

            //2 - Récupérer les informations projet
            _MenuData = _ScreenBusiness.GetMenuData();
            _ProjectData = _ScreenBusiness.GetProjectData();
            _TerminologyData = _ScreenBusiness.GetTerminologyData();  
        }
        #endregion

        #region Methods
        /// <summary>
        /// Demande de changement de d'écran
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangeScreen(object sender, GameScreenEventArgs e)
        {
            if (ChangingScreen != null)
            {
                this.ChangingScreen(this, e);
            }
        }

        #region Surchages Draw
        public virtual void Draw(VO_Sprite sprite)
        {
            _SpriteBatch.Draw(sprite.Image, sprite.Position, sprite.Source, Color.White, 0f, Vector2.Zero, sprite.Scale, SpriteEffects.None, 0f);
        }

        public virtual void Draw(VO_AnimatedSprite animatedObjectToDraw)
        {
            Draw(animatedObjectToDraw, true);
        }

        public virtual void Draw(VO_AnimatedSprite animatedObjectToDraw, float scale, bool animate)
        {
            Draw(animatedObjectToDraw, animate);
            if (scale != animatedObjectToDraw.Scale.X)
                animatedObjectToDraw.SetScale(new Vector2(scale, scale));
        }

        public virtual void Draw(VO_CharacterSprite characterToDraw, float scale)
        {
            Draw(characterToDraw, scale, true);
        }

        public virtual void Draw(VO_CharacterSprite characterToDraw, float scale, bool move)
        {
            Draw(characterToDraw.Sprites, move);
            if (move)
            {
                if (scale != characterToDraw.Scale.X)
                {
                    characterToDraw.SetScale(new Vector2(scale, scale));
                    characterToDraw.SetPosition(characterToDraw.Location.X, characterToDraw.Location.Y);
                }
                characterToDraw.GetNextPosition();
            }
        }

        /// <summary>
        /// Dessiner sur scène
        /// </summary>
        /// <param name="objectToDraw"></param>
        public virtual void Draw(List<VO_AnimatedSprite> animations)
        {
            if (animations != null)
            {
                foreach (VO_AnimatedSprite anim in animations)
                {
                    Draw(anim);
                }
            }
        }

        /// <summary>
        /// Dessiner sur scène
        /// </summary>
        /// <param name="objectToDraw"></param>
        public virtual void Draw(List<VO_String2D> textes)
        {
            if (textes != null)
                foreach (VO_String2D text in textes)
                    _SpriteBatch.DrawString(text.SpriteFont, text.Text, text.Position, text.Color);
        }

        /// <summary>
        /// Dessiner sur scène
        /// </summary>
        /// <param name="menu"></param>
        public virtual void Draw(GameTime gameTime, VO_SelectableMenu menu)
        {
            if (menu != null)
            {
                menu.Draw(gameTime);
            }
        }


        /// <summary>
        /// Dessiner sur scène
        /// </summary>
        /// <param name="animatedObjectToDraw"></param>
        public virtual void Draw(VO_AnimatedSprite animatedObjectToDraw, bool animate)
        {
            //Cas particulier de l'animation
            if (animatedObjectToDraw != null && animatedObjectToDraw.Sprite != null && animatedObjectToDraw.Sprite.Image != null)
            {
                //Si l'animation est conforme, on l'affiche
                Draw(animatedObjectToDraw.Sprite);
                if (animate)
                    animatedObjectToDraw.GetNextSprite();
            }
            else if (animatedObjectToDraw != null && animatedObjectToDraw.Sprite != null && animatedObjectToDraw.Sprite.Image != null)
            {
                //Si les ressources ont été détruites, on regénére l'animation
                animatedObjectToDraw.RegenerateAnim();
                Draw(animatedObjectToDraw, animate);
            }
        }
        #endregion
        #endregion

        #region Inherited Methods
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Mets à jour le son
            SoundManager.Update();
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //Gestion du Input
            if (ScriptManager.ScriptUserControls)
            {
                #region Gestion souris
                MouseState mouseState = Mouse.GetState();
                if (MouseManager.IsInWindow())
                {
                    if (MouseManager.OnLeftClick)
                        MouseLeftPress(mouseState);
                    if (MouseManager.OnRightClick)
                        MouseRightPress(mouseState);
                    if (MouseManager.OnLeftReleased)
                        MouseLeftReleased(mouseState);
                    if (MouseManager.OnRightReleased)
                        MouseRightReleased(mouseState);
                    if (MouseManager.Moved())
                        MouseMove(mouseState);
                }
                MouseManager.Update();
                #endregion

                #region Gestion keyboard
                KeyboardManager.Update();
                if (KeyboardManager.StateChanged)
                    KeyPress();
                #endregion
            }

            //Dessiner Screen
            DrawScene(gameTime);

            //Mise à jour de la position souris
            ActionManager.SetPosition(MouseManager.MouseX, MouseManager.MouseY);

            //Gestion curseur
            if (!DebugConsole.Visible && ScriptManager.ScriptUserControls)
            {
                _SpriteBatch.Begin();
                //Curseur
                if (ActionManager.ClickedState)
                    Draw(ActionManager.CurrentActionSprite[2]);
                else
                    Draw(ActionManager.CurrentActionSprite[1]);
                _SpriteBatch.End();
            }

            //Changement de scène
            if (ScriptManager.GoToScreen != null && ScriptManager.CurrentScript == null)
                ChangeScreen(this, ScriptManager.GoToScreen);
        }

        
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Dessiner la scène
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void DrawScene(GameTime gameTime)
        {
        }

        #region Input
        /// <summary>
        /// KeyPress
        /// </summary>
        public virtual void KeyPress()
        {
        }

        /// <summary>
        /// MouseLeftPress
        /// </summary>
        /// <param name="mouseState"></param>
        public virtual void MouseLeftPress(MouseState mouseState)
        {
        }

        /// <summary>
        /// MouseRightPress
        /// </summary>
        /// <param name="mouseState"></param>
        public virtual void MouseRightPress(MouseState mouseState)
        {
        }
        
        /// <summary>
        /// MouseLeftReleased
        /// </summary>
        /// <param name="mouseState"></param>
        public virtual void MouseLeftReleased(MouseState mouseState)
        {
        }

        /// <summary>
        /// MouseRightReleased
        /// </summary>
        /// <param name="mouseState"></param>
        public virtual void MouseRightReleased(MouseState mouseState)
        {
        }

        /// <summary>
        /// MouseMove
        /// </summary>
        /// <param name="mouseState"></param>
        public virtual void MouseMove(MouseState mouseState)
        {
        }
        #endregion

        #region Load/Unload
        /// <summary>
        /// Load Screen
        /// </summary>
        public virtual void LoadScreen()
        {
            ScriptManager.GoToScreen = null;

            //Cursor
            ActionManager.SetCurrentActionToGo();

            ActionManager.ClickedState = false;
        }

        /// <summary>
        /// Lance la musique
        /// </summary>
        public virtual void LaunchMusic()
        {
            //Démarrer la musique
            SoundManager.EndMusic -= new SoundManager.EndMusicEventHandler(SoundManager_EndMusic);
            if (string.IsNullOrEmpty(_Music))
                SoundManager.StopMusic();
            else
            {
                SoundManager.PlayMusic(_Music);
                SoundManager.SetFrequency(_MusicFrequency);
            }
            SoundManager.EndMusic += new SoundManager.EndMusicEventHandler(SoundManager_EndMusic);
        }

        /// <summary>
        /// Loop music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SoundManager_EndMusic(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_Music))
                SoundManager.PlayMusic(_Music);
        }

        /// <summary>
        /// Unload Screen
        /// </summary>
        public virtual void UnloadScreen()
        {
            SpriteManager.FreeScreenSprite();
            if(ImageManager.CurrentStage != null)
                ImageManager.CurrentStage.FreeScreenImages();

            SoundManager.EndMusic -= new SoundManager.EndMusicEventHandler(SoundManager_EndMusic);
        }
        #endregion
        #endregion
    }
}
