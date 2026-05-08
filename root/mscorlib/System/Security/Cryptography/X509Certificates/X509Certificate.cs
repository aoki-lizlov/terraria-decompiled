using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Internal.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020004AA RID: 1194
	[Serializable]
	public class X509Certificate : IDisposable, IDeserializationCallback, ISerializable
	{
		// Token: 0x06003116 RID: 12566 RVA: 0x000B6804 File Offset: 0x000B4A04
		public virtual void Reset()
		{
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
			this.lazyCertHash = null;
			this.lazyIssuer = null;
			this.lazySubject = null;
			this.lazySerialNumber = null;
			this.lazyKeyAlgorithm = null;
			this.lazyKeyAlgorithmParameters = null;
			this.lazyPublicKey = null;
			this.lazyNotBefore = DateTime.MinValue;
			this.lazyNotAfter = DateTime.MinValue;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000B6880 File Offset: 0x000B4A80
		public X509Certificate()
		{
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000B689E File Offset: 0x000B4A9E
		public X509Certificate(byte[] data)
		{
			if (data != null && data.Length != 0)
			{
				this.impl = X509Helper.Import(data);
			}
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000B68CF File Offset: 0x000B4ACF
		public X509Certificate(byte[] rawData, string password)
			: this(rawData, password, X509KeyStorageFlags.DefaultKeySet)
		{
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000B68DA File Offset: 0x000B4ADA
		[CLSCompliant(false)]
		public X509Certificate(byte[] rawData, SecureString password)
			: this(rawData, password, X509KeyStorageFlags.DefaultKeySet)
		{
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000B68E8 File Offset: 0x000B4AE8
		public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("Array cannot be empty or null.", "rawData");
			}
			X509Certificate.ValidateKeyStorageFlags(keyStorageFlags);
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				this.impl = X509Helper.Import(rawData, safePasswordHandle, keyStorageFlags);
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000B6960 File Offset: 0x000B4B60
		[CLSCompliant(false)]
		public X509Certificate(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("Array cannot be empty or null.", "rawData");
			}
			X509Certificate.ValidateKeyStorageFlags(keyStorageFlags);
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				this.impl = X509Helper.Import(rawData, safePasswordHandle, keyStorageFlags);
			}
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000B69D8 File Offset: 0x000B4BD8
		public X509Certificate(IntPtr handle)
		{
			throw new PlatformNotSupportedException("Initializing `X509Certificate` from native handle is not supported.");
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000B6A00 File Offset: 0x000B4C00
		internal X509Certificate(X509CertificateImpl impl)
		{
			this.impl = X509Helper.InitFromCertificate(impl);
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000B6A2A File Offset: 0x000B4C2A
		public X509Certificate(string fileName)
			: this(fileName, null, X509KeyStorageFlags.DefaultKeySet)
		{
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000B6A35 File Offset: 0x000B4C35
		public X509Certificate(string fileName, string password)
			: this(fileName, password, X509KeyStorageFlags.DefaultKeySet)
		{
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000B6A40 File Offset: 0x000B4C40
		[CLSCompliant(false)]
		public X509Certificate(string fileName, SecureString password)
			: this(fileName, password, X509KeyStorageFlags.DefaultKeySet)
		{
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000B6A4C File Offset: 0x000B4C4C
		public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			X509Certificate.ValidateKeyStorageFlags(keyStorageFlags);
			byte[] array = File.ReadAllBytes(fileName);
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				this.impl = X509Helper.Import(array, safePasswordHandle, keyStorageFlags);
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000B6AC4 File Offset: 0x000B4CC4
		[CLSCompliant(false)]
		public X509Certificate(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
			: this()
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			X509Certificate.ValidateKeyStorageFlags(keyStorageFlags);
			byte[] array = File.ReadAllBytes(fileName);
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				this.impl = X509Helper.Import(array, safePasswordHandle, keyStorageFlags);
			}
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000B6B24 File Offset: 0x000B4D24
		public X509Certificate(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			this.impl = X509Helper.InitFromCertificate(cert);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000B6B5C File Offset: 0x000B4D5C
		public X509Certificate(SerializationInfo info, StreamingContext context)
			: this()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000B6B69 File Offset: 0x000B4D69
		public static X509Certificate CreateFromCertFile(string filename)
		{
			return new X509Certificate(filename);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000B6B69 File Offset: 0x000B4D69
		public static X509Certificate CreateFromSignedFile(string filename)
		{
			return new X509Certificate(filename);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x0003CB93 File Offset: 0x0003AD93
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x0003CB93 File Offset: 0x0003AD93
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000B6B71 File Offset: 0x000B4D71
		public IntPtr Handle
		{
			get
			{
				if (X509Helper.IsValid(this.impl))
				{
					return this.impl.Handle;
				}
				return IntPtr.Zero;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000B6B94 File Offset: 0x000B4D94
		public string Issuer
		{
			get
			{
				this.ThrowIfInvalid();
				string text = this.lazyIssuer;
				if (text == null)
				{
					text = (this.lazyIssuer = this.Impl.Issuer);
				}
				return text;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000B6BCC File Offset: 0x000B4DCC
		public string Subject
		{
			get
			{
				this.ThrowIfInvalid();
				string text = this.lazySubject;
				if (text == null)
				{
					text = (this.lazySubject = this.Impl.Subject);
				}
				return text;
			}
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000B6C03 File Offset: 0x000B4E03
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000B6C0C File Offset: 0x000B4E0C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Reset();
			}
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000B6C18 File Offset: 0x000B4E18
		public override bool Equals(object obj)
		{
			X509Certificate x509Certificate = obj as X509Certificate;
			return x509Certificate != null && this.Equals(x509Certificate);
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000B6C38 File Offset: 0x000B4E38
		public virtual bool Equals(X509Certificate other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.Impl == null)
			{
				return other.Impl == null;
			}
			if (!this.Issuer.Equals(other.Issuer))
			{
				return false;
			}
			byte[] rawSerialNumber = this.GetRawSerialNumber();
			byte[] rawSerialNumber2 = other.GetRawSerialNumber();
			if (rawSerialNumber.Length != rawSerialNumber2.Length)
			{
				return false;
			}
			for (int i = 0; i < rawSerialNumber.Length; i++)
			{
				if (rawSerialNumber[i] != rawSerialNumber2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000B6CA2 File Offset: 0x000B4EA2
		public virtual byte[] Export(X509ContentType contentType)
		{
			return this.Export(contentType, null);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000B6CAC File Offset: 0x000B4EAC
		public virtual byte[] Export(X509ContentType contentType, string password)
		{
			this.VerifyContentType(contentType);
			if (this.Impl == null)
			{
				throw new CryptographicException(-2147467261);
			}
			byte[] array;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				array = this.Impl.Export(contentType, safePasswordHandle);
			}
			return array;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000B6D08 File Offset: 0x000B4F08
		[CLSCompliant(false)]
		public virtual byte[] Export(X509ContentType contentType, SecureString password)
		{
			this.VerifyContentType(contentType);
			if (this.Impl == null)
			{
				throw new CryptographicException(-2147467261);
			}
			byte[] array;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				array = this.Impl.Export(contentType, safePasswordHandle);
			}
			return array;
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000B6D64 File Offset: 0x000B4F64
		public virtual string GetRawCertDataString()
		{
			this.ThrowIfInvalid();
			return this.GetRawCertData().ToHexStringUpper();
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000B6D77 File Offset: 0x000B4F77
		public virtual byte[] GetCertHash()
		{
			this.ThrowIfInvalid();
			return this.GetRawCertHash().CloneByteArray();
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual byte[] GetCertHash(HashAlgorithmName hashAlgorithm)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public virtual bool TryGetCertHash(HashAlgorithmName hashAlgorithm, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000B6D8A File Offset: 0x000B4F8A
		public virtual string GetCertHashString()
		{
			this.ThrowIfInvalid();
			return this.GetRawCertHash().ToHexStringUpper();
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000B6D9D File Offset: 0x000B4F9D
		public virtual string GetCertHashString(HashAlgorithmName hashAlgorithm)
		{
			this.ThrowIfInvalid();
			return this.GetCertHash(hashAlgorithm).ToHexStringUpper();
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000B6DB4 File Offset: 0x000B4FB4
		private byte[] GetRawCertHash()
		{
			byte[] array;
			if ((array = this.lazyCertHash) == null)
			{
				array = (this.lazyCertHash = this.Impl.Thumbprint);
			}
			return array;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000B6DE4 File Offset: 0x000B4FE4
		public virtual string GetEffectiveDateString()
		{
			return this.GetNotBefore().ToString();
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000B6E00 File Offset: 0x000B5000
		public virtual string GetExpirationDateString()
		{
			return this.GetNotAfter().ToString();
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000B6E1B File Offset: 0x000B501B
		public virtual string GetFormat()
		{
			return "X509";
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000B6E22 File Offset: 0x000B5022
		public virtual string GetPublicKeyString()
		{
			return this.GetPublicKey().ToHexStringUpper();
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000B6E2F File Offset: 0x000B502F
		public virtual byte[] GetRawCertData()
		{
			this.ThrowIfInvalid();
			return this.Impl.RawData.CloneByteArray();
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000B6E48 File Offset: 0x000B5048
		public override int GetHashCode()
		{
			if (this.Impl == null)
			{
				return 0;
			}
			byte[] rawCertHash = this.GetRawCertHash();
			int num = 0;
			int num2 = 0;
			while (num2 < rawCertHash.Length && num2 < 4)
			{
				num = (num << 8) | (int)rawCertHash[num2];
				num2++;
			}
			return num;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000B6E84 File Offset: 0x000B5084
		public virtual string GetKeyAlgorithm()
		{
			this.ThrowIfInvalid();
			string text = this.lazyKeyAlgorithm;
			if (text == null)
			{
				text = (this.lazyKeyAlgorithm = this.Impl.KeyAlgorithm);
			}
			return text;
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000B6EBC File Offset: 0x000B50BC
		public virtual byte[] GetKeyAlgorithmParameters()
		{
			this.ThrowIfInvalid();
			byte[] array = this.lazyKeyAlgorithmParameters;
			if (array == null)
			{
				array = (this.lazyKeyAlgorithmParameters = this.Impl.KeyAlgorithmParameters);
			}
			return array.CloneByteArray();
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000B6EF8 File Offset: 0x000B50F8
		public virtual string GetKeyAlgorithmParametersString()
		{
			this.ThrowIfInvalid();
			return this.GetKeyAlgorithmParameters().ToHexStringUpper();
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000B6F0C File Offset: 0x000B510C
		public virtual byte[] GetPublicKey()
		{
			this.ThrowIfInvalid();
			byte[] array = this.lazyPublicKey;
			if (array == null)
			{
				array = (this.lazyPublicKey = this.Impl.PublicKeyValue);
			}
			return array.CloneByteArray();
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000B6F48 File Offset: 0x000B5148
		public virtual byte[] GetSerialNumber()
		{
			this.ThrowIfInvalid();
			byte[] array = this.GetRawSerialNumber().CloneByteArray();
			Array.Reverse<byte>(array);
			return array;
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000B6F61 File Offset: 0x000B5161
		public virtual string GetSerialNumberString()
		{
			this.ThrowIfInvalid();
			return this.GetRawSerialNumber().ToHexStringUpper();
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000B6F74 File Offset: 0x000B5174
		private byte[] GetRawSerialNumber()
		{
			byte[] array;
			if ((array = this.lazySerialNumber) == null)
			{
				array = (this.lazySerialNumber = this.Impl.SerialNumber);
			}
			return array;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000B6FA3 File Offset: 0x000B51A3
		[Obsolete("This method has been deprecated.  Please use the Subject property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual string GetName()
		{
			this.ThrowIfInvalid();
			return this.Impl.LegacySubject;
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000B6FB6 File Offset: 0x000B51B6
		[Obsolete("This method has been deprecated.  Please use the Issuer property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual string GetIssuerName()
		{
			this.ThrowIfInvalid();
			return this.Impl.LegacyIssuer;
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000B6FC9 File Offset: 0x000B51C9
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000B6FD4 File Offset: 0x000B51D4
		public virtual string ToString(bool fVerbose)
		{
			if (!fVerbose || !X509Helper.IsValid(this.impl))
			{
				return base.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[Subject]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(this.Subject);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Issuer]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(this.Issuer);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Serial Number]");
			stringBuilder.Append("  ");
			byte[] serialNumber = this.GetSerialNumber();
			Array.Reverse<byte>(serialNumber);
			stringBuilder.Append(serialNumber.ToHexArrayUpper());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Not Before]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(X509Certificate.FormatDate(this.GetNotBefore()));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Not After]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(X509Certificate.FormatDate(this.GetNotAfter()));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Thumbprint]");
			stringBuilder.Append("  ");
			stringBuilder.Append(this.GetRawCertHash().ToHexArrayUpper());
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000B712D File Offset: 0x000B532D
		[ComVisible(false)]
		public virtual void Import(byte[] rawData)
		{
			throw new PlatformNotSupportedException("X509Certificate is immutable on this platform. Use the equivalent constructor instead.");
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000B712D File Offset: 0x000B532D
		[ComVisible(false)]
		public virtual void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			throw new PlatformNotSupportedException("X509Certificate is immutable on this platform. Use the equivalent constructor instead.");
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000B712D File Offset: 0x000B532D
		public virtual void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			throw new PlatformNotSupportedException("X509Certificate is immutable on this platform. Use the equivalent constructor instead.");
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000B712D File Offset: 0x000B532D
		[ComVisible(false)]
		public virtual void Import(string fileName)
		{
			throw new PlatformNotSupportedException("X509Certificate is immutable on this platform. Use the equivalent constructor instead.");
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000B712D File Offset: 0x000B532D
		[ComVisible(false)]
		public virtual void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			throw new PlatformNotSupportedException("X509Certificate is immutable on this platform. Use the equivalent constructor instead.");
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000B712D File Offset: 0x000B532D
		public virtual void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			throw new PlatformNotSupportedException("X509Certificate is immutable on this platform. Use the equivalent constructor instead.");
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000B713C File Offset: 0x000B533C
		internal DateTime GetNotAfter()
		{
			this.ThrowIfInvalid();
			DateTime dateTime = this.lazyNotAfter;
			if (dateTime == DateTime.MinValue)
			{
				dateTime = (this.lazyNotAfter = this.impl.NotAfter);
			}
			return dateTime;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000B717C File Offset: 0x000B537C
		internal DateTime GetNotBefore()
		{
			this.ThrowIfInvalid();
			DateTime dateTime = this.lazyNotBefore;
			if (dateTime == DateTime.MinValue)
			{
				dateTime = (this.lazyNotBefore = this.impl.NotBefore);
			}
			return dateTime;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000B71BC File Offset: 0x000B53BC
		protected static string FormatDate(DateTime date)
		{
			CultureInfo cultureInfo = CultureInfo.CurrentCulture;
			if (!cultureInfo.DateTimeFormat.Calendar.IsValidDay(date.Year, date.Month, date.Day, 0))
			{
				if (cultureInfo.DateTimeFormat.Calendar is UmAlQuraCalendar)
				{
					cultureInfo = cultureInfo.Clone() as CultureInfo;
					cultureInfo.DateTimeFormat.Calendar = new HijriCalendar();
				}
				else
				{
					cultureInfo = CultureInfo.InvariantCulture;
				}
			}
			return date.ToString(cultureInfo);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000B7238 File Offset: 0x000B5438
		internal static void ValidateKeyStorageFlags(X509KeyStorageFlags keyStorageFlags)
		{
			if ((keyStorageFlags & ~(X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet)) != X509KeyStorageFlags.DefaultKeySet)
			{
				throw new ArgumentException("Value of flags is invalid.", "keyStorageFlags");
			}
			X509KeyStorageFlags x509KeyStorageFlags = keyStorageFlags & (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet);
			if (x509KeyStorageFlags == (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet))
			{
				throw new ArgumentException(SR.Format("The flags '{0}' may not be specified together.", x509KeyStorageFlags), "keyStorageFlags");
			}
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000B7280 File Offset: 0x000B5480
		private void VerifyContentType(X509ContentType contentType)
		{
			if (contentType != X509ContentType.Cert && contentType != X509ContentType.SerializedCert && contentType != X509ContentType.Pfx)
			{
				throw new CryptographicException("Invalid content type.");
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000B7299 File Offset: 0x000B5499
		internal void ImportHandle(X509CertificateImpl impl)
		{
			this.Reset();
			this.impl = impl;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000B72A8 File Offset: 0x000B54A8
		internal X509CertificateImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000B72B0 File Offset: 0x000B54B0
		internal bool IsValid
		{
			get
			{
				return X509Helper.IsValid(this.impl);
			}
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000B72BD File Offset: 0x000B54BD
		internal void ThrowIfInvalid()
		{
			X509Helper.ThrowIfContextInvalid(this.impl);
		}

		// Token: 0x0400221D RID: 8733
		private X509CertificateImpl impl;

		// Token: 0x0400221E RID: 8734
		private volatile byte[] lazyCertHash;

		// Token: 0x0400221F RID: 8735
		private volatile byte[] lazySerialNumber;

		// Token: 0x04002220 RID: 8736
		private volatile string lazyIssuer;

		// Token: 0x04002221 RID: 8737
		private volatile string lazySubject;

		// Token: 0x04002222 RID: 8738
		private volatile string lazyKeyAlgorithm;

		// Token: 0x04002223 RID: 8739
		private volatile byte[] lazyKeyAlgorithmParameters;

		// Token: 0x04002224 RID: 8740
		private volatile byte[] lazyPublicKey;

		// Token: 0x04002225 RID: 8741
		private DateTime lazyNotBefore = DateTime.MinValue;

		// Token: 0x04002226 RID: 8742
		private DateTime lazyNotAfter = DateTime.MinValue;

		// Token: 0x04002227 RID: 8743
		internal const X509KeyStorageFlags KeyStorageFlagsAll = X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet;
	}
}
