using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Input.Touch
{
	// Token: 0x02000075 RID: 117
	public struct TouchPanelCapabilities
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x000241FD File Offset: 0x000223FD
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x00024205 File Offset: 0x00022405
		public bool IsConnected
		{
			[CompilerGenerated]
			get
			{
				return this.<IsConnected>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsConnected>k__BackingField = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0002420E File Offset: 0x0002240E
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x00024216 File Offset: 0x00022416
		public int MaximumTouchCount
		{
			[CompilerGenerated]
			get
			{
				return this.<MaximumTouchCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MaximumTouchCount>k__BackingField = value;
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0002421F File Offset: 0x0002241F
		internal TouchPanelCapabilities(bool isConnected, int maximumTouchCount)
		{
			this = default(TouchPanelCapabilities);
			this.IsConnected = isConnected;
			this.MaximumTouchCount = maximumTouchCount;
		}

		// Token: 0x040007A9 RID: 1961
		[CompilerGenerated]
		private bool <IsConnected>k__BackingField;

		// Token: 0x040007AA RID: 1962
		[CompilerGenerated]
		private int <MaximumTouchCount>k__BackingField;
	}
}
