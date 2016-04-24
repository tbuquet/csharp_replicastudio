using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms
{
    public partial class NewStage : Form
    {
        #region Members
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public NewStage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        #endregion

        #region EventHandlers
        /// <summary>
        /// Chargement du formulaire
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtTitle.Text = GlobalConstants.STAGE_NEW_STAGE;
            ddpHeight.Value = GameCore.Instance.Game.Project.Resolution.Height;
            ddpWidth.Value = GameCore.Instance.Game.Project.Resolution.Width;
            ddpHeight.Minimum = GameCore.Instance.Game.Project.Resolution.Height;
            ddpWidth.Minimum = GameCore.Instance.Game.Project.Resolution.Width;
        }

        /// <summary>
        /// Click Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Click sur OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
                MessageBox.Show(Errors.STAGE_TITLE_EMPTY, Errors.ERROR_BOX_TITLE);
            else
            {
                VO_Stage stage = ObjectsFactory.CreateStage(Convert.ToInt32(ddpWidth.Value), Convert.ToInt32(ddpHeight.Value));
                stage.Title = txtTitle.Text;
                EditorHelper.Instance.CurrentStage = stage.Id;
                DialogResult = DialogResult.OK;
                this.Close();
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
