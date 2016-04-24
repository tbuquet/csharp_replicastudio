using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    public class VO_Directory : VO_Base
    {
        #region Properties
        /// <summary>
        /// Nom du répertoire 
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Chemin du répertoire
        /// </summary>
        public String Path { get; set; }
        /// <summary>
        /// Extensions autotisés dans le répertoire
        /// </summary>
        public string Extensions { get; set; }
        #endregion
        #region Constructors
        public VO_Directory()
        {

        }
        #endregion
    }
}
