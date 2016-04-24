using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms
{
    public partial class CharacterManager : Form
    {
        #region Members
        /// <summary>
        /// Référence au service Character
        /// </summary>
        CharacterService _ServiceCharacter;

        /// <summary>
        /// Référence au service Player
        /// </summary>
        PlayerService _ServicePlayer;
        #endregion

        #region Properties
        /// <summary>
        /// Bouton sélectionné
        /// </summary>
        public Guid SelectedCharacter
        {
            get;
            set;
        }

        /// <summary>
        /// Bouton courant
        /// </summary>
        public VO_Base CurrentCharacter
        {
            get;
            set;
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
        public CharacterManager()
        {
            InitializeComponent();
            _ServiceCharacter = new CharacterService();
            _ServicePlayer = new PlayerService();
        }
        #endregion

        #region Eventhandlers
        /// <summary>
        /// Au chargement du controle
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CurrentCharacter = null;
            ProvisionList();
            if (ListCharacters.DataSource.Count > 0)
            {
                Guid firstAction = ListCharacters.DataSource[0].Id;
                ListCharacters.SelectItem(firstAction);
                LoadCharacter(firstAction);
            }
            else
                ListCharacters_ListIsEmpty(this, new EventArgs());
        }

        /// <summary>
        /// Code ajouté lors de la sélection d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_CharacterChosen(object sender, EventArgs e)
        {
            LoadCharacter(ListCharacters.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_CharacterToCreate(object sender, EventArgs e)
        {
            VO_Character newCharacter = _ServiceCharacter.CreateCharacter();
            newCharacter.Title = GlobalConstants.CHARACTERS_NEW_ITEM;
            ListCharacters.AddItem(newCharacter.Id, newCharacter.Title);
            LoadCharacter(newCharacter.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_CharacterToDelete(object sender, EventArgs e)
        {
            CurrentCharacter.Delete();
            CurrentCharacter = null;
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ListIsEmpty(object sender, EventArgs e)
        {
            //grpInformations.Visible = false;
        }

        /// <summary>
        /// Code ajouté lors d'un double clic sur un Character de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_CharacterDoubleClicked(object sender, EventArgs e)
        {
            this.btnSelect_Click(this, new EventArgs());
        }

        /// <summary>
        /// Click sur select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(CurrentCharacter != null)
                SelectedCharacter = CurrentCharacter.Id;
            this.Close();
        }

        /// <summary>
        /// Click sur cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Le titre a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// La position initiale du bouton a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTrue_CheckedChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge un bouton
        /// </summary>
        /// <param name="value">Id du bouton</param>
        public void LoadCharacter(Guid value)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            if(UsePlayableCharacter)
                CurrentCharacter = GameCore.Instance.GetPlayableCharacterById(value);
            else
                CurrentCharacter = GameCore.Instance.GetCharacterById(value);

            ////Afficher les groupes
            //grpInformations.Visible = true;

            //Bind des infos dans les contrôles

            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Charge la liste des boutons
        /// </summary>
        private void ProvisionList()
        {
            if (UsePlayableCharacter)
            {
                ListCharacters.DataSource = _ServicePlayer.ProvisionList();
                ListCharacters.LoadList();
            }
            else
            {
                ListCharacters.DataSource = _ServiceCharacter.ProvisionList();
                ListCharacters.LoadList();
            }
        }
        #endregion

        #region Override
        /// <summary>
        /// Désactiver F4
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;
            else
                return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}
