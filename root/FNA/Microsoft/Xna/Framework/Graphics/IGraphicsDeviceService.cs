using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200009E RID: 158
	public interface IGraphicsDeviceService
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001393 RID: 5011
		GraphicsDevice GraphicsDevice { get; }

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001394 RID: 5012
		// (remove) Token: 0x06001395 RID: 5013
		event EventHandler<EventArgs> DeviceCreated;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001396 RID: 5014
		// (remove) Token: 0x06001397 RID: 5015
		event EventHandler<EventArgs> DeviceDisposing;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001398 RID: 5016
		// (remove) Token: 0x06001399 RID: 5017
		event EventHandler<EventArgs> DeviceReset;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600139A RID: 5018
		// (remove) Token: 0x0600139B RID: 5019
		event EventHandler<EventArgs> DeviceResetting;
	}
}
