// murasanca

using Steamworks;

using UnityEngine;
using UnityEngine.UI;

// murasanca

namespace murasanca
{
    /// <summary>
    /// GameController
    /// </summary>
    public class GC:MonoBehaviour
    {
        /// <summary>
        /// Level
        /// </summary>
        [HideInInspector]
		public int l;

        /// <summary>
        /// AudioClip
        /// </summary>
        [SerializeField]
        private AudioClip
            fAC,  // Final Round
            gAC; // Game Over

        /// <summary>
        /// EventSystem
        /// </summary>
        [SerializeField]
        private ES eS;

        /// <summary>
        /// GameObject
        /// </summary>
        [SerializeField]
        private GameObject gO;

        /// <summary>
        /// RawImage Heart
        /// </summary>
        [SerializeField]
        private RawImage rIH;

        /// <summary>
        /// AudioClip
        /// </summary>
        [SerializeField]
        private AudioClip[]
            dAC=new AudioClip[11],  // Dice
            lAC=new AudioClip[2],  // Lose
            wAC=new AudioClip[3]; // Win

        /// <summary>
        /// GameObject
        /// </summary>
        [SerializeField]
        private GameObject[]
            gOL=new GameObject[3],  // Left
            gOR=new GameObject[3]; // Right

        /// <summary>
        /// RawImage
        /// </summary>
        [SerializeField]
        private RawImage[]
            rIL=new RawImage[6],  // Left
            rIR=new RawImage[6]; // Right

        /// <summary>
        /// Sprite
        /// </summary>
        [SerializeField]
        private Sprite[]
            sF=new Sprite[10],    // False
            sH=new Sprite[4],    // Heart
            sL=new Sprite[10],  // Level
            sT=new Sprite[10]; // True

        /// <summary>
        /// ScriptableObject
        /// </summary>
        [SerializeField]
        private SO[]sO=new SO[900];

        /// <summary>
        /// Byte
        /// </summary>
        private byte
            h,  // Heart
            o; // One

        /// <summary>
        /// Float
        /// </summary>
        private float f;

        /// <summary>
        /// Int
        /// </summary>
        private int l2; // 2+l

        /// <summary>
        /// Puzzle
        /// </summary>
        private readonly byte[]p=new byte[81];

        /// <summary>
        /// GameObject
        /// </summary>
        public GameObject
            bLGO,  // Button Left
            bRGO; // Button Right

        // murasanca
        
        private void Awake()=>L(JSON.L);

        // murasanca

