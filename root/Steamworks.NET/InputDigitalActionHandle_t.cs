using System;

namespace Steamworks
{
	// Token: 0x020001A3 RID: 419
	[Serializable]
	public struct InputDigitalActionHandle_t : IEquatable<InputDigitalActionHandle_t>, IComparable<InputDigitalActionHandle_t>
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x0000F623 File Offset: 0x0000D823
		public InputDigitalActionHandle_t(ulong value)
		{
			this.m_InputDigitalActionHandle = value;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000F62C File Offset: 0x0000D82C
		public override string ToString()
		{
			return this.m_InputDigitalActionHandle.ToString();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000F639 File Offset: 0x0000D839
		public override bool Equals(object other)
		{
			return other is InputDigitalActionHandle_t && this == (InputDigitalActionHandle_t)other;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000F656 File Offset: 0x0000D856
		public override int GetHashCode()
		{
			return this.m_InputDigitalActionHandle.GetHashCode();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000F663 File Offset: 0x0000D863
		public static bool operator ==(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return x.m_InputDigitalActionHandle == y.m_InputDigitalActionHandle;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000F673 File Offset: 0x0000D873
		public static bool operator !=(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0000F67F File Offset: 0x0000D87F
		public static explicit operator InputDigitalActionHandle_t(ulong value)
		{
			return new InputDigitalActionHandle_t(value);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000F687 File Offset: 0x0000D887
		public static explicit operator ulong(InputDigitalActionHandle_t that)
		{
			return that.m_InputDigitalActionHandle;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0000F663 File Offset: 0x0000D863
		public bool Equals(InputDigitalActionHandle_t other)
		{
			return this.m_InputDigitalActionHandle == other.m_InputDigitalActionHandle;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000F68F File Offset: 0x0000D88F
		public int CompareTo(InputDigitalActionHandle_t other)
		{
			return this.m_InputDigitalActionHandle.CompareTo(other.m_InputDigitalActionHandle);
		}

		// Token: 0x04000ACC RID: 2764
		public ulong m_InputDigitalActionHandle;
	}
}
