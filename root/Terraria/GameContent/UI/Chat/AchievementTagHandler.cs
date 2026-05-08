using System;
using Microsoft.Xna.Framework;
using Terraria.Achievements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000383 RID: 899
	public class AchievementTagHandler : ITagHandler
	{
		// Token: 0x060029BD RID: 10685 RVA: 0x0057EA68 File Offset: 0x0057CC68
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			Achievement achievement = Main.Achievements.GetAchievement(text);
			if (achievement == null)
			{
				return new TextSnippet(text);
			}
			return new AchievementTagHandler.AchievementSnippet(achievement);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0057EA91 File Offset: 0x0057CC91
		public static string GenerateTag(Achievement achievement)
		{
			return "[a:" + achievement.Name + "]";
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x0000357B File Offset: 0x0000177B
		public AchievementTagHandler()
		{
		}

		// Token: 0x020008DD RID: 2269
		private class AchievementSnippet : TextSnippet
		{
			// Token: 0x06004693 RID: 18067 RVA: 0x006C86CF File Offset: 0x006C68CF
			public AchievementSnippet(Achievement achievement)
				: base(achievement.FriendlyName.Value, Color.LightBlue)
			{
				this.CheckForHover = true;
				this._achievement = achievement;
			}

			// Token: 0x06004694 RID: 18068 RVA: 0x006C86F5 File Offset: 0x006C68F5
			public override void OnClick()
			{
				IngameOptions.Close();
				IngameFancyUI.OpenAchievementsAndGoto(this._achievement);
			}

			// Token: 0x04007391 RID: 29585
			private Achievement _achievement;
		}
	}
}
