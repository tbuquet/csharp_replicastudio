using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Shared.TransverseLayer.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ReplicaStudio.Shared.TransverseLayer.VO;

    /// <summary>
    /// Cette classe permet de vérifier la validité d'un projet
    /// </summary>
    public static class ProjectIntegrity
    {
        #region Properties

        public static List<string> ErrorList;
        public static bool ProjectValid;

        #endregion

        #region Methods

        public static bool CheckCurrentProjectIntegrity()
        {
            #region Check Event Stage Integrity

            ProjectValid = true;
            ErrorList = new List<string>();

            #region Verification des nécessités de démarrage
            VO_Project project = GameCore.Instance.Game.Project;
            VO_PlayableCharacter character = GameCore.Instance.GetPlayableCharacterById(project.StartingCharacter);
            if (character == null)
            {
                ProjectValid = false;
                string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.STARTING_CHARACTER_NEEDED);
                ErrorList.Add(FormatedResult);
            }
            else
            {
                VO_Stage stage = GameCore.Instance.GetStageById(character.CoordsCharacter.Map);
                if (stage == null)
                {
                    ProjectValid = false;
                    string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.STARTING_STAGE_NEEDED);
                    ErrorList.Add(FormatedResult);
                }
            }
            if (string.IsNullOrEmpty(project.GuiResource))
            {
                ProjectValid = false;
                string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.GUI_SYSTEM_NEEDED);
                ErrorList.Add(FormatedResult);
            }
            #endregion

            #region Verification des GlobalEvents

            ErrorList.Add(Culture.Language.ProjectIntegrity.INTEGRITY_DATABASEZONE + "\r\n\r\n");

            foreach (VO_GlobalEvent CurrentEvent in GameCore.Instance.Game.GlobalEvents)
            {
                #region Verification des pages

                if (CurrentEvent.Script != null)
                    if (EventScriptIntegrity(CurrentEvent.Script) == false)
                    {
                        ProjectValid = false;
                        string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.SCRIPT_GLOBALEVENT, CurrentEvent.Title);
                        ErrorList.Add(FormatedResult);
                    }

                #endregion
            }

            #endregion

            #region Verification de GameOver

            VO_Script GameOverScript = project.GameOver;
            if (EventScriptIntegrity(GameOverScript) == false)
            {
                ProjectValid = false;
                string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.SCRIPT_GAMEOVER);
                ErrorList.Add(FormatedResult);
            }

            #endregion

            #region Verification des Items

            foreach (VO_Item CurrentItem in GameCore.Instance.Game.Items)
            {
                #region Verification des Action sur Item

                if (CurrentItem.Scripts != null)
                    foreach (VO_ActionOnItemScript CurrentItemScript in CurrentItem.Scripts)
                    {
                        if (EventScriptIntegrity(CurrentItemScript.Script) == false)
                        {
                            VO_Action CurrentAction = GameCore.Instance.GetActionById(CurrentItemScript.Id);
                            ProjectValid = false;
                            string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.SCRIPT_ITEM, CurrentItem.Title, CurrentAction.Title);
                            ErrorList.Add(FormatedResult);
                        }
                    }

                #endregion
            }

            #endregion

            #region Verification des ItemsInteraction

            foreach (VO_Script CurrentItemInteraction in GameCore.Instance.Game.InteractionScripts)
            {
                #region Verification de l'ItemInteraction

                if (CurrentItemInteraction != null)
                    if (EventScriptIntegrity(CurrentItemInteraction) == false)
                    {
                        ProjectValid = false;
                        int FoundItem = 0;
                        List<VO_Item> AssociatedItems = new List<VO_Item>();
                        foreach (VO_Item CurrentItem in GameCore.Instance.Game.Items)
                        {   
                            if (CurrentItem.ItemInteraction.Find(p => p.Script == CurrentItemInteraction.Id) != null)
                            {
                                AssociatedItems.Add(CurrentItem);
                                if (FoundItem == 2)
                                    break;
                                else
                                    FoundItem = FoundItem + 1;
                            }
                        }
                        string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.SCRIPT_ITEMINTERACTION, AssociatedItems[0].Title, AssociatedItems[1].Title);
                        ErrorList.Add(FormatedResult);
                    }
                #endregion
            }

            #endregion

            #region Verification des Personnages de la Database

            List<VO_PlayableCharacter> PlayableCharacterList = GameCore.Instance.Game.PlayableCharacters;

            foreach (VO_PlayableCharacter CurrentPlayableCharacter in PlayableCharacterList)
            {
                if (ValidationTools.CheckMapExistence(CurrentPlayableCharacter.CoordsCharacter) == false)
                {
                    ProjectValid = false;
                    string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.CHECK_PLAYABLECHARACTER_MAP, CurrentPlayableCharacter.Title);
                    ErrorList.Add(FormatedResult);
                }
            }

            #endregion

            ErrorList.Add("\r\n\r\n" + Culture.Language.ProjectIntegrity.INTEGRITY_STAGE + "\r\n\r\n");

            List<VO_Base> StageList = GameCore.Instance.GetStages();
            foreach (VO_Stage CurrentStage in StageList)
            {
                #region Verification du Script de démarrage (Premiere fois)

                if (EventScriptIntegrity(CurrentStage.StartingFirstScript) == false)
                {
                    ProjectValid = false;
                    string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.STAGE_SCRIPT_START_FIRST, CurrentStage.Title);
                    ErrorList.Add(FormatedResult);
                }

                #endregion

                #region Verification du Script de démarrage

                if (EventScriptIntegrity(CurrentStage.StartingScript) == false)
                {
                    ProjectValid = false;
                    string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.STAGE_SCRIPT_START, CurrentStage.Title);
                    ErrorList.Add(FormatedResult);
                }

                #endregion

                #region Verification du Script de fin (Premiere fois)

                if (EventScriptIntegrity(CurrentStage.EndingFirstScript) == false)
                {
                    ProjectValid = false;
                    string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.STAGE_SCRIPT_END_FIRST, CurrentStage.Title);
                    ErrorList.Add(FormatedResult);
                }

                #endregion

                #region Verification du Script de fin

                if (EventScriptIntegrity(CurrentStage.EndingScript) == false)
                {
                    ProjectValid = false;
                    string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.STAGE_SCRIPT_END, CurrentStage.Title);
                    ErrorList.Add(FormatedResult);
                }

                #endregion

                #region Verification des Hotspot

                foreach (VO_StageHotSpot CurrentHotSpot in CurrentStage.ListHotSpots)
                {
                    #region Verification des pages

                    if (CurrentHotSpot.Event != null)
                        foreach (VO_Page CurrentPage in CurrentHotSpot.Event.PageList)
                        {
                            #region Verification du Script Courant

                            if (EventScriptIntegrity(CurrentPage.Script) == false)
                            {
                                ProjectValid = false;
                                string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.STAGE_SCRIPT_HOTSPOT, CurrentStage.Title, CurrentHotSpot.Title, CurrentPage.PageNumber + 1);
                                ErrorList.Add(FormatedResult);
                            }

                            #endregion
                        }

                    #endregion
                }

                #endregion

                #region Verification des Personnages

                foreach (VO_StageCharacter CurrentCharacter in CurrentStage.ListCharacters)
                {
                    #region Verification des pages

                    if (CurrentCharacter.Event != null)
                        foreach (VO_Page CurrentPage in CurrentCharacter.Event.PageList)
                        {
                            #region Verification du Script Courant

                            if (EventScriptIntegrity(CurrentPage.Script) == false)
                            {
                                ProjectValid = false;
                                string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.STAGE_SCRIPT_CHARACTER, CurrentStage.Title, CurrentCharacter.Title, CurrentPage.PageNumber + 1);
                                ErrorList.Add(FormatedResult);
                            }

                            #endregion
                        }

                    #endregion
                }

                #endregion

                #region Verification des Animation

                foreach (VO_Layer CurrentLayer in CurrentStage.ListLayers)
                {
                    #region Verification des pages

                    List<VO_StageAnimation> StageAnimationList = CurrentLayer.ListAnimations;
                    
                    if (StageAnimationList != null)
                    {
                        foreach (VO_StageAnimation CurrentStageAnimation in StageAnimationList)
                        {
                            foreach (VO_Page CurrentPage in CurrentStageAnimation.Event.PageList)
                            {
                                #region Verification du Script Courant

                                if (EventScriptIntegrity(CurrentPage.Script) == false)
                                {
                                    ProjectValid = false;
                                    string FormatedResult = System.String.Format(Culture.Language.ProjectIntegrity.SCRIPT_ANIMATION, CurrentStage.Title, CurrentLayer.Title, CurrentStageAnimation.Title, CurrentPage.PageNumber);
                                    ErrorList.Add(FormatedResult);
                                }

                                #endregion
                            }
                        }
                    }

                    #endregion
                }

                #endregion
            }

            #endregion
            
            return ProjectValid;
        }

        public static bool EventScriptIntegrity(VO_Script CurrentScript)
        {
            if (CurrentScript == null || CurrentScript.Lines.Count <= 0)
                return true;

            bool IsValid = true;
            
            foreach (IScriptable CurrentLine in CurrentScript.Lines)
            {
                if (EventScriptIntegritySublinesCheck(CurrentLine as VO_Line) == false)
                    IsValid = false;
                CurrentLine.Valid = CurrentLine.IsScriptValid();
                if (CurrentLine.Valid == false)
                    IsValid = false;
            }

            return IsValid;
        }

        public static bool EventScriptIntegritySublinesCheck(VO_Line CurrentLine)
        {
            bool IsValid = true;

            if (CurrentLine is VO_Script_Condition)
            {
                VO_Script_Condition CurrentScriptCondition = CurrentLine as VO_Script_Condition;
                CurrentScriptCondition.Valid = CurrentScriptCondition.IsScriptValid();
                IsValid = CurrentScriptCondition.Valid;

                foreach (IScriptable CurrentIfSubLine in CurrentScriptCondition.IfSubLines)
                {
                    if ((CurrentIfSubLine.Valid = CurrentIfSubLine.IsScriptValid()) == false)
                        IsValid = false;
                    if (EventScriptIntegritySublinesCheck(CurrentIfSubLine as VO_Line) == false)
                        IsValid = false;
                }

                foreach (IScriptable CurrentElseSubLine in CurrentScriptCondition.ElseSubLines)
                {
                    if ((CurrentElseSubLine.Valid = CurrentElseSubLine.IsScriptValid()) == false)
                        IsValid = false;
                    if (EventScriptIntegritySublinesCheck(CurrentElseSubLine as VO_Line) == false)
                        IsValid = false;
                }
            }

            if (CurrentLine is VO_Script_Loop)
            {
                VO_Script_Loop CurrentLoop = CurrentLine as VO_Script_Loop;
                CurrentLoop.Valid = CurrentLoop.IsScriptValid();
                IsValid = CurrentLoop.Valid;

                foreach (IScriptable CurrentWhileSubLine in CurrentLoop.WhileSubLines)
                {
                    if ((CurrentWhileSubLine.Valid = CurrentWhileSubLine.IsScriptValid()) == false)
                        IsValid = false;
                    if (EventScriptIntegritySublinesCheck(CurrentWhileSubLine as VO_Line) == false)
                        IsValid = false;
                }
            }

            if (CurrentLine is VO_Script_ChoiceMessage)
            {
                VO_Script_ChoiceMessage CurrentChoiceMessage = CurrentLine as VO_Script_ChoiceMessage;
                CurrentChoiceMessage.Valid = CurrentChoiceMessage.IsScriptValid();
                IsValid = CurrentChoiceMessage.Valid;

                foreach (VO_LineChoices CurrentChoiceSubLine in CurrentChoiceMessage.Choices)
                {
                    foreach (IScriptable CurrentLineChoiceChoice in CurrentChoiceSubLine.SubLines)
                    {
                        if ((CurrentLineChoiceChoice.Valid = CurrentLineChoiceChoice.IsScriptValid()) == false)
                            IsValid = false;

                        if (EventScriptIntegritySublinesCheck(CurrentLineChoiceChoice as VO_Line) == false)
                            IsValid = false;
                    }
                }
            }
            
            return IsValid;
        }

        #endregion
    }
}
