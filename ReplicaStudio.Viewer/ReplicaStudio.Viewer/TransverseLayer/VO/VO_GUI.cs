using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.TransverseLayer.Managers;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    /// <summary>
    /// Classe graphique de l'interface
    /// </summary>
    public static class VO_GUI
    {
        #region Properties
        public static VO_Sprite BackT { get; set; }
        public static VO_Sprite BackRT { get; set; }
        public static VO_Sprite BackR { get; set; }
        public static VO_Sprite BackRB { get; set; }
        public static VO_Sprite BackB { get; set; }
        public static VO_Sprite BackLB { get; set; }
        public static VO_Sprite BackL { get; set; }
        public static VO_Sprite BackLT { get; set; }
        public static VO_Sprite BackC { get; set; }

        public static VO_Sprite FrontT { get; set; }
        public static VO_Sprite FrontRT { get; set; }
        public static VO_Sprite FrontR { get; set; }
        public static VO_Sprite FrontRB { get; set; }
        public static VO_Sprite FrontB { get; set; }
        public static VO_Sprite FrontLB { get; set; }
        public static VO_Sprite FrontL { get; set; }
        public static VO_Sprite FrontLT { get; set; }
        public static VO_Sprite FrontC { get; set; }

        public static string RefResource { get; set; }

        public static int BlockSize { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Charger le GUI
        /// </summary>
        /// <param name="url">Url de la ressource</param>
        public static void LoadNewGui(string url)
        {
            SpriteManager.LoadGUI(url);
        }
        #endregion
    }
}
