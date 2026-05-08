using System;
using System.IO;

namespace Terraria.IO
{
	// Token: 0x02000073 RID: 115
	public class FileMetadata
	{
		// Token: 0x0600150A RID: 5386 RVA: 0x0000357B File Offset: 0x0000177B
		private FileMetadata()
		{
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x004BD361 File Offset: 0x004BB561
		public void Write(BinaryWriter writer)
		{
			writer.Write(27981915666277746UL | ((ulong)this.Type << 56));
			writer.Write(this.Revision);
			writer.Write((ulong)((long)((this.IsFavorite.ToInt() & 1) | 0)));
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x004BD39F File Offset: 0x004BB59F
		public void IncrementAndWrite(BinaryWriter writer)
		{
			this.Revision += 1U;
			this.Write(writer);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x004BD3B6 File Offset: 0x004BB5B6
		public static FileMetadata FromCurrentSettings(FileType type)
		{
			return new FileMetadata
			{
				Type = type,
				Revision = 0U,
				IsFavorite = false
			};
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x004BD3D4 File Offset: 0x004BB5D4
		public static FileMetadata Read(BinaryReader reader, FileType expectedType)
		{
			FileMetadata fileMetadata = new FileMetadata();
			fileMetadata.Read(reader);
			if (fileMetadata.Type != expectedType)
			{
				throw new FormatException(string.Concat(new string[]
				{
					"Expected type \"",
					Enum.GetName(typeof(FileType), expectedType),
					"\" but found \"",
					Enum.GetName(typeof(FileType), fileMetadata.Type),
					"\"."
				}));
			}
			return fileMetadata;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x004BD458 File Offset: 0x004BB658
		private void Read(BinaryReader reader)
		{
			ulong num = reader.ReadUInt64();
			if ((num & 72057594037927935UL) != 27981915666277746UL)
			{
				throw new FormatException("Expected Re-Logic file format.");
			}
			byte b = (byte)((num >> 56) & 255UL);
			FileType fileType = FileType.None;
			FileType[] array = (FileType[])Enum.GetValues(typeof(FileType));
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == (FileType)b)
				{
					fileType = array[i];
					break;
				}
			}
			if (fileType == FileType.None)
			{
				throw new FormatException("Found invalid file type.");
			}
			this.Type = fileType;
			this.Revision = reader.ReadUInt32();
			ulong num2 = reader.ReadUInt64();
			this.IsFavorite = (num2 & 1UL) == 1UL;
		}

		// Token: 0x040010C5 RID: 4293
		public const ulong MAGIC_NUMBER = 27981915666277746UL;

		// Token: 0x040010C6 RID: 4294
		public const int SIZE = 20;

		// Token: 0x040010C7 RID: 4295
		public FileType Type;

		// Token: 0x040010C8 RID: 4296
		public uint Revision;

		// Token: 0x040010C9 RID: 4297
		public bool IsFavorite;
	}
}
