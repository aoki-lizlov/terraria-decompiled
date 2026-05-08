using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using CsvHelper;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Graphics;
using ReLogic.Utilities;
using Terraria.Utilities;

namespace Terraria.Localization
{
	// Token: 0x02000189 RID: 393
	public class LanguageManager
	{
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06001EA7 RID: 7847 RVA: 0x0051077C File Offset: 0x0050E97C
		// (remove) Token: 0x06001EA8 RID: 7848 RVA: 0x005107B4 File Offset: 0x0050E9B4
		public event LanguageChangeCallback OnLanguageChanged
		{
			[CompilerGenerated]
			add
			{
				LanguageChangeCallback languageChangeCallback = this.OnLanguageChanged;
				LanguageChangeCallback languageChangeCallback2;
				do
				{
					languageChangeCallback2 = languageChangeCallback;
					LanguageChangeCallback languageChangeCallback3 = (LanguageChangeCallback)Delegate.Combine(languageChangeCallback2, value);
					languageChangeCallback = Interlocked.CompareExchange<LanguageChangeCallback>(ref this.OnLanguageChanged, languageChangeCallback3, languageChangeCallback2);
				}
				while (languageChangeCallback != languageChangeCallback2);
			}
			[CompilerGenerated]
			remove
			{
				LanguageChangeCallback languageChangeCallback = this.OnLanguageChanged;
				LanguageChangeCallback languageChangeCallback2;
				do
				{
					languageChangeCallback2 = languageChangeCallback;
					LanguageChangeCallback languageChangeCallback3 = (LanguageChangeCallback)Delegate.Remove(languageChangeCallback2, value);
					languageChangeCallback = Interlocked.CompareExchange<LanguageChangeCallback>(ref this.OnLanguageChanged, languageChangeCallback3, languageChangeCallback2);
				}
				while (languageChangeCallback != languageChangeCallback2);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x005107E9 File Offset: 0x0050E9E9
		// (set) Token: 0x06001EAA RID: 7850 RVA: 0x005107F1 File Offset: 0x0050E9F1
		public GameCulture ActiveCulture
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveCulture>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActiveCulture>k__BackingField = value;
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x005107FC File Offset: 0x0050E9FC
		private LanguageManager()
		{
			this._localizedTexts[""] = LocalizedText.Empty;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0051085C File Offset: 0x0050EA5C
		public int GetCategorySize(string name)
		{
			List<string> list;
			if (this._categoryGroupedKeys.TryGetValue(name, out list))
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00510884 File Offset: 0x0050EA84
		public void SetLanguage(int legacyId)
		{
			GameCulture gameCulture = GameCulture.FromLegacyId(legacyId);
			this.SetLanguage(gameCulture);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x005108A0 File Offset: 0x0050EAA0
		public void SetLanguage(string cultureName)
		{
			GameCulture gameCulture = GameCulture.FromName(cultureName);
			this.SetLanguage(gameCulture);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x005108BC File Offset: 0x0050EABC
		public void EstimateWordCount()
		{
			string[] array = (from word in this._localizedTexts.Values.Select((LocalizedText v) => v.UnformattedValue).SelectMany((string text) => text.Split(new char[] { ' ', '\n', '-', ',' }))
				where !string.IsNullOrWhiteSpace(word) && !word.StartsWith("{") && !word.EndsWith("}")
				select word).ToArray<string>();
			(from w in array.Distinct<string>()
				orderby w.Length
				select w).ToArray<string>();
			Trace.WriteLine("Estimated word count: " + array.Length);
			Trace.WriteLine("Excluding one letter words: " + array.Where((string w) => w.Length > 1).Count<string>());
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x005109CC File Offset: 0x0050EBCC
		private void SetAllTextValuesToKeys()
		{
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				keyValuePair.Value.SetValue(keyValuePair.Key);
			}
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00510A2C File Offset: 0x0050EC2C
		private string[] GetLanguageFilesForCulture(GameCulture culture)
		{
			Assembly.GetExecutingAssembly();
			return Array.FindAll<string>(typeof(Program).Assembly.GetManifestResourceNames(), (string element) => element.StartsWith("Terraria.Localization.Content." + culture.CultureInfo.Name) && element.EndsWith(".json"));
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00510A71 File Offset: 0x0050EC71
		public void SetLanguage(GameCulture culture)
		{
			if (this.ActiveCulture == culture)
			{
				return;
			}
			Thread.CurrentThread.CurrentCulture = culture.CultureInfo;
			Thread.CurrentThread.CurrentUICulture = culture.CultureInfo;
			this.ReloadLanguage(culture);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00510AA4 File Offset: 0x0050ECA4
		private void ReloadLanguage(GameCulture targetCulture)
		{
			if (this.ActiveCulture != this._fallbackCulture)
			{
				this.SetAllTextValuesToKeys();
				if (targetCulture != this._fallbackCulture)
				{
					this.LoadLanguage(this._fallbackCulture);
				}
			}
			this.LoadLanguage(targetCulture);
			if (this.OnLanguageChanged != null)
			{
				this.OnLanguageChanged(this);
			}
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00510AF5 File Offset: 0x0050ECF5
		private void LoadLanguage(GameCulture culture)
		{
			this.ActiveCulture = culture;
			this._textVariations.Clear();
			this.LoadFilesForCulture(culture);
			this.LoadFromContentSources();
			this.ProcessCopyCommandsInTexts();
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00510B1C File Offset: 0x0050ED1C
		private void LoadFilesForCulture(GameCulture culture)
		{
			foreach (string text in this.GetLanguageFilesForCulture(culture))
			{
				try
				{
					string text2 = null;
					if (text2 == null)
					{
						text2 = Utils.ReadEmbeddedResource(text);
					}
					if (text2 == null || text2.Length < 2)
					{
						throw new FormatException();
					}
					this.LoadLanguageFromFileTextJson(text2, true);
				}
				catch (Exception)
				{
					if (Debugger.IsAttached)
					{
						Debugger.Break();
					}
					Console.WriteLine("Failed to load language file: " + text);
					break;
				}
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00510B9C File Offset: 0x0050ED9C
		private void ProcessCopyCommandsInTexts()
		{
			Regex regex = new Regex("{\\$(\\w+\\.\\w+)}", RegexOptions.Compiled);
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				LocalizedText value = keyValuePair.Value;
				for (int i = 0; i < 100; i++)
				{
					string unformattedValue = value.UnformattedValue;
					string text = regex.Replace(unformattedValue, delegate(Match match)
					{
						string text2 = match.Groups[1].ToString();
						LocalizedText localizedText;
						if (!this._localizedTexts.TryGetValue(text2, out localizedText))
						{
							return text2;
						}
						return localizedText.UnformattedValue;
					});
					if (text == unformattedValue)
					{
						break;
					}
					value.SetValue(text);
				}
			}
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00510C40 File Offset: 0x0050EE40
		public void UseSources(List<IContentSource> sourcesFromLowestToHighest)
		{
			this._contentSources = sourcesFromLowestToHighest;
			this.ReloadLanguage(this.ActiveCulture);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00510C58 File Offset: 0x0050EE58
		private void LoadFromContentSources()
		{
			string name = this.ActiveCulture.Name;
			string text = ("Localization" + Path.DirectorySeparatorChar.ToString() + name).ToLower();
			foreach (IContentSource contentSource in this._contentSources)
			{
				foreach (string text2 in contentSource.GetAllAssetsStartingWith(text))
				{
					string extension = contentSource.GetExtension(text2);
					if (extension == ".json" || extension == ".csv")
					{
						try
						{
							using (Stream stream = contentSource.OpenStream(text2))
							{
								using (StreamReader streamReader = new StreamReader(stream))
								{
									string text3 = streamReader.ReadToEnd();
									if (extension == ".json")
									{
										this.LoadLanguageFromFileTextJson(text3, false);
									}
									if (extension == ".csv")
									{
										this.LoadLanguageFromFileTextCsv(text3);
									}
								}
							}
						}
						catch (Exception ex)
						{
							IAssetRepository assetRepository = XnaExtensions.Get<IAssetRepository>(Main.instance.Services);
							if (assetRepository != null && assetRepository.AssetLoadFailHandler != null)
							{
								string text4 = text2 + extension;
								assetRepository.AssetLoadFailHandler.Invoke(text4, AssetLoadException.FromAssetException(text4, ex));
							}
						}
					}
				}
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00510E54 File Offset: 0x0050F054
		public void LoadLanguageFromFileTextCsv(string fileText)
		{
			using (TextReader textReader = new StringReader(fileText))
			{
				using (CsvReader csvReader = new CsvReader(textReader))
				{
					csvReader.Configuration.HasHeaderRecord = true;
					if (csvReader.ReadHeader())
					{
						string[] fieldHeaders = csvReader.FieldHeaders;
						int num = -1;
						int num2 = -1;
						for (int i = 0; i < fieldHeaders.Length; i++)
						{
							string text = fieldHeaders[i].ToLower();
							if (text == "translation")
							{
								num2 = i;
							}
							if (text == "key")
							{
								num = i;
							}
						}
						if (num != -1 && num2 != -1)
						{
							int num3 = Math.Max(num, num2) + 1;
							while (csvReader.Read())
							{
								string[] currentRecord = csvReader.CurrentRecord;
								if (currentRecord.Length >= num3)
								{
									string text2 = currentRecord[num];
									string text3 = currentRecord[num2];
									if (!string.IsNullOrWhiteSpace(text2) && !string.IsNullOrWhiteSpace(text3))
									{
										this.UpdateTextValue(text2, text3);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00510F5C File Offset: 0x0050F15C
		public void LoadLanguageFromFileTextJson(string fileText, bool canCreateCategories)
		{
			foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(fileText))
			{
				string key = keyValuePair.Key;
				foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair.Value)
				{
					string text = keyValuePair.Key + "." + keyValuePair2.Key;
					if (!this.UpdateTextValue(text, keyValuePair2.Value) && canCreateCategories)
					{
						this._localizedTexts.Add(text, new LocalizedText(text, keyValuePair2.Value));
						List<string> list;
						if (!this._categoryGroupedKeys.TryGetValue(keyValuePair.Key, out list))
						{
							this._categoryGroupedKeys.Add(keyValuePair.Key, list = new List<string>());
						}
						list.Add(keyValuePair2.Key);
					}
				}
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00511084 File Offset: 0x0050F284
		private bool UpdateTextValue(string key, string value)
		{
			if (key.Contains('$'))
			{
				string[] array = key.Split(new char[] { '$' });
				this.AddVariant(array[0], array[1], value);
				return true;
			}
			LocalizedText localizedText;
			if (this._localizedTexts.TryGetValue(key, out localizedText))
			{
				localizedText.SetValue(value);
				return true;
			}
			return false;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x005110D8 File Offset: 0x0050F2D8
		public bool HotReloadContentFile(IContentSource contentSource, string path, string fullPath)
		{
			path = path.Replace('\\', '/');
			if (!path.StartsWith("Localization/"))
			{
				return false;
			}
			string text = File.ReadAllText(fullPath);
			if (path.EndsWith(".json"))
			{
				JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(text);
			}
			else if (!path.EndsWith(".csv"))
			{
				return false;
			}
			if (contentSource == null)
			{
				return false;
			}
			this.ReloadLanguage(this.ActiveCulture);
			return true;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00511140 File Offset: 0x0050F340
		[Conditional("DEBUG")]
		private void ValidateAllCharactersContainedInFont(DynamicSpriteFont font)
		{
			if (font == null)
			{
				return;
			}
			string text = "";
			foreach (LocalizedText localizedText in this._localizedTexts.Values)
			{
				foreach (char c in localizedText.Value)
				{
					if (!font.IsCharacterSupported(c))
					{
						text = string.Concat(new object[]
						{
							text,
							localizedText.Key,
							", ",
							c.ToString(),
							", ",
							(int)c,
							"\n"
						});
					}
				}
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00511214 File Offset: 0x0050F414
		public LocalizedText[] FindAll(Regex regex)
		{
			int num = 0;
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				if (regex.IsMatch(keyValuePair.Key))
				{
					num++;
				}
			}
			LocalizedText[] array = new LocalizedText[num];
			int num2 = 0;
			foreach (KeyValuePair<string, LocalizedText> keyValuePair2 in this._localizedTexts)
			{
				if (regex.IsMatch(keyValuePair2.Key))
				{
					array[num2] = keyValuePair2.Value;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x005112DC File Offset: 0x0050F4DC
		public LocalizedText[] FindAll(LanguageSearchFilter filter)
		{
			LinkedList<LocalizedText> linkedList = new LinkedList<LocalizedText>();
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				if (filter(keyValuePair.Key, keyValuePair.Value))
				{
					linkedList.AddLast(keyValuePair.Value);
				}
			}
			return linkedList.ToArray<LocalizedText>();
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00511358 File Offset: 0x0050F558
		public LocalizedText SelectRandom(LanguageSearchFilter filter, UnifiedRandom random = null)
		{
			int num = 0;
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				if (filter(keyValuePair.Key, keyValuePair.Value))
				{
					num++;
				}
			}
			int num2 = (random ?? Main.rand).Next(num);
			foreach (KeyValuePair<string, LocalizedText> keyValuePair2 in this._localizedTexts)
			{
				if (filter(keyValuePair2.Key, keyValuePair2.Value) && --num == num2)
				{
					return keyValuePair2.Value;
				}
			}
			return LocalizedText.Empty;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00511440 File Offset: 0x0050F640
		public LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
		{
			List<string> list;
			if (!this._categoryGroupedKeys.TryGetValue(categoryName, out list))
			{
				return new LocalizedText(categoryName + ".RANDOM", categoryName + ".RANDOM");
			}
			return this.GetText(categoryName + "." + list[(random ?? Main.rand).Next(list.Count)]);
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x005114A8 File Offset: 0x0050F6A8
		public LocalizedText IndexedFromCategory(string categoryName, int index)
		{
			List<string> list;
			if (!this._categoryGroupedKeys.TryGetValue(categoryName, out list))
			{
				return new LocalizedText(categoryName + ".INDEXED", categoryName + ".INDEXED");
			}
			int num = index % list.Count;
			return this.GetText(categoryName + "." + list[num]);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00511502 File Offset: 0x0050F702
		public bool Exists(string key)
		{
			return this._localizedTexts.ContainsKey(key);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00511510 File Offset: 0x0050F710
		public LocalizedText GetText(string key)
		{
			LocalizedText localizedText;
			if (this._localizedTexts.TryGetValue(key, out localizedText))
			{
				return localizedText;
			}
			return this._localizedTexts[key] = new LocalizedText(key, key);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00511548 File Offset: 0x0050F748
		public string GetTextValue(string key)
		{
			LocalizedText localizedText;
			if (this._localizedTexts.TryGetValue(key, out localizedText))
			{
				return localizedText.Value;
			}
			return key;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00511570 File Offset: 0x0050F770
		public string GetTextValue(string key, object arg0)
		{
			LocalizedText localizedText;
			if (this._localizedTexts.TryGetValue(key, out localizedText))
			{
				return localizedText.Format(arg0);
			}
			return key;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00511598 File Offset: 0x0050F798
		public string GetTextValue(string key, object arg0, object arg1)
		{
			LocalizedText localizedText;
			if (this._localizedTexts.TryGetValue(key, out localizedText))
			{
				return localizedText.Format(arg0, arg1);
			}
			return key;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x005115C0 File Offset: 0x0050F7C0
		public string GetTextValue(string key, object arg0, object arg1, object arg2)
		{
			LocalizedText localizedText;
			if (this._localizedTexts.TryGetValue(key, out localizedText))
			{
				return localizedText.Format(arg0, arg1, arg2);
			}
			return key;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x005115EC File Offset: 0x0050F7EC
		public string GetTextValue(string key, params object[] args)
		{
			LocalizedText localizedText;
			if (this._localizedTexts.TryGetValue(key, out localizedText))
			{
				return localizedText.Format(args);
			}
			return key;
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00511614 File Offset: 0x0050F814
		private void AddVariant(string key, string variant, string value)
		{
			Dictionary<string, string> dictionary;
			if (!this._textVariations.TryGetValue(key, out dictionary))
			{
				dictionary = (this._textVariations[key] = new Dictionary<string, string>());
			}
			dictionary[variant] = value;
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0051164C File Offset: 0x0050F84C
		public bool TryGetVariation(string key, string variant, out string value)
		{
			value = null;
			Dictionary<string, string> dictionary;
			return this._textVariations.TryGetValue(key, out dictionary) && dictionary.TryGetValue(variant, out value);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00511676 File Offset: 0x0050F876
		public void SetFallbackCulture(GameCulture culture)
		{
			this._fallbackCulture = culture;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0051167F File Offset: 0x0050F87F
		// Note: this type is marked as 'beforefieldinit'.
		static LanguageManager()
		{
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x0051168C File Offset: 0x0050F88C
		[CompilerGenerated]
		private string <ProcessCopyCommandsInTexts>b__25_0(Match match)
		{
			string text = match.Groups[1].ToString();
			LocalizedText localizedText;
			if (!this._localizedTexts.TryGetValue(text, out localizedText))
			{
				return text;
			}
			return localizedText.UnformattedValue;
		}

		// Token: 0x040016EB RID: 5867
		public static LanguageManager Instance = new LanguageManager();

		// Token: 0x040016EC RID: 5868
		[CompilerGenerated]
		private LanguageChangeCallback OnLanguageChanged;

		// Token: 0x040016ED RID: 5869
		[CompilerGenerated]
		private GameCulture <ActiveCulture>k__BackingField;

		// Token: 0x040016EE RID: 5870
		private readonly Dictionary<string, LocalizedText> _localizedTexts = new Dictionary<string, LocalizedText>();

		// Token: 0x040016EF RID: 5871
		private readonly Dictionary<string, List<string>> _categoryGroupedKeys = new Dictionary<string, List<string>>();

		// Token: 0x040016F0 RID: 5872
		private readonly Dictionary<string, Dictionary<string, string>> _textVariations = new Dictionary<string, Dictionary<string, string>>();

		// Token: 0x040016F1 RID: 5873
		private GameCulture _fallbackCulture = GameCulture.DefaultCulture;

		// Token: 0x040016F2 RID: 5874
		private List<IContentSource> _contentSources = new List<IContentSource>();

		// Token: 0x040016F3 RID: 5875
		public const char VariationSeparatorSign = '$';

		// Token: 0x02000757 RID: 1879
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060040F5 RID: 16629 RVA: 0x0069EFBF File Offset: 0x0069D1BF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060040F6 RID: 16630 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060040F7 RID: 16631 RVA: 0x0069EFCB File Offset: 0x0069D1CB
			internal string <EstimateWordCount>b__18_0(LocalizedText v)
			{
				return v.UnformattedValue;
			}

			// Token: 0x060040F8 RID: 16632 RVA: 0x0069EFD3 File Offset: 0x0069D1D3
			internal IEnumerable<string> <EstimateWordCount>b__18_1(string text)
			{
				return text.Split(new char[] { ' ', '\n', '-', ',' });
			}

			// Token: 0x060040F9 RID: 16633 RVA: 0x0069EFEC File Offset: 0x0069D1EC
			internal bool <EstimateWordCount>b__18_2(string word)
			{
				return !string.IsNullOrWhiteSpace(word) && !word.StartsWith("{") && !word.EndsWith("}");
			}

			// Token: 0x060040FA RID: 16634 RVA: 0x0069F013 File Offset: 0x0069D213
			internal int <EstimateWordCount>b__18_3(string w)
			{
				return w.Length;
			}

			// Token: 0x060040FB RID: 16635 RVA: 0x0069F01B File Offset: 0x0069D21B
			internal bool <EstimateWordCount>b__18_4(string w)
			{
				return w.Length > 1;
			}

			// Token: 0x040069F2 RID: 27122
			public static readonly LanguageManager.<>c <>9 = new LanguageManager.<>c();

			// Token: 0x040069F3 RID: 27123
			public static Func<LocalizedText, string> <>9__18_0;

			// Token: 0x040069F4 RID: 27124
			public static Func<string, IEnumerable<string>> <>9__18_1;

			// Token: 0x040069F5 RID: 27125
			public static Func<string, bool> <>9__18_2;

			// Token: 0x040069F6 RID: 27126
			public static Func<string, int> <>9__18_3;

			// Token: 0x040069F7 RID: 27127
			public static Func<string, bool> <>9__18_4;
		}

		// Token: 0x02000758 RID: 1880
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x060040FC RID: 16636 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x060040FD RID: 16637 RVA: 0x0069F026 File Offset: 0x0069D226
			internal bool <GetLanguageFilesForCulture>b__0(string element)
			{
				return element.StartsWith("Terraria.Localization.Content." + this.culture.CultureInfo.Name) && element.EndsWith(".json");
			}

			// Token: 0x040069F8 RID: 27128
			public GameCulture culture;
		}
	}
}
