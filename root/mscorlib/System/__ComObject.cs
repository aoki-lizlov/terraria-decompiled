using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Interop;

namespace System
{
	// Token: 0x0200024A RID: 586
	[StructLayout(LayoutKind.Sequential)]
	internal class __ComObject : MarshalByRefObject
	{
		// Token: 0x06001C50 RID: 7248
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern __ComObject CreateRCW(Type t);

		// Token: 0x06001C51 RID: 7249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseInterfaces();

		// Token: 0x06001C52 RID: 7250 RVA: 0x0006AC24 File Offset: 0x00068E24
		~__ComObject()
		{
			if (this.hash_table != IntPtr.Zero)
			{
				if (this.synchronization_context != null)
				{
					this.synchronization_context.Post(delegate(object state)
					{
						this.ReleaseInterfaces();
					}, this);
				}
				else
				{
					this.ReleaseInterfaces();
				}
			}
			this.proxy = null;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0006AC8C File Offset: 0x00068E8C
		public __ComObject()
		{
			this.Initialize(base.GetType());
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0006ACA0 File Offset: 0x00068EA0
		internal __ComObject(Type t)
		{
			this.Initialize(t);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0006ACB0 File Offset: 0x00068EB0
		internal __ComObject(IntPtr pItf, ComInteropProxy p)
		{
			this.proxy = p;
			this.InitializeApartmentDetails();
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pItf, ref iid_IUnknown, out this.iunknown));
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0006ACE9 File Offset: 0x00068EE9
		internal void Initialize(IntPtr pUnk, ComInteropProxy p)
		{
			this.proxy = p;
			this.InitializeApartmentDetails();
			this.iunknown = pUnk;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0006ACFF File Offset: 0x00068EFF
		internal void Initialize(Type t)
		{
			this.InitializeApartmentDetails();
			if (this.iunknown != IntPtr.Zero)
			{
				return;
			}
			this.iunknown = __ComObject.CreateIUnknown(t);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0006AD28 File Offset: 0x00068F28
		internal static IntPtr CreateIUnknown(Type t)
		{
			RuntimeHelpers.RunClassConstructor(t.TypeHandle);
			ObjectCreationDelegate objectCreationCallback = ExtensibleClassFactory.GetObjectCreationCallback(t);
			IntPtr intPtr;
			if (objectCreationCallback != null)
			{
				intPtr = objectCreationCallback(IntPtr.Zero);
				if (intPtr == IntPtr.Zero)
				{
					throw new COMException(string.Format("ObjectCreationDelegate for type {0} failed to return a valid COM object", t));
				}
			}
			else
			{
				Marshal.ThrowExceptionForHR(__ComObject.CoCreateInstance(__ComObject.GetCLSID(t), IntPtr.Zero, 21U, __ComObject.IID_IUnknown, out intPtr));
			}
			return intPtr;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0006AD94 File Offset: 0x00068F94
		private void InitializeApartmentDetails()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				return;
			}
			this.synchronization_context = SynchronizationContext.Current;
			if (this.synchronization_context != null && this.synchronization_context.GetType() == typeof(SynchronizationContext))
			{
				this.synchronization_context = null;
			}
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0006ADE4 File Offset: 0x00068FE4
		private static Guid GetCLSID(Type t)
		{
			if (t.IsImport)
			{
				return t.GUID;
			}
			Type type = t.BaseType;
			while (type != typeof(object))
			{
				if (type.IsImport)
				{
					return type.GUID;
				}
				type = type.BaseType;
			}
			throw new COMException("Could not find base COM type for type " + t.ToString());
		}

		// Token: 0x06001C5B RID: 7259
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetInterfaceInternal(Type t, bool throwException);

		// Token: 0x06001C5C RID: 7260 RVA: 0x0006AE46 File Offset: 0x00069046
		internal IntPtr GetInterface(Type t, bool throwException)
		{
			this.CheckIUnknown();
			return this.GetInterfaceInternal(t, throwException);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0006AE56 File Offset: 0x00069056
		internal IntPtr GetInterface(Type t)
		{
			return this.GetInterface(t, true);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0006AE60 File Offset: 0x00069060
		private void CheckIUnknown()
		{
			if (this.iunknown == IntPtr.Zero)
			{
				throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0006AE7F File Offset: 0x0006907F
		internal IntPtr IUnknown
		{
			get
			{
				if (this.iunknown == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return this.iunknown;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x0006AEA4 File Offset: 0x000690A4
		internal IntPtr IDispatch
		{
			get
			{
				IntPtr @interface = this.GetInterface(typeof(IDispatch));
				if (@interface == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return @interface;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x0006AECE File Offset: 0x000690CE
		internal static Guid IID_IUnknown
		{
			get
			{
				return new Guid("00000000-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0006AEDA File Offset: 0x000690DA
		internal static Guid IID_IDispatch
		{
			get
			{
				return new Guid("00020400-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0006AEE8 File Offset: 0x000690E8
		public override bool Equals(object obj)
		{
			this.CheckIUnknown();
			if (obj == null)
			{
				return false;
			}
			__ComObject _ComObject = obj as __ComObject;
			return _ComObject != null && this.iunknown == _ComObject.IUnknown;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0006AF1D File Offset: 0x0006911D
		public override int GetHashCode()
		{
			this.CheckIUnknown();
			return this.iunknown.ToInt32();
		}

		// Token: 0x06001C65 RID: 7269
		[DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
		private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] [In] Guid rclsid, IntPtr pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, out IntPtr pUnk);

		// Token: 0x06001C66 RID: 7270 RVA: 0x0006AF30 File Offset: 0x00069130
		[CompilerGenerated]
		private void <Finalize>b__6_0(object state)
		{
			this.ReleaseInterfaces();
		}

		// Token: 0x040018C4 RID: 6340
		private IntPtr iunknown;

		// Token: 0x040018C5 RID: 6341
		private IntPtr hash_table;

		// Token: 0x040018C6 RID: 6342
		private SynchronizationContext synchronization_context;

		// Token: 0x040018C7 RID: 6343
		private ComInteropProxy proxy;
	}
}
