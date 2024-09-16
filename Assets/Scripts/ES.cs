// murasanca

using UnityEngine;
using UnityEngine.UI;

// murasanca

namespace murasanca
{
    /// <summary>
    /// EventSystem
    /// </summary>
    public class ES:MonoBehaviour
    {
		/// <summary>
		/// AudioClip
		/// </summary>
		[SerializeField]
		private AudioClip
			gAC,  // Go Go Go
			rAC; // Reloading

		/// <summary>
		/// AudioSource
		/// </summary>
		[SerializeField]
		private AudioSource aS;

		/// <summary>
		/// Button
		/// </summary>
		[SerializeField]
		private Button
			e,   // Escape
			l,  // Left
			r; // Right

        /// <summary>
        /// RawImage GameObject
        /// </summary>
        [SerializeField]
		private GameObject rIGO;

        /// <summary>
        /// GameController
        /// </summary>
        [SerializeField]
        private GC gC;

		/// <summary>
		/// AudioClip
		/// </summary>
		[SerializeField]
		private AudioClip[]
			dAC=new AudioClip[4],  // Dice
			eAC=new AudioClip[3]; // Escape

		/// <summary>
		/// One
		/// </summary>
		[SerializeField]
		private Button[]o=new Button[81];

		/// <summary>
		/// RectTransform
		/// </summary>
		[SerializeField]
		private RectTransform
			c,    // Canvas
			gO;  // GameObject

		/// <summary>
		/// Dimension
		/// </summary>
		private float
			d,
			dH,    // Height
			dW,   // Weight
			dX,  // X
			dY; // Y

        /// <summary>
        /// Selected
        /// </summary>
        private int s=81;

        /// <summary>
        /// Highlight
        /// </summary>
        private Image[,]h;

		/// <summary>
		/// RawImage
		/// </summary>
		public RawImage rI;

        /// <summary>
        /// Image
        /// </summary>
        public Image[]i=new Image[81];

        /// <summary>
        /// Byte
        /// </summary>
        private readonly byte[,]b=new byte[81,8]
        {
            {8,4,4,4,0,0,0,0},         // 00
			{6,6,4,3,1,0,0,0},        // 01
			{5,7,4,2,2,0,0,0},       // 02
			{6,6,4,3,1,0,0,0},      // 03
			{5,7,4,2,2,0,0,0},     // 04
			{6,6,4,3,1,0,0,0},    // 05
			{5,7,4,2,2,0,0,0},   // 06
			{6,6,4,3,1,0,0,0},  // 07
			{5,7,4,2,2,0,0,0}, // 08

			{8,3,3,3,1,1,1,0},         // 10
			{5,5,3,3,1,1,1,1},        // 11
			{4,6,3,2,2,1,1,1},       // 12
			{6,5,3,2,2,1,1,0},      // 13
			{5,7,3,2,2,1,0,0},     // 14
			{6,6,3,3,1,1,0,0},    // 15
			{5,7,3,2,2,1,0,0},   // 16
			{6,5,3,2,2,1,1,0},  // 17
			{4,6,3,2,2,1,1,1}, // 18

			{8,2,2,2,2,2,2,0},         // 20
			{5,4,2,2,2,2,2,1},        // 21
			{3,5,2,2,2,2,2,2},       // 22
			{5,4,2,2,2,2,2,1},      // 23
			{4,6,2,2,2,2,1,1},     // 24
			{6,5,2,2,2,2,1,0},    // 25
			{5,7,2,2,2,2,0,0},   // 26
			{6,5,2,2,2,2,1,0},  // 27
			{4,6,2,2,2,2,1,1}, // 28

			{8,3,3,3,1,1,1,0},         // 30
			{6,5,3,2,2,1,1,0},        // 31
			{4,6,3,2,2,1,1,1},       // 32
			{5,5,3,3,1,1,1,1},      // 33
			{4,6,3,2,2,1,1,1},     // 34
			{6,5,3,2,2,1,1,0},    // 35
			{5,7,3,2,2,1,0,0},   // 36
			{6,6,3,3,1,1,0,0},  // 37
			{5,7,3,2,2,1,0,0}, // 38

			{8,2,2,2,2,2,2,0},         // 40
			{6,5,2,2,2,2,1,0},        // 41
			{4,6,2,2,2,2,1,1},       // 42
			{5,4,2,2,2,2,2,1},      // 43
			{3,5,2,2,2,2,2,2},     // 44
			{5,4,2,2,2,2,2,1},    // 45
			{4,6,2,2,2,2,1,1},   // 46
			{6,5,2,2,2,2,1,0},  // 47
			{5,7,2,2,2,2,0,0}, // 48

			{8,3,3,3,1,1,1,0},         // 50
			{6,6,3,3,1,1,0,0},        // 51
			{5,7,3,2,2,1,0,0},       // 52
			{6,5,3,2,2,1,1,0},      // 53
			{4,6,3,2,2,1,1,1},     // 54
			{5,5,3,3,1,1,1,1},    // 55
			{4,6,3,2,2,1,1,1},   // 56
			{6,5,3,2,2,1,1,0},  // 57
			{5,7,3,2,2,1,0,0}, // 58

			{8,2,2,2,2,2,2,0},         // 60
			{6,5,2,2,2,2,1,0},        // 61
			{5,7,2,2,2,2,0,0},       // 62
			{6,5,2,2,2,2,1,0},      // 63
			{4,6,2,2,2,2,1,1},     // 64
			{5,4,2,2,2,2,2,1},    // 65
			{3,5,2,2,2,2,2,2},   // 66
			{5,4,2,2,2,2,2,1},  // 67
			{4,6,2,2,2,2,1,1}, // 68

			{8,3,3,3,1,1,1,0},         // 70
			{6,5,3,2,2,1,1,0},        // 71
			{5,7,3,2,2,1,0,0},       // 72
			{6,6,3,3,1,1,0,0},      // 73
			{5,7,3,2,2,1,0,0},     // 74
			{6,5,3,2,2,1,1,0},    // 75
			{4,6,3,2,2,1,1,1},   // 76
			{5,5,3,3,1,1,1,1},  // 77
			{4,6,3,2,2,1,1,1}, // 78

			{8,2,2,2,2,2,2,0},        // 80
			{5,4,2,2,2,2,2,1},       // 81
			{4,6,2,2,2,2,1,1},      // 82
			{6,5,2,2,2,2,1,0},     // 83
			{5,7,2,2,2,2,0,0},    // 84
			{6,5,2,2,2,2,1,0},   // 85
			{4,6,2,2,2,2,1,1},  // 86
			{5,4,2,2,2,2,2,1}, // 87
			{3,5,2,2,2,2,2,2} // 88
        };

