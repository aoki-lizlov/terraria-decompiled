using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	public struct SteamNetworkingConfigValue_t
	{
		// Token: 0x04000AE2 RID: 2786
		public ESteamNetworkingConfigValue m_eValue;

		// Token: 0x04000AE3 RID: 2787
		public ESteamNetworkingConfigDataType m_eDataType;

		// Token: 0x04000AE4 RID: 2788
		public SteamNetworkingConfigValue_t.OptionValue m_val;

		// Token: 0x020001F4 RID: 500
		[StructLayout(2)]
		public struct OptionValue
		{
			// Token: 0x04000B67 RID: 2919
			[FieldOffset(0)]
			public int m_int32;

			// Token: 0x04000B68 RID: 2920
			[FieldOffset(0)]
			public long m_int64;

			// Token: 0x04000B69 RID: 2921
			[FieldOffset(0)]
			public float m_float;

			// Token: 0x04000B6A RID: 2922
			[FieldOffset(0)]
			public IntPtr m_string;

			// Token: 0x04000B6B RID: 2923
			[FieldOffset(0)]
			public IntPtr m_functionPtr;
		}
	}
}
