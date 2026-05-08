using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Resources
{
	// Token: 0x02000848 RID: 2120
	internal class Win32VersionResource : Win32Resource
	{
		// Token: 0x060047B6 RID: 18358 RVA: 0x000ECCB4 File Offset: 0x000EAEB4
		public Win32VersionResource(int id, int language, bool compilercontext)
			: base(Win32ResourceType.RT_VERSION, id, language)
		{
			this.signature = (long)((ulong)(-17890115));
			this.struct_version = 65536;
			this.file_flags_mask = 63;
			this.file_flags = 0;
			this.file_os = 4;
			this.file_type = 2;
			this.file_subtype = 0;
			this.file_date = 0L;
			this.file_lang = (compilercontext ? 0 : 127);
			this.file_codepage = 1200;
			this.properties = new Hashtable();
			string text = (compilercontext ? string.Empty : " ");
			foreach (string text2 in this.WellKnownProperties)
			{
				this.properties[text2] = text;
			}
			this.LegalCopyright = " ";
			this.FileDescription = " ";
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x000ECDCC File Offset: 0x000EAFCC
		// (set) Token: 0x060047B8 RID: 18360 RVA: 0x000ECE60 File Offset: 0x000EB060
		public string Version
		{
			get
			{
				return string.Concat(new string[]
				{
					(this.file_version >> 48).ToString(),
					".",
					((this.file_version >> 32) & 65535L).ToString(),
					".",
					((this.file_version >> 16) & 65535L).ToString(),
					".",
					(this.file_version & 65535L).ToString()
				});
			}
			set
			{
				long[] array = new long[4];
				if (value != null)
				{
					string[] array2 = value.Split('.', StringSplitOptions.None);
					try
					{
						for (int i = 0; i < array2.Length; i++)
						{
							if (i < array.Length)
							{
								array[i] = (long)int.Parse(array2[i]);
							}
						}
					}
					catch (FormatException)
					{
					}
				}
				this.file_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
				this.properties["FileVersion"] = this.Version;
			}
		}

		// Token: 0x17000B0C RID: 2828
		public virtual string this[string key]
		{
			set
			{
				this.properties[key] = value;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x000ECEFB File Offset: 0x000EB0FB
		// (set) Token: 0x060047BB RID: 18363 RVA: 0x000ECF12 File Offset: 0x000EB112
		public virtual string Comments
		{
			get
			{
				return (string)this.properties["Comments"];
			}
			set
			{
				this.properties["Comments"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x000ECF39 File Offset: 0x000EB139
		// (set) Token: 0x060047BD RID: 18365 RVA: 0x000ECF50 File Offset: 0x000EB150
		public virtual string CompanyName
		{
			get
			{
				return (string)this.properties["CompanyName"];
			}
			set
			{
				this.properties["CompanyName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060047BE RID: 18366 RVA: 0x000ECF77 File Offset: 0x000EB177
		// (set) Token: 0x060047BF RID: 18367 RVA: 0x000ECF8E File Offset: 0x000EB18E
		public virtual string LegalCopyright
		{
			get
			{
				return (string)this.properties["LegalCopyright"];
			}
			set
			{
				this.properties["LegalCopyright"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060047C0 RID: 18368 RVA: 0x000ECFB5 File Offset: 0x000EB1B5
		// (set) Token: 0x060047C1 RID: 18369 RVA: 0x000ECFCC File Offset: 0x000EB1CC
		public virtual string LegalTrademarks
		{
			get
			{
				return (string)this.properties["LegalTrademarks"];
			}
			set
			{
				this.properties["LegalTrademarks"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060047C2 RID: 18370 RVA: 0x000ECFF3 File Offset: 0x000EB1F3
		// (set) Token: 0x060047C3 RID: 18371 RVA: 0x000ED00A File Offset: 0x000EB20A
		public virtual string OriginalFilename
		{
			get
			{
				return (string)this.properties["OriginalFilename"];
			}
			set
			{
				this.properties["OriginalFilename"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060047C4 RID: 18372 RVA: 0x000ED031 File Offset: 0x000EB231
		// (set) Token: 0x060047C5 RID: 18373 RVA: 0x000ED048 File Offset: 0x000EB248
		public virtual string ProductName
		{
			get
			{
				return (string)this.properties["ProductName"];
			}
			set
			{
				this.properties["ProductName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060047C6 RID: 18374 RVA: 0x000ED06F File Offset: 0x000EB26F
		// (set) Token: 0x060047C7 RID: 18375 RVA: 0x000ED088 File Offset: 0x000EB288
		public virtual string ProductVersion
		{
			get
			{
				return (string)this.properties["ProductVersion"];
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split('.', StringSplitOptions.None);
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["ProductVersion"] = value;
				this.product_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060047C8 RID: 18376 RVA: 0x000ED11C File Offset: 0x000EB31C
		// (set) Token: 0x060047C9 RID: 18377 RVA: 0x000ED133 File Offset: 0x000EB333
		public virtual string InternalName
		{
			get
			{
				return (string)this.properties["InternalName"];
			}
			set
			{
				this.properties["InternalName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x000ED15A File Offset: 0x000EB35A
		// (set) Token: 0x060047CB RID: 18379 RVA: 0x000ED171 File Offset: 0x000EB371
		public virtual string FileDescription
		{
			get
			{
				return (string)this.properties["FileDescription"];
			}
			set
			{
				this.properties["FileDescription"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060047CC RID: 18380 RVA: 0x000ED198 File Offset: 0x000EB398
		// (set) Token: 0x060047CD RID: 18381 RVA: 0x000ED1A0 File Offset: 0x000EB3A0
		public virtual int FileLanguage
		{
			get
			{
				return this.file_lang;
			}
			set
			{
				this.file_lang = value;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x000ED1A9 File Offset: 0x000EB3A9
		// (set) Token: 0x060047CF RID: 18383 RVA: 0x000ED1C0 File Offset: 0x000EB3C0
		public virtual string FileVersion
		{
			get
			{
				return (string)this.properties["FileVersion"];
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split('.', StringSplitOptions.None);
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["FileVersion"] = value;
				this.file_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
			}
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x000ED254 File Offset: 0x000EB454
		private void emit_padding(BinaryWriter w)
		{
			if (w.BaseStream.Position % 4L != 0L)
			{
				w.Write(0);
			}
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x000ED270 File Offset: 0x000EB470
		private void patch_length(BinaryWriter w, long len_pos)
		{
			Stream baseStream = w.BaseStream;
			long position = baseStream.Position;
			baseStream.Position = len_pos;
			w.Write((short)(position - len_pos));
			baseStream.Position = position;
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x000ED2A4 File Offset: 0x000EB4A4
		public override void WriteTo(Stream ms)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(ms, Encoding.Unicode))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(52);
				binaryWriter.Write(0);
				binaryWriter.Write("VS_VERSION_INFO".ToCharArray());
				binaryWriter.Write(0);
				this.emit_padding(binaryWriter);
				binaryWriter.Write((uint)this.signature);
				binaryWriter.Write(this.struct_version);
				binaryWriter.Write((int)(this.file_version >> 32));
				binaryWriter.Write((int)(this.file_version & (long)((ulong)(-1))));
				binaryWriter.Write((int)(this.product_version >> 32));
				binaryWriter.Write((int)(this.product_version & (long)((ulong)(-1))));
				binaryWriter.Write(this.file_flags_mask);
				binaryWriter.Write(this.file_flags);
				binaryWriter.Write(this.file_os);
				binaryWriter.Write(this.file_type);
				binaryWriter.Write(this.file_subtype);
				binaryWriter.Write((int)(this.file_date >> 32));
				binaryWriter.Write((int)(this.file_date & (long)((ulong)(-1))));
				this.emit_padding(binaryWriter);
				long position = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("VarFileInfo".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				long position2 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(4);
				binaryWriter.Write(0);
				binaryWriter.Write("Translation".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				binaryWriter.Write((short)this.file_lang);
				binaryWriter.Write((short)this.file_codepage);
				this.patch_length(binaryWriter, position2);
				this.patch_length(binaryWriter, position);
				long position3 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("StringFileInfo".ToCharArray());
				this.emit_padding(binaryWriter);
				long position4 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write(string.Format("{0:x4}{1:x4}", this.file_lang, this.file_codepage).ToCharArray());
				this.emit_padding(binaryWriter);
				foreach (object obj in this.properties.Keys)
				{
					string text = (string)obj;
					string text2 = (string)this.properties[text];
					long position5 = ms.Position;
					binaryWriter.Write(0);
					binaryWriter.Write((short)(text2.ToCharArray().Length + 1));
					binaryWriter.Write(1);
					binaryWriter.Write(text.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					binaryWriter.Write(text2.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					this.patch_length(binaryWriter, position5);
				}
				this.patch_length(binaryWriter, position4);
				this.patch_length(binaryWriter, position3);
				this.patch_length(binaryWriter, 0L);
			}
		}

		// Token: 0x04002DB0 RID: 11696
		public string[] WellKnownProperties = new string[] { "Comments", "CompanyName", "FileVersion", "InternalName", "LegalTrademarks", "OriginalFilename", "ProductName", "ProductVersion" };

		// Token: 0x04002DB1 RID: 11697
		private long signature;

		// Token: 0x04002DB2 RID: 11698
		private int struct_version;

		// Token: 0x04002DB3 RID: 11699
		private long file_version;

		// Token: 0x04002DB4 RID: 11700
		private long product_version;

		// Token: 0x04002DB5 RID: 11701
		private int file_flags_mask;

		// Token: 0x04002DB6 RID: 11702
		private int file_flags;

		// Token: 0x04002DB7 RID: 11703
		private int file_os;

		// Token: 0x04002DB8 RID: 11704
		private int file_type;

		// Token: 0x04002DB9 RID: 11705
		private int file_subtype;

		// Token: 0x04002DBA RID: 11706
		private long file_date;

		// Token: 0x04002DBB RID: 11707
		private int file_lang;

		// Token: 0x04002DBC RID: 11708
		private int file_codepage;

		// Token: 0x04002DBD RID: 11709
		private Hashtable properties;
	}
}
