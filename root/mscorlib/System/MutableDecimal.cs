using System;

namespace System
{
	// Token: 0x0200018F RID: 399
	internal struct MutableDecimal
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0004E279 File Offset: 0x0004C479
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x0004E28A File Offset: 0x0004C48A
		public bool IsNegative
		{
			get
			{
				return (this.Flags & 2147483648U) > 0U;
			}
			set
			{
				this.Flags = (this.Flags & 2147483647U) | (value ? 2147483648U : 0U);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0004E2AA File Offset: 0x0004C4AA
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x0004E2B6 File Offset: 0x0004C4B6
		public int Scale
		{
			get
			{
				return (int)((byte)(this.Flags >> 16));
			}
			set
			{
				this.Flags = (this.Flags & 4278255615U) | (uint)((uint)value << 16);
			}
		}

		// Token: 0x04001276 RID: 4726
		public uint Flags;

		// Token: 0x04001277 RID: 4727
		public uint High;

		// Token: 0x04001278 RID: 4728
		public uint Low;

		// Token: 0x04001279 RID: 4729
		public uint Mid;

		// Token: 0x0400127A RID: 4730
		private const uint SignMask = 2147483648U;

		// Token: 0x0400127B RID: 4731
		private const uint ScaleMask = 16711680U;

		// Token: 0x0400127C RID: 4732
		private const int ScaleShift = 16;
	}
}
