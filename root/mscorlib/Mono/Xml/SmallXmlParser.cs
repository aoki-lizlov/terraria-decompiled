using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x02000042 RID: 66
	internal class SmallXmlParser
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00004708 File Offset: 0x00002908
		public SmallXmlParser()
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004760 File Offset: 0x00002960
		private Exception Error(string msg)
		{
			return new SmallXmlParserException(msg, this.line, this.column);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004774 File Offset: 0x00002974
		private Exception UnexpectedEndError()
		{
			string[] array = new string[this.elementNames.Count];
			this.elementNames.CopyTo(array, 0);
			return this.Error(string.Format("Unexpected end of stream. Element stack content is {0}", string.Join(",", array)));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000047BC File Offset: 0x000029BC
		private bool IsNameChar(char c, bool start)
		{
			if (c <= '.')
			{
				if (c == '-' || c == '.')
				{
					return !start;
				}
			}
			else if (c == ':' || c == '_')
			{
				return true;
			}
			if (c > 'Ā')
			{
				if (c == 'ՙ' || c == 'ۥ' || c == 'ۦ')
				{
					return true;
				}
				if ('ʻ' <= c && c <= 'ˁ')
				{
					return true;
				}
			}
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.DecimalDigitNumber:
				return !start;
			default:
				return false;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000485E File Offset: 0x00002A5E
		private bool IsWhitespace(int c)
		{
			return c - 9 <= 1 || c == 13 || c == 32;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004874 File Offset: 0x00002A74
		public void SkipWhitespaces()
		{
			this.SkipWhitespaces(false);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000487D File Offset: 0x00002A7D
		private void HandleWhitespaces()
		{
			while (this.IsWhitespace(this.Peek()))
			{
				this.buffer.Append((char)this.Read());
			}
			if (this.Peek() != 60 && this.Peek() >= 0)
			{
				this.isWhitespace = false;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000048BC File Offset: 0x00002ABC
		public void SkipWhitespaces(bool expected)
		{
			for (;;)
			{
				int num = this.Peek();
				if (num - 9 > 1 && num != 13 && num != 32)
				{
					break;
				}
				this.Read();
				if (expected)
				{
					expected = false;
				}
			}
			if (expected)
			{
				throw this.Error("Whitespace is expected.");
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000048FF File Offset: 0x00002AFF
		private int Peek()
		{
			return this.reader.Peek();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000490C File Offset: 0x00002B0C
		private int Read()
		{
			int num = this.reader.Read();
			if (num == 10)
			{
				this.resetColumn = true;
			}
			if (this.resetColumn)
			{
				this.line++;
				this.resetColumn = false;
				this.column = 1;
				return num;
			}
			this.column++;
			return num;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004964 File Offset: 0x00002B64
		public void Expect(int c)
		{
			int num = this.Read();
			if (num < 0)
			{
				throw this.UnexpectedEndError();
			}
			if (num != c)
			{
				throw this.Error(string.Format("Expected '{0}' but got {1}", (char)c, (char)num));
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000049A8 File Offset: 0x00002BA8
		private string ReadUntil(char until, bool handleReferences)
		{
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == until)
				{
					string text = this.buffer.ToString();
					this.buffer.Length = 0;
					return text;
				}
				if (handleReferences && c == '&')
				{
					this.ReadReference();
				}
				else
				{
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004A08 File Offset: 0x00002C08
		public string ReadName()
		{
			int num = 0;
			if (this.Peek() < 0 || !this.IsNameChar((char)this.Peek(), true))
			{
				throw this.Error("XML name start character is expected.");
			}
			for (int i = this.Peek(); i >= 0; i = this.Peek())
			{
				char c = (char)i;
				if (!this.IsNameChar(c, false))
				{
					break;
				}
				if (num == this.nameBuffer.Length)
				{
					char[] array = new char[num * 2];
					Array.Copy(this.nameBuffer, array, num);
					this.nameBuffer = array;
				}
				this.nameBuffer[num++] = c;
				this.Read();
			}
			if (num == 0)
			{
				throw this.Error("Valid XML name is expected.");
			}
			return new string(this.nameBuffer, 0, num);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public void Parse(TextReader input, SmallXmlParser.IContentHandler handler)
		{
			this.reader = input;
			this.handler = handler;
			handler.OnStartParsing(this);
			while (this.Peek() >= 0)
			{
				this.ReadContent();
			}
			this.HandleBufferedContent();
			if (this.elementNames.Count > 0)
			{
				throw this.Error(string.Format("Insufficient close tag: {0}", this.elementNames.Peek()));
			}
			handler.OnEndParsing(this);
			this.Cleanup();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004B28 File Offset: 0x00002D28
		private void Cleanup()
		{
			this.line = 1;
			this.column = 0;
			this.handler = null;
			this.reader = null;
			this.elementNames.Clear();
			this.xmlSpaces.Clear();
			this.attributes.Clear();
			this.buffer.Length = 0;
			this.xmlSpace = null;
			this.isWhitespace = false;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004B8C File Offset: 0x00002D8C
		public void ReadContent()
		{
			if (this.IsWhitespace(this.Peek()))
			{
				if (this.buffer.Length == 0)
				{
					this.isWhitespace = true;
				}
				this.HandleWhitespaces();
			}
			if (this.Peek() != 60)
			{
				this.ReadCharacters();
				return;
			}
			this.Read();
			int num = this.Peek();
			if (num != 33)
			{
				if (num != 47)
				{
					string text;
					if (num != 63)
					{
						this.HandleBufferedContent();
						text = this.ReadName();
						while (this.Peek() != 62 && this.Peek() != 47)
						{
							this.ReadAttribute(this.attributes);
						}
						this.handler.OnStartElement(text, this.attributes);
						this.attributes.Clear();
						this.SkipWhitespaces();
						if (this.Peek() == 47)
						{
							this.Read();
							this.handler.OnEndElement(text);
						}
						else
						{
							this.elementNames.Push(text);
							this.xmlSpaces.Push(this.xmlSpace);
						}
						this.Expect(62);
						return;
					}
					this.HandleBufferedContent();
					this.Read();
					text = this.ReadName();
					this.SkipWhitespaces();
					string text2 = string.Empty;
					if (this.Peek() != 63)
					{
						for (;;)
						{
							text2 += this.ReadUntil('?', false);
							if (this.Peek() == 62)
							{
								break;
							}
							text2 += "?";
						}
					}
					this.handler.OnProcessingInstruction(text, text2);
					this.Expect(62);
					return;
				}
				else
				{
					this.HandleBufferedContent();
					if (this.elementNames.Count == 0)
					{
						throw this.UnexpectedEndError();
					}
					this.Read();
					string text = this.ReadName();
					this.SkipWhitespaces();
					string text3 = (string)this.elementNames.Pop();
					this.xmlSpaces.Pop();
					if (this.xmlSpaces.Count > 0)
					{
						this.xmlSpace = (string)this.xmlSpaces.Peek();
					}
					else
					{
						this.xmlSpace = null;
					}
					if (text != text3)
					{
						throw this.Error(string.Format("End tag mismatch: expected {0} but found {1}", text3, text));
					}
					this.handler.OnEndElement(text);
					this.Expect(62);
					return;
				}
			}
			else
			{
				this.Read();
				if (this.Peek() == 91)
				{
					this.Read();
					if (this.ReadName() != "CDATA")
					{
						throw this.Error("Invalid declaration markup");
					}
					this.Expect(91);
					this.ReadCDATASection();
					return;
				}
				else
				{
					if (this.Peek() == 45)
					{
						this.ReadComment();
						return;
					}
					if (this.ReadName() != "DOCTYPE")
					{
						throw this.Error("Invalid declaration markup.");
					}
					throw this.Error("This parser does not support document type.");
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004E24 File Offset: 0x00003024
		private void HandleBufferedContent()
		{
			if (this.buffer.Length == 0)
			{
				return;
			}
			if (this.isWhitespace)
			{
				this.handler.OnIgnorableWhitespace(this.buffer.ToString());
			}
			else
			{
				this.handler.OnChars(this.buffer.ToString());
			}
			this.buffer.Length = 0;
			this.isWhitespace = false;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004E88 File Offset: 0x00003088
		private void ReadCharacters()
		{
			this.isWhitespace = false;
			for (;;)
			{
				int num = this.Peek();
				if (num == -1)
				{
					break;
				}
				if (num != 38)
				{
					if (num == 60)
					{
						return;
					}
					this.buffer.Append((char)this.Read());
				}
				else
				{
					this.Read();
					this.ReadReference();
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004ED8 File Offset: 0x000030D8
		private void ReadReference()
		{
			if (this.Peek() == 35)
			{
				this.Read();
				this.ReadCharacterReference();
				return;
			}
			string text = this.ReadName();
			this.Expect(59);
			if (text == "amp")
			{
				this.buffer.Append('&');
				return;
			}
			if (text == "quot")
			{
				this.buffer.Append('"');
				return;
			}
			if (text == "apos")
			{
				this.buffer.Append('\'');
				return;
			}
			if (text == "lt")
			{
				this.buffer.Append('<');
				return;
			}
			if (!(text == "gt"))
			{
				throw this.Error("General non-predefined entity reference is not supported in this parser.");
			}
			this.buffer.Append('>');
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004FA8 File Offset: 0x000031A8
		private int ReadCharacterReference()
		{
			int num = 0;
			if (this.Peek() == 120)
			{
				this.Read();
				for (int i = this.Peek(); i >= 0; i = this.Peek())
				{
					if (48 <= i && i <= 57)
					{
						num <<= 4 + i - 48;
					}
					else if (65 <= i && i <= 70)
					{
						num <<= 4 + i - 65 + 10;
					}
					else
					{
						if (97 > i || i > 102)
						{
							break;
						}
						num <<= 4 + i - 97 + 10;
					}
					this.Read();
				}
			}
			else
			{
				int num2 = this.Peek();
				while (num2 >= 0 && 48 <= num2 && num2 <= 57)
				{
					num <<= 4 + num2 - 48;
					this.Read();
					num2 = this.Peek();
				}
			}
			return num;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005064 File Offset: 0x00003264
		private void ReadAttribute(SmallXmlParser.AttrListImpl a)
		{
			this.SkipWhitespaces(true);
			if (this.Peek() == 47 || this.Peek() == 62)
			{
				return;
			}
			string text = this.ReadName();
			this.SkipWhitespaces();
			this.Expect(61);
			this.SkipWhitespaces();
			int num = this.Read();
			string text2;
			if (num != 34)
			{
				if (num != 39)
				{
					throw this.Error("Invalid attribute value markup.");
				}
				text2 = this.ReadUntil('\'', true);
			}
			else
			{
				text2 = this.ReadUntil('"', true);
			}
			if (text == "xml:space")
			{
				this.xmlSpace = text2;
			}
			a.Add(text, text2);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000050FC File Offset: 0x000032FC
		private void ReadCDATASection()
		{
			int num = 0;
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == ']')
				{
					num++;
				}
				else
				{
					if (c == '>' && num > 1)
					{
						for (int i = num; i > 2; i--)
						{
							this.buffer.Append(']');
						}
						return;
					}
					for (int j = 0; j < num; j++)
					{
						this.buffer.Append(']');
					}
					num = 0;
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000517C File Offset: 0x0000337C
		private void ReadComment()
		{
			this.Expect(45);
			this.Expect(45);
			while (this.Read() != 45 || this.Read() != 45)
			{
			}
			if (this.Read() != 62)
			{
				throw this.Error("'--' is not allowed inside comment markup.");
			}
		}

		// Token: 0x04000D14 RID: 3348
		private SmallXmlParser.IContentHandler handler;

		// Token: 0x04000D15 RID: 3349
		private TextReader reader;

		// Token: 0x04000D16 RID: 3350
		private Stack elementNames = new Stack();

		// Token: 0x04000D17 RID: 3351
		private Stack xmlSpaces = new Stack();

		// Token: 0x04000D18 RID: 3352
		private string xmlSpace;

		// Token: 0x04000D19 RID: 3353
		private StringBuilder buffer = new StringBuilder(200);

		// Token: 0x04000D1A RID: 3354
		private char[] nameBuffer = new char[30];

		// Token: 0x04000D1B RID: 3355
		private bool isWhitespace;

		// Token: 0x04000D1C RID: 3356
		private SmallXmlParser.AttrListImpl attributes = new SmallXmlParser.AttrListImpl();

		// Token: 0x04000D1D RID: 3357
		private int line = 1;

		// Token: 0x04000D1E RID: 3358
		private int column;

		// Token: 0x04000D1F RID: 3359
		private bool resetColumn;

		// Token: 0x02000043 RID: 67
		public interface IContentHandler
		{
			// Token: 0x0600011C RID: 284
			void OnStartParsing(SmallXmlParser parser);

			// Token: 0x0600011D RID: 285
			void OnEndParsing(SmallXmlParser parser);

			// Token: 0x0600011E RID: 286
			void OnStartElement(string name, SmallXmlParser.IAttrList attrs);

			// Token: 0x0600011F RID: 287
			void OnEndElement(string name);

			// Token: 0x06000120 RID: 288
			void OnProcessingInstruction(string name, string text);

			// Token: 0x06000121 RID: 289
			void OnChars(string text);

			// Token: 0x06000122 RID: 290
			void OnIgnorableWhitespace(string text);
		}

		// Token: 0x02000044 RID: 68
		public interface IAttrList
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000123 RID: 291
			int Length { get; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000124 RID: 292
			bool IsEmpty { get; }

			// Token: 0x06000125 RID: 293
			string GetName(int i);

			// Token: 0x06000126 RID: 294
			string GetValue(int i);

			// Token: 0x06000127 RID: 295
			string GetValue(string name);

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000128 RID: 296
			string[] Names { get; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000129 RID: 297
			string[] Values { get; }
		}

		// Token: 0x02000045 RID: 69
		private class AttrListImpl : SmallXmlParser.IAttrList
		{
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600012A RID: 298 RVA: 0x000051B8 File Offset: 0x000033B8
			public int Length
			{
				get
				{
					return this.attrNames.Count;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600012B RID: 299 RVA: 0x000051C5 File Offset: 0x000033C5
			public bool IsEmpty
			{
				get
				{
					return this.attrNames.Count == 0;
				}
			}

			// Token: 0x0600012C RID: 300 RVA: 0x000051D5 File Offset: 0x000033D5
			public string GetName(int i)
			{
				return this.attrNames[i];
			}

			// Token: 0x0600012D RID: 301 RVA: 0x000051E3 File Offset: 0x000033E3
			public string GetValue(int i)
			{
				return this.attrValues[i];
			}

			// Token: 0x0600012E RID: 302 RVA: 0x000051F4 File Offset: 0x000033F4
			public string GetValue(string name)
			{
				for (int i = 0; i < this.attrNames.Count; i++)
				{
					if (this.attrNames[i] == name)
					{
						return this.attrValues[i];
					}
				}
				return null;
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600012F RID: 303 RVA: 0x00005239 File Offset: 0x00003439
			public string[] Names
			{
				get
				{
					return this.attrNames.ToArray();
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000130 RID: 304 RVA: 0x00005246 File Offset: 0x00003446
			public string[] Values
			{
				get
				{
					return this.attrValues.ToArray();
				}
			}

			// Token: 0x06000131 RID: 305 RVA: 0x00005253 File Offset: 0x00003453
			internal void Clear()
			{
				this.attrNames.Clear();
				this.attrValues.Clear();
			}

			// Token: 0x06000132 RID: 306 RVA: 0x0000526B File Offset: 0x0000346B
			internal void Add(string name, string value)
			{
				this.attrNames.Add(name);
				this.attrValues.Add(value);
			}

			// Token: 0x06000133 RID: 307 RVA: 0x00005285 File Offset: 0x00003485
			public AttrListImpl()
			{
			}

			// Token: 0x04000D20 RID: 3360
			private List<string> attrNames = new List<string>();

			// Token: 0x04000D21 RID: 3361
			private List<string> attrValues = new List<string>();
		}
	}
}
