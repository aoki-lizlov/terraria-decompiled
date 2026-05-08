using System;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Mono;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020004AC RID: 1196
	internal static class X509Helper
	{
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x000B73C8 File Offset: 0x000B55C8
		private static ISystemCertificateProvider CertificateProvider
		{
			get
			{
				return DependencyInjector.SystemProvider.CertificateProvider;
			}
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000B73D4 File Offset: 0x000B55D4
		public static X509CertificateImpl InitFromCertificate(X509Certificate cert)
		{
			return X509Helper.CertificateProvider.Import(cert, CertificateImportFlags.None);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000B73E2 File Offset: 0x000B55E2
		public static X509CertificateImpl InitFromCertificate(X509CertificateImpl impl)
		{
			if (impl == null)
			{
				return null;
			}
			return impl.Clone();
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000B73EF File Offset: 0x000B55EF
		public static bool IsValid(X509CertificateImpl impl)
		{
			return impl != null && impl.IsValid;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000B73FC File Offset: 0x000B55FC
		internal static void ThrowIfContextInvalid(X509CertificateImpl impl)
		{
			if (!X509Helper.IsValid(impl))
			{
				throw X509Helper.GetInvalidContextException();
			}
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000B740C File Offset: 0x000B560C
		internal static Exception GetInvalidContextException()
		{
			return new CryptographicException(Locale.GetText("Certificate instance is empty."));
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000B741D File Offset: 0x000B561D
		public static X509CertificateImpl Import(byte[] rawData)
		{
			return X509Helper.CertificateProvider.Import(rawData, CertificateImportFlags.None);
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000B742B File Offset: 0x000B562B
		public static X509CertificateImpl Import(byte[] rawData, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			return X509Helper.CertificateProvider.Import(rawData, password, keyStorageFlags, CertificateImportFlags.None);
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000B743B File Offset: 0x000B563B
		public static byte[] Export(X509CertificateImpl impl, X509ContentType contentType, SafePasswordHandle password)
		{
			X509Helper.ThrowIfContextInvalid(impl);
			return impl.Export(contentType, password);
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000B744C File Offset: 0x000B564C
		public static bool Equals(X509CertificateImpl first, X509CertificateImpl second)
		{
			if (!X509Helper.IsValid(first) || !X509Helper.IsValid(second))
			{
				return false;
			}
			bool flag;
			if (first.Equals(second, out flag))
			{
				return flag;
			}
			byte[] rawData = first.RawData;
			byte[] rawData2 = second.RawData;
			if (rawData == null)
			{
				return rawData2 == null;
			}
			if (rawData2 == null)
			{
				return false;
			}
			if (rawData.Length != rawData2.Length)
			{
				return false;
			}
			for (int i = 0; i < rawData.Length; i++)
			{
				if (rawData[i] != rawData2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000B74B8 File Offset: 0x000B56B8
		public static string ToHexString(byte[] data)
		{
			if (data != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					stringBuilder.Append(data[i].ToString("X2"));
				}
				return stringBuilder.ToString();
			}
			return null;
		}
	}
}
