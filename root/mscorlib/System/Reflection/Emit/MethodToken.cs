using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000907 RID: 2311
	[ComVisible(true)]
	[Serializable]
	public readonly struct MethodToken : IEquatable<MethodToken>
	{
		// Token: 0x060050CF RID: 20687 RVA: 0x000FE3A5 File Offset: 0x000FC5A5
		internal MethodToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x000FE3B0 File Offset: 0x000FC5B0
		public override bool Equals(object obj)
		{
			bool flag = obj is MethodToken;
			if (flag)
			{
				MethodToken methodToken = (MethodToken)obj;
				flag = this.tokValue == methodToken.tokValue;
			}
			return flag;
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x000FE3E1 File Offset: 0x000FC5E1
		public bool Equals(MethodToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x000FE3F1 File Offset: 0x000FC5F1
		public static bool operator ==(MethodToken a, MethodToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x000FE404 File Offset: 0x000FC604
		public static bool operator !=(MethodToken a, MethodToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x000FE41A File Offset: 0x000FC61A
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060050D5 RID: 20693 RVA: 0x000FE41A File Offset: 0x000FC61A
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x00004088 File Offset: 0x00002288
		// Note: this type is marked as 'beforefieldinit'.
		static MethodToken()
		{
		}

		// Token: 0x04003157 RID: 12631
		internal readonly int tokValue;

		// Token: 0x04003158 RID: 12632
		public static readonly MethodToken Empty;
	}
}
