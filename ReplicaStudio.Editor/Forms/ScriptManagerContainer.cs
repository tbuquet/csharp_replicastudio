using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Editor.Forms
{
    public partial class ScriptManagerContainer : Form
    {
        #region Properties
        public VO_Script Script { get; set; }
        #endregion

        #region Constructor
        public ScriptManagerContainer()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventhandlers

        private void OnLoad(object sender, EventArgs e)
        {
            this.scriptManager1.EnableDrawManager();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.scriptManager1.DisableDrawManager();
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Script = scriptManager1.Script;
            this.scriptManager1.DisableDrawManager();
            this.Close();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge le projet
        /// </summary>
        /// <param name="script"></param>
        public void LoadScript(VO_Script script)
        {
            scriptManager1.LoadScript(script);
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
