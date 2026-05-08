using System;

namespace System.Globalization
{
	// Token: 0x020009C7 RID: 2503
	[Serializable]
	public sealed class SortVersion : IEquatable<SortVersion>
	{
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06005B60 RID: 23392 RVA: 0x001373B9 File Offset: 0x001355B9
		public int FullVersion
		{
			get
			{
				return this.m_NlsVersion;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06005B61 RID: 23393 RVA: 0x001373C1 File Offset: 0x001355C1
		public Guid SortId
		{
			get
			{
				return this.m_SortId;
			}
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x001373C9 File Offset: 0x001355C9
		public SortVersion(int fullVersion, Guid sortId)
		{
			this.m_SortId = sortId;
			this.m_NlsVersion = fullVersion;
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x001373E0 File Offset: 0x001355E0
		internal SortVersion(int nlsVersion, int effectiveId, Guid customVersion)
		{
			this.m_NlsVersion = nlsVersion;
			if (customVersion == Guid.Empty)
			{
				byte b = (byte)(effectiveId >> 24);
				byte b2 = (byte)((effectiveId & 16711680) >> 16);
				byte b3 = (byte)((effectiveId & 65280) >> 8);
				byte b4 = (byte)(effectiveId & 255);
				customVersion = new Guid(0, 0, 0, 0, 0, 0, 0, b, b2, b3, b4);
			}
			this.m_SortId = customVersion;
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x00137448 File Offset: 0x00135648
		public override bool Equals(object obj)
		{
			SortVersion sortVersion = obj as SortVersion;
			return sortVersion != null && this.Equals(sortVersion);
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x0013746E File Offset: 0x0013566E
		public bool Equals(SortVersion other)
		{
			return !(other == null) && this.m_NlsVersion == other.m_NlsVersion && this.m_SortId == other.m_SortId;
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x0013749C File Offset: 0x0013569C
		public override int GetHashCode()
		{
			return (this.m_NlsVersion * 7) | this.m_SortId.GetHashCode();
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x001374B8 File Offset: 0x001356B8
		public static bool operator ==(SortVersion left, SortVersion right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null || right.Equals(left);
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x001374D1 File Offset: 0x001356D1
		public static bool operator !=(SortVersion left, SortVersion right)
		{
			return !(left == right);
		}

		// Token: 0x04003710 RID: 14096
		private int m_NlsVersion;

		// Token: 0x04003711 RID: 14097
		private Guid m_SortId;
	}
}
