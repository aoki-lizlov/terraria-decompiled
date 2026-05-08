using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000767 RID: 1895
	[ComVisible(false)]
	public class DesignerNamespaceResolveEventArgs : EventArgs
	{
		// Token: 0x0600446D RID: 17517 RVA: 0x000E4914 File Offset: 0x000E2B14
		public DesignerNamespaceResolveEventArgs(string namespaceName)
		{
			this.NamespaceName = namespaceName;
			this.ResolvedAssemblyFiles = new Collection<string>();
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x000E492E File Offset: 0x000E2B2E
		// (set) Token: 0x0600446F RID: 17519 RVA: 0x000E4936 File Offset: 0x000E2B36
		public string NamespaceName
		{
			[CompilerGenerated]
			get
			{
				return this.<NamespaceName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NamespaceName>k__BackingField = value;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x000E493F File Offset: 0x000E2B3F
		// (set) Token: 0x06004471 RID: 17521 RVA: 0x000E4947 File Offset: 0x000E2B47
		public Collection<string> ResolvedAssemblyFiles
		{
			[CompilerGenerated]
			get
			{
				return this.<ResolvedAssemblyFiles>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResolvedAssemblyFiles>k__BackingField = value;
			}
		}

		// Token: 0x04002BBD RID: 11197
		[CompilerGenerated]
		private string <NamespaceName>k__BackingField;

		// Token: 0x04002BBE RID: 11198
		[CompilerGenerated]
		private Collection<string> <ResolvedAssemblyFiles>k__BackingField;
	}
}
