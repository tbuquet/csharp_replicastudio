using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptCallGlobalEvent : Form
    {
        public bool IsAdd = true;
        public Guid GlobalEventId { get; set; }

        public ScriptCallGlobalEvent()
        {
            InitializeComponent();
            GlobalEventId = Guid.Empty;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (IsAdd == true)
                GlobalEventId = Guid.Empty;
            cmbGlobalEvent.Items.Clear();
            List<VO_Base> EventList = GameCore.Instance.GetGlobalEvents();
            foreach (VO_Base CurrentEvent in EventList)
            {
                cmbGlobalEvent.Items.Add(CurrentEvent);
                if (CurrentEvent.Id == GlobalEventId)
                    cmbGlobalEvent.SelectedItem = CurrentEvent;
            }
            cmbGlobalEvent.DisplayMember = "Title";
            cmbGlobalEvent.ValueMember = "Id";
            cmbGlobalEvent.Enabled = true;
            if (cmbGlobalEvent.Items.Count <= 0)
                cmbGlobalEvent.Enabled = false;
        }

        private void ScriptCallGlobalEvent_Ok(object sender, EventArgs e)
        {
            if (cmbGlobalEvent.Items.Count <= 0)
                MessageBox.Show(Culture.Language.Notifications.NO_GLOBALEVENT_SELECTION);
            else
            {
                VO_Base CurrentEvent = (VO_Base)cmbGlobalEvent.SelectedItem;
                GlobalEventId = CurrentEvent.Id;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ScriptCallGlobalEvent_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
