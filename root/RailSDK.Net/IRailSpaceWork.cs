using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200016F RID: 367
	public interface IRailSpaceWork : IRailComponent
	{
		// Token: 0x06001850 RID: 6224
		void Close();

		// Token: 0x06001851 RID: 6225
		SpaceWorkID GetSpaceWorkID();

		// Token: 0x06001852 RID: 6226
		bool Editable();

		// Token: 0x06001853 RID: 6227
		RailResult StartSync(string user_data);

		// Token: 0x06001854 RID: 6228
		RailResult GetSyncProgress(RailSpaceWorkSyncProgress progress);

		// Token: 0x06001855 RID: 6229
		RailResult CancelSync();

		// Token: 0x06001856 RID: 6230
		RailResult GetWorkLocalFolder(out string path);

		// Token: 0x06001857 RID: 6231
		RailResult AsyncUpdateMetadata(string user_data);

		// Token: 0x06001858 RID: 6232
		RailResult GetName(out string name);

		// Token: 0x06001859 RID: 6233
		RailResult GetDescription(out string description);

		// Token: 0x0600185A RID: 6234
		RailResult GetUrl(out string url);

		// Token: 0x0600185B RID: 6235
		uint GetCreateTime();

		// Token: 0x0600185C RID: 6236
		uint GetLastUpdateTime();

		// Token: 0x0600185D RID: 6237
		ulong GetWorkFileSize();

		// Token: 0x0600185E RID: 6238
		RailResult GetTags(List<string> tags);

		// Token: 0x0600185F RID: 6239
		RailResult GetPreviewImage(out string path);

		// Token: 0x06001860 RID: 6240
		RailResult GetVersion(out string version);

		// Token: 0x06001861 RID: 6241
		ulong GetDownloadCount();

		// Token: 0x06001862 RID: 6242
		ulong GetSubscribedCount();

		// Token: 0x06001863 RID: 6243
		EnumRailSpaceWorkShareLevel GetShareLevel();

		// Token: 0x06001864 RID: 6244
		ulong GetScore();

		// Token: 0x06001865 RID: 6245
		RailResult GetMetadata(string key, out string value);

		// Token: 0x06001866 RID: 6246
		EnumRailSpaceWorkVoteValue GetMyVote();

		// Token: 0x06001867 RID: 6247
		bool IsFavorite();

		// Token: 0x06001868 RID: 6248
		bool IsSubscribed();

		// Token: 0x06001869 RID: 6249
		RailResult SetName(string name);

		// Token: 0x0600186A RID: 6250
		RailResult SetDescription(string description);

		// Token: 0x0600186B RID: 6251
		RailResult SetTags(List<string> tags);

		// Token: 0x0600186C RID: 6252
		RailResult SetPreviewImage(string path_filename);

		// Token: 0x0600186D RID: 6253
		RailResult SetVersion(string version);

		// Token: 0x0600186E RID: 6254
		RailResult SetShareLevel(EnumRailSpaceWorkShareLevel level);

		// Token: 0x0600186F RID: 6255
		RailResult SetShareLevel();

		// Token: 0x06001870 RID: 6256
		RailResult SetMetadata(string key, string value);

		// Token: 0x06001871 RID: 6257
		RailResult SetContentFromFolder(string path);

		// Token: 0x06001872 RID: 6258
		RailResult GetAllMetadata(List<RailKeyValue> metadata);

		// Token: 0x06001873 RID: 6259
		RailResult GetAdditionalPreviewUrls(List<string> preview_urls);

		// Token: 0x06001874 RID: 6260
		RailResult GetAssociatedSpaceWorks(List<SpaceWorkID> ids);

		// Token: 0x06001875 RID: 6261
		RailResult GetLanguages(List<string> languages);

		// Token: 0x06001876 RID: 6262
		RailResult RemoveMetadata(string key);

		// Token: 0x06001877 RID: 6263
		RailResult SetAdditionalPreviews(List<string> local_paths);

		// Token: 0x06001878 RID: 6264
		RailResult SetAssociatedSpaceWorks(List<SpaceWorkID> ids);

		// Token: 0x06001879 RID: 6265
		RailResult SetLanguages(List<string> languages);

		// Token: 0x0600187A RID: 6266
		RailResult GetPreviewUrl(out string url);

		// Token: 0x0600187B RID: 6267
		RailResult GetVoteDetail(List<RailSpaceWorkVoteDetail> vote_details);

		// Token: 0x0600187C RID: 6268
		RailResult GetUploaderIDs(List<RailID> uploader_ids);

		// Token: 0x0600187D RID: 6269
		RailResult SetUpdateOptions(RailSpaceWorkUpdateOptions options);

		// Token: 0x0600187E RID: 6270
		RailResult GetStatistic(EnumRailSpaceWorkStatistic stat_type, out ulong value);

		// Token: 0x0600187F RID: 6271
		RailResult RemovePreviewImage();

		// Token: 0x06001880 RID: 6272
		uint GetState();

		// Token: 0x06001881 RID: 6273
		RailResult AddAssociatedGameIDs(List<RailGameID> game_ids);

		// Token: 0x06001882 RID: 6274
		RailResult RemoveAssociatedGameIDs(List<RailGameID> game_ids);

		// Token: 0x06001883 RID: 6275
		RailResult GetAssociatedGameIDs(List<RailGameID> game_ids);

		// Token: 0x06001884 RID: 6276
		RailResult GetLocalVersion(out string version);
	}
}
