using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200003C RID: 60
	public class RailConverter
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x00008CD8 File Offset: 0x00006ED8
		public static void Cpp2Csharp(IntPtr ptr, AcquireSessionTicketResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AcquireSessionTicketResponse_session_ticket_get(ptr), ret.session_ticket);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00008CF2 File Offset: 0x00006EF2
		public static void Csharp2Cpp(AcquireSessionTicketResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.session_ticket, RAIL_API_PINVOKE.AcquireSessionTicketResponse_session_ticket_get(ptr));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00008D0C File Offset: 0x00006F0C
		public static void Cpp2Csharp(IntPtr ptr, AsyncAcquireGameServerSessionTicketResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncAcquireGameServerSessionTicketResponse_session_ticket_get(ptr), ret.session_ticket);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00008D26 File Offset: 0x00006F26
		public static void Csharp2Cpp(AsyncAcquireGameServerSessionTicketResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.session_ticket, RAIL_API_PINVOKE.AsyncAcquireGameServerSessionTicketResponse_session_ticket_get(ptr));
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00008D40 File Offset: 0x00006F40
		public static void Cpp2Csharp(IntPtr ptr, AsyncAddFavoriteGameServerResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncAddFavoriteGameServerResult_server_id_get(ptr), ret.server_id);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00008D5A File Offset: 0x00006F5A
		public static void Csharp2Cpp(AsyncAddFavoriteGameServerResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.server_id, RAIL_API_PINVOKE.AsyncAddFavoriteGameServerResult_server_id_get(ptr));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00008D74 File Offset: 0x00006F74
		public static void Cpp2Csharp(IntPtr ptr, AsyncDeleteStreamFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.filename = RAIL_API_PINVOKE.AsyncDeleteStreamFileResult_filename_get(ptr);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00008D89 File Offset: 0x00006F89
		public static void Csharp2Cpp(AsyncDeleteStreamFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncDeleteStreamFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00008D9E File Offset: 0x00006F9E
		public static void Cpp2Csharp(IntPtr ptr, AsyncGetFavoriteGameServersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetFavoriteGameServersResult_server_id_array_get(ptr), ret.server_id_array);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public static void Csharp2Cpp(AsyncGetFavoriteGameServersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.server_id_array, RAIL_API_PINVOKE.AsyncGetFavoriteGameServersResult_server_id_array_get(ptr));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00008DD2 File Offset: 0x00006FD2
		public static void Cpp2Csharp(IntPtr ptr, AsyncGetMyFavoritesWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_available_works = RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_total_available_works_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00008DF8 File Offset: 0x00006FF8
		public static void Csharp2Cpp(AsyncGetMyFavoritesWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_total_available_works_set(ptr, data.total_available_works);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00008E1E File Offset: 0x0000701E
		public static void Cpp2Csharp(IntPtr ptr, AsyncGetMySubscribedWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_available_works = RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_total_available_works_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00008E44 File Offset: 0x00007044
		public static void Csharp2Cpp(AsyncGetMySubscribedWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_total_available_works_set(ptr, data.total_available_works);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00008E6A File Offset: 0x0000706A
		public static void Cpp2Csharp(IntPtr ptr, AsyncListFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncListFileResult_file_list_get(ptr), ret.file_list);
			ret.try_list_file_num = RAIL_API_PINVOKE.AsyncListFileResult_try_list_file_num_get(ptr);
			ret.all_file_num = RAIL_API_PINVOKE.AsyncListFileResult_all_file_num_get(ptr);
			ret.start_index = RAIL_API_PINVOKE.AsyncListFileResult_start_index_get(ptr);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00008EA8 File Offset: 0x000070A8
		public static void Csharp2Cpp(AsyncListFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.file_list, RAIL_API_PINVOKE.AsyncListFileResult_file_list_get(ptr));
			RAIL_API_PINVOKE.AsyncListFileResult_try_list_file_num_set(ptr, data.try_list_file_num);
			RAIL_API_PINVOKE.AsyncListFileResult_all_file_num_set(ptr, data.all_file_num);
			RAIL_API_PINVOKE.AsyncListFileResult_start_index_set(ptr, data.start_index);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00008EE6 File Offset: 0x000070E6
		public static void Cpp2Csharp(IntPtr ptr, AsyncModifyFavoritesWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_success_ids_get(ptr), ret.success_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_failure_ids_get(ptr), ret.failure_ids);
			ret.modify_flag = (EnumRailModifyFavoritesSpaceWorkType)RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_modify_flag_get(ptr);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00008F1D File Offset: 0x0000711D
		public static void Csharp2Cpp(AsyncModifyFavoritesWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_success_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.failure_ids, RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_failure_ids_get(ptr));
			RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_modify_flag_set(ptr, (int)data.modify_flag);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00008F54 File Offset: 0x00007154
		public static void Cpp2Csharp(IntPtr ptr, AsyncQueryQuotaResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.available_quota = RAIL_API_PINVOKE.AsyncQueryQuotaResult_available_quota_get(ptr);
			ret.total_quota = RAIL_API_PINVOKE.AsyncQueryQuotaResult_total_quota_get(ptr);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00008F75 File Offset: 0x00007175
		public static void Csharp2Cpp(AsyncQueryQuotaResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncQueryQuotaResult_available_quota_set(ptr, data.available_quota);
			RAIL_API_PINVOKE.AsyncQueryQuotaResult_total_quota_set(ptr, data.total_quota);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00008F96 File Offset: 0x00007196
		public static void Cpp2Csharp(IntPtr ptr, AsyncQuerySpaceWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_available_works = RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_total_available_works_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00008FBC File Offset: 0x000071BC
		public static void Csharp2Cpp(AsyncQuerySpaceWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00008FE2 File Offset: 0x000071E2
		public static void Cpp2Csharp(IntPtr ptr, AsyncReadFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.try_read_length = RAIL_API_PINVOKE.AsyncReadFileResult_try_read_length_get(ptr);
			ret.offset = RAIL_API_PINVOKE.AsyncReadFileResult_offset_get(ptr);
			ret.data = RAIL_API_PINVOKE.AsyncReadFileResult_data_get(ptr);
			ret.filename = RAIL_API_PINVOKE.AsyncReadFileResult_filename_get(ptr);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0000901B File Offset: 0x0000721B
		public static void Csharp2Cpp(AsyncReadFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncReadFileResult_try_read_length_set(ptr, data.try_read_length);
			RAIL_API_PINVOKE.AsyncReadFileResult_offset_set(ptr, data.offset);
			RAIL_API_PINVOKE.AsyncReadFileResult_data_set(ptr, data.data);
			RAIL_API_PINVOKE.AsyncReadFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00009054 File Offset: 0x00007254
		public static void Cpp2Csharp(IntPtr ptr, AsyncReadStreamFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.try_read_length = RAIL_API_PINVOKE.AsyncReadStreamFileResult_try_read_length_get(ptr);
			ret.offset = RAIL_API_PINVOKE.AsyncReadStreamFileResult_offset_get(ptr);
			ret.data = RAIL_API_PINVOKE.AsyncReadStreamFileResult_data_get(ptr);
			ret.filename = RAIL_API_PINVOKE.AsyncReadStreamFileResult_filename_get(ptr);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0000908D File Offset: 0x0000728D
		public static void Csharp2Cpp(AsyncReadStreamFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncReadStreamFileResult_try_read_length_set(ptr, data.try_read_length);
			RAIL_API_PINVOKE.AsyncReadStreamFileResult_offset_set(ptr, data.offset);
			RAIL_API_PINVOKE.AsyncReadStreamFileResult_data_set(ptr, data.data);
			RAIL_API_PINVOKE.AsyncReadStreamFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000090C6 File Offset: 0x000072C6
		public static void Cpp2Csharp(IntPtr ptr, AsyncRemoveFavoriteGameServerResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncRemoveFavoriteGameServerResult_server_id_get(ptr), ret.server_id);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000090E0 File Offset: 0x000072E0
		public static void Csharp2Cpp(AsyncRemoveFavoriteGameServerResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.server_id, RAIL_API_PINVOKE.AsyncRemoveFavoriteGameServerResult_server_id_get(ptr));
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000090FA File Offset: 0x000072FA
		public static void Cpp2Csharp(IntPtr ptr, AsyncRemoveSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncRemoveSpaceWorkResult_id_get(ptr), ret.id);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00009114 File Offset: 0x00007314
		public static void Csharp2Cpp(AsyncRemoveSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncRemoveSpaceWorkResult_id_get(ptr));
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0000912E File Offset: 0x0000732E
		public static void Cpp2Csharp(IntPtr ptr, AsyncRenameStreamFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.old_filename = RAIL_API_PINVOKE.AsyncRenameStreamFileResult_old_filename_get(ptr);
			ret.new_filename = RAIL_API_PINVOKE.AsyncRenameStreamFileResult_new_filename_get(ptr);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0000914F File Offset: 0x0000734F
		public static void Csharp2Cpp(AsyncRenameStreamFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncRenameStreamFileResult_old_filename_set(ptr, data.old_filename);
			RAIL_API_PINVOKE.AsyncRenameStreamFileResult_new_filename_set(ptr, data.new_filename);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00009170 File Offset: 0x00007370
		public static void Cpp2Csharp(IntPtr ptr, AsyncSearchSpaceWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_available_works = RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_total_available_works_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00009196 File Offset: 0x00007396
		public static void Csharp2Cpp(AsyncSearchSpaceWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000091BC File Offset: 0x000073BC
		public static void Cpp2Csharp(IntPtr ptr, AsyncSubscribeSpaceWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_success_ids_get(ptr), ret.success_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_failure_ids_get(ptr), ret.failure_ids);
			ret.subscribe = RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_subscribe_get(ptr);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000091F3 File Offset: 0x000073F3
		public static void Csharp2Cpp(AsyncSubscribeSpaceWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_success_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.failure_ids, RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_failure_ids_get(ptr));
			RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_subscribe_set(ptr, data.subscribe);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0000922A File Offset: 0x0000742A
		public static void Cpp2Csharp(IntPtr ptr, AsyncUpdateMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.type = (EnumRailSpaceWorkType)RAIL_API_PINVOKE.AsyncUpdateMetadataResult_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncUpdateMetadataResult_id_get(ptr), ret.id);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00009250 File Offset: 0x00007450
		public static void Csharp2Cpp(AsyncUpdateMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncUpdateMetadataResult_type_set(ptr, (int)data.type);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncUpdateMetadataResult_id_get(ptr));
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00009276 File Offset: 0x00007476
		public static void Cpp2Csharp(IntPtr ptr, AsyncVoteSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncVoteSpaceWorkResult_id_get(ptr), ret.id);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00009290 File Offset: 0x00007490
		public static void Csharp2Cpp(AsyncVoteSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncVoteSpaceWorkResult_id_get(ptr));
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000092AA File Offset: 0x000074AA
		public static void Cpp2Csharp(IntPtr ptr, AsyncWriteFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.written_length = RAIL_API_PINVOKE.AsyncWriteFileResult_written_length_get(ptr);
			ret.offset = RAIL_API_PINVOKE.AsyncWriteFileResult_offset_get(ptr);
			ret.try_write_length = RAIL_API_PINVOKE.AsyncWriteFileResult_try_write_length_get(ptr);
			ret.filename = RAIL_API_PINVOKE.AsyncWriteFileResult_filename_get(ptr);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000092E3 File Offset: 0x000074E3
		public static void Csharp2Cpp(AsyncWriteFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncWriteFileResult_written_length_set(ptr, data.written_length);
			RAIL_API_PINVOKE.AsyncWriteFileResult_offset_set(ptr, data.offset);
			RAIL_API_PINVOKE.AsyncWriteFileResult_try_write_length_set(ptr, data.try_write_length);
			RAIL_API_PINVOKE.AsyncWriteFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0000931C File Offset: 0x0000751C
		public static void Cpp2Csharp(IntPtr ptr, AsyncWriteStreamFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.written_length = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_written_length_get(ptr);
			ret.offset = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_offset_get(ptr);
			ret.try_write_length = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_try_write_length_get(ptr);
			ret.filename = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_filename_get(ptr);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00009355 File Offset: 0x00007555
		public static void Csharp2Cpp(AsyncWriteStreamFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncWriteStreamFileResult_written_length_set(ptr, data.written_length);
			RAIL_API_PINVOKE.AsyncWriteStreamFileResult_offset_set(ptr, data.offset);
			RAIL_API_PINVOKE.AsyncWriteStreamFileResult_try_write_length_set(ptr, data.try_write_length);
			RAIL_API_PINVOKE.AsyncWriteStreamFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00009390 File Offset: 0x00007590
		public static void Cpp2Csharp(IntPtr ptr, BrowserDamageRectNeedsPaintRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.update_bgra_height = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_height_get(ptr);
			ret.scroll_x_pos = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_x_pos_get(ptr);
			ret.bgra_data = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_data_get(ptr);
			ret.update_bgra_width = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_width_get(ptr);
			ret.page_scale_factor = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_page_scale_factor_get(ptr);
			ret.update_offset_y = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_y_get(ptr);
			ret.update_offset_x = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_x_get(ptr);
			ret.offset_x = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_x_get(ptr);
			ret.offset_y = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_y_get(ptr);
			ret.bgra_height = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_height_get(ptr);
			ret.scroll_y_pos = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_y_pos_get(ptr);
			ret.bgra_width = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_width_get(ptr);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00009434 File Offset: 0x00007634
		public static void Csharp2Cpp(BrowserDamageRectNeedsPaintRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_height_set(ptr, data.update_bgra_height);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_x_pos_set(ptr, data.scroll_x_pos);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_data_set(ptr, data.bgra_data);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_width_set(ptr, data.update_bgra_width);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_page_scale_factor_set(ptr, data.page_scale_factor);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_y_set(ptr, data.update_offset_y);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_x_set(ptr, data.update_offset_x);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_x_set(ptr, data.offset_x);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_y_set(ptr, data.offset_y);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_height_set(ptr, data.bgra_height);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_y_pos_set(ptr, data.scroll_y_pos);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_width_set(ptr, data.bgra_width);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000094D8 File Offset: 0x000076D8
		public static void Cpp2Csharp(IntPtr ptr, BrowserNeedsPaintRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.bgra_width = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_width_get(ptr);
			ret.scroll_y_pos = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_y_pos_get(ptr);
			ret.bgra_data = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_data_get(ptr);
			ret.page_scale_factor = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_page_scale_factor_get(ptr);
			ret.offset_x = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_x_get(ptr);
			ret.scroll_x_pos = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_x_pos_get(ptr);
			ret.bgra_height = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_height_get(ptr);
			ret.offset_y = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_y_get(ptr);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0000954C File Offset: 0x0000774C
		public static void Csharp2Cpp(BrowserNeedsPaintRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_width_set(ptr, data.bgra_width);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_y_pos_set(ptr, data.scroll_y_pos);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_data_set(ptr, data.bgra_data);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_page_scale_factor_set(ptr, data.page_scale_factor);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_x_set(ptr, data.offset_x);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_x_pos_set(ptr, data.scroll_x_pos);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_height_set(ptr, data.bgra_height);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_y_set(ptr, data.offset_y);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x000095C0 File Offset: 0x000077C0
		public static void Cpp2Csharp(IntPtr ptr, BrowserRenderNavigateResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.url = RAIL_API_PINVOKE.BrowserRenderNavigateResult_url_get(ptr);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000095D5 File Offset: 0x000077D5
		public static void Csharp2Cpp(BrowserRenderNavigateResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserRenderNavigateResult_url_set(ptr, data.url);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x000095EA File Offset: 0x000077EA
		public static void Cpp2Csharp(IntPtr ptr, BrowserRenderStateChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.can_go_back = RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_back_get(ptr);
			ret.can_go_forward = RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_forward_get(ptr);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0000960B File Offset: 0x0000780B
		public static void Csharp2Cpp(BrowserRenderStateChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_back_set(ptr, data.can_go_back);
			RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_forward_set(ptr, data.can_go_forward);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0000962C File Offset: 0x0000782C
		public static void Cpp2Csharp(IntPtr ptr, BrowserRenderTitleChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.new_title = RAIL_API_PINVOKE.BrowserRenderTitleChanged_new_title_get(ptr);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00009641 File Offset: 0x00007841
		public static void Csharp2Cpp(BrowserRenderTitleChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserRenderTitleChanged_new_title_set(ptr, data.new_title);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00009656 File Offset: 0x00007856
		public static void Cpp2Csharp(IntPtr ptr, BrowserTryNavigateNewPageRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.url = RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_url_get(ptr);
			ret.target_type = RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_target_type_get(ptr);
			ret.is_redirect_request = RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_is_redirect_request_get(ptr);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00009683 File Offset: 0x00007883
		public static void Csharp2Cpp(BrowserTryNavigateNewPageRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_url_set(ptr, data.url);
			RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_target_type_set(ptr, data.target_type);
			RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_is_redirect_request_set(ptr, data.is_redirect_request);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, CheckAllDlcsStateReadyResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(CheckAllDlcsStateReadyResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000096C2 File Offset: 0x000078C2
		public static void Cpp2Csharp(IntPtr ptr, ClearRoomMetadataInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.ClearRoomMetadataInfo_room_id_get(ptr);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000096D7 File Offset: 0x000078D7
		public static void Csharp2Cpp(ClearRoomMetadataInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.ClearRoomMetadataInfo_room_id_set(ptr, data.room_id);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, CloseBrowserResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(CloseBrowserResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000096EC File Offset: 0x000078EC
		public static void Cpp2Csharp(IntPtr ptr, CompleteConsumeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CompleteConsumeAssetsFinished_asset_item_get(ptr), ret.asset_item);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00009706 File Offset: 0x00007906
		public static void Csharp2Cpp(CompleteConsumeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.asset_item, RAIL_API_PINVOKE.CompleteConsumeAssetsFinished_asset_item_get(ptr));
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, CompleteConsumeByExchangeAssetsToFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(CompleteConsumeByExchangeAssetsToFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00009720 File Offset: 0x00007920
		public static void Cpp2Csharp(IntPtr ptr, CreateBrowserOptions ret)
		{
			ret.has_minimum_button = RAIL_API_PINVOKE.CreateBrowserOptions_has_minimum_button_get(ptr);
			ret.has_border = RAIL_API_PINVOKE.CreateBrowserOptions_has_border_get(ptr);
			ret.is_movable = RAIL_API_PINVOKE.CreateBrowserOptions_is_movable_get(ptr);
			ret.has_maximum_button = RAIL_API_PINVOKE.CreateBrowserOptions_has_maximum_button_get(ptr);
			ret.margin_left = RAIL_API_PINVOKE.CreateBrowserOptions_margin_left_get(ptr);
			ret.margin_top = RAIL_API_PINVOKE.CreateBrowserOptions_margin_top_get(ptr);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00009778 File Offset: 0x00007978
		public static void Csharp2Cpp(CreateBrowserOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.CreateBrowserOptions_has_minimum_button_set(ptr, data.has_minimum_button);
			RAIL_API_PINVOKE.CreateBrowserOptions_has_border_set(ptr, data.has_border);
			RAIL_API_PINVOKE.CreateBrowserOptions_is_movable_set(ptr, data.is_movable);
			RAIL_API_PINVOKE.CreateBrowserOptions_has_maximum_button_set(ptr, data.has_maximum_button);
			RAIL_API_PINVOKE.CreateBrowserOptions_margin_left_set(ptr, data.margin_left);
			RAIL_API_PINVOKE.CreateBrowserOptions_margin_top_set(ptr, data.margin_top);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, CreateBrowserResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(CreateBrowserResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000097CD File Offset: 0x000079CD
		public static void Cpp2Csharp(IntPtr ptr, CreateCustomerDrawBrowserOptions ret)
		{
			ret.content_offset_x = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_x_get(ptr);
			ret.content_offset_y = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_y_get(ptr);
			ret.content_window_height = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_height_get(ptr);
			ret.has_scroll = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_has_scroll_get(ptr);
			ret.content_window_width = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_width_get(ptr);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0000980B File Offset: 0x00007A0B
		public static void Csharp2Cpp(CreateCustomerDrawBrowserOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_x_set(ptr, data.content_offset_x);
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_y_set(ptr, data.content_offset_y);
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_height_set(ptr, data.content_window_height);
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_has_scroll_set(ptr, data.has_scroll);
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_width_set(ptr, data.content_window_width);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00009849 File Offset: 0x00007A49
		public static void Cpp2Csharp(IntPtr ptr, CreateGameServerOptions ret)
		{
			ret.has_password = RAIL_API_PINVOKE.CreateGameServerOptions_has_password_get(ptr);
			ret.enable_team_voice = RAIL_API_PINVOKE.CreateGameServerOptions_enable_team_voice_get(ptr);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00009863 File Offset: 0x00007A63
		public static void Csharp2Cpp(CreateGameServerOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.CreateGameServerOptions_has_password_set(ptr, data.has_password);
			RAIL_API_PINVOKE.CreateGameServerOptions_enable_team_voice_set(ptr, data.enable_team_voice);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0000987D File Offset: 0x00007A7D
		public static void Cpp2Csharp(IntPtr ptr, CreateGameServerResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateGameServerResult_game_server_id_get(ptr), ret.game_server_id);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00009897 File Offset: 0x00007A97
		public static void Csharp2Cpp(CreateGameServerResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.CreateGameServerResult_game_server_id_get(ptr));
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x000098B1 File Offset: 0x00007AB1
		public static void Cpp2Csharp(IntPtr ptr, CreateRoomInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.CreateRoomInfo_room_id_get(ptr);
			ret.zone_id = RAIL_API_PINVOKE.CreateRoomInfo_zone_id_get(ptr);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x000098D2 File Offset: 0x00007AD2
		public static void Csharp2Cpp(CreateRoomInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.CreateRoomInfo_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.CreateRoomInfo_zone_id_set(ptr, data.zone_id);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000098F3 File Offset: 0x00007AF3
		public static void Cpp2Csharp(IntPtr ptr, CreateSessionFailed ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionFailed_local_peer_get(ptr), ret.local_peer);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionFailed_remote_peer_get(ptr), ret.remote_peer);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0000991E File Offset: 0x00007B1E
		public static void Csharp2Cpp(CreateSessionFailed data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.CreateSessionFailed_local_peer_get(ptr));
			RailConverter.Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.CreateSessionFailed_remote_peer_get(ptr));
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00009949 File Offset: 0x00007B49
		public static void Cpp2Csharp(IntPtr ptr, CreateSessionRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionRequest_local_peer_get(ptr), ret.local_peer);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionRequest_remote_peer_get(ptr), ret.remote_peer);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00009974 File Offset: 0x00007B74
		public static void Csharp2Cpp(CreateSessionRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.CreateSessionRequest_local_peer_get(ptr));
			RailConverter.Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.CreateSessionRequest_remote_peer_get(ptr));
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0000999F File Offset: 0x00007B9F
		public static void Cpp2Csharp(IntPtr ptr, CreateVoiceChannelOption ret)
		{
			ret.join_channel_after_created = RAIL_API_PINVOKE.CreateVoiceChannelOption_join_channel_after_created_get(ptr);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000099AD File Offset: 0x00007BAD
		public static void Csharp2Cpp(CreateVoiceChannelOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.CreateVoiceChannelOption_join_channel_after_created_set(ptr, data.join_channel_after_created);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x000099BB File Offset: 0x00007BBB
		public static void Cpp2Csharp(IntPtr ptr, CreateVoiceChannelResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateVoiceChannelResult_voice_channel_id_get(ptr), ret.voice_channel_id);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x000099D5 File Offset: 0x00007BD5
		public static void Csharp2Cpp(CreateVoiceChannelResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.CreateVoiceChannelResult_voice_channel_id_get(ptr));
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x000099EF File Offset: 0x00007BEF
		public static void Cpp2Csharp(IntPtr ptr, DirectConsumeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DirectConsumeAssetsFinished_assets_get(ptr), ret.assets);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00009A09 File Offset: 0x00007C09
		public static void Csharp2Cpp(DirectConsumeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.assets, RAIL_API_PINVOKE.DirectConsumeAssetsFinished_assets_get(ptr));
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00009A23 File Offset: 0x00007C23
		public static void Cpp2Csharp(IntPtr ptr, DlcInstallFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallFinished_dlc_id_get(ptr), ret.dlc_id);
			ret.result = (RailResult)RAIL_API_PINVOKE.DlcInstallFinished_result_get(ptr);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00009A49 File Offset: 0x00007C49
		public static void Csharp2Cpp(DlcInstallFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallFinished_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcInstallFinished_result_set(ptr, (int)data.result);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00009A6F File Offset: 0x00007C6F
		public static void Cpp2Csharp(IntPtr ptr, DlcInstallProgress ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallProgress_progress_get(ptr), ret.progress);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallProgress_dlc_id_get(ptr), ret.dlc_id);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00009A9A File Offset: 0x00007C9A
		public static void Csharp2Cpp(DlcInstallProgress data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.progress, RAIL_API_PINVOKE.DlcInstallProgress_progress_get(ptr));
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallProgress_dlc_id_get(ptr));
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00009AC5 File Offset: 0x00007CC5
		public static void Cpp2Csharp(IntPtr ptr, DlcInstallStart ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallStart_dlc_id_get(ptr), ret.dlc_id);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00009ADF File Offset: 0x00007CDF
		public static void Csharp2Cpp(DlcInstallStart data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallStart_dlc_id_get(ptr));
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00009AF9 File Offset: 0x00007CF9
		public static void Cpp2Csharp(IntPtr ptr, DlcInstallStartResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallStartResult_dlc_id_get(ptr), ret.dlc_id);
			ret.result = (RailResult)RAIL_API_PINVOKE.DlcInstallStartResult_result_get(ptr);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00009B1F File Offset: 0x00007D1F
		public static void Csharp2Cpp(DlcInstallStartResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallStartResult_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcInstallStartResult_result_set(ptr, (int)data.result);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00009B45 File Offset: 0x00007D45
		public static void Cpp2Csharp(IntPtr ptr, DlcOwnershipChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcOwnershipChanged_dlc_id_get(ptr), ret.dlc_id);
			ret.is_active = RAIL_API_PINVOKE.DlcOwnershipChanged_is_active_get(ptr);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00009B6B File Offset: 0x00007D6B
		public static void Csharp2Cpp(DlcOwnershipChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcOwnershipChanged_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcOwnershipChanged_is_active_set(ptr, data.is_active);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00009B91 File Offset: 0x00007D91
		public static void Cpp2Csharp(IntPtr ptr, DlcRefundChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcRefundChanged_dlc_id_get(ptr), ret.dlc_id);
			ret.refund_state = (EnumRailGameRefundState)RAIL_API_PINVOKE.DlcRefundChanged_refund_state_get(ptr);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00009BB7 File Offset: 0x00007DB7
		public static void Csharp2Cpp(DlcRefundChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcRefundChanged_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcRefundChanged_refund_state_set(ptr, (int)data.refund_state);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00009BDD File Offset: 0x00007DDD
		public static void Cpp2Csharp(IntPtr ptr, DlcUninstallFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcUninstallFinished_dlc_id_get(ptr), ret.dlc_id);
			ret.result = (RailResult)RAIL_API_PINVOKE.DlcUninstallFinished_result_get(ptr);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00009C03 File Offset: 0x00007E03
		public static void Csharp2Cpp(DlcUninstallFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcUninstallFinished_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcUninstallFinished_result_set(ptr, (int)data.result);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00009C29 File Offset: 0x00007E29
		public static void Cpp2Csharp(IntPtr ptr, EventBase ret)
		{
			ret.result = (RailResult)RAIL_API_PINVOKE.EventBase_result_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.EventBase_game_id_get(ptr), ret.game_id);
			ret.user_data = RAIL_API_PINVOKE.EventBase_user_data_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.EventBase_rail_id_get(ptr), ret.rail_id);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00009C65 File Offset: 0x00007E65
		public static void Csharp2Cpp(EventBase data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.EventBase_result_set(ptr, (int)data.result);
			RailConverter.Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.EventBase_game_id_get(ptr));
			RAIL_API_PINVOKE.EventBase_user_data_set(ptr, data.user_data);
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.EventBase_rail_id_get(ptr));
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00009CA1 File Offset: 0x00007EA1
		public static void Cpp2Csharp(IntPtr ptr, ExchangeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsFinished_to_product_info_get(ptr), ret.to_product_info);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsFinished_old_assets_get(ptr), ret.old_assets);
			ret.new_asset_id = RAIL_API_PINVOKE.ExchangeAssetsFinished_new_asset_id_get(ptr);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public static void Csharp2Cpp(ExchangeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.to_product_info, RAIL_API_PINVOKE.ExchangeAssetsFinished_to_product_info_get(ptr));
			RailConverter.Csharp2Cpp(data.old_assets, RAIL_API_PINVOKE.ExchangeAssetsFinished_old_assets_get(ptr));
			RAIL_API_PINVOKE.ExchangeAssetsFinished_new_asset_id_set(ptr, data.new_asset_id);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00009D0F File Offset: 0x00007F0F
		public static void Cpp2Csharp(IntPtr ptr, ExchangeAssetsToFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.exchange_to_asset_id = RAIL_API_PINVOKE.ExchangeAssetsToFinished_exchange_to_asset_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsToFinished_to_product_info_get(ptr), ret.to_product_info);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsToFinished_old_assets_get(ptr), ret.old_assets);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00009D46 File Offset: 0x00007F46
		public static void Csharp2Cpp(ExchangeAssetsToFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.ExchangeAssetsToFinished_exchange_to_asset_id_set(ptr, data.exchange_to_asset_id);
			RailConverter.Csharp2Cpp(data.to_product_info, RAIL_API_PINVOKE.ExchangeAssetsToFinished_to_product_info_get(ptr));
			RailConverter.Csharp2Cpp(data.old_assets, RAIL_API_PINVOKE.ExchangeAssetsToFinished_old_assets_get(ptr));
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00009D80 File Offset: 0x00007F80
		public static void Cpp2Csharp(IntPtr ptr, GameServerInfo ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_server_kvs_get(ptr), ret.server_kvs);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_owner_rail_id_get(ptr), ret.owner_rail_id);
			ret.game_server_name = RAIL_API_PINVOKE.GameServerInfo_game_server_name_get(ptr);
			ret.server_fullname = RAIL_API_PINVOKE.GameServerInfo_server_fullname_get(ptr);
			ret.is_dedicated = RAIL_API_PINVOKE.GameServerInfo_is_dedicated_get(ptr);
			ret.server_info = RAIL_API_PINVOKE.GameServerInfo_server_info_get(ptr);
			ret.server_tags = RAIL_API_PINVOKE.GameServerInfo_server_tags_get(ptr);
			ret.spectator_host = RAIL_API_PINVOKE.GameServerInfo_spectator_host_get(ptr);
			ret.server_description = RAIL_API_PINVOKE.GameServerInfo_server_description_get(ptr);
			ret.server_host = RAIL_API_PINVOKE.GameServerInfo_server_host_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_game_server_rail_id_get(ptr), ret.game_server_rail_id);
			ret.has_password = RAIL_API_PINVOKE.GameServerInfo_has_password_get(ptr);
			ret.server_version = RAIL_API_PINVOKE.GameServerInfo_server_version_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_server_mods_get(ptr), ret.server_mods);
			ret.bot_players = RAIL_API_PINVOKE.GameServerInfo_bot_players_get(ptr);
			ret.game_server_map = RAIL_API_PINVOKE.GameServerInfo_game_server_map_get(ptr);
			ret.max_players = RAIL_API_PINVOKE.GameServerInfo_max_players_get(ptr);
			ret.current_players = RAIL_API_PINVOKE.GameServerInfo_current_players_get(ptr);
			ret.is_friend_only = RAIL_API_PINVOKE.GameServerInfo_is_friend_only_get(ptr);
			ret.zone_id = RAIL_API_PINVOKE.GameServerInfo_zone_id_get(ptr);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00009E94 File Offset: 0x00008094
		public static void Csharp2Cpp(GameServerInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.server_kvs, RAIL_API_PINVOKE.GameServerInfo_server_kvs_get(ptr));
			RailConverter.Csharp2Cpp(data.owner_rail_id, RAIL_API_PINVOKE.GameServerInfo_owner_rail_id_get(ptr));
			RAIL_API_PINVOKE.GameServerInfo_game_server_name_set(ptr, data.game_server_name);
			RAIL_API_PINVOKE.GameServerInfo_server_fullname_set(ptr, data.server_fullname);
			RAIL_API_PINVOKE.GameServerInfo_is_dedicated_set(ptr, data.is_dedicated);
			RAIL_API_PINVOKE.GameServerInfo_server_info_set(ptr, data.server_info);
			RAIL_API_PINVOKE.GameServerInfo_server_tags_set(ptr, data.server_tags);
			RAIL_API_PINVOKE.GameServerInfo_spectator_host_set(ptr, data.spectator_host);
			RAIL_API_PINVOKE.GameServerInfo_server_description_set(ptr, data.server_description);
			RAIL_API_PINVOKE.GameServerInfo_server_host_set(ptr, data.server_host);
			RailConverter.Csharp2Cpp(data.game_server_rail_id, RAIL_API_PINVOKE.GameServerInfo_game_server_rail_id_get(ptr));
			RAIL_API_PINVOKE.GameServerInfo_has_password_set(ptr, data.has_password);
			RAIL_API_PINVOKE.GameServerInfo_server_version_set(ptr, data.server_version);
			RailConverter.Csharp2Cpp(data.server_mods, RAIL_API_PINVOKE.GameServerInfo_server_mods_get(ptr));
			RAIL_API_PINVOKE.GameServerInfo_bot_players_set(ptr, data.bot_players);
			RAIL_API_PINVOKE.GameServerInfo_game_server_map_set(ptr, data.game_server_map);
			RAIL_API_PINVOKE.GameServerInfo_max_players_set(ptr, data.max_players);
			RAIL_API_PINVOKE.GameServerInfo_current_players_set(ptr, data.current_players);
			RAIL_API_PINVOKE.GameServerInfo_is_friend_only_set(ptr, data.is_friend_only);
			RAIL_API_PINVOKE.GameServerInfo_zone_id_set(ptr, data.zone_id);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00009FA8 File Offset: 0x000081A8
		public static void Cpp2Csharp(IntPtr ptr, GameServerListFilter ret)
		{
			ret.tags_contained = RAIL_API_PINVOKE.GameServerListFilter_tags_contained_get(ptr);
			ret.tags_not_contained = RAIL_API_PINVOKE.GameServerListFilter_tags_not_contained_get(ptr);
			ret.filter_password = (EnumRailOptionalValue)RAIL_API_PINVOKE.GameServerListFilter_filter_password_get(ptr);
			ret.filter_game_server_name = RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_name_get(ptr);
			ret.filter_zone_id = RAIL_API_PINVOKE.GameServerListFilter_filter_zone_id_get(ptr);
			ret.filter_friends_created = (EnumRailOptionalValue)RAIL_API_PINVOKE.GameServerListFilter_filter_friends_created_get(ptr);
			ret.filter_dedicated_server = (EnumRailOptionalValue)RAIL_API_PINVOKE.GameServerListFilter_filter_dedicated_server_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerListFilter_filters_get(ptr), ret.filters);
			ret.filter_game_server_map = RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_map_get(ptr);
			ret.filter_game_server_host = RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_host_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerListFilter_owner_id_get(ptr), ret.owner_id);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0000A044 File Offset: 0x00008244
		public static void Csharp2Cpp(GameServerListFilter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.GameServerListFilter_tags_contained_set(ptr, data.tags_contained);
			RAIL_API_PINVOKE.GameServerListFilter_tags_not_contained_set(ptr, data.tags_not_contained);
			RAIL_API_PINVOKE.GameServerListFilter_filter_password_set(ptr, (int)data.filter_password);
			RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_name_set(ptr, data.filter_game_server_name);
			RAIL_API_PINVOKE.GameServerListFilter_filter_zone_id_set(ptr, data.filter_zone_id);
			RAIL_API_PINVOKE.GameServerListFilter_filter_friends_created_set(ptr, (int)data.filter_friends_created);
			RAIL_API_PINVOKE.GameServerListFilter_filter_dedicated_server_set(ptr, (int)data.filter_dedicated_server);
			RailConverter.Csharp2Cpp(data.filters, RAIL_API_PINVOKE.GameServerListFilter_filters_get(ptr));
			RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_map_set(ptr, data.filter_game_server_map);
			RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_host_set(ptr, data.filter_game_server_host);
			RailConverter.Csharp2Cpp(data.owner_id, RAIL_API_PINVOKE.GameServerListFilter_owner_id_get(ptr));
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0000A0DF File Offset: 0x000082DF
		public static void Cpp2Csharp(IntPtr ptr, GameServerListFilterKey ret)
		{
			ret.filter_value = RAIL_API_PINVOKE.GameServerListFilterKey_filter_value_get(ptr);
			ret.key_name = RAIL_API_PINVOKE.GameServerListFilterKey_key_name_get(ptr);
			ret.value_type = (EnumRailPropertyValueType)RAIL_API_PINVOKE.GameServerListFilterKey_value_type_get(ptr);
			ret.comparison_type = (EnumRailComparisonType)RAIL_API_PINVOKE.GameServerListFilterKey_comparison_type_get(ptr);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0000A111 File Offset: 0x00008311
		public static void Csharp2Cpp(GameServerListFilterKey data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.GameServerListFilterKey_filter_value_set(ptr, data.filter_value);
			RAIL_API_PINVOKE.GameServerListFilterKey_key_name_set(ptr, data.key_name);
			RAIL_API_PINVOKE.GameServerListFilterKey_value_type_set(ptr, (int)data.value_type);
			RAIL_API_PINVOKE.GameServerListFilterKey_comparison_type_set(ptr, (int)data.comparison_type);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0000A143 File Offset: 0x00008343
		public static void Cpp2Csharp(IntPtr ptr, GameServerListSorter ret)
		{
			ret.sort_key = RAIL_API_PINVOKE.GameServerListSorter_sort_key_get(ptr);
			ret.sort_type = (EnumRailSortType)RAIL_API_PINVOKE.GameServerListSorter_sort_type_get(ptr);
			ret.sorter_key_type = (GameServerListSorterKeyType)RAIL_API_PINVOKE.GameServerListSorter_sorter_key_type_get(ptr);
			ret.sort_value_type = (EnumRailPropertyValueType)RAIL_API_PINVOKE.GameServerListSorter_sort_value_type_get(ptr);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0000A175 File Offset: 0x00008375
		public static void Csharp2Cpp(GameServerListSorter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.GameServerListSorter_sort_key_set(ptr, data.sort_key);
			RAIL_API_PINVOKE.GameServerListSorter_sort_type_set(ptr, (int)data.sort_type);
			RAIL_API_PINVOKE.GameServerListSorter_sorter_key_type_set(ptr, (int)data.sorter_key_type);
			RAIL_API_PINVOKE.GameServerListSorter_sort_value_type_set(ptr, (int)data.sort_value_type);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0000A1A7 File Offset: 0x000083A7
		public static void Cpp2Csharp(IntPtr ptr, GameServerPlayerInfo ret)
		{
			ret.member_nickname = RAIL_API_PINVOKE.GameServerPlayerInfo_member_nickname_get(ptr);
			ret.member_score = RAIL_API_PINVOKE.GameServerPlayerInfo_member_score_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerPlayerInfo_member_id_get(ptr), ret.member_id);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0000A1D2 File Offset: 0x000083D2
		public static void Csharp2Cpp(GameServerPlayerInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.GameServerPlayerInfo_member_nickname_set(ptr, data.member_nickname);
			RAIL_API_PINVOKE.GameServerPlayerInfo_member_score_set(ptr, data.member_score);
			RailConverter.Csharp2Cpp(data.member_id, RAIL_API_PINVOKE.GameServerPlayerInfo_member_id_get(ptr));
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, GameServerRegisterToServerListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(GameServerRegisterToServerListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0000A1FD File Offset: 0x000083FD
		public static void Cpp2Csharp(IntPtr ptr, GameServerStartSessionWithPlayerResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerStartSessionWithPlayerResponse_remote_rail_id_get(ptr), ret.remote_rail_id);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0000A217 File Offset: 0x00008417
		public static void Csharp2Cpp(GameServerStartSessionWithPlayerResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.remote_rail_id, RAIL_API_PINVOKE.GameServerStartSessionWithPlayerResponse_remote_rail_id_get(ptr));
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0000A231 File Offset: 0x00008431
		public static void Cpp2Csharp(IntPtr ptr, GetAuthenticateURLResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.ticket_expire_time = RAIL_API_PINVOKE.GetAuthenticateURLResult_ticket_expire_time_get(ptr);
			ret.authenticate_url = RAIL_API_PINVOKE.GetAuthenticateURLResult_authenticate_url_get(ptr);
			ret.source_url = RAIL_API_PINVOKE.GetAuthenticateURLResult_source_url_get(ptr);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0000A25E File Offset: 0x0000845E
		public static void Csharp2Cpp(GetAuthenticateURLResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.GetAuthenticateURLResult_ticket_expire_time_set(ptr, data.ticket_expire_time);
			RAIL_API_PINVOKE.GetAuthenticateURLResult_authenticate_url_set(ptr, data.authenticate_url);
			RAIL_API_PINVOKE.GetAuthenticateURLResult_source_url_set(ptr, data.source_url);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0000A28B File Offset: 0x0000848B
		public static void Cpp2Csharp(IntPtr ptr, GetGameServerListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerListResult_server_info_get(ptr), ret.server_info);
			ret.total_num = RAIL_API_PINVOKE.GetGameServerListResult_total_num_get(ptr);
			ret.start_index = RAIL_API_PINVOKE.GetGameServerListResult_start_index_get(ptr);
			ret.end_index = RAIL_API_PINVOKE.GetGameServerListResult_end_index_get(ptr);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0000A2C9 File Offset: 0x000084C9
		public static void Csharp2Cpp(GetGameServerListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.server_info, RAIL_API_PINVOKE.GetGameServerListResult_server_info_get(ptr));
			RAIL_API_PINVOKE.GetGameServerListResult_total_num_set(ptr, data.total_num);
			RAIL_API_PINVOKE.GetGameServerListResult_start_index_set(ptr, data.start_index);
			RAIL_API_PINVOKE.GetGameServerListResult_end_index_set(ptr, data.end_index);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0000A307 File Offset: 0x00008507
		public static void Cpp2Csharp(IntPtr ptr, GetGameServerMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerMetadataResult_game_server_id_get(ptr), ret.game_server_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerMetadataResult_key_value_get(ptr), ret.key_value);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0000A332 File Offset: 0x00008532
		public static void Csharp2Cpp(GetGameServerMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.GetGameServerMetadataResult_game_server_id_get(ptr));
			RailConverter.Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetGameServerMetadataResult_key_value_get(ptr));
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0000A35D File Offset: 0x0000855D
		public static void Cpp2Csharp(IntPtr ptr, GetGameServerPlayerListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerPlayerListResult_game_server_id_get(ptr), ret.game_server_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerPlayerListResult_server_player_info_get(ptr), ret.server_player_info);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0000A388 File Offset: 0x00008588
		public static void Csharp2Cpp(GetGameServerPlayerListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.GetGameServerPlayerListResult_game_server_id_get(ptr));
			RailConverter.Csharp2Cpp(data.server_player_info, RAIL_API_PINVOKE.GetGameServerPlayerListResult_server_player_info_get(ptr));
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0000A3B3 File Offset: 0x000085B3
		public static void Cpp2Csharp(IntPtr ptr, GetMemberMetadataInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetMemberMetadataInfo_key_value_get(ptr), ret.key_value);
			ret.room_id = RAIL_API_PINVOKE.GetMemberMetadataInfo_room_id_get(ptr);
			ret.member_id = RAIL_API_PINVOKE.GetMemberMetadataInfo_member_id_get(ptr);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0000A3E5 File Offset: 0x000085E5
		public static void Csharp2Cpp(GetMemberMetadataInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetMemberMetadataInfo_key_value_get(ptr));
			RAIL_API_PINVOKE.GetMemberMetadataInfo_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.GetMemberMetadataInfo_member_id_set(ptr, data.member_id);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0000A417 File Offset: 0x00008617
		public static void Cpp2Csharp(IntPtr ptr, GetRoomMetadataInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetRoomMetadataInfo_key_value_get(ptr), ret.key_value);
			ret.room_id = RAIL_API_PINVOKE.GetRoomMetadataInfo_room_id_get(ptr);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0000A43D File Offset: 0x0000863D
		public static void Csharp2Cpp(GetRoomMetadataInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetRoomMetadataInfo_key_value_get(ptr));
			RAIL_API_PINVOKE.GetRoomMetadataInfo_room_id_set(ptr, data.room_id);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0000A463 File Offset: 0x00008663
		public static void Cpp2Csharp(IntPtr ptr, GlobalAchievementReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.count = RAIL_API_PINVOKE.GlobalAchievementReceived_count_get(ptr);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0000A478 File Offset: 0x00008678
		public static void Csharp2Cpp(GlobalAchievementReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.GlobalAchievementReceived_count_set(ptr, data.count);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, GlobalStatsRequestReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(GlobalStatsRequestReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0000A48D File Offset: 0x0000868D
		public static void Cpp2Csharp(IntPtr ptr, JavascriptEventResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.event_name = RAIL_API_PINVOKE.JavascriptEventResult_event_name_get(ptr);
			ret.event_value = RAIL_API_PINVOKE.JavascriptEventResult_event_value_get(ptr);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0000A4AE File Offset: 0x000086AE
		public static void Csharp2Cpp(JavascriptEventResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.JavascriptEventResult_event_name_set(ptr, data.event_name);
			RAIL_API_PINVOKE.JavascriptEventResult_event_value_set(ptr, data.event_value);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0000A4CF File Offset: 0x000086CF
		public static void Cpp2Csharp(IntPtr ptr, JoinRoomInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.JoinRoomInfo_room_id_get(ptr);
			ret.zone_id = RAIL_API_PINVOKE.JoinRoomInfo_zone_id_get(ptr);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public static void Csharp2Cpp(JoinRoomInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.JoinRoomInfo_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.JoinRoomInfo_zone_id_set(ptr, data.zone_id);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0000A511 File Offset: 0x00008711
		public static void Cpp2Csharp(IntPtr ptr, JoinVoiceChannelResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.JoinVoiceChannelResult_already_joined_channel_id_get(ptr), ret.already_joined_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.JoinVoiceChannelResult_voice_channel_id_get(ptr), ret.voice_channel_id);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0000A53C File Offset: 0x0000873C
		public static void Csharp2Cpp(JoinVoiceChannelResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.already_joined_channel_id, RAIL_API_PINVOKE.JoinVoiceChannelResult_already_joined_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.JoinVoiceChannelResult_voice_channel_id_get(ptr));
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0000A567 File Offset: 0x00008767
		public static void Cpp2Csharp(IntPtr ptr, KickOffMemberInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.KickOffMemberInfo_room_id_get(ptr);
			ret.kicked_id = RAIL_API_PINVOKE.KickOffMemberInfo_kicked_id_get(ptr);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0000A588 File Offset: 0x00008788
		public static void Csharp2Cpp(KickOffMemberInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.KickOffMemberInfo_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.KickOffMemberInfo_kicked_id_set(ptr, data.kicked_id);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0000A5A9 File Offset: 0x000087A9
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardAttachSpaceWork ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_leaderboard_name_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_spacework_id_get(ptr), ret.spacework_id);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0000A5CF File Offset: 0x000087CF
		public static void Csharp2Cpp(LeaderboardAttachSpaceWork data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_leaderboard_name_set(ptr, data.leaderboard_name);
			RailConverter.Csharp2Cpp(data.spacework_id, RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_spacework_id_get(ptr));
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0000A5F5 File Offset: 0x000087F5
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardCreated ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardCreated_leaderboard_name_get(ptr);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0000A60A File Offset: 0x0000880A
		public static void Csharp2Cpp(LeaderboardCreated data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardCreated_leaderboard_name_set(ptr, data.leaderboard_name);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0000A61F File Offset: 0x0000881F
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardData ret)
		{
			ret.additional_infomation = RAIL_API_PINVOKE.LeaderboardData_additional_infomation_get(ptr);
			ret.score = RAIL_API_PINVOKE.LeaderboardData_score_get(ptr);
			ret.rank = RAIL_API_PINVOKE.LeaderboardData_rank_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardData_spacework_id_get(ptr), ret.spacework_id);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0000A656 File Offset: 0x00008856
		public static void Csharp2Cpp(LeaderboardData data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.LeaderboardData_additional_infomation_set(ptr, data.additional_infomation);
			RAIL_API_PINVOKE.LeaderboardData_score_set(ptr, data.score);
			RAIL_API_PINVOKE.LeaderboardData_rank_set(ptr, data.rank);
			RailConverter.Csharp2Cpp(data.spacework_id, RAIL_API_PINVOKE.LeaderboardData_spacework_id_get(ptr));
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0000A68D File Offset: 0x0000888D
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardEntry ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardEntry_player_id_get(ptr), ret.player_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardEntry_data_get(ptr), ret.data);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0000A6B1 File Offset: 0x000088B1
		public static void Csharp2Cpp(LeaderboardEntry data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.player_id, RAIL_API_PINVOKE.LeaderboardEntry_player_id_get(ptr));
			RailConverter.Csharp2Cpp(data.data, RAIL_API_PINVOKE.LeaderboardEntry_data_get(ptr));
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0000A6D5 File Offset: 0x000088D5
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardEntryReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardEntryReceived_leaderboard_name_get(ptr);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0000A6EA File Offset: 0x000088EA
		public static void Csharp2Cpp(LeaderboardEntryReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardEntryReceived_leaderboard_name_set(ptr, data.leaderboard_name);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0000A6FF File Offset: 0x000088FF
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardParameters ret)
		{
			ret.param = RAIL_API_PINVOKE.LeaderboardParameters_param_get(ptr);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0000A70D File Offset: 0x0000890D
		public static void Csharp2Cpp(LeaderboardParameters data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.LeaderboardParameters_param_set(ptr, data.param);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0000A71B File Offset: 0x0000891B
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardReceived_leaderboard_name_get(ptr);
			ret.does_exist = RAIL_API_PINVOKE.LeaderboardReceived_does_exist_get(ptr);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0000A73C File Offset: 0x0000893C
		public static void Csharp2Cpp(LeaderboardReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardReceived_leaderboard_name_set(ptr, data.leaderboard_name);
			RAIL_API_PINVOKE.LeaderboardReceived_does_exist_set(ptr, data.does_exist);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0000A760 File Offset: 0x00008960
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardUploaded ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.old_rank = RAIL_API_PINVOKE.LeaderboardUploaded_old_rank_get(ptr);
			ret.leaderboard_name = RAIL_API_PINVOKE.LeaderboardUploaded_leaderboard_name_get(ptr);
			ret.score = RAIL_API_PINVOKE.LeaderboardUploaded_score_get(ptr);
			ret.better_score = RAIL_API_PINVOKE.LeaderboardUploaded_better_score_get(ptr);
			ret.new_rank = RAIL_API_PINVOKE.LeaderboardUploaded_new_rank_get(ptr);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0000A7B0 File Offset: 0x000089B0
		public static void Csharp2Cpp(LeaderboardUploaded data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardUploaded_old_rank_set(ptr, data.old_rank);
			RAIL_API_PINVOKE.LeaderboardUploaded_leaderboard_name_set(ptr, data.leaderboard_name);
			RAIL_API_PINVOKE.LeaderboardUploaded_score_set(ptr, data.score);
			RAIL_API_PINVOKE.LeaderboardUploaded_better_score_set(ptr, data.better_score);
			RAIL_API_PINVOKE.LeaderboardUploaded_new_rank_set(ptr, data.new_rank);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0000A800 File Offset: 0x00008A00
		public static void Cpp2Csharp(IntPtr ptr, LeaveRoomInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.reason = (EnumLeaveRoomReason)RAIL_API_PINVOKE.LeaveRoomInfo_reason_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.LeaveRoomInfo_room_id_get(ptr);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0000A821 File Offset: 0x00008A21
		public static void Csharp2Cpp(LeaveRoomInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaveRoomInfo_reason_set(ptr, (int)data.reason);
			RAIL_API_PINVOKE.LeaveRoomInfo_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0000A842 File Offset: 0x00008A42
		public static void Cpp2Csharp(IntPtr ptr, LeaveVoiceChannelResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaveVoiceChannelResult_voice_channel_id_get(ptr), ret.voice_channel_id);
			ret.reason = (EnumRailVoiceLeaveChannelReason)RAIL_API_PINVOKE.LeaveVoiceChannelResult_reason_get(ptr);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0000A868 File Offset: 0x00008A68
		public static void Csharp2Cpp(LeaveVoiceChannelResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.LeaveVoiceChannelResult_voice_channel_id_get(ptr));
			RAIL_API_PINVOKE.LeaveVoiceChannelResult_reason_set(ptr, (int)data.reason);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0000A88E File Offset: 0x00008A8E
		public static void Cpp2Csharp(IntPtr ptr, MemberInfo ret)
		{
			ret.member_name = RAIL_API_PINVOKE.MemberInfo_member_name_get(ptr);
			ret.member_index = RAIL_API_PINVOKE.MemberInfo_member_index_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.MemberInfo_room_id_get(ptr);
			ret.member_id = RAIL_API_PINVOKE.MemberInfo_member_id_get(ptr);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		public static void Csharp2Cpp(MemberInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.MemberInfo_member_name_set(ptr, data.member_name);
			RAIL_API_PINVOKE.MemberInfo_member_index_set(ptr, data.member_index);
			RAIL_API_PINVOKE.MemberInfo_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.MemberInfo_member_id_set(ptr, data.member_id);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0000A8F2 File Offset: 0x00008AF2
		public static void Cpp2Csharp(IntPtr ptr, MergeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.MergeAssetsFinished_source_assets_get(ptr), ret.source_assets);
			ret.new_asset_id = RAIL_API_PINVOKE.MergeAssetsFinished_new_asset_id_get(ptr);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0000A918 File Offset: 0x00008B18
		public static void Csharp2Cpp(MergeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.source_assets, RAIL_API_PINVOKE.MergeAssetsFinished_source_assets_get(ptr));
			RAIL_API_PINVOKE.MergeAssetsFinished_new_asset_id_set(ptr, data.new_asset_id);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0000A93E File Offset: 0x00008B3E
		public static void Cpp2Csharp(IntPtr ptr, MergeAssetsToFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.merge_to_asset_id = RAIL_API_PINVOKE.MergeAssetsToFinished_merge_to_asset_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.MergeAssetsToFinished_source_assets_get(ptr), ret.source_assets);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0000A964 File Offset: 0x00008B64
		public static void Csharp2Cpp(MergeAssetsToFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.MergeAssetsToFinished_merge_to_asset_id_set(ptr, data.merge_to_asset_id);
			RailConverter.Csharp2Cpp(data.source_assets, RAIL_API_PINVOKE.MergeAssetsToFinished_source_assets_get(ptr));
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0000A98A File Offset: 0x00008B8A
		public static void Cpp2Csharp(IntPtr ptr, NotifyMetadataChange ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.changer_id = RAIL_API_PINVOKE.NotifyMetadataChange_changer_id_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.NotifyMetadataChange_room_id_get(ptr);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0000A9AB File Offset: 0x00008BAB
		public static void Csharp2Cpp(NotifyMetadataChange data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NotifyMetadataChange_changer_id_set(ptr, data.changer_id);
			RAIL_API_PINVOKE.NotifyMetadataChange_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomDestroy ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomDestroy_room_id_get(ptr);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0000A9E1 File Offset: 0x00008BE1
		public static void Csharp2Cpp(NotifyRoomDestroy data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NotifyRoomDestroy_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0000A9F6 File Offset: 0x00008BF6
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomGameServerChange ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.game_server_rail_id = RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_rail_id_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomGameServerChange_room_id_get(ptr);
			ret.game_server_channel_id = RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_channel_id_get(ptr);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0000AA23 File Offset: 0x00008C23
		public static void Csharp2Cpp(NotifyRoomGameServerChange data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_rail_id_set(ptr, data.game_server_rail_id);
			RAIL_API_PINVOKE.NotifyRoomGameServerChange_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_channel_id_set(ptr, data.game_server_channel_id);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0000AA50 File Offset: 0x00008C50
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomMemberChange ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.changer_id = RAIL_API_PINVOKE.NotifyRoomMemberChange_changer_id_get(ptr);
			ret.id_for_making_change = RAIL_API_PINVOKE.NotifyRoomMemberChange_id_for_making_change_get(ptr);
			ret.state_change = (EnumRoomMemberActionStatus)RAIL_API_PINVOKE.NotifyRoomMemberChange_state_change_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomMemberChange_room_id_get(ptr);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0000AA89 File Offset: 0x00008C89
		public static void Csharp2Cpp(NotifyRoomMemberChange data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NotifyRoomMemberChange_changer_id_set(ptr, data.changer_id);
			RAIL_API_PINVOKE.NotifyRoomMemberChange_id_for_making_change_set(ptr, data.id_for_making_change);
			RAIL_API_PINVOKE.NotifyRoomMemberChange_state_change_set(ptr, (int)data.state_change);
			RAIL_API_PINVOKE.NotifyRoomMemberChange_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0000AAC2 File Offset: 0x00008CC2
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomMemberKicked ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.id_for_making_kick = RAIL_API_PINVOKE.NotifyRoomMemberKicked_id_for_making_kick_get(ptr);
			ret.due_to_kicker_lost_connect = RAIL_API_PINVOKE.NotifyRoomMemberKicked_due_to_kicker_lost_connect_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomMemberKicked_room_id_get(ptr);
			ret.kicked_id = RAIL_API_PINVOKE.NotifyRoomMemberKicked_kicked_id_get(ptr);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0000AAFB File Offset: 0x00008CFB
		public static void Csharp2Cpp(NotifyRoomMemberKicked data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NotifyRoomMemberKicked_id_for_making_kick_set(ptr, data.id_for_making_kick);
			RAIL_API_PINVOKE.NotifyRoomMemberKicked_due_to_kicker_lost_connect_set(ptr, data.due_to_kicker_lost_connect);
			RAIL_API_PINVOKE.NotifyRoomMemberKicked_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.NotifyRoomMemberKicked_kicked_id_set(ptr, data.kicked_id);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0000AB34 File Offset: 0x00008D34
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomOwnerChange ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.old_owner_id = RAIL_API_PINVOKE.NotifyRoomOwnerChange_old_owner_id_get(ptr);
			ret.reason = (EnumRoomOwnerChangeReason)RAIL_API_PINVOKE.NotifyRoomOwnerChange_reason_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomOwnerChange_room_id_get(ptr);
			ret.new_owner_id = RAIL_API_PINVOKE.NotifyRoomOwnerChange_new_owner_id_get(ptr);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0000AB6D File Offset: 0x00008D6D
		public static void Csharp2Cpp(NotifyRoomOwnerChange data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NotifyRoomOwnerChange_old_owner_id_set(ptr, data.old_owner_id);
			RAIL_API_PINVOKE.NotifyRoomOwnerChange_reason_set(ptr, (int)data.reason);
			RAIL_API_PINVOKE.NotifyRoomOwnerChange_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.NotifyRoomOwnerChange_new_owner_id_set(ptr, data.new_owner_id);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0000ABA6 File Offset: 0x00008DA6
		public static void Cpp2Csharp(IntPtr ptr, NumberOfPlayerReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.online_number = RAIL_API_PINVOKE.NumberOfPlayerReceived_online_number_get(ptr);
			ret.offline_number = RAIL_API_PINVOKE.NumberOfPlayerReceived_offline_number_get(ptr);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0000ABC7 File Offset: 0x00008DC7
		public static void Csharp2Cpp(NumberOfPlayerReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NumberOfPlayerReceived_online_number_set(ptr, data.online_number);
			RAIL_API_PINVOKE.NumberOfPlayerReceived_offline_number_set(ptr, data.offline_number);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, PlayerAchievementReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(PlayerAchievementReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public static void Cpp2Csharp(IntPtr ptr, PlayerAchievementStored ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.group_achievement = RAIL_API_PINVOKE.PlayerAchievementStored_group_achievement_get(ptr);
			ret.achievement_name = RAIL_API_PINVOKE.PlayerAchievementStored_achievement_name_get(ptr);
			ret.current_progress = RAIL_API_PINVOKE.PlayerAchievementStored_current_progress_get(ptr);
			ret.max_progress = RAIL_API_PINVOKE.PlayerAchievementStored_max_progress_get(ptr);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0000AC21 File Offset: 0x00008E21
		public static void Csharp2Cpp(PlayerAchievementStored data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.PlayerAchievementStored_group_achievement_set(ptr, data.group_achievement);
			RAIL_API_PINVOKE.PlayerAchievementStored_achievement_name_set(ptr, data.achievement_name);
			RAIL_API_PINVOKE.PlayerAchievementStored_current_progress_set(ptr, data.current_progress);
			RAIL_API_PINVOKE.PlayerAchievementStored_max_progress_set(ptr, data.max_progress);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0000AC5A File Offset: 0x00008E5A
		public static void Cpp2Csharp(IntPtr ptr, PlayerGetGamePurchaseKeyResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.purchase_key = RAIL_API_PINVOKE.PlayerGetGamePurchaseKeyResult_purchase_key_get(ptr);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0000AC6F File Offset: 0x00008E6F
		public static void Csharp2Cpp(PlayerGetGamePurchaseKeyResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.PlayerGetGamePurchaseKeyResult_purchase_key_set(ptr, data.purchase_key);
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0000AC84 File Offset: 0x00008E84
		public static void Cpp2Csharp(IntPtr ptr, PlayerPersonalInfo ret)
		{
			ret.error_code = (RailResult)RAIL_API_PINVOKE.PlayerPersonalInfo_error_code_get(ptr);
			ret.avatar_url = RAIL_API_PINVOKE.PlayerPersonalInfo_avatar_url_get(ptr);
			ret.rail_level = RAIL_API_PINVOKE.PlayerPersonalInfo_rail_level_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.PlayerPersonalInfo_rail_id_get(ptr), ret.rail_id);
			ret.rail_name = RAIL_API_PINVOKE.PlayerPersonalInfo_rail_name_get(ptr);
			ret.email_address = RAIL_API_PINVOKE.PlayerPersonalInfo_email_address_get(ptr);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0000ACE0 File Offset: 0x00008EE0
		public static void Csharp2Cpp(PlayerPersonalInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.PlayerPersonalInfo_error_code_set(ptr, (int)data.error_code);
			RAIL_API_PINVOKE.PlayerPersonalInfo_avatar_url_set(ptr, data.avatar_url);
			RAIL_API_PINVOKE.PlayerPersonalInfo_rail_level_set(ptr, data.rail_level);
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.PlayerPersonalInfo_rail_id_get(ptr));
			RAIL_API_PINVOKE.PlayerPersonalInfo_rail_name_set(ptr, data.rail_name);
			RAIL_API_PINVOKE.PlayerPersonalInfo_email_address_set(ptr, data.email_address);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, PlayerStatsReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(PlayerStatsReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, PlayerStatsStored ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(PlayerStatsStored data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0000AD3A File Offset: 0x00008F3A
		public static void Cpp2Csharp(IntPtr ptr, PublishScreenshotResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.PublishScreenshotResult_work_id_get(ptr), ret.work_id);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0000AD54 File Offset: 0x00008F54
		public static void Csharp2Cpp(PublishScreenshotResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.work_id, RAIL_API_PINVOKE.PublishScreenshotResult_work_id_get(ptr));
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0000AD6E File Offset: 0x00008F6E
		public static void Cpp2Csharp(IntPtr ptr, QueryIsOwnedDlcsResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.QueryIsOwnedDlcsResult_dlc_owned_list_get(ptr), ret.dlc_owned_list);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0000AD88 File Offset: 0x00008F88
		public static void Csharp2Cpp(QueryIsOwnedDlcsResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_owned_list, RAIL_API_PINVOKE.QueryIsOwnedDlcsResult_dlc_owned_list_get(ptr));
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0000ADA2 File Offset: 0x00008FA2
		public static void Cpp2Csharp(IntPtr ptr, QueryMySubscribedSpaceWorksResult ret)
		{
			ret.total_available_works = RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_total_available_works_get(ptr);
			ret.spacework_type = (EnumRailSpaceWorkType)RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0000ADCD File Offset: 0x00008FCD
		public static void Csharp2Cpp(QueryMySubscribedSpaceWorksResult data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
			RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_type_set(ptr, (int)data.spacework_type);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public static void Cpp2Csharp(IntPtr ptr, QueryPlayerBannedStatus ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.status = (EnumRailPlayerBannedStatus)RAIL_API_PINVOKE.QueryPlayerBannedStatus_status_get(ptr);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0000AE0D File Offset: 0x0000900D
		public static void Csharp2Cpp(QueryPlayerBannedStatus data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.QueryPlayerBannedStatus_status_set(ptr, (int)data.status);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0000AE22 File Offset: 0x00009022
		public static void Cpp2Csharp(IntPtr ptr, QuerySubscribeWishPlayStateResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.is_subscribed = RAIL_API_PINVOKE.QuerySubscribeWishPlayStateResult_is_subscribed_get(ptr);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0000AE37 File Offset: 0x00009037
		public static void Csharp2Cpp(QuerySubscribeWishPlayStateResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.QuerySubscribeWishPlayStateResult_is_subscribed_set(ptr, data.is_subscribed);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0000AE4C File Offset: 0x0000904C
		public static void Cpp2Csharp(IntPtr ptr, RailAntiAddictionGameOnlineTimeChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.game_online_time_count_minutes = RAIL_API_PINVOKE.RailAntiAddictionGameOnlineTimeChanged_game_online_time_count_minutes_get(ptr);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0000AE61 File Offset: 0x00009061
		public static void Csharp2Cpp(RailAntiAddictionGameOnlineTimeChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailAntiAddictionGameOnlineTimeChanged_game_online_time_count_minutes_set(ptr, data.game_online_time_count_minutes);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0000AE78 File Offset: 0x00009078
		public static void Cpp2Csharp(IntPtr ptr, RailAssetInfo ret)
		{
			ret.asset_id = RAIL_API_PINVOKE.RailAssetInfo_asset_id_get(ptr);
			ret.origin = RAIL_API_PINVOKE.RailAssetInfo_origin_get(ptr);
			ret.product_id = RAIL_API_PINVOKE.RailAssetInfo_product_id_get(ptr);
			ret.flag = RAIL_API_PINVOKE.RailAssetInfo_flag_get(ptr);
			ret.state = RAIL_API_PINVOKE.RailAssetInfo_state_get(ptr);
			ret.progress = RAIL_API_PINVOKE.RailAssetInfo_progress_get(ptr);
			ret.expire_time = RAIL_API_PINVOKE.RailAssetInfo_expire_time_get(ptr);
			ret.position = RAIL_API_PINVOKE.RailAssetInfo_position_get(ptr);
			ret.product_name = RAIL_API_PINVOKE.RailAssetInfo_product_name_get(ptr);
			ret.quantity = RAIL_API_PINVOKE.RailAssetInfo_quantity_get(ptr);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0000AF00 File Offset: 0x00009100
		public static void Csharp2Cpp(RailAssetInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailAssetInfo_asset_id_set(ptr, data.asset_id);
			RAIL_API_PINVOKE.RailAssetInfo_origin_set(ptr, data.origin);
			RAIL_API_PINVOKE.RailAssetInfo_product_id_set(ptr, data.product_id);
			RAIL_API_PINVOKE.RailAssetInfo_flag_set(ptr, data.flag);
			RAIL_API_PINVOKE.RailAssetInfo_state_set(ptr, data.state);
			RAIL_API_PINVOKE.RailAssetInfo_progress_set(ptr, data.progress);
			RAIL_API_PINVOKE.RailAssetInfo_expire_time_set(ptr, data.expire_time);
			RAIL_API_PINVOKE.RailAssetInfo_position_set(ptr, data.position);
			RAIL_API_PINVOKE.RailAssetInfo_product_name_set(ptr, data.product_name);
			RAIL_API_PINVOKE.RailAssetInfo_quantity_set(ptr, data.quantity);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0000AF85 File Offset: 0x00009185
		public static void Cpp2Csharp(IntPtr ptr, RailAssetItem ret)
		{
			ret.asset_id = RAIL_API_PINVOKE.RailAssetItem_asset_id_get(ptr);
			ret.quantity = RAIL_API_PINVOKE.RailAssetItem_quantity_get(ptr);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0000AF9F File Offset: 0x0000919F
		public static void Csharp2Cpp(RailAssetItem data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailAssetItem_asset_id_set(ptr, data.asset_id);
			RAIL_API_PINVOKE.RailAssetItem_quantity_set(ptr, data.quantity);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0000AFB9 File Offset: 0x000091B9
		public static void Cpp2Csharp(IntPtr ptr, RailAssetProperty ret)
		{
			ret.asset_id = RAIL_API_PINVOKE.RailAssetProperty_asset_id_get(ptr);
			ret.position = RAIL_API_PINVOKE.RailAssetProperty_position_get(ptr);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0000AFD3 File Offset: 0x000091D3
		public static void Csharp2Cpp(RailAssetProperty data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailAssetProperty_asset_id_set(ptr, data.asset_id);
			RAIL_API_PINVOKE.RailAssetProperty_position_set(ptr, data.position);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0000AFED File Offset: 0x000091ED
		public static void Cpp2Csharp(IntPtr ptr, RailBranchInfo ret)
		{
			ret.branch_name = RAIL_API_PINVOKE.RailBranchInfo_branch_name_get(ptr);
			ret.build_number = RAIL_API_PINVOKE.RailBranchInfo_build_number_get(ptr);
			ret.branch_type = RAIL_API_PINVOKE.RailBranchInfo_branch_type_get(ptr);
			ret.branch_id = RAIL_API_PINVOKE.RailBranchInfo_branch_id_get(ptr);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0000B01F File Offset: 0x0000921F
		public static void Csharp2Cpp(RailBranchInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailBranchInfo_branch_name_set(ptr, data.branch_name);
			RAIL_API_PINVOKE.RailBranchInfo_build_number_set(ptr, data.build_number);
			RAIL_API_PINVOKE.RailBranchInfo_branch_type_set(ptr, data.branch_type);
			RAIL_API_PINVOKE.RailBranchInfo_branch_id_set(ptr, data.branch_id);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0000B051 File Offset: 0x00009251
		public static void Cpp2Csharp(IntPtr ptr, RailCrashInfo ret)
		{
			ret.exception_type = (RailUtilsCrashType)RAIL_API_PINVOKE.RailCrashInfo_exception_type_get(ptr);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0000B05F File Offset: 0x0000925F
		public static void Csharp2Cpp(RailCrashInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailCrashInfo_exception_type_set(ptr, (int)data.exception_type);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0000B06D File Offset: 0x0000926D
		public static void Cpp2Csharp(IntPtr ptr, RailDirtyWordsCheckResult ret)
		{
			ret.dirty_type = (EnumRailDirtyWordsType)RAIL_API_PINVOKE.RailDirtyWordsCheckResult_dirty_type_get(ptr);
			ret.replace_string = (string)UTF8Marshaler.GetInstance("").MarshalNativeToManaged(RAIL_API_PINVOKE.RailDirtyWordsCheckResult_replace_string_get(ptr));
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0000B09B File Offset: 0x0000929B
		public static void Csharp2Cpp(RailDirtyWordsCheckResult data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDirtyWordsCheckResult_dirty_type_set(ptr, (int)data.dirty_type);
			RAIL_API_PINVOKE.RailDirtyWordsCheckResult_replace_string_set(ptr, data.replace_string);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0000B0B5 File Offset: 0x000092B5
		public static void Cpp2Csharp(IntPtr ptr, RailDiscountInfo ret)
		{
			ret.type = (PurchaseProductDiscountType)RAIL_API_PINVOKE.RailDiscountInfo_type_get(ptr);
			ret.start_time = RAIL_API_PINVOKE.RailDiscountInfo_start_time_get(ptr);
			ret.off = RAIL_API_PINVOKE.RailDiscountInfo_off_get(ptr);
			ret.discount_price = RAIL_API_PINVOKE.RailDiscountInfo_discount_price_get(ptr);
			ret.end_time = RAIL_API_PINVOKE.RailDiscountInfo_end_time_get(ptr);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0000B0F3 File Offset: 0x000092F3
		public static void Csharp2Cpp(RailDiscountInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDiscountInfo_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RailDiscountInfo_start_time_set(ptr, data.start_time);
			RAIL_API_PINVOKE.RailDiscountInfo_off_set(ptr, data.off);
			RAIL_API_PINVOKE.RailDiscountInfo_discount_price_set(ptr, data.discount_price);
			RAIL_API_PINVOKE.RailDiscountInfo_end_time_set(ptr, data.end_time);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0000B131 File Offset: 0x00009331
		public static void Cpp2Csharp(IntPtr ptr, RailDlcID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailDlcID_get_id(ptr);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0000B13F File Offset: 0x0000933F
		public static void Csharp2Cpp(RailDlcID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDlcID_set_id(ptr, data.id_);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0000B150 File Offset: 0x00009350
		public static void Cpp2Csharp(IntPtr ptr, RailDlcInfo ret)
		{
			ret.original_price = RAIL_API_PINVOKE.RailDlcInfo_original_price_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailDlcInfo_dlc_id_get(ptr), ret.dlc_id);
			ret.description = RAIL_API_PINVOKE.RailDlcInfo_description_get(ptr);
			ret.discount_price = RAIL_API_PINVOKE.RailDlcInfo_discount_price_get(ptr);
			ret.version = RAIL_API_PINVOKE.RailDlcInfo_version_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailDlcInfo_game_id_get(ptr), ret.game_id);
			ret.name = RAIL_API_PINVOKE.RailDlcInfo_name_get(ptr);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0000B1BC File Offset: 0x000093BC
		public static void Csharp2Cpp(RailDlcInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDlcInfo_original_price_set(ptr, data.original_price);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.RailDlcInfo_dlc_id_get(ptr));
			RAIL_API_PINVOKE.RailDlcInfo_description_set(ptr, data.description);
			RAIL_API_PINVOKE.RailDlcInfo_discount_price_set(ptr, data.discount_price);
			RAIL_API_PINVOKE.RailDlcInfo_version_set(ptr, data.version);
			RailConverter.Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.RailDlcInfo_game_id_get(ptr));
			RAIL_API_PINVOKE.RailDlcInfo_name_set(ptr, data.name);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0000B227 File Offset: 0x00009427
		public static void Cpp2Csharp(IntPtr ptr, RailDlcInstallProgress ret)
		{
			ret.progress = RAIL_API_PINVOKE.RailDlcInstallProgress_progress_get(ptr);
			ret.finished_bytes = RAIL_API_PINVOKE.RailDlcInstallProgress_finished_bytes_get(ptr);
			ret.total_bytes = RAIL_API_PINVOKE.RailDlcInstallProgress_total_bytes_get(ptr);
			ret.speed = RAIL_API_PINVOKE.RailDlcInstallProgress_speed_get(ptr);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0000B259 File Offset: 0x00009459
		public static void Csharp2Cpp(RailDlcInstallProgress data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDlcInstallProgress_progress_set(ptr, data.progress);
			RAIL_API_PINVOKE.RailDlcInstallProgress_finished_bytes_set(ptr, data.finished_bytes);
			RAIL_API_PINVOKE.RailDlcInstallProgress_total_bytes_set(ptr, data.total_bytes);
			RAIL_API_PINVOKE.RailDlcInstallProgress_speed_set(ptr, data.speed);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0000B28B File Offset: 0x0000948B
		public static void Cpp2Csharp(IntPtr ptr, RailDlcOwned ret)
		{
			ret.is_owned = RAIL_API_PINVOKE.RailDlcOwned_is_owned_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailDlcOwned_dlc_id_get(ptr), ret.dlc_id);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0000B2AA File Offset: 0x000094AA
		public static void Csharp2Cpp(RailDlcOwned data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDlcOwned_is_owned_set(ptr, data.is_owned);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.RailDlcOwned_dlc_id_get(ptr));
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, RailFinalize ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(RailFinalize data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0000B2C9 File Offset: 0x000094C9
		public static void Cpp2Csharp(IntPtr ptr, RailFriendInfo ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendInfo_friend_rail_id_get(ptr), ret.friend_rail_id);
			ret.friend_type = (EnumRailFriendType)RAIL_API_PINVOKE.RailFriendInfo_friend_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendInfo_online_state_get(ptr), ret.online_state);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0000B2F9 File Offset: 0x000094F9
		public static void Csharp2Cpp(RailFriendInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.friend_rail_id, RAIL_API_PINVOKE.RailFriendInfo_friend_rail_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendInfo_friend_type_set(ptr, (int)data.friend_type);
			RailConverter.Csharp2Cpp(data.online_state, RAIL_API_PINVOKE.RailFriendInfo_online_state_get(ptr));
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0000B329 File Offset: 0x00009529
		public static void Cpp2Csharp(IntPtr ptr, RailFriendOnLineState ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendOnLineState_friend_rail_id_get(ptr), ret.friend_rail_id);
			ret.game_define_game_playing_state = RAIL_API_PINVOKE.RailFriendOnLineState_game_define_game_playing_state_get(ptr);
			ret.friend_online_state = (EnumRailPlayerOnLineState)RAIL_API_PINVOKE.RailFriendOnLineState_friend_online_state_get(ptr);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0000B354 File Offset: 0x00009554
		public static void Csharp2Cpp(RailFriendOnLineState data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.friend_rail_id, RAIL_API_PINVOKE.RailFriendOnLineState_friend_rail_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendOnLineState_game_define_game_playing_state_set(ptr, data.game_define_game_playing_state);
			RAIL_API_PINVOKE.RailFriendOnLineState_friend_online_state_set(ptr, (int)data.friend_online_state);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0000B380 File Offset: 0x00009580
		public static void Cpp2Csharp(IntPtr ptr, RailFriendPlayedGameInfo ret)
		{
			ret.in_room = RAIL_API_PINVOKE.RailFriendPlayedGameInfo_in_room_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendPlayedGameInfo_friend_id_get(ptr), ret.friend_id);
			ret.game_server_id = RAIL_API_PINVOKE.RailFriendPlayedGameInfo_game_server_id_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.RailFriendPlayedGameInfo_room_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendPlayedGameInfo_game_id_get(ptr), ret.game_id);
			ret.in_game_server = RAIL_API_PINVOKE.RailFriendPlayedGameInfo_in_game_server_get(ptr);
			ret.friend_played_game_play_state = (RailFriendPlayedGamePlayState)RAIL_API_PINVOKE.RailFriendPlayedGameInfo_friend_played_game_play_state_get(ptr);
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0000B3EC File Offset: 0x000095EC
		public static void Csharp2Cpp(RailFriendPlayedGameInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailFriendPlayedGameInfo_in_room_set(ptr, data.in_room);
			RailConverter.Csharp2Cpp(data.friend_id, RAIL_API_PINVOKE.RailFriendPlayedGameInfo_friend_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendPlayedGameInfo_game_server_id_set(ptr, data.game_server_id);
			RAIL_API_PINVOKE.RailFriendPlayedGameInfo_room_id_set(ptr, data.room_id);
			RailConverter.Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.RailFriendPlayedGameInfo_game_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendPlayedGameInfo_in_game_server_set(ptr, data.in_game_server);
			RAIL_API_PINVOKE.RailFriendPlayedGameInfo_friend_played_game_play_state_set(ptr, (int)data.friend_played_game_play_state);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0000B457 File Offset: 0x00009657
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsAddFriendRequest ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsAddFriendRequest_target_rail_id_get(ptr), ret.target_rail_id);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0000B46A File Offset: 0x0000966A
		public static void Csharp2Cpp(RailFriendsAddFriendRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.target_rail_id, RAIL_API_PINVOKE.RailFriendsAddFriendRequest_target_rail_id_get(ptr));
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0000B47D File Offset: 0x0000967D
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsAddFriendResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsAddFriendResult_target_rail_id_get(ptr), ret.target_rail_id);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0000B497 File Offset: 0x00009697
		public static void Csharp2Cpp(RailFriendsAddFriendResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.target_rail_id, RAIL_API_PINVOKE.RailFriendsAddFriendResult_target_rail_id_get(ptr));
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsClearMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(RailFriendsClearMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0000B4B1 File Offset: 0x000096B1
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsGetInviteCommandLine ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_friend_id_get(ptr), ret.friend_id);
			ret.invite_command_line = RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_invite_command_line_get(ptr);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0000B4D7 File Offset: 0x000096D7
		public static void Csharp2Cpp(RailFriendsGetInviteCommandLine data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friend_id, RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_friend_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_invite_command_line_set(ptr, data.invite_command_line);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0000B4FD File Offset: 0x000096FD
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsGetMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_id_get(ptr), ret.friend_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_kvs_get(ptr), ret.friend_kvs);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0000B528 File Offset: 0x00009728
		public static void Csharp2Cpp(RailFriendsGetMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friend_id, RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_id_get(ptr));
			RailConverter.Csharp2Cpp(data.friend_kvs, RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_kvs_get(ptr));
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsListChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(RailFriendsListChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0000B553 File Offset: 0x00009753
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsOnlineStateChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsOnlineStateChanged_friend_online_state_get(ptr), ret.friend_online_state);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0000B56D File Offset: 0x0000976D
		public static void Csharp2Cpp(RailFriendsOnlineStateChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friend_online_state, RAIL_API_PINVOKE.RailFriendsOnlineStateChanged_friend_online_state_get(ptr));
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0000B587 File Offset: 0x00009787
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsQueryFriendPlayedGamesResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsQueryFriendPlayedGamesResult_friend_played_games_info_list_get(ptr), ret.friend_played_games_info_list);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0000B5A1 File Offset: 0x000097A1
		public static void Csharp2Cpp(RailFriendsQueryFriendPlayedGamesResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friend_played_games_info_list, RAIL_API_PINVOKE.RailFriendsQueryFriendPlayedGamesResult_friend_played_games_info_list_get(ptr));
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0000B5BB File Offset: 0x000097BB
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsQueryPlayedWithFriendsGamesResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsGamesResult_played_with_friends_game_list_get(ptr), ret.played_with_friends_game_list);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0000B5D5 File Offset: 0x000097D5
		public static void Csharp2Cpp(RailFriendsQueryPlayedWithFriendsGamesResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.played_with_friends_game_list, RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsGamesResult_played_with_friends_game_list_get(ptr));
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0000B5EF File Offset: 0x000097EF
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsQueryPlayedWithFriendsListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsListResult_played_with_friends_list_get(ptr), ret.played_with_friends_list);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0000B609 File Offset: 0x00009809
		public static void Csharp2Cpp(RailFriendsQueryPlayedWithFriendsListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.played_with_friends_list, RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsListResult_played_with_friends_list_get(ptr));
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0000B623 File Offset: 0x00009823
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsQueryPlayedWithFriendsTimeResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsTimeResult_played_with_friends_time_list_get(ptr), ret.played_with_friends_time_list);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0000B63D File Offset: 0x0000983D
		public static void Csharp2Cpp(RailFriendsQueryPlayedWithFriendsTimeResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.played_with_friends_time_list, RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsTimeResult_played_with_friends_time_list_get(ptr));
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsReportPlayedWithUserListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(RailFriendsReportPlayedWithUserListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsSetMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(RailFriendsSetMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0000B657 File Offset: 0x00009857
		public static void Cpp2Csharp(IntPtr ptr, RailGameDefineGamePlayingState ret)
		{
			ret.state_name_zh_cn = RAIL_API_PINVOKE.RailGameDefineGamePlayingState_state_name_zh_cn_get(ptr);
			ret.state_name_en_us = RAIL_API_PINVOKE.RailGameDefineGamePlayingState_state_name_en_us_get(ptr);
			ret.game_define_game_playing_state = RAIL_API_PINVOKE.RailGameDefineGamePlayingState_game_define_game_playing_state_get(ptr);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0000B67D File Offset: 0x0000987D
		public static void Csharp2Cpp(RailGameDefineGamePlayingState data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailGameDefineGamePlayingState_state_name_zh_cn_set(ptr, data.state_name_zh_cn);
			RAIL_API_PINVOKE.RailGameDefineGamePlayingState_state_name_en_us_set(ptr, data.state_name_en_us);
			RAIL_API_PINVOKE.RailGameDefineGamePlayingState_game_define_game_playing_state_set(ptr, data.game_define_game_playing_state);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0000B6A3 File Offset: 0x000098A3
		public static void Cpp2Csharp(IntPtr ptr, RailGameID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailGameID_get_id(ptr);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0000B6B1 File Offset: 0x000098B1
		public static void Csharp2Cpp(RailGameID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailGameID_set_id(ptr, data.id_);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0000B6BF File Offset: 0x000098BF
		public static void Cpp2Csharp(IntPtr ptr, RailGetImageDataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.image_data = RAIL_API_PINVOKE.RailGetImageDataResult_image_data_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGetImageDataResult_image_data_descriptor_get(ptr), ret.image_data_descriptor);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0000B6E5 File Offset: 0x000098E5
		public static void Csharp2Cpp(RailGetImageDataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailGetImageDataResult_image_data_set(ptr, data.image_data);
			RailConverter.Csharp2Cpp(data.image_data_descriptor, RAIL_API_PINVOKE.RailGetImageDataResult_image_data_descriptor_get(ptr));
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0000B70B File Offset: 0x0000990B
		public static void Cpp2Csharp(IntPtr ptr, RailHttpSessionResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.http_response_data = RAIL_API_PINVOKE.RailHttpSessionResponse_http_response_data_get(ptr);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0000B720 File Offset: 0x00009920
		public static void Csharp2Cpp(RailHttpSessionResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailHttpSessionResponse_http_response_data_set(ptr, data.http_response_data);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0000B735 File Offset: 0x00009935
		public static void Cpp2Csharp(IntPtr ptr, RailID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailID_get_id(ptr);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0000B743 File Offset: 0x00009943
		public static void Csharp2Cpp(RailID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailID_set_id(ptr, data.id_);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0000B751 File Offset: 0x00009951
		public static void Cpp2Csharp(IntPtr ptr, RailImageDataDescriptor ret)
		{
			ret.pixel_format = (EnumRailImagePixelFormat)RAIL_API_PINVOKE.RailImageDataDescriptor_pixel_format_get(ptr);
			ret.image_height = RAIL_API_PINVOKE.RailImageDataDescriptor_image_height_get(ptr);
			ret.stride_in_bytes = RAIL_API_PINVOKE.RailImageDataDescriptor_stride_in_bytes_get(ptr);
			ret.image_width = RAIL_API_PINVOKE.RailImageDataDescriptor_image_width_get(ptr);
			ret.bits_per_pixel = RAIL_API_PINVOKE.RailImageDataDescriptor_bits_per_pixel_get(ptr);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0000B78F File Offset: 0x0000998F
		public static void Csharp2Cpp(RailImageDataDescriptor data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailImageDataDescriptor_pixel_format_set(ptr, (int)data.pixel_format);
			RAIL_API_PINVOKE.RailImageDataDescriptor_image_height_set(ptr, data.image_height);
			RAIL_API_PINVOKE.RailImageDataDescriptor_stride_in_bytes_set(ptr, data.stride_in_bytes);
			RAIL_API_PINVOKE.RailImageDataDescriptor_image_width_set(ptr, data.image_width);
			RAIL_API_PINVOKE.RailImageDataDescriptor_bits_per_pixel_set(ptr, data.bits_per_pixel);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0000B7CD File Offset: 0x000099CD
		public static void Cpp2Csharp(IntPtr ptr, RailIMEHelperTextInputSelectedResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.content = RAIL_API_PINVOKE.RailIMEHelperTextInputSelectedResult_content_get(ptr);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0000B7E2 File Offset: 0x000099E2
		public static void Csharp2Cpp(RailIMEHelperTextInputSelectedResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailIMEHelperTextInputSelectedResult_content_set(ptr, data.content);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0000B7F7 File Offset: 0x000099F7
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchaseFinishOrderResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = RAIL_API_PINVOKE.RailInGamePurchaseFinishOrderResponse_order_id_get(ptr);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0000B80C File Offset: 0x00009A0C
		public static void Csharp2Cpp(RailInGamePurchaseFinishOrderResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGamePurchaseFinishOrderResponse_order_id_set(ptr, data.order_id);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0000B821 File Offset: 0x00009A21
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchasePurchaseProductsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_order_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_delivered_products_get(ptr), ret.delivered_products);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0000B847 File Offset: 0x00009A47
		public static void Csharp2Cpp(RailInGamePurchasePurchaseProductsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_order_id_set(ptr, data.order_id);
			RailConverter.Csharp2Cpp(data.delivered_products, RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_delivered_products_get(ptr));
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0000B86D File Offset: 0x00009A6D
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchasePurchaseProductsToAssetsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_delivered_assets_get(ptr), ret.delivered_assets);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0000B893 File Offset: 0x00009A93
		public static void Csharp2Cpp(RailInGamePurchasePurchaseProductsToAssetsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_set(ptr, data.order_id);
			RailConverter.Csharp2Cpp(data.delivered_assets, RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_delivered_assets_get(ptr));
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0000B8B9 File Offset: 0x00009AB9
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchaseRequestAllProductsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchaseRequestAllProductsResponse_all_products_get(ptr), ret.all_products);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0000B8D3 File Offset: 0x00009AD3
		public static void Csharp2Cpp(RailInGamePurchaseRequestAllProductsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.all_products, RAIL_API_PINVOKE.RailInGamePurchaseRequestAllProductsResponse_all_products_get(ptr));
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0000B8ED File Offset: 0x00009AED
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchaseRequestAllPurchasableProductsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get(ptr), ret.purchasable_products);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0000B907 File Offset: 0x00009B07
		public static void Csharp2Cpp(RailInGamePurchaseRequestAllPurchasableProductsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.purchasable_products, RAIL_API_PINVOKE.RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get(ptr));
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0000B921 File Offset: 0x00009B21
		public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchasePayWindowClosed ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowClosed_order_id_get(ptr);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0000B936 File Offset: 0x00009B36
		public static void Csharp2Cpp(RailInGameStorePurchasePayWindowClosed data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowClosed_order_id_set(ptr, data.order_id);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0000B94B File Offset: 0x00009B4B
		public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchasePayWindowDisplayed ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowDisplayed_order_id_get(ptr);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0000B960 File Offset: 0x00009B60
		public static void Csharp2Cpp(RailInGameStorePurchasePayWindowDisplayed data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowDisplayed_order_id_set(ptr, data.order_id);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0000B975 File Offset: 0x00009B75
		public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchaseResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = RAIL_API_PINVOKE.RailInGameStorePurchaseResult_order_id_get(ptr);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0000B98A File Offset: 0x00009B8A
		public static void Csharp2Cpp(RailInGameStorePurchaseResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGameStorePurchaseResult_order_id_set(ptr, data.order_id);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0000B99F File Offset: 0x00009B9F
		public static void Cpp2Csharp(IntPtr ptr, RailInviteOptions ret)
		{
			ret.additional_message = RAIL_API_PINVOKE.RailInviteOptions_additional_message_get(ptr);
			ret.expire_time = RAIL_API_PINVOKE.RailInviteOptions_expire_time_get(ptr);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailInviteOptions_invite_type_get(ptr);
			ret.need_respond_in_game = RAIL_API_PINVOKE.RailInviteOptions_need_respond_in_game_get(ptr);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		public static void Csharp2Cpp(RailInviteOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailInviteOptions_additional_message_set(ptr, data.additional_message);
			RAIL_API_PINVOKE.RailInviteOptions_expire_time_set(ptr, data.expire_time);
			RAIL_API_PINVOKE.RailInviteOptions_invite_type_set(ptr, (int)data.invite_type);
			RAIL_API_PINVOKE.RailInviteOptions_need_respond_in_game_set(ptr, data.need_respond_in_game);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0000BA03 File Offset: 0x00009C03
		public static void Cpp2Csharp(IntPtr ptr, RailKeyValue ret)
		{
			ret.value = RAIL_API_PINVOKE.RailKeyValue_value_get(ptr);
			ret.key = RAIL_API_PINVOKE.RailKeyValue_key_get(ptr);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0000BA1D File Offset: 0x00009C1D
		public static void Csharp2Cpp(RailKeyValue data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailKeyValue_value_set(ptr, data.value);
			RAIL_API_PINVOKE.RailKeyValue_key_set(ptr, data.key);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0000BA37 File Offset: 0x00009C37
		public static void Cpp2Csharp(IntPtr ptr, RailKeyValueResult ret)
		{
			ret.error_code = (RailResult)RAIL_API_PINVOKE.RailKeyValueResult_error_code_get(ptr);
			ret.value = RAIL_API_PINVOKE.RailKeyValueResult_value_get(ptr);
			ret.key = RAIL_API_PINVOKE.RailKeyValueResult_key_get(ptr);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0000BA5D File Offset: 0x00009C5D
		public static void Csharp2Cpp(RailKeyValueResult data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailKeyValueResult_error_code_set(ptr, (int)data.error_code);
			RAIL_API_PINVOKE.RailKeyValueResult_value_set(ptr, data.value);
			RAIL_API_PINVOKE.RailKeyValueResult_key_set(ptr, data.key);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0000BA83 File Offset: 0x00009C83
		public static void Cpp2Csharp(IntPtr ptr, RailListStreamFileOption ret)
		{
			ret.num_files = RAIL_API_PINVOKE.RailListStreamFileOption_num_files_get(ptr);
			ret.start_index = RAIL_API_PINVOKE.RailListStreamFileOption_start_index_get(ptr);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0000BA9D File Offset: 0x00009C9D
		public static void Csharp2Cpp(RailListStreamFileOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailListStreamFileOption_num_files_set(ptr, data.num_files);
			RAIL_API_PINVOKE.RailListStreamFileOption_start_index_set(ptr, data.start_index);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public static void Cpp2Csharp(IntPtr ptr, RailNetworkSessionState ret)
		{
			ret.session_error = (RailResult)RAIL_API_PINVOKE.RailNetworkSessionState_session_error_get(ptr);
			ret.remote_port = RAIL_API_PINVOKE.RailNetworkSessionState_remote_port_get(ptr);
			ret.packets_in_send_buffer = RAIL_API_PINVOKE.RailNetworkSessionState_packets_in_send_buffer_get(ptr);
			ret.is_connecting = RAIL_API_PINVOKE.RailNetworkSessionState_is_connecting_get(ptr);
			ret.bytes_in_send_buffer = RAIL_API_PINVOKE.RailNetworkSessionState_bytes_in_send_buffer_get(ptr);
			ret.is_using_relay = RAIL_API_PINVOKE.RailNetworkSessionState_is_using_relay_get(ptr);
			ret.is_connection_active = RAIL_API_PINVOKE.RailNetworkSessionState_is_connection_active_get(ptr);
			ret.remote_ip = RAIL_API_PINVOKE.RailNetworkSessionState_remote_ip_get(ptr);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0000BB28 File Offset: 0x00009D28
		public static void Csharp2Cpp(RailNetworkSessionState data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailNetworkSessionState_session_error_set(ptr, (int)data.session_error);
			RAIL_API_PINVOKE.RailNetworkSessionState_remote_port_set(ptr, data.remote_port);
			RAIL_API_PINVOKE.RailNetworkSessionState_packets_in_send_buffer_set(ptr, data.packets_in_send_buffer);
			RAIL_API_PINVOKE.RailNetworkSessionState_is_connecting_set(ptr, data.is_connecting);
			RAIL_API_PINVOKE.RailNetworkSessionState_bytes_in_send_buffer_set(ptr, data.bytes_in_send_buffer);
			RAIL_API_PINVOKE.RailNetworkSessionState_is_using_relay_set(ptr, data.is_using_relay);
			RAIL_API_PINVOKE.RailNetworkSessionState_is_connection_active_set(ptr, data.is_connection_active);
			RAIL_API_PINVOKE.RailNetworkSessionState_remote_ip_set(ptr, data.remote_ip);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0000BB95 File Offset: 0x00009D95
		public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByGameServer ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.commandline_info = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_commandline_info_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get(ptr), ret.gameserver_railid);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0000BBBB File Offset: 0x00009DBB
		public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByGameServer data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_commandline_info_set(ptr, data.commandline_info);
			RailConverter.Csharp2Cpp(data.gameserver_railid, RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get(ptr));
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0000BBE1 File Offset: 0x00009DE1
		public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByRoom ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.commandline_info = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_commandline_info_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_room_id_get(ptr);
			ret.zone_id = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_zone_id_get(ptr);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0000BC0E File Offset: 0x00009E0E
		public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByRoom data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_commandline_info_set(ptr, data.commandline_info);
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_zone_id_set(ptr, data.zone_id);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0000BC3B File Offset: 0x00009E3B
		public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByUser ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get(ptr), ret.rail_id_to_join);
			ret.commandline_info = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_commandline_info_get(ptr);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0000BC61 File Offset: 0x00009E61
		public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByUser data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.rail_id_to_join, RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get(ptr));
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_commandline_info_set(ptr, data.commandline_info);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0000BC87 File Offset: 0x00009E87
		public static void Cpp2Csharp(IntPtr ptr, RailPlayedWithFriendsGameItem ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlayedWithFriendsGameItem_game_ids_get(ptr), ret.game_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlayedWithFriendsGameItem_rail_id_get(ptr), ret.rail_id);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0000BCAB File Offset: 0x00009EAB
		public static void Csharp2Cpp(RailPlayedWithFriendsGameItem data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.game_ids, RAIL_API_PINVOKE.RailPlayedWithFriendsGameItem_game_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.RailPlayedWithFriendsGameItem_rail_id_get(ptr));
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0000BCCF File Offset: 0x00009ECF
		public static void Cpp2Csharp(IntPtr ptr, RailPlayedWithFriendsTimeItem ret)
		{
			ret.play_time = RAIL_API_PINVOKE.RailPlayedWithFriendsTimeItem_play_time_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlayedWithFriendsTimeItem_rail_id_get(ptr), ret.rail_id);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0000BCEE File Offset: 0x00009EEE
		public static void Csharp2Cpp(RailPlayedWithFriendsTimeItem data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailPlayedWithFriendsTimeItem_play_time_set(ptr, data.play_time);
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.RailPlayedWithFriendsTimeItem_rail_id_get(ptr));
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0000BD0D File Offset: 0x00009F0D
		public static void Cpp2Csharp(IntPtr ptr, RailProductItem ret)
		{
			ret.product_id = RAIL_API_PINVOKE.RailProductItem_product_id_get(ptr);
			ret.quantity = RAIL_API_PINVOKE.RailProductItem_quantity_get(ptr);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0000BD27 File Offset: 0x00009F27
		public static void Csharp2Cpp(RailProductItem data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailProductItem_product_id_set(ptr, data.product_id);
			RAIL_API_PINVOKE.RailProductItem_quantity_set(ptr, data.quantity);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0000BD44 File Offset: 0x00009F44
		public static void Cpp2Csharp(IntPtr ptr, RailPublishFileToUserSpaceOption ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_key_value_get(ptr), ret.key_value);
			ret.description = RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_description_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_tags_get(ptr), ret.tags);
			ret.level = (EnumRailSpaceWorkShareLevel)RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_level_get(ptr);
			ret.version = RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_version_get(ptr);
			ret.preview_path_filename = RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_preview_path_filename_get(ptr);
			ret.type = (EnumRailSpaceWorkType)RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_type_get(ptr);
			ret.space_work_name = RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_space_work_name_get(ptr);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0000BDBC File Offset: 0x00009FBC
		public static void Csharp2Cpp(RailPublishFileToUserSpaceOption data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_key_value_get(ptr));
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_description_set(ptr, data.description);
			RailConverter.Csharp2Cpp(data.tags, RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_tags_get(ptr));
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_level_set(ptr, (int)data.level);
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_version_set(ptr, data.version);
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_preview_path_filename_set(ptr, data.preview_path_filename);
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_space_work_name_set(ptr, data.space_work_name);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0000BE33 File Offset: 0x0000A033
		public static void Cpp2Csharp(IntPtr ptr, RailPurchaseProductExtraInfo ret)
		{
			ret.bundle_rule = RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_bundle_rule_get(ptr);
			ret.exchange_rule = RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_exchange_rule_get(ptr);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0000BE4D File Offset: 0x0000A04D
		public static void Csharp2Cpp(RailPurchaseProductExtraInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_bundle_rule_set(ptr, data.bundle_rule);
			RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_exchange_rule_set(ptr, data.exchange_rule);
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0000BE68 File Offset: 0x0000A068
		public static void Cpp2Csharp(IntPtr ptr, RailPurchaseProductInfo ret)
		{
			ret.category = RAIL_API_PINVOKE.RailPurchaseProductInfo_category_get(ptr);
			ret.original_price = RAIL_API_PINVOKE.RailPurchaseProductInfo_original_price_get(ptr);
			ret.description = RAIL_API_PINVOKE.RailPurchaseProductInfo_description_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPurchaseProductInfo_discount_get(ptr), ret.discount);
			ret.is_purchasable = RAIL_API_PINVOKE.RailPurchaseProductInfo_is_purchasable_get(ptr);
			ret.name = RAIL_API_PINVOKE.RailPurchaseProductInfo_name_get(ptr);
			ret.currency_type = RAIL_API_PINVOKE.RailPurchaseProductInfo_currency_type_get(ptr);
			ret.product_thumbnail = RAIL_API_PINVOKE.RailPurchaseProductInfo_product_thumbnail_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPurchaseProductInfo_extra_info_get(ptr), ret.extra_info);
			ret.product_id = RAIL_API_PINVOKE.RailPurchaseProductInfo_product_id_get(ptr);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public static void Csharp2Cpp(RailPurchaseProductInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailPurchaseProductInfo_category_set(ptr, data.category);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_original_price_set(ptr, data.original_price);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_description_set(ptr, data.description);
			RailConverter.Csharp2Cpp(data.discount, RAIL_API_PINVOKE.RailPurchaseProductInfo_discount_get(ptr));
			RAIL_API_PINVOKE.RailPurchaseProductInfo_is_purchasable_set(ptr, data.is_purchasable);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_name_set(ptr, data.name);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_currency_type_set(ptr, data.currency_type);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_product_thumbnail_set(ptr, data.product_thumbnail);
			RailConverter.Csharp2Cpp(data.extra_info, RAIL_API_PINVOKE.RailPurchaseProductInfo_extra_info_get(ptr));
			RAIL_API_PINVOKE.RailPurchaseProductInfo_product_id_set(ptr, data.product_id);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0000BF88 File Offset: 0x0000A188
		public static void Cpp2Csharp(IntPtr ptr, RailQueryWorkFileOptions ret)
		{
			ret.with_url = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_url_get(ptr);
			ret.with_uploader_ids = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_uploader_ids_get(ptr);
			ret.with_vote_detail = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_vote_detail_get(ptr);
			ret.with_preview_url = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_preview_url_get(ptr);
			ret.with_description = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_description_get(ptr);
			ret.query_total_only = RAIL_API_PINVOKE.RailQueryWorkFileOptions_query_total_only_get(ptr);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public static void Csharp2Cpp(RailQueryWorkFileOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_url_set(ptr, data.with_url);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_uploader_ids_set(ptr, data.with_uploader_ids);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_vote_detail_set(ptr, data.with_vote_detail);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_preview_url_set(ptr, data.with_preview_url);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_description_set(ptr, data.with_description);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_query_total_only_set(ptr, data.query_total_only);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0000C035 File Offset: 0x0000A235
		public static void Cpp2Csharp(IntPtr ptr, RailSessionTicket ret)
		{
			ret.ticket = RAIL_API_PINVOKE.RailSessionTicket_ticket_get(ptr);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0000C043 File Offset: 0x0000A243
		public static void Csharp2Cpp(RailSessionTicket data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSessionTicket_ticket_set(ptr, data.ticket);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0000C051 File Offset: 0x0000A251
		public static void Cpp2Csharp(IntPtr ptr, RailShowChatWindowWithFriendResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.is_show = RAIL_API_PINVOKE.RailShowChatWindowWithFriendResult_is_show_get(ptr);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0000C066 File Offset: 0x0000A266
		public static void Csharp2Cpp(RailShowChatWindowWithFriendResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailShowChatWindowWithFriendResult_is_show_set(ptr, data.is_show);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0000C07B File Offset: 0x0000A27B
		public static void Cpp2Csharp(IntPtr ptr, RailShowUserHomepageWindowResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.is_show = RAIL_API_PINVOKE.RailShowUserHomepageWindowResult_is_show_get(ptr);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0000C090 File Offset: 0x0000A290
		public static void Csharp2Cpp(RailShowUserHomepageWindowResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailShowUserHomepageWindowResult_is_show_set(ptr, data.is_show);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0000C0A5 File Offset: 0x0000A2A5
		public static void Cpp2Csharp(IntPtr ptr, RailSmallObjectDownloadInfo ret)
		{
			ret.index = RAIL_API_PINVOKE.RailSmallObjectDownloadInfo_index_get(ptr);
			ret.result = (RailResult)RAIL_API_PINVOKE.RailSmallObjectDownloadInfo_result_get(ptr);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0000C0BF File Offset: 0x0000A2BF
		public static void Csharp2Cpp(RailSmallObjectDownloadInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSmallObjectDownloadInfo_index_set(ptr, data.index);
			RAIL_API_PINVOKE.RailSmallObjectDownloadInfo_result_set(ptr, (int)data.result);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0000C0D9 File Offset: 0x0000A2D9
		public static void Cpp2Csharp(IntPtr ptr, RailSmallObjectDownloadResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSmallObjectDownloadResult_download_infos_get(ptr), ret.download_infos);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
		public static void Csharp2Cpp(RailSmallObjectDownloadResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.download_infos, RAIL_API_PINVOKE.RailSmallObjectDownloadResult_download_infos_get(ptr));
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0000C10D File Offset: 0x0000A30D
		public static void Cpp2Csharp(IntPtr ptr, RailSmallObjectState ret)
		{
			ret.update_state = (EnumRailSmallObjectUpdateState)RAIL_API_PINVOKE.RailSmallObjectState_update_state_get(ptr);
			ret.index = RAIL_API_PINVOKE.RailSmallObjectState_index_get(ptr);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0000C127 File Offset: 0x0000A327
		public static void Csharp2Cpp(RailSmallObjectState data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSmallObjectState_update_state_set(ptr, (int)data.update_state);
			RAIL_API_PINVOKE.RailSmallObjectState_index_set(ptr, data.index);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0000C141 File Offset: 0x0000A341
		public static void Cpp2Csharp(IntPtr ptr, RailSmallObjectStateQueryResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSmallObjectStateQueryResult_objects_state_get(ptr), ret.objects_state);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0000C15B File Offset: 0x0000A35B
		public static void Csharp2Cpp(RailSmallObjectStateQueryResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.objects_state, RAIL_API_PINVOKE.RailSmallObjectStateQueryResult_objects_state_get(ptr));
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0000C178 File Offset: 0x0000A378
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkDescriptor ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_vote_details_get(ptr), ret.vote_details);
			ret.description = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_description_get(ptr);
			ret.preview_url = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_preview_url_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_id_get(ptr), ret.id);
			ret.detail_url = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_detail_url_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_uploader_ids_get(ptr), ret.uploader_ids);
			ret.name = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_name_get(ptr);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		public static void Csharp2Cpp(RailSpaceWorkDescriptor data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.vote_details, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_vote_details_get(ptr));
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_description_set(ptr, data.description);
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_preview_url_set(ptr, data.preview_url);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_id_get(ptr));
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_detail_url_set(ptr, data.detail_url);
			RailConverter.Csharp2Cpp(data.uploader_ids, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_uploader_ids_get(ptr));
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_name_set(ptr, data.name);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0000C258 File Offset: 0x0000A458
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkFilter ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_classes_get(ptr), ret.classes);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_type_get(ptr), ret.type);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_collector_list_get(ptr), ret.collector_list);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_subscriber_list_get(ptr), ret.subscriber_list);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_creator_list_get(ptr), ret.creator_list);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public static void Csharp2Cpp(RailSpaceWorkFilter data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.classes, RAIL_API_PINVOKE.RailSpaceWorkFilter_classes_get(ptr));
			RailConverter.Csharp2Cpp(data.type, RAIL_API_PINVOKE.RailSpaceWorkFilter_type_get(ptr));
			RailConverter.Csharp2Cpp(data.collector_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_collector_list_get(ptr));
			RailConverter.Csharp2Cpp(data.subscriber_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_subscriber_list_get(ptr));
			RailConverter.Csharp2Cpp(data.creator_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_creator_list_get(ptr));
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0000C320 File Offset: 0x0000A520
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkSearchFilter ret)
		{
			ret.search_text = RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_search_text_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_metadata_get(ptr), ret.required_metadata);
			ret.match_all_required_metadata = RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_match_all_required_metadata_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_tags_get(ptr), ret.required_tags);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_metadata_get(ptr), ret.excluded_metadata);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_tags_get(ptr), ret.excluded_tags);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0000C38C File Offset: 0x0000A58C
		public static void Csharp2Cpp(RailSpaceWorkSearchFilter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_search_text_set(ptr, data.search_text);
			RailConverter.Csharp2Cpp(data.required_metadata, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_metadata_get(ptr));
			RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_match_all_required_metadata_set(ptr, data.match_all_required_metadata);
			RailConverter.Csharp2Cpp(data.required_tags, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_tags_get(ptr));
			RailConverter.Csharp2Cpp(data.excluded_metadata, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_metadata_get(ptr));
			RailConverter.Csharp2Cpp(data.excluded_tags, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_tags_get(ptr));
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0000C3F5 File Offset: 0x0000A5F5
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkSyncProgress ret)
		{
			ret.progress = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_progress_get(ptr);
			ret.finished_bytes = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_finished_bytes_get(ptr);
			ret.total_bytes = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_total_bytes_get(ptr);
			ret.current_state = (EnumRailSpaceWorkSyncState)RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_current_state_get(ptr);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0000C427 File Offset: 0x0000A627
		public static void Csharp2Cpp(RailSpaceWorkSyncProgress data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_progress_set(ptr, data.progress);
			RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_finished_bytes_set(ptr, data.finished_bytes);
			RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_total_bytes_set(ptr, data.total_bytes);
			RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_current_state_set(ptr, (int)data.current_state);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0000C45C File Offset: 0x0000A65C
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkUpdateOptions ret)
		{
			ret.with_my_vote = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_my_vote_get(ptr);
			ret.with_vote_detail = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_vote_detail_get(ptr);
			ret.with_metadata = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_metadata_get(ptr);
			ret.with_detail = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_detail_get(ptr);
			ret.check_has_subscribed = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_subscribed_get(ptr);
			ret.check_has_favorited = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_favorited_get(ptr);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		public static void Csharp2Cpp(RailSpaceWorkUpdateOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_my_vote_set(ptr, data.with_my_vote);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_vote_detail_set(ptr, data.with_vote_detail);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_metadata_set(ptr, data.with_metadata);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_detail_set(ptr, data.with_detail);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_subscribed_set(ptr, data.check_has_subscribed);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_favorited_set(ptr, data.check_has_favorited);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0000C509 File Offset: 0x0000A709
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkVoteDetail ret)
		{
			ret.vote_value = (EnumRailSpaceWorkVoteValue)RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_vote_value_get(ptr);
			ret.voted_players = RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_voted_players_get(ptr);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0000C523 File Offset: 0x0000A723
		public static void Csharp2Cpp(RailSpaceWorkVoteDetail data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_vote_value_set(ptr, (int)data.vote_value);
			RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_voted_players_set(ptr, data.voted_players);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0000C53D File Offset: 0x0000A73D
		public static void Cpp2Csharp(IntPtr ptr, RailStoreOptions ret)
		{
			ret.window_margin_top = RAIL_API_PINVOKE.RailStoreOptions_window_margin_top_get(ptr);
			ret.window_margin_left = RAIL_API_PINVOKE.RailStoreOptions_window_margin_left_get(ptr);
			ret.store_type = (EnumRailStoreType)RAIL_API_PINVOKE.RailStoreOptions_store_type_get(ptr);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0000C563 File Offset: 0x0000A763
		public static void Csharp2Cpp(RailStoreOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailStoreOptions_window_margin_top_set(ptr, data.window_margin_top);
			RAIL_API_PINVOKE.RailStoreOptions_window_margin_left_set(ptr, data.window_margin_left);
			RAIL_API_PINVOKE.RailStoreOptions_store_type_set(ptr, (int)data.store_type);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0000C589 File Offset: 0x0000A789
		public static void Cpp2Csharp(IntPtr ptr, RailStreamFileInfo ret)
		{
			ret.file_size = RAIL_API_PINVOKE.RailStreamFileInfo_file_size_get(ptr);
			ret.filename = RAIL_API_PINVOKE.RailStreamFileInfo_filename_get(ptr);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
		public static void Csharp2Cpp(RailStreamFileInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailStreamFileInfo_file_size_set(ptr, data.file_size);
			RAIL_API_PINVOKE.RailStreamFileInfo_filename_set(ptr, data.filename);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0000C5BD File Offset: 0x0000A7BD
		public static void Cpp2Csharp(IntPtr ptr, RailStreamFileOption ret)
		{
			ret.open_type = (EnumRailStreamOpenFileType)RAIL_API_PINVOKE.RailStreamFileOption_open_type_get(ptr);
			ret.unavaliabe_when_new_file_writing = RAIL_API_PINVOKE.RailStreamFileOption_unavaliabe_when_new_file_writing_get(ptr);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0000C5D7 File Offset: 0x0000A7D7
		public static void Csharp2Cpp(RailStreamFileOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailStreamFileOption_open_type_set(ptr, (int)data.open_type);
			RAIL_API_PINVOKE.RailStreamFileOption_unavaliabe_when_new_file_writing_set(ptr, data.unavaliabe_when_new_file_writing);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, RailSwitchPlayerSelectedZoneResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(RailSwitchPlayerSelectedZoneResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0000C5F1 File Offset: 0x0000A7F1
		public static void Cpp2Csharp(IntPtr ptr, RailSyncFileOption ret)
		{
			ret.sync_file_not_to_remote = RAIL_API_PINVOKE.RailSyncFileOption_sync_file_not_to_remote_get(ptr);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0000C5FF File Offset: 0x0000A7FF
		public static void Csharp2Cpp(RailSyncFileOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSyncFileOption_sync_file_not_to_remote_set(ptr, data.sync_file_not_to_remote);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0000C60D File Offset: 0x0000A80D
		public static void Cpp2Csharp(IntPtr ptr, RailSystemStateChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.state = (RailSystemState)RAIL_API_PINVOKE.RailSystemStateChanged_state_get(ptr);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0000C622 File Offset: 0x0000A822
		public static void Csharp2Cpp(RailSystemStateChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailSystemStateChanged_state_set(ptr, (int)data.state);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0000C637 File Offset: 0x0000A837
		public static void Cpp2Csharp(IntPtr ptr, RailTextInputResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.content = RAIL_API_PINVOKE.RailTextInputResult_content_get(ptr);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0000C64C File Offset: 0x0000A84C
		public static void Csharp2Cpp(RailTextInputResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailTextInputResult_content_set(ptr, data.content);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0000C664 File Offset: 0x0000A864
		public static void Cpp2Csharp(IntPtr ptr, RailTextInputWindowOption ret)
		{
			ret.enable_multi_line_edit = RAIL_API_PINVOKE.RailTextInputWindowOption_enable_multi_line_edit_get(ptr);
			ret.description = RAIL_API_PINVOKE.RailTextInputWindowOption_description_get(ptr);
			ret.position_top = RAIL_API_PINVOKE.RailTextInputWindowOption_position_top_get(ptr);
			ret.position_left = RAIL_API_PINVOKE.RailTextInputWindowOption_position_left_get(ptr);
			ret.caption_text = RAIL_API_PINVOKE.RailTextInputWindowOption_caption_text_get(ptr);
			ret.show_password_input = RAIL_API_PINVOKE.RailTextInputWindowOption_show_password_input_get(ptr);
			ret.is_min_window = RAIL_API_PINVOKE.RailTextInputWindowOption_is_min_window_get(ptr);
			ret.content_placeholder = RAIL_API_PINVOKE.RailTextInputWindowOption_content_placeholder_get(ptr);
			ret.auto_cancel = RAIL_API_PINVOKE.RailTextInputWindowOption_auto_cancel_get(ptr);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		public static void Csharp2Cpp(RailTextInputWindowOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailTextInputWindowOption_enable_multi_line_edit_set(ptr, data.enable_multi_line_edit);
			RAIL_API_PINVOKE.RailTextInputWindowOption_description_set(ptr, data.description);
			RAIL_API_PINVOKE.RailTextInputWindowOption_position_top_set(ptr, data.position_top);
			RAIL_API_PINVOKE.RailTextInputWindowOption_position_left_set(ptr, data.position_left);
			RAIL_API_PINVOKE.RailTextInputWindowOption_caption_text_set(ptr, data.caption_text);
			RAIL_API_PINVOKE.RailTextInputWindowOption_show_password_input_set(ptr, data.show_password_input);
			RAIL_API_PINVOKE.RailTextInputWindowOption_is_min_window_set(ptr, data.is_min_window);
			RAIL_API_PINVOKE.RailTextInputWindowOption_content_placeholder_set(ptr, data.content_placeholder);
			RAIL_API_PINVOKE.RailTextInputWindowOption_auto_cancel_set(ptr, data.auto_cancel);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0000C759 File Offset: 0x0000A959
		public static void Cpp2Csharp(IntPtr ptr, RailUserPlayedWith ret)
		{
			ret.user_rich_content = RAIL_API_PINVOKE.RailUserPlayedWith_user_rich_content_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUserPlayedWith_rail_id_get(ptr), ret.rail_id);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0000C778 File Offset: 0x0000A978
		public static void Csharp2Cpp(RailUserPlayedWith data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailUserPlayedWith_user_rich_content_set(ptr, data.user_rich_content);
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.RailUserPlayedWith_rail_id_get(ptr));
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0000C797 File Offset: 0x0000A997
		public static void Cpp2Csharp(IntPtr ptr, RailUsersCancelInviteResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailUsersCancelInviteResult_invite_type_get(ptr);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public static void Csharp2Cpp(RailUsersCancelInviteResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailUsersCancelInviteResult_invite_type_set(ptr, (int)data.invite_type);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0000C7C1 File Offset: 0x0000A9C1
		public static void Cpp2Csharp(IntPtr ptr, RailUsersGetInviteDetailResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.command_line = RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_command_line_get(ptr);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_invite_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_inviter_id_get(ptr), ret.inviter_id);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0000C7F3 File Offset: 0x0000A9F3
		public static void Csharp2Cpp(RailUsersGetInviteDetailResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_command_line_set(ptr, data.command_line);
			RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_invite_type_set(ptr, (int)data.invite_type);
			RailConverter.Csharp2Cpp(data.inviter_id, RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_inviter_id_get(ptr));
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0000C825 File Offset: 0x0000AA25
		public static void Cpp2Csharp(IntPtr ptr, RailUsersGetUserLimitsResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersGetUserLimitsResult_user_id_get(ptr), ret.user_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersGetUserLimitsResult_user_limits_get(ptr), ret.user_limits);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0000C850 File Offset: 0x0000AA50
		public static void Csharp2Cpp(RailUsersGetUserLimitsResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.user_id, RAIL_API_PINVOKE.RailUsersGetUserLimitsResult_user_id_get(ptr));
			RailConverter.Csharp2Cpp(data.user_limits, RAIL_API_PINVOKE.RailUsersGetUserLimitsResult_user_limits_get(ptr));
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0000C87B File Offset: 0x0000AA7B
		public static void Cpp2Csharp(IntPtr ptr, RailUsersInfoData ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersInfoData_user_info_list_get(ptr), ret.user_info_list);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0000C895 File Offset: 0x0000AA95
		public static void Csharp2Cpp(RailUsersInfoData data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.user_info_list, RAIL_API_PINVOKE.RailUsersInfoData_user_info_list_get(ptr));
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0000C8AF File Offset: 0x0000AAAF
		public static void Cpp2Csharp(IntPtr ptr, RailUsersInviteJoinGameResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.response_value = (EnumRailUsersInviteResponseType)RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_response_value_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invitee_id_get(ptr), ret.invitee_id);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invite_type_get(ptr);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0000C8E1 File Offset: 0x0000AAE1
		public static void Csharp2Cpp(RailUsersInviteJoinGameResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_response_value_set(ptr, (int)data.response_value);
			RailConverter.Csharp2Cpp(data.invitee_id, RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invitee_id_get(ptr));
			RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invite_type_set(ptr, (int)data.invite_type);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0000C913 File Offset: 0x0000AB13
		public static void Cpp2Csharp(IntPtr ptr, RailUsersInviteUsersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailUsersInviteUsersResult_invite_type_get(ptr);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0000C928 File Offset: 0x0000AB28
		public static void Csharp2Cpp(RailUsersInviteUsersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailUsersInviteUsersResult_invite_type_set(ptr, (int)data.invite_type);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0000C93D File Offset: 0x0000AB3D
		public static void Cpp2Csharp(IntPtr ptr, RailUsersNotifyInviter ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersNotifyInviter_invitee_id_get(ptr), ret.invitee_id);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0000C957 File Offset: 0x0000AB57
		public static void Csharp2Cpp(RailUsersNotifyInviter data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.invitee_id, RAIL_API_PINVOKE.RailUsersNotifyInviter_invitee_id_get(ptr));
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0000C971 File Offset: 0x0000AB71
		public static void Cpp2Csharp(IntPtr ptr, RailUsersRespondInvitation ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersRespondInvitation_original_invite_option_get(ptr), ret.original_invite_option);
			ret.response = (EnumRailUsersInviteResponseType)RAIL_API_PINVOKE.RailUsersRespondInvitation_response_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersRespondInvitation_inviter_id_get(ptr), ret.inviter_id);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		public static void Csharp2Cpp(RailUsersRespondInvitation data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.original_invite_option, RAIL_API_PINVOKE.RailUsersRespondInvitation_original_invite_option_get(ptr));
			RAIL_API_PINVOKE.RailUsersRespondInvitation_response_set(ptr, (int)data.response);
			RailConverter.Csharp2Cpp(data.inviter_id, RAIL_API_PINVOKE.RailUsersRespondInvitation_inviter_id_get(ptr));
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0000C9DF File Offset: 0x0000ABDF
		public static void Cpp2Csharp(IntPtr ptr, RailVoiceCaptureOption ret)
		{
			ret.voice_data_format = (EnumRailVoiceCaptureFormat)RAIL_API_PINVOKE.RailVoiceCaptureOption_voice_data_format_get(ptr);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0000C9ED File Offset: 0x0000ABED
		public static void Csharp2Cpp(RailVoiceCaptureOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailVoiceCaptureOption_voice_data_format_set(ptr, (int)data.voice_data_format);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0000C9FB File Offset: 0x0000ABFB
		public static void Cpp2Csharp(IntPtr ptr, RailVoiceCaptureSpecification ret)
		{
			ret.channels = (EnumRailVoiceCaptureChannel)RAIL_API_PINVOKE.RailVoiceCaptureSpecification_channels_get(ptr);
			ret.samples_per_second = RAIL_API_PINVOKE.RailVoiceCaptureSpecification_samples_per_second_get(ptr);
			ret.bits_per_sample = RAIL_API_PINVOKE.RailVoiceCaptureSpecification_bits_per_sample_get(ptr);
			ret.capture_format = (EnumRailVoiceCaptureFormat)RAIL_API_PINVOKE.RailVoiceCaptureSpecification_capture_format_get(ptr);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0000CA2D File Offset: 0x0000AC2D
		public static void Csharp2Cpp(RailVoiceCaptureSpecification data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailVoiceCaptureSpecification_channels_set(ptr, (int)data.channels);
			RAIL_API_PINVOKE.RailVoiceCaptureSpecification_samples_per_second_set(ptr, data.samples_per_second);
			RAIL_API_PINVOKE.RailVoiceCaptureSpecification_bits_per_sample_set(ptr, data.bits_per_sample);
			RAIL_API_PINVOKE.RailVoiceCaptureSpecification_capture_format_set(ptr, (int)data.capture_format);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0000CA5F File Offset: 0x0000AC5F
		public static void Cpp2Csharp(IntPtr ptr, RailVoiceChannelID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailVoiceChannelID_get_id(ptr);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0000CA6D File Offset: 0x0000AC6D
		public static void Csharp2Cpp(RailVoiceChannelID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailVoiceChannelID_set_id(ptr, data.id_);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0000CA7B File Offset: 0x0000AC7B
		public static void Cpp2Csharp(IntPtr ptr, RailVoiceChannelUserSpeakingState ret)
		{
			ret.speaking_limit = (EnumRailVoiceChannelUserSpeakingLimit)RAIL_API_PINVOKE.RailVoiceChannelUserSpeakingState_speaking_limit_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailVoiceChannelUserSpeakingState_user_id_get(ptr), ret.user_id);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0000CA9A File Offset: 0x0000AC9A
		public static void Csharp2Cpp(RailVoiceChannelUserSpeakingState data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailVoiceChannelUserSpeakingState_speaking_limit_set(ptr, (int)data.speaking_limit);
			RailConverter.Csharp2Cpp(data.user_id, RAIL_API_PINVOKE.RailVoiceChannelUserSpeakingState_user_id_get(ptr));
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0000CAB9 File Offset: 0x0000ACB9
		public static void Cpp2Csharp(IntPtr ptr, RailWindowLayout ret)
		{
			ret.x_margin = RAIL_API_PINVOKE.RailWindowLayout_x_margin_get(ptr);
			ret.y_margin = RAIL_API_PINVOKE.RailWindowLayout_y_margin_get(ptr);
			ret.position_type = (EnumRailNotifyWindowPosition)RAIL_API_PINVOKE.RailWindowLayout_position_type_get(ptr);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0000CADF File Offset: 0x0000ACDF
		public static void Csharp2Cpp(RailWindowLayout data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailWindowLayout_x_margin_set(ptr, data.x_margin);
			RAIL_API_PINVOKE.RailWindowLayout_y_margin_set(ptr, data.y_margin);
			RAIL_API_PINVOKE.RailWindowLayout_position_type_set(ptr, (int)data.position_type);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0000CB05 File Offset: 0x0000AD05
		public static void Cpp2Csharp(IntPtr ptr, RailWindowPosition ret)
		{
			ret.position_top = RAIL_API_PINVOKE.RailWindowPosition_position_top_get(ptr);
			ret.position_left = RAIL_API_PINVOKE.RailWindowPosition_position_left_get(ptr);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0000CB1F File Offset: 0x0000AD1F
		public static void Csharp2Cpp(RailWindowPosition data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailWindowPosition_position_top_set(ptr, data.position_top);
			RAIL_API_PINVOKE.RailWindowPosition_position_left_set(ptr, data.position_left);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0000CB39 File Offset: 0x0000AD39
		public static void Cpp2Csharp(IntPtr ptr, RailZoneID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailZoneID_get_id(ptr);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0000CB47 File Offset: 0x0000AD47
		public static void Csharp2Cpp(RailZoneID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailZoneID_set_id(ptr, data.id_);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, ReloadBrowserResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(ReloadBrowserResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0000CB55 File Offset: 0x0000AD55
		public static void Cpp2Csharp(IntPtr ptr, RequestAllAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RequestAllAssetsFinished_assetinfo_list_get(ptr), ret.assetinfo_list);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0000CB6F File Offset: 0x0000AD6F
		public static void Csharp2Cpp(RequestAllAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.assetinfo_list, RAIL_API_PINVOKE.RequestAllAssetsFinished_assetinfo_list_get(ptr));
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0000CB89 File Offset: 0x0000AD89
		public static void Cpp2Csharp(IntPtr ptr, RequestLeaderboardEntryParam ret)
		{
			ret.range_end = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_end_get(ptr);
			ret.range_start = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_start_get(ptr);
			ret.type = (LeaderboardType)RAIL_API_PINVOKE.RequestLeaderboardEntryParam_type_get(ptr);
			ret.user_coordinate = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_user_coordinate_get(ptr);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0000CBBB File Offset: 0x0000ADBB
		public static void Csharp2Cpp(RequestLeaderboardEntryParam data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_end_set(ptr, data.range_end);
			RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_start_set(ptr, data.range_start);
			RAIL_API_PINVOKE.RequestLeaderboardEntryParam_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RequestLeaderboardEntryParam_user_coordinate_set(ptr, data.user_coordinate);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0000CBED File Offset: 0x0000ADED
		public static void Cpp2Csharp(IntPtr ptr, RoomAllData ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomAllData_room_info_get(ptr), ret.room_info);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0000CC07 File Offset: 0x0000AE07
		public static void Csharp2Cpp(RoomAllData data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.room_info, RAIL_API_PINVOKE.RoomAllData_room_info_get(ptr));
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0000CC21 File Offset: 0x0000AE21
		public static void Cpp2Csharp(IntPtr ptr, RoomDataReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.data_len = RAIL_API_PINVOKE.RoomDataReceived_data_len_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomDataReceived_remote_peer_get(ptr), ret.remote_peer);
			ret.message_type = RAIL_API_PINVOKE.RoomDataReceived_message_type_get(ptr);
			ret.data_buf = RAIL_API_PINVOKE.RoomDataReceived_data_buf_get(ptr);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0000CC5F File Offset: 0x0000AE5F
		public static void Csharp2Cpp(RoomDataReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RoomDataReceived_data_len_set(ptr, data.data_len);
			RailConverter.Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.RoomDataReceived_remote_peer_get(ptr));
			RAIL_API_PINVOKE.RoomDataReceived_message_type_set(ptr, data.message_type);
			RAIL_API_PINVOKE.RoomDataReceived_data_buf_set(ptr, data.data_buf);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		public static void Cpp2Csharp(IntPtr ptr, RoomInfo ret)
		{
			ret.zone_id = RAIL_API_PINVOKE.RoomInfo_zone_id_get(ptr);
			ret.has_password = RAIL_API_PINVOKE.RoomInfo_has_password_get(ptr);
			ret.create_time = RAIL_API_PINVOKE.RoomInfo_create_time_get(ptr);
			ret.max_members = RAIL_API_PINVOKE.RoomInfo_max_members_get(ptr);
			ret.room_name = RAIL_API_PINVOKE.RoomInfo_room_name_get(ptr);
			ret.game_server_rail_id = RAIL_API_PINVOKE.RoomInfo_game_server_rail_id_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.RoomInfo_room_id_get(ptr);
			ret.current_members = RAIL_API_PINVOKE.RoomInfo_current_members_get(ptr);
			ret.is_joinable = RAIL_API_PINVOKE.RoomInfo_is_joinable_get(ptr);
			ret.room_state = (EnumRoomStatus)RAIL_API_PINVOKE.RoomInfo_room_state_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomInfo_room_kvs_get(ptr), ret.room_kvs);
			ret.type = (EnumRoomType)RAIL_API_PINVOKE.RoomInfo_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomInfo_owner_id_get(ptr), ret.owner_id);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0000CD54 File Offset: 0x0000AF54
		public static void Csharp2Cpp(RoomInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomInfo_zone_id_set(ptr, data.zone_id);
			RAIL_API_PINVOKE.RoomInfo_has_password_set(ptr, data.has_password);
			RAIL_API_PINVOKE.RoomInfo_create_time_set(ptr, data.create_time);
			RAIL_API_PINVOKE.RoomInfo_max_members_set(ptr, data.max_members);
			RAIL_API_PINVOKE.RoomInfo_room_name_set(ptr, data.room_name);
			RAIL_API_PINVOKE.RoomInfo_game_server_rail_id_set(ptr, data.game_server_rail_id);
			RAIL_API_PINVOKE.RoomInfo_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.RoomInfo_current_members_set(ptr, data.current_members);
			RAIL_API_PINVOKE.RoomInfo_is_joinable_set(ptr, data.is_joinable);
			RAIL_API_PINVOKE.RoomInfo_room_state_set(ptr, (int)data.room_state);
			RailConverter.Csharp2Cpp(data.room_kvs, RAIL_API_PINVOKE.RoomInfo_room_kvs_get(ptr));
			RAIL_API_PINVOKE.RoomInfo_type_set(ptr, (int)data.type);
			RailConverter.Csharp2Cpp(data.owner_id, RAIL_API_PINVOKE.RoomInfo_owner_id_get(ptr));
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0000CE08 File Offset: 0x0000B008
		public static void Cpp2Csharp(IntPtr ptr, RoomInfoList ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_room_num_in_zone = RAIL_API_PINVOKE.RoomInfoList_total_room_num_in_zone_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomInfoList_room_info_get(ptr), ret.room_info);
			ret.end_index = RAIL_API_PINVOKE.RoomInfoList_end_index_get(ptr);
			ret.zone_id = RAIL_API_PINVOKE.RoomInfoList_zone_id_get(ptr);
			ret.begin_index = RAIL_API_PINVOKE.RoomInfoList_begin_index_get(ptr);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0000CE60 File Offset: 0x0000B060
		public static void Csharp2Cpp(RoomInfoList data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RoomInfoList_total_room_num_in_zone_set(ptr, data.total_room_num_in_zone);
			RailConverter.Csharp2Cpp(data.room_info, RAIL_API_PINVOKE.RoomInfoList_room_info_get(ptr));
			RAIL_API_PINVOKE.RoomInfoList_end_index_set(ptr, data.end_index);
			RAIL_API_PINVOKE.RoomInfoList_zone_id_set(ptr, data.zone_id);
			RAIL_API_PINVOKE.RoomInfoList_begin_index_set(ptr, data.begin_index);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		public static void Cpp2Csharp(IntPtr ptr, RoomInfoListFilter ret)
		{
			ret.room_name_contained = RAIL_API_PINVOKE.RoomInfoListFilter_room_name_contained_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomInfoListFilter_key_filters_get(ptr), ret.key_filters);
			ret.filter_password = (EnumRailOptionalValue)RAIL_API_PINVOKE.RoomInfoListFilter_filter_password_get(ptr);
			ret.filter_friends_owned = (EnumRailOptionalValue)RAIL_API_PINVOKE.RoomInfoListFilter_filter_friends_owned_get(ptr);
			ret.available_slot_at_least = RAIL_API_PINVOKE.RoomInfoListFilter_available_slot_at_least_get(ptr);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0000CF08 File Offset: 0x0000B108
		public static void Csharp2Cpp(RoomInfoListFilter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomInfoListFilter_room_name_contained_set(ptr, data.room_name_contained);
			RailConverter.Csharp2Cpp(data.key_filters, RAIL_API_PINVOKE.RoomInfoListFilter_key_filters_get(ptr));
			RAIL_API_PINVOKE.RoomInfoListFilter_filter_password_set(ptr, (int)data.filter_password);
			RAIL_API_PINVOKE.RoomInfoListFilter_filter_friends_owned_set(ptr, (int)data.filter_friends_owned);
			RAIL_API_PINVOKE.RoomInfoListFilter_available_slot_at_least_set(ptr, data.available_slot_at_least);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0000CF56 File Offset: 0x0000B156
		public static void Cpp2Csharp(IntPtr ptr, RoomInfoListFilterKey ret)
		{
			ret.filter_value = RAIL_API_PINVOKE.RoomInfoListFilterKey_filter_value_get(ptr);
			ret.key_name = RAIL_API_PINVOKE.RoomInfoListFilterKey_key_name_get(ptr);
			ret.value_type = (EnumRailPropertyValueType)RAIL_API_PINVOKE.RoomInfoListFilterKey_value_type_get(ptr);
			ret.comparison_type = (EnumRailComparisonType)RAIL_API_PINVOKE.RoomInfoListFilterKey_comparison_type_get(ptr);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0000CF88 File Offset: 0x0000B188
		public static void Csharp2Cpp(RoomInfoListFilterKey data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomInfoListFilterKey_filter_value_set(ptr, data.filter_value);
			RAIL_API_PINVOKE.RoomInfoListFilterKey_key_name_set(ptr, data.key_name);
			RAIL_API_PINVOKE.RoomInfoListFilterKey_value_type_set(ptr, (int)data.value_type);
			RAIL_API_PINVOKE.RoomInfoListFilterKey_comparison_type_set(ptr, (int)data.comparison_type);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0000CFBA File Offset: 0x0000B1BA
		public static void Cpp2Csharp(IntPtr ptr, RoomInfoListSorter ret)
		{
			ret.close_to_value = RAIL_API_PINVOKE.RoomInfoListSorter_close_to_value_get(ptr);
			ret.property_key = RAIL_API_PINVOKE.RoomInfoListSorter_property_key_get(ptr);
			ret.property_sort_type = (EnumRailSortType)RAIL_API_PINVOKE.RoomInfoListSorter_property_sort_type_get(ptr);
			ret.property_value_type = (EnumRailPropertyValueType)RAIL_API_PINVOKE.RoomInfoListSorter_property_value_type_get(ptr);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		public static void Csharp2Cpp(RoomInfoListSorter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomInfoListSorter_close_to_value_set(ptr, data.close_to_value);
			RAIL_API_PINVOKE.RoomInfoListSorter_property_key_set(ptr, data.property_key);
			RAIL_API_PINVOKE.RoomInfoListSorter_property_sort_type_set(ptr, (int)data.property_sort_type);
			RAIL_API_PINVOKE.RoomInfoListSorter_property_value_type_set(ptr, (int)data.property_value_type);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0000D01E File Offset: 0x0000B21E
		public static void Cpp2Csharp(IntPtr ptr, RoomMembersInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomMembersInfo_member_info_get(ptr), ret.member_info);
			ret.room_id = RAIL_API_PINVOKE.RoomMembersInfo_room_id_get(ptr);
			ret.member_num = RAIL_API_PINVOKE.RoomMembersInfo_member_num_get(ptr);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0000D050 File Offset: 0x0000B250
		public static void Csharp2Cpp(RoomMembersInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.member_info, RAIL_API_PINVOKE.RoomMembersInfo_member_info_get(ptr));
			RAIL_API_PINVOKE.RoomMembersInfo_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.RoomMembersInfo_member_num_set(ptr, data.member_num);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0000D082 File Offset: 0x0000B282
		public static void Cpp2Csharp(IntPtr ptr, RoomOptions ret)
		{
			ret.max_members = RAIL_API_PINVOKE.RoomOptions_max_members_get(ptr);
			ret.password = RAIL_API_PINVOKE.RoomOptions_password_get(ptr);
			ret.type = (EnumRoomType)RAIL_API_PINVOKE.RoomOptions_type_get(ptr);
			ret.zone_id = RAIL_API_PINVOKE.RoomOptions_zone_id_get(ptr);
			ret.enable_team_voice = RAIL_API_PINVOKE.RoomOptions_enable_team_voice_get(ptr);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		public static void Csharp2Cpp(RoomOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomOptions_max_members_set(ptr, data.max_members);
			RAIL_API_PINVOKE.RoomOptions_password_set(ptr, data.password);
			RAIL_API_PINVOKE.RoomOptions_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RoomOptions_zone_id_set(ptr, data.zone_id);
			RAIL_API_PINVOKE.RoomOptions_enable_team_voice_set(ptr, data.enable_team_voice);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000096B0 File Offset: 0x000078B0
		public static void Cpp2Csharp(IntPtr ptr, ScreenshotRequestInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000096B9 File Offset: 0x000078B9
		public static void Csharp2Cpp(ScreenshotRequestInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0000D0FE File Offset: 0x0000B2FE
		public static void Cpp2Csharp(IntPtr ptr, SetGameServerMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.SetGameServerMetadataResult_game_server_id_get(ptr), ret.game_server_id);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0000D118 File Offset: 0x0000B318
		public static void Csharp2Cpp(SetGameServerMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.SetGameServerMetadataResult_game_server_id_get(ptr));
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0000D132 File Offset: 0x0000B332
		public static void Cpp2Csharp(IntPtr ptr, SetMemberMetadataInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.SetMemberMetadataInfo_room_id_get(ptr);
			ret.member_id = RAIL_API_PINVOKE.SetMemberMetadataInfo_member_id_get(ptr);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0000D153 File Offset: 0x0000B353
		public static void Csharp2Cpp(SetMemberMetadataInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SetMemberMetadataInfo_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.SetMemberMetadataInfo_member_id_set(ptr, data.member_id);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0000D174 File Offset: 0x0000B374
		public static void Cpp2Csharp(IntPtr ptr, SetRoomMetadataInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.SetRoomMetadataInfo_room_id_get(ptr);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0000D189 File Offset: 0x0000B389
		public static void Csharp2Cpp(SetRoomMetadataInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SetRoomMetadataInfo_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0000D19E File Offset: 0x0000B39E
		public static void Cpp2Csharp(IntPtr ptr, ShareStorageToSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ShareStorageToSpaceWorkResult_space_work_id_get(ptr), ret.space_work_id);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public static void Csharp2Cpp(ShareStorageToSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.space_work_id, RAIL_API_PINVOKE.ShareStorageToSpaceWorkResult_space_work_id_get(ptr));
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0000D1D2 File Offset: 0x0000B3D2
		public static void Cpp2Csharp(IntPtr ptr, ShowFloatingWindowResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.window_type = (EnumRailWindowType)RAIL_API_PINVOKE.ShowFloatingWindowResult_window_type_get(ptr);
			ret.is_show = RAIL_API_PINVOKE.ShowFloatingWindowResult_is_show_get(ptr);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0000D1F3 File Offset: 0x0000B3F3
		public static void Csharp2Cpp(ShowFloatingWindowResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.ShowFloatingWindowResult_window_type_set(ptr, (int)data.window_type);
			RAIL_API_PINVOKE.ShowFloatingWindowResult_is_show_set(ptr, data.is_show);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0000D214 File Offset: 0x0000B414
		public static void Cpp2Csharp(IntPtr ptr, ShowNotifyWindow ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.window_type = (EnumRailNotifyWindowType)RAIL_API_PINVOKE.ShowNotifyWindow_window_type_get(ptr);
			ret.json_content = RAIL_API_PINVOKE.ShowNotifyWindow_json_content_get(ptr);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0000D235 File Offset: 0x0000B435
		public static void Csharp2Cpp(ShowNotifyWindow data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.ShowNotifyWindow_window_type_set(ptr, (int)data.window_type);
			RAIL_API_PINVOKE.ShowNotifyWindow_json_content_set(ptr, data.json_content);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0000D256 File Offset: 0x0000B456
		public static void Cpp2Csharp(IntPtr ptr, SpaceWorkID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.SpaceWorkID_get_id(ptr);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0000D264 File Offset: 0x0000B464
		public static void Csharp2Cpp(SpaceWorkID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.SpaceWorkID_set_id(ptr, data.id_);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0000D272 File Offset: 0x0000B472
		public static void Cpp2Csharp(IntPtr ptr, SplitAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.source_asset = RAIL_API_PINVOKE.SplitAssetsFinished_source_asset_get(ptr);
			ret.to_quantity = RAIL_API_PINVOKE.SplitAssetsFinished_to_quantity_get(ptr);
			ret.new_asset_id = RAIL_API_PINVOKE.SplitAssetsFinished_new_asset_id_get(ptr);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0000D29F File Offset: 0x0000B49F
		public static void Csharp2Cpp(SplitAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SplitAssetsFinished_source_asset_set(ptr, data.source_asset);
			RAIL_API_PINVOKE.SplitAssetsFinished_to_quantity_set(ptr, data.to_quantity);
			RAIL_API_PINVOKE.SplitAssetsFinished_new_asset_id_set(ptr, data.new_asset_id);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		public static void Cpp2Csharp(IntPtr ptr, SplitAssetsToFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.source_asset = RAIL_API_PINVOKE.SplitAssetsToFinished_source_asset_get(ptr);
			ret.to_quantity = RAIL_API_PINVOKE.SplitAssetsToFinished_to_quantity_get(ptr);
			ret.split_to_asset_id = RAIL_API_PINVOKE.SplitAssetsToFinished_split_to_asset_id_get(ptr);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0000D2F9 File Offset: 0x0000B4F9
		public static void Csharp2Cpp(SplitAssetsToFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SplitAssetsToFinished_source_asset_set(ptr, data.source_asset);
			RAIL_API_PINVOKE.SplitAssetsToFinished_to_quantity_set(ptr, data.to_quantity);
			RAIL_API_PINVOKE.SplitAssetsToFinished_split_to_asset_id_set(ptr, data.split_to_asset_id);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0000D326 File Offset: 0x0000B526
		public static void Cpp2Csharp(IntPtr ptr, StartConsumeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.asset_id = RAIL_API_PINVOKE.StartConsumeAssetsFinished_asset_id_get(ptr);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0000D33B File Offset: 0x0000B53B
		public static void Csharp2Cpp(StartConsumeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.StartConsumeAssetsFinished_asset_id_set(ptr, data.asset_id);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0000D350 File Offset: 0x0000B550
		public static void Cpp2Csharp(IntPtr ptr, StartSessionWithPlayerResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.StartSessionWithPlayerResponse_remote_rail_id_get(ptr), ret.remote_rail_id);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0000D36A File Offset: 0x0000B56A
		public static void Csharp2Cpp(StartSessionWithPlayerResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.remote_rail_id, RAIL_API_PINVOKE.StartSessionWithPlayerResponse_remote_rail_id_get(ptr));
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0000D384 File Offset: 0x0000B584
		public static void Cpp2Csharp(IntPtr ptr, SyncSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.SyncSpaceWorkResult_id_get(ptr), ret.id);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0000D39E File Offset: 0x0000B59E
		public static void Csharp2Cpp(SyncSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.SyncSpaceWorkResult_id_get(ptr));
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public static void Cpp2Csharp(IntPtr ptr, TakeScreenshotResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.thumbnail_file_size = RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_file_size_get(ptr);
			ret.thumbnail_filepath = RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_filepath_get(ptr);
			ret.image_file_size = RAIL_API_PINVOKE.TakeScreenshotResult_image_file_size_get(ptr);
			ret.image_file_path = RAIL_API_PINVOKE.TakeScreenshotResult_image_file_path_get(ptr);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0000D3F1 File Offset: 0x0000B5F1
		public static void Csharp2Cpp(TakeScreenshotResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_file_size_set(ptr, data.thumbnail_file_size);
			RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_filepath_set(ptr, data.thumbnail_filepath);
			RAIL_API_PINVOKE.TakeScreenshotResult_image_file_size_set(ptr, data.image_file_size);
			RAIL_API_PINVOKE.TakeScreenshotResult_image_file_path_set(ptr, data.image_file_path);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0000D42A File Offset: 0x0000B62A
		public static void Cpp2Csharp(IntPtr ptr, UpdateAssetsPropertyFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.UpdateAssetsPropertyFinished_asset_property_list_get(ptr), ret.asset_property_list);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0000D444 File Offset: 0x0000B644
		public static void Csharp2Cpp(UpdateAssetsPropertyFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.asset_property_list, RAIL_API_PINVOKE.UpdateAssetsPropertyFinished_asset_property_list_get(ptr));
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0000D45E File Offset: 0x0000B65E
		public static void Cpp2Csharp(IntPtr ptr, UpdateConsumeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.asset_id = RAIL_API_PINVOKE.UpdateConsumeAssetsFinished_asset_id_get(ptr);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0000D473 File Offset: 0x0000B673
		public static void Csharp2Cpp(UpdateConsumeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.UpdateConsumeAssetsFinished_asset_id_set(ptr, data.asset_id);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0000D488 File Offset: 0x0000B688
		public static void Cpp2Csharp(IntPtr ptr, UploadLeaderboardParam ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.UploadLeaderboardParam_data_get(ptr), ret.data);
			ret.type = (LeaderboardUploadType)RAIL_API_PINVOKE.UploadLeaderboardParam_type_get(ptr);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0000D4A7 File Offset: 0x0000B6A7
		public static void Csharp2Cpp(UploadLeaderboardParam data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.data, RAIL_API_PINVOKE.UploadLeaderboardParam_data_get(ptr));
			RAIL_API_PINVOKE.UploadLeaderboardParam_type_set(ptr, (int)data.type);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0000D4C6 File Offset: 0x0000B6C6
		public static void Cpp2Csharp(IntPtr ptr, UserRoomListInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.UserRoomListInfo_room_info_get(ptr), ret.room_info);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		public static void Csharp2Cpp(UserRoomListInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.room_info, RAIL_API_PINVOKE.UserRoomListInfo_room_info_get(ptr));
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0000D4FA File Offset: 0x0000B6FA
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelAddUsersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelAddUsersResult_success_ids_get(ptr), ret.success_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelAddUsersResult_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelAddUsersResult_failed_ids_get(ptr), ret.failed_ids);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0000D536 File Offset: 0x0000B736
		public static void Csharp2Cpp(VoiceChannelAddUsersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.VoiceChannelAddUsersResult_success_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelAddUsersResult_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.failed_ids, RAIL_API_PINVOKE.VoiceChannelAddUsersResult_failed_ids_get(ptr));
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0000D572 File Offset: 0x0000B772
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelInviteEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.channel_name = RAIL_API_PINVOKE.VoiceChannelInviteEvent_channel_name_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelInviteEvent_voice_channel_id_get(ptr), ret.voice_channel_id);
			ret.inviter_name = RAIL_API_PINVOKE.VoiceChannelInviteEvent_inviter_name_get(ptr);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		public static void Csharp2Cpp(VoiceChannelInviteEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.VoiceChannelInviteEvent_channel_name_set(ptr, data.channel_name);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelInviteEvent_voice_channel_id_get(ptr));
			RAIL_API_PINVOKE.VoiceChannelInviteEvent_inviter_name_set(ptr, data.inviter_name);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0000D5D6 File Offset: 0x0000B7D6
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelMemeberChangedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelMemeberChangedEvent_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelMemeberChangedEvent_member_ids_get(ptr), ret.member_ids);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0000D601 File Offset: 0x0000B801
		public static void Csharp2Cpp(VoiceChannelMemeberChangedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelMemeberChangedEvent_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.member_ids, RAIL_API_PINVOKE.VoiceChannelMemeberChangedEvent_member_ids_get(ptr));
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0000D62C File Offset: 0x0000B82C
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelPushToTalkKeyChangedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.push_to_talk_hot_key = RAIL_API_PINVOKE.VoiceChannelPushToTalkKeyChangedEvent_push_to_talk_hot_key_get(ptr);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0000D641 File Offset: 0x0000B841
		public static void Csharp2Cpp(VoiceChannelPushToTalkKeyChangedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.VoiceChannelPushToTalkKeyChangedEvent_push_to_talk_hot_key_set(ptr, data.push_to_talk_hot_key);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0000D656 File Offset: 0x0000B856
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelRemoveUsersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_success_ids_get(ptr), ret.success_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_failed_ids_get(ptr), ret.failed_ids);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0000D692 File Offset: 0x0000B892
		public static void Csharp2Cpp(VoiceChannelRemoveUsersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_success_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.failed_ids, RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_failed_ids_get(ptr));
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0000D6CE File Offset: 0x0000B8CE
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelSpeakingUsersChangedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_speaking_users_get(ptr), ret.speaking_users);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_not_speaking_users_get(ptr), ret.not_speaking_users);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0000D70A File Offset: 0x0000B90A
		public static void Csharp2Cpp(VoiceChannelSpeakingUsersChangedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.speaking_users, RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_speaking_users_get(ptr));
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.not_speaking_users, RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_not_speaking_users_get(ptr));
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0000D746 File Offset: 0x0000B946
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelUsersSpeakingStateChangedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelUsersSpeakingStateChangedEvent_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelUsersSpeakingStateChangedEvent_users_speaking_state_get(ptr), ret.users_speaking_state);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0000D771 File Offset: 0x0000B971
		public static void Csharp2Cpp(VoiceChannelUsersSpeakingStateChangedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelUsersSpeakingStateChangedEvent_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.users_speaking_state, RAIL_API_PINVOKE.VoiceChannelUsersSpeakingStateChangedEvent_users_speaking_state_get(ptr));
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0000D79C File Offset: 0x0000B99C
		public static void Cpp2Csharp(IntPtr ptr, VoiceDataCapturedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.is_last_package = RAIL_API_PINVOKE.VoiceDataCapturedEvent_is_last_package_get(ptr);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0000D7B1 File Offset: 0x0000B9B1
		public static void Csharp2Cpp(VoiceDataCapturedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.VoiceDataCapturedEvent_is_last_package_set(ptr, data.is_last_package);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		public static void Cpp2Csharp(IntPtr ptr, ZoneInfo ret)
		{
			ret.status = (EnumZoneStatus)RAIL_API_PINVOKE.ZoneInfo_status_get(ptr);
			ret.description = RAIL_API_PINVOKE.ZoneInfo_description_get(ptr);
			ret.name = RAIL_API_PINVOKE.ZoneInfo_name_get(ptr);
			ret.idc_id = RAIL_API_PINVOKE.ZoneInfo_idc_id_get(ptr);
			ret.country_code = RAIL_API_PINVOKE.ZoneInfo_country_code_get(ptr);
			ret.zone_id = RAIL_API_PINVOKE.ZoneInfo_zone_id_get(ptr);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0000D820 File Offset: 0x0000BA20
		public static void Csharp2Cpp(ZoneInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.ZoneInfo_status_set(ptr, (int)data.status);
			RAIL_API_PINVOKE.ZoneInfo_description_set(ptr, data.description);
			RAIL_API_PINVOKE.ZoneInfo_name_set(ptr, data.name);
			RAIL_API_PINVOKE.ZoneInfo_idc_id_set(ptr, data.idc_id);
			RAIL_API_PINVOKE.ZoneInfo_country_code_set(ptr, data.country_code);
			RAIL_API_PINVOKE.ZoneInfo_zone_id_set(ptr, data.zone_id);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0000D875 File Offset: 0x0000BA75
		public static void Cpp2Csharp(IntPtr ptr, ZoneInfoList ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ZoneInfoList_zone_info_get(ptr), ret.zone_info);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0000D88F File Offset: 0x0000BA8F
		public static void Csharp2Cpp(ZoneInfoList data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.zone_info, RAIL_API_PINVOKE.ZoneInfoList_zone_info_get(ptr));
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		public static void Csharp2Cpp(List<GameServerPlayerInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerPlayerInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerPlayerInfo(intPtr);
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerPlayerInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_Item(ptr, (uint)num2);
				GameServerPlayerInfo gameServerPlayerInfo = new GameServerPlayerInfo();
				RailConverter.Cpp2Csharp(intPtr, gameServerPlayerInfo);
				ret.Add(gameServerPlayerInfo);
				num2++;
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0000D938 File Offset: 0x0000BB38
		public static void Csharp2Cpp(List<RailSpaceWorkDescriptor> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailSpaceWorkDescriptor__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailSpaceWorkDescriptor(intPtr);
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0000D980 File Offset: 0x0000BB80
		public static void Cpp2Csharp(IntPtr ptr, List<RailSpaceWorkDescriptor> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_Item(ptr, (uint)num2);
				RailSpaceWorkDescriptor railSpaceWorkDescriptor = new RailSpaceWorkDescriptor();
				RailConverter.Cpp2Csharp(intPtr, railSpaceWorkDescriptor);
				ret.Add(railSpaceWorkDescriptor);
				num2++;
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public static void Csharp2Cpp(List<RailDlcOwned> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailDlcOwned_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailDlcOwned__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailDlcOwned_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailDlcOwned(intPtr);
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		public static void Cpp2Csharp(IntPtr ptr, List<RailDlcOwned> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailDlcOwned_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailDlcOwned_Item(ptr, (uint)num2);
				RailDlcOwned railDlcOwned = new RailDlcOwned();
				RailConverter.Cpp2Csharp(intPtr, railDlcOwned);
				ret.Add(railDlcOwned);
				num2++;
			}
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0000DA50 File Offset: 0x0000BC50
		public static void Csharp2Cpp(List<RailSmallObjectDownloadInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailSmallObjectDownloadInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailSmallObjectDownloadInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailSmallObjectDownloadInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailSmallObjectDownloadInfo(intPtr);
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public static void Cpp2Csharp(IntPtr ptr, List<RailSmallObjectDownloadInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailSmallObjectDownloadInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailSmallObjectDownloadInfo_Item(ptr, (uint)num2);
				RailSmallObjectDownloadInfo railSmallObjectDownloadInfo = new RailSmallObjectDownloadInfo();
				RailConverter.Cpp2Csharp(intPtr, railSmallObjectDownloadInfo);
				ret.Add(railSmallObjectDownloadInfo);
				num2++;
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0000DADC File Offset: 0x0000BCDC
		public static void Csharp2Cpp(List<MemberInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayMemberInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_MemberInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayMemberInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_MemberInfo(intPtr);
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public static void Cpp2Csharp(IntPtr ptr, List<MemberInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayMemberInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayMemberInfo_Item(ptr, (uint)num2);
				MemberInfo memberInfo = new MemberInfo();
				RailConverter.Cpp2Csharp(intPtr, memberInfo);
				ret.Add(memberInfo);
				num2++;
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0000DB68 File Offset: 0x0000BD68
		public static void Csharp2Cpp(List<RoomInfoListFilterKey> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomInfoListFilterKey__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomInfoListFilterKey(intPtr);
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListFilterKey> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_Item(ptr, (uint)num2);
				RoomInfoListFilterKey roomInfoListFilterKey = new RoomInfoListFilterKey();
				RailConverter.Cpp2Csharp(intPtr, roomInfoListFilterKey);
				ret.Add(roomInfoListFilterKey);
				num2++;
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		public static void Csharp2Cpp(List<RailPlayedWithFriendsTimeItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsTimeItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailPlayedWithFriendsTimeItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsTimeItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailPlayedWithFriendsTimeItem(intPtr);
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		public static void Cpp2Csharp(IntPtr ptr, List<RailPlayedWithFriendsTimeItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsTimeItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsTimeItem_Item(ptr, (uint)num2);
				RailPlayedWithFriendsTimeItem railPlayedWithFriendsTimeItem = new RailPlayedWithFriendsTimeItem();
				RailConverter.Cpp2Csharp(intPtr, railPlayedWithFriendsTimeItem);
				ret.Add(railPlayedWithFriendsTimeItem);
				num2++;
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0000DC80 File Offset: 0x0000BE80
		public static void Csharp2Cpp(List<GameServerInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerInfo(intPtr);
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerInfo_Item(ptr, (uint)num2);
				GameServerInfo gameServerInfo = new GameServerInfo();
				RailConverter.Cpp2Csharp(intPtr, gameServerInfo);
				ret.Add(gameServerInfo);
				num2++;
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		public static void Csharp2Cpp(List<RailGameDefineGamePlayingState> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailGameDefineGamePlayingState_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailGameDefineGamePlayingState__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailGameDefineGamePlayingState_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailGameDefineGamePlayingState(intPtr);
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0000DD54 File Offset: 0x0000BF54
		public static void Cpp2Csharp(IntPtr ptr, List<RailGameDefineGamePlayingState> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailGameDefineGamePlayingState_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailGameDefineGamePlayingState_Item(ptr, (uint)num2);
				RailGameDefineGamePlayingState railGameDefineGamePlayingState = new RailGameDefineGamePlayingState();
				RailConverter.Cpp2Csharp(intPtr, railGameDefineGamePlayingState);
				ret.Add(railGameDefineGamePlayingState);
				num2++;
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0000DD98 File Offset: 0x0000BF98
		public static void Csharp2Cpp(List<RailUserPlayedWith> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailUserPlayedWith__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailUserPlayedWith(intPtr);
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		public static void Cpp2Csharp(IntPtr ptr, List<RailUserPlayedWith> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_Item(ptr, (uint)num2);
				RailUserPlayedWith railUserPlayedWith = new RailUserPlayedWith();
				RailConverter.Cpp2Csharp(intPtr, railUserPlayedWith);
				ret.Add(railUserPlayedWith);
				num2++;
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0000DE24 File Offset: 0x0000C024
		public static void Csharp2Cpp(List<RailID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0000DE6C File Offset: 0x0000C06C
		public static void Cpp2Csharp(IntPtr ptr, List<RailID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailID_Item(ptr, (uint)num2);
				RailID railID = new RailID();
				RailConverter.Cpp2Csharp(intPtr, railID);
				ret.Add(railID);
				num2++;
			}
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public static void Csharp2Cpp(List<RailDlcID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailDlcID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailDlcID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailDlcID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		public static void Cpp2Csharp(IntPtr ptr, List<RailDlcID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailDlcID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailDlcID_Item(ptr, (uint)num2);
				RailDlcID railDlcID = new RailDlcID();
				RailConverter.Cpp2Csharp(intPtr, railDlcID);
				ret.Add(railDlcID);
				num2++;
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0000DF3C File Offset: 0x0000C13C
		public static void Csharp2Cpp(List<GameServerListSorter> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerListSorter_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerListSorter__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerListSorter_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerListSorter(intPtr);
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0000DF84 File Offset: 0x0000C184
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerListSorter> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerListSorter_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerListSorter_Item(ptr, (uint)num2);
				GameServerListSorter gameServerListSorter = new GameServerListSorter();
				RailConverter.Cpp2Csharp(intPtr, gameServerListSorter);
				ret.Add(gameServerListSorter);
				num2++;
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		public static void Csharp2Cpp(List<RoomInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomInfo(intPtr);
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0000E010 File Offset: 0x0000C210
		public static void Cpp2Csharp(IntPtr ptr, List<RoomInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomInfo_Item(ptr, (uint)num2);
				RoomInfo roomInfo = new RoomInfo();
				RailConverter.Cpp2Csharp(intPtr, roomInfo);
				ret.Add(roomInfo);
				num2++;
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0000E054 File Offset: 0x0000C254
		public static void Csharp2Cpp(List<RailPurchaseProductInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailPurchaseProductInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailPurchaseProductInfo(intPtr);
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0000E09C File Offset: 0x0000C29C
		public static void Cpp2Csharp(IntPtr ptr, List<RailPurchaseProductInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_Item(ptr, (uint)num2);
				RailPurchaseProductInfo railPurchaseProductInfo = new RailPurchaseProductInfo();
				RailConverter.Cpp2Csharp(intPtr, railPurchaseProductInfo);
				ret.Add(railPurchaseProductInfo);
				num2++;
			}
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public static void Csharp2Cpp(List<RailGameID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailGameID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailGameID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailGameID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailGameID(intPtr);
			}
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0000E128 File Offset: 0x0000C328
		public static void Cpp2Csharp(IntPtr ptr, List<RailGameID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailGameID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailGameID_Item(ptr, (uint)num2);
				RailGameID railGameID = new RailGameID();
				RailConverter.Cpp2Csharp(intPtr, railGameID);
				ret.Add(railGameID);
				num2++;
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0000E16C File Offset: 0x0000C36C
		public static void Csharp2Cpp(List<RailVoiceChannelUserSpeakingState> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailVoiceChannelUserSpeakingState_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailVoiceChannelUserSpeakingState__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailVoiceChannelUserSpeakingState_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailVoiceChannelUserSpeakingState(intPtr);
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		public static void Cpp2Csharp(IntPtr ptr, List<RailVoiceChannelUserSpeakingState> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailVoiceChannelUserSpeakingState_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailVoiceChannelUserSpeakingState_Item(ptr, (uint)num2);
				RailVoiceChannelUserSpeakingState railVoiceChannelUserSpeakingState = new RailVoiceChannelUserSpeakingState();
				RailConverter.Cpp2Csharp(intPtr, railVoiceChannelUserSpeakingState);
				ret.Add(railVoiceChannelUserSpeakingState);
				num2++;
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public static void Csharp2Cpp(List<RailFriendPlayedGameInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailFriendPlayedGameInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailFriendPlayedGameInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailFriendPlayedGameInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailFriendPlayedGameInfo(intPtr);
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0000E240 File Offset: 0x0000C440
		public static void Cpp2Csharp(IntPtr ptr, List<RailFriendPlayedGameInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailFriendPlayedGameInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailFriendPlayedGameInfo_Item(ptr, (uint)num2);
				RailFriendPlayedGameInfo railFriendPlayedGameInfo = new RailFriendPlayedGameInfo();
				RailConverter.Cpp2Csharp(intPtr, railFriendPlayedGameInfo);
				ret.Add(railFriendPlayedGameInfo);
				num2++;
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0000E284 File Offset: 0x0000C484
		public static void Csharp2Cpp(List<RailSmallObjectState> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailSmallObjectState_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailSmallObjectState__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailSmallObjectState_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailSmallObjectState(intPtr);
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0000E2CC File Offset: 0x0000C4CC
		public static void Cpp2Csharp(IntPtr ptr, List<RailSmallObjectState> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailSmallObjectState_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailSmallObjectState_Item(ptr, (uint)num2);
				RailSmallObjectState railSmallObjectState = new RailSmallObjectState();
				RailConverter.Cpp2Csharp(intPtr, railSmallObjectState);
				ret.Add(railSmallObjectState);
				num2++;
			}
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0000E310 File Offset: 0x0000C510
		public static void Csharp2Cpp(List<RailSpaceWorkVoteDetail> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailSpaceWorkVoteDetail__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailSpaceWorkVoteDetail(intPtr);
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0000E358 File Offset: 0x0000C558
		public static void Cpp2Csharp(IntPtr ptr, List<RailSpaceWorkVoteDetail> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_Item(ptr, (uint)num2);
				RailSpaceWorkVoteDetail railSpaceWorkVoteDetail = new RailSpaceWorkVoteDetail();
				RailConverter.Cpp2Csharp(intPtr, railSpaceWorkVoteDetail);
				ret.Add(railSpaceWorkVoteDetail);
				num2++;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0000E39C File Offset: 0x0000C59C
		public static void Csharp2Cpp(List<RailKeyValue> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailKeyValue_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailKeyValue();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailKeyValue_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailKeyValue(intPtr);
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		public static void Cpp2Csharp(IntPtr ptr, List<RailKeyValue> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailKeyValue_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailKeyValue_Item(ptr, (uint)num2);
				RailKeyValue railKeyValue = new RailKeyValue();
				RailConverter.Cpp2Csharp(intPtr, railKeyValue);
				ret.Add(railKeyValue);
				num2++;
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0000E428 File Offset: 0x0000C628
		public static void Csharp2Cpp(List<RailZoneID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailZoneID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailZoneID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailZoneID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailZoneID(intPtr);
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0000E470 File Offset: 0x0000C670
		public static void Cpp2Csharp(IntPtr ptr, List<RailZoneID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailZoneID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailZoneID_Item(ptr, (uint)num2);
				RailZoneID railZoneID = new RailZoneID();
				RailConverter.Cpp2Csharp(intPtr, railZoneID);
				ret.Add(railZoneID);
				num2++;
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public static void Csharp2Cpp(List<RailFriendInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailFriendInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailFriendInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailFriendInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailFriendInfo(intPtr);
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		public static void Cpp2Csharp(IntPtr ptr, List<RailFriendInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailFriendInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailFriendInfo_Item(ptr, (uint)num2);
				RailFriendInfo railFriendInfo = new RailFriendInfo();
				RailConverter.Cpp2Csharp(intPtr, railFriendInfo);
				ret.Add(railFriendInfo);
				num2++;
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0000E540 File Offset: 0x0000C740
		public static void Csharp2Cpp(List<string> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailString_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
				RAIL_API_PINVOKE.RailString_SetValue(intPtr, ret[i]);
				RAIL_API_PINVOKE.RailArrayRailString_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0000E588 File Offset: 0x0000C788
		public static void Cpp2Csharp(IntPtr ptr, List<string> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailString_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailString_Item(ptr, (uint)num2);
				ret.Add(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				num2++;
			}
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		public static void Csharp2Cpp(List<RailKeyValueResult> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailKeyValueResult_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailKeyValueResult__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailKeyValueResult_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailKeyValueResult(intPtr);
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0000E60C File Offset: 0x0000C80C
		public static void Cpp2Csharp(IntPtr ptr, List<RailKeyValueResult> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailKeyValueResult_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailKeyValueResult_Item(ptr, (uint)num2);
				RailKeyValueResult railKeyValueResult = new RailKeyValueResult();
				RailConverter.Cpp2Csharp(intPtr, railKeyValueResult);
				ret.Add(railKeyValueResult);
				num2++;
			}
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0000E650 File Offset: 0x0000C850
		public static void Csharp2Cpp(List<RoomInfoListSorter> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomInfoListSorter__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomInfoListSorter(intPtr);
			}
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0000E698 File Offset: 0x0000C898
		public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListSorter> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_Item(ptr, (uint)num2);
				RoomInfoListSorter roomInfoListSorter = new RoomInfoListSorter();
				RailConverter.Cpp2Csharp(intPtr, roomInfoListSorter);
				ret.Add(roomInfoListSorter);
				num2++;
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public static void Csharp2Cpp(List<SpaceWorkID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArraySpaceWorkID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArraySpaceWorkID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0000E724 File Offset: 0x0000C924
		public static void Cpp2Csharp(IntPtr ptr, List<SpaceWorkID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArraySpaceWorkID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArraySpaceWorkID_Item(ptr, (uint)num2);
				SpaceWorkID spaceWorkID = new SpaceWorkID();
				RailConverter.Cpp2Csharp(intPtr, spaceWorkID);
				ret.Add(spaceWorkID);
				num2++;
			}
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0000E768 File Offset: 0x0000C968
		public static void Csharp2Cpp(List<RailPlayedWithFriendsGameItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsGameItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailPlayedWithFriendsGameItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsGameItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailPlayedWithFriendsGameItem(intPtr);
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		public static void Cpp2Csharp(IntPtr ptr, List<RailPlayedWithFriendsGameItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsGameItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsGameItem_Item(ptr, (uint)num2);
				RailPlayedWithFriendsGameItem railPlayedWithFriendsGameItem = new RailPlayedWithFriendsGameItem();
				RailConverter.Cpp2Csharp(intPtr, railPlayedWithFriendsGameItem);
				ret.Add(railPlayedWithFriendsGameItem);
				num2++;
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0000E7F4 File Offset: 0x0000C9F4
		public static void Csharp2Cpp(List<EnumRailUsersLimits> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayEnumRailUsersLimits_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
				RAIL_API_PINVOKE.SetInt(intPtr, (int)ret[i]);
				RAIL_API_PINVOKE.RailArrayEnumRailUsersLimits_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0000E83C File Offset: 0x0000CA3C
		public static void Cpp2Csharp(IntPtr ptr, List<EnumRailUsersLimits> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayEnumRailUsersLimits_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayEnumRailUsersLimits_Item(ptr, (uint)num2);
				ret.Add((EnumRailUsersLimits)RAIL_API_PINVOKE.GetInt(intPtr));
				num2++;
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0000E878 File Offset: 0x0000CA78
		public static void Csharp2Cpp(List<EnumRailSpaceWorkType> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
				RAIL_API_PINVOKE.SetInt(intPtr, (int)ret[i]);
				RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public static void Cpp2Csharp(IntPtr ptr, List<EnumRailSpaceWorkType> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_Item(ptr, (uint)num2);
				ret.Add((EnumRailSpaceWorkType)RAIL_API_PINVOKE.GetInt(intPtr));
				num2++;
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		public static void Csharp2Cpp(List<GameServerListFilter> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerListFilter_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerListFilter__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerListFilter_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerListFilter(intPtr);
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0000E944 File Offset: 0x0000CB44
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerListFilter> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerListFilter_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerListFilter_Item(ptr, (uint)num2);
				GameServerListFilter gameServerListFilter = new GameServerListFilter();
				RailConverter.Cpp2Csharp(intPtr, gameServerListFilter);
				ret.Add(gameServerListFilter);
				num2++;
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0000E988 File Offset: 0x0000CB88
		public static void Csharp2Cpp(List<RoomInfoListFilter> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomInfoListFilter__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomInfoListFilter(intPtr);
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListFilter> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_Item(ptr, (uint)num2);
				RoomInfoListFilter roomInfoListFilter = new RoomInfoListFilter();
				RailConverter.Cpp2Csharp(intPtr, roomInfoListFilter);
				ret.Add(roomInfoListFilter);
				num2++;
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0000EA14 File Offset: 0x0000CC14
		public static void Csharp2Cpp(List<RailStreamFileInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailStreamFileInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailStreamFileInfo(intPtr);
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public static void Cpp2Csharp(IntPtr ptr, List<RailStreamFileInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_Item(ptr, (uint)num2);
				RailStreamFileInfo railStreamFileInfo = new RailStreamFileInfo();
				RailConverter.Cpp2Csharp(intPtr, railStreamFileInfo);
				ret.Add(railStreamFileInfo);
				num2++;
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		public static void Csharp2Cpp(List<GameServerListFilterKey> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerListFilterKey__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerListFilterKey(intPtr);
			}
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerListFilterKey> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_Item(ptr, (uint)num2);
				GameServerListFilterKey gameServerListFilterKey = new GameServerListFilterKey();
				RailConverter.Cpp2Csharp(intPtr, gameServerListFilterKey);
				ret.Add(gameServerListFilterKey);
				num2++;
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public static void Csharp2Cpp(List<RailAssetProperty> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailAssetProperty_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailAssetProperty__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailAssetProperty_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailAssetProperty(intPtr);
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0000EB74 File Offset: 0x0000CD74
		public static void Cpp2Csharp(IntPtr ptr, List<RailAssetProperty> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailAssetProperty_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailAssetProperty_Item(ptr, (uint)num2);
				RailAssetProperty railAssetProperty = new RailAssetProperty();
				RailConverter.Cpp2Csharp(intPtr, railAssetProperty);
				ret.Add(railAssetProperty);
				num2++;
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public static void Csharp2Cpp(List<EnumRailWorkFileClass> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
				RAIL_API_PINVOKE.SetInt(intPtr, (int)ret[i]);
				RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public static void Cpp2Csharp(IntPtr ptr, List<EnumRailWorkFileClass> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_Item(ptr, (uint)num2);
				ret.Add((EnumRailWorkFileClass)RAIL_API_PINVOKE.GetInt(intPtr));
				num2++;
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		public static void Csharp2Cpp(List<PlayerPersonalInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_PlayerPersonalInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_PlayerPersonalInfo(intPtr);
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public static void Cpp2Csharp(IntPtr ptr, List<PlayerPersonalInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_Item(ptr, (uint)num2);
				PlayerPersonalInfo playerPersonalInfo = new PlayerPersonalInfo();
				RailConverter.Cpp2Csharp(intPtr, playerPersonalInfo);
				ret.Add(playerPersonalInfo);
				num2++;
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public static void Csharp2Cpp(List<RailAssetItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailAssetItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailAssetItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailAssetItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailAssetItem(intPtr);
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public static void Cpp2Csharp(IntPtr ptr, List<RailAssetItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailAssetItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailAssetItem_Item(ptr, (uint)num2);
				RailAssetItem railAssetItem = new RailAssetItem();
				RailConverter.Cpp2Csharp(intPtr, railAssetItem);
				ret.Add(railAssetItem);
				num2++;
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0000ED54 File Offset: 0x0000CF54
		public static void Csharp2Cpp(List<RailAssetInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailAssetInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailAssetInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailAssetInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailAssetInfo(intPtr);
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0000ED9C File Offset: 0x0000CF9C
		public static void Cpp2Csharp(IntPtr ptr, List<RailAssetInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailAssetInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailAssetInfo_Item(ptr, (uint)num2);
				RailAssetInfo railAssetInfo = new RailAssetInfo();
				RailConverter.Cpp2Csharp(intPtr, railAssetInfo);
				ret.Add(railAssetInfo);
				num2++;
			}
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		public static void Csharp2Cpp(List<uint> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayuint32_t_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
				RAIL_API_PINVOKE.SetInt(intPtr, (int)ret[i]);
				RAIL_API_PINVOKE.RailArrayuint32_t_push_back(ptr, (uint)RAIL_API_PINVOKE.GetInt(intPtr));
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0000EE2C File Offset: 0x0000D02C
		public static void Cpp2Csharp(IntPtr ptr, List<uint> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayuint32_t_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayuint32_t_Item(ptr, (uint)num2);
				ret.Add((uint)RAIL_API_PINVOKE.GetInt(intPtr));
				num2++;
			}
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0000EE68 File Offset: 0x0000D068
		public static void Csharp2Cpp(List<ZoneInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayZoneInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_ZoneInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayZoneInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_ZoneInfo(intPtr);
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public static void Cpp2Csharp(IntPtr ptr, List<ZoneInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayZoneInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayZoneInfo_Item(ptr, (uint)num2);
				ZoneInfo zoneInfo = new ZoneInfo();
				RailConverter.Cpp2Csharp(intPtr, zoneInfo);
				ret.Add(zoneInfo);
				num2++;
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		public static void Csharp2Cpp(List<RailProductItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailProductItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailProductItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailProductItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailProductItem(intPtr);
			}
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0000EF3C File Offset: 0x0000D13C
		public static void Cpp2Csharp(IntPtr ptr, List<RailProductItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailProductItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailProductItem_Item(ptr, (uint)num2);
				RailProductItem railProductItem = new RailProductItem();
				RailConverter.Cpp2Csharp(intPtr, railProductItem);
				ret.Add(railProductItem);
				num2++;
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00002119 File Offset: 0x00000319
		public RailConverter()
		{
		}
	}
}
