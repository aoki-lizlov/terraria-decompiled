using System;
using System.Text.RegularExpressions;
using Terraria.Utilities;

namespace Terraria.Localization
{
	// Token: 0x02000188 RID: 392
	public static class Language
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x005106A8 File Offset: 0x0050E8A8
		public static GameCulture ActiveCulture
		{
			get
			{
				return LanguageManager.Instance.ActiveCulture;
			}
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x005106B4 File Offset: 0x0050E8B4
		public static LocalizedText GetText(string key)
		{
			return LanguageManager.Instance.GetText(key);
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x005106C1 File Offset: 0x0050E8C1
		public static string GetTextValue(string key)
		{
			return LanguageManager.Instance.GetTextValue(key);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x005106CE File Offset: 0x0050E8CE
		public static string GetTextValue(string key, object arg0)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x005106DC File Offset: 0x0050E8DC
		public static string GetTextValue(string key, object arg0, object arg1)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0, arg1);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x005106EB File Offset: 0x0050E8EB
		public static string GetTextValue(string key, object arg0, object arg1, object arg2)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0, arg1, arg2);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x005106FB File Offset: 0x0050E8FB
		public static string GetTextValue(string key, params object[] args)
		{
			return LanguageManager.Instance.GetTextValue(key, args);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00510709 File Offset: 0x0050E909
		public static string GetTextValueWith(string key, object obj)
		{
			return LanguageManager.Instance.GetText(key).FormatWith(obj);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0051071C File Offset: 0x0050E91C
		public static bool Exists(string key)
		{
			return LanguageManager.Instance.Exists(key);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00510729 File Offset: 0x0050E929
		public static int GetCategorySize(string key)
		{
			return LanguageManager.Instance.GetCategorySize(key);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00510736 File Offset: 0x0050E936
		public static LocalizedText[] FindAll(Regex regex)
		{
			return LanguageManager.Instance.FindAll(regex);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00510743 File Offset: 0x0050E943
		public static LocalizedText[] FindAll(LanguageSearchFilter filter)
		{
			return LanguageManager.Instance.FindAll(filter);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00510750 File Offset: 0x0050E950
		public static LocalizedText SelectRandom(LanguageSearchFilter filter, UnifiedRandom random = null)
		{
			return LanguageManager.Instance.SelectRandom(filter, random);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0051075E File Offset: 0x0050E95E
		public static LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
		{
			return LanguageManager.Instance.RandomFromCategory(categoryName, random);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0051076C File Offset: 0x0050E96C
		public static bool TryGetVariation(string key, string variant, out string value)
		{
			return LanguageManager.Instance.TryGetVariation(key, variant, out value);
		}
	}
}
