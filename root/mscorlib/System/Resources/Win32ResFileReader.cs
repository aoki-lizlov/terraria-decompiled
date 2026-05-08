using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Resources
{
	// Token: 0x02000849 RID: 2121
	internal class Win32ResFileReader
	{
		// Token: 0x060047D3 RID: 18387 RVA: 0x000ED600 File Offset: 0x000EB800
		public Win32ResFileReader(Stream s)
		{
			this.res_file = s;
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x000ED610 File Offset: 0x000EB810
		private int read_int16()
		{
			int num = this.res_file.ReadByte();
			if (num == -1)
			{
				return -1;
			}
			int num2 = this.res_file.ReadByte();
			if (num2 == -1)
			{
				return -1;
			}
			return num | (num2 << 8);
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x000ED648 File Offset: 0x000EB848
		private int read_int32()
		{
			int num = this.read_int16();
			if (num == -1)
			{
				return -1;
			}
			int num2 = this.read_int16();
			if (num2 == -1)
			{
				return -1;
			}
			return num | (num2 << 16);
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x000ED675 File Offset: 0x000EB875
		private bool read_padding()
		{
			while (this.res_file.Position % 4L != 0L)
			{
				if (this.read_int16() == -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x000ED698 File Offset: 0x000EB898
		private NameOrId read_ordinal()
		{
			if ((this.read_int16() & 65535) != 0)
			{
				return new NameOrId(this.read_int16());
			}
			byte[] array = new byte[16];
			int num = 0;
			for (;;)
			{
				int num2 = this.read_int16();
				if (num2 == 0)
				{
					break;
				}
				if (num == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, array2, array.Length);
					array = array2;
				}
				array[num] = (byte)(num2 >> 8);
				array[num + 1] = (byte)(num2 & 255);
				num += 2;
			}
			return new NameOrId(new string(Encoding.Unicode.GetChars(array, 0, num)));
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x000ED724 File Offset: 0x000EB924
		public ICollection ReadResources()
		{
			ArrayList arrayList = new ArrayList();
			while (this.read_padding())
			{
				int num = this.read_int32();
				if (num == -1)
				{
					break;
				}
				this.read_int32();
				NameOrId nameOrId = this.read_ordinal();
				NameOrId nameOrId2 = this.read_ordinal();
				if (!this.read_padding())
				{
					break;
				}
				this.read_int32();
				this.read_int16();
				int num2 = this.read_int16();
				this.read_int32();
				this.read_int32();
				if (num != 0)
				{
					byte[] array = new byte[num];
					if (this.res_file.Read(array, 0, num) != num)
					{
						break;
					}
					arrayList.Add(new Win32EncodedResource(nameOrId, nameOrId2, num2, array));
				}
			}
			return arrayList;
		}

		// Token: 0x04002DBE RID: 11710
		private Stream res_file;
	}
}
