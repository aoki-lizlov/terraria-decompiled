using System;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000014 RID: 20
	internal class Residue2 : Residue0
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00004A75 File Offset: 0x00002C75
		public override void Init(IPacket packet, int channels, ICodebook[] codebooks)
		{
			this._channels = channels;
			base.Init(packet, 1, codebooks);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004A87 File Offset: 0x00002C87
		public override void Decode(IPacket packet, bool[] doNotDecodeChannel, int blockSize, float[][] buffer)
		{
			base.Decode(packet, doNotDecodeChannel, blockSize * this._channels, buffer);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004A9C File Offset: 0x00002C9C
		protected override bool WriteVectors(ICodebook codebook, IPacket packet, float[][] residue, int channel, int offset, int partitionSize)
		{
			int num = 0;
			offset /= this._channels;
			int i = 0;
			while (i < partitionSize)
			{
				int num2 = codebook.DecodeScalar(packet);
				if (num2 == -1)
				{
					return true;
				}
				int j = 0;
				while (j < codebook.Dimensions)
				{
					residue[num][offset] += codebook[num2, j];
					if (++num == this._channels)
					{
						num = 0;
						offset++;
					}
					j++;
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004B0E File Offset: 0x00002D0E
		public Residue2()
		{
		}

		// Token: 0x04000053 RID: 83
		private int _channels;
	}
}
