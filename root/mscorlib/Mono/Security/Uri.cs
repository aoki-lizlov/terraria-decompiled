using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Security
{
	// Token: 0x02000063 RID: 99
	internal class Uri
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000B877 File Offset: 0x00009A77
		public Uri(string uriString)
			: this(uriString, false)
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B884 File Offset: 0x00009A84
		public Uri(string uriString, bool dontEscape)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.reduce = true;
			base..ctor();
			this.userEscaped = dontEscape;
			this.source = uriString;
			this.Parse();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B8FC File Offset: 0x00009AFC
		public Uri(string uriString, bool dontEscape, bool reduce)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.reduce = true;
			base..ctor();
			this.userEscaped = dontEscape;
			this.source = uriString;
			this.reduce = reduce;
			this.Parse();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B97A File Offset: 0x00009B7A
		public Uri(Uri baseUri, string relativeUri)
			: this(baseUri, relativeUri, false)
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B988 File Offset: 0x00009B88
		public Uri(Uri baseUri, string relativeUri, bool dontEscape)
		{
			this.scheme = string.Empty;
			this.host = string.Empty;
			this.port = -1;
			this.path = string.Empty;
			this.query = string.Empty;
			this.fragment = string.Empty;
			this.userinfo = string.Empty;
			this.reduce = true;
			base..ctor();
			if (baseUri == null)
			{
				throw new NullReferenceException("baseUri");
			}
			this.userEscaped = dontEscape;
			if (relativeUri == null)
			{
				throw new NullReferenceException("relativeUri");
			}
			if (relativeUri.StartsWith("\\\\"))
			{
				this.source = relativeUri;
				this.Parse();
				return;
			}
			int num = relativeUri.IndexOf(':');
			if (num != -1)
			{
				int num2 = relativeUri.IndexOfAny(new char[] { '/', '\\', '?' });
				if (num2 > num || num2 < 0)
				{
					this.source = relativeUri;
					this.Parse();
					return;
				}
			}
			this.scheme = baseUri.scheme;
			this.host = baseUri.host;
			this.port = baseUri.port;
			this.userinfo = baseUri.userinfo;
			this.isUnc = baseUri.isUnc;
			this.isUnixFilePath = baseUri.isUnixFilePath;
			this.isOpaquePart = baseUri.isOpaquePart;
			if (relativeUri == string.Empty)
			{
				this.path = baseUri.path;
				this.query = baseUri.query;
				this.fragment = baseUri.fragment;
				return;
			}
			num = relativeUri.IndexOf('#');
			if (num != -1)
			{
				this.fragment = relativeUri.Substring(num);
				relativeUri = relativeUri.Substring(0, num);
			}
			num = relativeUri.IndexOf('?');
			if (num != -1)
			{
				this.query = relativeUri.Substring(num);
				if (!this.userEscaped)
				{
					this.query = Uri.EscapeString(this.query);
				}
				relativeUri = relativeUri.Substring(0, num);
			}
			if (relativeUri.Length > 0 && relativeUri[0] == '/')
			{
				if (relativeUri.Length > 1 && relativeUri[1] == '/')
				{
					this.source = this.scheme + ":" + relativeUri;
					this.Parse();
					return;
				}
				this.path = relativeUri;
				if (!this.userEscaped)
				{
					this.path = Uri.EscapeString(this.path);
				}
				return;
			}
			else
			{
				this.path = baseUri.path;
				if (relativeUri.Length > 0 || this.query.Length > 0)
				{
					num = this.path.LastIndexOf('/');
					if (num >= 0)
					{
						this.path = this.path.Substring(0, num + 1);
					}
				}
				if (relativeUri.Length == 0)
				{
					return;
				}
				this.path += relativeUri;
				int num3 = 0;
				for (;;)
				{
					num = this.path.IndexOf("./", num3);
					if (num == -1)
					{
						break;
					}
					if (num == 0)
					{
						this.path = this.path.Remove(0, 2);
					}
					else if (this.path[num - 1] != '.')
					{
						this.path = this.path.Remove(num, 2);
					}
					else
					{
						num3 = num + 1;
					}
				}
				if (this.path.Length > 1 && this.path[this.path.Length - 1] == '.' && this.path[this.path.Length - 2] == '/')
				{
					this.path = this.path.Remove(this.path.Length - 1, 1);
				}
				num3 = 0;
				for (;;)
				{
					num = this.path.IndexOf("/../", num3);
					if (num == -1)
					{
						break;
					}
					if (num == 0)
					{
						num3 = 3;
					}
					else
					{
						int num4 = this.path.LastIndexOf('/', num - 1);
						if (num4 == -1)
						{
							num3 = num + 1;
						}
						else if (this.path.Substring(num4 + 1, num - num4 - 1) != "..")
						{
							this.path = this.path.Remove(num4 + 1, num - num4 + 3);
						}
						else
						{
							num3 = num + 1;
						}
					}
				}
				if (this.path.Length > 3 && this.path.EndsWith("/.."))
				{
					num = this.path.LastIndexOf('/', this.path.Length - 4);
					if (num != -1 && this.path.Substring(num + 1, this.path.Length - num - 4) != "..")
					{
						this.path = this.path.Remove(num + 1, this.path.Length - num - 1);
					}
				}
				if (!this.userEscaped)
				{
					this.path = Uri.EscapeString(this.path);
				}
				return;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000BDF7 File Offset: 0x00009FF7
		public string AbsolutePath
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000BDFF File Offset: 0x00009FFF
		public string AbsoluteUri
		{
			get
			{
				if (this.cachedAbsoluteUri == null)
				{
					this.cachedAbsoluteUri = this.GetLeftPart(UriPartial.Path) + this.query + this.fragment;
				}
				return this.cachedAbsoluteUri;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000BE2D File Offset: 0x0000A02D
		public string Authority
		{
			get
			{
				if (Uri.GetDefaultPort(this.scheme) != this.port)
				{
					return this.host + ":" + this.port.ToString();
				}
				return this.host;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000BE64 File Offset: 0x0000A064
		public string Fragment
		{
			get
			{
				return this.fragment;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000BE74 File Offset: 0x0000A074
		public bool IsDefaultPort
		{
			get
			{
				return Uri.GetDefaultPort(this.scheme) == this.port;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000BE89 File Offset: 0x0000A089
		public bool IsFile
		{
			get
			{
				return this.scheme == Uri.UriSchemeFile;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000BE9B File Offset: 0x0000A09B
		public bool IsLoopback
		{
			get
			{
				return !(this.host == string.Empty) && (this.host == "loopback" || this.host == "localhost");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		public bool IsUnc
		{
			get
			{
				return this.isUnc;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		public string LocalPath
		{
			get
			{
				if (this.cachedLocalPath != null)
				{
					return this.cachedLocalPath;
				}
				if (!this.IsFile)
				{
					return this.AbsolutePath;
				}
				bool flag = this.path.Length > 3 && this.path[1] == ':' && (this.path[2] == '\\' || this.path[2] == '/');
				if (!this.IsUnc)
				{
					string text = this.Unescape(this.path);
					if (Path.DirectorySeparatorChar == '\\' || flag)
					{
						this.cachedLocalPath = text.Replace('/', '\\');
					}
					else
					{
						this.cachedLocalPath = text;
					}
				}
				else if (this.path.Length > 1 && this.path[1] == ':')
				{
					this.cachedLocalPath = this.Unescape(this.path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));
				}
				else if (Path.DirectorySeparatorChar == '\\')
				{
					this.cachedLocalPath = "\\\\" + this.Unescape(this.host + this.path.Replace('/', '\\'));
				}
				else
				{
					this.cachedLocalPath = this.Unescape(this.path);
				}
				if (this.cachedLocalPath == string.Empty)
				{
					this.cachedLocalPath = Path.DirectorySeparatorChar.ToString();
				}
				return this.cachedLocalPath;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000C049 File Offset: 0x0000A249
		public string PathAndQuery
		{
			get
			{
				return this.path + this.query;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000C05C File Offset: 0x0000A25C
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000C064 File Offset: 0x0000A264
		public string Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000C06C File Offset: 0x0000A26C
		public string Scheme
		{
			get
			{
				return this.scheme;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000C074 File Offset: 0x0000A274
		public string[] Segments
		{
			get
			{
				if (this.segments != null)
				{
					return this.segments;
				}
				if (this.path.Length == 0)
				{
					this.segments = new string[0];
					return this.segments;
				}
				string[] array = this.path.Split('/', StringSplitOptions.None);
				this.segments = array;
				bool flag = this.path.EndsWith("/");
				if (array.Length != 0 && flag)
				{
					string[] array2 = new string[array.Length - 1];
					Array.Copy(array, 0, array2, 0, array.Length - 1);
					array = array2;
				}
				int i = 0;
				if (this.IsFile && this.path.Length > 1 && this.path[1] == ':')
				{
					string[] array3 = new string[array.Length + 1];
					Array.Copy(array, 1, array3, 2, array.Length - 1);
					array = array3;
					array[0] = this.path.Substring(0, 2);
					array[1] = string.Empty;
					i++;
				}
				int num = array.Length;
				while (i < num)
				{
					if (i != num - 1 || flag)
					{
						string[] array4 = array;
						int num2 = i;
						array4[num2] += "/";
					}
					i++;
				}
				this.segments = array;
				return this.segments;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000C19C File Offset: 0x0000A39C
		public bool UserEscaped
		{
			get
			{
				return this.userEscaped;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		public string UserInfo
		{
			get
			{
				return this.userinfo;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		internal static bool IsIPv4Address(string name)
		{
			string[] array = name.Split(new char[] { '.' });
			if (array.Length != 4)
			{
				return false;
			}
			for (int i = 0; i < 4; i++)
			{
				try
				{
					int num = int.Parse(array[i], CultureInfo.InvariantCulture);
					if (num < 0 || num > 255)
					{
						return false;
					}
				}
				catch (Exception)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C218 File Offset: 0x0000A418
		internal static bool IsDomainAddress(string name)
		{
			int length = name.Length;
			if (name[length - 1] == '.')
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = name[i];
				if (num == 0)
				{
					if (!char.IsLetterOrDigit(c))
					{
						return false;
					}
				}
				else if (c == '.')
				{
					num = 0;
				}
				else if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					return false;
				}
				if (++num == 64)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C288 File Offset: 0x0000A488
		public static bool CheckSchemeName(string schemeName)
		{
			if (schemeName == null || schemeName.Length == 0)
			{
				return false;
			}
			if (!char.IsLetter(schemeName[0]))
			{
				return false;
			}
			int length = schemeName.Length;
			for (int i = 1; i < length; i++)
			{
				char c = schemeName[i];
				if (!char.IsLetterOrDigit(c) && c != '.' && c != '+' && c != '-')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		public override bool Equals(object comparant)
		{
			if (comparant == null)
			{
				return false;
			}
			Uri uri = comparant as Uri;
			if (uri == null)
			{
				string text = comparant as string;
				if (text == null)
				{
					return false;
				}
				uri = new Uri(text);
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			return this.scheme.ToLower(invariantCulture) == uri.scheme.ToLower(invariantCulture) && this.userinfo.ToLower(invariantCulture) == uri.userinfo.ToLower(invariantCulture) && this.host.ToLower(invariantCulture) == uri.host.ToLower(invariantCulture) && this.port == uri.port && this.path == uri.path && this.query.ToLower(invariantCulture) == uri.query.ToLower(invariantCulture);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		public override int GetHashCode()
		{
			if (this.cachedHashCode == 0)
			{
				this.cachedHashCode = this.scheme.GetHashCode() + this.userinfo.GetHashCode() + this.host.GetHashCode() + this.port + this.path.GetHashCode() + this.query.GetHashCode();
			}
			return this.cachedHashCode;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C420 File Offset: 0x0000A620
		public string GetLeftPart(UriPartial part)
		{
			switch (part)
			{
			case UriPartial.Scheme:
				return this.scheme + this.GetOpaqueWiseSchemeDelimiter();
			case UriPartial.Authority:
			{
				if (this.host == string.Empty || this.scheme == Uri.UriSchemeMailto || this.scheme == Uri.UriSchemeNews)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.scheme);
				stringBuilder.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder.Append(this.userinfo).Append('@');
				}
				stringBuilder.Append(this.host);
				int num = Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != num)
				{
					stringBuilder.Append(':').Append(this.port);
				}
				return stringBuilder.ToString();
			}
			case UriPartial.Path:
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(this.scheme);
				stringBuilder2.Append(this.GetOpaqueWiseSchemeDelimiter());
				if (this.path.Length > 1 && this.path[1] == ':' && Uri.UriSchemeFile == this.scheme)
				{
					stringBuilder2.Append('/');
				}
				if (this.userinfo.Length > 0)
				{
					stringBuilder2.Append(this.userinfo).Append('@');
				}
				stringBuilder2.Append(this.host);
				int num = Uri.GetDefaultPort(this.scheme);
				if (this.port != -1 && this.port != num)
				{
					stringBuilder2.Append(':').Append(this.port);
				}
				stringBuilder2.Append(this.path);
				return stringBuilder2.ToString();
			}
			default:
				return null;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C622 File Offset: 0x0000A822
		public static int FromHex(char digit)
		{
			if ('0' <= digit && digit <= '9')
			{
				return (int)(digit - '0');
			}
			if ('a' <= digit && digit <= 'f')
			{
				return (int)(digit - 'a' + '\n');
			}
			if ('A' <= digit && digit <= 'F')
			{
				return (int)(digit - 'A' + '\n');
			}
			throw new ArgumentException("digit");
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C664 File Offset: 0x0000A864
		public static string HexEscape(char character)
		{
			if (character > 'ÿ')
			{
				throw new ArgumentOutOfRangeException("character");
			}
			return "%" + Uri.hexUpperChars[(int)((character & 'ð') >> 4)].ToString() + Uri.hexUpperChars[(int)(character & '\u000f')].ToString();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		public static char HexUnescape(string pattern, ref int index)
		{
			if (pattern == null)
			{
				throw new ArgumentException("pattern");
			}
			if (index < 0 || index >= pattern.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = 0;
			int num2 = 0;
			while (index + 3 <= pattern.Length && pattern[index] == '%' && Uri.IsHexDigit(pattern[index + 1]) && Uri.IsHexDigit(pattern[index + 2]))
			{
				index++;
				int num3 = index;
				index = num3 + 1;
				int num4 = Uri.FromHex(pattern[num3]);
				num3 = index;
				index = num3 + 1;
				int num5 = Uri.FromHex(pattern[num3]);
				int num6 = (num4 << 4) + num5;
				if (num == 0)
				{
					if (num6 < 192)
					{
						return (char)num6;
					}
					if (num6 < 224)
					{
						num2 = num6 - 192;
						num = 2;
					}
					else if (num6 < 240)
					{
						num2 = num6 - 224;
						num = 3;
					}
					else if (num6 < 248)
					{
						num2 = num6 - 240;
						num = 4;
					}
					else if (num6 < 251)
					{
						num2 = num6 - 248;
						num = 5;
					}
					else if (num6 < 254)
					{
						num2 = num6 - 252;
						num = 6;
					}
					num2 <<= (num - 1) * 6;
				}
				else
				{
					num2 += num6 - 128 << (num - 1) * 6;
				}
				num--;
				if (num <= 0)
				{
					IL_0154:
					return (char)num2;
				}
			}
			if (num == 0)
			{
				int num3 = index;
				index = num3 + 1;
				return pattern[num3];
			}
			goto IL_0154;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C823 File Offset: 0x0000AA23
		public static bool IsHexDigit(char digit)
		{
			return ('0' <= digit && digit <= '9') || ('a' <= digit && digit <= 'f') || ('A' <= digit && digit <= 'F');
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000C84A File Offset: 0x0000AA4A
		public static bool IsHexEncoding(string pattern, int index)
		{
			return index + 3 <= pattern.Length && (pattern[index++] == '%' && Uri.IsHexDigit(pattern[index++])) && Uri.IsHexDigit(pattern[index]);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000C88C File Offset: 0x0000AA8C
		public string MakeRelative(Uri toUri)
		{
			if (this.Scheme != toUri.Scheme || this.Authority != toUri.Authority)
			{
				return toUri.ToString();
			}
			if (this.path == toUri.path)
			{
				return string.Empty;
			}
			string[] array = this.Segments;
			string[] array2 = toUri.Segments;
			int num = 0;
			int num2 = Math.Min(array.Length, array2.Length);
			while (num < num2 && !(array[num] != array2[num]))
			{
				num++;
			}
			string text = string.Empty;
			for (int i = num + 1; i < array.Length; i++)
			{
				text += "../";
			}
			for (int j = num; j < array2.Length; j++)
			{
				text += array2[j];
			}
			return text;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public override string ToString()
		{
			if (this.cachedToString != null)
			{
				return this.cachedToString;
			}
			string text = (this.query.StartsWith("?") ? ("?" + this.Unescape(this.query.Substring(1))) : this.Unescape(this.query));
			this.cachedToString = this.Unescape(this.GetLeftPart(UriPartial.Path), true) + text + this.fragment;
			return this.cachedToString;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000C9DB File Offset: 0x0000ABDB
		protected void Escape()
		{
			this.path = Uri.EscapeString(this.path);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C9EE File Offset: 0x0000ABEE
		protected static string EscapeString(string str)
		{
			return Uri.EscapeString(str, false, true, true);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		internal static string EscapeString(string str, bool escapeReserved, bool escapeHex, bool escapeBrackets)
		{
			if (str == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				if (Uri.IsHexEncoding(str, i))
				{
					stringBuilder.Append(str.Substring(i, 3));
					i += 2;
				}
				else
				{
					byte[] bytes = Encoding.UTF8.GetBytes(new char[] { str[i] });
					int num = bytes.Length;
					for (int j = 0; j < num; j++)
					{
						char c = (char)bytes[j];
						if (c <= ' ' || c >= '\u007f' || "<>%\"{}|\\^`".IndexOf(c) != -1 || (escapeHex && c == '#') || (escapeBrackets && (c == '[' || c == ']')) || (escapeReserved && ";/?:@&=+$,".IndexOf(c) != -1))
						{
							stringBuilder.Append(Uri.HexEscape(c));
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000CAED File Offset: 0x0000ACED
		protected void Parse()
		{
			this.Parse(this.source);
			if (this.userEscaped)
			{
				return;
			}
			this.host = Uri.EscapeString(this.host, false, true, false);
			this.path = Uri.EscapeString(this.path);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000CB29 File Offset: 0x0000AD29
		protected string Unescape(string str)
		{
			return this.Unescape(str, false);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000CB34 File Offset: 0x0000AD34
		internal string Unescape(string str, bool excludeSharp)
		{
			if (str == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if (c == '%')
				{
					char c2 = Uri.HexUnescape(str, ref i);
					if (excludeSharp && c2 == '#')
					{
						stringBuilder.Append("%23");
					}
					else
					{
						stringBuilder.Append(c2);
					}
					i--;
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		private void ParseAsWindowsUNC(string uriString)
		{
			this.scheme = Uri.UriSchemeFile;
			this.port = -1;
			this.fragment = string.Empty;
			this.query = string.Empty;
			this.isUnc = true;
			uriString = uriString.TrimStart(new char[] { '\\' });
			int num = uriString.IndexOf('\\');
			if (num > 0)
			{
				this.path = uriString.Substring(num);
				this.host = uriString.Substring(0, num);
			}
			else
			{
				this.host = uriString;
				this.path = string.Empty;
			}
			this.path = this.path.Replace("\\", "/");
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CC58 File Offset: 0x0000AE58
		private void ParseAsWindowsAbsoluteFilePath(string uriString)
		{
			if (uriString.Length > 2 && uriString[2] != '\\' && uriString[2] != '/')
			{
				throw new FormatException("Relative file path is not allowed.");
			}
			this.scheme = Uri.UriSchemeFile;
			this.host = string.Empty;
			this.port = -1;
			this.path = uriString.Replace("\\", "/");
			this.fragment = string.Empty;
			this.query = string.Empty;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		private void ParseAsUnixAbsoluteFilePath(string uriString)
		{
			this.isUnixFilePath = true;
			this.scheme = Uri.UriSchemeFile;
			this.port = -1;
			this.fragment = string.Empty;
			this.query = string.Empty;
			this.host = string.Empty;
			this.path = null;
			if (uriString.StartsWith("//"))
			{
				uriString = uriString.TrimStart(new char[] { '/' });
				this.path = "/" + uriString;
			}
			if (this.path == null)
			{
				this.path = uriString;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CD68 File Offset: 0x0000AF68
		private void Parse(string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			if (uriString.Length <= 1)
			{
				throw new FormatException();
			}
			int num = uriString.IndexOf(':');
			if (num < 0)
			{
				if (uriString[0] == '/')
				{
					this.ParseAsUnixAbsoluteFilePath(uriString);
					return;
				}
				if (uriString.StartsWith("\\\\"))
				{
					this.ParseAsWindowsUNC(uriString);
					return;
				}
				throw new FormatException("URI scheme was not recognized, nor input string is not recognized as an absolute file path.");
			}
			else if (num == 1)
			{
				if (!char.IsLetter(uriString[0]))
				{
					throw new FormatException("URI scheme must start with alphabet character.");
				}
				this.ParseAsWindowsAbsoluteFilePath(uriString);
				return;
			}
			else
			{
				this.scheme = uriString.Substring(0, num).ToLower(CultureInfo.InvariantCulture);
				if (!char.IsLetter(this.scheme[0]))
				{
					throw new FormatException("URI scheme must start with alphabet character.");
				}
				for (int i = 1; i < this.scheme.Length; i++)
				{
					if (!char.IsLetterOrDigit(this.scheme, i))
					{
						switch (this.scheme[i])
						{
						case '+':
						case '-':
						case '.':
							break;
						default:
							throw new FormatException("URI scheme must consist of one of alphabet, digits, '+', '-' or '.' character.");
						}
					}
				}
				uriString = uriString.Substring(num + 1);
				num = uriString.IndexOf('#');
				if (!this.IsUnc && num != -1)
				{
					this.fragment = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
				}
				num = uriString.IndexOf('?');
				if (num != -1)
				{
					this.query = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
					if (!this.userEscaped)
					{
						this.query = Uri.EscapeString(this.query);
					}
				}
				bool flag = this.scheme == Uri.UriSchemeFile && uriString.StartsWith("///");
				if (uriString.StartsWith("//"))
				{
					if (uriString.StartsWith("////"))
					{
						flag = false;
					}
					uriString = uriString.TrimStart(new char[] { '/' });
					if (uriString.Length > 1 && uriString[1] == ':')
					{
						flag = false;
					}
				}
				else if (!Uri.IsPredefinedScheme(this.scheme))
				{
					this.path = uriString;
					this.isOpaquePart = true;
					return;
				}
				num = uriString.IndexOfAny(new char[] { '/', '\\' });
				if (flag)
				{
					num = -1;
				}
				if (num == -1)
				{
					if (this.scheme != Uri.UriSchemeMailto && this.scheme != Uri.UriSchemeNews && this.scheme != Uri.UriSchemeFile)
					{
						this.path = "/";
					}
				}
				else
				{
					this.path = uriString.Substring(num);
					uriString = uriString.Substring(0, num);
				}
				num = uriString.IndexOf("@");
				if (flag)
				{
					num = -1;
				}
				if (num != -1)
				{
					this.userinfo = uriString.Substring(0, num);
					uriString = uriString.Remove(0, num + 1);
				}
				this.port = -1;
				num = uriString.LastIndexOf(":");
				if (flag)
				{
					num = -1;
				}
				if (num == 1 && this.scheme == Uri.UriSchemeFile && char.IsLetter(uriString[0]))
				{
					num = -1;
				}
				if (num != -1 && num != uriString.Length - 1)
				{
					string text = uriString.Remove(0, num + 1);
					if (text.Length > 1 && text[text.Length - 1] != ']')
					{
						try
						{
							this.port = (int)uint.Parse(text, CultureInfo.InvariantCulture);
							uriString = uriString.Substring(0, num);
						}
						catch (Exception)
						{
							throw new FormatException("Invalid URI: invalid port number");
						}
					}
				}
				if (this.port == -1)
				{
					this.port = Uri.GetDefaultPort(this.scheme);
				}
				this.host = uriString;
				if (flag)
				{
					this.path = "/" + uriString;
					this.host = string.Empty;
				}
				else if (this.host.Length == 2 && this.host[1] == ':')
				{
					this.path = this.host + this.path;
					this.host = string.Empty;
				}
				else if (this.isUnixFilePath)
				{
					uriString = "//" + uriString;
					this.host = string.Empty;
				}
				else
				{
					if (this.host.Length == 0)
					{
						throw new FormatException("Invalid URI: The hostname could not be parsed");
					}
					if (this.scheme == Uri.UriSchemeFile)
					{
						this.isUnc = true;
					}
				}
				if (this.scheme != Uri.UriSchemeMailto && this.scheme != Uri.UriSchemeNews && this.scheme != Uri.UriSchemeFile && this.reduce)
				{
					this.path = Uri.Reduce(this.path);
				}
				return;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D200 File Offset: 0x0000B400
		private static string Reduce(string path)
		{
			path = path.Replace('\\', '/');
			string[] array = path.Split('/', StringSplitOptions.None);
			List<string> list = new List<string>();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string text = array[i];
				if (text.Length != 0 && !(text == "."))
				{
					if (text == "..")
					{
						if (list.Count == 0)
						{
							if (i != 1)
							{
								throw new Exception("Invalid path.");
							}
						}
						else
						{
							list.RemoveAt(list.Count - 1);
						}
					}
					else
					{
						list.Add(text);
					}
				}
			}
			if (list.Count == 0)
			{
				return "/";
			}
			list.Insert(0, string.Empty);
			string text2 = string.Join("/", list.ToArray());
			if (path.EndsWith("/"))
			{
				text2 += "/";
			}
			return text2;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		internal static string GetSchemeDelimiter(string scheme)
		{
			for (int i = 0; i < Uri.schemes.Length; i++)
			{
				if (Uri.schemes[i].scheme == scheme)
				{
					return Uri.schemes[i].delimiter;
				}
			}
			return Uri.SchemeDelimiter;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000D330 File Offset: 0x0000B530
		internal static int GetDefaultPort(string scheme)
		{
			for (int i = 0; i < Uri.schemes.Length; i++)
			{
				if (Uri.schemes[i].scheme == scheme)
				{
					return Uri.schemes[i].defaultPort;
				}
			}
			return -1;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000D379 File Offset: 0x0000B579
		private string GetOpaqueWiseSchemeDelimiter()
		{
			if (this.isOpaquePart)
			{
				return ":";
			}
			return Uri.GetSchemeDelimiter(this.scheme);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D394 File Offset: 0x0000B594
		protected bool IsBadFileSystemCharacter(char ch)
		{
			if (ch < ' ' || (ch < '@' && ch > '9'))
			{
				return true;
			}
			if (ch <= '*')
			{
				if (ch <= '"')
				{
					if (ch != '\0' && ch != '"')
					{
						return false;
					}
				}
				else if (ch != '&' && ch != '*')
				{
					return false;
				}
			}
			else if (ch <= '/')
			{
				if (ch != ',' && ch != '/')
				{
					return false;
				}
			}
			else if (ch != '\\' && ch != '^' && ch != '|')
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		protected static bool IsExcludedCharacter(char ch)
		{
			return ch <= ' ' || ch >= '\u007f' || (ch == '"' || ch == '#' || ch == '%' || ch == '<' || ch == '>' || ch == '[' || ch == '\\' || ch == ']' || ch == '^' || ch == '`' || ch == '{' || ch == '|' || ch == '}');
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D458 File Offset: 0x0000B658
		private static bool IsPredefinedScheme(string scheme)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(scheme);
			if (num <= 2867484483U)
			{
				if (num <= 1271381062U)
				{
					if (num != 227981521U)
					{
						if (num != 1271381062U)
						{
							return false;
						}
						if (!(scheme == "news"))
						{
							return false;
						}
					}
					else if (!(scheme == "nntp"))
					{
						return false;
					}
				}
				else if (num != 1315902419U)
				{
					if (num != 2867484483U)
					{
						return false;
					}
					if (!(scheme == "file"))
					{
						return false;
					}
				}
				else if (!(scheme == "mailto"))
				{
					return false;
				}
			}
			else if (num <= 3378792613U)
			{
				if (num != 3101544485U)
				{
					if (num != 3378792613U)
					{
						return false;
					}
					if (!(scheme == "http"))
					{
						return false;
					}
				}
				else if (!(scheme == "ftp"))
				{
					return false;
				}
			}
			else if (num != 3500961320U)
			{
				if (num != 3739134178U)
				{
					return false;
				}
				if (!(scheme == "https"))
				{
					return false;
				}
			}
			else if (!(scheme == "gopher"))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D557 File Offset: 0x0000B757
		protected bool IsReservedCharacter(char ch)
		{
			return ch == '$' || ch == '&' || ch == '+' || ch == ',' || ch == '/' || ch == ':' || ch == ';' || ch == '=' || ch == '@';
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D58C File Offset: 0x0000B78C
		// Note: this type is marked as 'beforefieldinit'.
		static Uri()
		{
		}

		// Token: 0x04000DB0 RID: 3504
		private bool isUnixFilePath;

		// Token: 0x04000DB1 RID: 3505
		private string source;

		// Token: 0x04000DB2 RID: 3506
		private string scheme;

		// Token: 0x04000DB3 RID: 3507
		private string host;

		// Token: 0x04000DB4 RID: 3508
		private int port;

		// Token: 0x04000DB5 RID: 3509
		private string path;

		// Token: 0x04000DB6 RID: 3510
		private string query;

		// Token: 0x04000DB7 RID: 3511
		private string fragment;

		// Token: 0x04000DB8 RID: 3512
		private string userinfo;

		// Token: 0x04000DB9 RID: 3513
		private bool isUnc;

		// Token: 0x04000DBA RID: 3514
		private bool isOpaquePart;

		// Token: 0x04000DBB RID: 3515
		private string[] segments;

		// Token: 0x04000DBC RID: 3516
		private bool userEscaped;

		// Token: 0x04000DBD RID: 3517
		private string cachedAbsoluteUri;

		// Token: 0x04000DBE RID: 3518
		private string cachedToString;

		// Token: 0x04000DBF RID: 3519
		private string cachedLocalPath;

		// Token: 0x04000DC0 RID: 3520
		private int cachedHashCode;

		// Token: 0x04000DC1 RID: 3521
		private bool reduce;

		// Token: 0x04000DC2 RID: 3522
		private static readonly string hexUpperChars = "0123456789ABCDEF";

		// Token: 0x04000DC3 RID: 3523
		public static readonly string SchemeDelimiter = "://";

		// Token: 0x04000DC4 RID: 3524
		public static readonly string UriSchemeFile = "file";

		// Token: 0x04000DC5 RID: 3525
		public static readonly string UriSchemeFtp = "ftp";

		// Token: 0x04000DC6 RID: 3526
		public static readonly string UriSchemeGopher = "gopher";

		// Token: 0x04000DC7 RID: 3527
		public static readonly string UriSchemeHttp = "http";

		// Token: 0x04000DC8 RID: 3528
		public static readonly string UriSchemeHttps = "https";

		// Token: 0x04000DC9 RID: 3529
		public static readonly string UriSchemeMailto = "mailto";

		// Token: 0x04000DCA RID: 3530
		public static readonly string UriSchemeNews = "news";

		// Token: 0x04000DCB RID: 3531
		public static readonly string UriSchemeNntp = "nntp";

		// Token: 0x04000DCC RID: 3532
		private static Uri.UriScheme[] schemes = new Uri.UriScheme[]
		{
			new Uri.UriScheme(Uri.UriSchemeHttp, Uri.SchemeDelimiter, 80),
			new Uri.UriScheme(Uri.UriSchemeHttps, Uri.SchemeDelimiter, 443),
			new Uri.UriScheme(Uri.UriSchemeFtp, Uri.SchemeDelimiter, 21),
			new Uri.UriScheme(Uri.UriSchemeFile, Uri.SchemeDelimiter, -1),
			new Uri.UriScheme(Uri.UriSchemeMailto, ":", 25),
			new Uri.UriScheme(Uri.UriSchemeNews, ":", -1),
			new Uri.UriScheme(Uri.UriSchemeNntp, Uri.SchemeDelimiter, 119),
			new Uri.UriScheme(Uri.UriSchemeGopher, Uri.SchemeDelimiter, 70)
		};

		// Token: 0x02000064 RID: 100
		private struct UriScheme
		{
			// Token: 0x06000262 RID: 610 RVA: 0x0000D6C9 File Offset: 0x0000B8C9
			public UriScheme(string s, string d, int p)
			{
				this.scheme = s;
				this.delimiter = d;
				this.defaultPort = p;
			}

			// Token: 0x04000DCD RID: 3533
			public string scheme;

			// Token: 0x04000DCE RID: 3534
			public string delimiter;

			// Token: 0x04000DCF RID: 3535
			public int defaultPort;
		}
	}
}
