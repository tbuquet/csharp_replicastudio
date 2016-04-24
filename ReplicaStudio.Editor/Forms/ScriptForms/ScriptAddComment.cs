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
    public partial class ScriptAddComment : Form
    {
        public bool IsAdd { get; set; }
        public String Comment { get; set; }

        public ScriptAddComment()
        {
            InitializeComponent();
            IsAdd = true;
        }

        private void AddComment_Ok(object sender, EventArgs e)
        {
            Comment = txtComment.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddComment_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (IsAdd == false)
                txtComment.Text = Comment;
            else
                txtComment.Text = String.Empty;
        }
    }
}
