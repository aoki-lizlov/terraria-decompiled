using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000F6 RID: 246
	[NonVersionable]
	[Serializable]
	public struct Guid : IFormattable, IComparable, IComparable<Guid>, IEquatable<Guid>, ISpanFormattable
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x00027014 File Offset: 0x00025214
		public unsafe static Guid NewGuid()
		{
			Guid guid;
			Interop.GetRandomBytes((byte*)(&guid), sizeof(Guid));
			guid._c = (short)(((int)guid._c & -61441) | 16384);
			guid._d = (byte)(((int)guid._d & -193) | 128);
			return guid;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00027064 File Offset: 0x00025264
		public Guid(byte[] b)
		{
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			this = new Guid(new ReadOnlySpan<byte>(b));
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00027084 File Offset: 0x00025284
		public unsafe Guid(ReadOnlySpan<byte> b)
		{
			if (b.Length != 16)
			{
				throw new ArgumentException(SR.Format("Byte array for GUID must be exactly {0} bytes long.", "16"), "b");
			}
			this._a = ((int)(*b[3]) << 24) | ((int)(*b[2]) << 16) | ((int)(*b[1]) << 8) | (int)(*b[0]);
			this._b = (short)(((int)(*b[5]) << 8) | (int)(*b[4]));
			this._c = (short)(((int)(*b[7]) << 8) | (int)(*b[6]));
			this._d = *b[8];
			this._e = *b[9];
			this._f = *b[10];
			this._g = *b[11];
			this._h = *b[12];
			this._i = *b[13];
			this._j = *b[14];
			this._k = *b[15];
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000271A4 File Offset: 0x000253A4
		[CLSCompliant(false)]
		public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = (int)a;
			this._b = (short)b;
			this._c = (short)c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00027208 File Offset: 0x00025408
		public Guid(int a, short b, short c, byte[] d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			if (d.Length != 8)
			{
				throw new ArgumentException(SR.Format("Byte array for GUID must be exactly {0} bytes long.", "8"), "d");
			}
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d[0];
			this._e = d[1];
			this._f = d[2];
			this._g = d[3];
			this._h = d[4];
			this._i = d[5];
			this._j = d[6];
			this._k = d[7];
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000272AC File Offset: 0x000254AC
		public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00027310 File Offset: 0x00025510
		public Guid(string g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.All);
			if (Guid.TryParseGuid(g, Guid.GuidStyles.Any, ref guidResult))
			{
				this = guidResult._parsedGuid;
				return;
			}
			throw guidResult.GetGuidParseException();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00027360 File Offset: 0x00025560
		public static Guid Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Guid.Parse(input);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00027388 File Offset: 0x00025588
		public static Guid Parse(ReadOnlySpan<char> input)
		{
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.AllButOverflow);
			if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref guidResult))
			{
				return guidResult._parsedGuid;
			}
			throw guidResult.GetGuidParseException();
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x000273BF File Offset: 0x000255BF
		public static bool TryParse(string input, out Guid result)
		{
			if (input == null)
			{
				result = default(Guid);
				return false;
			}
			return Guid.TryParse(input, out result);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000273DC File Offset: 0x000255DC
		public static bool TryParse(ReadOnlySpan<char> input, out Guid result)
		{
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref guidResult))
			{
				result = guidResult._parsedGuid;
				return true;
			}
			result = default(Guid);
			return false;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002741C File Offset: 0x0002561C
		public static Guid ParseExact(string input, string format)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			ReadOnlySpan<char> readOnlySpan = input;
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return Guid.ParseExact(readOnlySpan, format);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00027458 File Offset: 0x00025658
		public unsafe static Guid ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format)
		{
			if (format.Length != 1)
			{
				throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			}
			char c = (char)(*format[0]);
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_0071;
					}
					if (c != 'D')
					{
						goto IL_0083;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_006D;
					}
					if (c == 'P')
					{
						goto IL_0076;
					}
					if (c != 'X')
					{
						goto IL_0083;
					}
					goto IL_007B;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_0071;
				}
				if (c != 'd')
				{
					goto IL_0083;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_006D;
				}
				if (c == 'p')
				{
					goto IL_0076;
				}
				if (c != 'x')
				{
					goto IL_0083;
				}
				goto IL_007B;
			}
			Guid.GuidStyles guidStyles = Guid.GuidStyles.RequireDashes;
			goto IL_008E;
			IL_006D:
			guidStyles = Guid.GuidStyles.None;
			goto IL_008E;
			IL_0071:
			guidStyles = Guid.GuidStyles.BraceFormat;
			goto IL_008E;
			IL_0076:
			guidStyles = Guid.GuidStyles.ParenthesisFormat;
			goto IL_008E;
			IL_007B:
			guidStyles = Guid.GuidStyles.HexFormat;
			goto IL_008E;
			IL_0083:
			throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			IL_008E:
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.AllButOverflow);
			if (Guid.TryParseGuid(input, guidStyles, ref guidResult))
			{
				return guidResult._parsedGuid;
			}
			throw guidResult.GetGuidParseException();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002751C File Offset: 0x0002571C
		public static bool TryParseExact(string input, string format, out Guid result)
		{
			if (input == null)
			{
				result = default(Guid);
				return false;
			}
			return Guid.TryParseExact(input, format, out result);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002753C File Offset: 0x0002573C
		public unsafe static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, out Guid result)
		{
			if (format.Length != 1)
			{
				result = default(Guid);
				return false;
			}
			char c = (char)(*format[0]);
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_006F;
					}
					if (c != 'D')
					{
						goto IL_0081;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_006B;
					}
					if (c == 'P')
					{
						goto IL_0074;
					}
					if (c != 'X')
					{
						goto IL_0081;
					}
					goto IL_0079;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_006F;
				}
				if (c != 'd')
				{
					goto IL_0081;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_006B;
				}
				if (c == 'p')
				{
					goto IL_0074;
				}
				if (c != 'x')
				{
					goto IL_0081;
				}
				goto IL_0079;
			}
			Guid.GuidStyles guidStyles = Guid.GuidStyles.RequireDashes;
			goto IL_008A;
			IL_006B:
			guidStyles = Guid.GuidStyles.None;
			goto IL_008A;
			IL_006F:
			guidStyles = Guid.GuidStyles.BraceFormat;
			goto IL_008A;
			IL_0074:
			guidStyles = Guid.GuidStyles.ParenthesisFormat;
			goto IL_008A;
			IL_0079:
			guidStyles = Guid.GuidStyles.HexFormat;
			goto IL_008A;
			IL_0081:
			result = default(Guid);
			return false;
			IL_008A:
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, guidStyles, ref guidResult))
			{
				result = guidResult._parsedGuid;
				return true;
			}
			result = default(Guid);
			return false;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00027604 File Offset: 0x00025804
		private static bool TryParseGuid(ReadOnlySpan<char> guidString, Guid.GuidStyles flags, ref Guid.GuidResult result)
		{
			guidString = guidString.Trim();
			if (guidString.Length == 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool flag = guidString.IndexOf('-') >= 0;
			if (flag)
			{
				if ((flags & (Guid.GuidStyles.AllowDashes | Guid.GuidStyles.RequireDashes)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireDashes) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool flag2 = guidString.IndexOf('{') >= 0;
			if (flag2)
			{
				if ((flags & (Guid.GuidStyles.AllowBraces | Guid.GuidStyles.RequireBraces)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireBraces) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			if (guidString.IndexOf('(') >= 0)
			{
				if ((flags & (Guid.GuidStyles.AllowParenthesis | Guid.GuidStyles.RequireParenthesis)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireParenthesis) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool flag3;
			try
			{
				if (flag)
				{
					flag3 = Guid.TryParseGuidWithDashes(guidString, ref result);
				}
				else if (flag2)
				{
					flag3 = Guid.TryParseGuidWithHexPrefix(guidString, ref result);
				}
				else
				{
					flag3 = Guid.TryParseGuidWithNoStyle(guidString, ref result);
				}
			}
			catch (IndexOutOfRangeException ex)
			{
				result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Unrecognized Guid format.", null, null, ex);
				flag3 = false;
			}
			catch (ArgumentException ex2)
			{
				result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Unrecognized Guid format.", null, null, ex2);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00027748 File Offset: 0x00025948
		private unsafe static bool TryParseGuidWithHexPrefix(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			guidString = Guid.EatAllWhitespace(guidString);
			if (guidString.Length == 0 || *guidString[0] != 123)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected {0xdddddddd, etc}.");
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, etc}");
				return false;
			}
			int num = 3;
			int num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToInt(guidString.Slice(num, num2), -1, 4096, out result._parsedGuid._a, ref result))
			{
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, 0xdddd, etc}");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToShort(guidString.Slice(num, num2), -1, 4096, out result._parsedGuid._b, ref result))
			{
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, 0xdddd, 0xdddd, etc}");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToShort(guidString.Slice(num, num2), -1, 4096, out result._parsedGuid._c, ref result))
			{
				return false;
			}
			if (guidString.Length <= num + num2 + 1 || *guidString[num + num2 + 1] != 123)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected {0xdddddddd, etc}.");
				return false;
			}
			num2++;
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)8], 8);
			for (int i = 0; i < span.Length; i++)
			{
				if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{... { ... 0xdd, ...}}");
					return false;
				}
				num = num + num2 + 3;
				if (i < 7)
				{
					num2 = guidString.Slice(num).IndexOf(',');
					if (num2 <= 0)
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
						return false;
					}
				}
				else
				{
					num2 = guidString.Slice(num).IndexOf('}');
					if (num2 <= 0)
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).");
						return false;
					}
				}
				int num3;
				if (!Guid.StringToInt(guidString.Slice(num, num2), -1, 4096, out num3, ref result))
				{
					return false;
				}
				uint num4 = (uint)num3;
				if (num4 > 255U)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Value was either too large or too small for an unsigned byte.");
					return false;
				}
				*span[i] = (byte)num4;
			}
			result._parsedGuid._d = *span[0];
			result._parsedGuid._e = *span[1];
			result._parsedGuid._f = *span[2];
			result._parsedGuid._g = *span[3];
			result._parsedGuid._h = *span[4];
			result._parsedGuid._i = *span[5];
			result._parsedGuid._j = *span[6];
			result._parsedGuid._k = *span[7];
			if (num + num2 + 1 >= guidString.Length || *guidString[num + num2 + 1] != 125)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find the ending brace.");
				return false;
			}
			if (num + num2 + 1 != guidString.Length - 1)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Additional non-parsable characters are at the end of the string.");
				return false;
			}
			return true;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00027AB4 File Offset: 0x00025CB4
		private unsafe static bool TryParseGuidWithNoStyle(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			int num = 0;
			int num2 = 0;
			if (guidString.Length != 32)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			for (int i = 0; i < guidString.Length; i++)
			{
				char c = (char)(*guidString[i]);
				if (c < '0' || c > '9')
				{
					char c2 = char.ToUpperInvariant(c);
					if (c2 < 'A' || c2 > 'F')
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Guid string should only contain hexadecimal characters.");
						return false;
					}
				}
			}
			if (!Guid.StringToInt(guidString.Slice(num, 8), -1, 4096, out result._parsedGuid._a, ref result))
			{
				return false;
			}
			num += 8;
			if (!Guid.StringToShort(guidString.Slice(num, 4), -1, 4096, out result._parsedGuid._b, ref result))
			{
				return false;
			}
			num += 4;
			if (!Guid.StringToShort(guidString.Slice(num, 4), -1, 4096, out result._parsedGuid._c, ref result))
			{
				return false;
			}
			num += 4;
			int num3;
			if (!Guid.StringToInt(guidString.Slice(num, 4), -1, 4096, out num3, ref result))
			{
				return false;
			}
			num += 4;
			num2 = num;
			long num4;
			if (!Guid.StringToLong(guidString, ref num2, 8192, out num4, ref result))
			{
				return false;
			}
			if (num2 - num != 12)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			result._parsedGuid._d = (byte)(num3 >> 8);
			result._parsedGuid._e = (byte)num3;
			num3 = (int)(num4 >> 32);
			result._parsedGuid._f = (byte)(num3 >> 8);
			result._parsedGuid._g = (byte)num3;
			num3 = (int)num4;
			result._parsedGuid._h = (byte)(num3 >> 24);
			result._parsedGuid._i = (byte)(num3 >> 16);
			result._parsedGuid._j = (byte)(num3 >> 8);
			result._parsedGuid._k = (byte)num3;
			return true;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00027C70 File Offset: 0x00025E70
		private unsafe static bool TryParseGuidWithDashes(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			int num = 0;
			int num2 = 0;
			if (*guidString[0] == 123)
			{
				if (guidString.Length != 38 || *guidString[37] != 125)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
					return false;
				}
				num = 1;
			}
			else if (*guidString[0] == 40)
			{
				if (guidString.Length != 38 || *guidString[37] != 41)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
					return false;
				}
				num = 1;
			}
			else if (guidString.Length != 36)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			if (*guidString[8 + num] != 45 || *guidString[13 + num] != 45 || *guidString[18 + num] != 45 || *guidString[23 + num] != 45)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Dashes are in the wrong position for GUID parsing.");
				return false;
			}
			num2 = num;
			int num3;
			if (!Guid.StringToInt(guidString, ref num2, 8, 8192, out num3, ref result))
			{
				return false;
			}
			result._parsedGuid._a = num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			result._parsedGuid._b = (short)num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			result._parsedGuid._c = (short)num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			num2++;
			num = num2;
			long num4;
			if (!Guid.StringToLong(guidString, ref num2, 8192, out num4, ref result))
			{
				return false;
			}
			if (num2 - num != 12)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			result._parsedGuid._d = (byte)(num3 >> 8);
			result._parsedGuid._e = (byte)num3;
			num3 = (int)(num4 >> 32);
			result._parsedGuid._f = (byte)(num3 >> 8);
			result._parsedGuid._g = (byte)num3;
			num3 = (int)num4;
			result._parsedGuid._h = (byte)(num3 >> 24);
			result._parsedGuid._i = (byte)(num3 >> 16);
			result._parsedGuid._j = (byte)(num3 >> 8);
			result._parsedGuid._k = (byte)num3;
			return true;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00027E8C File Offset: 0x0002608C
		private static bool StringToShort(ReadOnlySpan<char> str, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			int num = 0;
			return Guid.StringToShort(str, ref num, requiredLength, flags, out result, ref parseResult);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00027EA8 File Offset: 0x000260A8
		private static bool StringToShort(ReadOnlySpan<char> str, ref int parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			result = 0;
			int num;
			bool flag = Guid.StringToInt(str, ref parsePos, requiredLength, flags, out num, ref parseResult);
			result = (short)num;
			return flag;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00027ECC File Offset: 0x000260CC
		private static bool StringToInt(ReadOnlySpan<char> str, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			int num = 0;
			return Guid.StringToInt(str, ref num, requiredLength, flags, out result, ref parseResult);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00027EE8 File Offset: 0x000260E8
		private static bool StringToInt(ReadOnlySpan<char> str, ref int parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			result = 0;
			int num = parsePos;
			try
			{
				result = ParseNumbers.StringToInt(str, 16, flags, ref parsePos);
			}
			catch (OverflowException ex)
			{
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw;
				}
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
				{
					throw new FormatException("Unrecognized Guid format.", ex);
				}
				parseResult.SetFailure(ex);
				return false;
			}
			catch (Exception ex2)
			{
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.None)
				{
					parseResult.SetFailure(ex2);
					return false;
				}
				throw;
			}
			if (requiredLength != -1 && parsePos - num != requiredLength)
			{
				parseResult.SetFailure(Guid.ParseFailureKind.Format, "Guid string should only contain hexadecimal characters.");
				return false;
			}
			return true;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00027F8C File Offset: 0x0002618C
		private static bool StringToLong(ReadOnlySpan<char> str, ref int parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
		{
			result = 0L;
			try
			{
				result = ParseNumbers.StringToLong(str, 16, flags, ref parsePos);
			}
			catch (OverflowException ex)
			{
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw;
				}
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
				{
					throw new FormatException("Unrecognized Guid format.", ex);
				}
				parseResult.SetFailure(ex);
				return false;
			}
			catch (Exception ex2)
			{
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.None)
				{
					parseResult.SetFailure(ex2);
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00028014 File Offset: 0x00026214
		private unsafe static ReadOnlySpan<char> EatAllWhitespace(ReadOnlySpan<char> str)
		{
			int i = 0;
			while (i < str.Length && !char.IsWhiteSpace((char)(*str[i])))
			{
				i++;
			}
			if (i == str.Length)
			{
				return str;
			}
			char[] array = new char[str.Length];
			int num = 0;
			if (i > 0)
			{
				num = i;
				str.Slice(0, i).CopyTo(array);
			}
			while (i < str.Length)
			{
				char c = (char)(*str[i]);
				if (!char.IsWhiteSpace(c))
				{
					array[num++] = c;
				}
				i++;
			}
			return new ReadOnlySpan<char>(array, 0, num);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000280B0 File Offset: 0x000262B0
		private unsafe static bool IsHexPrefix(ReadOnlySpan<char> str, int i)
		{
			return i + 1 < str.Length && *str[i] == 48 && (*str[i + 1] == 120 || char.ToLowerInvariant((char)(*str[i + 1])) == 'x');
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00028100 File Offset: 0x00026300
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteByteHelper(Span<byte> destination)
		{
			*destination[0] = (byte)this._a;
			*destination[1] = (byte)(this._a >> 8);
			*destination[2] = (byte)(this._a >> 16);
			*destination[3] = (byte)(this._a >> 24);
			*destination[4] = (byte)this._b;
			*destination[5] = (byte)(this._b >> 8);
			*destination[6] = (byte)this._c;
			*destination[7] = (byte)(this._c >> 8);
			*destination[8] = this._d;
			*destination[9] = this._e;
			*destination[10] = this._f;
			*destination[11] = this._g;
			*destination[12] = this._h;
			*destination[13] = this._i;
			*destination[14] = this._j;
			*destination[15] = this._k;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00028218 File Offset: 0x00026418
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			this.WriteByteHelper(array);
			return array;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002823A File Offset: 0x0002643A
		public bool TryWriteBytes(Span<byte> destination)
		{
			if (destination.Length < 16)
			{
				return false;
			}
			this.WriteByteHelper(destination);
			return true;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00028251 File Offset: 0x00026451
		public override string ToString()
		{
			return this.ToString("D", null);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002825F File Offset: 0x0002645F
		public unsafe override int GetHashCode()
		{
			return this._a ^ *Unsafe.Add<int>(ref this._a, 1) ^ *Unsafe.Add<int>(ref this._a, 2) ^ *Unsafe.Add<int>(ref this._a, 3);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00028294 File Offset: 0x00026494
		public unsafe override bool Equals(object o)
		{
			if (o == null || !(o is Guid))
			{
				return false;
			}
			Guid guid = (Guid)o;
			return guid._a == this._a && *Unsafe.Add<int>(ref guid._a, 1) == *Unsafe.Add<int>(ref this._a, 1) && *Unsafe.Add<int>(ref guid._a, 2) == *Unsafe.Add<int>(ref this._a, 2) && *Unsafe.Add<int>(ref guid._a, 3) == *Unsafe.Add<int>(ref this._a, 3);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002831C File Offset: 0x0002651C
		public unsafe bool Equals(Guid g)
		{
			return g._a == this._a && *Unsafe.Add<int>(ref g._a, 1) == *Unsafe.Add<int>(ref this._a, 1) && *Unsafe.Add<int>(ref g._a, 2) == *Unsafe.Add<int>(ref this._a, 2) && *Unsafe.Add<int>(ref g._a, 3) == *Unsafe.Add<int>(ref this._a, 3);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00028390 File Offset: 0x00026590
		private int GetResult(uint me, uint them)
		{
			if (me < them)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002839C File Offset: 0x0002659C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is Guid))
			{
				throw new ArgumentException("Object must be of type GUID.", "value");
			}
			Guid guid = (Guid)value;
			if (guid._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)guid._a);
			}
			if (guid._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)guid._b);
			}
			if (guid._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)guid._c);
			}
			if (guid._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)guid._d);
			}
			if (guid._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)guid._e);
			}
			if (guid._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)guid._f);
			}
			if (guid._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)guid._g);
			}
			if (guid._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)guid._h);
			}
			if (guid._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)guid._i);
			}
			if (guid._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)guid._j);
			}
			if (guid._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)guid._k);
			}
			return 0;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002853C File Offset: 0x0002673C
		public int CompareTo(Guid value)
		{
			if (value._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)value._a);
			}
			if (value._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)value._b);
			}
			if (value._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)value._c);
			}
			if (value._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)value._d);
			}
			if (value._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)value._e);
			}
			if (value._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)value._f);
			}
			if (value._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)value._g);
			}
			if (value._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)value._h);
			}
			if (value._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)value._i);
			}
			if (value._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)value._j);
			}
			if (value._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)value._k);
			}
			return 0;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000286B8 File Offset: 0x000268B8
		public unsafe static bool operator ==(Guid a, Guid b)
		{
			return a._a == b._a && *Unsafe.Add<int>(ref a._a, 1) == *Unsafe.Add<int>(ref b._a, 1) && *Unsafe.Add<int>(ref a._a, 2) == *Unsafe.Add<int>(ref b._a, 2) && *Unsafe.Add<int>(ref a._a, 3) == *Unsafe.Add<int>(ref b._a, 3);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00028730 File Offset: 0x00026930
		public unsafe static bool operator !=(Guid a, Guid b)
		{
			return a._a != b._a || *Unsafe.Add<int>(ref a._a, 1) != *Unsafe.Add<int>(ref b._a, 1) || *Unsafe.Add<int>(ref a._a, 2) != *Unsafe.Add<int>(ref b._a, 2) || *Unsafe.Add<int>(ref a._a, 3) != *Unsafe.Add<int>(ref b._a, 3);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000287AA File Offset: 0x000269AA
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000287B4 File Offset: 0x000269B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static char HexToChar(int a)
		{
			a &= 15;
			return (char)((a > 9) ? (a - 10 + 97) : (a + 48));
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000287CF File Offset: 0x000269CF
		private unsafe static int HexsToChars(char* guidChars, int a, int b)
		{
			*guidChars = Guid.HexToChar(a >> 4);
			guidChars[1] = Guid.HexToChar(a);
			guidChars[2] = Guid.HexToChar(b >> 4);
			guidChars[3] = Guid.HexToChar(b);
			return 4;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00028804 File Offset: 0x00026A04
		private unsafe static int HexsToCharsHexOutput(char* guidChars, int a, int b)
		{
			*guidChars = '0';
			guidChars[1] = 'x';
			guidChars[2] = Guid.HexToChar(a >> 4);
			guidChars[3] = Guid.HexToChar(a);
			guidChars[4] = ',';
			guidChars[5] = '0';
			guidChars[6] = 'x';
			guidChars[7] = Guid.HexToChar(b >> 4);
			guidChars[8] = Guid.HexToChar(b);
			return 9;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00028870 File Offset: 0x00026A70
		public unsafe string ToString(string format, IFormatProvider provider)
		{
			if (format == null || format.Length == 0)
			{
				format = "D";
			}
			if (format.Length != 1)
			{
				throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			}
			char c = format[0];
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_0081;
					}
					if (c != 'D')
					{
						goto IL_008B;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_007C;
					}
					if (c == 'P')
					{
						goto IL_0081;
					}
					if (c != 'X')
					{
						goto IL_008B;
					}
					goto IL_0086;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_0081;
				}
				if (c != 'd')
				{
					goto IL_008B;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_007C;
				}
				if (c == 'p')
				{
					goto IL_0081;
				}
				if (c != 'x')
				{
					goto IL_008B;
				}
				goto IL_0086;
			}
			int num = 36;
			goto IL_0096;
			IL_007C:
			num = 32;
			goto IL_0096;
			IL_0081:
			num = 38;
			goto IL_0096;
			IL_0086:
			num = 68;
			goto IL_0096;
			IL_008B:
			throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			IL_0096:
			string text = string.FastAllocateString(num);
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num2;
				this.TryFormat(new Span<char>((void*)ptr, text.Length), out num2, format);
			}
			return text;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00028950 File Offset: 0x00026B50
		public unsafe bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>))
		{
			if (format.Length == 0)
			{
				format = "D";
			}
			if (format.Length != 1)
			{
				throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			}
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			char c = (char)(*format[0]);
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_009D;
					}
					if (c != 'D')
					{
						goto IL_00C2;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_0096;
					}
					if (c == 'P')
					{
						goto IL_00A8;
					}
					if (c != 'X')
					{
						goto IL_00C2;
					}
					goto IL_00B3;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_009D;
				}
				if (c != 'd')
				{
					goto IL_00C2;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_0096;
				}
				if (c == 'p')
				{
					goto IL_00A8;
				}
				if (c != 'x')
				{
					goto IL_00C2;
				}
				goto IL_00B3;
			}
			int num2 = 36;
			goto IL_00CD;
			IL_0096:
			flag = false;
			num2 = 32;
			goto IL_00CD;
			IL_009D:
			num = 8192123;
			num2 = 38;
			goto IL_00CD;
			IL_00A8:
			num = 2687016;
			num2 = 38;
			goto IL_00CD;
			IL_00B3:
			num = 8192123;
			flag = false;
			flag2 = true;
			num2 = 68;
			goto IL_00CD;
			IL_00C2:
			throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			IL_00CD:
			if (destination.Length < num2)
			{
				charsWritten = 0;
				return false;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				if (num != 0)
				{
					*(ptr++) = (char)num;
				}
				if (flag2)
				{
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += Guid.HexsToChars(ptr, this._a >> 24, this._a >> 16);
					ptr += Guid.HexsToChars(ptr, this._a >> 8, this._a);
					*(ptr++) = ',';
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += Guid.HexsToChars(ptr, this._b >> 8, (int)this._b);
					*(ptr++) = ',';
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += Guid.HexsToChars(ptr, this._c >> 8, (int)this._c);
					*(ptr++) = ',';
					*(ptr++) = '{';
					ptr += Guid.HexsToCharsHexOutput(ptr, (int)this._d, (int)this._e);
					*(ptr++) = ',';
					ptr += Guid.HexsToCharsHexOutput(ptr, (int)this._f, (int)this._g);
					*(ptr++) = ',';
					ptr += Guid.HexsToCharsHexOutput(ptr, (int)this._h, (int)this._i);
					*(ptr++) = ',';
					ptr += Guid.HexsToCharsHexOutput(ptr, (int)this._j, (int)this._k);
					*(ptr++) = '}';
				}
				else
				{
					ptr += Guid.HexsToChars(ptr, this._a >> 24, this._a >> 16);
					ptr += Guid.HexsToChars(ptr, this._a >> 8, this._a);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += Guid.HexsToChars(ptr, this._b >> 8, (int)this._b);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += Guid.HexsToChars(ptr, this._c >> 8, (int)this._c);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += Guid.HexsToChars(ptr, (int)this._d, (int)this._e);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += Guid.HexsToChars(ptr, (int)this._f, (int)this._g);
					ptr += Guid.HexsToChars(ptr, (int)this._h, (int)this._i);
					ptr += Guid.HexsToChars(ptr, (int)this._j, (int)this._k);
				}
				if (num != 0)
				{
					*(ptr++) = (char)(num >> 16);
				}
			}
			charsWritten = num2;
			return true;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00028CFE File Offset: 0x00026EFE
		bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return this.TryFormat(destination, out charsWritten, format);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00028D0C File Offset: 0x00026F0C
		internal unsafe static byte[] FastNewGuidArray()
		{
			byte[] array = new byte[16];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Interop.GetRandomBytes(ptr, 16);
			array2 = null;
			array[8] = (array[8] & 63) | 128;
			array[7] = (array[7] & 15) | 64;
			return array;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00004088 File Offset: 0x00002288
		// Note: this type is marked as 'beforefieldinit'.
		static Guid()
		{
		}

		// Token: 0x04001036 RID: 4150
		public static readonly Guid Empty;

		// Token: 0x04001037 RID: 4151
		private int _a;

		// Token: 0x04001038 RID: 4152
		private short _b;

		// Token: 0x04001039 RID: 4153
		private short _c;

		// Token: 0x0400103A RID: 4154
		private byte _d;

		// Token: 0x0400103B RID: 4155
		private byte _e;

		// Token: 0x0400103C RID: 4156
		private byte _f;

		// Token: 0x0400103D RID: 4157
		private byte _g;

		// Token: 0x0400103E RID: 4158
		private byte _h;

		// Token: 0x0400103F RID: 4159
		private byte _i;

		// Token: 0x04001040 RID: 4160
		private byte _j;

		// Token: 0x04001041 RID: 4161
		private byte _k;

		// Token: 0x020000F7 RID: 247
		[Flags]
		private enum GuidStyles
		{
			// Token: 0x04001043 RID: 4163
			None = 0,
			// Token: 0x04001044 RID: 4164
			AllowParenthesis = 1,
			// Token: 0x04001045 RID: 4165
			AllowBraces = 2,
			// Token: 0x04001046 RID: 4166
			AllowDashes = 4,
			// Token: 0x04001047 RID: 4167
			AllowHexPrefix = 8,
			// Token: 0x04001048 RID: 4168
			RequireParenthesis = 16,
			// Token: 0x04001049 RID: 4169
			RequireBraces = 32,
			// Token: 0x0400104A RID: 4170
			RequireDashes = 64,
			// Token: 0x0400104B RID: 4171
			RequireHexPrefix = 128,
			// Token: 0x0400104C RID: 4172
			HexFormat = 160,
			// Token: 0x0400104D RID: 4173
			NumberFormat = 0,
			// Token: 0x0400104E RID: 4174
			DigitFormat = 64,
			// Token: 0x0400104F RID: 4175
			BraceFormat = 96,
			// Token: 0x04001050 RID: 4176
			ParenthesisFormat = 80,
			// Token: 0x04001051 RID: 4177
			Any = 15
		}

		// Token: 0x020000F8 RID: 248
		private enum GuidParseThrowStyle
		{
			// Token: 0x04001053 RID: 4179
			None,
			// Token: 0x04001054 RID: 4180
			All,
			// Token: 0x04001055 RID: 4181
			AllButOverflow
		}

		// Token: 0x020000F9 RID: 249
		private enum ParseFailureKind
		{
			// Token: 0x04001057 RID: 4183
			None,
			// Token: 0x04001058 RID: 4184
			ArgumentNull,
			// Token: 0x04001059 RID: 4185
			Format,
			// Token: 0x0400105A RID: 4186
			FormatWithParameter,
			// Token: 0x0400105B RID: 4187
			NativeException,
			// Token: 0x0400105C RID: 4188
			FormatWithInnerException
		}

		// Token: 0x020000FA RID: 250
		private struct GuidResult
		{
			// Token: 0x060009F8 RID: 2552 RVA: 0x00028D61 File Offset: 0x00026F61
			internal void Init(Guid.GuidParseThrowStyle canThrow)
			{
				this._throwStyle = canThrow;
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x00028D6A File Offset: 0x00026F6A
			internal void SetFailure(Exception nativeException)
			{
				this._failure = Guid.ParseFailureKind.NativeException;
				this._innerException = nativeException;
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x00028D7A File Offset: 0x00026F7A
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID)
			{
				this.SetFailure(failure, failureMessageID, null, null, null);
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x00028D87 File Offset: 0x00026F87
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, null, null);
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x00028D94 File Offset: 0x00026F94
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName, Exception innerException)
			{
				this._failure = failure;
				this._failureMessageID = failureMessageID;
				this._failureMessageFormatArgument = failureMessageFormatArgument;
				this._failureArgumentName = failureArgumentName;
				this._innerException = innerException;
				if (this._throwStyle != Guid.GuidParseThrowStyle.None)
				{
					throw this.GetGuidParseException();
				}
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x00028DCC File Offset: 0x00026FCC
			internal Exception GetGuidParseException()
			{
				switch (this._failure)
				{
				case Guid.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this._failureArgumentName, SR.GetResourceString(this._failureMessageID));
				case Guid.ParseFailureKind.Format:
					return new FormatException(SR.GetResourceString(this._failureMessageID));
				case Guid.ParseFailureKind.FormatWithParameter:
					return new FormatException(SR.Format(SR.GetResourceString(this._failureMessageID), this._failureMessageFormatArgument));
				case Guid.ParseFailureKind.NativeException:
					return this._innerException;
				case Guid.ParseFailureKind.FormatWithInnerException:
					return new FormatException(SR.GetResourceString(this._failureMessageID), this._innerException);
				default:
					return new FormatException("Unrecognized Guid format.");
				}
			}

			// Token: 0x0400105D RID: 4189
			internal Guid _parsedGuid;

			// Token: 0x0400105E RID: 4190
			internal Guid.GuidParseThrowStyle _throwStyle;

			// Token: 0x0400105F RID: 4191
			private Guid.ParseFailureKind _failure;

			// Token: 0x04001060 RID: 4192
			private string _failureMessageID;

			// Token: 0x04001061 RID: 4193
			private object _failureMessageFormatArgument;

			// Token: 0x04001062 RID: 4194
			private string _failureArgumentName;

			// Token: 0x04001063 RID: 4195
			private Exception _innerException;
		}
	}
}
