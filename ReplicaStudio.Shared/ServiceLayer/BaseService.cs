using System;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.Reflection;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.Shared.ServiceLayer
{
    /// <summary>
    /// Classe base de service
    /// </summary>
    public class BaseService
    {
        #region Delegate
        /// <summary>
        /// Méthode TryCatchDeleguée
        /// </summary>
        protected delegate void ServiceTask();

        /// <summary>
        /// Méthode Memory Error
        /// </summary>
        protected delegate void ServiceMemoryError();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public BaseService()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Méthode delegate de try/catch
        /// </summary>
        /// <param name="method">Methode qui sera executée</param>
        /// <param name="errorMessage">Message d'erreur utilisateur</param>
        /// <param name="writelog">Doit écrire dans le log</param>
        /// <param name="values">Valeurs de log</param>
        protected void RunServiceTask(ServiceTask method, ServiceMemoryError memoryMethod, string errorMessage, bool writelog, params string[] values)
        {
            if(writelog)
                EnteringMethodLogMessage(method.Method, values);

            try
            {
                method();
            }
            catch (InsufficientMemoryException ie)
            {
                LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                LogTools.WriteDebug(ie.Message);
                if (memoryMethod != null)
                    memoryMethod();
            }
            catch (Exception ex)
            {
                if (LogTools.IsDebugModeActive())
                {
                    MessageBox.Show(errorMessage + "\r\n" + Errors.ERROR_METHOD + method.Method.Name + ": " + ex.Message, Errors.ERROR_BOX_TITLE);
                    if (writelog)
                        LogTools.WriteDebug(errorMessage + "\r\n" + Errors.ERROR_METHOD + method.Method.Name + ": " + ex.Message);
                }
                else
                    MessageBox.Show(errorMessage, Errors.ERROR_BOX_TITLE);
            }
        }

        /// <summary>
        /// Méthode delegate de try/catch
        /// </summary>
        /// <param name="method">Methode qui sera executée</param>
        /// <param name="errorMessage">Message d'erreur utilisateur</param>
        /// <param name="writelog">Doit écrire dans le log</param>
        /// <param name="values">Valeurs de log</param>
        protected void RunServiceTask(ServiceTask method, string errorMessage, bool writelog, params string[] values)
        {
            RunServiceTask(method, null, errorMessage, writelog, values);
        }

        /// <summary>
        /// Méthode delegate de try/catch
        /// </summary>
        /// <param name="method">Methode qui sera executée</param>
        /// <param name="errorMessage">Message d'erreur utilisateur</param>
        /// <param name="values">Valeurs de log</param>
        protected void RunServiceTask(ServiceTask method, string errorMessage, params string[] values)
        {
            RunServiceTask(method, errorMessage, true, values);
        }

        /// <summary>
        /// Méthode delegate de try/catch
        /// </summary>
        /// <param name="method">Methode qui sera executée</param>
        /// <param name="errorMessage">Message d'erreur utilisateur</param>
        protected void RunServiceTask(ServiceTask method, string errorMessage)
        {
            RunServiceTask(method, errorMessage, new string[0]);
        }

        /// <summary>
        /// Méthode delegate de try/catch
        /// </summary>
        /// <param name="method">Methode qui sera executée</param>
        /// <param name="values">Valeurs de log</param>
        protected void RunServiceTask(ServiceTask method, params string[] values)
        {
            RunServiceTask(method, string.Empty, values);
        }

        /// <summary>
        /// Méthode delegate de try/catch
        /// </summary>
        /// <param name="method">Methode qui sera executée</param>
        protected void RunServiceTask(ServiceTask method)
        {
            RunServiceTask(method, string.Empty, new string[0]);
        }

        /// <summary>
        /// Prépare le message de log
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="values"></param>
        private void EnteringMethodLogMessage(MethodInfo infos, params string[] values)
        {
            if (LogTools.IsDebugModeActive())
            {
                ParameterInfo[] parameters = infos.GetParameters();
                string paramsString = string.Empty;

                int i = 0;
                if (values != null)
                {
                    foreach (ParameterInfo parameter in parameters)
                    {
                        if (values.Length > i)
                        {
                            paramsString += parameter.ParameterType.Name + " " + parameter.Name + " = '" + values[i] + "', ";
                            i++;
                        }
                    }
                }

                if (paramsString.Length > 2)
                {
                    paramsString = paramsString.Substring(0, paramsString.Length - 2);
                }

                LogTools.WriteDebug(string.Format(Logs.SERVICE_DEBUG_ENTERING_METHOD, infos.Name, paramsString));
            }
        }
        #endregion
    }
}
