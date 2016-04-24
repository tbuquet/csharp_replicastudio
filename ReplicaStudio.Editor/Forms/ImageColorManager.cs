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
using ReplicaStudio.Editor.TransverseLayer;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire de choix des couleurs
    /// </summary>
    public partial class ImageColorManager : Form
    {
        #region Events
        /// <summary>
        /// Survient quand une couleur est changée
        /// </summary>
        public event EventHandler ColorTransformationChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Sauvegarde des couleurs originales.
        /// </summary>
        public VO_ColorTransformation OriginalColorTransformations { get; set; }

        /// <summary>
        /// Working Layer
        /// </summary>
        public VO_Layer CurrentLayer { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ImageColorManager()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge le panneau avec les données du calque
        /// </summary>
        public void LoadPanel()
        {
            List<VO_Layer> layers = EditorHelper.Instance.GetCurrentStageInstance().ListLayers;
            foreach (VO_Layer layer in layers)
            {
                if (layer.Id == EditorHelper.Instance.CurrentLayer)
                {
                    CurrentLayer = layer;
                    OriginalColorTransformations = new VO_ColorTransformation();
                    OriginalColorTransformations.Red = layer.ColorTransformations.Red;
                    OriginalColorTransformations.Blue = layer.ColorTransformations.Blue;
                    OriginalColorTransformations.Green = layer.ColorTransformations.Green;
                    OriginalColorTransformations.Grey = layer.ColorTransformations.Grey;
                    OriginalColorTransformations.Opacity = layer.ColorTransformations.Opacity;
                    tbRed.Value = Convert.ToInt32(layer.ColorTransformations.Red);
                    tbGreen.Value = Convert.ToInt32(layer.ColorTransformations.Green);
                    tbBlue.Value = Convert.ToInt32(layer.ColorTransformations.Blue);
                    tbGrey.Value = Convert.ToInt32(layer.ColorTransformations.Grey);
                    tbOpacity.Value = Convert.ToInt32(layer.ColorTransformations.Opacity);
                }
            }
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Click sur Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CurrentLayer.ColorTransformations = OriginalColorTransformations;
            this.ColorTransformationChanged(this, new EventArgs());
            this.Close();
        }

        /// <summary>
        /// Click sur OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Changement de la valeur de teinte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRed_Scroll(object sender, EventArgs e)
        {
            CurrentLayer.ColorTransformations.Red = tbRed.Value;
            this.ColorTransformationChanged(this, new EventArgs());
        }

        /// <summary>
        /// Changement de la valeur de saturation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbGreen_Scroll(object sender, EventArgs e)
        {
            CurrentLayer.ColorTransformations.Green = tbGreen.Value;
            this.ColorTransformationChanged(this, new EventArgs());
        }

        /// <summary>
        /// Changement de la valeur de luminance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbBlue_Scroll(object sender, EventArgs e)
        {
            CurrentLayer.ColorTransformations.Blue = tbBlue.Value;
            this.ColorTransformationChanged(this, new EventArgs());
        }

        /// <summary>
        /// Changement de la valeur de transparence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbOpacity_Scroll(object sender, EventArgs e)
        {
            CurrentLayer.ColorTransformations.Opacity = tbOpacity.Value;
            this.ColorTransformationChanged(this, new EventArgs());
        }

        /// <summary>
        /// Changement de la valeur de gris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbGrey_Scroll(object sender, EventArgs e)
        {
            CurrentLayer.ColorTransformations.Grey = tbGrey.Value;
            this.ColorTransformationChanged(this, new EventArgs());
        }

        /// <summary>
        /// Reset les valeurs par défaut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            tbBlue.Value = 0;
            tbRed.Value = 0;
            tbGreen.Value = 0;
            tbGrey.Value = 0;
            tbOpacity.Value = 255;
            CurrentLayer.ColorTransformations.Opacity = tbOpacity.Value;
            CurrentLayer.ColorTransformations.Blue = tbBlue.Value;
            CurrentLayer.ColorTransformations.Green = tbGreen.Value;
            CurrentLayer.ColorTransformations.Red = tbRed.Value;
            CurrentLayer.ColorTransformations.Grey = tbGrey.Value;
            this.ColorTransformationChanged(this, new EventArgs());
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
