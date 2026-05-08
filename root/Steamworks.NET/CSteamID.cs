using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	[StructLayout(0, Pack = 4)]
	public struct CSteamID : IEquatable<CSteamID>, IComparable<CSteamID>
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x0000EE55 File Offset: 0x0000D055
		public CSteamID(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.Set(unAccountID, eUniverse, eAccountType);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0000EE68 File Offset: 0x0000D068
		public CSteamID(AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0000EE7D File Offset: 0x0000D07D
		public CSteamID(ulong ulSteamID)
		{
			this.m_SteamID = ulSteamID;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000EE86 File Offset: 0x0000D086
		public void Set(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			if (eAccountType == EAccountType.k_EAccountTypeClan || eAccountType == EAccountType.k_EAccountTypeGameServer)
			{
				this.SetAccountInstance(0U);
				return;
			}
			this.SetAccountInstance(1U);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		public void InstancedSet(AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			this.SetAccountInstance(unInstance);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000EED3 File Offset: 0x0000D0D3
		public void Clear()
		{
			this.m_SteamID = 0UL;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000EEDD File Offset: 0x0000D0DD
		public void CreateBlankAnonLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonGameServer);
			this.SetAccountInstance(0U);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0000EF00 File Offset: 0x0000D100
		public void CreateBlankAnonUserLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonUser);
			this.SetAccountInstance(0U);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0000EF24 File Offset: 0x0000D124
		public bool BBlankAnonAccount()
		{
			return this.GetAccountID() == new AccountID_t(0U) && this.BAnonAccount() && this.GetUnAccountInstance() == 0U;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000EF4C File Offset: 0x0000D14C
		public bool BGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0000EF62 File Offset: 0x0000D162
		public bool BPersistentGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0000EF6D File Offset: 0x0000D16D
		public bool BAnonGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0000EF78 File Offset: 0x0000D178
		public bool BContentServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeContentServer;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000EF83 File Offset: 0x0000D183
		public bool BClanAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeClan;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0000EF8E File Offset: 0x0000D18E
		public bool BChatAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000EF99 File Offset: 0x0000D199
		public bool IsLobby()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat && (this.GetUnAccountInstance() & 262144U) > 0U;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
		public bool BIndividualAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeIndividual || this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		public bool BAnonAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0000EFE3 File Offset: 0x0000D1E3
		public bool BAnonUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0000EFEF File Offset: 0x0000D1EF
		public bool BConsoleUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0000EFFB File Offset: 0x0000D1FB
		public void SetAccountID(AccountID_t other)
		{
			this.m_SteamID = (this.m_SteamID & 18446744069414584320UL) | ((ulong)(uint)other & (ulong)(-1));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0000F01E File Offset: 0x0000D21E
		public void SetAccountInstance(uint other)
		{
			this.m_SteamID = (this.m_SteamID & 18442240478377148415UL) | (((ulong)other & 1048575UL) << 32);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0000F043 File Offset: 0x0000D243
		public void SetEAccountType(EAccountType other)
		{
			this.m_SteamID = (this.m_SteamID & 18379190079298994175UL) | (ulong)((ulong)((long)other & 15L) << 52);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0000F065 File Offset: 0x0000D265
		public void SetEUniverse(EUniverse other)
		{
			this.m_SteamID = (this.m_SteamID & 72057594037927935UL) | (ulong)((ulong)((long)other & 255L) << 56);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0000F08A File Offset: 0x0000D28A
		public AccountID_t GetAccountID()
		{
			return new AccountID_t((uint)(this.m_SteamID & (ulong)(-1)));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000F09B File Offset: 0x0000D29B
		public uint GetUnAccountInstance()
		{
			return (uint)((this.m_SteamID >> 32) & 1048575UL);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0000F0AE File Offset: 0x0000D2AE
		public EAccountType GetEAccountType()
		{
			return (EAccountType)((this.m_SteamID >> 52) & 15UL);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000F0BE File Offset: 0x0000D2BE
		public EUniverse GetEUniverse()
		{
			return (EUniverse)((this.m_SteamID >> 56) & 255UL);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		public bool IsValid()
		{
			return this.GetEAccountType() > EAccountType.k_EAccountTypeInvalid && this.GetEAccountType() < EAccountType.k_EAccountTypeMax && this.GetEUniverse() > EUniverse.k_EUniverseInvalid && this.GetEUniverse() < EUniverse.k_EUniverseMax && (this.GetEAccountType() != EAccountType.k_EAccountTypeIndividual || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() <= 1U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeClan || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() == 0U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeGameServer || !(this.GetAccountID() == new AccountID_t(0U)));
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0000F176 File Offset: 0x0000D376
		public override string ToString()
		{
			return this.m_SteamID.ToString();
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0000F183 File Offset: 0x0000D383
		public override bool Equals(object other)
		{
			return other is CSteamID && this == (CSteamID)other;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public override int GetHashCode()
		{
			return this.m_SteamID.GetHashCode();
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0000F1AD File Offset: 0x0000D3AD
		public static bool operator ==(CSteamID x, CSteamID y)
		{
			return x.m_SteamID == y.m_SteamID;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0000F1BD File Offset: 0x0000D3BD
		public static bool operator !=(CSteamID x, CSteamID y)
		{
			return !(x == y);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0000F1C9 File Offset: 0x0000D3C9
		public static explicit operator CSteamID(ulong value)
		{
			return new CSteamID(value);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0000F1D1 File Offset: 0x0000D3D1
		public static explicit operator ulong(CSteamID that)
		{
			return that.m_SteamID;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0000F1AD File Offset: 0x0000D3AD
		public bool Equals(CSteamID other)
		{
			return this.m_SteamID == other.m_SteamID;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0000F1D9 File Offset: 0x0000D3D9
		public int CompareTo(CSteamID other)
		{
			return this.m_SteamID.CompareTo(other.m_SteamID);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		// Note: this type is marked as 'beforefieldinit'.
		static CSteamID()
		{
		}

		// Token: 0x04000AAE RID: 2734
		public static readonly CSteamID Nil = default(CSteamID);

		// Token: 0x04000AAF RID: 2735
		public static readonly CSteamID OutofDateGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000AB0 RID: 2736
		public static readonly CSteamID LanModeGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniversePublic, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000AB1 RID: 2737
		public static readonly CSteamID NotInitYetGS = new CSteamID(new AccountID_t(1U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000AB2 RID: 2738
		public static readonly CSteamID NonSteamGS = new CSteamID(new AccountID_t(2U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000AB3 RID: 2739
		public ulong m_SteamID;
	}
}
