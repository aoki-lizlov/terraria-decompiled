using System;
using System.Globalization;
using System.Text;

namespace System
{
	// Token: 0x0200018D RID: 397
	[Serializable]
	public sealed class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>, ISpanFormattable
	{
		// Token: 0x060012ED RID: 4845 RVA: 0x0004D9EC File Offset: 0x0004BBEC
		public Version(int major, int minor, int build, int revision)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", "Version's parameters must be greater than or equal to zero.");
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "Version's parameters must be greater than or equal to zero.");
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", "Version's parameters must be greater than or equal to zero.");
			}
			if (revision < 0)
			{
				throw new ArgumentOutOfRangeException("revision", "Version's parameters must be greater than or equal to zero.");
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
			this._Revision = revision;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004DA7C File Offset: 0x0004BC7C
		public Version(int major, int minor, int build)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", "Version's parameters must be greater than or equal to zero.");
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "Version's parameters must be greater than or equal to zero.");
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", "Version's parameters must be greater than or equal to zero.");
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0004DAF0 File Offset: 0x0004BCF0
		public Version(int major, int minor)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", "Version's parameters must be greater than or equal to zero.");
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", "Version's parameters must be greater than or equal to zero.");
			}
			this._Major = major;
			this._Minor = minor;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004DB48 File Offset: 0x0004BD48
		public Version(string version)
		{
			Version version2 = Version.Parse(version);
			this._Major = version2.Major;
			this._Minor = version2.Minor;
			this._Build = version2.Build;
			this._Revision = version2.Revision;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0004DBA0 File Offset: 0x0004BDA0
		public Version()
		{
			this._Major = 0;
			this._Minor = 0;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004DBC4 File Offset: 0x0004BDC4
		private Version(Version version)
		{
			this._Major = version._Major;
			this._Minor = version._Minor;
			this._Build = version._Build;
			this._Revision = version._Revision;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004DC15 File Offset: 0x0004BE15
		public object Clone()
		{
			return new Version(this);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0004DC1D File Offset: 0x0004BE1D
		public int Major
		{
			get
			{
				return this._Major;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0004DC25 File Offset: 0x0004BE25
		public int Minor
		{
			get
			{
				return this._Minor;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0004DC2D File Offset: 0x0004BE2D
		public int Build
		{
			get
			{
				return this._Build;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0004DC35 File Offset: 0x0004BE35
		public int Revision
		{
			get
			{
				return this._Revision;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0004DC3D File Offset: 0x0004BE3D
		public short MajorRevision
		{
			get
			{
				return (short)(this._Revision >> 16);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0004DC49 File Offset: 0x0004BE49
		public short MinorRevision
		{
			get
			{
				return (short)(this._Revision & 65535);
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004DC58 File Offset: 0x0004BE58
		public int CompareTo(object version)
		{
			if (version == null)
			{
				return 1;
			}
			Version version2 = version as Version;
			if (version2 == null)
			{
				throw new ArgumentException("Object must be of type Version.");
			}
			return this.CompareTo(version2);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004DC8C File Offset: 0x0004BE8C
		public int CompareTo(Version value)
		{
			if (value == this)
			{
				return 0;
			}
			if (value == null)
			{
				return 1;
			}
			if (this._Major == value._Major)
			{
				if (this._Minor == value._Minor)
				{
					if (this._Build == value._Build)
					{
						if (this._Revision == value._Revision)
						{
							return 0;
						}
						if (this._Revision <= value._Revision)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						if (this._Build <= value._Build)
						{
							return -1;
						}
						return 1;
					}
				}
				else
				{
					if (this._Minor <= value._Minor)
					{
						return -1;
					}
					return 1;
				}
			}
			else
			{
				if (this._Major <= value._Major)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004DD2B File Offset: 0x0004BF2B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Version);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004DD3C File Offset: 0x0004BF3C
		public bool Equals(Version obj)
		{
			return obj == this || (obj != null && this._Major == obj._Major && this._Minor == obj._Minor && this._Build == obj._Build && this._Revision == obj._Revision);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004DD8C File Offset: 0x0004BF8C
		public override int GetHashCode()
		{
			return 0 | ((this._Major & 15) << 28) | ((this._Minor & 255) << 20) | ((this._Build & 255) << 12) | (this._Revision & 4095);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004DDC9 File Offset: 0x0004BFC9
		public override string ToString()
		{
			return this.ToString(this.DefaultFormatFieldCount);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004DDD7 File Offset: 0x0004BFD7
		public string ToString(int fieldCount)
		{
			if (fieldCount == 0)
			{
				return string.Empty;
			}
			if (fieldCount != 1)
			{
				return StringBuilderCache.GetStringAndRelease(this.ToCachedStringBuilder(fieldCount));
			}
			return this._Major.ToString();
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004DDFE File Offset: 0x0004BFFE
		public bool TryFormat(Span<char> destination, out int charsWritten)
		{
			return this.TryFormat(destination, this.DefaultFormatFieldCount, out charsWritten);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004DE10 File Offset: 0x0004C010
		public bool TryFormat(Span<char> destination, int fieldCount, out int charsWritten)
		{
			if (fieldCount == 0)
			{
				charsWritten = 0;
				return true;
			}
			if (fieldCount == 1)
			{
				return this._Major.TryFormat(destination, out charsWritten, default(ReadOnlySpan<char>), null);
			}
			StringBuilder stringBuilder = this.ToCachedStringBuilder(fieldCount);
			if (stringBuilder.Length <= destination.Length)
			{
				stringBuilder.CopyTo(0, destination, stringBuilder.Length);
				StringBuilderCache.Release(stringBuilder);
				charsWritten = stringBuilder.Length;
				return true;
			}
			StringBuilderCache.Release(stringBuilder);
			charsWritten = 0;
			return false;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004DE80 File Offset: 0x0004C080
		bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return this.TryFormat(destination, out charsWritten);
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0004DE8A File Offset: 0x0004C08A
		private int DefaultFormatFieldCount
		{
			get
			{
				if (this._Build == -1)
				{
					return 2;
				}
				if (this._Revision != -1)
				{
					return 4;
				}
				return 3;
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004DEA4 File Offset: 0x0004C0A4
		private StringBuilder ToCachedStringBuilder(int fieldCount)
		{
			if (fieldCount == 2)
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(this._Major);
				stringBuilder.Append('.');
				stringBuilder.Append(this._Minor);
				return stringBuilder;
			}
			if (this._Build == -1)
			{
				throw new ArgumentException(SR.Format("Argument must be between {0} and {1}.", "0", "2"), "fieldCount");
			}
			if (fieldCount == 3)
			{
				StringBuilder stringBuilder2 = StringBuilderCache.Acquire(16);
				stringBuilder2.Append(this._Major);
				stringBuilder2.Append('.');
				stringBuilder2.Append(this._Minor);
				stringBuilder2.Append('.');
				stringBuilder2.Append(this._Build);
				return stringBuilder2;
			}
			if (this._Revision == -1)
			{
				throw new ArgumentException(SR.Format("Argument must be between {0} and {1}.", "0", "3"), "fieldCount");
			}
			if (fieldCount == 4)
			{
				StringBuilder stringBuilder3 = StringBuilderCache.Acquire(16);
				stringBuilder3.Append(this._Major);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Minor);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Build);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Revision);
				return stringBuilder3;
			}
			throw new ArgumentException(SR.Format("Argument must be between {0} and {1}.", "0", "4"), "fieldCount");
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004DFEE File Offset: 0x0004C1EE
		public static Version Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Version.ParseVersion(input.AsSpan(), true);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0004E00A File Offset: 0x0004C20A
		public static Version Parse(ReadOnlySpan<char> input)
		{
			return Version.ParseVersion(input, true);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004E014 File Offset: 0x0004C214
		public static bool TryParse(string input, out Version result)
		{
			if (input == null)
			{
				result = null;
				return false;
			}
			Version version;
			result = (version = Version.ParseVersion(input.AsSpan(), false));
			return version != null;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0004E040 File Offset: 0x0004C240
		public static bool TryParse(ReadOnlySpan<char> input, out Version result)
		{
			Version version;
			result = (version = Version.ParseVersion(input, false));
			return version != null;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004E060 File Offset: 0x0004C260
		private static Version ParseVersion(ReadOnlySpan<char> input, bool throwOnFailure)
		{
			int num = input.IndexOf('.');
			if (num < 0)
			{
				if (throwOnFailure)
				{
					throw new ArgumentException("Version string portion was too short or too long.", "input");
				}
				return null;
			}
			else
			{
				int num2 = -1;
				int num3 = input.Slice(num + 1).IndexOf('.');
				if (num3 != -1)
				{
					num3 += num + 1;
					num2 = input.Slice(num3 + 1).IndexOf('.');
					if (num2 != -1)
					{
						num2 += num3 + 1;
						if (input.Slice(num2 + 1).IndexOf('.') != -1)
						{
							if (throwOnFailure)
							{
								throw new ArgumentException("Version string portion was too short or too long.", "input");
							}
							return null;
						}
					}
				}
				int num4;
				if (!Version.TryParseComponent(input.Slice(0, num), "input", throwOnFailure, out num4))
				{
					return null;
				}
				if (num3 != -1)
				{
					int num5;
					if (!Version.TryParseComponent(input.Slice(num + 1, num3 - num - 1), "input", throwOnFailure, out num5))
					{
						return null;
					}
					if (num2 != -1)
					{
						int num6;
						int num7;
						if (!Version.TryParseComponent(input.Slice(num3 + 1, num2 - num3 - 1), "build", throwOnFailure, out num6) || !Version.TryParseComponent(input.Slice(num2 + 1), "revision", throwOnFailure, out num7))
						{
							return null;
						}
						return new Version(num4, num5, num6, num7);
					}
					else
					{
						int num6;
						if (!Version.TryParseComponent(input.Slice(num3 + 1), "build", throwOnFailure, out num6))
						{
							return null;
						}
						return new Version(num4, num5, num6);
					}
				}
				else
				{
					int num5;
					if (!Version.TryParseComponent(input.Slice(num + 1), "input", throwOnFailure, out num5))
					{
						return null;
					}
					return new Version(num4, num5);
				}
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0004E1C8 File Offset: 0x0004C3C8
		private static bool TryParseComponent(ReadOnlySpan<char> component, string componentName, bool throwOnFailure, out int parsedComponent)
		{
			if (!throwOnFailure)
			{
				return int.TryParse(component, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedComponent) && parsedComponent >= 0;
			}
			if ((parsedComponent = int.Parse(component, NumberStyles.Integer, CultureInfo.InvariantCulture)) < 0)
			{
				throw new ArgumentOutOfRangeException(componentName, "Version's parameters must be greater than or equal to zero.");
			}
			return true;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0004E213 File Offset: 0x0004C413
		public static bool operator ==(Version v1, Version v2)
		{
			if (v1 == null)
			{
				return v2 == null;
			}
			return v1.Equals(v2);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004E224 File Offset: 0x0004C424
		public static bool operator !=(Version v1, Version v2)
		{
			return !(v1 == v2);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0004E230 File Offset: 0x0004C430
		public static bool operator <(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) < 0;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0004E24A File Offset: 0x0004C44A
		public static bool operator <=(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) <= 0;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004E267 File Offset: 0x0004C467
		public static bool operator >(Version v1, Version v2)
		{
			return v2 < v1;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004E270 File Offset: 0x0004C470
		public static bool operator >=(Version v1, Version v2)
		{
			return v2 <= v1;
		}

		// Token: 0x04001272 RID: 4722
		private readonly int _Major;

		// Token: 0x04001273 RID: 4723
		private readonly int _Minor;

		// Token: 0x04001274 RID: 4724
		private readonly int _Build = -1;

		// Token: 0x04001275 RID: 4725
		private readonly int _Revision = -1;
	}
}
