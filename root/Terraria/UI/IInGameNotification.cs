using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI
{
	// Token: 0x020000F7 RID: 247
	public interface IInGameNotification
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001950 RID: 6480
		object CreationObject { get; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001951 RID: 6481
		bool ShouldBeRemoved { get; }

		// Token: 0x06001952 RID: 6482
		void Update();

		// Token: 0x06001953 RID: 6483
		void DrawInGame(SpriteBatch spriteBatch, Vector2 bottomAnchorPosition);

		// Token: 0x06001954 RID: 6484
		void PushAnchor(ref Vector2 positionAnchorBottom);

		// Token: 0x06001955 RID: 6485
		void DrawInNotificationsArea(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointLocalIndexTouse);
	}
}
