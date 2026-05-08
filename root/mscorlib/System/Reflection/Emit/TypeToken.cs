using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000919 RID: 2329
	[ComVisible(true)]
	[Serializable]
	public readonly struct TypeToken : IEquatable<TypeToken>
	{
		// Token: 0x060052AE RID: 21166 RVA: 0x0010563C File Offset: 0x0010383C
		internal TypeToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x00105648 File Offset: 0x00103848
		public override bool Equals(object obj)
		{
			bool flag = obj is TypeToken;
			if (flag)
			{
				TypeToken typeToken = (TypeToken)obj;
				flag = this.tokValue == typeToken.tokValue;
			}
			return flag;
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x00105679 File Offset: 0x00103879
		public bool Equals(TypeToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x00105689 File Offset: 0x00103889
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0010569C File Offset: 0x0010389C
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x001056B2 File Offset: 0x001038B2
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x001056B2 File Offset: 0x001038B2
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x00004088 File Offset: 0x00002288
		// Note: this type is marked as 'beforefieldinit'.
		static TypeToken()
		{
		}

		// Token: 0x040032BC RID: 12988
		internal readonly int tokValue;

		// Token: 0x040032BD RID: 12989
		public static readonly TypeToken Empty;
	}
}
