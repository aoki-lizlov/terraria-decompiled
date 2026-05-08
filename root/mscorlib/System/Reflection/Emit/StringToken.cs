using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000916 RID: 2326
	[ComVisible(true)]
	[Serializable]
	public readonly struct StringToken : IEquatable<StringToken>
	{
		// Token: 0x060051D8 RID: 20952 RVA: 0x0010286A File Offset: 0x00100A6A
		internal StringToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x00102874 File Offset: 0x00100A74
		public override bool Equals(object obj)
		{
			bool flag = obj is StringToken;
			if (flag)
			{
				StringToken stringToken = (StringToken)obj;
				flag = this.tokValue == stringToken.tokValue;
			}
			return flag;
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x001028A5 File Offset: 0x00100AA5
		public bool Equals(StringToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x001028B5 File Offset: 0x00100AB5
		public static bool operator ==(StringToken a, StringToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x001028C8 File Offset: 0x00100AC8
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x001028DE File Offset: 0x00100ADE
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060051DE RID: 20958 RVA: 0x001028DE File Offset: 0x00100ADE
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x04003299 RID: 12953
		internal readonly int tokValue;
	}
}
