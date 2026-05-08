using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CsvHelper.Configuration
{
	// Token: 0x02000037 RID: 55
	public class CsvConfiguration
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000070B8 File Offset: 0x000052B8
		public virtual CsvClassMapCollection Maps
		{
			get
			{
				return this.maps;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000070C0 File Offset: 0x000052C0
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000070C8 File Offset: 0x000052C8
		public virtual BindingFlags PropertyBindingFlags
		{
			get
			{
				return this.propertyBindingFlags;
			}
			set
			{
				this.propertyBindingFlags = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000070D1 File Offset: 0x000052D1
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000070D9 File Offset: 0x000052D9
		public virtual bool HasHeaderRecord
		{
			get
			{
				return this.hasHeaderRecord;
			}
			set
			{
				this.hasHeaderRecord = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000070E2 File Offset: 0x000052E2
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000070EA File Offset: 0x000052EA
		public virtual bool HasExcelSeparator
		{
			[CompilerGenerated]
			get
			{
				return this.<HasExcelSeparator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HasExcelSeparator>k__BackingField = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000070F3 File Offset: 0x000052F3
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x000070FB File Offset: 0x000052FB
		public virtual bool WillThrowOnMissingField
		{
			get
			{
				return this.willThrowOnMissingField;
			}
			set
			{
				this.willThrowOnMissingField = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007104 File Offset: 0x00005304
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000710C File Offset: 0x0000530C
		public virtual bool DetectColumnCountChanges
		{
			[CompilerGenerated]
			get
			{
				return this.<DetectColumnCountChanges>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DetectColumnCountChanges>k__BackingField = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007115 File Offset: 0x00005315
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000711D File Offset: 0x0000531D
		public virtual bool IsHeaderCaseSensitive
		{
			get
			{
				return this.isHeaderCaseSensitive;
			}
			set
			{
				this.isHeaderCaseSensitive = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007126 File Offset: 0x00005326
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000712E File Offset: 0x0000532E
		public virtual bool IgnoreHeaderWhiteSpace
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreHeaderWhiteSpace>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreHeaderWhiteSpace>k__BackingField = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007137 File Offset: 0x00005337
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000713F File Offset: 0x0000533F
		public virtual bool IgnoreReferences
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreReferences>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreReferences>k__BackingField = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00007148 File Offset: 0x00005348
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00007150 File Offset: 0x00005350
		public virtual bool TrimHeaders
		{
			[CompilerGenerated]
			get
			{
				return this.<TrimHeaders>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TrimHeaders>k__BackingField = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00007159 File Offset: 0x00005359
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00007161 File Offset: 0x00005361
		public virtual bool TrimFields
		{
			[CompilerGenerated]
			get
			{
				return this.<TrimFields>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TrimFields>k__BackingField = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000716A File Offset: 0x0000536A
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00007174 File Offset: 0x00005374
		public virtual string Delimiter
		{
			get
			{
				return this.delimiter;
			}
			set
			{
				if (value == "\n")
				{
					throw new CsvConfigurationException("Newline is not a valid delimiter.");
				}
				if (value == "\r")
				{
					throw new CsvConfigurationException("Carriage return is not a valid delimiter.");
				}
				if (value == "\0")
				{
					throw new CsvConfigurationException("Null is not a valid delimiter.");
				}
				if (value == Convert.ToString(this.quote))
				{
					throw new CsvConfigurationException("You can not use the quote as a delimiter.");
				}
				this.delimiter = value;
				this.BuildRequiredQuoteChars();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000071F4 File Offset: 0x000053F4
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000071FC File Offset: 0x000053FC
		public virtual char Quote
		{
			get
			{
				return this.quote;
			}
			set
			{
				if (value == '\n')
				{
					throw new CsvConfigurationException("Newline is not a valid quote.");
				}
				if (value == '\r')
				{
					throw new CsvConfigurationException("Carriage return is not a valid quote.");
				}
				if (value == '\0')
				{
					throw new CsvConfigurationException("Null is not a valid quote.");
				}
				if (Convert.ToString(value) == this.delimiter)
				{
					throw new CsvConfigurationException("You can not use the delimiter as a quote.");
				}
				this.quote = value;
				this.quoteString = Convert.ToString(value, this.cultureInfo);
				this.doubleQuoteString = this.quoteString + this.quoteString;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007285 File Offset: 0x00005485
		public virtual string QuoteString
		{
			get
			{
				return this.quoteString;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000728D File Offset: 0x0000548D
		public virtual string DoubleQuoteString
		{
			get
			{
				return this.doubleQuoteString;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00007295 File Offset: 0x00005495
		public virtual char[] QuoteRequiredChars
		{
			get
			{
				return this.quoteRequiredChars;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000729D File Offset: 0x0000549D
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000072A5 File Offset: 0x000054A5
		public virtual char Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000072AE File Offset: 0x000054AE
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000072B6 File Offset: 0x000054B6
		public virtual bool AllowComments
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowComments>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowComments>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000072BF File Offset: 0x000054BF
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000072C7 File Offset: 0x000054C7
		public virtual int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
			set
			{
				this.bufferSize = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000072D0 File Offset: 0x000054D0
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000072D8 File Offset: 0x000054D8
		public virtual bool QuoteAllFields
		{
			get
			{
				return this.quoteAllFields;
			}
			set
			{
				this.quoteAllFields = value;
				if (this.quoteAllFields && this.quoteNoFields)
				{
					this.quoteNoFields = false;
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000072F8 File Offset: 0x000054F8
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00007300 File Offset: 0x00005500
		public virtual bool QuoteNoFields
		{
			get
			{
				return this.quoteNoFields;
			}
			set
			{
				this.quoteNoFields = value;
				if (this.quoteNoFields && this.quoteAllFields)
				{
					this.quoteAllFields = false;
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007320 File Offset: 0x00005520
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00007328 File Offset: 0x00005528
		public virtual bool CountBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<CountBytes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CountBytes>k__BackingField = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007331 File Offset: 0x00005531
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00007339 File Offset: 0x00005539
		public virtual Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007342 File Offset: 0x00005542
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000734A File Offset: 0x0000554A
		public virtual CultureInfo CultureInfo
		{
			get
			{
				return this.cultureInfo;
			}
			set
			{
				this.cultureInfo = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007353 File Offset: 0x00005553
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000735B File Offset: 0x0000555B
		public virtual bool SkipEmptyRecords
		{
			[CompilerGenerated]
			get
			{
				return this.<SkipEmptyRecords>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SkipEmptyRecords>k__BackingField = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007364 File Offset: 0x00005564
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000736C File Offset: 0x0000556C
		public virtual Func<string[], bool> ShouldSkipRecord
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldSkipRecord>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShouldSkipRecord>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007375 File Offset: 0x00005575
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000737D File Offset: 0x0000557D
		public virtual bool IgnoreQuotes
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreQuotes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreQuotes>k__BackingField = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007386 File Offset: 0x00005586
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000738E File Offset: 0x0000558E
		public virtual bool IgnorePrivateAccessor
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnorePrivateAccessor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnorePrivateAccessor>k__BackingField = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007397 File Offset: 0x00005597
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000739F File Offset: 0x0000559F
		public virtual bool IgnoreBlankLines
		{
			get
			{
				return this.ignoreBlankLines;
			}
			set
			{
				this.ignoreBlankLines = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000073A8 File Offset: 0x000055A8
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x000073B0 File Offset: 0x000055B0
		public virtual bool UseExcelLeadingZerosFormatForNumerics
		{
			[CompilerGenerated]
			get
			{
				return this.<UseExcelLeadingZerosFormatForNumerics>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseExcelLeadingZerosFormatForNumerics>k__BackingField = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000073B9 File Offset: 0x000055B9
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x000073C1 File Offset: 0x000055C1
		public virtual bool PrefixReferenceHeaders
		{
			[CompilerGenerated]
			get
			{
				return this.<PrefixReferenceHeaders>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrefixReferenceHeaders>k__BackingField = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000073CA File Offset: 0x000055CA
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x000073D2 File Offset: 0x000055D2
		public virtual bool ThrowOnBadData
		{
			[CompilerGenerated]
			get
			{
				return this.<ThrowOnBadData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ThrowOnBadData>k__BackingField = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000073DB File Offset: 0x000055DB
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000073E3 File Offset: 0x000055E3
		public virtual Action<string> BadDataCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<BadDataCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BadDataCallback>k__BackingField = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000073EC File Offset: 0x000055EC
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x000073F4 File Offset: 0x000055F4
		public virtual bool IgnoreReadingExceptions
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreReadingExceptions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreReadingExceptions>k__BackingField = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000073FD File Offset: 0x000055FD
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00007405 File Offset: 0x00005605
		public virtual Action<Exception, ICsvReader> ReadingExceptionCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<ReadingExceptionCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReadingExceptionCallback>k__BackingField = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000740E File Offset: 0x0000560E
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00007416 File Offset: 0x00005616
		public virtual bool UseNewObjectForNullReferenceProperties
		{
			get
			{
				return this.useNewObjectForNullReferenceProperties;
			}
			set
			{
				this.useNewObjectForNullReferenceProperties = value;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007420 File Offset: 0x00005620
		public virtual TMap RegisterClassMap<TMap>() where TMap : CsvClassMap
		{
			TMap tmap = ReflectionHelper.CreateInstance<TMap>(new object[0]);
			this.RegisterClassMap(tmap);
			return tmap;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007448 File Offset: 0x00005648
		public virtual CsvClassMap RegisterClassMap(Type classMapType)
		{
			if (!typeof(CsvClassMap).IsAssignableFrom(classMapType))
			{
				throw new ArgumentException("The class map type must inherit from CsvClassMap.");
			}
			CsvClassMap csvClassMap = (CsvClassMap)ReflectionHelper.CreateInstance(classMapType, new object[0]);
			this.RegisterClassMap(csvClassMap);
			return csvClassMap;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000748C File Offset: 0x0000568C
		public virtual void RegisterClassMap(CsvClassMap map)
		{
			map.CreateMap();
			if (map.Constructor == null && map.PropertyMaps.Count == 0 && map.ReferenceMaps.Count == 0)
			{
				throw new CsvConfigurationException("No mappings were specified in the CsvClassMap.");
			}
			this.Maps.Add(map);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000074D8 File Offset: 0x000056D8
		public virtual void UnregisterClassMap<TMap>() where TMap : CsvClassMap
		{
			this.UnregisterClassMap(typeof(TMap));
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000074EA File Offset: 0x000056EA
		public virtual void UnregisterClassMap(Type classMapType)
		{
			this.maps.Remove(classMapType);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000074F8 File Offset: 0x000056F8
		public virtual void UnregisterClassMap()
		{
			this.maps.Clear();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007505 File Offset: 0x00005705
		public virtual CsvClassMap AutoMap<T>()
		{
			return this.AutoMap(typeof(T));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007517 File Offset: 0x00005717
		public virtual CsvClassMap AutoMap(Type type)
		{
			CsvClassMap csvClassMap = (CsvClassMap)ReflectionHelper.CreateInstance(typeof(DefaultCsvClassMap<>).MakeGenericType(new Type[] { type }), new object[0]);
			csvClassMap.AutoMap(this.IgnoreReferences, this.PrefixReferenceHeaders);
			return csvClassMap;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007554 File Offset: 0x00005754
		public CsvConfiguration()
		{
			this.BuildRequiredQuoteChars();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000075F8 File Offset: 0x000057F8
		private void BuildRequiredQuoteChars()
		{
			char[] array2;
			if (this.delimiter.Length <= 1)
			{
				char[] array = new char[3];
				array[0] = '\r';
				array[1] = '\n';
				array2 = array;
				array[2] = this.delimiter.get_Chars(0);
			}
			else
			{
				char[] array3 = new char[2];
				array3[0] = '\r';
				array2 = array3;
				array3[1] = '\n';
			}
			this.quoteRequiredChars = array2;
		}

		// Token: 0x0400003E RID: 62
		private BindingFlags propertyBindingFlags = 20;

		// Token: 0x0400003F RID: 63
		private bool hasHeaderRecord = true;

		// Token: 0x04000040 RID: 64
		private bool willThrowOnMissingField = true;

		// Token: 0x04000041 RID: 65
		private string delimiter = ",";

		// Token: 0x04000042 RID: 66
		private char quote = '"';

		// Token: 0x04000043 RID: 67
		private string quoteString = "\"";

		// Token: 0x04000044 RID: 68
		private string doubleQuoteString = "\"\"";

		// Token: 0x04000045 RID: 69
		private char[] quoteRequiredChars;

		// Token: 0x04000046 RID: 70
		private char comment = '#';

		// Token: 0x04000047 RID: 71
		private int bufferSize = 2048;

		// Token: 0x04000048 RID: 72
		private bool isHeaderCaseSensitive = true;

		// Token: 0x04000049 RID: 73
		private Encoding encoding = Encoding.UTF8;

		// Token: 0x0400004A RID: 74
		private CultureInfo cultureInfo = CultureInfo.CurrentCulture;

		// Token: 0x0400004B RID: 75
		private bool quoteAllFields;

		// Token: 0x0400004C RID: 76
		private bool quoteNoFields;

		// Token: 0x0400004D RID: 77
		private bool ignoreBlankLines = true;

		// Token: 0x0400004E RID: 78
		private bool useNewObjectForNullReferenceProperties = true;

		// Token: 0x0400004F RID: 79
		private readonly CsvClassMapCollection maps = new CsvClassMapCollection();

		// Token: 0x04000050 RID: 80
		[CompilerGenerated]
		private bool <HasExcelSeparator>k__BackingField;

		// Token: 0x04000051 RID: 81
		[CompilerGenerated]
		private bool <DetectColumnCountChanges>k__BackingField;

		// Token: 0x04000052 RID: 82
		[CompilerGenerated]
		private bool <IgnoreHeaderWhiteSpace>k__BackingField;

		// Token: 0x04000053 RID: 83
		[CompilerGenerated]
		private bool <IgnoreReferences>k__BackingField;

		// Token: 0x04000054 RID: 84
		[CompilerGenerated]
		private bool <TrimHeaders>k__BackingField;

		// Token: 0x04000055 RID: 85
		[CompilerGenerated]
		private bool <TrimFields>k__BackingField;

		// Token: 0x04000056 RID: 86
		[CompilerGenerated]
		private bool <AllowComments>k__BackingField;

		// Token: 0x04000057 RID: 87
		[CompilerGenerated]
		private bool <CountBytes>k__BackingField;

		// Token: 0x04000058 RID: 88
		[CompilerGenerated]
		private bool <SkipEmptyRecords>k__BackingField;

		// Token: 0x04000059 RID: 89
		[CompilerGenerated]
		private Func<string[], bool> <ShouldSkipRecord>k__BackingField;

		// Token: 0x0400005A RID: 90
		[CompilerGenerated]
		private bool <IgnoreQuotes>k__BackingField;

		// Token: 0x0400005B RID: 91
		[CompilerGenerated]
		private bool <IgnorePrivateAccessor>k__BackingField;

		// Token: 0x0400005C RID: 92
		[CompilerGenerated]
		private bool <UseExcelLeadingZerosFormatForNumerics>k__BackingField;

		// Token: 0x0400005D RID: 93
		[CompilerGenerated]
		private bool <PrefixReferenceHeaders>k__BackingField;

		// Token: 0x0400005E RID: 94
		[CompilerGenerated]
		private bool <ThrowOnBadData>k__BackingField;

		// Token: 0x0400005F RID: 95
		[CompilerGenerated]
		private Action<string> <BadDataCallback>k__BackingField;

		// Token: 0x04000060 RID: 96
		[CompilerGenerated]
		private bool <IgnoreReadingExceptions>k__BackingField;

		// Token: 0x04000061 RID: 97
		[CompilerGenerated]
		private Action<Exception, ICsvReader> <ReadingExceptionCallback>k__BackingField;
	}
}
