using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200035E RID: 862
	public class NPCPortraitInfoElement : IBestiaryInfoElement
	{
		// Token: 0x060028C0 RID: 10432 RVA: 0x0057407D File Offset: 0x0057227D
		public NPCPortraitInfoElement(int? rarityStars = null)
		{
			this._filledStarsCount = rarityStars;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0057408C File Offset: 0x0057228C
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(112f, 0f)
			};
			uielement.SetPadding(0f);
			BestiaryEntry bestiaryEntry = new BestiaryEntry();
			Asset<Texture2D> asset = null;
			Color color = Color.White;
			bestiaryEntry.Icon = info.OwnerEntry.Icon.CreateClone();
			bestiaryEntry.UIInfoProvider = info.OwnerEntry.UIInfoProvider;
			List<IBestiaryBackgroundOverlayAndColorProvider> list = new List<IBestiaryBackgroundOverlayAndColorProvider>();
			bool flag = info.UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0;
			if (flag)
			{
				List<IBestiaryInfoElement> list2 = new List<IBestiaryInfoElement>();
				IEnumerable<IBestiaryBackgroundImagePathAndColorProvider> enumerable = info.OwnerEntry.Info.OfType<IBestiaryBackgroundImagePathAndColorProvider>();
				IEnumerable<IPreferenceProviderElement> preferences = info.OwnerEntry.Info.OfType<IPreferenceProviderElement>();
				IEnumerable<IBestiaryBackgroundImagePathAndColorProvider> enumerable2 = enumerable.Where((IBestiaryBackgroundImagePathAndColorProvider provider) => preferences.Any((IPreferenceProviderElement preference) => preference.Matches(provider)));
				bool flag2 = false;
				foreach (IBestiaryBackgroundImagePathAndColorProvider bestiaryBackgroundImagePathAndColorProvider in enumerable2)
				{
					Asset<Texture2D> backgroundImage = bestiaryBackgroundImagePathAndColorProvider.GetBackgroundImage();
					if (backgroundImage != null)
					{
						asset = backgroundImage;
						flag2 = true;
						Color? backgroundColor = bestiaryBackgroundImagePathAndColorProvider.GetBackgroundColor();
						if (backgroundColor != null)
						{
							color = backgroundColor.Value;
							break;
						}
						break;
					}
				}
				foreach (IBestiaryInfoElement bestiaryInfoElement in info.OwnerEntry.Info)
				{
					IBestiaryBackgroundImagePathAndColorProvider bestiaryBackgroundImagePathAndColorProvider2 = bestiaryInfoElement as IBestiaryBackgroundImagePathAndColorProvider;
					if (bestiaryBackgroundImagePathAndColorProvider2 != null)
					{
						Asset<Texture2D> backgroundImage2 = bestiaryBackgroundImagePathAndColorProvider2.GetBackgroundImage();
						if (backgroundImage2 == null)
						{
							continue;
						}
						if (!flag2)
						{
							asset = backgroundImage2;
						}
						Color? backgroundColor2 = bestiaryBackgroundImagePathAndColorProvider2.GetBackgroundColor();
						if (backgroundColor2 != null)
						{
							color = backgroundColor2.Value;
						}
					}
					if (!flag2)
					{
						IBestiaryBackgroundOverlayAndColorProvider bestiaryBackgroundOverlayAndColorProvider = bestiaryInfoElement as IBestiaryBackgroundOverlayAndColorProvider;
						if (bestiaryBackgroundOverlayAndColorProvider != null && bestiaryBackgroundOverlayAndColorProvider.GetBackgroundOverlayImage() != null)
						{
							list2.Add(bestiaryInfoElement);
						}
					}
				}
				list.AddRange(from x in list2.OrderBy(new Func<IBestiaryInfoElement, float>(this.GetSortingValueForElement))
					select x as IBestiaryBackgroundOverlayAndColorProvider);
			}
			UIBestiaryNPCEntryPortrait uibestiaryNPCEntryPortrait = new UIBestiaryNPCEntryPortrait(bestiaryEntry, asset, color, list)
			{
				Left = new StyleDimension(4f, 0f),
				HAlign = 0f
			};
			uielement.Append(uibestiaryNPCEntryPortrait);
			if (flag && this._filledStarsCount != null)
			{
				UIElement uielement2 = this.CreateStarsContainer();
				uielement.Append(uielement2);
			}
			return uielement;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x00574308 File Offset: 0x00572508
		private float GetSortingValueForElement(IBestiaryInfoElement element)
		{
			IBestiaryBackgroundOverlayAndColorProvider bestiaryBackgroundOverlayAndColorProvider = element as IBestiaryBackgroundOverlayAndColorProvider;
			if (bestiaryBackgroundOverlayAndColorProvider != null)
			{
				return bestiaryBackgroundOverlayAndColorProvider.DisplayPriority;
			}
			return 0f;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x0057432C File Offset: 0x0057252C
		private UIElement CreateStarsContainer()
		{
			int num = 14;
			int num2 = 14;
			int num3 = -4;
			int num4 = num + num3;
			int num5 = 5;
			int num6 = 5;
			int value = this._filledStarsCount.Value;
			float num7 = 1f;
			int num8 = num4 * Math.Min(num6, num5) - num3;
			double num9 = (double)num4 * Math.Ceiling((double)num5 / (double)num6) - (double)num3;
			UIElement uielement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", 1), null, 5, 21)
			{
				Width = new StyleDimension((float)num8 + num7 * 2f, 0f),
				Height = new StyleDimension((float)num9 + num7 * 2f, 0f),
				BackgroundColor = Color.Gray * 0f,
				BorderColor = Color.Transparent,
				Left = new StyleDimension(10f, 0f),
				Top = new StyleDimension(6f, 0f),
				VAlign = 0f
			};
			uielement.SetPadding(0f);
			for (int i = num5 - 1; i >= 0; i--)
			{
				string text = "Images/UI/Bestiary/Icon_Rank_Light";
				if (i >= value)
				{
					text = "Images/UI/Bestiary/Icon_Rank_Dim";
				}
				UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>(text, 1))
				{
					Left = new StyleDimension((float)(num4 * (i % num6)) - (float)num8 * 0.5f + (float)num * 0.5f, 0f),
					Top = new StyleDimension((float)(num4 * (i / num6)) - (float)num9 * 0.5f + (float)num2 * 0.5f, 0f),
					HAlign = 0.5f,
					VAlign = 0.5f
				};
				uielement.Append(uiimage);
			}
			return uielement;
		}

		// Token: 0x04005167 RID: 20839
		private int? _filledStarsCount;

		// Token: 0x020008C1 RID: 2241
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x06004624 RID: 17956 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06004625 RID: 17957 RVA: 0x006C6678 File Offset: 0x006C4878
			internal bool <ProvideUIElement>b__0(IBestiaryBackgroundImagePathAndColorProvider provider)
			{
				NPCPortraitInfoElement.<>c__DisplayClass2_1 CS$<>8__locals1 = new NPCPortraitInfoElement.<>c__DisplayClass2_1();
				CS$<>8__locals1.provider = provider;
				return this.preferences.Any((IPreferenceProviderElement preference) => preference.Matches(CS$<>8__locals1.provider));
			}

			// Token: 0x04007336 RID: 29494
			public IEnumerable<IPreferenceProviderElement> preferences;
		}

		// Token: 0x020008C2 RID: 2242
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_1
		{
			// Token: 0x06004626 RID: 17958 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass2_1()
			{
			}

			// Token: 0x06004627 RID: 17959 RVA: 0x006C66A9 File Offset: 0x006C48A9
			internal bool <ProvideUIElement>b__2(IPreferenceProviderElement preference)
			{
				return preference.Matches(this.provider);
			}

			// Token: 0x04007337 RID: 29495
			public IBestiaryBackgroundImagePathAndColorProvider provider;
		}

		// Token: 0x020008C3 RID: 2243
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004628 RID: 17960 RVA: 0x006C66B7 File Offset: 0x006C48B7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004629 RID: 17961 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600462A RID: 17962 RVA: 0x006C66C3 File Offset: 0x006C48C3
			internal IBestiaryBackgroundOverlayAndColorProvider <ProvideUIElement>b__2_1(IBestiaryInfoElement x)
			{
				return x as IBestiaryBackgroundOverlayAndColorProvider;
			}

			// Token: 0x04007338 RID: 29496
			public static readonly NPCPortraitInfoElement.<>c <>9 = new NPCPortraitInfoElement.<>c();

			// Token: 0x04007339 RID: 29497
			public static Func<IBestiaryInfoElement, IBestiaryBackgroundOverlayAndColorProvider> <>9__2_1;
		}
	}
}
