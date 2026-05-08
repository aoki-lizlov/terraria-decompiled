using System;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000054 RID: 84
	public class VisualizationData
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0001FDC1 File Offset: 0x0001DFC1
		public ReadOnlyCollection<float> Frequencies
		{
			get
			{
				return new ReadOnlyCollection<float>(this.freq);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0001FDCE File Offset: 0x0001DFCE
		public ReadOnlyCollection<float> Samples
		{
			get
			{
				return new ReadOnlyCollection<float>(this.samp);
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0001FDDB File Offset: 0x0001DFDB
		public VisualizationData()
		{
			this.freq = new float[256];
			this.samp = new float[256];
		}

		// Token: 0x04000604 RID: 1540
		internal const int Size = 256;

		// Token: 0x04000605 RID: 1541
		internal float[] freq;

		// Token: 0x04000606 RID: 1542
		internal float[] samp;
	}
}
