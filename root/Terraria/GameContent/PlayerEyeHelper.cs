using System;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent
{
	// Token: 0x02000263 RID: 611
	public struct PlayerEyeHelper
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x0053FF08 File Offset: 0x0053E108
		// (set) Token: 0x0600239A RID: 9114 RVA: 0x0053FF10 File Offset: 0x0053E110
		public int EyeFrameToShow
		{
			[CompilerGenerated]
			get
			{
				return this.<EyeFrameToShow>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EyeFrameToShow>k__BackingField = value;
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0053FF19 File Offset: 0x0053E119
		public void Update(Player player)
		{
			this.SetStateByPlayerInfo(player);
			this.UpdateEyeFrameToShow(player);
			this._timeInState++;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0053FF38 File Offset: 0x0053E138
		private void UpdateEyeFrameToShow(Player player)
		{
			PlayerEyeHelper.EyeFrame eyeFrame = PlayerEyeHelper.EyeFrame.EyeOpen;
			switch (this._state)
			{
			case PlayerEyeHelper.EyeState.NormalBlinking:
			{
				int num = this._timeInState % 240 - 234;
				if (num >= 4)
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				else if (num >= 2)
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeClosed;
				}
				else if (num >= 0)
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				else
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeOpen;
				}
				break;
			}
			case PlayerEyeHelper.EyeState.InStorm:
				if (this._timeInState % 120 - 114 >= 0)
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeClosed;
				}
				else
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				break;
			case PlayerEyeHelper.EyeState.InBed:
			{
				PlayerEyeHelper.EyeFrame eyeFrame2 = (this.DoesPlayerCountAsModeratelyDamaged(player) ? PlayerEyeHelper.EyeFrame.EyeHalfClosed : PlayerEyeHelper.EyeFrame.EyeOpen);
				this._timeInState = player.sleeping.timeSleeping;
				if (this._timeInState < 60)
				{
					eyeFrame = eyeFrame2;
				}
				else if (this._timeInState < 120)
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				else
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeClosed;
				}
				break;
			}
			case PlayerEyeHelper.EyeState.JustTookDamage:
				eyeFrame = PlayerEyeHelper.EyeFrame.EyeClosed;
				break;
			case PlayerEyeHelper.EyeState.IsModeratelyDamaged:
			case PlayerEyeHelper.EyeState.IsTipsy:
			case PlayerEyeHelper.EyeState.IsPoisoned:
				if (this._timeInState % 120 - 100 >= 0)
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeClosed;
				}
				else
				{
					eyeFrame = PlayerEyeHelper.EyeFrame.EyeHalfClosed;
				}
				break;
			case PlayerEyeHelper.EyeState.IsBlind:
				eyeFrame = PlayerEyeHelper.EyeFrame.EyeClosed;
				break;
			}
			this.EyeFrameToShow = (int)eyeFrame;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00540028 File Offset: 0x0053E228
		private void SetStateByPlayerInfo(Player player)
		{
			if (player.blackout || player.blind)
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.IsBlind, false);
				return;
			}
			if (this._state == PlayerEyeHelper.EyeState.JustTookDamage && this._timeInState < 20)
			{
				return;
			}
			if (player.sleeping.isSleeping)
			{
				bool flag = player.itemAnimation > 0;
				this.SwitchToState(PlayerEyeHelper.EyeState.InBed, flag);
				return;
			}
			if (this.DoesPlayerCountAsModeratelyDamaged(player))
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.IsModeratelyDamaged, false);
				return;
			}
			if (player.tipsy)
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.IsTipsy, false);
				return;
			}
			if (player.poisoned || player.venom || player.starving)
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.IsPoisoned, false);
				return;
			}
			bool flag2 = player.ZoneSandstorm || (player.ZoneSnow && Main.IsItRaining);
			if (player.behindBackWall)
			{
				flag2 = false;
			}
			if (flag2)
			{
				this.SwitchToState(PlayerEyeHelper.EyeState.InStorm, false);
				return;
			}
			this.SwitchToState(PlayerEyeHelper.EyeState.NormalBlinking, false);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00540100 File Offset: 0x0053E300
		private void SwitchToState(PlayerEyeHelper.EyeState newState, bool resetStateTimerEvenIfAlreadyInState = false)
		{
			if (this._state == newState && !resetStateTimerEvenIfAlreadyInState)
			{
				return;
			}
			this._state = newState;
			this._timeInState = 0;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0054011D File Offset: 0x0053E31D
		private bool DoesPlayerCountAsModeratelyDamaged(Player player)
		{
			return (float)player.statLife <= (float)player.statLifeMax2 * 0.25f;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00540138 File Offset: 0x0053E338
		public void BlinkBecausePlayerGotHurt()
		{
			this.SwitchToState(PlayerEyeHelper.EyeState.JustTookDamage, true);
		}

		// Token: 0x04004D99 RID: 19865
		private PlayerEyeHelper.EyeState _state;

		// Token: 0x04004D9A RID: 19866
		private int _timeInState;

		// Token: 0x04004D9B RID: 19867
		[CompilerGenerated]
		private int <EyeFrameToShow>k__BackingField;

		// Token: 0x04004D9C RID: 19868
		private const int TimeToActDamaged = 20;

		// Token: 0x020007E8 RID: 2024
		private enum EyeFrame
		{
			// Token: 0x0400713D RID: 28989
			EyeOpen,
			// Token: 0x0400713E RID: 28990
			EyeHalfClosed,
			// Token: 0x0400713F RID: 28991
			EyeClosed
		}

		// Token: 0x020007E9 RID: 2025
		private enum EyeState
		{
			// Token: 0x04007141 RID: 28993
			NormalBlinking,
			// Token: 0x04007142 RID: 28994
			InStorm,
			// Token: 0x04007143 RID: 28995
			InBed,
			// Token: 0x04007144 RID: 28996
			JustTookDamage,
			// Token: 0x04007145 RID: 28997
			IsModeratelyDamaged,
			// Token: 0x04007146 RID: 28998
			IsBlind,
			// Token: 0x04007147 RID: 28999
			IsTipsy,
			// Token: 0x04007148 RID: 29000
			IsPoisoned
		}
	}
}
