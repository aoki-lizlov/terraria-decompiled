using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Xml;

namespace Mono.Security
{
	// Token: 0x02000060 RID: 96
	internal class StrongNameManager
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000B270 File Offset: 0x00009470
		public static void LoadConfig(string filename)
		{
			if (File.Exists(filename))
			{
				SecurityParser securityParser = new SecurityParser();
				using (StreamReader streamReader = new StreamReader(filename))
				{
					string text = streamReader.ReadToEnd();
					securityParser.LoadXml(text);
				}
				SecurityElement securityElement = securityParser.ToXml();
				if (securityElement != null && securityElement.Tag == "configuration")
				{
					SecurityElement securityElement2 = securityElement.SearchForChildByTag("strongNames");
					if (securityElement2 != null && securityElement2.Children.Count > 0)
					{
						SecurityElement securityElement3 = securityElement2.SearchForChildByTag("pubTokenMapping");
						if (securityElement3 != null && securityElement3.Children.Count > 0)
						{
							StrongNameManager.LoadMapping(securityElement3);
						}
						SecurityElement securityElement4 = securityElement2.SearchForChildByTag("verificationSettings");
						if (securityElement4 != null && securityElement4.Children.Count > 0)
						{
							StrongNameManager.LoadVerificationSettings(securityElement4);
						}
					}
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000B350 File Offset: 0x00009550
		private static void LoadMapping(SecurityElement mapping)
		{
			if (StrongNameManager.mappings == null)
			{
				StrongNameManager.mappings = new Hashtable();
			}
			object syncRoot = StrongNameManager.mappings.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in mapping.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (!(securityElement.Tag != "map"))
					{
						string text = securityElement.Attribute("Token");
						if (text != null && text.Length == 16)
						{
							text = text.ToUpper(CultureInfo.InvariantCulture);
							string text2 = securityElement.Attribute("PublicKey");
							if (text2 != null)
							{
								if (StrongNameManager.mappings[text] == null)
								{
									StrongNameManager.mappings.Add(text, text2);
								}
								else
								{
									StrongNameManager.mappings[text] = text2;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000B460 File Offset: 0x00009660
		private static void LoadVerificationSettings(SecurityElement settings)
		{
			if (StrongNameManager.tokens == null)
			{
				StrongNameManager.tokens = new Hashtable();
			}
			object syncRoot = StrongNameManager.tokens.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in settings.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (!(securityElement.Tag != "skip"))
					{
						string text = securityElement.Attribute("Token");
						if (text != null)
						{
							text = text.ToUpper(CultureInfo.InvariantCulture);
							string text2 = securityElement.Attribute("Assembly");
							if (text2 == null)
							{
								text2 = "*";
							}
							string text3 = securityElement.Attribute("Users");
							if (text3 == null)
							{
								text3 = "*";
							}
							StrongNameManager.Element element = (StrongNameManager.Element)StrongNameManager.tokens[text];
							if (element == null)
							{
								element = new StrongNameManager.Element(text2, text3);
								StrongNameManager.tokens.Add(text, element);
							}
							else if ((string)element.assemblies[text2] == null)
							{
								element.assemblies.Add(text2, text3);
							}
							else if (text3 == "*")
							{
								element.assemblies[text2] = "*";
							}
							else
							{
								string text4 = (string)element.assemblies[text2] + "," + text3;
								element.assemblies[text2] = text4;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000B624 File Offset: 0x00009824
		public static byte[] GetMappedPublicKey(byte[] token)
		{
			if (StrongNameManager.mappings == null || token == null)
			{
				return null;
			}
			string text = CryptoConvert.ToHex(token);
			string text2 = (string)StrongNameManager.mappings[text];
			if (text2 == null)
			{
				return null;
			}
			return CryptoConvert.FromHex(text2);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000B660 File Offset: 0x00009860
		public static bool MustVerify(AssemblyName an)
		{
			if (an == null || StrongNameManager.tokens == null)
			{
				return true;
			}
			string text = CryptoConvert.ToHex(an.GetPublicKeyToken());
			StrongNameManager.Element element = (StrongNameManager.Element)StrongNameManager.tokens[text];
			if (element != null)
			{
				string text2 = element.GetUsers(an.Name);
				if (text2 == null)
				{
					text2 = element.GetUsers("*");
				}
				if (text2 != null)
				{
					return !(text2 == "*") && text2.IndexOf(Environment.UserName) < 0;
				}
			}
			return true;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Public Key Token\tAssemblies\t\tUsers");
			stringBuilder.Append(Environment.NewLine);
			foreach (object obj in StrongNameManager.tokens)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				stringBuilder.Append((string)dictionaryEntry.Key);
				StrongNameManager.Element element = (StrongNameManager.Element)dictionaryEntry.Value;
				bool flag = true;
				foreach (object obj2 in element.assemblies)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (flag)
					{
						stringBuilder.Append("\t");
						flag = false;
					}
					else
					{
						stringBuilder.Append("\t\t\t");
					}
					stringBuilder.Append((string)dictionaryEntry2.Key);
					stringBuilder.Append("\t");
					string text = (string)dictionaryEntry2.Value;
					if (text == "*")
					{
						text = "All users";
					}
					stringBuilder.Append(text);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000025BE File Offset: 0x000007BE
		public StrongNameManager()
		{
		}

		// Token: 0x04000DA9 RID: 3497
		private static Hashtable mappings;

		// Token: 0x04000DAA RID: 3498
		private static Hashtable tokens;

		// Token: 0x02000061 RID: 97
		private class Element
		{
			// Token: 0x06000229 RID: 553 RVA: 0x0000B83C File Offset: 0x00009A3C
			public Element()
			{
				this.assemblies = new Hashtable();
			}

			// Token: 0x0600022A RID: 554 RVA: 0x0000B84F File Offset: 0x00009A4F
			public Element(string assembly, string users)
				: this()
			{
				this.assemblies.Add(assembly, users);
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000B864 File Offset: 0x00009A64
			public string GetUsers(string assembly)
			{
				return (string)this.assemblies[assembly];
			}

			// Token: 0x04000DAB RID: 3499
			internal Hashtable assemblies;
		}
	}
}
