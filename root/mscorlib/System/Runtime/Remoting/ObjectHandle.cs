using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x02000535 RID: 1333
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class ObjectHandle : MarshalByRefObject, IObjectHandle
	{
		// Token: 0x060035B8 RID: 13752 RVA: 0x000C24E1 File Offset: 0x000C06E1
		public ObjectHandle(object o)
		{
			this._wrapped = o;
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000C24F0 File Offset: 0x000C06F0
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000C24F8 File Offset: 0x000C06F8
		public object Unwrap()
		{
			return this._wrapped;
		}

		// Token: 0x040024B1 RID: 9393
		private object _wrapped;
	}
}
