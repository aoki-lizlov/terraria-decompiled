using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000B6 RID: 182
	public sealed class ResourceDestroyedEventArgs : EventArgs
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0002FBD3 File Offset: 0x0002DDD3
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x0002FBDB File Offset: 0x0002DDDB
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0002FBE4 File Offset: 0x0002DDE4
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x0002FBEC File Offset: 0x0002DDEC
		public object Tag
		{
			[CompilerGenerated]
			get
			{
				return this.<Tag>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Tag>k__BackingField = value;
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0002FBF5 File Offset: 0x0002DDF5
		internal ResourceDestroyedEventArgs(string name, object tag)
		{
			this.Name = name;
			this.Tag = tag;
		}

		// Token: 0x0400098B RID: 2443
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x0400098C RID: 2444
		[CompilerGenerated]
		private object <Tag>k__BackingField;
	}
}
