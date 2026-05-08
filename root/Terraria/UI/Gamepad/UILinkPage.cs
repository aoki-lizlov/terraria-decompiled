using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.UI.Gamepad
{
	// Token: 0x02000106 RID: 262
	public class UILinkPage
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001A43 RID: 6723 RVA: 0x004F59B0 File Offset: 0x004F3BB0
		// (remove) Token: 0x06001A44 RID: 6724 RVA: 0x004F59E8 File Offset: 0x004F3BE8
		public event Action<int, int> ReachEndEvent
		{
			[CompilerGenerated]
			add
			{
				Action<int, int> action = this.ReachEndEvent;
				Action<int, int> action2;
				do
				{
					action2 = action;
					Action<int, int> action3 = (Action<int, int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<int, int>>(ref this.ReachEndEvent, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<int, int> action = this.ReachEndEvent;
				Action<int, int> action2;
				do
				{
					action2 = action;
					Action<int, int> action3 = (Action<int, int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<int, int>>(ref this.ReachEndEvent, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001A45 RID: 6725 RVA: 0x004F5A20 File Offset: 0x004F3C20
		// (remove) Token: 0x06001A46 RID: 6726 RVA: 0x004F5A58 File Offset: 0x004F3C58
		public event Action TravelEvent
		{
			[CompilerGenerated]
			add
			{
				Action action = this.TravelEvent;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.TravelEvent, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.TravelEvent;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.TravelEvent, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001A47 RID: 6727 RVA: 0x004F5A90 File Offset: 0x004F3C90
		// (remove) Token: 0x06001A48 RID: 6728 RVA: 0x004F5AC8 File Offset: 0x004F3CC8
		public event Action LeaveEvent
		{
			[CompilerGenerated]
			add
			{
				Action action = this.LeaveEvent;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.LeaveEvent, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.LeaveEvent;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.LeaveEvent, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001A49 RID: 6729 RVA: 0x004F5B00 File Offset: 0x004F3D00
		// (remove) Token: 0x06001A4A RID: 6730 RVA: 0x004F5B38 File Offset: 0x004F3D38
		public event Action EnterEvent
		{
			[CompilerGenerated]
			add
			{
				Action action = this.EnterEvent;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.EnterEvent, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.EnterEvent;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.EnterEvent, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001A4B RID: 6731 RVA: 0x004F5B70 File Offset: 0x004F3D70
		// (remove) Token: 0x06001A4C RID: 6732 RVA: 0x004F5BA8 File Offset: 0x004F3DA8
		public event Action UpdateEvent
		{
			[CompilerGenerated]
			add
			{
				Action action = this.UpdateEvent;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.UpdateEvent, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.UpdateEvent;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.UpdateEvent, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001A4D RID: 6733 RVA: 0x004F5BE0 File Offset: 0x004F3DE0
		// (remove) Token: 0x06001A4E RID: 6734 RVA: 0x004F5C18 File Offset: 0x004F3E18
		public event Func<bool> IsValidEvent
		{
			[CompilerGenerated]
			add
			{
				Func<bool> func = this.IsValidEvent;
				Func<bool> func2;
				do
				{
					func2 = func;
					Func<bool> func3 = (Func<bool>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<bool>>(ref this.IsValidEvent, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<bool> func = this.IsValidEvent;
				Func<bool> func2;
				do
				{
					func2 = func;
					Func<bool> func3 = (Func<bool>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<bool>>(ref this.IsValidEvent, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001A4F RID: 6735 RVA: 0x004F5C50 File Offset: 0x004F3E50
		// (remove) Token: 0x06001A50 RID: 6736 RVA: 0x004F5C88 File Offset: 0x004F3E88
		public event Func<bool> CanEnterEvent
		{
			[CompilerGenerated]
			add
			{
				Func<bool> func = this.CanEnterEvent;
				Func<bool> func2;
				do
				{
					func2 = func;
					Func<bool> func3 = (Func<bool>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<bool>>(ref this.CanEnterEvent, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<bool> func = this.CanEnterEvent;
				Func<bool> func2;
				do
				{
					func2 = func;
					Func<bool> func3 = (Func<bool>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<bool>>(ref this.CanEnterEvent, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06001A51 RID: 6737 RVA: 0x004F5CC0 File Offset: 0x004F3EC0
		// (remove) Token: 0x06001A52 RID: 6738 RVA: 0x004F5CF8 File Offset: 0x004F3EF8
		public event Action<int> OnPageMoveAttempt
		{
			[CompilerGenerated]
			add
			{
				Action<int> action = this.OnPageMoveAttempt;
				Action<int> action2;
				do
				{
					action2 = action;
					Action<int> action3 = (Action<int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<int>>(ref this.OnPageMoveAttempt, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<int> action = this.OnPageMoveAttempt;
				Action<int> action2;
				do
				{
					action2 = action;
					Action<int> action3 = (Action<int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<int>>(ref this.OnPageMoveAttempt, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x004F5D2D File Offset: 0x004F3F2D
		public UILinkPage()
		{
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x004F5D4E File Offset: 0x004F3F4E
		public UILinkPage(int id)
		{
			this.ID = id;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x004F5D76 File Offset: 0x004F3F76
		public void Update()
		{
			if (this.UpdateEvent != null)
			{
				this.UpdateEvent();
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x004F5D8B File Offset: 0x004F3F8B
		public void Leave()
		{
			if (this.LeaveEvent != null)
			{
				this.LeaveEvent();
			}
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x004F5DA0 File Offset: 0x004F3FA0
		public void Enter()
		{
			if (this.EnterEvent != null)
			{
				this.EnterEvent();
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x004F5DB5 File Offset: 0x004F3FB5
		public bool IsValid()
		{
			return this.IsValidEvent == null || this.IsValidEvent();
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x004F5DCC File Offset: 0x004F3FCC
		public bool CanEnter()
		{
			return this.CanEnterEvent == null || this.CanEnterEvent();
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x004F5DE3 File Offset: 0x004F3FE3
		public void TravelUp()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Up);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x004F5E01 File Offset: 0x004F4001
		public void TravelDown()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Down);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x004F5E1F File Offset: 0x004F401F
		public void TravelLeft()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Left);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x004F5E3D File Offset: 0x004F403D
		public void TravelRight()
		{
			this.Travel(this.LinkMap[this.CurrentPoint].Right);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x004F5E5B File Offset: 0x004F405B
		public void SwapPageLeft()
		{
			if (this.OnPageMoveAttempt != null)
			{
				this.OnPageMoveAttempt(-1);
			}
			UILinkPointNavigator.ChangePage(this.PageOnLeft);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x004F5E7C File Offset: 0x004F407C
		public void SwapPageRight()
		{
			if (this.OnPageMoveAttempt != null)
			{
				this.OnPageMoveAttempt(1);
			}
			UILinkPointNavigator.ChangePage(this.PageOnRight);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x004F5EA0 File Offset: 0x004F40A0
		private void Travel(int next)
		{
			if (next < 0)
			{
				if (this.ReachEndEvent != null)
				{
					this.ReachEndEvent(this.CurrentPoint, next);
					if (this.TravelEvent != null)
					{
						this.TravelEvent();
						return;
					}
				}
			}
			else
			{
				UILinkPointNavigator.ChangePoint(next);
				if (this.TravelEvent != null)
				{
					this.TravelEvent();
				}
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06001A61 RID: 6753 RVA: 0x004F5EF8 File Offset: 0x004F40F8
		// (remove) Token: 0x06001A62 RID: 6754 RVA: 0x004F5F30 File Offset: 0x004F4130
		public event Func<string> OnSpecialInteracts
		{
			[CompilerGenerated]
			add
			{
				Func<string> func = this.OnSpecialInteracts;
				Func<string> func2;
				do
				{
					func2 = func;
					Func<string> func3 = (Func<string>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<string>>(ref this.OnSpecialInteracts, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<string> func = this.OnSpecialInteracts;
				Func<string> func2;
				do
				{
					func2 = func;
					Func<string> func3 = (Func<string>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<string>>(ref this.OnSpecialInteracts, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x004F5F65 File Offset: 0x004F4165
		public string SpecialInteractions()
		{
			if (this.OnSpecialInteracts != null)
			{
				return this.OnSpecialInteracts();
			}
			return string.Empty;
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06001A64 RID: 6756 RVA: 0x004F5F80 File Offset: 0x004F4180
		// (remove) Token: 0x06001A65 RID: 6757 RVA: 0x004F5FB8 File Offset: 0x004F41B8
		public event Func<string> OnSpecialInteractsLate
		{
			[CompilerGenerated]
			add
			{
				Func<string> func = this.OnSpecialInteractsLate;
				Func<string> func2;
				do
				{
					func2 = func;
					Func<string> func3 = (Func<string>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<string>>(ref this.OnSpecialInteractsLate, func3, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<string> func = this.OnSpecialInteractsLate;
				Func<string> func2;
				do
				{
					func2 = func;
					Func<string> func3 = (Func<string>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<string>>(ref this.OnSpecialInteractsLate, func3, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x004F5FED File Offset: 0x004F41ED
		public string SpecialInteractionsLate()
		{
			if (this.OnSpecialInteractsLate != null)
			{
				return this.OnSpecialInteractsLate();
			}
			return string.Empty;
		}

		// Token: 0x040014CE RID: 5326
		public int ID;

		// Token: 0x040014CF RID: 5327
		public int PageOnLeft = -1;

		// Token: 0x040014D0 RID: 5328
		public int PageOnRight = -1;

		// Token: 0x040014D1 RID: 5329
		public int DefaultPoint;

		// Token: 0x040014D2 RID: 5330
		public int CurrentPoint;

		// Token: 0x040014D3 RID: 5331
		public Dictionary<int, UILinkPoint> LinkMap = new Dictionary<int, UILinkPoint>();

		// Token: 0x040014D4 RID: 5332
		[CompilerGenerated]
		private Action<int, int> ReachEndEvent;

		// Token: 0x040014D5 RID: 5333
		[CompilerGenerated]
		private Action TravelEvent;

		// Token: 0x040014D6 RID: 5334
		[CompilerGenerated]
		private Action LeaveEvent;

		// Token: 0x040014D7 RID: 5335
		[CompilerGenerated]
		private Action EnterEvent;

		// Token: 0x040014D8 RID: 5336
		[CompilerGenerated]
		private Action UpdateEvent;

		// Token: 0x040014D9 RID: 5337
		[CompilerGenerated]
		private Func<bool> IsValidEvent;

		// Token: 0x040014DA RID: 5338
		[CompilerGenerated]
		private Func<bool> CanEnterEvent;

		// Token: 0x040014DB RID: 5339
		[CompilerGenerated]
		private Action<int> OnPageMoveAttempt;

		// Token: 0x040014DC RID: 5340
		[CompilerGenerated]
		private Func<string> OnSpecialInteracts;

		// Token: 0x040014DD RID: 5341
		[CompilerGenerated]
		private Func<string> OnSpecialInteractsLate;
	}
}
