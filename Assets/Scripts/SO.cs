// murasanca

using UnityEngine;

// murasanca

namespace murasanca
{
    /// <summary>
    /// ScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName="SO",menuName="Scriptable Objects/SO")]
    public class SO:ScriptableObject
    {
        /// <summary>
        /// One
        /// </summary>
        public byte o;
        
        public byte[]
            p=new byte[81],  // Puzzle
            s=new byte[81]; // Solution
        
        /// <summary>
        /// Texture
        /// </summary>
        public Texture t;
    }
}

// murasanca