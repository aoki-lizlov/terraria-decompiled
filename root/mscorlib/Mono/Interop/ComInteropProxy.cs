using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace Mono.Interop
{
	// Token: 0x02000047 RID: 71
	[StructLayout(LayoutKind.Sequential)]
	internal class ComInteropProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddProxy(IntPtr pItf, ref ComInteropProxy proxy);

		// Token: 0x06000138 RID: 312
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FindProxy(IntPtr pItf, ref ComInteropProxy proxy);

		// Token: 0x06000139 RID: 313 RVA: 0x000052E0 File Offset: 0x000034E0
		private ComInteropProxy(Type t)
			: base(t)
		{
			this.com_object = __ComObject.CreateRCW(t);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000052FC File Offset: 0x000034FC
		private void CacheProxy()
		{
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(this.com_object.IUnknown, ref comInteropProxy);
			if (comInteropProxy == null)
			{
				ComInteropProxy comInteropProxy2 = this;
				ComInteropProxy.AddProxy(this.com_object.IUnknown, ref comInteropProxy2);
				return;
			}
			Interlocked.Increment(ref this.ref_count);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005341 File Offset: 0x00003541
		private ComInteropProxy(IntPtr pUnk)
			: this(pUnk, typeof(__ComObject))
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005354 File Offset: 0x00003554
		internal ComInteropProxy(IntPtr pUnk, Type t)
			: base(t)
		{
			this.com_object = new __ComObject(pUnk, this);
			this.CacheProxy();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005378 File Offset: 0x00003578
		internal static ComInteropProxy GetProxy(IntPtr pItf, Type t)
		{
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pItf, ref iid_IUnknown, out intPtr));
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(intPtr, ref comInteropProxy);
			if (comInteropProxy == null)
			{
				Marshal.Release(intPtr);
				return new ComInteropProxy(intPtr);
			}
			Marshal.Release(intPtr);
			Interlocked.Increment(ref comInteropProxy.ref_count);
			return comInteropProxy;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000053CC File Offset: 0x000035CC
		internal static ComInteropProxy CreateProxy(Type t)
		{
			IntPtr intPtr = __ComObject.CreateIUnknown(t);
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(intPtr, ref comInteropProxy);
			ComInteropProxy comInteropProxy2;
			if (comInteropProxy != null)
			{
				Type type = comInteropProxy.com_object.GetType();
				if (type != t)
				{
					throw new InvalidCastException(string.Format("Unable to cast object of type '{0}' to type '{1}'.", type, t));
				}
				comInteropProxy2 = comInteropProxy;
				Marshal.Release(intPtr);
			}
			else
			{
				comInteropProxy2 = new ComInteropProxy(t);
				comInteropProxy2.com_object.Initialize(intPtr, comInteropProxy2);
			}
			return comInteropProxy2;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005434 File Offset: 0x00003634
		public override IMessage Invoke(IMessage msg)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005440 File Offset: 0x00003640
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00005448 File Offset: 0x00003648
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005454 File Offset: 0x00003654
		public bool CanCastTo(Type fromType, object o)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new NotSupportedException("Only RCWs are currently supported");
			}
			return (fromType.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic && !(_ComObject.GetInterface(fromType, false) == IntPtr.Zero);
		}

		// Token: 0x04000D24 RID: 3364
		private __ComObject com_object;

		// Token: 0x04000D25 RID: 3365
		private int ref_count = 1;

		// Token: 0x04000D26 RID: 3366
		private string type_name;
	}
}
