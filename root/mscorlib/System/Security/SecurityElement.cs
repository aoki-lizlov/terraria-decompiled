using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Xml;

namespace System.Security
{
	// Token: 0x020003B6 RID: 950
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement
	{
		// Token: 0x060028BD RID: 10429 RVA: 0x00094F90 File Offset: 0x00093190
		public SecurityElement(string tag)
			: this(tag, null)
		{
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x00094F9C File Offset: 0x0009319C
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + tag);
			}
			this.tag = tag;
			this.Text = text;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x00094FF0 File Offset: 0x000931F0
		internal SecurityElement(SecurityElement se)
		{
			this.Tag = se.Tag;
			this.Text = se.Text;
			if (se.attributes != null)
			{
				foreach (object obj in se.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					this.AddAttribute(securityAttribute.Name, securityAttribute.Value);
				}
			}
			if (se.children != null)
			{
				foreach (object obj2 in se.children)
				{
					SecurityElement securityElement = (SecurityElement)obj2;
					this.AddChild(securityElement);
				}
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x000950CC File Offset: 0x000932CC
		// (set) Token: 0x060028C1 RID: 10433 RVA: 0x0009514C File Offset: 0x0009334C
		public Hashtable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.attributes.Count);
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					hashtable.Add(securityAttribute.Name, securityAttribute.Value);
				}
				return hashtable;
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this.attributes.Clear();
					return;
				}
				if (this.attributes == null)
				{
					this.attributes = new ArrayList();
				}
				else
				{
					this.attributes.Clear();
				}
				IDictionaryEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.attributes.Add(new SecurityElement.SecurityAttribute((string)enumerator.Key, (string)enumerator.Value));
				}
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000951C8 File Offset: 0x000933C8
		// (set) Token: 0x060028C3 RID: 10435 RVA: 0x000951D0 File Offset: 0x000933D0
		public ArrayList Children
		{
			get
			{
				return this.children;
			}
			set
			{
				if (value != null)
				{
					using (IEnumerator enumerator = value.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == null)
							{
								throw new ArgumentNullException();
							}
						}
					}
				}
				this.children = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x0009522C File Offset: 0x0009342C
		// (set) Token: 0x060028C5 RID: 10437 RVA: 0x00095234 File Offset: 0x00093434
		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Tag");
				}
				if (!SecurityElement.IsValidTag(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.tag = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x0009526E File Offset: 0x0009346E
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x00095276 File Offset: 0x00093476
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value != null && !SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.text = SecurityElement.Unescape(value);
			}
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000952AC File Offset: 0x000934AC
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.GetAttribute(name) != null)
			{
				throw new ArgumentException(Locale.GetText("Duplicate attribute : " + name));
			}
			if (this.attributes == null)
			{
				this.attributes = new ArrayList();
			}
			this.attributes.Add(new SecurityElement.SecurityAttribute(name, value));
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0009531A File Offset: 0x0009351A
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.children == null)
			{
				this.children = new ArrayList();
			}
			this.children.Add(child);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0009534C File Offset: 0x0009354C
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			SecurityElement.SecurityAttribute attribute = this.GetAttribute(name);
			if (attribute != null)
			{
				return attribute.Value;
			}
			return null;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0009537A File Offset: 0x0009357A
		[ComVisible(false)]
		public SecurityElement Copy()
		{
			return new SecurityElement(this);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x00095384 File Offset: 0x00093584
		public bool Equal(SecurityElement other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.text != other.text)
			{
				return false;
			}
			if (this.tag != other.tag)
			{
				return false;
			}
			if (this.attributes == null && other.attributes != null && other.attributes.Count != 0)
			{
				return false;
			}
			if (other.attributes == null && this.attributes != null && this.attributes.Count != 0)
			{
				return false;
			}
			if (this.attributes != null && other.attributes != null)
			{
				if (this.attributes.Count != other.attributes.Count)
				{
					return false;
				}
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					SecurityElement.SecurityAttribute attribute = other.GetAttribute(securityAttribute.Name);
					if (attribute == null || securityAttribute.Value != attribute.Value)
					{
						return false;
					}
				}
			}
			if (this.children == null && other.children != null && other.children.Count != 0)
			{
				return false;
			}
			if (other.children == null && this.children != null && this.children.Count != 0)
			{
				return false;
			}
			if (this.children != null && other.children != null)
			{
				if (this.children.Count != other.children.Count)
				{
					return false;
				}
				for (int i = 0; i < this.children.Count; i++)
				{
					if (!((SecurityElement)this.children[i]).Equal((SecurityElement)other.children[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x00095558 File Offset: 0x00093758
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			if (str.IndexOfAny(SecurityElement.invalid_chars) == -1)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			int i = 0;
			while (i < length)
			{
				char c = str[i];
				if (c <= '&')
				{
					if (c != '"')
					{
						if (c != '&')
						{
							goto IL_0096;
						}
						stringBuilder.Append("&amp;");
					}
					else
					{
						stringBuilder.Append("&quot;");
					}
				}
				else if (c != '\'')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							goto IL_0096;
						}
						stringBuilder.Append("&gt;");
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else
				{
					stringBuilder.Append("&apos;");
				}
				IL_009E:
				i++;
				continue;
				IL_0096:
				stringBuilder.Append(c);
				goto IL_009E;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x00095614 File Offset: 0x00093814
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(str);
			stringBuilder.Replace("&lt;", "<");
			stringBuilder.Replace("&gt;", ">");
			stringBuilder.Replace("&amp;", "&");
			stringBuilder.Replace("&quot;", "\"");
			stringBuilder.Replace("&apos;", "'");
			return stringBuilder.ToString();
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x00095688 File Offset: 0x00093888
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (xml.Length == 0)
			{
				throw new XmlSyntaxException(Locale.GetText("Empty string."));
			}
			SecurityElement securityElement;
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xml);
				securityElement = securityParser.ToXml();
			}
			catch (Exception ex)
			{
				throw new XmlSyntaxException(Locale.GetText("Invalid XML."), ex);
			}
			return securityElement;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000956F4 File Offset: 0x000938F4
		public static bool IsValidAttributeName(string name)
		{
			return name != null && name.IndexOfAny(SecurityElement.invalid_attr_name_chars) == -1;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x00095709 File Offset: 0x00093909
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.invalid_attr_value_chars) == -1;
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x0009571E File Offset: 0x0009391E
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.invalid_tag_chars) == -1;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x00095733 File Offset: 0x00093933
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.invalid_text_chars) == -1;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x00095748 File Offset: 0x00093948
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				SecurityElement securityElement = (SecurityElement)this.children[i];
				if (securityElement.tag == tag)
				{
					return securityElement;
				}
			}
			return null;
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000957A8 File Offset: 0x000939A8
		public string SearchForTextOfTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.tag == tag)
			{
				return this.text;
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				string text = ((SecurityElement)this.children[i]).SearchForTextOfTag(tag);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x00095818 File Offset: 0x00093A18
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToXml(ref stringBuilder, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0009583C File Offset: 0x00093A3C
		private void ToXml(ref StringBuilder s, int level)
		{
			s.Append("<");
			s.Append(this.tag);
			if (this.attributes != null)
			{
				s.Append(" ");
				for (int i = 0; i < this.attributes.Count; i++)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)this.attributes[i];
					s.Append(securityAttribute.Name).Append("=\"").Append(SecurityElement.Escape(securityAttribute.Value))
						.Append("\"");
					if (i != this.attributes.Count - 1)
					{
						s.Append(Environment.NewLine);
					}
				}
			}
			if ((this.text == null || this.text == string.Empty) && (this.children == null || this.children.Count == 0))
			{
				s.Append("/>").Append(Environment.NewLine);
				return;
			}
			s.Append(">").Append(SecurityElement.Escape(this.text));
			if (this.children != null)
			{
				s.Append(Environment.NewLine);
				foreach (object obj in this.children)
				{
					((SecurityElement)obj).ToXml(ref s, level + 1);
				}
			}
			s.Append("</").Append(this.tag).Append(">")
				.Append(Environment.NewLine);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000959E8 File Offset: 0x00093BE8
		internal SecurityElement.SecurityAttribute GetAttribute(string name)
		{
			if (this.attributes != null)
			{
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					if (securityAttribute.Name == name)
					{
						return securityAttribute;
					}
				}
			}
			return null;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x0009522C File Offset: 0x0009342C
		internal string m_strTag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x0009526E File Offset: 0x0009346E
		// (set) Token: 0x060028DB RID: 10459 RVA: 0x00095A58 File Offset: 0x00093C58
		internal string m_strText
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x00095A61 File Offset: 0x00093C61
		internal ArrayList m_lAttributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x000951C8 File Offset: 0x000933C8
		internal ArrayList InternalChildren
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x00095A6C File Offset: 0x00093C6C
		internal string SearchForTextOfLocalName(string strLocalName)
		{
			if (strLocalName == null)
			{
				throw new ArgumentNullException("strLocalName");
			}
			if (this.tag == null)
			{
				return null;
			}
			if (this.tag.Equals(strLocalName) || this.tag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
			{
				return SecurityElement.Unescape(this.text);
			}
			if (this.children == null)
			{
				return null;
			}
			foreach (object obj in this.children)
			{
				string text = ((SecurityElement)obj).SearchForTextOfLocalName(strLocalName);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00095AFC File Offset: 0x00093CFC
		// Note: this type is marked as 'beforefieldinit'.
		static SecurityElement()
		{
		}

		// Token: 0x04001DA1 RID: 7585
		private string text;

		// Token: 0x04001DA2 RID: 7586
		private string tag;

		// Token: 0x04001DA3 RID: 7587
		private ArrayList attributes;

		// Token: 0x04001DA4 RID: 7588
		private ArrayList children;

		// Token: 0x04001DA5 RID: 7589
		private static readonly char[] invalid_tag_chars = new char[] { ' ', '<', '>' };

		// Token: 0x04001DA6 RID: 7590
		private static readonly char[] invalid_text_chars = new char[] { '<', '>' };

		// Token: 0x04001DA7 RID: 7591
		private static readonly char[] invalid_attr_name_chars = new char[] { ' ', '<', '>' };

		// Token: 0x04001DA8 RID: 7592
		private static readonly char[] invalid_attr_value_chars = new char[] { '"', '<', '>' };

		// Token: 0x04001DA9 RID: 7593
		private static readonly char[] invalid_chars = new char[] { '<', '>', '"', '\'', '&' };

		// Token: 0x020003B7 RID: 951
		internal class SecurityAttribute
		{
			// Token: 0x060028E0 RID: 10464 RVA: 0x00095B78 File Offset: 0x00093D78
			public SecurityAttribute(string name, string value)
			{
				if (!SecurityElement.IsValidAttributeName(name))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute name") + ": " + name);
				}
				if (!SecurityElement.IsValidAttributeValue(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute value") + ": " + value);
				}
				this._name = name;
				this._value = SecurityElement.Unescape(value);
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x060028E1 RID: 10465 RVA: 0x00095BE4 File Offset: 0x00093DE4
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x060028E2 RID: 10466 RVA: 0x00095BEC File Offset: 0x00093DEC
			public string Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x04001DAA RID: 7594
			private string _name;

			// Token: 0x04001DAB RID: 7595
			private string _value;
		}
	}
}
