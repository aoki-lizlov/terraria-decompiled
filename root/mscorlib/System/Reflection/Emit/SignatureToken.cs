using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000915 RID: 2325
	[ComVisible(true)]
	public readonly struct SignatureToken : IEquatable<SignatureToken>
	{
		// Token: 0x060051D0 RID: 20944 RVA: 0x001027EF File Offset: 0x001009EF
		internal SignatureToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x001027F8 File Offset: 0x001009F8
		public override bool Equals(object obj)
		{
			bool flag = obj is SignatureToken;
			if (flag)
			{
				SignatureToken signatureToken = (SignatureToken)obj;
				flag = this.tokValue == signatureToken.tokValue;
			}
			return flag;
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x00102829 File Offset: 0x00100A29
		public bool Equals(SignatureToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x00102839 File Offset: 0x00100A39
		public static bool operator ==(SignatureToken a, SignatureToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0010284C File Offset: 0x00100A4C
		public static bool operator !=(SignatureToken a, SignatureToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x00102862 File Offset: 0x00100A62
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060051D6 RID: 20950 RVA: 0x00102862 File Offset: 0x00100A62
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x00004088 File Offset: 0x00002288
		// Note: this type is marked as 'beforefieldinit'.
		static SignatureToken()
		{
		}

		// Token: 0x04003297 RID: 12951
		internal readonly int tokValue;

		// Token: 0x04003298 RID: 12952
		public static readonly SignatureToken Empty;
	}
}
