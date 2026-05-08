using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.WorldBuilding
{
	// Token: 0x0200009A RID: 154
	public class WorldSeedOption_Normal : AWorldGenerationOption
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x004DD86B File Offset: 0x004DBA6B
		protected override string KeyName
		{
			get
			{
				return "Seed_Normal";
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00076333 File Offset: 0x00074533
		public override string ServerConfigName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x004DD872 File Offset: 0x004DBA72
		public WorldSeedOption_Normal()
		{
			base.SpecialSeedNames = new string[0];
			base.SpecialSeedValues = new int[0];
			AWorldGenerationOption.OnOptionStateChanged += this.UpdateDependentState;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x004DD8A3 File Offset: 0x004DBAA3
		private void UpdateDependentState(AWorldGenerationOption changed)
		{
			base.Enabled = WorldGenerationOptions.Options.All((AWorldGenerationOption x) => x == this || !x.Enabled);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x004DD8C4 File Offset: 0x004DBAC4
		protected override void OnEnabledStateChanged()
		{
			if (!base.Enabled)
			{
				return;
			}
			foreach (AWorldGenerationOption aworldGenerationOption in WorldGenerationOptions.Options)
			{
				if (aworldGenerationOption != this)
				{
					aworldGenerationOption.Enabled = false;
				}
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x004DD920 File Offset: 0x004DBB20
		[CompilerGenerated]
		private bool <UpdateDependentState>b__5_0(AWorldGenerationOption x)
		{
			return x == this || !x.Enabled;
		}
	}
}
