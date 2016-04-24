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
    public class VO_Event
    {
        #region Properties
        /// <summary>
        /// Initialization d'une liste VO_Page
        /// </summary>
        public List<VO_Page> PageList
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_Event()
        {
            
        }
        #endregion

        #region Methods

        /*public void Update()
        {
            try
            {
                GameCore.Instance.Events[Id] = (VO_Event)this.MemberwiseClone();
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_UPDATE_VO + " #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }
        public void Delete()
        {
            try
            {
                GameCore.Instance.Events.Remove(Id);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Event #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }*/

        public VO_Event Clone()
        {
            VO_Event v_event = (VO_Event)this.MemberwiseClone();
            v_event.PageList = new List<VO_Page>();
            foreach (VO_Page page in PageList)
            {
                v_event.PageList.Add(page.Clone());
            }
            return v_event;
        }

        #endregion
    }
}
