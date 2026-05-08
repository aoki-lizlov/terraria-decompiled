using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace System.IO.Enumeration
{
	// Token: 0x020009A8 RID: 2472
	public abstract class FileSystemEnumerator<TResult> : CriticalFinalizerObject, IEnumerator<TResult>, IDisposable, IEnumerator
	{
		// Token: 0x06005A0B RID: 23051 RVA: 0x001311F0 File Offset: 0x0012F3F0
		public FileSystemEnumerator(string directory, EnumerationOptions options = null)
		{
			if (directory == null)
			{
				throw new ArgumentNullException("directory");
			}
			this._originalRootDirectory = directory;
			this._rootDirectory = PathInternal.TrimEndingDirectorySeparator(Path.GetFullPath(directory));
			this._options = options ?? EnumerationOptions.Default;
			this._directoryHandle = this.CreateDirectoryHandle(this._rootDirectory, false);
			if (this._directoryHandle == IntPtr.Zero)
			{
				this._lastEntryFound = true;
			}
			this._currentPath = this._rootDirectory;
			try
			{
				this._pathBuffer = ArrayPool<char>.Shared.Rent(4096);
				int readDirRBufferSize = Interop.Sys.GetReadDirRBufferSize();
				this._entryBuffer = ((readDirRBufferSize > 0) ? ArrayPool<byte>.Shared.Rent(readDirRBufferSize) : null);
			}
			catch
			{
				this.CloseDirectoryHandle();
				throw;
			}
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x001312CC File Offset: 0x0012F4CC
		private bool InternalContinueOnError(Interop.ErrorInfo info, bool ignoreNotFound = false)
		{
			return (ignoreNotFound && FileSystemEnumerator<TResult>.IsDirectoryNotFound(info)) || (this._options.IgnoreInaccessible && FileSystemEnumerator<TResult>.IsAccessError(info)) || this.ContinueOnError(info.RawErrno);
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x001312FD File Offset: 0x0012F4FD
		private static bool IsDirectoryNotFound(Interop.ErrorInfo info)
		{
			return info.Error == Interop.Error.ENOTDIR || info.Error == Interop.Error.ENOENT;
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x0013131D File Offset: 0x0012F51D
		private static bool IsAccessError(Interop.ErrorInfo info)
		{
			return info.Error == Interop.Error.EACCES || info.Error == Interop.Error.EBADF || info.Error == Interop.Error.EPERM;
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x0013134C File Offset: 0x0012F54C
		private IntPtr CreateDirectoryHandle(string path, bool ignoreNotFound = false)
		{
			IntPtr intPtr = Interop.Sys.OpenDir(path);
			if (!(intPtr == IntPtr.Zero))
			{
				return intPtr;
			}
			Interop.ErrorInfo lastErrorInfo = Interop.Sys.GetLastErrorInfo();
			if (this.InternalContinueOnError(lastErrorInfo, ignoreNotFound))
			{
				return IntPtr.Zero;
			}
			throw Interop.GetExceptionForIoErrno(lastErrorInfo, path, true);
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x00131390 File Offset: 0x0012F590
		private void CloseDirectoryHandle()
		{
			IntPtr intPtr = Interlocked.Exchange(ref this._directoryHandle, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				Interop.Sys.CloseDir(intPtr);
			}
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x001313C4 File Offset: 0x0012F5C4
		public unsafe bool MoveNext()
		{
			if (this._lastEntryFound)
			{
				return false;
			}
			FileSystemEntry fileSystemEntry = default(FileSystemEntry);
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				if (this._lastEntryFound)
				{
					flag2 = false;
				}
				else
				{
					byte[] entryBuffer;
					byte* ptr;
					if ((entryBuffer = this._entryBuffer) == null || entryBuffer.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &entryBuffer[0];
					}
					for (;;)
					{
						this.FindNextEntry(ptr, (this._entryBuffer == null) ? 0 : this._entryBuffer.Length);
						if (this._lastEntryFound)
						{
							break;
						}
						FileAttributes fileAttributes = FileSystemEntry.Initialize(ref fileSystemEntry, this._entry, this._currentPath, this._rootDirectory, this._originalRootDirectory, new Span<char>(this._pathBuffer));
						bool flag3 = (fileAttributes & FileAttributes.Directory) > (FileAttributes)0;
						bool flag4 = false;
						if (flag3 && *this._entry.Name == 46 && (this._entry.Name[1] == 0 || (this._entry.Name[1] == 46 && this._entry.Name[2] == 0)))
						{
							if (!this._options.ReturnSpecialDirectories)
							{
								continue;
							}
							flag4 = true;
						}
						if (!flag4 && this._options.AttributesToSkip != (FileAttributes)0)
						{
							if ((this._options.AttributesToSkip & FileAttributes.ReadOnly) != (FileAttributes)0)
							{
								fileAttributes = fileSystemEntry.Attributes;
							}
							if ((this._options.AttributesToSkip & fileAttributes) != (FileAttributes)0)
							{
								continue;
							}
						}
						if (flag3 && !flag4 && this._options.RecurseSubdirectories && this.ShouldRecurseIntoEntry(ref fileSystemEntry))
						{
							if (this._pending == null)
							{
								this._pending = new Queue<string>();
							}
							this._pending.Enqueue(Path.Join(this._currentPath, fileSystemEntry.FileName));
						}
						if (this.ShouldIncludeEntry(ref fileSystemEntry))
						{
							goto Block_21;
						}
					}
					return false;
					Block_21:
					this._current = this.TransformEntry(ref fileSystemEntry);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06005A12 RID: 23058 RVA: 0x001315C8 File Offset: 0x0012F7C8
		private unsafe void FindNextEntry()
		{
			byte[] array;
			byte* ptr;
			if ((array = this._entryBuffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			this.FindNextEntry(ptr, (this._entryBuffer == null) ? 0 : this._entryBuffer.Length);
			array = null;
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x00131610 File Offset: 0x0012F810
		private unsafe void FindNextEntry(byte* entryBufferPtr, int bufferLength)
		{
			int num = Interop.Sys.ReadDirR(this._directoryHandle, entryBufferPtr, bufferLength, out this._entry);
			if (num == -1)
			{
				this.DirectoryFinished();
				return;
			}
			if (num == 0)
			{
				return;
			}
			if (this.InternalContinueOnError(new Interop.ErrorInfo(num), false))
			{
				this.DirectoryFinished();
				return;
			}
			throw Interop.GetExceptionForIoErrno(new Interop.ErrorInfo(num), this._currentPath, true);
		}

		// Token: 0x06005A14 RID: 23060 RVA: 0x0013166C File Offset: 0x0012F86C
		private bool DequeueNextDirectory()
		{
			this._directoryHandle = IntPtr.Zero;
			while (this._directoryHandle == IntPtr.Zero)
			{
				if (this._pending == null || this._pending.Count == 0)
				{
					return false;
				}
				this._currentPath = this._pending.Dequeue();
				this._directoryHandle = this.CreateDirectoryHandle(this._currentPath, true);
			}
			return true;
		}

		// Token: 0x06005A15 RID: 23061 RVA: 0x001316D4 File Offset: 0x0012F8D4
		private void InternalDispose(bool disposing)
		{
			if (this._lock != null)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					this._lastEntryFound = true;
					this._pending = null;
					this.CloseDirectoryHandle();
					if (this._pathBuffer != null)
					{
						ArrayPool<char>.Shared.Return(this._pathBuffer, false);
					}
					this._pathBuffer = null;
					if (this._entryBuffer != null)
					{
						ArrayPool<byte>.Shared.Return(this._entryBuffer, false);
					}
					this._entryBuffer = null;
				}
			}
			this.Dispose(disposing);
		}

		// Token: 0x06005A16 RID: 23062 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected virtual bool ShouldIncludeEntry(ref FileSystemEntry entry)
		{
			return true;
		}

		// Token: 0x06005A17 RID: 23063 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected virtual bool ShouldRecurseIntoEntry(ref FileSystemEntry entry)
		{
			return true;
		}

		// Token: 0x06005A18 RID: 23064
		protected abstract TResult TransformEntry(ref FileSystemEntry entry);

		// Token: 0x06005A19 RID: 23065 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnDirectoryFinished(ReadOnlySpan<char> directory)
		{
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x0000408A File Offset: 0x0000228A
		protected virtual bool ContinueOnError(int error)
		{
			return false;
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06005A1B RID: 23067 RVA: 0x00131774 File Offset: 0x0012F974
		public TResult Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06005A1C RID: 23068 RVA: 0x0013177C File Offset: 0x0012F97C
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x00131789 File Offset: 0x0012F989
		private void DirectoryFinished()
		{
			this._entry = default(Interop.Sys.DirectoryEntry);
			this.CloseDirectoryHandle();
			this.OnDirectoryFinished(this._currentPath);
			if (!this.DequeueNextDirectory())
			{
				this._lastEntryFound = true;
				return;
			}
			this.FindNextEntry();
		}

		// Token: 0x06005A1E RID: 23070 RVA: 0x00047E00 File Offset: 0x00046000
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A1F RID: 23071 RVA: 0x001317C4 File Offset: 0x0012F9C4
		public void Dispose()
		{
			this.InternalDispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005A20 RID: 23072 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x001317D4 File Offset: 0x0012F9D4
		~FileSystemEnumerator()
		{
			this.InternalDispose(false);
		}

		// Token: 0x040035B1 RID: 13745
		private const int StandardBufferSize = 4096;

		// Token: 0x040035B2 RID: 13746
		private readonly string _originalRootDirectory;

		// Token: 0x040035B3 RID: 13747
		private readonly string _rootDirectory;

		// Token: 0x040035B4 RID: 13748
		private readonly EnumerationOptions _options;

		// Token: 0x040035B5 RID: 13749
		private readonly object _lock = new object();

		// Token: 0x040035B6 RID: 13750
		private string _currentPath;

		// Token: 0x040035B7 RID: 13751
		private IntPtr _directoryHandle;

		// Token: 0x040035B8 RID: 13752
		private bool _lastEntryFound;

		// Token: 0x040035B9 RID: 13753
		private Queue<string> _pending;

		// Token: 0x040035BA RID: 13754
		private Interop.Sys.DirectoryEntry _entry;

		// Token: 0x040035BB RID: 13755
		private TResult _current;

		// Token: 0x040035BC RID: 13756
		private char[] _pathBuffer;

		// Token: 0x040035BD RID: 13757
		private byte[] _entryBuffer;
	}
}
