using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007FA RID: 2042
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		// Token: 0x0600463B RID: 17979 RVA: 0x000E6AFE File Offset: 0x000E4CFE
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.loadHint = loadHintArgument;
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x000E6B0D File Offset: 0x000E4D0D
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002CF7 RID: 11511
		private LoadHint loadHint;
	}
}
