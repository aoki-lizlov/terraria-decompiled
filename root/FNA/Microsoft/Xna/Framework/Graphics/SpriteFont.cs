using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000BB RID: 187
	public sealed class SpriteFont
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0003188B File Offset: 0x0002FA8B
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00031893 File Offset: 0x0002FA93
		public ReadOnlyCollection<char> Characters
		{
			[CompilerGenerated]
			get
			{
				return this.<Characters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Characters>k__BackingField = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0003189C File Offset: 0x0002FA9C
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x000318A4 File Offset: 0x0002FAA4
		public char? DefaultCharacter
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultCharacter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DefaultCharacter>k__BackingField = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x000318AD File Offset: 0x0002FAAD
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x000318B5 File Offset: 0x0002FAB5
		public int LineSpacing
		{
			get
			{
				return this.lineSpacing;
			}
			set
			{
				this.lineSpacing = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x000318BE File Offset: 0x0002FABE
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x000318C6 File Offset: 0x0002FAC6
		public float Spacing
		{
			get
			{
				return this.spacing;
			}
			set
			{
				this.spacing = value;
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x000318D0 File Offset: 0x0002FAD0
		internal SpriteFont(Texture2D texture, List<Rectangle> glyphBounds, List<Rectangle> cropping, List<char> characters, int lineSpacing, float spacing, List<Vector3> kerningData, char? defaultCharacter)
		{
			this.Characters = new ReadOnlyCollection<char>(characters.ToArray());
			this.DefaultCharacter = defaultCharacter;
			this.LineSpacing = lineSpacing;
			this.Spacing = spacing;
			this.textureValue = texture;
			this.glyphData = glyphBounds;
			this.croppingData = cropping;
			this.kerning = kerningData;
			this.characterMap = characters;
			this.characterIndexMap = new Dictionary<char, int>(characters.Count);
			for (int i = 0; i < characters.Count; i++)
			{
				this.characterIndexMap[characters[i]] = i;
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0003196C File Offset: 0x0002FB6C
		public Vector2 MeasureString(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 0)
			{
				return Vector2.Zero;
			}
			Vector2 zero = Vector2.Zero;
			float num = 0f;
			float num2 = (float)this.LineSpacing;
			bool flag = true;
			foreach (char c in text)
			{
				if (c != '\r')
				{
					if (c == '\n')
					{
						zero.X = Math.Max(zero.X, num);
						zero.Y += (float)this.LineSpacing;
						num = 0f;
						num2 = (float)this.LineSpacing;
						flag = true;
					}
					else
					{
						int num3;
						if (!this.characterIndexMap.TryGetValue(c, out num3))
						{
							if (this.DefaultCharacter == null)
							{
								throw new ArgumentException("Text contains characters that cannot be resolved by this SpriteFont.", "text");
							}
							num3 = this.characterIndexMap[this.DefaultCharacter.Value];
						}
						Vector3 vector = this.kerning[num3];
						if (flag)
						{
							num += Math.Abs(vector.X);
							flag = false;
						}
						else
						{
							num += this.Spacing + vector.X;
						}
						num += vector.Y + vector.Z;
						int height = this.croppingData[num3].Height;
						if ((float)height > num2)
						{
							num2 = (float)height;
						}
					}
				}
			}
			zero.X = Math.Max(zero.X, num);
			zero.Y += num2;
			return zero;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00031AF4 File Offset: 0x0002FCF4
		public Vector2 MeasureString(StringBuilder text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 0)
			{
				return Vector2.Zero;
			}
			Vector2 zero = Vector2.Zero;
			float num = 0f;
			float num2 = (float)this.LineSpacing;
			bool flag = true;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c != '\r')
				{
					if (c == '\n')
					{
						zero.X = Math.Max(zero.X, num);
						zero.Y += (float)this.LineSpacing;
						num = 0f;
						num2 = (float)this.LineSpacing;
						flag = true;
					}
					else
					{
						int num3;
						if (!this.characterIndexMap.TryGetValue(c, out num3))
						{
							if (this.DefaultCharacter == null)
							{
								throw new ArgumentException("Text contains characters that cannot be resolved by this SpriteFont.", "text");
							}
							num3 = this.characterIndexMap[this.DefaultCharacter.Value];
						}
						Vector3 vector = this.kerning[num3];
						if (flag)
						{
							num += Math.Abs(vector.X);
							flag = false;
						}
						else
						{
							num += this.Spacing + vector.X;
						}
						num += vector.Y + vector.Z;
						int height = this.croppingData[num3].Height;
						if ((float)height > num2)
						{
							num2 = (float)height;
						}
					}
				}
			}
			zero.X = Math.Max(zero.X, num);
			zero.Y += num2;
			return zero;
		}

		// Token: 0x040009BA RID: 2490
		[CompilerGenerated]
		private ReadOnlyCollection<char> <Characters>k__BackingField;

		// Token: 0x040009BB RID: 2491
		[CompilerGenerated]
		private char? <DefaultCharacter>k__BackingField;

		// Token: 0x040009BC RID: 2492
		internal Texture2D textureValue;

		// Token: 0x040009BD RID: 2493
		internal List<Rectangle> glyphData;

		// Token: 0x040009BE RID: 2494
		internal List<Rectangle> croppingData;

		// Token: 0x040009BF RID: 2495
		internal List<Vector3> kerning;

		// Token: 0x040009C0 RID: 2496
		internal List<char> characterMap;

		// Token: 0x040009C1 RID: 2497
		internal int lineSpacing;

		// Token: 0x040009C2 RID: 2498
		internal float spacing;

		// Token: 0x040009C3 RID: 2499
		internal Dictionary<char, int> characterIndexMap;
	}
}
