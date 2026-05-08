using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000768 RID: 1896
	[ComVisible(false)]
	public class NamespaceResolveEventArgs : EventArgs
	{
		// Token: 0x06004472 RID: 17522 RVA: 0x000E4950 File Offset: 0x000E2B50
		public NamespaceResolveEventArgs(string namespaceName, Assembly requestingAssembly)
		{
			this.NamespaceName = namespaceName;
			this.RequestingAssembly = requestingAssembly;
			this.ResolvedAssemblies = new Collection<Assembly>();
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06004473 RID: 17523 RVA: 0x000E4971 File Offset: 0x000E2B71
		// (set) Token: 0x06004474 RID: 17524 RVA: 0x000E4979 File Offset: 0x000E2B79
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

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x000E4982 File Offset: 0x000E2B82
		// (set) Token: 0x06004476 RID: 17526 RVA: 0x000E498A File Offset: 0x000E2B8A
		public Assembly RequestingAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<RequestingAssembly>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RequestingAssembly>k__BackingField = value;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06004477 RID: 17527 RVA: 0x000E4993 File Offset: 0x000E2B93
		// (set) Token: 0x06004478 RID: 17528 RVA: 0x000E499B File Offset: 0x000E2B9B
		public Collection<Assembly> ResolvedAssemblies
		{
			[CompilerGenerated]
			get
			{
				return this.<ResolvedAssemblies>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResolvedAssemblies>k__BackingField = value;
			}
		}

		// Token: 0x04002BBF RID: 11199
		[CompilerGenerated]
		private string <NamespaceName>k__BackingField;

		// Token: 0x04002BC0 RID: 11200
		[CompilerGenerated]
		private Assembly <RequestingAssembly>k__BackingField;

		// Token: 0x04002BC1 RID: 11201
		[CompilerGenerated]
		private Collection<Assembly> <ResolvedAssemblies>k__BackingField;
	}
}
