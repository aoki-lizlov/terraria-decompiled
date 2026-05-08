using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000170 RID: 368
	public interface IRailUserSpaceHelper
	{
		// Token: 0x06001885 RID: 6277
		RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options, string user_data);

		// Token: 0x06001886 RID: 6278
		RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options);

		// Token: 0x06001887 RID: 6279
		RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type);

		// Token: 0x06001888 RID: 6280
		RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options, string user_data);

		// Token: 0x06001889 RID: 6281
		RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options);

		// Token: 0x0600188A RID: 6282
		RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type);

		// Token: 0x0600188B RID: 6283
		RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, RailQueryWorkFileOptions options, string user_data);

		// Token: 0x0600188C RID: 6284
		RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, RailQueryWorkFileOptions options);

		// Token: 0x0600188D RID: 6285
		RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by);

		// Token: 0x0600188E RID: 6286
		RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works);

		// Token: 0x0600188F RID: 6287
		RailResult AsyncSubscribeSpaceWorks(List<SpaceWorkID> ids, bool subscribe, string user_data);

		// Token: 0x06001890 RID: 6288
		IRailSpaceWork OpenSpaceWork(SpaceWorkID id);

		// Token: 0x06001891 RID: 6289
		IRailSpaceWork CreateSpaceWork(EnumRailSpaceWorkType type);

		// Token: 0x06001892 RID: 6290
		RailResult GetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, QueryMySubscribedSpaceWorksResult result);

		// Token: 0x06001893 RID: 6291
		uint GetMySubscribedWorksCount(EnumRailSpaceWorkType type, out RailResult result);

		// Token: 0x06001894 RID: 6292
		RailResult AsyncRemoveSpaceWork(SpaceWorkID id, string user_data);

		// Token: 0x06001895 RID: 6293
		RailResult AsyncModifyFavoritesWorks(List<SpaceWorkID> ids, EnumRailModifyFavoritesSpaceWorkType modify_flag, string user_data);

		// Token: 0x06001896 RID: 6294
		RailResult AsyncVoteSpaceWork(SpaceWorkID id, EnumRailSpaceWorkVoteValue vote, string user_data);

		// Token: 0x06001897 RID: 6295
		RailResult AsyncSearchSpaceWork(RailSpaceWorkSearchFilter filter, RailQueryWorkFileOptions options, List<EnumRailSpaceWorkType> types, uint offset, uint max_works, string user_data);
	}
}
