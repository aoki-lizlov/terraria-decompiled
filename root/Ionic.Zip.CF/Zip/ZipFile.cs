using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x02000025 RID: 37
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00005")]
	[ComVisible(true)]
	public class ZipFile : IEnumerable<ZipEntry>, IEnumerable, IDisposable
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00008C2B File Offset: 0x00006E2B
		public ZipEntry AddItem(string fileOrDirectoryName)
		{
			return this.AddItem(fileOrDirectoryName, null);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008C35 File Offset: 0x00006E35
		public ZipEntry AddItem(string fileOrDirectoryName, string directoryPathInArchive)
		{
			if (File.Exists(fileOrDirectoryName))
			{
				return this.AddFile(fileOrDirectoryName, directoryPathInArchive);
			}
			if (Directory.Exists(fileOrDirectoryName))
			{
				return this.AddDirectory(fileOrDirectoryName, directoryPathInArchive);
			}
			throw new FileNotFoundException(string.Format("That file or directory ({0}) does not exist!", fileOrDirectoryName));
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008C69 File Offset: 0x00006E69
		public ZipEntry AddFile(string fileName)
		{
			return this.AddFile(fileName, null);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008C74 File Offset: 0x00006E74
		public ZipEntry AddFile(string fileName, string directoryPathInArchive)
		{
			string text = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			ZipEntry zipEntry = ZipEntry.CreateFromFile(fileName, text);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", fileName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public void RemoveEntries(ICollection<ZipEntry> entriesToRemove)
		{
			if (entriesToRemove == null)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (ZipEntry zipEntry in entriesToRemove)
			{
				this.RemoveEntry(zipEntry);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008D0C File Offset: 0x00006F0C
		public void RemoveEntries(ICollection<string> entriesToRemove)
		{
			if (entriesToRemove == null)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (string text in entriesToRemove)
			{
				this.RemoveEntry(text);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008D64 File Offset: 0x00006F64
		public void AddFiles(IEnumerable<string> fileNames)
		{
			this.AddFiles(fileNames, null);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008D6E File Offset: 0x00006F6E
		public void UpdateFiles(IEnumerable<string> fileNames)
		{
			this.UpdateFiles(fileNames, null);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008D78 File Offset: 0x00006F78
		public void AddFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			this.AddFiles(fileNames, false, directoryPathInArchive);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008D84 File Offset: 0x00006F84
		public void AddFiles(IEnumerable<string> fileNames, bool preserveDirHierarchy, string directoryPathInArchive)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			this._addOperationCanceled = false;
			this.OnAddStarted();
			if (preserveDirHierarchy)
			{
				using (IEnumerator<string> enumerator = fileNames.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (this._addOperationCanceled)
						{
							break;
						}
						if (directoryPathInArchive != null)
						{
							string fullPath = Path.GetFullPath(Path.Combine(directoryPathInArchive, Path.GetDirectoryName(text)));
							this.AddFile(text, fullPath);
						}
						else
						{
							this.AddFile(text, null);
						}
					}
					goto IL_00AD;
				}
			}
			foreach (string text2 in fileNames)
			{
				if (this._addOperationCanceled)
				{
					break;
				}
				this.AddFile(text2, directoryPathInArchive);
			}
			IL_00AD:
			if (!this._addOperationCanceled)
			{
				this.OnAddCompleted();
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008E68 File Offset: 0x00007068
		public void UpdateFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			this.OnAddStarted();
			foreach (string text in fileNames)
			{
				this.UpdateFile(text, directoryPathInArchive);
			}
			this.OnAddCompleted();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008ECC File Offset: 0x000070CC
		public ZipEntry UpdateFile(string fileName)
		{
			return this.UpdateFile(fileName, null);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008ED8 File Offset: 0x000070D8
		public ZipEntry UpdateFile(string fileName, string directoryPathInArchive)
		{
			string text = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			if (this[text] != null)
			{
				this.RemoveEntry(text);
			}
			return this.AddFile(fileName, directoryPathInArchive);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008F05 File Offset: 0x00007105
		public ZipEntry UpdateDirectory(string directoryName)
		{
			return this.UpdateDirectory(directoryName, null);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008F0F File Offset: 0x0000710F
		public ZipEntry UpdateDirectory(string directoryName, string directoryPathInArchive)
		{
			return this.AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOrUpdate);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008F1A File Offset: 0x0000711A
		public void UpdateItem(string itemName)
		{
			this.UpdateItem(itemName, null);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008F24 File Offset: 0x00007124
		public void UpdateItem(string itemName, string directoryPathInArchive)
		{
			if (File.Exists(itemName))
			{
				this.UpdateFile(itemName, directoryPathInArchive);
				return;
			}
			if (Directory.Exists(itemName))
			{
				this.UpdateDirectory(itemName, directoryPathInArchive);
				return;
			}
			throw new FileNotFoundException(string.Format("That file or directory ({0}) does not exist!", itemName));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008F5A File Offset: 0x0000715A
		public ZipEntry AddEntry(string entryName, string content)
		{
			return this.AddEntry(entryName, content, Encoding.Default);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008F6C File Offset: 0x0000716C
		public ZipEntry AddEntry(string entryName, string content, Encoding encoding)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, encoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Seek(0L, 0);
			return this.AddEntry(entryName, memoryStream);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008FA8 File Offset: 0x000071A8
		public ZipEntry AddEntry(string entryName, Stream stream)
		{
			ZipEntry zipEntry = ZipEntry.CreateForStream(entryName, stream);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008FF4 File Offset: 0x000071F4
		public ZipEntry AddEntry(string entryName, WriteDelegate writer)
		{
			ZipEntry zipEntry = ZipEntry.CreateForWriter(entryName, writer);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000902C File Offset: 0x0000722C
		public ZipEntry AddEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			ZipEntry zipEntry = ZipEntry.CreateForJitStreamProvider(entryName, opener, closer);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009078 File Offset: 0x00007278
		private ZipEntry _InternalAddEntry(ZipEntry ze)
		{
			ze._container = new ZipContainer(this);
			ze.CompressionMethod = this.CompressionMethod;
			ze.CompressionLevel = this.CompressionLevel;
			ze.ExtractExistingFile = this.ExtractExistingFile;
			ze.ZipErrorAction = this.ZipErrorAction;
			ze.SetCompression = this.SetCompression;
			ze.AlternateEncoding = this.AlternateEncoding;
			ze.AlternateEncodingUsage = this.AlternateEncodingUsage;
			ze.Password = this._Password;
			ze.Encryption = this.Encryption;
			ze.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
			ze.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
			this.InternalAddEntry(ze.FileName, ze);
			this.AfterAddEntry(ze);
			return ze;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000912A File Offset: 0x0000732A
		public ZipEntry UpdateEntry(string entryName, string content)
		{
			return this.UpdateEntry(entryName, content, Encoding.Default);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009139 File Offset: 0x00007339
		public ZipEntry UpdateEntry(string entryName, string content, Encoding encoding)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, content, encoding);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000914B File Offset: 0x0000734B
		public ZipEntry UpdateEntry(string entryName, WriteDelegate writer)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, writer);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000915C File Offset: 0x0000735C
		public ZipEntry UpdateEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, opener, closer);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000916E File Offset: 0x0000736E
		public ZipEntry UpdateEntry(string entryName, Stream stream)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, stream);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009180 File Offset: 0x00007380
		private void RemoveEntryForUpdate(string entryName)
		{
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentNullException("entryName");
			}
			string text = null;
			if (entryName.IndexOf('\\') != -1)
			{
				text = Path.GetDirectoryName(entryName);
				entryName = Path.GetFileName(entryName);
			}
			string text2 = ZipEntry.NameInArchive(entryName, text);
			if (this[text2] != null)
			{
				this.RemoveEntry(text2);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000091D4 File Offset: 0x000073D4
		public ZipEntry AddEntry(string entryName, byte[] byteContent)
		{
			if (byteContent == null)
			{
				throw new ArgumentException("bad argument", "byteContent");
			}
			MemoryStream memoryStream = new MemoryStream(byteContent);
			return this.AddEntry(entryName, memoryStream);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009203 File Offset: 0x00007403
		public ZipEntry UpdateEntry(string entryName, byte[] byteContent)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, byteContent);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009214 File Offset: 0x00007414
		public ZipEntry AddDirectory(string directoryName)
		{
			return this.AddDirectory(directoryName, null);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000921E File Offset: 0x0000741E
		public ZipEntry AddDirectory(string directoryName, string directoryPathInArchive)
		{
			return this.AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOnly);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000922C File Offset: 0x0000742C
		public ZipEntry AddDirectoryByName(string directoryNameInArchive)
		{
			ZipEntry zipEntry = ZipEntry.CreateFromNothing(directoryNameInArchive);
			zipEntry._container = new ZipContainer(this);
			zipEntry.MarkAsDirectory();
			zipEntry.AlternateEncoding = this.AlternateEncoding;
			zipEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			zipEntry.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
			zipEntry.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
			zipEntry._Source = ZipEntrySource.Stream;
			this.InternalAddEntry(zipEntry.FileName, zipEntry);
			this.AfterAddEntry(zipEntry);
			return zipEntry;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000092B3 File Offset: 0x000074B3
		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action)
		{
			if (rootDirectoryPathInArchive == null)
			{
				rootDirectoryPathInArchive = "";
			}
			return this.AddOrUpdateDirectoryImpl(directoryName, rootDirectoryPathInArchive, action, true, 0);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000092CA File Offset: 0x000074CA
		internal void InternalAddEntry(string name, ZipEntry entry)
		{
			this._entries.Add(name, entry);
			this._zipEntriesAsList = null;
			this._contentsChanged = true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000092E8 File Offset: 0x000074E8
		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action, bool recurse, int level)
		{
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("{0} {1}...", (action == AddOrUpdateAction.AddOnly) ? "adding" : "Adding or updating", directoryName);
			}
			if (level == 0)
			{
				this._addOperationCanceled = false;
				this.OnAddStarted();
			}
			if (this._addOperationCanceled)
			{
				return null;
			}
			string text = rootDirectoryPathInArchive;
			ZipEntry zipEntry = null;
			if (level > 0)
			{
				int num = directoryName.Length;
				for (int i = level; i > 0; i--)
				{
					num = directoryName.LastIndexOfAny("/\\".ToCharArray(), num - 1, num - 1);
				}
				text = directoryName.Substring(num + 1);
				text = Path.Combine(rootDirectoryPathInArchive, text);
			}
			if (level > 0 || rootDirectoryPathInArchive != "")
			{
				zipEntry = ZipEntry.CreateFromFile(directoryName, text);
				zipEntry._container = new ZipContainer(this);
				zipEntry.AlternateEncoding = this.AlternateEncoding;
				zipEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
				zipEntry.MarkAsDirectory();
				zipEntry.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
				zipEntry.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
				if (!this._entries.ContainsKey(zipEntry.FileName))
				{
					this.InternalAddEntry(zipEntry.FileName, zipEntry);
					this.AfterAddEntry(zipEntry);
				}
				text = zipEntry.FileName;
			}
			if (!this._addOperationCanceled)
			{
				string[] files = Directory.GetFiles(directoryName);
				if (recurse)
				{
					foreach (string text2 in files)
					{
						if (this._addOperationCanceled)
						{
							break;
						}
						if (action == AddOrUpdateAction.AddOnly)
						{
							this.AddFile(text2, text);
						}
						else
						{
							this.UpdateFile(text2, text);
						}
					}
					if (!this._addOperationCanceled)
					{
						string[] directories = Directory.GetDirectories(directoryName);
						foreach (string text3 in directories)
						{
							FileAttributes attributes = NetCfFile.GetAttributes(text3);
							if (this.AddDirectoryWillTraverseReparsePoints || (attributes & 1024) == null)
							{
								this.AddOrUpdateDirectoryImpl(text3, rootDirectoryPathInArchive, action, recurse, level + 1);
							}
						}
					}
				}
			}
			if (level == 0)
			{
				this.OnAddCompleted();
			}
			return zipEntry;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000094C6 File Offset: 0x000076C6
		public static bool CheckZip(string zipFileName)
		{
			return ZipFile.CheckZip(zipFileName, false, null);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000094D0 File Offset: 0x000076D0
		public static bool CheckZip(string zipFileName, bool fixIfNecessary, TextWriter writer)
		{
			ZipFile zipFile = null;
			ZipFile zipFile2 = null;
			bool flag = true;
			try
			{
				zipFile = new ZipFile();
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile2 = ZipFile.Read(zipFileName);
				foreach (ZipEntry zipEntry in zipFile)
				{
					foreach (ZipEntry zipEntry2 in zipFile2)
					{
						if (zipEntry.FileName == zipEntry2.FileName)
						{
							if (zipEntry._RelativeOffsetOfLocalHeader != zipEntry2._RelativeOffsetOfLocalHeader)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in RelativeOffsetOfLocalHeader  (0x{1:X16} != 0x{2:X16})", new object[] { zipEntry.FileName, zipEntry._RelativeOffsetOfLocalHeader, zipEntry2._RelativeOffsetOfLocalHeader });
								}
							}
							if (zipEntry._CompressedSize != zipEntry2._CompressedSize)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in CompressedSize  (0x{1:X16} != 0x{2:X16})", new object[] { zipEntry.FileName, zipEntry._CompressedSize, zipEntry2._CompressedSize });
								}
							}
							if (zipEntry._UncompressedSize != zipEntry2._UncompressedSize)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in UncompressedSize  (0x{1:X16} != 0x{2:X16})", new object[] { zipEntry.FileName, zipEntry._UncompressedSize, zipEntry2._UncompressedSize });
								}
							}
							if (zipEntry.CompressionMethod != zipEntry2.CompressionMethod)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in CompressionMethod  (0x{1:X4} != 0x{2:X4})", new object[] { zipEntry.FileName, zipEntry.CompressionMethod, zipEntry2.CompressionMethod });
								}
							}
							if (zipEntry.Crc == zipEntry2.Crc)
							{
								break;
							}
							flag = false;
							if (writer != null)
							{
								writer.WriteLine("{0}: mismatch in Crc32  (0x{1:X4} != 0x{2:X4})", new object[] { zipEntry.FileName, zipEntry.Crc, zipEntry2.Crc });
								break;
							}
							break;
						}
					}
				}
				zipFile2.Dispose();
				zipFile2 = null;
				if (!flag && fixIfNecessary)
				{
					string text = Path.GetFileNameWithoutExtension(zipFileName);
					text = string.Format("{0}_fixed.zip", text);
					zipFile.Save(text);
				}
			}
			finally
			{
				if (zipFile != null)
				{
					zipFile.Dispose();
				}
				if (zipFile2 != null)
				{
					zipFile2.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000097A0 File Offset: 0x000079A0
		public static void FixZipDirectory(string zipFileName)
		{
			using (ZipFile zipFile = new ZipFile())
			{
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile.Save(zipFileName);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000097E4 File Offset: 0x000079E4
		public static bool CheckZipPassword(string zipFileName, string password)
		{
			bool flag = false;
			try
			{
				using (ZipFile zipFile = ZipFile.Read(zipFileName))
				{
					foreach (ZipEntry zipEntry in zipFile)
					{
						if (!zipEntry.IsDirectory && zipEntry.UsesEncryption)
						{
							zipEntry.ExtractWithPassword(Stream.Null, password);
						}
					}
				}
				flag = true;
			}
			catch (BadPasswordException)
			{
			}
			return flag;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00009878 File Offset: 0x00007A78
		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("          ZipFile: {0}\n", this.Name));
				if (!string.IsNullOrEmpty(this._Comment))
				{
					stringBuilder.Append(string.Format("          Comment: {0}\n", this._Comment));
				}
				if (this._versionMadeBy != 0)
				{
					stringBuilder.Append(string.Format("  version made by: 0x{0:X4}\n", this._versionMadeBy));
				}
				if (this._versionNeededToExtract != 0)
				{
					stringBuilder.Append(string.Format("needed to extract: 0x{0:X4}\n", this._versionNeededToExtract));
				}
				stringBuilder.Append(string.Format("       uses ZIP64: {0}\n", this.InputUsesZip64));
				stringBuilder.Append(string.Format("     disk with CD: {0}\n", this._diskNumberWithCd));
				if (this._OffsetOfCentralDirectory == 4294967295U)
				{
					stringBuilder.Append(string.Format("      CD64 offset: 0x{0:X16}\n", this._OffsetOfCentralDirectory64));
				}
				else
				{
					stringBuilder.Append(string.Format("        CD offset: 0x{0:X8}\n", this._OffsetOfCentralDirectory));
				}
				stringBuilder.Append("\n");
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					stringBuilder.Append(zipEntry.Info);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000099EC File Offset: 0x00007BEC
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000099F4 File Offset: 0x00007BF4
		public bool FullScan
		{
			get
			{
				return this.<FullScan>k__BackingField;
			}
			set
			{
				this.<FullScan>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000099FD File Offset: 0x00007BFD
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00009A05 File Offset: 0x00007C05
		public bool SortEntriesBeforeSaving
		{
			get
			{
				return this.<SortEntriesBeforeSaving>k__BackingField;
			}
			set
			{
				this.<SortEntriesBeforeSaving>k__BackingField = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009A0E File Offset: 0x00007C0E
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00009A16 File Offset: 0x00007C16
		public bool AddDirectoryWillTraverseReparsePoints
		{
			get
			{
				return this.<AddDirectoryWillTraverseReparsePoints>k__BackingField;
			}
			set
			{
				this.<AddDirectoryWillTraverseReparsePoints>k__BackingField = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00009A1F File Offset: 0x00007C1F
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00009A27 File Offset: 0x00007C27
		public int BufferSize
		{
			get
			{
				return this._BufferSize;
			}
			set
			{
				this._BufferSize = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009A30 File Offset: 0x00007C30
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00009A38 File Offset: 0x00007C38
		public int CodecBufferSize
		{
			get
			{
				return this.<CodecBufferSize>k__BackingField;
			}
			set
			{
				this.<CodecBufferSize>k__BackingField = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00009A41 File Offset: 0x00007C41
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00009A49 File Offset: 0x00007C49
		public bool FlattenFoldersOnExtract
		{
			get
			{
				return this.<FlattenFoldersOnExtract>k__BackingField;
			}
			set
			{
				this.<FlattenFoldersOnExtract>k__BackingField = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00009A52 File Offset: 0x00007C52
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00009A5A File Offset: 0x00007C5A
		public CompressionStrategy Strategy
		{
			get
			{
				return this._Strategy;
			}
			set
			{
				this._Strategy = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00009A63 File Offset: 0x00007C63
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00009A6B File Offset: 0x00007C6B
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00009A74 File Offset: 0x00007C74
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00009A7C File Offset: 0x00007C7C
		public CompressionLevel CompressionLevel
		{
			get
			{
				return this.<CompressionLevel>k__BackingField;
			}
			set
			{
				this.<CompressionLevel>k__BackingField = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00009A85 File Offset: 0x00007C85
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00009A8D File Offset: 0x00007C8D
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this._compressionMethod;
			}
			set
			{
				this._compressionMethod = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00009A96 File Offset: 0x00007C96
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00009A9E File Offset: 0x00007C9E
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				this._Comment = value;
				this._contentsChanged = true;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00009AAE File Offset: 0x00007CAE
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00009AB6 File Offset: 0x00007CB6
		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return this._emitNtfsTimes;
			}
			set
			{
				this._emitNtfsTimes = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00009ABF File Offset: 0x00007CBF
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00009AC7 File Offset: 0x00007CC7
		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return this._emitUnixTimes;
			}
			set
			{
				this._emitUnixTimes = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00009AD0 File Offset: 0x00007CD0
		internal bool Verbose
		{
			get
			{
				return this._StatusMessageTextWriter != null;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009ADE File Offset: 0x00007CDE
		public bool ContainsEntry(string name)
		{
			return this._entries.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009AF1 File Offset: 0x00007CF1
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009AF9 File Offset: 0x00007CF9
		public bool CaseSensitiveRetrieval
		{
			get
			{
				return this._CaseSensitiveRetrieval;
			}
			set
			{
				if (value != this._CaseSensitiveRetrieval)
				{
					this._CaseSensitiveRetrieval = value;
					this._initEntriesDictionary();
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009B11 File Offset: 0x00007D11
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00009B30 File Offset: 0x00007D30
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this._alternateEncoding == Encoding.GetEncoding("UTF-8") && this._alternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this._alternateEncoding = Encoding.GetEncoding("UTF-8");
					this._alternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this._alternateEncoding = ZipFile.DefaultEncoding;
				this._alternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00009B5F File Offset: 0x00007D5F
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00009B67 File Offset: 0x00007D67
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				return this._zip64;
			}
			set
			{
				this._zip64 = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009B70 File Offset: 0x00007D70
		public bool? RequiresZip64
		{
			get
			{
				if (this._entries.Count > 65534)
				{
					return new bool?(true);
				}
				if (!this._hasBeenSaved || this._contentsChanged)
				{
					return default(bool?);
				}
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					if (zipEntry.RequiresZip64.Value)
					{
						return new bool?(true);
					}
				}
				return new bool?(false);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00009C18 File Offset: 0x00007E18
		public bool? OutputUsedZip64
		{
			get
			{
				return this._OutputUsesZip64;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00009C20 File Offset: 0x00007E20
		public bool? InputUsesZip64
		{
			get
			{
				if (this._entries.Count > 65534)
				{
					return new bool?(true);
				}
				foreach (ZipEntry zipEntry in this)
				{
					if (zipEntry.Source != ZipEntrySource.ZipFile)
					{
						return default(bool?);
					}
					if (zipEntry._InputUsesZip64)
					{
						return new bool?(true);
					}
				}
				return new bool?(false);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009CA8 File Offset: 0x00007EA8
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00009CBB File Offset: 0x00007EBB
		[Obsolete("use AlternateEncoding instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				if (this._alternateEncodingUsage == ZipOption.AsNecessary)
				{
					return this._alternateEncoding;
				}
				return null;
			}
			set
			{
				this._alternateEncoding = value;
				this._alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00009CCB File Offset: 0x00007ECB
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00009CD3 File Offset: 0x00007ED3
		public Encoding AlternateEncoding
		{
			get
			{
				return this._alternateEncoding;
			}
			set
			{
				this._alternateEncoding = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009CDC File Offset: 0x00007EDC
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00009CE4 File Offset: 0x00007EE4
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this._alternateEncodingUsage;
			}
			set
			{
				this._alternateEncodingUsage = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00009CED File Offset: 0x00007EED
		public static Encoding DefaultEncoding
		{
			get
			{
				return ZipFile._defaultEncoding;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00009CF4 File Offset: 0x00007EF4
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00009CFC File Offset: 0x00007EFC
		public TextWriter StatusMessageTextWriter
		{
			get
			{
				return this._StatusMessageTextWriter;
			}
			set
			{
				this._StatusMessageTextWriter = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00009D05 File Offset: 0x00007F05
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00009D0D File Offset: 0x00007F0D
		public string TempFileFolder
		{
			get
			{
				return this._TempFileFolder;
			}
			set
			{
				this._TempFileFolder = value;
				if (value == null)
				{
					return;
				}
				if (!Directory.Exists(value))
				{
					throw new FileNotFoundException(string.Format("That directory ({0}) does not exist.", value));
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009D5B File Offset: 0x00007F5B
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00009D33 File Offset: 0x00007F33
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				if (this._Password == null)
				{
					this.Encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this.Encryption == EncryptionAlgorithm.None)
				{
					this.Encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00009D63 File Offset: 0x00007F63
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00009D6B File Offset: 0x00007F6B
		public ExtractExistingFileAction ExtractExistingFile
		{
			get
			{
				return this.<ExtractExistingFile>k__BackingField;
			}
			set
			{
				this.<ExtractExistingFile>k__BackingField = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00009D74 File Offset: 0x00007F74
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00009D8B File Offset: 0x00007F8B
		public ZipErrorAction ZipErrorAction
		{
			get
			{
				if (this.ZipError != null)
				{
					this._zipErrorAction = ZipErrorAction.InvokeErrorEvent;
				}
				return this._zipErrorAction;
			}
			set
			{
				this._zipErrorAction = value;
				if (this._zipErrorAction != ZipErrorAction.InvokeErrorEvent && this.ZipError != null)
				{
					this.ZipError = null;
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00009DAC File Offset: 0x00007FAC
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00009DB4 File Offset: 0x00007FB4
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				if (value == EncryptionAlgorithm.Unsupported)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._Encryption = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009DCC File Offset: 0x00007FCC
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00009DD4 File Offset: 0x00007FD4
		public SetCompressionCallback SetCompression
		{
			get
			{
				return this.<SetCompression>k__BackingField;
			}
			set
			{
				this.<SetCompression>k__BackingField = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009DDD File Offset: 0x00007FDD
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00009DE5 File Offset: 0x00007FE5
		public int MaxOutputSegmentSize
		{
			get
			{
				return this._maxOutputSegmentSize;
			}
			set
			{
				if (value < 65536 && value != 0)
				{
					throw new ZipException("The minimum acceptable segment size is 65536.");
				}
				this._maxOutputSegmentSize = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009E04 File Offset: 0x00008004
		public int NumberOfSegmentsForMostRecentSave
		{
			get
			{
				return (int)(this._numberOfSegmentsForMostRecentSave + 1U);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009E0E File Offset: 0x0000800E
		public override string ToString()
		{
			return string.Format("ZipFile::{0}", this.Name);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009E20 File Offset: 0x00008020
		public static Version LibraryVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009E31 File Offset: 0x00008031
		internal void NotifyEntryChanged()
		{
			this._contentsChanged = true;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009E3A File Offset: 0x0000803A
		internal Stream StreamForDiskNumber(uint diskNumber)
		{
			if (diskNumber + 1U == this._diskNumberWithCd || (diskNumber == 0U && this._diskNumberWithCd == 0U))
			{
				return this.ReadStream;
			}
			return ZipSegmentedStream.ForReading(this._readName ?? this._name, diskNumber, this._diskNumberWithCd);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009E78 File Offset: 0x00008078
		internal void Reset(bool whileSaving)
		{
			if (this._JustSaved)
			{
				using (ZipFile zipFile = new ZipFile())
				{
					zipFile._readName = (zipFile._name = (whileSaving ? (this._readName ?? this._name) : this._name));
					zipFile.AlternateEncoding = this.AlternateEncoding;
					zipFile.AlternateEncodingUsage = this.AlternateEncodingUsage;
					ZipFile.ReadIntoInstance(zipFile);
					foreach (ZipEntry zipEntry in zipFile)
					{
						foreach (ZipEntry zipEntry2 in this)
						{
							if (zipEntry.FileName == zipEntry2.FileName)
							{
								zipEntry2.CopyMetaData(zipEntry);
								break;
							}
						}
					}
				}
				this._JustSaved = false;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009F88 File Offset: 0x00008188
		public ZipFile(string fileName)
		{
			try
			{
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("Could not read {0} as a zip file", fileName), ex);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000A014 File Offset: 0x00008214
		public ZipFile(string fileName, Encoding encoding)
		{
			try
			{
				this.AlternateEncoding = encoding;
				this.AlternateEncodingUsage = ZipOption.Always;
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public ZipFile()
		{
			this._InitInstance(null, null);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A118 File Offset: 0x00008318
		public ZipFile(Encoding encoding)
		{
			this.AlternateEncoding = encoding;
			this.AlternateEncodingUsage = ZipOption.Always;
			this._InitInstance(null, null);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A190 File Offset: 0x00008390
		public ZipFile(string fileName, TextWriter statusMessageWriter)
		{
			try
			{
				this._InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000A21C File Offset: 0x0000841C
		public ZipFile(string fileName, TextWriter statusMessageWriter, Encoding encoding)
		{
			try
			{
				this.AlternateEncoding = encoding;
				this.AlternateEncodingUsage = ZipOption.Always;
				this._InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A2B8 File Offset: 0x000084B8
		public void Initialize(string fileName)
		{
			try
			{
				this._InitInstance(fileName, null);
			}
			catch (Exception ex)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), ex);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A2F4 File Offset: 0x000084F4
		private void _initEntriesDictionary()
		{
			StringComparer stringComparer = (this.CaseSensitiveRetrieval ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
			this._entries = ((this._entries == null) ? new Dictionary<string, ZipEntry>(stringComparer) : new Dictionary<string, ZipEntry>(this._entries, stringComparer));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A338 File Offset: 0x00008538
		private void _InitInstance(string zipFileName, TextWriter statusMessageWriter)
		{
			this._name = zipFileName;
			this._StatusMessageTextWriter = statusMessageWriter;
			this._contentsChanged = true;
			this.AddDirectoryWillTraverseReparsePoints = true;
			this.CompressionLevel = CompressionLevel.Default;
			this._initEntriesDictionary();
			if (File.Exists(this._name))
			{
				if (this.FullScan)
				{
					ZipFile.ReadIntoInstance_Orig(this);
				}
				else
				{
					ZipFile.ReadIntoInstance(this);
				}
				this._fileAlreadyExists = true;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000A398 File Offset: 0x00008598
		private List<ZipEntry> ZipEntriesAsList
		{
			get
			{
				if (this._zipEntriesAsList == null)
				{
					this._zipEntriesAsList = new List<ZipEntry>(this._entries.Values);
				}
				return this._zipEntriesAsList;
			}
		}

		// Token: 0x17000073 RID: 115
		public ZipEntry this[int ix]
		{
			get
			{
				return this.ZipEntriesAsList[ix];
			}
		}

		// Token: 0x17000074 RID: 116
		public ZipEntry this[string fileName]
		{
			get
			{
				string text = SharedUtilities.NormalizePathForUseInZipFile(fileName);
				if (this._entries.ContainsKey(text))
				{
					return this._entries[text];
				}
				text = text.Replace("/", "\\");
				if (this._entries.ContainsKey(text))
				{
					return this._entries[text];
				}
				return null;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000A428 File Offset: 0x00008628
		public ICollection<string> EntryFileNames
		{
			get
			{
				return this._entries.Keys;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000A435 File Offset: 0x00008635
		public ICollection<ZipEntry> Entries
		{
			get
			{
				return this._entries.Values;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000A464 File Offset: 0x00008664
		public ICollection<ZipEntry> EntriesSorted
		{
			get
			{
				ZipFile.<>c__DisplayClass1 <>c__DisplayClass = new ZipFile.<>c__DisplayClass1();
				List<ZipEntry> list = new List<ZipEntry>();
				foreach (ZipEntry zipEntry in this.Entries)
				{
					list.Add(zipEntry);
				}
				<>c__DisplayClass.sc = (this.CaseSensitiveRetrieval ? 4 : 5);
				list.Sort(new Comparison<ZipEntry>(<>c__DisplayClass.<get_EntriesSorted>b__0));
				return list.AsReadOnly();
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A4E8 File Offset: 0x000086E8
		public int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A4F5 File Offset: 0x000086F5
		public void RemoveEntry(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this._entries.Remove(SharedUtilities.NormalizePathForUseInZipFile(entry.FileName));
			this._zipEntriesAsList = null;
			this._contentsChanged = true;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A52C File Offset: 0x0000872C
		public void RemoveEntry(string fileName)
		{
			string text = ZipEntry.NameInArchive(fileName, null);
			ZipEntry zipEntry = this[text];
			if (zipEntry == null)
			{
				throw new ArgumentException("The entry you specified was not found in the zip archive.");
			}
			this.RemoveEntry(zipEntry);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A55E File Offset: 0x0000875E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A570 File Offset: 0x00008770
		protected virtual void Dispose(bool disposeManagedResources)
		{
			if (!this._disposed)
			{
				if (disposeManagedResources)
				{
					if (this._ReadStreamIsOurs && this._readstream != null)
					{
						this._readstream.Close();
						this._readstream = null;
					}
					if (this._temporaryFileName != null && this._name != null && this._writestream != null)
					{
						this._writestream.Close();
						this._writestream = null;
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A5DC File Offset: 0x000087DC
		internal Stream ReadStream
		{
			get
			{
				if (this._readstream == null && (this._readName != null || this._name != null))
				{
					this._readstream = File.Open(this._readName ?? this._name, 3, 1, 3);
					this._ReadStreamIsOurs = true;
				}
				return this._readstream;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A62C File Offset: 0x0000882C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000A6A9 File Offset: 0x000088A9
		private Stream WriteStream
		{
			get
			{
				if (this._writestream != null)
				{
					return this._writestream;
				}
				if (this._name == null)
				{
					return this._writestream;
				}
				if (this._maxOutputSegmentSize != 0)
				{
					this._writestream = ZipSegmentedStream.ForWriting(this._name, this._maxOutputSegmentSize);
					return this._writestream;
				}
				SharedUtilities.CreateAndOpenUniqueTempFile(this.TempFileFolder ?? Path.GetDirectoryName(this._name), out this._writestream, out this._temporaryFileName);
				return this._writestream;
			}
			set
			{
				if (value != null)
				{
					throw new ZipException("Cannot set the stream to a non-null value.");
				}
				this._writestream = null;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A6C0 File Offset: 0x000088C0
		private string ArchiveNameForEvent
		{
			get
			{
				if (this._name == null)
				{
					return "(stream)";
				}
				return this._name;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001D2 RID: 466 RVA: 0x0000A6D8 File Offset: 0x000088D8
		// (remove) Token: 0x060001D3 RID: 467 RVA: 0x0000A710 File Offset: 0x00008910
		public event EventHandler<SaveProgressEventArgs> SaveProgress
		{
			add
			{
				EventHandler<SaveProgressEventArgs> eventHandler = this.SaveProgress;
				EventHandler<SaveProgressEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SaveProgressEventArgs> eventHandler3 = (EventHandler<SaveProgressEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SaveProgressEventArgs>>(ref this.SaveProgress, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<SaveProgressEventArgs> eventHandler = this.SaveProgress;
				EventHandler<SaveProgressEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SaveProgressEventArgs> eventHandler3 = (EventHandler<SaveProgressEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<SaveProgressEventArgs>>(ref this.SaveProgress, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A748 File Offset: 0x00008948
		internal bool OnSaveBlock(ZipEntry entry, long bytesXferred, long totalBytesToXfer)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, bytesXferred, totalBytesToXfer);
				saveProgress.Invoke(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
			return this._saveOperationCanceled;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A78C File Offset: 0x0000898C
		private void OnSaveEntry(int current, ZipEntry entry, bool before)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(this.ArchiveNameForEvent, before, this._entries.Count, current, entry);
				saveProgress.Invoke(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A7D4 File Offset: 0x000089D4
		private void OnSaveEvent(ZipProgressEventType eventFlavor)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(this.ArchiveNameForEvent, eventFlavor);
				saveProgress.Invoke(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A810 File Offset: 0x00008A10
		private void OnSaveStarted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.Started(this.ArchiveNameForEvent);
				saveProgress.Invoke(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A84C File Offset: 0x00008A4C
		private void OnSaveCompleted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.Completed(this.ArchiveNameForEvent);
				saveProgress.Invoke(this, saveProgressEventArgs);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001D9 RID: 473 RVA: 0x0000A878 File Offset: 0x00008A78
		// (remove) Token: 0x060001DA RID: 474 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public event EventHandler<ReadProgressEventArgs> ReadProgress
		{
			add
			{
				EventHandler<ReadProgressEventArgs> eventHandler = this.ReadProgress;
				EventHandler<ReadProgressEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ReadProgressEventArgs> eventHandler3 = (EventHandler<ReadProgressEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ReadProgressEventArgs>>(ref this.ReadProgress, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<ReadProgressEventArgs> eventHandler = this.ReadProgress;
				EventHandler<ReadProgressEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ReadProgressEventArgs> eventHandler3 = (EventHandler<ReadProgressEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ReadProgressEventArgs>>(ref this.ReadProgress, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		private void OnReadStarted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs readProgressEventArgs = ReadProgressEventArgs.Started(this.ArchiveNameForEvent);
				readProgress.Invoke(this, readProgressEventArgs);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A914 File Offset: 0x00008B14
		private void OnReadCompleted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs readProgressEventArgs = ReadProgressEventArgs.Completed(this.ArchiveNameForEvent);
				readProgress.Invoke(this, readProgressEventArgs);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A940 File Offset: 0x00008B40
		internal void OnReadBytes(ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs readProgressEventArgs = ReadProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, this.ReadStream.Position, this.LengthOfReadStream);
				readProgress.Invoke(this, readProgressEventArgs);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A980 File Offset: 0x00008B80
		internal void OnReadEntry(bool before, ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs readProgressEventArgs = (before ? ReadProgressEventArgs.Before(this.ArchiveNameForEvent, this._entries.Count) : ReadProgressEventArgs.After(this.ArchiveNameForEvent, entry, this._entries.Count));
				readProgress.Invoke(this, readProgressEventArgs);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A9D2 File Offset: 0x00008BD2
		private long LengthOfReadStream
		{
			get
			{
				if (this._lengthOfReadStream == -99L)
				{
					this._lengthOfReadStream = (this._ReadStreamIsOurs ? SharedUtilities.GetFileLength(this._name) : (-1L));
				}
				return this._lengthOfReadStream;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001E0 RID: 480 RVA: 0x0000AA04 File Offset: 0x00008C04
		// (remove) Token: 0x060001E1 RID: 481 RVA: 0x0000AA3C File Offset: 0x00008C3C
		public event EventHandler<ExtractProgressEventArgs> ExtractProgress
		{
			add
			{
				EventHandler<ExtractProgressEventArgs> eventHandler = this.ExtractProgress;
				EventHandler<ExtractProgressEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ExtractProgressEventArgs> eventHandler3 = (EventHandler<ExtractProgressEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ExtractProgressEventArgs>>(ref this.ExtractProgress, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<ExtractProgressEventArgs> eventHandler = this.ExtractProgress;
				EventHandler<ExtractProgressEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ExtractProgressEventArgs> eventHandler3 = (EventHandler<ExtractProgressEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ExtractProgressEventArgs>>(ref this.ExtractProgress, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000AA74 File Offset: 0x00008C74
		private void OnExtractEntry(int current, bool before, ZipEntry currentEntry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs(this.ArchiveNameForEvent, before, this._entries.Count, current, currentEntry, path);
				extractProgress.Invoke(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		internal bool OnExtractBlock(ZipEntry entry, long bytesWritten, long totalBytesToWrite)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, bytesWritten, totalBytesToWrite);
				extractProgress.Invoke(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000AB04 File Offset: 0x00008D04
		internal bool OnSingleEntryExtract(ZipEntry entry, string path, bool before)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = (before ? ExtractProgressEventArgs.BeforeExtractEntry(this.ArchiveNameForEvent, entry, path) : ExtractProgressEventArgs.AfterExtractEntry(this.ArchiveNameForEvent, entry, path));
				extractProgress.Invoke(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AB58 File Offset: 0x00008D58
		internal bool OnExtractExisting(ZipEntry entry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ExtractExisting(this.ArchiveNameForEvent, entry, path);
				extractProgress.Invoke(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000AB9C File Offset: 0x00008D9C
		private void OnExtractAllCompleted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ExtractAllCompleted(this.ArchiveNameForEvent, path);
				extractProgress.Invoke(this, extractProgressEventArgs);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		private void OnExtractAllStarted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ExtractAllStarted(this.ArchiveNameForEvent, path);
				extractProgress.Invoke(this, extractProgressEventArgs);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001E8 RID: 488 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		// (remove) Token: 0x060001E9 RID: 489 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public event EventHandler<AddProgressEventArgs> AddProgress
		{
			add
			{
				EventHandler<AddProgressEventArgs> eventHandler = this.AddProgress;
				EventHandler<AddProgressEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<AddProgressEventArgs> eventHandler3 = (EventHandler<AddProgressEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<AddProgressEventArgs>>(ref this.AddProgress, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<AddProgressEventArgs> eventHandler = this.AddProgress;
				EventHandler<AddProgressEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<AddProgressEventArgs> eventHandler3 = (EventHandler<AddProgressEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<AddProgressEventArgs>>(ref this.AddProgress, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000AC64 File Offset: 0x00008E64
		private void OnAddStarted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs addProgressEventArgs = AddProgressEventArgs.Started(this.ArchiveNameForEvent);
				addProgress.Invoke(this, addProgressEventArgs);
				if (addProgressEventArgs.Cancel)
				{
					this._addOperationCanceled = true;
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		private void OnAddCompleted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs addProgressEventArgs = AddProgressEventArgs.Completed(this.ArchiveNameForEvent);
				addProgress.Invoke(this, addProgressEventArgs);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000ACCC File Offset: 0x00008ECC
		internal void AfterAddEntry(ZipEntry entry)
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs addProgressEventArgs = AddProgressEventArgs.AfterEntry(this.ArchiveNameForEvent, entry, this._entries.Count);
				addProgress.Invoke(this, addProgressEventArgs);
				if (addProgressEventArgs.Cancel)
				{
					this._addOperationCanceled = true;
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001ED RID: 493 RVA: 0x0000AD14 File Offset: 0x00008F14
		// (remove) Token: 0x060001EE RID: 494 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public event EventHandler<ZipErrorEventArgs> ZipError
		{
			add
			{
				EventHandler<ZipErrorEventArgs> eventHandler = this.ZipError;
				EventHandler<ZipErrorEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ZipErrorEventArgs> eventHandler3 = (EventHandler<ZipErrorEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ZipErrorEventArgs>>(ref this.ZipError, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<ZipErrorEventArgs> eventHandler = this.ZipError;
				EventHandler<ZipErrorEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ZipErrorEventArgs> eventHandler3 = (EventHandler<ZipErrorEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ZipErrorEventArgs>>(ref this.ZipError, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000AD84 File Offset: 0x00008F84
		internal bool OnZipErrorSaving(ZipEntry entry, Exception exc)
		{
			if (this.ZipError != null)
			{
				lock (this.LOCK)
				{
					ZipErrorEventArgs zipErrorEventArgs = ZipErrorEventArgs.Saving(this.Name, entry, exc);
					this.ZipError.Invoke(this, zipErrorEventArgs);
					if (zipErrorEventArgs.Cancel)
					{
						this._saveOperationCanceled = true;
					}
				}
			}
			return this._saveOperationCanceled;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		public void ExtractAll(string path)
		{
			this._InternalExtractAll(path, true);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000ADFA File Offset: 0x00008FFA
		public void ExtractAll(string path, ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this._InternalExtractAll(path, true);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000AE0C File Offset: 0x0000900C
		private void _InternalExtractAll(string path, bool overrideExtractExistingProperty)
		{
			bool flag = this.Verbose;
			this._inExtractAll = true;
			try
			{
				this.OnExtractAllStarted(path);
				int num = 0;
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					if (flag)
					{
						this.StatusMessageTextWriter.WriteLine("\n{1,-22} {2,-8} {3,4}   {4,-8}  {0}", new object[] { "Name", "Modified", "Size", "Ratio", "Packed" });
						this.StatusMessageTextWriter.WriteLine(new string('-', 72));
						flag = false;
					}
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("{1,-22} {2,-8} {3,4:F0}%   {4,-8} {0}", new object[]
						{
							zipEntry.FileName,
							zipEntry.LastModified.ToString("yyyy-MM-dd HH:mm:ss"),
							zipEntry.UncompressedSize,
							zipEntry.CompressionRatio,
							zipEntry.CompressedSize
						});
						if (!string.IsNullOrEmpty(zipEntry.Comment))
						{
							this.StatusMessageTextWriter.WriteLine("  Comment: {0}", zipEntry.Comment);
						}
					}
					zipEntry.Password = this._Password;
					this.OnExtractEntry(num, true, zipEntry, path);
					if (overrideExtractExistingProperty)
					{
						zipEntry.ExtractExistingFile = this.ExtractExistingFile;
					}
					zipEntry.Extract(path);
					num++;
					this.OnExtractEntry(num, false, zipEntry, path);
					if (this._extractOperationCanceled)
					{
						break;
					}
				}
				if (!this._extractOperationCanceled)
				{
					foreach (ZipEntry zipEntry2 in this._entries.Values)
					{
						if (zipEntry2.IsDirectory || zipEntry2.FileName.EndsWith("/"))
						{
							string text = (zipEntry2.FileName.StartsWith("/") ? Path.Combine(path, zipEntry2.FileName.Substring(1)) : Path.Combine(path, zipEntry2.FileName));
							zipEntry2._SetTimes(text, false);
						}
					}
					this.OnExtractAllCompleted(path);
				}
			}
			finally
			{
				this._inExtractAll = false;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B098 File Offset: 0x00009298
		public static ZipFile Read(string fileName)
		{
			return ZipFile.Read(fileName, null, null, null);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B0A3 File Offset: 0x000092A3
		public static ZipFile Read(string fileName, ReadOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return ZipFile.Read(fileName, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B0CC File Offset: 0x000092CC
		private static ZipFile Read(string fileName, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			ZipFile zipFile = new ZipFile();
			zipFile.AlternateEncoding = encoding ?? ZipFile.DefaultEncoding;
			zipFile.AlternateEncodingUsage = ZipOption.Always;
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._name = fileName;
			if (readProgress != null)
			{
				zipFile.ReadProgress = readProgress;
			}
			if (zipFile.Verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from {0}...", fileName);
			}
			ZipFile.ReadIntoInstance(zipFile);
			zipFile._fileAlreadyExists = true;
			return zipFile;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B135 File Offset: 0x00009335
		public static ZipFile Read(Stream zipStream)
		{
			return ZipFile.Read(zipStream, null, null, null);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B140 File Offset: 0x00009340
		public static ZipFile Read(Stream zipStream, ReadOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return ZipFile.Read(zipStream, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B168 File Offset: 0x00009368
		private static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			if (zipStream == null)
			{
				throw new ArgumentNullException("zipStream");
			}
			ZipFile zipFile = new ZipFile();
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._alternateEncoding = encoding ?? ZipFile.DefaultEncoding;
			zipFile._alternateEncodingUsage = ZipOption.Always;
			if (readProgress != null)
			{
				zipFile.ReadProgress += readProgress;
			}
			zipFile._readstream = ((zipStream.Position == 0L) ? zipStream : new OffsetStream(zipStream));
			zipFile._ReadStreamIsOurs = false;
			if (zipFile.Verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from stream...");
			}
			ZipFile.ReadIntoInstance(zipFile);
			return zipFile;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B1F0 File Offset: 0x000093F0
		private static void ReadIntoInstance(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			try
			{
				zf._readName = zf._name;
				if (!readStream.CanSeek)
				{
					ZipFile.ReadIntoInstance_Orig(zf);
					return;
				}
				zf.OnReadStarted();
				uint num = ZipFile.ReadFirstFourBytes(readStream);
				if (num == 101010256U)
				{
					return;
				}
				int num2 = 0;
				bool flag = false;
				long num3 = readStream.Length - 64L;
				long num4 = Math.Max(readStream.Length - 16384L, 10L);
				do
				{
					if (num3 < 0L)
					{
						num3 = 0L;
					}
					readStream.Seek(num3, 0);
					long num5 = SharedUtilities.FindSignature(readStream, 101010256);
					if (num5 != -1L)
					{
						flag = true;
					}
					else
					{
						if (num3 == 0L)
						{
							break;
						}
						num2++;
						num3 -= (long)(32 * (num2 + 1) * num2);
					}
				}
				while (!flag && num3 > num4);
				if (flag)
				{
					zf._locEndOfCDS = readStream.Position - 4L;
					byte[] array = new byte[16];
					readStream.Read(array, 0, array.Length);
					zf._diskNumberWithCd = (uint)BitConverter.ToUInt16(array, 2);
					if (zf._diskNumberWithCd == 65535U)
					{
						throw new ZipException("Spanned archives with more than 65534 segments are not supported at this time.");
					}
					zf._diskNumberWithCd += 1U;
					int num6 = 12;
					uint num7 = BitConverter.ToUInt32(array, num6);
					if (num7 == 4294967295U)
					{
						ZipFile.Zip64SeekToCentralDirectory(zf);
					}
					else
					{
						zf._OffsetOfCentralDirectory = num7;
						readStream.Seek((long)((ulong)num7), 0);
					}
					ZipFile.ReadCentralDirectory(zf);
				}
				else
				{
					readStream.Seek(0L, 0);
					ZipFile.ReadIntoInstance_Orig(zf);
				}
			}
			catch (Exception ex)
			{
				if (zf._ReadStreamIsOurs && zf._readstream != null)
				{
					zf._readstream.Close();
					zf._readstream = null;
				}
				throw new ZipException("Cannot read that as a ZipFile", ex);
			}
			zf._contentsChanged = false;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B3B0 File Offset: 0x000095B0
		private static void Zip64SeekToCentralDirectory(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			byte[] array = new byte[16];
			readStream.Seek(-40L, 1);
			readStream.Read(array, 0, 16);
			long num = BitConverter.ToInt64(array, 8);
			zf._OffsetOfCentralDirectory = uint.MaxValue;
			zf._OffsetOfCentralDirectory64 = num;
			readStream.Seek(num, 0);
			uint num2 = (uint)SharedUtilities.ReadInt(readStream);
			if (num2 != 101075792U)
			{
				throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) looking for ZIP64 EoCD Record at position 0x{1:X8}", num2, readStream.Position));
			}
			readStream.Read(array, 0, 8);
			long num3 = BitConverter.ToInt64(array, 0);
			array = new byte[num3];
			readStream.Read(array, 0, array.Length);
			num = BitConverter.ToInt64(array, 36);
			readStream.Seek(num, 0);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B46C File Offset: 0x0000966C
		private static uint ReadFirstFourBytes(Stream s)
		{
			return (uint)SharedUtilities.ReadInt(s);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B484 File Offset: 0x00009684
		private static void ReadCentralDirectory(ZipFile zf)
		{
			bool flag = false;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
			{
				zipEntry.ResetDirEntry();
				zf.OnReadEntry(true, null);
				if (zf.Verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("entry {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				if (zipEntry._InputUsesZip64)
				{
					flag = true;
				}
				dictionary.Add(zipEntry.FileName, null);
			}
			if (flag)
			{
				zf.UseZip64WhenSaving = Zip64Option.Always;
			}
			if (zf._locEndOfCDS > 0L)
			{
				zf.ReadStream.Seek(zf._locEndOfCDS, 0);
			}
			ZipFile.ReadCentralDirectoryFooter(zf);
			if (zf.Verbose && !string.IsNullOrEmpty(zf.Comment))
			{
				zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
			}
			if (zf.Verbose)
			{
				zf.StatusMessageTextWriter.WriteLine("read in {0} entries.", zf._entries.Count);
			}
			zf.OnReadCompleted();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B584 File Offset: 0x00009784
		private static void ReadIntoInstance_Orig(ZipFile zf)
		{
			zf.OnReadStarted();
			zf._entries = new Dictionary<string, ZipEntry>();
			if (zf.Verbose)
			{
				if (zf.Name == null)
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip from stream...");
				}
				else
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip {0}...", zf.Name);
				}
			}
			bool flag = true;
			ZipContainer zipContainer = new ZipContainer(zf);
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadEntry(zipContainer, flag)) != null)
			{
				if (zf.Verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("  {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				flag = false;
			}
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				ZipEntry zipEntry2;
				while ((zipEntry2 = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
				{
					ZipEntry zipEntry3 = zf._entries[zipEntry2.FileName];
					if (zipEntry3 != null)
					{
						zipEntry3._Comment = zipEntry2.Comment;
						if (zipEntry2.IsDirectory)
						{
							zipEntry3.MarkAsDirectory();
						}
					}
					dictionary.Add(zipEntry2.FileName, null);
				}
				if (zf._locEndOfCDS > 0L)
				{
					zf.ReadStream.Seek(zf._locEndOfCDS, 0);
				}
				ZipFile.ReadCentralDirectoryFooter(zf);
				if (zf.Verbose && !string.IsNullOrEmpty(zf.Comment))
				{
					zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
				}
			}
			catch (ZipException)
			{
			}
			catch (IOException)
			{
			}
			zf.OnReadCompleted();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B6F0 File Offset: 0x000098F0
		private static void ReadCentralDirectoryFooter(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			int num = SharedUtilities.ReadSignature(readStream);
			int num2 = 0;
			byte[] array;
			if ((long)num == 101075792L)
			{
				array = new byte[52];
				readStream.Read(array, 0, array.Length);
				long num3 = BitConverter.ToInt64(array, 0);
				if (num3 < 44L)
				{
					throw new ZipException("Bad size in the ZIP64 Central Directory.");
				}
				zf._versionMadeBy = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._versionNeededToExtract = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._diskNumberWithCd = BitConverter.ToUInt32(array, num2);
				num2 += 2;
				array = new byte[num3 - 44L];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
				if ((long)num != 117853008L)
				{
					throw new ZipException("Inconsistent metadata in the ZIP64 Central Directory.");
				}
				array = new byte[16];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
			}
			if ((long)num != 101010256L)
			{
				readStream.Seek(-4L, 1);
				throw new BadReadException(string.Format("Bad signature ({0:X8}) at position 0x{1:X8}", num, readStream.Position));
			}
			array = new byte[16];
			zf.ReadStream.Read(array, 0, array.Length);
			if (zf._diskNumberWithCd == 0U)
			{
				zf._diskNumberWithCd = (uint)BitConverter.ToUInt16(array, 2);
			}
			ZipFile.ReadZipFileComment(zf);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000B838 File Offset: 0x00009A38
		private static void ReadZipFileComment(ZipFile zf)
		{
			byte[] array = new byte[2];
			zf.ReadStream.Read(array, 0, array.Length);
			short num = (short)((int)array[0] + (int)array[1] * 256);
			if (num > 0)
			{
				array = new byte[(int)num];
				zf.ReadStream.Read(array, 0, array.Length);
				string @string = zf.AlternateEncoding.GetString(array, 0, array.Length);
				zf.Comment = @string;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		public static bool IsZipFile(string fileName)
		{
			return ZipFile.IsZipFile(fileName, false);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public static bool IsZipFile(string fileName, bool testExtract)
		{
			bool flag = false;
			try
			{
				if (!File.Exists(fileName))
				{
					return false;
				}
				using (FileStream fileStream = File.Open(fileName, 3, 1, 3))
				{
					flag = ZipFile.IsZipFile(fileStream, testExtract);
				}
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return flag;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000B918 File Offset: 0x00009B18
		public static bool IsZipFile(Stream stream, bool testExtract)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag = false;
			try
			{
				if (!stream.CanRead)
				{
					return false;
				}
				Stream @null = Stream.Null;
				using (ZipFile zipFile = ZipFile.Read(stream, null, null, null))
				{
					if (testExtract)
					{
						foreach (ZipEntry zipEntry in zipFile)
						{
							if (!zipEntry.IsDirectory)
							{
								zipEntry.Extract(@null);
							}
						}
					}
				}
				flag = true;
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return flag;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private void DeleteFileWithRetry(string filename)
		{
			bool flag = false;
			int num = 3;
			int num2 = 0;
			while (num2 < num && !flag)
			{
				try
				{
					File.Delete(filename);
					flag = true;
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine("************************************************** Retry delete.");
					Thread.Sleep(200 + num2 * 200);
				}
				num2++;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000BA38 File Offset: 0x00009C38
		public void Save()
		{
			try
			{
				bool flag = false;
				this._saveOperationCanceled = false;
				this._numberOfSegmentsForMostRecentSave = 0U;
				this.OnSaveStarted();
				if (this.WriteStream == null)
				{
					throw new BadStateException("You haven't specified where to save the zip.");
				}
				if (this._name != null && this._name.EndsWith(".exe") && !this._SavingSfx)
				{
					throw new BadStateException("You specified an EXE for a plain zip file.");
				}
				if (!this._contentsChanged)
				{
					this.OnSaveCompleted();
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("No save is necessary....");
					}
				}
				else
				{
					this.Reset(true);
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("saving....");
					}
					if (this._entries.Count >= 65535 && this._zip64 == Zip64Option.Default)
					{
						throw new ZipException("The number of entries is 65535 or greater. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
					}
					int num = 0;
					ICollection<ZipEntry> collection = (this.SortEntriesBeforeSaving ? this.EntriesSorted : this.Entries);
					foreach (ZipEntry zipEntry in collection)
					{
						this.OnSaveEntry(num, zipEntry, true);
						zipEntry.Write(this.WriteStream);
						if (this._saveOperationCanceled)
						{
							break;
						}
						num++;
						this.OnSaveEntry(num, zipEntry, false);
						if (this._saveOperationCanceled)
						{
							break;
						}
						if (zipEntry.IncludedInMostRecentSave)
						{
							flag |= zipEntry.OutputUsedZip64.Value;
						}
					}
					if (!this._saveOperationCanceled)
					{
						ZipSegmentedStream zipSegmentedStream = this.WriteStream as ZipSegmentedStream;
						this._numberOfSegmentsForMostRecentSave = ((zipSegmentedStream != null) ? zipSegmentedStream.CurrentSegment : 1U);
						bool flag2 = ZipOutput.WriteCentralDirectoryStructure(this.WriteStream, collection, this._numberOfSegmentsForMostRecentSave, this._zip64, this.Comment, new ZipContainer(this));
						this.OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
						this._hasBeenSaved = true;
						this._contentsChanged = false;
						flag = flag || flag2;
						this._OutputUsesZip64 = new bool?(flag);
						if (this._name != null && (this._temporaryFileName != null || zipSegmentedStream != null))
						{
							this.WriteStream.Close();
							if (this._saveOperationCanceled)
							{
								return;
							}
							if (this._fileAlreadyExists && this._readstream != null)
							{
								this._readstream.Close();
								this._readstream = null;
								foreach (ZipEntry zipEntry2 in collection)
								{
									ZipSegmentedStream zipSegmentedStream2 = zipEntry2._archiveStream as ZipSegmentedStream;
									if (zipSegmentedStream2 != null)
									{
										zipSegmentedStream2.Close();
									}
									zipEntry2._archiveStream = null;
								}
							}
							string text = null;
							if (File.Exists(this._name))
							{
								text = this._name + "." + SharedUtilities.GenerateRandomStringImpl(8, 0) + ".tmp";
								if (File.Exists(text))
								{
									this.DeleteFileWithRetry(text);
								}
								File.Move(this._name, text);
							}
							this.OnSaveEvent(ZipProgressEventType.Saving_BeforeRenameTempArchive);
							File.Move((zipSegmentedStream != null) ? zipSegmentedStream.CurrentTempName : this._temporaryFileName, this._name);
							this.OnSaveEvent(ZipProgressEventType.Saving_AfterRenameTempArchive);
							if (text != null)
							{
								try
								{
									if (File.Exists(text))
									{
										File.Delete(text);
									}
								}
								catch
								{
								}
							}
							this._fileAlreadyExists = true;
						}
						ZipFile.NotifyEntriesSaveComplete(collection);
						this.OnSaveCompleted();
						this._JustSaved = true;
					}
				}
			}
			finally
			{
				this.CleanupAfterSaveOperation();
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000BDCC File Offset: 0x00009FCC
		private static void NotifyEntriesSaveComplete(ICollection<ZipEntry> c)
		{
			foreach (ZipEntry zipEntry in c)
			{
				zipEntry.NotifySaveComplete();
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000BE14 File Offset: 0x0000A014
		private void RemoveTempFile()
		{
			try
			{
				if (File.Exists(this._temporaryFileName))
				{
					File.Delete(this._temporaryFileName);
				}
			}
			catch (IOException ex)
			{
				if (this.Verbose)
				{
					this.StatusMessageTextWriter.WriteLine("ZipFile::Save: could not delete temp file: {0}.", ex.Message);
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000BE6C File Offset: 0x0000A06C
		private void CleanupAfterSaveOperation()
		{
			if (this._name != null)
			{
				if (this._writestream != null)
				{
					try
					{
						this._writestream.Close();
					}
					catch (IOException)
					{
					}
				}
				this._writestream = null;
				if (this._temporaryFileName != null)
				{
					this.RemoveTempFile();
					this._temporaryFileName = null;
				}
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		public void Save(string fileName)
		{
			if (this._name == null)
			{
				this._writestream = null;
			}
			else
			{
				this._readName = this._name;
			}
			this._name = fileName;
			if (Directory.Exists(this._name))
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "fileName"));
			}
			this._contentsChanged = true;
			this._fileAlreadyExists = File.Exists(this._name);
			this.Save();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000BF40 File Offset: 0x0000A140
		public void Save(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (!outputStream.CanWrite)
			{
				throw new ArgumentException("Must be a writable stream.", "outputStream");
			}
			this._name = null;
			this._writestream = new CountingStream(outputStream);
			this._contentsChanged = true;
			this._fileAlreadyExists = false;
			this.Save();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public IEnumerator<ZipEntry> GetEnumerator()
		{
			return new ZipFile.<GetEnumerator>d__3(0)
			{
				<>4__this = this
			};
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		[DispId(-4)]
		public IEnumerator GetNewEnum()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C100 File Offset: 0x0000A300
		// Note: this type is marked as 'beforefieldinit'.
		static ZipFile()
		{
		}

		// Token: 0x040000B5 RID: 181
		private TextWriter _StatusMessageTextWriter;

		// Token: 0x040000B6 RID: 182
		private bool _CaseSensitiveRetrieval;

		// Token: 0x040000B7 RID: 183
		private Stream _readstream;

		// Token: 0x040000B8 RID: 184
		private Stream _writestream;

		// Token: 0x040000B9 RID: 185
		private ushort _versionMadeBy;

		// Token: 0x040000BA RID: 186
		private ushort _versionNeededToExtract;

		// Token: 0x040000BB RID: 187
		private uint _diskNumberWithCd;

		// Token: 0x040000BC RID: 188
		private int _maxOutputSegmentSize;

		// Token: 0x040000BD RID: 189
		private uint _numberOfSegmentsForMostRecentSave;

		// Token: 0x040000BE RID: 190
		private ZipErrorAction _zipErrorAction;

		// Token: 0x040000BF RID: 191
		private bool _disposed;

		// Token: 0x040000C0 RID: 192
		private Dictionary<string, ZipEntry> _entries;

		// Token: 0x040000C1 RID: 193
		private List<ZipEntry> _zipEntriesAsList;

		// Token: 0x040000C2 RID: 194
		private string _name;

		// Token: 0x040000C3 RID: 195
		private string _readName;

		// Token: 0x040000C4 RID: 196
		private string _Comment;

		// Token: 0x040000C5 RID: 197
		internal string _Password;

		// Token: 0x040000C6 RID: 198
		private bool _emitNtfsTimes = true;

		// Token: 0x040000C7 RID: 199
		private bool _emitUnixTimes;

		// Token: 0x040000C8 RID: 200
		private CompressionStrategy _Strategy;

		// Token: 0x040000C9 RID: 201
		private CompressionMethod _compressionMethod = CompressionMethod.Deflate;

		// Token: 0x040000CA RID: 202
		private bool _fileAlreadyExists;

		// Token: 0x040000CB RID: 203
		private string _temporaryFileName;

		// Token: 0x040000CC RID: 204
		private bool _contentsChanged;

		// Token: 0x040000CD RID: 205
		private bool _hasBeenSaved;

		// Token: 0x040000CE RID: 206
		private string _TempFileFolder;

		// Token: 0x040000CF RID: 207
		private bool _ReadStreamIsOurs = true;

		// Token: 0x040000D0 RID: 208
		private object LOCK = new object();

		// Token: 0x040000D1 RID: 209
		private bool _saveOperationCanceled;

		// Token: 0x040000D2 RID: 210
		private bool _extractOperationCanceled;

		// Token: 0x040000D3 RID: 211
		private bool _addOperationCanceled;

		// Token: 0x040000D4 RID: 212
		private EncryptionAlgorithm _Encryption;

		// Token: 0x040000D5 RID: 213
		private bool _JustSaved;

		// Token: 0x040000D6 RID: 214
		private long _locEndOfCDS = -1L;

		// Token: 0x040000D7 RID: 215
		private uint _OffsetOfCentralDirectory;

		// Token: 0x040000D8 RID: 216
		private long _OffsetOfCentralDirectory64;

		// Token: 0x040000D9 RID: 217
		private bool? _OutputUsesZip64;

		// Token: 0x040000DA RID: 218
		internal bool _inExtractAll;

		// Token: 0x040000DB RID: 219
		private Encoding _alternateEncoding = Encoding.GetEncoding("IBM437");

		// Token: 0x040000DC RID: 220
		private ZipOption _alternateEncodingUsage;

		// Token: 0x040000DD RID: 221
		private static Encoding _defaultEncoding = Encoding.GetEncoding("IBM437");

		// Token: 0x040000DE RID: 222
		private int _BufferSize = ZipFile.BufferSizeDefault;

		// Token: 0x040000DF RID: 223
		internal Zip64Option _zip64;

		// Token: 0x040000E0 RID: 224
		private bool _SavingSfx;

		// Token: 0x040000E1 RID: 225
		public static readonly int BufferSizeDefault = 32768;

		// Token: 0x040000E2 RID: 226
		private EventHandler<SaveProgressEventArgs> SaveProgress;

		// Token: 0x040000E3 RID: 227
		private EventHandler<ReadProgressEventArgs> ReadProgress;

		// Token: 0x040000E4 RID: 228
		private long _lengthOfReadStream = -99L;

		// Token: 0x040000E5 RID: 229
		private EventHandler<ExtractProgressEventArgs> ExtractProgress;

		// Token: 0x040000E6 RID: 230
		private EventHandler<AddProgressEventArgs> AddProgress;

		// Token: 0x040000E7 RID: 231
		private EventHandler<ZipErrorEventArgs> ZipError;

		// Token: 0x040000E8 RID: 232
		private bool <FullScan>k__BackingField;

		// Token: 0x040000E9 RID: 233
		private bool <SortEntriesBeforeSaving>k__BackingField;

		// Token: 0x040000EA RID: 234
		private bool <AddDirectoryWillTraverseReparsePoints>k__BackingField;

		// Token: 0x040000EB RID: 235
		private int <CodecBufferSize>k__BackingField;

		// Token: 0x040000EC RID: 236
		private bool <FlattenFoldersOnExtract>k__BackingField;

		// Token: 0x040000ED RID: 237
		private CompressionLevel <CompressionLevel>k__BackingField;

		// Token: 0x040000EE RID: 238
		private ExtractExistingFileAction <ExtractExistingFile>k__BackingField;

		// Token: 0x040000EF RID: 239
		private SetCompressionCallback <SetCompression>k__BackingField;

		// Token: 0x0200005E RID: 94
		private sealed class <>c__DisplayClass1
		{
			// Token: 0x0600041A RID: 1050 RVA: 0x0000A442 File Offset: 0x00008642
			public <>c__DisplayClass1()
			{
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x0000A44A File Offset: 0x0000864A
			public int <get_EntriesSorted>b__0(ZipEntry x, ZipEntry y)
			{
				return string.Compare(x.FileName, y.FileName, this.sc);
			}

			// Token: 0x04000336 RID: 822
			public StringComparison sc;
		}

		// Token: 0x0200005F RID: 95
		private sealed class <GetEnumerator>d__3 : IEnumerator<ZipEntry>, IEnumerator, IDisposable
		{
			// Token: 0x0600041C RID: 1052 RVA: 0x0000BF9C File Offset: 0x0000A19C
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						this.<>7__wrap5 = this.<>4__this._entries.Values.GetEnumerator();
						this.<>1__state = 1;
						break;
					case 1:
						goto IL_0088;
					case 2:
						this.<>1__state = 1;
						break;
					default:
						goto IL_0088;
					}
					if (this.<>7__wrap5.MoveNext())
					{
						this.<e>5__4 = this.<>7__wrap5.Current;
						this.<>2__current = this.<e>5__4;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally6();
					IL_0088:
					flag = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000C050 File Offset: 0x0000A250
			ZipEntry IEnumerator<ZipEntry>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x0000C058 File Offset: 0x0000A258
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000C060 File Offset: 0x0000A260
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case 1:
				case 2:
					try
					{
					}
					finally
					{
						this.<>m__Finally6();
					}
					return;
				default:
					return;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
			[DebuggerHidden]
			public <GetEnumerator>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x0000C0B7 File Offset: 0x0000A2B7
			private void <>m__Finally6()
			{
				this.<>1__state = -1;
				this.<>7__wrap5.Dispose();
			}

			// Token: 0x04000337 RID: 823
			private ZipEntry <>2__current;

			// Token: 0x04000338 RID: 824
			private int <>1__state;

			// Token: 0x04000339 RID: 825
			public ZipFile <>4__this;

			// Token: 0x0400033A RID: 826
			public ZipEntry <e>5__4;

			// Token: 0x0400033B RID: 827
			public Dictionary<string, ZipEntry>.ValueCollection.Enumerator <>7__wrap5;
		}
	}
}
