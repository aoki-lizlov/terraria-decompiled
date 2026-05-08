using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000144 RID: 324
	public interface IRailStorageHelper
	{
		// Token: 0x06001806 RID: 6150
		IRailFile OpenFile(string filename, out RailResult result);

		// Token: 0x06001807 RID: 6151
		IRailFile OpenFile(string filename);

		// Token: 0x06001808 RID: 6152
		IRailFile CreateFile(string filename, out RailResult result);

		// Token: 0x06001809 RID: 6153
		IRailFile CreateFile(string filename);

		// Token: 0x0600180A RID: 6154
		bool IsFileExist(string filename);

		// Token: 0x0600180B RID: 6155
		bool ListFiles(List<string> filelist);

		// Token: 0x0600180C RID: 6156
		RailResult RemoveFile(string filename);

		// Token: 0x0600180D RID: 6157
		bool IsFileSyncedToCloud(string filename);

		// Token: 0x0600180E RID: 6158
		RailResult GetFileTimestamp(string filename, out ulong time_stamp);

		// Token: 0x0600180F RID: 6159
		uint GetFileCount();

		// Token: 0x06001810 RID: 6160
		RailResult GetFileNameAndSize(uint file_index, out string filename, out ulong file_size);

		// Token: 0x06001811 RID: 6161
		RailResult AsyncQueryQuota();

		// Token: 0x06001812 RID: 6162
		RailResult SetSyncFileOption(string filename, RailSyncFileOption option);

		// Token: 0x06001813 RID: 6163
		bool IsCloudStorageEnabledForApp();

		// Token: 0x06001814 RID: 6164
		bool IsCloudStorageEnabledForPlayer();

		// Token: 0x06001815 RID: 6165
		RailResult AsyncPublishFileToUserSpace(RailPublishFileToUserSpaceOption option, string user_data);

		// Token: 0x06001816 RID: 6166
		IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option, out RailResult result);

		// Token: 0x06001817 RID: 6167
		IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option);

		// Token: 0x06001818 RID: 6168
		RailResult AsyncListStreamFiles(string contents, RailListStreamFileOption option, string user_data);

		// Token: 0x06001819 RID: 6169
		RailResult AsyncRenameStreamFile(string old_filename, string new_filename, string user_data);

		// Token: 0x0600181A RID: 6170
		RailResult AsyncDeleteStreamFile(string filename, string user_data);

		// Token: 0x0600181B RID: 6171
		uint GetRailFileEnabledOS(string filename);

		// Token: 0x0600181C RID: 6172
		RailResult SetRailFileEnabledOS(string filename, EnumRailStorageFileEnabledOS sync_os);
	}
}
