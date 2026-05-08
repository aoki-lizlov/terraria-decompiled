using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000020 RID: 32
	public class GameComponentCollectionEventArgs : EventArgs
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00013550 File Offset: 0x00011750
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x00013558 File Offset: 0x00011758
		public IGameComponent GameComponent
		{
			[CompilerGenerated]
			get
			{
				return this.<GameComponent>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GameComponent>k__BackingField = value;
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00013561 File Offset: 0x00011761
		public GameComponentCollectionEventArgs(IGameComponent gameComponent)
		{
			this.GameComponent = gameComponent;
		}

		// Token: 0x04000561 RID: 1377
		[CompilerGenerated]
		private IGameComponent <GameComponent>k__BackingField;
	}
}
