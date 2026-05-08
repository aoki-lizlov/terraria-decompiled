using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000402 RID: 1026
	public class UIList : UIElement, IEnumerable<UIElement>, IEnumerable
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06002F17 RID: 12055 RVA: 0x005B1101 File Offset: 0x005AF301
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x005B1110 File Offset: 0x005AF310
		public UIList()
		{
			this._innerList.OverflowHidden = false;
			this._innerList.Width.Set(0f, 1f);
			this._innerList.Height.Set(0f, 1f);
			this.OverflowHidden = true;
			base.Append(this._innerList);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x005B1197 File Offset: 0x005AF397
		public float GetTotalHeight()
		{
			return this._innerListHeight;
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x005B11A0 File Offset: 0x005AF3A0
		public void Goto(UIList.ElementSearchMethod searchMethod)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				if (searchMethod(this._items[i]))
				{
					this._scrollbar.ViewPosition = this._items[i].Top.Pixels;
					return;
				}
			}
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x005B11F9 File Offset: 0x005AF3F9
		public virtual void Add(UIElement item)
		{
			this._items.Add(item);
			this._innerList.Append(item);
			this.UpdateOrder();
			this._innerList.Recalculate();
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x005B1224 File Offset: 0x005AF424
		public virtual bool Remove(UIElement item)
		{
			this._innerList.RemoveChild(item);
			this.UpdateOrder();
			return this._items.Remove(item);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x005B1244 File Offset: 0x005AF444
		public virtual void Clear()
		{
			this._innerList.RemoveAllChildren();
			this._items.Clear();
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x005B125C File Offset: 0x005AF45C
		public override void Recalculate()
		{
			base.Recalculate();
			this.UpdateScrollbar();
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x005B126A File Offset: 0x005AF46A
		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			base.ScrollWheel(evt);
			if (this._scrollbar != null)
			{
				this._scrollbar.ViewPosition -= (float)evt.ScrollWheelValue;
			}
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x005B1294 File Offset: 0x005AF494
		public override void RecalculateChildren()
		{
			base.RecalculateChildren();
			float num = 0f;
			for (int i = 0; i < this._items.Count; i++)
			{
				float num2 = ((this._items.Count == 1) ? 0f : this.ListPadding);
				this._items[i].Top.Set(num, 0f);
				this._items[i].Recalculate();
				CalculatedStyle outerDimensions = this._items[i].GetOuterDimensions();
				num += outerDimensions.Height + num2;
			}
			this._innerListHeight = num;
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x005B1330 File Offset: 0x005AF530
		private void UpdateScrollbar()
		{
			if (this._scrollbar == null)
			{
				return;
			}
			float height = base.GetInnerDimensions().Height;
			this._scrollbar.SetView(height, this._innerListHeight);
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x005B1364 File Offset: 0x005AF564
		public void SetScrollbar(UIScrollbar scrollbar)
		{
			this._scrollbar = scrollbar;
			this.UpdateScrollbar();
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x005B1373 File Offset: 0x005AF573
		public void UpdateOrder()
		{
			if (this.ManualSortMethod != null)
			{
				this.ManualSortMethod(this._items);
			}
			else
			{
				this._items.Sort(new Comparison<UIElement>(this.SortMethod));
			}
			this.UpdateScrollbar();
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x005B13AD File Offset: 0x005AF5AD
		public int SortMethod(UIElement item1, UIElement item2)
		{
			return item1.CompareTo(item2);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x005B13B8 File Offset: 0x005AF5B8
		public override List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			SnapPoint snapPoint;
			if (base.GetSnapPoint(out snapPoint))
			{
				list.Add(snapPoint);
			}
			foreach (UIElement uielement in this._items)
			{
				list.AddRange(uielement.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x005B1428 File Offset: 0x005AF628
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._scrollbar != null)
			{
				this._innerList.Top.Set(-this._scrollbar.GetValue(), 0f);
			}
			this.Recalculate();
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x005B1459 File Offset: 0x005AF659
		public IEnumerator<UIElement> GetEnumerator()
		{
			return ((IEnumerable<UIElement>)this._items).GetEnumerator();
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x005B1459 File Offset: 0x005AF659
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<UIElement>)this._items).GetEnumerator();
		}

		// Token: 0x04005638 RID: 22072
		protected List<UIElement> _items = new List<UIElement>();

		// Token: 0x04005639 RID: 22073
		protected UIScrollbar _scrollbar;

		// Token: 0x0400563A RID: 22074
		private UIElement _innerList = new UIList.UIInnerList();

		// Token: 0x0400563B RID: 22075
		private float _innerListHeight;

		// Token: 0x0400563C RID: 22076
		public float ListPadding = 5f;

		// Token: 0x0400563D RID: 22077
		public Action<List<UIElement>> ManualSortMethod;

		// Token: 0x02000935 RID: 2357
		// (Invoke) Token: 0x0600481E RID: 18462
		public delegate bool ElementSearchMethod(UIElement element);

		// Token: 0x02000936 RID: 2358
		private class UIInnerList : UIElement
		{
			// Token: 0x06004821 RID: 18465 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool ContainsPoint(Vector2 point)
			{
				return true;
			}

			// Token: 0x06004822 RID: 18466 RVA: 0x006CC8F0 File Offset: 0x006CAAF0
			protected override void DrawChildren(SpriteBatch spriteBatch)
			{
				Vector2 vector = base.Parent.GetDimensions().Position();
				Vector2 vector2 = new Vector2(base.Parent.GetDimensions().Width, base.Parent.GetDimensions().Height);
				foreach (UIElement uielement in this.Elements)
				{
					Vector2 vector3 = uielement.GetDimensions().Position();
					Vector2 vector4 = new Vector2(uielement.GetDimensions().Width, uielement.GetDimensions().Height);
					if (Collision.CheckAABBvAABBCollision(vector, vector2, vector3, vector4))
					{
						uielement.Draw(spriteBatch);
					}
				}
			}

			// Token: 0x06004823 RID: 18467 RVA: 0x006CC9C0 File Offset: 0x006CABC0
			public override Rectangle GetViewCullingArea()
			{
				return base.Parent.GetDimensions().ToRectangle();
			}

			// Token: 0x06004824 RID: 18468 RVA: 0x005A2DAD File Offset: 0x005A0FAD
			public UIInnerList()
			{
			}
		}
	}
}
