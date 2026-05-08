using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F3 RID: 2291
	[ComVisible(true)]
	[Serializable]
	public readonly struct EventToken : IEquatable<EventToken>
	{
		// Token: 0x06004F6B RID: 20331 RVA: 0x000FA481 File Offset: 0x000F8681
		internal EventToken(int val)
		{
			this.tokValue = val;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x000FA48C File Offset: 0x000F868C
		public override bool Equals(object obj)
		{
			bool flag = obj is EventToken;
			if (flag)
			{
				EventToken eventToken = (EventToken)obj;
				flag = this.tokValue == eventToken.tokValue;
			}
			return flag;
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x000FA4BD File Offset: 0x000F86BD
		public bool Equals(EventToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x000FA4CD File Offset: 0x000F86CD
		public static bool operator ==(EventToken a, EventToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x000FA4E0 File Offset: 0x000F86E0
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x000FA4F6 File Offset: 0x000F86F6
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004F71 RID: 20337 RVA: 0x000FA4F6 File Offset: 0x000F86F6
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x00004088 File Offset: 0x00002288
		// Note: this type is marked as 'beforefieldinit'.
		static EventToken()
		{
		}

		// Token: 0x040030DA RID: 12506
		internal readonly int tokValue;

		// Token: 0x040030DB RID: 12507
		public static readonly EventToken Empty;
	}
}
