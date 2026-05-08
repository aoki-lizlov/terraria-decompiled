using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200012E RID: 302
	internal class SoundEffectReader : ContentTypeReader<SoundEffect>
	{
		// Token: 0x06001771 RID: 6001 RVA: 0x0003A050 File Offset: 0x00038250
		protected internal override SoundEffect Read(ContentReader input, SoundEffect existingInstance)
		{
			bool flag = input.platform == 'x';
			uint num = input.ReadUInt32();
			ushort num2 = SoundEffectReader.Swap(flag, input.ReadUInt16());
			ushort num3 = SoundEffectReader.Swap(flag, input.ReadUInt16());
			uint num4 = SoundEffectReader.Swap(flag, input.ReadUInt32());
			uint num5 = SoundEffectReader.Swap(flag, input.ReadUInt32());
			ushort num6 = SoundEffectReader.Swap(flag, input.ReadUInt16());
			ushort num7 = SoundEffectReader.Swap(flag, input.ReadUInt16());
			byte[] array = null;
			if (num > 16U)
			{
				ushort num8 = SoundEffectReader.Swap(flag, input.ReadUInt16());
				if (num2 == 358 && num8 == 34)
				{
					array = new byte[34];
					using (MemoryStream memoryStream = new MemoryStream(array))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
						{
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt16()));
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt32()));
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt32()));
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt32()));
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt32()));
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt32()));
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt32()));
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt32()));
							binaryWriter.Write(input.ReadByte());
							binaryWriter.Write(input.ReadByte());
							binaryWriter.Write(SoundEffectReader.Swap(flag, input.ReadUInt16()));
						}
					}
					input.ReadBytes((int)(num - 18U - 34U));
				}
				else
				{
					input.ReadBytes((int)(num - 18U));
				}
			}
			byte[] array2 = input.ReadBytes(input.ReadInt32());
			int num9 = input.ReadInt32();
			int num10 = input.ReadInt32();
			input.ReadUInt32();
			return new SoundEffect(input.AssetName, array2, 0, array2.Length, array, num2, num3, num4, num5, num6, num7, num9, num10);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0003A268 File Offset: 0x00038468
		internal static ushort Swap(bool swap, ushort x)
		{
			if (swap)
			{
				return (ushort)(((x >> 8) & 255) | (((int)x << 8) & 65280));
			}
			return x;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0003A283 File Offset: 0x00038483
		internal static uint Swap(bool swap, uint x)
		{
			if (swap)
			{
				return ((x >> 24) & 255U) | ((x >> 8) & 65280U) | ((x << 8) & 16711680U) | ((x << 24) & 4278190080U);
			}
			return x;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0003A2B3 File Offset: 0x000384B3
		public SoundEffectReader()
		{
		}
	}
}
