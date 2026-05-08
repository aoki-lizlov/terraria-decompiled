using System;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000016 RID: 22
	public class ChromaEngineUpdateEventArgs : EventArgs
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000431B File Offset: 0x0000251B
		public ChromaEngineUpdateEventArgs(float timeElapsed)
		{
			this.TimeElapsed = timeElapsed;
		}

		// Token: 0x04000033 RID: 51
		public readonly float TimeElapsed;
	}
}
