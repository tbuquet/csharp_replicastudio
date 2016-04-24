using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire Items Interactions de la database
    /// </summary>
    public partial class DatabaseItemsInteraction : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ItemService _Service;

        /// <summary>
        /// Script chargé
        /// </summary>
        VO_Script _LoadedScript;
        #endregion

        #region Properties
        /// <summary>
        /// CurrentItem 1
        /// </summary>
        VO_Item CurrentItem1;

        /// <summary>
        /// CurrentItem 2
        /// </summary>
        VO_Item CurrentItem2;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseItemsInteraction()
        {
            InitializeComponent();
            _Service = new ItemService();
            ListItems1.Title = Culture.Language.DatabaseRessources.DatabaseItemsInteraction_1;
            ListItems2.Title = Culture.Language.DatabaseRessources.DatabaseItemsInteraction_2;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBItemsInteraction()
        {
        }

        /// <summary>
        /// Charge la liste de items
        /// </summary>
        private void ProvisionList()
        {
            ListItems1.ItemSelectedValue = new Guid();
            ListItems2.ItemSelectedValue = new Guid();
            ListItems1.DataSource = _Service.ProvisionList();
            ListItems1.LoadList();
            ListItems2.DataSource = _Service.ProvisionList();
            ListItems2.LoadList();
        }

        /// <summary>
        /// Chargement des items
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        private void LoadItem(Guid item1, Guid item2)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            if (item1 != new Guid())
                CurrentItem1 = GameCore.Instance.GetItemById(item1);
            if (item2 != new Guid())
                CurrentItem2 = GameCore.Instance.GetItemById(item2);

            if (CurrentItem1.Id != new Guid() && CurrentItem2.Id != new Guid() && CurrentItem1.Id != CurrentItem2.Id)
            {
                //Afficher les groupes
                grpCommands.Visible = true;

                //Récupération des données du script ou création si nécessaire.
                VO_Script script = null;
                VO_ItemInteraction itemInteraction = CurrentItem1.ItemInteraction.Find(p => p.AssociatedItem == CurrentItem2.Id);
                if (itemInteraction == null)
                {
                    script = ObjectsFactory.CreateScript(true, Enums.ScriptType.ItemEvents);
                    VO_ItemInteraction itemInteraction1 = new VO_ItemInteraction();
                    itemInteraction1.AssociatedItem = CurrentItem2.Id;
                    itemInteraction1.Script = script.Id;
                    CurrentItem1.ItemInteraction.Add(itemInteraction1);
                    VO_ItemInteraction itemInteraction2 = new VO_ItemInteraction();
                    itemInteraction2.AssociatedItem = CurrentItem1.Id;
                    itemInteraction2.Script = script.Id;
                    CurrentItem2.ItemInteraction.Add(itemInteraction2);
                }
                else
                {
                    script = GameCore.Instance.GetInteractionScriptsById(itemInteraction.Script);

                    if (script == null)
                    {
                        script = ObjectsFactory.CreateScript(true, Enums.ScriptType.ItemEvents);
                        itemInteraction.Script = script.Id;
                        VO_ItemInteraction itemInteraction2 = CurrentItem2.ItemInteraction.Find(p => p.AssociatedItem == CurrentItem1.Id);
                        itemInteraction2.Script = script.Id;
                    }
                }
                //Chargement du script
                _LoadedScript = script;
                ScriptManager.LoadScript(script);
            }
            else
            {
                grpCommands.Visible = false;
            }

            Cursor.Current = DefaultCursor;
        }

        public void EnableTreeNodeColor()
        {
            this.ScriptManager.EnableDrawManager();
        }

        public void DisableTreeNodeColor()
        {
            this.ScriptManager.DisableDrawManager();
        }

        #endregion

        #region EventHandlers
        /// <summary>
        /// Code ajouté lors de la sélection d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems1_ItemChosen(object sender, EventArgs e)
        {
            LoadItem(ListItems1.ItemSelectedValue, CurrentItem2.Id);
        }

        /// <summary>
        /// Code ajouté lors de la sélection d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems2_ItemChosen(object sender, EventArgs e)
        {
            LoadItem(CurrentItem1.Id, ListItems2.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems1_ListIsEmpty(object sender, EventArgs e)
        {
            grpCommands.Visible = false;
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems2_ListIsEmpty(object sender, EventArgs e)
        {
            ListItems1_ItemChosen(this, new EventArgs());
        }

        /// <summary>
        /// La visibilité change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseItemsInteraction_VisibleChanged(object sender, EventArgs e)
        {
            grpCommands.Visible = false;

            CurrentItem1 = new VO_Item();
            CurrentItem2 = new VO_Item();
            ProvisionList();

            LoadItem(ListItems1.ItemSelectedValue, CurrentItem2.Id);
            LoadItem(CurrentItem1.Id, ListItems2.ItemSelectedValue);
        }

        private void ScriptManager_ScriptUpdated(object sender, EventArgs e)
        {
            //_LoadedScript.Update();
        }
        #endregion  
    }
}
