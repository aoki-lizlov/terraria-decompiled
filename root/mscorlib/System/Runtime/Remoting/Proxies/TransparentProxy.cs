using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Mono;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000550 RID: 1360
	[StructLayout(LayoutKind.Sequential)]
	internal class TransparentProxy
	{
		// Token: 0x060036B4 RID: 14004 RVA: 0x000C6788 File Offset: 0x000C4988
		internal RuntimeType GetProxyType()
		{
			return (RuntimeType)Type.GetTypeFromHandle(this._class.ProxyClass.GetTypeHandle());
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060036B5 RID: 14005 RVA: 0x000C67B2 File Offset: 0x000C49B2
		private bool IsContextBoundObject
		{
			get
			{
				return this.GetProxyType().IsContextful;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000C67BF File Offset: 0x000C49BF
		private Context TargetContext
		{
			get
			{
				return this._rp._targetContext;
			}
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000C67CC File Offset: 0x000C49CC
		private bool InCurrentContext()
		{
			return this.IsContextBoundObject && this.TargetContext == Thread.CurrentContext;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000C67E8 File Offset: 0x000C49E8
		internal object LoadRemoteFieldNew(IntPtr classPtr, IntPtr fieldPtr)
		{
			RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(classPtr);
			RuntimeFieldHandle runtimeFieldHandle = new RuntimeFieldHandle(fieldPtr);
			RuntimeTypeHandle typeHandle = runtimeClassHandle.GetTypeHandle();
			FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle(runtimeFieldHandle);
			if (this.InCurrentContext())
			{
				object server = this._rp._server;
				return fieldFromHandle.GetValue(server);
			}
			string fullName = Type.GetTypeFromHandle(typeHandle).FullName;
			string name = fieldFromHandle.Name;
			object[] array = new object[] { fullName, name };
			object[] array2 = new object[1];
			MethodInfo method = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new MissingMethodException("System.Object", "FieldGetter");
			}
			MonoMethodMessage monoMethodMessage = new MonoMethodMessage(method, array, array2);
			Exception ex;
			object[] array3;
			RealProxy.PrivateInvoke(this._rp, monoMethodMessage, out ex, out array3);
			if (ex != null)
			{
				throw ex;
			}
			return array3[0];
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000C68B4 File Offset: 0x000C4AB4
		internal void StoreRemoteField(IntPtr classPtr, IntPtr fieldPtr, object arg)
		{
			RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(classPtr);
			RuntimeFieldHandle runtimeFieldHandle = new RuntimeFieldHandle(fieldPtr);
			RuntimeTypeHandle typeHandle = runtimeClassHandle.GetTypeHandle();
			FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle(runtimeFieldHandle);
			if (this.InCurrentContext())
			{
				object server = this._rp._server;
				fieldFromHandle.SetValue(server, arg);
				return;
			}
			string fullName = Type.GetTypeFromHandle(typeHandle).FullName;
			string name = fieldFromHandle.Name;
			object[] array = new object[] { fullName, name, arg };
			MethodInfo method = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new MissingMethodException("System.Object", "FieldSetter");
			}
			MonoMethodMessage monoMethodMessage = new MonoMethodMessage(method, array, null);
			Exception ex;
			object[] array2;
			RealProxy.PrivateInvoke(this._rp, monoMethodMessage, out ex, out array2);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x000025BE File Offset: 0x000007BE
		public TransparentProxy()
		{
		}

		// Token: 0x040024FC RID: 9468
		public RealProxy _rp;

		// Token: 0x040024FD RID: 9469
		private RuntimeRemoteClassHandle _class;

		// Token: 0x040024FE RID: 9470
		private bool _custom_type_info;
	}
}
