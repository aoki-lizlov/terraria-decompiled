using System;
using Newtonsoft.Json.Linq;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200001A RID: 26
	public class IntRgbGameValueTracker : ARgbGameValueTracker<int>
	{
		// Token: 0x060000CA RID: 202 RVA: 0x000044F7 File Offset: 0x000026F7
		protected override void WriteValueToData(JObject data)
		{
			data.Add("value", this._currentValue);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000450F File Offset: 0x0000270F
		public IntRgbGameValueTracker()
		{
		}
	}
}