        // murasanca

        private void Awake()=>h=new Image[81,20]
        {
            {i[1],i[2],i[3],i[4],i[5],i[6],i[7],i[8],i[14],i[34],i[46],i[66],i[9],i[27],i[45],i[63],i[10],i[30],i[50],i[70]},          // 00
			{i[0],i[2],i[3],i[7],i[8],i[14],i[4],i[5],i[6],i[9],i[35],i[65],i[10],i[28],i[46],i[64],i[29],i[45],i[71],i[50]},         // 01
			{i[0],i[1],i[3],i[13],i[35],i[4],i[5],i[6],i[7],i[8],i[12],i[28],i[11],i[29],i[47],i[65],i[48],i[64],i[49],i[71]},       // 02
			{i[0],i[1],i[2],i[4],i[5],i[34],i[6],i[7],i[8],i[13],i[27],i[47],i[12],i[30],i[48],i[66],i[11],i[49],i[63],i[70]},      // 03
			{i[0],i[3],i[5],i[33],i[47],i[1],i[2],i[6],i[7],i[8],i[32],i[48],i[13],i[31],i[49],i[67],i[12],i[68],i[11],i[69]},     // 04
			{i[0],i[3],i[4],i[6],i[7],i[46],i[1],i[2],i[8],i[33],i[45],i[67],i[14],i[32],i[50],i[68],i[9],i[31],i[69],i[10]},     // 05
			{i[0],i[5],i[7],i[53],i[67],i[1],i[2],i[3],i[4],i[8],i[52],i[68],i[15],i[33],i[51],i[69],i[16],i[32],i[17],i[31]},   // 06
			{i[0],i[1],i[5],i[6],i[8],i[66],i[2],i[3],i[4],i[15],i[53],i[63],i[16],i[34],i[52],i[70],i[17],i[27],i[51],i[30]},  // 07
			{i[0],i[1],i[7],i[15],i[65],i[2],i[3],i[4],i[5],i[6],i[16],i[64],i[17],i[35],i[53],i[71],i[28],i[52],i[29],i[51]}, // 08

			{i[10],i[11],i[12],i[13],i[14],i[15],i[16],i[17],i[1],i[25],i[75],i[0],i[18],i[72],i[5],i[21],i[79],i[46],i[45],i[50]},        // 10
			{i[9],i[11],i[12],i[16],i[17],i[13],i[14],i[15],i[26],i[74],i[1],i[19],i[73],i[0],i[20],i[80],i[5],i[46],i[45],i[50]},        // 11
			{i[9],i[10],i[12],i[26],i[13],i[14],i[15],i[16],i[17],i[19],i[2],i[20],i[74],i[3],i[73],i[4],i[80],i[47],i[48],i[49]},       // 12
			{i[9],i[10],i[11],i[13],i[14],i[25],i[2],i[15],i[16],i[17],i[18],i[3],i[21],i[75],i[4],i[72],i[47],i[79],i[48],i[49]},      // 13
			{i[2],i[9],i[12],i[14],i[24],i[3],i[10],i[11],i[15],i[16],i[17],i[23],i[4],i[22],i[76],i[47],i[77],i[48],i[78],i[49]},     // 14
			{i[1],i[9],i[12],i[13],i[15],i[16],i[0],i[10],i[11],i[17],i[24],i[76],i[5],i[23],i[77],i[22],i[46],i[78],i[45],i[50]},    // 15
			{i[8],i[9],i[14],i[16],i[76],i[7],i[10],i[11],i[12],i[13],i[17],i[77],i[6],i[24],i[78],i[23],i[53],i[22],i[52],i[51]},   // 16
			{i[9],i[10],i[14],i[15],i[17],i[75],i[8],i[11],i[12],i[13],i[72],i[7],i[25],i[79],i[6],i[18],i[21],i[53],i[52],i[51]},  // 17
			{i[9],i[10],i[16],i[74],i[11],i[12],i[13],i[14],i[15],i[73],i[8],i[26],i[80],i[7],i[19],i[6],i[20],i[53],i[52],i[51]}, // 18

			{i[19],i[20],i[21],i[22],i[23],i[24],i[25],i[26],i[12],i[28],i[9],i[27],i[16],i[32],i[37],i[75],i[36],i[72],i[41],i[79]},          // 20
			{i[18],i[20],i[21],i[25],i[26],i[11],i[22],i[23],i[24],i[10],i[28],i[17],i[27],i[32],i[74],i[37],i[73],i[36],i[80],i[41]},        // 21
			{i[18],i[19],i[21],i[22],i[23],i[24],i[25],i[26],i[11],i[29],i[10],i[30],i[17],i[31],i[38],i[74],i[39],i[73],i[40],i[80]},       // 22
			{i[18],i[19],i[20],i[22],i[23],i[24],i[25],i[26],i[29],i[12],i[30],i[9],i[31],i[16],i[38],i[39],i[75],i[40],i[72],i[79]},       // 23
			{i[18],i[21],i[23],i[29],i[19],i[20],i[24],i[25],i[26],i[30],i[13],i[31],i[14],i[38],i[15],i[39],i[40],i[76],i[77],i[78]},     // 24
			{i[18],i[21],i[22],i[24],i[25],i[28],i[13],i[19],i[20],i[26],i[27],i[14],i[32],i[15],i[37],i[36],i[76],i[41],i[77],i[78]},    // 25
			{i[13],i[18],i[23],i[25],i[35],i[14],i[19],i[20],i[21],i[22],i[26],i[34],i[15],i[33],i[44],i[76],i[43],i[77],i[42],i[78]},   // 26
			{i[12],i[18],i[19],i[23],i[24],i[26],i[9],i[20],i[21],i[22],i[35],i[16],i[34],i[33],i[75],i[44],i[72],i[43],i[79],i[42]},   // 27
			{i[11],i[18],i[19],i[25],i[20],i[21],i[22],i[23],i[24],i[10],i[17],i[35],i[34],i[74],i[33],i[73],i[44],i[80],i[43],i[42]}, // 28

			{i[28],i[29],i[30],i[31],i[32],i[33],i[34],i[35],i[3],i[23],i[37],i[0],i[18],i[36],i[7],i[19],i[41],i[66],i[63],i[70]},         // 30
			{i[23],i[27],i[29],i[30],i[34],i[35],i[2],i[18],i[31],i[32],i[33],i[1],i[19],i[37],i[8],i[36],i[41],i[65],i[64],i[71]},        // 31
			{i[22],i[27],i[28],i[30],i[21],i[31],i[32],i[33],i[34],i[35],i[2],i[20],i[38],i[1],i[39],i[8],i[40],i[65],i[64],i[71]},       // 32
			{i[27],i[28],i[29],i[31],i[32],i[22],i[33],i[34],i[35],i[38],i[3],i[21],i[39],i[0],i[20],i[40],i[7],i[66],i[63],i[70]},      // 33
			{i[27],i[30],i[32],i[38],i[28],i[29],i[33],i[34],i[35],i[39],i[4],i[22],i[40],i[5],i[21],i[6],i[20],i[67],i[68],i[69]},     // 34
			{i[27],i[30],i[31],i[33],i[34],i[37],i[4],i[28],i[29],i[35],i[36],i[5],i[23],i[41],i[6],i[18],i[19],i[67],i[68],i[69]},    // 35
			{i[4],i[27],i[32],i[34],i[44],i[5],i[28],i[29],i[30],i[31],i[35],i[43],i[6],i[24],i[42],i[25],i[67],i[26],i[68],i[69]},   // 36
			{i[3],i[27],i[28],i[32],i[33],i[35],i[0],i[24],i[29],i[30],i[31],i[44],i[7],i[25],i[43],i[26],i[42],i[66],i[63],i[70]},  // 37
			{i[2],i[24],i[27],i[28],i[34],i[1],i[25],i[29],i[30],i[31],i[32],i[33],i[8],i[26],i[44],i[43],i[65],i[42],i[64],i[71]}, // 38

			{i[37],i[38],i[39],i[40],i[41],i[42],i[43],i[44],i[32],i[48],i[27],i[45],i[28],i[52],i[23],i[57],i[18],i[54],i[19],i[61]},         // 40
			{i[32],i[36],i[38],i[39],i[43],i[44],i[27],i[40],i[41],i[42],i[47],i[28],i[46],i[23],i[53],i[18],i[56],i[19],i[55],i[62]},        // 41
			{i[31],i[36],i[37],i[39],i[30],i[40],i[41],i[42],i[43],i[44],i[29],i[47],i[22],i[46],i[21],i[53],i[20],i[56],i[55],i[62]},       // 42
			{i[36],i[37],i[38],i[40],i[41],i[31],i[42],i[43],i[44],i[30],i[48],i[29],i[45],i[22],i[52],i[21],i[57],i[20],i[54],i[61]},      // 43
			{i[36],i[39],i[41],i[37],i[38],i[42],i[43],i[44],i[31],i[49],i[30],i[50],i[29],i[51],i[22],i[58],i[21],i[59],i[20],i[60]},     // 44
			{i[36],i[39],i[40],i[42],i[43],i[37],i[38],i[44],i[49],i[32],i[50],i[27],i[51],i[28],i[58],i[23],i[59],i[18],i[60],i[19]},    // 45
			{i[36],i[41],i[43],i[49],i[37],i[38],i[39],i[40],i[44],i[50],i[33],i[51],i[34],i[58],i[35],i[59],i[24],i[60],i[25],i[26]},   // 46
			{i[36],i[37],i[41],i[42],i[44],i[48],i[33],i[38],i[39],i[40],i[45],i[34],i[52],i[35],i[57],i[24],i[54],i[25],i[61],i[26]},  // 47
			{i[33],i[36],i[37],i[43],i[47],i[34],i[38],i[39],i[40],i[41],i[42],i[46],i[35],i[53],i[24],i[56],i[25],i[55],i[26],i[62]}, // 48

			{i[46],i[47],i[48],i[49],i[50],i[51],i[52],i[53],i[5],i[43],i[57],i[0],i[36],i[54],i[1],i[39],i[61],i[14],i[9],i[10]},          // 50
			{i[5],i[45],i[47],i[48],i[52],i[53],i[0],i[44],i[49],i[50],i[51],i[56],i[1],i[37],i[55],i[14],i[38],i[62],i[9],i[10]},         // 51
			{i[4],i[44],i[45],i[46],i[48],i[3],i[37],i[49],i[50],i[51],i[52],i[53],i[2],i[38],i[56],i[13],i[55],i[12],i[62],i[11]},       // 52
			{i[43],i[45],i[46],i[47],i[49],i[50],i[4],i[36],i[51],i[52],i[53],i[3],i[39],i[57],i[2],i[54],i[13],i[61],i[12],i[11]},      // 53
			{i[42],i[45],i[48],i[50],i[41],i[46],i[47],i[51],i[52],i[53],i[4],i[40],i[58],i[3],i[59],i[2],i[60],i[13],i[12],i[11]},     // 54
			{i[45],i[48],i[49],i[51],i[52],i[42],i[46],i[47],i[53],i[58],i[5],i[41],i[59],i[0],i[40],i[60],i[1],i[14],i[9],i[10]},     // 55
			{i[45],i[50],i[52],i[58],i[46],i[47],i[48],i[49],i[53],i[59],i[6],i[42],i[60],i[7],i[41],i[8],i[40],i[15],i[16],i[17]},   // 56
			{i[45],i[46],i[50],i[51],i[53],i[57],i[6],i[47],i[48],i[49],i[54],i[7],i[43],i[61],i[8],i[36],i[15],i[39],i[16],i[17]},  // 57
			{i[6],i[45],i[46],i[52],i[56],i[7],i[47],i[48],i[49],i[50],i[51],i[55],i[8],i[44],i[62],i[15],i[37],i[16],i[38],i[17]}, // 58

			{i[55],i[56],i[57],i[58],i[59],i[60],i[61],i[62],i[52],i[68],i[45],i[63],i[48],i[64],i[43],i[77],i[36],i[72],i[39],i[73]},         // 60
			{i[54],i[56],i[57],i[61],i[62],i[68],i[53],i[58],i[59],i[60],i[63],i[46],i[64],i[47],i[77],i[44],i[72],i[37],i[73],i[38]},        // 61
			{i[53],i[54],i[55],i[57],i[67],i[46],i[58],i[59],i[60],i[61],i[62],i[66],i[47],i[65],i[44],i[76],i[37],i[75],i[38],i[74]},       // 62
			{i[52],i[54],i[55],i[56],i[58],i[59],i[45],i[60],i[61],i[62],i[67],i[48],i[66],i[43],i[65],i[36],i[76],i[39],i[75],i[74]},      // 63
			{i[51],i[54],i[57],i[59],i[50],i[55],i[56],i[60],i[61],i[62],i[49],i[67],i[42],i[66],i[41],i[65],i[40],i[76],i[75],i[74]},     // 64
			{i[54],i[57],i[58],i[60],i[61],i[51],i[55],i[56],i[62],i[50],i[68],i[49],i[63],i[42],i[64],i[41],i[77],i[40],i[72],i[73]},    // 65
			{i[54],i[59],i[61],i[55],i[56],i[57],i[58],i[62],i[51],i[69],i[50],i[70],i[49],i[71],i[42],i[78],i[41],i[79],i[40],i[80]},   // 66
			{i[54],i[55],i[59],i[60],i[62],i[56],i[57],i[58],i[69],i[52],i[70],i[45],i[71],i[48],i[78],i[43],i[79],i[36],i[80],i[39]},  // 67
			{i[54],i[55],i[61],i[69],i[56],i[57],i[58],i[59],i[60],i[70],i[53],i[71],i[46],i[78],i[47],i[79],i[44],i[80],i[37],i[38]}, // 68

			{i[64],i[65],i[66],i[67],i[68],i[69],i[70],i[71],i[7],i[55],i[77],i[0],i[54],i[72],i[3],i[59],i[73],i[34],i[27],i[30]},         // 70
			{i[63],i[65],i[66],i[70],i[71],i[77],i[8],i[67],i[68],i[69],i[72],i[1],i[55],i[73],i[2],i[54],i[35],i[59],i[28],i[29]},        // 71
			{i[8],i[63],i[64],i[66],i[76],i[1],i[67],i[68],i[69],i[70],i[71],i[75],i[2],i[56],i[74],i[35],i[57],i[28],i[58],i[29]},       // 72
			{i[7],i[63],i[64],i[65],i[67],i[68],i[0],i[56],i[69],i[70],i[71],i[76],i[3],i[57],i[75],i[34],i[58],i[74],i[27],i[30]},      // 73
			{i[6],i[56],i[63],i[66],i[68],i[5],i[57],i[64],i[65],i[69],i[70],i[71],i[4],i[58],i[76],i[33],i[75],i[32],i[74],i[31]},     // 74
			{i[55],i[63],i[66],i[67],i[69],i[70],i[6],i[54],i[64],i[65],i[71],i[5],i[59],i[77],i[4],i[72],i[33],i[73],i[32],i[31]},    // 75
			{i[62],i[63],i[68],i[70],i[61],i[64],i[65],i[66],i[67],i[71],i[6],i[60],i[78],i[5],i[79],i[4],i[80],i[33],i[32],i[31]},   // 76
			{i[63],i[64],i[68],i[69],i[71],i[62],i[65],i[66],i[67],i[78],i[7],i[61],i[79],i[0],i[60],i[80],i[3],i[34],i[27],i[30]},  // 77
			{i[63],i[64],i[70],i[78],i[65],i[66],i[67],i[68],i[69],i[79],i[8],i[62],i[80],i[1],i[61],i[2],i[60],i[35],i[28],i[29]}, // 78

			{i[73],i[74],i[75],i[76],i[77],i[78],i[79],i[80],i[16],i[64],i[9],i[63],i[12],i[68],i[25],i[55],i[18],i[54],i[21],i[59]},         // 80
			{i[72],i[74],i[75],i[79],i[80],i[17],i[76],i[77],i[78],i[10],i[64],i[11],i[63],i[26],i[68],i[19],i[55],i[20],i[54],i[59]},       // 81
			{i[17],i[72],i[73],i[75],i[10],i[76],i[77],i[78],i[79],i[80],i[11],i[65],i[26],i[66],i[19],i[67],i[20],i[56],i[57],i[58]},      // 82
			{i[16],i[72],i[73],i[74],i[76],i[77],i[9],i[65],i[78],i[79],i[80],i[12],i[66],i[25],i[67],i[18],i[56],i[21],i[57],i[58]},      // 83
			{i[15],i[65],i[72],i[75],i[77],i[14],i[66],i[73],i[74],i[78],i[79],i[80],i[13],i[67],i[24],i[56],i[23],i[57],i[22],i[58]},    // 84
			{i[64],i[72],i[75],i[76],i[78],i[79],i[15],i[63],i[73],i[74],i[80],i[14],i[68],i[13],i[55],i[24],i[54],i[23],i[59],i[22]},   // 85
			{i[71],i[72],i[77],i[79],i[70],i[73],i[74],i[75],i[76],i[80],i[15],i[69],i[14],i[62],i[13],i[61],i[24],i[60],i[23],i[22]},  // 86
			{i[72],i[73],i[77],i[78],i[80],i[71],i[74],i[75],i[76],i[16],i[70],i[9],i[69],i[12],i[62],i[25],i[61],i[18],i[60],i[21]},  // 87
			{i[72],i[73],i[79],i[74],i[75],i[76],i[77],i[78],i[17],i[71],i[10],i[70],i[11],i[69],i[26],i[62],i[19],i[61],i[20],i[60]} // 88
        };

