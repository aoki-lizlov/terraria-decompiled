using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Achievements;
using Terraria.GameInput;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.UI
{
	// Token: 0x020000F6 RID: 246
	public class InGameNotificationsTracker
	{
		// Token: 0x06001945 RID: 6469 RVA: 0x004E7ED0 File Offset: 0x004E60D0
		public static void Initialize()
		{
			Main.Achievements.OnAchievementCompleted += InGameNotificationsTracker.AddCompleted;
			SocialAPI.JoinRequests.OnRequestAdded += InGameNotificationsTracker.JoinRequests_OnRequestAdded;
			SocialAPI.JoinRequests.OnRequestRemoved += InGameNotificationsTracker.JoinRequests_OnRequestRemoved;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x004E7F1F File Offset: 0x004E611F
		private static void JoinRequests_OnRequestAdded(UserJoinToServerRequest request)
		{
			InGameNotificationsTracker.AddJoinRequest(request);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x004E7F28 File Offset: 0x004E6128
		private static void JoinRequests_OnRequestRemoved(UserJoinToServerRequest request)
		{
			for (int i = InGameNotificationsTracker._notifications.Count - 1; i >= 0; i--)
			{
				if (InGameNotificationsTracker._notifications[i].CreationObject == request)
				{
					InGameNotificationsTracker._notifications.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x004E7F6C File Offset: 0x004E616C
		public static void DrawInGame(SpriteBatch sb)
		{
			float num = (float)(Main.screenHeight - 40);
			if (PlayerInput.UsingGamepad)
			{
				num -= 25f;
			}
			Vector2 vector = new Vector2((float)(Main.screenWidth / 2), num);
			foreach (IInGameNotification inGameNotification in InGameNotificationsTracker._notifications)
			{
				inGameNotification.DrawInGame(sb, vector);
				inGameNotification.PushAnchor(ref vector);
				if (vector.Y < -100f)
				{
					break;
				}
			}
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x004E8000 File Offset: 0x004E6200
		public static void DrawInIngameOptions(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointIdLocalIndexToUse)
		{
			int num = 4;
			int num2 = area.Height / 5 - num;
			Rectangle rectangle = new Rectangle(area.X, area.Y, area.Width - 6, num2);
			int num3 = 0;
			foreach (IInGameNotification inGameNotification in InGameNotificationsTracker._notifications)
			{
				inGameNotification.DrawInNotificationsArea(spriteBatch, rectangle, ref gamepadPointIdLocalIndexToUse);
				rectangle.Y += num2 + num;
				num3++;
				if (num3 >= 5)
				{
					break;
				}
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x004E8098 File Offset: 0x004E6298
		public static void AddCompleted(Achievement achievement)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			InGameNotificationsTracker._notifications.Add(new InGamePopups.AchievementUnlockedPopup(achievement));
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x004E80B3 File Offset: 0x004E62B3
		public static void AddJoinRequest(UserJoinToServerRequest request)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			InGameNotificationsTracker._notifications.Add(new InGamePopups.PlayerWantsToJoinGamePopup(request));
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x004E80CE File Offset: 0x004E62CE
		public static void Clear()
		{
			InGameNotificationsTracker._notifications.Clear();
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x004E80DC File Offset: 0x004E62DC
		public static void Update()
		{
			for (int i = 0; i < InGameNotificationsTracker._notifications.Count; i++)
			{
				InGameNotificationsTracker._notifications[i].Update();
				if (InGameNotificationsTracker._notifications[i].ShouldBeRemoved)
				{
					InGameNotificationsTracker._notifications.Remove(InGameNotificationsTracker._notifications[i]);
					i--;
				}
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0000357B File Offset: 0x0000177B
		public InGameNotificationsTracker()
		{
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x004E813A File Offset: 0x004E633A
		// Note: this type is marked as 'beforefieldinit'.
		static InGameNotificationsTracker()
		{
		}

		// Token: 0x0400134A RID: 4938
		private static List<IInGameNotification> _notifications = new List<IInGameNotification>();
	}
}
