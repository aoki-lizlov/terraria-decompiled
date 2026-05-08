using System;
using System.IO;

namespace XPT.Core.Audio.MP3Sharp.Support
{
	// Token: 0x02000006 RID: 6
	public class SupportClass
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000025D0 File Offset: 0x000007D0
		internal static int URShift(int number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2 << ~bits);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025EB File Offset: 0x000007EB
		internal static int URShift(int number, long bits)
		{
			return SupportClass.URShift(number, (int)bits);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025F5 File Offset: 0x000007F5
		internal static long URShift(long number, int bits)
		{
			if (number >= 0L)
			{
				return number >> bits;
			}
			return (number >> bits) + (2L << ~bits);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002612 File Offset: 0x00000812
		internal static long URShift(long number, long bits)
		{
			return SupportClass.URShift(number, (int)bits);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000261C File Offset: 0x0000081C
		internal static void WriteStackTrace(Exception throwable, TextWriter stream)
		{
			stream.Write(throwable.StackTrace);
			stream.Flush();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002630 File Offset: 0x00000830
		internal static long Identity(long literal)
		{
			return literal;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002633 File Offset: 0x00000833
		internal static ulong Identity(ulong literal)
		{
			return literal;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002636 File Offset: 0x00000836
		internal static float Identity(float literal)
		{
			return literal;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002639 File Offset: 0x00000839
		internal static double Identity(double literal)
		{
			return literal;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000263C File Offset: 0x0000083C
		internal static int ReadInput(Stream sourceStream, ref sbyte[] target, int start, int count)
		{
			byte[] array = new byte[target.Length];
			int num = sourceStream.Read(array, start, count);
			for (int i = start; i < start + num; i++)
			{
				target[i] = (sbyte)array[i];
			}
			return num;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002674 File Offset: 0x00000874
		internal static byte[] ToByteArray(sbyte[] sbyteArray)
		{
			byte[] array = new byte[sbyteArray.Length];
			for (int i = 0; i < sbyteArray.Length; i++)
			{
				array[i] = (byte)sbyteArray[i];
			}
			return array;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026A0 File Offset: 0x000008A0
		internal static byte[] ToByteArray(string sourceString)
		{
			byte[] array = new byte[sourceString.Length];
			for (int i = 0; i < sourceString.Length; i++)
			{
				array[i] = (byte)sourceString.get_Chars(i);
			}
			return array;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026D8 File Offset: 0x000008D8
		internal static void GetSBytesFromString(string sourceString, int sourceStart, int sourceEnd, ref sbyte[] destinationArray, int destinationStart)
		{
			int i = sourceStart;
			int num = destinationStart;
			while (i < sourceEnd)
			{
				destinationArray[num] = (sbyte)sourceString.get_Chars(i);
				i++;
				num++;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002704 File Offset: 0x00000904
		public SupportClass()
		{
		}
	}
}
