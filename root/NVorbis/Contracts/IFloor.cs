using System;

namespace NVorbis.Contracts
{
	// Token: 0x02000028 RID: 40
	internal interface IFloor
	{
		// Token: 0x060001BD RID: 445
		void Init(IPacket packet, int channels, int block0Size, int block1Size, ICodebook[] codebooks);

		// Token: 0x060001BE RID: 446
		IFloorData Unpack(IPacket packet, int blockSize, int channel);

		// Token: 0x060001BF RID: 447
		void Apply(IFloorData floorData, int blockSize, float[] residue);
	}
}
