using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Viewer.TransverseLayer.VO;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    /// <summary>
    /// Etat du jeu pour save et chargement
    /// </summary>
    public class PlayableCharactersManager
    {
        #region Members
        /// <summary>
        /// Liste de persos jouables
        /// </summary>
        private static Dictionary<Guid, VO_Player> _Players;
        #endregion

        #region Properties
        /// <summary>
        /// Perso joué actuellement
        /// </summary>
        public static VO_Player CurrentPlayerCharacter { get; set; }
        #endregion

        #region Methods
        public static List<VO_Player> GetPlayers()
        {
            List<VO_Player> output = new List<VO_Player>();
            if (_Players != null)
            {
                foreach (VO_Player character in _Players.Values)
                {
                    output.Add(character);
                }
            }
            return output;
        }

        /// <summary>
        /// Reset la classe
        /// </summary>
        public static void ResetCharacters()
        {
            if (_Players != null)
            {
                foreach (VO_Player charSprite in _Players.Values)
                {
                    charSprite.Dispose();
                }
                _Players = null;

                CurrentPlayerCharacter = null;
            }
        }

        /// <summary>
        /// Crée un perso
        /// </summary>
        /// <param name="Character"></param>
        public static void CreatePlayer(Guid player)
        {
            if (_Players == null)
                _Players = new Dictionary<Guid, VO_Player>();

            try
            {
                VO_Player characterSprite = new VO_Player(GameCore.Instance.GetPlayableCharacterById(player));
                _Players.Add(player, characterSprite);
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_CHARACTER_NOT_LOADED, player));
                LogTools.WriteDebug(e.Message);
            }
        }

        /// <summary>
        /// Récupérer un personnage
        /// </summary>
        /// <param name="character">Guid du personnage</param>
        /// <param name="x">X de départ</param>
        /// <param name="y">Y de départ</param>
        /// <returns>Objet perso</returns>
        public static VO_Player GetPlayer(Guid character)
        {
            if (_Players == null)
                _Players = new Dictionary<Guid, VO_Player>();

            if (!_Players.ContainsKey(character))
                CreatePlayer(character);

            try
            {
                return _Players[character];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_CHARACTER_KEY_NOT_FOUND, character));
                LogTools.WriteDebug(e.Message);
                return null;
            }
        }
        #endregion
    }
}
