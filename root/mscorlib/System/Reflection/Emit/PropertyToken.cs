using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000912 RID: 2322
	[ComVisible(true)]
	[Serializable]
	public readonly struct PropertyToken : IEquatable<PropertyToken>
	{
		// Token: 0x060051A6 RID: 20902 RVA: 0x001021EA File Offset: 0x001003EA
		internal PropertyToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x001021F4 File Offset: 0x001003F4
		public override bool Equals(object obj)
		{
			bool flag = obj is PropertyToken;
			if (flag)
			{
				PropertyToken propertyToken = (PropertyToken)obj;
				flag = this.tokValue == propertyToken.tokValue;
			}
			return flag;
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x00102225 File Offset: 0x00100425
		public bool Equals(PropertyToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x00102235 File Offset: 0x00100435
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x00102248 File Offset: 0x00100448
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0010225E File Offset: 0x0010045E
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x0010225E File Offset: 0x0010045E
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x00004088 File Offset: 0x00002288
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyToken()
		{
		}

		// Token: 0x04003288 RID: 12936
		internal readonly int tokValue;

		// Token: 0x04003289 RID: 12937
		public static readonly PropertyToken Empty;
	}
}
