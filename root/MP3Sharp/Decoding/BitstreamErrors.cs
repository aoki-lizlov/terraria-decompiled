using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x0200000E RID: 14
	internal struct BitstreamErrors
	{
		// Token: 0x04000041 RID: 65
		internal const int UNKNOWN_ERROR = 256;

		// Token: 0x04000042 RID: 66
		internal const int UNKNOWN_SAMPLE_RATE = 257;

		// Token: 0x04000043 RID: 67
		internal const int STREA_ERROR = 258;

		// Token: 0x04000044 RID: 68
		internal const int UNEXPECTED_EOF = 259;

		// Token: 0x04000045 RID: 69
		internal const int STREA_EOF = 260;

		// Token: 0x04000046 RID: 70
		internal const int BITSTREA_LAST = 511;

		// Token: 0x04000047 RID: 71
		internal const int BITSTREAM_ERROR = 256;

		// Token: 0x04000048 RID: 72
		internal const int DECODER_ERROR = 512;
	}
}
