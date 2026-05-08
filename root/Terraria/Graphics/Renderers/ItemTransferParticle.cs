using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200020A RID: 522
	public class ItemTransferParticle : IPooledParticle, IParticle
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x0052F0DC File Offset: 0x0052D2DC
		// (set) Token: 0x06002164 RID: 8548 RVA: 0x0052F0E4 File Offset: 0x0052D2E4
		public bool ShouldBeRemovedFromRenderer
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldBeRemovedFromRenderer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ShouldBeRemovedFromRenderer>k__BackingField = value;
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0052F0ED File Offset: 0x0052D2ED
		public ItemTransferParticle()
		{
			this._itemInstance = new Item();
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0052F100 File Offset: 0x0052D300
		public void Update(ref ParticleRendererSettings settings)
		{
			int num = this._lifeTimeCounted + 1;
			this._lifeTimeCounted = num;
			if (num >= this._lifeTimeTotal)
			{
				this.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0052F130 File Offset: 0x0052D330
		public void Prepare(int itemType, int lifeTimeTotal, Vector2 startPosition, Vector2 endPosition, Vector2 offsetStart, Vector2 offsetEnd, bool transitionIn, bool fullbright, bool inInventory, int stack = 1)
		{
			this._itemInstance.SetDefaults(itemType, null);
			this._itemInstance.stack = stack;
			this._lifeTimeTotal = lifeTimeTotal;
			this.StartPosition = startPosition;
			this.StartOffset = offsetStart;
			this.EndPosition = endPosition;
			this.EndOffset = offsetEnd;
			this.TransitionIn = transitionIn;
			this.Fullbright = fullbright;
			this.InInventory = inInventory;
			Vector2 vector = (this.EndPosition - this.StartPosition).SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866, default(Vector2));
			bool flag = vector.Y < 0f;
			bool flag2 = vector.Y == 0f;
			if (!flag || (flag2 && Main.rand.Next(2) == 0))
			{
				vector *= -1f;
			}
			vector = new Vector2(0f, -1f);
			float num = Vector2.Distance(this.EndPosition, this.StartPosition);
			this.BezierHelper1 = vector * num + Main.rand.NextVector2Circular(32f, 32f);
			this.BezierHelper2 = -vector * num + Main.rand.NextVector2Circular(32f, 32f);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0052F278 File Offset: 0x0052D478
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			float num = (float)this._lifeTimeCounted / (float)this._lifeTimeTotal;
			float num2 = Utils.Remap(num, 0.1f, 0.5f, 0f, 0.85f, true);
			num2 = Utils.Remap(num, 0.5f, 0.9f, num2, 1f, true);
			Vector2 vector;
			Vector2.Hermite(ref this.StartPosition, ref this.BezierHelper1, ref this.EndPosition, ref this.BezierHelper2, num2, out vector);
			Vector2 vector2 = Vector2.Zero;
			if (num <= 0.15f)
			{
				vector2 = Vector2.Lerp(Vector2.Zero, this.StartOffset, num / 0.15f);
			}
			else if (num <= 0.5f)
			{
				vector2 = Vector2.Lerp(this.StartOffset, this.EndOffset, (num - 0.15f) / 0.35f);
			}
			else if (num <= 0.85f)
			{
				vector2 = this.EndOffset;
			}
			else
			{
				vector2 = Vector2.Lerp(this.EndOffset, Vector2.Zero, Utils.Remap(num, 0.85f, 0.95f, 0f, 1f, true));
			}
			vector += vector2;
			float num3 = Utils.Remap(num, 0f, 0.15f, (float)(this.TransitionIn ? 0 : 1), 1f, true) * Utils.Remap(num, 0.85f, 0.95f, 1f, 0f, true);
			Color color = (this.Fullbright ? Color.White : Lighting.GetColor(vector.ToTileCoordinates()));
			int num4 = 31;
			int num5 = 32;
			if (this.InInventory)
			{
				num5 = 32;
				num3 = 1f;
				float num6 = num;
				num6 *= num6;
				vector = Vector2.Lerp(this.StartPosition - new Vector2(26f, 26f) * Main.inventoryScale, this.EndPosition - new Vector2(26f, 26f) * Main.inventoryScale, num6);
				num4 = 14;
			}
			if (this.InInventory)
			{
				ItemSlot.Draw(spritebatch, ref this._itemInstance, num4, settings.AnchorPosition + vector, color);
				return;
			}
			ItemSlot.DrawItemIcon(this._itemInstance, num4, Main.spriteBatch, settings.AnchorPosition + vector, this._itemInstance.scale * num3, (float)num5, color, 1f, false);
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x0052F4B2 File Offset: 0x0052D6B2
		// (set) Token: 0x0600216A RID: 8554 RVA: 0x0052F4BA File Offset: 0x0052D6BA
		public bool IsRestingInPool
		{
			[CompilerGenerated]
			get
			{
				return this.<IsRestingInPool>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsRestingInPool>k__BackingField = value;
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0052F4C3 File Offset: 0x0052D6C3
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0052F4CC File Offset: 0x0052D6CC
		public virtual void FetchFromPool()
		{
			this._lifeTimeCounted = 0;
			this._lifeTimeTotal = 0;
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
			this.StartPosition = (this.EndPosition = (this.BezierHelper1 = (this.BezierHelper2 = Vector2.Zero)));
		}

		// Token: 0x04004BC3 RID: 19395
		[CompilerGenerated]
		private bool <ShouldBeRemovedFromRenderer>k__BackingField;

		// Token: 0x04004BC4 RID: 19396
		private Vector2 StartPosition;

		// Token: 0x04004BC5 RID: 19397
		private Vector2 EndPosition;

		// Token: 0x04004BC6 RID: 19398
		private Vector2 StartOffset;

		// Token: 0x04004BC7 RID: 19399
		private Vector2 EndOffset;

		// Token: 0x04004BC8 RID: 19400
		private Vector2 BezierHelper1;

		// Token: 0x04004BC9 RID: 19401
		private Vector2 BezierHelper2;

		// Token: 0x04004BCA RID: 19402
		private bool TransitionIn;

		// Token: 0x04004BCB RID: 19403
		private bool Fullbright;

		// Token: 0x04004BCC RID: 19404
		private bool InInventory;

		// Token: 0x04004BCD RID: 19405
		private Item _itemInstance;

		// Token: 0x04004BCE RID: 19406
		private int _lifeTimeCounted;

		// Token: 0x04004BCF RID: 19407
		private int _lifeTimeTotal;

		// Token: 0x04004BD0 RID: 19408
		[CompilerGenerated]
		private bool <IsRestingInPool>k__BackingField;
	}
}
