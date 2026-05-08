using System;
using System.Globalization;

namespace System.Collections
{
	// Token: 0x02000A7B RID: 2683
	[Obsolete("Please use StringComparer instead.")]
	[Serializable]
	public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
	{
		// Token: 0x060061DC RID: 25052 RVA: 0x0014E4D4 File Offset: 0x0014C6D4
		public CaseInsensitiveHashCodeProvider()
		{
			this._compareInfo = CultureInfo.CurrentCulture.CompareInfo;
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x0014E4EC File Offset: 0x0014C6EC
		public CaseInsensitiveHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this._compareInfo = culture.CompareInfo;
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x060061DE RID: 25054 RVA: 0x0014E50E File Offset: 0x0014C70E
		public static CaseInsensitiveHashCodeProvider Default
		{
			get
			{
				return new CaseInsensitiveHashCodeProvider();
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x060061DF RID: 25055 RVA: 0x0014E515 File Offset: 0x0014C715
		public static CaseInsensitiveHashCodeProvider DefaultInvariant
		{
			get
			{
				CaseInsensitiveHashCodeProvider caseInsensitiveHashCodeProvider;
				if ((caseInsensitiveHashCodeProvider = CaseInsensitiveHashCodeProvider.s_invariantCaseInsensitiveHashCodeProvider) == null)
				{
					caseInsensitiveHashCodeProvider = (CaseInsensitiveHashCodeProvider.s_invariantCaseInsensitiveHashCodeProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture));
				}
				return caseInsensitiveHashCodeProvider;
			}
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x0014E534 File Offset: 0x0014C734
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			return this._compareInfo.GetHashCode(text, CompareOptions.IgnoreCase);
		}

		// Token: 0x04003AB1 RID: 15025
		private static volatile CaseInsensitiveHashCodeProvider s_invariantCaseInsensitiveHashCodeProvider;

		// Token: 0x04003AB2 RID: 15026
		private readonly CompareInfo _compareInfo;
	}
}
