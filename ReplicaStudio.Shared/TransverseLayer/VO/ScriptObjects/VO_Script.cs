using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Script
    {
        #region Properties
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Mauvaises interactions
        /// </summary>
        public List<VO_Line> Lines
        {
            get;
            set;
        }

        /// <summary>
        /// Type de script
        /// </summary>
        public Enums.ScriptType ScriptType { get; set; }
        #endregion

        #region Constructors

        public VO_Script()
        {
        }

        public VO_Script(Guid ID)
        {
            Id = ID;
        }

        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                GameCore.Instance.Game.InteractionScripts.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Script #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Script Clone()
        {
            VO_Script newScript = (VO_Script)this.MemberwiseClone();
            newScript.Id = Guid.NewGuid();
            newScript.Lines = new List<VO_Line>();
            foreach (VO_Line line in this.Lines)
            {
                IScriptable ScriptLine = line as IScriptable;
                VO_Line NewLine = ScriptLine.Clone() as VO_Line;
                newScript.Lines.Add(NewLine);
            }
            return newScript;
        }

        public override string ToString()
        {
            return "...";
        }
        #endregion
    }
}
