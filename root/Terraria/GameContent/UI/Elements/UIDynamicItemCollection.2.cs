using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003C6 RID: 966
	public abstract class UIDynamicItemCollection<TEntry> : UIDynamicItemCollection
	{
		// Token: 0x06002D45 RID: 11589 RVA: 0x005A2DB8 File Offset: 0x005A0FB8
		public UIDynamicItemCollection()
		{
			this.Width = new StyleDimension(0f, 1f);
			this.HAlign = 0.5f;
			this.UpdateSize();
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x005A2E07 File Offset: 0x005A1007
		public int Count
		{
			get
			{
				return this._contents.Count;
			}
		}

		// Token: 0x06002D47 RID: 11591
		protected abstract Item GetItem(TEntry entry);

		// Token: 0x06002D48 RID: 11592 RVA: 0x005A2E14 File Offset: 0x005A1014
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Main.inventoryScale = 0.84615386f;
			int num;
			int num2;
			int num3;
			int num4;
			this.GetGridParameters(out num, out num2, out num3, out num4);
			int num5 = this._itemsPerLine;
			Vector2 vector = Main.MouseScreen;
			if (PlayerInput.UsingGamepad)
			{
				vector = UILinkPointNavigator.GetPosition(UILinkPointNavigator.CurrentPoint);
			}
			for (int i = num3; i < num4; i++)
			{
				TEntry tentry = this._contents[i];
				Rectangle itemSlotHitbox = this.GetItemSlotHitbox(num, num2, num3, i);
				if (TextureAssets.Item[this.GetItem(tentry).type].State == null)
				{
					num5--;
				}
				bool flag = base.IsMouseHovering && itemSlotHitbox.Contains(vector.ToPoint()) && !PlayerInput.IgnoreMouseInterface;
				this.DrawSlot(spriteBatch, tentry, itemSlotHitbox.TopLeft(), flag);
				if (num5 <= 0)
				{
					break;
				}
			}
			int num6 = 0;
			while (num6 < this._contents.Count && num5 > 0)
			{
				Item item = this.GetItem(this._contents[(num6 + num4) % this._contents.Count]);
				if (TextureAssets.Item[item.type].State == null)
				{
					Main.instance.LoadItem(item.type);
					num5 -= 4;
				}
				num6++;
			}
		}

		// Token: 0x06002D49 RID: 11593
		protected abstract void DrawSlot(SpriteBatch spriteBatch, TEntry entry, Vector2 pos, bool hovering);

		// Token: 0x06002D4A RID: 11594 RVA: 0x005A2F54 File Offset: 0x005A1154
		private Rectangle GetItemSlotHitbox(int startX, int startY, int startItemIndex, int i)
		{
			int num = i - startItemIndex;
			int num2 = num % this._itemsPerLine;
			int num3 = num / this._itemsPerLine;
			return new Rectangle(startX + num2 * 44, startY + num3 * 44, 44, 44);
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x005A2F8C File Offset: 0x005A118C
		private void GetGridParameters(out int startX, out int startY, out int startItemIndex, out int endItemIndex)
		{
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			Rectangle viewCullingArea = base.Parent.GetViewCullingArea();
			int x = rectangle.Center.X;
			startX = x - (int)((float)(44 * this._itemsPerLine) * 0.5f);
			startY = rectangle.Top;
			startItemIndex = 0;
			endItemIndex = this._contents.Count;
			int num = (Math.Min(viewCullingArea.Top, rectangle.Top) - viewCullingArea.Top) / 44;
			startY += -num * 44;
			startItemIndex += -num * this._itemsPerLine;
			int num2 = (int)Math.Ceiling((double)((float)viewCullingArea.Height / 44f)) * this._itemsPerLine;
			if (endItemIndex > num2 + startItemIndex + this._itemsPerLine)
			{
				endItemIndex = num2 + startItemIndex + this._itemsPerLine;
			}
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x005A3065 File Offset: 0x005A1265
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateSize();
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x005A3073 File Offset: 0x005A1273
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (base.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x005A308F File Offset: 0x005A128F
		public void SetContentsToShow(List<TEntry> itemsToShow)
		{
			this._contents.Clear();
			this._contents.AddRange(itemsToShow);
			this.UpdateSize();
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x005A30AE File Offset: 0x005A12AE
		public int GetItemsPerLine()
		{
			return this._itemsPerLine;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x005A30B8 File Offset: 0x005A12B8
		public override List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			int num;
			int num2;
			int num3;
			int num4;
			this.GetGridParameters(out num, out num2, out num3, out num4);
			int itemsPerLine = this._itemsPerLine;
			Rectangle viewCullingArea = base.Parent.GetViewCullingArea();
			int num5 = num4 - num3;
			while (this._dummySnapPoints.Count < num5)
			{
				this._dummySnapPoints.Add(new SnapPoint("DynamicItemCollectionSlot", 0, Vector2.Zero, Vector2.Zero));
			}
			int num6 = 0;
			Vector2 vector = base.GetDimensions().Position();
			for (int i = num3; i < num4; i++)
			{
				Point center = this.GetItemSlotHitbox(num, num2, num3, i).Center;
				if (viewCullingArea.Contains(center))
				{
					SnapPoint snapPoint = this._dummySnapPoints[num6];
					snapPoint.ThisIsAHackThatChangesTheSnapPointsInfo(Vector2.Zero, center.ToVector2() - vector, i);
					snapPoint.Calculate(this);
					num6++;
					list.Add(snapPoint);
				}
			}
			foreach (UIElement uielement in this.Elements)
			{
				list.AddRange(uielement.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x005A31FC File Offset: 0x005A13FC
		public void UpdateSize()
		{
			int num = base.GetDimensions().ToRectangle().Width / 44;
			this._itemsPerLine = num;
			int num2 = (int)Math.Ceiling((double)((float)this._contents.Count / (float)num));
			this.MinHeight.Set((float)(44 * num2), 0f);
		}

		// Token: 0x040054AF RID: 21679
		private List<TEntry> _contents = new List<TEntry>();

		// Token: 0x040054B0 RID: 21680
		private int _itemsPerLine;

		// Token: 0x040054B1 RID: 21681
		private const int sizePerEntryX = 44;

		// Token: 0x040054B2 RID: 21682
		private const int sizePerEntryY = 44;

		// Token: 0x040054B3 RID: 21683
		private List<SnapPoint> _dummySnapPoints = new List<SnapPoint>();
	}
}
