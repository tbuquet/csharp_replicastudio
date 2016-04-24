using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using System.Text.RegularExpressions;
using System.IO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.Runtime.Serialization.Json;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire du script manager
    /// </summary>
    public partial class ScriptManager : UserControl
    {
        #region Properties
        /// <summary>
        /// Script courant
        /// </summary>
        public VO_Script Script { get; set; }
        #endregion

        #region Events
        public event EventHandler ScriptUpdated;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ScriptManager()
        {
            InitializeComponent();
        }
        #endregion

        #region Override

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Manager.SelectedNode != null)
                this.ScriptManager_Delete(this, null);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Chargement du script
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        void RenderSpecialNode(object sender, DrawTreeNodeEventArgs e)
        {
            List<string[]> TextColorList = null;

            if (e.Node.Text == String.Empty || e.Node.Text[0] != '{')
            {
                    
            	string[] Simple = new string[2];
                Simple[0] = GlobalConstants.TREEVIEW_BLACK;
                Simple[1] = e.Node.Text;

                TextColorList = new List<string[]>();
                TextColorList.Add(Simple);
            }
            else
            {
                Stream ColorStream = new MemoryStream();
                byte[] StreamByte = Encoding.Unicode.GetBytes(e.Node.Text);

                ColorStream.Write(StreamByte, 0, StreamByte.Length);
                ColorStream.Seek(0, SeekOrigin.Begin);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TreeViewColorTool));

                TreeViewColorTool Instance = (TreeViewColorTool)ser.ReadObject(ColorStream);
                TextColorList = Instance.TextColorList;
            }

            string BuildedString = string.Empty;
            Point newst = e.Bounds.Location;

            VO_Line CurrentLine = (VO_Line)e.Node.Tag;
            foreach (string[] CurrentText in TextColorList)
            {
                #region Si le VO_Line n'est pas valide, alors remplacer le texte en rouge
                
                if (CurrentLine != null && CurrentLine.Valid == false)
                    CurrentText[0] = GlobalConstants.TREEVIEW_RED;

                #endregion

                e.Graphics.DrawString(CurrentText[1], new Font(this.Font, FontStyle.Regular), new SolidBrush(ColorTranslator.FromHtml(CurrentText[0])), newst);
                BuildedString += CurrentText[1];
                newst.X = newst.X + (int)e.Graphics.MeasureString(CurrentText[1], new Font(this.Font, FontStyle.Regular)).Width;
            }

            e.Node.ToolTipText = BuildedString;

            //e.Graphics.DrawString(s[0], new Font("Arial", 10f), Brushes.Red, e.Bounds.Location);
            //if (s.Length > 1)
            //{
            //    Point newst = e.Bounds.Location;
            //    newst.X = newst.X + (int)e.Graphics.MeasureString(s[0], new Font("Arial", 10f)).Width;
            //    e.Graphics.DrawString(s[1], new Font("Arial", 10f), Brushes.Green, newst);
                //}
        }

        /// <summary>
        /// Charger un script
        /// </summary>
        /// <param name="script">Objet script</param>
        public void LoadScript(VO_Script script)
        {
            if (script != null)
            {
                Script = script;
                Manager.SuspendLayout();
                Manager.Nodes.Clear();
                foreach (IScriptable line in Script.Lines)
                {
                    line.Valid = line.IsScriptValid();
                    List<TreeNode> nodes = line.RenderInScriptManager(string.Empty);
                    foreach(TreeNode node in nodes)
                    {
                        Manager.Nodes.Add(node);
                    }
                }
                Manager.Nodes.Add(string.Empty, "...");
                Manager.ExpandAll();
                Manager.ResumeLayout();
            }
        }

        public void EnableDrawManager()
        {
            Manager.DrawMode = TreeViewDrawMode.OwnerDrawText;
            Manager.DrawNode += new DrawTreeNodeEventHandler(RenderSpecialNode);
        }

        public void DisableDrawManager()
        {
            Manager.DrawNode -= new DrawTreeNodeEventHandler(RenderSpecialNode);
            Manager.DrawMode = TreeViewDrawMode.Normal;
            
        }

        /// <summary>
        /// Retire une ligne dans un script de manière récursive
        /// </summary>
        /// <param name="LineToRemove"></param>
        /// <param name="CurrentList"></param>
        /// <returns></returns>
        public bool RemoveLineInScript(VO_Line LineToRemove, List<VO_Line> CurrentList)
        {
            if (CurrentList.Find(VO_Line => VO_Line == LineToRemove) == null)
            {
                foreach (VO_Line CurrentLine in CurrentList)
                {
                    if (CurrentLine is VO_Script_Loop)
                    {
                        VO_Script_Loop CurrentLoop = CurrentLine as VO_Script_Loop;
                        if (RemoveLineInScript(LineToRemove, CurrentLoop.WhileSubLines) == true)
                            return true;
                    }
                    else if (CurrentLine is VO_Script_Condition)
                    {
                        VO_Script_Condition CurrentCondition = CurrentLine as VO_Script_Condition;
                        if (RemoveLineInScript(LineToRemove, CurrentCondition.ElseSubLines) == true)
                            return true;
                        if (RemoveLineInScript(LineToRemove, CurrentCondition.IfSubLines) == true)
                            return true;
                    }
                    else if (CurrentLine is VO_Script_ChoiceMessage)
                    {
                        VO_Script_ChoiceMessage CurrentChoice = CurrentLine as VO_Script_ChoiceMessage;
                        foreach (VO_LineChoices CurrentLineChoice in CurrentChoice.Choices)
                        {
                            if (RemoveLineInScript(LineToRemove, CurrentLineChoice.SubLines) == true)
                                return true;
                        }
                    }
                }
            }
            else
            {
                CurrentList.Remove(LineToRemove);
                return true;
            }
            return false;
        }
        #endregion

        #region EventHandlers
        #region Actions Manager
        private void ScriptManager_Add(int indexToAdd)
        {
            //Détection des lignes parents
            List<VO_Line> lines = null;
            if (Manager.SelectedNode == null || Manager.SelectedNode.Parent == null)
            {
                lines = Script.Lines;
            }
            else if (Manager.SelectedNode.Parent.Tag is IScriptableContainer)
            {
                lines = ((IScriptableContainer)(Manager.SelectedNode.Parent.Tag)).GetLines(Manager.SelectedNode.Name);
            }

            FormsManager.Instance.EventActions.ScriptType = Script.ScriptType;
            if (FormsManager.Instance.EventActions.ShowDialog() == DialogResult.OK)
            {
                if (indexToAdd == -1)
                    lines.Add(FormsManager.Instance.EventActions.ReturnLine);
                else
                {
                    if (lines.Count < indexToAdd)
                        indexToAdd = lines.Count;
                    lines.Insert(indexToAdd, FormsManager.Instance.EventActions.ReturnLine);
                }
                if (ScriptUpdated != null)
                    this.ScriptUpdated(this, new EventArgs());
                LoadScript(Script);
            }
        }
        #endregion

        //Non à mettre dans la méthode du eventactions        
        private void ScriptManager_Modify(object sender, EventArgs e)
        {
            if (this.Manager.SelectedNode != null)
            {
                Object CurrentNode = this.Manager.SelectedNode.Tag;
                if (CurrentNode is VO_Script_Message)
                {
                    VO_Script_Message line = (VO_Script_Message)CurrentNode;
                    FormsManager.Instance.DialogManager.CurrentDialog = line.Dialog;
                    FormsManager.Instance.DialogManager.CurrentDialog.Messages = line.Dialog.Messages;

                    FormsManager.Instance.DialogManager.LoadDialog(FormsManager.Instance.DialogManager.CurrentDialog, Script.ScriptType);

                    DialogResult Result = FormsManager.Instance.DialogManager.ShowDialog();
                    if (Result == DialogResult.OK)
                    {
                        line.Dialog = FormsManager.Instance.DialogManager.CurrentDialog;
                    }
                }
                else if (CurrentNode is VO_Script_Condition)
                {
                    VO_Script_Condition line = (VO_Script_Condition)CurrentNode;
                    FormsManager.Instance.ScriptCondition.Condition = line;
                    if (FormsManager.Instance.ScriptCondition.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line = FormsManager.Instance.ScriptCondition.Condition;
                    }
                }
                else if (CurrentNode is VO_Script_Loop)
                {
                    VO_Script_Loop line = (VO_Script_Loop)CurrentNode;
                    FormsManager.Instance.ScriptLoop.Loop = line;
                    if (FormsManager.Instance.ScriptLoop.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line = FormsManager.Instance.ScriptLoop.Loop;
                    }
                }
                else if (CurrentNode is VO_Script_ChoiceMessage)
                {
                    VO_Script_ChoiceMessage line = (VO_Script_ChoiceMessage)CurrentNode;
                    FormsManager.Instance.ScriptChoice.ChoiceMessage = line;
                    if (FormsManager.Instance.ScriptChoice.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line = FormsManager.Instance.ScriptChoice.ChoiceMessage;
                    }
                }
                else if (CurrentNode is VO_Script_MovePlayer)
                {
                    VO_Script_MovePlayer line = (VO_Script_MovePlayer)CurrentNode;
                    FormsManager.Instance.CoordsManager.SourceResolution = EditorHelper.Instance.GetCurrentStageInstance().Dimensions;
                    FormsManager.Instance.CoordsManager.SourceObject = new Rectangle(line.Coords.Location, new Size());
                    if (FormsManager.Instance.CoordsManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Coords = FormsManager.Instance.CoordsManager.DestinationObject;
                    }
                }
                else if (CurrentNode is VO_Script_ChangePlayerDirection)
                {
                    VO_Script_ChangePlayerDirection line = (VO_Script_ChangePlayerDirection)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptChangePlayerDirection.Direction = line.Direction;
                    if (FormsManager.Instance.EventActions.ScriptChangePlayerDirection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Direction = FormsManager.Instance.EventActions.ScriptChangePlayerDirection.Direction;
                    }
                }
                else if (CurrentNode is VO_Script_ChangeCharacterDirection)
                {
                    VO_Script_ChangeCharacterDirection line = (VO_Script_ChangeCharacterDirection)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptChangeCharacterDirection.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptChangeCharacterDirection.Direction = line.Direction;
                    FormsManager.Instance.EventActions.ScriptChangeCharacterDirection.CurrentCharacterId = line.CharacterId;
                    if (FormsManager.Instance.EventActions.ScriptChangeCharacterDirection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Direction = FormsManager.Instance.EventActions.ScriptChangeCharacterDirection.Direction;
                        line.CharacterId = FormsManager.Instance.EventActions.ScriptChangeCharacterDirection.CurrentCharacterId;
                    }
                }
                else if (CurrentNode is VO_Script_PlayMusic)
                {
                    VO_Script_PlayMusic line = (VO_Script_PlayMusic)CurrentNode;
                    FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_MUSICS;
                    FormsManager.Instance.ResourcesManager.SelectedFilePath = line.Music;
                    if (FormsManager.Instance.ResourcesManager.ShowDialog() == DialogResult.OK)
                    {
                        line.Music = FormsManager.Instance.ResourcesManager.SelectedFilePath;
                    }
                }
                else if (CurrentNode is VO_Script_AddItem)
                {
                    VO_Script_AddItem line = (VO_Script_AddItem)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptItem.CharacterGuid = line.Character;
                    FormsManager.Instance.EventActions.ScriptItem.ItemGuid = line.Item;
                    FormsManager.Instance.EventActions.ScriptItem.IsAdd = false;
                    if (FormsManager.Instance.EventActions.ScriptItem.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptItem.CharacterGuid;
                        line.Item = FormsManager.Instance.EventActions.ScriptItem.ItemGuid;
                    }
                }
                else if (CurrentNode is VO_Script_RemoveItem)
                {
                    VO_Script_RemoveItem line = (VO_Script_RemoveItem)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptItem.CharacterGuid = line.Character;
                    FormsManager.Instance.EventActions.ScriptItem.ItemGuid = line.Item;
                    FormsManager.Instance.EventActions.ScriptItem.IsAdd = false;
                    if (FormsManager.Instance.EventActions.ScriptItem.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptItem.CharacterGuid;
                        line.Item = FormsManager.Instance.EventActions.ScriptItem.ItemGuid;
                    }
                }
                else if (CurrentNode is VO_Script_ChangeMusicFrequency)
                {
                    VO_Script_ChangeMusicFrequency line = (VO_Script_ChangeMusicFrequency)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptMusicFrequency.Frequency = line.Frequency;
                    FormsManager.Instance.EventActions.ScriptMusicFrequency.IsAdd = false;
                    if (FormsManager.Instance.EventActions.ScriptMusicFrequency.ShowDialog() == DialogResult.OK)
                    {
                        line.Frequency = FormsManager.Instance.EventActions.ScriptMusicFrequency.Frequency;
                    }
                }
                else if (CurrentNode is VO_Script_Wait)
                {
                    VO_Script_Wait line = (VO_Script_Wait)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptWait.WaitTime = line.SecondsToWait;
                    FormsManager.Instance.EventActions.ScriptWait.IsAdd = false;
                    if (FormsManager.Instance.EventActions.ScriptWait.ShowDialog() == DialogResult.OK)
                    {
                        line.SecondsToWait = FormsManager.Instance.EventActions.ScriptWait.WaitTime;
                    }
                }
                else if (CurrentNode is VO_Script_ChangeVariable)
                {
                    VO_Script_ChangeVariable line = (VO_Script_ChangeVariable)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptVariable.CurrentVariable = line;
                    FormsManager.Instance.EventActions.ScriptVariable.IsAdd = false;
                    if (FormsManager.Instance.EventActions.ScriptVariable.ShowDialog() == DialogResult.OK)
                    {
                        line = FormsManager.Instance.EventActions.ScriptVariable.CurrentVariable;
                    }
                }
                else if (CurrentNode is VO_Script_PressSwitch)
                {
                    VO_Script_PressSwitch line = (VO_Script_PressSwitch)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptPressSwitch.Switch = line;
                    FormsManager.Instance.EventActions.ScriptPressSwitch.IsAdd = false;
                    if (FormsManager.Instance.EventActions.ScriptPressSwitch.ShowDialog() == DialogResult.OK)
                    {
                        line.Button = FormsManager.Instance.EventActions.ScriptPressSwitch.Switch.Button;
                        line.IsActive = FormsManager.Instance.EventActions.ScriptPressSwitch.Switch.IsActive;
                    }
                }
                else if (CurrentNode is VO_Script_Random)
                {
                    VO_Script_Random line = (VO_Script_Random)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptRandom.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptRandom.MaxValue = line.MaxValue;
                    FormsManager.Instance.EventActions.ScriptRandom.MinValue = line.MinValue;
                    FormsManager.Instance.EventActions.ScriptRandom.VariableId = line.Variable;
                    if (FormsManager.Instance.EventActions.ScriptRandom.ShowDialog() == DialogResult.OK)
                    {
                        line.MaxValue = FormsManager.Instance.EventActions.ScriptRandom.MaxValue;
                        line.MinValue = FormsManager.Instance.EventActions.ScriptRandom.MinValue;
                        line.Variable = FormsManager.Instance.EventActions.ScriptRandom.VariableId;
                    }
                }
                else if (CurrentNode is VO_Script_PlaySound)
                {
                    VO_Script_PlaySound line = (VO_Script_PlaySound)CurrentNode;
                    FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_EFFECTS;
                    FormsManager.Instance.ResourcesManager.SelectedFilePath = line.Sound;
                    if (FormsManager.Instance.ResourcesManager.ShowDialog() == DialogResult.OK)
                    {
                        line.Sound = FormsManager.Instance.ResourcesManager.SelectedFilePath;
                    }
                }
                else if (CurrentNode is VO_Script_ChangeCurrentCharacter)
                {
                    VO_Script_ChangeCurrentCharacter line = (VO_Script_ChangeCurrentCharacter)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.Character = line.Character;
                    FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.Coords = line.Coords;
                    FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.UseOldCoords = line.UseOldCoords;
                    if (FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.Character;
                        line.Coords = FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.Coords;
                        line.UseOldCoords = FormsManager.Instance.EventActions.ScriptChangeCurrentCharacter.Character.UseOldCoords;
                    }
                }
                else if (CurrentNode is VO_Script_SetAnchor)
                {
                    VO_Script_SetAnchor line = (VO_Script_SetAnchor)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptGetSetAnchor.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptGetSetAnchor.Anchor = line.Anchor;
                    if (FormsManager.Instance.EventActions.ScriptGetSetAnchor.ShowDialog() == DialogResult.OK)
                    {
                        line.Anchor = FormsManager.Instance.EventActions.ScriptGetSetAnchor.Anchor;
                    }
                }
                else if (CurrentNode is VO_Script_GoToAnchor)
                {
                    VO_Script_GoToAnchor line = (VO_Script_GoToAnchor)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptGetSetAnchor.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptGetSetAnchor.Anchor = line.Anchor;
                    if (FormsManager.Instance.EventActions.ScriptGetSetAnchor.ShowDialog() == DialogResult.OK)
                    {
                        line.Anchor = FormsManager.Instance.EventActions.ScriptGetSetAnchor.Anchor;
                    }
                }
                else if (CurrentNode is VO_Script_Teleport)
                {
                    VO_Script_Teleport line = (VO_Script_Teleport)CurrentNode;
                    FormsManager.Instance.CoordsManager.SourceFullObject = new VO_Coords(line.Coords.Location, line.Coords.Map);
                    FormsManager.Instance.CoordsManager.UseStages = true;
                    FormsManager.Instance.CoordsManager.SourceObject = new Rectangle(new Point(), new Size());
                    if (FormsManager.Instance.CoordsManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Coords = FormsManager.Instance.CoordsManager.DestinationObject;
                    }
                }
                else if (CurrentNode is VO_Script_StopCharacterMovements)
                {
                    VO_Script_StopCharacterMovements line = (VO_Script_StopCharacterMovements)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptStopCharacterMovement.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptStopCharacterMovement.CurrentCharacter.Id = line.Character;
                    if (FormsManager.Instance.EventActions.ScriptStopCharacterMovement.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptStopCharacterMovement.CurrentCharacter.Id;
                    }
                }
                else if (CurrentNode is VO_Script_MoveCamera)
                {
                    VO_Script_MoveCamera line = (VO_Script_MoveCamera)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera = new VO_Script_MoveCamera();
                    FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Coords = line.Coords;
                    FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.UseImmediately = line.UseImmediately;
                    FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Speed = line.Speed;
                    if (FormsManager.Instance.EventActions.ScriptMoveCamera.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Coords = FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Coords;
                        line.Speed = FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.Speed;
                        line.UseImmediately = FormsManager.Instance.EventActions.ScriptMoveCamera.MoveCamera.UseImmediately;
                    }
                }
                else if (CurrentNode is VO_Script_MoveCharacter)
                {
                    VO_Script_MoveCharacter line = (VO_Script_MoveCharacter)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter = new VO_Script_MoveCharacter();
                    FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter.Coords = line.Coords;
                    FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter.Character = line.Character;
                    if (FormsManager.Instance.EventActions.ScriptMoveCharacter.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Coords = FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter.Coords;
                        line.Character = FormsManager.Instance.EventActions.ScriptMoveCharacter.MoveCharacter.Character;
                    }
                }
                else if (CurrentNode is VO_Script_FocusOnCharacter)
                {
                    VO_Script_FocusOnCharacter line = (VO_Script_FocusOnCharacter)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter = new VO_Script_FocusOnCharacter();
                    FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.Character = line.Character;
                    FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.UseImmediately = line.UseImmediately;
                    FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.Speed = line.Speed;
                    if (FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.Character;
                        line.Speed = FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.Speed;
                        line.UseImmediately = FormsManager.Instance.EventActions.ScriptCameraFocusOnCharacter.FocusOnCharacter.UseImmediately;
                    }
                }
                else if (CurrentNode is VO_Script_FocusOnAnimation)
                {
                    VO_Script_FocusOnAnimation line = (VO_Script_FocusOnAnimation)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation = new VO_Script_FocusOnAnimation();
                    FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.Animation = line.Animation;
                    FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.UseImmediately = line.UseImmediately;
                    FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.Speed = line.Speed;
                    if (FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Animation = FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.Animation;
                        line.Speed = FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.Speed;
                        line.UseImmediately = FormsManager.Instance.EventActions.ScriptCameraFocusOnAnimation.FocusOnAnimation.UseImmediately;
                    }
                }
                else if (CurrentNode is VO_Script_AddPlayerAction)
                {
                    VO_Script_AddPlayerAction line = (VO_Script_AddPlayerAction)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptAddPlayerAction.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptAddPlayerAction.ActionId = line.ActionId;
                    FormsManager.Instance.EventActions.ScriptAddPlayerAction.CharacterId = line.CharacterId;
                    if (FormsManager.Instance.EventActions.ScriptAddPlayerAction.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.ActionId = FormsManager.Instance.EventActions.ScriptAddPlayerAction.ActionId;
                        line.CharacterId = FormsManager.Instance.EventActions.ScriptAddPlayerAction.CharacterId;
                    }
                }
                else if (CurrentNode is VO_Script_RemovePlayerAction)
                {
                    VO_Script_RemovePlayerAction line = (VO_Script_RemovePlayerAction)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptAddPlayerAction.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptAddPlayerAction.ActionId = line.ActionId;
                    FormsManager.Instance.EventActions.ScriptAddPlayerAction.CharacterId = line.CharacterId;
                    if (FormsManager.Instance.EventActions.ScriptAddPlayerAction.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.ActionId = FormsManager.Instance.EventActions.ScriptAddPlayerAction.ActionId;
                        line.CharacterId = FormsManager.Instance.EventActions.ScriptAddPlayerAction.CharacterId;
                    }
                }
                else if (CurrentNode is VO_Script_CallGlobalEvent)
                {
                    VO_Script_CallGlobalEvent line = (VO_Script_CallGlobalEvent)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptCallGlobalEvent.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptCallGlobalEvent.GlobalEventId = line.GlobalEvent;
                    if (FormsManager.Instance.EventActions.ScriptCallGlobalEvent.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.GlobalEvent = FormsManager.Instance.EventActions.ScriptCallGlobalEvent.GlobalEventId;
                    }
                }
                else if (CurrentNode is VO_Script_ChangePlayerSpeed)
                {
                    VO_Script_ChangePlayerSpeed line = (VO_Script_ChangePlayerSpeed)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptChangePlayerSpeed.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptChangePlayerSpeed.CharacterId = line.CharacterId;
                    FormsManager.Instance.EventActions.ScriptChangePlayerSpeed.Frequency = line.Speed.IntValue;
                    if (FormsManager.Instance.EventActions.ScriptChangePlayerSpeed.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Speed.IntValue = FormsManager.Instance.EventActions.ScriptChangePlayerSpeed.Frequency;
                        line.CharacterId = FormsManager.Instance.EventActions.ScriptChangePlayerSpeed.CharacterId;
                    }
                }
                else if (CurrentNode is VO_Script_Comment)
                {
                    VO_Script_Comment line = (VO_Script_Comment)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptAddComment.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptAddComment.Comment = line.Comment;
                    if (FormsManager.Instance.EventActions.ScriptAddComment.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        line.Comment = FormsManager.Instance.EventActions.ScriptAddComment.Comment;
                    }
                }
                else if (CurrentNode is VO_Script_ChangeHP)
                {
                    VO_Script_ChangeHP line = (VO_Script_ChangeHP)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptChangePlayerHP.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptChangePlayerHP.CharacterId = line.CharacterId;
                    FormsManager.Instance.EventActions.ScriptChangePlayerHP.Operator = line.Operator;
                    FormsManager.Instance.EventActions.ScriptChangePlayerHP.Value = line.Value;
                    if (FormsManager.Instance.EventActions.ScriptChangePlayerHP.ShowDialog() == DialogResult.OK)
                    {
                        line.CharacterId = FormsManager.Instance.EventActions.ScriptChangePlayerHP.CharacterId;
                        line.Operator = FormsManager.Instance.EventActions.ScriptChangePlayerHP.Operator;
                        line.Value = FormsManager.Instance.EventActions.ScriptChangePlayerHP.Value;
                    }
                }
                else if (CurrentNode is VO_Script_ChangeMaxHP)
                {
                    VO_Script_ChangeMaxHP line = (VO_Script_ChangeMaxHP)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptChangePlayerHP.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptChangePlayerHP.CharacterId = line.CharacterId;
                    FormsManager.Instance.EventActions.ScriptChangePlayerHP.Operator = line.Operator;
                    FormsManager.Instance.EventActions.ScriptChangePlayerHP.Value = line.Value;
                    if (FormsManager.Instance.EventActions.ScriptChangePlayerHP.ShowDialog() == DialogResult.OK)
                    {
                        line.CharacterId = FormsManager.Instance.EventActions.ScriptChangePlayerHP.CharacterId;
                        line.Operator = FormsManager.Instance.EventActions.ScriptChangePlayerHP.Operator;
                        line.Value = FormsManager.Instance.EventActions.ScriptChangePlayerHP.Value;
                    }
                }
                else if (CurrentNode is VO_Script_ChangeCharacterAnimFrequency)
                {
                    VO_Script_ChangeCharacterAnimFrequency line = (VO_Script_ChangeCharacterAnimFrequency)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptCharacterAnimationFrequency.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptCharacterAnimationFrequency.CharacterId = line.Character;
                    FormsManager.Instance.EventActions.ScriptCharacterAnimationFrequency.AnimationType = line.AnimationType;
                    FormsManager.Instance.EventActions.ScriptCharacterAnimationFrequency.Frequency = line.Frequency.IntValue;
                    if (FormsManager.Instance.EventActions.ScriptCharacterAnimationFrequency.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptCharacterAnimationFrequency.CharacterId;
                        line.AnimationType = FormsManager.Instance.EventActions.ScriptCharacterAnimationFrequency.AnimationType;
                        line.Frequency.IntValue = FormsManager.Instance.EventActions.ScriptCharacterAnimationFrequency.Frequency;
                    }
                }
                else if (CurrentNode is VO_Script_FreezeCharacterAnimation)
                {
                    VO_Script_FreezeCharacterAnimation line = (VO_Script_FreezeCharacterAnimation)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptFreezeCharacterAnimation.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptFreezeCharacterAnimation.CharacterId = line.Character;
                    FormsManager.Instance.EventActions.ScriptFreezeCharacterAnimation.AnimationType = line.AnimationType;
                    FormsManager.Instance.EventActions.ScriptFreezeCharacterAnimation.AllAnimation = line.FreezeAll;
                    if (FormsManager.Instance.EventActions.ScriptFreezeCharacterAnimation.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptFreezeCharacterAnimation.CharacterId;
                        line.AnimationType = FormsManager.Instance.EventActions.ScriptFreezeCharacterAnimation.AnimationType;
                        line.FreezeAll = FormsManager.Instance.EventActions.ScriptFreezeCharacterAnimation.AllAnimation;
                    }
                }
                else if (CurrentNode is VO_Script_FreeCharacterAnimation)
                {
                    VO_Script_FreeCharacterAnimation line = (VO_Script_FreeCharacterAnimation)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptFreeCharacterAnimation.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptFreeCharacterAnimation.CharacterId = line.Character;
                    FormsManager.Instance.EventActions.ScriptFreeCharacterAnimation.AnimationType = line.AnimationType;
                    FormsManager.Instance.EventActions.ScriptFreeCharacterAnimation.AllAnimation = line.FreeAll;
                    if (FormsManager.Instance.EventActions.ScriptFreeCharacterAnimation.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptFreeCharacterAnimation.CharacterId;
                        line.AnimationType = FormsManager.Instance.EventActions.ScriptFreeCharacterAnimation.AnimationType;
                        line.FreeAll = FormsManager.Instance.EventActions.ScriptFreeCharacterAnimation.AllAnimation;
                    }
                }
                else if (CurrentNode is VO_Script_FreezePlayerAnimation)
                {
                    VO_Script_FreezePlayerAnimation line = (VO_Script_FreezePlayerAnimation)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptFreezePlayerAnimation.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptFreezePlayerAnimation.CharacterId = line.Character;
                    FormsManager.Instance.EventActions.ScriptFreezePlayerAnimation.AnimationType = line.AnimationType;
                    FormsManager.Instance.EventActions.ScriptFreezePlayerAnimation.AllAnimation = line.FreezeAll;
                    if (FormsManager.Instance.EventActions.ScriptFreezePlayerAnimation.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptFreezePlayerAnimation.CharacterId;
                        line.AnimationType = FormsManager.Instance.EventActions.ScriptFreezePlayerAnimation.AnimationType;
                        line.FreezeAll = FormsManager.Instance.EventActions.ScriptFreezePlayerAnimation.AllAnimation;
                    }
                }
                else if (CurrentNode is VO_Script_FreePlayerAnimation)
                {
                    VO_Script_FreePlayerAnimation line = (VO_Script_FreePlayerAnimation)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptFreePlayerAnimation.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptFreePlayerAnimation.CharacterId = line.Character;
                    FormsManager.Instance.EventActions.ScriptFreePlayerAnimation.AnimationType = line.AnimationType;
                    FormsManager.Instance.EventActions.ScriptFreePlayerAnimation.AllAnimation = line.FreeAll;
                    if (FormsManager.Instance.EventActions.ScriptFreePlayerAnimation.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptFreePlayerAnimation.CharacterId;
                        line.AnimationType = FormsManager.Instance.EventActions.ScriptFreePlayerAnimation.AnimationType;
                        line.FreeAll = FormsManager.Instance.EventActions.ScriptFreePlayerAnimation.AllAnimation;
                    }
                }
                else if (CurrentNode is VO_Script_ChangePlayerAnimation)
                {
                    VO_Script_ChangePlayerAnimation line = (VO_Script_ChangePlayerAnimation)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.CharacterId = line.Character;
                    FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.Loop = line.Loop;
                    FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.AnimationType = line.AnimationType;
                    FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.CharacterAnimationType = line.Animation;
                    if (FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.CharacterId;
                        line.AnimationType = FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.AnimationType;
                        line.Animation = FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.CharacterAnimationType;
                        line.Loop = FormsManager.Instance.EventActions.ScriptChangePlayerAnimation.Loop;
                    }
                }
                else if (CurrentNode is VO_Script_LookForwardPlayer)
                {
                    VO_Script_LookForwardPlayer line = (VO_Script_LookForwardPlayer)CurrentNode;
                    FormsManager.Instance.EventActions.ScriptLookForwardPlayer.IsAdd = false;
                    FormsManager.Instance.EventActions.ScriptLookForwardPlayer.CharacterId = line.Character;
                    if (FormsManager.Instance.EventActions.ScriptLookForwardPlayer.ShowDialog() == DialogResult.OK)
                    {
                        line.Character = FormsManager.Instance.EventActions.ScriptLookForwardPlayer.CharacterId;
                    }
                }
                if (ScriptUpdated != null)
                    this.ScriptUpdated(this, new EventArgs());
                LoadScript(Script);
            }
        }

        // Suppresion de la VO_Line courante de VO_Script
        private void ScriptManager_Delete(object sender, EventArgs e)
        {
            if (this.Manager.SelectedNode != null)
            {
                Object CurrentNode = this.Manager.SelectedNode.Tag;
                Manager.Nodes.Remove(this.Manager.SelectedNode);
                RemoveLineInScript((VO_Line)CurrentNode, Script.Lines);
                if (ScriptUpdated != null)
                    this.ScriptUpdated(this, new EventArgs());
                LoadScript(Script);
            }
        }

        // Ajoute d'une VO_Line a la suite de la sélection courante
        private void ScriptManager_AddBelow(object sender, EventArgs e)
        {
            if (Manager.SelectedNode == null)
                ScriptManager_Add(-1);
            else
                ScriptManager_Add(Manager.SelectedNode.Index + 1);
        }

        // Ajoute d'une VO_Line précédent la sélection courante
        private void ScriptManager_AddAbove(object sender, EventArgs e)
        {
            if (Manager.SelectedNode == null)
                ScriptManager_Add(-1);
            else
                ScriptManager_Add(Manager.SelectedNode.Index);
        }

        // Mode Modification par Double Click sur une ligne
        private void ScriptManager_DoubleClickModification(object sender, EventArgs e)
        {
            ScriptManager_Modify(sender, e);
        }

        /// <summary>
        /// Click souris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_MouseClick(object sender, MouseEventArgs e)
        {
            Manager.SelectedNode = Manager.GetNodeAt(e.X, e.Y);
        }
        #endregion
    }
}
