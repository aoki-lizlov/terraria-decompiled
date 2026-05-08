using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000B5 RID: 181
	public sealed class ResourceCreatedEventArgs : EventArgs
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0002FBB3 File Offset: 0x0002DDB3
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x0002FBBB File Offset: 0x0002DDBB
		public object Resource
		{
			[CompilerGenerated]
			get
			{
				return this.<Resource>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Resource>k__BackingField = value;
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0002FBC4 File Offset: 0x0002DDC4
		internal ResourceCreatedEventArgs(object resource)
		{
			this.Resource = resource;
		}

		// Token: 0x0400098A RID: 2442
		[CompilerGenerated]
		private object <Resource>k__BackingField;
	}
}
