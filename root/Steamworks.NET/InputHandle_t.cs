using System;

namespace Steamworks
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public struct InputHandle_t : IEquatable<InputHandle_t>, IComparable<InputHandle_t>
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x0000F6A2 File Offset: 0x0000D8A2
		public InputHandle_t(ulong value)
		{
			this.m_InputHandle = value;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000F6AB File Offset: 0x0000D8AB
		public override string ToString()
		{
			return this.m_InputHandle.ToString();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0000F6B8 File Offset: 0x0000D8B8
		public override bool Equals(object other)
		{
			return other is InputHandle_t && this == (InputHandle_t)other;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
		public override int GetHashCode()
		{
			return this.m_InputHandle.GetHashCode();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0000F6E2 File Offset: 0x0000D8E2
		public static bool operator ==(InputHandle_t x, InputHandle_t y)
		{
			return x.m_InputHandle == y.m_InputHandle;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000F6F2 File Offset: 0x0000D8F2
		public static bool operator !=(InputHandle_t x, InputHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000F6FE File Offset: 0x0000D8FE
		public static explicit operator InputHandle_t(ulong value)
		{
			return new InputHandle_t(value);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000F706 File Offset: 0x0000D906
		public static explicit operator ulong(InputHandle_t that)
		{
			return that.m_InputHandle;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0000F6E2 File Offset: 0x0000D8E2
		public bool Equals(InputHandle_t other)
		{
			return this.m_InputHandle == other.m_InputHandle;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0000F70E File Offset: 0x0000D90E
		public int CompareTo(InputHandle_t other)
		{
			return this.m_InputHandle.CompareTo(other.m_InputHandle);
		}

		// Token: 0x04000ACD RID: 2765
		public ulong m_InputHandle;
	}
}
