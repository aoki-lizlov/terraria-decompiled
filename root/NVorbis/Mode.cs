using System;
using System.IO;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x0200000E RID: 14
	internal class Mode : IMode
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003FF0 File Offset: 0x000021F0
		public void Init(IPacket packet, int channels, int block0Size, int block1Size, IMapping[] mappings)
		{
			this._channels = channels;
			this._block0Size = block0Size;
			this._block1Size = block1Size;
			this._blockFlag = packet.ReadBit();
			if (packet.ReadBits(32) != 0UL)
			{
				throw new InvalidDataException("Mode header had invalid window or transform type!");
			}
			int num = (int)packet.ReadBits(8);
			if (num >= mappings.Length)
			{
				throw new InvalidDataException("Mode header had invalid mapping index!");
			}
			this._mapping = mappings[num];
			if (this._blockFlag)
			{
				this.Windows = new float[][]
				{
					new float[this._block1Size],
					new float[this._block1Size],
					new float[this._block1Size],
					new float[this._block1Size]
				};
			}
			else
			{
				this.Windows = new float[][] { new float[this._block0Size] };
			}
			this.CalcWindows();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000040C8 File Offset: 0x000022C8
		private void CalcWindows()
		{
			for (int i = 0; i < this.Windows.Length; i++)
			{
				float[] array = this.Windows[i];
				int num = (((i & 1) == 0) ? this._block0Size : this._block1Size) / 2;
				int blockSize = this.BlockSize;
				int num2 = (((i & 2) == 0) ? this._block0Size : this._block1Size) / 2;
				int num3 = blockSize / 4 - num / 2;
				int num4 = blockSize - blockSize / 4 - num2 / 2;
				for (int j = 0; j < num; j++)
				{
					float num5 = (float)Math.Sin(((double)j + 0.5) / (double)num * 1.5707963705062866);
					num5 *= num5;
					array[num3 + j] = (float)Math.Sin((double)(num5 * 1.5707964f));
				}
				for (int k = num3 + num; k < num4; k++)
				{
					array[k] = 1f;
				}
				for (int l = 0; l < num2; l++)
				{
					float num6 = (float)Math.Sin(((double)(num2 - l) - 0.5) / (double)num2 * 1.5707963705062866);
					num6 *= num6;
					array[num4 + l] = (float)Math.Sin((double)(num6 * 1.5707964f));
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000041FC File Offset: 0x000023FC
		private bool GetPacketInfo(IPacket packet, bool isLastInPage, out int blockSize, out int windowIndex, out int leftOverlapHalfSize, out int packetStartIndex, out int packetValidLength, out int packetTotalLength)
		{
			bool flag;
			bool flag2;
			if (this._blockFlag)
			{
				blockSize = this._block1Size;
				flag = packet.ReadBit();
				flag2 = packet.ReadBit();
			}
			else
			{
				blockSize = this._block0Size;
				flag2 = (flag = false);
			}
			if (packet.IsShort)
			{
				windowIndex = 0;
				leftOverlapHalfSize = 0;
				packetStartIndex = 0;
				packetValidLength = 0;
				packetTotalLength = 0;
				return false;
			}
			int num = (flag2 ? this._block1Size : this._block0Size) / 4;
			windowIndex = (flag ? 1 : 0) + (flag2 ? 2 : 0);
			leftOverlapHalfSize = (flag ? this._block1Size : this._block0Size) / 4;
			packetStartIndex = blockSize / 4 - leftOverlapHalfSize;
			packetTotalLength = blockSize / 4 * 3 + num;
			packetValidLength = packetTotalLength - num * 2;
			if (isLastInPage && this._blockFlag && !flag2)
			{
				packetValidLength -= this._block1Size / 4 - this._block0Size / 4;
			}
			return true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000042D8 File Offset: 0x000024D8
		public bool Decode(IPacket packet, float[][] buffer, out int packetStartindex, out int packetValidLength, out int packetTotalLength)
		{
			int num;
			int num2;
			int num3;
			if (this.GetPacketInfo(packet, false, out num, out num2, out num3, out packetStartindex, out packetValidLength, out packetTotalLength))
			{
				this._mapping.DecodePacket(packet, num, this._channels, buffer);
				float[] array = this.Windows[num2];
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < this._channels; j++)
					{
						buffer[j][i] *= array[i];
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004354 File Offset: 0x00002554
		public int GetPacketSampleCount(IPacket packet, bool isLastInPage)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			this.GetPacketInfo(packet, isLastInPage, out num, out num2, out num3, out num4, out num5, out num6);
			return num5 - num4;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004379 File Offset: 0x00002579
		public int BlockSize
		{
			get
			{
				if (!this._blockFlag)
				{
					return this._block0Size;
				}
				return this._block1Size;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004390 File Offset: 0x00002590
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00004398 File Offset: 0x00002598
		public float[][] Windows
		{
			[CompilerGenerated]
			get
			{
				return this.<Windows>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Windows>k__BackingField = value;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000043A1 File Offset: 0x000025A1
		public Mode()
		{
		}

		// Token: 0x04000039 RID: 57
		private const float M_PI2 = 1.5707964f;

		// Token: 0x0400003A RID: 58
		private int _channels;

		// Token: 0x0400003B RID: 59
		private bool _blockFlag;

		// Token: 0x0400003C RID: 60
		private int _block0Size;

		// Token: 0x0400003D RID: 61
		private int _block1Size;

		// Token: 0x0400003E RID: 62
		private IMapping _mapping;

		// Token: 0x0400003F RID: 63
		[CompilerGenerated]
		private float[][] <Windows>k__BackingField;
	}
}
