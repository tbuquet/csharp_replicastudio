using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.TransverseLayer.Interfaces;
using Microsoft.Xna.Framework;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    /// <summary>
    /// Menu sélectionnable
    /// </summary>
    public class VO_SelectableMenu
    {
        #region Members
        /// <summary>
        /// Messages
        /// </summary>
        List<VO_String2D> _Messages;

        /// <summary>
        /// Guid des messages
        /// </summary>
        List<Guid> _MessagesGuids;

        /// <summary>
        /// Previous
        /// </summary>
        VO_String2D _Previous;

        /// <summary>
        /// Next
        /// </summary>
        VO_String2D _Next;

        /// <summary>
        /// Largeur du menu
        /// </summary>
        int _Width;

        /// <summary>
        /// Hauteur du menu
        /// </summary>
        int _Height;

        /// <summary>
        /// Taille de police
        /// </summary>
        int _FontSize;

        /// <summary>
        /// Padding entre les choix
        /// </summary>
        int _PaddingBetweenChoices;

        /// <summary>
        /// Image du menu
        /// </summary>
        VO_Sprite _MenuBack;

        /// <summary>
        /// Image de sélection
        /// </summary>
        VO_Sprite _MenuSelector;

        /// <summary>
        /// Current page
        /// </summary>
        int _CurrentMenuPage;

        /// <summary>
        /// Limiter la vue à _LimitView items
        /// </summary>
        int _LimitView;

        /// <summary>
        /// Nombres pages
        /// </summary>
        int _NbrPages;

        /// <summary>
        /// SpriteBatch
        /// </summary>
        SpriteBatch _SpriteBatch;
        #endregion

        #region Delegates
        /// <summary>
        /// Delegate SelectedValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SelectedValueChangedEventHandler(object sender, GameMenuEventArgs e);

        /// <summary>
        /// Delegate OnClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void OnClickEventHandler(object sender, GameMenuEventArgs e);
        #endregion

        #region Events
        /// <summary>
        /// Lorsque la valeur sélectionnée change
        /// </summary>
        public event SelectedValueChangedEventHandler SelectedValueChanged;

        /// <summary>
        /// Lorsqu'un bouton du menu est sélectionné
        /// </summary>
        public event OnClickEventHandler OnClick;
        #endregion

        #region Properties
        public int LimitView { get { return _LimitView; } set { _LimitView = value; CalculateWidthAndHeight(); } }

        public Vector2 Position { get; set; }

        public ViewerEnums.Alignment Alignment { get; set; }

        public int SelectedIndex { get; set; }

        public Guid SelectedValue { get; set; }

        public int FontSize { get { return _FontSize; } set { _FontSize = value; CalculateWidthAndHeight(); } }

        public int PaddingBetweenChoices { get { return _PaddingBetweenChoices; } }

        public int Width { get { return _Width; } }

        public int Height { get { return _Height; } }

        public List<string> Messages { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur par messages
        /// </summary>
        /// <param name="messages"></param>
        public VO_SelectableMenu(SpriteBatch spriteBatch, Game game, List<string> messages, int padding)
        {
            //On clear la liste si il en existait une.
            Clear();

            _SpriteBatch = spriteBatch;

            _FontSize = 20;
            _Messages = new List<VO_String2D>();

            foreach (string mess in messages)
            {
                _Messages.Add(new VO_String2D(mess, "Arial", _FontSize));

            }

            SelectedIndex = 0;
            SelectedValue = new Guid();

            Messages = messages;

            _NbrPages = 1;
            _PaddingBetweenChoices = padding;

            CalculateWidthAndHeight();

            LoadContent();
        }

        /// <summary>
        /// Constructeur de choix de réponse
        /// </summary>
        /// <param name="messages"></param>
        public VO_SelectableMenu(SpriteBatch spriteBatch, Game game, List<VO_LineChoices> messages, int padding)
        {
            //On clear la liste si il en existait une.
            Clear();

            _SpriteBatch = spriteBatch;

            _FontSize = 20;
            _Messages = new List<VO_String2D>();
            _MessagesGuids = new List<Guid>();
            Messages = new List<string>();

            foreach (VO_LineChoices mess in messages)
            {
                _Messages.Add(new VO_String2D(mess.Choice, "Arial", _FontSize));
                _MessagesGuids.Add(mess.Id);
                Messages.Add(mess.Choice);
            }

            SelectedIndex = 0;
            SelectedValue = _MessagesGuids[0];

            _NbrPages = 1;
            _PaddingBetweenChoices = padding;

            CalculateWidthAndHeight();

            _Width = GameCore.Instance.Game.Project.Resolution.Width;

            LoadContent();
        }
        #endregion

        #region InheritedMethods
        protected void LoadContent()
        {
            //Chargement des ressources
            int blockSize = VO_GUI.BlockSize;
            if (_MenuBack == null)
                _MenuBack = new VO_Sprite(ImageManager.GetPermanentMenu(_Width, _Height, ViewerEnums.MenuType.Back));
            if (_MenuSelector == null)
                _MenuSelector = new VO_Sprite(ImageManager.GetPermanentMenu(_Width - blockSize, (int)_Messages[0].Height + blockSize, ViewerEnums.MenuType.Front));
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            if (Messages != null && Messages.Count > 0)
            {
                foreach (VO_String2D message in _Messages)
                {
                    message.Visible = false;
                }
                if (_Next != null)
                    _Next.Visible = false;
                if (_Previous != null)
                    _Previous.Visible = false;

                //Background
                if (_MenuBack != null)
                {
                    _MenuBack.Position = this.Position;
                    _SpriteBatch.Draw(_MenuBack.Image, new Rectangle((int)_MenuBack.Position.X, (int)_MenuBack.Position.Y, _MenuBack.Width, _MenuBack.Height), Color.White);
                }

                //Messages
                int startingIndex = 0;
                int blockSize = VO_GUI.BlockSize;
                //Cas simple
                if (_NbrPages == 1)
                {
                    for (int i = 0; i < _Messages.Count; i++)
                    {
                        _Messages[i].Position = new Vector2(Position.X + (uint)VO_GUI.BackL.Width, Position.Y + i * (PaddingBetweenChoices + _Messages[i].Height) + (uint)VO_GUI.BackT.Height);
                        _SpriteBatch.DrawString(_Messages[i].SpriteFont, _Messages[i].Text, _Messages[i].Position, Color.White);
                        _Messages[i].Visible = true;
                    }
                }
                //Cas complexe
                else
                {
                    //Première page
                    if (_CurrentMenuPage == 0)
                    {
                        _Previous.Position = new Vector2(-100, -100);
                        _Previous.Visible = false;
                        for (int i = 0; i < LimitView - 1; i++)
                        {
                            _Messages[i].Position = new Vector2(Position.X + (uint)VO_GUI.BackL.Width, Position.Y + i * (PaddingBetweenChoices + (int)_Messages[i].Height) + (uint)VO_GUI.BackT.Height);
                            _SpriteBatch.DrawString(_Messages[i].SpriteFont, _Messages[i].Text, _Messages[i].Position, Color.White);
                            _Messages[i].Visible = true;
                        }
                        _Next.Position = new Vector2(Position.X + (uint)VO_GUI.BackL.Width, Position.Y + (LimitView - 1) * (PaddingBetweenChoices + _Next.Height) + (uint)VO_GUI.BackT.Height);
                        _Next.Visible = true;
                        _SpriteBatch.DrawString(_Next.SpriteFont, _Next.Text, _Next.Position, Color.White);
                    }
                    //Dernière page
                    else if (_CurrentMenuPage == _NbrPages - 1)
                    {
                        _Next.Position = new Vector2(-100, -100);
                        _Next.Visible = false;
                        _Previous.Position = new Vector2(Position.X + (uint)VO_GUI.BackL.Width, Position.Y + (uint)VO_GUI.BackT.Height);
                        _Previous.Visible = true;
                        _SpriteBatch.DrawString(_Previous.SpriteFont, _Previous.Text, _Previous.Position, Color.White);
                        startingIndex = _CurrentMenuPage * (LimitView - 2) + 1;
                        int index = 1;
                        for (int i = startingIndex; i < startingIndex + LimitView - 1; i++)
                        {
                            _Messages[i].Visible = true;
                            if (i == _Messages.Count)
                                break;
                            _Messages[i].Position = new Vector2(Position.X + (uint)VO_GUI.BackL.Width, Position.Y + index * (PaddingBetweenChoices + (int)_Messages[i].Height) + (uint)VO_GUI.BackT.Height);
                            _SpriteBatch.DrawString(_Messages[i].SpriteFont, _Messages[i].Text, _Messages[i].Position, Color.White);
                            index++;
                        }
                    }
                    //Pages intermédiaires
                    else
                    {
                        _Previous.Position = new Vector2(Position.X + (uint)VO_GUI.BackL.Width, Position.Y + (uint)VO_GUI.BackT.Height);
                        _Previous.Visible = true;
                        _SpriteBatch.DrawString(_Previous.SpriteFont, _Previous.Text, _Previous.Position, Color.White);

                        startingIndex = _CurrentMenuPage * (LimitView - 2) + 1;
                        int index = 1;
                        for (int i = startingIndex; i < startingIndex + LimitView - 2; i++)
                        {
                            _Messages[i].Visible = true;
                            _Messages[i].Position = new Vector2(Position.X + (uint)VO_GUI.BackL.Width, Position.Y + index * (PaddingBetweenChoices + (int)_Messages[i].Height) + (uint)VO_GUI.BackT.Height);
                            _SpriteBatch.DrawString(_Messages[i].SpriteFont, _Messages[i].Text, _Messages[i].Position, Color.White);
                            index++;
                        }

                        _Next.Position = new Vector2(Position.X + (uint)VO_GUI.BackL.Width, Position.Y + (LimitView - 1) * (PaddingBetweenChoices + _Next.Height) + (uint)VO_GUI.BackT.Height);
                        _Next.Visible = true;
                        _SpriteBatch.DrawString(_Next.SpriteFont, _Next.Text, _Next.Position, Color.White);
                    }
                }

                //Selection
                int x = 0;
                int y = 0;
                if (SelectedIndex < 0)
                {
                    if (SelectedIndex == -1)
                    {
                        x = (int)_Previous.Position.X - (int)blockSize / 2;
                        y = (int)_Previous.Position.Y - (int)blockSize / 2;
                    }
                    else if (SelectedIndex == -2)
                    {
                        x = (int)_Next.Position.X - (int)blockSize / 2;
                        y = (int)_Next.Position.Y - (int)blockSize / 2;
                    }
                }
                else
                {
                    x = (int)_Messages[SelectedIndex].Position.X - (int)blockSize / 2;
                    y = (int)_Messages[SelectedIndex].Position.Y - (int)blockSize / 2;
                }
                _MenuSelector.Position = new Vector2(x, y);
                _SpriteBatch.Draw(_MenuSelector.Image, new Rectangle((int)_MenuSelector.Position.X, (int)_MenuSelector.Position.Y, _MenuSelector.Width, _MenuSelector.Height), Color.White);
            }
        }
        #endregion

        #region Methods
        public void ForceWidth(int width)
        {
            _Width = width;
        }

        private void MakePreviousNext()
        {
            if (_Next == null)
            {
                _Next = new VO_String2D(GameCore.Instance.Game.Terminology.ChoiceNext, "Arial", _FontSize);
                _Previous = new VO_String2D(GameCore.Instance.Game.Terminology.ChoicePrevious, "Arial", _FontSize);
            }
        }

        /// <summary>
        /// Calcule le nombre de page en tenant compte de Previous et Next
        /// </summary>
        private void CalculatePageNumber()
        {
            if (LimitView >= _Messages.Count)
                _NbrPages = 1;
            else
            {
                _NbrPages = 1;
                int messagesRemaining = _Messages.Count - LimitView + 1;
                while (messagesRemaining > LimitView - 1)
                {
                    _NbrPages++;
                    messagesRemaining -= LimitView - 2;
                }
                if (messagesRemaining > 0)
                    _NbrPages++;
            }
        }

        /// <summary>
        /// Calcule la largeur et la hauteur du menu
        /// </summary>
        private void CalculateWidthAndHeight()
        {
            _Height = (int)VO_GUI.BackT.Height + (int)VO_GUI.BackT.Height;
            _Width = 0;

            if (_MenuBack != null)
                _MenuBack.Dispose();

            if (LimitView == 0)
                _NbrPages = 1;
            else
            {
                CalculatePageNumber();
                MakePreviousNext();
            }

            //Cas simple
            if (_NbrPages == 1)
            {
                foreach (VO_String2D message in _Messages)
                {
                    float messageWidth = message.Width;
                    if (_Width < (int)messageWidth)
                        _Width = (int)messageWidth;
                    _Height += PaddingBetweenChoices + (int)message.Height;
                }
            }

            //Cas complexe
            else
            {
                int index = 0;
                foreach (VO_String2D message in _Messages)
                {
                    float messageWidth = message.Width;
                    if (_Width < (int)messageWidth)
                        _Width = (int)messageWidth;
                    _Height += PaddingBetweenChoices + (int)message.Height;
                    index++;
                    if (LimitView == index)
                        break;
                }
            }

            _Width += (int)VO_GUI.BackL.Width + (int)VO_GUI.BackL.Width;
            _Height -= PaddingBetweenChoices;
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="width"></param>
        public void LimitWidth(int width)
        {
        }

        /// <summary>
        /// Reset le menu
        /// </summary>
        public void Clear()
        {
            _Width = 0;
            _Height = 0;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Déplacement dans le menu via le clavier
        /// </summary>
        /// <param name="e"></param>
        public void KeyPress()
        {
            if (KeyboardManager.OnClick(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex >= _Messages.Count)
                    SelectedIndex = _Messages.Count - 1;
                else if (this.SelectedValueChanged != null)
                    this.SelectedValueChanged(this, new GameMenuEventArgs(SelectedIndex, SelectedValue, _Messages[SelectedIndex].Text));
            }
            else if (KeyboardManager.OnClick(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex < 0)
                    SelectedIndex = 0;
                else if (this.SelectedValueChanged != null)
                    this.SelectedValueChanged(this, new GameMenuEventArgs(SelectedIndex, SelectedValue, _Messages[SelectedIndex].Text));
            }
            else if (KeyboardManager.OnClick(Keys.Space))
            {
                if (this.OnClick != null)
                {
                    //TODO: BUG NON RESOLU, A CORRIGER !!
                    try
                    {
                        this.OnClick(this, new GameMenuEventArgs(SelectedIndex, SelectedValue, _Messages[SelectedIndex].Text));
                    }
                    catch
                    {
                        KeyPress();
                    }
                }
            }
        }

        //Is selected
        private bool IsSelected(Rectangle sourceRect, int x, int y)
        {
            int width = GameCore.Instance.Game.Project.Resolution.Width;
            Rectangle source = new Rectangle(sourceRect.X, sourceRect.Y, width, sourceRect.Height);
            if (_MenuSelector == null)
                return false;

            return source.Contains(x, y);
        }

        /// <summary>
        /// Souris déplacée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(MouseState e)
        {
            if (_Previous != null && IsSelected(_Previous.Destination, e.X, e.Y) && _Previous.Visible)
            {
                ActionManager.ClickedState = true;
                SelectedIndex = -1;
                SelectedValue = new Guid(ViewerConstants.MESS_CHOICES_PREVIOUS_GUID);
                if (this.SelectedValueChanged != null)
                    this.SelectedValueChanged(this, new GameMenuEventArgs(SelectedIndex, SelectedValue, _Previous.Text));
                return;
            }
            if (_Next != null && IsSelected(_Next.Destination, e.X, e.Y) && _Next.Visible)
            {
                ActionManager.ClickedState = true;
                SelectedIndex = -2;
                SelectedValue = new Guid(ViewerConstants.MESS_CHOICES_NEXT_GUID);
                if (this.SelectedValueChanged != null)
                    this.SelectedValueChanged(this, new GameMenuEventArgs(SelectedIndex, SelectedValue, _Next.Text));
                return;
            }

            int startingIndex = 0;
            if (_CurrentMenuPage != 0)
                startingIndex = _CurrentMenuPage * (LimitView - 2) + 1;
            for (int i = startingIndex; i < _Messages.Count; i++)
            {
                if (IsSelected(_Messages[i].Destination, e.X, e.Y) && _Messages[i].Visible)
                {
                    ActionManager.ClickedState = true;
                    if (i != SelectedIndex)
                    {
                        SelectedIndex = i;
                        if (_MessagesGuids != null)
                            SelectedValue = _MessagesGuids[SelectedIndex];
                        if (this.SelectedValueChanged != null)
                            this.SelectedValueChanged(this, new GameMenuEventArgs(SelectedIndex, SelectedValue, Messages[SelectedIndex]));
                        return;
                    }
                    SelectedIndex = i;
                    return;
                }
            }
            ActionManager.ClickedState = false;
        }

        public void MousePress(MouseState e)
        {
            if (_Previous != null && _Previous.Visible && IsSelected(_Previous.Destination, e.X, e.Y))
            {
                SelectedIndex = -1;
                SelectedValue = new Guid(ViewerConstants.MESS_CHOICES_PREVIOUS_GUID);
                _CurrentMenuPage--;
                return;
            }
            if (_Next != null && _Next.Visible && IsSelected(_Next.Destination, e.X, e.Y))
            {
                SelectedIndex = -2;
                SelectedValue = new Guid(ViewerConstants.MESS_CHOICES_NEXT_GUID);
                _CurrentMenuPage++;
                return;
            }

            int startingIndex = 0;
            if (_CurrentMenuPage != 0)
                startingIndex = _CurrentMenuPage * (LimitView - 2) + 1;
            for (int i = startingIndex; i < _Messages.Count; i++)
            {
                if (_Messages[i].Visible && IsSelected(_Messages[i].Destination, e.X, e.Y))
                {
                    SelectedIndex = i;
                    if (_MessagesGuids != null)
                        SelectedValue = _MessagesGuids[SelectedIndex];
                    if (this.OnClick != null)
                        this.OnClick(this, new GameMenuEventArgs(SelectedIndex, SelectedValue, Messages[SelectedIndex]));
                    return;
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
            Clear();
            if (_MenuBack != null)
                _MenuBack.Dispose();
            if (_MenuSelector != null)
                _MenuSelector.Dispose();
        }
        #endregion
    }
}
