using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Shared.TransverseLayer.Tools
{
    public static class ValidationTools
    {
        public static string NormalizeFolderName(string input)
        {
            return input;
        }

        public static VO_Base CreateEmptyRessource(VO_Base obj)
        {
            obj.Id = Guid.Empty;
            obj.Title = Culture.Language.NotFound.RESSOURCE_NOT_FOUND;

            return obj;
        }

        public static bool CheckObjectExistence(Guid CurrentGuid)
        {
            if (CurrentGuid == Guid.Empty)
                return false;
            return true;
        }

        public static bool CheckObjectExistence(bool CurrentValidation)
        {
            if (CurrentValidation == false)
                return false;
            return true;
        }

        public static bool CheckObjectExistence(VO_Base CurrentValidation)
        {
            if (CurrentValidation == null || CurrentValidation.Id == Guid.Empty)
                return false;
            return true;
        }

        public static bool CheckObjectExistence(string CurrentValidation)
        {
            if (CurrentValidation == null || CurrentValidation == string.Empty)
                return false;
            return true;
        }

        public static bool CheckObjectExistence(VO_Coords CurrentValidation)
        {
            if (CurrentValidation == null || CurrentValidation.Location.X < 0 || CurrentValidation.Location.Y < 0 )
                return false;
            return true;
        }

        public static bool CheckMapExistence(VO_Coords CurrentValidation)
        {
            if (CurrentValidation.Map == Guid.Empty)
                return false;
            VO_Stage CurrentStage = GameCore.Instance.GetStageById(CurrentValidation.Map);
             if (CurrentStage == null || CurrentStage.Id == Guid.Empty)
                return false;
            return true;
        }

        public static void CleanupProject()
        {
            #region Cleanup des scripts interaction
            List<Guid> interactionScripts = new List<Guid>();
            foreach (VO_Script script in GameCore.Instance.Game.InteractionScripts)
            {
                interactionScripts.Add(script.Id);
            }

            foreach (VO_Item item in GameCore.Instance.Game.Items)
            {
                foreach (VO_ItemInteraction itemInteraction in item.ItemInteraction)
                {
                    if (interactionScripts.Contains(itemInteraction.Script))
                        interactionScripts.Remove(itemInteraction.Script);
                }
            }

            foreach (Guid scriptId in interactionScripts)
            {
                GameCore.Instance.RemoveInteractionScriptById(scriptId);
            }
            #endregion
        }
    }
}
