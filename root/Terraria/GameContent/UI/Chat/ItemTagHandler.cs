using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000385 RID: 901
	public class ItemTagHandler : ITagHandler
	{
		// Token: 0x060029C2 RID: 10690 RVA: 0x0057EAF8 File Offset: 0x0057CCF8
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			Item item = new Item();
			int num;
			if (int.TryParse(text, out num))
			{
				item.SetDefaults(num, null);
			}
			if (item.type <= 0)
			{
				return new TextSnippet(text);
			}
			item.stack = 1;
			if (options != null)
			{
				string[] array = options.Split(new char[] { ',' });
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Length != 0)
					{
						char c = array[i][0];
						int num3;
						if (c != 'p')
						{
							int num2;
							if ((c == 's' || c == 'x') && int.TryParse(array[i].Substring(1), out num2))
							{
								item.stack = Utils.Clamp<int>(num2, 1, item.maxStack);
							}
						}
						else if (int.TryParse(array[i].Substring(1), out num3))
						{
							item.Prefix((int)((byte)Utils.Clamp<int>(num3, 0, PrefixID.Count)));
						}
					}
				}
			}
			return new ItemTagHandler.ItemSnippet(item);
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x0057EBD8 File Offset: 0x0057CDD8
		public static string GenerateTag(Item I)
		{
			string text = "[i";
			bool flag = false;
			if (I.prefix != 0)
			{
				text = text + (flag ? ",p" : "/p") + I.prefix;
				flag = true;
			}
			if (I.stack != 1)
			{
				text = text + (flag ? ",s" : "/s") + I.stack;
			}
			return string.Concat(new object[] { text, ":", I.type, "]" });
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x0000357B File Offset: 0x0000177B
		public ItemTagHandler()
		{
		}

		// Token: 0x020008DE RID: 2270
		private class ItemSnippet : TextSnippet
		{
			// Token: 0x06004695 RID: 18069 RVA: 0x006C8708 File Offset: 0x006C6908
			public ItemSnippet(Item item)
				: base("")
			{
				this._item = item;
				this.Color = ItemRarity.GetColor(item.rare);
				string text = "";
				if (item.stack > 1)
				{
					text = " (" + item.stack + ")";
				}
				this.Text = "[" + item.AffixName() + text + "]";
				this.CheckForHover = true;
				this.DeleteWhole = true;
			}

			// Token: 0x06004696 RID: 18070 RVA: 0x006C878C File Offset: 0x006C698C
			public override void OnHover()
			{
				Main.HoverItem = this._item.Clone();
				Main.instance.MouseText(this._item.Name, this._item.rare, 0, -1, -1, -1, -1, 0);
			}

			// Token: 0x06004697 RID: 18071 RVA: 0x006C87D0 File Offset: 0x006C69D0
			public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
			{
				if (Main.netMode != 2 && !Main.dedServ)
				{
					Main.instance.LoadItem(this._item.type);
				}
				scale *= 0.75f;
				if (!justCheckingString && color != Color.Black)
				{
					float inventoryScale = Main.inventoryScale;
					Main.inventoryScale = scale;
					ItemSlot.Draw(spriteBatch, ref this._item, 14, position - new Vector2(10f) * Main.inventoryScale, Color.White);
					Main.inventoryScale = inventoryScale;
				}
				size = new Vector2(32f) * scale;
				return true;
			}

			// Token: 0x04007392 RID: 29586
			private Item _item;
		}
	}
}
