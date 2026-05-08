using System;
using FullSerializer;
using SteelSeries.GameSense.DeviceZone;

namespace SteelSeries.GameSense
{
	// Token: 0x02000072 RID: 114
	[fsObject(Converter = typeof(ContextColorEventTypeConverter))]
	public class ContextColorEventHandlerType : AbstractHandler
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00008F67 File Offset: 0x00007167
		public ContextColorEventHandlerType()
		{
		}

		// Token: 0x0400021A RID: 538
		public AbstractIlluminationDevice_Zone DeviceZone;

		// Token: 0x0400021B RID: 539
		public string ContextFrameKey;
	}
}
