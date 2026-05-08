using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000457 RID: 1111
	[ComVisible(true)]
	public abstract class DeriveBytes : IDisposable
	{
		// Token: 0x06002E53 RID: 11859
		public abstract byte[] GetBytes(int cb);

		// Token: 0x06002E54 RID: 11860
		public abstract void Reset();

		// Token: 0x06002E55 RID: 11861 RVA: 0x000A6C33 File Offset: 0x000A4E33
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000025BE File Offset: 0x000007BE
		protected DeriveBytes()
		{
		}
	}
}
