using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Shared.TransverseLayer.Tools
{
    /// <summary>
    /// Class de tools
    /// </summary>
    public static class AppTools
    {
        /// <summary>
        /// Cast une liste d'objets enfants en une liste d'objet base
        /// </summary>
        /// <typeparam name="BASE">Classe de base</typeparam>
        /// <typeparam name="CHILD">Classe enfant</typeparam>
        /// <param name="childClassList">Classe enfant</param>
        /// <param name="baseClassList">Classe de base</param>
        /// <returns>List d'objets de la classe de base</returns>
        public static List<BASE> CastListBaseFromChild<BASE, CHILD>(List<CHILD> childClassList, List<BASE> baseClassList) where CHILD : BASE
        {
            foreach (CHILD childObject in childClassList)
            {
                BASE newBaseObject = (BASE)childObject;
                baseClassList.Add(newBaseObject);
            }

            return baseClassList;
        }

        /// <summary>
        /// Types de scripts
        /// </summary>
        /// <returns></returns>
        public static Type[] GetScriptTypes()
        {
            Type[] ScriptTypes = new Type[] {
                typeof(VO_Script_Message),
                typeof(VO_Script_ChoiceMessage),
                typeof(VO_Script_LookForwardPlayer),
                typeof(VO_Script_Loop),
                typeof(VO_Script_AddItem),
                typeof(VO_Script_RemoveItem),
                typeof(VO_Script_HideLifeBar),
                typeof(VO_Script_ShowLifeBar),
                typeof(VO_Script_RemoveItem),
                typeof(VO_Script_EnableUserControls),
                typeof(VO_Script_EnableStageInteractions),
                typeof(VO_Script_DisableUserControls),
                typeof(VO_Script_DisableStageInteractions),
                typeof(VO_Script_ChangeCurrentAction),
                typeof(VO_Script_CallScript),
                typeof(VO_Script_DefaultCamera),
                typeof(VO_Script_FocusOnAnimation),
                typeof(VO_Script_FocusOnCharacter),
                typeof(VO_Script_MoveCamera),
                typeof(VO_Script_ChangeCurrentCharacter),
                typeof(VO_Script_ChangeHP),
                typeof(VO_Script_ChangeMaxHP),
                typeof(VO_Script_ChangePlayerDirection),
                typeof(VO_Script_ChangePlayerSpeed),
                typeof(VO_Script_ChoiceMessage),
                typeof(VO_Script_ChangePlayerAnimation),
                typeof(VO_Script_FreezePlayerAnimation),
                typeof(VO_Script_FreePlayerAnimation),
                typeof(VO_Script_MovePlayer),
                typeof(VO_Script_Teleport),
                typeof(VO_Script_ChangeMusicFrequency),
                typeof(VO_Script_PlayMusic),
                typeof(VO_Script_PlaySound),
                typeof(VO_Script_StopMusic),
                typeof(VO_Script_StopSound),
                typeof(VO_Script_CloseInventory),
                typeof(VO_Script_DisableSaves),
                typeof(VO_Script_EnableSaves),
                typeof(VO_Script_GameOver),
                typeof(VO_Script_LoadGame),
                typeof(VO_Script_OpenInventory),
                typeof(VO_Script_SaveGame),
                typeof(VO_Script_TitleScreen),
                typeof(VO_Script_Break),
                typeof(VO_Script_CallGlobalEvent),
                typeof(VO_Script_ChangeVariable),
                typeof(VO_Script_Comment),
                typeof(VO_Script_Condition),
                typeof(VO_Script_GoToAnchor),
                typeof(VO_Script_PressSwitch),
                typeof(VO_Script_SetAnchor),
                typeof(VO_Script_Random),
                typeof(VO_Script_Wait),
                typeof(VO_Script_ChangeCharacterAnimFrequency),
                typeof(VO_Script_FreeCharacterAnimation),
                typeof(VO_Script_FreezeCharacterAnimation),
                typeof(VO_Script_MoveCharacter),
                typeof(VO_Script_StopCharacterMovements),
                typeof(VO_Script_StopCurrentPlayerMovement),
                typeof(VO_Script_ChangeCharacterDirection),
                typeof(VO_Script_AddPlayerAction),
                typeof(VO_Script_RemovePlayerAction),
                typeof(VO_Script_CallGlobalEvent),
                typeof(VO_Script_ChangePlayerSpeed),
                typeof(VO_Script_ShowPlayer),
                typeof(VO_Script_HidePlayer),
                typeof(VO_ActionOnItemScript)
            };
            return ScriptTypes;
        }

        /// <summary>
        /// Encrypte des données
        /// </summary>
        /// <param name="plainData"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] plainData, string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] encryptedData = desencrypt.TransformFinalBlock(plainData, 0, plainData.Length);
            return encryptedData;
        }

        /// <summary>
        /// Décrypte des données
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] encryptedData, string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desDecrypt = DES.CreateDecryptor();
            byte[] decryptedData = desDecrypt.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return decryptedData;
        }

        /// <summary>
        /// Sauvegarde un objet
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void SaveObjectToFile(object obj, string path)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binFormatter = new BinaryFormatter();
                binFormatter.Serialize(memStream, obj);
                byte[] encryptedBytes = Encrypt(memStream.ToArray(), GlobalConstants.CRYPT_KEY);
                memStream.Close();
                Stream streamToFile = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                streamToFile.Write(encryptedBytes, 0, encryptedBytes.Length);
                streamToFile.Flush();
                streamToFile.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Charge un objet
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object LoadObjectFromFile(string path)
        {
            try
            {

                Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] encryptedObj = new byte[fileStream.Length];
                fileStream.Read(encryptedObj, 0, (int)encryptedObj.Length);
                MemoryStream memStream = new MemoryStream(Decrypt(encryptedObj, GlobalConstants.CRYPT_KEY));
                BinaryFormatter binFormatter = new BinaryFormatter();
                object decryptedObj = binFormatter.Deserialize(memStream);
                memStream.Close();
                fileStream.Close();
                return decryptedObj;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        static public void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
                FileInfo fi = new FileInfo(dest);
                fi.IsReadOnly = false;
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        static public string GetVersionLitteralName()
        {
            string Beta = string.Empty;
            if (GlobalConstants.BETA_VERSION == true)
                Beta = " Beta";
            return "Version " + GlobalConstants.PROJECT_VERSION.ToString("0.00").Replace(',', '.') + Beta;
        }
    }

}
