using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    public class VO_ListItem
    {
        #region Properties
        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_ListItem()
        {
        }

        public VO_ListItem(int pId, string pTitle)
        {
            Id = pId;
            Title = pTitle;
        }
        #endregion
    }
}

