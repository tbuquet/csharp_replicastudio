using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptChangePlayerDirection : Form
    {
        #region Properties
        public Enums.Movement Direction { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ScriptChangePlayerDirection()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ddpMovements.SelectedIndexChanged += new EventHandler(ddpMovements_SelectedIndexChanged);

            List<VO_ListItem> list = FormsTools.GetMovementsList();
            ddpMovements.Items.Clear();
            ddpMovements.DisplayMember = "Title";
            ddpMovements.ValueMember = "Id";
            int i = 0;
            foreach (VO_ListItem item in list)
            {
                ddpMovements.Items.Add(item);
                if (item.Id == (int)Direction)
                    ddpMovements.SelectedItem = item;
                i++;
            }

            ddpMovements.SelectedIndexChanged += new EventHandler(ddpMovements_SelectedIndexChanged);
        }
        #endregion

        #region EventHandlers
        private void ddpMovements_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddpMovements.SelectedItem != null)
                Direction = (Enums.Movement)((VO_ListItem)ddpMovements.SelectedItem).Id;
        }

        private void ValidationButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
