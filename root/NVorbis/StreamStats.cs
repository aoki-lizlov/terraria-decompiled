using System;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000016 RID: 22
	internal class StreamStats : IStreamStats
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005988 File Offset: 0x00003B88
		public int EffectiveBitRate
		{
			get
			{
				object @lock = this._lock;
				long totalSamples;
				long num;
				lock (@lock)
				{
					totalSamples = this._totalSamples;
					num = this._audioBits + this._headerBits + this._containerBits + this._wasteBits;
				}
				if (totalSamples > 0L)
				{
					return (int)((double)num / (double)totalSamples * (double)this._sampleRate);
				}
				return 0;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000059FC File Offset: 0x00003BFC
		public int InstantBitRate
		{
			get
			{
				object @lock = this._lock;
				int num;
				int num2;
				lock (@lock)
				{
					num = this._packetBits[0] + this._packetBits[1];
					num2 = this._packetSamples[0] + this._packetSamples[1];
				}
				if (num2 > 0)
				{
					return (int)((double)num / (double)num2 * (double)this._sampleRate);
				}
				return 0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005A70 File Offset: 0x00003C70
		public long ContainerBits
		{
			get
			{
				return this._containerBits;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005A78 File Offset: 0x00003C78
		public long OverheadBits
		{
			get
			{
				return this._headerBits;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005A80 File Offset: 0x00003C80
		public long AudioBits
		{
			get
			{
				return this._audioBits;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005A88 File Offset: 0x00003C88
		public long WasteBits
		{
			get
			{
				return this._wasteBits;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005A90 File Offset: 0x00003C90
		public int PacketCount
		{
			get
			{
				return this._packetCount;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005A98 File Offset: 0x00003C98
		public void ResetStats()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._packetBits[0] = (this._packetBits[1] = 0);
				this._packetSamples[0] = (this._packetSamples[1] = 0);
				this._packetIndex = 0;
				this._packetCount = 0;
				this._audioBits = 0L;
				this._totalSamples = 0L;
				this._headerBits = 0L;
				this._containerBits = 0L;
				this._wasteBits = 0L;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005B30 File Offset: 0x00003D30
		internal void SetSampleRate(int sampleRate)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._sampleRate = sampleRate;
				this.ResetStats();
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005B78 File Offset: 0x00003D78
		internal void AddPacket(int samples, int bits, int waste, int container)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (samples >= 0)
				{
					this._audioBits += (long)bits;
					this._wasteBits += (long)waste;
					this._containerBits += (long)container;
					this._totalSamples += (long)samples;
					this._packetBits[this._packetIndex] = bits + waste;
					this._packetSamples[this._packetIndex] = samples;
					int num = this._packetIndex + 1;
					this._packetIndex = num;
					if (num == 2)
					{
						this._packetIndex = 0;
					}
				}
				else
				{
					this._headerBits += (long)bits;
					this._wasteBits += (long)waste;
					this._containerBits += (long)container;
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005C5C File Offset: 0x00003E5C
		public StreamStats()
		{
		}

		// Token: 0x04000071 RID: 113
		private int _sampleRate;

		// Token: 0x04000072 RID: 114
		private readonly int[] _packetBits = new int[2];

		// Token: 0x04000073 RID: 115
		private readonly int[] _packetSamples = new int[2];

		// Token: 0x04000074 RID: 116
		private int _packetIndex;

		// Token: 0x04000075 RID: 117
		private long _totalSamples;

		// Token: 0x04000076 RID: 118
		private long _audioBits;

		// Token: 0x04000077 RID: 119
		private long _headerBits;

		// Token: 0x04000078 RID: 120
		private long _containerBits;

		// Token: 0x04000079 RID: 121
		private long _wasteBits;

		// Token: 0x0400007A RID: 122
		private object _lock = new object();

		// Token: 0x0400007B RID: 123
		private int _packetCount;
	}
}