        private void OnApplicationFocus(bool f)
        {
			I(f);

			s=81;

			Time.timeScale=f?1:0;
        }
        private void OnApplicationPause(bool p)
        {
			I(!p);

            s=81;

            Time.timeScale=p?0:1;
        }

        private void Update()
        {
			gO.sizeDelta=new(d=Mathf.Min((dX=c.sizeDelta.x)-512,(dY=c.sizeDelta.y)-512),d);

			if(0!=Time.timeScale)
				if(Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P)||Input.GetKeyDown(KeyCode.X))e.onClick.Invoke();
				else if(81==s)
				{
				    if(gC.bLGO.activeSelf&&(0>Input.GetAxis("Mouse ScrollWheel")||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.Home)||Input.GetKeyDown(KeyCode.LeftArrow))||Input.GetKeyDown(KeyCode.PageDown)||Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.S))l.onClick.Invoke();
				    else if(gC.bRGO.activeSelf&&(0<Input.GetAxis("Mouse ScrollWheel")||Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.E)||Input.GetKeyDown(KeyCode.End)||Input.GetKeyDown(KeyCode.PageUp)||Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W)))r.onClick.Invoke();
				}
				else // if(81!=s)
				{
					if(Input.GetKeyDown(KeyCode.Alpha0)||Input.GetKeyDown(KeyCode.Backspace)||Input.GetKeyDown(KeyCode.Clear)||Input.GetKeyDown(KeyCode.Delete)||Input.GetKeyDown(KeyCode.JoystickButton0)||Input.GetKeyDown(KeyCode.Keypad0))gC.D(s);
					else if(Input.GetKeyDown(KeyCode.Alpha1)||Input.GetKeyDown(KeyCode.JoystickButton1)||Input.GetKeyDown(KeyCode.Keypad1))gC.W(1,s);
					else if(Input.GetKeyDown(KeyCode.Alpha2)||Input.GetKeyDown(KeyCode.JoystickButton2)||Input.GetKeyDown(KeyCode.Keypad2))gC.W(2,s);
					else if(Input.GetKeyDown(KeyCode.Alpha3)||Input.GetKeyDown(KeyCode.JoystickButton3)||Input.GetKeyDown(KeyCode.Keypad3))gC.W(3,s);
					else if(Input.GetKeyDown(KeyCode.Alpha4)||Input.GetKeyDown(KeyCode.JoystickButton4)||Input.GetKeyDown(KeyCode.Keypad4))gC.W(4,s);
					else if(Input.GetKeyDown(KeyCode.Alpha5)||Input.GetKeyDown(KeyCode.JoystickButton5)||Input.GetKeyDown(KeyCode.Keypad5))gC.W(5,s);
					else if(Input.GetKeyDown(KeyCode.Alpha6)||Input.GetKeyDown(KeyCode.JoystickButton6)||Input.GetKeyDown(KeyCode.Keypad6))gC.W(6,s);
					else if(Input.GetKeyDown(KeyCode.Alpha7)||Input.GetKeyDown(KeyCode.JoystickButton7)||Input.GetKeyDown(KeyCode.Keypad7))gC.W(7,s);
					else if(Input.GetKeyDown(KeyCode.Alpha8)||Input.GetKeyDown(KeyCode.JoystickButton8)||Input.GetKeyDown(KeyCode.Keypad8))gC.W(8,s);
					else if(Input.GetKeyDown(KeyCode.Alpha9)||Input.GetKeyDown(KeyCode.JoystickButton9)||Input.GetKeyDown(KeyCode.Keypad9))gC.W(9,s);
				}

			rI.uvRect=dX>dY?
				new
				(
					(1-(dW=dX/dY))/2,
					(1-(dH=(dY+512)/dY))/2,
					dW,
					dH
				)
				:
				new
				(
					(1-(dW=(dX+512)/dX))/2,
					(1-(dH=dY/dX))/2,
					dW,
					dH
				);
        }

        // murasanca

        /// <summary>
        /// Interactable
        /// </summary>
        /// <param name="i">Interactable</param>
        private void I(bool i)
        {
            foreach(Button b in o)
                b.interactable=i;
        }

        /// <summary>
        /// Pressed
        /// </summary>
        private void P()
        {
            foreach(Image p in i)
                p.CrossFadeColor(Color.gray,1,true,true);
        }
        
		/// <summary>
		/// Audio
		/// </summary>
		/// <param name="aC">AudioClip</param>
		public void A(AudioClip aC)
		{
			if(!aS.isPlaying)
			{
				aS.clip=aC;
				aS.Play();
			}
        }

        /// <summary>
        /// Button
        /// </summary>
        /// <param name="i">Int</param>
        public void B(int i)
        {
			A(dAC[Random.Range(0,4)]);

			P();
			for(int a=0,c=0;a<8;c+=b[i,a++])
				for(int d=c;d<c+b[i,a];++d)
                    h[i,d].CrossFadeColor(Color.white,a,true,true);

			s=i;
		}

        /// <summary>
        /// Escape
        /// </summary>
        public void E()
        {
			A(eAC[Random.Range(0,3)]);

			N();

            rIGO.SetActive(!rIGO.activeSelf);
        }

        /// <summary>
        /// Left
        /// </summary>
        public void L()
        {
			A(rAC);

            gC.L(gC.l-1);
        }

        /// <summary>
        /// Normal
        /// </summary>
        public void N()
        {
            foreach(Image b in i)
				b.CrossFadeColor(Color.white,1,true,true);

			s=81;
        }

        /// <summary>
        /// Right
        /// </summary>
        public void R()
        {
			A(gAC);

            gC.L(1+gC.l);
        }
    }
}

// murasanca