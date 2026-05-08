using System;
using Terraria.GameContent.UI.States;

namespace Terraria.Social.Base
{
	// Token: 0x02000150 RID: 336
	public class RichPresenceState : IEquatable<RichPresenceState>
	{
		// Token: 0x06001D23 RID: 7459 RVA: 0x00501310 File Offset: 0x004FF510
		public bool Equals(RichPresenceState other)
		{
			return this.GameMode == other.GameMode;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00501324 File Offset: 0x004FF524
		public static RichPresenceState GetCurrentState()
		{
			RichPresenceState richPresenceState = new RichPresenceState();
			if (Main.gameMenu)
			{
				bool flag = Main.MenuUI.CurrentState is UICharacterCreation;
				bool flag2 = Main.MenuUI.CurrentState is UIWorldCreation;
				if (flag)
				{
					richPresenceState.GameMode = RichPresenceState.GameModeState.CreatingPlayer;
				}
				else if (flag2)
				{
					richPresenceState.GameMode = RichPresenceState.GameModeState.CreatingWorld;
				}
				else
				{
					richPresenceState.GameMode = RichPresenceState.GameModeState.InMainMenu;
				}
			}
			else if (Main.netMode == 0)
			{
				richPresenceState.GameMode = RichPresenceState.GameModeState.PlayingSingle;
			}
			else
			{
				richPresenceState.GameMode = RichPresenceState.GameModeState.PlayingMulti;
			}
			return richPresenceState;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0000357B File Offset: 0x0000177B
		public RichPresenceState()
		{
		}

		// Token: 0x0400162C RID: 5676
		public RichPresenceState.GameModeState GameMode;

		// Token: 0x02000743 RID: 1859
		public enum GameModeState
		{
			// Token: 0x040069C3 RID: 27075
			InMainMenu,
			// Token: 0x040069C4 RID: 27076
			CreatingPlayer,
			// Token: 0x040069C5 RID: 27077
			CreatingWorld,
			// Token: 0x040069C6 RID: 27078
			PlayingSingle,
			// Token: 0x040069C7 RID: 27079
			PlayingMulti
		}
	}
}
