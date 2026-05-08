using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200004F RID: 79
	internal class Level2Map
	{
		// Token: 0x0600014D RID: 333 RVA: 0x000057D5 File Offset: 0x000039D5
		public Level2Map(byte source, byte replace)
		{
			this.Source = source;
			this.Replace = replace;
		}

		// Token: 0x04000D39 RID: 3385
		public byte Source;

		// Token: 0x04000D3A RID: 3386
		public byte Replace;
	}
}
