using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000097 RID: 151
	internal sealed class SafePasswordHandle : SafeHandle
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0001815B File Offset: 0x0001635B
		private IntPtr CreateHandle(string password)
		{
			return Marshal.StringToHGlobalAnsi(password);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00018163 File Offset: 0x00016363
		private IntPtr CreateHandle(SecureString password)
		{
			return Marshal.SecureStringToGlobalAllocAnsi(password);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001816B File Offset: 0x0001636B
		private void FreeHandle()
		{
			Marshal.ZeroFreeGlobalAllocAnsi(this.handle);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00018178 File Offset: 0x00016378
		public SafePasswordHandle(string password)
			: base(IntPtr.Zero, true)
		{
			if (password != null)
			{
				base.SetHandle(this.CreateHandle(password));
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00018196 File Offset: 0x00016396
		public SafePasswordHandle(SecureString password)
			: base(IntPtr.Zero, true)
		{
			if (password != null)
			{
				base.SetHandle(this.CreateHandle(password));
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000181B4 File Offset: 0x000163B4
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				this.FreeHandle();
			}
			base.SetHandle((IntPtr)(-1));
			return true;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000181DB File Offset: 0x000163DB
		protected override void Dispose(bool disposing)
		{
			if (disposing && SafeHandleCache<SafePasswordHandle>.IsCachedInvalidHandle(this))
			{
				return;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000181F0 File Offset: 0x000163F0
		public override bool IsInvalid
		{
			get
			{
				return this.handle == (IntPtr)(-1);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00018203 File Offset: 0x00016403
		public static SafePasswordHandle InvalidHandle
		{
			get
			{
				return SafeHandleCache<SafePasswordHandle>.GetInvalidHandle(() => new SafePasswordHandle(null)
				{
					handle = (IntPtr)(-1)
				});
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00018229 File Offset: 0x00016429
		internal string Mono_DangerousGetString()
		{
			return Marshal.PtrToStringAnsi(base.DangerousGetHandle());
		}

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060004A4 RID: 1188 RVA: 0x00018236 File Offset: 0x00016436
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060004A5 RID: 1189 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x060004A6 RID: 1190 RVA: 0x00018242 File Offset: 0x00016442
			internal SafePasswordHandle <get_InvalidHandle>b__10_0()
			{
				return new SafePasswordHandle(null)
				{
					handle = (IntPtr)(-1)
				};
			}

			// Token: 0x04000EC5 RID: 3781
			public static readonly SafePasswordHandle.<>c <>9 = new SafePasswordHandle.<>c();

			// Token: 0x04000EC6 RID: 3782
			public static Func<SafePasswordHandle> <>9__10_0;
		}
	}
}
