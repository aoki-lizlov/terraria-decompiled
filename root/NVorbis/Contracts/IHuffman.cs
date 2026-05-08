using System;
using System.Collections.Generic;

namespace NVorbis.Contracts
{
	// Token: 0x0200002A RID: 42
	internal interface IHuffman
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C5 RID: 453
		int TableBits { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C6 RID: 454
		IList<HuffmanListNode> PrefixTree { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001C7 RID: 455
		IList<HuffmanListNode> OverflowList { get; }

		// Token: 0x060001C8 RID: 456
		void GenerateTable(IList<int> value, int[] lengthList, int[] codeList);
	}
}
