using System;

namespace Steamworks
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	public struct SteamInventoryUpdateHandle_t : IEquatable<SteamInventoryUpdateHandle_t>, IComparable<SteamInventoryUpdateHandle_t>
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x0000F7AD File Offset: 0x0000D9AD
		public SteamInventoryUpdateHandle_t(ulong value)
		{
			this.m_SteamInventoryUpdateHandle = value;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0000F7B6 File Offset: 0x0000D9B6
		public override string ToString()
		{
			return this.m_SteamInventoryUpdateHandle.ToString();
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0000F7C3 File Offset: 0x0000D9C3
		public override bool Equals(object other)
		{
			return other is SteamInventoryUpdateHandle_t && this == (SteamInventoryUpdateHandle_t)other;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public override int GetHashCode()
		{
			return this.m_SteamInventoryUpdateHandle.GetHashCode();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0000F7ED File Offset: 0x0000D9ED
		public static bool operator ==(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return x.m_SteamInventoryUpdateHandle == y.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0000F7FD File Offset: 0x0000D9FD
		public static bool operator !=(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0000F809 File Offset: 0x0000DA09
		public static explicit operator SteamInventoryUpdateHandle_t(ulong value)
		{
			return new SteamInventoryUpdateHandle_t(value);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0000F811 File Offset: 0x0000DA11
		public static explicit operator ulong(SteamInventoryUpdateHandle_t that)
		{
			return that.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000F7ED File Offset: 0x0000D9ED
		public bool Equals(SteamInventoryUpdateHandle_t other)
		{
			return this.m_SteamInventoryUpdateHandle == other.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0000F819 File Offset: 0x0000DA19
		public int CompareTo(SteamInventoryUpdateHandle_t other)
		{
			return this.m_SteamInventoryUpdateHandle.CompareTo(other.m_SteamInventoryUpdateHandle);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000F82C File Offset: 0x0000DA2C
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryUpdateHandle_t()
		{
		}

		// Token: 0x04000AD3 RID: 2771
		public static readonly SteamInventoryUpdateHandle_t Invalid = new SteamInventoryUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000AD4 RID: 2772
		public ulong m_SteamInventoryUpdateHandle;
	}
}
