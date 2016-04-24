using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    public partial class CharacterButton : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service character
        /// </summary>
        CharacterService _ServiceCharacter;

        /// <summary>
        /// Référence au service character
        /// </summary>
        PlayerService _ServicePlayer;

        /// <summary>
        /// Valeur guid du bouton
        /// </summary>
        Guid _CharacterGuidValue;
        #endregion

        #region Events
        /// <summary>
        /// Survient quand la valeur de l'id du boutton change
        /// </summary>
        public event EventHandler ValueChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Bouton associé
        /// </summary>
        public Guid CharacterGuid
        {
            get
            {
                return _CharacterGuidValue;
            }
            set
            {
                _CharacterGuidValue = value;
                 VO_Base Character = null;
                if(UsePlayableCharacter)
                    Character = GameCore.Instance.GetPlayableCharacters().Find(p => p.Id == CharacterGuid);
                else
                    Character = GameCore.Instance.GetCharacters().Find(p => p.Id == CharacterGuid);
                if (Character != null)
                    txtButton.Text = Character.Title;
                else
                    txtButton.Text = GlobalConstants.UNKNOWN;
            }
        }

        /// <summary>
        /// Oui pour utiliser la source playable characters
        /// </summary>
        public bool UsePlayableCharacter
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public CharacterButton()
        {
            InitializeComponent();
            _ServiceCharacter = new CharacterService();
            _ServicePlayer = new PlayerService();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Ouvre le CharacterManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoose_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.CharacterManager.FormClosed += new FormClosedEventHandler(CharacterManager_FormClosed);
            FormsManager.Instance.CharacterManager.SelectedCharacter = CharacterGuid;
            FormsManager.Instance.CharacterManager.UsePlayableCharacter = UsePlayableCharacter;
            FormsManager.Instance.CharacterManager.ShowDialog(this);
        }

        /// <summary>
        /// Action lors de la fermeture du CharacterManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CharacterManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.CharacterManager.FormClosed -= new FormClosedEventHandler(CharacterManager_FormClosed);
            FormsManager.Instance.CharacterManager.UsePlayableCharacter = false;
            CharacterGuid = FormsManager.Instance.CharacterManager.SelectedCharacter;
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }
        #endregion
    }
}
