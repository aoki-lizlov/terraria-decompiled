using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200009F RID: 159
	internal interface IRenderTarget
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600139C RID: 5020
		int Width { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600139D RID: 5021
		int Height { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600139E RID: 5022
		int LevelCount { get; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600139F RID: 5023
		RenderTargetUsage RenderTargetUsage { get; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060013A0 RID: 5024
		DepthFormat DepthStencilFormat { get; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060013A1 RID: 5025
		IntPtr DepthStencilBuffer { get; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060013A2 RID: 5026
		IntPtr ColorBuffer { get; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060013A3 RID: 5027
		int MultiSampleCount { get; }
	}
}
