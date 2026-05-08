using System;
using System.Collections.Generic;
using System.Threading;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000144 RID: 324
	public class AchievementsSocialModule : AchievementsSocialModule
	{
		// Token: 0x06001CAE RID: 7342 RVA: 0x004FF60E File Offset: 0x004FD80E
		public override void Initialize()
		{
			this._userStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
			SteamUserStats.RequestCurrentStats();
			while (!this._areStatsReceived)
			{
				CoreSocialModule.Pulse();
				Thread.Sleep(10);
			}
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x004FF643 File Offset: 0x004FD843
		public override void Shutdown()
		{
			this._userStatsReceived.Unregister();
			this.StoreStats();
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x004FF658 File Offset: 0x004FD858
		public override bool IsAchievementCompleted(string name)
		{
			bool flag;
			return SteamUserStats.GetAchievement(name, ref flag) && flag;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x004FF670 File Offset: 0x004FD870
		public override byte[] GetEncryptionKey()
		{
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(SteamUser.GetSteamID().m_SteamID);
			Array.Copy(bytes, array, 8);
			Array.Copy(bytes, 0, array, 8, 8);
			return array;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x004FF6A6 File Offset: 0x004FD8A6
		public override string GetSavePath()
		{
			return "/achievements-steam.dat";
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x004FF6B0 File Offset: 0x004FD8B0
		private int GetIntStat(string name)
		{
			int num;
			if (this._intStatCache.TryGetValue(name, out num))
			{
				return num;
			}
			if (SteamUserStats.GetStat(name, ref num))
			{
				this._intStatCache.Add(name, num);
			}
			return num;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x004FF6E8 File Offset: 0x004FD8E8
		private float GetFloatStat(string name)
		{
			float num;
			if (this._floatStatCache.TryGetValue(name, out num))
			{
				return num;
			}
			if (SteamUserStats.GetStat(name, ref num))
			{
				this._floatStatCache.Add(name, num);
			}
			return num;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x004FF71F File Offset: 0x004FD91F
		private bool SetFloatStat(string name, float value)
		{
			this._floatStatCache[name] = value;
			return SteamUserStats.SetStat(name, value);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x004FF735 File Offset: 0x004FD935
		public override void UpdateIntStat(string name, int value)
		{
			if (this.GetIntStat(name) < value)
			{
				this.SetIntStat(name, value);
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x004FF74A File Offset: 0x004FD94A
		private bool SetIntStat(string name, int value)
		{
			this._intStatCache[name] = value;
			return SteamUserStats.SetStat(name, value);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x004FF760 File Offset: 0x004FD960
		public override void UpdateFloatStat(string name, float value)
		{
			if (this.GetFloatStat(name) < value)
			{
				this.SetFloatStat(name, value);
			}
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x004FF775 File Offset: 0x004FD975
		public override void StoreStats()
		{
			SteamUserStats.StoreStats();
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x004FF77D File Offset: 0x004FD97D
		public override void CompleteAchievement(string name)
		{
			SteamUserStats.SetAchievement(name);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x004FF786 File Offset: 0x004FD986
		private void OnUserStatsReceived(UserStatsReceived_t results)
		{
			if (results.m_nGameID == 105600UL && results.m_steamIDUser == SteamUser.GetSteamID())
			{
				this._areStatsReceived = true;
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x004FF7AF File Offset: 0x004FD9AF
		public AchievementsSocialModule()
		{
		}

		// Token: 0x040015E9 RID: 5609
		private const string FILE_NAME = "/achievements-steam.dat";

		// Token: 0x040015EA RID: 5610
		private Callback<UserStatsReceived_t> _userStatsReceived;

		// Token: 0x040015EB RID: 5611
		private bool _areStatsReceived;

		// Token: 0x040015EC RID: 5612
		private Dictionary<string, int> _intStatCache = new Dictionary<string, int>();

		// Token: 0x040015ED RID: 5613
		private Dictionary<string, float> _floatStatCache = new Dictionary<string, float>();
	}
}
