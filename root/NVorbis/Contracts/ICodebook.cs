using System;

namespace NVorbis.Contracts
{
	// Token: 0x02000024 RID: 36
	internal interface ICodebook
	{
		// Token: 0x060001A4 RID: 420
		void Init(IPacket packet, IHuffman huffman);

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A5 RID: 421
		int Dimensions { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A6 RID: 422
		int Entries { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A7 RID: 423
		int MapType { get; }

		// Token: 0x17000088 RID: 136
		float this[int entry, int dim] { get; }

		// Token: 0x060001A9 RID: 425
		int DecodeScalar(IPacket packet);
	}
}
