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
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.ServiceLayer;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire de création de calque
    /// </summary>
    public partial class NewLayer : Form
    {
        #region Members
        /// <summary>
        /// Référence au calque
        /// </summary>
        LayersPanelService _Service;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public NewLayer()
        {
            InitializeComponent();
            _Service = new LayersPanelService();
        }
        #endregion

        #region Eventhandlers
        /// <summary>
        /// Click sur Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Click sur OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (EditorHelper.Instance.GetCurrentStageInstance() != null)
            {
                if (string.IsNullOrEmpty(txtName.Text))
                    MessageBox.Show(Errors.LAYER_TITLE_EMPTY, Errors.ERROR_BOX_TITLE);
                else if (EditorHelper.Instance.GetCurrentStageInstance().ListLayers.Count >= GlobalConstants.PERF_MAX_LAYERS)
                    MessageBox.Show(string.Format(Errors.LAYER_LIMIT, GlobalConstants.PERF_MAX_LAYERS), Errors.ERROR_BOX_TITLE);
                else
                {
                    _Service.CreateLayer(txtName.Text);
                    this.Close();
                }
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
