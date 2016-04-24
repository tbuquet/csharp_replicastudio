using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Shared.TransverseLayer.Tools
{
    /// <summary>
    /// Class de log
    /// </summary>
    public static class LogTools
    {
        #region Members
        /// <summary>
        /// Instance du logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(LogTools));

        /// <summary>
        /// Propriété qui vérifie si le log a bien été configuré
        /// </summary>
        private static bool IsConfigured = false;
        #endregion

        #region Methods
        /// <summary>
        /// Ecrit une info
        /// </summary>
        /// <param name="message">Message</param>
        public static void WriteInfo(string message)
        {
            EnsureConfiguration();
            logger.Info(message);
        }

        /// <summary>
        /// Ecrit une erreur
        /// </summary>
        /// <param name="message">Message</param>
        public static void WriteError(string message)
        {
            EnsureConfiguration();
            logger.Error(message);
        }

        /// <summary>
        /// Ecrit un message debug
        /// </summary>
        /// <param name="message">Message</param>
        public static void WriteDebug(string message)
        {
            EnsureConfiguration();
            if (logger.IsDebugEnabled)
                logger.Debug(message);
        }

        /// <summary>
        /// Informe si le programme est en mode debug ou non
        /// </summary>
        /// <returns>True si oui, sinon non</returns>
        public static bool IsDebugModeActive()
        {
            EnsureConfiguration();
            if (logger.IsDebugEnabled)
                return true;
            return false;
        }

        /// <summary>
        /// S'assure que les logs sont bien configurés
        /// </summary>
        private static void EnsureConfiguration()
        {
            if (!IsConfigured)
            {
                XmlConfigurator.Configure(new Uri(Application.StartupPath + @"\" + Logs.LOG_FILENAME));
                IsConfigured = true;
            }
        }
        #endregion
    }
}
