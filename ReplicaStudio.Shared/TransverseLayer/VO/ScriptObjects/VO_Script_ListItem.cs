using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    public class VO_Script_ListItem
    {
        #region Properties
        public Guid Id { get; set; }
        public string Text { get; set; }
        #endregion

        #region Constructor
        public VO_Script_ListItem(Guid id, string text)
        {
            Id = id;
            Text = text;
        }
        #endregion
    }
}
