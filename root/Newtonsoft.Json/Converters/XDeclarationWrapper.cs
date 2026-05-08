using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E7 RID: 231
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0002CC4A File Offset: 0x0002AE4A
		internal XDeclaration Declaration
		{
			[CompilerGenerated]
			get
			{
				return this.<Declaration>k__BackingField;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002CC52 File Offset: 0x0002AE52
		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0002CC62 File Offset: 0x0002AE62
		public override XmlNodeType NodeType
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002CC66 File Offset: 0x0002AE66
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0002CC73 File Offset: 0x0002AE73
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x0002CC80 File Offset: 0x0002AE80
		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0002CC8E File Offset: 0x0002AE8E
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x0002CC9B File Offset: 0x0002AE9B
		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}

		// Token: 0x040003B2 RID: 946
		[CompilerGenerated]
		private readonly XDeclaration <Declaration>k__BackingField;
	}
}
