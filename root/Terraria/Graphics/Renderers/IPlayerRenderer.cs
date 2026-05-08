using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000216 RID: 534
	public interface IPlayerRenderer
	{
		// Token: 0x060021A8 RID: 8616
		void DrawPlayers(Camera camera, IEnumerable<Player> players);

		// Token: 0x060021A9 RID: 8617
		void DrawPlayerHead(Camera camera, Player drawPlayer, Vector2 position, float alpha = 1f, float scale = 1f, Color borderColor = default(Color));

		// Token: 0x060021AA RID: 8618
		void DrawPlayer(Camera camera, Player drawPlayer, Vector2 position, float rotation, Vector2 rotationOrigin, float shadow = 0f, float scale = 1f);

		// Token: 0x060021AB RID: 8619
		void PrepareDrawForFrame(Player drawPlayer);
	}
}
