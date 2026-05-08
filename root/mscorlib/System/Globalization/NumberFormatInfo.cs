using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x020009ED RID: 2541
	[ComVisible(true)]
	[Serializable]
	public sealed class NumberFormatInfo : ICloneable, IFormatProvider
	{
		// Token: 0x06005DA5 RID: 23973 RVA: 0x00140C1B File Offset: 0x0013EE1B
		public NumberFormatInfo()
			: this(null)
		{
		}

		// Token: 0x06005DA6 RID: 23974 RVA: 0x00140C24 File Offset: 0x0013EE24
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if (this.numberDecimalSeparator != this.numberGroupSeparator)
			{
				this.validForParseAsNumber = true;
			}
			else
			{
				this.validForParseAsNumber = false;
			}
			if (this.numberDecimalSeparator != this.numberGroupSeparator && this.numberDecimalSeparator != this.currencyGroupSeparator && this.currencyDecimalSeparator != this.numberGroupSeparator && this.currencyDecimalSeparator != this.currencyGroupSeparator)
			{
				this.validForParseAsCurrency = true;
				return;
			}
			this.validForParseAsCurrency = false;
		}

		// Token: 0x06005DA7 RID: 23975 RVA: 0x00004088 File Offset: 0x00002288
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
		}

		// Token: 0x06005DA8 RID: 23976 RVA: 0x00004088 File Offset: 0x00002288
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x00140CAF File Offset: 0x0013EEAF
		private static void VerifyDecimalSeparator(string decSep, string propertyName)
		{
			if (decSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			if (decSep.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Decimal separator cannot be the empty string."));
			}
		}

		// Token: 0x06005DAA RID: 23978 RVA: 0x00140CDD File Offset: 0x0013EEDD
		private static void VerifyGroupSeparator(string groupSep, string propertyName)
		{
			if (groupSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("String reference not set to an instance of a String."));
			}
		}

		// Token: 0x06005DAB RID: 23979 RVA: 0x00140CF4 File Offset: 0x0013EEF4
		private static void VerifyNativeDigits(string[] nativeDig, string propertyName)
		{
			if (nativeDig == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("Array cannot be null."));
			}
			if (nativeDig.Length != 10)
			{
				throw new ArgumentException(Environment.GetResourceString("The NativeDigits array must contain exactly ten members."), propertyName);
			}
			for (int i = 0; i < nativeDig.Length; i++)
			{
				if (nativeDig[i] == null)
				{
					throw new ArgumentNullException(propertyName, Environment.GetResourceString("Found a null value within an array."));
				}
				if (nativeDig[i].Length != 1)
				{
					if (nativeDig[i].Length != 2)
					{
						throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
					}
					if (!char.IsSurrogatePair(nativeDig[i][0], nativeDig[i][1]))
					{
						throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
					}
				}
				if (CharUnicodeInfo.GetDecimalDigitValue(nativeDig[i], 0) != i && CharUnicodeInfo.GetUnicodeCategory(nativeDig[i], 0) != UnicodeCategory.PrivateUse)
				{
					throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
				}
			}
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x00140DD2 File Offset: 0x0013EFD2
		private static void VerifyDigitSubstitution(DigitShapes digitSub, string propertyName)
		{
			if (digitSub > DigitShapes.NativeNational)
			{
				throw new ArgumentException(Environment.GetResourceString("The DigitSubstitution property must be of a valid member of the DigitShapes enumeration. Valid entries include Context, NativeNational or None."), propertyName);
			}
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x00140DEC File Offset: 0x0013EFEC
		[SecuritySafeCritical]
		internal NumberFormatInfo(CultureData cultureData)
		{
			if (GlobalizationMode.Invariant)
			{
				this.m_isInvariant = true;
				return;
			}
			if (cultureData != null)
			{
				cultureData.GetNFIValues(this);
				if (cultureData.IsInvariantCulture)
				{
					this.m_isInvariant = true;
				}
			}
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x00140F80 File Offset: 0x0013F180
		private void VerifyWritable()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x00140F9A File Offset: 0x0013F19A
		public static NumberFormatInfo InvariantInfo
		{
			get
			{
				if (NumberFormatInfo.invariantInfo == null)
				{
					NumberFormatInfo.invariantInfo = NumberFormatInfo.ReadOnly(new NumberFormatInfo
					{
						m_isInvariant = true
					});
				}
				return NumberFormatInfo.invariantInfo;
			}
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x00140FC4 File Offset: 0x0013F1C4
		public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
		{
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			if (cultureInfo != null && !cultureInfo.m_isInherited)
			{
				NumberFormatInfo numberFormatInfo = cultureInfo.numInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				return cultureInfo.NumberFormat;
			}
			else
			{
				NumberFormatInfo numberFormatInfo = formatProvider as NumberFormatInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				if (formatProvider != null)
				{
					numberFormatInfo = formatProvider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
					if (numberFormatInfo != null)
					{
						return numberFormatInfo;
					}
				}
				return NumberFormatInfo.CurrentInfo;
			}
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x00141027 File Offset: 0x0013F227
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo.isReadOnly = false;
			return numberFormatInfo;
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x0014103B File Offset: 0x0013F23B
		// (set) Token: 0x06005DB3 RID: 23987 RVA: 0x00141044 File Offset: 0x0013F244
		public int CurrencyDecimalDigits
		{
			get
			{
				return this.currencyDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("CurrencyDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.currencyDecimalDigits = value;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x00141093 File Offset: 0x0013F293
		// (set) Token: 0x06005DB5 RID: 23989 RVA: 0x0014109B File Offset: 0x0013F29B
		public string CurrencyDecimalSeparator
		{
			get
			{
				return this.currencyDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "CurrencyDecimalSeparator");
				this.currencyDecimalSeparator = value;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x001410B5 File Offset: 0x0013F2B5
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x001410C0 File Offset: 0x0013F2C0
		internal static void CheckGroupSize(string propName, int[] groupSize)
		{
			int i = 0;
			while (i < groupSize.Length)
			{
				if (groupSize[i] < 1)
				{
					if (i == groupSize.Length - 1 && groupSize[i] == 0)
					{
						return;
					}
					throw new ArgumentException(Environment.GetResourceString("Every element in the value array should be between one and nine, except for the last element, which can be zero."), propName);
				}
				else
				{
					if (groupSize[i] > 9)
					{
						throw new ArgumentException(Environment.GetResourceString("Every element in the value array should be between one and nine, except for the last element, which can be zero."), propName);
					}
					i++;
				}
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06005DB8 RID: 23992 RVA: 0x00141118 File Offset: 0x0013F318
		// (set) Token: 0x06005DB9 RID: 23993 RVA: 0x0014112C File Offset: 0x0013F32C
		public int[] CurrencyGroupSizes
		{
			get
			{
				return (int[])this.currencyGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencyGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("CurrencyGroupSizes", array);
				this.currencyGroupSizes = array;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x00141175 File Offset: 0x0013F375
		// (set) Token: 0x06005DBB RID: 23995 RVA: 0x00141188 File Offset: 0x0013F388
		public int[] NumberGroupSizes
		{
			get
			{
				return (int[])this.numberGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NumberGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("NumberGroupSizes", array);
				this.numberGroupSizes = array;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06005DBC RID: 23996 RVA: 0x001411D1 File Offset: 0x0013F3D1
		// (set) Token: 0x06005DBD RID: 23997 RVA: 0x001411E4 File Offset: 0x0013F3E4
		public int[] PercentGroupSizes
		{
			get
			{
				return (int[])this.percentGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("PercentGroupSizes", array);
				this.percentGroupSizes = array;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06005DBE RID: 23998 RVA: 0x0014122D File Offset: 0x0013F42D
		// (set) Token: 0x06005DBF RID: 23999 RVA: 0x00141235 File Offset: 0x0013F435
		public string CurrencyGroupSeparator
		{
			get
			{
				return this.currencyGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "CurrencyGroupSeparator");
				this.currencyGroupSeparator = value;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06005DC0 RID: 24000 RVA: 0x0014124F File Offset: 0x0013F44F
		// (set) Token: 0x06005DC1 RID: 24001 RVA: 0x00141257 File Offset: 0x0013F457
		public string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.currencySymbol = value;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x00141280 File Offset: 0x0013F480
		public static NumberFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				if (!currentCulture.m_isInherited)
				{
					NumberFormatInfo numInfo = currentCulture.numInfo;
					if (numInfo != null)
					{
						return numInfo;
					}
				}
				return (NumberFormatInfo)currentCulture.GetFormat(typeof(NumberFormatInfo));
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06005DC3 RID: 24003 RVA: 0x001412C3 File Offset: 0x0013F4C3
		// (set) Token: 0x06005DC4 RID: 24004 RVA: 0x001412CB File Offset: 0x0013F4CB
		public string NaNSymbol
		{
			get
			{
				return this.nanSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NaNSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.nanSymbol = value;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06005DC5 RID: 24005 RVA: 0x001412F2 File Offset: 0x0013F4F2
		// (set) Token: 0x06005DC6 RID: 24006 RVA: 0x001412FC File Offset: 0x0013F4FC
		public int CurrencyNegativePattern
		{
			get
			{
				return this.currencyNegativePattern;
			}
			set
			{
				if (value < 0 || value > 15)
				{
					throw new ArgumentOutOfRangeException("CurrencyNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 15));
				}
				this.VerifyWritable();
				this.currencyNegativePattern = value;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x0014134B File Offset: 0x0013F54B
		// (set) Token: 0x06005DC8 RID: 24008 RVA: 0x00141354 File Offset: 0x0013F554
		public int NumberNegativePattern
		{
			get
			{
				return this.numberNegativePattern;
			}
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("NumberNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 4));
				}
				this.VerifyWritable();
				this.numberNegativePattern = value;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06005DC9 RID: 24009 RVA: 0x001413A1 File Offset: 0x0013F5A1
		// (set) Token: 0x06005DCA RID: 24010 RVA: 0x001413AC File Offset: 0x0013F5AC
		public int PercentPositivePattern
		{
			get
			{
				return this.percentPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("PercentPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 3));
				}
				this.VerifyWritable();
				this.percentPositivePattern = value;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06005DCB RID: 24011 RVA: 0x001413F9 File Offset: 0x0013F5F9
		// (set) Token: 0x06005DCC RID: 24012 RVA: 0x00141404 File Offset: 0x0013F604
		public int PercentNegativePattern
		{
			get
			{
				return this.percentNegativePattern;
			}
			set
			{
				if (value < 0 || value > 11)
				{
					throw new ArgumentOutOfRangeException("PercentNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 11));
				}
				this.VerifyWritable();
				this.percentNegativePattern = value;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06005DCD RID: 24013 RVA: 0x00141453 File Offset: 0x0013F653
		// (set) Token: 0x06005DCE RID: 24014 RVA: 0x0014145B File Offset: 0x0013F65B
		public string NegativeInfinitySymbol
		{
			get
			{
				return this.negativeInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeInfinitySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.negativeInfinitySymbol = value;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06005DCF RID: 24015 RVA: 0x00141482 File Offset: 0x0013F682
		// (set) Token: 0x06005DD0 RID: 24016 RVA: 0x0014148A File Offset: 0x0013F68A
		public string NegativeSign
		{
			get
			{
				return this.negativeSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeSign", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.negativeSign = value;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06005DD1 RID: 24017 RVA: 0x001414B1 File Offset: 0x0013F6B1
		// (set) Token: 0x06005DD2 RID: 24018 RVA: 0x001414BC File Offset: 0x0013F6BC
		public int NumberDecimalDigits
		{
			get
			{
				return this.numberDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("NumberDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.numberDecimalDigits = value;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0014150B File Offset: 0x0013F70B
		// (set) Token: 0x06005DD4 RID: 24020 RVA: 0x00141513 File Offset: 0x0013F713
		public string NumberDecimalSeparator
		{
			get
			{
				return this.numberDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "NumberDecimalSeparator");
				this.numberDecimalSeparator = value;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x0014152D File Offset: 0x0013F72D
		// (set) Token: 0x06005DD6 RID: 24022 RVA: 0x00141535 File Offset: 0x0013F735
		public string NumberGroupSeparator
		{
			get
			{
				return this.numberGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "NumberGroupSeparator");
				this.numberGroupSeparator = value;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x0014154F File Offset: 0x0013F74F
		// (set) Token: 0x06005DD8 RID: 24024 RVA: 0x00141558 File Offset: 0x0013F758
		public int CurrencyPositivePattern
		{
			get
			{
				return this.currencyPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("CurrencyPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 3));
				}
				this.VerifyWritable();
				this.currencyPositivePattern = value;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005DD9 RID: 24025 RVA: 0x001415A5 File Offset: 0x0013F7A5
		// (set) Token: 0x06005DDA RID: 24026 RVA: 0x001415AD File Offset: 0x0013F7AD
		public string PositiveInfinitySymbol
		{
			get
			{
				return this.positiveInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveInfinitySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.positiveInfinitySymbol = value;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005DDB RID: 24027 RVA: 0x001415D4 File Offset: 0x0013F7D4
		// (set) Token: 0x06005DDC RID: 24028 RVA: 0x001415DC File Offset: 0x0013F7DC
		public string PositiveSign
		{
			get
			{
				return this.positiveSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveSign", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.positiveSign = value;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06005DDD RID: 24029 RVA: 0x00141603 File Offset: 0x0013F803
		// (set) Token: 0x06005DDE RID: 24030 RVA: 0x0014160C File Offset: 0x0013F80C
		public int PercentDecimalDigits
		{
			get
			{
				return this.percentDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("PercentDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.percentDecimalDigits = value;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x0014165B File Offset: 0x0013F85B
		// (set) Token: 0x06005DE0 RID: 24032 RVA: 0x00141663 File Offset: 0x0013F863
		public string PercentDecimalSeparator
		{
			get
			{
				return this.percentDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "PercentDecimalSeparator");
				this.percentDecimalSeparator = value;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005DE1 RID: 24033 RVA: 0x0014167D File Offset: 0x0013F87D
		// (set) Token: 0x06005DE2 RID: 24034 RVA: 0x00141685 File Offset: 0x0013F885
		public string PercentGroupSeparator
		{
			get
			{
				return this.percentGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "PercentGroupSeparator");
				this.percentGroupSeparator = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x0014169F File Offset: 0x0013F89F
		// (set) Token: 0x06005DE4 RID: 24036 RVA: 0x001416A7 File Offset: 0x0013F8A7
		public string PercentSymbol
		{
			get
			{
				return this.percentSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.percentSymbol = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005DE5 RID: 24037 RVA: 0x001416CE File Offset: 0x0013F8CE
		// (set) Token: 0x06005DE6 RID: 24038 RVA: 0x001416D6 File Offset: 0x0013F8D6
		public string PerMilleSymbol
		{
			get
			{
				return this.perMilleSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PerMilleSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.perMilleSymbol = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x001416FD File Offset: 0x0013F8FD
		// (set) Token: 0x06005DE8 RID: 24040 RVA: 0x0014170F File Offset: 0x0013F90F
		[ComVisible(false)]
		public string[] NativeDigits
		{
			get
			{
				return (string[])this.nativeDigits.Clone();
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyNativeDigits(value, "NativeDigits");
				this.nativeDigits = value;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x00141729 File Offset: 0x0013F929
		// (set) Token: 0x06005DEA RID: 24042 RVA: 0x00141731 File Offset: 0x0013F931
		[ComVisible(false)]
		public DigitShapes DigitSubstitution
		{
			get
			{
				return (DigitShapes)this.digitSubstitution;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDigitSubstitution(value, "DigitSubstitution");
				this.digitSubstitution = (int)value;
			}
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x0014174B File Offset: 0x0013F94B
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(NumberFormatInfo)))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x00141762 File Offset: 0x0013F962
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			if (nfi == null)
			{
				throw new ArgumentNullException("nfi");
			}
			if (nfi.IsReadOnly)
			{
				return nfi;
			}
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.MemberwiseClone();
			numberFormatInfo.isReadOnly = true;
			return numberFormatInfo;
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x00141790 File Offset: 0x0013F990
		internal static void ValidateParseStyleInteger(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined NumberStyles value is being used."), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber."));
			}
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x001417DD File Offset: 0x0013F9DD
		internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined NumberStyles value is being used."), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("The number style AllowHexSpecifier is not supported on floating point data types."));
			}
		}

		// Token: 0x04003896 RID: 14486
		private static volatile NumberFormatInfo invariantInfo;

		// Token: 0x04003897 RID: 14487
		internal int[] numberGroupSizes = new int[] { 3 };

		// Token: 0x04003898 RID: 14488
		internal int[] currencyGroupSizes = new int[] { 3 };

		// Token: 0x04003899 RID: 14489
		internal int[] percentGroupSizes = new int[] { 3 };

		// Token: 0x0400389A RID: 14490
		internal string positiveSign = "+";

		// Token: 0x0400389B RID: 14491
		internal string negativeSign = "-";

		// Token: 0x0400389C RID: 14492
		internal string numberDecimalSeparator = ".";

		// Token: 0x0400389D RID: 14493
		internal string numberGroupSeparator = ",";

		// Token: 0x0400389E RID: 14494
		internal string currencyGroupSeparator = ",";

		// Token: 0x0400389F RID: 14495
		internal string currencyDecimalSeparator = ".";

		// Token: 0x040038A0 RID: 14496
		internal string currencySymbol = "¤";

		// Token: 0x040038A1 RID: 14497
		internal string ansiCurrencySymbol;

		// Token: 0x040038A2 RID: 14498
		internal string nanSymbol = "NaN";

		// Token: 0x040038A3 RID: 14499
		internal string positiveInfinitySymbol = "Infinity";

		// Token: 0x040038A4 RID: 14500
		internal string negativeInfinitySymbol = "-Infinity";

		// Token: 0x040038A5 RID: 14501
		internal string percentDecimalSeparator = ".";

		// Token: 0x040038A6 RID: 14502
		internal string percentGroupSeparator = ",";

		// Token: 0x040038A7 RID: 14503
		internal string percentSymbol = "%";

		// Token: 0x040038A8 RID: 14504
		internal string perMilleSymbol = "‰";

		// Token: 0x040038A9 RID: 14505
		[OptionalField(VersionAdded = 2)]
		internal string[] nativeDigits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

		// Token: 0x040038AA RID: 14506
		[OptionalField(VersionAdded = 1)]
		internal int m_dataItem;

		// Token: 0x040038AB RID: 14507
		internal int numberDecimalDigits = 2;

		// Token: 0x040038AC RID: 14508
		internal int currencyDecimalDigits = 2;

		// Token: 0x040038AD RID: 14509
		internal int currencyPositivePattern;

		// Token: 0x040038AE RID: 14510
		internal int currencyNegativePattern;

		// Token: 0x040038AF RID: 14511
		internal int numberNegativePattern = 1;

		// Token: 0x040038B0 RID: 14512
		internal int percentPositivePattern;

		// Token: 0x040038B1 RID: 14513
		internal int percentNegativePattern;

		// Token: 0x040038B2 RID: 14514
		internal int percentDecimalDigits = 2;

		// Token: 0x040038B3 RID: 14515
		[OptionalField(VersionAdded = 2)]
		internal int digitSubstitution = 1;

		// Token: 0x040038B4 RID: 14516
		internal bool isReadOnly;

		// Token: 0x040038B5 RID: 14517
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x040038B6 RID: 14518
		[OptionalField(VersionAdded = 2)]
		internal bool m_isInvariant;

		// Token: 0x040038B7 RID: 14519
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsNumber = true;

		// Token: 0x040038B8 RID: 14520
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsCurrency = true;

		// Token: 0x040038B9 RID: 14521
		private const NumberStyles InvalidNumberStyles = ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier);
	}
}
