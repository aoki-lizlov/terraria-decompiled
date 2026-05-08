using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using SteelSeries.GameSense;
using SteelSeries.GameSense.DeviceZone;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x0200005F RID: 95
	internal class SteelSeriesSecondaryDeviceByZone : RgbDevice, IGameSenseDevice, IGameSenseUpdater
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000977C File Offset: 0x0000797C
		public SteelSeriesSecondaryDeviceByZone(DeviceColorProfile colorProfile, RgbDeviceType type, string zoneNameToCheck, int xPosition, int yPosition)
			: base(RgbDeviceVendor.SteelSeries, type, Fragment.FromGrid(new Rectangle(xPosition, yPosition, 1, 1)), colorProfile)
		{
			this._zoneTarget = zoneNameToCheck;
			base.PreferredLevelOfDetail = EffectDetailLevel.High;
			this._xPosition = xPosition;
			this._yPosition = yPosition;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000097CC File Offset: 0x000079CC
		public override void Present()
		{
			Vector4 processedLedColor = base.GetProcessedLedColor(0);
			Color color;
			color..ctor(processedLedColor);
			this._colorKey.UpdateColor(color, true);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000097F8 File Offset: 0x000079F8
		public List<JObject> TryGetEventUpdateRequest()
		{
			this._requestList.Clear();
			JObject jobject = this._colorKey.TryGettingRequest();
			if (jobject != null)
			{
				this._requestList.Add(jobject);
			}
			return this._requestList;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009834 File Offset: 0x00007A34
		public void CollectEventsToTrack(Bind_Event[] bindEvents, ARgbGameValueTracker[] miscEvents)
		{
			foreach (Bind_Event bind_Event in bindEvents)
			{
				ContextColorEventHandlerType contextColorEventHandlerType = bind_Event.handlers[0] as ContextColorEventHandlerType;
				if (contextColorEventHandlerType != null)
				{
					RGBZonedDevice rgbzonedDevice = contextColorEventHandlerType.DeviceZone as RGBZonedDevice;
					if (rgbzonedDevice != null && !(rgbzonedDevice.zone != this._zoneTarget))
					{
						ColorKey colorKey = new ColorKey
						{
							EventName = bind_Event.eventName,
							TriggerName = contextColorEventHandlerType.ContextFrameKey
						};
						this._colorKey = colorKey;
						return;
					}
				}
			}
		}

		// Token: 0x040002F9 RID: 761
		private string _zoneTarget;

		// Token: 0x040002FA RID: 762
		private int _xPosition;

		// Token: 0x040002FB RID: 763
		private int _yPosition;

		// Token: 0x040002FC RID: 764
		private ColorKey _colorKey;

		// Token: 0x040002FD RID: 765
		private List<JObject> _requestList = new List<JObject>();
	}
}
