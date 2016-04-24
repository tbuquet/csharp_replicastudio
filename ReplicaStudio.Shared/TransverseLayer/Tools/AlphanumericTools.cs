using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ReplicaStudio.Shared.TransverseLayer.Tools
{
    public static class AlphanumericTools
    {
        #region Methods
        static public bool IsNumeric(string aString)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(aString); 
        }
        #endregion

        #region EventHandlers
        static public void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }
        #endregion
    }
}
