using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer.Constants
{
    /// <summary>
    /// Classe d'énumérations propres au Viewer
    /// </summary>
    public static class ViewerEnums
    {
        public enum ScriptExecutionType
        {
            ParallelProcess,
            CurrentProcess
        }

        /// <summary>
        /// Etat de chargement du jeu
        /// </summary>
        public enum LoadingState
        {
            Unloaded = 0,
            OK = 1,
            UnknownError = 2
        }

        /// <summary>
        /// Type d'icone
        /// </summary>
        public enum TypeIcon
        {
            Inventory = 0,
            Icon = 1,
            ActiveIcon = 2
        }

        /// <summary>
        /// Raison de la fermeture de l'application
        /// </summary>
        public enum LeavingReason
        {
            Normal,
            BadLoading
        }

        /// <summary>
        /// Type d'enregistrement d'image
        /// </summary>
        public enum ImageResourceType
        {
            Permanent,
            Screen
        }

        /// <summary>
        /// Alignement
        /// </summary>
        public enum Alignment
        {
            Left,
            Center,
            Right
        }

        /// <summary>
        /// Type de menu
        /// </summary>
        public enum MenuType
        {
            Back,
            Front
        }

        /// <summary>
        /// Type de screen
        /// </summary>
        public enum ScreenType
        {
            LoadingGame,
            Title,
            Stage,
            Load,
            Save,
            Option,
            GameOver,
            Exit
        }

        /// <summary>
        /// Type de retour de Script
        /// </summary>
        public enum ScriptReturn
        {
            Normal,
            Wait,
            Break,
            While,
            RollBackWhile,
            If,
            Else,
            Choice,
            Abort
        }

        /// <summary>
        /// Types de blocage
        /// </summary>
        public enum BlockType
        {
            Free,
            BlockUserMoves,
            BlockUserControls,
            BlockUserControlsAndHideInterfaces
        }
    }
}
