using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Editor.TransverseLayer.Constants
{
    sealed class Notifications
    {
        #region Notifications Singleton
        /// <summary>
        /// Unique instance of ReplicaConfig
        /// </summary>
        private static volatile Notifications instance = null;
        /// <summary>
        /// Synchronization's object
        /// </summary>
        private static object syncRoot = new Object();
        /// <summary>
        /// Get the unique instance of ReplicaConfig
        /// </summary>
        /// 

        public static Notifications Instance
        {
            get
            {
                // Quick test
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        // Thread safe test
                        if (instance == null)
                        {
                            // First call, create the instance
                            instance = new Notifications();
                        }
                    }
                }
                return (instance);
            }
        }

        /// <summary>
        /// Private constructor of the singleton
        /// </summary>
        private Notifications()
        {
        }

        #endregion
        /// <summary>
        /// Notification
        /// </summary>
        public string NOTIFICATION { get { return Culture.Language.Notifications.NOTIFICATION; } set { } }

        #region Load/Save
        /// <summary>
        /// Message de prévention lors de chargement d'un nouveau jeu
        /// </summary>
        //public const string LOAD_PROJECT = "Do you really want to load another project? Unsaved changes will be definitely lost!";

        public string LOAD_PROJECT { get { return Culture.Language.Notifications.LOAD_PROJECT; } set { } }

        /// <summary>
        /// Message de prévention lorsqu'un projet est crée à l'emplacement d'un projet existant
        /// </summary>
        public string PROJECT_EXIST { get { return Culture.Language.Notifications.PROJECT_EXIST; } set { } }

        /// <summary>
        /// Message de prévention à la fermeture de l'application
        /// </summary>
        public string PROJECT_LEAVE_APP { get { return Culture.Language.Notifications.PROJECT_LEAVE_APP; } set { } }

        /// <summary>
        /// Message de prévention de la sauvegarde du projet lors du Try
        /// </summary>
        public string PROJECT_TRY { get { return Culture.Language.Notifications.PROJECT_TRY; } set { } }

        /// <summary>
        /// Message de prévention de la suppression du stage courant
        /// </summary>
        public string STAGE_DELETE { get { return Culture.Language.Notifications.STAGE_DELETE; } set { } }
        #endregion
    }
}
