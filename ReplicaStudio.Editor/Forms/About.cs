using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Reflection;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire "à propos"
    /// </summary>
    public partial class About : Form
    {
        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public About()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        #endregion

        #region Eventhandler
        private void About_Load(object sender, EventArgs e)
        {
            copyRight.Text = GlobalConstants.COPYRIGHT;
            versionNumber.Text = AppTools.GetVersionLitteralName();
            logo.Image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.logo-PCS.png"));
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
