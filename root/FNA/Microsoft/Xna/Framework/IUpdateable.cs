using System;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000029 RID: 41
	public interface IUpdateable
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000C39 RID: 3129
		bool Enabled { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000C3A RID: 3130
		int UpdateOrder { get; }

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000C3B RID: 3131
		// (remove) Token: 0x06000C3C RID: 3132
		event EventHandler<EventArgs> EnabledChanged;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000C3D RID: 3133
		// (remove) Token: 0x06000C3E RID: 3134
		event EventHandler<EventArgs> UpdateOrderChanged;

		// Token: 0x06000C3F RID: 3135
		void Update(GameTime gameTime);
	}
}
