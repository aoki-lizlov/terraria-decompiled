using System;
using Terraria.DataStructures;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200032F RID: 815
	public interface IBestiaryEntryFilter : IEntryFilter<BestiaryEntry>
	{
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600280C RID: 10252
		bool? ForcedDisplay { get; }
	}
}
