using System;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class DeviceInitializationException : Exception
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004675 File Offset: 0x00002875
		public DeviceInitializationException(string text)
			: base(text)
		{
		}
	}
}
