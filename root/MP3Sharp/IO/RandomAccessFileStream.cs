using System;
using System.IO;

namespace XPT.Core.Audio.MP3Sharp.IO
{
	// Token: 0x02000007 RID: 7
	public class RandomAccessFileStream
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000270C File Offset: 0x0000090C
		internal static FileStream CreateRandomAccessFile(string fileName, string mode)
		{
			FileStream fileStream;
			if (string.Compare(mode, "rw", 4) == 0)
			{
				fileStream = new FileStream(fileName, 4, 3);
			}
			else
			{
				if (string.Compare(mode, "r", 4) != 0)
				{
					throw new ArgumentException();
				}
				fileStream = new FileStream(fileName, 3, 1);
			}
			return fileStream;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002752 File Offset: 0x00000952
		public RandomAccessFileStream()
		{
		}
	}
}
