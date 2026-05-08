using System;

namespace Steamworks
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	public struct CGameID : IEquatable<CGameID>, IComparable<CGameID>
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x0000EC3D File Offset: 0x0000CE3D
		public CGameID(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0000EC46 File Offset: 0x0000CE46
		public CGameID(AppId_t nAppID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0000EC57 File Offset: 0x0000CE57
		public CGameID(AppId_t nAppID, uint nModID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
			this.SetType(CGameID.EGameIDType.k_EGameIDTypeGameMod);
			this.SetModID(nModID);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0000EC76 File Offset: 0x0000CE76
		public bool IsSteamApp()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeApp;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0000EC81 File Offset: 0x0000CE81
		public bool IsMod()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeGameMod;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public bool IsShortcut()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeShortcut;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0000EC97 File Offset: 0x0000CE97
		public bool IsP2PFile()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeP2P;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0000ECA2 File Offset: 0x0000CEA2
		public AppId_t AppID()
		{
			return new AppId_t((uint)(this.m_GameID & 16777215UL));
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
		public CGameID.EGameIDType Type()
		{
			return (CGameID.EGameIDType)((this.m_GameID >> 24) & 255UL);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0000ECCA File Offset: 0x0000CECA
		public uint ModID()
		{
			return (uint)((this.m_GameID >> 32) & (ulong)(-1));
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public bool IsValid()
		{
			switch (this.Type())
			{
			case CGameID.EGameIDType.k_EGameIDTypeApp:
				return this.AppID() != AppId_t.Invalid;
			case CGameID.EGameIDType.k_EGameIDTypeGameMod:
				return this.AppID() != AppId_t.Invalid && (this.ModID() & 2147483648U) > 0U;
			case CGameID.EGameIDType.k_EGameIDTypeShortcut:
				return (this.ModID() & 2147483648U) > 0U;
			case CGameID.EGameIDType.k_EGameIDTypeP2P:
				return this.AppID() == AppId_t.Invalid && (this.ModID() & 2147483648U) > 0U;
			default:
				return false;
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0000ED72 File Offset: 0x0000CF72
		public void Reset()
		{
			this.m_GameID = 0UL;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0000EC3D File Offset: 0x0000CE3D
		public void Set(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		private void SetAppID(AppId_t other)
		{
			this.m_GameID = (this.m_GameID & 18446744073692774400UL) | ((ulong)(uint)other & 16777215UL);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		private void SetType(CGameID.EGameIDType other)
		{
			this.m_GameID = (this.m_GameID & 18446744069431361535UL) | (ulong)((ulong)((long)other & 255L) << 24);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0000EDC5 File Offset: 0x0000CFC5
		private void SetModID(uint other)
		{
			this.m_GameID = (this.m_GameID & (ulong)(-1)) | (((ulong)other & (ulong)(-1)) << 32);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000EDDF File Offset: 0x0000CFDF
		public override string ToString()
		{
			return this.m_GameID.ToString();
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		public override bool Equals(object other)
		{
			return other is CGameID && this == (CGameID)other;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override int GetHashCode()
		{
			return this.m_GameID.GetHashCode();
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0000EE16 File Offset: 0x0000D016
		public static bool operator ==(CGameID x, CGameID y)
		{
			return x.m_GameID == y.m_GameID;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0000EE26 File Offset: 0x0000D026
		public static bool operator !=(CGameID x, CGameID y)
		{
			return !(x == y);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000EE32 File Offset: 0x0000D032
		public static explicit operator CGameID(ulong value)
		{
			return new CGameID(value);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0000EE3A File Offset: 0x0000D03A
		public static explicit operator ulong(CGameID that)
		{
			return that.m_GameID;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0000EE16 File Offset: 0x0000D016
		public bool Equals(CGameID other)
		{
			return this.m_GameID == other.m_GameID;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0000EE42 File Offset: 0x0000D042
		public int CompareTo(CGameID other)
		{
			return this.m_GameID.CompareTo(other.m_GameID);
		}

		// Token: 0x04000AAD RID: 2733
		public ulong m_GameID;

		// Token: 0x020001EF RID: 495
		public enum EGameIDType
		{
			// Token: 0x04000B5A RID: 2906
			k_EGameIDTypeApp,
			// Token: 0x04000B5B RID: 2907
			k_EGameIDTypeGameMod,
			// Token: 0x04000B5C RID: 2908
			k_EGameIDTypeShortcut,
			// Token: 0x04000B5D RID: 2909
			k_EGameIDTypeP2P
		}
	}
}
