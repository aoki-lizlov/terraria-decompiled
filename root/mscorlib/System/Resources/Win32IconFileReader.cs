using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x0200084B RID: 2123
	internal class Win32IconFileReader
	{
		// Token: 0x060047DB RID: 18395 RVA: 0x000ED826 File Offset: 0x000EBA26
		public Win32IconFileReader(Stream s)
		{
			this.iconFile = s;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x000ED838 File Offset: 0x000EBA38
		public ICONDIRENTRY[] ReadIcons()
		{
			ICONDIRENTRY[] array2;
			using (BinaryReader binaryReader = new BinaryReader(this.iconFile))
			{
				bool flag = binaryReader.ReadInt16() != 0;
				int num = (int)binaryReader.ReadInt16();
				if (flag || num != 1)
				{
					throw new Exception("Invalid .ico file format");
				}
				long num2 = (long)binaryReader.ReadInt16();
				ICONDIRENTRY[] array = new ICONDIRENTRY[num2];
				int num3 = 0;
				while ((long)num3 < num2)
				{
					ICONDIRENTRY icondirentry = new ICONDIRENTRY();
					icondirentry.bWidth = binaryReader.ReadByte();
					icondirentry.bHeight = binaryReader.ReadByte();
					icondirentry.bColorCount = binaryReader.ReadByte();
					icondirentry.bReserved = binaryReader.ReadByte();
					icondirentry.wPlanes = binaryReader.ReadInt16();
					icondirentry.wBitCount = binaryReader.ReadInt16();
					int num4 = binaryReader.ReadInt32();
					int num5 = binaryReader.ReadInt32();
					icondirentry.image = new byte[num4];
					long position = this.iconFile.Position;
					this.iconFile.Position = (long)num5;
					this.iconFile.Read(icondirentry.image, 0, num4);
					this.iconFile.Position = position;
					if (icondirentry.wPlanes == 0)
					{
						icondirentry.wPlanes = (short)((int)icondirentry.image[12] | ((int)icondirentry.image[13] << 8));
					}
					if (icondirentry.wBitCount == 0)
					{
						icondirentry.wBitCount = (short)((int)icondirentry.image[14] | ((int)icondirentry.image[15] << 8));
					}
					array[num3] = icondirentry;
					num3++;
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x04002DC8 RID: 11720
		private Stream iconFile;
	}
}
