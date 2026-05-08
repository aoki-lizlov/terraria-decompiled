using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x0200017D RID: 381
	public class PingMapLayer : IMapLayer
	{
		// Token: 0x06001E40 RID: 7744 RVA: 0x005042A8 File Offset: 0x005024A8
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			SpriteFrame spriteFrame = new SpriteFrame(1, 5);
			DateTime now = DateTime.Now;
			foreach (SlotVector<PingMapLayer.Ping>.ItemPair itemPair in this._pings)
			{
				PingMapLayer.Ping value = itemPair.Value;
				double totalSeconds = (now - value.Time).TotalSeconds;
				int num = (int)(totalSeconds * 10.0);
				spriteFrame.CurrentRow = (byte)(num % (int)spriteFrame.RowCount);
				context.Draw(TextureAssets.MapPing.Value, value.Position, spriteFrame, Alignment.Center);
				if (totalSeconds > 15.0)
				{
					this._pings.Remove(itemPair.Id);
				}
			}
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00504378 File Offset: 0x00502578
		public void Add(Vector2 position)
		{
			if (this._pings.Count == this._pings.Capacity)
			{
				return;
			}
			this._pings.Add(new PingMapLayer.Ping(position));
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x005043A5 File Offset: 0x005025A5
		public void Clear()
		{
			this._pings.Clear();
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x005043B2 File Offset: 0x005025B2
		public PingMapLayer()
		{
		}

		// Token: 0x040016A0 RID: 5792
		private const double PING_DURATION_IN_SECONDS = 15.0;

		// Token: 0x040016A1 RID: 5793
		private const double PING_FRAME_RATE = 10.0;

		// Token: 0x040016A2 RID: 5794
		private readonly SlotVector<PingMapLayer.Ping> _pings = new SlotVector<PingMapLayer.Ping>(100);

		// Token: 0x02000751 RID: 1873
		private struct Ping
		{
			// Token: 0x060040E8 RID: 16616 RVA: 0x0069EE16 File Offset: 0x0069D016
			public Ping(Vector2 position)
			{
				this.Position = position;
				this.Time = DateTime.Now;
			}

			// Token: 0x040069DB RID: 27099
			public readonly Vector2 Position;

			// Token: 0x040069DC RID: 27100
			public readonly DateTime Time;
		}
	}
}
