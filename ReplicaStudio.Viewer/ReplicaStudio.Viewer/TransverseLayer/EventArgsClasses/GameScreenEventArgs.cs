using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using Microsoft.Xna.Framework;

namespace ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses
{
    /// <summary>
    /// Classe EventArgs lors du changement d'écran
    /// </summary>
    public class GameScreenEventArgs : EventArgs
    {
        #region Properties
        public ViewerEnums.ScreenType ScreenCalled { get; set; }

        public Guid ScreenId { get; set; }

        public Point Position { get; set; }

        public bool IgnoreStartingScript { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="selectedIndex"></param>
        public GameScreenEventArgs(ViewerEnums.ScreenType screenCalled)
        {
            ScreenCalled = screenCalled;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="selectedIndex"></param>
        public GameScreenEventArgs(ViewerEnums.ScreenType screenCalled, Guid screenId)
        {
            ScreenCalled = screenCalled;
            ScreenId = screenId;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="selectedIndex"></param>
        public GameScreenEventArgs(ViewerEnums.ScreenType screenCalled, Guid screenId, Point position)
        {
            ScreenCalled = screenCalled;
            ScreenId = screenId;
            Position = position;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="selectedIndex"></param>
        public GameScreenEventArgs(ViewerEnums.ScreenType screenCalled, Guid screenId, Point position, bool ignoreStartingScript)
        {
            ScreenCalled = screenCalled;
            ScreenId = screenId;
            Position = position;
            IgnoreStartingScript = ignoreStartingScript;
        }
        #endregion
    }
}
