using System;
using System.Collections.Generic;
using FullSerializer;

namespace SteelSeries.GameSense
{
	// Token: 0x0200003D RID: 61
	[fsObject(Converter = typeof(BindEventLocalizationsConverter))]
	public class BindEventLocalizations
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00002493 File Offset: 0x00000693
		public BindEventLocalizations()
		{
		}

		// Token: 0x0400006E RID: 110
		public Dictionary<string, string> AvailableLocalizations;
	}
}
