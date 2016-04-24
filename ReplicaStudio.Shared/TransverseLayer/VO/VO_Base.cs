using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Base
    {
        #region Constants
        #endregion

        #region Properties
        [ReadOnly(true)]
        [CategoryAttribute("General"), DescriptionAttribute("Id of the object")]
        public Guid Id
        {
            get;
            set;
        }

        [CategoryAttribute ( "General" ), DescriptionAttribute ( "Title of the object" )]
        public string Title
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_Base()
        {
        }

        public VO_Base(Guid pId, string pTitle)
        {
            Id = pId;
            Title = pTitle;
        }
        #endregion

        #region Methods
        public virtual void Delete()
        {
        }
        #endregion
    }
}

