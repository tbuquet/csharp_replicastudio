using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire Characters de la database
    /// </summary>
    public partial class DatabasePlayers : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        PlayerService _Service;

        private VO_Character _characterTemplate;
        #endregion

        #region Properties
        /// <summary>
        /// Character courant
        /// </summary>
        public VO_PlayableCharacter CurrentCharacter { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabasePlayers()
        {
            InitializeComponent();
            _Service = new PlayerService();
            ListPlayers.Title = Culture.Language.DatabaseRessources.DatabasePlayers;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Méthode qui charge l'animation courante
        /// </summary>
        public void LoadCharacter(Guid guid)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Suppression des eventhandler
            this.chkLife.CheckedChanged -= new System.EventHandler(this.chkLife_CheckedChanged);
            this.ddpPVStarting.ValueChanged -= new System.EventHandler(this.ddpPV_ValueChanged);
            this.ddpPVMax.ValueChanged -= new System.EventHandler(this.ddpPV_ValueChanged);
            this.txtName.LostFocus -= new System.EventHandler(this.txtName_TextChanged);
            this.crdStartingPosition.ValueChanged -= new EventHandler(crdStartingPosition_ValueChanged);

            //Code de chargement
            CurrentCharacter = GameCore.Instance.GetPlayableCharacterById(guid);
            _characterTemplate = GameCore.Instance.GetCharacterById(CurrentCharacter.CharacterId);
            txtName.Text = CurrentCharacter.Title;
            ddpPVMax.Value = CurrentCharacter.PvMax;
            ddpPVStarting.Value = CurrentCharacter.PvAtStart;
            chkLife.Checked = CurrentCharacter.ActivateLife;
            chkLife_CheckedChanged(this, new EventArgs());
            crdStartingPosition.FullCoords = CurrentCharacter.CoordsCharacter;
            AnimCharacter.ParentCharacter = _characterTemplate.Id;

            LoadLists();
            if (_characterTemplate.StandingAnim != new Guid())
            {
                AnimCharacter.UseCustomRow = true;
                AnimCharacter.Row = (int)CurrentCharacter.StartPosition;
                AnimCharacter.LoadAnimation(_characterTemplate.StandingAnim);
                AnimCharacter.Start();
            }
            else
                AnimCharacter.LoadAnimation(new Guid());            

            //Chargement des actions
            ListSelectedActions.Items.Clear();
            ListSelectedActions.DisplayMember = "Title";
            ListSelectedActions.ValueMember = "Id";
            ListAvailableActions.Items.Clear();
            ListAvailableActions.DisplayMember = "Title";
            ListAvailableActions.ValueMember = "Id";
            foreach (VO_Base action in GameCore.Instance.Game.Actions)
            {
                if (CurrentCharacter.Actions.Contains(action.Id))
                    ListSelectedActions.Items.Add(action);
                else
                    ListAvailableActions.Items.Add(action);
            }

            //Chargement des items
            ListSelectedItems.Items.Clear();
            ListSelectedItems.DisplayMember = "Title";
            ListSelectedItems.ValueMember = "Id";
            ListAvailableItems.Items.Clear();
            ListAvailableItems.DisplayMember = "Title";
            ListAvailableItems.ValueMember = "Id";
            foreach (VO_Base item in GameCore.Instance.Game.Items)
            {
                if (CurrentCharacter.Items.Contains(item.Id))
                    ListSelectedItems.Items.Add(item);
                else
                    ListAvailableItems.Items.Add(item);
            }

            this.chkLife.CheckedChanged += new System.EventHandler(this.chkLife_CheckedChanged);
            this.ddpPVStarting.ValueChanged += new System.EventHandler(this.ddpPV_ValueChanged);
            this.ddpPVMax.ValueChanged += new System.EventHandler(this.ddpPV_ValueChanged);
            this.txtName.LostFocus += new System.EventHandler(this.txtName_TextChanged);
            this.crdStartingPosition.ValueChanged += new EventHandler(crdStartingPosition_ValueChanged);

            grpAnimations.Visible = true;
            grpInteractions.Visible = true;
            grpInformations.Visible = true;
            grpLife.Visible = true;

            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Charger les listes propres à CharacterDB
        /// </summary>
        private void LoadLists()
        {
            this.ddpStartingDirection.SelectedIndexChanged -= new System.EventHandler(this.ddpStartingDirection_SelectedIndexChanged);
            this.ddpCharacterTemplate.SelectedValueChanged -= new EventHandler(ddpCharacterTemplate_SelectedValueChanged);

            List<VO_Base> list = _Service.GetCharacterTemplateList(CurrentCharacter.Id);
            ddpCharacterTemplate.DataSource = list;
            ddpCharacterTemplate.DisplayMember = "Title";
            ddpCharacterTemplate.ValueMember = "Id";
            ddpCharacterTemplate.SelectedValue = CurrentCharacter.CharacterId;
            List<VO_ListItem> list2 = FormsTools.GetMovementsList();
            ddpStartingDirection.DataSource = list2;
            ddpStartingDirection.DisplayMember = "Title";
            ddpStartingDirection.ValueMember = "Id";
            ddpStartingDirection.SelectedValue = (int)CurrentCharacter.StartPosition;
            if (ddpStartingDirection.SelectedValue == null)
            {
                CurrentCharacter.StartPosition = 0;
                ddpStartingDirection.SelectedValue = 0;
            }

            this.ddpStartingDirection.SelectedIndexChanged += new System.EventHandler(this.ddpStartingDirection_SelectedIndexChanged);
            this.ddpCharacterTemplate.SelectedValueChanged += new EventHandler(ddpCharacterTemplate_SelectedValueChanged);
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        private void ProvisionList()
        {
            ListPlayers.DataSource = _Service.ProvisionList();
            ListPlayers.LoadList();
        }

        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBPlayers()
        {
            CurrentCharacter = null;
            ProvisionList();
            if (ListPlayers.DataSource.Count > 0)
            {
                Guid firstAnimation = ListPlayers.DataSource[0].Id;
                ListPlayers.SelectItem(firstAnimation);
                LoadCharacter(firstAnimation);
            }
            else
                ListCharacters_ListIsEmpty(this, new EventArgs());
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Position de départ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void crdStartingPosition_ValueChanged(object sender, EventArgs e)
        {
            CurrentCharacter.CoordsCharacter = crdStartingPosition.FullCoords;
        }

        /// <summary>
        /// Activation ou désactivation des contrôles de PV.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLife_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLife.Checked)
            {
                lblPVStarting.Enabled = true;
                lblPVMax.Enabled = true;
                ddpPVMax.Enabled = true;
                ddpPVStarting.Enabled = true;
            }
            else
            {
                lblPVStarting.Enabled = false;
                lblPVMax.Enabled = false;
                ddpPVMax.Enabled = false;
                ddpPVStarting.Enabled = false;
            }
            CurrentCharacter.ActivateLife = chkLife.Checked;
        }

        /// <summary>
        /// Limitation du contrôle PV starting en fonction de PVMax.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpPV_ValueChanged(object sender, EventArgs e)
        {
            if (ddpPVMax.Value < ddpPVStarting.Value)
                ddpPVStarting.Value = ddpPVMax.Value;
            CurrentCharacter.PvAtStart = Convert.ToInt32(ddpPVStarting.Value);
            CurrentCharacter.PvMax = Convert.ToInt32(ddpPVMax.Value);
        }

        /// <summary>
        /// Template perso change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ddpCharacterTemplate_SelectedValueChanged(object sender, EventArgs e)
        {
            object obj = ddpCharacterTemplate.SelectedValue;
            if (obj != null && obj is VO_Character)
            {
                CurrentCharacter.CharacterId = ((VO_Character)obj).Id;
            }
            else if(obj != null && obj is Guid)
            {
                CurrentCharacter.CharacterId = (Guid)obj;
            }
            LoadCharacter(CurrentCharacter.Id);
        }

        /// <summary>
        /// Masque tous les contrôles lorsqu'il n'y a pas d'item dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ListIsEmpty(object sender, EventArgs e)
        {
            grpAnimations.Visible = false;
            grpInteractions.Visible = false;
            grpInformations.Visible = false;
            grpLife.Visible = false;
        }

        /// <summary>
        /// Charge un nouveau character en fonction de l'item choisi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ItemChosen(object sender, EventArgs e)
        {
            LoadCharacter(ListPlayers.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ItemToCreate(object sender, EventArgs e)
        {
            VO_PlayableCharacter newChar = _Service.CreatePlayer();
            ListPlayers.AddItem(newChar.Id, newChar.Title);
            LoadCharacter(newChar.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ItemToDelete(object sender, EventArgs e)
        {
            CurrentCharacter.Delete();
            CurrentCharacter = null;
        }

        /// <summary>
        /// Synchronise le label d'un item avec la liste d'items et le titre dans le formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (ListPlayers.ChangeItemName(CurrentCharacter.Id, txtName.Text))
            {
                CurrentCharacter.Title = txtName.Text;
            }
            else
            {
                txtName.Text = CurrentCharacter.Title;
                MessageBox.Show(Errors.ERROR_UNIQUE_TITLE, Errors.ERROR_BOX_TITLE);
            }
        }

        /// <summary>
        /// Changement de la valeur de la direction de départ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpStartingDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentCharacter.StartPosition = (Enums.Movement)((VO_ListItem)ddpStartingDirection.SelectedItem).Id;
            this.LoadCharacter(CurrentCharacter.Id);
        }

        /// <summary>
        /// Double clic pour supprimer une action pour un personnage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSelectedActions_DoubleClick(object sender, EventArgs e)
        {
            if (ListSelectedActions.SelectedItem != null)
            {
                Guid selectedGuid = ((VO_Base)ListSelectedActions.SelectedItem).Id;
                foreach(VO_Action action in GameCore.Instance.SAVEDB.Actions)
                {
                    if (action.Id == selectedGuid && !action.GoAction && !action.UseAction)
                    {
                        CurrentCharacter.Actions.Remove(selectedGuid);
                        LoadCharacter(CurrentCharacter.Id);
                    }
                }
            }
        }

        /// <summary>
        /// Double clic pour supprimer un item pour un personnage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSelectedItems_DoubleClick(object sender, EventArgs e)
        {
            if (ListSelectedItems.SelectedItem != null)
            {
                Guid selectedGuid = ((VO_Base)ListSelectedItems.SelectedItem).Id;
                foreach (VO_Item item in GameCore.Instance.SAVEDB.Items)
                {
                    if (item.Id == selectedGuid)
                    {
                        CurrentCharacter.Items.Remove(selectedGuid);
                        LoadCharacter(CurrentCharacter.Id);
                    }
                }
            }
        }

        /// <summary>
        /// Ajoute une action pour le personnage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAction_Click(object sender, EventArgs e)
        {
            if (ListAvailableActions.SelectedItem != null)
            {
                Guid selectedGuid = ((VO_Base)ListAvailableActions.SelectedItem).Id;
                CurrentCharacter.Actions.Add(selectedGuid);
                LoadCharacter(CurrentCharacter.Id);
            }
        }

        /// <summary>
        /// Ajoute un item pour le personnage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (ListAvailableItems.SelectedItem != null)
            {
                Guid selectedGuid = ((VO_Base)ListAvailableItems.SelectedItem).Id;
                CurrentCharacter.Items.Add(selectedGuid);
                LoadCharacter(CurrentCharacter.Id);
            }
        }

        /// <summary>
        /// Recharger le character si changement de visibilité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAction_VisibleChanged(object sender, EventArgs e)
        {
            if (CurrentCharacter != null)
            {
                LoadCharacter(CurrentCharacter.Id);
            }
        }
        #endregion 
    }
}
