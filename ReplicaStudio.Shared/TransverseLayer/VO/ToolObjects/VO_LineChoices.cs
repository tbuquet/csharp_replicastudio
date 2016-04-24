using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_LineChoices
    {
        #region Properties
        public Guid Id { get; set; }

        public string Choice { get; set; }

        public List<VO_Line> SubLines { get; set; }
        #endregion

        public VO_LineChoices()
        {
        }

        public VO_LineChoices(string message)
        {
            SubLines = new List<VO_Line>();
            Choice = message;
        }

        public VO_LineChoices Clone()
        {
            VO_LineChoices NewLineChoice = (VO_LineChoices)this.MemberwiseClone();
            NewLineChoice.SubLines = new List<VO_Line>();

            foreach (VO_Line CurrentLine in this.SubLines)
            {
                IScriptable ScriptLine = CurrentLine as IScriptable;
                IScriptable NewScriptLine = ScriptLine.Clone();
                NewLineChoice.SubLines.Add(NewScriptLine as VO_Line);
            }
            return NewLineChoice;
        }
    }
}
