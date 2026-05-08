using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteelSeries.GameSense;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x02000058 RID: 88
	public class SteelSeriesDeviceGroup : RgbDeviceGroup
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public SteelSeriesDeviceGroup(VendorColorProfile colorProfiles, string gameNameIdInAllCaps, string gameDisplayName, IconColor iconColor)
		{
			this._gameSenseConnection = new GameSenseConnection
			{
				GameName = gameNameIdInAllCaps,
				GameDisplayName = gameDisplayName,
				IconColor = iconColor
			};
			this._colorProfiles = colorProfiles;
			GameSenseConnection gameSenseConnection = this._gameSenseConnection;
			gameSenseConnection.OnConnectionBecameActive = (GameSenseConnection.ClientStateEvent)Delegate.Combine(gameSenseConnection.OnConnectionBecameActive, new GameSenseConnection.ClientStateEvent(this._gameSenseConnection_OnConnectionBecameActive));
			GameSenseConnection gameSenseConnection2 = this._gameSenseConnection;
			gameSenseConnection2.OnConnectionBecameInactive = (GameSenseConnection.ClientStateEvent)Delegate.Combine(gameSenseConnection2.OnConnectionBecameInactive, new GameSenseConnection.ClientStateEvent(this._gameSenseConnection_OnConnectionBecameInactive));
			this._gameSenseConnection.SetEvents(new Bind_Event[0]);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008CC1 File Offset: 0x00006EC1
		private void Application_ApplicationExit(object sender, EventArgs e)
		{
			this._gameSenseConnection_OnConnectionBecameInactive();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008CCC File Offset: 0x00006ECC
		protected override void Initialize()
		{
			if (this._isInitialized)
			{
				return;
			}
			this.ConnectToGameSense();
			this._isInitialized = true;
			for (int i = 0; i < 12; i++)
			{
				this._devicesThatStaggerByFrameGroup.Add(new List<RgbDevice>());
			}
			this.TrackDeviceAndAddItToList(this._devicesThatDontStagger, new SteelSeriesKeyboard(this._colorProfiles.Keyboard));
			int num = 27;
			int num2 = 0;
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[0], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "one", num + 1, num2 + 4));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[1], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "two", num + 2, num2 + 4));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[2], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "three", num + 3, num2 + 4));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[3], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "four", num + 4, num2 + 3));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[4], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "five", num + 4, num2 + 2));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[5], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "six", num + 4, num2 + 1));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[6], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "seven", num + 3, num2));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[7], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "eight", num + 2, num2));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[8], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "nine", num + 1, num2));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[9], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "ten", num, num2 + 1));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[10], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "eleven", num, num2 + 2));
			this.TrackDeviceAndAddItToList(this._devicesThatStaggerByFrameGroup[11], new SteelSeriesSecondaryDeviceByZone(this._colorProfiles.Generic, RgbDeviceType.Generic, "twelve", num2, num2 + 3));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008F4C File Offset: 0x0000714C
		protected void TrackDeviceAndAddItToList(List<RgbDevice> deviceList, RgbDevice device)
		{
			this._devices.Add(device);
			deviceList.Add(device);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008F61 File Offset: 0x00007161
		protected override void Uninitialize()
		{
			if (!this._isInitialized)
			{
				return;
			}
			this._hasConnectionToGameSense = false;
			this.DisconnectFromGameSense();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008F7C File Offset: 0x0000717C
		public override void LoadSpecialRules(object specialRulesObject)
		{
			GameSenseSpecificInfo gameSenseSpecificInfo = specialRulesObject as GameSenseSpecificInfo;
			if (gameSenseSpecificInfo == null)
			{
				return;
			}
			this._bindEvents = gameSenseSpecificInfo.EventsToBind.ToArray();
			ARgbGameValueTracker[] array = gameSenseSpecificInfo.MiscEvents.ToArray();
			this._gameSenseConnection.TryRegisteringEvents(this._bindEvents);
			foreach (RgbDevice rgbDevice in this._devices)
			{
				IGameSenseDevice gameSenseDevice = rgbDevice as IGameSenseDevice;
				if (gameSenseDevice != null)
				{
					gameSenseDevice.CollectEventsToTrack(this._bindEvents, array);
				}
			}
			foreach (ARgbGameValueTracker argbGameValueTracker in array)
			{
				this._miscEvents.Add(new SteelSeriesEventRelay(argbGameValueTracker));
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009048 File Offset: 0x00007248
		private void SendUpdatesForBindEvents()
		{
			foreach (Bind_Event bind_Event in this._bindEvents)
			{
				this._gameSenseConnection.SendEvent(bind_Event.eventName, 1);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009080 File Offset: 0x00007280
		public override IEnumerator<RgbDevice> GetEnumerator()
		{
			return this._devices.GetEnumerator();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009092 File Offset: 0x00007292
		public override void OnceProcessed()
		{
			this.SendRelevantChangesToGameSense();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000909C File Offset: 0x0000729C
		private void SendRelevantChangesToGameSense()
		{
			if (!this._hasConnectionToGameSense)
			{
				return;
			}
			this.UpdateDeviceList(this._devicesThatDontStagger);
			this._throttleCounter++;
			this._throttleCounter %= 12;
			this.UpdateDeviceList(this._devicesThatStaggerByFrameGroup[this._throttleCounter]);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000090F4 File Offset: 0x000072F4
		private void UpdateDeviceList(List<RgbDevice> deviceList)
		{
			foreach (RgbDevice rgbDevice in deviceList)
			{
				IGameSenseDevice gameSenseDevice = rgbDevice as IGameSenseDevice;
				if (gameSenseDevice != null)
				{
					List<JObject> list = gameSenseDevice.TryGetEventUpdateRequest();
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							JObject jobject = list[i];
							if (jobject != null)
							{
								jobject.Add("game", this._gameSenseConnection.GameName);
								string text = jobject.ToString(Formatting.None, new JsonConverter[0]);
								this._gameSenseConnection.SendEvent(text);
							}
						}
					}
				}
			}
			foreach (IGameSenseUpdater gameSenseUpdater in this._miscEvents)
			{
				List<JObject> list2 = gameSenseUpdater.TryGetEventUpdateRequest();
				if (list2 != null)
				{
					for (int j = 0; j < list2.Count; j++)
					{
						JObject jobject2 = list2[j];
						if (jobject2 != null)
						{
							jobject2.Add("game", this._gameSenseConnection.GameName);
							string text2 = jobject2.ToString(Formatting.None, new JsonConverter[0]);
							this._gameSenseConnection.SendEvent(text2);
						}
					}
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009248 File Offset: 0x00007448
		private void ConnectToGameSense()
		{
			this._gameSenseConnection.BeginConnection();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009255 File Offset: 0x00007455
		private void DisconnectFromGameSense()
		{
			this._gameSenseConnection.EndConnection();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009262 File Offset: 0x00007462
		private void _gameSenseConnection_OnConnectionBecameActive()
		{
			this._hasConnectionToGameSense = true;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000926B File Offset: 0x0000746B
		private void _gameSenseConnection_OnConnectionBecameInactive()
		{
			this._hasConnectionToGameSense = false;
			this.Uninitialize();
		}

		// Token: 0x040002D8 RID: 728
		private readonly List<RgbDevice> _devices = new List<RgbDevice>();

		// Token: 0x040002D9 RID: 729
		private readonly List<IGameSenseUpdater> _miscEvents = new List<IGameSenseUpdater>();

		// Token: 0x040002DA RID: 730
		private const int ThrottleLoopSize = 12;

		// Token: 0x040002DB RID: 731
		private readonly List<RgbDevice> _devicesThatDontStagger = new List<RgbDevice>();

		// Token: 0x040002DC RID: 732
		private readonly List<List<RgbDevice>> _devicesThatStaggerByFrameGroup = new List<List<RgbDevice>>(12);

		// Token: 0x040002DD RID: 733
		private int _throttleCounter;

		// Token: 0x040002DE RID: 734
		private bool _isInitialized;

		// Token: 0x040002DF RID: 735
		private bool _hasConnectionToGameSense;

		// Token: 0x040002E0 RID: 736
		private readonly VendorColorProfile _colorProfiles;

		// Token: 0x040002E1 RID: 737
		private Bind_Event[] _bindEvents;

		// Token: 0x040002E2 RID: 738
		private GameSenseConnection _gameSenseConnection;

		// Token: 0x040002E3 RID: 739
		private JsonSerializer _serializer = JsonSerializer.Create(new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto,
			MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
			Formatting = Formatting.None
		});

		// Token: 0x020000CD RID: 205
		public static class EventNames
		{
			// Token: 0x040005BC RID: 1468
			public const string DoRainbows = "DO_RAINBOWS";
		}
	}
}
