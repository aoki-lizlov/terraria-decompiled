using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F6 RID: 2294
	[ComVisible(true)]
	[Serializable]
	public readonly struct FieldToken : IEquatable<FieldToken>
	{
		// Token: 0x06004FA0 RID: 20384 RVA: 0x000FA8D6 File Offset: 0x000F8AD6
		internal FieldToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x000FA8E0 File Offset: 0x000F8AE0
		public override bool Equals(object obj)
		{
			bool flag = obj is FieldToken;
			if (flag)
			{
				FieldToken fieldToken = (FieldToken)obj;
				flag = this.tokValue == fieldToken.tokValue;
			}
			return flag;
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x000FA911 File Offset: 0x000F8B11
		public bool Equals(FieldToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x000FA921 File Offset: 0x000F8B21
		public static bool operator ==(FieldToken a, FieldToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x000FA934 File Offset: 0x000F8B34
		public static bool operator !=(FieldToken a, FieldToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x000FA94A File Offset: 0x000F8B4A
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x000FA94A File Offset: 0x000F8B4A
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x00004088 File Offset: 0x00002288
		// Note: this type is marked as 'beforefieldinit'.
		static FieldToken()
		{
		}

		// Token: 0x040030EA RID: 12522
		internal readonly int tokValue;

		// Token: 0x040030EB RID: 12523
		public static readonly FieldToken Empty;
	}
}
