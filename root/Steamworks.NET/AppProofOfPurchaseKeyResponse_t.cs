using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002B RID: 43
	[CallbackIdentity(1021)]
	[StructLayout(0, Pack = 4)]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0000C225 File Offset: 0x0000A425
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0000C232 File Offset: 0x0000A432
		public string m_rgchKey
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchKey_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchKey_, 240);
			}
		}

		// Token: 0x04000006 RID: 6
		public const int k_iCallback = 1021;

		// Token: 0x04000007 RID: 7
		public EResult m_eResult;

		// Token: 0x04000008 RID: 8
		public uint m_nAppID;

		// Token: 0x04000009 RID: 9
		public uint m_cchKeyLength;

		// Token: 0x0400000A RID: 10
		[MarshalAs(30, SizeConst = 240)]
		private byte[] m_rgchKey_;
	}
}
