using System;
using System.Collections;
using System.IO;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x02000040 RID: 64
	internal class SecurityParser : SmallXmlParser, SmallXmlParser.IContentHandler
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00004615 File Offset: 0x00002815
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004628 File Offset: 0x00002828
		public void LoadXml(string xml)
		{
			this.root = null;
			this.stack.Clear();
			base.Parse(new StringReader(xml), this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004649 File Offset: 0x00002849
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004088 File Offset: 0x00002288
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004088 File Offset: 0x00002288
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004088 File Offset: 0x00002288
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004654 File Offset: 0x00002854
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			SecurityElement securityElement = new SecurityElement(name);
			if (this.root == null)
			{
				this.root = securityElement;
				this.current = securityElement;
			}
			else
			{
				((SecurityElement)this.stack.Peek()).AddChild(securityElement);
			}
			this.stack.Push(securityElement);
			this.current = securityElement;
			int length = attrs.Length;
			for (int i = 0; i < length; i++)
			{
				this.current.AddAttribute(attrs.GetName(i), SecurityElement.Escape(attrs.GetValue(i)));
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000046DA File Offset: 0x000028DA
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000046F2 File Offset: 0x000028F2
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004088 File Offset: 0x00002288
		public void OnEndParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x04000D11 RID: 3345
		private SecurityElement root;

		// Token: 0x04000D12 RID: 3346
		private SecurityElement current;

		// Token: 0x04000D13 RID: 3347
		private Stack stack;
	}
}
