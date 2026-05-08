using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200031E RID: 798
	public class CreativePowersHelper
	{
		// Token: 0x0600279C RID: 10140 RVA: 0x00568A16 File Offset: 0x00566C16
		private static Asset<Texture2D> GetPowerIconAsset(string path)
		{
			return Main.Assets.Request<Texture2D>(path, 1);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00568A24 File Offset: 0x00566C24
		public static UIImageFramed GetIconImage(Point iconLocation)
		{
			Asset<Texture2D> powerIconAsset = CreativePowersHelper.GetPowerIconAsset("Images/UI/Creative/Infinite_Powers");
			return new UIImageFramed(powerIconAsset, powerIconAsset.Frame(21, 1, iconLocation.X, iconLocation.Y, 0, 0))
			{
				MarginLeft = 4f,
				MarginTop = 4f,
				VAlign = 0.5f,
				HAlign = 1f,
				IgnoresMouseInteraction = true
			};
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00568A8C File Offset: 0x00566C8C
		public static GroupOptionButton<bool> CreateToggleButton(CreativePowerUIElementRequestInfo info)
		{
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 0.8f, 0.5f, 10f);
			groupOptionButton.Width = new StyleDimension((float)info.PreferredButtonWidth, 0f);
			groupOptionButton.Height = new StyleDimension((float)info.PreferredButtonHeight, 0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(false);
			groupOptionButton.SetColorsBasedOnSelectionState(new Color(152, 175, 235), Colors.InventoryDefaultColor, 1f, 0.7f);
			groupOptionButton.SetColorsBasedOnSelectionState(Main.OurFavoriteColor, Colors.InventoryDefaultColor, 1f, 0.7f);
			return groupOptionButton;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00568B38 File Offset: 0x00566D38
		public static GroupOptionButton<bool> CreateSimpleButton(CreativePowerUIElementRequestInfo info)
		{
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, null, 0.8f, 0.5f, 10f);
			groupOptionButton.Width = new StyleDimension((float)info.PreferredButtonWidth, 0f);
			groupOptionButton.Height = new StyleDimension((float)info.PreferredButtonHeight, 0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(false);
			groupOptionButton.SetColorsBasedOnSelectionState(new Color(152, 175, 235), Colors.InventoryDefaultColor, 1f, 0.7f);
			return groupOptionButton;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00568BC8 File Offset: 0x00566DC8
		public static GroupOptionButton<T> CreateCategoryButton<T>(CreativePowerUIElementRequestInfo info, T option, T currentOption) where T : IConvertible, IEquatable<T>
		{
			GroupOptionButton<T> groupOptionButton = new GroupOptionButton<T>(option, null, null, Color.White, null, 0.8f, 0.5f, 10f);
			groupOptionButton.Width = new StyleDimension((float)info.PreferredButtonWidth, 0f);
			groupOptionButton.Height = new StyleDimension((float)info.PreferredButtonHeight, 0f);
			groupOptionButton.ShowHighlightWhenSelected = false;
			groupOptionButton.SetCurrentOption(currentOption);
			groupOptionButton.SetColorsBasedOnSelectionState(new Color(152, 175, 235), Colors.InventoryDefaultColor, 1f, 0.7f);
			return groupOptionButton;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00568C58 File Offset: 0x00566E58
		public static void AddPermissionTextIfNeeded(ICreativePower power, ref string originalText)
		{
			if (!CreativePowersHelper.IsAvailableForPlayer(power, Main.myPlayer))
			{
				string textValue = Language.GetTextValue("CreativePowers.CantUsePowerBecauseOfNoPermissionFromServer");
				originalText = originalText + "\n" + textValue;
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00568C8C File Offset: 0x00566E8C
		public static void AddDescriptionIfNeeded(ref string originalText, string descriptionKey)
		{
			if (!CreativePowerSettings.ShouldPowersBeElaborated)
			{
				return;
			}
			string textValue = Language.GetTextValue(descriptionKey);
			originalText = originalText + "\n" + textValue;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x00568CB8 File Offset: 0x00566EB8
		public static void AddUnlockTextIfNeeded(ref string originalText, bool needed, string descriptionKey)
		{
			if (needed)
			{
				return;
			}
			string textValue = Language.GetTextValue(descriptionKey);
			originalText = originalText + "\n" + textValue;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x00568CE0 File Offset: 0x00566EE0
		public static UIVerticalSlider CreateSlider(Func<float> GetSliderValueMethod, Action<float> SetValueKeyboardMethod, Action SetValueGamepadMethod)
		{
			return new UIVerticalSlider(GetSliderValueMethod, SetValueKeyboardMethod, SetValueGamepadMethod, Color.Red)
			{
				Width = new StyleDimension(12f, 0f),
				Height = new StyleDimension(-10f, 1f),
				Left = new StyleDimension(6f, 0f),
				HAlign = 0f,
				VAlign = 0.5f,
				EmptyColor = Color.OrangeRed,
				FilledColor = Color.CornflowerBlue
			};
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00568D65 File Offset: 0x00566F65
		public static void UpdateUseMouseInterface(UIElement affectedElement)
		{
			if (affectedElement.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00568D7C File Offset: 0x00566F7C
		public static void UpdateUnlockStateByPower(ICreativePower power, UIElement button, Color colorWhenSelected)
		{
			IGroupOptionButton asButton = button as IGroupOptionButton;
			if (asButton == null)
			{
				return;
			}
			button.OnUpdate += delegate(UIElement element)
			{
				CreativePowersHelper.UpdateUnlockStateByPowerInternal(power, colorWhenSelected, asButton);
			};
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x00568DC4 File Offset: 0x00566FC4
		public static bool IsAvailableForPlayer(ICreativePower power, int playerIndex)
		{
			switch (power.CurrentPermissionLevel)
			{
			default:
				return false;
			case PowerPermissionLevel.CanBeChangedByHostAlone:
				return Main.netMode == 0 || Main.countsAsHostForGameplay[playerIndex];
			case PowerPermissionLevel.CanBeChangedByEveryone:
				return true;
			}
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x00568E00 File Offset: 0x00567000
		private static void UpdateUnlockStateByPowerInternal(ICreativePower power, Color colorWhenSelected, IGroupOptionButton asButton)
		{
			bool isUnlocked = power.GetIsUnlocked();
			bool flag = !CreativePowersHelper.IsAvailableForPlayer(power, Main.myPlayer);
			asButton.SetBorderColor(flag ? Color.DimGray : Color.White);
			if (flag)
			{
				asButton.SetColorsBasedOnSelectionState(new Color(60, 60, 60), new Color(60, 60, 60), 0.7f, 0.7f);
				return;
			}
			if (isUnlocked)
			{
				asButton.SetColorsBasedOnSelectionState(colorWhenSelected, Colors.InventoryDefaultColor, 1f, 0.7f);
				return;
			}
			asButton.SetColorsBasedOnSelectionState(Color.Crimson, Color.Red, 0.7f, 0.7f);
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0000357B File Offset: 0x0000177B
		public CreativePowersHelper()
		{
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x00568E96 File Offset: 0x00567096
		// Note: this type is marked as 'beforefieldinit'.
		static CreativePowersHelper()
		{
		}

		// Token: 0x040050F4 RID: 20724
		public const int TextureIconColumns = 21;

		// Token: 0x040050F5 RID: 20725
		public const int TextureIconRows = 1;

		// Token: 0x040050F6 RID: 20726
		public static Color CommonSelectedColor = new Color(152, 175, 235);

		// Token: 0x0200087D RID: 2173
		public class CreativePowerIconLocations
		{
			// Token: 0x06004478 RID: 17528 RVA: 0x0000357B File Offset: 0x0000177B
			public CreativePowerIconLocations()
			{
			}

			// Token: 0x06004479 RID: 17529 RVA: 0x006C2930 File Offset: 0x006C0B30
			// Note: this type is marked as 'beforefieldinit'.
			static CreativePowerIconLocations()
			{
			}

			// Token: 0x04007290 RID: 29328
			public static readonly Point Unassigned = new Point(0, 0);

			// Token: 0x04007291 RID: 29329
			public static readonly Point Deprecated = new Point(0, 0);

			// Token: 0x04007292 RID: 29330
			public static readonly Point ItemDuplication = new Point(0, 0);

			// Token: 0x04007293 RID: 29331
			public static readonly Point ItemResearch = new Point(1, 0);

			// Token: 0x04007294 RID: 29332
			public static readonly Point TimeCategory = new Point(2, 0);

			// Token: 0x04007295 RID: 29333
			public static readonly Point WeatherCategory = new Point(3, 0);

			// Token: 0x04007296 RID: 29334
			public static readonly Point EnemyStrengthSlider = new Point(4, 0);

			// Token: 0x04007297 RID: 29335
			public static readonly Point GameEvents = new Point(5, 0);

			// Token: 0x04007298 RID: 29336
			public static readonly Point Godmode = new Point(6, 0);

			// Token: 0x04007299 RID: 29337
			public static readonly Point BlockPlacementRange = new Point(7, 0);

			// Token: 0x0400729A RID: 29338
			public static readonly Point StopBiomeSpread = new Point(8, 0);

			// Token: 0x0400729B RID: 29339
			public static readonly Point EnemySpawnRate = new Point(9, 0);

			// Token: 0x0400729C RID: 29340
			public static readonly Point FreezeTime = new Point(10, 0);

			// Token: 0x0400729D RID: 29341
			public static readonly Point TimeDawn = new Point(11, 0);

			// Token: 0x0400729E RID: 29342
			public static readonly Point TimeNoon = new Point(12, 0);

			// Token: 0x0400729F RID: 29343
			public static readonly Point TimeDusk = new Point(13, 0);

			// Token: 0x040072A0 RID: 29344
			public static readonly Point TimeMidnight = new Point(14, 0);

			// Token: 0x040072A1 RID: 29345
			public static readonly Point WindDirection = new Point(15, 0);

			// Token: 0x040072A2 RID: 29346
			public static readonly Point WindFreeze = new Point(16, 0);

			// Token: 0x040072A3 RID: 29347
			public static readonly Point RainStrength = new Point(17, 0);

			// Token: 0x040072A4 RID: 29348
			public static readonly Point RainFreeze = new Point(18, 0);

			// Token: 0x040072A5 RID: 29349
			public static readonly Point ModifyTime = new Point(19, 0);

			// Token: 0x040072A6 RID: 29350
			public static readonly Point PersonalCategory = new Point(20, 0);
		}

		// Token: 0x0200087E RID: 2174
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x0600447A RID: 17530 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x0600447B RID: 17531 RVA: 0x006C2A5D File Offset: 0x006C0C5D
			internal void <UpdateUnlockStateByPower>b__0(UIElement element)
			{
				CreativePowersHelper.UpdateUnlockStateByPowerInternal(this.power, this.colorWhenSelected, this.asButton);
			}

			// Token: 0x040072A7 RID: 29351
			public ICreativePower power;

			// Token: 0x040072A8 RID: 29352
			public Color colorWhenSelected;

			// Token: 0x040072A9 RID: 29353
			public IGroupOptionButton asButton;
		}
	}
}
