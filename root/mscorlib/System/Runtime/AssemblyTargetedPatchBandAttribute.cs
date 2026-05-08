using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x0200051D RID: 1309
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTargetedPatchBandAttribute : Attribute
	{
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000C1979 File Offset: 0x000BFB79
		public string TargetedPatchBand
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetedPatchBand>k__BackingField;
			}
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000C1981 File Offset: 0x000BFB81
		public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
		{
			this.TargetedPatchBand = targetedPatchBand;
		}

		// Token: 0x04002488 RID: 9352
		[CompilerGenerated]
		private readonly string <TargetedPatchBand>k__BackingField;
	}
}
