using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006E3 RID: 1763
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(false)]
	public sealed class ManagedToNativeComInteropStubAttribute : Attribute
	{
		// Token: 0x0600405B RID: 16475 RVA: 0x000E1076 File Offset: 0x000DF276
		public ManagedToNativeComInteropStubAttribute(Type classType, string methodName)
		{
			this._classType = classType;
			this._methodName = methodName;
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000E108C File Offset: 0x000DF28C
		public Type ClassType
		{
			get
			{
				return this._classType;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x000E1094 File Offset: 0x000DF294
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x04002A72 RID: 10866
		internal Type _classType;

		// Token: 0x04002A73 RID: 10867
		internal string _methodName;
	}
}
