using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000758 RID: 1880
	public struct EventRegistrationToken
	{
		// Token: 0x0600441D RID: 17437 RVA: 0x000E3565 File Offset: 0x000E1765
		internal EventRegistrationToken(ulong value)
		{
			this.m_value = value;
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x000E356E File Offset: 0x000E176E
		internal ulong Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x000E3576 File Offset: 0x000E1776
		public static bool operator ==(EventRegistrationToken left, EventRegistrationToken right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x000E358B File Offset: 0x000E178B
		public static bool operator !=(EventRegistrationToken left, EventRegistrationToken right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x000E35A4 File Offset: 0x000E17A4
		public override bool Equals(object obj)
		{
			return obj is EventRegistrationToken && ((EventRegistrationToken)obj).Value == this.Value;
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x000E35D1 File Offset: 0x000E17D1
		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		// Token: 0x04002BA5 RID: 11173
		internal ulong m_value;
	}
}
