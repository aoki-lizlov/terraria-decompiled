using System;
using System.Text;

namespace System.IO
{
	// Token: 0x0200098D RID: 2445
	internal class CStreamWriter : StreamWriter
	{
		// Token: 0x0600593B RID: 22843 RVA: 0x0012EDA4 File Offset: 0x0012CFA4
		public CStreamWriter(Stream stream, Encoding encoding, bool leaveOpen)
			: base(stream, encoding, 1024, leaveOpen)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x0012EDC4 File Offset: 0x0012CFC4
		public override void Write(char[] buffer, int index, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (!this.driver.Initialized)
			{
				try
				{
					base.Write(buffer, index, count);
				}
				catch (IOException)
				{
				}
				return;
			}
			lock (this)
			{
				int num = index + count;
				int num2 = index;
				int num3 = 0;
				do
				{
					char c = buffer[num2++];
					if (this.driver.IsSpecialKey(c))
					{
						if (num3 > 0)
						{
							try
							{
								base.Write(buffer, index, num3);
							}
							catch (IOException)
							{
							}
							num3 = 0;
						}
						this.driver.WriteSpecialKey(c);
						index = num2;
					}
					else
					{
						num3++;
					}
				}
				while (num2 < num);
				if (num3 > 0)
				{
					try
					{
						base.Write(buffer, index, num3);
					}
					catch (IOException)
					{
					}
				}
			}
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x0012EEA8 File Offset: 0x0012D0A8
		public override void Write(char val)
		{
			lock (this)
			{
				try
				{
					if (this.driver.IsSpecialKey(val))
					{
						this.driver.WriteSpecialKey(val);
					}
					else
					{
						this.InternalWriteChar(val);
					}
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x0012EF10 File Offset: 0x0012D110
		public void InternalWriteString(string val)
		{
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x0012EF3C File Offset: 0x0012D13C
		public void InternalWriteChar(char val)
		{
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x0012EF68 File Offset: 0x0012D168
		public void InternalWriteChars(char[] buffer, int n)
		{
			try
			{
				base.Write(buffer, 0, n);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x0012EF94 File Offset: 0x0012D194
		public override void Write(char[] val)
		{
			this.Write(val, 0, val.Length);
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x0012EFA4 File Offset: 0x0012D1A4
		public override void Write(string val)
		{
			if (val == null)
			{
				return;
			}
			if (this.driver.Initialized)
			{
				this.Write(val.ToCharArray());
				return;
			}
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x0012EFEC File Offset: 0x0012D1EC
		public override void WriteLine(string val)
		{
			this.Write(val);
			this.Write(this.NewLine);
		}

		// Token: 0x0400356F RID: 13679
		private TermInfoDriver driver;
	}
}
