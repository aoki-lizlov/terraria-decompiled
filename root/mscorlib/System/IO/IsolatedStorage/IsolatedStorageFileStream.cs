using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000997 RID: 2455
	[ComVisible(true)]
	public class IsolatedStorageFileStream : FileStream
	{
		// Token: 0x060059A3 RID: 22947 RVA: 0x00130648 File Offset: 0x0012E848
		[ReflectionPermission(SecurityAction.Assert, TypeInformation = true)]
		private static string CreateIsolatedPath(IsolatedStorageFile isf, string path, FileMode mode)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!Enum.IsDefined(typeof(FileMode), mode))
			{
				throw new ArgumentException("mode");
			}
			if (isf == null)
			{
				isf = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, IsolatedStorageFile.GetDomainIdentityFromEvidence(AppDomain.CurrentDomain.Evidence), IsolatedStorageFile.GetAssemblyIdentityFromEvidence(new StackFrame(3).GetMethod().ReflectedType.Assembly.UnprotectedGetEvidence()));
			}
			if (isf.IsDisposed)
			{
				throw new ObjectDisposedException("IsolatedStorageFile");
			}
			if (isf.IsClosed)
			{
				throw new InvalidOperationException("Storage needs to be open for this operation.");
			}
			FileInfo fileInfo = new FileInfo(isf.Root);
			if (!fileInfo.Directory.Exists)
			{
				fileInfo.Directory.Create();
			}
			if (Path.IsPathRooted(path))
			{
				string pathRoot = Path.GetPathRoot(path);
				path = path.Remove(0, pathRoot.Length);
			}
			string text = Path.Combine(isf.Root, path);
			Path.GetFullPath(text);
			if (!Path.GetFullPath(text).StartsWith(isf.Root))
			{
				throw new IsolatedStorageException();
			}
			fileInfo = new FileInfo(text);
			if (!fileInfo.Directory.Exists)
			{
				throw new DirectoryNotFoundException(string.Format(Locale.GetText("Could not find a part of the path \"{0}\"."), path));
			}
			return text;
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x0013077C File Offset: 0x0012E97C
		public IsolatedStorageFileStream(string path, FileMode mode)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, null)
		{
		}

		// Token: 0x060059A5 RID: 22949 RVA: 0x00130795 File Offset: 0x0012E995
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access)
			: this(path, mode, access, (access == FileAccess.Write) ? FileShare.None : FileShare.Read, 4096, null)
		{
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x001307AE File Offset: 0x0012E9AE
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share)
			: this(path, mode, access, share, 4096, null)
		{
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x001307C1 File Offset: 0x0012E9C1
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
			: this(path, mode, access, share, bufferSize, null)
		{
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x001307D1 File Offset: 0x0012E9D1
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, IsolatedStorageFile isf)
			: base(IsolatedStorageFileStream.CreateIsolatedPath(isf, path, mode), mode, access, share, bufferSize, false, true)
		{
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x001307EA File Offset: 0x0012E9EA
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, FileShare share, IsolatedStorageFile isf)
			: this(path, mode, access, share, 4096, isf)
		{
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x001307FE File Offset: 0x0012E9FE
		public IsolatedStorageFileStream(string path, FileMode mode, FileAccess access, IsolatedStorageFile isf)
			: this(path, mode, access, (access == FileAccess.Write) ? FileShare.None : FileShare.Read, 4096, isf)
		{
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x00130818 File Offset: 0x0012EA18
		public IsolatedStorageFileStream(string path, FileMode mode, IsolatedStorageFile isf)
			: this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, isf)
		{
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x00130831 File Offset: 0x0012EA31
		public override bool CanRead
		{
			get
			{
				return base.CanRead;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x060059AD RID: 22957 RVA: 0x00130839 File Offset: 0x0012EA39
		public override bool CanSeek
		{
			get
			{
				return base.CanSeek;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x060059AE RID: 22958 RVA: 0x00130841 File Offset: 0x0012EA41
		public override bool CanWrite
		{
			get
			{
				return base.CanWrite;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x060059AF RID: 22959 RVA: 0x00130849 File Offset: 0x0012EA49
		public override SafeFileHandle SafeFileHandle
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			get
			{
				throw new IsolatedStorageException(Locale.GetText("Information is restricted"));
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x060059B0 RID: 22960 RVA: 0x00130849 File Offset: 0x0012EA49
		[Obsolete("Use SafeFileHandle - once available")]
		public override IntPtr Handle
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			get
			{
				throw new IsolatedStorageException(Locale.GetText("Information is restricted"));
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x060059B1 RID: 22961 RVA: 0x0013085A File Offset: 0x0012EA5A
		public override bool IsAsync
		{
			get
			{
				return base.IsAsync;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x060059B2 RID: 22962 RVA: 0x00130862 File Offset: 0x0012EA62
		public override long Length
		{
			get
			{
				return base.Length;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x060059B3 RID: 22963 RVA: 0x0013086A File Offset: 0x0012EA6A
		// (set) Token: 0x060059B4 RID: 22964 RVA: 0x00130872 File Offset: 0x0012EA72
		public override long Position
		{
			get
			{
				return base.Position;
			}
			set
			{
				base.Position = value;
			}
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x0013087B File Offset: 0x0012EA7B
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			return base.BeginRead(buffer, offset, numBytes, userCallback, stateObject);
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x0013088A File Offset: 0x0012EA8A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			return base.BeginWrite(buffer, offset, numBytes, userCallback, stateObject);
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x00130899 File Offset: 0x0012EA99
		public override int EndRead(IAsyncResult asyncResult)
		{
			return base.EndRead(asyncResult);
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x001308A2 File Offset: 0x0012EAA2
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.EndWrite(asyncResult);
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x001308AB File Offset: 0x0012EAAB
		public override void Flush()
		{
			base.Flush();
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x001308B3 File Offset: 0x0012EAB3
		public override void Flush(bool flushToDisk)
		{
			base.Flush(flushToDisk);
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x001308BC File Offset: 0x0012EABC
		public override int Read(byte[] buffer, int offset, int count)
		{
			return base.Read(buffer, offset, count);
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x001308C7 File Offset: 0x0012EAC7
		public override int ReadByte()
		{
			return base.ReadByte();
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x001308CF File Offset: 0x0012EACF
		public override long Seek(long offset, SeekOrigin origin)
		{
			return base.Seek(offset, origin);
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x001308D9 File Offset: 0x0012EAD9
		public override void SetLength(long value)
		{
			base.SetLength(value);
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x001308E2 File Offset: 0x0012EAE2
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.Write(buffer, offset, count);
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x001308ED File Offset: 0x0012EAED
		public override void WriteByte(byte value)
		{
			base.WriteByte(value);
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x001308F6 File Offset: 0x0012EAF6
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
