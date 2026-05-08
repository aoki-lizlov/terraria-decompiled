using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Xml;

namespace System.Security.Permissions
{
	// Token: 0x0200041B RID: 1051
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PermissionSetAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002C31 RID: 11313 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public PermissionSetAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x0009FE50 File Offset: 0x0009E050
		// (set) Token: 0x06002C33 RID: 11315 RVA: 0x0009FE58 File Offset: 0x0009E058
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				this.file = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x0009FE61 File Offset: 0x0009E061
		// (set) Token: 0x06002C35 RID: 11317 RVA: 0x0009FE69 File Offset: 0x0009E069
		public string Hex
		{
			get
			{
				return this.hex;
			}
			set
			{
				this.hex = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x0009FE72 File Offset: 0x0009E072
		// (set) Token: 0x06002C37 RID: 11319 RVA: 0x0009FE7A File Offset: 0x0009E07A
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x0009FE83 File Offset: 0x0009E083
		// (set) Token: 0x06002C39 RID: 11321 RVA: 0x0009FE8B File Offset: 0x0009E08B
		public bool UnicodeEncoded
		{
			get
			{
				return this.isUnicodeEncoded;
			}
			set
			{
				this.isUnicodeEncoded = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x0009FE94 File Offset: 0x0009E094
		// (set) Token: 0x06002C3B RID: 11323 RVA: 0x0009FE9C File Offset: 0x0009E09C
		public string XML
		{
			get
			{
				return this.xml;
			}
			set
			{
				this.xml = value;
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override IPermission CreatePermission()
		{
			return null;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0009FEA8 File Offset: 0x0009E0A8
		private PermissionSet CreateFromXml(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			try
			{
				securityParser.LoadXml(xml);
			}
			catch (SmallXmlParserException ex)
			{
				throw new XmlSyntaxException(ex.Line, ex.ToString());
			}
			SecurityElement securityElement = securityParser.ToXml();
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				return null;
			}
			PermissionState permissionState = PermissionState.None;
			if (CodeAccessPermission.IsUnrestricted(securityElement))
			{
				permissionState = PermissionState.Unrestricted;
			}
			if (text.EndsWith("NamedPermissionSet"))
			{
				NamedPermissionSet namedPermissionSet = new NamedPermissionSet(securityElement.Attribute("Name"), permissionState);
				namedPermissionSet.FromXml(securityElement);
				return namedPermissionSet;
			}
			if (text.EndsWith("PermissionSet"))
			{
				PermissionSet permissionSet = new PermissionSet(permissionState);
				permissionSet.FromXml(securityElement);
				return permissionSet;
			}
			return null;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x0009FF50 File Offset: 0x0009E150
		public PermissionSet CreatePermissionSet()
		{
			PermissionSet permissionSet = null;
			if (base.Unrestricted)
			{
				permissionSet = new PermissionSet(PermissionState.Unrestricted);
			}
			else
			{
				permissionSet = new PermissionSet(PermissionState.None);
				if (this.name != null)
				{
					return PolicyLevel.CreateAppDomainLevel().GetNamedPermissionSet(this.name);
				}
				if (this.file != null)
				{
					Encoding encoding = (this.isUnicodeEncoded ? Encoding.Unicode : Encoding.ASCII);
					using (StreamReader streamReader = new StreamReader(this.file, encoding))
					{
						return this.CreateFromXml(streamReader.ReadToEnd());
					}
				}
				if (this.xml != null)
				{
					permissionSet = this.CreateFromXml(this.xml);
				}
				else if (this.hex != null)
				{
					Encoding ascii = Encoding.ASCII;
					byte[] array = CryptoConvert.FromHex(this.hex);
					permissionSet = this.CreateFromXml(ascii.GetString(array, 0, array.Length));
				}
			}
			return permissionSet;
		}

		// Token: 0x04001F32 RID: 7986
		private string file;

		// Token: 0x04001F33 RID: 7987
		private string name;

		// Token: 0x04001F34 RID: 7988
		private bool isUnicodeEncoded;

		// Token: 0x04001F35 RID: 7989
		private string xml;

		// Token: 0x04001F36 RID: 7990
		private string hex;
	}
}
