using System;
using System.Text;

namespace Microsoft.Win32
{
	// Token: 0x0200008B RID: 139
	internal class ExpandString
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00015DAD File Offset: 0x00013FAD
		public ExpandString(string s)
		{
			this.value = s;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00015DBC File Offset: 0x00013FBC
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public string Expand()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.value.Length; i++)
			{
				if (this.value[i] == '%')
				{
					int j;
					for (j = i + 1; j < this.value.Length; j++)
					{
						if (this.value[j] == '%')
						{
							string text = this.value.Substring(i + 1, j - i - 1);
							stringBuilder.Append(Environment.GetEnvironmentVariable(text));
							i += j;
							break;
						}
					}
					if (j == this.value.Length)
					{
						stringBuilder.Append('%');
					}
				}
				else
				{
					stringBuilder.Append(this.value[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000E72 RID: 3698
		private string value;
	}
}
