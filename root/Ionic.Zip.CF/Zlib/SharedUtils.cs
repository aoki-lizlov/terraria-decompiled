using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x0200004E RID: 78
	internal class SharedUtils
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0001ADE2 File Offset: 0x00018FE2
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001ADEC File Offset: 0x00018FEC
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001AE2E File Offset: 0x0001902E
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001AE3B File Offset: 0x0001903B
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001AE48 File Offset: 0x00019048
		public SharedUtils()
		{
		}
	}
}
