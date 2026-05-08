using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000806 RID: 2054
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		// Token: 0x0600464B RID: 17995 RVA: 0x000E6B62 File Offset: 0x000E4D62
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600464C RID: 17996 RVA: 0x000E6B78 File Offset: 0x000E4D78
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x000E6B80 File Offset: 0x000E4D80
		// (set) Token: 0x0600464E RID: 17998 RVA: 0x000E6B88 File Offset: 0x000E4D88
		public bool AllInternalsVisible
		{
			get
			{
				return this._allInternalsVisible;
			}
			set
			{
				this._allInternalsVisible = value;
			}
		}

		// Token: 0x04002CFD RID: 11517
		private string _assemblyName;

		// Token: 0x04002CFE RID: 11518
		private bool _allInternalsVisible = true;
	}
}
