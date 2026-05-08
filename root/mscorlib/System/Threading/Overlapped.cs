using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020002C3 RID: 707
	[ComVisible(true)]
	public class Overlapped
	{
		// Token: 0x060020A3 RID: 8355 RVA: 0x000025BE File Offset: 0x000007BE
		public Overlapped()
		{
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000770E9 File Offset: 0x000752E9
		[Obsolete("Not 64bit compatible.  Please use the constructor that takes IntPtr for the event handle")]
		public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar)
		{
			this.offsetL = offsetLo;
			this.offsetH = offsetHi;
			this.evt = hEvent;
			this.ares = ar;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0007710E File Offset: 0x0007530E
		public Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar)
		{
			this.offsetL = offsetLo;
			this.offsetH = offsetHi;
			this.evt_ptr = hEvent;
			this.ares = ar;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00077133 File Offset: 0x00075333
		[CLSCompliant(false)]
		public unsafe static void Free(NativeOverlapped* nativeOverlappedPtr)
		{
			if ((IntPtr)((void*)nativeOverlappedPtr) == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			Marshal.FreeHGlobal((IntPtr)((void*)nativeOverlappedPtr));
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00077160 File Offset: 0x00075360
		[CLSCompliant(false)]
		public unsafe static Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
		{
			if ((IntPtr)((void*)nativeOverlappedPtr) == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			return new Overlapped
			{
				offsetL = nativeOverlappedPtr->OffsetLow,
				offsetH = nativeOverlappedPtr->OffsetHigh,
				evt = (int)nativeOverlappedPtr->EventHandle
			};
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000771B8 File Offset: 0x000753B8
		[CLSCompliant(false)]
		[Obsolete("Use Pack(iocb, userData) instead")]
		[MonoTODO("Security - we need to propagate the call stack")]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb)
		{
			NativeOverlapped* ptr = (NativeOverlapped*)(void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			ptr->OffsetLow = this.offsetL;
			ptr->OffsetHigh = this.offsetH;
			ptr->EventHandle = (IntPtr)this.evt;
			return ptr;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0007720C File Offset: 0x0007540C
		[CLSCompliant(false)]
		[ComVisible(false)]
		[MonoTODO("handle userData")]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			NativeOverlapped* ptr = (NativeOverlapped*)(void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			ptr->OffsetLow = this.offsetL;
			ptr->OffsetHigh = this.offsetH;
			ptr->EventHandle = this.evt_ptr;
			return ptr;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00077258 File Offset: 0x00075458
		[CLSCompliant(false)]
		[Obsolete("Use UnsafePack(iocb, userData) instead")]
		[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb)
		{
			return this.Pack(iocb);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00077261 File Offset: 0x00075461
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			return this.Pack(iocb, userData);
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0007726B File Offset: 0x0007546B
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x00077273 File Offset: 0x00075473
		public IAsyncResult AsyncResult
		{
			get
			{
				return this.ares;
			}
			set
			{
				this.ares = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x0007727C File Offset: 0x0007547C
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x00077284 File Offset: 0x00075484
		[Obsolete("Not 64bit compatible.  Use EventHandleIntPtr instead.")]
		public int EventHandle
		{
			get
			{
				return this.evt;
			}
			set
			{
				this.evt = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0007728D File Offset: 0x0007548D
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x00077295 File Offset: 0x00075495
		[ComVisible(false)]
		public IntPtr EventHandleIntPtr
		{
			get
			{
				return this.evt_ptr;
			}
			set
			{
				this.evt_ptr = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0007729E File Offset: 0x0007549E
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x000772A6 File Offset: 0x000754A6
		public int OffsetHigh
		{
			get
			{
				return this.offsetH;
			}
			set
			{
				this.offsetH = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x000772AF File Offset: 0x000754AF
		// (set) Token: 0x060020B5 RID: 8373 RVA: 0x000772B7 File Offset: 0x000754B7
		public int OffsetLow
		{
			get
			{
				return this.offsetL;
			}
			set
			{
				this.offsetL = value;
			}
		}

		// Token: 0x04001A3D RID: 6717
		private IAsyncResult ares;

		// Token: 0x04001A3E RID: 6718
		private int offsetL;

		// Token: 0x04001A3F RID: 6719
		private int offsetH;

		// Token: 0x04001A40 RID: 6720
		private int evt;

		// Token: 0x04001A41 RID: 6721
		private IntPtr evt_ptr;
	}
}
