using System;

namespace Steamworks
{
	// Token: 0x02000181 RID: 385
	public abstract class Callback
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060008C1 RID: 2241
		public abstract bool IsGameServer { get; }

		// Token: 0x060008C2 RID: 2242
		internal abstract Type GetCallbackType();

		// Token: 0x060008C3 RID: 2243
		internal abstract void OnRunCallback(IntPtr pvParam);

		// Token: 0x060008C4 RID: 2244
		internal abstract void SetUnregistered();

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		protected Callback()
		{
		}
	}
}
