using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Menu
    {
        #region Properties
        /// <summary>
        /// Activer le menu principal
        /// </summary>
        public bool ActivateMainMenu
        {
            get;
            set;
        }

        /// <summary>
        /// Activer le menu Echap
        /// </summary>
        public bool ActivateEchapMenu
        {
            get;
            set;
        }

        /// <summary>
        /// Activer le menu de chargement
        /// </summary>
        public bool ActivateLoadingMenu
        {
            get;
            set;
        }

        /// <summary>
        /// Activer le menu de sauvegarde
        /// </summary>
        public bool ActivateSaveMenu
        {
            get;
            set;
        }

        /// <summary>
        /// Animation du menu principal
        /// </summary>
        public Guid MainMenuAnimation
        {
            get;
            set;
        }

        /// <summary>
        /// Animation de l'inventaire
        /// </summary>
        public Guid InventoryAnimation
        {
            get;
            set;
        }

        /// <summary>
        /// Animation du bouton retour de l'inventaire
        /// </summary>
        public Guid InventoryBackButtonAnimation
        {
            get;
            set;
        }

        /// <summary>
        /// Coordonnées du tableau de l'inventaire
        /// </summary>
        public Point InventoryCoords
        {
            get;
            set;
        }

        /// <summary>
        /// Coordonnées du bouton retour de l'inventaire
        /// </summary>
        public Point InventoryBackButtonCoords
        {
            get;
            set;
        }

        /// <summary>
        /// Coordonnées du background de l'inventaire
        /// </summary>
        public Point InventoryBackgroundCoords
        {
            get;
            set;
        }

        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public int ItemWidth { get; set; }
        public int ItemHeight { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public VO_Menu()
        {
        }
        #endregion

        #region Methods
        public void Update()
        {
            try
            {
                GameCore.Instance.Game.Menu = (VO_Menu)this.MemberwiseClone();
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_UPDATE_VO + "Menu :" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Menu Clone()
        {
            return (VO_Menu)this.MemberwiseClone();
        }
        #endregion
    }
}
