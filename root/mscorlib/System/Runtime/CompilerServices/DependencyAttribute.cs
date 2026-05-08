using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007FB RID: 2043
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		// Token: 0x0600463D RID: 17981 RVA: 0x000E6B15 File Offset: 0x000E4D15
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x000E6B2B File Offset: 0x000E4D2B
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600463F RID: 17983 RVA: 0x000E6B33 File Offset: 0x000E4D33
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002CF8 RID: 11512
		private string dependentAssembly;

		// Token: 0x04002CF9 RID: 11513
		private LoadHint loadHint;
	}
}
