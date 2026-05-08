using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006DD RID: 1757
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComEventInterfaceAttribute : Attribute
	{
		// Token: 0x0600404B RID: 16459 RVA: 0x000E0FB7 File Offset: 0x000DF1B7
		public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
		{
			this._SourceInterface = SourceInterface;
			this._EventProvider = EventProvider;
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x000E0FCD File Offset: 0x000DF1CD
		public Type SourceInterface
		{
			get
			{
				return this._SourceInterface;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x000E0FD5 File Offset: 0x000DF1D5
		public Type EventProvider
		{
			get
			{
				return this._EventProvider;
			}
		}

		// Token: 0x04002A67 RID: 10855
		internal Type _SourceInterface;

		// Token: 0x04002A68 RID: 10856
		internal Type _EventProvider;
	}
}
