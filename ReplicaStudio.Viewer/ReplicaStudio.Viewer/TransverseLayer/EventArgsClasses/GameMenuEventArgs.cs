using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses
{
    /// <summary>
    /// Classe EventArgs lors de la sortie de l'application
    /// </summary>
    public class GameMenuEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Index sélectionné
        /// </summary>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Guid sélectionné
        /// </summary>
        public Guid SelectedId { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="selectedIndex"></param>
        public GameMenuEventArgs(int selectedIndex, Guid selectedId, string key)
        {
            SelectedIndex = selectedIndex;
            SelectedId = selectedId;
            Key = key;
        }
        #endregion
    }
}
