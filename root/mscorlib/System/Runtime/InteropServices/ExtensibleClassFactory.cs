using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200070D RID: 1805
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x060040B3 RID: 16563 RVA: 0x000025BE File Offset: 0x000007BE
		private ExtensibleClassFactory()
		{
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x000E179B File Offset: 0x000DF99B
		internal static ObjectCreationDelegate GetObjectCreationCallback(Type t)
		{
			return ExtensibleClassFactory.hashtable[t] as ObjectCreationDelegate;
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x000E17B0 File Offset: 0x000DF9B0
		public static void RegisterObjectCreationCallback(ObjectCreationDelegate callback)
		{
			int i = 1;
			StackTrace stackTrace = new StackTrace(false);
			while (i < stackTrace.FrameCount)
			{
				MethodBase method = stackTrace.GetFrame(i).GetMethod();
				if (method.MemberType == MemberTypes.Constructor && method.IsStatic)
				{
					ExtensibleClassFactory.hashtable.Add(method.DeclaringType, callback);
					return;
				}
				i++;
			}
			throw new InvalidOperationException("RegisterObjectCreationCallback must be called from .cctor of class derived from ComImport type.");
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x000E180F File Offset: 0x000DFA0F
		// Note: this type is marked as 'beforefieldinit'.
		static ExtensibleClassFactory()
		{
		}

		// Token: 0x04002B40 RID: 11072
		private static readonly Hashtable hashtable = new Hashtable();
	}
}
