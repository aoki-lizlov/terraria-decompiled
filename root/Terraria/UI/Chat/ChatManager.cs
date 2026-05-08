using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Chat;
using Terraria.GameContent.UI.Chat;
using Terraria.Localization;
using Terraria.Testing.ChatCommands;

namespace Terraria.UI.Chat
{
	// Token: 0x0200010C RID: 268
	public static class ChatManager
	{
		// Token: 0x06001A94 RID: 6804 RVA: 0x004F7158 File Offset: 0x004F5358
		public static Color WaveColor(Color color)
		{
			float num = (float)Main.mouseTextColor / 255f;
			color = Color.Lerp(color, Color.Black, 1f - num);
			color.A = Main.mouseTextColor;
			return color;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x004F7194 File Offset: 0x004F5394
		public static void ConvertNormalSnippets(List<TextSnippet> snippets)
		{
			for (int i = 0; i < snippets.Count; i++)
			{
				TextSnippet textSnippet = snippets[i];
				if (textSnippet.GetType() == typeof(TextSnippet))
				{
					snippets[i] = new PlainTagHandler.PlainSnippet(textSnippet.Text, textSnippet.Color);
				}
			}
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x004F71EC File Offset: 0x004F53EC
		public static void Register<T>(params string[] names) where T : ITagHandler, new()
		{
			T t = new T();
			for (int i = 0; i < names.Length; i++)
			{
				ChatManager._handlers[names[i].ToLower()] = t;
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x004F7228 File Offset: 0x004F5428
		private static ITagHandler GetHandler(string tagName)
		{
			string text = tagName.ToLower();
			if (ChatManager._handlers.ContainsKey(text))
			{
				return ChatManager._handlers[text];
			}
			return null;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x004F7256 File Offset: 0x004F5456
		public static bool MayNeedParsing(string text)
		{
			return text.IndexOf('\r') >= 0 || ChatManager.Regexes.Format.IsMatch(text);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x004F7270 File Offset: 0x004F5470
		public static List<TextSnippet> ParseMessage(string text, Color baseColor)
		{
			text = text.Replace("\r", "");
			MatchCollection matchCollection = ChatManager.Regexes.Format.Matches(text);
			List<TextSnippet> list = new List<TextSnippet>();
			int num = 0;
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				if (match.Index > num)
				{
					list.Add(new TextSnippet(text.Substring(num, match.Index - num), baseColor));
				}
				num = match.Index + match.Length;
				string value = match.Groups["tag"].Value;
				string text2 = match.Groups["text"].Value.Replace("\\]", "]");
				string value2 = match.Groups["options"].Value;
				ITagHandler handler = ChatManager.GetHandler(value);
				if (handler != null)
				{
					list.Add(handler.Parse(text2, baseColor, value2));
					list[list.Count - 1].TextOriginal = match.ToString();
				}
				else
				{
					list.Add(new TextSnippet(text2, baseColor));
				}
			}
			if (text.Length > num)
			{
				list.Add(new TextSnippet(text.Substring(num, text.Length - num), baseColor));
			}
			return list;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x004F73DC File Offset: 0x004F55DC
		public static bool AddChatText(DynamicSpriteFont font, string text, Vector2 baseScale)
		{
			int num = Main.screenWidth - 330;
			if (ChatManager.GetStringSize(font, Main.chatText + text, baseScale, -1f).X > (float)num)
			{
				return false;
			}
			Main.chatText += text;
			return true;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x004F742E File Offset: 0x004F562E
		public static IEnumerable<PositionedSnippet> LayoutSnippets(DynamicSpriteFont font, IEnumerable<TextSnippet> snippets, Vector2 scale, float maxWidth = -1f)
		{
			int line = 0;
			Vector2 pos = Vector2.Zero;
			float uniqueDrawScale = Math.Min(scale.X, scale.Y);
			int i = 0;
			foreach (TextSnippet snippet in snippets)
			{
				Vector2 size;
				int num;
				if (snippet.UniqueDraw(true, out size, null, default(Vector2), default(Color), uniqueDrawScale))
				{
					if (maxWidth >= 0f && pos.X + size.X > maxWidth)
					{
						pos.X = 0f;
						pos.Y += (float)font.LineSpacing * scale.Y;
						num = line;
						line = num + 1;
					}
					yield return new PositionedSnippet(snippet, i, line, pos, size);
					pos.X += size.X;
				}
				else
				{
					string text = font.CreateWrappedText(snippet.Text, scale.X, maxWidth, pos.X, Language.ActiveCulture.CultureInfo);
					int num2 = 0;
					for (;;)
					{
						int sep = text.IndexOf('\n', num2);
						int num3 = ((sep < 0) ? text.Length : sep) - num2;
						if (num3 > 0)
						{
							string text2 = text.Substring(num2, num3);
							size = font.MeasureString(text2) * scale;
							yield return new PositionedSnippet(snippet.CopyMorph(text2), i, line, pos, size);
							pos.X += size.X;
						}
						if (sep < 0)
						{
							break;
						}
						pos.X = 0f;
						pos.Y += (float)font.LineSpacing * scale.Y;
						num = line;
						line = num + 1;
						num2 = sep + 1;
					}
					text = null;
				}
				num = i;
				i = num + 1;
				size = default(Vector2);
				snippet = null;
			}
			IEnumerator<TextSnippet> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x004F7453 File Offset: 0x004F5653
		public static Vector2 GetStringSize(DynamicSpriteFont font, string text, Vector2 baseScale, float maxWidth = -1f)
		{
			return ChatManager.GetStringSize(font, ChatManager.ParseMessage(text, Color.White), baseScale, maxWidth);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x004F7468 File Offset: 0x004F5668
		public static Vector2 GetStringSize(DynamicSpriteFont font, IEnumerable<TextSnippet> snippets, Vector2 scale, float maxWidth = -1f)
		{
			return ChatManager.GetStringSize(ChatManager.LayoutSnippets(font, snippets, scale, maxWidth));
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x004F7478 File Offset: 0x004F5678
		public static Vector2 GetStringSize(IEnumerable<PositionedSnippet> snippets)
		{
			Vector2 zero = Vector2.Zero;
			foreach (PositionedSnippet positionedSnippet in snippets)
			{
				zero.X = Math.Max(zero.X, positionedSnippet.Position.X + positionedSnippet.Size.X);
				zero.Y = Math.Max(zero.Y, positionedSnippet.Position.Y + positionedSnippet.Size.Y);
			}
			return zero;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x004F7514 File Offset: 0x004F5714
		public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, IEnumerable<TextSnippet> snippets, Vector2 position, Color shadowColor, float rotation, Vector2 origin, Vector2 scale, float maxWidth = -1f, float spread = 2f)
		{
			List<PositionedSnippet> list = ChatManager.LayoutSnippets(font, snippets, scale, maxWidth).ToList<PositionedSnippet>();
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, list, position, shadowColor, rotation, origin, scale, spread);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x004F7548 File Offset: 0x004F5748
		public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, List<PositionedSnippet> snippets, Vector2 position, Color shadowColor, float rotation, Vector2 origin, Vector2 scale, float spread = 2f)
		{
			for (int i = 0; i < ChatManager.ShadowDirections.Length; i++)
			{
				int num;
				ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position + ChatManager.ShadowDirections[i] * spread, rotation, origin, scale, out num, new Color?(shadowColor));
			}
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x004F7598 File Offset: 0x004F5798
		public static void DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, IEnumerable<TextSnippet> snippets, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 scale, out int hoveredSnippet, float maxWidth = -1f, bool ignoreColors = false)
		{
			ChatManager.DrawColorCodedString(spriteBatch, font, ChatManager.LayoutSnippets(font, snippets, scale, maxWidth), position, rotation, origin, scale, out hoveredSnippet, ignoreColors ? new Color?(baseColor) : null);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x004F75D8 File Offset: 0x004F57D8
		public static void DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, IEnumerable<TextSnippet> snippets, Vector2 position, float rotation, Vector2 origin, Vector2 scale, out int hoveredSnippet, float maxWidth = -1f)
		{
			ChatManager.DrawColorCodedString(spriteBatch, font, ChatManager.LayoutSnippets(font, snippets, scale, maxWidth), position, rotation, origin, scale, out hoveredSnippet, null);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x004F760C File Offset: 0x004F580C
		public static void DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, IEnumerable<PositionedSnippet> snippets, Vector2 position, float rotation, Vector2 origin, Vector2 scale, out int hoveredSnippet, Color? colorOverride = null)
		{
			hoveredSnippet = -1;
			Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float num = Math.Min(scale.X, scale.Y);
			foreach (PositionedSnippet positionedSnippet in snippets)
			{
				Vector2 vector2 = position + positionedSnippet.Position;
				TextSnippet snippet = positionedSnippet.Snippet;
				Color color = ((colorOverride != null) ? colorOverride.Value : snippet.GetVisibleColor());
				Vector2 vector3;
				if (!snippet.UniqueDraw(false, out vector3, spriteBatch, vector2, color, num))
				{
					DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, snippet.Text, vector2, color, rotation, origin, scale, SpriteEffects.None, 0f);
				}
				if (positionedSnippet.Snippet.CheckForHover && vector.Between(vector2, vector2 + positionedSnippet.Size))
				{
					hoveredSnippet = positionedSnippet.OrigIndex;
				}
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x004F770C File Offset: 0x004F590C
		public static void DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth = -1f, float spread = 2f)
		{
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, Color.Black, rotation, origin, baseScale, maxWidth, spread);
			ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, rotation, origin, baseScale, out hoveredSnippet, maxWidth);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x004F7744 File Offset: 0x004F5944
		public static void DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, TextSnippet[] snippets, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 baseScale, out int hoveredSnippet, float maxWidth = -1f, float spread = 2f)
		{
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, snippets, position, color.MultiplyRGBA(Color.Black), rotation, origin, baseScale, maxWidth, spread);
			ChatManager.DrawColorCodedString(spriteBatch, font, snippets, position, color, rotation, origin, baseScale, out hoveredSnippet, maxWidth, true);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x004F7788 File Offset: 0x004F5988
		public static void DrawColorCodedStringShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, float spread = 2f)
		{
			for (int i = 0; i < ChatManager.ShadowDirections.Length; i++)
			{
				ChatManager.DrawColorCodedString(spriteBatch, font, text, position + ChatManager.ShadowDirections[i] * spread, baseColor, rotation, origin, baseScale, maxWidth, true);
			}
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x004F77D4 File Offset: 0x004F59D4
		public static Vector2 DrawColorCodedString(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth = -1f, bool ignoreColors = false)
		{
			Vector2 vector = position;
			Vector2 vector2 = vector;
			string[] array = text.Split(new char[] { '\n' });
			float x = font.MeasureString(" ").X;
			Color color = baseColor;
			float num = 1f;
			float num2 = 0f;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				foreach (string text2 in array2[i].Split(new char[] { ':' }))
				{
					if (text2.StartsWith("sss"))
					{
						if (text2.StartsWith("sss1"))
						{
							if (!ignoreColors)
							{
								color = Color.Red;
							}
						}
						else if (text2.StartsWith("sss2"))
						{
							if (!ignoreColors)
							{
								color = Color.Blue;
							}
						}
						else if (text2.StartsWith("sssr") && !ignoreColors)
						{
							color = Color.White;
						}
					}
					else
					{
						string[] array4 = text2.Split(new char[] { ' ' });
						for (int k = 0; k < array4.Length; k++)
						{
							if (k != 0)
							{
								vector.X += x * baseScale.X * num;
							}
							if (maxWidth > 0f)
							{
								float num3 = font.MeasureString(array4[k]).X * baseScale.X * num;
								if (vector.X - position.X + num3 > maxWidth)
								{
									vector.X = position.X;
									vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
									vector2.Y = Math.Max(vector2.Y, vector.Y);
									num2 = 0f;
								}
							}
							if (num2 < num)
							{
								num2 = num;
							}
							DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, array4[k], vector, color, rotation, origin, baseScale * num, SpriteEffects.None, 0f);
							vector.X += font.MeasureString(array4[k]).X * baseScale.X * num;
							vector2.X = Math.Max(vector2.X, vector.X);
						}
					}
				}
				vector.X = position.X;
				vector.Y += (float)font.LineSpacing * num2 * baseScale.Y;
				vector2.Y = Math.Max(vector2.Y, vector.Y);
				num2 = 0f;
			}
			return vector2;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x004F7A5C File Offset: 0x004F5C5C
		public static void DrawColorCodedStringWithShadow(SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 scale, float maxWidth = -1f, float spread = 2f)
		{
			Color color = baseColor.MultiplyRGBA(Color.Black);
			if (maxWidth < 0f && !ChatManager.MayNeedParsing(text))
			{
				foreach (Vector2 vector in ChatManager.ShadowDirections)
				{
					DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, text, position + vector * spread, color, rotation, origin, scale, SpriteEffects.None, 0f);
				}
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, text, position, baseColor, rotation, origin, scale, SpriteEffects.None, 0f);
				return;
			}
			List<TextSnippet> list = ChatManager.ParseMessage(text, baseColor);
			ChatManager.ConvertNormalSnippets(list);
			List<PositionedSnippet> list2 = ChatManager.LayoutSnippets(font, list, scale, maxWidth).ToList<PositionedSnippet>();
			ChatManager.DrawColorCodedStringShadow(spriteBatch, font, list2, position, color, rotation, origin, scale, spread);
			int num;
			ChatManager.DrawColorCodedString(spriteBatch, font, list2, position, rotation, origin, scale, out num, null);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x004F7B38 File Offset: 0x004F5D38
		// Note: this type is marked as 'beforefieldinit'.
		static ChatManager()
		{
		}

		// Token: 0x04001507 RID: 5383
		public static readonly DebugCommandProcessor DebugCommands = new DebugCommandProcessor();

		// Token: 0x04001508 RID: 5384
		public static readonly ChatCommandProcessor Commands = new ChatCommandProcessor();

		// Token: 0x04001509 RID: 5385
		private static ConcurrentDictionary<string, ITagHandler> _handlers = new ConcurrentDictionary<string, ITagHandler>();

		// Token: 0x0400150A RID: 5386
		public static readonly Vector2[] ShadowDirections = new Vector2[]
		{
			-Vector2.UnitX,
			Vector2.UnitX,
			-Vector2.UnitY,
			Vector2.UnitY
		};

		// Token: 0x0200071D RID: 1821
		public static class Regexes
		{
			// Token: 0x06004052 RID: 16466 RVA: 0x0069D954 File Offset: 0x0069BB54
			// Note: this type is marked as 'beforefieldinit'.
			static Regexes()
			{
			}

			// Token: 0x04006935 RID: 26933
			public static readonly Regex Format = new Regex("(?<!\\\\)\\[(?<tag>[a-zA-Z]{1,10})(\\/(?<options>[^:]+))?:(?<text>.+?)(?<!\\\\)\\]", RegexOptions.Compiled | RegexOptions.Singleline);
		}

		// Token: 0x0200071E RID: 1822
		[CompilerGenerated]
		private sealed class <LayoutSnippets>d__12 : IEnumerable<PositionedSnippet>, IEnumerable, IEnumerator<PositionedSnippet>, IDisposable, IEnumerator
		{
			// Token: 0x06004053 RID: 16467 RVA: 0x0069D967 File Offset: 0x0069BB67
			[DebuggerHidden]
			public <LayoutSnippets>d__12(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06004054 RID: 16468 RVA: 0x0069D988 File Offset: 0x0069BB88
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num - 1 <= 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06004055 RID: 16469 RVA: 0x0069D9C4 File Offset: 0x0069BBC4
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						line = 0;
						pos = Vector2.Zero;
						uniqueDrawScale = Math.Min(scale.X, scale.Y);
						i = 0;
						enumerator = snippets.GetEnumerator();
						this.<>1__state = -3;
						goto IL_0332;
					case 1:
						this.<>1__state = -3;
						pos.X += size.X;
						goto IL_030D;
					case 2:
						this.<>1__state = -3;
						pos.X += size.X;
						goto IL_02A5;
					default:
						return false;
					}
					IL_01D8:
					int num;
					sep = text.IndexOf('\n', num);
					int num2 = ((sep < 0) ? text.Length : sep) - num;
					if (num2 > 0)
					{
						string text2 = text.Substring(num, num2);
						size = font.MeasureString(text2) * scale;
						this.<>2__current = new PositionedSnippet(snippet.CopyMorph(text2), i, line, pos, size);
						this.<>1__state = 2;
						return true;
					}
					IL_02A5:
					int num3;
					if (sep >= 0)
					{
						pos.X = 0f;
						pos.Y += (float)font.LineSpacing * scale.Y;
						num3 = line;
						line = num3 + 1;
						num = sep + 1;
						goto IL_01D8;
					}
					text = null;
					IL_030D:
					num3 = i;
					i = num3 + 1;
					size = default(Vector2);
					snippet = null;
					IL_0332:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						snippet = enumerator.Current;
						if (!snippet.UniqueDraw(true, out size, null, default(Vector2), default(Color), uniqueDrawScale))
						{
							text = font.CreateWrappedText(snippet.Text, scale.X, maxWidth, pos.X, Language.ActiveCulture.CultureInfo);
							num = 0;
							goto IL_01D8;
						}
						if (maxWidth >= 0f && pos.X + size.X > maxWidth)
						{
							pos.X = 0f;
							pos.Y += (float)font.LineSpacing * scale.Y;
							num3 = line;
							line = num3 + 1;
						}
						this.<>2__current = new PositionedSnippet(snippet, i, line, pos, size);
						this.<>1__state = 1;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06004056 RID: 16470 RVA: 0x0069DD48 File Offset: 0x0069BF48
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x06004057 RID: 16471 RVA: 0x0069DD64 File Offset: 0x0069BF64
			PositionedSnippet IEnumerator<PositionedSnippet>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004058 RID: 16472 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x06004059 RID: 16473 RVA: 0x0069DD6C File Offset: 0x0069BF6C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600405A RID: 16474 RVA: 0x0069DD7C File Offset: 0x0069BF7C
			[DebuggerHidden]
			IEnumerator<PositionedSnippet> IEnumerable<PositionedSnippet>.GetEnumerator()
			{
				ChatManager.<LayoutSnippets>d__12 <LayoutSnippets>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<LayoutSnippets>d__ = this;
				}
				else
				{
					<LayoutSnippets>d__ = new ChatManager.<LayoutSnippets>d__12(0);
				}
				<LayoutSnippets>d__.font = font;
				<LayoutSnippets>d__.snippets = snippets;
				<LayoutSnippets>d__.scale = scale;
				<LayoutSnippets>d__.maxWidth = maxWidth;
				return <LayoutSnippets>d__;
			}

			// Token: 0x0600405B RID: 16475 RVA: 0x0069DDE8 File Offset: 0x0069BFE8
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Terraria.UI.Chat.PositionedSnippet>.GetEnumerator();
			}

			// Token: 0x04006936 RID: 26934
			private int <>1__state;

			// Token: 0x04006937 RID: 26935
			private PositionedSnippet <>2__current;

			// Token: 0x04006938 RID: 26936
			private int <>l__initialThreadId;

			// Token: 0x04006939 RID: 26937
			private Vector2 scale;

			// Token: 0x0400693A RID: 26938
			public Vector2 <>3__scale;

			// Token: 0x0400693B RID: 26939
			private IEnumerable<TextSnippet> snippets;

			// Token: 0x0400693C RID: 26940
			public IEnumerable<TextSnippet> <>3__snippets;

			// Token: 0x0400693D RID: 26941
			private float maxWidth;

			// Token: 0x0400693E RID: 26942
			public float <>3__maxWidth;

			// Token: 0x0400693F RID: 26943
			private DynamicSpriteFont font;

			// Token: 0x04006940 RID: 26944
			public DynamicSpriteFont <>3__font;

			// Token: 0x04006941 RID: 26945
			private int <line>5__2;

			// Token: 0x04006942 RID: 26946
			private Vector2 <pos>5__3;

			// Token: 0x04006943 RID: 26947
			private float <uniqueDrawScale>5__4;

			// Token: 0x04006944 RID: 26948
			private int <i>5__5;

			// Token: 0x04006945 RID: 26949
			private IEnumerator<TextSnippet> <>7__wrap5;

			// Token: 0x04006946 RID: 26950
			private TextSnippet <snippet>5__7;

			// Token: 0x04006947 RID: 26951
			private Vector2 <size>5__8;

			// Token: 0x04006948 RID: 26952
			private string <text>5__9;

			// Token: 0x04006949 RID: 26953
			private int <sep>5__10;
		}
	}
}
