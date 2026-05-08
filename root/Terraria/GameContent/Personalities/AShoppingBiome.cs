using System;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000422 RID: 1058
	public abstract class AShoppingBiome
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x005BA561 File Offset: 0x005B8761
		// (set) Token: 0x0600307A RID: 12410 RVA: 0x005BA569 File Offset: 0x005B8769
		public string NameKey
		{
			[CompilerGenerated]
			get
			{
				return this.<NameKey>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<NameKey>k__BackingField = value;
			}
		}

		// Token: 0x0600307B RID: 12411
		public abstract bool IsInBiome(Player player);

		// Token: 0x0600307C RID: 12412 RVA: 0x0000357B File Offset: 0x0000177B
		protected AShoppingBiome()
		{
		}

		// Token: 0x040056DF RID: 22239
		[CompilerGenerated]
		private string <NameKey>k__BackingField;
	}
}
