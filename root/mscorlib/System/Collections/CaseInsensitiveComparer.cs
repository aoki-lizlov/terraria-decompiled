using System;
using System.Globalization;

namespace System.Collections
{
	// Token: 0x02000A7A RID: 2682
	[Serializable]
	public class CaseInsensitiveComparer : IComparer
	{
		// Token: 0x060061D7 RID: 25047 RVA: 0x0014E42E File Offset: 0x0014C62E
		public CaseInsensitiveComparer()
		{
			this._compareInfo = CultureInfo.CurrentCulture.CompareInfo;
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x0014E446 File Offset: 0x0014C646
		public CaseInsensitiveComparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this._compareInfo = culture.CompareInfo;
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x060061D9 RID: 25049 RVA: 0x0014E468 File Offset: 0x0014C668
		public static CaseInsensitiveComparer Default
		{
			get
			{
				return new CaseInsensitiveComparer(CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x060061DA RID: 25050 RVA: 0x0014E474 File Offset: 0x0014C674
		public static CaseInsensitiveComparer DefaultInvariant
		{
			get
			{
				if (CaseInsensitiveComparer.s_InvariantCaseInsensitiveComparer == null)
				{
					CaseInsensitiveComparer.s_InvariantCaseInsensitiveComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CaseInsensitiveComparer.s_InvariantCaseInsensitiveComparer;
			}
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x0014E498 File Offset: 0x0014C698
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this._compareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x04003AAF RID: 15023
		private CompareInfo _compareInfo;

		// Token: 0x04003AB0 RID: 15024
		private static volatile CaseInsensitiveComparer s_InvariantCaseInsensitiveComparer;
	}
}
