using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer.Constants
{
    public class ConsoleConstants
    {
        #region Settings
        public const float CONSOLE_PADDING = 14;
        public const float CONSOLE_TEXTBOXSPACE = 22;
        public const float CONSOLE_HEIGHT_PERCENTAGE = 0.75f;
        public const float CONSOLE_TRANSPARENCY = 0.75f;
        #endregion

        #region Commands
        public const string C_HELP = "help";
        public const string C_GETVAR = "getvar";
        public const string C_SETVAR = "setvar";
        public const string C_GETSWITCH = "getswt";
        public const string C_SETSWITCH = "setswt";
        public const string C_SAY = "say";
        public const string C_CHAR = "char";
        public const string C_MOVE = "move";
        public const string C_ADDITEM = "additem";
        public const string C_REMOVEITEM = "rmitem";
        public const string C_TELEPORT = "teleport";
        public const string C_CHANGEPLAYER = "changeplayer";
        #endregion
    }
}
