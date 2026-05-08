using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Map
{
	// Token: 0x0200017B RID: 379
	public class MapIconOverlay
	{
		// Token: 0x06001E36 RID: 7734 RVA: 0x0050388F File Offset: 0x00501A8F
		public MapIconOverlay AddLayer(IMapLayer layer)
		{
			this._layers.Add(layer);
			return this;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x005038A0 File Offset: 0x00501AA0
		public void Draw(Vector2 mapPosition, Vector2 mapOffset, Rectangle? clippingRect, float mapScale, float drawScale, int alpha, ref string text)
		{
			MapOverlayDrawContext mapOverlayDrawContext = new MapOverlayDrawContext(mapPosition, mapOffset, clippingRect, mapScale, drawScale, (float)alpha / 255f);
			foreach (IMapLayer mapLayer in this._layers)
			{
				mapLayer.Draw(ref mapOverlayDrawContext, ref text);
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0050390C File Offset: 0x00501B0C
		public MapIconOverlay()
		{
		}

		// Token: 0x04001699 RID: 5785
		private readonly List<IMapLayer> _layers = new List<IMapLayer>();
	}
}
