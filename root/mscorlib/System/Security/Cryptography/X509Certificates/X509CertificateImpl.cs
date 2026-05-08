using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020004AB RID: 1195
	internal abstract class X509CertificateImpl : IDisposable
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600315B RID: 12635
		public abstract bool IsValid { get; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600315C RID: 12636
		public abstract IntPtr Handle { get; }

		// Token: 0x0600315D RID: 12637
		public abstract IntPtr GetNativeAppleCertificate();

		// Token: 0x0600315E RID: 12638 RVA: 0x000B72CA File Offset: 0x000B54CA
		protected void ThrowIfContextInvalid()
		{
			if (!this.IsValid)
			{
				throw X509Helper.GetInvalidContextException();
			}
		}

		// Token: 0x0600315F RID: 12639
		public abstract X509CertificateImpl Clone();

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06003160 RID: 12640
		public abstract string Issuer { get; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06003161 RID: 12641
		public abstract string Subject { get; }

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06003162 RID: 12642
		public abstract string LegacyIssuer { get; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06003163 RID: 12643
		public abstract string LegacySubject { get; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06003164 RID: 12644
		public abstract byte[] RawData { get; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06003165 RID: 12645
		public abstract DateTime NotAfter { get; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06003166 RID: 12646
		public abstract DateTime NotBefore { get; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06003167 RID: 12647
		public abstract byte[] Thumbprint { get; }

		// Token: 0x06003168 RID: 12648 RVA: 0x000B72DC File Offset: 0x000B54DC
		public sealed override int GetHashCode()
		{
			if (!this.IsValid)
			{
				return 0;
			}
			byte[] thumbprint = this.Thumbprint;
			int num = 0;
			int num2 = 0;
			while (num2 < thumbprint.Length && num2 < 4)
			{
				num = (num << 8) | (int)thumbprint[num2];
				num2++;
			}
			return num;
		}

		// Token: 0x06003169 RID: 12649
		public abstract bool Equals(X509CertificateImpl other, out bool result);

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600316A RID: 12650
		public abstract string KeyAlgorithm { get; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600316B RID: 12651
		public abstract byte[] KeyAlgorithmParameters { get; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600316C RID: 12652
		public abstract byte[] PublicKeyValue { get; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600316D RID: 12653
		public abstract byte[] SerialNumber { get; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600316E RID: 12654
		public abstract bool HasPrivateKey { get; }

		// Token: 0x0600316F RID: 12655
		public abstract RSA GetRSAPrivateKey();

		// Token: 0x06003170 RID: 12656
		public abstract DSA GetDSAPrivateKey();

		// Token: 0x06003171 RID: 12657
		public abstract byte[] Export(X509ContentType contentType, SafePasswordHandle password);

		// Token: 0x06003172 RID: 12658
		public abstract X509CertificateImpl CopyWithPrivateKey(RSA privateKey);

		// Token: 0x06003173 RID: 12659
		public abstract X509Certificate CreateCertificate();

		// Token: 0x06003174 RID: 12660 RVA: 0x000B7318 File Offset: 0x000B5518
		public sealed override bool Equals(object obj)
		{
			X509CertificateImpl x509CertificateImpl = obj as X509CertificateImpl;
			if (x509CertificateImpl == null)
			{
				return false;
			}
			if (!this.IsValid || !x509CertificateImpl.IsValid)
			{
				return false;
			}
			if (!this.Issuer.Equals(x509CertificateImpl.Issuer))
			{
				return false;
			}
			byte[] serialNumber = this.SerialNumber;
			byte[] serialNumber2 = x509CertificateImpl.SerialNumber;
			if (serialNumber.Length != serialNumber2.Length)
			{
				return false;
			}
			for (int i = 0; i < serialNumber.Length; i++)
			{
				if (serialNumber[i] != serialNumber2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000B7389 File Offset: 0x000B5589
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000B7398 File Offset: 0x000B5598
		~X509CertificateImpl()
		{
			this.Dispose(false);
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000025BE File Offset: 0x000007BE
		protected X509CertificateImpl()
		{
		}
	}
}
