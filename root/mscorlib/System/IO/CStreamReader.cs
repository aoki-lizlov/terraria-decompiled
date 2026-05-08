using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200098C RID: 2444
	internal class CStreamReader : StreamReader
	{
		// Token: 0x06005935 RID: 22837 RVA: 0x0012EC40 File Offset: 0x0012CE40
		public CStreamReader(Stream stream, Encoding encoding)
			: base(stream, encoding)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x0012EC5C File Offset: 0x0012CE5C
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

		// Token: 0x06005937 RID: 22839 RVA: 0x0012EC88 File Offset: 0x0012CE88
		public override int Read()
		{
			try
			{
				return (int)Console.ReadKey().KeyChar;
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x0012ECBC File Offset: 0x0012CEBC
		public override int Read([In] [Out] char[] dest, int index, int count)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest.Length - count)
			{
				throw new ArgumentException("index + count > dest.Length");
			}
			try
			{
				return this.driver.Read(dest, index, count);
			}
			catch (IOException)
			{
			}
			return 0;
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x0012ED3C File Offset: 0x0012CF3C
		public override string ReadLine()
		{
			try
			{
				return this.driver.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x0012ED70 File Offset: 0x0012CF70
		public override string ReadToEnd()
		{
			try
			{
				return this.driver.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x0400356E RID: 13678
		private TermInfoDriver driver;
	}
}
