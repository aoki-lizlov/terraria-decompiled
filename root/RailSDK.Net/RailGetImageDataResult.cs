using System;

namespace rail
{
	// Token: 0x02000194 RID: 404
	public class RailGetImageDataResult : EventBase
	{
		// Token: 0x060018BF RID: 6335 RVA: 0x00011143 File Offset: 0x0000F343
		public RailGetImageDataResult()
		{
		}

		// Token: 0x040005A1 RID: 1441
		public string image_data;

		// Token: 0x040005A2 RID: 1442
		public RailImageDataDescriptor image_data_descriptor = new RailImageDataDescriptor();
	}
}
