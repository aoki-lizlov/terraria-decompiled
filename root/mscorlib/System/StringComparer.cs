using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System
{
	// Token: 0x02000151 RID: 337
	[Serializable]
	public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0003D07B File Offset: 0x0003B27B
		public static StringComparer InvariantCulture
		{
			get
			{
				return StringComparer.s_invariantCulture;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0003D082 File Offset: 0x0003B282
		public static StringComparer InvariantCultureIgnoreCase
		{
			get
			{
				return StringComparer.s_invariantCultureIgnoreCase;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0003D089 File Offset: 0x0003B289
		public static StringComparer CurrentCulture
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, CompareOptions.None);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0003D096 File Offset: 0x0003B296
		public static StringComparer CurrentCultureIgnoreCase
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0003D0A3 File Offset: 0x0003B2A3
		public static StringComparer Ordinal
		{
			get
			{
				return StringComparer.s_ordinal;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0003D0AA File Offset: 0x0003B2AA
		public static StringComparer OrdinalIgnoreCase
		{
			get
			{
				return StringComparer.s_ordinalIgnoreCase;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003D0B4 File Offset: 0x0003B2B4
		public static StringComparer FromComparison(StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return StringComparer.CurrentCulture;
			case StringComparison.CurrentCultureIgnoreCase:
				return StringComparer.CurrentCultureIgnoreCase;
			case StringComparison.InvariantCulture:
				return StringComparer.InvariantCulture;
			case StringComparison.InvariantCultureIgnoreCase:
				return StringComparer.InvariantCultureIgnoreCase;
			case StringComparison.Ordinal:
				return StringComparer.Ordinal;
			case StringComparison.OrdinalIgnoreCase:
				return StringComparer.OrdinalIgnoreCase;
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003D114 File Offset: 0x0003B314
		public static StringComparer Create(CultureInfo culture, bool ignoreCase)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003D131 File Offset: 0x0003B331
		public static StringComparer Create(CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentException("culture");
			}
			return new CultureAwareComparer(culture, options);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003D148 File Offset: 0x0003B348
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(y);
			}
			throw new ArgumentException("At least one object must implement IComparable.");
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003D1A0 File Offset: 0x0003B3A0
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003D1E0 File Offset: 0x0003B3E0
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text != null)
			{
				return this.GetHashCode(text);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000ED9 RID: 3801
		public abstract int Compare(string x, string y);

		// Token: 0x06000EDA RID: 3802
		public abstract bool Equals(string x, string y);

		// Token: 0x06000EDB RID: 3803
		public abstract int GetHashCode(string obj);

		// Token: 0x06000EDC RID: 3804 RVA: 0x000025BE File Offset: 0x000007BE
		protected StringComparer()
		{
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003D213 File Offset: 0x0003B413
		// Note: this type is marked as 'beforefieldinit'.
		static StringComparer()
		{
		}

		// Token: 0x0400117D RID: 4477
		private static readonly CultureAwareComparer s_invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, CompareOptions.None);

		// Token: 0x0400117E RID: 4478
		private static readonly CultureAwareComparer s_invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);

		// Token: 0x0400117F RID: 4479
		private static readonly OrdinalCaseSensitiveComparer s_ordinal = new OrdinalCaseSensitiveComparer();

		// Token: 0x04001180 RID: 4480
		private static readonly OrdinalIgnoreCaseComparer s_ordinalIgnoreCase = new OrdinalIgnoreCaseComparer();
	}
}
