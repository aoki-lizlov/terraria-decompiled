using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000338 RID: 824
	public interface IEntryIcon
	{
		// Token: 0x0600283D RID: 10301
		void Update(BestiaryUICollectionInfo providedInfo, Rectangle hitbox, EntryIconDrawSettings settings);

		// Token: 0x0600283E RID: 10302
		void Draw(BestiaryUICollectionInfo providedInfo, SpriteBatch spriteBatch, EntryIconDrawSettings settings);

		// Token: 0x0600283F RID: 10303
		bool GetUnlockState(BestiaryUICollectionInfo providedInfo);

		// Token: 0x06002840 RID: 10304
		string GetHoverText(BestiaryUICollectionInfo providedInfo);

		// Token: 0x06002841 RID: 10305
		IEntryIcon CreateClone();
	}
}
