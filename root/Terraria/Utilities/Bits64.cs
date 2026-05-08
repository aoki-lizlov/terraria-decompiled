using System;

namespace Terraria.Utilities
{
	// Token: 0x020000C9 RID: 201
	public struct Bits64
	{
		// Token: 0x170002A6 RID: 678
		public bool this[int i]
		{
			get
			{
				return (this.v & (1UL << i)) > 0UL;
			}
			set
			{
				if (value)
				{
					this.v |= 1UL << i;
					return;
				}
				this.v &= ~(1UL << i);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x004E0D99 File Offset: 0x004DEF99
		public bool IsEmpty
		{
			get
			{
				return this.v == 0UL;
			}
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x004E0DA5 File Offset: 0x004DEFA5
		public static implicit operator ulong(Bits64 b)
		{
			return b.v;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x004E0DB0 File Offset: 0x004DEFB0
		public static implicit operator Bits64(ulong v)
		{
			return new Bits64
			{
				v = v
			};
		}

		// Token: 0x040012B9 RID: 4793
		private ulong v;
	}
}
