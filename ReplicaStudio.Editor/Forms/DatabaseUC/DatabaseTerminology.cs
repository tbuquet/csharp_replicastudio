using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.IO;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire System de la database
    /// </summary>
    public partial class DatabaseTerminology : UserControl
    {
        #region Members
        #endregion

        #region Properties
        /// <summary>
        /// Terminology
        /// </summary>
        public VO_Terminology Terminology
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseTerminology()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBTerminology()
        {
            //Code de chargement
            Terminology = GameCore.Instance.Game.Terminology.Clone();

            //Désactiver events
            txtNewGame.TextChanged -= new EventHandler(txtNewGame_TextChanged);
            txtLoadSave.TextChanged -= new EventHandler(txtLoadSave_TextChanged);
            txtSaveGame.TextChanged -= new EventHandler(txtSaveGame_TextChanged);
            txtLeaveGame.TextChanged -= new EventHandler(txtLeaveGame_TextChanged);
            txtOptions.TextChanged -= new EventHandler(txtOptions_TextChanged);
            txtSaveState.TextChanged -= new EventHandler(txtSaveState_TextChanged);
            txtReturnToTitle.TextChanged -= new EventHandler(txtReturnToTitle_TextChanged);
            txtPrevious.TextChanged -= new EventHandler(txtPrevious_TextChanged);
            txtNext.TextChanged -= new EventHandler(txtNext_TextChanged);
            
            //Bind des infos dans les contrôles
            txtNewGame.Text = Terminology.NewGame;
            txtLoadSave.Text = Terminology.LoadGame;
            txtSaveGame.Text = Terminology.SaveGame;
            txtLeaveGame.Text = Terminology.LeaveGame;
            txtOptions.Text = Terminology.Options;
            txtReturnToTitle.Text = Terminology.ReturnTitle;
            txtSaveState.Text = Terminology.SaveState;
            txtPrevious.Text = Terminology.ChoicePrevious;
            txtNext.Text = Terminology.ChoiceNext;

            //Réactiver events
            txtNewGame.TextChanged += new EventHandler(txtNewGame_TextChanged);
            txtLoadSave.TextChanged += new EventHandler(txtLoadSave_TextChanged);
            txtSaveGame.TextChanged += new EventHandler(txtSaveGame_TextChanged);
            txtLeaveGame.TextChanged += new EventHandler(txtLeaveGame_TextChanged);
            txtOptions.TextChanged += new EventHandler(txtOptions_TextChanged);
            txtSaveState.TextChanged += new EventHandler(txtSaveState_TextChanged);
            txtReturnToTitle.TextChanged += new EventHandler(txtReturnToTitle_TextChanged);
            txtPrevious.TextChanged += new EventHandler(txtPrevious_TextChanged);
            txtNext.TextChanged += new EventHandler(txtNext_TextChanged);
        }
        #endregion

        #region EventHandlers
        void txtNext_TextChanged(object sender, EventArgs e)
        {
            Terminology.ChoiceNext = txtNext.Text;
            Terminology.Update();
        }

        void txtPrevious_TextChanged(object sender, EventArgs e)
        {
            Terminology.ChoicePrevious = txtPrevious.Text;
            Terminology.Update();
        }

        void txtReturnToTitle_TextChanged(object sender, EventArgs e)
        {
            Terminology.ReturnTitle = txtReturnToTitle.Text;
            Terminology.Update();
        }

        void txtSaveState_TextChanged(object sender, EventArgs e)
        {
            Terminology.SaveState = txtSaveState.Text;
            Terminology.Update();
        }

        void txtOptions_TextChanged(object sender, EventArgs e)
        {
            Terminology.Options = txtOptions.Text;
            Terminology.Update();
        }

        void txtLeaveGame_TextChanged(object sender, EventArgs e)
        {
            Terminology.LeaveGame = txtLeaveGame.Text;
            Terminology.Update();
        }

        void txtSaveGame_TextChanged(object sender, EventArgs e)
        {
            Terminology.SaveGame = txtSaveGame.Text;
            Terminology.Update();
        }

        void txtLoadSave_TextChanged(object sender, EventArgs e)
        {
            Terminology.LoadGame = txtLoadSave.Text;
            Terminology.Update();
        }

        void txtNewGame_TextChanged(object sender, EventArgs e)
        {
            Terminology.NewGame = txtNewGame.Text;
            Terminology.Update();
        }
        #endregion
    }
}
