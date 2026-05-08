using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(64, Inherited = false)]
	public sealed class RgbProcessorAttribute : Attribute
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004A30 File Offset: 0x00002C30
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00004A38 File Offset: 0x00002C38
		public bool IsTransparent
		{
			[CompilerGenerated]
			get
			{
				return this.<IsTransparent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsTransparent>k__BackingField = value;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004A41 File Offset: 0x00002C41
		public RgbProcessorAttribute(params EffectDetailLevel[] detailLevels)
		{
			this.IsTransparent = false;
			this.SupportedDetailLevels = new ReadOnlyCollection<EffectDetailLevel>(Enumerable.ToList<EffectDetailLevel>(Enumerable.Distinct<EffectDetailLevel>(detailLevels)));
		}

		// Token: 0x04000061 RID: 97
		[CompilerGenerated]
		private bool <IsTransparent>k__BackingField;

		// Token: 0x04000062 RID: 98
		public readonly ReadOnlyCollection<EffectDetailLevel> SupportedDetailLevels;
	}
}
