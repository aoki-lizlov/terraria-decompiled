using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000878 RID: 2168
	public class ManifestResourceInfo
	{
		// Token: 0x06004888 RID: 18568 RVA: 0x000EE350 File Offset: 0x000EC550
		public ManifestResourceInfo(Assembly containingAssembly, string containingFileName, ResourceLocation resourceLocation)
		{
			this.ReferencedAssembly = containingAssembly;
			this.FileName = containingFileName;
			this.ResourceLocation = resourceLocation;
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x000EE36D File Offset: 0x000EC56D
		public virtual Assembly ReferencedAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<ReferencedAssembly>k__BackingField;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600488A RID: 18570 RVA: 0x000EE375 File Offset: 0x000EC575
		public virtual string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x000EE37D File Offset: 0x000EC57D
		public virtual ResourceLocation ResourceLocation
		{
			[CompilerGenerated]
			get
			{
				return this.<ResourceLocation>k__BackingField;
			}
		}

		// Token: 0x04002E37 RID: 11831
		[CompilerGenerated]
		private readonly Assembly <ReferencedAssembly>k__BackingField;

		// Token: 0x04002E38 RID: 11832
		[CompilerGenerated]
		private readonly string <FileName>k__BackingField;

		// Token: 0x04002E39 RID: 11833
		[CompilerGenerated]
		private readonly ResourceLocation <ResourceLocation>k__BackingField;
	}
}
