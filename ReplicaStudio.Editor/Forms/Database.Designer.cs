using ReplicaStudio.Shared.TransverseLayer.Constants;
namespace ReplicaStudio.Editor.Forms
{
    partial class Database
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Database));
            this.PanelAction = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.TabCharacters = new System.Windows.Forms.TabPage();
            this.UCCharacters = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseCharacters();
            this.TabItems = new System.Windows.Forms.TabPage();
            this.UCItems = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseItems();
            this.TabActions = new System.Windows.Forms.TabPage();
            this.UCActions = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseActions();
            this.TabClasses = new System.Windows.Forms.TabPage();
            this.UCClasses = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseClasses();
            this.Animations = new System.Windows.Forms.TabPage();
            this.TabGlobalEvents = new System.Windows.Forms.TabPage();
            this.UCGlobalEvents = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseGlobalEvents();
            this.TabMenus = new System.Windows.Forms.TabPage();
            this.UCMenu = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseMenus();
            this.TabInteractionItems = new System.Windows.Forms.TabPage();
            this.UCItemsInteraction = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseItemsInteraction();
            this.TabSystemSettings = new System.Windows.Forms.TabPage();
            this.UCSystem = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseSystem();
            this.TabTerminology = new System.Windows.Forms.TabPage();
            this.UCTerminology = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabaseTerminology();
            this.TabPlayers = new System.Windows.Forms.TabPage();
            this.UCPlayers = new ReplicaStudio.Editor.Forms.DatabaseUC.DatabasePlayers();
            this.PanelAction.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.TabCharacters.SuspendLayout();
            this.TabItems.SuspendLayout();
            this.TabActions.SuspendLayout();
            this.TabClasses.SuspendLayout();
            this.TabGlobalEvents.SuspendLayout();
            this.TabMenus.SuspendLayout();
            this.TabInteractionItems.SuspendLayout();
            this.TabSystemSettings.SuspendLayout();
            this.TabTerminology.SuspendLayout();
            this.TabPlayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelAction
            // 
            this.PanelAction.Controls.Add(this.btnOK);
            this.PanelAction.Controls.Add(this.btnCancel);
            this.PanelAction.Controls.Add(this.btnApply);
            resources.ApplyResources(this.PanelAction, "PanelAction");
            this.PanelAction.Name = "PanelAction";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.TabCharacters);
            this.Tabs.Controls.Add(this.TabPlayers);
            this.Tabs.Controls.Add(this.TabItems);
            this.Tabs.Controls.Add(this.TabActions);
            this.Tabs.Controls.Add(this.TabClasses);
            this.Tabs.Controls.Add(this.Animations);
            this.Tabs.Controls.Add(this.TabGlobalEvents);
            this.Tabs.Controls.Add(this.TabMenus);
            this.Tabs.Controls.Add(this.TabInteractionItems);
            this.Tabs.Controls.Add(this.TabSystemSettings);
            this.Tabs.Controls.Add(this.TabTerminology);
            resources.ApplyResources(this.Tabs, "Tabs");
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            // 
            // TabCharacters
            // 
            this.TabCharacters.Controls.Add(this.UCCharacters);
            resources.ApplyResources(this.TabCharacters, "TabCharacters");
            this.TabCharacters.Name = "TabCharacters";
            this.TabCharacters.UseVisualStyleBackColor = true;
            // 
            // UCCharacters
            // 
            this.UCCharacters.CurrentCharacter = null;
            resources.ApplyResources(this.UCCharacters, "UCCharacters");
            this.UCCharacters.Name = "UCCharacters";
            // 
            // TabItems
            // 
            this.TabItems.Controls.Add(this.UCItems);
            resources.ApplyResources(this.TabItems, "TabItems");
            this.TabItems.Name = "TabItems";
            this.TabItems.UseVisualStyleBackColor = true;
            // 
            // UCItems
            // 
            resources.ApplyResources(this.UCItems, "UCItems");
            this.UCItems.Name = "UCItems";
            // 
            // TabActions
            // 
            this.TabActions.Controls.Add(this.UCActions);
            resources.ApplyResources(this.TabActions, "TabActions");
            this.TabActions.Name = "TabActions";
            this.TabActions.UseVisualStyleBackColor = true;
            // 
            // UCActions
            // 
            resources.ApplyResources(this.UCActions, "UCActions");
            this.UCActions.Name = "UCActions";
            // 
            // TabClasses
            // 
            this.TabClasses.Controls.Add(this.UCClasses);
            resources.ApplyResources(this.TabClasses, "TabClasses");
            this.TabClasses.Name = "TabClasses";
            this.TabClasses.UseVisualStyleBackColor = true;
            // 
            // UCClasses
            // 
            resources.ApplyResources(this.UCClasses, "UCClasses");
            this.UCClasses.Name = "UCClasses";
            // 
            // Animations
            // 
            resources.ApplyResources(this.Animations, "Animations");
            this.Animations.Name = "Animations";
            this.Animations.UseVisualStyleBackColor = true;
            // 
            // TabGlobalEvents
            // 
            this.TabGlobalEvents.Controls.Add(this.UCGlobalEvents);
            resources.ApplyResources(this.TabGlobalEvents, "TabGlobalEvents");
            this.TabGlobalEvents.Name = "TabGlobalEvents";
            this.TabGlobalEvents.UseVisualStyleBackColor = true;
            // 
            // UCGlobalEvents
            // 
            resources.ApplyResources(this.UCGlobalEvents, "UCGlobalEvents");
            this.UCGlobalEvents.Name = "UCGlobalEvents";
            // 
            // TabMenus
            // 
            this.TabMenus.Controls.Add(this.UCMenu);
            resources.ApplyResources(this.TabMenus, "TabMenus");
            this.TabMenus.Name = "TabMenus";
            this.TabMenus.UseVisualStyleBackColor = true;
            // 
            // UCMenu
            // 
            resources.ApplyResources(this.UCMenu, "UCMenu");
            this.UCMenu.Name = "UCMenu";
            // 
            // TabInteractionItems
            // 
            this.TabInteractionItems.Controls.Add(this.UCItemsInteraction);
            resources.ApplyResources(this.TabInteractionItems, "TabInteractionItems");
            this.TabInteractionItems.Name = "TabInteractionItems";
            this.TabInteractionItems.UseVisualStyleBackColor = true;
            // 
            // UCItemsInteraction
            // 
            resources.ApplyResources(this.UCItemsInteraction, "UCItemsInteraction");
            this.UCItemsInteraction.Name = "UCItemsInteraction";
            // 
            // TabSystemSettings
            // 
            this.TabSystemSettings.Controls.Add(this.UCSystem);
            resources.ApplyResources(this.TabSystemSettings, "TabSystemSettings");
            this.TabSystemSettings.Name = "TabSystemSettings";
            this.TabSystemSettings.UseVisualStyleBackColor = true;
            // 
            // UCSystem
            // 
            resources.ApplyResources(this.UCSystem, "UCSystem");
            this.UCSystem.Name = "UCSystem";
            this.UCSystem.Project = null;
            // 
            // TabTerminology
            // 
            this.TabTerminology.Controls.Add(this.UCTerminology);
            resources.ApplyResources(this.TabTerminology, "TabTerminology");
            this.TabTerminology.Name = "TabTerminology";
            this.TabTerminology.UseVisualStyleBackColor = true;
            // 
            // UCTerminology
            // 
            resources.ApplyResources(this.UCTerminology, "UCTerminology");
            this.UCTerminology.Name = "UCTerminology";
            this.UCTerminology.Terminology = null;
            // 
            // TabPlayers
            // 
            this.TabPlayers.Controls.Add(this.UCPlayers);
            resources.ApplyResources(this.TabPlayers, "TabPlayers");
            this.TabPlayers.Name = "TabPlayers";
            this.TabPlayers.UseVisualStyleBackColor = true;
            // 
            // UCPlayers
            // 
            this.UCPlayers.CurrentCharacter = null;
            resources.ApplyResources(this.UCPlayers, "UCPlayers");
            this.UCPlayers.Name = "UCPlayers";
            // 
            // Database
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.PanelAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Database";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.OnLoad);
            this.PanelAction.ResumeLayout(false);
            this.Tabs.ResumeLayout(false);
            this.TabCharacters.ResumeLayout(false);
            this.TabItems.ResumeLayout(false);
            this.TabActions.ResumeLayout(false);
            this.TabClasses.ResumeLayout(false);
            this.TabGlobalEvents.ResumeLayout(false);
            this.TabMenus.ResumeLayout(false);
            this.TabInteractionItems.ResumeLayout(false);
            this.TabSystemSettings.ResumeLayout(false);
            this.TabTerminology.ResumeLayout(false);
            this.TabPlayers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void InitializeSDL()
        {
            this.UCAnimations = new ReplicaStudio.Editor.Forms.UserControls.AnimationManager(Enums.AnimationType.ObjectAnimation);
            this.UCAnimations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UCAnimations.Location = new System.Drawing.Point(0, 0);
            this.UCAnimations.Name = "UCAnimations";
            this.UCAnimations.Padding = new System.Windows.Forms.Padding(3);
            this.UCAnimations.Size = new System.Drawing.Size(786, 479);
            this.UCAnimations.TabIndex = 0;
            this.Animations.Controls.Add(this.UCAnimations);
        }

        #endregion

        private System.Windows.Forms.Panel PanelAction;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage TabCharacters;
        private System.Windows.Forms.TabPage TabItems;
        private System.Windows.Forms.TabPage TabActions;
        private System.Windows.Forms.TabPage TabMenus;
        private System.Windows.Forms.TabPage Animations;
        private System.Windows.Forms.TabPage TabGlobalEvents;
        private System.Windows.Forms.TabPage TabSystemSettings;
        private System.Windows.Forms.TabPage TabTerminology;
        private DatabaseUC.DatabaseCharacters UCCharacters;
        private System.Windows.Forms.TabPage TabInteractionItems;
        private DatabaseUC.DatabaseItems UCItems;
        private DatabaseUC.DatabaseActions UCActions;
        private DatabaseUC.DatabaseGlobalEvents UCGlobalEvents;
        private DatabaseUC.DatabaseMenus UCMenu;
        private DatabaseUC.DatabaseItemsInteraction UCItemsInteraction;
        private DatabaseUC.DatabaseSystem UCSystem;
        private UserControls.AnimationManager UCAnimations;
        private System.Windows.Forms.TabPage TabClasses;
        private DatabaseUC.DatabaseClasses UCClasses;
        private DatabaseUC.DatabaseTerminology UCTerminology;
        private System.Windows.Forms.TabPage TabPlayers;
        private DatabaseUC.DatabasePlayers UCPlayers;
    }
}