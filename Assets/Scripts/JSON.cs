// murasanca

using System;
using System.IO;
using System.Text;

using UnityEngine;

// murasanca

namespace murasanca
{
    /// <summary>
    /// JavaScript Object Notation
    /// </summary>
    public class JSON
    {
        /// <summary>
        /// Path
        /// </summary>
        private readonly static string p=string.Concat(Application.persistentDataPath,"/l.json");

        /// <summary>
        /// Level
        /// </summary>
        public static int L
        {
            get
            {
                try
                {
                    return JsonUtility.FromJson<L>
                    (
                        Encoding.UTF8.GetString
                        (
                            Convert.FromBase64String
                            (
                                Encoding.UTF8.GetString
                                (
                                    File.ReadAllBytes
                                    (
                                        p
                                    )
                                )
                            )
                        )
                    ).l;
                }
                catch
                {
                    return 0;
                }
            }
            set=>
                File.WriteAllBytes
                (
                    p,
                    Encoding.UTF8.GetBytes
                    (
                        Convert.ToBase64String
                        (
                            Encoding.UTF8.GetBytes
                            (
                                JsonUtility.ToJson
                                (
                                    new L
                                    {
                                        l=value
                                    }
                                )
                            )
                        )
                    )
                );
        }
    }
}

// murasanca