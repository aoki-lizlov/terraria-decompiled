using System;
using System.IO;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000005 RID: 5
	internal class Factory : IFactory
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public IHuffman CreateHuffman()
		{
			return new Huffman();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public IMdct CreateMdct()
		{
			return new Mdct();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002ADE File Offset: 0x00000CDE
		public ICodebook CreateCodebook()
		{
			return new Codebook();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public IFloor CreateFloor(IPacket packet)
		{
			int num = (int)packet.ReadBits(16);
			if (num == 0)
			{
				return new Floor0();
			}
			if (num != 1)
			{
				throw new InvalidDataException("Invalid floor type!");
			}
			return new Floor1();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B1E File Offset: 0x00000D1E
		public IMapping CreateMapping(IPacket packet)
		{
			if (packet.ReadBits(16) != 0UL)
			{
				throw new InvalidDataException("Invalid mapping type!");
			}
			return new Mapping();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B3A File Offset: 0x00000D3A
		public IMode CreateMode()
		{
			return new Mode();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B44 File Offset: 0x00000D44
		public IResidue CreateResidue(IPacket packet)
		{
			switch ((int)packet.ReadBits(16))
			{
			case 0:
				return new Residue0();
			case 1:
				return new Residue1();
			case 2:
				return new Residue2();
			default:
				throw new InvalidDataException("Invalid residue type!");
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B8B File Offset: 0x00000D8B
		public Factory()
		{
		}
	}
}
