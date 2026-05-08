using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Permissions
{
	// Token: 0x0200041F RID: 1055
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002C5E RID: 11358 RVA: 0x0009ED64 File Offset: 0x0009CF64
		public PublisherIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000A0A85 File Offset: 0x0009EC85
		public PublisherIdentityPermission(X509Certificate certificate)
		{
			this.Certificate = certificate;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000A0A94 File Offset: 0x0009EC94
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x000A0A9C File Offset: 0x0009EC9C
		public X509Certificate Certificate
		{
			get
			{
				return this.x509;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("X509Certificate");
				}
				this.x509 = value;
			}
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000A0AB4 File Offset: 0x0009ECB4
		public override IPermission Copy()
		{
			PublisherIdentityPermission publisherIdentityPermission = new PublisherIdentityPermission(PermissionState.None);
			if (this.x509 != null)
			{
				publisherIdentityPermission.Certificate = this.x509;
			}
			return publisherIdentityPermission;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000A0AE0 File Offset: 0x0009ECE0
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attributes["X509v3Certificate"] as string;
			if (text != null)
			{
				byte[] array = CryptoConvert.FromHex(text);
				this.x509 = new X509Certificate(array);
			}
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000A0B28 File Offset: 0x0009ED28
		public override IPermission Intersect(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			if (publisherIdentityPermission == null)
			{
				return null;
			}
			if (this.x509 != null && publisherIdentityPermission.x509 != null && this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString())
			{
				return new PublisherIdentityPermission(publisherIdentityPermission.x509);
			}
			return null;
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000A0B7C File Offset: 0x0009ED7C
		public override bool IsSubsetOf(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			return publisherIdentityPermission != null && (this.x509 == null || (publisherIdentityPermission.x509 != null && this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString()));
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000A0BC8 File Offset: 0x0009EDC8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.x509 != null)
			{
				securityElement.AddAttribute("X509v3Certificate", this.x509.GetRawCertDataString());
			}
			return securityElement;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000A0BFC File Offset: 0x0009EDFC
		public override IPermission Union(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			if (publisherIdentityPermission == null)
			{
				return this.Copy();
			}
			if (this.x509 != null && publisherIdentityPermission.x509 != null)
			{
				if (this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString())
				{
					return new PublisherIdentityPermission(this.x509);
				}
			}
			else
			{
				if (this.x509 == null && publisherIdentityPermission.x509 != null)
				{
					return new PublisherIdentityPermission(publisherIdentityPermission.x509);
				}
				if (this.x509 != null && publisherIdentityPermission.x509 == null)
				{
					return new PublisherIdentityPermission(this.x509);
				}
			}
			return null;
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x00048CF2 File Offset: 0x00046EF2
		int IBuiltInPermission.GetTokenIndex()
		{
			return 10;
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000A0C8D File Offset: 0x0009EE8D
		private PublisherIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PublisherIdentityPermission publisherIdentityPermission = target as PublisherIdentityPermission;
			if (publisherIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(PublisherIdentityPermission));
			}
			return publisherIdentityPermission;
		}

		// Token: 0x04001F3F RID: 7999
		private const int version = 1;

		// Token: 0x04001F40 RID: 8000
		private X509Certificate x509;
	}
}
