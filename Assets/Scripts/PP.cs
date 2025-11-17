// murasanca

namespace murasanca
{
    /// <summary>
    /// PlayerPrefs
    /// </summary>
    public class PP:UnityEngine.PlayerPrefs
    {
        /// <summary>
        /// ActiveSelf
        /// </summary>
        public static bool A
        {
            get=>0==GetInt("a",1)?false:true;
            set
            {
                SetInt("a",value?1:0);
                Save();
            }
        }
    }
}

// murasanca