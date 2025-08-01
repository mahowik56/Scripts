namespace SharpCompress.Compressor.Deflate
{
	internal sealed class StaticTree
	{
		internal static readonly short[] lengthAndLiteralsTreeCodes;

		internal static readonly short[] distTreeCodes;

		internal static readonly int[] extra_blbits;

		internal static readonly StaticTree Literals;

		internal static readonly StaticTree Distances;

		internal static readonly StaticTree BitLengths;

		internal short[] treeCodes;

		internal int[] extraBits;

		internal int extraBase;

		internal int elems;

		internal int maxLength;

		private StaticTree(short[] treeCodes, int[] extraBits, int extraBase, int elems, int maxLength)
		{
			this.treeCodes = treeCodes;
			this.extraBits = extraBits;
			this.extraBase = extraBase;
			this.elems = elems;
			this.maxLength = maxLength;
		}

		static StaticTree()
		{
			lengthAndLiteralsTreeCodes = new short[576]
			{
				12, 8, 140, 8, 76, 8, 204, 8, 44, 8,
				172, 8, 108, 8, 236, 8, 28, 8, 156, 8,
				92, 8, 220, 8, 60, 8, 188, 8, 124, 8,
				252, 8, 2, 8, 130, 8, 66, 8, 194, 8,
				34, 8, 162, 8, 98, 8, 226, 8, 18, 8,
				146, 8, 82, 8, 210, 8, 50, 8, 178, 8,
				114, 8, 242, 8, 10, 8, 138, 8, 74, 8,
				202, 8, 42, 8, 170, 8, 106, 8, 234, 8,
				26, 8, 154, 8, 90, 8, 218, 8, 58, 8,
				186, 8, 122, 8, 250, 8, 6, 8, 134, 8,
				70, 8, 198, 8, 38, 8, 166, 8, 102, 8,
				230, 8, 22, 8, 150, 8, 86, 8, 214, 8,
				54, 8, 182, 8, 118, 8, 246, 8, 14, 8,
				142, 8, 78, 8, 206, 8, 46, 8, 174, 8,
				110, 8, 238, 8, 30, 8, 158, 8, 94, 8,
				222, 8, 62, 8, 190, 8, 126, 8, 254, 8,
				1, 8, 129, 8, 65, 8, 193, 8, 33, 8,
				161, 8, 97, 8, 225, 8, 17, 8, 145, 8,
				81, 8, 209, 8, 49, 8, 177, 8, 113, 8,
				241, 8, 9, 8, 137, 8, 73, 8, 201, 8,
				41, 8, 169, 8, 105, 8, 233, 8, 25, 8,
				153, 8, 89, 8, 217, 8, 57, 8, 185, 8,
				121, 8, 249, 8, 5, 8, 133, 8, 69, 8,
				197, 8, 37, 8, 165, 8, 101, 8, 229, 8,
				21, 8, 149, 8, 85, 8, 213, 8, 53, 8,
				181, 8, 117, 8, 245, 8, 13, 8, 141, 8,
				77, 8, 205, 8, 45, 8, 173, 8, 109, 8,
				237, 8, 29, 8, 157, 8, 93, 8, 221, 8,
				61, 8, 189, 8, 125, 8, 253, 8, 19, 9,
				275, 9, 147, 9, 403, 9, 83, 9, 339, 9,
				211, 9, 467, 9, 51, 9, 307, 9, 179, 9,
				435, 9, 115, 9, 371, 9, 243, 9, 499, 9,
				11, 9, 267, 9, 139, 9, 395, 9, 75, 9,
				331, 9, 203, 9, 459, 9, 43, 9, 299, 9,
				171, 9, 427, 9, 107, 9, 363, 9, 235, 9,
				491, 9, 27, 9, 283, 9, 155, 9, 411, 9,
				91, 9, 347, 9, 219, 9, 475, 9, 59, 9,
				315, 9, 187, 9, 443, 9, 123, 9, 379, 9,
				251, 9, 507, 9, 7, 9, 263, 9, 135, 9,
				391, 9, 71, 9, 327, 9, 199, 9, 455, 9,
				39, 9, 295, 9, 167, 9, 423, 9, 103, 9,
				359, 9, 231, 9, 487, 9, 23, 9, 279, 9,
				151, 9, 407, 9, 87, 9, 343, 9, 215, 9,
				471, 9, 55, 9, 311, 9, 183, 9, 439, 9,
				119, 9, 375, 9, 247, 9, 503, 9, 15, 9,
				271, 9, 143, 9, 399, 9, 79, 9, 335, 9,
				207, 9, 463, 9, 47, 9, 303, 9, 175, 9,
				431, 9, 111, 9, 367, 9, 239, 9, 495, 9,
				31, 9, 287, 9, 159, 9, 415, 9, 95, 9,
				351, 9, 223, 9, 479, 9, 63, 9, 319, 9,
				191, 9, 447, 9, 127, 9, 383, 9, 255, 9,
				511, 9, 0, 7, 64, 7, 32, 7, 96, 7,
				16, 7, 80, 7, 48, 7, 112, 7, 8, 7,
				72, 7, 40, 7, 104, 7, 24, 7, 88, 7,
				56, 7, 120, 7, 4, 7, 68, 7, 36, 7,
				100, 7, 20, 7, 84, 7, 52, 7, 116, 7,
				3, 8, 131, 8, 67, 8, 195, 8, 35, 8,
				163, 8, 99, 8, 227, 8
			};
			distTreeCodes = new short[60]
			{
				0, 5, 16, 5, 8, 5, 24, 5, 4, 5,
				20, 5, 12, 5, 28, 5, 2, 5, 18, 5,
				10, 5, 26, 5, 6, 5, 22, 5, 14, 5,
				30, 5, 1, 5, 17, 5, 9, 5, 25, 5,
				5, 5, 21, 5, 13, 5, 29, 5, 3, 5,
				19, 5, 11, 5, 27, 5, 7, 5, 23, 5
			};
			extra_blbits = new int[19]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 2, 3, 7
			};
			Literals = new StaticTree(lengthAndLiteralsTreeCodes, DeflateManager.ExtraLengthBits, InternalConstants.LITERALS + 1, InternalConstants.L_CODES, InternalConstants.MAX_BITS);
			Distances = new StaticTree(distTreeCodes, DeflateManager.ExtraDistanceBits, 0, InternalConstants.D_CODES, InternalConstants.MAX_BITS);
			BitLengths = new StaticTree(null, extra_blbits, 0, InternalConstants.BL_CODES, InternalConstants.MAX_BL_BITS);
		}
	}
}
