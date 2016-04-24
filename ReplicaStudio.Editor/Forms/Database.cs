using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.ServiceLayer;
using ReplicaStudio.Editor.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms
{
    public partial class Database : Form
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        DatabaseService _Service;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public Database()
        {
            InitializeComponent();
            InitializeSDL();
            _Service = new DatabaseService();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lors du chargement d'un jeu, initialise les différents onglets
        /// </summary>
        public void InitializeDatabase()
        {
            UCCharacters.InitializeDBCharacters();
            UCPlayers.InitializeDBPlayers();
            UCActions.InitializeDBActions();
            UCClasses.InitializeDBClasses();
            UCGlobalEvents.InitializeDBGlobalEvents();
            UCItems.InitializeDBItems();
            UCItemsInteraction.InitializeDBItemsInteraction();
            UCMenu.InitializeDBMenus();
            UCSystem.InitializeDBSystem();
            UCTerminology.InitializeDBTerminology();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            this.UCGlobalEvents.EnableTreeNodeColor();
            this.UCSystem.EnableTreeNodeColor();
            this.UCItems.EnableTreeNodeColor();
            this.UCItemsInteraction.EnableTreeNodeColor();
        }

        private void UnloadDrawManager(object sender, EventArgs e)
        {
            this.UCGlobalEvents.DisableTreeNodeColor();
            this.UCSystem.DisableTreeNodeColor();
            this.UCItems.DisableTreeNodeColor();
            this.UCItemsInteraction.DisableTreeNodeColor();
        }

        #endregion

        #region EventHandlers
        /// <summary>
        /// Click sur OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _Service.SaveDB();
            Cursor.Current = DefaultCursor;
            this.UnloadDrawManager(sender, e);
            this.Close();
        }

        /// <summary>
        /// Click sur Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Culture.Language.Notifications.DATABASE_CANCEL_CHANGES, Notifications.Instance.NOTIFICATION, MessageBoxButtons.YesNo);
            switch (result)
            {
                case DialogResult.No:
                    this.DialogResult = DialogResult.None;
                    break;
                case DialogResult.Yes:
                    Cursor.Current = Cursors.WaitCursor;
                    _Service.RestoreDB();
                    Cursor.Current = DefaultCursor;
                    this.UnloadDrawManager(sender, e);
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// Click sur Apply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _Service.SaveDB();
            Cursor.Current = DefaultCursor;
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
