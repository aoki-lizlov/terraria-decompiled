using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CsvHelper.Configuration;

namespace CsvHelper
{
	// Token: 0x02000009 RID: 9
	public class CsvParser : ICsvParser, IDisposable
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022A5 File Offset: 0x000004A5
		public virtual CsvConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022AD File Offset: 0x000004AD
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000022B5 File Offset: 0x000004B5
		public virtual int FieldCount
		{
			[CompilerGenerated]
			get
			{
				return this.<FieldCount>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<FieldCount>k__BackingField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000022BE File Offset: 0x000004BE
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000022C6 File Offset: 0x000004C6
		public virtual long CharPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<CharPosition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<CharPosition>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000022CF File Offset: 0x000004CF
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000022D7 File Offset: 0x000004D7
		public virtual long BytePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<BytePosition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BytePosition>k__BackingField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022E0 File Offset: 0x000004E0
		public virtual int Row
		{
			get
			{
				return this.currentRow;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000022E8 File Offset: 0x000004E8
		public virtual int RawRow
		{
			get
			{
				return this.currentRawRow;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000022F0 File Offset: 0x000004F0
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000022F8 File Offset: 0x000004F8
		public virtual string RawRecord
		{
			[CompilerGenerated]
			get
			{
				return this.<RawRecord>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RawRecord>k__BackingField = value;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002301 File Offset: 0x00000501
		public CsvParser(TextReader reader)
			: this(reader, new CsvConfiguration())
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002310 File Offset: 0x00000510
		public CsvParser(TextReader reader, CsvConfiguration configuration)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.reader = reader;
			this.configuration = configuration;
			this.readerBuffer = new char[configuration.BufferSize];
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002360 File Offset: 0x00000560
		public virtual string[] Read()
		{
			this.CheckDisposed();
			string[] array2;
			try
			{
				if (this.configuration.HasExcelSeparator && !this.hasExcelSeparatorBeenRead)
				{
					this.ReadExcelSeparator();
				}
				string[] array = this.ReadLine();
				if (this.configuration.DetectColumnCountChanges && array != null && this.FieldCount > 0)
				{
					if (this.FieldCount == array.Length)
					{
						if (!Enumerable.Any<string>(array, (string field) => field == null))
						{
							goto IL_007E;
						}
					}
					throw new CsvBadDataException("An inconsistent number of columns has been detected.");
				}
				IL_007E:
				array2 = array;
			}
			catch (Exception ex)
			{
				ExceptionHelper.AddExceptionDataMessage(ex, this, null, null, default(int?), null);
				throw;
			}
			return array2;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002414 File Offset: 0x00000614
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002423 File Offset: 0x00000623
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing && this.reader != null)
			{
				this.reader.Dispose();
			}
			this.disposed = true;
			this.reader = null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002452 File Offset: 0x00000652
		protected virtual void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002470 File Offset: 0x00000670
		protected virtual void AddFieldToRecord(ref int recordPosition, string field, ref bool fieldIsBad)
		{
			if (this.record.Length < recordPosition + 1)
			{
				Array.Resize<string>(ref this.record, recordPosition + 1);
				if (this.currentRow == 1)
				{
					this.FieldCount = this.record.Length;
				}
			}
			if (fieldIsBad && this.configuration.ThrowOnBadData)
			{
				throw new CsvBadDataException(string.Format("Field: '{0}'", field));
			}
			if (fieldIsBad && this.configuration.BadDataCallback != null)
			{
				this.configuration.BadDataCallback.Invoke(field);
			}
			fieldIsBad = false;
			this.record[recordPosition] = field;
			recordPosition++;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002508 File Offset: 0x00000708
		protected virtual void AppendField(ref string field, int fieldStartPosition, int length)
		{
			field += new string(this.readerBuffer, fieldStartPosition, length);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002520 File Offset: 0x00000720
		protected virtual void UpdateBytePosition(int fieldStartPosition, int length)
		{
			if (this.configuration.CountBytes)
			{
				this.BytePosition += (long)this.configuration.Encoding.GetByteCount(this.readerBuffer, fieldStartPosition, length);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002558 File Offset: 0x00000758
		protected virtual string[] ReadLine()
		{
			string text = null;
			int num = this.readerBufferPosition;
			int num2 = this.readerBufferPosition;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			int num3 = 0;
			bool flag7 = false;
			int num4 = 0;
			this.record = new string[this.FieldCount];
			this.RawRecord = string.Empty;
			this.currentRow++;
			this.currentRawRow++;
			int num5;
			for (;;)
			{
				if (this.read)
				{
					this.cPrev = new char?(this.c);
				}
				num5 = this.readerBufferPosition - num;
				this.read = this.GetChar(out this.c, ref num, ref num2, ref text, ref flag3, flag7, ref num4, ref num5, flag4, flag5, flag, false);
				if (!this.read)
				{
					goto IL_0904;
				}
				this.readerBufferPosition++;
				long num6 = this.CharPosition;
				this.CharPosition = num6 + 1L;
				if (this.configuration.UseExcelLeadingZerosFormatForNumerics)
				{
					if (this.c == '=' && !flag6)
					{
						if (!flag7)
						{
							char? c = this.cPrev;
							if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 13))
							{
								c = this.cPrev;
								if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 10) && this.cPrev != null)
								{
									goto IL_01D3;
								}
							}
						}
						num5 = this.readerBufferPosition - num;
						char c2;
						this.GetChar(out c2, ref num, ref num2, ref text, ref flag3, flag7, ref num4, ref num5, flag4, flag5, flag, true);
						if (c2 == '"')
						{
							flag6 = true;
							continue;
						}
						goto IL_02ED;
					}
					IL_01D3:
					if (flag6)
					{
						if (this.c == '"')
						{
							char? c = this.cPrev;
							if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 61)
							{
								continue;
							}
						}
						if (char.IsDigit(this.c))
						{
							continue;
						}
						if (this.c != '"')
						{
							flag6 = false;
							continue;
						}
						char c3;
						bool @char = this.GetChar(out c3, ref num, ref num2, ref text, ref flag3, flag7, ref num4, ref num5, flag4, flag5, flag, true);
						if (c3 == this.configuration.Delimiter.get_Chars(0) || c3 == '\r' || c3 == '\n' || c3 == '\0')
						{
							this.AppendField(ref text, num, this.readerBufferPosition - num);
							this.UpdateBytePosition(num, this.readerBufferPosition - num);
							text = text.Trim(new char[] { '=', '"' });
							num = this.readerBufferPosition;
							if (!@char)
							{
								this.AddFieldToRecord(ref num4, text, ref flag3);
							}
							flag6 = false;
							continue;
						}
						continue;
					}
				}
				IL_02ED:
				if (this.c == this.configuration.Quote && !this.configuration.IgnoreQuotes)
				{
					if (!flag2)
					{
						if (!flag7)
						{
							char? c = this.cPrev;
							if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 13))
							{
								c = this.cPrev;
								if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 10) && this.cPrev != null)
								{
									goto IL_03BD;
								}
							}
						}
						flag2 = true;
					}
					IL_03BD:
					if (!flag2)
					{
						flag3 = true;
					}
					else
					{
						flag = !flag;
						if (num != this.readerBufferPosition - 1)
						{
							this.AppendField(ref text, num, this.readerBufferPosition - num - 1);
							this.UpdateBytePosition(num, this.readerBufferPosition - num);
						}
						char? c = this.cPrev;
						if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) != (int)this.configuration.Quote || !flag)
						{
							if (flag)
							{
								goto IL_04AB;
							}
							c = this.cPrev;
							if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == (int)this.configuration.Quote || this.readerBufferPosition == 1)
							{
								goto IL_04AB;
							}
							IL_04BA:
							num = this.readerBufferPosition;
							goto IL_04C1;
							IL_04AB:
							this.UpdateBytePosition(num, this.readerBufferPosition - num);
							goto IL_04BA;
						}
						IL_04C1:
						flag7 = false;
					}
				}
				else
				{
					flag7 = false;
					if (flag2 && flag)
					{
						if (this.c != '\r')
						{
							if (this.c != '\n')
							{
								continue;
							}
							char? c = this.cPrev;
							if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) != 13))
							{
								continue;
							}
						}
						this.currentRawRow++;
					}
					else
					{
						char? c = this.cPrev;
						if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == (int)this.configuration.Quote && !this.configuration.IgnoreQuotes)
						{
							if (this.c != this.configuration.Delimiter.get_Chars(0) && this.c != '\r' && this.c != '\n')
							{
								flag3 = true;
							}
							flag2 = false;
						}
						if (!flag4 || this.c == '\r' || this.c == '\n')
						{
							if (this.c == this.configuration.Delimiter.get_Chars(0) || flag5)
							{
								if (!flag5)
								{
									num3 = 0;
									this.AppendField(ref text, num, this.readerBufferPosition - num - 1);
									this.UpdateBytePosition(num, this.readerBufferPosition - num);
									this.AddFieldToRecord(ref num4, text, ref flag3);
									num = this.readerBufferPosition;
									text = null;
									flag5 = true;
								}
								if (num3 == this.configuration.Delimiter.Length - 1)
								{
									this.UpdateBytePosition(num, this.readerBufferPosition - num);
									flag5 = false;
									flag7 = true;
									num = this.readerBufferPosition;
								}
								else if (this.configuration.Delimiter.get_Chars(num3) != this.c)
								{
									num4--;
									num -= num3 + 1;
									flag5 = false;
								}
								else
								{
									num3++;
								}
							}
							else if (this.c == '\r' || this.c == '\n')
							{
								num5 = this.readerBufferPosition - num - 1;
								if (this.c == '\r')
								{
									char c4;
									this.GetChar(out c4, ref num, ref num2, ref text, ref flag3, flag7, ref num4, ref num5, flag4, flag5, flag, true);
									if (c4 == '\n')
									{
										this.readerBufferPosition++;
										num6 = this.CharPosition;
										this.CharPosition = num6 + 1L;
									}
								}
								c = this.cPrev;
								bool flag8;
								if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 13))
								{
									c = this.cPrev;
									flag8 = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 10;
								}
								else
								{
									flag8 = true;
								}
								if (!flag8 && !flag4 && this.cPrev != null)
								{
									break;
								}
								this.UpdateBytePosition(num, this.readerBufferPosition - num);
								num = this.readerBufferPosition;
								flag4 = false;
								if (!this.configuration.IgnoreBlankLines)
								{
									goto IL_0904;
								}
								this.currentRow++;
							}
							else if (this.configuration.AllowComments && this.c == this.configuration.Comment)
							{
								c = this.cPrev;
								if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 13))
								{
									c = this.cPrev;
									if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 10) && this.cPrev != null)
									{
										continue;
									}
								}
								flag4 = true;
							}
						}
					}
				}
			}
			this.AppendField(ref text, num, num5);
			this.UpdateBytePosition(num, this.readerBufferPosition - num);
			this.AddFieldToRecord(ref num4, text, ref flag3);
			IL_0904:
			if (this.record != null)
			{
				this.RawRecord += new string(this.readerBuffer, num2, this.readerBufferPosition - num2);
			}
			return this.record;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002E9C File Offset: 0x0000109C
		protected bool GetChar(out char ch, ref int fieldStartPosition, ref int rawFieldStartPosition, ref string field, ref bool fieldIsBad, bool prevCharWasDelimiter, ref int recordPosition, ref int fieldLength, bool inComment, bool inDelimiter, bool inQuotes, bool isPeek)
		{
			if (this.readerBufferPosition == this.charsRead)
			{
				if (!inDelimiter && !inComment)
				{
					this.AppendField(ref field, fieldStartPosition, fieldLength);
				}
				this.UpdateBytePosition(fieldStartPosition, this.readerBufferPosition - fieldStartPosition);
				fieldLength = 0;
				this.RawRecord += new string(this.readerBuffer, rawFieldStartPosition, this.readerBufferPosition - rawFieldStartPosition);
				this.charsRead = this.reader.Read(this.readerBuffer, 0, this.readerBuffer.Length);
				this.readerBufferPosition = 0;
				fieldStartPosition = 0;
				rawFieldStartPosition = 0;
				if (this.charsRead == 0)
				{
					if (isPeek)
					{
						ch = '\0';
						return false;
					}
					if ((this.c != '\r' && this.c != '\n' && this.c != '\0' && !inComment) || inQuotes)
					{
						if (prevCharWasDelimiter)
						{
							field = "";
						}
						this.AddFieldToRecord(ref recordPosition, field, ref fieldIsBad);
					}
					else
					{
						this.RawRecord = null;
						this.record = null;
					}
					ch = '\0';
					return false;
				}
			}
			ch = this.readerBuffer[this.readerBufferPosition];
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002FB0 File Offset: 0x000011B0
		protected virtual void ReadExcelSeparator()
		{
			string text = this.reader.ReadLine();
			if (text != null)
			{
				this.configuration.Delimiter = text.Substring(4);
			}
			this.hasExcelSeparatorBeenRead = true;
		}

		// Token: 0x04000004 RID: 4
		private bool disposed;

		// Token: 0x04000005 RID: 5
		private TextReader reader;

		// Token: 0x04000006 RID: 6
		private readonly char[] readerBuffer;

		// Token: 0x04000007 RID: 7
		private int readerBufferPosition;

		// Token: 0x04000008 RID: 8
		private int charsRead;

		// Token: 0x04000009 RID: 9
		private string[] record;

		// Token: 0x0400000A RID: 10
		private int currentRow;

		// Token: 0x0400000B RID: 11
		private int currentRawRow;

		// Token: 0x0400000C RID: 12
		private readonly CsvConfiguration configuration;

		// Token: 0x0400000D RID: 13
		private char? cPrev;

		// Token: 0x0400000E RID: 14
		private char c;

		// Token: 0x0400000F RID: 15
		private bool read;

		// Token: 0x04000010 RID: 16
		private bool hasExcelSeparatorBeenRead;

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		private int <FieldCount>k__BackingField;

		// Token: 0x04000012 RID: 18
		[CompilerGenerated]
		private long <CharPosition>k__BackingField;

		// Token: 0x04000013 RID: 19
		[CompilerGenerated]
		private long <BytePosition>k__BackingField;

		// Token: 0x04000014 RID: 20
		[CompilerGenerated]
		private string <RawRecord>k__BackingField;

		// Token: 0x02000041 RID: 65
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000242 RID: 578 RVA: 0x00007CFC File Offset: 0x00005EFC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000243 RID: 579 RVA: 0x00002253 File Offset: 0x00000453
			public <>c()
			{
			}

			// Token: 0x06000244 RID: 580 RVA: 0x00007D08 File Offset: 0x00005F08
			internal bool <Read>b__37_0(string field)
			{
				return field == null;
			}

			// Token: 0x04000077 RID: 119
			public static readonly CsvParser.<>c <>9 = new CsvParser.<>c();

			// Token: 0x04000078 RID: 120
			public static Func<string, bool> <>9__37_0;
		}
	}
}
