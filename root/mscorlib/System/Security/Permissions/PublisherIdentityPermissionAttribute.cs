using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Permissions
{
	// Token: 0x02000420 RID: 1056
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002C6A RID: 11370 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public PublisherIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000A0CAD File Offset: 0x0009EEAD
		// (set) Token: 0x06002C6C RID: 11372 RVA: 0x000A0CB5 File Offset: 0x0009EEB5
		public string CertFile
		{
			get
			{
				return this.certFile;
			}
			set
			{
				this.certFile = value;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000A0CBE File Offset: 0x0009EEBE
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x000A0CC6 File Offset: 0x0009EEC6
		public string SignedFile
		{
			get
			{
				return this.signedFile;
			}
			set
			{
				this.signedFile = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x000A0CCF File Offset: 0x0009EECF
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x000A0CD7 File Offset: 0x0009EED7
		public string X509Certificate
		{
			get
			{
				return this.x509data;
			}
			set
			{
				this.x509data = value;
			}
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000A0CE0 File Offset: 0x0009EEE0
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.x509data != null)
			{
				return new PublisherIdentityPermission(new X509Certificate(CryptoConvert.FromHex(this.x509data)));
			}
			if (this.certFile != null)
			{
				return new PublisherIdentityPermission(global::System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.certFile));
			}
			if (this.signedFile != null)
			{
				return new PublisherIdentityPermission(global::System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.signedFile));
			}
			return new PublisherIdentityPermission(PermissionState.None);
		}

		// Token: 0x04001F41 RID: 8001
		private string certFile;

		// Token: 0x04001F42 RID: 8002
		private string signedFile;

		// Token: 0x04001F43 RID: 8003
		private string x509data;
	}
}
