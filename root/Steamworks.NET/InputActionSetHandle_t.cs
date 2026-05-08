using System;

namespace Steamworks
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public struct InputActionSetHandle_t : IEquatable<InputActionSetHandle_t>, IComparable<InputActionSetHandle_t>
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x0000F525 File Offset: 0x0000D725
		public InputActionSetHandle_t(ulong value)
		{
			this.m_InputActionSetHandle = value;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0000F52E File Offset: 0x0000D72E
		public override string ToString()
		{
			return this.m_InputActionSetHandle.ToString();
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0000F53B File Offset: 0x0000D73B
		public override bool Equals(object other)
		{
			return other is InputActionSetHandle_t && this == (InputActionSetHandle_t)other;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0000F558 File Offset: 0x0000D758
		public override int GetHashCode()
		{
			return this.m_InputActionSetHandle.GetHashCode();
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0000F565 File Offset: 0x0000D765
		public static bool operator ==(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return x.m_InputActionSetHandle == y.m_InputActionSetHandle;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0000F575 File Offset: 0x0000D775
		public static bool operator !=(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000F581 File Offset: 0x0000D781
		public static explicit operator InputActionSetHandle_t(ulong value)
		{
			return new InputActionSetHandle_t(value);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0000F589 File Offset: 0x0000D789
		public static explicit operator ulong(InputActionSetHandle_t that)
		{
			return that.m_InputActionSetHandle;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0000F565 File Offset: 0x0000D765
		public bool Equals(InputActionSetHandle_t other)
		{
			return this.m_InputActionSetHandle == other.m_InputActionSetHandle;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0000F591 File Offset: 0x0000D791
		public int CompareTo(InputActionSetHandle_t other)
		{
			return this.m_InputActionSetHandle.CompareTo(other.m_InputActionSetHandle);
		}

		// Token: 0x04000ACA RID: 2762
		public ulong m_InputActionSetHandle;
	}
}
