using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000405 RID: 1029
	public class UIText : UIElement
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x005B1D7A File Offset: 0x005AFF7A
		public string Text
		{
			get
			{
				return this._text.ToString();
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x005B1D87 File Offset: 0x005AFF87
		// (set) Token: 0x06002F3B RID: 12091 RVA: 0x005B1D8F File Offset: 0x005AFF8F
		public float TextOriginX
		{
			[CompilerGenerated]
			get
			{
				return this.<TextOriginX>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TextOriginX>k__BackingField = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x005B1D98 File Offset: 0x005AFF98
		// (set) Token: 0x06002F3D RID: 12093 RVA: 0x005B1DA0 File Offset: 0x005AFFA0
		public float TextOriginY
		{
			[CompilerGenerated]
			get
			{
				return this.<TextOriginY>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TextOriginY>k__BackingField = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x005B1DA9 File Offset: 0x005AFFA9
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x005B1DB1 File Offset: 0x005AFFB1
		public float WrappedTextBottomPadding
		{
			[CompilerGenerated]
			get
			{
				return this.<WrappedTextBottomPadding>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WrappedTextBottomPadding>k__BackingField = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x005B1DBA File Offset: 0x005AFFBA
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x005B1DC2 File Offset: 0x005AFFC2
		public bool IsWrapped
		{
			get
			{
				return this._isWrapped;
			}
			set
			{
				this._isWrapped = value;
				this.InternalSetText(this._text, this._textScale, this._isLarge);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06002F42 RID: 12098 RVA: 0x005B1DE4 File Offset: 0x005AFFE4
		// (remove) Token: 0x06002F43 RID: 12099 RVA: 0x005B1E1C File Offset: 0x005B001C
		public event Action OnInternalTextChange
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnInternalTextChange;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnInternalTextChange, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnInternalTextChange;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnInternalTextChange, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x005B1E51 File Offset: 0x005B0051
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x005B1E59 File Offset: 0x005B0059
		public Color TextColor
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x005B1E62 File Offset: 0x005B0062
		// (set) Token: 0x06002F47 RID: 12103 RVA: 0x005B1E6A File Offset: 0x005B006A
		public Color ShadowColor
		{
			get
			{
				return this._shadowColor;
			}
			set
			{
				this._shadowColor = value;
			}
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x005B1E74 File Offset: 0x005B0074
		public UIText(string text, float textScale = 1f, bool large = false)
		{
			this.TextOriginX = 0.5f;
			this.TextOriginY = 0f;
			this.IsWrapped = false;
			this.WrappedTextBottomPadding = 20f;
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x005B1EF0 File Offset: 0x005B00F0
		public UIText(LocalizedText text, float textScale = 1f, bool large = false)
		{
			this.TextOriginX = 0.5f;
			this.TextOriginY = 0f;
			this.IsWrapped = false;
			this.WrappedTextBottomPadding = 20f;
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x005B1F6B File Offset: 0x005B016B
		public override void Recalculate()
		{
			this.InternalSetText(this._text, this._textScale, this._isLarge);
			base.Recalculate();
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x005B1F8B File Offset: 0x005B018B
		public void SetText(string text)
		{
			this.InternalSetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x005B1F8B File Offset: 0x005B018B
		public void SetText(LocalizedText text)
		{
			this.InternalSetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x005B1FA0 File Offset: 0x005B01A0
		public void SetText(string text, float textScale, bool large)
		{
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x005B1FA0 File Offset: 0x005B01A0
		public void SetText(LocalizedText text, float textScale, bool large)
		{
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x005B1FAC File Offset: 0x005B01AC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			this.VerifyTextState();
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 vector = innerDimensions.Position();
			if (this._isLarge)
			{
				vector.Y -= 10f * this._textScale;
			}
			else
			{
				vector.Y -= 2f * this._textScale;
			}
			List<PositionedSnippet> list = this._textLayout;
			Vector2 vector2 = new Vector2(this._textScale);
			Vector2 vector3 = this._textSize;
			if (this.DynamicallyScaleDownToWidth && vector3.X > innerDimensions.Width)
			{
				float num = innerDimensions.Width / vector3.X;
				list = new List<PositionedSnippet>();
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Scale(num);
				}
				vector2 *= num;
				vector3 *= num;
			}
			vector.X += (innerDimensions.Width - vector3.X) * this.TextOriginX;
			vector.Y += (innerDimensions.Height - vector3.Y) * this.TextOriginY;
			Color color = this._shadowColor * ((float)this._color.A / 255f);
			DynamicSpriteFont dynamicSpriteFont = (this._isLarge ? FontAssets.DeathText.Value : FontAssets.MouseText.Value);
			ChatManager.DrawColorCodedStringShadow(spriteBatch, dynamicSpriteFont, this._textLayout, vector, color, 0f, Vector2.Zero, vector2, 1.5f);
			int num2;
			ChatManager.DrawColorCodedString(spriteBatch, dynamicSpriteFont, this._textLayout, vector, 0f, Vector2.Zero, vector2, out num2, null);
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x005B2158 File Offset: 0x005B0358
		private void VerifyTextState()
		{
			if (this._lastTextReference == this.Text)
			{
				return;
			}
			this.InternalSetText(this._text, this._textScale, this._isLarge);
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x005B2184 File Offset: 0x005B0384
		private void InternalSetText(object text, float textScale, bool large)
		{
			this._text = text;
			this._isLarge = large;
			this._textScale = textScale;
			this._lastTextReference = this._text.ToString();
			List<TextSnippet> list = ChatManager.ParseMessage(this._lastTextReference, this._color);
			ChatManager.ConvertNormalSnippets(list);
			DynamicSpriteFont dynamicSpriteFont = (large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value);
			this._textLayout = ChatManager.LayoutSnippets(dynamicSpriteFont, list, new Vector2(this._textScale), this.IsWrapped ? base.GetInnerDimensions().Width : (-1f)).ToList<PositionedSnippet>();
			this._textSize = ChatManager.GetStringSize(this._textLayout);
			if (this.IsWrapped)
			{
				this._textSize.Y = this._textSize.Y + this.WrappedTextBottomPadding * this._textScale;
			}
			else
			{
				this._textSize.Y = (large ? 32f : 16f) * this._textScale;
			}
			this.MinWidth.Set((this.IsWrapped || this.DynamicallyScaleDownToWidth) ? 0f : (this._textSize.X + this.PaddingLeft + this.PaddingRight), 0f);
			this.MinHeight.Set(this._textSize.Y + this.PaddingTop + this.PaddingBottom, 0f);
			if (this.OnInternalTextChange != null)
			{
				this.OnInternalTextChange();
			}
		}

		// Token: 0x0400564E RID: 22094
		private object _text = "";

		// Token: 0x0400564F RID: 22095
		private float _textScale = 1f;

		// Token: 0x04005650 RID: 22096
		private Vector2 _textSize = Vector2.Zero;

		// Token: 0x04005651 RID: 22097
		private bool _isLarge;

		// Token: 0x04005652 RID: 22098
		private Color _color = Color.White;

		// Token: 0x04005653 RID: 22099
		private Color _shadowColor = Color.Black;

		// Token: 0x04005654 RID: 22100
		private bool _isWrapped;

		// Token: 0x04005655 RID: 22101
		[CompilerGenerated]
		private float <TextOriginX>k__BackingField;

		// Token: 0x04005656 RID: 22102
		[CompilerGenerated]
		private float <TextOriginY>k__BackingField;

		// Token: 0x04005657 RID: 22103
		[CompilerGenerated]
		private float <WrappedTextBottomPadding>k__BackingField;

		// Token: 0x04005658 RID: 22104
		public bool DynamicallyScaleDownToWidth;

		// Token: 0x04005659 RID: 22105
		private List<PositionedSnippet> _textLayout;

		// Token: 0x0400565A RID: 22106
		private string _lastTextReference;

		// Token: 0x0400565B RID: 22107
		[CompilerGenerated]
		private Action OnInternalTextChange;
	}
}
