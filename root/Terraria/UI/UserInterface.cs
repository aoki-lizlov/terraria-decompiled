using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameInput;

namespace Terraria.UI
{
	// Token: 0x02000102 RID: 258
	public class UserInterface
	{
		// Token: 0x06001A2A RID: 6698 RVA: 0x004F4FC1 File Offset: 0x004F31C1
		public void ClearPointers()
		{
			this.LeftMouse.Clear();
			this.RightMouse.Clear();
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x004F4FD9 File Offset: 0x004F31D9
		public bool MouseCaptured()
		{
			return (this.LeftMouse.WasDown && this.LeftMouse.LastDown != null) || (this.RightMouse.WasDown && this.RightMouse.LastDown != null);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x004F5014 File Offset: 0x004F3214
		public void ResetLasts()
		{
			if (this._lastElementHover != null)
			{
				this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
			}
			this.ClearPointers();
			this._lastElementHover = null;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x004F5047 File Offset: 0x004F3247
		public void EscapeElements()
		{
			this.ResetLasts();
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x004F504F File Offset: 0x004F324F
		public UIState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x004F5058 File Offset: 0x004F3258
		public UserInterface()
		{
			UserInterface.InputPointerCache inputPointerCache = new UserInterface.InputPointerCache();
			inputPointerCache.MouseDownEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.LeftMouseDown(evt);
			};
			inputPointerCache.MouseUpEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.LeftMouseUp(evt);
			};
			inputPointerCache.ClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.LeftClick(evt);
			};
			inputPointerCache.DoubleClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.LeftDoubleClick(evt);
			};
			this.LeftMouse = inputPointerCache;
			UserInterface.InputPointerCache inputPointerCache2 = new UserInterface.InputPointerCache();
			inputPointerCache2.MouseDownEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.RightMouseDown(evt);
			};
			inputPointerCache2.MouseUpEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.RightMouseUp(evt);
			};
			inputPointerCache2.ClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.RightClick(evt);
			};
			inputPointerCache2.DoubleClickEvent = delegate(UIElement element, UIMouseEvent evt)
			{
				element.RightDoubleClick(evt);
			};
			this.RightMouse = inputPointerCache2;
			base..ctor();
			UserInterface.ActiveInstance = this;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x004F51BA File Offset: 0x004F33BA
		public void Use()
		{
			if (UserInterface.ActiveInstance != this)
			{
				UserInterface.ActiveInstance = this;
				this.Recalculate();
				return;
			}
			UserInterface.ActiveInstance = this;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x004F51D7 File Offset: 0x004F33D7
		private void ImmediatelyUpdateInputPointers()
		{
			this.LeftMouse.WasDown = Main.mouseLeft;
			this.RightMouse.WasDown = Main.mouseRight;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x004F51FC File Offset: 0x004F33FC
		private void ResetState()
		{
			if (!Main.dedServ)
			{
				this.GetMousePosition();
				this.ImmediatelyUpdateInputPointers();
				if (this._lastElementHover != null)
				{
					this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
				}
			}
			this.ClearPointers();
			this._lastElementHover = null;
			this._clickDisabledTimeRemaining = Math.Max(this._clickDisabledTimeRemaining, 200.0);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x004F5267 File Offset: 0x004F3467
		private void GetMousePosition()
		{
			this.MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x004F5280 File Offset: 0x004F3480
		public void Update(GameTime time)
		{
			if (this._currentState == null)
			{
				return;
			}
			bool flag = FocusHelper.AllowUIInputs;
			if (!Main.gameMenu && PlayerInput.IgnoreMouseInterface)
			{
				flag = false;
			}
			this.GetMousePosition();
			UIElement uielement = (flag ? this._currentState.GetElementAt(this.MousePosition) : null);
			this._clickDisabledTimeRemaining = Math.Max(0.0, this._clickDisabledTimeRemaining - time.ElapsedGameTime.TotalMilliseconds);
			bool flag2 = this._clickDisabledTimeRemaining > 0.0;
			if (uielement != this._lastElementHover)
			{
				if (this._lastElementHover != null)
				{
					this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
				}
				if (uielement != null)
				{
					uielement.MouseOver(new UIMouseEvent(uielement, this.MousePosition));
				}
				this._lastElementHover = uielement;
			}
			if (!flag2)
			{
				this.HandleClick(this.LeftMouse, time, Main.mouseLeft && flag, uielement);
				this.HandleClick(this.RightMouse, time, Main.mouseRight && flag, uielement);
			}
			if (PlayerInput.ScrollWheelDeltaForUI != 0)
			{
				if (uielement != null)
				{
					uielement.ScrollWheel(new UIScrollWheelEvent(uielement, this.MousePosition, PlayerInput.ScrollWheelDeltaForUI));
				}
				PlayerInput.ScrollWheelDeltaForUI = 0;
			}
			if (this._currentState != null)
			{
				this._currentState.Update(time);
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x004F53B4 File Offset: 0x004F35B4
		private void HandleClick(UserInterface.InputPointerCache cache, GameTime time, bool isDown, UIElement mouseElement)
		{
			if (isDown && !cache.WasDown && mouseElement != null)
			{
				cache.LastDown = mouseElement;
				cache.MouseDownEvent(mouseElement, new UIMouseEvent(mouseElement, this.MousePosition));
				if (cache.LastClicked == mouseElement && time.TotalGameTime.TotalMilliseconds - cache.LastTimeDown < 500.0)
				{
					cache.DoubleClickEvent(mouseElement, new UIMouseEvent(mouseElement, this.MousePosition));
					cache.LastClicked = null;
				}
				cache.LastTimeDown = time.TotalGameTime.TotalMilliseconds;
			}
			else if (!isDown && cache.WasDown && cache.LastDown != null)
			{
				UIElement lastDown = cache.LastDown;
				if (lastDown.ContainsPoint(this.MousePosition))
				{
					cache.ClickEvent(lastDown, new UIMouseEvent(lastDown, this.MousePosition));
					cache.LastClicked = cache.LastDown;
				}
				cache.MouseUpEvent(lastDown, new UIMouseEvent(lastDown, this.MousePosition));
				cache.LastDown = null;
			}
			cache.WasDown = isDown;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x004F54CE File Offset: 0x004F36CE
		public void Draw(SpriteBatch spriteBatch, GameTime time)
		{
			this.Use();
			if (this._currentState != null)
			{
				if (this._isStateDirty)
				{
					this._currentState.Recalculate();
					this._isStateDirty = false;
				}
				this._currentState.Draw(spriteBatch);
			}
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x004F5504 File Offset: 0x004F3704
		public void DrawDebugHitbox(BasicDebugDrawer drawer)
		{
			UIState currentState = this._currentState;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x004F5510 File Offset: 0x004F3710
		public void SetState(UIState state)
		{
			if (state == this._currentState)
			{
				return;
			}
			if (state != null)
			{
				this.AddToHistory(state);
			}
			if (this._currentState != null)
			{
				if (this._lastElementHover != null)
				{
					this._lastElementHover.MouseOut(new UIMouseEvent(this._lastElementHover, this.MousePosition));
				}
				this._currentState.Deactivate();
			}
			this._currentState = state;
			this.ResetState();
			if (state != null)
			{
				this._isStateDirty = true;
				state.Activate();
				state.Recalculate();
			}
			this.IsVisible = this._currentState != null;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x004F559C File Offset: 0x004F379C
		public void GoBack()
		{
			if (this._history.Count < 2)
			{
				return;
			}
			UIState uistate = this._history[this._history.Count - 2];
			this._history.RemoveRange(this._history.Count - 2, 2);
			this.SetState(uistate);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x004F55F1 File Offset: 0x004F37F1
		private void AddToHistory(UIState state)
		{
			this._history.Add(state);
			if (this._history.Count > 32)
			{
				this._history.RemoveRange(0, 4);
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x004F561B File Offset: 0x004F381B
		public void Recalculate()
		{
			if (this._currentState != null)
			{
				this._currentState.Recalculate();
			}
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x004F5630 File Offset: 0x004F3830
		public CalculatedStyle GetDimensions()
		{
			Vector2 originalScreenSize = PlayerInput.OriginalScreenSize;
			return new CalculatedStyle(0f, 0f, originalScreenSize.X / Main.UIScale, originalScreenSize.Y / Main.UIScale);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x004F566A File Offset: 0x004F386A
		internal void RefreshState()
		{
			if (this._currentState != null)
			{
				this._currentState.Deactivate();
			}
			this.ResetState();
			this._currentState.Activate();
			this._currentState.Recalculate();
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x004F569B File Offset: 0x004F389B
		public bool IsElementUnderMouse()
		{
			return this.IsVisible && this._lastElementHover != null && !(this._lastElementHover is UIState);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x004F56C0 File Offset: 0x004F38C0
		// Note: this type is marked as 'beforefieldinit'.
		static UserInterface()
		{
		}

		// Token: 0x0400139E RID: 5022
		private const double DOUBLE_CLICK_TIME = 500.0;

		// Token: 0x0400139F RID: 5023
		private const double STATE_CHANGE_CLICK_DISABLE_TIME = 200.0;

		// Token: 0x040013A0 RID: 5024
		private const int MAX_HISTORY_SIZE = 32;

		// Token: 0x040013A1 RID: 5025
		private const int HISTORY_PRUNE_SIZE = 4;

		// Token: 0x040013A2 RID: 5026
		public static UserInterface ActiveInstance = new UserInterface();

		// Token: 0x040013A3 RID: 5027
		private List<UIState> _history = new List<UIState>();

		// Token: 0x040013A4 RID: 5028
		private UserInterface.InputPointerCache LeftMouse;

		// Token: 0x040013A5 RID: 5029
		private UserInterface.InputPointerCache RightMouse;

		// Token: 0x040013A6 RID: 5030
		public Vector2 MousePosition;

		// Token: 0x040013A7 RID: 5031
		private UIElement _lastElementHover;

		// Token: 0x040013A8 RID: 5032
		private double _clickDisabledTimeRemaining;

		// Token: 0x040013A9 RID: 5033
		private bool _isStateDirty;

		// Token: 0x040013AA RID: 5034
		public bool IsVisible;

		// Token: 0x040013AB RID: 5035
		private UIState _currentState;

		// Token: 0x02000719 RID: 1817
		// (Invoke) Token: 0x06004042 RID: 16450
		private delegate void MouseElementEvent(UIElement element, UIMouseEvent evt);

		// Token: 0x0200071A RID: 1818
		private class InputPointerCache
		{
			// Token: 0x06004045 RID: 16453 RVA: 0x0069D81F File Offset: 0x0069BA1F
			public void Clear()
			{
				this.LastClicked = null;
				this.LastDown = null;
				this.LastTimeDown = 0.0;
			}

			// Token: 0x06004046 RID: 16454 RVA: 0x0000357B File Offset: 0x0000177B
			public InputPointerCache()
			{
			}

			// Token: 0x04006904 RID: 26884
			public double LastTimeDown;

			// Token: 0x04006905 RID: 26885
			public bool WasDown;

			// Token: 0x04006906 RID: 26886
			public UIElement LastDown;

			// Token: 0x04006907 RID: 26887
			public UIElement LastClicked;

			// Token: 0x04006908 RID: 26888
			public UserInterface.MouseElementEvent MouseDownEvent;

			// Token: 0x04006909 RID: 26889
			public UserInterface.MouseElementEvent MouseUpEvent;

			// Token: 0x0400690A RID: 26890
			public UserInterface.MouseElementEvent ClickEvent;

			// Token: 0x0400690B RID: 26891
			public UserInterface.MouseElementEvent DoubleClickEvent;
		}

		// Token: 0x0200071B RID: 1819
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004047 RID: 16455 RVA: 0x0069D83E File Offset: 0x0069BA3E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004048 RID: 16456 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004049 RID: 16457 RVA: 0x0069D84A File Offset: 0x0069BA4A
			internal void <.ctor>b__22_0(UIElement element, UIMouseEvent evt)
			{
				element.LeftMouseDown(evt);
			}

			// Token: 0x0600404A RID: 16458 RVA: 0x0069D853 File Offset: 0x0069BA53
			internal void <.ctor>b__22_1(UIElement element, UIMouseEvent evt)
			{
				element.LeftMouseUp(evt);
			}

			// Token: 0x0600404B RID: 16459 RVA: 0x0069D85C File Offset: 0x0069BA5C
			internal void <.ctor>b__22_2(UIElement element, UIMouseEvent evt)
			{
				element.LeftClick(evt);
			}

			// Token: 0x0600404C RID: 16460 RVA: 0x0069D865 File Offset: 0x0069BA65
			internal void <.ctor>b__22_3(UIElement element, UIMouseEvent evt)
			{
				element.LeftDoubleClick(evt);
			}

			// Token: 0x0600404D RID: 16461 RVA: 0x0069D86E File Offset: 0x0069BA6E
			internal void <.ctor>b__22_4(UIElement element, UIMouseEvent evt)
			{
				element.RightMouseDown(evt);
			}

			// Token: 0x0600404E RID: 16462 RVA: 0x0069D877 File Offset: 0x0069BA77
			internal void <.ctor>b__22_5(UIElement element, UIMouseEvent evt)
			{
				element.RightMouseUp(evt);
			}

			// Token: 0x0600404F RID: 16463 RVA: 0x0069D880 File Offset: 0x0069BA80
			internal void <.ctor>b__22_6(UIElement element, UIMouseEvent evt)
			{
				element.RightClick(evt);
			}

			// Token: 0x06004050 RID: 16464 RVA: 0x0069D889 File Offset: 0x0069BA89
			internal void <.ctor>b__22_7(UIElement element, UIMouseEvent evt)
			{
				element.RightDoubleClick(evt);
			}

			// Token: 0x0400690C RID: 26892
			public static readonly UserInterface.<>c <>9 = new UserInterface.<>c();

			// Token: 0x0400690D RID: 26893
			public static UserInterface.MouseElementEvent <>9__22_0;

			// Token: 0x0400690E RID: 26894
			public static UserInterface.MouseElementEvent <>9__22_1;

			// Token: 0x0400690F RID: 26895
			public static UserInterface.MouseElementEvent <>9__22_2;

			// Token: 0x04006910 RID: 26896
			public static UserInterface.MouseElementEvent <>9__22_3;

			// Token: 0x04006911 RID: 26897
			public static UserInterface.MouseElementEvent <>9__22_4;

			// Token: 0x04006912 RID: 26898
			public static UserInterface.MouseElementEvent <>9__22_5;

			// Token: 0x04006913 RID: 26899
			public static UserInterface.MouseElementEvent <>9__22_6;

			// Token: 0x04006914 RID: 26900
			public static UserInterface.MouseElementEvent <>9__22_7;
		}
	}
}
