using System;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020002C2 RID: 706
	internal static class NativeEventCalls
	{
		// Token: 0x0600209A RID: 8346 RVA: 0x00077004 File Offset: 0x00075204
		public unsafe static IntPtr CreateEvent_internal(bool manual, bool initial, string name, out int errorCode)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return NativeEventCalls.CreateEvent_icall(manual, initial, ptr, (name != null) ? name.Length : 0, out errorCode);
		}

		// Token: 0x0600209B RID: 8347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr CreateEvent_icall(bool manual, bool initial, char* name, int name_length, out int errorCode);

		// Token: 0x0600209C RID: 8348 RVA: 0x00077038 File Offset: 0x00075238
		public static bool SetEvent(SafeWaitHandle handle)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeEventCalls.SetEvent_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x0600209D RID: 8349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetEvent_internal(IntPtr handle);

		// Token: 0x0600209E RID: 8350 RVA: 0x00077078 File Offset: 0x00075278
		public static bool ResetEvent(SafeWaitHandle handle)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeEventCalls.ResetEvent_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x0600209F RID: 8351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ResetEvent_internal(IntPtr handle);

		// Token: 0x060020A0 RID: 8352
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CloseEvent_internal(IntPtr handle);

		// Token: 0x060020A1 RID: 8353 RVA: 0x000770B8 File Offset: 0x000752B8
		public unsafe static IntPtr OpenEvent_internal(string name, EventWaitHandleRights rights, out int errorCode)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return NativeEventCalls.OpenEvent_icall(ptr, (name != null) ? name.Length : 0, rights, out errorCode);
		}

		// Token: 0x060020A2 RID: 8354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr OpenEvent_icall(char* name, int name_length, EventWaitHandleRights rights, out int errorCode);
	}
}
