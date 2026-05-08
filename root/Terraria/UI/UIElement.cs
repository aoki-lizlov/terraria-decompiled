using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent.UI.Elements;

namespace Terraria.UI
{
	// Token: 0x020000FC RID: 252
	public class UIElement : IComparable
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x004F3AA6 File Offset: 0x004F1CA6
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x004F3AAE File Offset: 0x004F1CAE
		public UIElement Parent
		{
			[CompilerGenerated]
			get
			{
				return this.<Parent>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Parent>k__BackingField = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x004F3AB7 File Offset: 0x004F1CB7
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x004F3ABF File Offset: 0x004F1CBF
		public int UniqueId
		{
			[CompilerGenerated]
			get
			{
				return this.<UniqueId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UniqueId>k__BackingField = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x004F3AC8 File Offset: 0x004F1CC8
		public IEnumerable<UIElement> Children
		{
			get
			{
				return this.Elements;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060019DC RID: 6620 RVA: 0x004F3AD0 File Offset: 0x004F1CD0
		// (remove) Token: 0x060019DD RID: 6621 RVA: 0x004F3B08 File Offset: 0x004F1D08
		public event UIElement.MouseEvent OnLeftMouseDown
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnLeftMouseDown;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnLeftMouseDown, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnLeftMouseDown;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnLeftMouseDown, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060019DE RID: 6622 RVA: 0x004F3B40 File Offset: 0x004F1D40
		// (remove) Token: 0x060019DF RID: 6623 RVA: 0x004F3B78 File Offset: 0x004F1D78
		public event UIElement.MouseEvent OnLeftMouseUp
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnLeftMouseUp;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnLeftMouseUp, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnLeftMouseUp;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnLeftMouseUp, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060019E0 RID: 6624 RVA: 0x004F3BB0 File Offset: 0x004F1DB0
		// (remove) Token: 0x060019E1 RID: 6625 RVA: 0x004F3BE8 File Offset: 0x004F1DE8
		public event UIElement.MouseEvent OnLeftClick
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnLeftClick;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnLeftClick, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnLeftClick;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnLeftClick, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060019E2 RID: 6626 RVA: 0x004F3C20 File Offset: 0x004F1E20
		// (remove) Token: 0x060019E3 RID: 6627 RVA: 0x004F3C58 File Offset: 0x004F1E58
		public event UIElement.MouseEvent OnLeftDoubleClick
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnLeftDoubleClick;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnLeftDoubleClick, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnLeftDoubleClick;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnLeftDoubleClick, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060019E4 RID: 6628 RVA: 0x004F3C90 File Offset: 0x004F1E90
		// (remove) Token: 0x060019E5 RID: 6629 RVA: 0x004F3CC8 File Offset: 0x004F1EC8
		public event UIElement.MouseEvent OnRightMouseDown
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnRightMouseDown;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnRightMouseDown, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnRightMouseDown;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnRightMouseDown, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060019E6 RID: 6630 RVA: 0x004F3D00 File Offset: 0x004F1F00
		// (remove) Token: 0x060019E7 RID: 6631 RVA: 0x004F3D38 File Offset: 0x004F1F38
		public event UIElement.MouseEvent OnRightMouseUp
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnRightMouseUp;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnRightMouseUp, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnRightMouseUp;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnRightMouseUp, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060019E8 RID: 6632 RVA: 0x004F3D70 File Offset: 0x004F1F70
		// (remove) Token: 0x060019E9 RID: 6633 RVA: 0x004F3DA8 File Offset: 0x004F1FA8
		public event UIElement.MouseEvent OnRightClick
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnRightClick;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnRightClick, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnRightClick;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnRightClick, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060019EA RID: 6634 RVA: 0x004F3DE0 File Offset: 0x004F1FE0
		// (remove) Token: 0x060019EB RID: 6635 RVA: 0x004F3E18 File Offset: 0x004F2018
		public event UIElement.MouseEvent OnRightDoubleClick
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnRightDoubleClick;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnRightDoubleClick, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnRightDoubleClick;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnRightDoubleClick, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060019EC RID: 6636 RVA: 0x004F3E50 File Offset: 0x004F2050
		// (remove) Token: 0x060019ED RID: 6637 RVA: 0x004F3E88 File Offset: 0x004F2088
		public event UIElement.MouseEvent OnMouseOver
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnMouseOver;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnMouseOver, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnMouseOver;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnMouseOver, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060019EE RID: 6638 RVA: 0x004F3EC0 File Offset: 0x004F20C0
		// (remove) Token: 0x060019EF RID: 6639 RVA: 0x004F3EF8 File Offset: 0x004F20F8
		public event UIElement.MouseEvent OnMouseOut
		{
			[CompilerGenerated]
			add
			{
				UIElement.MouseEvent mouseEvent = this.OnMouseOut;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Combine(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnMouseOut, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.MouseEvent mouseEvent = this.OnMouseOut;
				UIElement.MouseEvent mouseEvent2;
				do
				{
					mouseEvent2 = mouseEvent;
					UIElement.MouseEvent mouseEvent3 = (UIElement.MouseEvent)Delegate.Remove(mouseEvent2, value);
					mouseEvent = Interlocked.CompareExchange<UIElement.MouseEvent>(ref this.OnMouseOut, mouseEvent3, mouseEvent2);
				}
				while (mouseEvent != mouseEvent2);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060019F0 RID: 6640 RVA: 0x004F3F30 File Offset: 0x004F2130
		// (remove) Token: 0x060019F1 RID: 6641 RVA: 0x004F3F68 File Offset: 0x004F2168
		public event UIElement.ScrollWheelEvent OnScrollWheel
		{
			[CompilerGenerated]
			add
			{
				UIElement.ScrollWheelEvent scrollWheelEvent = this.OnScrollWheel;
				UIElement.ScrollWheelEvent scrollWheelEvent2;
				do
				{
					scrollWheelEvent2 = scrollWheelEvent;
					UIElement.ScrollWheelEvent scrollWheelEvent3 = (UIElement.ScrollWheelEvent)Delegate.Combine(scrollWheelEvent2, value);
					scrollWheelEvent = Interlocked.CompareExchange<UIElement.ScrollWheelEvent>(ref this.OnScrollWheel, scrollWheelEvent3, scrollWheelEvent2);
				}
				while (scrollWheelEvent != scrollWheelEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.ScrollWheelEvent scrollWheelEvent = this.OnScrollWheel;
				UIElement.ScrollWheelEvent scrollWheelEvent2;
				do
				{
					scrollWheelEvent2 = scrollWheelEvent;
					UIElement.ScrollWheelEvent scrollWheelEvent3 = (UIElement.ScrollWheelEvent)Delegate.Remove(scrollWheelEvent2, value);
					scrollWheelEvent = Interlocked.CompareExchange<UIElement.ScrollWheelEvent>(ref this.OnScrollWheel, scrollWheelEvent3, scrollWheelEvent2);
				}
				while (scrollWheelEvent != scrollWheelEvent2);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060019F2 RID: 6642 RVA: 0x004F3FA0 File Offset: 0x004F21A0
		// (remove) Token: 0x060019F3 RID: 6643 RVA: 0x004F3FD8 File Offset: 0x004F21D8
		public event UIElement.ElementEvent OnUpdate
		{
			[CompilerGenerated]
			add
			{
				UIElement.ElementEvent elementEvent = this.OnUpdate;
				UIElement.ElementEvent elementEvent2;
				do
				{
					elementEvent2 = elementEvent;
					UIElement.ElementEvent elementEvent3 = (UIElement.ElementEvent)Delegate.Combine(elementEvent2, value);
					elementEvent = Interlocked.CompareExchange<UIElement.ElementEvent>(ref this.OnUpdate, elementEvent3, elementEvent2);
				}
				while (elementEvent != elementEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.ElementEvent elementEvent = this.OnUpdate;
				UIElement.ElementEvent elementEvent2;
				do
				{
					elementEvent2 = elementEvent;
					UIElement.ElementEvent elementEvent3 = (UIElement.ElementEvent)Delegate.Remove(elementEvent2, value);
					elementEvent = Interlocked.CompareExchange<UIElement.ElementEvent>(ref this.OnUpdate, elementEvent3, elementEvent2);
				}
				while (elementEvent != elementEvent2);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060019F4 RID: 6644 RVA: 0x004F4010 File Offset: 0x004F2210
		// (remove) Token: 0x060019F5 RID: 6645 RVA: 0x004F4048 File Offset: 0x004F2248
		public event UIElement.DrawEvent OnDraw
		{
			[CompilerGenerated]
			add
			{
				UIElement.DrawEvent drawEvent = this.OnDraw;
				UIElement.DrawEvent drawEvent2;
				do
				{
					drawEvent2 = drawEvent;
					UIElement.DrawEvent drawEvent3 = (UIElement.DrawEvent)Delegate.Combine(drawEvent2, value);
					drawEvent = Interlocked.CompareExchange<UIElement.DrawEvent>(ref this.OnDraw, drawEvent3, drawEvent2);
				}
				while (drawEvent != drawEvent2);
			}
			[CompilerGenerated]
			remove
			{
				UIElement.DrawEvent drawEvent = this.OnDraw;
				UIElement.DrawEvent drawEvent2;
				do
				{
					drawEvent2 = drawEvent;
					UIElement.DrawEvent drawEvent3 = (UIElement.DrawEvent)Delegate.Remove(drawEvent2, value);
					drawEvent = Interlocked.CompareExchange<UIElement.DrawEvent>(ref this.OnDraw, drawEvent3, drawEvent2);
				}
				while (drawEvent != drawEvent2);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x004F407D File Offset: 0x004F227D
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x004F4085 File Offset: 0x004F2285
		public bool IsMouseHovering
		{
			[CompilerGenerated]
			get
			{
				return this.<IsMouseHovering>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsMouseHovering>k__BackingField = value;
			}
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x004F4090 File Offset: 0x004F2290
		public UIElement()
		{
			this.UniqueId = UIElement._idCounter++;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x004F40F0 File Offset: 0x004F22F0
		public void SetSnapPoint(string name, int id, Vector2? anchor = null, Vector2? offset = null)
		{
			if (anchor == null)
			{
				anchor = new Vector2?(new Vector2(0.5f));
			}
			if (offset == null)
			{
				offset = new Vector2?(Vector2.Zero);
			}
			this._snapPoint = new SnapPoint(name, id, anchor.Value, offset.Value);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x004F4147 File Offset: 0x004F2347
		public bool GetSnapPoint(out SnapPoint point)
		{
			point = this._snapPoint;
			if (this._snapPoint != null)
			{
				this._snapPoint.Calculate(this);
			}
			return this._snapPoint != null;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x004F4170 File Offset: 0x004F2370
		public virtual void ExecuteRecursively(UIElement.UIElementAction action)
		{
			action(this);
			foreach (UIElement uielement in this.Elements)
			{
				uielement.ExecuteRecursively(action);
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void DrawSelf(SpriteBatch spriteBatch)
		{
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x004F41C8 File Offset: 0x004F23C8
		protected virtual void DrawChildren(SpriteBatch spriteBatch)
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Draw(spriteBatch);
			}
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x004F421C File Offset: 0x004F241C
		public void Append(UIElement element)
		{
			element.Remove();
			element.Parent = this;
			this.Elements.Add(element);
			element.Recalculate();
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x004F423D File Offset: 0x004F243D
		public void Remove()
		{
			if (this.Parent != null)
			{
				this.Parent.RemoveChild(this);
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x004F4253 File Offset: 0x004F2453
		public void RemoveChild(UIElement child)
		{
			this.Elements.Remove(child);
			child.Parent = null;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x004F426C File Offset: 0x004F246C
		public void RemoveAllChildren()
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Parent = null;
			}
			this.Elements.Clear();
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x004F42C8 File Offset: 0x004F24C8
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (this.OnDraw != null)
			{
				this.OnDraw(this, spriteBatch);
			}
			bool overflowHidden = this.OverflowHidden;
			bool useImmediateMode = this.UseImmediateMode;
			RasterizerState rasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
			Rectangle scissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
			SamplerState anisotropicClamp = SamplerState.AnisotropicClamp;
			if (useImmediateMode || this.OverrideSamplerState != null)
			{
				spriteBatch.End();
				spriteBatch.Begin(useImmediateMode ? SpriteSortMode.Immediate : SpriteSortMode.Deferred, BlendState.AlphaBlend, (this.OverrideSamplerState != null) ? this.OverrideSamplerState : anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
				this.DrawSelf(spriteBatch);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
			}
			else
			{
				this.DrawSelf(spriteBatch);
			}
			if (overflowHidden)
			{
				spriteBatch.End();
				Rectangle clippingRectangle = this.GetClippingRectangle(spriteBatch);
				spriteBatch.GraphicsDevice.ScissorRectangle = clippingRectangle;
				spriteBatch.GraphicsDevice.RasterizerState = UIElement.OverflowHiddenRasterizerState;
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, UIElement.OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
			}
			this.DrawChildren(spriteBatch);
			if (overflowHidden)
			{
				spriteBatch.End();
				spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle;
				spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, rasterizerState, null, Main.UIScaleMatrix);
			}
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x004F4418 File Offset: 0x004F2618
		public virtual void Update(GameTime gameTime)
		{
			if (this.OnUpdate != null)
			{
				this.OnUpdate(this);
			}
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Update(gameTime);
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x004F4480 File Offset: 0x004F2680
		public Rectangle GetClippingRectangle(SpriteBatch spriteBatch)
		{
			Vector2 vector = new Vector2(this._innerDimensions.X, this._innerDimensions.Y);
			Vector2 vector2 = new Vector2(this._innerDimensions.Width, this._innerDimensions.Height) + vector;
			vector = Vector2.Transform(vector, Main.UIScaleMatrix);
			vector2 = Vector2.Transform(vector2, Main.UIScaleMatrix);
			Rectangle rectangle = new Rectangle((int)vector.X, (int)vector.Y, (int)(vector2.X - vector.X), (int)(vector2.Y - vector.Y));
			int num = (int)((float)Main.screenWidth * Main.UIScale);
			int num2 = (int)((float)Main.screenHeight * Main.UIScale);
			rectangle.X = Utils.Clamp<int>(rectangle.X, 0, num);
			rectangle.Y = Utils.Clamp<int>(rectangle.Y, 0, num2);
			rectangle.Width = Utils.Clamp<int>(rectangle.Width, 0, num - rectangle.X);
			rectangle.Height = Utils.Clamp<int>(rectangle.Height, 0, num2 - rectangle.Y);
			Rectangle scissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
			int num3 = Utils.Clamp<int>(rectangle.Left, scissorRectangle.Left, scissorRectangle.Right);
			int num4 = Utils.Clamp<int>(rectangle.Top, scissorRectangle.Top, scissorRectangle.Bottom);
			int num5 = Utils.Clamp<int>(rectangle.Right, scissorRectangle.Left, scissorRectangle.Right);
			int num6 = Utils.Clamp<int>(rectangle.Bottom, scissorRectangle.Top, scissorRectangle.Bottom);
			return new Rectangle(num3, num4, num5 - num3, num6 - num4);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x004F4624 File Offset: 0x004F2824
		public virtual List<SnapPoint> GetSnapPoints()
		{
			List<SnapPoint> list = new List<SnapPoint>();
			SnapPoint snapPoint;
			if (this.GetSnapPoint(out snapPoint))
			{
				list.Add(snapPoint);
			}
			foreach (UIElement uielement in this.Elements)
			{
				list.AddRange(uielement.GetSnapPoints());
			}
			return list;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x004F4694 File Offset: 0x004F2894
		public virtual void Recalculate()
		{
			CalculatedStyle calculatedStyle;
			if (this.Parent != null)
			{
				calculatedStyle = this.Parent.GetInnerDimensions();
			}
			else
			{
				calculatedStyle = UserInterface.ActiveInstance.GetDimensions();
			}
			if (this.Parent != null && this.Parent is UIList)
			{
				calculatedStyle.Height = float.MaxValue;
			}
			CalculatedStyle dimensionsBasedOnParentDimensions = this.GetDimensionsBasedOnParentDimensions(calculatedStyle);
			this._outerDimensions = dimensionsBasedOnParentDimensions;
			dimensionsBasedOnParentDimensions.X += this.MarginLeft;
			dimensionsBasedOnParentDimensions.Y += this.MarginTop;
			dimensionsBasedOnParentDimensions.Width -= this.MarginLeft + this.MarginRight;
			dimensionsBasedOnParentDimensions.Height -= this.MarginTop + this.MarginBottom;
			this._dimensions = dimensionsBasedOnParentDimensions;
			dimensionsBasedOnParentDimensions.X += this.PaddingLeft;
			dimensionsBasedOnParentDimensions.Y += this.PaddingTop;
			dimensionsBasedOnParentDimensions.Width -= this.PaddingLeft + this.PaddingRight;
			dimensionsBasedOnParentDimensions.Height -= this.PaddingTop + this.PaddingBottom;
			this._innerDimensions = dimensionsBasedOnParentDimensions;
			this.RecalculateChildren();
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x004F47AC File Offset: 0x004F29AC
		private CalculatedStyle GetDimensionsBasedOnParentDimensions(CalculatedStyle parentDimensions)
		{
			CalculatedStyle calculatedStyle;
			calculatedStyle.X = this.Left.GetValue(parentDimensions.Width) + parentDimensions.X;
			calculatedStyle.Y = this.Top.GetValue(parentDimensions.Height) + parentDimensions.Y;
			float value = this.MinWidth.GetValue(parentDimensions.Width);
			float value2 = this.MaxWidth.GetValue(parentDimensions.Width);
			float value3 = this.MinHeight.GetValue(parentDimensions.Height);
			float value4 = this.MaxHeight.GetValue(parentDimensions.Height);
			calculatedStyle.Width = MathHelper.Clamp(this.Width.GetValue(parentDimensions.Width), value, value2);
			calculatedStyle.Height = MathHelper.Clamp(this.Height.GetValue(parentDimensions.Height), value3, value4);
			calculatedStyle.Width += this.MarginLeft + this.MarginRight;
			calculatedStyle.Height += this.MarginTop + this.MarginBottom;
			calculatedStyle.X += parentDimensions.Width * this.HAlign - calculatedStyle.Width * this.HAlign;
			calculatedStyle.Y += parentDimensions.Height * this.VAlign - calculatedStyle.Height * this.VAlign;
			return calculatedStyle;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x004F48FC File Offset: 0x004F2AFC
		public UIElement GetElementAt(Vector2 point)
		{
			UIElement uielement = null;
			for (int i = this.Elements.Count - 1; i >= 0; i--)
			{
				UIElement uielement2 = this.Elements[i];
				if (!uielement2.IgnoresMouseInteraction && uielement2.ContainsPoint(point))
				{
					uielement = uielement2;
					if (!uielement2.PassThroughMouseInteraction)
					{
						break;
					}
				}
			}
			if (uielement != null)
			{
				return uielement.GetElementAt(point);
			}
			if (this.IgnoresMouseInteraction)
			{
				return null;
			}
			if (this.ContainsPoint(point))
			{
				return this;
			}
			return null;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x004F496C File Offset: 0x004F2B6C
		public virtual bool ContainsPoint(Vector2 point)
		{
			return point.X > this._dimensions.X && point.Y > this._dimensions.Y && point.X < this._dimensions.X + this._dimensions.Width && point.Y < this._dimensions.Y + this._dimensions.Height;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x004F49DF File Offset: 0x004F2BDF
		public virtual Rectangle GetViewCullingArea()
		{
			return this._dimensions.ToRectangle();
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x004F49EC File Offset: 0x004F2BEC
		public void SetPadding(float pixels)
		{
			this.PaddingBottom = pixels;
			this.PaddingLeft = pixels;
			this.PaddingRight = pixels;
			this.PaddingTop = pixels;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x004F4A0C File Offset: 0x004F2C0C
		public virtual void RecalculateChildren()
		{
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Recalculate();
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x004F4A5C File Offset: 0x004F2C5C
		public CalculatedStyle GetInnerDimensions()
		{
			return this._innerDimensions;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x004F4A64 File Offset: 0x004F2C64
		public CalculatedStyle GetDimensions()
		{
			return this._dimensions;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x004F4A6C File Offset: 0x004F2C6C
		public CalculatedStyle GetOuterDimensions()
		{
			return this._outerDimensions;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x004F4A74 File Offset: 0x004F2C74
		public void CopyStyle(UIElement element)
		{
			this.Top = element.Top;
			this.Left = element.Left;
			this.Width = element.Width;
			this.Height = element.Height;
			this.PaddingBottom = element.PaddingBottom;
			this.PaddingLeft = element.PaddingLeft;
			this.PaddingRight = element.PaddingRight;
			this.PaddingTop = element.PaddingTop;
			this.HAlign = element.HAlign;
			this.VAlign = element.VAlign;
			this.MinWidth = element.MinWidth;
			this.MaxWidth = element.MaxWidth;
			this.MinHeight = element.MinHeight;
			this.MaxHeight = element.MaxHeight;
			this.Recalculate();
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x004F4B2F File Offset: 0x004F2D2F
		public virtual void LeftMouseDown(UIMouseEvent evt)
		{
			if (this.OnLeftMouseDown != null)
			{
				this.OnLeftMouseDown(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.LeftMouseDown(evt);
			}
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x004F4B5A File Offset: 0x004F2D5A
		public virtual void LeftMouseUp(UIMouseEvent evt)
		{
			if (this.OnLeftMouseUp != null)
			{
				this.OnLeftMouseUp(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.LeftMouseUp(evt);
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x004F4B85 File Offset: 0x004F2D85
		public virtual void LeftClick(UIMouseEvent evt)
		{
			if (this.OnLeftClick != null)
			{
				this.OnLeftClick(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.LeftClick(evt);
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x004F4BB0 File Offset: 0x004F2DB0
		public virtual void LeftDoubleClick(UIMouseEvent evt)
		{
			if (this.OnLeftDoubleClick != null)
			{
				this.OnLeftDoubleClick(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.LeftDoubleClick(evt);
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x004F4BDB File Offset: 0x004F2DDB
		public virtual void RightMouseDown(UIMouseEvent evt)
		{
			if (this.OnRightMouseDown != null)
			{
				this.OnRightMouseDown(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.RightMouseDown(evt);
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x004F4C06 File Offset: 0x004F2E06
		public virtual void RightMouseUp(UIMouseEvent evt)
		{
			if (this.OnRightMouseUp != null)
			{
				this.OnRightMouseUp(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.RightMouseUp(evt);
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x004F4C31 File Offset: 0x004F2E31
		public virtual void RightClick(UIMouseEvent evt)
		{
			if (this.OnRightClick != null)
			{
				this.OnRightClick(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.RightClick(evt);
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x004F4C5C File Offset: 0x004F2E5C
		public virtual void RightDoubleClick(UIMouseEvent evt)
		{
			if (this.OnRightDoubleClick != null)
			{
				this.OnRightDoubleClick(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.RightDoubleClick(evt);
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x004F4C87 File Offset: 0x004F2E87
		public virtual void MouseOver(UIMouseEvent evt)
		{
			this.IsMouseHovering = true;
			if (this.OnMouseOver != null)
			{
				this.OnMouseOver(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.MouseOver(evt);
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x004F4CB9 File Offset: 0x004F2EB9
		public virtual void MouseOut(UIMouseEvent evt)
		{
			this.IsMouseHovering = false;
			if (this.OnMouseOut != null)
			{
				this.OnMouseOut(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.MouseOut(evt);
			}
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x004F4CEB File Offset: 0x004F2EEB
		public virtual void ScrollWheel(UIScrollWheelEvent evt)
		{
			if (this.OnScrollWheel != null)
			{
				this.OnScrollWheel(evt, this);
			}
			if (this.Parent != null)
			{
				this.Parent.ScrollWheel(evt);
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x004F4D18 File Offset: 0x004F2F18
		public void Activate()
		{
			if (!this._isInitialized)
			{
				this.Initialize();
			}
			this.OnActivate();
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Activate();
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnActivate()
		{
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x004F4D7C File Offset: 0x004F2F7C
		[Conditional("DEBUG")]
		public void DrawDebugHitbox(BasicDebugDrawer drawer, float colorIntensity = 0f)
		{
			if (this.IsMouseHovering)
			{
				colorIntensity += 0.1f;
			}
			Color color = Main.hslToRgb(colorIntensity, colorIntensity, 0.5f, byte.MaxValue);
			CalculatedStyle innerDimensions = this.GetInnerDimensions();
			drawer.DrawLine(innerDimensions.Position(), innerDimensions.Position() + new Vector2(innerDimensions.Width, 0f), 2f, color);
			drawer.DrawLine(innerDimensions.Position() + new Vector2(innerDimensions.Width, 0f), innerDimensions.Position() + new Vector2(innerDimensions.Width, innerDimensions.Height), 2f, color);
			drawer.DrawLine(innerDimensions.Position() + new Vector2(innerDimensions.Width, innerDimensions.Height), innerDimensions.Position() + new Vector2(0f, innerDimensions.Height), 2f, color);
			drawer.DrawLine(innerDimensions.Position() + new Vector2(0f, innerDimensions.Height), innerDimensions.Position(), 2f, color);
			foreach (UIElement uielement in this.Elements)
			{
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x004F4EDC File Offset: 0x004F30DC
		public void Deactivate()
		{
			this.OnDeactivate();
			foreach (UIElement uielement in this.Elements)
			{
				uielement.Deactivate();
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnDeactivate()
		{
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x004F4F34 File Offset: 0x004F3134
		public void Initialize()
		{
			this.OnInitialize();
			this._isInitialized = true;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnInitialize()
		{
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public virtual int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x004F4F43 File Offset: 0x004F3143
		// Note: this type is marked as 'beforefieldinit'.
		static UIElement()
		{
		}

		// Token: 0x0400136B RID: 4971
		[CompilerGenerated]
		private UIElement <Parent>k__BackingField;

		// Token: 0x0400136C RID: 4972
		protected readonly List<UIElement> Elements = new List<UIElement>();

		// Token: 0x0400136D RID: 4973
		[CompilerGenerated]
		private int <UniqueId>k__BackingField;

		// Token: 0x0400136E RID: 4974
		public StyleDimension Top;

		// Token: 0x0400136F RID: 4975
		public StyleDimension Left;

		// Token: 0x04001370 RID: 4976
		public StyleDimension Width;

		// Token: 0x04001371 RID: 4977
		public StyleDimension Height;

		// Token: 0x04001372 RID: 4978
		public StyleDimension MaxWidth = StyleDimension.Fill;

		// Token: 0x04001373 RID: 4979
		public StyleDimension MaxHeight = StyleDimension.Fill;

		// Token: 0x04001374 RID: 4980
		public StyleDimension MinWidth = StyleDimension.Empty;

		// Token: 0x04001375 RID: 4981
		public StyleDimension MinHeight = StyleDimension.Empty;

		// Token: 0x04001376 RID: 4982
		[CompilerGenerated]
		private UIElement.MouseEvent OnLeftMouseDown;

		// Token: 0x04001377 RID: 4983
		[CompilerGenerated]
		private UIElement.MouseEvent OnLeftMouseUp;

		// Token: 0x04001378 RID: 4984
		[CompilerGenerated]
		private UIElement.MouseEvent OnLeftClick;

		// Token: 0x04001379 RID: 4985
		[CompilerGenerated]
		private UIElement.MouseEvent OnLeftDoubleClick;

		// Token: 0x0400137A RID: 4986
		[CompilerGenerated]
		private UIElement.MouseEvent OnRightMouseDown;

		// Token: 0x0400137B RID: 4987
		[CompilerGenerated]
		private UIElement.MouseEvent OnRightMouseUp;

		// Token: 0x0400137C RID: 4988
		[CompilerGenerated]
		private UIElement.MouseEvent OnRightClick;

		// Token: 0x0400137D RID: 4989
		[CompilerGenerated]
		private UIElement.MouseEvent OnRightDoubleClick;

		// Token: 0x0400137E RID: 4990
		[CompilerGenerated]
		private UIElement.MouseEvent OnMouseOver;

		// Token: 0x0400137F RID: 4991
		[CompilerGenerated]
		private UIElement.MouseEvent OnMouseOut;

		// Token: 0x04001380 RID: 4992
		[CompilerGenerated]
		private UIElement.ScrollWheelEvent OnScrollWheel;

		// Token: 0x04001381 RID: 4993
		[CompilerGenerated]
		private UIElement.ElementEvent OnUpdate;

		// Token: 0x04001382 RID: 4994
		[CompilerGenerated]
		private UIElement.DrawEvent OnDraw;

		// Token: 0x04001383 RID: 4995
		private bool _isInitialized;

		// Token: 0x04001384 RID: 4996
		public bool IgnoresMouseInteraction;

		// Token: 0x04001385 RID: 4997
		public bool PassThroughMouseInteraction;

		// Token: 0x04001386 RID: 4998
		public bool OverflowHidden;

		// Token: 0x04001387 RID: 4999
		public SamplerState OverrideSamplerState;

		// Token: 0x04001388 RID: 5000
		public float PaddingTop;

		// Token: 0x04001389 RID: 5001
		public float PaddingLeft;

		// Token: 0x0400138A RID: 5002
		public float PaddingRight;

		// Token: 0x0400138B RID: 5003
		public float PaddingBottom;

		// Token: 0x0400138C RID: 5004
		public float MarginTop;

		// Token: 0x0400138D RID: 5005
		public float MarginLeft;

		// Token: 0x0400138E RID: 5006
		public float MarginRight;

		// Token: 0x0400138F RID: 5007
		public float MarginBottom;

		// Token: 0x04001390 RID: 5008
		public float HAlign;

		// Token: 0x04001391 RID: 5009
		public float VAlign;

		// Token: 0x04001392 RID: 5010
		private CalculatedStyle _innerDimensions;

		// Token: 0x04001393 RID: 5011
		private CalculatedStyle _dimensions;

		// Token: 0x04001394 RID: 5012
		private CalculatedStyle _outerDimensions;

		// Token: 0x04001395 RID: 5013
		private static readonly RasterizerState OverflowHiddenRasterizerState = new RasterizerState
		{
			CullMode = CullMode.None,
			ScissorTestEnable = true
		};

		// Token: 0x04001396 RID: 5014
		public bool UseImmediateMode;

		// Token: 0x04001397 RID: 5015
		private SnapPoint _snapPoint;

		// Token: 0x04001398 RID: 5016
		[CompilerGenerated]
		private bool <IsMouseHovering>k__BackingField;

		// Token: 0x04001399 RID: 5017
		private static int _idCounter = 0;

		// Token: 0x02000714 RID: 1812
		// (Invoke) Token: 0x0600402E RID: 16430
		public delegate void MouseEvent(UIMouseEvent evt, UIElement listeningElement);

		// Token: 0x02000715 RID: 1813
		// (Invoke) Token: 0x06004032 RID: 16434
		public delegate void ScrollWheelEvent(UIScrollWheelEvent evt, UIElement listeningElement);

		// Token: 0x02000716 RID: 1814
		// (Invoke) Token: 0x06004036 RID: 16438
		public delegate void ElementEvent(UIElement affectedElement);

		// Token: 0x02000717 RID: 1815
		// (Invoke) Token: 0x0600403A RID: 16442
		public delegate void DrawEvent(UIElement affectedElement, SpriteBatch sb);

		// Token: 0x02000718 RID: 1816
		// (Invoke) Token: 0x0600403E RID: 16446
		public delegate void UIElementAction(UIElement element);
	}
}
