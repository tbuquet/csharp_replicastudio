using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.Forms.ScriptForms;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire d'actions pour le script manager
    /// </summary>
    public partial class EventActions : Form
    {
        #region Properties
        /// <summary>
        /// Ligne retournée
        /// </summary>
        public VO_Line ReturnLine { get; set; }

        /// <summary>
        /// Type de script
        /// </summary>
        public Enums.ScriptType ScriptType { get; set; }

        /// <summary>
        /// Formulaire de Add/Remove Item
        /// </summary>
        public ScriptItem ScriptItem { get; set; }

        /// <summary>
        /// Formulaire de Change Music Frequency
        /// </summary>
        public ScriptMusicFrequency ScriptMusicFrequency { get; set; }

        /// <summary>
        /// Formulaire de ScriptChangePlayerDirection
        /// </summary>
        public ScriptChangePlayerDirection ScriptChangePlayerDirection { get; set; }

        /// <summary>
        /// Formulaire de ScriptChangeCharacterDirection
        /// </summary>
        public ScriptChangeCharacterDirection ScriptChangeCharacterDirection { get; set; }

        /// <summary>
        /// Formulaire de Wait
        /// </summary>
        public ScriptWait ScriptWait { get; set; }

        /// <summary>
        /// Formulaire de Change Variable
        /// </summary>
        public ScriptVariable ScriptVariable { get; set; }

        /// <summary>
        /// Formulaire de Press Switch
        /// </summary>
        public ScriptPressSwitch ScriptPressSwitch { get; set; }

        /// <summary>
        /// Formulaire de Random
        /// </summary>
        public ScriptRandom ScriptRandom { get; set; }

        /// <summary>
        /// Formulaire de ChangeCurrentCharacter
        /// </summary>
        public ScriptChangeCurrentCharacter ScriptChangeCurrentCharacter { get; set; }

        /// <summary>
        /// Formulaire de Get/Set Anchor
        /// </summary>
        public ScriptGetSetAnchor ScriptGetSetAnchor { get; set; }

        /// <summary>
        /// Formulaire de Get/Set Anchor
        /// </summary>
        public ScriptStopCharacterMovement ScriptStopCharacterMovement { get; set; }

        /// <summary>
        /// Formulaire MoveCamera
        /// </summary>
        public ScriptMoveCamera ScriptMoveCamera { get; set; }

        /// <summary>
        /// Formulaire MoveCharacter
        /// </summary>
        public ScriptMoveCharacter ScriptMoveCharacter { get; set; }

        /// <summary>
        /// Formulaire AddPlayerAction
        /// </summary>
        public ScriptAddPlayerAction ScriptAddPlayerAction { get; set; }

        /// <summary>
        /// Formulaire FocusOnCharacter
        /// </summary>
        public ScriptCameraFocusOnCharacter ScriptCameraFocusOnCharacter { get; set; }

        /// <summary>
        /// Formulaire FocusOnAnimation
        /// </summary>
        public ScriptCameraFocusOnAnimation ScriptCameraFocusOnAnimation { get; set; }

        /// <summary>
        /// Formulaire Call Global Event
        /// </summary>
        public ScriptCallGlobalEvent ScriptCallGlobalEvent { get; set; }

        /// <summary>
        /// Formulaire Change Player Speed
        /// </summary>
        public ScriptChangePlayerSpeed ScriptChangePlayerSpeed { get; set; }

        /// <summary>
        /// Formulaire Add Comment
        /// </summary>
        public ScriptAddComment ScriptAddComment { get; set; }

        /// <summary>
        /// Formulaire Change Player HP
        /// </summary>
        public ScriptChangePlayerHP ScriptChangePlayerHP { get; set; }

        /// <summary>
        /// Formulaire de Animation Frequency
        /// </summary>
        public ScriptCharacterAnimationFrequency ScriptCharacterAnimationFrequency { get; set; }

        /// <summary>
        /// Formulaire de Freeze Character Animation
        /// </summary>
        public ScriptFreezeCharacterAnimation ScriptFreezeCharacterAnimation { get; set; }

        /// <summary>
        /// Formulaire de Free Character Animation
        /// </summary>
        public ScriptFreeCharacterAnimation ScriptFreeCharacterAnimation { get; set; }

        /// <summary>
        /// Formulaire de Freeze Player Animation
        /// </summary>
        public ScriptFreezePlayerAnimation ScriptFreezePlayerAnimation { get; set; }

        /// <summary>
        /// Formulaire de Free Player Animation
        /// </summary>
        public ScriptFreePlayerAnimation ScriptFreePlayerAnimation { get; set; }

        /// <summary>
        /// Formulaire de Change Player Animation
        /// </summary>
        public ScriptChangePlayerAnimation ScriptChangePlayerAnimation { get; set; }

        /// <summary>
        /// Formulaire de Look forward Player
        /// </summary>
        public ScriptLookForwardPlayer ScriptLookForwardPlayer { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public EventActions()
        {
            InitializeComponent();
            ScriptItem = new ScriptItem();
            ScriptMusicFrequency = new ScriptMusicFrequency();
            ScriptChangePlayerDirection = new ScriptChangePlayerDirection();
            ScriptChangeCharacterDirection = new ScriptChangeCharacterDirection();
            ScriptWait = new ScriptWait();
            ScriptVariable = new ScriptVariable();
            ScriptPressSwitch = new ScriptPressSwitch();
            ScriptRandom = new ScriptRandom();
            ScriptChangeCurrentCharacter = new ScriptChangeCurrentCharacter();
            ScriptGetSetAnchor = new ScriptGetSetAnchor();
            ScriptStopCharacterMovement = new ScriptStopCharacterMovement();
            ScriptMoveCamera = new ScriptMoveCamera();
            ScriptMoveCharacter = new ScriptMoveCharacter();
            ScriptAddPlayerAction = new ScriptAddPlayerAction();
            ScriptCameraFocusOnCharacter = new ScriptCameraFocusOnCharacter();
            ScriptCameraFocusOnAnimation = new ScriptCameraFocusOnAnimation();
            ScriptCallGlobalEvent = new ScriptCallGlobalEvent();
            ScriptChangePlayerSpeed = new ScriptChangePlayerSpeed();
            ScriptAddComment = new ScriptAddComment();
            ScriptChangePlayerHP = new ScriptChangePlayerHP();
            ScriptCharacterAnimationFrequency = new ScriptCharacterAnimationFrequency();
            ScriptFreezeCharacterAnimation = new ScriptFreezeCharacterAnimation();
            ScriptFreeCharacterAnimation = new ScriptFreeCharacterAnimation();
            ScriptFreezePlayerAnimation = new ScriptFreezePlayerAnimation();
            ScriptFreePlayerAnimation = new ScriptFreePlayerAnimation();
            ScriptChangePlayerAnimation = new ScriptChangePlayerAnimation();
            ScriptLookForwardPlayer = new ScriptLookForwardPlayer();
        }
        #endregion

        #region Methods
        public void DesactivateAllScripts()
        {
            #region Player
            btnScriptAddItem.Enabled = false;
            chgAddPlayerAction.Enabled = false;
            btnScriptChangeCharacter.Enabled = false;
            btnScriptChangeHP.Enabled = false;
            btnScriptChangeMaxHP.Enabled = false;
            btnScriptChangePlayerAnimation.Enabled = false;
            btnChangePlayerDirection.Enabled = false;
            btnScriptChangePlayerSpeed.Enabled = false;
            btnScriptChoiceMessages.Enabled = false;
            btnScriptFreePlayerAnimations.Enabled = false;
            btnScriptFreezePlayerAnimations.Enabled = false;
            btnScriptHideCurrentPlayer.Enabled = false;
            btnScriptMessage.Enabled = false;
            btnScriptMovePlayer.Enabled = false;
            btnScriptRemoveItem.Enabled = false;
            RemovePlayerAction.Enabled = false;
            btnScriptShowCurrentPlayer.Enabled = false;
            StopCurrentPlayerMovement.Enabled = false;
            btnScriptTeleport.Enabled = false;
            #endregion

            #region Stage
            btnScriptChangeCharacterFrequency.Enabled = false;
            chgCharacterDirection.Enabled = false;
            btnScriptDefaultCamera.Enabled = false;
            btnScriptFocusOnCharacter.Enabled = false;
            btnScriptFocusOnAnimation.Enabled = false;
            btnScriptFreeCharacter.Enabled = false;
            btnScriptFreezeCharacter.Enabled = false;
            btnLookForwardPlayer.Enabled = false;
            btnScriptMoveCamera.Enabled = false;
            btnScriptMoveCharacter.Enabled = false;
            StopCharacterMovement.Enabled = false;
            #endregion

            #region Interface
            btnScriptChangeCurrentAction.Enabled = false;
            btnScriptDisableStageInteractions.Enabled = false;
            btnScriptDisableUserControls.Enabled = false;
            btnScriptEnableUserControls.Enabled = false;
            btnScriptEnableStageInteractions.Enabled = false;
            btnScriptHideLifeBar.Enabled = false;
            btnScriptShowLifeBar.Enabled = false;
            #endregion

            #region Media
            btnScriptChangeMusicFrequency.Enabled = false;
            btnScriptPlayMusic.Enabled = false;
            btnScriptPlaySound.Enabled = false;
            btnScriptStopMusic.Enabled = false;
            btnScriptStopSound.Enabled = false;
            #endregion

            #region Menu
            btnScriptCloseInventory.Enabled = false;
            btnScriptDisableSaves.Enabled = false;
            btnScriptEnableSaves.Enabled = false;
            btnScriptGameOver.Enabled = false;
            btnScriptLoadGame.Enabled = false;
            btnScriptOpenInventory.Enabled = false;
            btnScriptSaveGame.Enabled = false;
            btnScriptGoToTitleScreen.Enabled = false;
            #endregion

            #region Programming
            btnScriptBreakLoop.Enabled = false;
            btnScriptCallGlobalEvent.Enabled = false;
            btnScriptChangeVariable.Enabled = false;
            btnScriptInsertComment.Enabled = false;
            btnScriptCondition.Enabled = false;
            btnScriptGoToAnchor.Enabled = false;
            btnScriptLoop.Enabled = false;
            btnScriptChangeSwitch.Enabled = false;
            btnScriptRandom.Enabled = false;
            btnScriptSetAnchor.Enabled = false;
            btnScriptWait.Enabled = false;
            #endregion  
        }

        public void ActivateStageScripts()
        {
            #region Player
            btnScriptAddItem.Enabled = true;
            chgAddPlayerAction.Enabled = true;
            btnScriptChangeCharacter.Enabled = true;
            btnScriptChangeHP.Enabled = true;
            btnScriptChangeMaxHP.Enabled = true;
            btnScriptChangePlayerAnimation.Enabled = true;
            btnChangePlayerDirection.Enabled = true;
            btnScriptChangePlayerSpeed.Enabled = true;
            btnScriptChoiceMessages.Enabled = true;
            btnScriptFreePlayerAnimations.Enabled = true;
            btnScriptFreezePlayerAnimations.Enabled = true;
            btnScriptHideCurrentPlayer.Enabled = true;
            btnScriptMessage.Enabled = true;
            btnScriptMovePlayer.Enabled = true;
            btnScriptRemoveItem.Enabled = true;
            RemovePlayerAction.Enabled = true;
            btnScriptShowCurrentPlayer.Enabled = true;
            StopCurrentPlayerMovement.Enabled = true;
            btnScriptTeleport.Enabled = true;
            #endregion

            #region Stage
            btnScriptChangeCharacterFrequency.Enabled = true;
            chgCharacterDirection.Enabled = true;
            btnScriptDefaultCamera.Enabled = true;
            btnScriptFocusOnCharacter.Enabled = true;
            btnScriptFocusOnAnimation.Enabled = true;
            btnScriptFreeCharacter.Enabled = true;
            btnScriptFreezeCharacter.Enabled = true;
            btnLookForwardPlayer.Enabled = true;
            btnScriptMoveCamera.Enabled = true;
            btnScriptMoveCharacter.Enabled = true;
            StopCharacterMovement.Enabled = true;
            #endregion

            #region Interface
            btnScriptChangeCurrentAction.Enabled = true;
            btnScriptDisableStageInteractions.Enabled = true;
            btnScriptDisableUserControls.Enabled = true;
            btnScriptEnableUserControls.Enabled = true;
            btnScriptEnableStageInteractions.Enabled = true;
            btnScriptHideLifeBar.Enabled = true;
            btnScriptShowLifeBar.Enabled = true;
            #endregion

            #region Media
            btnScriptChangeMusicFrequency.Enabled = true;
            btnScriptPlayMusic.Enabled = true;
            btnScriptPlaySound.Enabled = true;
            btnScriptStopMusic.Enabled = true;
            btnScriptStopSound.Enabled = true;
            #endregion

            #region Menu
            btnScriptCloseInventory.Enabled = true;
            btnScriptDisableSaves.Enabled = true;
            btnScriptEnableSaves.Enabled = true;
            btnScriptGameOver.Enabled = true;
            btnScriptLoadGame.Enabled = true;
            btnScriptOpenInventory.Enabled = true;
            btnScriptSaveGame.Enabled = true;
            btnScriptGoToTitleScreen.Enabled = true;
            #endregion

            #region Programming
            btnScriptBreakLoop.Enabled = true;
            btnScriptCallGlobalEvent.Enabled = true;
            btnScriptChangeVariable.Enabled = true;
            btnScriptInsertComment.Enabled = true;
            btnScriptCondition.Enabled = true;
            btnScriptGoToAnchor.Enabled = true;
            btnScriptLoop.Enabled = true;
            btnScriptChangeSwitch.Enabled = true;
            btnScriptRandom.Enabled = true;
            btnScriptSetAnchor.Enabled = true;
            btnScriptWait.Enabled = true;
            #endregion
        }

        public void ActivateGlobalScripts()
        {
            #region Player
            btnScriptAddItem.Enabled = true;
            chgAddPlayerAction.Enabled = true;
            btnScriptChangeCharacter.Enabled = true;
            btnScriptChangeHP.Enabled = true;
            btnScriptChangeMaxHP.Enabled = true;
            btnScriptChangePlayerAnimation.Enabled = true;
            btnChangePlayerDirection.Enabled = true;
            btnScriptChangePlayerSpeed.Enabled = true;
            btnScriptChoiceMessages.Enabled = true;
            btnScriptFreePlayerAnimations.Enabled = true;
            btnScriptFreezePlayerAnimations.Enabled = true;
            btnScriptHideCurrentPlayer.Enabled = true;
            btnScriptMessage.Enabled = true;
            btnScriptRemoveItem.Enabled = true;
            RemovePlayerAction.Enabled = true;
            btnScriptShowCurrentPlayer.Enabled = true;
            StopCurrentPlayerMovement.Enabled = true;
            btnScriptTeleport.Enabled = true;
            #endregion

            #region Stage
            btnScriptDefaultCamera.Enabled = true;
            #endregion

            #region Interface
            btnScriptChangeCurrentAction.Enabled = true;
            btnScriptDisableStageInteractions.Enabled = true;
            btnScriptDisableUserControls.Enabled = true;
            btnScriptEnableUserControls.Enabled = true;
            btnScriptEnableStageInteractions.Enabled = true;
            btnScriptHideLifeBar.Enabled = true;
            btnScriptShowLifeBar.Enabled = true;
            #endregion

            #region Media
            btnScriptChangeMusicFrequency.Enabled = true;
            btnScriptPlayMusic.Enabled = true;
            btnScriptPlaySound.Enabled = true;
            btnScriptStopMusic.Enabled = true;
            btnScriptStopSound.Enabled = true;
            #endregion

            #region Menu
            btnScriptCloseInventory.Enabled = true;
            btnScriptDisableSaves.Enabled = true;
            btnScriptEnableSaves.Enabled = true;
            btnScriptGameOver.Enabled = true;
            btnScriptLoadGame.Enabled = true;
            btnScriptOpenInventory.Enabled = true;
            btnScriptSaveGame.Enabled = true;
            btnScriptGoToTitleScreen.Enabled = true;
            #endregion

            #region Programming
            btnScriptBreakLoop.Enabled = true;
            btnScriptCallGlobalEvent.Enabled = true;
            btnScriptChangeVariable.Enabled = true;
            btnScriptInsertComment.Enabled = true;
            btnScriptCondition.Enabled = true;
            btnScriptGoToAnchor.Enabled = true;
            btnScriptLoop.Enabled = true;
            btnScriptChangeSwitch.Enabled = true;
            btnScriptRandom.Enabled = true;
            btnScriptSetAnchor.Enabled = true;
            btnScriptWait.Enabled = true;
            #endregion
        }
        #endregion

        #region Scripts
        #region Character
        /// <summary>
        /// Script Hide Player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_HideCurrentPlayer_Click(object sender, EventArgs e)
        {
            VO_Script_HidePlayer line = new VO_Script_HidePlayer();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Script Show Player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_ShowCurrentPlayer_Click(object sender, EventArgs e)
        {
            VO_Script_ShowPlayer line = new VO_Script_ShowPlayer();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Script Déplacer le joueur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_MovePlayer_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.CoordsManager.SourceResolution = EditorHelper.Instance.GetCurrentStageInstance().Dimensions;
            FormsManager.Instance.CoordsManager.SourceObject = new Rectangle(new Point(), new Size());
            FormsManager.Instance.CoordsManager.UseStageBackground = true;
            if (FormsManager.Instance.CoordsManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_MovePlayer line = new VO_Script_MovePlayer();
                line.Id = Guid.NewGuid();
                line.Coords = FormsManager.Instance.CoordsManager.DestinationObject;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Script Changer Direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_ChangePlayerDirection_Click(object sender, EventArgs e)
        {
            ScriptChangePlayerDirection.Direction = Enums.Movement.Down;
            if (ScriptChangePlayerDirection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_ChangePlayerDirection line = new VO_Script_ChangePlayerDirection();
                line.Id = Guid.NewGuid();
                line.Direction = ScriptChangePlayerDirection.Direction;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Script Afficher un message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_Message_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.DialogManager.CurrentDialog = new VO_Dialog();
            FormsManager.Instance.DialogManager.CurrentDialog.Messages = new List<VO_Message>();
            FormsManager.Instance.DialogManager.LoadDialog(FormsManager.Instance.DialogManager.CurrentDialog, ScriptType);
            if (FormsManager.Instance.DialogManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_Message line = new VO_Script_Message();
                line.Id = Guid.NewGuid();
                line.Dialog = FormsManager.Instance.DialogManager.CurrentDialog;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Script afficher des choix
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_ChoiceMessages_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.ScriptChoice.ChoiceMessage = new VO_Script_ChoiceMessage();
            FormsManager.Instance.ScriptChoice.InitNewScript();
            if (FormsManager.Instance.ScriptChoice.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReturnLine = FormsManager.Instance.ScriptChoice.ChoiceMessage;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region Programmation
        /// <summary>
        /// Script Condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_Condition_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.ScriptCondition.Condition = new VO_Script_Condition();
            FormsManager.Instance.ScriptCondition.Condition.InitNewScript();
            if (FormsManager.Instance.ScriptCondition.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReturnLine = FormsManager.Instance.ScriptCondition.Condition;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Script Loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_Loop_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.ScriptLoop.Loop = new VO_Script_Loop();
            FormsManager.Instance.ScriptLoop.Loop.InitNewScript();
            if (FormsManager.Instance.ScriptLoop.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReturnLine = FormsManager.Instance.ScriptLoop.Loop;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Change Variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_ChangeVariable(object sender, EventArgs e)
        {
            ScriptVariable.IsAdd = true;
            if (ScriptVariable.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_ChangeVariable line = new VO_Script_ChangeVariable();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Value = ScriptVariable.CurrentVariable.Value;
                line.Variable = ScriptVariable.CurrentVariable.Variable;
                line.Operator = ScriptVariable.CurrentVariable.Operator;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        #endregion

        #region Effets
        /// <summary>
        /// Script Move Camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_MoveCamera_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera = new VO_Script_MoveCamera();
            FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Coords = new VO_Coords();
            FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Coords.Location = new Point();
            FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.UseImmediately = true;
            FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Speed = 1;
            if (FormsManager.Instance.EventActions.ScriptMoveCamera.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_MoveCamera line = new VO_Script_MoveCamera();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Coords = FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Coords;
                line.Speed = FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Speed;
                line.UseImmediately = FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.UseImmediately;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_DefaultCamera_Click(object sender, EventArgs e)
        {
            VO_Script_DefaultCamera line = new VO_Script_DefaultCamera();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_FocusOnCharacter_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter = new VO_Script_FocusOnCharacter();
            FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.UseImmediately = true;
            FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.Speed = 1;
            if (FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_FocusOnCharacter line = new VO_Script_FocusOnCharacter();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Character = FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.Character;
                line.Speed = FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.Speed;
                line.UseImmediately = FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.UseImmediately;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_FocusOnAnimation_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation = new VO_Script_FocusOnAnimation();
            FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.UseImmediately = true;
            FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.Speed = 1;
            if (FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_FocusOnAnimation line = new VO_Script_FocusOnAnimation();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Animation = FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.Animation;
                line.Speed = FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.Speed;
                line.UseImmediately = FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.UseImmediately;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region Stage
        /// <summary>
        /// Script MoveCharacter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventActions_MoveCharacter_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter = new VO_Script_MoveCharacter();
            FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter.Coords = new VO_Coords();
            FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter.Coords.Location = new Point();
            if (FormsManager.Instance.EventActions.ScriptMoveCharacter.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_MoveCharacter line = new VO_Script_MoveCharacter();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Coords = FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter.Coords;
                line.Character = FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter.Character;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        private void EventActions_HideLifeBar(object sender, EventArgs e)
        {
            VO_Script_HideLifeBar line = new VO_Script_HideLifeBar();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_ShowLifeBar(object sender, EventArgs e)
        {
            VO_Script_ShowLifeBar line = new VO_Script_ShowLifeBar();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_DisableStageInteractions(object sender, EventArgs e)
        {
            VO_Script_DisableStageInteractions line = new VO_Script_DisableStageInteractions();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_EnableStageInteractions(object sender, EventArgs e)
        {
            VO_Script_EnableStageInteractions line = new VO_Script_EnableStageInteractions();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_DisableUserControls(object sender, EventArgs e)
        {
            VO_Script_DisableUserControls line = new VO_Script_DisableUserControls();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_EnableUserControls(object sender, EventArgs e)
        {
            VO_Script_EnableUserControls line = new VO_Script_EnableUserControls();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_TitleScreen(object sender, EventArgs e)
        {
            VO_Script_TitleScreen line = new VO_Script_TitleScreen();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_OpenInventory(object sender, EventArgs e)
        {
            VO_Script_OpenInventory line = new VO_Script_OpenInventory();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_CloseInventory(object sender, EventArgs e)
        {
            VO_Script_CloseInventory line = new VO_Script_CloseInventory();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_GameOver(object sender, EventArgs e)
        {
            VO_Script_GameOver line = new VO_Script_GameOver();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_DisableSaves(object sender, EventArgs e)
        {
            VO_Script_DisableSaves line = new VO_Script_DisableSaves();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_EnableSaves(object sender, EventArgs e)
        {
            VO_Script_EnableSaves line = new VO_Script_EnableSaves();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_StopMusic(object sender, EventArgs e)
        {
            VO_Script_StopMusic line = new VO_Script_StopMusic();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_StopSound(object sender, EventArgs e)
        {
            VO_Script_StopSound line = new VO_Script_StopSound();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void EventActions_AddItem(object sender, EventArgs e)
        {
            if (ScriptItem.ShowDialog() == DialogResult.OK)
            {
                VO_Script_AddItem line = new VO_Script_AddItem();
                line.Id = Guid.NewGuid();
                line.Character = ScriptItem.CharacterGuid;
                line.Item = ScriptItem.ItemGuid;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_RemoveItem(object sender, EventArgs e)
        {
            if (ScriptItem.ShowDialog() == DialogResult.OK)
            {
                VO_Script_RemoveItem line = new VO_Script_RemoveItem();
                line.Id = Guid.NewGuid();
                line.Character = ScriptItem.CharacterGuid;
                line.Item = ScriptItem.ItemGuid;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_PlayMusic(object sender, EventArgs e)
        {
            FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_MUSICS;
            FormsManager.Instance.ResourcesManager.SelectedFilePath = String.Empty;
            if (FormsManager.Instance.ResourcesManager.ShowDialog() == DialogResult.OK)
            {
                VO_Script_PlayMusic line = new VO_Script_PlayMusic();
                line.Id = Guid.NewGuid();
                line.Music = FormsManager.Instance.ResourcesManager.SelectedFilePath;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_PlaySound(object sender, EventArgs e)
        {
            FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_EFFECTS;
            FormsManager.Instance.ResourcesManager.SelectedFilePath = String.Empty;
            if (FormsManager.Instance.ResourcesManager.ShowDialog() == DialogResult.OK)
            {
                VO_Script_PlaySound line = new VO_Script_PlaySound();
                line.Id = Guid.NewGuid();
                line.Sound = FormsManager.Instance.ResourcesManager.SelectedFilePath;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_ChangeMusicFrequency(object sender, EventArgs e)
        {
            ScriptMusicFrequency.IsAdd = true;
            if (ScriptMusicFrequency.ShowDialog() == DialogResult.OK)
            {
                VO_Script_ChangeMusicFrequency line = new VO_Script_ChangeMusicFrequency();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Frequency = ScriptMusicFrequency.Frequency;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_Teleport(object sender, EventArgs e)
        {
            FormsManager.Instance.CoordsManager.SourceFullObject = new VO_Coords(new Point(), Guid.Empty);
            FormsManager.Instance.CoordsManager.UseStages = true;
            FormsManager.Instance.CoordsManager.SourceObject = new Rectangle(new Point(), new Size());
            if (FormsManager.Instance.CoordsManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_Teleport line = new VO_Script_Teleport();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Coords = FormsManager.Instance.CoordsManager.DestinationObject;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_Wait(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptWait.IsAdd = true;
            if (FormsManager.Instance.EventActions.ScriptWait.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_Wait line = new VO_Script_Wait();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.SecondsToWait = FormsManager.Instance.EventActions.ScriptWait.WaitTime;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_PressSwitch(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptPressSwitch.IsAdd = true;
            if (FormsManager.Instance.EventActions.ScriptPressSwitch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_PressSwitch line = new VO_Script_PressSwitch();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.IsActive = FormsManager.Instance.EventActions.ScriptPressSwitch.Switch.IsActive;
                line.Button = FormsManager.Instance.EventActions.ScriptPressSwitch.Switch.Button;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_Random(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptRandom.IsAdd = true;
            FormsManager.Instance.EventActions.ScriptRandom.VariableId = Guid.Empty;
            FormsManager.Instance.EventActions.ScriptRandom.MaxValue = new VO_IntValue();
            FormsManager.Instance.EventActions.ScriptRandom.MinValue = new VO_IntValue();
            if (FormsManager.Instance.EventActions.ScriptRandom.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_Random line = new VO_Script_Random();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Variable = FormsManager.Instance.EventActions.ScriptRandom.VariableId;
                line.MaxValue = FormsManager.Instance.EventActions.ScriptRandom.MaxValue;
                line.MinValue = FormsManager.Instance.EventActions.ScriptRandom.MinValue;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_ChangeCurrentCharacter(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.IsAdd = true;
            if (FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_ChangeCurrentCharacter line = new VO_Script_ChangeCurrentCharacter();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Character = FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.Character;
                line.Coords = FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.Coords;
                line.UseOldCoords = FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.UseOldCoords;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_Breakloop(object sender, EventArgs e)
        {
            VO_Script_Break line = new VO_Script_Break();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EventActions_SetAnchor(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptGetSetAnchor.IsAdd = true;
            if (FormsManager.Instance.EventActions.ScriptGetSetAnchor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_SetAnchor line = new VO_Script_SetAnchor();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Anchor = FormsManager.Instance.EventActions.ScriptGetSetAnchor.Anchor;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_GoToAnchor(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptGetSetAnchor.IsAdd = true;
            if (FormsManager.Instance.EventActions.ScriptGetSetAnchor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_GoToAnchor line = new VO_Script_GoToAnchor();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Anchor = FormsManager.Instance.EventActions.ScriptGetSetAnchor.Anchor;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_StopCurrentPlayerMovement(object sender, EventArgs e)
        {
            VO_Script_StopCurrentPlayerMovement line = new VO_Script_StopCurrentPlayerMovement();
            line.Id = Guid.NewGuid();
            ReturnLine = line;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EventActions_StopCharacterMovement(object sender, EventArgs e)
        {
            FormsManager.Instance.EventActions.ScriptStopCharacterMovement.IsAdd = true;
            if (FormsManager.Instance.EventActions.ScriptStopCharacterMovement.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_StopCharacterMovements line = new VO_Script_StopCharacterMovements();
                line.Id = Guid.NewGuid();
                line.Character = FormsManager.Instance.EventActions.ScriptStopCharacterMovement.CurrentCharacter.Id;
                ReturnLine = line;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_ChangeCharacterDirection(object sender, EventArgs e)
        {
            ScriptChangeCharacterDirection.Direction = Enums.Movement.Down;
            ScriptChangeCharacterDirection.IsAdd = true;
            if (ScriptChangeCharacterDirection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_ChangeCharacterDirection line = new VO_Script_ChangeCharacterDirection();
                line.Id = Guid.NewGuid();
                line.Direction = ScriptChangeCharacterDirection.Direction;
                line.CharacterId = ScriptChangeCharacterDirection.CurrentCharacterId;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_AddPlayerAction(object sender, EventArgs e)
        {
            ScriptAddPlayerAction.IsAdd = true;
            if (ScriptAddPlayerAction.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_AddPlayerAction line = new VO_Script_AddPlayerAction();
                line.Id = Guid.NewGuid();
                line.ActionId = ScriptAddPlayerAction.ActionId;
                line.CharacterId = ScriptAddPlayerAction.CharacterId;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_RemovePlayerAction(object sender, EventArgs e)
        {
            ScriptAddPlayerAction.IsAdd = true;
            if (ScriptAddPlayerAction.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_RemovePlayerAction line = new VO_Script_RemovePlayerAction();
                line.Id = Guid.NewGuid();
                line.ActionId = ScriptAddPlayerAction.ActionId;
                line.CharacterId = ScriptAddPlayerAction.CharacterId;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_CallGlobalEvent(object sender, EventArgs e)
        {
            ScriptCallGlobalEvent.IsAdd = true;
            if (ScriptCallGlobalEvent.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_CallGlobalEvent line = new VO_Script_CallGlobalEvent();
                line.Id = Guid.NewGuid();
                line.GlobalEvent = ScriptCallGlobalEvent.GlobalEventId;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_ChangePlayerSpeed(object sender, EventArgs e)
        {
            ScriptChangePlayerSpeed.IsAdd = true;
            if (ScriptChangePlayerSpeed.ShowDialog() == DialogResult.OK)
            {
                VO_Script_ChangePlayerSpeed line = new VO_Script_ChangePlayerSpeed();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Speed.IntValue = ScriptChangePlayerSpeed.Frequency;
                line.CharacterId = ScriptChangePlayerSpeed.CharacterId;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_InsertComment(object sender, EventArgs e)
        {
            ScriptAddComment.IsAdd = true;
            if (ScriptAddComment.ShowDialog() == DialogResult.OK)
            {
                VO_Script_Comment line = new VO_Script_Comment();
                line.Id = Guid.NewGuid();
                line.Comment = ScriptAddComment.Comment;
                ReturnLine = line;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_ChangeHP(object sender, EventArgs e)
        {
            ScriptChangePlayerHP.IsAdd = true;
            if (ScriptChangePlayerHP.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_ChangeHP line = new VO_Script_ChangeHP();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Value = ScriptChangePlayerHP.Value;
                line.Operator = ScriptChangePlayerHP.Operator;
                line.CharacterId = ScriptChangePlayerHP.CharacterId;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_ChangeMaxHP(object sender, EventArgs e)
        {
            ScriptChangePlayerHP.IsAdd = true;
            if (ScriptChangePlayerHP.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_ChangeMaxHP line = new VO_Script_ChangeMaxHP();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Value = ScriptChangePlayerHP.Value;
                line.Operator = ScriptChangePlayerHP.Operator;
                line.CharacterId = ScriptChangePlayerHP.CharacterId;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EvenActions_ChangeAnimationFrequency(object sender, EventArgs e)
        {
            ScriptCharacterAnimationFrequency.IsAdd = true;
            if (ScriptCharacterAnimationFrequency.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_ChangeCharacterAnimFrequency line = new VO_Script_ChangeCharacterAnimFrequency();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.AnimationType = ScriptCharacterAnimationFrequency.AnimationType;
                line.Frequency.IntValue = ScriptCharacterAnimationFrequency.Frequency;
                line.Character = ScriptCharacterAnimationFrequency.CharacterId;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_FreezeCharacterAnimation(object sender, EventArgs e)
        {
            ScriptFreezeCharacterAnimation.IsAdd = true;
            if (ScriptFreezeCharacterAnimation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_FreezeCharacterAnimation line = new VO_Script_FreezeCharacterAnimation();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.AnimationType = ScriptFreezeCharacterAnimation.AnimationType;
                line.Character = ScriptFreezeCharacterAnimation.CharacterId;
                line.FreezeAll = ScriptFreezeCharacterAnimation.AllAnimation;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_FreeCharacterAnimation(object sender, EventArgs e)
        {
            ScriptFreeCharacterAnimation.IsAdd = true;
            if (ScriptFreeCharacterAnimation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_FreeCharacterAnimation line = new VO_Script_FreeCharacterAnimation();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.AnimationType = ScriptFreeCharacterAnimation.AnimationType;
                line.Character = ScriptFreeCharacterAnimation.CharacterId;
                line.FreeAll = ScriptFreeCharacterAnimation.AllAnimation;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_FreezePlayerAnimations(object sender, EventArgs e)
        {
            ScriptFreezePlayerAnimation.IsAdd = true;
            if (ScriptFreezePlayerAnimation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_FreezePlayerAnimation line = new VO_Script_FreezePlayerAnimation();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.AnimationType = ScriptFreezePlayerAnimation.AnimationType;
                line.Character = ScriptFreezePlayerAnimation.CharacterId;
                line.FreezeAll = ScriptFreezePlayerAnimation.AllAnimation;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_FreePlayerAnimations(object sender, EventArgs e)
        {
            ScriptFreePlayerAnimation.IsAdd = true;
            if (ScriptFreePlayerAnimation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_FreePlayerAnimation line = new VO_Script_FreePlayerAnimation();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.AnimationType = ScriptFreePlayerAnimation.AnimationType;
                line.Character = ScriptFreePlayerAnimation.CharacterId;
                line.FreeAll = ScriptFreePlayerAnimation.AllAnimation;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_ChangePlayerAnimation(object sender, EventArgs e)
        {
            ScriptChangePlayerAnimation.IsAdd = true;
            if (ScriptChangePlayerAnimation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_ChangePlayerAnimation line = new VO_Script_ChangePlayerAnimation();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.AnimationType = ScriptChangePlayerAnimation.AnimationType;
                line.Character = ScriptChangePlayerAnimation.CharacterId;
                line.Animation = ScriptChangePlayerAnimation.CharacterAnimationType;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void EventActions_LookForwardPlayer(object sender, EventArgs e)
        {
            ScriptLookForwardPlayer.IsAdd = true;
            if (ScriptLookForwardPlayer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VO_Script_LookForwardPlayer line = new VO_Script_LookForwardPlayer();
                line.Id = Guid.NewGuid();
                ReturnLine = line;
                line.Character = ScriptLookForwardPlayer.CharacterId;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        #endregion

        #region Autres EventHandlers
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Override
        protected override void OnLoad(EventArgs e)
        {
            DesactivateAllScripts();
            switch (ScriptType)
            {
                case Enums.ScriptType.AnimationEvents:
                    ActivateStageScripts();
                    break;
                case Enums.ScriptType.CharacterEvents:
                    ActivateStageScripts();
                    break;
                case Enums.ScriptType.Events:
                    ActivateStageScripts();
                    break;
                case Enums.ScriptType.GameOverEvent:
                    ActivateGlobalScripts();
                    break;
                case Enums.ScriptType.GlobalEvents:
                    ActivateGlobalScripts();
                    break;
                case Enums.ScriptType.ItemEvents:
                    ActivateGlobalScripts();
                    break;
                case Enums.ScriptType.StageEvents:
                    ActivateStageScripts();
                    break;
            }
        }

        /// <summary>
        /// Désactiver F4
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;
            else
                return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}
