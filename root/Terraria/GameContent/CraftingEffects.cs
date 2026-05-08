using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.Graphics.Renderers;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x0200022C RID: 556
	public class CraftingEffects
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x00532B94 File Offset: 0x00530D94
		public static void OnCraft(Recipe recipe, bool quickCraft)
		{
			CraftingEffects._justCraftedItemType = recipe.createItem.type;
			Item createItem = recipe.createItem;
			CraftingEffects.SpawnEffects_BeforeGrantingItem(recipe, createItem);
			if (!quickCraft)
			{
				CraftingEffects._mouseItemGlow = 1f;
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00532BCC File Offset: 0x00530DCC
		public static void OnCraftItemGranted(Recipe recipe, Item result, bool quickCraft)
		{
			PopupText.NewText(PopupTextContext.ItemCraft, result, Main.LocalPlayer.Center, recipe.createItem.stack, false, false);
			CraftingEffects.SpawnEffects_AfterGrantingItem(recipe, result, quickCraft);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00532BF5 File Offset: 0x00530DF5
		public static void Update()
		{
			if (CraftingEffects._mouseItemGlow > 0f)
			{
				CraftingEffects._mouseItemGlow -= 0.035f;
			}
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x00532C13 File Offset: 0x00530E13
		public static float GetGlow(Item cursorItem)
		{
			if (CraftingEffects._mouseItemGlow <= 0f || CraftingEffects._justCraftedItemType != cursorItem.type)
			{
				return 0f;
			}
			return CraftingEffects._mouseItemGlow;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00532C39 File Offset: 0x00530E39
		private static void SpawnEffects_BeforeGrantingItem(Recipe recipe, Item result)
		{
			SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00009E46 File Offset: 0x00008046
		public static void SpawnEffects_AfterGrantingItem(Recipe recipe, Item result, bool quickCraft)
		{
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00532C4F File Offset: 0x00530E4F
		private static bool RecipeUsesCraftingStation(Recipe recipe, int tileId)
		{
			return recipe.requiredTile == tileId;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00532C5C File Offset: 0x00530E5C
		public static CraftingEffectDetails GetEffectDetails(Item newItem)
		{
			int rare = newItem.rare;
			CraftingEffectDetails craftingEffectDetails = default(CraftingEffectDetails);
			craftingEffectDetails.Rarity = rare;
			if ((newItem.healLife > 0 || newItem.healMana > 0 || newItem.buffType > 0 || ItemID.Sets.IsFood[newItem.type] || ItemID.Sets.SortingPriorityPotionsBuffs[newItem.type] != -1) & newItem.consumable)
			{
				craftingEffectDetails.Style = PopupEffectStyle.Potion;
				craftingEffectDetails.Intensity = rare;
			}
			bool flag = newItem.GetRollablePrefixes() != null || newItem.accessory || newItem.bodySlot != -1 || newItem.headSlot != -1 || newItem.legSlot != -1 || (newItem.shoot != 0 && Main.projHook[newItem.shoot]) || newItem.mountType != -1;
			if (flag)
			{
				craftingEffectDetails.Style = PopupEffectStyle.Metal;
				craftingEffectDetails.Intensity = rare;
			}
			if (flag && newItem.magic)
			{
				craftingEffectDetails.Style = PopupEffectStyle.MagicWeapon;
				craftingEffectDetails.Intensity = rare;
			}
			if (flag && newItem.melee)
			{
				craftingEffectDetails.Style = PopupEffectStyle.MeleeWeapon;
				craftingEffectDetails.Intensity = rare;
			}
			if (flag && newItem.ranged)
			{
				craftingEffectDetails.Style = PopupEffectStyle.RangedWeapon;
				craftingEffectDetails.Intensity = rare;
			}
			return craftingEffectDetails;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00532D8C File Offset: 0x00530F8C
		private static void CreateBubbleParticles(int n)
		{
			for (float num = 0f; num < 2f; num += 0.083333336f)
			{
				float num2 = 15f;
				float num3 = 6.2831855f * (num + Main.rand.NextFloat());
				FadingParticle fadingParticle = ParticleOrchestrator._poolFading.RequestParticle();
				fadingParticle.SetBasicInfo(TextureAssets.Bubble, null, num3.ToRotationVector2() * (2f + 3f * Main.rand.NextFloat()), Main.MouseScreen + num3.ToRotationVector2() * (10f + 40f * Main.rand.NextFloat()));
				fadingParticle.SetTypeInfo(num2, true);
				fadingParticle.AccelerationPerFrame = fadingParticle.Velocity * (-1f / num2);
				fadingParticle.LocalPosition -= fadingParticle.Velocity * 4f;
				fadingParticle.FadeInNormalizedTime = 0.2f;
				fadingParticle.FadeOutNormalizedTime = 0.7f;
				fadingParticle.Scale = Vector2.One;
				Main.ParticleSystem_OverInventory.Add(fadingParticle);
			}
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0000357B File Offset: 0x0000177B
		public CraftingEffects()
		{
		}

		// Token: 0x04004CB1 RID: 19633
		private static int _justCraftedItemType;

		// Token: 0x04004CB2 RID: 19634
		private static float _mouseItemGlow;
	}
}
