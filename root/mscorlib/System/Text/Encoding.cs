using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x0200038C RID: 908
	[ComVisible(true)]
	[Serializable]
	public abstract class Encoding : ICloneable
	{
		// Token: 0x0600274F RID: 10063 RVA: 0x00090572 File Offset: 0x0008E772
		protected Encoding()
			: this(0)
		{
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0009057B File Offset: 0x0008E77B
		protected Encoding(int codePage)
		{
			this.m_isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.SetDefaultFallbacks();
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000905A8 File Offset: 0x0008E7A8
		protected Encoding(int codePage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			this.m_isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.encoderFallback = encoderFallback ?? new InternalEncoderBestFitFallback(this);
			this.decoderFallback = decoderFallback ?? new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000905FA File Offset: 0x0008E7FA
		internal virtual void SetDefaultFallbacks()
		{
			this.encoderFallback = new InternalEncoderBestFitFallback(this);
			this.decoderFallback = new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x00090614 File Offset: 0x0008E814
		internal void OnDeserializing()
		{
			this.encoderFallback = null;
			this.decoderFallback = null;
			this.m_isReadOnly = true;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0009062B File Offset: 0x0008E82B
		internal void OnDeserialized()
		{
			if (this.encoderFallback == null || this.decoderFallback == null)
			{
				this.m_deserializedFromEverett = true;
				this.SetDefaultFallbacks();
			}
			this.dataItem = null;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x00090651 File Offset: 0x0008E851
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.OnDeserializing();
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x00090659 File Offset: 0x0008E859
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x00090661 File Offset: 0x0008E861
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.dataItem = null;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0009066C File Offset: 0x0008E86C
		internal void DeserializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			this.dataItem = null;
			try
			{
				this.m_isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.m_deserializedFromEverett = true;
				this.m_isReadOnly = true;
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x00090738 File Offset: 0x0008E938
		internal void SerializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_isReadOnly", this.m_isReadOnly);
			info.AddValue("encoderFallback", this.EncoderFallback);
			info.AddValue("decoderFallback", this.DecoderFallback);
			info.AddValue("m_codePage", this.m_codePage);
			info.AddValue("dataItem", null);
			info.AddValue("Encoding+m_codePage", this.m_codePage);
			info.AddValue("Encoding+dataItem", null);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000907C0 File Offset: 0x0008E9C0
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return Encoding.Convert(srcEncoding, dstEncoding, bytes, 0, bytes.Length);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000907DC File Offset: 0x0008E9DC
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
		{
			if (srcEncoding == null || dstEncoding == null)
			{
				throw new ArgumentNullException((srcEncoding == null) ? "srcEncoding" : "dstEncoding", Environment.GetResourceString("Array cannot be null."));
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, index, count));
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x00090838 File Offset: 0x0008EA38
		private static object InternalSyncObject
		{
			get
			{
				if (Encoding.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref Encoding.s_InternalSyncObject, obj, null);
				}
				return Encoding.s_InternalSyncObject;
			}
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x00090864 File Offset: 0x0008EA64
		[SecurityCritical]
		public static void RegisterProvider(EncodingProvider provider)
		{
			EncodingProvider.AddProvider(provider);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x0009086C File Offset: 0x0008EA6C
		[SecuritySafeCritical]
		public static Encoding GetEncoding(int codepage)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage);
			if (encoding != null)
			{
				return encoding;
			}
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException("codepage", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 0, 65535 }));
			}
			if (Encoding.encodings != null)
			{
				Encoding.encodings.TryGetValue(codepage, out encoding);
			}
			if (encoding == null)
			{
				object internalSyncObject = Encoding.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (Encoding.encodings == null)
					{
						Encoding.encodings = new Dictionary<int, Encoding>();
					}
					if (Encoding.encodings.TryGetValue(codepage, out encoding))
					{
						return encoding;
					}
					if (codepage <= 1201)
					{
						if (codepage <= 3)
						{
							if (codepage == 0)
							{
								encoding = Encoding.Default;
								goto IL_0233;
							}
							if (codepage - 1 > 2)
							{
								goto IL_01B0;
							}
						}
						else if (codepage != 42)
						{
							if (codepage == 1200)
							{
								encoding = Encoding.Unicode;
								goto IL_0233;
							}
							if (codepage != 1201)
							{
								goto IL_01B0;
							}
							encoding = Encoding.BigEndianUnicode;
							goto IL_0233;
						}
						throw new ArgumentException(Environment.GetResourceString("{0} is not a supported code page.", new object[] { codepage }), "codepage");
					}
					if (codepage <= 20127)
					{
						if (codepage == 12000)
						{
							encoding = Encoding.UTF32;
							goto IL_0233;
						}
						if (codepage == 12001)
						{
							encoding = new UTF32Encoding(true, true);
							goto IL_0233;
						}
						if (codepage == 20127)
						{
							encoding = Encoding.ASCII;
							goto IL_0233;
						}
					}
					else
					{
						if (codepage == 28591)
						{
							encoding = Encoding.Latin1;
							goto IL_0233;
						}
						if (codepage == 65000)
						{
							encoding = Encoding.UTF7;
							goto IL_0233;
						}
						if (codepage == 65001)
						{
							encoding = Encoding.UTF8;
							goto IL_0233;
						}
					}
					IL_01B0:
					if (EncodingTable.GetCodePageDataItem(codepage) == null)
					{
						throw new NotSupportedException(Environment.GetResourceString("No data is available for encoding {0}. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.", new object[] { codepage }));
					}
					if (codepage != 12000)
					{
						if (codepage != 12001)
						{
							encoding = (Encoding)EncodingHelper.InvokeI18N("GetEncoding", new object[] { codepage });
							if (encoding == null)
							{
								throw new NotSupportedException(string.Format("Encoding {0} data could not be found. Make sure you have correct international codeset assembly installed and enabled.", codepage));
							}
						}
						else
						{
							encoding = new UTF32Encoding(true, true);
						}
					}
					else
					{
						encoding = Encoding.UTF32;
					}
					IL_0233:
					Encoding.encodings.Add(codepage, encoding);
				}
				return encoding;
			}
			return encoding;
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00090AE8 File Offset: 0x0008ECE8
		public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage, encoderFallback, decoderFallback);
			if (encoding != null)
			{
				return encoding;
			}
			encoding = Encoding.GetEncoding(codepage);
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = encoderFallback;
			encoding2.DecoderFallback = decoderFallback;
			return encoding2;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00090B24 File Offset: 0x0008ED24
		public static Encoding GetEncoding(string name)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name));
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00090B48 File Offset: 0x0008ED48
		public static Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name, encoderFallback, decoderFallback);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name), encoderFallback, decoderFallback);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00090B70 File Offset: 0x0008ED70
		public static EncodingInfo[] GetEncodings()
		{
			return EncodingTable.GetEncodings();
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00090B77 File Offset: 0x0008ED77
		public virtual byte[] GetPreamble()
		{
			return EmptyArray<byte>.Value;
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x00090B7E File Offset: 0x0008ED7E
		public virtual ReadOnlySpan<byte> Preamble
		{
			get
			{
				return this.GetPreamble();
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00090B8C File Offset: 0x0008ED8C
		private void GetDataItem()
		{
			if (this.dataItem == null)
			{
				this.dataItem = EncodingTable.GetCodePageDataItem(this.m_codePage);
				if (this.dataItem == null)
				{
					throw new NotSupportedException(Environment.GetResourceString("No data is available for encoding {0}. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.", new object[] { this.m_codePage }));
				}
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x00090BDE File Offset: 0x0008EDDE
		public virtual string BodyName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.BodyName;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x00090BF9 File Offset: 0x0008EDF9
		public virtual string EncodingName
		{
			get
			{
				return Environment.GetResourceStringEncodingName(this.m_codePage);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x00090C06 File Offset: 0x0008EE06
		public virtual string HeaderName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.HeaderName;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x00090C21 File Offset: 0x0008EE21
		public virtual string WebName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.WebName;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x00090C3C File Offset: 0x0008EE3C
		public virtual int WindowsCodePage
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.UIFamilyCodePage;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x00090C57 File Offset: 0x0008EE57
		public virtual bool IsBrowserDisplay
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 2U) > 0U;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x00090C77 File Offset: 0x0008EE77
		public virtual bool IsBrowserSave
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 512U) > 0U;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00090C9B File Offset: 0x0008EE9B
		public virtual bool IsMailNewsDisplay
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 1U) > 0U;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x00090CBB File Offset: 0x0008EEBB
		public virtual bool IsMailNewsSave
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 256U) > 0U;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x0000408A File Offset: 0x0000228A
		[ComVisible(false)]
		public virtual bool IsSingleByte
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x00090CDF File Offset: 0x0008EEDF
		// (set) Token: 0x06002771 RID: 10097 RVA: 0x00090CE7 File Offset: 0x0008EEE7
		[ComVisible(false)]
		public EncoderFallback EncoderFallback
		{
			get
			{
				return this.encoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoderFallback = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x00090D16 File Offset: 0x0008EF16
		// (set) Token: 0x06002773 RID: 10099 RVA: 0x00090D1E File Offset: 0x0008EF1E
		[ComVisible(false)]
		public DecoderFallback DecoderFallback
		{
			get
			{
				return this.decoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.decoderFallback = value;
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00090D4D File Offset: 0x0008EF4D
		[ComVisible(false)]
		public virtual object Clone()
		{
			Encoding encoding = (Encoding)base.MemberwiseClone();
			encoding.m_isReadOnly = false;
			return encoding;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x00090D61 File Offset: 0x0008EF61
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x00090D69 File Offset: 0x0008EF69
		public static Encoding ASCII
		{
			get
			{
				if (Encoding.asciiEncoding == null)
				{
					Encoding.asciiEncoding = new ASCIIEncoding();
				}
				return Encoding.asciiEncoding;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x00090D87 File Offset: 0x0008EF87
		private static Encoding Latin1
		{
			get
			{
				if (Encoding.latin1Encoding == null)
				{
					Encoding.latin1Encoding = new Latin1Encoding();
				}
				return Encoding.latin1Encoding;
			}
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x00090DA5 File Offset: 0x0008EFA5
		public virtual int GetByteCount(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetByteCount(chars, 0, chars.Length);
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00090DCC File Offset: 0x0008EFCC
		public virtual int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char[] array = s.ToCharArray();
			return this.GetByteCount(array, 0, array.Length);
		}

		// Token: 0x0600277A RID: 10106
		public abstract int GetByteCount(char[] chars, int index, int count);

		// Token: 0x0600277B RID: 10107 RVA: 0x00090DF9 File Offset: 0x0008EFF9
		public int GetByteCount(string str, int index, int count)
		{
			return this.GetByteCount(str.ToCharArray(), index, count);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x00090E0C File Offset: 0x0008F00C
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00090E72 File Offset: 0x0008F072
		[SecurityCritical]
		internal unsafe virtual int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			return this.GetByteCount(chars, count);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00090E7C File Offset: 0x0008F07C
		public virtual byte[] GetBytes(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetBytes(chars, 0, chars.Length);
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00090EA4 File Offset: 0x0008F0A4
		public virtual byte[] GetBytes(char[] chars, int index, int count)
		{
			byte[] array = new byte[this.GetByteCount(chars, index, count)];
			this.GetBytes(chars, index, count, array, 0);
			return array;
		}

		// Token: 0x06002780 RID: 10112
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

		// Token: 0x06002781 RID: 10113 RVA: 0x00090ED0 File Offset: 0x0008F0D0
		public virtual byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			byte[] array = new byte[this.GetByteCount(s)];
			this.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00090F14 File Offset: 0x0008F114
		public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return this.GetBytes(s.ToCharArray(), charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00090F36 File Offset: 0x0008F136
		[SecurityCritical]
		internal unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			return this.GetBytes(chars, charCount, bytes, byteCount);
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00090F44 File Offset: 0x0008F144
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00090FF4 File Offset: 0x0008F1F4
		public virtual int GetCharCount(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetCharCount(bytes, 0, bytes.Length);
		}

		// Token: 0x06002786 RID: 10118
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		// Token: 0x06002787 RID: 10119 RVA: 0x0009101C File Offset: 0x0008F21C
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0009107F File Offset: 0x0008F27F
		[SecurityCritical]
		internal unsafe virtual int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
		{
			return this.GetCharCount(bytes, count);
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00091089 File Offset: 0x0008F289
		public virtual char[] GetChars(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetChars(bytes, 0, bytes.Length);
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000910B0 File Offset: 0x0008F2B0
		public virtual char[] GetChars(byte[] bytes, int index, int count)
		{
			char[] array = new char[this.GetCharCount(bytes, index, count)];
			this.GetChars(bytes, index, count, array, 0);
			return array;
		}

		// Token: 0x0600278B RID: 10123
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x0600278C RID: 10124 RVA: 0x000910DC File Offset: 0x0008F2DC
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("Non-negative number required."));
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x0009118C File Offset: 0x0008F38C
		[SecurityCritical]
		internal unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
		{
			return this.GetChars(bytes, byteCount, chars, charCount);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00091199 File Offset: 0x0008F399
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe string GetString(byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			return string.CreateStringFromEncoding(bytes, byteCount, this);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000911D8 File Offset: 0x0008F3D8
		public unsafe virtual int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* ptr = nonNullPinnableReference;
				fixed (char* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
				{
					char* ptr2 = nonNullPinnableReference2;
					return this.GetChars(ptr, bytes.Length, ptr2, chars.Length);
				}
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00091210 File Offset: 0x0008F410
		public unsafe string GetString(ReadOnlySpan<byte> bytes)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* ptr = nonNullPinnableReference;
				return this.GetString(ptr, bytes.Length);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x00091235 File Offset: 0x0008F435
		public virtual int CodePage
		{
			get
			{
				return this.m_codePage;
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x0009123D File Offset: 0x0008F43D
		[ComVisible(false)]
		public bool IsAlwaysNormalized()
		{
			return this.IsAlwaysNormalized(NormalizationForm.FormC);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x0000408A File Offset: 0x0000228A
		[ComVisible(false)]
		public virtual bool IsAlwaysNormalized(NormalizationForm form)
		{
			return false;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00091246 File Offset: 0x0008F446
		public virtual Decoder GetDecoder()
		{
			return new Encoding.DefaultDecoder(this);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0009124E File Offset: 0x0008F44E
		[SecurityCritical]
		private static Encoding CreateDefaultEncoding()
		{
			Encoding encoding = EncodingHelper.GetDefaultEncoding();
			encoding.m_isReadOnly = true;
			return encoding;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0009125C File Offset: 0x0008F45C
		internal void setReadOnly(bool value = true)
		{
			this.m_isReadOnly = value;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x00091265 File Offset: 0x0008F465
		public static Encoding Default
		{
			[SecuritySafeCritical]
			get
			{
				if (Encoding.defaultEncoding == null)
				{
					Encoding.defaultEncoding = Encoding.CreateDefaultEncoding();
				}
				return Encoding.defaultEncoding;
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00091283 File Offset: 0x0008F483
		public virtual Encoder GetEncoder()
		{
			return new Encoding.DefaultEncoder(this);
		}

		// Token: 0x06002799 RID: 10137
		public abstract int GetMaxByteCount(int charCount);

		// Token: 0x0600279A RID: 10138
		public abstract int GetMaxCharCount(int byteCount);

		// Token: 0x0600279B RID: 10139 RVA: 0x0009128B File Offset: 0x0008F48B
		public virtual string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000912B0 File Offset: 0x0008F4B0
		public virtual string GetString(byte[] bytes, int index, int count)
		{
			return new string(this.GetChars(bytes, index, count));
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x000912C0 File Offset: 0x0008F4C0
		public static Encoding Unicode
		{
			get
			{
				if (Encoding.unicodeEncoding == null)
				{
					Encoding.unicodeEncoding = new UnicodeEncoding(false, true);
				}
				return Encoding.unicodeEncoding;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000912E0 File Offset: 0x0008F4E0
		public static Encoding BigEndianUnicode
		{
			get
			{
				if (Encoding.bigEndianUnicode == null)
				{
					Encoding.bigEndianUnicode = new UnicodeEncoding(true, true);
				}
				return Encoding.bigEndianUnicode;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x00091300 File Offset: 0x0008F500
		public static Encoding UTF7
		{
			get
			{
				if (Encoding.utf7Encoding == null)
				{
					Encoding.utf7Encoding = new UTF7Encoding();
				}
				return Encoding.utf7Encoding;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x0009131E File Offset: 0x0008F51E
		public static Encoding UTF8
		{
			get
			{
				if (Encoding.utf8Encoding == null)
				{
					Encoding.utf8Encoding = new UTF8Encoding(true);
				}
				return Encoding.utf8Encoding;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x0009133D File Offset: 0x0008F53D
		public static Encoding UTF32
		{
			get
			{
				if (Encoding.utf32Encoding == null)
				{
					Encoding.utf32Encoding = new UTF32Encoding(false, true);
				}
				return Encoding.utf32Encoding;
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00091360 File Offset: 0x0008F560
		public override bool Equals(object value)
		{
			Encoding encoding = value as Encoding;
			return encoding != null && (this.m_codePage == encoding.m_codePage && this.EncoderFallback.Equals(encoding.EncoderFallback)) && this.DecoderFallback.Equals(encoding.DecoderFallback);
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000913AD File Offset: 0x0008F5AD
		public override int GetHashCode()
		{
			return this.m_codePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000913CD File Offset: 0x0008F5CD
		internal virtual char[] GetBestFitUnicodeToBytesData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000913CD File Offset: 0x0008F5CD
		internal virtual char[] GetBestFitBytesToUnicodeData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000913D4 File Offset: 0x0008F5D4
		internal void ThrowBytesOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("The output byte buffer is too small to contain the encoded data, encoding '{0}' fallback '{1}'.", new object[]
			{
				this.EncodingName,
				this.EncoderFallback.GetType()
			}), "bytes");
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x00091407 File Offset: 0x0008F607
		[SecurityCritical]
		internal void ThrowBytesOverflow(EncoderNLS encoder, bool nothingEncoded)
		{
			if (encoder == null || encoder._throwOnOverflow || nothingEncoded)
			{
				if (encoder != null && encoder.InternalHasFallbackBuffer)
				{
					encoder.FallbackBuffer.InternalReset();
				}
				this.ThrowBytesOverflow();
			}
			encoder.ClearMustFlush();
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0009143B File Offset: 0x0008F63B
		internal void ThrowCharsOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("The output char buffer is too small to contain the decoded characters, encoding '{0}' fallback '{1}'.", new object[]
			{
				this.EncodingName,
				this.DecoderFallback.GetType()
			}), "chars");
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0009146E File Offset: 0x0008F66E
		[SecurityCritical]
		internal void ThrowCharsOverflow(DecoderNLS decoder, bool nothingDecoded)
		{
			if (decoder == null || decoder._throwOnOverflow || nothingDecoded)
			{
				if (decoder != null && decoder.InternalHasFallbackBuffer)
				{
					decoder.FallbackBuffer.InternalReset();
				}
				this.ThrowCharsOverflow();
			}
			decoder.ClearMustFlush();
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000914A4 File Offset: 0x0008F6A4
		public unsafe virtual int GetCharCount(ReadOnlySpan<byte> bytes)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* ptr = nonNullPinnableReference;
				return this.GetCharCount(ptr, bytes.Length);
			}
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000914CC File Offset: 0x0008F6CC
		public unsafe virtual int GetByteCount(ReadOnlySpan<char> chars)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* ptr = nonNullPinnableReference;
				return this.GetByteCount(ptr, chars.Length);
			}
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000914F4 File Offset: 0x0008F6F4
		public unsafe virtual int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* ptr = nonNullPinnableReference;
				fixed (byte* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
				{
					byte* ptr2 = nonNullPinnableReference2;
					return this.GetBytes(ptr, chars.Length, ptr2, bytes.Length);
				}
			}
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0009152C File Offset: 0x0008F72C
		public unsafe byte[] GetBytes(string s, int index, int count)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", "String reference not set to an instance of a String.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (index > s.Length - count)
			{
				throw new ArgumentOutOfRangeException("index", "Index and count must refer to a location within the string.");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int byteCount = this.GetByteCount(ptr + index, count);
			if (byteCount == 0)
			{
				return Array.Empty<byte>();
			}
			byte[] array = new byte[byteCount];
			fixed (byte* ptr2 = &array[0])
			{
				byte* ptr3 = ptr2;
				this.GetBytes(ptr + index, count, ptr3, byteCount);
			}
			return array;
		}

		// Token: 0x04001CDF RID: 7391
		private static volatile Encoding defaultEncoding;

		// Token: 0x04001CE0 RID: 7392
		private static volatile Encoding unicodeEncoding;

		// Token: 0x04001CE1 RID: 7393
		private static volatile Encoding bigEndianUnicode;

		// Token: 0x04001CE2 RID: 7394
		private static volatile Encoding utf7Encoding;

		// Token: 0x04001CE3 RID: 7395
		private static volatile Encoding utf8Encoding;

		// Token: 0x04001CE4 RID: 7396
		private static volatile Encoding utf32Encoding;

		// Token: 0x04001CE5 RID: 7397
		private static volatile Encoding asciiEncoding;

		// Token: 0x04001CE6 RID: 7398
		private static volatile Encoding latin1Encoding;

		// Token: 0x04001CE7 RID: 7399
		private static volatile Dictionary<int, Encoding> encodings;

		// Token: 0x04001CE8 RID: 7400
		private const int MIMECONTF_MAILNEWS = 1;

		// Token: 0x04001CE9 RID: 7401
		private const int MIMECONTF_BROWSER = 2;

		// Token: 0x04001CEA RID: 7402
		private const int MIMECONTF_SAVABLE_MAILNEWS = 256;

		// Token: 0x04001CEB RID: 7403
		private const int MIMECONTF_SAVABLE_BROWSER = 512;

		// Token: 0x04001CEC RID: 7404
		private const int CodePageDefault = 0;

		// Token: 0x04001CED RID: 7405
		private const int CodePageNoOEM = 1;

		// Token: 0x04001CEE RID: 7406
		private const int CodePageNoMac = 2;

		// Token: 0x04001CEF RID: 7407
		private const int CodePageNoThread = 3;

		// Token: 0x04001CF0 RID: 7408
		private const int CodePageNoSymbol = 42;

		// Token: 0x04001CF1 RID: 7409
		private const int CodePageUnicode = 1200;

		// Token: 0x04001CF2 RID: 7410
		private const int CodePageBigEndian = 1201;

		// Token: 0x04001CF3 RID: 7411
		private const int CodePageWindows1252 = 1252;

		// Token: 0x04001CF4 RID: 7412
		private const int CodePageMacGB2312 = 10008;

		// Token: 0x04001CF5 RID: 7413
		private const int CodePageGB2312 = 20936;

		// Token: 0x04001CF6 RID: 7414
		private const int CodePageMacKorean = 10003;

		// Token: 0x04001CF7 RID: 7415
		private const int CodePageDLLKorean = 20949;

		// Token: 0x04001CF8 RID: 7416
		private const int ISO2022JP = 50220;

		// Token: 0x04001CF9 RID: 7417
		private const int ISO2022JPESC = 50221;

		// Token: 0x04001CFA RID: 7418
		private const int ISO2022JPSISO = 50222;

		// Token: 0x04001CFB RID: 7419
		private const int ISOKorean = 50225;

		// Token: 0x04001CFC RID: 7420
		private const int ISOSimplifiedCN = 50227;

		// Token: 0x04001CFD RID: 7421
		private const int EUCJP = 51932;

		// Token: 0x04001CFE RID: 7422
		private const int ChineseHZ = 52936;

		// Token: 0x04001CFF RID: 7423
		private const int DuplicateEUCCN = 51936;

		// Token: 0x04001D00 RID: 7424
		private const int EUCCN = 936;

		// Token: 0x04001D01 RID: 7425
		private const int EUCKR = 51949;

		// Token: 0x04001D02 RID: 7426
		internal const int CodePageASCII = 20127;

		// Token: 0x04001D03 RID: 7427
		internal const int ISO_8859_1 = 28591;

		// Token: 0x04001D04 RID: 7428
		private const int ISCIIAssemese = 57006;

		// Token: 0x04001D05 RID: 7429
		private const int ISCIIBengali = 57003;

		// Token: 0x04001D06 RID: 7430
		private const int ISCIIDevanagari = 57002;

		// Token: 0x04001D07 RID: 7431
		private const int ISCIIGujarathi = 57010;

		// Token: 0x04001D08 RID: 7432
		private const int ISCIIKannada = 57008;

		// Token: 0x04001D09 RID: 7433
		private const int ISCIIMalayalam = 57009;

		// Token: 0x04001D0A RID: 7434
		private const int ISCIIOriya = 57007;

		// Token: 0x04001D0B RID: 7435
		private const int ISCIIPanjabi = 57011;

		// Token: 0x04001D0C RID: 7436
		private const int ISCIITamil = 57004;

		// Token: 0x04001D0D RID: 7437
		private const int ISCIITelugu = 57005;

		// Token: 0x04001D0E RID: 7438
		private const int GB18030 = 54936;

		// Token: 0x04001D0F RID: 7439
		private const int ISO_8859_8I = 38598;

		// Token: 0x04001D10 RID: 7440
		private const int ISO_8859_8_Visual = 28598;

		// Token: 0x04001D11 RID: 7441
		private const int ENC50229 = 50229;

		// Token: 0x04001D12 RID: 7442
		private const int CodePageUTF7 = 65000;

		// Token: 0x04001D13 RID: 7443
		private const int CodePageUTF8 = 65001;

		// Token: 0x04001D14 RID: 7444
		private const int CodePageUTF32 = 12000;

		// Token: 0x04001D15 RID: 7445
		private const int CodePageUTF32BE = 12001;

		// Token: 0x04001D16 RID: 7446
		internal int m_codePage;

		// Token: 0x04001D17 RID: 7447
		internal CodePageDataItem dataItem;

		// Token: 0x04001D18 RID: 7448
		[NonSerialized]
		internal bool m_deserializedFromEverett;

		// Token: 0x04001D19 RID: 7449
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04001D1A RID: 7450
		[OptionalField(VersionAdded = 2)]
		internal EncoderFallback encoderFallback;

		// Token: 0x04001D1B RID: 7451
		[OptionalField(VersionAdded = 2)]
		internal DecoderFallback decoderFallback;

		// Token: 0x04001D1C RID: 7452
		private static object s_InternalSyncObject;

		// Token: 0x0200038D RID: 909
		[Serializable]
		internal class DefaultEncoder : Encoder, ISerializable, IObjectReference
		{
			// Token: 0x060027AE RID: 10158 RVA: 0x000915DD File Offset: 0x0008F7DD
			public DefaultEncoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x060027AF RID: 10159 RVA: 0x000915F4 File Offset: 0x0008F7F4
			internal DefaultEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this._fallback = (EncoderFallback)info.GetValue("_fallback", typeof(EncoderFallback));
					this.charLeftOver = (char)info.GetValue("charLeftOver", typeof(char));
				}
				catch (SerializationException)
				{
				}
			}

			// Token: 0x060027B0 RID: 10160 RVA: 0x0009168C File Offset: 0x0008F88C
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Encoder encoder = this.m_encoding.GetEncoder();
				if (this._fallback != null)
				{
					encoder._fallback = this._fallback;
				}
				if (this.charLeftOver != '\0')
				{
					EncoderNLS encoderNLS = encoder as EncoderNLS;
					if (encoderNLS != null)
					{
						encoderNLS._charLeftOver = this.charLeftOver;
					}
				}
				return encoder;
			}

			// Token: 0x060027B1 RID: 10161 RVA: 0x000916E2 File Offset: 0x0008F8E2
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x060027B2 RID: 10162 RVA: 0x00091703 File Offset: 0x0008F903
			public override int GetByteCount(char[] chars, int index, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, index, count);
			}

			// Token: 0x060027B3 RID: 10163 RVA: 0x00091713 File Offset: 0x0008F913
			[SecurityCritical]
			public unsafe override int GetByteCount(char* chars, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, count);
			}

			// Token: 0x060027B4 RID: 10164 RVA: 0x00091722 File Offset: 0x0008F922
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
			}

			// Token: 0x060027B5 RID: 10165 RVA: 0x00091736 File Offset: 0x0008F936
			[SecurityCritical]
			public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount);
			}

			// Token: 0x04001D1D RID: 7453
			private Encoding m_encoding;

			// Token: 0x04001D1E RID: 7454
			[NonSerialized]
			private bool m_hasInitializedEncoding;

			// Token: 0x04001D1F RID: 7455
			[NonSerialized]
			internal char charLeftOver;
		}

		// Token: 0x0200038E RID: 910
		[Serializable]
		internal class DefaultDecoder : Decoder, ISerializable, IObjectReference
		{
			// Token: 0x060027B6 RID: 10166 RVA: 0x00091748 File Offset: 0x0008F948
			public DefaultDecoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x060027B7 RID: 10167 RVA: 0x00091760 File Offset: 0x0008F960
			internal DefaultDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this._fallback = (DecoderFallback)info.GetValue("_fallback", typeof(DecoderFallback));
				}
				catch (SerializationException)
				{
					this._fallback = null;
				}
			}

			// Token: 0x060027B8 RID: 10168 RVA: 0x000917E0 File Offset: 0x0008F9E0
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Decoder decoder = this.m_encoding.GetDecoder();
				if (this._fallback != null)
				{
					decoder._fallback = this._fallback;
				}
				return decoder;
			}

			// Token: 0x060027B9 RID: 10169 RVA: 0x00091818 File Offset: 0x0008FA18
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x060027BA RID: 10170 RVA: 0x000863CE File Offset: 0x000845CE
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return this.GetCharCount(bytes, index, count, false);
			}

			// Token: 0x060027BB RID: 10171 RVA: 0x00091839 File Offset: 0x0008FA39
			public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, index, count);
			}

			// Token: 0x060027BC RID: 10172 RVA: 0x00091849 File Offset: 0x0008FA49
			[SecurityCritical]
			public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, count);
			}

			// Token: 0x060027BD RID: 10173 RVA: 0x000864A6 File Offset: 0x000846A6
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
			}

			// Token: 0x060027BE RID: 10174 RVA: 0x00091858 File Offset: 0x0008FA58
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			// Token: 0x060027BF RID: 10175 RVA: 0x0009186C File Offset: 0x0008FA6C
			[SecurityCritical]
			public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteCount, chars, charCount);
			}

			// Token: 0x04001D20 RID: 7456
			private Encoding m_encoding;

			// Token: 0x04001D21 RID: 7457
			[NonSerialized]
			private bool m_hasInitializedEncoding;
		}

		// Token: 0x0200038F RID: 911
		internal class EncodingCharBuffer
		{
			// Token: 0x060027C0 RID: 10176 RVA: 0x00091880 File Offset: 0x0008FA80
			[SecurityCritical]
			internal unsafe EncodingCharBuffer(Encoding enc, DecoderNLS decoder, char* charStart, int charCount, byte* byteStart, int byteCount)
			{
				this.enc = enc;
				this.decoder = decoder;
				this.chars = charStart;
				this.charStart = charStart;
				this.charEnd = charStart + charCount;
				this.byteStart = byteStart;
				this.bytes = byteStart;
				this.byteEnd = byteStart + byteCount;
				if (this.decoder == null)
				{
					this.fallbackBuffer = enc.DecoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.decoder.FallbackBuffer;
				}
				this.fallbackBuffer.InternalInitialize(this.bytes, this.charEnd);
			}

			// Token: 0x060027C1 RID: 10177 RVA: 0x0009191C File Offset: 0x0008FB1C
			[SecurityCritical]
			internal unsafe bool AddChar(char ch, int numBytes)
			{
				if (this.chars != null)
				{
					if (this.chars >= this.charEnd)
					{
						this.bytes -= numBytes;
						this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
						return false;
					}
					char* ptr = this.chars;
					this.chars = ptr + 1;
					*ptr = ch;
				}
				this.charCountResult++;
				return true;
			}

			// Token: 0x060027C2 RID: 10178 RVA: 0x00091995 File Offset: 0x0008FB95
			[SecurityCritical]
			internal bool AddChar(char ch)
			{
				return this.AddChar(ch, 1);
			}

			// Token: 0x060027C3 RID: 10179 RVA: 0x000919A0 File Offset: 0x0008FBA0
			[SecurityCritical]
			internal bool AddChar(char ch1, char ch2, int numBytes)
			{
				if (this.chars >= this.charEnd - 1)
				{
					this.bytes -= numBytes;
					this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
					return false;
				}
				return this.AddChar(ch1, numBytes) && this.AddChar(ch2, numBytes);
			}

			// Token: 0x060027C4 RID: 10180 RVA: 0x00091A03 File Offset: 0x0008FC03
			[SecurityCritical]
			internal void AdjustBytes(int count)
			{
				this.bytes += count;
			}

			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00091A13 File Offset: 0x0008FC13
			internal bool MoreData
			{
				[SecurityCritical]
				get
				{
					return this.bytes < this.byteEnd;
				}
			}

			// Token: 0x060027C6 RID: 10182 RVA: 0x00091A23 File Offset: 0x0008FC23
			[SecurityCritical]
			internal bool EvenMoreData(int count)
			{
				return this.bytes == this.byteEnd - count;
			}

			// Token: 0x060027C7 RID: 10183 RVA: 0x00091A38 File Offset: 0x0008FC38
			[SecurityCritical]
			internal unsafe byte GetNextByte()
			{
				if (this.bytes >= this.byteEnd)
				{
					return 0;
				}
				byte* ptr = this.bytes;
				this.bytes = ptr + 1;
				return *ptr;
			}

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x060027C8 RID: 10184 RVA: 0x00091A67 File Offset: 0x0008FC67
			internal int BytesUsed
			{
				[SecurityCritical]
				get
				{
					return (int)((long)(this.bytes - this.byteStart));
				}
			}

			// Token: 0x060027C9 RID: 10185 RVA: 0x00091A7C File Offset: 0x0008FC7C
			[SecurityCritical]
			internal bool Fallback(byte fallbackByte)
			{
				byte[] array = new byte[] { fallbackByte };
				return this.Fallback(array);
			}

			// Token: 0x060027CA RID: 10186 RVA: 0x00091A9C File Offset: 0x0008FC9C
			[SecurityCritical]
			internal bool Fallback(byte byte1, byte byte2)
			{
				byte[] array = new byte[] { byte1, byte2 };
				return this.Fallback(array);
			}

			// Token: 0x060027CB RID: 10187 RVA: 0x00091AC0 File Offset: 0x0008FCC0
			[SecurityCritical]
			internal bool Fallback(byte byte1, byte byte2, byte byte3, byte byte4)
			{
				byte[] array = new byte[] { byte1, byte2, byte3, byte4 };
				return this.Fallback(array);
			}

			// Token: 0x060027CC RID: 10188 RVA: 0x00091AEC File Offset: 0x0008FCEC
			[SecurityCritical]
			internal unsafe bool Fallback(byte[] byteBuffer)
			{
				if (this.chars != null)
				{
					char* ptr = this.chars;
					if (!this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes, ref this.chars))
					{
						this.bytes -= byteBuffer.Length;
						this.fallbackBuffer.InternalReset();
						this.enc.ThrowCharsOverflow(this.decoder, this.chars == this.charStart);
						return false;
					}
					this.charCountResult += (int)((long)(this.chars - ptr));
				}
				else
				{
					this.charCountResult += this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes);
				}
				return true;
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x060027CD RID: 10189 RVA: 0x00091B9B File Offset: 0x0008FD9B
			internal int Count
			{
				get
				{
					return this.charCountResult;
				}
			}

			// Token: 0x04001D22 RID: 7458
			[SecurityCritical]
			private unsafe char* chars;

			// Token: 0x04001D23 RID: 7459
			[SecurityCritical]
			private unsafe char* charStart;

			// Token: 0x04001D24 RID: 7460
			[SecurityCritical]
			private unsafe char* charEnd;

			// Token: 0x04001D25 RID: 7461
			private int charCountResult;

			// Token: 0x04001D26 RID: 7462
			private Encoding enc;

			// Token: 0x04001D27 RID: 7463
			private DecoderNLS decoder;

			// Token: 0x04001D28 RID: 7464
			[SecurityCritical]
			private unsafe byte* byteStart;

			// Token: 0x04001D29 RID: 7465
			[SecurityCritical]
			private unsafe byte* byteEnd;

			// Token: 0x04001D2A RID: 7466
			[SecurityCritical]
			private unsafe byte* bytes;

			// Token: 0x04001D2B RID: 7467
			private DecoderFallbackBuffer fallbackBuffer;
		}

		// Token: 0x02000390 RID: 912
		internal class EncodingByteBuffer
		{
			// Token: 0x060027CE RID: 10190 RVA: 0x00091BA4 File Offset: 0x0008FDA4
			[SecurityCritical]
			internal unsafe EncodingByteBuffer(Encoding inEncoding, EncoderNLS inEncoder, byte* inByteStart, int inByteCount, char* inCharStart, int inCharCount)
			{
				this.enc = inEncoding;
				this.encoder = inEncoder;
				this.charStart = inCharStart;
				this.chars = inCharStart;
				this.charEnd = inCharStart + inCharCount;
				this.bytes = inByteStart;
				this.byteStart = inByteStart;
				this.byteEnd = inByteStart + inByteCount;
				if (this.encoder == null)
				{
					this.fallbackBuffer = this.enc.EncoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.encoder.FallbackBuffer;
					if (this.encoder._throwOnOverflow && this.encoder.InternalHasFallbackBuffer && this.fallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.", new object[]
						{
							this.encoder.Encoding.EncodingName,
							this.encoder.Fallback.GetType()
						}));
					}
				}
				this.fallbackBuffer.InternalInitialize(this.chars, this.charEnd, this.encoder, this.bytes != null);
			}

			// Token: 0x060027CF RID: 10191 RVA: 0x00091CBC File Offset: 0x0008FEBC
			[SecurityCritical]
			internal unsafe bool AddByte(byte b, int moreBytesExpected)
			{
				if (this.bytes != null)
				{
					if (this.bytes >= this.byteEnd - moreBytesExpected)
					{
						this.MovePrevious(true);
						return false;
					}
					byte* ptr = this.bytes;
					this.bytes = ptr + 1;
					*ptr = b;
				}
				this.byteCountResult++;
				return true;
			}

			// Token: 0x060027D0 RID: 10192 RVA: 0x00091D0E File Offset: 0x0008FF0E
			[SecurityCritical]
			internal bool AddByte(byte b1)
			{
				return this.AddByte(b1, 0);
			}

			// Token: 0x060027D1 RID: 10193 RVA: 0x00091D18 File Offset: 0x0008FF18
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2)
			{
				return this.AddByte(b1, b2, 0);
			}

			// Token: 0x060027D2 RID: 10194 RVA: 0x00091D23 File Offset: 0x0008FF23
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, int moreBytesExpected)
			{
				return this.AddByte(b1, 1 + moreBytesExpected) && this.AddByte(b2, moreBytesExpected);
			}

			// Token: 0x060027D3 RID: 10195 RVA: 0x00091D3B File Offset: 0x0008FF3B
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3)
			{
				return this.AddByte(b1, b2, b3, 0);
			}

			// Token: 0x060027D4 RID: 10196 RVA: 0x00091D47 File Offset: 0x0008FF47
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3, int moreBytesExpected)
			{
				return this.AddByte(b1, 2 + moreBytesExpected) && this.AddByte(b2, 1 + moreBytesExpected) && this.AddByte(b3, moreBytesExpected);
			}

			// Token: 0x060027D5 RID: 10197 RVA: 0x00091D6E File Offset: 0x0008FF6E
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3, byte b4)
			{
				return this.AddByte(b1, 3) && this.AddByte(b2, 2) && this.AddByte(b3, 1) && this.AddByte(b4, 0);
			}

			// Token: 0x060027D6 RID: 10198 RVA: 0x00091D9C File Offset: 0x0008FF9C
			[SecurityCritical]
			internal void MovePrevious(bool bThrow)
			{
				if (this.fallbackBuffer.bFallingBack)
				{
					this.fallbackBuffer.MovePrevious();
				}
				else if (this.chars != this.charStart)
				{
					this.chars--;
				}
				if (bThrow)
				{
					this.enc.ThrowBytesOverflow(this.encoder, this.bytes == this.byteStart);
				}
			}

			// Token: 0x060027D7 RID: 10199 RVA: 0x00091E02 File Offset: 0x00090002
			[SecurityCritical]
			internal bool Fallback(char charFallback)
			{
				return this.fallbackBuffer.InternalFallback(charFallback, ref this.chars);
			}

			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x060027D8 RID: 10200 RVA: 0x00091E16 File Offset: 0x00090016
			internal bool MoreData
			{
				[SecurityCritical]
				get
				{
					return this.fallbackBuffer.Remaining > 0 || this.chars < this.charEnd;
				}
			}

			// Token: 0x060027D9 RID: 10201 RVA: 0x00091E38 File Offset: 0x00090038
			[SecurityCritical]
			internal unsafe char GetNextChar()
			{
				char c = this.fallbackBuffer.InternalGetNextChar();
				if (c == '\0' && this.chars < this.charEnd)
				{
					char* ptr = this.chars;
					this.chars = ptr + 1;
					c = *ptr;
				}
				return c;
			}

			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x060027DA RID: 10202 RVA: 0x00091E76 File Offset: 0x00090076
			internal int CharsUsed
			{
				[SecurityCritical]
				get
				{
					return (int)((long)(this.chars - this.charStart));
				}
			}

			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x060027DB RID: 10203 RVA: 0x00091E89 File Offset: 0x00090089
			internal int Count
			{
				get
				{
					return this.byteCountResult;
				}
			}

			// Token: 0x04001D2C RID: 7468
			[SecurityCritical]
			private unsafe byte* bytes;

			// Token: 0x04001D2D RID: 7469
			[SecurityCritical]
			private unsafe byte* byteStart;

			// Token: 0x04001D2E RID: 7470
			[SecurityCritical]
			private unsafe byte* byteEnd;

			// Token: 0x04001D2F RID: 7471
			[SecurityCritical]
			private unsafe char* chars;

			// Token: 0x04001D30 RID: 7472
			[SecurityCritical]
			private unsafe char* charStart;

			// Token: 0x04001D31 RID: 7473
			[SecurityCritical]
			private unsafe char* charEnd;

			// Token: 0x04001D32 RID: 7474
			private int byteCountResult;

			// Token: 0x04001D33 RID: 7475
			private Encoding enc;

			// Token: 0x04001D34 RID: 7476
			private EncoderNLS encoder;

			// Token: 0x04001D35 RID: 7477
			internal EncoderFallbackBuffer fallbackBuffer;
		}
	}
}
