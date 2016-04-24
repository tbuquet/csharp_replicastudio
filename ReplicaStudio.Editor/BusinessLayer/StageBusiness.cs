using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using System.Drawing;
using ReplicaStudio.Editor.TransverseLayer;
using System.Drawing.Imaging;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.Drawing.Drawing2D;
using ReplicaStudio.TransverseLayer;
using System.IO;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.Editor.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère le panneau de la scène
    /// </summary>
    public class StageBusiness : BaseBusiness
    {
        #region Members
        #region Ressources permanentes
        /// <summary>
        /// Ressource du background
        /// </summary>
        VO_BackgroundSerial _StageBackground = null;

        /// <summary>
        /// Matrice de couleur pour les calques inférieurs
        /// </summary>
        QColorMatrix _InferiorMatrix = null;

        /// <summary>
        /// Attributs pour les images des calques inférieurs
        /// </summary>
        ImageAttributes _InferiorAttributes;

        /// <summary>
        /// Pinceau pour vecteurs
        /// </summary>
        Pen[] _Pens;

        /// <summary>
        /// Brushes pour vecteurs
        /// </summary>
        Brush[] _Brushes;

        /// <summary>
        /// Dictionnaire des couleurs des hotspots
        /// </summary>
        Dictionary<Guid, Brush> _HotspotColors;
        #endregion

        #region Ressources d'optimisation
        /// <summary>
        /// Image mémoire qui enregistre l'état de tous les calques précédants le calque courant
        /// </summary>
        Image _InfLayersSurface = null;

        /// <summary>
        /// Image mémoire qui enregistre l'état de tous les calques suivants le calque courant
        /// </summary>
        Image _SupLayersSurface = null;

        /// <summary>
        /// Booléen qui permet de savoir s'il est utile de dessiner _SupLayersSurface.
        /// </summary>
        bool _SupSurfaceUsed = false;

        /// <summary>
        /// Décors recolorées enregistrées du calque courant
        /// </summary>
        Dictionary<string, Image> _ColoredDecors = null;

        /// <summary>
        /// Animations recolorées enregistrées du calque courant
        /// </summary>
        Dictionary<string, Image> _ColoredAnimations = null;

        /// <summary>
        /// Characters recolorés enregistrés du calque courant
        /// </summary>
        Dictionary<string, Image> _ColoredCharacters = null;
        #endregion

        #region Ressources du calque courant [Uniquement pour Decors/Animations/Characters
        /// <summary>
        /// Matrice de couleur du calque courant
        /// </summary>
        ColorMatrix _Matrix = null;

        /// <summary>
        /// Attributs pour les images courantes
        /// </summary>
        ImageAttributes _Attributes;

        /// <summary>
        /// Attributs pour transparence
        /// </summary>
        ImageAttributes _Opacity;
        #endregion

        /// <summary>
        /// Positions sauvegardés des objets lors d'un drag & drop
        /// </summary>
        Dictionary<Guid, Point> _SavedPositions = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public StageBusiness()
        {
            _Attributes = new ImageAttributes();
            _Opacity = new ImageAttributes();
            _Matrix = new ColorMatrix();
            _InferiorMatrix = new QColorMatrix();
            _InferiorAttributes = new ImageAttributes();
            _HotspotColors = new Dictionary<Guid, Brush>();
            _Pens = FormsTools.GetMasksColors();
            _Brushes = FormsTools.GetMasksFillingColors();

            _InferiorMatrix.SetSaturation2(0.02f);
            _InferiorAttributes.SetColorMatrix(_InferiorMatrix.ToColorMatrix(), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }
        #endregion

        #region Methods
        #region Affichage Scène
        /// <summary>
        /// Charge un background vide en temps que ressource permanente
        /// </summary>
        /// <param name="pSizeContainer">Taille de la surface totale</param>
        /// <param name="pSizeStage">Taille de la scène</param>
        /// <returns>Surface</returns>
        public void LoadEmptyBackground(Size sizeContainer, Size sizeStage)
        {
            _StageBackground = new VO_BackgroundSerial(sizeStage, EditorSettings.Instance.TransparentBlockSize / EditorHelper.Instance.CurrentZoom, EditorSettings.Instance.StagePadding);
            ImageManager.CreateNewImageBackground(_StageBackground);

            //Optimisations
            LoadMinusMaximusLayers();
        }

        /// <summary>
        /// Rafraichi la scène
        /// </summary>
        /// <returns>Surface</returns>
        public Image RefreshStage()
        {
            VO_Stage stage = EditorHelper.Instance.GetCurrentStageInstance();
            stage.ListLayers.Sort();

            Image final = (Image)_InfLayersSurface.Clone();
            Graphics graphic = Graphics.FromImage(final);
            graphic.ResetTransform();
            graphic.CompositingQuality = CompositingQuality.HighSpeed;
            graphic.SmoothingMode = SmoothingMode.HighSpeed;
            graphic.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            //LAYERS POUR DECOR/ANIMATION
            foreach (VO_Layer layer in stage.ListLayers)
            {
                if (layer.Id == EditorHelper.Instance.CurrentLayer && !layer.Hidden)
                {
                    //Application des paramètres Couleur du calque
                    LoadNewMatrix(layer);

                    if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Decors)
                    {
                        RefreshDecorLayer(layer, graphic);
                    }
                    else if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Objects)
                    {
                        RefreshAnimationLayer(layer, graphic);
                    }

                    //Reset de la matrice de la couleur
                    ResetMatrix();
                }
            }
            //CHARACTERS
            if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Characters)
            {
                RefreshCharacterLayer(stage, graphic);
            }

            if (_SupSurfaceUsed)
                graphic.DrawImage(_SupLayersSurface, new Rectangle(new Point(0, 0), new Size(_SupLayersSurface.Width, _SupLayersSurface.Height)));

            graphic.Dispose();
            return final;
        }

        /// <summary>
        /// Rafraichi un calque
        /// </summary>
        /// <param name="pLayer">Calque</param>
        /// <param name="pFinal">Surface final</param>
        private void RefreshDecorLayer(VO_Layer layer, Graphics graphics)
        {
            List<VO_StageObject> selectedObjects = new List<VO_StageObject>();
            string layerColorSerial = string.Empty;
            if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Decors)
                layerColorSerial = layer.ColorTransformations.ToString();

            //Décors
            foreach (VO_StageDecor decor in layer.ListDecors)
            {
                Image decorImage = null;
                if (!_ColoredDecors.ContainsKey(layerColorSerial + decor.Filename))
                {
                    //Mise en cache des textures
                    decorImage = ImageManager.GetImageStageDecor(GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + decor.Filename);
                    Image coloredSurface = new Bitmap(decorImage);
                    Graphics coloredGraphics = Graphics.FromImage(coloredSurface);
                    if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Decors)
                        coloredGraphics.DrawImage(decorImage, new Rectangle(new Point(0, 0), decor.Size), 0, 0, decorImage.Width, decorImage.Height, GraphicsUnit.Pixel, _Attributes);
                    else
                        coloredGraphics.DrawImage(decorImage, new Rectangle(new Point(0, 0), decor.Size), 0, 0, decorImage.Width, decorImage.Height, GraphicsUnit.Pixel, _InferiorAttributes);
                    _ColoredDecors.Add(layerColorSerial + decor.Filename, coloredSurface);
                    coloredGraphics.Dispose();
                }

                //Affichage
                decorImage = ImageManager.GetImageStageDecor(GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + decor.Filename);
                if (layer.ColorTransformations.Opacity == 100.0f)
                {
                    graphics.DrawImage(_ColoredDecors[layerColorSerial + decor.Filename], new Rectangle(SetObjectInStage(decor.Location), decor.Size), 0, 0, decorImage.Width, decorImage.Height, GraphicsUnit.Pixel);
                }
                else
                {
                    graphics.DrawImage(_ColoredDecors[layerColorSerial + decor.Filename], new Rectangle(SetObjectInStage(decor.Location), decor.Size), 0, 0, decorImage.Width, decorImage.Height, GraphicsUnit.Pixel, _Opacity);
                }

                //Sélection des objets
                if (EditorHelper.Instance.SelectedObjects.Contains(decor))
                    selectedObjects.Add(decor);
            }
        }

        /// <summary>
        /// Rafraichi un calque
        /// </summary>
        /// <param name="pLayer">Calque</param>
        /// <param name="pFinal">Surface final</param>
        private void RefreshAnimationLayer(VO_Layer layer, Graphics graphics)
        {
            List<VO_StageObject> selectedObjects = new List<VO_StageObject>();
            string layerColorSerial = layer.ColorTransformations.ToString();

            //Animations
            foreach (VO_StageAnimation animation in layer.ListAnimations)
            {
                string finalSerial = layerColorSerial + animation.AnimationId;
                VO_Animation animObject = GameCore.Instance.GetAnimationById(animation.AnimationId);
                animation.Location = animation.Location;
                animation.Size = new Size(animObject.SpriteWidth, animObject.SpriteHeight);
                if (!_ColoredAnimations.ContainsKey(finalSerial))
                {
                    //Mise en cache des textures
                    Image animImage = ImageManager.GetImageStageAnim(animObject.Id);
                    Bitmap coloredSurface = new Bitmap(animImage);
                    Graphics coloredGraphics = Graphics.FromImage(coloredSurface);
                    coloredGraphics.DrawImage(animImage, new Rectangle(new Point(0, 0), new Size(animImage.Width, animImage.Height)), 0, 0, animImage.Width, animImage.Height, GraphicsUnit.Pixel, _Attributes);
                    _ColoredAnimations.Add(finalSerial, coloredSurface);
                    coloredGraphics.Dispose();
                }

                //Affichage
                if (layer.ColorTransformations.Opacity == 100.0f)
                {
                    graphics.DrawImage(_ColoredAnimations[finalSerial], new Rectangle(SetObjectInStage(animation.Location), animation.Size), 0, animObject.Row * animObject.SpriteHeight, animObject.SpriteWidth, animObject.SpriteHeight, GraphicsUnit.Pixel);
                }
                else
                {
                    graphics.DrawImage(_ColoredAnimations[finalSerial], new Rectangle(SetObjectInStage(animation.Location), animation.Size), 0, animObject.Row * animObject.SpriteHeight, animObject.SpriteWidth, animObject.SpriteHeight, GraphicsUnit.Pixel, _Opacity);
                }

                //Sélection des objets
                if (EditorHelper.Instance.SelectedObjects.Contains(animation))
                    selectedObjects.Add(animation);
            }
        }

        /// <summary>
        /// Rafraichi un calque
        /// </summary>
        /// <param name="pLayer">Calque</param>
        /// <param name="pFinal">Surface final</param>
        private void RefreshCharacterLayer(VO_Stage stage, Graphics graphics)
        {
            List<VO_StageObject> selectedObjects = new List<VO_StageObject>();
            //string layerColorSerial = layer.ColorTransformations.ToString();

            //Décors
            foreach (VO_StageCharacter character in stage.ListCharacters)
            {
                //string finalSerial = layerColorSerial + character.AnimationId;
                VO_Character charObject = GameCore.Instance.GetCharacterById(character.CharacterId);
                character.AnimationId = charObject.StandingAnim;
                VO_Animation animObject = charObject.GetAnimationById(character.AnimationId);
                character.Location = character.Location;
                character.Size = new Size(animObject.SpriteWidth, animObject.SpriteHeight);
                /*if (!_ColoredCharacters.ContainsKey(finalSerial))
                {
                    //Mise en cache des textures
                    Image imageChar = ImageManager.GetImageStageChar(charObject.Id);
                    Bitmap coloredSurface = new Bitmap(imageChar);
                    Graphics coloredGraphics = Graphics.FromImage(coloredSurface);
                    coloredGraphics.DrawImage(imageChar, new Rectangle(new Point(0, 0), new Size(imageChar.Width, imageChar.Height)), 0, 0, imageChar.Width, imageChar.Height, GraphicsUnit.Pixel, _Attributes);
                    _ColoredCharacters.Add(finalSerial, coloredSurface);
                    coloredGraphics.Dispose();
                }*/

                //Affichage
                int selectedRow = (int)character.Event.PageList[0].CharacterDirection;
                /*if (layer.ColorTransformations.Opacity == 100.0f)
                {
                    graphics.DrawImage(_ColoredCharacters[finalSerial], new Rectangle(SetObjectInStage(character.Location), character.Size), 0, selectedRow * animObject.SpriteHeight, animObject.SpriteWidth, animObject.SpriteHeight, GraphicsUnit.Pixel);
                }
                else
                {*/
                graphics.DrawImage(ImageManager.GetImageStageChar(charObject.Id), new Rectangle(SetObjectInStage(character.Location), character.Size), 0, selectedRow * animObject.SpriteHeight, animObject.SpriteWidth, animObject.SpriteHeight, GraphicsUnit.Pixel, _Opacity);
                //}

                //Sélection des objets
                if (EditorHelper.Instance.SelectedObjects.Contains(character))
                    selectedObjects.Add(character);
            }
        }

        /// <summary>
        /// Charge les couleurs d'une matrice d'un calque
        /// </summary>
        /// <param name="pLayer">Calque</param>
        private void LoadNewMatrix(VO_Layer layer)
        {
            float red = 1.0f + layer.ColorTransformations.Red * 0.39f / 100.0f;
            float green = 1.0f + layer.ColorTransformations.Green * 0.39f / 100.0f;
            float blue = 1.0f + layer.ColorTransformations.Blue * 0.39f / 100.0f;

            if (layer.ColorTransformations.Grey > 0)
            {
                float lvlGrayDefault = 0.33f;
                float ratioGray = layer.ColorTransformations.Grey * 100 / 255;
                float redGray = red * ((100 - ratioGray) / 100) + (lvlGrayDefault * ratioGray / 100);
                float greenGray = green * ((100 - ratioGray) / 100) + (lvlGrayDefault * ratioGray / 100);
                float blueGray = blue * ((100 - ratioGray) / 100) + (lvlGrayDefault * ratioGray / 100);
                float lumiGray = (red + green + blue) / 3.0f - 1.0f;
                float otherGray = ratioGray * lvlGrayDefault / 100.0f;

                _Matrix.Matrix00 = redGray;
                _Matrix.Matrix01 = otherGray;
                _Matrix.Matrix02 = otherGray;
                _Matrix.Matrix10 = otherGray;
                _Matrix.Matrix11 = greenGray;
                _Matrix.Matrix12 = otherGray;
                _Matrix.Matrix20 = otherGray;
                _Matrix.Matrix21 = otherGray;
                _Matrix.Matrix22 = blueGray;
                _Matrix.Matrix40 = lumiGray;
                _Matrix.Matrix41 = lumiGray;
                _Matrix.Matrix42 = lumiGray;
                /*_Matrix = new System.Drawing.Imaging.ColorMatrix(new float[][]
                {
                    new float[]{redGray, otherGray, otherGray, 0.0f, 0.0f},
                    new float[]{otherGray, greenGray, otherGray, 0.0f, 0.0f},
                    new float[]{otherGray, otherGray, blueGray, 0.0f, 0.0f},
                    new float[]{0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                    new float[]{lumiGray, lumiGray, lumiGray, 1.0f, otherGray},

                });*/
            }
            else
            {
                _Matrix.Matrix00 = red;
                _Matrix.Matrix11 = green;
                _Matrix.Matrix22 = blue;
                _Matrix.Matrix40 = red - 1.0f;
                _Matrix.Matrix41 = green - 1.0f;
                _Matrix.Matrix42 = blue - 1.0f;
                /*_Matrix = new System.Drawing.Imaging.ColorMatrix(new float[][]
                {
                    new float[]{red, 0.0f, 0.0f, 0.0f, 0.0f},
                    new float[]{0.0f, green, 0.0f, 0.0f, 0.0f},
                    new float[]{0.0f, 0.0f, blue, 0.0f, 0.0f},
                    new float[]{0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                    new float[]{red - 1.0f, green -1.0f, blue  -1.0f, 1.0f, 1.0f},

                });*/
            }
            _Attributes.SetColorMatrix(_Matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            ColorMatrix opacity = new ColorMatrix();
            opacity.Matrix33 = layer.ColorTransformations.Opacity / 255.0f;
            _Opacity.SetColorMatrix(opacity, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }

        /// <summary>
        /// Reset la matrice de couleurs
        /// </summary>
        private void ResetMatrix()
        {
            _Matrix = new System.Drawing.Imaging.ColorMatrix(new float[][]
            {
                new float[]{1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                new float[]{0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                new float[]{0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                new float[]{0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                new float[]{0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                new float[]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f}

            });
        }

        #region Optimisations
        /// <summary>
        /// Passe une mémoire une image de la scène antérieur et postérieur au calque et au mode courant.
        /// </summary>
        public void LoadMinusMaximusLayers()
        {
            //Suppression des surfaces Minus/Maximus
            if (_InfLayersSurface != null)
                _InfLayersSurface.Dispose();
            if (_SupLayersSurface != null)
                _SupLayersSurface.Dispose();

            //Minus
            _InfLayersSurface = new Bitmap(ImageManager.GetImageBackground(_StageBackground));
            Graphics minGraphic = Graphics.FromImage(_InfLayersSurface);
            minGraphic.CompositingQuality = CompositingQuality.HighSpeed;
            minGraphic.SmoothingMode = SmoothingMode.HighSpeed;
            minGraphic.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            ProvisionMinusSurface(minGraphic);
            minGraphic.Dispose();

            //Maximum
            Size backgroundSize = ImageManager.GetImageBackground(_StageBackground).Size;
            Bitmap max = new Bitmap(backgroundSize.Width, backgroundSize.Height);
            max.MakeTransparent();
            _SupLayersSurface = max;
            Graphics maxGraphic = Graphics.FromImage(_SupLayersSurface);
            maxGraphic.CompositingQuality = CompositingQuality.HighSpeed;
            maxGraphic.SmoothingMode = SmoothingMode.HighSpeed;
            maxGraphic.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            ProvisionMaximusSurface(maxGraphic);
            maxGraphic.Dispose();
        }

        /// <summary>
        /// Créer l'image Minus
        /// </summary>
        /// <param name="pGraphic"></param>
        private void ProvisionMinusSurface(Graphics graphic)
        {
            VO_Stage stage = EditorHelper.Instance.GetCurrentStageInstance();
            stage.ListLayers.Sort();

            //LAYERS POUR DECOR/ANIMATION/CHARACTERS
            foreach (VO_Layer layer in stage.ListLayers)
            {
                //Application des paramètres Couleur du calque
                LoadNewMatrix(layer);

                //DECORS
                if (layer.Id == EditorHelper.Instance.CurrentLayer && EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Decors)
                {
                    ResetMatrix();
                    return;
                }
                if (!layer.Hidden)
                    RefreshDecorLayer(layer, graphic);

                //ANIMATIONS
                if ((EditorHelper.Instance.CurrentStageState.CompareTo(Enums.StagePanelState.Objects) >= 0 && EditorSettings.Instance.ShowAnimationsWhileMasking) || EditorHelper.Instance.CurrentStageState.CompareTo(Enums.StagePanelState.Objects) == 0)
                {
                    if (layer.Id == EditorHelper.Instance.CurrentLayer && EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Objects)
                    {
                        ResetMatrix();
                        return;
                    }
                    if (!layer.Hidden)
                    {
                        RefreshAnimationLayer(layer, graphic);
                    }
                }

                //Reset de la matrice de la couleur
                ResetMatrix();
            }

            //CHARACTERS
            if (EditorHelper.Instance.CurrentStageState.CompareTo(Enums.StagePanelState.Characters) > 0 && EditorSettings.Instance.ShowCharactersWhileMasking)
            {
                RefreshCharacterLayer(stage, graphic);
            }
        }

        /// <summary>
        /// Créer l'image maximum
        /// </summary>
        /// <param name="pGraphic"></param>
        private void ProvisionMaximusSurface(Graphics graphic)
        {
            VO_Stage stage = EditorHelper.Instance.GetCurrentStageInstance();
            stage.ListLayers.Sort();
            bool startToCatch = false;
            _SupSurfaceUsed = false;

            //LAYERS POUR DECOR/ANIMATION/CHARACTERS
            foreach (VO_Layer layer in stage.ListLayers)
            {
                //Application des paramètres Couleur du calque
                LoadNewMatrix(layer);

                //DECORS
                if (!layer.Hidden && startToCatch)
                {
                    RefreshDecorLayer(layer, graphic);
                    _SupSurfaceUsed = true;
                }
                if (layer.Id == EditorHelper.Instance.CurrentLayer && EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Decors)
                    startToCatch = true;

                //ANIMATIONS
                if (EditorHelper.Instance.CurrentStageState.CompareTo(Enums.StagePanelState.Objects) >= 0)
                {
                    if (!layer.Hidden && startToCatch)
                    {
                        RefreshAnimationLayer(layer, graphic);
                        _SupSurfaceUsed = true;
                    }
                    if (layer.Id == EditorHelper.Instance.CurrentLayer && EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Objects)
                        startToCatch = true;
                }

                //Reset de la matrice de la couleur
                ResetMatrix();
            }

            //CHARACTERS
            if (EditorHelper.Instance.CurrentStageState.CompareTo(Enums.StagePanelState.Characters) >= 0)
            {
                RefreshCharacterLayer(stage, graphic);
            }
        }
        #endregion
        #endregion

        #region Gestion des vecteurs
        /// <summary>
        /// Rafraichie les zones vectorielles
        /// </summary>
        public void RefreshVectorPoints(Graphics e)
        {
            VO_Stage stage = EditorHelper.Instance.GetCurrentStageInstance();
            switch (EditorHelper.Instance.CurrentStageState)
            {
                case Enums.StagePanelState.HotSpots:
                    RefreshVectorPointsPerMode(e, stage.ListHotSpots);
                    break;
                case Enums.StagePanelState.WalkableAreas:
                    foreach(VO_Layer layer in stage.ListLayers)
                        if(!layer.Hidden)
                            RefreshVectorPointsPerMode(e, layer.ListWalkableAreas);
                    break;
                case Enums.StagePanelState.Regions:
                    RefreshVectorPointsPerMode(e, stage.ListRegions);
                    break;
            }
        }

        /// <summary>
        /// Rafraichie les zones vectorielles par mode d'affichage
        /// </summary>
        private void RefreshVectorPointsPerMode(Graphics e, List<VO_StageHotSpot> hotspots)
        {
            int i = 0;
            Random rnd = new Random();
            foreach (VO_StageHotSpot hotSpot in hotspots)
            {
                if (hotSpot == EditorHelper.Instance.SelectedHotSpot && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
                {
                    foreach (Point point in hotSpot.Points)
                    {
                        e.DrawRectangle(EditorSettings.Instance.SelectedHotSpotColor, point.X + EditorSettings.Instance.StagePadding - EditorSettings.Instance.VectorPointsSize / 2, point.Y + EditorSettings.Instance.StagePadding - EditorSettings.Instance.VectorPointsSize / 2, EditorSettings.Instance.VectorPointsSize, EditorSettings.Instance.VectorPointsSize);
                    }
                    if (hotSpot.Points.Length > 1)
                        if (EditorHelper.Instance.HotSpotEditionMode)
                            e.DrawPolygon(EditorSettings.Instance.SelectedHotSpotColor, MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                        else
                            e.DrawLines(EditorSettings.Instance.SelectedHotSpotColor, MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                }
                else
                {
                    if (hotSpot.Points.Length > 1)
                    {
                        if (!_HotspotColors.ContainsKey(hotSpot.Id))
                            _HotspotColors.Add(hotSpot.Id, _Brushes[rnd.Next(0, _Brushes.Length - 1)]);
                        e.FillPolygon(_HotspotColors[hotSpot.Id], MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Rafraichie les zones vectorielles par mode d'affichage
        /// </summary>
        private void RefreshVectorPointsPerMode(Graphics e, List<VO_StageWalkable> hotspots)
        {
            int i = 0;
            Random rnd = new Random();
            foreach (VO_StageHotSpot hotSpot in hotspots)
            {
                if (hotSpot == EditorHelper.Instance.SelectedHotSpot && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
                {
                    foreach (Point point in hotSpot.Points)
                    {
                        e.DrawRectangle(EditorSettings.Instance.SelectedHotSpotColor, point.X + EditorSettings.Instance.StagePadding - EditorSettings.Instance.VectorPointsSize / 2, point.Y + EditorSettings.Instance.StagePadding - EditorSettings.Instance.VectorPointsSize / 2, EditorSettings.Instance.VectorPointsSize, EditorSettings.Instance.VectorPointsSize);
                    }
                    if (hotSpot.Points.Length > 1)
                        if (EditorHelper.Instance.HotSpotEditionMode)
                            e.DrawPolygon(EditorSettings.Instance.SelectedHotSpotColor, MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                        else
                            e.DrawLines(EditorSettings.Instance.SelectedHotSpotColor, MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                }
                else
                {
                    if (hotSpot.Points.Length > 1)
                    {
                        if (!_HotspotColors.ContainsKey(hotSpot.Id))
                            _HotspotColors.Add(hotSpot.Id, _Brushes[rnd.Next(0, _Brushes.Length - 1)]);
                        e.FillPolygon(_HotspotColors[hotSpot.Id], MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Rafraichie les zones vectorielles par mode d'affichage
        /// </summary>
        private void RefreshVectorPointsPerMode(Graphics e, List<VO_StageRegion> hotspots)
        {
            int i = 0;
            Random rnd = new Random();
            foreach (VO_StageHotSpot hotSpot in hotspots)
            {
                if (hotSpot == EditorHelper.Instance.SelectedHotSpot && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
                {
                    foreach (Point point in hotSpot.Points)
                    {
                        e.DrawRectangle(EditorSettings.Instance.SelectedHotSpotColor, point.X + EditorSettings.Instance.StagePadding - EditorSettings.Instance.VectorPointsSize / 2, point.Y + EditorSettings.Instance.StagePadding - EditorSettings.Instance.VectorPointsSize / 2, EditorSettings.Instance.VectorPointsSize, EditorSettings.Instance.VectorPointsSize);
                    }
                    if (hotSpot.Points.Length > 1)
                        if (EditorHelper.Instance.HotSpotEditionMode)
                            e.DrawPolygon(EditorSettings.Instance.SelectedHotSpotColor, MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                        else
                            e.DrawLines(EditorSettings.Instance.SelectedHotSpotColor, MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                }
                else
                {
                    if (hotSpot.Points.Length > 1)
                    {
                        if (!_HotspotColors.ContainsKey(hotSpot.Id))
                            _HotspotColors.Add(hotSpot.Id, _Brushes[rnd.Next(0, _Brushes.Length - 1)]);
                        e.FillPolygon(_HotspotColors[hotSpot.Id], MovePoints((Point[])hotSpot.Points.Clone(), EditorSettings.Instance.StagePadding, EditorSettings.Instance.StagePadding));
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Ajoute une ligne sur la position mouvante.
        /// </summary>
        /// <param name="e">Graphic</param>
        /// <param name="position">Position souris</param>
        public void RefreshVectorMovingPoint(Graphics e, Point position)
        {
            VO_StageHotSpot hotSpot = EditorHelper.Instance.SelectedHotSpot;
            if (hotSpot == EditorHelper.Instance.SelectedHotSpot)
            {
                Point lastPoint = hotSpot.Points[hotSpot.Points.Length - 1];
                e.DrawLine(EditorSettings.Instance.SelectedHotSpotColor, new Point(EditorSettings.Instance.StagePadding + lastPoint.X, EditorSettings.Instance.StagePadding + lastPoint.Y), new Point(position.X + EditorSettings.Instance.StagePadding, position.Y + EditorSettings.Instance.StagePadding));
            }
        }

        /// <summary>
        /// Mets à jour la location de la zone
        /// </summary>
        /// <param name="hotSpot">HotSpot</param>
        public void UpdateAreaLocation(VO_StageHotSpot hotSpot)
        {
            int xMin = 0;
            int xMax = 0;
            int yMin = 0;
            int yMax = 0;
            bool first = true;

            foreach (Point point in hotSpot.Points)
            {
                if (first)
                {
                    xMin = xMax = point.X;
                    yMin = yMax = point.Y;
                    first = false;
                }
                else
                {
                    if (point.X < xMin)
                        xMin = point.X;
                    if (point.X > xMax)
                        xMax = point.X;
                    if (point.Y < yMin)
                        yMin = point.Y;
                    if (point.Y > yMax)
                        yMax = point.Y;
                }
            }
            hotSpot.Location = new Point(xMin, yMin);
            hotSpot.Size = new Size(xMax - xMin, yMax - yMin);
        }

        /// <summary>
        /// Synchronise les points avec le padding de stage
        /// </summary>
        /// <param name="points">liste de points</param>
        /// <returns>liste de point</returns>
        public Point[] MovePoints(Point[] points, int x, int y)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X += x;
                points[i].Y += y;
            }
            return points;
        }

        /// <summary>
        /// Synchronise le point avec le padding de stage
        /// </summary>
        /// <param name="point">point</param>
        /// <returns>point</returns>
        private Point MovePoint(Point point, int x, int y)
        {
            point.X += x;
            point.Y += y;
            return point;
        }

        /// <summary>
        /// Récupère un point et renvoie son index
        /// </summary>
        /// <param name="point">point</param>
        /// <returns>index</returns>
        public int GetVectorPoint(Point mousePoint)
        {
            int index = 0;
            Rectangle mouseRect = new Rectangle(mousePoint, new Size(1, 1));

            foreach (Point point in EditorHelper.Instance.SelectedHotSpot.Points)
            {
                Rectangle rect = new Rectangle(new Point(point.X - EditorSettings.Instance.VectorPointsSize / 2, point.Y - EditorSettings.Instance.VectorPointsSize / 2), new Size(EditorSettings.Instance.VectorPointsSize, EditorSettings.Instance.VectorPointsSize));
                if (rect.IntersectsWith(mouseRect))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Vérifie si un point se trouve sur le polygon sélectionné
        /// </summary>
        /// <param name="point">Point à tester</param>
        /// <param name="addPoint">Ajouter le point au polygone</param>
        /// <returns></returns>
        public bool IsOnTheSelectedPolygon(Point checkedPoint, bool addPoint)
        {
            VO_StageHotSpot polygon = EditorHelper.Instance.SelectedHotSpot;
            if (polygon != null)
            {
                Point point1 = new Point(0, 0);
                Point point2 = new Point(0, 0);
                Point[] points = polygon.Points;
                bool firstpass = true;
                for (int i = 0; i < polygon.Points.Length; i++)
                {
                    if (firstpass)
                    {
                        point1 = points[i];
                        firstpass = false;
                        continue;
                    }
                    point2 = point1;
                    point1 = points[i];
                    if (IsOnLine(point1, point2, checkedPoint))
                    {
                        if (addPoint)
                            CreateNewPointToTheCurrentSelectedHotSpot(checkedPoint, i);
                        return true;
                    }
                }
                point2 = points[0];
                if (IsOnLine(point1, point2, checkedPoint))
                {
                    if (addPoint)
                        CreateNewPointToTheCurrentSelectedHotSpot(checkedPoint, polygon.Points.Length);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Vérifie si un point se trouve sur une ligne
        /// </summary>
        /// <param name="point1">point 1</param>
        /// <param name="point2">point 2</param>
        /// <param name="checkPoint">point à vérifier</param>
        /// <returns>oui si la condition est vérifiée, sinon non</returns>
        public bool IsOnLine(Point point1, Point point2, Point checkPoint)
        {
            List<PointF> points = new List<PointF>();

            // Get Points From Line(s)
            float curDist = 0;
            float distance = 0;
            float deltaX = point2.X - point1.X;
            float deltaY = point2.Y - point1.Y;
            curDist = 0;
            distance = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            while (curDist < distance)
            {
                curDist++;
                float offsetX = (float)((double)curDist / (double)distance * (double)deltaX);
                float offsetY = (float)((double)curDist / (double)distance * (double)deltaY);
                if ((checkPoint.X == Convert.ToInt32(point1.X + offsetX)) || (checkPoint.X - 1 == Convert.ToInt32(point1.X + offsetX)) || (checkPoint.X + 1 == Convert.ToInt32(point1.X + offsetX)))
                    if ((checkPoint.Y == Convert.ToInt32(point1.Y + offsetY)) || (checkPoint.Y - 1 == Convert.ToInt32(point1.Y + offsetY)) || (checkPoint.Y + 1 == Convert.ToInt32(point1.Y + offsetY)))
                        return true;
            }
            return false;
        }

        /// <summary>
        /// Déplace le point choisi en index
        /// </summary>
        /// <param name="mousePoint">position</param>
        /// <param name="index">index</param>
        public void MoveVectorPoint(Point mousePoint, int index)
        {
            for (int i = 0; i < EditorHelper.Instance.SelectedHotSpot.Points.Length; i++)
            {
                if (index == i)
                {
                    EditorHelper.Instance.SelectedHotSpot.Points[i] = new Point(mousePoint.X, mousePoint.Y);
                    return;
                }
            }
        }
        #endregion

        #region Calculs de coordonnées
        /// <summary>
        /// Récupère le point sur scène en tenant compte du zoom
        /// </summary>
        /// <param name="pPoint">Coordonnées à traiter</param>
        /// <returns>Coordonnées traitées</returns>
        public Point GetDragStageCoords(Point point)
        {
            Point newPoint = new Point(Convert.ToInt32(Math.Round(Convert.ToDouble(point.X) / Convert.ToDouble(EditorHelper.Instance.CurrentZoom))), Convert.ToInt32(Math.Round(Convert.ToDouble(point.Y) / Convert.ToDouble(EditorHelper.Instance.CurrentZoom))));
            newPoint.X -= EditorSettings.Instance.StagePadding;
            newPoint.Y -= EditorSettings.Instance.StagePadding;
            return newPoint;
        }

        /// <summary>
        /// Positionne un objet sur la map, indépendament du zoom ou du padding.
        /// </summary>
        /// <param name="pLocation">Position par rapport à la map</param>
        /// <returns>Position réelle.</returns>
        public Point SetObjectInStage(Point location)
        {
            location.X = EditorSettings.Instance.StagePadding + location.X;// *StageHelper.Instance.CurrentZoom;
            location.Y = EditorSettings.Instance.StagePadding + location.Y;// *StageHelper.Instance.CurrentZoom;

            return location;
        }
        #endregion

        #region Creation/Suppression
        /// <summary>
        /// Recharge les ressources de la scène
        /// </summary>
        public void ResetStageResources()
        {
            //Reset des ressources
            ImageManager.ResetStageResources();

            if (_ColoredDecors != null)
            {
                foreach (Image surface in _ColoredDecors.Values)
                    surface.Dispose();
            }
            if (_ColoredAnimations != null)
            {
                foreach (Image surface in _ColoredAnimations.Values)
                    surface.Dispose();
            }
            if (_ColoredCharacters != null)
            {
                foreach (Image surface in _ColoredCharacters.Values)
                    surface.Dispose();
            }
            if (_HotspotColors.Count > 1000)
                _HotspotColors = new Dictionary<Guid, Brush>();
            _ColoredDecors = new Dictionary<string, Image>();
            _ColoredAnimations = new Dictionary<string, Image>();
            _ColoredCharacters = new Dictionary<string, Image>();
        }

        /// <summary>
        /// Créer un décor
        /// </summary>
        /// <param name="pLocation">Localisation sur scène</param>
        /// <param name="pFile">Fichier décor</param>
        public void CreateDecor(Point location, string file)
        {
            ObjectsFactory.CreateDecor(EditorHelper.Instance.GetCurrentLayerInstance(), location, file);
        }

        /// <summary>
        /// Créer une animation
        /// </summary>
        /// <param name="pLocation">Localisation sur scène</param>
        /// <param name="pAnimId">ID d'animation</param>
        public void CreateAnimation(Point location, Guid animId)
        {
            ObjectsFactory.CreateAnimation(EditorHelper.Instance.GetCurrentLayerInstance(), location, animId);
        }

        /// <summary>
        /// Créer un character
        /// </summary>
        /// <param name="location">Localisation sur scène</param>
        /// <param name="animId">ID d'animation</param>
        public void CreateCharacter(Point location, Guid animId)
        {
            ObjectsFactory.CreateCharacter(EditorHelper.Instance.GetCurrentStageInstance(), location, animId);
        }

        /// <summary>
        /// Créer un HotSpot
        /// </summary>
        /// <param name="location">Localisation du scène</param>
        public VO_StageHotSpot CreateHotSpot(Point location)
        {
            return ObjectsFactory.CreateHotSpot(EditorHelper.Instance.GetCurrentStageInstance(), location);
        }

        /// <summary>
        /// Créer une walkable area
        /// </summary>
        /// <param name="location">Localisation du scène</param>
        public VO_StageHotSpot CreateWalkableArea(Point location)
        {
            return ObjectsFactory.CreateWalkableArea(EditorHelper.Instance.GetCurrentLayerInstance(), location);
        }

        /// <summary>
        /// Créer une region
        /// </summary>
        /// <param name="location">Localisation du scène</param>
        public VO_StageHotSpot CreateRegion(Point location)
        {
            return ObjectsFactory.CreateRegion(EditorHelper.Instance.GetCurrentStageInstance(), location);
        }

        /// <summary>
        /// Créer un nouveau point de HotSpot
        /// </summary>
        /// <param name="location">Coordonnées du point</param>
        /// <param name="i">index</param>
        /// <returns>Index sélectioné</returns>
        public int CreateNewPointToTheCurrentSelectedHotSpot(Point location, int i)
        {
            VO_StageHotSpot hotSpot = EditorHelper.Instance.SelectedHotSpot;

            List<Point> points = new List<Point>();
            foreach (Point point in hotSpot.Points)
                points.Add(point);
            hotSpot.Points = new Point[hotSpot.Points.Length + 1];
            int index = 0;
            foreach (Point point in points)
            {
                hotSpot.Points[index] = point;
                index++;
                if (i == index)
                {
                    hotSpot.Points[index] = location;
                    index++;
                }
            }
            if (i == hotSpot.Points.Length - 1)
                hotSpot.Points[hotSpot.Points.Length - 1] = location;
            return hotSpot.Points.Length - 1;
        }

        /// <summary>
        /// Supprime un point de hotspot
        /// </summary>
        /// <param name="location">Coordonnées du point</param>
        /// <param name="i">index</param>
        /// <returns>true si un point a été supprimé, sinon false</returns>
        public bool RemovePointOfTheCurrentSelectedHotSpot(Point location)
        {
            int index = GetVectorPoint(location);
            if (index > -1 && EditorHelper.Instance.SelectedHotSpot.Points.Length > 1)
            {
                RemovePointFromPolygone(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Supprime un point de polygone
        /// </summary>
        /// <param name="index">index</param>
        private void RemovePointFromPolygone(int i)
        {
            VO_StageHotSpot hotSpot = EditorHelper.Instance.SelectedHotSpot;

            List<Point> points = new List<Point>();
            int index = 0;
            foreach (Point point in hotSpot.Points)
            {
                if (index != i)
                    points.Add(point);
                index++;
            }
            hotSpot.Points = new Point[hotSpot.Points.Length - 1];
            index = 0;
            foreach (Point point in points)
            {
                hotSpot.Points[index] = point;
                index++;
            }
        }
        #endregion

        #region Opérations sur scène
        /// <summary>
        /// Déplace un objet en avant ou arrière plan.
        /// </summary>
        /// <param name="direction">Avant ou arrière plan</param>
        /// <param name="max">Au premier ou au dernier plan</param>
        public void MoveObjectInPlan(Enums.Direction direction, bool max)
        {
            VO_Stage stage = EditorHelper.Instance.GetCurrentStageInstance();
            VO_Layer layer = EditorHelper.Instance.GetCurrentLayerInstance();
            int currentIndex = 0;
            foreach (VO_StageObject objectStage in EditorHelper.Instance.SelectedObjects)
            {
                switch (objectStage.ObjectType)
                {
                    case Enums.StageObjectType.Decors:
                        #region Decors
                        currentIndex = layer.ListDecors.FindIndex(p => p.Id == objectStage.Id);
                        if (direction == Enums.Direction.Up)
                        {
                            if (max)
                                layer.ListDecors.Add((VO_StageDecor)objectStage);
                            else if (currentIndex != layer.ListDecors.Count - 1)
                                layer.ListDecors.Insert(currentIndex + 2, (VO_StageDecor)objectStage);
                            else
                                continue;
                            layer.ListDecors.RemoveAt(currentIndex);
                        }
                        else
                        {
                            if (max)
                                layer.ListDecors.Insert(0, ((VO_StageDecor)objectStage));
                            else if (currentIndex > 0)
                                layer.ListDecors.Insert(currentIndex - 1, (VO_StageDecor)objectStage);
                            else
                                continue;
                            layer.ListDecors.RemoveAt(currentIndex + 1);
                        }
                        #endregion
                        break;
                    case Enums.StageObjectType.Animations:
                        #region Animations
                        currentIndex = layer.ListAnimations.FindIndex(p => p.Id == objectStage.Id);
                        if (direction == Enums.Direction.Up)
                        {
                            if (max)
                                layer.ListAnimations.Add((VO_StageAnimation)objectStage);
                            else if (currentIndex != layer.ListAnimations.Count - 1)
                                layer.ListAnimations.Insert(currentIndex + 2, (VO_StageAnimation)objectStage);
                            else
                                continue;
                            layer.ListAnimations.RemoveAt(currentIndex);
                        }
                        else
                        {
                            if (max)
                                layer.ListAnimations.Insert(0, ((VO_StageAnimation)objectStage));
                            else if (currentIndex > 0)
                                layer.ListAnimations.Insert(currentIndex - 1, (VO_StageAnimation)objectStage);
                            else
                                continue;
                            layer.ListAnimations.RemoveAt(currentIndex + 1);
                        }
                        #endregion
                        break;
                    case Enums.StageObjectType.Characters:
                        #region Characters
                        currentIndex = stage.ListCharacters.FindIndex(p => p.Id == objectStage.Id);
                        if (direction == Enums.Direction.Up)
                        {
                            if (max)
                                stage.ListCharacters.Add((VO_StageCharacter)objectStage);
                            else if (currentIndex != stage.ListCharacters.Count - 1)
                                stage.ListCharacters.Insert(currentIndex + 2, (VO_StageCharacter)objectStage);
                            else
                                continue;
                            stage.ListCharacters.RemoveAt(currentIndex);
                        }
                        else
                        {
                            if (max)
                                stage.ListCharacters.Insert(0, ((VO_StageCharacter)objectStage));
                            else if (currentIndex > 0)
                                stage.ListCharacters.Insert(currentIndex - 1, (VO_StageCharacter)objectStage);
                            else
                                continue;
                            stage.ListCharacters.RemoveAt(currentIndex + 1);
                        }
                        #endregion
                        break;
                    case Enums.StageObjectType.HotSpots:
                        #region Hotspots
                        currentIndex = stage.ListHotSpots.FindIndex(p => p.Id == objectStage.Id);
                        if (direction == Enums.Direction.Up)
                        {
                            if (max)
                                stage.ListHotSpots.Add((VO_StageHotSpot)objectStage);
                            else if (currentIndex != stage.ListHotSpots.Count - 1)
                                stage.ListHotSpots.Insert(currentIndex + 2, (VO_StageHotSpot)objectStage);
                            else
                                continue;
                            stage.ListHotSpots.RemoveAt(currentIndex);
                        }
                        else
                        {
                            if (max)
                                stage.ListHotSpots.Insert(0, ((VO_StageHotSpot)objectStage));
                            else if (currentIndex > 0)
                                stage.ListHotSpots.Insert(currentIndex - 1, (VO_StageHotSpot)objectStage);
                            else
                                continue;
                            stage.ListHotSpots.RemoveAt(currentIndex + 1);
                        }
                        #endregion
                        break;
                    case Enums.StageObjectType.Walkables:
                        #region Walkables
                        currentIndex = layer.ListWalkableAreas.FindIndex(p => p.Id == objectStage.Id);
                        if (direction == Enums.Direction.Up)
                        {
                            if (max)
                                layer.ListWalkableAreas.Add((VO_StageWalkable)objectStage);
                            else if (currentIndex != layer.ListWalkableAreas.Count - 1)
                                layer.ListWalkableAreas.Insert(currentIndex + 2, (VO_StageWalkable)objectStage);
                            else
                                continue;
                            layer.ListWalkableAreas.RemoveAt(currentIndex);
                        }
                        else
                        {
                            if (max)
                                layer.ListWalkableAreas.Insert(0, ((VO_StageWalkable)objectStage));
                            else if (currentIndex > 0)
                                layer.ListWalkableAreas.Insert(currentIndex - 1, (VO_StageWalkable)objectStage);
                            else
                                continue;
                            layer.ListWalkableAreas.RemoveAt(currentIndex + 1);
                        }
                        #endregion
                        break;
                    case Enums.StageObjectType.Regions:
                        #region Regions
                        currentIndex = stage.ListRegions.FindIndex(p => p.Id == objectStage.Id);
                        if (direction == Enums.Direction.Up)
                        {
                            if (max)
                                stage.ListRegions.Add((VO_StageRegion)objectStage);
                            else if (currentIndex != stage.ListRegions.Count - 1)
                                stage.ListRegions.Insert(currentIndex + 2, (VO_StageRegion)objectStage);
                            else
                                continue;
                            stage.ListRegions.RemoveAt(currentIndex);
                        }
                        else
                        {
                            if (max)
                                stage.ListRegions.Insert(0, ((VO_StageRegion)objectStage));
                            else if (currentIndex > 0)
                                stage.ListRegions.Insert(currentIndex - 1, (VO_StageRegion)objectStage);
                            else
                                continue;
                            stage.ListRegions.RemoveAt(currentIndex + 1);
                        }
                        #endregion
                        break;
                }
            }
        }

        /// <summary>
        /// Génère la liste des items sélectionnés
        /// </summary>
        /// <param name="pPosition">Position de la souris relative à la scène</param>
        /// <param name="CtrlPressed">Bouton Ctrl pressé</param>
        public void GetSelectedItem(Point position, bool ctrlPressed)
        {
            //Si Ctrl n'est pas pressé, on reset la liste des objets sélectionnés.
            if (!ctrlPressed)
                EditorHelper.Instance.SelectedObjects.Clear();

            VO_StageObject selectedObject = null;
            VO_Layer layer = EditorHelper.Instance.GetCurrentLayerInstance();
            VO_Stage stage = EditorHelper.Instance.GetCurrentStageInstance();
            if (!layer.Hidden)
            {
                //SI EN MODE DECORS - Décors
                if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Decors)
                {
                    foreach (VO_StageObject decor in layer.ListDecors)
                    {
                        Rectangle rect = new Rectangle(decor.Location, decor.Size);
                        if (rect.IntersectsWith(new Rectangle(position, new Size(1, 1))))
                        {
                            selectedObject = decor;
                        }
                    }
                }
                //SI EN MODE OBJECTS - Animations
                else if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Objects)
                {
                    foreach (VO_StageObject animation in layer.ListAnimations)
                    {
                        Rectangle rect = new Rectangle(animation.Location, animation.Size);
                        if (rect.IntersectsWith(new Rectangle(position, new Size(1, 1))))
                        {
                            selectedObject = animation;
                        }
                    }
                }
                //SI EN MODE Characters
                else if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Characters)
                {
                    foreach (VO_StageObject character in stage.ListCharacters)
                    {
                        Rectangle rect = new Rectangle(character.Location, character.Size);
                        if (rect.IntersectsWith(new Rectangle(position, new Size(1, 1))))
                        {
                            selectedObject = character;
                        }
                    }
                }
                //SI EN MODE HOTSPOTS
                else if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.HotSpots)
                {
                    foreach (VO_StageObject hotspot in stage.ListHotSpots)
                    {
                        Rectangle rect = new Rectangle(hotspot.Location, hotspot.Size);
                        if (rect.IntersectsWith(new Rectangle(position, new Size(1, 1))))
                        {
                            selectedObject = hotspot;
                        }
                    }
                }
                //SI EN MODE WALKABLE AREAS - Walkable regions
                else if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.WalkableAreas)
                {
                    foreach (VO_StageObject hotspot in layer.ListWalkableAreas)
                    {
                        Rectangle rect = new Rectangle(hotspot.Location, hotspot.Size);
                        if (rect.IntersectsWith(new Rectangle(position, new Size(1, 1))))
                        {
                            selectedObject = hotspot;
                        }
                    }
                }
                //SI EN MODE REGIONS - Regions
                else if (EditorHelper.Instance.CurrentStageState == Enums.StagePanelState.Regions)
                {
                    foreach (VO_StageObject hotspot in stage.ListRegions)
                    {
                        Rectangle rect = new Rectangle(hotspot.Location, hotspot.Size);
                        if (rect.IntersectsWith(new Rectangle(position, new Size(1, 1))))
                        {
                            selectedObject = hotspot;
                        }
                    }
                }

                if (selectedObject != null)
                    if (!EditorHelper.Instance.SelectedObjects.Contains(selectedObject))
                    {
                        EditorHelper.Instance.SelectedObjects.Add(selectedObject);
                        if (selectedObject.ObjectType == Enums.StageObjectType.HotSpots || selectedObject.ObjectType == Enums.StageObjectType.Walkables || selectedObject.ObjectType == Enums.StageObjectType.Regions)
                        {
                            EditorHelper.Instance.SelectedHotSpot = (VO_StageHotSpot)selectedObject;
                            EditorHelper.Instance.SelectedHotSpotPoint = -1;
                            EditorHelper.Instance.HotSpotEditionMode = true;
                        }
                    }
                    else
                    {
                        EditorHelper.Instance.SelectedObjects.Remove(selectedObject);
                        if (selectedObject.ObjectType == Enums.StageObjectType.HotSpots || selectedObject.ObjectType == Enums.StageObjectType.Walkables || selectedObject.ObjectType == Enums.StageObjectType.Regions)
                        {
                            EditorHelper.Instance.SelectedHotSpot = null;
                            EditorHelper.Instance.SelectedHotSpotPoint = -1;
                            EditorHelper.Instance.HotSpotEditionMode = false;
                        }
                    }
            }
        }

        /// <summary>
        /// Enregistre la position des objets sélectionnés au démarrage du Drag & Drop.
        /// </summary>
        /// <param name="pPosition">Position brute de la souris</param>
        public void StartObjectDrag(Point position)
        {
            _SavedPositions = new Dictionary<Guid, Point>();
            Point mouse = GetDragStageCoords(position);
            foreach (VO_StageObject vObject in EditorHelper.Instance.SelectedObjects)
                _SavedPositions.Add(vObject.Id, new Point(mouse.X - vObject.Location.X, mouse.Y - vObject.Location.Y));
        }

        /// <summary>
        /// Déplacer les objets en fonction du point souris original
        /// </summary>
        /// <param name="pPosition">Position brute de la souris</param>
        public void MoveObjectDragDrop(Point position)
        {
            Point mouse = GetDragStageCoords(position);
            foreach (VO_StageObject vo in EditorHelper.Instance.SelectedObjects)
            {
                Rectangle oldPosition = new Rectangle(vo.Location, vo.Size);
                vo.Location = new Point(mouse.X - _SavedPositions[vo.Id].X, mouse.Y - _SavedPositions[vo.Id].Y);
                vo.Size = vo.Size;

                //Si ce sont des coordonnées, on déplace les points en même temps que la sélection.
                if (vo.ObjectType == Enums.StageObjectType.HotSpots || vo.ObjectType == Enums.StageObjectType.Walkables || vo.ObjectType == Enums.StageObjectType.Regions)
                {
                    int movX = vo.Location.X - oldPosition.X;
                    int movY = vo.Location.Y - oldPosition.Y;
                    VO_StageHotSpot hotspot = (VO_StageHotSpot)vo;
                    MovePoints(hotspot.Points, movX, movY);
                }
            }
        }
        #endregion
        #endregion
    }
}