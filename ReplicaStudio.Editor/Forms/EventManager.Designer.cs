using ReplicaStudio.Editor.Forms;
namespace ReplicaStudio.Editor.Forms
{
    partial class EventManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventManager));
            this.Page_1 = new System.Windows.Forms.TabPage();
            this.ScriptManager = new ReplicaStudio.Editor.Forms.UserControls.ScriptManager();
            this.lblCommands = new System.Windows.Forms.Label();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.grpConditionsExecute = new System.Windows.Forms.GroupBox();
            this.itemButton1 = new ReplicaStudio.Editor.Forms.UserControls.ItemButton();
            this.ddpAction = new System.Windows.Forms.ComboBox();
            this.lblActionIsUsed = new System.Windows.Forms.Label();
            this.chkItem = new System.Windows.Forms.CheckBox();
            this.lblIsUsed = new System.Windows.Forms.Label();
            this.chkAction = new System.Windows.Forms.CheckBox();
            this.AnimationConditions = new ReplicaStudio.Editor.Forms.UserControls.AnimationsConditions();
            this.EventConditions = new ReplicaStudio.Editor.Forms.UserControls.EventConditions();
            this.CharacterConditions = new ReplicaStudio.Editor.Forms.UserControls.CharacterConditions();
            this.grpConditionsAppear = new System.Windows.Forms.GroupBox();
            this.characterButton1 = new ReplicaStudio.Editor.Forms.UserControls.CharacterButton();
            this.variableButton1 = new ReplicaStudio.Editor.Forms.UserControls.VariableButton();
            this.triggerButton1 = new ReplicaStudio.Editor.Forms.UserControls.TriggerButton();
            this.lblCharacterIsPlayed = new System.Windows.Forms.Label();
            this.chkCharacter = new System.Windows.Forms.CheckBox();
            this.lblVariableOrMore = new System.Windows.Forms.Label();
            this.txtVariableValue = new System.Windows.Forms.TextBox();
            this.lblVariableIs = new System.Windows.Forms.Label();
            this.chkVariable = new System.Windows.Forms.CheckBox();
            this.chkTrigger = new System.Windows.Forms.CheckBox();
            this.PagesManager = new System.Windows.Forms.TabControl();
            this.lblEventName = new System.Windows.Forms.Label();
            this.btnNewPage = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnDeletePage = new System.Windows.Forms.Button();
            this.btnEmpty = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtEventName = new System.Windows.Forms.TextBox();
            this.Page_1.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.grpConditionsExecute.SuspendLayout();
            this.grpConditionsAppear.SuspendLayout();
            this.PagesManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // Page_1
            // 
            this.Page_1.Controls.Add(this.ScriptManager);
            this.Page_1.Controls.Add(this.lblCommands);
            this.Page_1.Controls.Add(this.LeftPanel);
            resources.ApplyResources(this.Page_1, "Page_1");
            this.Page_1.Name = "Page_1";
            this.Page_1.UseVisualStyleBackColor = true;
            // 
            // ScriptManager
            // 
            resources.ApplyResources(this.ScriptManager, "ScriptManager");
            this.ScriptManager.Name = "ScriptManager";
            this.ScriptManager.Script = null;
            // 
            // lblCommands
            // 
            resources.ApplyResources(this.lblCommands, "lblCommands");
            this.lblCommands.Name = "lblCommands";
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.grpConditionsExecute);
            this.LeftPanel.Controls.Add(this.AnimationConditions);
            this.LeftPanel.Controls.Add(this.EventConditions);
            this.LeftPanel.Controls.Add(this.CharacterConditions);
            this.LeftPanel.Controls.Add(this.grpConditionsAppear);
            resources.ApplyResources(this.LeftPanel, "LeftPanel");
            this.LeftPanel.Name = "LeftPanel";
            // 
            // grpConditionsExecute
            // 
            this.grpConditionsExecute.Controls.Add(this.itemButton1);
            this.grpConditionsExecute.Controls.Add(this.ddpAction);
            this.grpConditionsExecute.Controls.Add(this.lblActionIsUsed);
            this.grpConditionsExecute.Controls.Add(this.chkItem);
            this.grpConditionsExecute.Controls.Add(this.lblIsUsed);
            this.grpConditionsExecute.Controls.Add(this.chkAction);
            resources.ApplyResources(this.grpConditionsExecute, "grpConditionsExecute");
            this.grpConditionsExecute.Name = "grpConditionsExecute";
            this.grpConditionsExecute.TabStop = false;
            // 
            // itemButton1
            // 
            this.itemButton1.ItemGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.itemButton1, "itemButton1");
            this.itemButton1.Name = "itemButton1";
            // 
            // ddpAction
            // 
            this.ddpAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpAction.FormattingEnabled = true;
            resources.ApplyResources(this.ddpAction, "ddpAction");
            this.ddpAction.Name = "ddpAction";
            // 
            // lblActionIsUsed
            // 
            resources.ApplyResources(this.lblActionIsUsed, "lblActionIsUsed");
            this.lblActionIsUsed.Name = "lblActionIsUsed";
            // 
            // chkItem
            // 
            resources.ApplyResources(this.chkItem, "chkItem");
            this.chkItem.Name = "chkItem";
            this.chkItem.UseVisualStyleBackColor = true;
            // 
            // lblIsUsed
            // 
            resources.ApplyResources(this.lblIsUsed, "lblIsUsed");
            this.lblIsUsed.Name = "lblIsUsed";
            // 
            // chkAction
            // 
            resources.ApplyResources(this.chkAction, "chkAction");
            this.chkAction.Name = "chkAction";
            this.chkAction.UseVisualStyleBackColor = true;
            // 
            // AnimationConditions
            // 
            resources.ApplyResources(this.AnimationConditions, "AnimationConditions");
            this.AnimationConditions.Name = "AnimationConditions";
            // 
            // EventConditions
            // 
            resources.ApplyResources(this.EventConditions, "EventConditions");
            this.EventConditions.Name = "EventConditions";
            // 
            // CharacterConditions
            // 
            resources.ApplyResources(this.CharacterConditions, "CharacterConditions");
            this.CharacterConditions.Name = "CharacterConditions";
            // 
            // grpConditionsAppear
            // 
            this.grpConditionsAppear.Controls.Add(this.characterButton1);
            this.grpConditionsAppear.Controls.Add(this.variableButton1);
            this.grpConditionsAppear.Controls.Add(this.triggerButton1);
            this.grpConditionsAppear.Controls.Add(this.lblCharacterIsPlayed);
            this.grpConditionsAppear.Controls.Add(this.chkCharacter);
            this.grpConditionsAppear.Controls.Add(this.lblVariableOrMore);
            this.grpConditionsAppear.Controls.Add(this.txtVariableValue);
            this.grpConditionsAppear.Controls.Add(this.lblVariableIs);
            this.grpConditionsAppear.Controls.Add(this.chkVariable);
            this.grpConditionsAppear.Controls.Add(this.chkTrigger);
            resources.ApplyResources(this.grpConditionsAppear, "grpConditionsAppear");
            this.grpConditionsAppear.Name = "grpConditionsAppear";
            this.grpConditionsAppear.TabStop = false;
            // 
            // characterButton1
            // 
            this.characterButton1.CharacterGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.characterButton1, "characterButton1");
            this.characterButton1.Name = "characterButton1";
            this.characterButton1.UsePlayableCharacter = true;
            // 
            // variableButton1
            // 
            resources.ApplyResources(this.variableButton1, "variableButton1");
            this.variableButton1.Name = "variableButton1";
            this.variableButton1.VariableGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // triggerButton1
            // 
            resources.ApplyResources(this.triggerButton1, "triggerButton1");
            this.triggerButton1.Name = "triggerButton1";
            this.triggerButton1.TriggerGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // lblCharacterIsPlayed
            // 
            resources.ApplyResources(this.lblCharacterIsPlayed, "lblCharacterIsPlayed");
            this.lblCharacterIsPlayed.Name = "lblCharacterIsPlayed";
            // 
            // chkCharacter
            // 
            resources.ApplyResources(this.chkCharacter, "chkCharacter");
            this.chkCharacter.Name = "chkCharacter";
            this.chkCharacter.UseVisualStyleBackColor = true;
            // 
            // lblVariableOrMore
            // 
            resources.ApplyResources(this.lblVariableOrMore, "lblVariableOrMore");
            this.lblVariableOrMore.Name = "lblVariableOrMore";
            // 
            // txtVariableValue
            // 
            resources.ApplyResources(this.txtVariableValue, "txtVariableValue");
            this.txtVariableValue.Name = "txtVariableValue";
            this.txtVariableValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EventManager_VariableValue_Pressed);
            this.txtVariableValue.Leave += new System.EventHandler(this.EventManager_VariableValue_Reseter);
            // 
            // lblVariableIs
            // 
            resources.ApplyResources(this.lblVariableIs, "lblVariableIs");
            this.lblVariableIs.Name = "lblVariableIs";
            // 
            // chkVariable
            // 
            resources.ApplyResources(this.chkVariable, "chkVariable");
            this.chkVariable.Name = "chkVariable";
            this.chkVariable.UseVisualStyleBackColor = true;
            // 
            // chkTrigger
            // 
            resources.ApplyResources(this.chkTrigger, "chkTrigger");
            this.chkTrigger.Name = "chkTrigger";
            this.chkTrigger.UseVisualStyleBackColor = true;
            // 
            // PagesManager
            // 
            this.PagesManager.Controls.Add(this.Page_1);
            resources.ApplyResources(this.PagesManager, "PagesManager");
            this.PagesManager.Name = "PagesManager";
            this.PagesManager.SelectedIndex = 0;
            this.PagesManager.SelectedIndexChanged += new System.EventHandler(this.EventManager_PageSelectedChanged);
            // 
            // lblEventName
            // 
            resources.ApplyResources(this.lblEventName, "lblEventName");
            this.lblEventName.Name = "lblEventName";
            // 
            // btnNewPage
            // 
            resources.ApplyResources(this.btnNewPage, "btnNewPage");
            this.btnNewPage.Name = "btnNewPage";
            this.btnNewPage.UseVisualStyleBackColor = true;
            this.btnNewPage.Click += new System.EventHandler(this.EventManager_NewPage);
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.EventManager_CopyPage);
            // 
            // btnDeletePage
            // 
            resources.ApplyResources(this.btnDeletePage, "btnDeletePage");
            this.btnDeletePage.Name = "btnDeletePage";
            this.btnDeletePage.UseVisualStyleBackColor = true;
            this.btnDeletePage.Click += new System.EventHandler(this.EventManager_DeletePage);
            // 
            // btnEmpty
            // 
            resources.ApplyResources(this.btnEmpty, "btnEmpty");
            this.btnEmpty.Name = "btnEmpty";
            this.btnEmpty.UseVisualStyleBackColor = true;
            this.btnEmpty.Click += new System.EventHandler(this.EventManager_EmptyPage);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.EventManager_ValidateChangesAndClose);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.EventManager_CancelChanges);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.EventManager_ValidateChanges);
            // 
            // txtEventName
            // 
            resources.ApplyResources(this.txtEventName, "txtEventName");
            this.txtEventName.Name = "txtEventName";
            // 
            // EventManager
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnEmpty);
            this.Controls.Add(this.btnDeletePage);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnNewPage);
            this.Controls.Add(this.txtEventName);
            this.Controls.Add(this.lblEventName);
            this.Controls.Add(this.PagesManager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Page_1.ResumeLayout(false);
            this.Page_1.PerformLayout();
            this.LeftPanel.ResumeLayout(false);
            this.grpConditionsExecute.ResumeLayout(false);
            this.grpConditionsExecute.PerformLayout();
            this.grpConditionsAppear.ResumeLayout(false);
            this.grpConditionsAppear.PerformLayout();
            this.PagesManager.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage Page_1;
        private System.Windows.Forms.TabControl PagesManager;
        private System.Windows.Forms.Label lblEventName;
        private System.Windows.Forms.Label lblCommands;
        private System.Windows.Forms.Panel LeftPanel;
        private UserControls.ScriptManager ScriptManager;
        private System.Windows.Forms.GroupBox grpConditionsAppear;
        private System.Windows.Forms.CheckBox chkTrigger;
        private System.Windows.Forms.Label lblVariableOrMore;
        private System.Windows.Forms.TextBox txtVariableValue;
        private System.Windows.Forms.Label lblVariableIs;
        private System.Windows.Forms.CheckBox chkVariable;
        private System.Windows.Forms.CheckBox chkCharacter;
        private System.Windows.Forms.Label lblCharacterIsPlayed;
        private System.Windows.Forms.CheckBox chkAction;
        private System.Windows.Forms.Label lblActionIsUsed;
        private UserControls.CharacterConditions CharacterConditions;
        private UserControls.EventConditions EventConditions;
        private System.Windows.Forms.Button btnNewPage;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnDeletePage;
        private System.Windows.Forms.Button btnEmpty;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private UserControls.AnimationsConditions AnimationConditions;
        private System.Windows.Forms.GroupBox grpConditionsExecute;
        private System.Windows.Forms.CheckBox chkItem;
        private System.Windows.Forms.Label lblIsUsed;
        private System.Windows.Forms.ComboBox ddpAction;
        private UserControls.TriggerButton triggerButton1;
        private System.Windows.Forms.TextBox txtEventName;
        private UserControls.VariableButton variableButton1;
        private UserControls.ItemButton itemButton1;
        private UserControls.CharacterButton characterButton1;

    }
}