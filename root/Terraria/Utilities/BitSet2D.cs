using System;
using Microsoft.Xna.Framework;

namespace Terraria.Utilities
{
	// Token: 0x020000CB RID: 203
	public class BitSet2D
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x004E0F6C File Offset: 0x004DF16C
		public void Reset(Point center, int maxDist)
		{
			this.size = maxDist * 2 + 1;
			this.offset = new Point(center.X - maxDist, center.Y - maxDist);
			int num = this.size * this.size + 63 >> 6;
			if (this.bits == null || this.bits.Length < num)
			{
				Array.Resize<Bits64>(ref this.bits, num);
			}
			Array.Clear(this.bits, 0, this.bits.Length);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x004E0FE8 File Offset: 0x004DF1E8
		private int Coord(Point p)
		{
			int num = p.X - this.offset.X;
			return (p.Y - this.offset.Y) * this.size + num;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x004E1024 File Offset: 0x004DF224
		public bool InBounds(Point p)
		{
			int num = p.X - this.offset.X;
			int num2 = p.Y - this.offset.Y;
			return num >= 0 && num < this.size && num2 >= 0 && num2 < this.size;
		}

		// Token: 0x170002A9 RID: 681
		public bool this[Point p]
		{
			get
			{
				int num = this.Coord(p);
				return this.bits[num >> 6][num & 63];
			}
			set
			{
				int num = this.Coord(p);
				this.bits[num >> 6][num & 63] = value;
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x004E10D0 File Offset: 0x004DF2D0
		public bool Add(Point p)
		{
			int num = this.Coord(p);
			if (this.bits[num >> 6][num & 63])
			{
				return false;
			}
			this.bits[num >> 6][num & 63] = true;
			return true;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x004E111C File Offset: 0x004DF31C
		public bool Remove(Point p)
		{
			int num = this.Coord(p);
			if (!this.bits[num >> 6][num & 63])
			{
				return false;
			}
			this.bits[num >> 6][num & 63] = false;
			return true;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0000357B File Offset: 0x0000177B
		public BitSet2D()
		{
		}

		// Token: 0x040012BB RID: 4795
		private Point offset;

		// Token: 0x040012BC RID: 4796
		private int size;

		// Token: 0x040012BD RID: 4797
		private Bits64[] bits;
	}
}
