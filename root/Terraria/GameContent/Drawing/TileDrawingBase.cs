using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000443 RID: 1091
	public class TileDrawingBase
	{
		// Token: 0x0600314A RID: 12618 RVA: 0x005C986A File Offset: 0x005C7A6A
		public void Begin(RasterizerState rasterizer, Matrix transformation)
		{
			this.batchBeginner = new SpriteBatchBeginner(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, rasterizer, null, transformation);
			this.batchBeginner.Begin(Main.tileBatch);
			this.batchBeginner.Begin(Main.spriteBatch);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x005C98AC File Offset: 0x005C7AAC
		public void End()
		{
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			int num = Main.tileBatch.End();
			num += Main.spriteBatch.PendingDrawCallCount();
			Main.spriteBatch.End();
			this.DrawCallLogData.Add(num);
			this.FlushLogData.AddTime(startTimestamp);
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x005C98FC File Offset: 0x005C7AFC
		public void RestartLayeredBatch()
		{
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			int num = Main.tileBatch.Restart();
			this.DrawCallLogData.Add(num);
			this.FlushLogData.AddTime(startTimestamp);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x005C9934 File Offset: 0x005C7B34
		public void RestartSpriteBatch()
		{
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			int num = Main.spriteBatch.PendingDrawCallCount();
			Main.spriteBatch.End();
			this.batchBeginner.Begin(Main.spriteBatch);
			this.DrawCallLogData.Add(num);
			this.FlushLogData.AddTime(startTimestamp);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x0000357B File Offset: 0x0000177B
		public TileDrawingBase()
		{
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x005C9984 File Offset: 0x005C7B84
		// Note: this type is marked as 'beforefieldinit'.
		static TileDrawingBase()
		{
		}

		// Token: 0x04005778 RID: 22392
		public static bool DrawOwnBlacks = true;

		// Token: 0x04005779 RID: 22393
		protected TimeLogger.TimeLogData FlushLogData;

		// Token: 0x0400577A RID: 22394
		protected TimeLogger.TimeLogData DrawCallLogData;

		// Token: 0x0400577B RID: 22395
		private SpriteBatchBeginner batchBeginner;
	}
}
