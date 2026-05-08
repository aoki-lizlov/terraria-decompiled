using System;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000026 RID: 38
	public interface IDrawable
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000C2E RID: 3118
		int DrawOrder { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000C2F RID: 3119
		bool Visible { get; }

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000C30 RID: 3120
		// (remove) Token: 0x06000C31 RID: 3121
		event EventHandler<EventArgs> DrawOrderChanged;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000C32 RID: 3122
		// (remove) Token: 0x06000C33 RID: 3123
		event EventHandler<EventArgs> VisibleChanged;

		// Token: 0x06000C34 RID: 3124
		void Draw(GameTime gameTime);
	}
}
