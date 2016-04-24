using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer.Editors;
using System.Drawing.Design;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    public partial class InfoPanel : UserControl
    {
        #region Events
        /// <summary>
        /// Survient quand la souris survole le contrôle
        /// </summary>
        public event EventHandler MouseEnterCustom;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public InfoPanel()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reset
        /// </summary>
        /// <param name="obj"></param>
        public void ResetObject()
        {
            InformationPanel.SelectedObject = null;
        }

        /// <summary>
        /// Charger un objet dans la propertygrid
        /// </summary>
        /// <param name="obj"></param>
        public void LoadObject(object obj)
        {
            if (obj != null)
            {
                TypeDescriptor.AddAttributes(typeof(VO_ColorTransformation), new EditorAttribute(typeof(ColorEditor), typeof(UITypeEditor)));
                TypeDescriptor.AddAttributes(typeof(VO_Script), new EditorAttribute(typeof(ScriptEditor), typeof(UITypeEditor)));
                TypeDescriptor.AddAttributes(typeof(VO_Coords), new EditorAttribute(typeof(CoordsEditor), typeof(UITypeEditor)));
                InformationPanel.SelectedObject = obj;
            }
            else
            {
                InformationPanel.SelectedObject = null;
            }
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Survient quand la souris entre dans le contrôle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewPanel_MouseEnter(object sender, EventArgs e)
        {
            this.MouseEnterCustom(null, new EventArgs());
        }
        #endregion
    }
}
