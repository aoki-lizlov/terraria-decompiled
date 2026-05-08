using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using SteelSeries.GameSense;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x0200005E RID: 94
	internal class SteelSeriesKeyboard : RgbKeyboard, IGameSenseDevice, IGameSenseUpdater
	{
		// Token: 0x06000207 RID: 519 RVA: 0x000093E4 File Offset: 0x000075E4
		public SteelSeriesKeyboard(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.SteelSeries, Fragment.FromGrid(new Rectangle(0, 0, 22, 6)), colorProfile)
		{
			for (int i = 0; i < this._keyboardKeyColors.Length; i++)
			{
				this._keyboardKeyColors[i] = new int[3];
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009468 File Offset: 0x00007668
		public override void Render(IEnumerable<RgbKey> keys)
		{
			foreach (RgbKey rgbKey in keys)
			{
				ColorKey colorKey;
				if (this._keyboardTriggersForLookup.TryGetValue(rgbKey.KeyTriggerName, ref colorKey))
				{
					Color color = base.ProcessLedColor(rgbKey.CurrentColor);
					colorKey.UpdateColor(color, rgbKey.IsVisible);
				}
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000094D8 File Offset: 0x000076D8
		public override void Present()
		{
			for (int i = 0; i < base.LedCount; i++)
			{
				Vector4 processedLedColor = base.GetProcessedLedColor(i);
				int[] array = this._keyboardKeyColors[i];
				array[0] = (int)(processedLedColor.X * 255f);
				array[1] = (int)(processedLedColor.Y * 255f);
				array[2] = (int)(processedLedColor.Z * 255f);
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009538 File Offset: 0x00007738
		public List<JObject> TryGetEventUpdateRequest()
		{
			this._updateRequestsList.Clear();
			this._updateRequestsList.Add(this.GetBitmapRequestForFullKeyboard());
			for (int i = 0; i < this._keyboardTriggersForIteration.Count; i++)
			{
				JObject jobject = this._keyboardTriggersForIteration[i].TryGettingRequest();
				if (jobject != null)
				{
					this._updateRequestsList.Add(jobject);
				}
			}
			return this._updateRequestsList;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000095A0 File Offset: 0x000077A0
		private JObject GetBitmapRequestForFullKeyboard()
		{
			JObject jobject = new JObject();
			jobject.Add("bitmap", JToken.FromObject(this._keyboardKeyColors));
			List<string> list = Enumerable.ToList<string>(Enumerable.Select<ColorKey, string>(Enumerable.Where<ColorKey>(this._keyboardTriggersForIteration, (ColorKey x) => x.IsVisible), (ColorKey x) => x.EventName));
			IEnumerable<string> enumerable = Enumerable.Select<ARgbGameValueTracker, string>(Enumerable.Where<ARgbGameValueTracker>(this.eventTrackers, (ARgbGameValueTracker x) => x.IsVisible), (ARgbGameValueTracker x) => x.EventName);
			list.AddRange(enumerable);
			jobject.Add("excluded-events", JToken.FromObject(list));
			JObject jobject2 = new JObject();
			jobject2.Add("frame", jobject);
			jobject2.Add("value", this._timesSent);
			this._timesSent++;
			if (this._timesSent >= 100)
			{
				this._timesSent = 1;
			}
			return new JObject
			{
				{ "event", "DO_RAINBOWS" },
				{ "data", jobject2 }
			};
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000096F4 File Offset: 0x000078F4
		public void CollectEventsToTrack(Bind_Event[] bindEvents, ARgbGameValueTracker[] miscEvents)
		{
			foreach (Bind_Event bind_Event in bindEvents)
			{
				if (bind_Event.handlers.Length >= 1)
				{
					ContextColorEventHandlerType contextColorEventHandlerType = bind_Event.handlers[0] as ContextColorEventHandlerType;
					if (contextColorEventHandlerType != null)
					{
						ColorKey colorKey = new ColorKey
						{
							EventName = bind_Event.eventName,
							TriggerName = contextColorEventHandlerType.ContextFrameKey
						};
						this._keyboardTriggersForLookup.Add(contextColorEventHandlerType.ContextFrameKey, colorKey);
						this._keyboardTriggersForIteration.Add(colorKey);
					}
				}
			}
			this.eventTrackers.AddRange(miscEvents);
		}

		// Token: 0x040002EF RID: 751
		public const string EVENT_ID_BITMAP = "DO_RAINBOWS";

		// Token: 0x040002F0 RID: 752
		private const int HowManyKeysGameSenseKeyboardUses = 132;

		// Token: 0x040002F1 RID: 753
		private const int Rows = 6;

		// Token: 0x040002F2 RID: 754
		private const int Columns = 22;

		// Token: 0x040002F3 RID: 755
		private readonly int[][] _keyboardKeyColors = new int[132][];

		// Token: 0x040002F4 RID: 756
		private readonly Dictionary<string, ColorKey> _keyboardTriggersForLookup = new Dictionary<string, ColorKey>();

		// Token: 0x040002F5 RID: 757
		private readonly List<ColorKey> _keyboardTriggersForIteration = new List<ColorKey>();

		// Token: 0x040002F6 RID: 758
		private readonly List<ARgbGameValueTracker> eventTrackers = new List<ARgbGameValueTracker>();

		// Token: 0x040002F7 RID: 759
		private int _timesSent;

		// Token: 0x040002F8 RID: 760
		private List<JObject> _updateRequestsList = new List<JObject>();

		// Token: 0x020000CE RID: 206
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600044F RID: 1103 RVA: 0x0000E46E File Offset: 0x0000C66E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c()
			{
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x0000E47A File Offset: 0x0000C67A
			internal bool <GetBitmapRequestForFullKeyboard>b__14_0(ColorKey x)
			{
				return x.IsVisible;
			}

			// Token: 0x06000452 RID: 1106 RVA: 0x0000E482 File Offset: 0x0000C682
			internal string <GetBitmapRequestForFullKeyboard>b__14_1(ColorKey x)
			{
				return x.EventName;
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x0000E48A File Offset: 0x0000C68A
			internal bool <GetBitmapRequestForFullKeyboard>b__14_2(ARgbGameValueTracker x)
			{
				return x.IsVisible;
			}

			// Token: 0x06000454 RID: 1108 RVA: 0x0000E492 File Offset: 0x0000C692
			internal string <GetBitmapRequestForFullKeyboard>b__14_3(ARgbGameValueTracker x)
			{
				return x.EventName;
			}

			// Token: 0x040005BD RID: 1469
			public static readonly SteelSeriesKeyboard.<>c <>9 = new SteelSeriesKeyboard.<>c();

			// Token: 0x040005BE RID: 1470
			public static Func<ColorKey, bool> <>9__14_0;

			// Token: 0x040005BF RID: 1471
			public static Func<ColorKey, string> <>9__14_1;

			// Token: 0x040005C0 RID: 1472
			public static Func<ARgbGameValueTracker, bool> <>9__14_2;

			// Token: 0x040005C1 RID: 1473
			public static Func<ARgbGameValueTracker, string> <>9__14_3;
		}
	}
}
