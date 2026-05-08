using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders.LayerIII
{
	// Token: 0x0200002D RID: 45
	public class ScaleFactorTable
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0001B517 File Offset: 0x00019717
		internal ScaleFactorTable(LayerIIIDecoder enclosingInstance)
		{
			this.InitBlock(enclosingInstance);
			this.L = new int[5];
			this.S = new int[3];
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0001B53E File Offset: 0x0001973E
		internal ScaleFactorTable(LayerIIIDecoder enclosingInstance, int[] thel, int[] thes)
		{
			this.InitBlock(enclosingInstance);
			this.L = thel;
			this.S = thes;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0001B55B File Offset: 0x0001975B
		internal LayerIIIDecoder EnclosingInstance
		{
			get
			{
				return this._EnclosingInstance;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0001B563 File Offset: 0x00019763
		private void InitBlock(LayerIIIDecoder enclosingInstance)
		{
			this._EnclosingInstance = enclosingInstance;
		}

		// Token: 0x04000183 RID: 387
		internal int[] L;

		// Token: 0x04000184 RID: 388
		internal int[] S;

		// Token: 0x04000185 RID: 389
		private LayerIIIDecoder _EnclosingInstance;
	}
}
