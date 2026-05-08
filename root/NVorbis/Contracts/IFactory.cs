using System;

namespace NVorbis.Contracts
{
	// Token: 0x02000027 RID: 39
	internal interface IFactory
	{
		// Token: 0x060001B6 RID: 438
		ICodebook CreateCodebook();

		// Token: 0x060001B7 RID: 439
		IFloor CreateFloor(IPacket packet);

		// Token: 0x060001B8 RID: 440
		IResidue CreateResidue(IPacket packet);

		// Token: 0x060001B9 RID: 441
		IMapping CreateMapping(IPacket packet);

		// Token: 0x060001BA RID: 442
		IMode CreateMode();

		// Token: 0x060001BB RID: 443
		IMdct CreateMdct();

		// Token: 0x060001BC RID: 444
		IHuffman CreateHuffman();
	}
}
