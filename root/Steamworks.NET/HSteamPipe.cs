using System;

namespace Steamworks
{
	// Token: 0x020001CD RID: 461
	[Serializable]
	public struct HSteamPipe : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x00010B4E File Offset: 0x0000ED4E
		public HSteamPipe(int value)
		{
			this.m_HSteamPipe = value;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00010B57 File Offset: 0x0000ED57
		public override string ToString()
		{
			return this.m_HSteamPipe.ToString();
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00010B64 File Offset: 0x0000ED64
		public override bool Equals(object other)
		{
			return other is HSteamPipe && this == (HSteamPipe)other;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00010B81 File Offset: 0x0000ED81
		public override int GetHashCode()
		{
			return this.m_HSteamPipe.GetHashCode();
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00010B8E File Offset: 0x0000ED8E
		public static bool operator ==(HSteamPipe x, HSteamPipe y)
		{
			return x.m_HSteamPipe == y.m_HSteamPipe;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00010B9E File Offset: 0x0000ED9E
		public static bool operator !=(HSteamPipe x, HSteamPipe y)
		{
			return !(x == y);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00010BAA File Offset: 0x0000EDAA
		public static explicit operator HSteamPipe(int value)
		{
			return new HSteamPipe(value);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00010BB2 File Offset: 0x0000EDB2
		public static explicit operator int(HSteamPipe that)
		{
			return that.m_HSteamPipe;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00010B8E File Offset: 0x0000ED8E
		public bool Equals(HSteamPipe other)
		{
			return this.m_HSteamPipe == other.m_HSteamPipe;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00010BBA File Offset: 0x0000EDBA
		public int CompareTo(HSteamPipe other)
		{
			return this.m_HSteamPipe.CompareTo(other.m_HSteamPipe);
		}

		// Token: 0x04000B40 RID: 2880
		public int m_HSteamPipe;
	}
}
