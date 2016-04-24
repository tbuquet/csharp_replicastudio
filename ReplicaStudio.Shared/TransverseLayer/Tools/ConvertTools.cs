using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.Tools
{
    /// <summary>
    /// Classe d'aide de conversion
    /// </summary>
    public static class ConvertTools
    {
        /// <summary>
        /// Cast en Int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int CastInt(object value)
        {
            if (value is int)
                return (int)value;
            if (value is string)
                return Convert.ToInt32(value);
            if (value is decimal)
                return Convert.ToInt32(value);
            if (value is double)
                return Convert.ToInt32(value);
            return 0;
        }

        /// <summary>
        /// Cast en bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CastBool(object value)
        {
            if (value is string)
            {
                if (value.ToString().ToUpper() == "TRUE")
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
