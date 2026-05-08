using System;

namespace ReLogic.Utilities
{
	// Token: 0x02000007 RID: 7
	public struct SlotId
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002658 File Offset: 0x00000858
		public bool IsValid
		{
			get
			{
				return (this.Value & 65535U) != 65535U;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002670 File Offset: 0x00000870
		internal bool IsActive
		{
			get
			{
				return (this.Value & 2147483648U) != 0U && this.IsValid;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002688 File Offset: 0x00000888
		internal uint Index
		{
			get
			{
				return this.Value & 65535U;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002696 File Offset: 0x00000896
		internal uint Key
		{
			get
			{
				return this.Value & 2147418112U;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026A4 File Offset: 0x000008A4
		internal SlotId ToInactive(uint freeHead)
		{
			return new SlotId(this.Key | freeHead);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026B4 File Offset: 0x000008B4
		internal SlotId ToActive(uint index)
		{
			uint num = 2147418112U & (this.Key + 65536U);
			return new SlotId(2147483648U | num | index);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026E2 File Offset: 0x000008E2
		public SlotId(uint value)
		{
			this.Value = value;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026EB File Offset: 0x000008EB
		public override bool Equals(object obj)
		{
			return obj is SlotId && ((SlotId)obj).Value == this.Value;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000270C File Offset: 0x0000090C
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002727 File Offset: 0x00000927
		public static bool operator ==(SlotId lhs, SlotId rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002737 File Offset: 0x00000937
		public static bool operator !=(SlotId lhs, SlotId rhs)
		{
			return lhs.Value != rhs.Value;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000274A File Offset: 0x0000094A
		public float ToFloat()
		{
			return ReinterpretCast.UIntAsFloat(this.Value);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002757 File Offset: 0x00000957
		public static SlotId FromFloat(float value)
		{
			return new SlotId(ReinterpretCast.FloatAsUInt(value));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002764 File Offset: 0x00000964
		// Note: this type is marked as 'beforefieldinit'.
		static SlotId()
		{
		}

		// Token: 0x04000008 RID: 8
		public static readonly SlotId Invalid = new SlotId(65535U);

		// Token: 0x04000009 RID: 9
		private const uint KEY_INC = 65536U;

		// Token: 0x0400000A RID: 10
		private const uint INDEX_MASK = 65535U;

		// Token: 0x0400000B RID: 11
		private const uint ACTIVE_MASK = 2147483648U;

		// Token: 0x0400000C RID: 12
		private const uint KEY_MASK = 2147418112U;

		// Token: 0x0400000D RID: 13
		public readonly uint Value;
	}
}
