using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Policy
{
	// Token: 0x020003D3 RID: 979
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
	{
		// Token: 0x0600299D RID: 10653 RVA: 0x0009834A File Offset: 0x0009654A
		public ApplicationTrust()
		{
			this.fullTrustAssemblies = new List<StrongName>(0);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x0009835E File Offset: 0x0009655E
		public ApplicationTrust(ApplicationIdentity applicationIdentity)
			: this()
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._appid = applicationIdentity;
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0009837C File Offset: 0x0009657C
		public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
		{
			if (defaultGrantSet == null)
			{
				throw new ArgumentNullException("defaultGrantSet");
			}
			this._defaultPolicy = new PolicyStatement(defaultGrantSet);
			if (fullTrustAssemblies == null)
			{
				throw new ArgumentNullException("fullTrustAssemblies");
			}
			this.fullTrustAssemblies = new List<StrongName>();
			foreach (StrongName strongName in fullTrustAssemblies)
			{
				if (strongName == null)
				{
					throw new ArgumentException("fullTrustAssemblies contains an assembly that does not have a StrongName");
				}
				this.fullTrustAssemblies.Add((StrongName)strongName.Copy());
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x0009841C File Offset: 0x0009661C
		// (set) Token: 0x060029A1 RID: 10657 RVA: 0x00098424 File Offset: 0x00096624
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._appid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationIdentity");
				}
				this._appid = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x0009843B File Offset: 0x0009663B
		// (set) Token: 0x060029A3 RID: 10659 RVA: 0x00098457 File Offset: 0x00096657
		public PolicyStatement DefaultGrantSet
		{
			get
			{
				if (this._defaultPolicy == null)
				{
					this._defaultPolicy = this.GetDefaultGrantSet();
				}
				return this._defaultPolicy;
			}
			set
			{
				this._defaultPolicy = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x00098460 File Offset: 0x00096660
		// (set) Token: 0x060029A5 RID: 10661 RVA: 0x00098468 File Offset: 0x00096668
		public object ExtraInfo
		{
			get
			{
				return this._xtranfo;
			}
			set
			{
				this._xtranfo = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x00098471 File Offset: 0x00096671
		// (set) Token: 0x060029A7 RID: 10663 RVA: 0x00098479 File Offset: 0x00096679
		public bool IsApplicationTrustedToRun
		{
			get
			{
				return this._trustrun;
			}
			set
			{
				this._trustrun = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x00098482 File Offset: 0x00096682
		// (set) Token: 0x060029A9 RID: 10665 RVA: 0x0009848A File Offset: 0x0009668A
		public bool Persist
		{
			get
			{
				return this._persist;
			}
			set
			{
				this._persist = value;
			}
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00098494 File Offset: 0x00096694
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Tag != "ApplicationTrust")
			{
				throw new ArgumentException("element");
			}
			string text = element.Attribute("FullName");
			if (text != null)
			{
				this._appid = new ApplicationIdentity(text);
			}
			else
			{
				this._appid = null;
			}
			this._defaultPolicy = null;
			SecurityElement securityElement = element.SearchForChildByTag("DefaultGrant");
			if (securityElement != null)
			{
				for (int i = 0; i < securityElement.Children.Count; i++)
				{
					SecurityElement securityElement2 = securityElement.Children[i] as SecurityElement;
					if (securityElement2.Tag == "PolicyStatement")
					{
						this.DefaultGrantSet.FromXml(securityElement2, null);
						break;
					}
				}
			}
			if (!bool.TryParse(element.Attribute("TrustedToRun"), out this._trustrun))
			{
				this._trustrun = false;
			}
			if (!bool.TryParse(element.Attribute("Persist"), out this._persist))
			{
				this._persist = false;
			}
			this._xtranfo = null;
			SecurityElement securityElement3 = element.SearchForChildByTag("ExtraInfo");
			if (securityElement3 != null)
			{
				text = securityElement3.Attribute("Data");
				if (text != null)
				{
					using (MemoryStream memoryStream = new MemoryStream(CryptoConvert.FromHex(text)))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						this._xtranfo = binaryFormatter.Deserialize(memoryStream);
					}
				}
			}
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000985F8 File Offset: 0x000967F8
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("ApplicationTrust");
			securityElement.AddAttribute("version", "1");
			if (this._appid != null)
			{
				securityElement.AddAttribute("FullName", this._appid.FullName);
			}
			if (this._trustrun)
			{
				securityElement.AddAttribute("TrustedToRun", "true");
			}
			if (this._persist)
			{
				securityElement.AddAttribute("Persist", "true");
			}
			SecurityElement securityElement2 = new SecurityElement("DefaultGrant");
			securityElement2.AddChild(this.DefaultGrantSet.ToXml());
			securityElement.AddChild(securityElement2);
			if (this._xtranfo != null)
			{
				byte[] array = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					new BinaryFormatter().Serialize(memoryStream, this._xtranfo);
					array = memoryStream.ToArray();
				}
				SecurityElement securityElement3 = new SecurityElement("ExtraInfo");
				securityElement3.AddAttribute("Data", CryptoConvert.ToHex(array));
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x00098700 File Offset: 0x00096900
		public IList<StrongName> FullTrustAssemblies
		{
			get
			{
				return this.fullTrustAssemblies;
			}
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x00098708 File Offset: 0x00096908
		private PolicyStatement GetDefaultGrantSet()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04001E26 RID: 7718
		private ApplicationIdentity _appid;

		// Token: 0x04001E27 RID: 7719
		private PolicyStatement _defaultPolicy;

		// Token: 0x04001E28 RID: 7720
		private object _xtranfo;

		// Token: 0x04001E29 RID: 7721
		private bool _trustrun;

		// Token: 0x04001E2A RID: 7722
		private bool _persist;

		// Token: 0x04001E2B RID: 7723
		private IList<StrongName> fullTrustAssemblies;
	}
}
