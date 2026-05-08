using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200002A RID: 42
	public class IRailSpaceWorkImpl : RailObject, IRailSpaceWork, IRailComponent
	{
		// Token: 0x0600129C RID: 4764 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailSpaceWorkImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000067CC File Offset: 0x000049CC
		~IRailSpaceWorkImpl()
		{
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000067F4 File Offset: 0x000049F4
		public virtual void Close()
		{
			RAIL_API_PINVOKE.IRailSpaceWork_Close(this.swigCPtr_);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00006804 File Offset: 0x00004A04
		public virtual SpaceWorkID GetSpaceWorkID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailSpaceWork_GetSpaceWorkID(this.swigCPtr_);
			SpaceWorkID spaceWorkID = new SpaceWorkID();
			RailConverter.Cpp2Csharp(intPtr, spaceWorkID);
			return spaceWorkID;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00006829 File Offset: 0x00004A29
		public virtual bool Editable()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_Editable(this.swigCPtr_);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00006836 File Offset: 0x00004A36
		public virtual RailResult StartSync(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_StartSync(this.swigCPtr_, user_data);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00006844 File Offset: 0x00004A44
		public virtual RailResult GetSyncProgress(RailSpaceWorkSyncProgress progress)
		{
			IntPtr intPtr = ((progress == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkSyncProgress__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetSyncProgress(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (progress != null)
				{
					RailConverter.Cpp2Csharp(intPtr, progress);
				}
				RAIL_API_PINVOKE.delete_RailSpaceWorkSyncProgress(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00006894 File Offset: 0x00004A94
		public virtual RailResult CancelSync()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_CancelSync(this.swigCPtr_);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000068A4 File Offset: 0x00004AA4
		public virtual RailResult GetWorkLocalFolder(out string path)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetWorkLocalFolder(this.swigCPtr_, intPtr);
			}
			finally
			{
				path = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000068E8 File Offset: 0x00004AE8
		public virtual RailResult AsyncUpdateMetadata(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_AsyncUpdateMetadata(this.swigCPtr_, user_data);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000068F8 File Offset: 0x00004AF8
		public virtual RailResult GetName(out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetName(this.swigCPtr_, intPtr);
			}
			finally
			{
				name = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0000693C File Offset: 0x00004B3C
		public virtual RailResult GetDescription(out string description)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetDescription(this.swigCPtr_, intPtr);
			}
			finally
			{
				description = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00006980 File Offset: 0x00004B80
		public virtual RailResult GetUrl(out string url)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetUrl(this.swigCPtr_, intPtr);
			}
			finally
			{
				url = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000069C4 File Offset: 0x00004BC4
		public virtual uint GetCreateTime()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetCreateTime(this.swigCPtr_);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000069D1 File Offset: 0x00004BD1
		public virtual uint GetLastUpdateTime()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetLastUpdateTime(this.swigCPtr_);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000069DE File Offset: 0x00004BDE
		public virtual ulong GetWorkFileSize()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetWorkFileSize(this.swigCPtr_);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000069EC File Offset: 0x00004BEC
		public virtual RailResult GetTags(List<string> tags)
		{
			IntPtr intPtr = ((tags == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetTags(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (tags != null)
				{
					RailConverter.Cpp2Csharp(intPtr, tags);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00006A3C File Offset: 0x00004C3C
		public virtual RailResult GetPreviewImage(out string path)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetPreviewImage(this.swigCPtr_, intPtr);
			}
			finally
			{
				path = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00006A80 File Offset: 0x00004C80
		public virtual RailResult GetVersion(out string version)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetVersion(this.swigCPtr_, intPtr);
			}
			finally
			{
				version = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public virtual ulong GetDownloadCount()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetDownloadCount(this.swigCPtr_);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00006AD1 File Offset: 0x00004CD1
		public virtual ulong GetSubscribedCount()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetSubscribedCount(this.swigCPtr_);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00006ADE File Offset: 0x00004CDE
		public virtual EnumRailSpaceWorkShareLevel GetShareLevel()
		{
			return (EnumRailSpaceWorkShareLevel)RAIL_API_PINVOKE.IRailSpaceWork_GetShareLevel(this.swigCPtr_);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00006AEB File Offset: 0x00004CEB
		public virtual ulong GetScore()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetScore(this.swigCPtr_);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00006AF8 File Offset: 0x00004CF8
		public virtual RailResult GetMetadata(string key, out string value)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetMetadata(this.swigCPtr_, key, intPtr);
			}
			finally
			{
				value = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00006B3C File Offset: 0x00004D3C
		public virtual EnumRailSpaceWorkVoteValue GetMyVote()
		{
			return (EnumRailSpaceWorkVoteValue)RAIL_API_PINVOKE.IRailSpaceWork_GetMyVote(this.swigCPtr_);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00006B49 File Offset: 0x00004D49
		public virtual bool IsFavorite()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_IsFavorite(this.swigCPtr_);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00006B56 File Offset: 0x00004D56
		public virtual bool IsSubscribed()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_IsSubscribed(this.swigCPtr_);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00006B63 File Offset: 0x00004D63
		public virtual RailResult SetName(string name)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetName(this.swigCPtr_, name);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00006B71 File Offset: 0x00004D71
		public virtual RailResult SetDescription(string description)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetDescription(this.swigCPtr_, description);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00006B80 File Offset: 0x00004D80
		public virtual RailResult SetTags(List<string> tags)
		{
			IntPtr intPtr = ((tags == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (tags != null)
			{
				RailConverter.Csharp2Cpp(tags, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetTags(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00006BD0 File Offset: 0x00004DD0
		public virtual RailResult SetPreviewImage(string path_filename)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetPreviewImage(this.swigCPtr_, path_filename);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00006BDE File Offset: 0x00004DDE
		public virtual RailResult SetVersion(string version)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetVersion(this.swigCPtr_, version);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00006BEC File Offset: 0x00004DEC
		public virtual RailResult SetShareLevel(EnumRailSpaceWorkShareLevel level)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetShareLevel__SWIG_0(this.swigCPtr_, (int)level);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00006BFA File Offset: 0x00004DFA
		public virtual RailResult SetShareLevel()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetShareLevel__SWIG_1(this.swigCPtr_);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00006C07 File Offset: 0x00004E07
		public virtual RailResult SetMetadata(string key, string value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetMetadata(this.swigCPtr_, key, value);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00006C16 File Offset: 0x00004E16
		public virtual RailResult SetContentFromFolder(string path)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetContentFromFolder(this.swigCPtr_, path);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00006C24 File Offset: 0x00004E24
		public virtual RailResult GetAllMetadata(List<RailKeyValue> metadata)
		{
			IntPtr intPtr = ((metadata == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetAllMetadata(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (metadata != null)
				{
					RailConverter.Cpp2Csharp(intPtr, metadata);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00006C74 File Offset: 0x00004E74
		public virtual RailResult GetAdditionalPreviewUrls(List<string> preview_urls)
		{
			IntPtr intPtr = ((preview_urls == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetAdditionalPreviewUrls(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (preview_urls != null)
				{
					RailConverter.Cpp2Csharp(intPtr, preview_urls);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00006CC4 File Offset: 0x00004EC4
		public virtual RailResult GetAssociatedSpaceWorks(List<SpaceWorkID> ids)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetAssociatedSpaceWorks(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, ids);
				}
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00006D14 File Offset: 0x00004F14
		public virtual RailResult GetLanguages(List<string> languages)
		{
			IntPtr intPtr = ((languages == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetLanguages(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (languages != null)
				{
					RailConverter.Cpp2Csharp(intPtr, languages);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00006D64 File Offset: 0x00004F64
		public virtual RailResult RemoveMetadata(string key)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_RemoveMetadata(this.swigCPtr_, key);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00006D74 File Offset: 0x00004F74
		public virtual RailResult SetAdditionalPreviews(List<string> local_paths)
		{
			IntPtr intPtr = ((local_paths == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (local_paths != null)
			{
				RailConverter.Csharp2Cpp(local_paths, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetAdditionalPreviews(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public virtual RailResult SetAssociatedSpaceWorks(List<SpaceWorkID> ids)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (ids != null)
			{
				RailConverter.Csharp2Cpp(ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetAssociatedSpaceWorks(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00006E14 File Offset: 0x00005014
		public virtual RailResult SetLanguages(List<string> languages)
		{
			IntPtr intPtr = ((languages == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (languages != null)
			{
				RailConverter.Csharp2Cpp(languages, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetLanguages(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00006E64 File Offset: 0x00005064
		public virtual RailResult GetPreviewUrl(out string url)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetPreviewUrl(this.swigCPtr_, intPtr);
			}
			finally
			{
				url = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00006EA8 File Offset: 0x000050A8
		public virtual RailResult GetVoteDetail(List<RailSpaceWorkVoteDetail> vote_details)
		{
			IntPtr intPtr = ((vote_details == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailSpaceWorkVoteDetail__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetVoteDetail(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (vote_details != null)
				{
					RailConverter.Cpp2Csharp(intPtr, vote_details);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailSpaceWorkVoteDetail(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00006EF8 File Offset: 0x000050F8
		public virtual RailResult GetUploaderIDs(List<RailID> uploader_ids)
		{
			IntPtr intPtr = ((uploader_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetUploaderIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (uploader_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, uploader_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00006F48 File Offset: 0x00005148
		public virtual RailResult SetUpdateOptions(RailSpaceWorkUpdateOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkUpdateOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetUpdateOptions(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkUpdateOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00006F98 File Offset: 0x00005198
		public virtual RailResult GetStatistic(EnumRailSpaceWorkStatistic stat_type, out ulong value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetStatistic(this.swigCPtr_, (int)stat_type, out value);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00006FA7 File Offset: 0x000051A7
		public virtual RailResult RemovePreviewImage()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_RemovePreviewImage(this.swigCPtr_);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00006FB4 File Offset: 0x000051B4
		public virtual uint GetState()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetState(this.swigCPtr_);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00006FC4 File Offset: 0x000051C4
		public virtual RailResult AddAssociatedGameIDs(List<RailGameID> game_ids)
		{
			IntPtr intPtr = ((game_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailGameID__SWIG_0());
			if (game_ids != null)
			{
				RailConverter.Csharp2Cpp(game_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_AddAssociatedGameIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailGameID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00007014 File Offset: 0x00005214
		public virtual RailResult RemoveAssociatedGameIDs(List<RailGameID> game_ids)
		{
			IntPtr intPtr = ((game_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailGameID__SWIG_0());
			if (game_ids != null)
			{
				RailConverter.Csharp2Cpp(game_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_RemoveAssociatedGameIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailGameID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00007064 File Offset: 0x00005264
		public virtual RailResult GetAssociatedGameIDs(List<RailGameID> game_ids)
		{
			IntPtr intPtr = ((game_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailGameID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetAssociatedGameIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (game_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, game_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailGameID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x000070B4 File Offset: 0x000052B4
		public virtual RailResult GetLocalVersion(out string version)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetLocalVersion(this.swigCPtr_, intPtr);
			}
			finally
			{
				version = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
