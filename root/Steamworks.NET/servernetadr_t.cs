using System;

namespace Steamworks
{
	// Token: 0x02000195 RID: 405
	[Serializable]
	public struct servernetadr_t
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x0000EA47 File Offset: 0x0000CC47
		public void Init(uint ip, ushort usQueryPort, ushort usConnectionPort)
		{
			this.m_unIP = ip;
			this.m_usQueryPort = usQueryPort;
			this.m_usConnectionPort = usConnectionPort;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0000EA5E File Offset: 0x0000CC5E
		public ushort GetQueryPort()
		{
			return this.m_usQueryPort;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0000EA66 File Offset: 0x0000CC66
		public void SetQueryPort(ushort usPort)
		{
			this.m_usQueryPort = usPort;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0000EA6F File Offset: 0x0000CC6F
		public ushort GetConnectionPort()
		{
			return this.m_usConnectionPort;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0000EA77 File Offset: 0x0000CC77
		public void SetConnectionPort(ushort usPort)
		{
			this.m_usConnectionPort = usPort;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0000EA80 File Offset: 0x0000CC80
		public uint GetIP()
		{
			return this.m_unIP;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0000EA88 File Offset: 0x0000CC88
		public void SetIP(uint unIP)
		{
			this.m_unIP = unIP;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0000EA91 File Offset: 0x0000CC91
		public string GetConnectionAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usConnectionPort);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public string GetQueryAddressString()
		{
			return servernetadr_t.ToString(this.m_unIP, this.m_usQueryPort);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		public static string ToString(uint unIP, ushort usPort)
		{
			return string.Format("{0}.{1}.{2}.{3}:{4}", new object[]
			{
				(ulong)(unIP >> 24) & 255UL,
				(ulong)(unIP >> 16) & 255UL,
				(ulong)(unIP >> 8) & 255UL,
				(ulong)unIP & 255UL,
				usPort
			});
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0000EB2A File Offset: 0x0000CD2A
		public static bool operator <(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP < y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort < y.m_usQueryPort);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0000EB5A File Offset: 0x0000CD5A
		public static bool operator >(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP > y.m_unIP || (x.m_unIP == y.m_unIP && x.m_usQueryPort > y.m_usQueryPort);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000EB8A File Offset: 0x0000CD8A
		public override bool Equals(object other)
		{
			return other is servernetadr_t && this == (servernetadr_t)other;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000EBA7 File Offset: 0x0000CDA7
		public override int GetHashCode()
		{
			return this.m_unIP.GetHashCode() + this.m_usQueryPort.GetHashCode() + this.m_usConnectionPort.GetHashCode();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		public static bool operator ==(servernetadr_t x, servernetadr_t y)
		{
			return x.m_unIP == y.m_unIP && x.m_usQueryPort == y.m_usQueryPort && x.m_usConnectionPort == y.m_usConnectionPort;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0000EBFA File Offset: 0x0000CDFA
		public static bool operator !=(servernetadr_t x, servernetadr_t y)
		{
			return !(x == y);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		public bool Equals(servernetadr_t other)
		{
			return this.m_unIP == other.m_unIP && this.m_usQueryPort == other.m_usQueryPort && this.m_usConnectionPort == other.m_usConnectionPort;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0000EC06 File Offset: 0x0000CE06
		public int CompareTo(servernetadr_t other)
		{
			return this.m_unIP.CompareTo(other.m_unIP) + this.m_usQueryPort.CompareTo(other.m_usQueryPort) + this.m_usConnectionPort.CompareTo(other.m_usConnectionPort);
		}

		// Token: 0x04000AAA RID: 2730
		private ushort m_usConnectionPort;

		// Token: 0x04000AAB RID: 2731
		private ushort m_usQueryPort;

		// Token: 0x04000AAC RID: 2732
		private uint m_unIP;
	}
}
