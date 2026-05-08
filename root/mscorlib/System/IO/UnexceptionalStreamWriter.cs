using System;
using System.Text;

namespace System.IO
{
	// Token: 0x0200098B RID: 2443
	internal class UnexceptionalStreamWriter : StreamWriter
	{
		// Token: 0x0600592F RID: 22831 RVA: 0x0012EB58 File Offset: 0x0012CD58
		public UnexceptionalStreamWriter(Stream stream, Encoding encoding)
			: base(stream, encoding, 1024, true)
		{
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x0012EB68 File Offset: 0x0012CD68
		public override void Flush()
		{
			try
			{
				base.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x0012EB90 File Offset: 0x0012CD90
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x0012EBBC File Offset: 0x0012CDBC
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x0012EBE8 File Offset: 0x0012CDE8
		public override void Write(char[] value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x0012EC14 File Offset: 0x0012CE14
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}
	}
}
