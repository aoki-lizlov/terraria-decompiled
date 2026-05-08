using System;

namespace Steamworks
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public struct InputAnalogActionHandle_t : IEquatable<InputAnalogActionHandle_t>, IComparable<InputAnalogActionHandle_t>
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		public InputAnalogActionHandle_t(ulong value)
		{
			this.m_InputAnalogActionHandle = value;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000F5AD File Offset: 0x0000D7AD
		public override string ToString()
		{
			return this.m_InputAnalogActionHandle.ToString();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0000F5BA File Offset: 0x0000D7BA
		public override bool Equals(object other)
		{
			return other is InputAnalogActionHandle_t && this == (InputAnalogActionHandle_t)other;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0000F5D7 File Offset: 0x0000D7D7
		public override int GetHashCode()
		{
			return this.m_InputAnalogActionHandle.GetHashCode();
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		public static bool operator ==(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return x.m_InputAnalogActionHandle == y.m_InputAnalogActionHandle;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		public static bool operator !=(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0000F600 File Offset: 0x0000D800
		public static explicit operator InputAnalogActionHandle_t(ulong value)
		{
			return new InputAnalogActionHandle_t(value);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000F608 File Offset: 0x0000D808
		public static explicit operator ulong(InputAnalogActionHandle_t that)
		{
			return that.m_InputAnalogActionHandle;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		public bool Equals(InputAnalogActionHandle_t other)
		{
			return this.m_InputAnalogActionHandle == other.m_InputAnalogActionHandle;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0000F610 File Offset: 0x0000D810
		public int CompareTo(InputAnalogActionHandle_t other)
		{
			return this.m_InputAnalogActionHandle.CompareTo(other.m_InputAnalogActionHandle);
		}

		// Token: 0x04000ACB RID: 2763
		public ulong m_InputAnalogActionHandle;
	}
}
