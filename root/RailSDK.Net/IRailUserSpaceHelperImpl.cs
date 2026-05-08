using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000031 RID: 49
	public class IRailUserSpaceHelperImpl : RailObject, IRailUserSpaceHelper
	{
		// Token: 0x06001310 RID: 4880 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailUserSpaceHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000079C8 File Offset: 0x00005BC8
		~IRailUserSpaceHelperImpl()
		{
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000079F0 File Offset: 0x00005BF0
		public virtual RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_0(this.swigCPtr_, offset, max_works, (int)type, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00007A48 File Offset: 0x00005C48
		public virtual RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_1(this.swigCPtr_, offset, max_works, (int)type, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public virtual RailResult AsyncGetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_2(this.swigCPtr_, offset, max_works, (int)type);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public virtual RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_0(this.swigCPtr_, offset, max_works, (int)type, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00007B08 File Offset: 0x00005D08
		public virtual RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, RailQueryWorkFileOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_1(this.swigCPtr_, offset, max_works, (int)type, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00007B60 File Offset: 0x00005D60
		public virtual RailResult AsyncGetMyFavoritesWorks(uint offset, uint max_works, EnumRailSpaceWorkType type)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_2(this.swigCPtr_, offset, max_works, (int)type);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00007B70 File Offset: 0x00005D70
		public virtual RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, RailQueryWorkFileOptions options, string user_data)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			IntPtr intPtr2 = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_0(this.swigCPtr_, intPtr, offset, max_works, (int)order_by, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(intPtr);
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00007BEC File Offset: 0x00005DEC
		public virtual RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by, RailQueryWorkFileOptions options)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			IntPtr intPtr2 = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_1(this.swigCPtr_, intPtr, offset, max_works, (int)order_by, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(intPtr);
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00007C64 File Offset: 0x00005E64
		public virtual RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works, EnumRailSpaceWorkOrderBy order_by)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_2(this.swigCPtr_, intPtr, offset, max_works, (int)order_by);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public virtual RailResult AsyncQuerySpaceWorks(RailSpaceWorkFilter filter, uint offset, uint max_works)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_3(this.swigCPtr_, intPtr, offset, max_works);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkFilter(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00007D0C File Offset: 0x00005F0C
		public virtual RailResult AsyncSubscribeSpaceWorks(List<SpaceWorkID> ids, bool subscribe, string user_data)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (ids != null)
			{
				RailConverter.Csharp2Cpp(ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncSubscribeSpaceWorks(this.swigCPtr_, intPtr, subscribe, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00007D60 File Offset: 0x00005F60
		public virtual IRailSpaceWork OpenSpaceWork(SpaceWorkID id)
		{
			IntPtr intPtr = ((id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (id != null)
			{
				RailConverter.Csharp2Cpp(id, intPtr);
			}
			IRailSpaceWork railSpaceWork;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailUserSpaceHelper_OpenSpaceWork(this.swigCPtr_, intPtr);
				railSpaceWork = ((intPtr2 == IntPtr.Zero) ? null : new IRailSpaceWorkImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railSpaceWork;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public virtual IRailSpaceWork CreateSpaceWork(EnumRailSpaceWorkType type)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailUserSpaceHelper_CreateSpaceWork(this.swigCPtr_, (int)type);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailSpaceWorkImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00007E04 File Offset: 0x00006004
		public virtual RailResult GetMySubscribedWorks(uint offset, uint max_works, EnumRailSpaceWorkType type, QueryMySubscribedSpaceWorksResult result)
		{
			IntPtr intPtr = ((result == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_QueryMySubscribedSpaceWorksResult__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_GetMySubscribedWorks(this.swigCPtr_, offset, max_works, (int)type, intPtr);
			}
			finally
			{
				if (result != null)
				{
					RailConverter.Cpp2Csharp(intPtr, result);
				}
				RAIL_API_PINVOKE.delete_QueryMySubscribedSpaceWorksResult(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00007E58 File Offset: 0x00006058
		public virtual uint GetMySubscribedWorksCount(EnumRailSpaceWorkType type, out RailResult result)
		{
			return RAIL_API_PINVOKE.IRailUserSpaceHelper_GetMySubscribedWorksCount(this.swigCPtr_, (int)type, out result);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00007E68 File Offset: 0x00006068
		public virtual RailResult AsyncRemoveSpaceWork(SpaceWorkID id, string user_data)
		{
			IntPtr intPtr = ((id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (id != null)
			{
				RailConverter.Csharp2Cpp(id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncRemoveSpaceWork(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00007EC4 File Offset: 0x000060C4
		public virtual RailResult AsyncModifyFavoritesWorks(List<SpaceWorkID> ids, EnumRailModifyFavoritesSpaceWorkType modify_flag, string user_data)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (ids != null)
			{
				RailConverter.Csharp2Cpp(ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncModifyFavoritesWorks(this.swigCPtr_, intPtr, (int)modify_flag, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00007F18 File Offset: 0x00006118
		public virtual RailResult AsyncVoteSpaceWork(SpaceWorkID id, EnumRailSpaceWorkVoteValue vote, string user_data)
		{
			IntPtr intPtr = ((id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (id != null)
			{
				RailConverter.Csharp2Cpp(id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncVoteSpaceWork(this.swigCPtr_, intPtr, (int)vote, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00007F78 File Offset: 0x00006178
		public virtual RailResult AsyncSearchSpaceWork(RailSpaceWorkSearchFilter filter, RailQueryWorkFileOptions options, List<EnumRailSpaceWorkType> types, uint offset, uint max_works, string user_data)
		{
			IntPtr intPtr = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkSearchFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr);
			}
			IntPtr intPtr2 = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailQueryWorkFileOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr2);
			}
			IntPtr intPtr3 = ((types == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayEnumRailSpaceWorkType__SWIG_0());
			if (types != null)
			{
				RailConverter.Csharp2Cpp(types, intPtr3);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUserSpaceHelper_AsyncSearchSpaceWork(this.swigCPtr_, intPtr, intPtr2, intPtr3, offset, max_works, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkSearchFilter(intPtr);
				RAIL_API_PINVOKE.delete_RailQueryWorkFileOptions(intPtr2);
				RAIL_API_PINVOKE.delete_RailArrayEnumRailSpaceWorkType(intPtr3);
			}
			return railResult;
		}
	}
}
