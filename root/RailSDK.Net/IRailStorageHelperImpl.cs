using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200002C RID: 44
	public class IRailStorageHelperImpl : RailObject, IRailStorageHelper
	{
		// Token: 0x060012DA RID: 4826 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailStorageHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000071D0 File Offset: 0x000053D0
		~IRailStorageHelperImpl()
		{
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000071F8 File Offset: 0x000053F8
		public virtual IRailFile OpenFile(string filename, out RailResult result)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStorageHelper_OpenFile__SWIG_0(this.swigCPtr_, filename, out result);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFileImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00007228 File Offset: 0x00005428
		public virtual IRailFile OpenFile(string filename)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStorageHelper_OpenFile__SWIG_1(this.swigCPtr_, filename);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFileImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00007258 File Offset: 0x00005458
		public virtual IRailFile CreateFile(string filename, out RailResult result)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStorageHelper_CreateFile__SWIG_0(this.swigCPtr_, filename, out result);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFileImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00007288 File Offset: 0x00005488
		public virtual IRailFile CreateFile(string filename)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStorageHelper_CreateFile__SWIG_1(this.swigCPtr_, filename);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFileImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000072B7 File Offset: 0x000054B7
		public virtual bool IsFileExist(string filename)
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_IsFileExist(this.swigCPtr_, filename);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000072C8 File Offset: 0x000054C8
		public virtual bool ListFiles(List<string> filelist)
		{
			IntPtr intPtr = ((filelist == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailStorageHelper_ListFiles(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (filelist != null)
				{
					RailConverter.Cpp2Csharp(intPtr, filelist);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00007318 File Offset: 0x00005518
		public virtual RailResult RemoveFile(string filename)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_RemoveFile(this.swigCPtr_, filename);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00007326 File Offset: 0x00005526
		public virtual bool IsFileSyncedToCloud(string filename)
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_IsFileSyncedToCloud(this.swigCPtr_, filename);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00007334 File Offset: 0x00005534
		public virtual RailResult GetFileTimestamp(string filename, out ulong time_stamp)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_GetFileTimestamp(this.swigCPtr_, filename, out time_stamp);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00007343 File Offset: 0x00005543
		public virtual uint GetFileCount()
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_GetFileCount(this.swigCPtr_);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00007350 File Offset: 0x00005550
		public virtual RailResult GetFileNameAndSize(uint file_index, out string filename, out ulong file_size)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_GetFileNameAndSize(this.swigCPtr_, file_index, intPtr, out file_size);
			}
			finally
			{
				filename = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00007394 File Offset: 0x00005594
		public virtual RailResult AsyncQueryQuota()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncQueryQuota(this.swigCPtr_);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000073A4 File Offset: 0x000055A4
		public virtual RailResult SetSyncFileOption(string filename, RailSyncFileOption option)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSyncFileOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_SetSyncFileOption(this.swigCPtr_, filename, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSyncFileOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000073F4 File Offset: 0x000055F4
		public virtual bool IsCloudStorageEnabledForApp()
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_IsCloudStorageEnabledForApp(this.swigCPtr_);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00007401 File Offset: 0x00005601
		public virtual bool IsCloudStorageEnabledForPlayer()
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_IsCloudStorageEnabledForPlayer(this.swigCPtr_);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00007410 File Offset: 0x00005610
		public virtual RailResult AsyncPublishFileToUserSpace(RailPublishFileToUserSpaceOption option, string user_data)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailPublishFileToUserSpaceOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncPublishFileToUserSpace(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailPublishFileToUserSpaceOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00007460 File Offset: 0x00005660
		public virtual IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option, out RailResult result)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStreamFileOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			IRailStreamFile railStreamFile;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailStorageHelper_OpenStreamFile__SWIG_0(this.swigCPtr_, filename, intPtr, out result);
				railStreamFile = ((intPtr2 == IntPtr.Zero) ? null : new IRailStreamFileImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailStreamFileOption(intPtr);
			}
			return railStreamFile;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000074C8 File Offset: 0x000056C8
		public virtual IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailStreamFileOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			IRailStreamFile railStreamFile;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailStorageHelper_OpenStreamFile__SWIG_1(this.swigCPtr_, filename, intPtr);
				railStreamFile = ((intPtr2 == IntPtr.Zero) ? null : new IRailStreamFileImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailStreamFileOption(intPtr);
			}
			return railStreamFile;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00007530 File Offset: 0x00005730
		public virtual RailResult AsyncListStreamFiles(string contents, RailListStreamFileOption option, string user_data)
		{
			IntPtr intPtr = ((option == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailListStreamFileOption__SWIG_0());
			if (option != null)
			{
				RailConverter.Csharp2Cpp(option, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncListStreamFiles(this.swigCPtr_, contents, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailListStreamFileOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00007584 File Offset: 0x00005784
		public virtual RailResult AsyncRenameStreamFile(string old_filename, string new_filename, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncRenameStreamFile(this.swigCPtr_, old_filename, new_filename, user_data);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00007594 File Offset: 0x00005794
		public virtual RailResult AsyncDeleteStreamFile(string filename, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_AsyncDeleteStreamFile(this.swigCPtr_, filename, user_data);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000075A3 File Offset: 0x000057A3
		public virtual uint GetRailFileEnabledOS(string filename)
		{
			return RAIL_API_PINVOKE.IRailStorageHelper_GetRailFileEnabledOS(this.swigCPtr_, filename);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000075B1 File Offset: 0x000057B1
		public virtual RailResult SetRailFileEnabledOS(string filename, EnumRailStorageFileEnabledOS sync_os)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStorageHelper_SetRailFileEnabledOS(this.swigCPtr_, filename, (int)sync_os);
		}
	}
}
