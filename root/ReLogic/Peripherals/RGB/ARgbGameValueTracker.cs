using System;
using Newtonsoft.Json.Linq;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000018 RID: 24
	public abstract class ARgbGameValueTracker
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004438 File Offset: 0x00002638
		public JObject TryGettingRequest()
		{
			if (!this._needsToSendMessage)
			{
				return null;
			}
			this._needsToSendMessage = false;
			JObject jobject = new JObject();
			this.WriteValueToData(jobject);
			return new JObject
			{
				{ "event", this.EventName },
				{ "data", jobject }
			};
		}

		// Token: 0x060000C6 RID: 198
		protected abstract void WriteValueToData(JObject data);

		// Token: 0x060000C7 RID: 199 RVA: 0x0000448A File Offset: 0x0000268A
		protected ARgbGameValueTracker()
		{
		}

		// Token: 0x04000035 RID: 53
		public string EventName;

		// Token: 0x04000036 RID: 54
		protected bool _needsToSendMessage;

		// Token: 0x04000037 RID: 55
		public bool IsVisible;
	}
}
