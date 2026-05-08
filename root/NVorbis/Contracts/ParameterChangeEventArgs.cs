using System;
using System.Runtime.CompilerServices;

namespace NVorbis.Contracts
{
	// Token: 0x02000036 RID: 54
	public class ParameterChangeEventArgs : EventArgs
	{
		// Token: 0x06000234 RID: 564 RVA: 0x000086FD File Offset: 0x000068FD
		public ParameterChangeEventArgs(int channels, int sampleRate)
		{
			this.Channels = channels;
			this.SampleRate = sampleRate;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008713 File Offset: 0x00006913
		public int Channels
		{
			[CompilerGenerated]
			get
			{
				return this.<Channels>k__BackingField;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000871B File Offset: 0x0000691B
		public int SampleRate
		{
			[CompilerGenerated]
			get
			{
				return this.<SampleRate>k__BackingField;
			}
		}

		// Token: 0x040000DB RID: 219
		[CompilerGenerated]
		private readonly int <Channels>k__BackingField;

		// Token: 0x040000DC RID: 220
		[CompilerGenerated]
		private readonly int <SampleRate>k__BackingField;
	}
}
