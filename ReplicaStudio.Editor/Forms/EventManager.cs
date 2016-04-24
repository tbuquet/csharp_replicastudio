using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire de l'event manager
    /// </summary>
    public partial class EventManager : Form
    {
        #region Members
        /// <summary>
        /// Type d'évènement à charger
        /// </summary>
        private Enums.EventType _EventType;

        /// <summary>
        /// Référence au service
        /// </summary>
        EventService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Référence au stageobject
        /// </summary>
        public VO_StageObject CurrentStageObject { get; set; }

        /// <summary>
        /// Type d'evènement à afficher
        /// </summary>
        public Enums.EventType EventType {
            get
            {
                return _EventType;
            }
            set
            {
                _EventType = value;
                LoadSpecificPanels();
            }
        }

        /// <summary>
        /// Event actuellement chargé
        /// </summary>
        public VO_Event CurrentEvent;

        /// <summary>
        /// Page Actuellement sélectionnée
        /// </summary>
        public int PageIndex = 0;

        /// <summary>
        /// Liste des controles présent sur la vue pour futurs ajouts dans les nouvelles Pages
        /// </summary>
        List<Control> CurrentControlList = new List<Control>();

        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public EventManager()
        {
            InitializeComponent();
            _Service = new EventService();

            // Référencement des Contrôles contenu dans le TabPage
            foreach (Control CurrentControl in PagesManager.TabPages[0].Controls)
            {
                CurrentControlList.Add(CurrentControl);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge le bon type de contrôle pour l'evènement choisi
        /// </summary>
        public void LoadSpecificPanels()
        {
            CharacterConditions.Visible = false;
            EventConditions.Visible = false;
            AnimationConditions.Visible = false;
            if (EventType == Enums.EventType.Character)
            {
                CharacterConditions.Visible = true;
                grpConditionsExecute.Visible = false;
            }
            else if (EventType == Enums.EventType.Event)
            {
                EventConditions.Visible = true;
                grpConditionsExecute.Visible = true;
            }
            else
            {
                AnimationConditions.Visible = true;
                grpConditionsExecute.Visible = false;
            }
        }

        /// <summary>
        /// Chargement de l'EventManager
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            txtEventName.Text = CurrentStageObject.Title;

            //Reinitialisation a 1 TAB
            while (PagesManager.TabCount > 1)
            {
                PagesManager.TabPages.RemoveAt(PagesManager.TabCount - 1);
            }
            
            PageIndex = 0;
            int i = 0;

            // Creation de la liste de TAB correspondant a l'Event Chargé
            while (i < CurrentEvent.PageList.Count - 1)
            {
                TabPage NewTabPage = new TabPage(Convert.ToString(i + 2));

                PagesManager.TabPages.Add(NewTabPage);
                i++;
            }

            // Affectation des User Control sur le premierTab, qui est selectionné automatiquement
            PagesManager.SelectedIndex = 0;
            foreach (Control CurrentControl in CurrentControlList)
            {
                PagesManager.TabPages[0].Controls.Add(CurrentControl);
            }

            // Mise a jour des valeurs sur l'IHM
            UpdateEventManagerControlFromPage(CurrentEvent.PageList[0]);

            this.ScriptManager.EnableDrawManager();

            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Fonction Générique d'EventManager de mise à jour des champs selon la valeur de Page
        /// </summary>
        private void UpdateEventManagerControlFromPage(VO_Page CurrentPage)
        {
            //Actions
            chkAction.CheckedChanged -= new EventHandler(EventManager_ActivateAction);
            chkAction.Checked = CurrentPage.ActionActivated;
            CheckActionState();
            chkAction.CheckedChanged += new EventHandler(EventManager_ActivateAction);

            ddpAction.SelectedValueChanged -= new EventHandler(EventManager_ActionSelectionChanged);
            ddpAction.DataSource = GameCore.Instance.GetActions();
            ddpAction.DisplayMember = "Title";
            ddpAction.ValueMember = "Id";
            ddpAction.SelectedValue = CurrentPage.ActionId;
            ddpAction.SelectedValueChanged += new EventHandler(EventManager_ActionSelectionChanged);


            //Boutons
            chkTrigger.CheckedChanged -= new EventHandler(EventManager_ActivateTrigger);
            chkTrigger.Checked = CurrentPage.TriggerActivated;
            CheckTriggerState();
            chkTrigger.CheckedChanged += new EventHandler(EventManager_ActivateTrigger);

            triggerButton1.ValueChanged -= new EventHandler(EventManager_TriggerSelectedChanged);
            triggerButton1.TriggerGuid = CurrentPage.TriggerId;
            triggerButton1.ValueChanged += new EventHandler(EventManager_TriggerSelectedChanged);


            //Variables
            chkVariable.CheckedChanged -= new EventHandler(EventManager_ActivateVariable);
            chkVariable.Checked = CurrentPage.VariableActivated;
            CheckVariableState();
            chkVariable.CheckedChanged += new EventHandler(EventManager_ActivateVariable);

            txtVariableValue.TextChanged -= new EventHandler(EventManager_VariableValue_Changed);
            txtVariableValue.Text = Convert.ToString(CurrentPage.VariableValue);
            txtVariableValue.TextChanged += new EventHandler(EventManager_VariableValue_Changed);

            variableButton1.ValueChanged -= new EventHandler(EventManager_VariableSelectedChanged);
            variableButton1.VariableGuid = CurrentPage.VariableId;
            variableButton1.ValueChanged += new EventHandler(EventManager_VariableSelectedChanged);


            //Charaters
            chkCharacter.CheckedChanged -= new EventHandler(EventManager_ActivateCharacter);
            chkCharacter.Checked = CurrentPage.CharacterActivated;
            CheckCharacterState();
            chkCharacter.CheckedChanged += new EventHandler(EventManager_ActivateCharacter);

            characterButton1.ValueChanged -= new EventHandler(EventManager_CharacterSelectedChanged);
            characterButton1.CharacterGuid = CurrentPage.CharacterId;
            characterButton1.ValueChanged += new EventHandler(EventManager_CharacterSelectedChanged);


            //Items
            chkItem.CheckedChanged -= new EventHandler(EventManager_ActivateItem);
            chkItem.Checked = CurrentPage.ItemActivated;
            CheckItemState();
            chkItem.CheckedChanged += new EventHandler(EventManager_ActivateItem);

            itemButton1.ValueChanged -= new EventHandler(EventManager_ItemSelectedChanged);
            itemButton1.ItemGuid = CurrentPage.ItemId;
            itemButton1.ValueChanged += new EventHandler(EventManager_ItemSelectedChanged);


            //Sous controles
            if (CurrentStageObject is VO_StageAnimation)
                AnimationConditions.LoadControls(CurrentPage, ((VO_StageAnimation)CurrentStageObject).AnimationId);
            else if (CurrentStageObject is VO_StageHotSpot)
                EventConditions.LoadControls(CurrentPage);
            else if(CurrentStageObject is VO_StageCharacter)
                CharacterConditions.LoadControls(CurrentPage, ((VO_StageCharacter)CurrentStageObject).CharacterId);

            ScriptManager.LoadScript(CurrentPage.Script);
        }

        /// <summary>
        /// Verifie si le TriggerButton doit être actif
        /// </summary>
        private void CheckTriggerState()
        {
            if (chkTrigger.Checked)
                triggerButton1.Enabled = true;
            else
                triggerButton1.Enabled = false;
        }

        /// <summary>
        /// Verifie si le VariableButton doit être actif
        /// </summary>
        private void CheckVariableState()
        {
            if (chkVariable.Checked)
            {
                variableButton1.Enabled = true;
                lblVariableIs.Enabled = true;
                txtVariableValue.Enabled = true;
                lblVariableOrMore.Enabled = true;
            }
            else
            {
                variableButton1.Enabled = false;
                lblVariableIs.Enabled = false;
                txtVariableValue.Enabled = false;
                lblVariableOrMore.Enabled = false;
            }
        }

        /// <summary>
        /// Verifie si le CharacterButton doit être actif
        /// </summary>
        private void CheckCharacterState()
        {
            if (chkCharacter.Checked)
            {
                characterButton1.Enabled = true;
                lblCharacterIsPlayed.Enabled = true;
            }
            else
            {
                characterButton1.Enabled = false;
                lblCharacterIsPlayed.Enabled = false;
            }
        }

        /// <summary>
        /// Verifie si le ItemButton doit être actif
        /// </summary>
        private void CheckItemState()
        {
            if (chkItem.Checked)
            {
                itemButton1.Enabled = true;
                lblIsUsed.Enabled = true;
            }
            else
            {
                itemButton1.Enabled = false;
                lblIsUsed.Enabled = false;
            }
        }

        /// <summary>
        /// Verifie si le ActionButton doit être actif
        /// </summary>
        private void CheckActionState()
        {
            if (chkAction.Checked)
            {
                ddpAction.Enabled = true;
                lblActionIsUsed.Enabled = true;
            }
            else
            {
                ddpAction.Enabled = false;
                lblActionIsUsed.Enabled = false;
            }
        }
        #endregion

        #region Page UI Controls Events
        /// <summary>
        /// Activation / Désactivation Conditions Trigger
        /// </summary>
        private void EventManager_ActivateTrigger(object sender, EventArgs e)
        {
            CheckTriggerState();
            CurrentEvent.PageList[PageIndex].TriggerActivated = this.chkTrigger.Checked;
        }

        /// <summary>
        /// Activation / Désactivation Conditions Variable
        /// </summary>
        private void EventManager_ActivateVariable(object sender, EventArgs e)
        {
            CheckVariableState();
            CurrentEvent.PageList[PageIndex].VariableActivated = this.chkVariable.Checked;
        }

        /// <summary>
        /// Affectation de valeur de condition d'apparition sur Variable
        /// </summary>
        private void EventManager_VariableValue_Changed(object sender, EventArgs e)
        {
            // Verification que la valeur est bien un nombre...
            int ResultValue = 0;
            if (Int32.TryParse(txtVariableValue.Text, out ResultValue) == true)
                CurrentEvent.PageList[PageIndex].VariableValue = Convert.ToInt32(ResultValue);
            else
            {

                // Annulation de la dernière modification si la valeur n'était pas un nombre
                CurrentEvent.PageList[PageIndex].VariableValue = 0;
                txtVariableValue.Text = "0";
            }
        }

        /// <summary>
        /// Obligation de taper un nombre au clavier
        /// </summary>
        private void EventManager_VariableValue_Pressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                return;
            if (Char.IsControl(e.KeyChar) || !Char.IsNumber(e.KeyChar))
            {
                e.Handled = true; // Set l'evenement comme etant completement fini
                return;
            }

        }

        /// <summary>
        /// Ecriture du chiffre par défaut (0) si valeur invalide au Leave du controle
        /// </summary>
        private void EventManager_VariableValue_Reseter(object sender, EventArgs e)
        {
            if (txtVariableValue.Text == string.Empty)
                txtVariableValue.Text = "0";
            // Verification que la valeur est bien un nombre...
            int ResultValue = 0;
            if (Int32.TryParse(txtVariableValue.Text, out ResultValue) == true)
                CurrentEvent.PageList[PageIndex].VariableValue = Convert.ToInt32(ResultValue);
            else
            {
                // Annulation de la dernière modification si la valeur n'était pas un nombre
                CurrentEvent.PageList[PageIndex].VariableValue = 0;
            }
        }

        /// <summary>
        /// Activation / Désactivation Conditions Character
        /// </summary>
        private void EventManager_ActivateCharacter(object sender, EventArgs e)
        {
            CheckCharacterState();
            CurrentEvent.PageList[PageIndex].CharacterActivated = this.chkCharacter.Checked;
        }

        /// <summary>
        /// Activation / Désactivation Conditions Item
        /// </summary>
        private void EventManager_ActivateItem(object sender, EventArgs e)
        {
            CheckItemState();
            CurrentEvent.PageList[PageIndex].ItemActivated = this.chkItem.Checked;
        }

        /// <summary>
        /// Activation / Désactivation Conditions Action
        /// </summary>
        private void EventManager_ActivateAction(object sender, EventArgs e)
        {
            CheckActionState();
            CurrentEvent.PageList[PageIndex].ActionActivated = this.chkAction.Checked;
        }

        #endregion

        #region EventManager UI Controls Events

        /// <summary>
        /// Validation des changements
        /// </summary>
        private void EventManager_ValidateChanges(object sender, EventArgs e)
        {
            EventManager_UpdateEventName(sender, e);
            GameCore.Instance.SaveEvent(CurrentEvent);
        }

        /// <summary>
        /// Validation des changements et fermeture de la fenêtre
        /// </summary>
        private void EventManager_ValidateChangesAndClose(object sender, EventArgs e)
        {
            EventManager_UpdateEventName(sender, e);
            ddpAction.SelectedValueChanged -= new  EventHandler(EventManager_ActionSelectionChanged);
            this.ScriptManager.DisableDrawManager();
            this.Close();
        }

        /// <summary>
        /// Annulation des changements
        /// </summary>
        private void EventManager_CancelChanges(object sender, EventArgs e)
        {
            ddpAction.SelectedValueChanged -= new EventHandler(EventManager_ActionSelectionChanged);
            CurrentEvent = GameCore.Instance.RestoreEvent();
            this.ScriptManager.DisableDrawManager();
            this.Close();
        }

        /// <summary>
        /// Création d'une Nouvelle page dans l'EventManager
        /// </summary>
        private void EventManager_NewPage(object sender, EventArgs e)
        {
            // Instanciation d'une nouvelle Page et d'un VO_Page correspondant, puis sélection de la page
            int TabPageIndex = PagesManager.TabPages.Count;

            VO_Page NewPage = null;
            if(CurrentEvent.PageList[0].PageEventType == Enums.EventType.Character)
                NewPage = ObjectsFactory.CreatePage(CurrentEvent.PageList[0].PageEventType, CurrentEvent.PageList.Count + 1, ((VO_StageCharacter)CurrentStageObject).CharacterId);
            else
                NewPage = ObjectsFactory.CreatePage(CurrentEvent.PageList[0].PageEventType, CurrentEvent.PageList.Count + 1, Guid.Empty);
            CurrentEvent.PageList.Add(NewPage);

            TabPage NewTabPage = new TabPage(Convert.ToString(TabPageIndex + 1));

            foreach (Control CurrentControl in CurrentControlList)
            {
                NewTabPage.Controls.Add(CurrentControl);
            }

            PagesManager.TabPages.Add(NewTabPage);
            PagesManager.SelectedIndex = TabPageIndex;

            PageIndex = TabPageIndex;

            //Selection de la valeur par défaut la nouvelle page dans la ComboBox des Types d'execution de Trigger
            if (CurrentStageObject is VO_StageAnimation)
                AnimationConditions.LoadControls(NewPage, ((VO_StageAnimation)CurrentStageObject).AnimationId);
            else if (CurrentStageObject is VO_StageHotSpot)
                EventConditions.LoadControls(NewPage);
            else if (CurrentStageObject is VO_StageCharacter)
                CharacterConditions.LoadControls(NewPage, ((VO_StageCharacter)CurrentStageObject).CharacterId);
        }

        /// <summary>
        /// Copie d'une page dans l'EventManager
        /// </summary>
        private void EventManager_CopyPage(object sender, EventArgs e)
        {
            // Creation de la copie
            VO_Page OriginalPage = CurrentEvent.PageList[PageIndex];
            VO_Page CopyPage = OriginalPage.Clone();
            CopyPage.PageId = new Guid();
            CopyPage.PageNumber = CurrentEvent.PageList.Count + 1;
            CurrentEvent.PageList.Add(CopyPage);

            // Instanciation d'une nouvelle Page et d'un VO_Page correspondant, puis sélection de la page
            int TabPageIndex = PagesManager.TabPages.Count;

            TabPage NewTabPage = new TabPage(Convert.ToString(TabPageIndex + 1));

            foreach (Control CurrentControl in CurrentControlList)
            {
                NewTabPage.Controls.Add(CurrentControl);
            }

            PagesManager.TabPages.Add(NewTabPage);
            PagesManager.SelectedIndex = TabPageIndex;

            PageIndex = TabPageIndex;
        }

        /// <summary>
        /// Changement de Page dans l'EventManager
        /// </summary>
        private void EventManager_PageSelectedChanged(object sender, EventArgs e)
        {
            PageIndex = PagesManager.SelectedIndex;
            UpdateEventManagerControlFromPage(CurrentEvent.PageList[PageIndex]);
            foreach (Control CurrentControl in CurrentControlList)
            {
                PagesManager.TabPages[PagesManager.SelectedIndex].Controls.Add(CurrentControl);
            }
        }

        /// <summary>
        /// Vidage d'un VO_Page
        /// </summary>
        private void EventManager_EmptyPage(object sender, EventArgs e)
        {
            VO_Page CurrentPage = CurrentEvent.PageList[PageIndex];
            VO_Page EmptyPage = new VO_Page();

            EmptyPage.PageId = CurrentPage.PageId;
            EmptyPage.PageName = CurrentPage.PageName;
            EmptyPage.PageNumber = CurrentPage.PageNumber;

            CurrentEvent.PageList[PageIndex] = EmptyPage;
            UpdateEventManagerControlFromPage(EmptyPage);
        }

        /// <summary>
        /// Suppression d'une Page
        /// </summary>
        private void EventManager_DeletePage(object sender, EventArgs e)
        {
            // Pas de suppression de la première page, Simulation d'un vidage de page
            if (CurrentEvent.PageList.Count <= 1)
            {
                VO_Page CurrentPage = CurrentEvent.PageList[PageIndex];
                VO_Page EmptyPage = new VO_Page();

                EmptyPage.PageId = CurrentPage.PageId;
                EmptyPage.PageName = CurrentPage.PageName;
                EmptyPage.PageNumber = CurrentPage.PageNumber;

                CurrentEvent.PageList[PageIndex] = EmptyPage;
                UpdateEventManagerControlFromPage(EmptyPage);
                return;
            }

            int SelectedPageIndex = PageIndex;
            CurrentEvent.PageList.RemoveAt(PageIndex);
            PagesManager.TabPages.RemoveAt(PagesManager.TabPages.Count - 1);
            int Counter = 0;
            foreach (TabPage CurrentTabPage in PagesManager.TabPages)
            {
                CurrentTabPage.Text = Convert.ToString(Counter + 1);
                Counter = Counter + 1;
            }

            if (Counter < SelectedPageIndex)
                SelectedPageIndex = Counter;

            Counter = 0;
            foreach (VO_Page CurrentPage in CurrentEvent.PageList)
            {
                CurrentPage.PageNumber = Counter;
                Counter = Counter + 1;
            }

            PageIndex = SelectedPageIndex;

            PagesManager.SelectedIndex = PageIndex;
            UpdateEventManagerControlFromPage(CurrentEvent.PageList[PageIndex]);
            foreach (Control CurrentControl in CurrentControlList)
            {
                PagesManager.TabPages[PagesManager.SelectedIndex].Controls.Add(CurrentControl);
            }
        }

        /// <summary>
        /// Mise a jour des données si Trigger sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventManager_TriggerSelectedChanged(object sender, EventArgs e)
        {
            CurrentEvent.PageList[PageIndex].TriggerId = triggerButton1.TriggerGuid;
        }

        /// <summary>
        /// Mise a jour des données si Variable sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventManager_VariableSelectedChanged(object sender, EventArgs e)
        {
            CurrentEvent.PageList[PageIndex].VariableId = variableButton1.VariableGuid;
        }

        /// <summary>
        /// Mise a jour des données si Item sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventManager_ItemSelectedChanged(object sender, EventArgs e)
        {
            CurrentEvent.PageList[PageIndex].ItemId = itemButton1.ItemGuid;
        }

        /// <summary>
        /// Mise a jour des données si Character sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventManager_CharacterSelectedChanged(object sender, EventArgs e)
        {
            CurrentEvent.PageList[PageIndex].CharacterId = characterButton1.CharacterGuid;
        }

        /// <summary>
        /// Mets à jour le nom de l'event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventManager_UpdateEventName(object sender, EventArgs e)
        {
            CurrentStageObject.Title = txtEventName.Text;
        }

        /// <summary>
        /// Mise à jour de l'action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventManager_ActionSelectionChanged(object sender, EventArgs e)
        {
            if (ddpAction.ValueMember != null && ddpAction.ValueMember != string.Empty)
            {
                VO_Action CurrentAction = ddpAction.SelectedItem as VO_Action;
                if (CurrentAction != null)
                    CurrentEvent.PageList[PageIndex].ActionId = CurrentAction.Id;
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
