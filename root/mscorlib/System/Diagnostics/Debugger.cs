using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A18 RID: 2584
	[ComVisible(true)]
	public sealed class Debugger
	{
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06005FD3 RID: 24531 RVA: 0x0014C2D1 File Offset: 0x0014A4D1
		public static bool IsAttached
		{
			get
			{
				return Debugger.IsAttached_internal();
			}
		}

		// Token: 0x06005FD4 RID: 24532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAttached_internal();

		// Token: 0x06005FD5 RID: 24533 RVA: 0x00004088 File Offset: 0x00002288
		public static void Break()
		{
		}

		// Token: 0x06005FD6 RID: 24534
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLogging();

		// Token: 0x06005FD7 RID: 24535 RVA: 0x000174FB File Offset: 0x000156FB
		public static bool Launch()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005FD8 RID: 24536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Log_icall(int level, ref string category, ref string message);

		// Token: 0x06005FD9 RID: 24537 RVA: 0x0014C2D8 File Offset: 0x0014A4D8
		public static void Log(int level, string category, string message)
		{
			Debugger.Log_icall(level, ref category, ref message);
		}

		// Token: 0x06005FDA RID: 24538 RVA: 0x00004088 File Offset: 0x00002288
		public static void NotifyOfCrossThreadDependency()
		{
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x000025BE File Offset: 0x000007BE
		[Obsolete("Call the static methods directly on this type", true)]
		public Debugger()
		{
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x0014C2E4 File Offset: 0x0014A4E4
		// Note: this type is marked as 'beforefieldinit'.
		static Debugger()
		{
		}

		// Token: 0x040039BA RID: 14778
		public static readonly string DefaultCategory = "";
	}
}