        /// <summary>
        /// GameObject
        /// </summary>
        /// <param name="a">ActiveSelf</param>
        private void GO(bool a)
        {
            gO.SetActive(a);

            if(a)
            {
                eS.rI.canvasRenderer.SetAlpha((f=Mathf.PI*(o=sO[l].o))/255);
                eS.rI.canvasRenderer.SetColor(Color.white/255*f);
            }
            else
            {
                eS.rI.canvasRenderer.SetAlpha(1);
                eS.rI.canvasRenderer.SetColor(Color.white);
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="i">Int</param>
        public void D(int i)
        {
            if(p[i]!=sO[JSON.L].s[i])
                eS.i[i].sprite=sT[0];
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="l">Level</param>
        public void L(int l)
        {
            bLGO.SetActive(0!=l);

            this.l=l;
            if(l==JSON.L) // New Game
            {
                bRGO.SetActive(false);

                if(900==l)
                {
                    l=899;
                    GO(false);

                    rIH.CrossFadeAlpha(0,1,true);
                    rIH.texture=sH[0].texture;
                }
                else
                {
                    for(int i = 0;i<81;++i)
                        eS.i[i].sprite=sT[p[i]=sO[JSON.L].p[i]];

                    GO(true);

                    if(899==l) // Final Round
                        eS.A(fAC);

                    rIH.CrossFadeAlpha(1,1,true);
                    rIH.texture=sH[h=3].texture;
                }
            }
            else
            {
                bRGO.SetActive(899>l);

                GO(false);

                rIH.CrossFadeAlpha(0,1,true);
                rIH.texture=sH[0].texture;
            }

            eS.rI.texture=sO[l].t;

            switch((int)Mathf.Log10(l)+1)
            {
                case 1:
                    gOL[0].SetActive(true);    // GameObject Left 0
                    gOL[1].SetActive(false);  // GameObject Left 1
                    gOL[2].SetActive(false); // GameObject Left 2

                    rIL[0].texture=sL[l].texture;
                    break;
                case 2:
                    gOL[0].SetActive(false);   // GameObject Left 0
                    gOL[1].SetActive(true);   // GameObject Left 1
                    gOL[2].SetActive(false); // GameObject Left 2

                    rIL[1].texture=sL[l/10%10].texture;
                    rIL[2].texture=sL[l%10].texture;
                    break;
                case 3:
                    gOL[0].SetActive(false);  // GameObject Left 0
                    gOL[1].SetActive(false); // GameObject Left 1
                    gOL[2].SetActive(true); // GameObject Left 2

                    rIL[3].texture=sL[l/100%10].texture;
                    rIL[4].texture=sL[l/10%10].texture;
                    rIL[5].texture=sL[l%10].texture;
                    break;
            }
            switch((int)Mathf.Log10(l2=2+l)+1)
            {
                case 1:
                    gOR[0].SetActive(true);    // GameObject Right 0
                    gOR[1].SetActive(false);  // GameObject Right 1
                    gOR[2].SetActive(false); // GameObject Right 2

                    rIR[0].texture=sL[l2].texture;
                    break;
                case 2:
                    gOR[0].SetActive(false);   // GameObject Right 0
                    gOR[1].SetActive(true);   // GameObject Right 1
                    gOR[2].SetActive(false); // GameObject Right 2

                    rIR[1].texture=sL[l2/10%10].texture;
                    rIR[2].texture=sL[l2%10].texture;
                    break;
                case 3:
                    gOR[0].SetActive(false);  // GameObject Right 0
                    gOR[1].SetActive(false); // GameObject Right 1
                    gOR[2].SetActive(true); // GameObject Right 2

                    rIR[3].texture=sL[l2/100%10].texture;
                    rIR[4].texture=sL[l2/10%10].texture;
                    rIR[5].texture=sL[l2%10].texture;
                    break;
            }
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="b">Byte</param>
        /// <param name="i">Int</param>
        public void W(byte b,int i)
        {
            if(p[i]!=sO[JSON.L].s[i])
                if((p[i]=b)==sO[JSON.L].s[i])
                    if(++o==81) // Win
                    {
                        GO(false);

                        if(++JSON.L==900) // Game Over
                            eS.A(gAC);
                        else
                        {
                            bRGO.SetActive(true);

                            eS.A(wAC[Random.Range(0,3)]);
                        }

                        if(SM.Initialized)
                        {
                            _=SteamUserStats.IndicateAchievementProgress("8",(uint)JSON.L,900);
                            _=SteamUserStats.SetStat("l",JSON.L);
                            switch(JSON.L)
                            {
                                case 1:_=SteamUserStats.SetAchievement("1");break;
                                case 2:_=SteamUserStats.SetAchievement("2");break;
                                case 4:_=SteamUserStats.SetAchievement("4");break;
                                case 10:_=SteamUserStats.SetAchievement("10");break;
                                case 16:_=SteamUserStats.SetAchievement("16");break;
                                case 20:_=SteamUserStats.SetAchievement("20");break;
                                case 30:_=SteamUserStats.SetAchievement("30");break;
                                case 32:_=SteamUserStats.SetAchievement("32");break;
                                case 40:_=SteamUserStats.SetAchievement("40");break;
                                case 50:_=SteamUserStats.SetAchievement("50");break;
                                case 60:_=SteamUserStats.SetAchievement("60");break;
                                case 64:_=SteamUserStats.SetAchievement("64");break;
                                case 70:_=SteamUserStats.SetAchievement("70");break;
                                case 80:_=SteamUserStats.SetAchievement("80");break;
                                case 90:_=SteamUserStats.SetAchievement("90");break;
                                case 100:_=SteamUserStats.SetAchievement("100");break;
                                case 110:_=SteamUserStats.SetAchievement("110");break;
                                case 120:_=SteamUserStats.SetAchievement("120");break;
                                case 128:_=SteamUserStats.SetAchievement("128");break;
                                case 130:_=SteamUserStats.SetAchievement("130");break;
                                case 140:_=SteamUserStats.SetAchievement("140");break;
                                case 150:_=SteamUserStats.SetAchievement("150");break;
                                case 160:_=SteamUserStats.SetAchievement("160");break;
                                case 170:_=SteamUserStats.SetAchievement("170");break;
                                case 180:_=SteamUserStats.SetAchievement("180");break;
                                case 190:_=SteamUserStats.SetAchievement("190");break;
                                case 200:_=SteamUserStats.SetAchievement("200");break;
                                case 210:_=SteamUserStats.SetAchievement("210");break;
                                case 220:_=SteamUserStats.SetAchievement("220");break;
                                case 230:_=SteamUserStats.SetAchievement("230");break;
                                case 240:_=SteamUserStats.SetAchievement("240");break;
                                case 250:_=SteamUserStats.SetAchievement("250");break;
                                case 256:_=SteamUserStats.SetAchievement("256");break;
                                case 260:_=SteamUserStats.SetAchievement("260");break;
                                case 270:_=SteamUserStats.SetAchievement("270");break;
                                case 280:_=SteamUserStats.SetAchievement("280");break;
                                case 290:_=SteamUserStats.SetAchievement("290");break;
                                case 300:_=SteamUserStats.SetAchievement("300");break;
                                case 310:_=SteamUserStats.SetAchievement("310");break;
                                case 320:_=SteamUserStats.SetAchievement("320");break;
                                case 330:_=SteamUserStats.SetAchievement("330");break;
                                case 340:_=SteamUserStats.SetAchievement("340");break;
                                case 350:_=SteamUserStats.SetAchievement("350");break;
                                case 360:_=SteamUserStats.SetAchievement("360");break;
                                case 370:_=SteamUserStats.SetAchievement("370");break;
                                case 380:_=SteamUserStats.SetAchievement("380");break;
                                case 390:_=SteamUserStats.SetAchievement("390");break;
                                case 400:_=SteamUserStats.SetAchievement("400");break;
                                case 410:_=SteamUserStats.SetAchievement("410");break;
                                case 420:_=SteamUserStats.SetAchievement("420");break;
                                case 430:_=SteamUserStats.SetAchievement("430");break;
                                case 440:_=SteamUserStats.SetAchievement("440");break;
                                case 450:_=SteamUserStats.SetAchievement("450");break;
                                case 460:_=SteamUserStats.SetAchievement("460");break;
                                case 470:_=SteamUserStats.SetAchievement("470");break;
                                case 480:_=SteamUserStats.SetAchievement("480");break;
                                case 490:_=SteamUserStats.SetAchievement("490");break;
                                case 500:_=SteamUserStats.SetAchievement("500");break;
                                case 510:_=SteamUserStats.SetAchievement("510");break;
                                case 512:_=SteamUserStats.SetAchievement("512");break;
                                case 520:_=SteamUserStats.SetAchievement("520");break;
                                case 530:_=SteamUserStats.SetAchievement("530");break;
                                case 540:_=SteamUserStats.SetAchievement("540");break;
                                case 550:_=SteamUserStats.SetAchievement("550");break;
                                case 560:_=SteamUserStats.SetAchievement("560");break;
                                case 570:_=SteamUserStats.SetAchievement("570");break;
                                case 580:_=SteamUserStats.SetAchievement("580");break;
                                case 590:_=SteamUserStats.SetAchievement("590");break;
                                case 600:_=SteamUserStats.SetAchievement("600");break;
                                case 610:_=SteamUserStats.SetAchievement("610");break;
                                case 620:_=SteamUserStats.SetAchievement("620");break;
                                case 630:_=SteamUserStats.SetAchievement("630");break;
                                case 640:_=SteamUserStats.SetAchievement("640");break;
                                case 650:_=SteamUserStats.SetAchievement("650");break;
                                case 660:_=SteamUserStats.SetAchievement("660");break;
                                case 670:_=SteamUserStats.SetAchievement("670");break;
                                case 680:_=SteamUserStats.SetAchievement("680");break;
                                case 690:_=SteamUserStats.SetAchievement("690");break;
                                case 700:_=SteamUserStats.SetAchievement("700");break;
                                case 710:_=SteamUserStats.SetAchievement("710");break;
                                case 720:_=SteamUserStats.SetAchievement("720");break;
                                case 730:_=SteamUserStats.SetAchievement("730");break;
                                case 740:_=SteamUserStats.SetAchievement("740");break;
                                case 750:_=SteamUserStats.SetAchievement("750");break;
                                case 760:_=SteamUserStats.SetAchievement("760");break;
                                case 770:_=SteamUserStats.SetAchievement("770");break;
                                case 780:_=SteamUserStats.SetAchievement("780");break;
                                case 790:_=SteamUserStats.SetAchievement("790");break;
                                case 800:_=SteamUserStats.SetAchievement("800");break;
                                case 810:_=SteamUserStats.SetAchievement("810");break;
                                case 820:_=SteamUserStats.SetAchievement("820");break;
                                case 830:_=SteamUserStats.SetAchievement("830");break;
                                case 840:_=SteamUserStats.SetAchievement("840");break;
                                case 850:_=SteamUserStats.SetAchievement("850");break;
                                case 860:_=SteamUserStats.SetAchievement("860");break;
                                case 870:_=SteamUserStats.SetAchievement("870");break;
                                case 880:_=SteamUserStats.SetAchievement("880");break;
                                case 890:_=SteamUserStats.SetAchievement("890");break;
                                case 900:
                                    _=SteamUserStats.SetAchievement("8");
                                    _=SteamUserStats.SetAchievement("900");
                                    break;
                                default:break;
                            }
                            _=SteamUserStats.StoreStats();
                        }
                    }
                    else // Correct
                    {
                        eS.A(0==Random.Range(0,2)?dAC[0]:dAC[sO[JSON.L].s[i]]);

                        eS.i[i].sprite=sT[b];

                        eS.rI.CrossFadeAlpha((f=Mathf.PI*o)/255,1,true);
                        eS.rI.CrossFadeColor(Color.white/255*f,1,true,false,true);
                    }
                else // Wrong
                {
                    if(--h==0) // Lose
                    {
                        eS.A(lAC[Random.Range(0,2)]);

                        L(JSON.L);
                    }
                    else
                    {
                        eS.A(0==Random.Range(0,2)?dAC[10]:dAC[sO[JSON.L].s[i]]);

                        eS.i[i].sprite=sF[b];

                        rIH.texture=sH[h].texture;
                    }
                }
        }
    }
}

// murasanca