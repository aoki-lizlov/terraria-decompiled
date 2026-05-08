using System;
using System.Threading;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000125 RID: 293
	public class AchievementsSocialModule : AchievementsSocialModule
	{
		// Token: 0x06001B88 RID: 7048 RVA: 0x004FB5C8 File Offset: 0x004F97C8
		public override void Initialize()
		{
			this._callbackHelper.RegisterCallback(2001, new RailEventCallBackHandler(this.RailEventCallBack));
			this._callbackHelper.RegisterCallback(2101, new RailEventCallBackHandler(this.RailEventCallBack));
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerStats != null && myPlayerAchievement != null)
			{
				myPlayerStats.AsyncRequestStats("");
				myPlayerAchievement.AsyncRequestAchievement("");
				while (!this._areStatsReceived && !this._areAchievementReceived)
				{
					CoreSocialModule.RailEventTick();
					Thread.Sleep(10);
				}
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x004FB657 File Offset: 0x004F9857
		public override void Shutdown()
		{
			this.StoreStats();
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x004FB660 File Offset: 0x004F9860
		private IRailPlayerStats GetMyPlayerStats()
		{
			if (this._playerStats == null)
			{
				IRailStatisticHelper railStatisticHelper = rail_api.RailFactory().RailStatisticHelper();
				if (railStatisticHelper != null)
				{
					this._playerStats = railStatisticHelper.CreatePlayerStats(new RailID
					{
						id_ = 0UL
					});
				}
			}
			return this._playerStats;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x004FB6A4 File Offset: 0x004F98A4
		private IRailPlayerAchievement GetMyPlayerAchievement()
		{
			if (this._playerAchievement == null)
			{
				IRailAchievementHelper railAchievementHelper = rail_api.RailFactory().RailAchievementHelper();
				if (railAchievementHelper != null)
				{
					this._playerAchievement = railAchievementHelper.CreatePlayerAchievement(new RailID
					{
						id_ = 0UL
					});
				}
			}
			return this._playerAchievement;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x004FB6E8 File Offset: 0x004F98E8
		public void RailEventCallBack(RAILEventID eventId, EventBase data)
		{
			if (eventId == 2001)
			{
				this._areStatsReceived = true;
				return;
			}
			if (eventId != 2101)
			{
				return;
			}
			this._areAchievementReceived = true;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x004FB70C File Offset: 0x004F990C
		public override bool IsAchievementCompleted(string name)
		{
			bool flag = false;
			RailResult railResult = 1;
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement != null)
			{
				railResult = myPlayerAchievement.HasAchieved(name, ref flag);
			}
			return flag && railResult == 0;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x004FB73C File Offset: 0x004F993C
		public override byte[] GetEncryptionKey()
		{
			RailComparableID railID = rail_api.RailFactory().RailPlayer().GetRailID();
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(railID.id_);
			Array.Copy(bytes, array, 8);
			Array.Copy(bytes, 0, array, 8, 8);
			return array;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x004FB77C File Offset: 0x004F997C
		public override string GetSavePath()
		{
			return "/achievements-wegame.dat";
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x004FB784 File Offset: 0x004F9984
		private int GetIntStat(string name)
		{
			int num = 0;
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				myPlayerStats.GetStatValue(name, ref num);
			}
			return num;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x004FB7A8 File Offset: 0x004F99A8
		private float GetFloatStat(string name)
		{
			double num = 0.0;
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				myPlayerStats.GetStatValue(name, ref num);
			}
			return (float)num;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x004FB7D8 File Offset: 0x004F99D8
		private bool SetFloatStat(string name, float value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			RailResult railResult = 1;
			if (myPlayerStats != null)
			{
				railResult = myPlayerStats.SetStatValue(name, (double)value);
			}
			return railResult == 0;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x004FB800 File Offset: 0x004F9A00
		public override void UpdateIntStat(string name, int value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				int num = 0;
				if (myPlayerStats.GetStatValue(name, ref num) == null && num < value)
				{
					myPlayerStats.SetStatValue(name, value);
				}
			}
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x004FB834 File Offset: 0x004F9A34
		private bool SetIntStat(string name, int value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			RailResult railResult = 1;
			if (myPlayerStats != null)
			{
				railResult = myPlayerStats.SetStatValue(name, value);
			}
			return railResult == 0;
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x004FB85C File Offset: 0x004F9A5C
		public override void UpdateFloatStat(string name, float value)
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				double num = 0.0;
				if (myPlayerStats.GetStatValue(name, ref num) == null && (float)num < value)
				{
					myPlayerStats.SetStatValue(name, (double)value);
				}
			}
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x004FB897 File Offset: 0x004F9A97
		public override void StoreStats()
		{
			this.SaveStats();
			this.SaveAchievement();
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x004FB8A8 File Offset: 0x004F9AA8
		private void SaveStats()
		{
			IRailPlayerStats myPlayerStats = this.GetMyPlayerStats();
			if (myPlayerStats != null)
			{
				myPlayerStats.AsyncStoreStats("");
			}
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x004FB8CC File Offset: 0x004F9ACC
		private void SaveAchievement()
		{
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement != null)
			{
				myPlayerAchievement.AsyncStoreAchievement("");
			}
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x004FB8F0 File Offset: 0x004F9AF0
		public override void CompleteAchievement(string name)
		{
			IRailPlayerAchievement myPlayerAchievement = this.GetMyPlayerAchievement();
			if (myPlayerAchievement != null)
			{
				myPlayerAchievement.MakeAchievement(name);
			}
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x004FB90F File Offset: 0x004F9B0F
		public AchievementsSocialModule()
		{
		}

		// Token: 0x04001587 RID: 5511
		private const string FILE_NAME = "/achievements-wegame.dat";

		// Token: 0x04001588 RID: 5512
		private bool _areStatsReceived;

		// Token: 0x04001589 RID: 5513
		private bool _areAchievementReceived;

		// Token: 0x0400158A RID: 5514
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x0400158B RID: 5515
		private IRailPlayerAchievement _playerAchievement;

		// Token: 0x0400158C RID: 5516
		private IRailPlayerStats _playerStats;
	}
}
