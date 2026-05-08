using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Text;

namespace ReLogic.Graphics
{
	// Token: 0x02000083 RID: 131
	public class DynamicSpriteFont : IFontMetrics
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000B328 File Offset: 0x00009528
		public float CharacterSpacing
		{
			get
			{
				return this._characterSpacing;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000B330 File Offset: 0x00009530
		public int LineSpacing
		{
			get
			{
				return this._lineSpacing;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B338 File Offset: 0x00009538
		public DynamicSpriteFont(float spacing, int lineSpacing, char defaultCharacter)
		{
			this._characterSpacing = spacing;
			this._lineSpacing = lineSpacing;
			this.DefaultCharacter = defaultCharacter;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B360 File Offset: 0x00009560
		public bool IsCharacterSupported(char character)
		{
			return character == '\n' || character == '\r' || this._spriteCharacters.ContainsKey(character);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B37C File Offset: 0x0000957C
		public bool AreCharactersSupported(IEnumerable<char> characters)
		{
			foreach (char c in characters)
			{
				if (!this.IsCharacterSupported(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B3D0 File Offset: 0x000095D0
		internal void SetPages(FontPage[] pages)
		{
			int num = 0;
			foreach (FontPage fontPage in pages)
			{
				num += fontPage.Characters.Count;
			}
			this._spriteCharacters = new Dictionary<char, DynamicSpriteFont.SpriteCharacterData>(num);
			foreach (FontPage fontPage2 in pages)
			{
				for (int j = 0; j < fontPage2.Characters.Count; j++)
				{
					this._spriteCharacters.Add(fontPage2.Characters[j], new DynamicSpriteFont.SpriteCharacterData(fontPage2.Texture, fontPage2.Glyphs[j], fontPage2.Padding[j], fontPage2.Kerning[j]));
					if (fontPage2.Characters[j] == this.DefaultCharacter)
					{
						this._defaultCharacterData = this._spriteCharacters[fontPage2.Characters[j]];
					}
				}
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B4D0 File Offset: 0x000096D0
		internal void InternalDraw(string text, SpriteBatch spriteBatch, Vector2 startPosition, Color color, float rotation, Vector2 origin, ref Vector2 scale, SpriteEffects spriteEffects, float depth)
		{
			this.InternalDraw(text, spriteBatch, startPosition, color, rotation, origin, ref scale, spriteEffects, depth, null, null, null);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000B4F8 File Offset: 0x000096F8
		internal void InternalDraw(string text, SpriteBatch spriteBatch, Vector2 startPosition, Color color, float rotation, Vector2 origin, ref Vector2 scale, SpriteEffects spriteEffects, float depth, Vector2[] charOffsets, Color[] charColors, float[] charRotations)
		{
			Matrix matrix = Matrix.CreateTranslation(-origin.X * scale.X, -origin.Y * scale.Y, 0f) * Matrix.CreateRotationZ(rotation);
			Vector2 zero = Vector2.Zero;
			Vector2 one = Vector2.One;
			bool flag = true;
			float num = 0f;
			if (spriteEffects != null)
			{
				Vector2 vector = this.MeasureString(text);
				if ((spriteEffects & 1) != null)
				{
					num = vector.X * scale.X;
					one.X = -1f;
				}
				if ((spriteEffects & 2) != null)
				{
					zero.Y = (vector.Y - (float)this.LineSpacing) * scale.Y;
					one.Y = -1f;
				}
			}
			zero.X = num;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				if (c != '\n')
				{
					if (c != '\r')
					{
						DynamicSpriteFont.SpriteCharacterData characterData = this.GetCharacterData(c);
						Vector3 kerning = characterData.Kerning;
						Rectangle padding = characterData.Padding;
						if ((spriteEffects & 1) != null)
						{
							padding.X -= padding.Width;
						}
						if ((spriteEffects & 2) != null)
						{
							padding.Y = this.LineSpacing - characterData.Glyph.Height - padding.Y;
						}
						if (flag)
						{
							kerning.X = Math.Max(kerning.X, 0f);
						}
						else
						{
							zero.X += this.CharacterSpacing * scale.X * one.X;
						}
						zero.X += kerning.X * scale.X * one.X;
						Color color2 = color;
						if (charColors != null)
						{
							color2 = charColors[i];
						}
						float num2 = rotation;
						if (charRotations != null)
						{
							num2 += charRotations[i];
						}
						Vector2 vector2 = zero;
						vector2.X += (float)padding.X * scale.X;
						vector2.Y += (float)padding.Y * scale.Y;
						if (charOffsets != null)
						{
							vector2 += charOffsets[i];
						}
						Vector2.Transform(ref vector2, ref matrix, ref vector2);
						vector2 += startPosition;
						spriteBatch.Draw(characterData.Texture, vector2, new Rectangle?(characterData.Glyph), color2, num2, Vector2.Zero, scale, spriteEffects, depth);
						zero.X += (kerning.Y + kerning.Z) * scale.X * one.X;
						flag = false;
					}
				}
				else
				{
					zero.X = num;
					zero.Y += (float)this.LineSpacing * scale.Y * one.Y;
					flag = true;
				}
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B7C0 File Offset: 0x000099C0
		private DynamicSpriteFont.SpriteCharacterData GetCharacterData(char character)
		{
			DynamicSpriteFont.SpriteCharacterData defaultCharacterData;
			if (!this._spriteCharacters.TryGetValue(character, ref defaultCharacterData))
			{
				defaultCharacterData = this._defaultCharacterData;
			}
			return defaultCharacterData;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public Vector2 MeasureString(string text)
		{
			if (text.Length == 0)
			{
				return Vector2.Zero;
			}
			Vector2 vector = Vector2.Zero;
			vector.Y = (float)this.LineSpacing;
			float num = 0f;
			int num2 = 0;
			float num3 = 0f;
			bool flag = true;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				if (c != '\n')
				{
					if (c != '\r')
					{
						DynamicSpriteFont.SpriteCharacterData characterData = this.GetCharacterData(c);
						Vector3 kerning = characterData.Kerning;
						if (flag)
						{
							kerning.X = Math.Max(kerning.X, 0f);
						}
						else
						{
							vector.X += this.CharacterSpacing + num3;
						}
						vector.X += kerning.X + kerning.Y;
						num3 = kerning.Z;
						vector.Y = Math.Max(vector.Y, (float)characterData.Padding.Height);
						flag = false;
					}
				}
				else
				{
					num = Math.Max(vector.X + Math.Max(num3, 0f), num);
					num3 = 0f;
					vector = Vector2.Zero;
					vector.Y = (float)this.LineSpacing;
					flag = true;
					num2++;
				}
			}
			vector.X += Math.Max(num3, 0f);
			vector.Y += (float)(num2 * this.LineSpacing);
			vector.X = Math.Max(vector.X, num);
			return vector;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000B963 File Offset: 0x00009B63
		public string CreateWrappedText(string text, float scale, float maxWidth, float firstLineOffset, CultureInfo culture)
		{
			if (scale == 0f)
			{
				scale = 1f;
			}
			return this.CreateWrappedText(text, maxWidth / scale, firstLineOffset, culture);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000B982 File Offset: 0x00009B82
		public string CreateWrappedText(string text, float maxWidth, CultureInfo culture)
		{
			return this.CreateWrappedText(text, maxWidth, 0f, culture);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000B992 File Offset: 0x00009B92
		public string CreateWrappedText(string text, float maxWidth, float firstLineOffset, CultureInfo culture)
		{
			if (maxWidth <= 0f)
			{
				return text;
			}
			return new WrappedTextBuilder(this, culture, maxWidth, firstLineOffset).Build(text);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public string CreateCroppedText(string text, float maxWidth)
		{
			Vector2 vector = this.MeasureString(text);
			Vector2 vector2 = this.MeasureString("…");
			maxWidth -= vector2.X;
			if (maxWidth <= vector2.X)
			{
				return "…";
			}
			if (vector.X > maxWidth)
			{
				int num = 200;
				while (vector.X > maxWidth && text.Length > 1)
				{
					num--;
					if (num <= 0)
					{
						break;
					}
					text = text.Substring(0, text.Length - 1);
					if (text.Length == 1)
					{
						text = "";
						break;
					}
					vector = this.MeasureString(text);
				}
				text += "…";
			}
			return text;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000BA50 File Offset: 0x00009C50
		public GlyphMetrics GetCharacterMetrics(char character)
		{
			return this.GetCharacterData(character).ToGlyphMetric();
		}

		// Token: 0x040004EA RID: 1258
		private Dictionary<char, DynamicSpriteFont.SpriteCharacterData> _spriteCharacters = new Dictionary<char, DynamicSpriteFont.SpriteCharacterData>();

		// Token: 0x040004EB RID: 1259
		private DynamicSpriteFont.SpriteCharacterData _defaultCharacterData;

		// Token: 0x040004EC RID: 1260
		public readonly char DefaultCharacter;

		// Token: 0x040004ED RID: 1261
		private readonly float _characterSpacing;

		// Token: 0x040004EE RID: 1262
		private readonly int _lineSpacing;

		// Token: 0x020000E6 RID: 230
		private struct SpriteCharacterData
		{
			// Token: 0x06000474 RID: 1140 RVA: 0x0000E5EA File Offset: 0x0000C7EA
			public SpriteCharacterData(Texture2D texture, Rectangle glyph, Rectangle padding, Vector3 kerning)
			{
				this.Texture = texture;
				this.Glyph = glyph;
				this.Padding = padding;
				this.Kerning = kerning;
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x0000E609 File Offset: 0x0000C809
			public GlyphMetrics ToGlyphMetric()
			{
				return GlyphMetrics.FromKerningData(this.Kerning.X, this.Kerning.Y, this.Kerning.Z);
			}

			// Token: 0x04000614 RID: 1556
			public readonly Texture2D Texture;

			// Token: 0x04000615 RID: 1557
			public readonly Rectangle Glyph;

			// Token: 0x04000616 RID: 1558
			public readonly Rectangle Padding;

			// Token: 0x04000617 RID: 1559
			public readonly Vector3 Kerning;
		}
	}
}
