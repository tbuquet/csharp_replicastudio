using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    public partial class CoordsButton : UserControl
    {
        #region Members
        /// <summary>
        /// Valeur guid du bouton
        /// </summary>
        Rectangle _Coords;

        /// <summary>
        /// Coords avec map
        /// </summary>
        VO_Coords _FullCoords;
        #endregion

        #region Events
        /// <summary>
        /// Survient quand la valeur de l'id du boutton change
        /// </summary>
        public event EventHandler ValueChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Bouton associé
        /// </summary>
        public Rectangle Coords
        {
            get
            {
                return _Coords;
            }
            set
            {
                _Coords = value;
                txtButton.Text = _Coords.X + " x " + _Coords.Y;
            }
        }

        /// <summary>
        /// Bouton associé avec map
        /// </summary>
        public VO_Coords FullCoords
        {
            get
            {
                return _FullCoords;
            }
            set
            {
                _FullCoords = value;
                if (UseStages)
                {
                    VO_Stage stage = GameCore.Instance.Game.Stages.Find(p => p.Id == _FullCoords.Map);
                    if(stage != null)
                        txtButton.Text = _FullCoords.Location.X + " x " + _FullCoords.Location.Y + " (" + GameCore.Instance.Game.Stages.Find(p => p.Id == _FullCoords.Map).Title + ")";
                    else
                        txtButton.Text = _FullCoords.Location.X + " x " + _FullCoords.Location.Y + " (" + GlobalConstants.UNKNOWN + ")";
                }
            }
        }

        /// <summary>
        /// Resolution Source
        /// </summary>
        public Size SourceResolution
        {
            get;
            set;
        }

        public bool UseStages
        {
            get;
            set;
        }

        /// <summary>
        /// Utiliser le background de la scène actuelle.
        /// </summary>
        public bool UseStageBackground
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public CoordsButton()
        {
            InitializeComponent();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Ouvre le TriggerManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoose_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.CoordsManager.FormClosed += new FormClosedEventHandler(CoordsManager_FormClosed);
            FormsManager.Instance.CoordsManager.SourceObject = Coords;
            FormsManager.Instance.CoordsManager.SourceResolution = SourceResolution;
            FormsManager.Instance.CoordsManager.SourceFullObject = FullCoords;
            FormsManager.Instance.CoordsManager.UseStageBackground = UseStageBackground;
            FormsManager.Instance.CoordsManager.UseStages = UseStages;
            FormsManager.Instance.CoordsManager.ShowDialog(this);
        }

        /// <summary>
        /// Action lors de la fermeture du TriggerManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CoordsManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.CoordsManager.FormClosed -= new FormClosedEventHandler(CoordsManager_FormClosed);
            Coords = new Rectangle(FormsManager.Instance.CoordsManager.DestinationObject.Location, Coords.Size);
            FullCoords = FormsManager.Instance.CoordsManager.DestinationObject;
            if(this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }
        #endregion
    }
}
