using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x0200028D RID: 653
	public class TileMaterial
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x00553855 File Offset: 0x00551A55
		// (set) Token: 0x06002527 RID: 9511 RVA: 0x0055385D File Offset: 0x00551A5D
		[JsonProperty]
		public TileGolfPhysics GolfPhysics
		{
			[CompilerGenerated]
			get
			{
				return this.<GolfPhysics>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GolfPhysics>k__BackingField = value;
			}
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x0000357B File Offset: 0x0000177B
		public TileMaterial()
		{
		}

		// Token: 0x04004F8C RID: 20364
		[CompilerGenerated]
		private TileGolfPhysics <GolfPhysics>k__BackingField;
	}
}
