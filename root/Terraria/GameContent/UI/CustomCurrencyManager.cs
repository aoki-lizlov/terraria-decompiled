using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000375 RID: 885
	public class CustomCurrencyManager
	{
		// Token: 0x0600295C RID: 10588 RVA: 0x0057B299 File Offset: 0x00579499
		public static void Initialize()
		{
			CustomCurrencyManager._nextCurrencyIndex = 0;
			CustomCurrencyID.DefenderMedals = CustomCurrencyManager.RegisterCurrency(new CustomCurrencySingleCoin(3817, 999L));
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x0057B2BC File Offset: 0x005794BC
		public static int RegisterCurrency(CustomCurrencySystem collection)
		{
			int nextCurrencyIndex = CustomCurrencyManager._nextCurrencyIndex;
			CustomCurrencyManager._nextCurrencyIndex++;
			CustomCurrencyManager._currencies[nextCurrencyIndex] = collection;
			return nextCurrencyIndex;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x0057B2E8 File Offset: 0x005794E8
		public static void DrawSavings(SpriteBatch sb, int currencyIndex, float shopx, float shopy, bool horizontal = false)
		{
			CustomCurrencySystem customCurrencySystem = CustomCurrencyManager._currencies[currencyIndex];
			Player player = Main.player[Main.myPlayer];
			bool flag;
			long num = customCurrencySystem.CountCurrency(out flag, player.bank.item, new int[0]);
			long num2 = customCurrencySystem.CountCurrency(out flag, player.bank2.item, new int[0]);
			long num3 = customCurrencySystem.CountCurrency(out flag, player.bank3.item, new int[0]);
			long num4 = customCurrencySystem.CountCurrency(out flag, player.bank4.item, new int[0]);
			long num5 = customCurrencySystem.CombineStacks(out flag, new long[] { num, num2, num3, num4 });
			if (num5 > 0L)
			{
				Texture2D texture2D;
				Rectangle rectangle;
				Main.GetItemDrawFrame(4076, out texture2D, out rectangle);
				Texture2D texture2D2;
				Rectangle rectangle2;
				Main.GetItemDrawFrame(3813, out texture2D2, out rectangle2);
				Texture2D texture2D3;
				Rectangle rectangle3;
				Main.GetItemDrawFrame(346, out texture2D3, out rectangle3);
				Texture2D texture2D4;
				Rectangle rectangle4;
				Main.GetItemDrawFrame(87, out texture2D4, out rectangle4);
				if (num4 > 0L)
				{
					sb.Draw(texture2D, Utils.CenteredRectangle(new Vector2(shopx + 96f, shopy + 50f), rectangle.Size() * 0.65f), null, Color.White);
				}
				if (num3 > 0L)
				{
					sb.Draw(texture2D2, Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), rectangle2.Size() * 0.65f), null, Color.White);
				}
				if (num2 > 0L)
				{
					sb.Draw(texture2D3, Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), rectangle3.Size() * 0.65f), null, Color.White);
				}
				if (num > 0L)
				{
					sb.Draw(texture2D4, Utils.CenteredRectangle(new Vector2(shopx + 70f, shopy + 60f), rectangle4.Size() * 0.65f), null, Color.White);
				}
				Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Lang.inter[66].Value, shopx, shopy + 40f, Color.White * ((float)Main.mouseTextColor / 255f), Color.Black, Vector2.Zero, 1f);
				customCurrencySystem.DrawSavingsMoney(sb, Lang.inter[66].Value, shopx, shopy, num5, horizontal);
			}
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x0057B554 File Offset: 0x00579754
		public static void GetPriceText(int currencyIndex, string[] lines, ref int currentLine, long price)
		{
			CustomCurrencyManager._currencies[currencyIndex].GetPriceText(lines, ref currentLine, price);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x0057B56C File Offset: 0x0057976C
		public static bool BuyItem(Player player, long price, int currencyIndex)
		{
			CustomCurrencySystem customCurrencySystem = CustomCurrencyManager._currencies[currencyIndex];
			bool flag;
			long num = customCurrencySystem.CountCurrency(out flag, player.inventory, new int[] { 58, 57, 56, 55, 54 });
			long num2 = customCurrencySystem.CountCurrency(out flag, player.bank.item, new int[0]);
			long num3 = customCurrencySystem.CountCurrency(out flag, player.bank2.item, new int[0]);
			long num4 = customCurrencySystem.CountCurrency(out flag, player.bank3.item, new int[0]);
			long num5 = customCurrencySystem.CountCurrency(out flag, player.bank4.item, new int[0]);
			if (customCurrencySystem.CombineStacks(out flag, new long[] { num, num2, num3, num4, num5 }) < price)
			{
				return false;
			}
			List<Item[]> list = new List<Item[]>();
			Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
			List<Point> list2 = new List<Point>();
			List<Point> list3 = new List<Point>();
			List<Point> list4 = new List<Point>();
			List<Point> list5 = new List<Point>();
			List<Point> list6 = new List<Point>();
			List<Point> list7 = new List<Point>();
			list.Add(player.inventory);
			list.Add(player.bank.item);
			list.Add(player.bank2.item);
			list.Add(player.bank3.item);
			list.Add(player.bank4.item);
			for (int i = 0; i < list.Count; i++)
			{
				dictionary[i] = new List<int>();
			}
			dictionary[0] = new List<int> { 58, 57, 56, 55, 54 };
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = 0; k < list[j].Length; k++)
				{
					if (!dictionary[j].Contains(k) && customCurrencySystem.Accepts(list[j][k]))
					{
						list3.Add(new Point(j, k));
					}
				}
			}
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list2, 0);
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list4, 1);
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list5, 2);
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list6, 3);
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list7, 4);
			return customCurrencySystem.TryPurchasing(price, list, list3, list2, list4, list5, list6, list7);
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x0057B7DC File Offset: 0x005799DC
		private static void FindEmptySlots(List<Item[]> inventories, Dictionary<int, List<int>> slotsToIgnore, List<Point> emptySlots, int currentInventoryIndex)
		{
			for (int i = inventories[currentInventoryIndex].Length - 1; i >= 0; i--)
			{
				if (!slotsToIgnore[currentInventoryIndex].Contains(i) && (inventories[currentInventoryIndex][i].type == 0 || inventories[currentInventoryIndex][i].stack == 0))
				{
					emptySlots.Add(new Point(currentInventoryIndex, i));
				}
			}
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0057B83C File Offset: 0x00579A3C
		public static bool IsCustomCurrency(Item item)
		{
			foreach (KeyValuePair<int, CustomCurrencySystem> keyValuePair in CustomCurrencyManager._currencies)
			{
				if (keyValuePair.Value.Accepts(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x0057B8A0 File Offset: 0x00579AA0
		public static void GetPrices(Item item, out long calcForSelling, out long calcForBuying)
		{
			CustomCurrencyManager._currencies[item.shopSpecialCurrency].GetItemExpectedPrice(item, out calcForSelling, out calcForBuying);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x0000357B File Offset: 0x0000177B
		public CustomCurrencyManager()
		{
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x0057B8BA File Offset: 0x00579ABA
		// Note: this type is marked as 'beforefieldinit'.
		static CustomCurrencyManager()
		{
		}

		// Token: 0x040051EB RID: 20971
		private static int _nextCurrencyIndex;

		// Token: 0x040051EC RID: 20972
		private static Dictionary<int, CustomCurrencySystem> _currencies = new Dictionary<int, CustomCurrencySystem>();
	}
}
