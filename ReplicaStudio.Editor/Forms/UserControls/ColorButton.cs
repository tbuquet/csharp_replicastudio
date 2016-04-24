using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    public partial class ColorButton : UserControl
    {
        #region Members
        /// <summary>
        /// Couleur format VO
        /// </summary>
        VO_Color _SelectedVOColor;

        /// <summary>
        /// Couleur format GDI
        /// </summary>
        Color _SelectedColor;
        #endregion

        #region Properties
        /// <summary>
        /// Couleur version VO
        /// </summary>
        public VO_Color SelectedVOColor
        {
            get
            {
                return _SelectedVOColor;
            }
            set
            {
                _SelectedVOColor = value;
                if(_SelectedVOColor != null)
                    _SelectedColor = FormsTools.GetGDIColorFromVOColor(_SelectedVOColor);
                RefreshColor();
            }
        }

        /// <summary>
        /// Couleur version GDI
        /// </summary>
        public Color SelectedColor
        {
            get
            {
                return _SelectedColor;
            }
            set
            {
                _SelectedColor = value;
                if(_SelectedColor != null)
                    _SelectedVOColor = FormsTools.GetVOColorFromGDIColor(_SelectedColor);
                RefreshColor();
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public ColorButton()
        {
            InitializeComponent();
        }
        #endregion

        #region Method
        /// <summary>
        /// Rafraichi la couleur
        /// </summary>
        private void RefreshColor()
        {
            if (colorImage.Image != null)
                colorImage.Image.Dispose();
            colorImage.Image = new Bitmap(23, 23);
            Graphics g = Graphics.FromImage(colorImage.Image);
            Brush brush = new SolidBrush(_SelectedColor);
            g.FillRectangle(brush, new Rectangle(0, 0, 24, 24));
            brush.Dispose();
        }
        #endregion

        #region EventHandlers
        private void colorImage_Click(object sender, EventArgs e)
        {
            if(_SelectedColor != null)
                colorDialog.Color = _SelectedColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
                SelectedColor = colorDialog.Color;
        }
        #endregion
    }
}
