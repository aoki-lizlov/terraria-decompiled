using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A6 RID: 422
	[Serializable]
	public struct SteamInputActionEvent_t
	{
		// Token: 0x04000ACE RID: 2766
		public InputHandle_t controllerHandle;

		// Token: 0x04000ACF RID: 2767
		public ESteamInputActionEventType eEventType;

		// Token: 0x04000AD0 RID: 2768
		public SteamInputActionEvent_t.OptionValue m_val;

		// Token: 0x020001F1 RID: 497
		[Serializable]
		public struct AnalogAction_t
		{
			// Token: 0x04000B61 RID: 2913
			public InputAnalogActionHandle_t actionHandle;

			// Token: 0x04000B62 RID: 2914
			public InputAnalogActionData_t analogActionData;
		}

		// Token: 0x020001F2 RID: 498
		[Serializable]
		public struct DigitalAction_t
		{
			// Token: 0x04000B63 RID: 2915
			public InputDigitalActionHandle_t actionHandle;

			// Token: 0x04000B64 RID: 2916
			public InputDigitalActionData_t digitalActionData;
		}

		// Token: 0x020001F3 RID: 499
		[Serializable]
		[StructLayout(2)]
		public struct OptionValue
		{
			// Token: 0x04000B65 RID: 2917
			[FieldOffset(0)]
			public SteamInputActionEvent_t.AnalogAction_t analogAction;

			// Token: 0x04000B66 RID: 2918
			[FieldOffset(0)]
			public SteamInputActionEvent_t.DigitalAction_t digitalAction;
		}
	}
}
