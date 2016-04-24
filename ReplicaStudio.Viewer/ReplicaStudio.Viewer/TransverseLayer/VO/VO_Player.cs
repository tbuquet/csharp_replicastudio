using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Viewer.TransverseLayer.Interfaces;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    public class VO_Player : IEntity
    {
        #region Members
        /// <summary>
        /// Référence au sprite
        /// </summary>
        private VO_CharacterSprite _characterSprite;

        /// <summary>
        /// Référence à la data playablecharacter
        /// </summary>
        private VO_PlayableCharacter _playableCharacter;
        #endregion

        #region Properties
        /// <summary>
        /// Identifiant
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Référence au sprite
        /// </summary>
        public VO_CharacterSprite CharacterSprite { get { return _characterSprite; } }

        /// <summary>
        /// Référence à la data playablecharacter
        /// </summary>
        public VO_PlayableCharacter PlayableCharacter { get { return _playableCharacter; } }

        /// <summary>
        /// Actions
        /// </summary>
        public List<Guid> Actions { get; set; }

        /// <summary>
        /// Activer la vie
        /// </summary>
        public bool ActivateLife { get; set; }

        /// <summary>
        /// PV au démarrage
        /// </summary>
        public int PvAtStart { get; set; }

        /// <summary>
        /// PV max
        /// </summary>
        public int PvMax { get; set; }

        /// <summary>
        /// Items possédés
        /// </summary>
        public Guid[,] Items
        {
            get;
            set;
        }

        /// <summary>
        /// Stage courant
        /// </summary>
        public Guid CurrentStage { get; set; }
        #endregion

        #region Constructor
        public VO_Player(VO_PlayableCharacter playableCharacter)
        {
            //Perso associé
            VO_Character character = GameCore.Instance.GetCharacterById(playableCharacter.CharacterId);

            //Bindings
            _playableCharacter = playableCharacter;
            _characterSprite = new VO_CharacterSprite(character, playableCharacter.StartPosition, playableCharacter.CoordsCharacter);
            this.Id = _playableCharacter.Id;
            this.Actions = new List<Guid>();
            foreach (Guid action in playableCharacter.Actions)
            {
                AddAction(action);
            }
            this.ActivateLife = playableCharacter.ActivateLife;
            this.PvAtStart = playableCharacter.PvAtStart;
            this.PvMax = playableCharacter.PvMax;

            //Inventaire
            CreateInventory(GameCore.Instance.Game.Menu.GridWidth, GameCore.Instance.Game.Menu.GridHeight);
            foreach (Guid item in playableCharacter.Items)
            {
                AddItem(item);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Switch à la prochaine action
        /// </summary>
        public void ChangeNextAction()
        {
            if (ActionManager.ItemAsAction)
                ActionManager.SetCurrentActionToGo();
            else
            {
                Guid currentActionId = ActionManager.CurrentAction.Id;
                bool actionFound = false;
                foreach (Guid action in Actions)
                {
                    if (GameCore.Instance.GetActionById(action).UseAction)
                        continue;
                    if (actionFound)
                    {
                        ActionManager.SetCurrentAction(action);
                        return;
                    }
                    else if (action == currentActionId)
                        actionFound = true;
                }
                if (ActionManager.ItemInUse != Guid.Empty)
                    ActionManager.SetCurrentItem(ActionManager.ItemInUse);
                else
                    ActionManager.SetCurrentAction(Actions[0]);
            }
        }

        /// <summary>
        /// Ajouter un item
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Guid item)
        {
            //On vérifie déjà si un item n'existe pas déjà...
            for (int i = 0; i < GameCore.Instance.Game.Menu.GridHeight; i++)
            {
                for (int j = 0; j < GameCore.Instance.Game.Menu.GridWidth; j++)
                {
                    if (Items[j, i] == item)
                    {
                        return;
                    }
                }
            }

            for (int i = 0; i < GameCore.Instance.Game.Menu.GridHeight; i++)
            {
                for (int j = 0; j < GameCore.Instance.Game.Menu.GridWidth; j++)
                {
                    if (Items[j, i] == Guid.Empty)
                    {
                        Items[j, i] = item;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Ajouter une action
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(Guid action)
        {
            Guid actionGuid = Actions.Find(p => p == action);
            if (actionGuid == Guid.Empty)
                Actions.Add(action);
        }

        /// <summary>
        /// Remove action
        /// </summary>
        /// <param name="action"></param>
        public void RemoveAction(Guid action)
        {
            Guid actionGuid = Actions.Find(p => p == action);
            if (actionGuid != Guid.Empty)
                Actions.Remove(action);
        }

        /// <summary>
        /// Retirer un item
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Guid item)
        {
            for (int i = 0; i < GameCore.Instance.Game.Menu.GridHeight; i++)
            {
                for (int j = 0; j < GameCore.Instance.Game.Menu.GridWidth; j++)
                {
                    if (Items[j, i] == item)
                    {
                        Items[j, i] = Guid.Empty;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Crée l'inventaire
        /// </summary>
        /// <param name="gridWidth">Largeur de la grille</param>
        /// <param name="gridHeight">Hauteur de la grille</param>
        public void CreateInventory(int gridWidth, int gridHeight)
        {
            this.Items = new Guid[gridWidth, gridHeight];
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Détruire l'objet
        /// </summary>
        public void Dispose()
        {
            CharacterSprite.Dispose();
        }
        #endregion
    }
}
