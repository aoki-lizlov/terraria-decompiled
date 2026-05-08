using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;

namespace ReLogic.Peripherals.RGB.SteelSeries
{
	// Token: 0x0200005D RID: 93
	internal class ColorKey
	{
		// Token: 0x06000204 RID: 516 RVA: 0x000092D0 File Offset: 0x000074D0
		public void UpdateColor(Color color, bool isVisible)
		{
			this.IsVisible = isVisible;
			if (this._colorToShow == color && this._timesDeniedColorRepeats < 30)
			{
				this._timesDeniedColorRepeats++;
				return;
			}
			this._timesDeniedColorRepeats = 0;
			this._colorToShow = color;
			this._needsToSendMessage = true;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009320 File Offset: 0x00007520
		public JObject TryGettingRequest()
		{
			if (!this._needsToSendMessage)
			{
				return null;
			}
			this._needsToSendMessage = false;
			JObject jobject = new JObject();
			jobject.Add("red", this._colorToShow.R);
			jobject.Add("green", this._colorToShow.G);
			jobject.Add("blue", this._colorToShow.B);
			JObject jobject2 = new JObject();
			jobject2.Add(this.TriggerName, jobject);
			JObject jobject3 = new JObject();
			jobject3.Add("frame", jobject2);
			return new JObject
			{
				{ "event", this.EventName },
				{ "data", jobject3 }
			};
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000448A File Offset: 0x0000268A
		public ColorKey()
		{
		}

		// Token: 0x040002E8 RID: 744
		private const int TimesToDenyIdenticalColors = 30;

		// Token: 0x040002E9 RID: 745
		public string EventName;

		// Token: 0x040002EA RID: 746
		public string TriggerName;

		// Token: 0x040002EB RID: 747
		private Color _colorToShow;

		// Token: 0x040002EC RID: 748
		private bool _needsToSendMessage;

		// Token: 0x040002ED RID: 749
		public bool IsVisible;

		// Token: 0x040002EE RID: 750
		private int _timesDeniedColorRepeats;
	}
}
