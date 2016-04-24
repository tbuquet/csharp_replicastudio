using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.DataLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    public static class DebugConsole
    {
        #region Members
        /// <summary>
        /// Référence au Viewer
        /// </summary>
        private static Viewer _Game;

        /// <summary>
        /// Référence au SpriteBatch
        /// </summary>
        private static SpriteBatch _SpriteBatch;

        /// <summary>
        /// Texture de Background
        /// </summary>
        private static Texture2D _ConsoleBackground;

        /// <summary>
        /// Logs de la console
        /// </summary>
        private static List<VO_String2D> _Logs;

        /// <summary>
        /// Commande courante
        /// </summary>
        private static string _CurrentCommand = string.Empty;

        /// <summary>
        /// Dernières commandes
        /// </summary>
        private static List<string> _LastCommands;

        /// <summary>
        /// Index de dernière commande
        /// </summary>
        private static int _LastCommandsIndex = -1;

        /// <summary>
        /// Référence au service
        /// </summary>
        private static StageService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// La Console est-elle visible?
        /// </summary>
        public static bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Les informations de debug sont elle visibles?
        /// </summary>
        public static bool PermanentInfosVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Mode release (console désactivée)
        /// </summary>
        public static bool ReleaseMode
        {
            get;
            set;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialisation de la Console de debug
        /// </summary>
        public static void InitializeConsole(Viewer viewer, SpriteBatch spriteBatch, StageService service)
        {
            if (_Service == null)
            {
                //Informations permanentes
                _Game = viewer;
                _SpriteBatch = spriteBatch;
                _Service = service;
                _Game.GraphicsDevice.BlendState = BlendState.AlphaBlend;

                //Charger console background
                RenderTarget2D texture = new RenderTarget2D(_Game.GraphicsDevice, _Game.GraphicsDevice.PresentationParameters.BackBufferWidth, (int)((float)_Game.GraphicsDevice.PresentationParameters.BackBufferHeight * ConsoleConstants.CONSOLE_HEIGHT_PERCENTAGE));
                _Game.GraphicsDevice.SetRenderTarget(texture);
                _Game.GraphicsDevice.Clear(Color.Black);

                _Game.GraphicsDevice.SetRenderTarget(null);

                _ConsoleBackground = (Texture2D)texture;
            }
        }

        /// <summary>
        /// Draw method
        /// </summary>
        public static void Draw()
        {
            if (Visible && !ReleaseMode)
            {
                _SpriteBatch.Draw(_ConsoleBackground, new Rectangle(0, _Game.GraphicsDevice.PresentationParameters.BackBufferHeight - _ConsoleBackground.Height, _Game.GraphicsDevice.PresentationParameters.BackBufferWidth, _Game.GraphicsDevice.PresentationParameters.BackBufferHeight), Color.White * ConsoleConstants.CONSOLE_TRANSPARENCY);

                if (_CurrentCommand != null)
                {
                    _SpriteBatch.DrawString(FontManager.Debug, _CurrentCommand + "_", new Vector2(2f, (float)_Game.GraphicsDevice.PresentationParameters.BackBufferHeight - 18f), Color.White);
                }

                if (_Logs != null)
                {
                    float messagesWidth = _Game.GraphicsDevice.PresentationParameters.BackBufferHeight - ConsoleConstants.CONSOLE_TEXTBOXSPACE - _Logs.Count * ConsoleConstants.CONSOLE_PADDING;
                    foreach (VO_String2D message in _Logs)
                    {
                        _SpriteBatch.DrawString(FontManager.LogDebug, message.Text, new Vector2(2f, messagesWidth), message.Color);
                        messagesWidth += ConsoleConstants.CONSOLE_PADDING;
                    }
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        public static void Update()
        {
            if (Visible && !ReleaseMode)
            {
                List<Keys> Keys = KeyboardManager.GetPressedKeys();
                foreach (Keys key in Keys)
                {
                    if (key == Microsoft.Xna.Framework.Input.Keys.Up)
                    {
                        if (_LastCommands != null && _LastCommandsIndex < _LastCommands.Count - 1)
                        {
                            _LastCommandsIndex++;
                            _CurrentCommand = _LastCommands[_LastCommandsIndex];
                        }
                    }
                    else if (key == Microsoft.Xna.Framework.Input.Keys.Down)
                    {
                        if (_LastCommands != null && _LastCommandsIndex > 0)
                        {
                            _LastCommandsIndex--;
                            _CurrentCommand = _LastCommands[_LastCommandsIndex];
                        }
                    }
                    else
                    {
                        if ((int)key >= 65 && (int)key <= 90)
                        {
                            _CurrentCommand += key.ToString().ToLower();
                        }
                        else if ((int)key >= 96 && (int)key <= 105)
                        {
                            switch ((int)key)
                            {
                                case 96:
                                    _CurrentCommand += 0;
                                    break;
                                case 97:
                                    _CurrentCommand += 1;
                                    break;
                                case 98:
                                    _CurrentCommand += 2;
                                    break;
                                case 99:
                                    _CurrentCommand += 3;
                                    break;
                                case 100:
                                    _CurrentCommand += 4;
                                    break;
                                case 101:
                                    _CurrentCommand += 5;
                                    break;
                                case 102:
                                    _CurrentCommand += 6;
                                    break;
                                case 103:
                                    _CurrentCommand += 7;
                                    break;
                                case 104:
                                    _CurrentCommand += 8;
                                    break;
                                case 105:
                                    _CurrentCommand += 9;
                                    break;
                            }
                        }
                        else if (key == Microsoft.Xna.Framework.Input.Keys.Space)
                        {
                            _CurrentCommand += " ";
                        }
                        else if (key == Microsoft.Xna.Framework.Input.Keys.Back)
                        {
                            if (_CurrentCommand.Length > 0)
                                _CurrentCommand = _CurrentCommand.Substring(0, _CurrentCommand.Length - 1);
                        }
                        else if (key == Microsoft.Xna.Framework.Input.Keys.Enter)
                        {
                            ExecuteCommand();
                            _CurrentCommand = string.Empty;
                        }
                        _LastCommandsIndex = -1;
                    }
                }
            }
        }

        /// <summary>
        /// Executer une commande
        /// </summary>
        public static void ExecuteCommand()
        {
            if (_CurrentCommand.Length > 0)
            {
                string[] words = _CurrentCommand.Split(" ".ToCharArray());

                if (words.Length > 0)
                {
                    switch (words[0])
                    {
                        case ConsoleConstants.C_HELP:
                            CommandHelp();
                            break;
                        case ConsoleConstants.C_GETVAR:
                            CommandGetVar(words);
                            break;
                        case ConsoleConstants.C_GETSWITCH:
                            CommandGetSwitch(words);
                            break;
                        case ConsoleConstants.C_SETVAR:
                            CommandSetVar(words);
                            break;
                        case ConsoleConstants.C_SETSWITCH:
                            CommandSetSwitch(words);
                            break;
                        case ConsoleConstants.C_SAY:
                            CommandSay(words);
                            break;
                        case ConsoleConstants.C_CHAR:
                            if (words.Length >= 4)
                            {
                                switch (words[2])
                                {
                                    case ConsoleConstants.C_SAY:
                                        CommandCharSay(words);
                                        break;
                                    case ConsoleConstants.C_MOVE:
                                        CommandCharMove(words);
                                        break;
                                    default:
                                        AddConsoleLine(string.Format("command \"{0}\" is incorrect", words[0]));
                                        break;
                                }
                            }
                            break;
                        case ConsoleConstants.C_MOVE:
                            CommandMove(words);
                            break;
                        case ConsoleConstants.C_ADDITEM:
                            CommandAddItem(words);
                            break;
                        case ConsoleConstants.C_REMOVEITEM:
                            CommandRemoveItem(words);
                            break;
                        case ConsoleConstants.C_TELEPORT:
                            CommandTeleport(words);
                            break;
                        case ConsoleConstants.C_CHANGEPLAYER:
                            CommandChangePlayer(words);
                            break;
                        default:
                            AddConsoleLine(string.Format("command \"{0}\" not found", words[0]));
                            break;
                    }
                }

                if (_LastCommands == null)
                    _LastCommands = new List<string>();

                if (!_LastCommands.Contains(_CurrentCommand))
                {
                    _LastCommands.Insert(0, _CurrentCommand);
                    if (_LastCommands.Count > 10)
                        _LastCommands.RemoveAt(10);
                }
            }
        }

        /// <summary>
        /// Ajouter une ligne à la console de commande
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        public static void AddConsoleLine(string message, Color color)
        {
            if (_Logs == null)
                _Logs = new List<VO_String2D>();

            _Logs.Add(new VO_String2D(message, 10, color));

            float newWidth = _Logs.Count * ConsoleConstants.CONSOLE_PADDING + ConsoleConstants.CONSOLE_TEXTBOXSPACE;
            if(newWidth > _ConsoleBackground.Height)
                _Logs.RemoveAt(0);
        }
        public static void AddConsoleLine(string message)
        {
            AddConsoleLine(message, Color.White);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Commande Help
        /// </summary>
        private static void CommandHelp()
        {
            AddConsoleLine("Help Menu");
            AddConsoleLine(string.Format("-{0}: get help", ConsoleConstants.C_HELP));
            AddConsoleLine(string.Format("-{0} [variable]: get variable value", ConsoleConstants.C_GETVAR));
            AddConsoleLine(string.Format("-{0} [switch]: get switch value", ConsoleConstants.C_GETSWITCH));
            AddConsoleLine(string.Format("-{0} [variable] [int value]: set variable value", ConsoleConstants.C_SETVAR));
            AddConsoleLine(string.Format("-{0} [switch] [false|true]: set switch value", ConsoleConstants.C_SETSWITCH));
            AddConsoleLine(string.Format("-{0} [string]: the current player say something", ConsoleConstants.C_SAY));
            AddConsoleLine(string.Format("-{0} [character] {1} [string]: chosen character say something", ConsoleConstants.C_CHAR, ConsoleConstants.C_SAY));
            AddConsoleLine(string.Format("-{0} [x y]: the current player move to position x, y", ConsoleConstants.C_MOVE));
            AddConsoleLine(string.Format("-{0} [character] {1} [x y]: chosen character move to position x, y", ConsoleConstants.C_CHAR, ConsoleConstants.C_MOVE));
            AddConsoleLine(string.Format("-{0} [item]: add item to the current player bag", ConsoleConstants.C_ADDITEM));
            AddConsoleLine(string.Format("-{0} [item]: remove item from the current player bag", ConsoleConstants.C_REMOVEITEM));
            AddConsoleLine(string.Format("-{0} [map x y]: teleport the current player to the mapname, position x, y", ConsoleConstants.C_TELEPORT));
            AddConsoleLine(string.Format("-{0} [character]: change the current player to another character", ConsoleConstants.C_CHANGEPLAYER));
        }

        /// <summary>
        /// Commande GetVar
        /// </summary>
        /// <param name="command"></param>
        private static void CommandGetVar(string[] command)
        {
            if (command.Length != 2)
            {
                AddConsoleLine(string.Format("{0}: command format is incorrect", ConsoleConstants.C_GETVAR));
                return;
            }

            List<VO_Variable> variables = GameState.State.Variables.FindAll(p => p.Title.ToLower() == command[1]);

            if (variables != null)
            {
                if (variables.Count == 0)
                    AddConsoleLine(string.Format("{0}: {1} not found", ConsoleConstants.C_GETVAR, command[1]));
                else
                    foreach (VO_Variable variable in variables)
                        AddConsoleLine(string.Format("{0}: {1} -> {2}", ConsoleConstants.C_GETVAR, variable.Title, variable.Value));
            }
        }

        /// <summary>
        /// Commande GetSwitch
        /// </summary>
        /// <param name="command"></param>
        private static void CommandGetSwitch(string[] command)
        {
            if (command.Length != 2)
            {
                AddConsoleLine(string.Format("{0}: command format is incorrect", ConsoleConstants.C_GETSWITCH));
                return;
            }

            List<VO_Trigger> triggers = GameState.State.Triggers.FindAll(p => p.Title.ToLower() == command[1]);

            if (triggers != null)
            {
                if (triggers == null || triggers.Count == 0)
                    AddConsoleLine(string.Format("{0}: {1} not found", ConsoleConstants.C_GETSWITCH, command[1]));
                else
                    foreach (VO_Trigger trigger in triggers)
                        AddConsoleLine(string.Format("{0}: {1} -> {2}", ConsoleConstants.C_GETSWITCH, trigger.Title, trigger.Value));
            }
        }

        /// <summary>
        /// Commande SetVar
        /// </summary>
        /// <param name="command"></param>
        private static void CommandSetVar(string[] command)
        {
            if (command.Length != 3)
            {
                AddConsoleLine(string.Format("{0}: command format is incorrect", ConsoleConstants.C_SETVAR));
                return;
            }

            VO_Variable variable = GameState.State.Variables.Find(p => p.Title.ToLower() == command[1]);

            if (variable != null)
            {
                try
                {
                    variable.Value = Convert.ToInt32(command[2]);
                    AddConsoleLine(string.Format("{0}: {1} -> {2}", ConsoleConstants.C_SETVAR, variable.Title, variable.Value));
                }
                catch
                {
                    AddConsoleLine(string.Format("{0}: {1} -> {2}", ConsoleConstants.C_SETVAR, variable.Title, "invalid assignement"));
                }
            }
            else
            {
                AddConsoleLine(string.Format("{0}: {1} not found", ConsoleConstants.C_SETVAR, command[1]));
            }
        }

        /// <summary>
        /// Commande SetSwitch
        /// </summary>
        /// <param name="command"></param>
        private static void CommandSetSwitch(string[] command)
        {
            if (command.Length != 3)
            {
                AddConsoleLine(string.Format("{0}: command format is incorrect", ConsoleConstants.C_SETSWITCH));
                return;
            }

            VO_Trigger trigger = GameState.State.Triggers.Find(p => p.Title.ToLower() == command[1]);

            if (trigger != null)
            {
                try
                {
                    trigger.Value = Convert.ToBoolean(command[2]);
                    AddConsoleLine(string.Format("{0}: {1} -> {2}", ConsoleConstants.C_SETSWITCH, trigger.Title, trigger.Value));
                }
                catch
                {
                    AddConsoleLine(string.Format("{0}: {1} -> {2}", ConsoleConstants.C_SETSWITCH, trigger.Title, "invalid assignement"));
                }
            }
            else
            {
                AddConsoleLine(string.Format("{0}: {1} not found", ConsoleConstants.C_SETSWITCH, command[1]));
            }
        }

        /// <summary>
        /// Commande Say
        /// </summary>
        /// <param name="command"></param>
        private static void CommandSay(string[] command)
        {
            if (ScriptManager.CurrentScript == null)
            {
                string message = string.Join(" ", command, 1, command.Length - 1);

                VO_RunningScript runningScript = new VO_RunningScript();
                runningScript.ScriptType = Enums.ScriptType.Events;
                runningScript.Lines = new List<VO_Line>();
                VO_Script_Message messageScript = new VO_Script_Message();
                messageScript.Dialog = new VO_Dialog();
                messageScript.Dialog.Messages = new List<VO_Message>();
                messageScript.Dialog.Messages.Add(new VO_Message() { Character = new Guid(GlobalConstants.CURRENT_PLAYER_ID), Duration = message.Length, FontSize = 14, Text = message });
                runningScript.Lines.Add(messageScript);
                runningScript.CurrentLine = runningScript.Lines[0];
                ScriptManager.CurrentScript = runningScript;
            }
        }

        /// <summary>
        /// Commande CharSay
        /// </summary>
        /// <param name="command"></param>
        private static void CommandCharSay(string[] command)
        {
            if (ScriptManager.CurrentScript == null)
            {
                string message = string.Join(" ", command, 3, command.Length - 3);

                string characterName = command[1];
                VO_Stage currentStage = _Service.GetCurrentStage();
                VO_StageCharacter character = currentStage.ListCharacters.Find(p => p.Title.ToLower() == characterName);

                if (character != null)
                {
                    VO_RunningScript runningScript = new VO_RunningScript();
                    runningScript.ScriptType = Enums.ScriptType.Events;
                    runningScript.Lines = new List<VO_Line>();
                    VO_Script_Message messageScript = new VO_Script_Message();
                    messageScript.Dialog = new VO_Dialog();
                    messageScript.Dialog.Messages = new List<VO_Message>();
                    messageScript.Dialog.Messages.Add(new VO_Message() { Character = character.Id, Duration = message.Length, FontSize = 14, Text = message });
                    runningScript.Lines.Add(messageScript);
                    runningScript.CurrentLine = runningScript.Lines[0];
                    ScriptManager.CurrentScript = runningScript;
                }
                else
                {
                    AddConsoleLine(string.Format("{0}: {1} not found", ConsoleConstants.C_SAY, command[1]));
                }
            }
        }

        /// <summary>
        /// Commande Move
        /// </summary>
        /// <param name="command"></param>
        private static void CommandMove(string[] command)
        {
            if (ScriptManager.CurrentScript == null)
            {
                if (command.Length != 3)
                {
                    AddConsoleLine(string.Format("{0}: coords format is incorrect", ConsoleConstants.C_MOVE));
                }
                else
                {
                    int x = 0;
                    int y = 0;

                    try
                    {
                        x = Convert.ToInt32(command[1]);
                    }
                    catch
                    {
                        AddConsoleLine(string.Format("{0}: coords format is incorrect", ConsoleConstants.C_MOVE));
                        return;
                    }
                    try
                    {
                        y = Convert.ToInt32(command[2]);
                    }
                    catch
                    {
                        AddConsoleLine(string.Format("{0}: coords format is incorrect", ConsoleConstants.C_MOVE));
                        return;
                    }

                    VO_RunningScript runningScript = new VO_RunningScript();
                    runningScript.ScriptType = Enums.ScriptType.Events;
                    runningScript.Lines = new List<VO_Line>();
                    VO_Script_MovePlayer movePlayer = new VO_Script_MovePlayer();
                    movePlayer.CanBeInterrupted = true;
                    movePlayer.Coords = new VO_Coords(new System.Drawing.Point(x, y), _Service.GetCurrentStage().Id);
                    runningScript.Lines.Add(movePlayer);
                    runningScript.CurrentLine = runningScript.Lines[0];
                    ScriptManager.CurrentScript = runningScript;
                }
            }
        }

        /// <summary>
        /// Commande CharSay
        /// </summary>
        /// <param name="command"></param>
        private static void CommandCharMove(string[] command)
        {
            if (ScriptManager.CurrentScript == null)
            {
                if (command.Length != 5)
                {
                    AddConsoleLine(string.Format("{0}: coords format is incorrect", ConsoleConstants.C_MOVE));
                }
                else
                {
                    int x = 0;
                    int y = 0;

                    try
                    {
                        x = Convert.ToInt32(command[3]);
                    }
                    catch
                    {
                        AddConsoleLine(string.Format("{0}: coords format is incorrect", ConsoleConstants.C_MOVE));
                        return;
                    }
                    try
                    {
                        y = Convert.ToInt32(command[4]);
                    }
                    catch
                    {
                        AddConsoleLine(string.Format("{0}: coords format is incorrect", ConsoleConstants.C_MOVE));
                        return;
                    }

                    string characterName = command[1];
                    VO_Stage currentStage = _Service.GetCurrentStage();
                    VO_StageCharacter character = currentStage.ListCharacters.Find(p => p.Title.ToLower() == characterName);

                    if (character != null)
                    {
                        VO_RunningScript runningScript = new VO_RunningScript();
                        runningScript.ScriptType = Enums.ScriptType.Events;
                        runningScript.Lines = new List<VO_Line>();
                        VO_Script_MoveCharacter moveCharacter = new VO_Script_MoveCharacter();
                        moveCharacter.Character = character.Id;
                        moveCharacter.Coords = new VO_Coords(new System.Drawing.Point(x, y), _Service.GetCurrentStage().Id);
                        runningScript.Lines.Add(moveCharacter);
                        runningScript.CurrentLine = runningScript.Lines[0];
                        ScriptManager.CurrentScript = runningScript;
                    }
                    else
                    {
                        AddConsoleLine(string.Format("{0}: {1} not found", ConsoleConstants.C_MOVE, command[1]));
                    }
                }
            }
        }

        /// <summary>
        /// Commande AddItem
        /// </summary>
        /// <param name="command"></param>
        private static void CommandAddItem(string[] command)
        {
            if (command.Length != 2)
            {
                AddConsoleLine(string.Format("{0}: command format is incorrect", ConsoleConstants.C_ADDITEM));
                return;
            }

            VO_Item item = GameCore.Instance.Game.Items.Find(p => p.Title.ToLower() == command[1]);

            if (item != null)
            {
                PlayableCharactersManager.CurrentPlayerCharacter.AddItem(item.Id);
                AddConsoleLine(string.Format("{0}: added item {1}", ConsoleConstants.C_ADDITEM, item.Title));
            }
            else
            {
                AddConsoleLine(string.Format("{0}: item {1} not found", ConsoleConstants.C_ADDITEM, command[1]));
            }
        }

        /// <summary>
        /// Commande RemoveItem
        /// </summary>
        /// <param name="command"></param>
        private static void CommandRemoveItem(string[] command)
        {
            if (command.Length != 2)
            {
                AddConsoleLine(string.Format("{0}: command format is incorrect", ConsoleConstants.C_REMOVEITEM));
                return;
            }

            VO_Item item = GameCore.Instance.Game.Items.Find(p => p.Title.ToLower() == command[1]);

            if (item != null)
            {
                PlayableCharactersManager.CurrentPlayerCharacter.RemoveItem(item.Id);
                AddConsoleLine(string.Format("{0}: removed item {1}", ConsoleConstants.C_REMOVEITEM, item.Title));
            }
            else
            {
                AddConsoleLine(string.Format("{0}: item {1} not found", ConsoleConstants.C_REMOVEITEM, command[1]));
            }
        }

        /// <summary>
        /// Commande Teleport
        /// </summary>
        /// <param name="command"></param>
        private static void CommandTeleport(string[] command)
        {
            if (command.Length != 4)
            {
                AddConsoleLine(string.Format("{0}: command format is incorrect", ConsoleConstants.C_TELEPORT));
                return;
            }

            VO_Stage stage = GameCore.Instance.Game.Stages.Find(p => p.Title.ToLower() == command[1]);

            if (stage != null)
            {
                int x = 0;
                int y = 0;

                try
                {
                    x = Convert.ToInt32(command[2]);
                }
                catch
                {
                    AddConsoleLine(string.Format("{0}: coords format is incorrect", ConsoleConstants.C_TELEPORT));
                    return;
                }
                try
                {
                    y = Convert.ToInt32(command[3]);
                }
                catch
                {
                    AddConsoleLine(string.Format("{0}: coords format is incorrect", ConsoleConstants.C_TELEPORT));
                    return;
                }

                VO_RunningScript runningScript = new VO_RunningScript();
                runningScript.ScriptType = Enums.ScriptType.Events;
                runningScript.Lines = new List<VO_Line>();
                VO_Script_Teleport teleportScript = new VO_Script_Teleport();
                teleportScript.Coords = new VO_Coords(new System.Drawing.Point(x, y), stage.Id);
                runningScript.Lines.Add(teleportScript);
                runningScript.CurrentLine = runningScript.Lines[0];
                ScriptManager.CurrentScript = runningScript;
            }
            else
            {
                AddConsoleLine(string.Format("{0}: stage {1} not found", ConsoleConstants.C_TELEPORT, command[1]));
            }
        }

        /// <summary>
        /// Commande ChangePlayer
        /// </summary>
        /// <param name="command"></param>
        private static void CommandChangePlayer(string[] command)
        {
            if (command.Length != 2)
            {
                AddConsoleLine(string.Format("{0}: command format is incorrect", ConsoleConstants.C_CHANGEPLAYER));
                return;
            }

            VO_Character character = GameCore.Instance.Game.Characters.Find(p => p.Title.ToLower() == command[1]);

            if (character != null)
            {
                VO_RunningScript runningScript = new VO_RunningScript();
                runningScript.ScriptType = Enums.ScriptType.Events;
                runningScript.Lines = new List<VO_Line>();
                VO_Script_ChangeCurrentCharacter characterScript = new VO_Script_ChangeCurrentCharacter();
                characterScript.Character = character.Id;
                characterScript.UseOldCoords = true;
                runningScript.Lines.Add(characterScript);
                runningScript.CurrentLine = runningScript.Lines[0];
                ScriptManager.CurrentScript = runningScript;
            }
            else
            {
                AddConsoleLine(string.Format("{0}: character {1} not found", ConsoleConstants.C_CHANGEPLAYER, command[1]));
            }
        }
        #endregion
    }
}