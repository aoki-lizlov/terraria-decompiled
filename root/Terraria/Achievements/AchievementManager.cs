using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Terraria.Social;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.Achievements
{
	// Token: 0x020005E4 RID: 1508
	public class AchievementManager
	{
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06003B2A RID: 15146 RVA: 0x0065A4C0 File Offset: 0x006586C0
		// (remove) Token: 0x06003B2B RID: 15147 RVA: 0x0065A4F8 File Offset: 0x006586F8
		public event Achievement.AchievementCompleted OnAchievementCompleted
		{
			[CompilerGenerated]
			add
			{
				Achievement.AchievementCompleted achievementCompleted = this.OnAchievementCompleted;
				Achievement.AchievementCompleted achievementCompleted2;
				do
				{
					achievementCompleted2 = achievementCompleted;
					Achievement.AchievementCompleted achievementCompleted3 = (Achievement.AchievementCompleted)Delegate.Combine(achievementCompleted2, value);
					achievementCompleted = Interlocked.CompareExchange<Achievement.AchievementCompleted>(ref this.OnAchievementCompleted, achievementCompleted3, achievementCompleted2);
				}
				while (achievementCompleted != achievementCompleted2);
			}
			[CompilerGenerated]
			remove
			{
				Achievement.AchievementCompleted achievementCompleted = this.OnAchievementCompleted;
				Achievement.AchievementCompleted achievementCompleted2;
				do
				{
					achievementCompleted2 = achievementCompleted;
					Achievement.AchievementCompleted achievementCompleted3 = (Achievement.AchievementCompleted)Delegate.Remove(achievementCompleted2, value);
					achievementCompleted = Interlocked.CompareExchange<Achievement.AchievementCompleted>(ref this.OnAchievementCompleted, achievementCompleted3, achievementCompleted2);
				}
				while (achievementCompleted != achievementCompleted2);
			}
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x0065A530 File Offset: 0x00658730
		public AchievementManager()
		{
			if (SocialAPI.Achievements != null)
			{
				this._savePath = SocialAPI.Achievements.GetSavePath();
				this._isCloudSave = true;
				this._cryptoKey = SocialAPI.Achievements.GetEncryptionKey();
				return;
			}
			this._savePath = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "achievements.dat";
			this._isCloudSave = false;
			this._cryptoKey = Encoding.ASCII.GetBytes("RELOGIC-TERRARIA");
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x0065A5D1 File Offset: 0x006587D1
		public void Save()
		{
			FileUtilities.ProtectedInvoke(delegate
			{
				this.Save(this._savePath, this._isCloudSave);
			});
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x0065A5E4 File Offset: 0x006587E4
		private void Save(string path, bool cloud)
		{
			object ioLock = AchievementManager._ioLock;
			lock (ioLock)
			{
				if (SocialAPI.Achievements != null)
				{
					SocialAPI.Achievements.StoreStats();
				}
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, new RijndaelManaged().CreateEncryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Write))
						{
							using (BsonWriter bsonWriter = new BsonWriter(cryptoStream))
							{
								JsonSerializer.Create(this._serializerSettings).Serialize(bsonWriter, this._achievements);
								bsonWriter.Flush();
								cryptoStream.FlushFinalBlock();
								FileUtilities.WriteAllBytes(path, memoryStream.ToArray(), cloud);
							}
						}
					}
				}
				catch (Exception ex)
				{
					FancyErrorPrinter.ShowFileSavingFailError(ex, this._savePath);
				}
			}
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x0065A6F4 File Offset: 0x006588F4
		public List<Achievement> CreateAchievementsList()
		{
			return this._achievements.Values.ToList<Achievement>();
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x0065A706 File Offset: 0x00658906
		public void Load()
		{
			this.Load(this._savePath, this._isCloudSave);
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x0065A71C File Offset: 0x0065891C
		private void Load(string path, bool cloud)
		{
			bool flag = false;
			object ioLock = AchievementManager._ioLock;
			lock (ioLock)
			{
				if (!FileUtilities.Exists(path, cloud))
				{
					return;
				}
				byte[] array = FileUtilities.ReadAllBytes(path, cloud);
				Dictionary<string, AchievementManager.StoredAchievement> dictionary = null;
				try
				{
					using (MemoryStream memoryStream = new MemoryStream(array))
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, new RijndaelManaged().CreateDecryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Read))
						{
							using (BsonReader bsonReader = new BsonReader(cryptoStream))
							{
								dictionary = JsonSerializer.Create(this._serializerSettings).Deserialize<Dictionary<string, AchievementManager.StoredAchievement>>(bsonReader);
							}
						}
					}
				}
				catch (Exception)
				{
					FileUtilities.Delete(path, cloud, false);
					return;
				}
				if (dictionary == null)
				{
					return;
				}
				foreach (KeyValuePair<string, AchievementManager.StoredAchievement> keyValuePair in dictionary)
				{
					if (this._achievements.ContainsKey(keyValuePair.Key))
					{
						this._achievements[keyValuePair.Key].Load(keyValuePair.Value.Conditions);
					}
				}
				if (SocialAPI.Achievements != null)
				{
					foreach (KeyValuePair<string, Achievement> keyValuePair2 in this._achievements)
					{
						if (keyValuePair2.Value.IsCompleted && !SocialAPI.Achievements.IsAchievementCompleted(keyValuePair2.Key))
						{
							flag = true;
							keyValuePair2.Value.ClearProgress();
						}
					}
				}
			}
			if (flag)
			{
				this.Save();
			}
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x0065A968 File Offset: 0x00658B68
		public bool Clear(string achievementName)
		{
			if (SocialAPI.Achievements != null)
			{
				return false;
			}
			Achievement achievement;
			if (!this._achievements.TryGetValue(achievementName, out achievement))
			{
				return false;
			}
			achievement.ClearProgress();
			this.Save();
			return true;
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x0065A9A0 File Offset: 0x00658BA0
		public void ClearAll()
		{
			if (SocialAPI.Achievements != null)
			{
				return;
			}
			foreach (KeyValuePair<string, Achievement> keyValuePair in this._achievements)
			{
				keyValuePair.Value.ClearProgress();
			}
			this.Save();
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x0065AA08 File Offset: 0x00658C08
		private void AchievementCompleted(Achievement achievement)
		{
			this.Save();
			if (this.OnAchievementCompleted != null)
			{
				this.OnAchievementCompleted(achievement);
			}
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x0065AA24 File Offset: 0x00658C24
		public void Register(Achievement achievement)
		{
			this._achievements.Add(achievement.Name, achievement);
			achievement.OnCompleted += this.AchievementCompleted;
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x0065AA4A File Offset: 0x00658C4A
		public void RegisterIconIndex(string achievementName, int iconIndex)
		{
			this._achievementIconIndexes.Add(achievementName, iconIndex);
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x0065AA59 File Offset: 0x00658C59
		public void RegisterAchievementCategory(string achievementName, AchievementCategory category)
		{
			this._achievements[achievementName].SetCategory(category);
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x0065AA70 File Offset: 0x00658C70
		public Achievement GetAchievement(string achievementName)
		{
			Achievement achievement;
			if (this._achievements.TryGetValue(achievementName, out achievement))
			{
				return achievement;
			}
			return null;
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x0065AA90 File Offset: 0x00658C90
		public T GetCondition<T>(string achievementName, string conditionName) where T : AchievementCondition
		{
			return this.GetCondition(achievementName, conditionName) as T;
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x0065AAA4 File Offset: 0x00658CA4
		public AchievementCondition GetCondition(string achievementName, string conditionName)
		{
			Achievement achievement;
			if (this._achievements.TryGetValue(achievementName, out achievement))
			{
				return achievement.GetCondition(conditionName);
			}
			return null;
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x0065AACC File Offset: 0x00658CCC
		public int GetIconIndex(string achievementName)
		{
			int num;
			if (this._achievementIconIndexes.TryGetValue(achievementName, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x0065AAEC File Offset: 0x00658CEC
		// Note: this type is marked as 'beforefieldinit'.
		static AchievementManager()
		{
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x0065AAF8 File Offset: 0x00658CF8
		[CompilerGenerated]
		private void <Save>b__12_0()
		{
			this.Save(this._savePath, this._isCloudSave);
		}

		// Token: 0x04005E69 RID: 24169
		private string _savePath;

		// Token: 0x04005E6A RID: 24170
		private bool _isCloudSave;

		// Token: 0x04005E6B RID: 24171
		[CompilerGenerated]
		private Achievement.AchievementCompleted OnAchievementCompleted;

		// Token: 0x04005E6C RID: 24172
		private Dictionary<string, Achievement> _achievements = new Dictionary<string, Achievement>();

		// Token: 0x04005E6D RID: 24173
		private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();

		// Token: 0x04005E6E RID: 24174
		private byte[] _cryptoKey;

		// Token: 0x04005E6F RID: 24175
		private Dictionary<string, int> _achievementIconIndexes = new Dictionary<string, int>();

		// Token: 0x04005E70 RID: 24176
		private static object _ioLock = new object();

		// Token: 0x020009CF RID: 2511
		private class StoredAchievement
		{
			// Token: 0x06004A6E RID: 19054 RVA: 0x0000357B File Offset: 0x0000177B
			public StoredAchievement()
			{
			}

			// Token: 0x040076F5 RID: 30453
			[JsonProperty]
			public Dictionary<string, JObject> Conditions;
		}
	}
}
