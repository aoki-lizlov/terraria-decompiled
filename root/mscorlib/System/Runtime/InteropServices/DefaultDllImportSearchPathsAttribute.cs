using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D5 RID: 1749
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
	[ComVisible(false)]
	public sealed class DefaultDllImportSearchPathsAttribute : Attribute
	{
		// Token: 0x06004031 RID: 16433 RVA: 0x000E0C64 File Offset: 0x000DEE64
		public DefaultDllImportSearchPathsAttribute(DllImportSearchPath paths)
		{
			this._paths = paths;
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x000E0C73 File Offset: 0x000DEE73
		public DllImportSearchPath Paths
		{
			get
			{
				return this._paths;
			}
		}

		// Token: 0x04002A52 RID: 10834
		internal DllImportSearchPath _paths;
	}
}
