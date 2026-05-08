using System;
using System.Text;

namespace Terraria.Utilities
{
	// Token: 0x020000CA RID: 202
	public struct Vertical64BitStrips
	{
		// Token: 0x060017F8 RID: 6136 RVA: 0x004E0DCE File Offset: 0x004DEFCE
		public Vertical64BitStrips(int len)
		{
			this.arr = new Bits64[len];
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x004E0DDC File Offset: 0x004DEFDC
		public void Clear()
		{
			Array.Clear(this.arr, 0, this.arr.Length);
		}

		// Token: 0x170002A8 RID: 680
		public Bits64 this[int x]
		{
			get
			{
				return this.arr[x];
			}
			set
			{
				this.arr[x] = value;
			}
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x004E0E10 File Offset: 0x004DF010
		public void Expand3x3()
		{
			for (int i = 0; i < this.arr.Length - 1; i++)
			{
				Bits64[] array = this.arr;
				int num = i;
				array[num] |= this.arr[i + 1];
			}
			for (int j = this.arr.Length - 1; j > 0; j--)
			{
				Bits64[] array2 = this.arr;
				int num2 = j;
				array2[num2] |= this.arr[j - 1];
			}
			for (int k = 0; k < this.arr.Length; k++)
			{
				Bits64 bits = this.arr[k];
				this.arr[k] = (bits << 1) | bits | (bits >> 1);
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x004E0EFC File Offset: 0x004DF0FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.arr.Length * 65);
			for (int i = 0; i < 64; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append('\n');
				}
				for (int j = 0; j < this.arr.Length; j++)
				{
					stringBuilder.Append(this[j][i] ? 'x' : ' ');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040012BA RID: 4794
		private Bits64[] arr;
	}
}
