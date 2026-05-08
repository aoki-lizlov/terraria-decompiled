using System;

namespace Steamworks
{
	// Token: 0x02000183 RID: 387
	public abstract class CallResult
	{
		// Token: 0x060008D3 RID: 2259
		internal abstract Type GetCallbackType();

		// Token: 0x060008D4 RID: 2260
		internal abstract void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall);

		// Token: 0x060008D5 RID: 2261
		internal abstract void SetUnregistered();

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		protected CallResult()
		{
		}
	}
}
