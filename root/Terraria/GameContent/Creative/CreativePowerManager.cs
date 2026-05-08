using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.NetModules;
using Terraria.Net;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200031C RID: 796
	public class CreativePowerManager
	{
		// Token: 0x06002788 RID: 10120 RVA: 0x0056831F File Offset: 0x0056651F
		private CreativePowerManager()
		{
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00568340 File Offset: 0x00566540
		public void Register<T>(string nameInServerConfig) where T : ICreativePower, new()
		{
			T t = new T();
			CreativePowerManager.PowerTypeStorage<T>.Power = t;
			CreativePowerManager.PowerTypeStorage<T>.Id = this._powersCount;
			CreativePowerManager.PowerTypeStorage<T>.Name = nameInServerConfig;
			t.DefaultPermissionLevel = PowerPermissionLevel.CanBeChangedByEveryone;
			t.CurrentPermissionLevel = PowerPermissionLevel.CanBeChangedByEveryone;
			this._powersById[this._powersCount] = t;
			this._powersByName[nameInServerConfig] = t;
			t.PowerId = this._powersCount;
			t.ServerConfigName = nameInServerConfig;
			this._powersCount += 1;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x005683DF File Offset: 0x005665DF
		public T GetPower<T>() where T : ICreativePower
		{
			return CreativePowerManager.PowerTypeStorage<T>.Power;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x005683E6 File Offset: 0x005665E6
		public ushort GetPowerId<T>() where T : ICreativePower
		{
			return CreativePowerManager.PowerTypeStorage<T>.Id;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x005683ED File Offset: 0x005665ED
		public bool TryGetPower(ushort id, out ICreativePower power)
		{
			return this._powersById.TryGetValue(id, out power);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x005683FC File Offset: 0x005665FC
		public static void TryListingPermissionsFrom(string line)
		{
			int length = "journeypermission_".Length;
			if (line.Length < length)
			{
				return;
			}
			if (!line.ToLower().StartsWith("journeypermission_"))
			{
				return;
			}
			string[] array = line.Substring(length).Split(new char[] { '=' });
			if (array.Length != 2)
			{
				return;
			}
			int num;
			if (!int.TryParse(array[1].Trim(), out num))
			{
				return;
			}
			PowerPermissionLevel powerPermissionLevel = (PowerPermissionLevel)Utils.Clamp<int>(num, 0, 2);
			string text = array[0].Trim().ToLower();
			CreativePowerManager.Initialize();
			ICreativePower creativePower;
			if (!CreativePowerManager.Instance._powersByName.TryGetValue(text, out creativePower))
			{
				return;
			}
			creativePower.DefaultPermissionLevel = powerPermissionLevel;
			creativePower.CurrentPermissionLevel = powerPermissionLevel;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x005684A8 File Offset: 0x005666A8
		public static void Initialize()
		{
			if (CreativePowerManager._initialized)
			{
				return;
			}
			CreativePowerManager.Instance.Register<CreativePowers.FreezeTime>("time_setfrozen");
			CreativePowerManager.Instance.Register<CreativePowers.StartDayImmediately>("time_setdawn");
			CreativePowerManager.Instance.Register<CreativePowers.StartNoonImmediately>("time_setnoon");
			CreativePowerManager.Instance.Register<CreativePowers.StartNightImmediately>("time_setdusk");
			CreativePowerManager.Instance.Register<CreativePowers.StartMidnightImmediately>("time_setmidnight");
			CreativePowerManager.Instance.Register<CreativePowers.GodmodePower>("godmode");
			CreativePowerManager.Instance.Register<CreativePowers.ModifyWindDirectionAndStrength>("wind_setstrength");
			CreativePowerManager.Instance.Register<CreativePowers.ModifyRainPower>("rain_setstrength");
			CreativePowerManager.Instance.Register<CreativePowers.ModifyTimeRate>("time_setspeed");
			CreativePowerManager.Instance.Register<CreativePowers.FreezeRainPower>("rain_setfrozen");
			CreativePowerManager.Instance.Register<CreativePowers.FreezeWindDirectionAndStrength>("wind_setfrozen");
			CreativePowerManager.Instance.Register<CreativePowers.FarPlacementRangePower>("increaseplacementrange");
			CreativePowerManager.Instance.Register<CreativePowers.DifficultySliderPower>("setdifficulty");
			CreativePowerManager.Instance.Register<CreativePowers.StopBiomeSpreadPower>("biomespread_setfrozen");
			CreativePowerManager.Instance.Register<CreativePowers.SpawnRateSliderPerPlayerPower>("setspawnrate");
			CreativePowerManager._initialized = true;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x005685A4 File Offset: 0x005667A4
		public void Reset()
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				keyValuePair.Value.CurrentPermissionLevel = keyValuePair.Value.DefaultPermissionLevel;
				IPersistentPerWorldContent persistentPerWorldContent = keyValuePair.Value as IPersistentPerWorldContent;
				if (persistentPerWorldContent != null)
				{
					persistentPerWorldContent.Reset();
				}
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.Reset();
				}
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00568634 File Offset: 0x00566834
		public void SaveToWorld(BinaryWriter writer)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerWorldContent persistentPerWorldContent = keyValuePair.Value as IPersistentPerWorldContent;
				if (persistentPerWorldContent != null)
				{
					writer.Write(true);
					writer.Write(keyValuePair.Key);
					persistentPerWorldContent.Save(writer);
				}
			}
			writer.Write(false);
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x005686B4 File Offset: 0x005668B4
		public void LoadFromWorld(BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort num = reader.ReadUInt16();
				ICreativePower creativePower;
				if (!this._powersById.TryGetValue(num, out creativePower))
				{
					break;
				}
				IPersistentPerWorldContent persistentPerWorldContent = creativePower as IPersistentPerWorldContent;
				if (persistentPerWorldContent == null)
				{
					break;
				}
				persistentPerWorldContent.Load(reader, versionGameWasLastSavedOn);
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x005686F4 File Offset: 0x005668F4
		public void ValidateWorld(BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort num = reader.ReadUInt16();
				ICreativePower creativePower;
				if (!this._powersById.TryGetValue(num, out creativePower))
				{
					break;
				}
				IPersistentPerWorldContent persistentPerWorldContent = creativePower as IPersistentPerWorldContent;
				if (persistentPerWorldContent == null)
				{
					break;
				}
				persistentPerWorldContent.ValidateWorld(reader, versionGameWasLastSavedOn);
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00568734 File Offset: 0x00566934
		public void SyncThingsToJoiningPlayer(int playerIndex)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				NetPacket netPacket = NetCreativePowerPermissionsModule.SerializeCurrentPowerPermissionLevel(keyValuePair.Key, (int)keyValuePair.Value.CurrentPermissionLevel);
				NetManager.Instance.SendToClient(netPacket, playerIndex);
			}
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair2 in this._powersById)
			{
				IOnPlayerJoining onPlayerJoining = keyValuePair2.Value as IOnPlayerJoining;
				if (onPlayerJoining != null)
				{
					onPlayerJoining.OnPlayerJoining(playerIndex);
				}
			}
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x005687FC File Offset: 0x005669FC
		public void SaveToPlayer(Player player, BinaryWriter writer)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					writer.Write(true);
					writer.Write(keyValuePair.Key);
					persistentPerPlayerContent.Save(player, writer);
				}
			}
			writer.Write(false);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0056887C File Offset: 0x00566A7C
		public void LoadToPlayer(Player player, BinaryReader reader, int versionGameWasLastSavedOn)
		{
			while (reader.ReadBoolean())
			{
				ushort num = reader.ReadUInt16();
				ICreativePower creativePower;
				if (!this._powersById.TryGetValue(num, out creativePower))
				{
					break;
				}
				IPersistentPerPlayerContent persistentPerPlayerContent = creativePower as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.Load(player, reader, versionGameWasLastSavedOn);
				}
			}
			if (player.difficulty != 3)
			{
				this.ResetPowersForPlayer(player);
			}
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x005688D0 File Offset: 0x00566AD0
		public void ApplyLoadedDataToPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.ApplyLoadedDataToOutOfPlayerFields(player);
				}
			}
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00568934 File Offset: 0x00566B34
		public void ResetPowersForPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.ResetDataForNewPlayer(player);
				}
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00568998 File Offset: 0x00566B98
		public void ResetDataForNewPlayer(Player player)
		{
			foreach (KeyValuePair<ushort, ICreativePower> keyValuePair in this._powersById)
			{
				IPersistentPerPlayerContent persistentPerPlayerContent = keyValuePair.Value as IPersistentPerPlayerContent;
				if (persistentPerPlayerContent != null)
				{
					persistentPerPlayerContent.Reset();
					persistentPerPlayerContent.ResetDataForNewPlayer(player);
				}
			}
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00568A04 File Offset: 0x00566C04
		// Note: this type is marked as 'beforefieldinit'.
		static CreativePowerManager()
		{
		}

		// Token: 0x040050ED RID: 20717
		public static readonly CreativePowerManager Instance = new CreativePowerManager();

		// Token: 0x040050EE RID: 20718
		private Dictionary<ushort, ICreativePower> _powersById = new Dictionary<ushort, ICreativePower>();

		// Token: 0x040050EF RID: 20719
		private Dictionary<string, ICreativePower> _powersByName = new Dictionary<string, ICreativePower>();

		// Token: 0x040050F0 RID: 20720
		private ushort _powersCount;

		// Token: 0x040050F1 RID: 20721
		private static bool _initialized = false;

		// Token: 0x040050F2 RID: 20722
		private const string _powerPermissionsLineHeader = "journeypermission_";

		// Token: 0x0200087C RID: 2172
		private class PowerTypeStorage<T> where T : ICreativePower
		{
			// Token: 0x06004477 RID: 17527 RVA: 0x0000357B File Offset: 0x0000177B
			public PowerTypeStorage()
			{
			}

			// Token: 0x0400728D RID: 29325
			public static ushort Id;

			// Token: 0x0400728E RID: 29326
			public static string Name;

			// Token: 0x0400728F RID: 29327
			public static T Power;
		}
	}
}
