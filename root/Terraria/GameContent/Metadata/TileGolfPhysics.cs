using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x0200028C RID: 652
	public class TileGolfPhysics
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x00553800 File Offset: 0x00551A00
		// (set) Token: 0x0600251C RID: 9500 RVA: 0x00553808 File Offset: 0x00551A08
		[JsonProperty]
		public float DirectImpactDampening
		{
			[CompilerGenerated]
			get
			{
				return this.<DirectImpactDampening>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DirectImpactDampening>k__BackingField = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x00553811 File Offset: 0x00551A11
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x00553819 File Offset: 0x00551A19
		[JsonProperty]
		public float SideImpactDampening
		{
			[CompilerGenerated]
			get
			{
				return this.<SideImpactDampening>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SideImpactDampening>k__BackingField = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x00553822 File Offset: 0x00551A22
		// (set) Token: 0x06002520 RID: 9504 RVA: 0x0055382A File Offset: 0x00551A2A
		[JsonProperty]
		public float ClubImpactDampening
		{
			[CompilerGenerated]
			get
			{
				return this.<ClubImpactDampening>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ClubImpactDampening>k__BackingField = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00553833 File Offset: 0x00551A33
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x0055383B File Offset: 0x00551A3B
		[JsonProperty]
		public float PassThroughDampening
		{
			[CompilerGenerated]
			get
			{
				return this.<PassThroughDampening>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PassThroughDampening>k__BackingField = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00553844 File Offset: 0x00551A44
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x0055384C File Offset: 0x00551A4C
		[JsonProperty]
		public float ImpactDampeningResistanceEfficiency
		{
			[CompilerGenerated]
			get
			{
				return this.<ImpactDampeningResistanceEfficiency>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ImpactDampeningResistanceEfficiency>k__BackingField = value;
			}
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x0000357B File Offset: 0x0000177B
		public TileGolfPhysics()
		{
		}

		// Token: 0x04004F87 RID: 20359
		[CompilerGenerated]
		private float <DirectImpactDampening>k__BackingField;

		// Token: 0x04004F88 RID: 20360
		[CompilerGenerated]
		private float <SideImpactDampening>k__BackingField;

		// Token: 0x04004F89 RID: 20361
		[CompilerGenerated]
		private float <ClubImpactDampening>k__BackingField;

		// Token: 0x04004F8A RID: 20362
		[CompilerGenerated]
		private float <PassThroughDampening>k__BackingField;

		// Token: 0x04004F8B RID: 20363
		[CompilerGenerated]
		private float <ImpactDampeningResistanceEfficiency>k__BackingField;
	}
}
