using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200098A RID: 2442
	internal class UnexceptionalStreamReader : StreamReader
	{
		// Token: 0x06005927 RID: 22823 RVA: 0x0012E944 File Offset: 0x0012CB44
		static UnexceptionalStreamReader()
		{
			string newLine = Environment.NewLine;
			if (newLine.Length == 1)
			{
				UnexceptionalStreamReader.newlineChar = newLine[0];
			}
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x0012E980 File Offset: 0x0012CB80
		public UnexceptionalStreamReader(Stream stream, Encoding encoding)
			: base(stream, encoding)
		{
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0012E98C File Offset: 0x0012CB8C
		public override int Peek()
		{
			try
			{
				return base.Peek();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x0012E9B8 File Offset: 0x0012CBB8
		public override int Read()
		{
			try
			{
				return base.Read();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0012E9E4 File Offset: 0x0012CBE4
		public override int Read([In] [Out] char[] dest_buffer, int index, int count)
		{
			if (dest_buffer == null)
			{
				throw new ArgumentNullException("dest_buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest_buffer.Length - count)
			{
				throw new ArgumentException("index + count > dest_buffer.Length");
			}
			int num = 0;
			char c = UnexceptionalStreamReader.newlineChar;
			try
			{
				while (count > 0)
				{
					int num2 = base.Read();
					if (num2 < 0)
					{
						break;
					}
					num++;
					count--;
					dest_buffer[index] = (char)num2;
					if (c != '\0')
					{
						if ((char)num2 == c)
						{
							return num;
						}
					}
					else if (this.CheckEOL((char)num2))
					{
						return num;
					}
					index++;
				}
			}
			catch (IOException)
			{
			}
			return num;
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x0012EA98 File Offset: 0x0012CC98
		private bool CheckEOL(char current)
		{
			int i = 0;
			while (i < UnexceptionalStreamReader.newline.Length)
			{
				if (!UnexceptionalStreamReader.newline[i])
				{
					if (current == Environment.NewLine[i])
					{
						UnexceptionalStreamReader.newline[i] = true;
						return i == UnexceptionalStreamReader.newline.Length - 1;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < UnexceptionalStreamReader.newline.Length; j++)
			{
				UnexceptionalStreamReader.newline[j] = false;
			}
			return false;
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x0012EB00 File Offset: 0x0012CD00
		public override string ReadLine()
		{
			try
			{
				return base.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x0012EB2C File Offset: 0x0012CD2C
		public override string ReadToEnd()
		{
			try
			{
				return base.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x0400356C RID: 13676
		private static bool[] newline = new bool[Environment.NewLine.Length];

		// Token: 0x0400356D RID: 13677
		private static char newlineChar;
	}
}
