using System;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000013 RID: 19
	internal class Residue1 : Residue0
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004A18 File Offset: 0x00002C18
		protected override bool WriteVectors(ICodebook codebook, IPacket packet, float[][] residue, int channel, int offset, int partitionSize)
		{
			float[] array = residue[channel];
			int i = 0;
			while (i < partitionSize)
			{
				int num = codebook.DecodeScalar(packet);
				if (num == -1)
				{
					return true;
				}
				for (int j = 0; j < codebook.Dimensions; j++)
				{
					array[offset + i] += codebook[num, j];
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004A6D File Offset: 0x00002C6D
		public Residue1()
		{
		}
	}
}
