using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.Initializers
{
	// Token: 0x02000087 RID: 135
	public static class PlayerDataInitializer
	{
		// Token: 0x0600159C RID: 5532 RVA: 0x004CC270 File Offset: 0x004CA470
		public static void Load()
		{
			TextureAssets.Players = new Asset<Texture2D>[PlayerVariantID.Count, PlayerTextureID.Count];
			PlayerDataInitializer.LoadStarterMale();
			PlayerDataInitializer.LoadStarterFemale();
			PlayerDataInitializer.LoadStickerMale();
			PlayerDataInitializer.LoadStickerFemale();
			PlayerDataInitializer.LoadGangsterMale();
			PlayerDataInitializer.LoadGangsterFemale();
			PlayerDataInitializer.LoadCoatMale();
			PlayerDataInitializer.LoadDressFemale();
			PlayerDataInitializer.LoadDressMale();
			PlayerDataInitializer.LoadCoatFemale();
			PlayerDataInitializer.LoadDisplayDollMale();
			PlayerDataInitializer.LoadDisplayDollFemale();
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x004CC2D0 File Offset: 0x004CA4D0
		private static void LoadVariant(int ID, int[] pieceIDs)
		{
			for (int i = 0; i < pieceIDs.Length; i++)
			{
				TextureAssets.Players[ID, pieceIDs[i]] = Main.Assets.Request<Texture2D>(string.Concat(new object[]
				{
					"Images/Player_",
					ID,
					"_",
					pieceIDs[i]
				}), 2);
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x004CC334 File Offset: 0x004CA534
		private static void CopyVariant(int to, int from)
		{
			for (int i = 0; i < PlayerTextureID.Count; i++)
			{
				TextureAssets.Players[to, i] = TextureAssets.Players[from, i];
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x004CC369 File Offset: 0x004CA569
		private static void LoadStarterMale()
		{
			PlayerDataInitializer.LoadVariant(0, new int[]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12, 13, 15
			});
			TextureAssets.Players[0, 14] = Asset<Texture2D>.Empty;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x004CC395 File Offset: 0x004CA595
		private static void LoadStickerMale()
		{
			PlayerDataInitializer.CopyVariant(1, 0);
			PlayerDataInitializer.LoadVariant(1, new int[] { 4, 6, 8, 11, 12, 13 });
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x004CC3B5 File Offset: 0x004CA5B5
		private static void LoadGangsterMale()
		{
			PlayerDataInitializer.CopyVariant(2, 0);
			PlayerDataInitializer.LoadVariant(2, new int[] { 4, 6, 8, 11, 12, 13 });
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x004CC3D5 File Offset: 0x004CA5D5
		private static void LoadCoatMale()
		{
			PlayerDataInitializer.CopyVariant(3, 0);
			PlayerDataInitializer.LoadVariant(3, new int[] { 4, 6, 8, 11, 12, 13, 14 });
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x004CC3F5 File Offset: 0x004CA5F5
		private static void LoadDressMale()
		{
			PlayerDataInitializer.CopyVariant(8, 0);
			PlayerDataInitializer.LoadVariant(8, new int[] { 4, 6, 8, 11, 12, 13, 14 });
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x004CC415 File Offset: 0x004CA615
		private static void LoadStarterFemale()
		{
			PlayerDataInitializer.CopyVariant(4, 0);
			PlayerDataInitializer.LoadVariant(4, new int[]
			{
				3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
				13
			});
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x004CC436 File Offset: 0x004CA636
		private static void LoadStickerFemale()
		{
			PlayerDataInitializer.CopyVariant(5, 4);
			PlayerDataInitializer.LoadVariant(5, new int[] { 4, 6, 8, 11, 12, 13 });
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x004CC456 File Offset: 0x004CA656
		private static void LoadGangsterFemale()
		{
			PlayerDataInitializer.CopyVariant(6, 4);
			PlayerDataInitializer.LoadVariant(6, new int[] { 4, 6, 8, 11, 12, 13 });
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x004CC476 File Offset: 0x004CA676
		private static void LoadCoatFemale()
		{
			PlayerDataInitializer.CopyVariant(7, 4);
			PlayerDataInitializer.LoadVariant(7, new int[] { 4, 6, 8, 11, 12, 13, 14 });
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x004CC496 File Offset: 0x004CA696
		private static void LoadDressFemale()
		{
			PlayerDataInitializer.CopyVariant(9, 4);
			PlayerDataInitializer.LoadVariant(9, new int[] { 4, 6, 8, 11, 12, 13 });
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x004CC4B8 File Offset: 0x004CA6B8
		private static void LoadDisplayDollMale()
		{
			PlayerDataInitializer.CopyVariant(10, 0);
			PlayerDataInitializer.LoadVariant(10, new int[] { 0, 2, 3, 5, 7, 9, 10 });
			Asset<Texture2D> asset = TextureAssets.Players[10, 2];
			TextureAssets.Players[10, 2] = asset;
			TextureAssets.Players[10, 1] = asset;
			TextureAssets.Players[10, 4] = asset;
			TextureAssets.Players[10, 6] = asset;
			TextureAssets.Players[10, 11] = asset;
			TextureAssets.Players[10, 12] = asset;
			TextureAssets.Players[10, 13] = asset;
			TextureAssets.Players[10, 8] = asset;
			TextureAssets.Players[10, 15] = asset;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x004CC578 File Offset: 0x004CA778
		private static void LoadDisplayDollFemale()
		{
			PlayerDataInitializer.CopyVariant(11, 10);
			PlayerDataInitializer.LoadVariant(11, new int[] { 3, 5, 7, 9, 10 });
			Asset<Texture2D> asset = TextureAssets.Players[10, 2];
			TextureAssets.Players[11, 2] = asset;
			TextureAssets.Players[11, 1] = asset;
			TextureAssets.Players[11, 4] = asset;
			TextureAssets.Players[11, 6] = asset;
			TextureAssets.Players[11, 11] = asset;
			TextureAssets.Players[11, 12] = asset;
			TextureAssets.Players[11, 13] = asset;
			TextureAssets.Players[11, 8] = asset;
			TextureAssets.Players[11, 15] = asset;
		}
	}
}
