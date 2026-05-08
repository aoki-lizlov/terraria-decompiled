using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000DF RID: 223
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x0002C8EA File Offset: 0x0002AAEA
		public XmlDeclarationWrapper(XmlDeclaration declaration)
			: base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002C8FA File Offset: 0x0002AAFA
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002C907 File Offset: 0x0002AB07
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0002C914 File Offset: 0x0002AB14
		public string Encoding
		{
			get
			{
				return this._declaration.Encoding;
			}
			set
			{
				this._declaration.Encoding = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002C922 File Offset: 0x0002AB22
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0002C92F File Offset: 0x0002AB2F
		public string Standalone
		{
			get
			{
				return this._declaration.Standalone;
			}
			set
			{
				this._declaration.Standalone = value;
			}
		}

		// Token: 0x040003AD RID: 941
		private readonly XmlDeclaration _declaration;
	}
}
