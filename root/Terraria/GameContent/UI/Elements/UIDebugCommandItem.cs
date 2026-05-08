using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.Testing.ChatCommands;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F2 RID: 1010
	public class UIDebugCommandItem : UIPanel
	{
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x005AC439 File Offset: 0x005AA639
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x005AC441 File Offset: 0x005AA641
		public int Order
		{
			[CompilerGenerated]
			get
			{
				return this.<Order>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Order>k__BackingField = value;
			}
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x005AC44C File Offset: 0x005AA64C
		public UIDebugCommandItem(IDebugCommand command, int order)
		{
			this.Command = command;
			this.Order = order;
			this.Height.Set(30f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.BorderColor = Color.Transparent;
			this.BackgroundColor = Color.Transparent;
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._hoverInfoLabel = new UIText("", 1f, false);
			this._hoverInfoLabel.VAlign = 1f;
			this._hoverInfoLabel.Left.Set(80f, 0f);
			this._hoverInfoLabel.Top.Set(-3f, 0f);
			base.Append(this._hoverInfoLabel);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x005AC54C File Offset: 0x005AA74C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			string name = this.Command.Name;
			string text = this.Command.Description ?? "";
			string helpText = this.Command.HelpText;
			"Authority:  " + this.Command.Requirements;
			base.DrawSelf(spriteBatch);
			if (base.IsMouseHovering)
			{
				Item item = Main.DisplayAndGetFakeItem(ItemRarityColor.StrongRed10);
				item.SetNameOverride(name);
				item.ToolTip = this._preparedTooltip;
			}
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 vector = innerDimensions.Position() - innerDimensions.Position();
			float num = 6f;
			float num2 = vector.X + num;
			float num3 = 21f;
			FontAssets.MouseText.Value.MeasureString(name);
			Color color = Color.White;
			Color color2 = Color.Gold;
			if (!this.CanCurrentlyBeUsed())
			{
				color = Color.DarkGray;
				color2 = Color.DarkGray;
			}
			Utils.DrawBorderString(spriteBatch, name, innerDimensions.Position() + new Vector2(num2 + 6f, vector.Y - 2f), color, 1.1f, 0f, 0f, -1);
			Utils.DrawBorderString(spriteBatch, text, innerDimensions.Position() + new Vector2(num2 + 6f + 180f + 16f, vector.Y + 2f + num3), color2, 0.8f, 0f, 1f, -1);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x005AC6BF File Offset: 0x005AA8BF
		private bool CanCurrentlyBeUsed()
		{
			return (this.Command.Requirements & ~CommandRequirement.SinglePlayer) != (CommandRequirement)0 || Main.netMode == 0;
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x005AC6E0 File Offset: 0x005AA8E0
		public override int CompareTo(object obj)
		{
			return this.Order.CompareTo(((UIDebugCommandItem)obj).Order);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x005AC708 File Offset: 0x005AA908
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = new Color(76, 90, 149);
			this.BorderColor = new Color(50, 60, 86);
			ItemTooltip preparedTooltip = this._preparedTooltip;
			string text = FontAssets.ItemStack.Value.CreateWrappedText((this.Command.Description ?? "").Replace("\n", " "), 480f, Language.ActiveCulture.CultureInfo);
			List<string> list = new List<string> { text };
			list.Add(" ");
			list.Add("Authority:  " + this.Command.Requirements);
			this._preparedTooltip = ItemTooltip.FromHardcodedText(list.ToArray());
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x005AC7D4 File Offset: 0x005AA9D4
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = (this.BorderColor = Color.Transparent);
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x005AC7FC File Offset: 0x005AA9FC
		public override void LeftClick(UIMouseEvent evt)
		{
			IngameFancyUI.Close(false);
			Main.drawingPlayerChat = true;
			Main.chatText = "/" + this.Command.Name.ToLower() + " ";
			Main.NewText("Chat has been set to \"" + Main.chatText + "\"", byte.MaxValue, byte.MaxValue, 0);
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x005AC860 File Offset: 0x005AAA60
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x040055CB RID: 21963
		public readonly IDebugCommand Command;

		// Token: 0x040055CC RID: 21964
		[CompilerGenerated]
		private int <Order>k__BackingField;

		// Token: 0x040055CD RID: 21965
		private readonly Asset<Texture2D> _dividerTexture;

		// Token: 0x040055CE RID: 21966
		private readonly Asset<Texture2D> _innerPanelTexture;

		// Token: 0x040055CF RID: 21967
		private readonly UIText _hoverInfoLabel;

		// Token: 0x040055D0 RID: 21968
		private ItemTooltip _preparedTooltip;
	}
}
