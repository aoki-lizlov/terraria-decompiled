using System;
using System.Runtime.CompilerServices;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000AD RID: 173
	public abstract class GenPass : GenBase
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x004DE02B File Offset: 0x004DC22B
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x004DE033 File Offset: 0x004DC233
		public bool Enabled
		{
			[CompilerGenerated]
			get
			{
				return this.<Enabled>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Enabled>k__BackingField = value;
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x004DE03C File Offset: 0x004DC23C
		public void Disable()
		{
			this.Enabled = false;
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x004DE045 File Offset: 0x004DC245
		internal void Enable()
		{
			this.Enabled = true;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x004DE04E File Offset: 0x004DC24E
		public GenPass(string name, double loadWeight)
		{
			this.Name = name;
			this.Weight = loadWeight;
			this.Enabled = true;
		}

		// Token: 0x0600175C RID: 5980
		protected abstract void ApplyPass(GenerationProgress progress, GameConfiguration configuration);

		// Token: 0x0600175D RID: 5981 RVA: 0x004DE06B File Offset: 0x004DC26B
		public void Apply(GenerationProgress progress, GameConfiguration configuration)
		{
			this.ApplyPass(progress, configuration);
		}

		// Token: 0x040011E4 RID: 4580
		[CompilerGenerated]
		private bool <Enabled>k__BackingField;

		// Token: 0x040011E5 RID: 4581
		public string Name;

		// Token: 0x040011E6 RID: 4582
		public double Weight;
	}
}
