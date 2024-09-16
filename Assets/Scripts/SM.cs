// murasanca

#if!(UNITY_STANDALONE_WIN||UNITY_STANDALONE_LINUX||UNITY_STANDALONE_OSX||STEAMWORKS_WIN||STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

// murasanca

#if!DISABLESTEAMWORKS
using Steamworks;
#endif
using UnityEngine;

// murasanca

namespace murasanca
{
    /// <summary>
    /// SteamManager
    /// </summary>
    [DisallowMultipleComponent]
    public class SM:MonoBehaviour
    {
#if!DISABLESTEAMWORKS
        protected static bool s_EverInitialized=false;

        protected static SM s_instance;
        protected static SM Instance=>s_instance==null?new GameObject("SteamManager").AddComponent<SM>():s_instance;

        protected bool m_bInitialized=false;
        public static bool Initialized=>Instance.m_bInitialized;

        protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

        [AOT.MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
        protected static void SteamAPIDebugTextHook(int nSeverity,System.Text.StringBuilder pchDebugText)=>Debug.LogWarning(pchDebugText);

#if UNITY_2019_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitOnPlayMode()
        {
            s_EverInitialized=false;
            s_instance=null;
        }
#endif

        protected virtual void Awake()
        {
            if(s_instance!=null)
            {
                Destroy(gameObject);
                return;
            }
            s_instance=this;

            if(s_EverInitialized)
                throw new System.Exception("Tried to Initialize the SteamAPI twice in one session!");

            DontDestroyOnLoad(gameObject);

            if(!Packsize.Test())
                Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.",this);

            if(!DllCheck.Test())
                Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.",this);

            try
            {
                if(SteamAPI.RestartAppIfNecessary((AppId_t)3103000)) // AppId_t.Invalid
                {
                    Debug.Log("[Steamworks.NET] Shutting down because RestartAppIfNecessary returned true. Steam will restart the application.");

                    Application.Quit();
                    return;
                }
            }
            catch(System.DllNotFoundException e)
            {
                Debug.LogError("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n"+e,this);

                Application.Quit();
                return;
            }

            m_bInitialized=SteamAPI.Init();
            if(!m_bInitialized)
            {
                Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.",this);

                return;
            }

            s_EverInitialized=true;
        }

        protected virtual void OnEnable()
        {
            if(s_instance==null)
                s_instance=this;

            if(!m_bInitialized)
                return;

            if(m_SteamAPIWarningMessageHook==null)
            {
                m_SteamAPIWarningMessageHook=new SteamAPIWarningMessageHook_t(SteamAPIDebugTextHook);
                SteamClient.SetWarningMessageHook(m_SteamAPIWarningMessageHook);
            }
        }

        protected virtual void OnDestroy()
        {
            if(s_instance!=this)
                return;

            s_instance=null;

            if(!m_bInitialized)
                return;

            SteamAPI.Shutdown();
        }

        protected virtual void Update()
        {
            if(!m_bInitialized)
                return;

            SteamAPI.RunCallbacks();
        }
#else
    public static bool Initialized=>false;
#endif // !DISABLESTEAMWORKS
    }
}

// murasanca