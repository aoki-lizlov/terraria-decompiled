using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200090F RID: 2319
	[ComVisible(true)]
	[Serializable]
	public readonly struct ParameterToken : IEquatable<ParameterToken>
	{
		// Token: 0x0600516C RID: 20844 RVA: 0x00101E1A File Offset: 0x0010001A
		internal ParameterToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x00101E24 File Offset: 0x00100024
		public override bool Equals(object obj)
		{
			bool flag = obj is ParameterToken;
			if (flag)
			{
				ParameterToken parameterToken = (ParameterToken)obj;
				flag = this.tokValue == parameterToken.tokValue;
			}
			return flag;
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x00101E55 File Offset: 0x00100055
		public bool Equals(ParameterToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x00101E65 File Offset: 0x00100065
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x00101E78 File Offset: 0x00100078
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00101E8E File Offset: 0x0010008E
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06005172 RID: 20850 RVA: 0x00101E8E File Offset: 0x0010008E
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x00004088 File Offset: 0x00002288
		// Note: this type is marked as 'beforefieldinit'.
		static ParameterToken()
		{
		}

		// Token: 0x04003275 RID: 12917
		internal readonly int tokValue;

		// Token: 0x04003276 RID: 12918
		public static readonly ParameterToken Empty;
	}
}
