using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000031 RID: 49
	public class PreparingDeviceSettingsEventArgs : EventArgs
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00018D5F File Offset: 0x00016F5F
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00018D67 File Offset: 0x00016F67
		public GraphicsDeviceInformation GraphicsDeviceInformation
		{
			[CompilerGenerated]
			get
			{
				return this.<GraphicsDeviceInformation>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GraphicsDeviceInformation>k__BackingField = value;
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00018D70 File Offset: 0x00016F70
		public PreparingDeviceSettingsEventArgs(GraphicsDeviceInformation graphicsDeviceInformation)
		{
			this.GraphicsDeviceInformation = graphicsDeviceInformation;
		}

		// Token: 0x040005AF RID: 1455
		[CompilerGenerated]
		private GraphicsDeviceInformation <GraphicsDeviceInformation>k__BackingField;
	}
}
