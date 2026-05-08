using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020004A1 RID: 1185
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		// Token: 0x060030F5 RID: 12533 RVA: 0x000B6570 File Offset: 0x000B4770
		public SHA1CryptoServiceProvider()
		{
			this.sha = new SHA1Internal();
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000B6584 File Offset: 0x000B4784
		~SHA1CryptoServiceProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000B65B4 File Offset: 0x000B47B4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000B65BD File Offset: 0x000B47BD
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this.State = 1;
			this.sha.HashCore(rgb, ibStart, cbSize);
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000B65D4 File Offset: 0x000B47D4
		protected override byte[] HashFinal()
		{
			this.State = 0;
			return this.sha.HashFinal();
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000B65E8 File Offset: 0x000B47E8
		public override void Initialize()
		{
			this.sha.Initialize();
		}

		// Token: 0x040021FC RID: 8700
		private SHA1Internal sha;
	}
}
