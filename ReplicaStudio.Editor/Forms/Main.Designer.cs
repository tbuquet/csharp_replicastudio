namespace ReplicaStudio.Editor.Forms
{
    partial class Main
    {
        private UserControls.StagePanel StagePanel;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_GeneralSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Edition = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_View = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_ShowHideToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_ShowHideRightPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_ShowHideLeftPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Mode = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Decors = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Objects = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Characters = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Events = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Walks = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Regions = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Drawing = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Pointer = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Pencil = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Zoom = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Z11 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Z12 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Z14 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Z18 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Resources = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_CreateNewMap = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_DeleteStage = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Separator10 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu_Database = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_ResourcesManager = new System.Windows.Forms.ToolStripMenuItem();
            this.spriteCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Build = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_ExportToPC = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Game = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Try = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_TryFullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.langageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_AboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Documentation = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Website = new System.Windows.Forms.ToolStripMenuItem();
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.Toolbar_NewProject = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_LoadProject = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Save = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Toolbar_Cut = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Copy = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Paste = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Delete = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Toolbar_Decor = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Objects = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Characters = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Events = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Walk = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Regions = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Separator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Toolbar_Pointer = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Pencil = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Separator5 = new System.Windows.Forms.ToolStripSeparator();
            this.Toolbar_Z11 = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Z12 = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Z14 = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Z18 = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Separator6 = new System.Windows.Forms.ToolStripSeparator();
            this.Toolbar_CreateNewMap = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Database = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_ResourcesManager = new System.Windows.Forms.ToolStripButton();
            this.Toolbar_Separator7 = new System.Windows.Forms.ToolStripSeparator();
            this.Toolbar_Try = new System.Windows.Forms.ToolStripButton();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.InfoPanel = new ReplicaStudio.Editor.Forms.UserControls.InfoPanel();
            this.VerticalLeftSplitterBottom = new System.Windows.Forms.Splitter();
            this.ProjectPanel = new ReplicaStudio.Editor.Forms.UserControls.ProjectPanel();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.VerticalRightSplitter = new System.Windows.Forms.Splitter();
            this.StageObjectsPanel = new ReplicaStudio.Editor.Forms.UserControls.StageObjectsPanel();
            this.LayersPanel = new ReplicaStudio.Editor.Forms.UserControls.LayersPanel();
            this.MiddlePanel = new System.Windows.Forms.Panel();
            this.PanelMiddleSplitted = new System.Windows.Forms.Panel();
            this.PanelRightSplitter = new System.Windows.Forms.Panel();
            this.PanelLeftSplitter = new System.Windows.Forms.Panel();
            this.LeftSplitter = new System.Windows.Forms.Splitter();
            this.RightSplitter = new System.Windows.Forms.Splitter();
            this.StatusBar = new System.Windows.Forms.Panel();
            this.StatusPanelRight = new System.Windows.Forms.Panel();
            this.lblStatusObjectFocus = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblStatusMap = new System.Windows.Forms.Label();
            this.StatusPanelLeft = new System.Windows.Forms.Panel();
            this.lblStatusDescription = new System.Windows.Forms.Label();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.InfoValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InfoProperties = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.MainMenu.SuspendLayout();
            this.Toolbar.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.MiddlePanel.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.StatusPanelRight.SuspendLayout();
            this.StatusPanelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_File,
            this.MainMenu_Edition,
            this.MainMenu_View,
            this.MainMenu_Mode,
            this.MainMenu_Drawing,
            this.MainMenu_Zoom,
            this.MainMenu_Resources,
            this.MainMenu_Build,
            this.MainMenu_Game,
            this.langageToolStripMenuItem,
            this.MainMenu_About});
            resources.ApplyResources(this.MainMenu, "MainMenu");
            this.MainMenu.Name = "MainMenu";
            // 
            // MainMenu_File
            // 
            this.MainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_New,
            this.MainMenu_Load,
            this.MainMenu_Save,
            this.toolStripSeparator1,
            this.MainMenu_GeneralSettings,
            this.MainMenu_Exit});
            this.MainMenu_File.Name = "MainMenu_File";
            resources.ApplyResources(this.MainMenu_File, "MainMenu_File");
            this.MainMenu_File.MouseEnter += new System.EventHandler(this.MainMenu_File_MouseEnter);
            // 
            // MainMenu_New
            // 
            this.MainMenu_New.Name = "MainMenu_New";
            resources.ApplyResources(this.MainMenu_New, "MainMenu_New");
            this.MainMenu_New.Click += new System.EventHandler(this.MainMenu_New_Click);
            this.MainMenu_New.MouseEnter += new System.EventHandler(this.MainMenu_New_MouseEnter);
            // 
            // MainMenu_Load
            // 
            this.MainMenu_Load.Name = "MainMenu_Load";
            resources.ApplyResources(this.MainMenu_Load, "MainMenu_Load");
            this.MainMenu_Load.Click += new System.EventHandler(this.MainMenu_Load_Click);
            this.MainMenu_Load.MouseEnter += new System.EventHandler(this.MainMenu_Load_MouseEnter);
            // 
            // MainMenu_Save
            // 
            this.MainMenu_Save.Name = "MainMenu_Save";
            resources.ApplyResources(this.MainMenu_Save, "MainMenu_Save");
            this.MainMenu_Save.Click += new System.EventHandler(this.MainMenu_Save_Click);
            this.MainMenu_Save.MouseEnter += new System.EventHandler(this.MainMenu_Save_MouseEnter);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // MainMenu_GeneralSettings
            // 
            this.MainMenu_GeneralSettings.Name = "MainMenu_GeneralSettings";
            resources.ApplyResources(this.MainMenu_GeneralSettings, "MainMenu_GeneralSettings");
            this.MainMenu_GeneralSettings.Click += new System.EventHandler(this.MainMenu_GeneralSettings_Click);
            this.MainMenu_GeneralSettings.MouseEnter += new System.EventHandler(this.MainMenu_GeneralSettings_MouseEnter);
            // 
            // MainMenu_Exit
            // 
            this.MainMenu_Exit.Name = "MainMenu_Exit";
            resources.ApplyResources(this.MainMenu_Exit, "MainMenu_Exit");
            this.MainMenu_Exit.Click += new System.EventHandler(this.MainMenu_Exit_Click);
            this.MainMenu_Exit.MouseEnter += new System.EventHandler(this.MainMenu_Exit_MouseEnter);
            // 
            // MainMenu_Edition
            // 
            this.MainMenu_Edition.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Cut,
            this.MainMenu_Copy,
            this.MainMenu_Paste,
            this.MainMenu_Delete});
            resources.ApplyResources(this.MainMenu_Edition, "MainMenu_Edition");
            this.MainMenu_Edition.Name = "MainMenu_Edition";
            this.MainMenu_Edition.MouseEnter += new System.EventHandler(this.MainMenu_Edition_MouseEnter);
            // 
            // MainMenu_Cut
            // 
            this.MainMenu_Cut.Name = "MainMenu_Cut";
            resources.ApplyResources(this.MainMenu_Cut, "MainMenu_Cut");
            this.MainMenu_Cut.MouseEnter += new System.EventHandler(this.MainMenu_Cut_MouseEnter);
            // 
            // MainMenu_Copy
            // 
            this.MainMenu_Copy.Name = "MainMenu_Copy";
            resources.ApplyResources(this.MainMenu_Copy, "MainMenu_Copy");
            this.MainMenu_Copy.MouseEnter += new System.EventHandler(this.MainMenu_Copy_MouseEnter);
            // 
            // MainMenu_Paste
            // 
            this.MainMenu_Paste.Name = "MainMenu_Paste";
            resources.ApplyResources(this.MainMenu_Paste, "MainMenu_Paste");
            this.MainMenu_Paste.MouseEnter += new System.EventHandler(this.MainMenu_Paste_MouseEnter);
            // 
            // MainMenu_Delete
            // 
            this.MainMenu_Delete.Name = "MainMenu_Delete";
            resources.ApplyResources(this.MainMenu_Delete, "MainMenu_Delete");
            this.MainMenu_Delete.MouseEnter += new System.EventHandler(this.MainMenu_Delete_MouseEnter);
            // 
            // MainMenu_View
            // 
            this.MainMenu_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_ShowHideToolbar,
            this.MainMenu_ShowHideRightPanel,
            this.MainMenu_ShowHideLeftPanel});
            this.MainMenu_View.Name = "MainMenu_View";
            resources.ApplyResources(this.MainMenu_View, "MainMenu_View");
            this.MainMenu_View.MouseEnter += new System.EventHandler(this.MainMenu_View_MouseEnter);
            // 
            // MainMenu_ShowHideToolbar
            // 
            this.MainMenu_ShowHideToolbar.Name = "MainMenu_ShowHideToolbar";
            resources.ApplyResources(this.MainMenu_ShowHideToolbar, "MainMenu_ShowHideToolbar");
            this.MainMenu_ShowHideToolbar.Click += new System.EventHandler(this.MainMenu_ShowHideToolbar_Click);
            this.MainMenu_ShowHideToolbar.MouseEnter += new System.EventHandler(this.MainMenu_ShowHideToolbar_MouseEnter);
            // 
            // MainMenu_ShowHideRightPanel
            // 
            this.MainMenu_ShowHideRightPanel.Name = "MainMenu_ShowHideRightPanel";
            resources.ApplyResources(this.MainMenu_ShowHideRightPanel, "MainMenu_ShowHideRightPanel");
            this.MainMenu_ShowHideRightPanel.Click += new System.EventHandler(this.MainMenu_ShowHideRightPanel_Click);
            this.MainMenu_ShowHideRightPanel.MouseEnter += new System.EventHandler(this.MainMenu_ShowHideRightPanel_MouseEnter);
            // 
            // MainMenu_ShowHideLeftPanel
            // 
            this.MainMenu_ShowHideLeftPanel.Name = "MainMenu_ShowHideLeftPanel";
            resources.ApplyResources(this.MainMenu_ShowHideLeftPanel, "MainMenu_ShowHideLeftPanel");
            this.MainMenu_ShowHideLeftPanel.Click += new System.EventHandler(this.MainMenu_ShowHideLeftPanel_Click);
            this.MainMenu_ShowHideLeftPanel.MouseEnter += new System.EventHandler(this.MainMenu_ShowHideLeftPanel_MouseEnter);
            // 
            // MainMenu_Mode
            // 
            this.MainMenu_Mode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Decors,
            this.MainMenu_Objects,
            this.MainMenu_Characters,
            this.MainMenu_Events,
            this.MainMenu_Walks,
            this.MainMenu_Regions});
            this.MainMenu_Mode.Name = "MainMenu_Mode";
            resources.ApplyResources(this.MainMenu_Mode, "MainMenu_Mode");
            this.MainMenu_Mode.MouseEnter += new System.EventHandler(this.MainMenu_Mode_MouseEnter);
            // 
            // MainMenu_Decors
            // 
            this.MainMenu_Decors.Name = "MainMenu_Decors";
            resources.ApplyResources(this.MainMenu_Decors, "MainMenu_Decors");
            this.MainMenu_Decors.Click += new System.EventHandler(this.MainMenu_Decors_Click);
            this.MainMenu_Decors.MouseEnter += new System.EventHandler(this.MainMenu_Decors_MouseEnter);
            // 
            // MainMenu_Objects
            // 
            this.MainMenu_Objects.Name = "MainMenu_Objects";
            resources.ApplyResources(this.MainMenu_Objects, "MainMenu_Objects");
            this.MainMenu_Objects.Click += new System.EventHandler(this.MainMenu_Objects_Click);
            this.MainMenu_Objects.MouseEnter += new System.EventHandler(this.MainMenu_Objects_MouseEnter);
            // 
            // MainMenu_Characters
            // 
            this.MainMenu_Characters.Name = "MainMenu_Characters";
            resources.ApplyResources(this.MainMenu_Characters, "MainMenu_Characters");
            this.MainMenu_Characters.Click += new System.EventHandler(this.MainMenu_Characters_Click);
            this.MainMenu_Characters.MouseEnter += new System.EventHandler(this.MainMenu_Characters_MouseEnter);
            // 
            // MainMenu_Events
            // 
            this.MainMenu_Events.Name = "MainMenu_Events";
            resources.ApplyResources(this.MainMenu_Events, "MainMenu_Events");
            this.MainMenu_Events.Click += new System.EventHandler(this.MainMenu_Events_Click);
            this.MainMenu_Events.MouseEnter += new System.EventHandler(this.MainMenu_Events_MouseEnter);
            // 
            // MainMenu_Walks
            // 
            this.MainMenu_Walks.Name = "MainMenu_Walks";
            resources.ApplyResources(this.MainMenu_Walks, "MainMenu_Walks");
            this.MainMenu_Walks.Click += new System.EventHandler(this.MainMenu_Walks_Click);
            this.MainMenu_Walks.MouseEnter += new System.EventHandler(this.MainMenu_Walks_MouseEnter);
            // 
            // MainMenu_Regions
            // 
            this.MainMenu_Regions.Name = "MainMenu_Regions";
            resources.ApplyResources(this.MainMenu_Regions, "MainMenu_Regions");
            this.MainMenu_Regions.Click += new System.EventHandler(this.MainMenu_Regions_Click);
            this.MainMenu_Regions.MouseEnter += new System.EventHandler(this.MainMenu_Regions_MouseEnter);
            // 
            // MainMenu_Drawing
            // 
            this.MainMenu_Drawing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Pointer,
            this.MainMenu_Pencil});
            this.MainMenu_Drawing.Name = "MainMenu_Drawing";
            resources.ApplyResources(this.MainMenu_Drawing, "MainMenu_Drawing");
            this.MainMenu_Drawing.MouseEnter += new System.EventHandler(this.MainMenu_Drawing_MouseEnter);
            // 
            // MainMenu_Pointer
            // 
            this.MainMenu_Pointer.Name = "MainMenu_Pointer";
            resources.ApplyResources(this.MainMenu_Pointer, "MainMenu_Pointer");
            this.MainMenu_Pointer.Click += new System.EventHandler(this.MainMenu_Pointer_Click);
            this.MainMenu_Pointer.MouseEnter += new System.EventHandler(this.MainMenu_Pointer_MouseEnter);
            // 
            // MainMenu_Pencil
            // 
            this.MainMenu_Pencil.Name = "MainMenu_Pencil";
            resources.ApplyResources(this.MainMenu_Pencil, "MainMenu_Pencil");
            this.MainMenu_Pencil.Click += new System.EventHandler(this.MainMenu_Pencil_Click);
            this.MainMenu_Pencil.MouseEnter += new System.EventHandler(this.MainMenu_Pencil_MouseEnter);
            // 
            // MainMenu_Zoom
            // 
            this.MainMenu_Zoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Z11,
            this.MainMenu_Z12,
            this.MainMenu_Z14,
            this.MainMenu_Z18});
            this.MainMenu_Zoom.Name = "MainMenu_Zoom";
            resources.ApplyResources(this.MainMenu_Zoom, "MainMenu_Zoom");
            this.MainMenu_Zoom.MouseEnter += new System.EventHandler(this.MainMenu_Zoom_MouseEnter);
            // 
            // MainMenu_Z11
            // 
            this.MainMenu_Z11.Name = "MainMenu_Z11";
            resources.ApplyResources(this.MainMenu_Z11, "MainMenu_Z11");
            this.MainMenu_Z11.Click += new System.EventHandler(this.MainMenu_Z11_Click);
            this.MainMenu_Z11.MouseEnter += new System.EventHandler(this.MainMenu_Z11_MouseEnter);
            // 
            // MainMenu_Z12
            // 
            this.MainMenu_Z12.Name = "MainMenu_Z12";
            resources.ApplyResources(this.MainMenu_Z12, "MainMenu_Z12");
            this.MainMenu_Z12.Click += new System.EventHandler(this.MainMenu_Z12_Click);
            this.MainMenu_Z12.MouseEnter += new System.EventHandler(this.MainMenu_Z12_MouseEnter);
            // 
            // MainMenu_Z14
            // 
            this.MainMenu_Z14.Name = "MainMenu_Z14";
            resources.ApplyResources(this.MainMenu_Z14, "MainMenu_Z14");
            this.MainMenu_Z14.Click += new System.EventHandler(this.MainMenu_Z14_Click);
            this.MainMenu_Z14.MouseEnter += new System.EventHandler(this.MainMenu_Z14_MouseEnter);
            // 
            // MainMenu_Z18
            // 
            this.MainMenu_Z18.Name = "MainMenu_Z18";
            resources.ApplyResources(this.MainMenu_Z18, "MainMenu_Z18");
            this.MainMenu_Z18.Click += new System.EventHandler(this.MainMenu_Z18_Click);
            this.MainMenu_Z18.MouseEnter += new System.EventHandler(this.MainMenu_Z18_MouseEnter);
            // 
            // MainMenu_Resources
            // 
            this.MainMenu_Resources.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_CreateNewMap,
            this.MainMenu_DeleteStage,
            this.MainMenu_Separator10,
            this.MainMenu_Database,
            this.MainMenu_ResourcesManager,
            this.spriteCreatorToolStripMenuItem});
            this.MainMenu_Resources.Name = "MainMenu_Resources";
            resources.ApplyResources(this.MainMenu_Resources, "MainMenu_Resources");
            this.MainMenu_Resources.MouseEnter += new System.EventHandler(this.MainMenu_Resources_MouseEnter);
            // 
            // MainMenu_CreateNewMap
            // 
            this.MainMenu_CreateNewMap.Name = "MainMenu_CreateNewMap";
            resources.ApplyResources(this.MainMenu_CreateNewMap, "MainMenu_CreateNewMap");
            this.MainMenu_CreateNewMap.Click += new System.EventHandler(this.MainMenu_CreateNewMap_Click);
            this.MainMenu_CreateNewMap.MouseEnter += new System.EventHandler(this.MainMenu_CreateNewMap_MouseEnter);
            // 
            // MainMenu_DeleteStage
            // 
            resources.ApplyResources(this.MainMenu_DeleteStage, "MainMenu_DeleteStage");
            this.MainMenu_DeleteStage.Name = "MainMenu_DeleteStage";
            this.MainMenu_DeleteStage.Click += new System.EventHandler(this.MainMenu_DeleteStage_Click);
            // 
            // MainMenu_Separator10
            // 
            this.MainMenu_Separator10.Name = "MainMenu_Separator10";
            resources.ApplyResources(this.MainMenu_Separator10, "MainMenu_Separator10");
            // 
            // MainMenu_Database
            // 
            this.MainMenu_Database.Name = "MainMenu_Database";
            resources.ApplyResources(this.MainMenu_Database, "MainMenu_Database");
            this.MainMenu_Database.Click += new System.EventHandler(this.MainMenu_Database_Click);
            this.MainMenu_Database.MouseEnter += new System.EventHandler(this.MainMenu_Database_MouseEnter);
            // 
            // MainMenu_ResourcesManager
            // 
            this.MainMenu_ResourcesManager.Name = "MainMenu_ResourcesManager";
            resources.ApplyResources(this.MainMenu_ResourcesManager, "MainMenu_ResourcesManager");
            this.MainMenu_ResourcesManager.Click += new System.EventHandler(this.MainMenu_ResourcesManager_Click);
            this.MainMenu_ResourcesManager.MouseEnter += new System.EventHandler(this.MainMenu_ResourcesManager_MouseEnter);
            // 
            // spriteCreatorToolStripMenuItem
            // 
            this.spriteCreatorToolStripMenuItem.Name = "spriteCreatorToolStripMenuItem";
            resources.ApplyResources(this.spriteCreatorToolStripMenuItem, "spriteCreatorToolStripMenuItem");
            this.spriteCreatorToolStripMenuItem.Click += new System.EventHandler(this.MainMenu_SpriteCreator_Click);
            this.spriteCreatorToolStripMenuItem.MouseEnter += new System.EventHandler(this.MainMenu_SpriteCreator_MouseEnter);
            // 
            // MainMenu_Build
            // 
            this.MainMenu_Build.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_ExportToPC});
            this.MainMenu_Build.Name = "MainMenu_Build";
            resources.ApplyResources(this.MainMenu_Build, "MainMenu_Build");
            this.MainMenu_Build.MouseEnter += new System.EventHandler(this.MainMenu_Build_MouseEnter);
            // 
            // MainMenu_ExportToPC
            // 
            this.MainMenu_ExportToPC.Name = "MainMenu_ExportToPC";
            resources.ApplyResources(this.MainMenu_ExportToPC, "MainMenu_ExportToPC");
            this.MainMenu_ExportToPC.Click += new System.EventHandler(this.MainMenu_ExportToPC_Click);
            this.MainMenu_ExportToPC.MouseEnter += new System.EventHandler(this.MainMenu_ExportToPC_MouseEnter);
            // 
            // MainMenu_Game
            // 
            this.MainMenu_Game.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_Try,
            this.MainMenu_TryFullscreen});
            this.MainMenu_Game.Name = "MainMenu_Game";
            resources.ApplyResources(this.MainMenu_Game, "MainMenu_Game");
            this.MainMenu_Game.MouseEnter += new System.EventHandler(this.MainMenu_Game_MouseEnter);
            // 
            // MainMenu_Try
            // 
            this.MainMenu_Try.Name = "MainMenu_Try";
            resources.ApplyResources(this.MainMenu_Try, "MainMenu_Try");
            this.MainMenu_Try.Click += new System.EventHandler(this.MainMenu_Try_Click);
            this.MainMenu_Try.MouseEnter += new System.EventHandler(this.MainMenu_Try_MouseEnter);
            // 
            // MainMenu_TryFullscreen
            // 
            this.MainMenu_TryFullscreen.Name = "MainMenu_TryFullscreen";
            resources.ApplyResources(this.MainMenu_TryFullscreen, "MainMenu_TryFullscreen");
            this.MainMenu_TryFullscreen.Click += new System.EventHandler(this.MainMenu_TryFullscreen_Click);
            this.MainMenu_TryFullscreen.MouseEnter += new System.EventHandler(this.MainMenu_TryFullscreen_MouseEnter);
            // 
            // langageToolStripMenuItem
            // 
            this.langageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.frenchToolStripMenuItem});
            this.langageToolStripMenuItem.Name = "langageToolStripMenuItem";
            resources.ApplyResources(this.langageToolStripMenuItem, "langageToolStripMenuItem");
            this.langageToolStripMenuItem.MouseEnter += new System.EventHandler(this.MainMenu_Language_MouseEnter);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguageEnglish_Click);
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            resources.ApplyResources(this.frenchToolStripMenuItem, "frenchToolStripMenuItem");
            this.frenchToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguageFrench_Click);
            // 
            // MainMenu_About
            // 
            this.MainMenu_About.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_AboutUs,
            this.MainMenu_Documentation,
            this.MainMenu_Website});
            this.MainMenu_About.Name = "MainMenu_About";
            resources.ApplyResources(this.MainMenu_About, "MainMenu_About");
            this.MainMenu_About.MouseEnter += new System.EventHandler(this.MainMenu_About_MouseEnter);
            // 
            // MainMenu_AboutUs
            // 
            this.MainMenu_AboutUs.Name = "MainMenu_AboutUs";
            resources.ApplyResources(this.MainMenu_AboutUs, "MainMenu_AboutUs");
            this.MainMenu_AboutUs.Click += new System.EventHandler(this.MainMenu_AboutUs_Click);
            this.MainMenu_AboutUs.MouseEnter += new System.EventHandler(this.MainMenu_AboutUs_MouseEnter);
            // 
            // MainMenu_Documentation
            // 
            this.MainMenu_Documentation.Name = "MainMenu_Documentation";
            resources.ApplyResources(this.MainMenu_Documentation, "MainMenu_Documentation");
            this.MainMenu_Documentation.MouseEnter += new System.EventHandler(this.MainMenu_Documentation_MouseEnter);
            // 
            // MainMenu_Website
            // 
            this.MainMenu_Website.Name = "MainMenu_Website";
            resources.ApplyResources(this.MainMenu_Website, "MainMenu_Website");
            this.MainMenu_Website.MouseEnter += new System.EventHandler(this.MainMenu_Website_MouseEnter);
            // 
            // Toolbar
            // 
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Toolbar_NewProject,
            this.Toolbar_LoadProject,
            this.Toolbar_Save,
            this.Toolbar_Separator1,
            this.Toolbar_Cut,
            this.Toolbar_Copy,
            this.Toolbar_Paste,
            this.Toolbar_Delete,
            this.Toolbar_Separator2,
            this.Toolbar_Decor,
            this.Toolbar_Objects,
            this.Toolbar_Characters,
            this.Toolbar_Events,
            this.Toolbar_Walk,
            this.Toolbar_Regions,
            this.Toolbar_Separator4,
            this.Toolbar_Pointer,
            this.Toolbar_Pencil,
            this.Toolbar_Separator5,
            this.Toolbar_Z11,
            this.Toolbar_Z12,
            this.Toolbar_Z14,
            this.Toolbar_Z18,
            this.Toolbar_Separator6,
            this.Toolbar_CreateNewMap,
            this.Toolbar_Database,
            this.Toolbar_ResourcesManager,
            this.Toolbar_Separator7,
            this.Toolbar_Try});
            resources.ApplyResources(this.Toolbar, "Toolbar");
            this.Toolbar.Name = "Toolbar";
            // 
            // Toolbar_NewProject
            // 
            this.Toolbar_NewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_NewProject.Image = global::ReplicaStudio.Editor.Properties.Resources.new_project;
            resources.ApplyResources(this.Toolbar_NewProject, "Toolbar_NewProject");
            this.Toolbar_NewProject.Name = "Toolbar_NewProject";
            this.Toolbar_NewProject.Click += new System.EventHandler(this.MainMenu_New_Click);
            this.Toolbar_NewProject.MouseEnter += new System.EventHandler(this.MainMenu_New_MouseEnter);
            // 
            // Toolbar_LoadProject
            // 
            this.Toolbar_LoadProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_LoadProject.Image = global::ReplicaStudio.Editor.Properties.Resources.open_project;
            resources.ApplyResources(this.Toolbar_LoadProject, "Toolbar_LoadProject");
            this.Toolbar_LoadProject.Name = "Toolbar_LoadProject";
            this.Toolbar_LoadProject.Click += new System.EventHandler(this.MainMenu_Load_Click);
            this.Toolbar_LoadProject.MouseEnter += new System.EventHandler(this.MainMenu_Load_MouseEnter);
            // 
            // Toolbar_Save
            // 
            this.Toolbar_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Save.Image = global::ReplicaStudio.Editor.Properties.Resources.save_project;
            resources.ApplyResources(this.Toolbar_Save, "Toolbar_Save");
            this.Toolbar_Save.Name = "Toolbar_Save";
            this.Toolbar_Save.Click += new System.EventHandler(this.MainMenu_Save_Click);
            this.Toolbar_Save.MouseEnter += new System.EventHandler(this.MainMenu_Save_MouseEnter);
            // 
            // Toolbar_Separator1
            // 
            this.Toolbar_Separator1.Name = "Toolbar_Separator1";
            resources.ApplyResources(this.Toolbar_Separator1, "Toolbar_Separator1");
            // 
            // Toolbar_Cut
            // 
            this.Toolbar_Cut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Cut.Image = global::ReplicaStudio.Editor.Properties.Resources.cut;
            resources.ApplyResources(this.Toolbar_Cut, "Toolbar_Cut");
            this.Toolbar_Cut.Name = "Toolbar_Cut";
            this.Toolbar_Cut.MouseEnter += new System.EventHandler(this.MainMenu_Cut_MouseEnter);
            // 
            // Toolbar_Copy
            // 
            this.Toolbar_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Copy.Image = global::ReplicaStudio.Editor.Properties.Resources.copy;
            resources.ApplyResources(this.Toolbar_Copy, "Toolbar_Copy");
            this.Toolbar_Copy.Name = "Toolbar_Copy";
            this.Toolbar_Copy.MouseEnter += new System.EventHandler(this.MainMenu_Copy_MouseEnter);
            // 
            // Toolbar_Paste
            // 
            this.Toolbar_Paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Paste.Image = global::ReplicaStudio.Editor.Properties.Resources.paste;
            resources.ApplyResources(this.Toolbar_Paste, "Toolbar_Paste");
            this.Toolbar_Paste.Name = "Toolbar_Paste";
            this.Toolbar_Paste.MouseEnter += new System.EventHandler(this.MainMenu_Paste_MouseEnter);
            // 
            // Toolbar_Delete
            // 
            this.Toolbar_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Delete.Image = global::ReplicaStudio.Editor.Properties.Resources.delete;
            resources.ApplyResources(this.Toolbar_Delete, "Toolbar_Delete");
            this.Toolbar_Delete.Name = "Toolbar_Delete";
            this.Toolbar_Delete.MouseEnter += new System.EventHandler(this.MainMenu_Delete_MouseEnter);
            // 
            // Toolbar_Separator2
            // 
            this.Toolbar_Separator2.Name = "Toolbar_Separator2";
            resources.ApplyResources(this.Toolbar_Separator2, "Toolbar_Separator2");
            // 
            // Toolbar_Decor
            // 
            this.Toolbar_Decor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Decor.Image = global::ReplicaStudio.Editor.Properties.Resources.decor;
            resources.ApplyResources(this.Toolbar_Decor, "Toolbar_Decor");
            this.Toolbar_Decor.Name = "Toolbar_Decor";
            this.Toolbar_Decor.Click += new System.EventHandler(this.MainMenu_Decors_Click);
            this.Toolbar_Decor.MouseEnter += new System.EventHandler(this.MainMenu_Decors_MouseEnter);
            // 
            // Toolbar_Objects
            // 
            this.Toolbar_Objects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Objects.Image = global::ReplicaStudio.Editor.Properties.Resources._object;
            resources.ApplyResources(this.Toolbar_Objects, "Toolbar_Objects");
            this.Toolbar_Objects.Name = "Toolbar_Objects";
            this.Toolbar_Objects.Click += new System.EventHandler(this.MainMenu_Objects_Click);
            this.Toolbar_Objects.MouseEnter += new System.EventHandler(this.MainMenu_Objects_MouseEnter);
            // 
            // Toolbar_Characters
            // 
            this.Toolbar_Characters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Characters.Image = global::ReplicaStudio.Editor.Properties.Resources.character;
            resources.ApplyResources(this.Toolbar_Characters, "Toolbar_Characters");
            this.Toolbar_Characters.Name = "Toolbar_Characters";
            this.Toolbar_Characters.Click += new System.EventHandler(this.MainMenu_Characters_Click);
            this.Toolbar_Characters.MouseEnter += new System.EventHandler(this.MainMenu_Characters_MouseEnter);
            // 
            // Toolbar_Events
            // 
            this.Toolbar_Events.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Events.Image = global::ReplicaStudio.Editor.Properties.Resources._event;
            resources.ApplyResources(this.Toolbar_Events, "Toolbar_Events");
            this.Toolbar_Events.Name = "Toolbar_Events";
            this.Toolbar_Events.Click += new System.EventHandler(this.MainMenu_Events_Click);
            this.Toolbar_Events.MouseEnter += new System.EventHandler(this.MainMenu_Events_MouseEnter);
            // 
            // Toolbar_Walk
            // 
            this.Toolbar_Walk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Walk.Image = global::ReplicaStudio.Editor.Properties.Resources.walk;
            resources.ApplyResources(this.Toolbar_Walk, "Toolbar_Walk");
            this.Toolbar_Walk.Name = "Toolbar_Walk";
            this.Toolbar_Walk.Click += new System.EventHandler(this.MainMenu_Walks_Click);
            this.Toolbar_Walk.MouseEnter += new System.EventHandler(this.MainMenu_Walks_MouseEnter);
            // 
            // Toolbar_Regions
            // 
            this.Toolbar_Regions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Regions.Image = global::ReplicaStudio.Editor.Properties.Resources.region;
            resources.ApplyResources(this.Toolbar_Regions, "Toolbar_Regions");
            this.Toolbar_Regions.Name = "Toolbar_Regions";
            this.Toolbar_Regions.Click += new System.EventHandler(this.MainMenu_Regions_Click);
            this.Toolbar_Regions.MouseEnter += new System.EventHandler(this.MainMenu_Regions_MouseEnter);
            // 
            // Toolbar_Separator4
            // 
            this.Toolbar_Separator4.Name = "Toolbar_Separator4";
            resources.ApplyResources(this.Toolbar_Separator4, "Toolbar_Separator4");
            // 
            // Toolbar_Pointer
            // 
            this.Toolbar_Pointer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Pointer.Image = global::ReplicaStudio.Editor.Properties.Resources.pointer;
            resources.ApplyResources(this.Toolbar_Pointer, "Toolbar_Pointer");
            this.Toolbar_Pointer.Name = "Toolbar_Pointer";
            this.Toolbar_Pointer.Click += new System.EventHandler(this.MainMenu_Pointer_Click);
            // 
            // Toolbar_Pencil
            // 
            this.Toolbar_Pencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Pencil.Image = global::ReplicaStudio.Editor.Properties.Resources.pencil;
            resources.ApplyResources(this.Toolbar_Pencil, "Toolbar_Pencil");
            this.Toolbar_Pencil.Name = "Toolbar_Pencil";
            this.Toolbar_Pencil.Click += new System.EventHandler(this.MainMenu_Pencil_Click);
            this.Toolbar_Pencil.MouseEnter += new System.EventHandler(this.MainMenu_Pencil_MouseEnter);
            // 
            // Toolbar_Separator5
            // 
            this.Toolbar_Separator5.Name = "Toolbar_Separator5";
            resources.ApplyResources(this.Toolbar_Separator5, "Toolbar_Separator5");
            // 
            // Toolbar_Z11
            // 
            this.Toolbar_Z11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Z11.Image = global::ReplicaStudio.Editor.Properties.Resources.zoom11;
            resources.ApplyResources(this.Toolbar_Z11, "Toolbar_Z11");
            this.Toolbar_Z11.Name = "Toolbar_Z11";
            this.Toolbar_Z11.Click += new System.EventHandler(this.MainMenu_Z11_Click);
            this.Toolbar_Z11.MouseEnter += new System.EventHandler(this.MainMenu_Z11_MouseEnter);
            // 
            // Toolbar_Z12
            // 
            this.Toolbar_Z12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Z12.Image = global::ReplicaStudio.Editor.Properties.Resources.zoom12;
            resources.ApplyResources(this.Toolbar_Z12, "Toolbar_Z12");
            this.Toolbar_Z12.Name = "Toolbar_Z12";
            this.Toolbar_Z12.Click += new System.EventHandler(this.MainMenu_Z12_Click);
            this.Toolbar_Z12.MouseEnter += new System.EventHandler(this.MainMenu_Z12_MouseEnter);
            // 
            // Toolbar_Z14
            // 
            this.Toolbar_Z14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Z14.Image = global::ReplicaStudio.Editor.Properties.Resources.zoom14;
            resources.ApplyResources(this.Toolbar_Z14, "Toolbar_Z14");
            this.Toolbar_Z14.Name = "Toolbar_Z14";
            this.Toolbar_Z14.Click += new System.EventHandler(this.MainMenu_Z14_Click);
            this.Toolbar_Z14.MouseEnter += new System.EventHandler(this.MainMenu_Z14_MouseEnter);
            // 
            // Toolbar_Z18
            // 
            this.Toolbar_Z18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Z18.Image = global::ReplicaStudio.Editor.Properties.Resources.zoom18;
            resources.ApplyResources(this.Toolbar_Z18, "Toolbar_Z18");
            this.Toolbar_Z18.Name = "Toolbar_Z18";
            this.Toolbar_Z18.Click += new System.EventHandler(this.MainMenu_Z18_Click);
            this.Toolbar_Z18.MouseEnter += new System.EventHandler(this.MainMenu_Z18_MouseEnter);
            // 
            // Toolbar_Separator6
            // 
            this.Toolbar_Separator6.Name = "Toolbar_Separator6";
            resources.ApplyResources(this.Toolbar_Separator6, "Toolbar_Separator6");
            // 
            // Toolbar_CreateNewMap
            // 
            this.Toolbar_CreateNewMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_CreateNewMap.Image = global::ReplicaStudio.Editor.Properties.Resources.new_map;
            resources.ApplyResources(this.Toolbar_CreateNewMap, "Toolbar_CreateNewMap");
            this.Toolbar_CreateNewMap.Name = "Toolbar_CreateNewMap";
            this.Toolbar_CreateNewMap.Click += new System.EventHandler(this.MainMenu_CreateNewMap_Click);
            this.Toolbar_CreateNewMap.MouseEnter += new System.EventHandler(this.MainMenu_CreateNewMap_MouseEnter);
            // 
            // Toolbar_Database
            // 
            this.Toolbar_Database.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Database.Image = global::ReplicaStudio.Editor.Properties.Resources.database;
            resources.ApplyResources(this.Toolbar_Database, "Toolbar_Database");
            this.Toolbar_Database.Name = "Toolbar_Database";
            this.Toolbar_Database.Click += new System.EventHandler(this.MainMenu_Database_Click);
            this.Toolbar_Database.MouseEnter += new System.EventHandler(this.MainMenu_Database_MouseEnter);
            // 
            // Toolbar_ResourcesManager
            // 
            this.Toolbar_ResourcesManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_ResourcesManager.Image = global::ReplicaStudio.Editor.Properties.Resources.resource;
            resources.ApplyResources(this.Toolbar_ResourcesManager, "Toolbar_ResourcesManager");
            this.Toolbar_ResourcesManager.Name = "Toolbar_ResourcesManager";
            this.Toolbar_ResourcesManager.Click += new System.EventHandler(this.MainMenu_ResourcesManager_Click);
            this.Toolbar_ResourcesManager.MouseEnter += new System.EventHandler(this.MainMenu_ResourcesManager_MouseEnter);
            // 
            // Toolbar_Separator7
            // 
            this.Toolbar_Separator7.Name = "Toolbar_Separator7";
            resources.ApplyResources(this.Toolbar_Separator7, "Toolbar_Separator7");
            // 
            // Toolbar_Try
            // 
            this.Toolbar_Try.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Toolbar_Try.Image = global::ReplicaStudio.Editor.Properties.Resources._try;
            resources.ApplyResources(this.Toolbar_Try, "Toolbar_Try");
            this.Toolbar_Try.Name = "Toolbar_Try";
            this.Toolbar_Try.Click += new System.EventHandler(this.MainMenu_TryFullscreen_Click);
            this.Toolbar_Try.MouseEnter += new System.EventHandler(this.MainMenu_Try_MouseEnter);
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LeftPanel.Controls.Add(this.InfoPanel);
            this.LeftPanel.Controls.Add(this.VerticalLeftSplitterBottom);
            this.LeftPanel.Controls.Add(this.ProjectPanel);
            resources.ApplyResources(this.LeftPanel, "LeftPanel");
            this.LeftPanel.MaximumSize = new System.Drawing.Size(200, 9999);
            this.LeftPanel.Name = "LeftPanel";
            // 
            // InfoPanel
            // 
            resources.ApplyResources(this.InfoPanel, "InfoPanel");
            this.InfoPanel.Name = "InfoPanel";
            // 
            // VerticalLeftSplitterBottom
            // 
            resources.ApplyResources(this.VerticalLeftSplitterBottom, "VerticalLeftSplitterBottom");
            this.VerticalLeftSplitterBottom.Name = "VerticalLeftSplitterBottom";
            this.VerticalLeftSplitterBottom.TabStop = false;
            // 
            // ProjectPanel
            // 
            resources.ApplyResources(this.ProjectPanel, "ProjectPanel");
            this.ProjectPanel.Name = "ProjectPanel";
            this.ProjectPanel.StageLoading += new System.EventHandler(this.ProjectPanel_StageLoading);
            this.ProjectPanel.MouseEnterCustom += new System.EventHandler(this.ProjectPanel_MouseEnter);
            this.ProjectPanel.StageDelete += new System.EventHandler(this.MainMenu_DeleteStage_Click);
            // 
            // RightPanel
            // 
            this.RightPanel.BackColor = System.Drawing.SystemColors.Control;
            this.RightPanel.Controls.Add(this.VerticalRightSplitter);
            this.RightPanel.Controls.Add(this.StageObjectsPanel);
            this.RightPanel.Controls.Add(this.LayersPanel);
            resources.ApplyResources(this.RightPanel, "RightPanel");
            this.RightPanel.MaximumSize = new System.Drawing.Size(200, 9999);
            this.RightPanel.Name = "RightPanel";
            // 
            // VerticalRightSplitter
            // 
            resources.ApplyResources(this.VerticalRightSplitter, "VerticalRightSplitter");
            this.VerticalRightSplitter.Name = "VerticalRightSplitter";
            this.VerticalRightSplitter.TabStop = false;
            // 
            // StageObjectsPanel
            // 
            resources.ApplyResources(this.StageObjectsPanel, "StageObjectsPanel");
            this.StageObjectsPanel.Name = "StageObjectsPanel";
            this.StageObjectsPanel.MouseEnterCustom += new System.EventHandler(this.StageObjectsPanel_MouseEnter);
            this.StageObjectsPanel.StageNeedsToBeRefreshed += new System.EventHandler(this.StageObjectsPanel_StageNeedsToBeRefreshed);
            this.StageObjectsPanel.StageNeedsToRefreshSelection += new System.EventHandler(this.StageObjectsPanel_StageNeedsToRefreshSelection);
            this.StageObjectsPanel.ItemChosen += new System.EventHandler(this.Component_ItemChosen);
            // 
            // LayersPanel
            // 
            resources.ApplyResources(this.LayersPanel, "LayersPanel");
            this.LayersPanel.Name = "LayersPanel";
            this.LayersPanel.CurrentLayerHasChanged += new System.EventHandler(this.LayersPanel_CurrentLayerHasChanged);
            this.LayersPanel.StageNeedsToBeRefreshed += new System.EventHandler(this.LayersPanel_StageNeedsToBeRefreshed);
            this.LayersPanel.LayerDeleted += new System.EventHandler(this.LayersPanel_LayerDeleted);
            this.LayersPanel.ColorTransformationChanged += new System.EventHandler(this.LayersPanel_ColorTransformationChanged);
            this.LayersPanel.MouseEnterCustom += new System.EventHandler(this.LayersPanel_MouseEnter);
            // 
            // MiddlePanel
            // 
            this.MiddlePanel.AllowDrop = true;
            this.MiddlePanel.BackColor = System.Drawing.SystemColors.Control;
            this.MiddlePanel.Controls.Add(this.PanelMiddleSplitted);
            this.MiddlePanel.Controls.Add(this.PanelRightSplitter);
            this.MiddlePanel.Controls.Add(this.PanelLeftSplitter);
            resources.ApplyResources(this.MiddlePanel, "MiddlePanel");
            this.MiddlePanel.Name = "MiddlePanel";
            this.MiddlePanel.MouseEnter += new System.EventHandler(this.Main_MouseEnter);
            this.MiddlePanel.MouseHover += new System.EventHandler(this.Main_MouseEnter);
            // 
            // PanelMiddleSplitted
            // 
            resources.ApplyResources(this.PanelMiddleSplitted, "PanelMiddleSplitted");
            this.PanelMiddleSplitted.BackColor = System.Drawing.Color.Gray;
            this.PanelMiddleSplitted.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelMiddleSplitted.Name = "PanelMiddleSplitted";
            // 
            // PanelRightSplitter
            // 
            this.PanelRightSplitter.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.PanelRightSplitter, "PanelRightSplitter");
            this.PanelRightSplitter.Name = "PanelRightSplitter";
            // 
            // PanelLeftSplitter
            // 
            this.PanelLeftSplitter.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.PanelLeftSplitter, "PanelLeftSplitter");
            this.PanelLeftSplitter.Name = "PanelLeftSplitter";
            // 
            // LeftSplitter
            // 
            resources.ApplyResources(this.LeftSplitter, "LeftSplitter");
            this.LeftSplitter.Name = "LeftSplitter";
            this.LeftSplitter.TabStop = false;
            // 
            // RightSplitter
            // 
            resources.ApplyResources(this.RightSplitter, "RightSplitter");
            this.RightSplitter.Name = "RightSplitter";
            this.RightSplitter.TabStop = false;
            // 
            // StatusBar
            // 
            this.StatusBar.Controls.Add(this.StatusPanelRight);
            this.StatusBar.Controls.Add(this.StatusPanelLeft);
            resources.ApplyResources(this.StatusBar, "StatusBar");
            this.StatusBar.Name = "StatusBar";
            // 
            // StatusPanelRight
            // 
            this.StatusPanelRight.Controls.Add(this.lblStatusObjectFocus);
            this.StatusPanelRight.Controls.Add(this.lblPoints);
            this.StatusPanelRight.Controls.Add(this.lblStatusMap);
            resources.ApplyResources(this.StatusPanelRight, "StatusPanelRight");
            this.StatusPanelRight.Name = "StatusPanelRight";
            // 
            // lblStatusObjectFocus
            // 
            this.lblStatusObjectFocus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblStatusObjectFocus, "lblStatusObjectFocus");
            this.lblStatusObjectFocus.Name = "lblStatusObjectFocus";
            // 
            // lblPoints
            // 
            this.lblPoints.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblPoints, "lblPoints");
            this.lblPoints.Name = "lblPoints";
            // 
            // lblStatusMap
            // 
            this.lblStatusMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblStatusMap, "lblStatusMap");
            this.lblStatusMap.Name = "lblStatusMap";
            // 
            // StatusPanelLeft
            // 
            resources.ApplyResources(this.StatusPanelLeft, "StatusPanelLeft");
            this.StatusPanelLeft.Controls.Add(this.lblStatusDescription);
            this.StatusPanelLeft.Name = "StatusPanelLeft";
            // 
            // lblStatusDescription
            // 
            this.lblStatusDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblStatusDescription, "lblStatusDescription");
            this.lblStatusDescription.Name = "lblStatusDescription";
            // 
            // OpenFile
            // 
            this.OpenFile.FileName = "project";
            resources.ApplyResources(this.OpenFile, "OpenFile");
            this.OpenFile.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFile_FileOk);
            // 
            // InfoValue
            // 
            resources.ApplyResources(this.InfoValue, "InfoValue");
            this.InfoValue.Name = "InfoValue";
            // 
            // InfoProperties
            // 
            resources.ApplyResources(this.InfoProperties, "InfoProperties");
            this.InfoProperties.Name = "InfoProperties";
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.RightSplitter);
            this.Controls.Add(this.LeftSplitter);
            this.Controls.Add(this.MiddlePanel);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.Toolbar);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.MouseEnter += new System.EventHandler(this.Main_MouseEnter);
            this.MouseHover += new System.EventHandler(this.Main_MouseEnter);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.LeftPanel.ResumeLayout(false);
            this.RightPanel.ResumeLayout(false);
            this.MiddlePanel.ResumeLayout(false);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.StatusPanelRight.ResumeLayout(false);
            this.StatusPanelLeft.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Evite un plantage de VS10, le composant n'est pas affiché dans le Windows Form et Designer et évite tout problème.
        /// </summary>
        private void InitializeStageWindow()
        {
            this.StagePanel = new ReplicaStudio.Editor.Forms.UserControls.StagePanel();
            this.StagePanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.StagePanel.Location = new System.Drawing.Point(0, 0);
            this.StagePanel.Name = "StagePanel";
            this.StagePanel.Size = new System.Drawing.Size(1000, 1000);
            this.StagePanel.TabIndex = 0;
            this.StagePanel.AllowDrop = true;
            this.StagePanel.CurrentModeHasChanged += new System.EventHandler(StagePanel_CurrentModeHasChanged);
            this.StagePanel.MouseChangePosition += new System.EventHandler(StagePanel_MousePosition);
            this.StagePanel.ItemChosen += new System.EventHandler(Component_ItemChosen);
            this.StagePanel.MouseEnterCustom +=new System.EventHandler(StagePanel_MouseEnter);
            this.StagePanel.NeedToRefreshStageObjects += new System.EventHandler(StagePanel_NeedToRefreshStageObjects);
            this.StagePanel.HotSpotEditionCompleted += new System.EventHandler(StagePanel_HotSpotEditionCompleted);
            this.StagePanel.HotSpotEditionBeginning += new System.EventHandler(StagePanel_HotSpotEditionBeginning);
            this.StagePanel.ZoomChanged += new System.EventHandler(StagePanel_ZoomChanged);
            this.PanelMiddleSplitted.Controls.Add(this.StagePanel);
        }
        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_File;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_New;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Load;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_GeneralSettings;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Exit;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Edition;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Cut;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Copy;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Paste;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Delete;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Mode;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Decors;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Walks;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Zoom;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Z11;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Z12;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Z14;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Z18;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Resources;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Database;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_ResourcesManager;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Game;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Try;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_TryFullscreen;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Build;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_ExportToPC;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_About;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_AboutUs;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Documentation;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Website;
        private System.Windows.Forms.ToolStrip Toolbar;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_View;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_ShowHideToolbar;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Events;
        private System.Windows.Forms.ToolStripButton Toolbar_NewProject;
        private System.Windows.Forms.ToolStripButton Toolbar_LoadProject;
        private System.Windows.Forms.ToolStripButton Toolbar_Save;
        private System.Windows.Forms.ToolStripSeparator Toolbar_Separator1;
        private System.Windows.Forms.ToolStripButton Toolbar_Cut;
        private System.Windows.Forms.ToolStripButton Toolbar_Copy;
        private System.Windows.Forms.ToolStripButton Toolbar_Paste;
        private System.Windows.Forms.ToolStripButton Toolbar_Delete;
        private System.Windows.Forms.ToolStripButton Toolbar_Decor;
        private System.Windows.Forms.ToolStripButton Toolbar_Walk;
        private System.Windows.Forms.ToolStripButton Toolbar_Events;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Drawing;
        private System.Windows.Forms.ToolStripSeparator Toolbar_Separator4;
        private System.Windows.Forms.ToolStripSeparator Toolbar_Separator5;
        private System.Windows.Forms.ToolStripButton Toolbar_Z11;
        private System.Windows.Forms.ToolStripButton Toolbar_Z12;
        private System.Windows.Forms.ToolStripButton Toolbar_Z14;
        private System.Windows.Forms.ToolStripButton Toolbar_Z18;
        private System.Windows.Forms.ToolStripSeparator Toolbar_Separator6;
        private System.Windows.Forms.ToolStripButton Toolbar_Database;
        private System.Windows.Forms.ToolStripButton Toolbar_ResourcesManager;
        private System.Windows.Forms.ToolStripSeparator Toolbar_Separator7;
        private System.Windows.Forms.ToolStripButton Toolbar_Try;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Panel MiddlePanel;
        private System.Windows.Forms.Splitter LeftSplitter;
        private System.Windows.Forms.Splitter RightSplitter;
        private UserControls.ProjectPanel ProjectPanel;
        private UserControls.StageObjectsPanel StageObjectsPanel;
        private System.Windows.Forms.Splitter VerticalLeftSplitterBottom;
        private UserControls.LayersPanel LayersPanel;
        private System.Windows.Forms.Panel PanelLeftSplitter;
        private System.Windows.Forms.Panel PanelRightSplitter;
        private System.Windows.Forms.Panel PanelMiddleSplitted;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_ShowHideRightPanel;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_ShowHideLeftPanel;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Pointer;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Pencil;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Regions;
        private System.Windows.Forms.ToolStripButton Toolbar_Regions;
        private System.Windows.Forms.ToolStripButton Toolbar_Pointer;
        private System.Windows.Forms.ToolStripButton Toolbar_Pencil;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Objects;
        private System.Windows.Forms.Panel StatusBar;
        private System.Windows.Forms.Panel StatusPanelRight;
        private System.Windows.Forms.Panel StatusPanelLeft;
        private System.Windows.Forms.Label lblStatusDescription;
        private System.Windows.Forms.Label lblStatusMap;
        private System.Windows.Forms.Label lblStatusObjectFocus;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_CreateNewMap;
        private System.Windows.Forms.ToolStripSeparator MainMenu_Separator10;
        private System.Windows.Forms.ToolStripButton Toolbar_CreateNewMap;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.ToolStripButton Toolbar_Objects;
        private System.Windows.Forms.ToolStripSeparator Toolbar_Separator2;
        private System.Windows.Forms.Splitter VerticalRightSplitter;
        private UserControls.InfoPanel InfoPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfoProperties;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfoValue;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_DeleteStage;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Characters;
        private System.Windows.Forms.ToolStripButton Toolbar_Characters;
        private System.Windows.Forms.FolderBrowserDialog OpenDirectory;
        private System.Windows.Forms.ToolStripMenuItem langageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spriteCreatorToolStripMenuItem;
        
    }
}