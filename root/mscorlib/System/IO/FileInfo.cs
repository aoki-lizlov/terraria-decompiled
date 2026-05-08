using System;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;

namespace System.IO
{
	// Token: 0x02000957 RID: 2391
	[Serializable]
	public sealed class FileInfo : FileSystemInfo
	{
		// Token: 0x0600565A RID: 22106 RVA: 0x00123A16 File Offset: 0x00121C16
		private FileInfo()
		{
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x00123A1E File Offset: 0x00121C1E
		public FileInfo(string fileName)
			: this(fileName, null, null, false)
		{
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00123A2C File Offset: 0x00121C2C
		internal FileInfo(string originalPath, string fullPath = null, string fileName = null, bool isNormalized = false)
		{
			if (originalPath == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.OriginalPath = originalPath;
			fullPath = fullPath ?? originalPath;
			this.FullPath = (isNormalized ? (fullPath ?? originalPath) : Path.GetFullPath(fullPath));
			this._name = fileName ?? Path.GetFileName(originalPath);
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600565D RID: 22109 RVA: 0x00123A86 File Offset: 0x00121C86
		public long Length
		{
			get
			{
				if ((base.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
				{
					throw new FileNotFoundException(SR.Format("Could not find file '{0}'.", this.FullPath), this.FullPath);
				}
				return base.LengthCore;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600565E RID: 22110 RVA: 0x00123AB7 File Offset: 0x00121CB7
		public string DirectoryName
		{
			get
			{
				return Path.GetDirectoryName(this.FullPath);
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600565F RID: 22111 RVA: 0x00123AC4 File Offset: 0x00121CC4
		public DirectoryInfo Directory
		{
			get
			{
				string directoryName = this.DirectoryName;
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName);
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06005660 RID: 22112 RVA: 0x00123AE3 File Offset: 0x00121CE3
		// (set) Token: 0x06005661 RID: 22113 RVA: 0x00123AF0 File Offset: 0x00121CF0
		public bool IsReadOnly
		{
			get
			{
				return (base.Attributes & FileAttributes.ReadOnly) > (FileAttributes)0;
			}
			set
			{
				if (value)
				{
					base.Attributes |= FileAttributes.ReadOnly;
					return;
				}
				base.Attributes &= ~FileAttributes.ReadOnly;
			}
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x00123B13 File Offset: 0x00121D13
		public StreamReader OpenText()
		{
			return new StreamReader(base.NormalizedPath, Encoding.UTF8, true);
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x00123B26 File Offset: 0x00121D26
		public StreamWriter CreateText()
		{
			return new StreamWriter(base.NormalizedPath, false);
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x00123B34 File Offset: 0x00121D34
		public StreamWriter AppendText()
		{
			return new StreamWriter(base.NormalizedPath, true);
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x00123B42 File Offset: 0x00121D42
		public FileInfo CopyTo(string destFileName)
		{
			return this.CopyTo(destFileName, false);
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x00123B4C File Offset: 0x00121D4C
		public FileInfo CopyTo(string destFileName, bool overwrite)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", "File name cannot be null.");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destFileName");
			}
			string fullPath = Path.GetFullPath(destFileName);
			FileSystem.CopyFile(this.FullPath, fullPath, overwrite);
			return new FileInfo(fullPath, null, null, true);
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x00123BA1 File Offset: 0x00121DA1
		public FileStream Create()
		{
			return File.Create(base.NormalizedPath);
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x00123BAE File Offset: 0x00121DAE
		public override void Delete()
		{
			FileSystem.DeleteFile(this.FullPath);
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x00123BBB File Offset: 0x00121DBB
		public FileStream Open(FileMode mode)
		{
			return this.Open(mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x00123BCD File Offset: 0x00121DCD
		public FileStream Open(FileMode mode, FileAccess access)
		{
			return this.Open(mode, access, FileShare.None);
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x00123BD8 File Offset: 0x00121DD8
		public FileStream Open(FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(base.NormalizedPath, mode, access, share);
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x00123BE8 File Offset: 0x00121DE8
		public FileStream OpenRead()
		{
			return new FileStream(base.NormalizedPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x00123BFE File Offset: 0x00121DFE
		public FileStream OpenWrite()
		{
			return new FileStream(base.NormalizedPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x00123C10 File Offset: 0x00121E10
		public void MoveTo(string destFileName)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "destFileName");
			}
			string fullPath = Path.GetFullPath(destFileName);
			if (!new DirectoryInfo(Path.GetDirectoryName(this.FullName)).Exists)
			{
				throw new DirectoryNotFoundException(SR.Format("Could not find a part of the path '{0}'.", this.FullName));
			}
			if (!this.Exists)
			{
				throw new FileNotFoundException(SR.Format("Could not find file '{0}'.", this.FullName), this.FullName);
			}
			FileSystem.MoveFile(this.FullPath, fullPath);
			this.FullPath = fullPath;
			this.OriginalPath = destFileName;
			this._name = Path.GetFileName(fullPath);
			base.Invalidate();
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x00123CC7 File Offset: 0x00121EC7
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
		{
			return this.Replace(destinationFileName, destinationBackupFileName, false);
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x00123CD2 File Offset: 0x00121ED2
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			FileSystem.ReplaceFile(this.FullPath, Path.GetFullPath(destinationFileName), (destinationBackupFileName != null) ? Path.GetFullPath(destinationBackupFileName) : null, ignoreMetadataErrors);
			return new FileInfo(destinationFileName);
		}

		// Token: 0x06005671 RID: 22129 RVA: 0x00123D06 File Offset: 0x00121F06
		public void Decrypt()
		{
			File.Decrypt(this.FullPath);
		}

		// Token: 0x06005672 RID: 22130 RVA: 0x00123D13 File Offset: 0x00121F13
		public void Encrypt()
		{
			File.Encrypt(this.FullPath);
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x0012194B File Offset: 0x0011FB4B
		private FileInfo(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005674 RID: 22132 RVA: 0x00123D20 File Offset: 0x00121F20
		public FileSecurity GetAccessControl()
		{
			return File.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06005675 RID: 22133 RVA: 0x00123D2F File Offset: 0x00121F2F
		public FileSecurity GetAccessControl(AccessControlSections includeSections)
		{
			return File.GetAccessControl(this.FullPath, includeSections);
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x00123D3D File Offset: 0x00121F3D
		public void SetAccessControl(FileSecurity fileSecurity)
		{
			File.SetAccessControl(this.FullPath, fileSecurity);
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x00123D4B File Offset: 0x00121F4B
		internal FileInfo(string fullPath, bool ignoreThis)
		{
			this._name = Path.GetFileName(fullPath);
			this.OriginalPath = this._name;
			this.FullPath = fullPath;
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06005678 RID: 22136 RVA: 0x00123D72 File Offset: 0x00121F72
		public override string Name
		{
			get
			{
				return this._name;
			}
		}
	}
}
