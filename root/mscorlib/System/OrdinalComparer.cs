using System;
using System.Globalization;

namespace System
{
	// Token: 0x02000153 RID: 339
	[Serializable]
	public class OrdinalComparer : StringComparer
	{
		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003D401 File Offset: 0x0003B601
		internal OrdinalComparer(bool ignoreCase)
		{
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003D410 File Offset: 0x0003B610
		public override int Compare(string x, string y)
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
			if (this._ignoreCase)
			{
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
			}
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003D43A File Offset: 0x0003B63A
		public override bool Equals(string x, string y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (this._ignoreCase)
			{
				return x.Length == y.Length && string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return x.Equals(y);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003D475 File Offset: 0x0003B675
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			if (this._ignoreCase)
			{
				return CompareInfo.GetIgnoreCaseHash(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003D498 File Offset: 0x0003B698
		public override bool Equals(object obj)
		{
			OrdinalComparer ordinalComparer = obj as OrdinalComparer;
			return ordinalComparer != null && this._ignoreCase == ordinalComparer._ignoreCase;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003D4C0 File Offset: 0x0003B6C0
		public override int GetHashCode()
		{
			int hashCode = "OrdinalComparer".GetHashCode();
			if (!this._ignoreCase)
			{
				return hashCode;
			}
			return ~hashCode;
		}

		// Token: 0x04001184 RID: 4484
		private readonly bool _ignoreCase;
	}
}
