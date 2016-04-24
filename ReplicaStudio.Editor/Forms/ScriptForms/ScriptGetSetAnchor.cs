using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptGetSetAnchor : Form
    {
        #region Properties

        public Boolean IsAdd = true;
        public String Anchor = String.Empty;

        #endregion

        public ScriptGetSetAnchor()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (IsAdd == true)
            {
                Anchor = String.Empty;
                txtAnchor.Text = String.Empty;
            }
            else
            {
                txtAnchor.Text = Anchor;
            }
        }

        private void Anchor_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Anchor_Ok(object sender, EventArgs e)
        {
            Anchor = txtAnchor.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
