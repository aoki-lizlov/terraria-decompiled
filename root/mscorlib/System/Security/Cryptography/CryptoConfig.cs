using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Xml;

namespace System.Security.Cryptography
{
	// Token: 0x02000496 RID: 1174
	[ComVisible(true)]
	public class CryptoConfig
	{
		// Token: 0x0600307A RID: 12410 RVA: 0x000B1B68 File Offset: 0x000AFD68
		public static void AddAlgorithm(Type algorithm, params string[] names)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (!algorithm.IsVisible)
			{
				throw new ArgumentException("Algorithms added to CryptoConfig must be accessable from outside their assembly.", "algorithm");
			}
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			string[] array = new string[names.Length];
			Array.Copy(names, array, array.Length);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (string.IsNullOrEmpty(array2[i]))
				{
					throw new ArgumentException("CryptoConfig cannot add a mapping for a null or empty name.");
				}
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.algorithms == null)
				{
					CryptoConfig.Initialize();
				}
				foreach (string text in array)
				{
					CryptoConfig.algorithms[text] = algorithm;
				}
			}
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000B1C44 File Offset: 0x000AFE44
		public static byte[] EncodeOID(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			char[] array = new char[] { '.' };
			string[] array2 = str.Split(array);
			if (array2.Length < 2)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("OID must have at least two parts"));
			}
			byte[] array3 = new byte[str.Length];
			try
			{
				byte b = Convert.ToByte(array2[0]);
				byte b2 = Convert.ToByte(array2[1]);
				array3[2] = Convert.ToByte((int)(b * 40 + b2));
			}
			catch
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Invalid OID"));
			}
			int num = 3;
			for (int i = 2; i < array2.Length; i++)
			{
				long num2 = Convert.ToInt64(array2[i]);
				if (num2 > 127L)
				{
					byte[] array4 = CryptoConfig.EncodeLongNumber(num2);
					Buffer.BlockCopy(array4, 0, array3, num, array4.Length);
					num += array4.Length;
				}
				else
				{
					array3[num++] = Convert.ToByte(num2);
				}
			}
			int num3 = 2;
			byte[] array5 = new byte[num];
			array5[0] = 6;
			if (num > 127)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("OID > 127 bytes"));
			}
			array5[1] = Convert.ToByte(num - 2);
			Buffer.BlockCopy(array3, num3, array5, num3, num - num3);
			return array5;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000B1D74 File Offset: 0x000AFF74
		private static byte[] EncodeLongNumber(long x)
		{
			if (x > 2147483647L || x < -2147483648L)
			{
				throw new OverflowException(Locale.GetText("Part of OID doesn't fit in Int32"));
			}
			long num = x;
			int num2 = 1;
			while (num > 127L)
			{
				num >>= 7;
				num2++;
			}
			byte[] array = new byte[num2];
			for (int i = 0; i < num2; i++)
			{
				num = x >> 7 * i;
				num &= 127L;
				if (i != 0)
				{
					num += 128L;
				}
				array[num2 - i - 1] = Convert.ToByte(num);
			}
			return array;
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoLimitation("nothing is FIPS certified so it never make sense to restrict to this (empty) subset")]
		public static bool AllowOnlyFipsAlgorithms
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000B1DF4 File Offset: 0x000AFFF4
		private static void Initialize()
		{
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("SHA", CryptoConfig.defaultSHA1);
			dictionary.Add("SHA1", CryptoConfig.defaultSHA1);
			dictionary.Add("System.Security.Cryptography.SHA1", CryptoConfig.defaultSHA1);
			dictionary.Add("System.Security.Cryptography.HashAlgorithm", CryptoConfig.defaultSHA1);
			dictionary.Add("MD5", CryptoConfig.defaultMD5);
			dictionary.Add("System.Security.Cryptography.MD5", CryptoConfig.defaultMD5);
			dictionary.Add("SHA256", CryptoConfig.defaultSHA256);
			dictionary.Add("SHA-256", CryptoConfig.defaultSHA256);
			dictionary.Add("System.Security.Cryptography.SHA256", CryptoConfig.defaultSHA256);
			dictionary.Add("SHA384", CryptoConfig.defaultSHA384);
			dictionary.Add("SHA-384", CryptoConfig.defaultSHA384);
			dictionary.Add("System.Security.Cryptography.SHA384", CryptoConfig.defaultSHA384);
			dictionary.Add("SHA512", CryptoConfig.defaultSHA512);
			dictionary.Add("SHA-512", CryptoConfig.defaultSHA512);
			dictionary.Add("System.Security.Cryptography.SHA512", CryptoConfig.defaultSHA512);
			dictionary.Add("RSA", CryptoConfig.defaultRSA);
			dictionary.Add("System.Security.Cryptography.RSA", CryptoConfig.defaultRSA);
			dictionary.Add("System.Security.Cryptography.AsymmetricAlgorithm", CryptoConfig.defaultRSA);
			dictionary.Add("DSA", CryptoConfig.defaultDSA);
			dictionary.Add("System.Security.Cryptography.DSA", CryptoConfig.defaultDSA);
			dictionary.Add("DES", CryptoConfig.defaultDES);
			dictionary.Add("System.Security.Cryptography.DES", CryptoConfig.defaultDES);
			dictionary.Add("3DES", CryptoConfig.default3DES);
			dictionary.Add("TripleDES", CryptoConfig.default3DES);
			dictionary.Add("Triple DES", CryptoConfig.default3DES);
			dictionary.Add("System.Security.Cryptography.TripleDES", CryptoConfig.default3DES);
			dictionary.Add("RC2", CryptoConfig.defaultRC2);
			dictionary.Add("System.Security.Cryptography.RC2", CryptoConfig.defaultRC2);
			dictionary.Add("Rijndael", CryptoConfig.defaultAES);
			dictionary.Add("System.Security.Cryptography.Rijndael", CryptoConfig.defaultAES);
			dictionary.Add("System.Security.Cryptography.SymmetricAlgorithm", CryptoConfig.defaultAES);
			dictionary.Add("RandomNumberGenerator", CryptoConfig.defaultRNG);
			dictionary.Add("System.Security.Cryptography.RandomNumberGenerator", CryptoConfig.defaultRNG);
			dictionary.Add("System.Security.Cryptography.KeyedHashAlgorithm", CryptoConfig.defaultHMAC);
			dictionary.Add("HMACSHA1", CryptoConfig.defaultHMAC);
			dictionary.Add("System.Security.Cryptography.HMACSHA1", CryptoConfig.defaultHMAC);
			dictionary.Add("MACTripleDES", CryptoConfig.defaultMAC3DES);
			dictionary.Add("System.Security.Cryptography.MACTripleDES", CryptoConfig.defaultMAC3DES);
			dictionary.Add("RIPEMD160", CryptoConfig.defaultRIPEMD160);
			dictionary.Add("RIPEMD-160", CryptoConfig.defaultRIPEMD160);
			dictionary.Add("System.Security.Cryptography.RIPEMD160", CryptoConfig.defaultRIPEMD160);
			dictionary.Add("System.Security.Cryptography.HMAC", CryptoConfig.defaultHMAC);
			dictionary.Add("HMACMD5", CryptoConfig.defaultHMACMD5);
			dictionary.Add("System.Security.Cryptography.HMACMD5", CryptoConfig.defaultHMACMD5);
			dictionary.Add("HMACRIPEMD160", CryptoConfig.defaultHMACRIPEMD160);
			dictionary.Add("System.Security.Cryptography.HMACRIPEMD160", CryptoConfig.defaultHMACRIPEMD160);
			dictionary.Add("HMACSHA256", CryptoConfig.defaultHMACSHA256);
			dictionary.Add("System.Security.Cryptography.HMACSHA256", CryptoConfig.defaultHMACSHA256);
			dictionary.Add("HMACSHA384", CryptoConfig.defaultHMACSHA384);
			dictionary.Add("System.Security.Cryptography.HMACSHA384", CryptoConfig.defaultHMACSHA384);
			dictionary.Add("HMACSHA512", CryptoConfig.defaultHMACSHA512);
			dictionary.Add("System.Security.Cryptography.HMACSHA512", CryptoConfig.defaultHMACSHA512);
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("http://www.w3.org/2000/09/xmldsig#dsa-sha1", CryptoConfig.defaultDSASigDesc);
			dictionary.Add("http://www.w3.org/2000/09/xmldsig#rsa-sha1", CryptoConfig.defaultRSAPKCS1SHA1SigDesc);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", CryptoConfig.defaultRSAPKCS1SHA256SigDesc);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", CryptoConfig.defaultRSAPKCS1SHA384SigDesc);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", CryptoConfig.defaultRSAPKCS1SHA512SigDesc);
			dictionary.Add("http://www.w3.org/2000/09/xmldsig#sha1", CryptoConfig.defaultSHA1);
			dictionary2.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315", "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments", "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig#base64", "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/TR/1999/REC-xpath-19991116", "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/TR/1999/REC-xslt-19991116", "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig#enveloped-signature", "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2001/10/xml-exc-c14n#", "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments", "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2002/07/decrypt#XML", "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary.Add("http://www.w3.org/2001/04/xmlenc#sha256", CryptoConfig.defaultSHA256);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#sha384", CryptoConfig.defaultSHA384);
			dictionary.Add("http://www.w3.org/2001/04/xmlenc#sha512", CryptoConfig.defaultSHA512);
			dictionary.Add("http://www.w3.org/2000/09/xmldsig#hmac-sha1", CryptoConfig.defaultHMAC);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", CryptoConfig.defaultHMACSHA256);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", CryptoConfig.defaultHMACSHA384);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha512", CryptoConfig.defaultHMACSHA512);
			dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160", CryptoConfig.defaultHMACRIPEMD160);
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# X509Data", "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# KeyName", "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue", "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue", "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("http://www.w3.org/2000/09/xmldsig# RetrievalMethod", "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			dictionary2.Add("2.5.29.14", "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("2.5.29.15", "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("2.5.29.19", "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("2.5.29.37", "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("X509Chain", "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("AES", "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.AesCryptoServiceProvider", "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("AesManaged", "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.AesManaged", "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDH", "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDiffieHellman", "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDiffieHellmanCng", "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.ECDiffieHellmanCng", "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDsa", "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("ECDsaCng", "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.ECDsaCng", "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA1Cng", "System.Security.Cryptography.SHA1Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA256Cng", "System.Security.Cryptography.SHA256Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA256CryptoServiceProvider", "System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA384Cng", "System.Security.Cryptography.SHA384Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA384CryptoServiceProvider", "System.Security.Cryptography.SHA384CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA512Cng", "System.Security.Cryptography.SHA512Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			dictionary2.Add("System.Security.Cryptography.SHA512CryptoServiceProvider", "System.Security.Cryptography.SHA512CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			dictionary3.Add("System.Security.Cryptography.SHA1CryptoServiceProvider", "1.3.14.3.2.26");
			dictionary3.Add("System.Security.Cryptography.SHA1Managed", "1.3.14.3.2.26");
			dictionary3.Add("SHA1", "1.3.14.3.2.26");
			dictionary3.Add("System.Security.Cryptography.SHA1", "1.3.14.3.2.26");
			dictionary3.Add("System.Security.Cryptography.SHA1Cng", "1.3.14.3.2.26");
			dictionary3.Add("System.Security.Cryptography.MD5CryptoServiceProvider", "1.2.840.113549.2.5");
			dictionary3.Add("MD5", "1.2.840.113549.2.5");
			dictionary3.Add("System.Security.Cryptography.MD5", "1.2.840.113549.2.5");
			dictionary3.Add("System.Security.Cryptography.SHA256Managed", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("SHA256", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("System.Security.Cryptography.SHA256", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("System.Security.Cryptography.SHA256Cng", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("System.Security.Cryptography.SHA256CryptoServiceProvider", "2.16.840.1.101.3.4.2.1");
			dictionary3.Add("System.Security.Cryptography.SHA384Managed", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("SHA384", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("System.Security.Cryptography.SHA384", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("System.Security.Cryptography.SHA384Cng", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("System.Security.Cryptography.SHA384CryptoServiceProvider", "2.16.840.1.101.3.4.2.2");
			dictionary3.Add("System.Security.Cryptography.SHA512Managed", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("SHA512", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("System.Security.Cryptography.SHA512", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("System.Security.Cryptography.SHA512Cng", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("System.Security.Cryptography.SHA512CryptoServiceProvider", "2.16.840.1.101.3.4.2.3");
			dictionary3.Add("System.Security.Cryptography.RIPEMD160Managed", "1.3.36.3.2.1");
			dictionary3.Add("RIPEMD160", "1.3.36.3.2.1");
			dictionary3.Add("System.Security.Cryptography.RIPEMD160", "1.3.36.3.2.1");
			dictionary3.Add("TripleDESKeyWrap", "1.2.840.113549.1.9.16.3.6");
			dictionary3.Add("DES", "1.3.14.3.2.7");
			dictionary3.Add("TripleDES", "1.2.840.113549.3.7");
			dictionary3.Add("RC2", "1.2.840.113549.3.2");
			CryptoConfig.LoadConfig(Environment.GetMachineConfigPath(), dictionary, dictionary3);
			CryptoConfig.algorithms = dictionary;
			CryptoConfig.unresolved_algorithms = dictionary2;
			CryptoConfig.oids = dictionary3;
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000B2690 File Offset: 0x000B0890
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void LoadConfig(string filename, IDictionary<string, Type> algorithms, IDictionary<string, string> oid)
		{
			if (!File.Exists(filename))
			{
				return;
			}
			try
			{
				using (TextReader textReader = new StreamReader(filename))
				{
					CryptoConfig.CryptoHandler cryptoHandler = new CryptoConfig.CryptoHandler(algorithms, oid);
					new SmallXmlParser().Parse(textReader, cryptoHandler);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000B26F0 File Offset: 0x000B08F0
		public static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name, null);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000B26FC File Offset: 0x000B08FC
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public static object CreateFromName(string name, params object[] args)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.algorithms == null)
				{
					CryptoConfig.Initialize();
				}
			}
			try
			{
				Type type = null;
				if (!CryptoConfig.algorithms.TryGetValue(name, out type))
				{
					string text = null;
					if (!CryptoConfig.unresolved_algorithms.TryGetValue(name, out text))
					{
						text = name;
					}
					type = Type.GetType(text);
				}
				if (type == null)
				{
					obj = null;
				}
				else
				{
					obj = Activator.CreateInstance(type, args);
				}
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000B27A4 File Offset: 0x000B09A4
		public static string MapNameToOID(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.oids == null)
				{
					CryptoConfig.Initialize();
				}
			}
			string text = null;
			CryptoConfig.oids.TryGetValue(name, out text);
			return text;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000B2808 File Offset: 0x000B0A08
		public static void AddOID(string oid, params string[] names)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			foreach (string text in names)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ArithmeticException("names");
				}
				CryptoConfig.oids[oid] = text;
			}
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000025BE File Offset: 0x000007BE
		public CryptoConfig()
		{
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000B2864 File Offset: 0x000B0A64
		// Note: this type is marked as 'beforefieldinit'.
		static CryptoConfig()
		{
		}

		// Token: 0x0400210F RID: 8463
		private static readonly object lockObject = new object();

		// Token: 0x04002110 RID: 8464
		private static Dictionary<string, Type> algorithms;

		// Token: 0x04002111 RID: 8465
		private static Dictionary<string, string> unresolved_algorithms;

		// Token: 0x04002112 RID: 8466
		private static Dictionary<string, string> oids;

		// Token: 0x04002113 RID: 8467
		private const string defaultNamespace = "System.Security.Cryptography.";

		// Token: 0x04002114 RID: 8468
		private static Type defaultSHA1 = typeof(SHA1CryptoServiceProvider);

		// Token: 0x04002115 RID: 8469
		private static Type defaultMD5 = typeof(MD5CryptoServiceProvider);

		// Token: 0x04002116 RID: 8470
		private static Type defaultSHA256 = typeof(SHA256Managed);

		// Token: 0x04002117 RID: 8471
		private static Type defaultSHA384 = typeof(SHA384Managed);

		// Token: 0x04002118 RID: 8472
		private static Type defaultSHA512 = typeof(SHA512Managed);

		// Token: 0x04002119 RID: 8473
		private static Type defaultRSA = typeof(RSACryptoServiceProvider);

		// Token: 0x0400211A RID: 8474
		private static Type defaultDSA = typeof(DSACryptoServiceProvider);

		// Token: 0x0400211B RID: 8475
		private static Type defaultDES = typeof(DESCryptoServiceProvider);

		// Token: 0x0400211C RID: 8476
		private static Type default3DES = typeof(TripleDESCryptoServiceProvider);

		// Token: 0x0400211D RID: 8477
		private static Type defaultRC2 = typeof(RC2CryptoServiceProvider);

		// Token: 0x0400211E RID: 8478
		private static Type defaultAES = typeof(RijndaelManaged);

		// Token: 0x0400211F RID: 8479
		private static Type defaultRNG = typeof(RNGCryptoServiceProvider);

		// Token: 0x04002120 RID: 8480
		private static Type defaultHMAC = typeof(HMACSHA1);

		// Token: 0x04002121 RID: 8481
		private static Type defaultMAC3DES = typeof(MACTripleDES);

		// Token: 0x04002122 RID: 8482
		private static Type defaultDSASigDesc = typeof(DSASignatureDescription);

		// Token: 0x04002123 RID: 8483
		private static Type defaultRSAPKCS1SHA1SigDesc = typeof(RSAPKCS1SHA1SignatureDescription);

		// Token: 0x04002124 RID: 8484
		private static Type defaultRSAPKCS1SHA256SigDesc = typeof(RSAPKCS1SHA256SignatureDescription);

		// Token: 0x04002125 RID: 8485
		private static Type defaultRSAPKCS1SHA384SigDesc = typeof(RSAPKCS1SHA384SignatureDescription);

		// Token: 0x04002126 RID: 8486
		private static Type defaultRSAPKCS1SHA512SigDesc = typeof(RSAPKCS1SHA512SignatureDescription);

		// Token: 0x04002127 RID: 8487
		private static Type defaultRIPEMD160 = typeof(RIPEMD160Managed);

		// Token: 0x04002128 RID: 8488
		private static Type defaultHMACMD5 = typeof(HMACMD5);

		// Token: 0x04002129 RID: 8489
		private static Type defaultHMACRIPEMD160 = typeof(HMACRIPEMD160);

		// Token: 0x0400212A RID: 8490
		private static Type defaultHMACSHA256 = typeof(HMACSHA256);

		// Token: 0x0400212B RID: 8491
		private static Type defaultHMACSHA384 = typeof(HMACSHA384);

		// Token: 0x0400212C RID: 8492
		private static Type defaultHMACSHA512 = typeof(HMACSHA512);

		// Token: 0x0400212D RID: 8493
		private const string defaultC14N = "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400212E RID: 8494
		private const string defaultC14NWithComments = "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400212F RID: 8495
		private const string defaultBase64 = "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002130 RID: 8496
		private const string defaultXPath = "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002131 RID: 8497
		private const string defaultXslt = "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002132 RID: 8498
		private const string defaultEnveloped = "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002133 RID: 8499
		private const string defaultXmlDecryption = "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002134 RID: 8500
		private const string defaultExcC14N = "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002135 RID: 8501
		private const string defaultExcC14NWithComments = "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002136 RID: 8502
		private const string defaultX509Data = "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002137 RID: 8503
		private const string defaultKeyName = "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002138 RID: 8504
		private const string defaultKeyValueDSA = "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04002139 RID: 8505
		private const string defaultKeyValueRSA = "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400213A RID: 8506
		private const string defaultRetrievalMethod = "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400213B RID: 8507
		private const string managedSHA1 = "System.Security.Cryptography.SHA1Managed";

		// Token: 0x0400213C RID: 8508
		private const string oidSHA1 = "1.3.14.3.2.26";

		// Token: 0x0400213D RID: 8509
		private const string oidMD5 = "1.2.840.113549.2.5";

		// Token: 0x0400213E RID: 8510
		private const string oidSHA256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x0400213F RID: 8511
		private const string oidSHA384 = "2.16.840.1.101.3.4.2.2";

		// Token: 0x04002140 RID: 8512
		private const string oidSHA512 = "2.16.840.1.101.3.4.2.3";

		// Token: 0x04002141 RID: 8513
		private const string oidRIPEMD160 = "1.3.36.3.2.1";

		// Token: 0x04002142 RID: 8514
		private const string oidDES = "1.3.14.3.2.7";

		// Token: 0x04002143 RID: 8515
		private const string oid3DES = "1.2.840.113549.3.7";

		// Token: 0x04002144 RID: 8516
		private const string oidRC2 = "1.2.840.113549.3.2";

		// Token: 0x04002145 RID: 8517
		private const string oid3DESKeyWrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x04002146 RID: 8518
		private const string nameSHA1 = "System.Security.Cryptography.SHA1CryptoServiceProvider";

		// Token: 0x04002147 RID: 8519
		private const string nameSHA1a = "SHA";

		// Token: 0x04002148 RID: 8520
		private const string nameSHA1b = "SHA1";

		// Token: 0x04002149 RID: 8521
		private const string nameSHA1c = "System.Security.Cryptography.SHA1";

		// Token: 0x0400214A RID: 8522
		private const string nameSHA1d = "System.Security.Cryptography.HashAlgorithm";

		// Token: 0x0400214B RID: 8523
		private const string nameMD5 = "System.Security.Cryptography.MD5CryptoServiceProvider";

		// Token: 0x0400214C RID: 8524
		private const string nameMD5a = "MD5";

		// Token: 0x0400214D RID: 8525
		private const string nameMD5b = "System.Security.Cryptography.MD5";

		// Token: 0x0400214E RID: 8526
		private const string nameSHA256 = "System.Security.Cryptography.SHA256Managed";

		// Token: 0x0400214F RID: 8527
		private const string nameSHA256a = "SHA256";

		// Token: 0x04002150 RID: 8528
		private const string nameSHA256b = "SHA-256";

		// Token: 0x04002151 RID: 8529
		private const string nameSHA256c = "System.Security.Cryptography.SHA256";

		// Token: 0x04002152 RID: 8530
		private const string nameSHA384 = "System.Security.Cryptography.SHA384Managed";

		// Token: 0x04002153 RID: 8531
		private const string nameSHA384a = "SHA384";

		// Token: 0x04002154 RID: 8532
		private const string nameSHA384b = "SHA-384";

		// Token: 0x04002155 RID: 8533
		private const string nameSHA384c = "System.Security.Cryptography.SHA384";

		// Token: 0x04002156 RID: 8534
		private const string nameSHA512 = "System.Security.Cryptography.SHA512Managed";

		// Token: 0x04002157 RID: 8535
		private const string nameSHA512a = "SHA512";

		// Token: 0x04002158 RID: 8536
		private const string nameSHA512b = "SHA-512";

		// Token: 0x04002159 RID: 8537
		private const string nameSHA512c = "System.Security.Cryptography.SHA512";

		// Token: 0x0400215A RID: 8538
		private const string nameRSAa = "RSA";

		// Token: 0x0400215B RID: 8539
		private const string nameRSAb = "System.Security.Cryptography.RSA";

		// Token: 0x0400215C RID: 8540
		private const string nameRSAc = "System.Security.Cryptography.AsymmetricAlgorithm";

		// Token: 0x0400215D RID: 8541
		private const string nameDSAa = "DSA";

		// Token: 0x0400215E RID: 8542
		private const string nameDSAb = "System.Security.Cryptography.DSA";

		// Token: 0x0400215F RID: 8543
		private const string nameDESa = "DES";

		// Token: 0x04002160 RID: 8544
		private const string nameDESb = "System.Security.Cryptography.DES";

		// Token: 0x04002161 RID: 8545
		private const string name3DESa = "3DES";

		// Token: 0x04002162 RID: 8546
		private const string name3DESb = "TripleDES";

		// Token: 0x04002163 RID: 8547
		private const string name3DESc = "Triple DES";

		// Token: 0x04002164 RID: 8548
		private const string name3DESd = "System.Security.Cryptography.TripleDES";

		// Token: 0x04002165 RID: 8549
		private const string nameRC2a = "RC2";

		// Token: 0x04002166 RID: 8550
		private const string nameRC2b = "System.Security.Cryptography.RC2";

		// Token: 0x04002167 RID: 8551
		private const string nameAESa = "Rijndael";

		// Token: 0x04002168 RID: 8552
		private const string nameAESb = "System.Security.Cryptography.Rijndael";

		// Token: 0x04002169 RID: 8553
		private const string nameAESc = "System.Security.Cryptography.SymmetricAlgorithm";

		// Token: 0x0400216A RID: 8554
		private const string nameRNGa = "RandomNumberGenerator";

		// Token: 0x0400216B RID: 8555
		private const string nameRNGb = "System.Security.Cryptography.RandomNumberGenerator";

		// Token: 0x0400216C RID: 8556
		private const string nameKeyHasha = "System.Security.Cryptography.KeyedHashAlgorithm";

		// Token: 0x0400216D RID: 8557
		private const string nameHMACSHA1a = "HMACSHA1";

		// Token: 0x0400216E RID: 8558
		private const string nameHMACSHA1b = "System.Security.Cryptography.HMACSHA1";

		// Token: 0x0400216F RID: 8559
		private const string nameMAC3DESa = "MACTripleDES";

		// Token: 0x04002170 RID: 8560
		private const string nameMAC3DESb = "System.Security.Cryptography.MACTripleDES";

		// Token: 0x04002171 RID: 8561
		private const string name3DESKeyWrap = "TripleDESKeyWrap";

		// Token: 0x04002172 RID: 8562
		private const string nameRIPEMD160 = "System.Security.Cryptography.RIPEMD160Managed";

		// Token: 0x04002173 RID: 8563
		private const string nameRIPEMD160a = "RIPEMD160";

		// Token: 0x04002174 RID: 8564
		private const string nameRIPEMD160b = "RIPEMD-160";

		// Token: 0x04002175 RID: 8565
		private const string nameRIPEMD160c = "System.Security.Cryptography.RIPEMD160";

		// Token: 0x04002176 RID: 8566
		private const string nameHMACb = "System.Security.Cryptography.HMAC";

		// Token: 0x04002177 RID: 8567
		private const string nameHMACMD5a = "HMACMD5";

		// Token: 0x04002178 RID: 8568
		private const string nameHMACMD5b = "System.Security.Cryptography.HMACMD5";

		// Token: 0x04002179 RID: 8569
		private const string nameHMACRIPEMD160a = "HMACRIPEMD160";

		// Token: 0x0400217A RID: 8570
		private const string nameHMACRIPEMD160b = "System.Security.Cryptography.HMACRIPEMD160";

		// Token: 0x0400217B RID: 8571
		private const string nameHMACSHA256a = "HMACSHA256";

		// Token: 0x0400217C RID: 8572
		private const string nameHMACSHA256b = "System.Security.Cryptography.HMACSHA256";

		// Token: 0x0400217D RID: 8573
		private const string nameHMACSHA384a = "HMACSHA384";

		// Token: 0x0400217E RID: 8574
		private const string nameHMACSHA384b = "System.Security.Cryptography.HMACSHA384";

		// Token: 0x0400217F RID: 8575
		private const string nameHMACSHA512a = "HMACSHA512";

		// Token: 0x04002180 RID: 8576
		private const string nameHMACSHA512b = "System.Security.Cryptography.HMACSHA512";

		// Token: 0x04002181 RID: 8577
		private const string urlXmlDsig = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04002182 RID: 8578
		private const string urlDSASHA1 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x04002183 RID: 8579
		private const string urlRSASHA1 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x04002184 RID: 8580
		private const string urlRSASHA256 = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x04002185 RID: 8581
		private const string urlRSASHA384 = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384";

		// Token: 0x04002186 RID: 8582
		private const string urlRSASHA512 = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512";

		// Token: 0x04002187 RID: 8583
		private const string urlSHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x04002188 RID: 8584
		private const string urlC14N = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		// Token: 0x04002189 RID: 8585
		private const string urlC14NWithComments = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		// Token: 0x0400218A RID: 8586
		private const string urlBase64 = "http://www.w3.org/2000/09/xmldsig#base64";

		// Token: 0x0400218B RID: 8587
		private const string urlXPath = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		// Token: 0x0400218C RID: 8588
		private const string urlXslt = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		// Token: 0x0400218D RID: 8589
		private const string urlEnveloped = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x0400218E RID: 8590
		private const string urlXmlDecryption = "http://www.w3.org/2002/07/decrypt#XML";

		// Token: 0x0400218F RID: 8591
		private const string urlExcC14NWithComments = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x04002190 RID: 8592
		private const string urlExcC14N = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x04002191 RID: 8593
		private const string urlSHA256 = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x04002192 RID: 8594
		private const string urlSHA384 = "http://www.w3.org/2001/04/xmldsig-more#sha384";

		// Token: 0x04002193 RID: 8595
		private const string urlSHA512 = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x04002194 RID: 8596
		private const string urlHMACSHA1 = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		// Token: 0x04002195 RID: 8597
		private const string urlHMACSHA256 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x04002196 RID: 8598
		private const string urlHMACSHA384 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";

		// Token: 0x04002197 RID: 8599
		private const string urlHMACSHA512 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

		// Token: 0x04002198 RID: 8600
		private const string urlHMACRIPEMD160 = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";

		// Token: 0x04002199 RID: 8601
		private const string urlX509Data = "http://www.w3.org/2000/09/xmldsig# X509Data";

		// Token: 0x0400219A RID: 8602
		private const string urlKeyName = "http://www.w3.org/2000/09/xmldsig# KeyName";

		// Token: 0x0400219B RID: 8603
		private const string urlKeyValueDSA = "http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue";

		// Token: 0x0400219C RID: 8604
		private const string urlKeyValueRSA = "http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue";

		// Token: 0x0400219D RID: 8605
		private const string urlRetrievalMethod = "http://www.w3.org/2000/09/xmldsig# RetrievalMethod";

		// Token: 0x0400219E RID: 8606
		private const string oidX509SubjectKeyIdentifier = "2.5.29.14";

		// Token: 0x0400219F RID: 8607
		private const string oidX509KeyUsage = "2.5.29.15";

		// Token: 0x040021A0 RID: 8608
		private const string oidX509BasicConstraints = "2.5.29.19";

		// Token: 0x040021A1 RID: 8609
		private const string oidX509EnhancedKeyUsage = "2.5.29.37";

		// Token: 0x040021A2 RID: 8610
		private const string nameX509SubjectKeyIdentifier = "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021A3 RID: 8611
		private const string nameX509KeyUsage = "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021A4 RID: 8612
		private const string nameX509BasicConstraints = "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021A5 RID: 8613
		private const string nameX509EnhancedKeyUsage = "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021A6 RID: 8614
		private const string nameX509Chain = "X509Chain";

		// Token: 0x040021A7 RID: 8615
		private const string defaultX509Chain = "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021A8 RID: 8616
		private const string system_core_assembly = ", System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021A9 RID: 8617
		private const string nameAES_1 = "AES";

		// Token: 0x040021AA RID: 8618
		private const string nameAES_2 = "System.Security.Cryptography.AesCryptoServiceProvider";

		// Token: 0x040021AB RID: 8619
		private const string defaultAES_1 = "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021AC RID: 8620
		private const string nameAESManaged_1 = "AesManaged";

		// Token: 0x040021AD RID: 8621
		private const string nameAESManaged_2 = "System.Security.Cryptography.AesManaged";

		// Token: 0x040021AE RID: 8622
		private const string defaultAESManaged = "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021AF RID: 8623
		private const string nameECDiffieHellman_1 = "ECDH";

		// Token: 0x040021B0 RID: 8624
		private const string nameECDiffieHellman_2 = "ECDiffieHellman";

		// Token: 0x040021B1 RID: 8625
		private const string nameECDiffieHellman_3 = "ECDiffieHellmanCng";

		// Token: 0x040021B2 RID: 8626
		private const string nameECDiffieHellman_4 = "System.Security.Cryptography.ECDiffieHellmanCng";

		// Token: 0x040021B3 RID: 8627
		private const string defaultECDiffieHellman = "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021B4 RID: 8628
		private const string nameECDsa_1 = "ECDsa";

		// Token: 0x040021B5 RID: 8629
		private const string nameECDsa_2 = "ECDsaCng";

		// Token: 0x040021B6 RID: 8630
		private const string nameECDsa_3 = "System.Security.Cryptography.ECDsaCng";

		// Token: 0x040021B7 RID: 8631
		private const string defaultECDsa = "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021B8 RID: 8632
		private const string nameSHA1Cng = "System.Security.Cryptography.SHA1Cng";

		// Token: 0x040021B9 RID: 8633
		private const string defaultSHA1Cng = "System.Security.Cryptography.SHA1Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021BA RID: 8634
		private const string nameSHA256Cng = "System.Security.Cryptography.SHA256Cng";

		// Token: 0x040021BB RID: 8635
		private const string defaultSHA256Cng = "System.Security.Cryptography.SHA256Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021BC RID: 8636
		private const string nameSHA256Provider = "System.Security.Cryptography.SHA256CryptoServiceProvider";

		// Token: 0x040021BD RID: 8637
		private const string defaultSHA256Provider = "System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021BE RID: 8638
		private const string nameSHA384Cng = "System.Security.Cryptography.SHA384Cng";

		// Token: 0x040021BF RID: 8639
		private const string defaultSHA384Cng = "System.Security.Cryptography.SHA384Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021C0 RID: 8640
		private const string nameSHA384Provider = "System.Security.Cryptography.SHA384CryptoServiceProvider";

		// Token: 0x040021C1 RID: 8641
		private const string defaultSHA384Provider = "System.Security.Cryptography.SHA384CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021C2 RID: 8642
		private const string nameSHA512Cng = "System.Security.Cryptography.SHA512Cng";

		// Token: 0x040021C3 RID: 8643
		private const string defaultSHA512Cng = "System.Security.Cryptography.SHA512Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040021C4 RID: 8644
		private const string nameSHA512Provider = "System.Security.Cryptography.SHA512CryptoServiceProvider";

		// Token: 0x040021C5 RID: 8645
		private const string defaultSHA512Provider = "System.Security.Cryptography.SHA512CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x02000497 RID: 1175
		private class CryptoHandler : SmallXmlParser.IContentHandler
		{
			// Token: 0x06003086 RID: 12422 RVA: 0x000B29F2 File Offset: 0x000B0BF2
			public CryptoHandler(IDictionary<string, Type> algorithms, IDictionary<string, string> oid)
			{
				this.algorithms = algorithms;
				this.oid = oid;
				this.names = new Dictionary<string, string>();
				this.classnames = new Dictionary<string, string>();
			}

			// Token: 0x06003087 RID: 12423 RVA: 0x00004088 File Offset: 0x00002288
			public void OnStartParsing(SmallXmlParser parser)
			{
			}

			// Token: 0x06003088 RID: 12424 RVA: 0x000B2A20 File Offset: 0x000B0C20
			public void OnEndParsing(SmallXmlParser parser)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.names)
				{
					try
					{
						this.algorithms[keyValuePair.Key] = Type.GetType(this.classnames[keyValuePair.Value]);
					}
					catch
					{
					}
				}
				this.names.Clear();
				this.classnames.Clear();
			}

			// Token: 0x06003089 RID: 12425 RVA: 0x000B2ABC File Offset: 0x000B0CBC
			private string Get(SmallXmlParser.IAttrList attrs, string name)
			{
				for (int i = 0; i < attrs.Names.Length; i++)
				{
					if (attrs.Names[i] == name)
					{
						return attrs.Values[i];
					}
				}
				return string.Empty;
			}

			// Token: 0x0600308A RID: 12426 RVA: 0x000B2AFC File Offset: 0x000B0CFC
			public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
			{
				switch (this.level)
				{
				case 0:
					if (name == "configuration")
					{
						this.level++;
						return;
					}
					break;
				case 1:
					if (name == "mscorlib")
					{
						this.level++;
						return;
					}
					break;
				case 2:
					if (name == "cryptographySettings")
					{
						this.level++;
						return;
					}
					break;
				case 3:
					if (name == "oidMap")
					{
						this.level++;
						return;
					}
					if (name == "cryptoNameMapping")
					{
						this.level++;
						return;
					}
					break;
				case 4:
					if (name == "oidEntry")
					{
						this.oid[this.Get(attrs, "name")] = this.Get(attrs, "OID");
						return;
					}
					if (name == "nameEntry")
					{
						this.names[this.Get(attrs, "name")] = this.Get(attrs, "class");
						return;
					}
					if (name == "cryptoClasses")
					{
						this.level++;
						return;
					}
					break;
				case 5:
					if (name == "cryptoClass")
					{
						this.classnames[attrs.Names[0]] = attrs.Values[0];
					}
					break;
				default:
					return;
				}
			}

			// Token: 0x0600308B RID: 12427 RVA: 0x000B2C70 File Offset: 0x000B0E70
			public void OnEndElement(string name)
			{
				switch (this.level)
				{
				case 1:
					if (name == "configuration")
					{
						this.level--;
						return;
					}
					break;
				case 2:
					if (name == "mscorlib")
					{
						this.level--;
						return;
					}
					break;
				case 3:
					if (name == "cryptographySettings")
					{
						this.level--;
						return;
					}
					break;
				case 4:
					if (name == "oidMap" || name == "cryptoNameMapping")
					{
						this.level--;
						return;
					}
					break;
				case 5:
					if (name == "cryptoClasses")
					{
						this.level--;
					}
					break;
				default:
					return;
				}
			}

			// Token: 0x0600308C RID: 12428 RVA: 0x00004088 File Offset: 0x00002288
			public void OnProcessingInstruction(string name, string text)
			{
			}

			// Token: 0x0600308D RID: 12429 RVA: 0x00004088 File Offset: 0x00002288
			public void OnChars(string text)
			{
			}

			// Token: 0x0600308E RID: 12430 RVA: 0x00004088 File Offset: 0x00002288
			public void OnIgnorableWhitespace(string text)
			{
			}

			// Token: 0x040021C6 RID: 8646
			private IDictionary<string, Type> algorithms;

			// Token: 0x040021C7 RID: 8647
			private IDictionary<string, string> oid;

			// Token: 0x040021C8 RID: 8648
			private Dictionary<string, string> names;

			// Token: 0x040021C9 RID: 8649
			private Dictionary<string, string> classnames;

			// Token: 0x040021CA RID: 8650
			private int level;
		}
	}
}
