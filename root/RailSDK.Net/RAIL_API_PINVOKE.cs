using System;
using System.IO;
using System.Runtime.InteropServices;

namespace rail
{
	// Token: 0x02000007 RID: 7
	internal class RAIL_API_PINVOKE
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002121 File Offset: 0x00000321
		static RAIL_API_PINVOKE()
		{
		}

		// Token: 0x06000019 RID: 25
		[DllImport("rail_api", EntryPoint = "CSharp_USE_MANUAL_ALLOC_get")]
		public static extern int USE_MANUAL_ALLOC_get();

		// Token: 0x0600001A RID: 26
		[DllImport("rail_api", EntryPoint = "CSharp_RAIL_SDK_PACKING_get")]
		public static extern int RAIL_SDK_PACKING_get();

		// Token: 0x0600001B RID: 27
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailString__SWIG_0")]
		public static extern IntPtr new_RailString__SWIG_0();

		// Token: 0x0600001C RID: 28
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailString__SWIG_1")]
		public static extern IntPtr new_RailString__SWIG_1(string jarg1);

		// Token: 0x0600001D RID: 29
		[DllImport("rail_api", EntryPoint = "CSharp_RailString_SetValue")]
		public static extern IntPtr RailString_SetValue(IntPtr jarg1, string jarg2);

		// Token: 0x0600001E RID: 30
		[DllImport("rail_api", EntryPoint = "CSharp_RailString_assign")]
		public static extern void RailString_assign(IntPtr jarg1, string jarg2, uint jarg3);

		// Token: 0x0600001F RID: 31
		[DllImport("rail_api", EntryPoint = "CSharp_RailString_c_str")]
		public static extern string RailString_c_str(IntPtr jarg1);

		// Token: 0x06000020 RID: 32
		[DllImport("rail_api", EntryPoint = "CSharp_RailString_data")]
		public static extern string RailString_data(IntPtr jarg1);

		// Token: 0x06000021 RID: 33
		[DllImport("rail_api", EntryPoint = "CSharp_RailString_clear")]
		public static extern void RailString_clear(IntPtr jarg1);

		// Token: 0x06000022 RID: 34
		[DllImport("rail_api", EntryPoint = "CSharp_RailString_size")]
		public static extern uint RailString_size(IntPtr jarg1);

		// Token: 0x06000023 RID: 35
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailString")]
		public static extern void delete_RailString(IntPtr jarg1);

		// Token: 0x06000024 RID: 36
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailID__SWIG_0")]
		public static extern IntPtr new_RailID__SWIG_0();

		// Token: 0x06000025 RID: 37
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailID__SWIG_1")]
		public static extern IntPtr new_RailID__SWIG_1(ulong jarg1);

		// Token: 0x06000026 RID: 38
		[DllImport("rail_api", EntryPoint = "CSharp_RailID_set_id")]
		public static extern void RailID_set_id(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000027 RID: 39
		[DllImport("rail_api", EntryPoint = "CSharp_RailID_get_id")]
		public static extern ulong RailID_get_id(IntPtr jarg1);

		// Token: 0x06000028 RID: 40
		[DllImport("rail_api", EntryPoint = "CSharp_RailID_IsValid")]
		public static extern bool RailID_IsValid(IntPtr jarg1);

		// Token: 0x06000029 RID: 41
		[DllImport("rail_api", EntryPoint = "CSharp_RailID_GetDomain")]
		public static extern int RailID_GetDomain(IntPtr jarg1);

		// Token: 0x0600002A RID: 42
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailID")]
		public static extern void delete_RailID(IntPtr jarg1);

		// Token: 0x0600002B RID: 43
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailGameID__SWIG_0")]
		public static extern IntPtr new_RailGameID__SWIG_0();

		// Token: 0x0600002C RID: 44
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailGameID__SWIG_1")]
		public static extern IntPtr new_RailGameID__SWIG_1(ulong jarg1);

		// Token: 0x0600002D RID: 45
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameID_set_id")]
		public static extern void RailGameID_set_id(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600002E RID: 46
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameID_get_id")]
		public static extern ulong RailGameID_get_id(IntPtr jarg1);

		// Token: 0x0600002F RID: 47
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameID_IsValid")]
		public static extern bool RailGameID_IsValid(IntPtr jarg1);

		// Token: 0x06000030 RID: 48
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailGameID")]
		public static extern void delete_RailGameID(IntPtr jarg1);

		// Token: 0x06000031 RID: 49
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcID__SWIG_0")]
		public static extern IntPtr new_RailDlcID__SWIG_0();

		// Token: 0x06000032 RID: 50
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcID__SWIG_1")]
		public static extern IntPtr new_RailDlcID__SWIG_1(ulong jarg1);

		// Token: 0x06000033 RID: 51
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcID_set_id")]
		public static extern void RailDlcID_set_id(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000034 RID: 52
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcID_get_id")]
		public static extern ulong RailDlcID_get_id(IntPtr jarg1);

		// Token: 0x06000035 RID: 53
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcID_IsValid")]
		public static extern bool RailDlcID_IsValid(IntPtr jarg1);

		// Token: 0x06000036 RID: 54
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailDlcID")]
		public static extern void delete_RailDlcID(IntPtr jarg1);

		// Token: 0x06000037 RID: 55
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceChannelID__SWIG_0")]
		public static extern IntPtr new_RailVoiceChannelID__SWIG_0();

		// Token: 0x06000038 RID: 56
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceChannelID__SWIG_1")]
		public static extern IntPtr new_RailVoiceChannelID__SWIG_1(ulong jarg1);

		// Token: 0x06000039 RID: 57
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelID_set_id")]
		public static extern void RailVoiceChannelID_set_id(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600003A RID: 58
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelID_get_id")]
		public static extern ulong RailVoiceChannelID_get_id(IntPtr jarg1);

		// Token: 0x0600003B RID: 59
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelID_IsValid")]
		public static extern bool RailVoiceChannelID_IsValid(IntPtr jarg1);

		// Token: 0x0600003C RID: 60
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailVoiceChannelID")]
		public static extern void delete_RailVoiceChannelID(IntPtr jarg1);

		// Token: 0x0600003D RID: 61
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailZoneID__SWIG_0")]
		public static extern IntPtr new_RailZoneID__SWIG_0();

		// Token: 0x0600003E RID: 62
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailZoneID__SWIG_1")]
		public static extern IntPtr new_RailZoneID__SWIG_1(ulong jarg1);

		// Token: 0x0600003F RID: 63
		[DllImport("rail_api", EntryPoint = "CSharp_RailZoneID_set_id")]
		public static extern void RailZoneID_set_id(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000040 RID: 64
		[DllImport("rail_api", EntryPoint = "CSharp_RailZoneID_get_id")]
		public static extern ulong RailZoneID_get_id(IntPtr jarg1);

		// Token: 0x06000041 RID: 65
		[DllImport("rail_api", EntryPoint = "CSharp_RailZoneID_IsValid")]
		public static extern bool RailZoneID_IsValid(IntPtr jarg1);

		// Token: 0x06000042 RID: 66
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailZoneID")]
		public static extern void delete_RailZoneID(IntPtr jarg1);

		// Token: 0x06000043 RID: 67
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValue_key_set")]
		public static extern void RailKeyValue_key_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000044 RID: 68
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValue_key_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailKeyValue_key_get(IntPtr jarg1);

		// Token: 0x06000045 RID: 69
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValue_value_set")]
		public static extern void RailKeyValue_value_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000046 RID: 70
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValue_value_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailKeyValue_value_get(IntPtr jarg1);

		// Token: 0x06000047 RID: 71
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailKeyValue")]
		public static extern IntPtr new_RailKeyValue();

		// Token: 0x06000048 RID: 72
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailKeyValue")]
		public static extern void delete_RailKeyValue(IntPtr jarg1);

		// Token: 0x06000049 RID: 73
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSessionTicket")]
		public static extern IntPtr new_RailSessionTicket();

		// Token: 0x0600004A RID: 74
		[DllImport("rail_api", EntryPoint = "CSharp_RailSessionTicket_ticket_set")]
		public static extern void RailSessionTicket_ticket_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600004B RID: 75
		[DllImport("rail_api", EntryPoint = "CSharp_RailSessionTicket_ticket_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailSessionTicket_ticket_get(IntPtr jarg1);

		// Token: 0x0600004C RID: 76
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSessionTicket")]
		public static extern void delete_RailSessionTicket(IntPtr jarg1);

		// Token: 0x0600004D RID: 77
		[DllImport("rail_api", EntryPoint = "CSharp_IRailComponent_GetComponentVersion")]
		public static extern ulong IRailComponent_GetComponentVersion(IntPtr jarg1);

		// Token: 0x0600004E RID: 78
		[DllImport("rail_api", EntryPoint = "CSharp_IRailComponent_Release")]
		public static extern void IRailComponent_Release(IntPtr jarg1);

		// Token: 0x0600004F RID: 79
		[DllImport("rail_api", EntryPoint = "CSharp_delete_EventBase")]
		public static extern void delete_EventBase(IntPtr jarg1);

		// Token: 0x06000050 RID: 80
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_get_event_id")]
		public static extern int EventBase_get_event_id(IntPtr jarg1);

		// Token: 0x06000051 RID: 81
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_rail_id_set")]
		public static extern void EventBase_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000052 RID: 82
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_rail_id_get")]
		public static extern IntPtr EventBase_rail_id_get(IntPtr jarg1);

		// Token: 0x06000053 RID: 83
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_game_id_set")]
		public static extern void EventBase_game_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000054 RID: 84
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_game_id_get")]
		public static extern IntPtr EventBase_game_id_get(IntPtr jarg1);

		// Token: 0x06000055 RID: 85
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_user_data_set")]
		public static extern void EventBase_user_data_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000056 RID: 86
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_user_data_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string EventBase_user_data_get(IntPtr jarg1);

		// Token: 0x06000057 RID: 87
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_result_set")]
		public static extern void EventBase_result_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000058 RID: 88
		[DllImport("rail_api", EntryPoint = "CSharp_EventBase_result_get")]
		public static extern int EventBase_result_get(IntPtr jarg1);

		// Token: 0x06000059 RID: 89
		[DllImport("rail_api", EntryPoint = "CSharp_IRailEvent_OnRailEvent")]
		public static extern void IRailEvent_OnRailEvent(IntPtr jarg1, int jarg2, IntPtr jarg3);

		// Token: 0x0600005A RID: 90
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailEvent")]
		public static extern void delete_IRailEvent(IntPtr jarg1);

		// Token: 0x0600005B RID: 91
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailProductItem__SWIG_0")]
		public static extern IntPtr new_RailArrayRailProductItem__SWIG_0();

		// Token: 0x0600005C RID: 92
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailProductItem__SWIG_1")]
		public static extern IntPtr new_RailArrayRailProductItem__SWIG_1(uint jarg1);

		// Token: 0x0600005D RID: 93
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailProductItem__SWIG_2")]
		public static extern IntPtr new_RailArrayRailProductItem__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600005E RID: 94
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailProductItem__SWIG_3")]
		public static extern IntPtr new_RailArrayRailProductItem__SWIG_3(IntPtr jarg1);

		// Token: 0x0600005F RID: 95
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_SetValue")]
		public static extern IntPtr RailArrayRailProductItem_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000060 RID: 96
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailProductItem")]
		public static extern void delete_RailArrayRailProductItem(IntPtr jarg1);

		// Token: 0x06000061 RID: 97
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_assign")]
		public static extern void RailArrayRailProductItem_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000062 RID: 98
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_buf")]
		public static extern IntPtr RailArrayRailProductItem_buf(IntPtr jarg1);

		// Token: 0x06000063 RID: 99
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_size")]
		public static extern uint RailArrayRailProductItem_size(IntPtr jarg1);

		// Token: 0x06000064 RID: 100
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_Item")]
		public static extern IntPtr RailArrayRailProductItem_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000065 RID: 101
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_resize")]
		public static extern void RailArrayRailProductItem_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000066 RID: 102
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_push_back")]
		public static extern void RailArrayRailProductItem_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000067 RID: 103
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_clear")]
		public static extern void RailArrayRailProductItem_clear(IntPtr jarg1);

		// Token: 0x06000068 RID: 104
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailProductItem_erase")]
		public static extern void RailArrayRailProductItem_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000069 RID: 105
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayZoneInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayZoneInfo__SWIG_0();

		// Token: 0x0600006A RID: 106
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayZoneInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayZoneInfo__SWIG_1(uint jarg1);

		// Token: 0x0600006B RID: 107
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayZoneInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayZoneInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600006C RID: 108
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayZoneInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayZoneInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x0600006D RID: 109
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_SetValue")]
		public static extern IntPtr RailArrayZoneInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600006E RID: 110
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayZoneInfo")]
		public static extern void delete_RailArrayZoneInfo(IntPtr jarg1);

		// Token: 0x0600006F RID: 111
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_assign")]
		public static extern void RailArrayZoneInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000070 RID: 112
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_buf")]
		public static extern IntPtr RailArrayZoneInfo_buf(IntPtr jarg1);

		// Token: 0x06000071 RID: 113
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_size")]
		public static extern uint RailArrayZoneInfo_size(IntPtr jarg1);

		// Token: 0x06000072 RID: 114
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_Item")]
		public static extern IntPtr RailArrayZoneInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000073 RID: 115
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_resize")]
		public static extern void RailArrayZoneInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000074 RID: 116
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_push_back")]
		public static extern void RailArrayZoneInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000075 RID: 117
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_clear")]
		public static extern void RailArrayZoneInfo_clear(IntPtr jarg1);

		// Token: 0x06000076 RID: 118
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayZoneInfo_erase")]
		public static extern void RailArrayZoneInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000077 RID: 119
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayuint32_t__SWIG_0")]
		public static extern IntPtr new_RailArrayuint32_t__SWIG_0();

		// Token: 0x06000078 RID: 120
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayuint32_t__SWIG_1")]
		public static extern IntPtr new_RailArrayuint32_t__SWIG_1(uint jarg1);

		// Token: 0x06000079 RID: 121
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayuint32_t__SWIG_2")]
		public static extern IntPtr new_RailArrayuint32_t__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600007A RID: 122
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayuint32_t__SWIG_3")]
		public static extern IntPtr new_RailArrayuint32_t__SWIG_3(IntPtr jarg1);

		// Token: 0x0600007B RID: 123
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_SetValue")]
		public static extern IntPtr RailArrayuint32_t_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600007C RID: 124
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayuint32_t")]
		public static extern void delete_RailArrayuint32_t(IntPtr jarg1);

		// Token: 0x0600007D RID: 125
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_assign")]
		public static extern void RailArrayuint32_t_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600007E RID: 126
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_buf")]
		public static extern IntPtr RailArrayuint32_t_buf(IntPtr jarg1);

		// Token: 0x0600007F RID: 127
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_size")]
		public static extern uint RailArrayuint32_t_size(IntPtr jarg1);

		// Token: 0x06000080 RID: 128
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_Item")]
		public static extern IntPtr RailArrayuint32_t_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000081 RID: 129
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_resize")]
		public static extern void RailArrayuint32_t_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000082 RID: 130
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_push_back")]
		public static extern void RailArrayuint32_t_push_back(IntPtr jarg1, uint jarg2);

		// Token: 0x06000083 RID: 131
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_clear")]
		public static extern void RailArrayuint32_t_clear(IntPtr jarg1);

		// Token: 0x06000084 RID: 132
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayuint32_t_erase")]
		public static extern void RailArrayuint32_t_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000085 RID: 133
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayRailAssetInfo__SWIG_0();

		// Token: 0x06000086 RID: 134
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayRailAssetInfo__SWIG_1(uint jarg1);

		// Token: 0x06000087 RID: 135
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayRailAssetInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000088 RID: 136
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayRailAssetInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x06000089 RID: 137
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_SetValue")]
		public static extern IntPtr RailArrayRailAssetInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600008A RID: 138
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailAssetInfo")]
		public static extern void delete_RailArrayRailAssetInfo(IntPtr jarg1);

		// Token: 0x0600008B RID: 139
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_assign")]
		public static extern void RailArrayRailAssetInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600008C RID: 140
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_buf")]
		public static extern IntPtr RailArrayRailAssetInfo_buf(IntPtr jarg1);

		// Token: 0x0600008D RID: 141
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_size")]
		public static extern uint RailArrayRailAssetInfo_size(IntPtr jarg1);

		// Token: 0x0600008E RID: 142
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_Item")]
		public static extern IntPtr RailArrayRailAssetInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600008F RID: 143
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_resize")]
		public static extern void RailArrayRailAssetInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000090 RID: 144
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_push_back")]
		public static extern void RailArrayRailAssetInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000091 RID: 145
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_clear")]
		public static extern void RailArrayRailAssetInfo_clear(IntPtr jarg1);

		// Token: 0x06000092 RID: 146
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetInfo_erase")]
		public static extern void RailArrayRailAssetInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000093 RID: 147
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetItem__SWIG_0")]
		public static extern IntPtr new_RailArrayRailAssetItem__SWIG_0();

		// Token: 0x06000094 RID: 148
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetItem__SWIG_1")]
		public static extern IntPtr new_RailArrayRailAssetItem__SWIG_1(uint jarg1);

		// Token: 0x06000095 RID: 149
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetItem__SWIG_2")]
		public static extern IntPtr new_RailArrayRailAssetItem__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000096 RID: 150
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetItem__SWIG_3")]
		public static extern IntPtr new_RailArrayRailAssetItem__SWIG_3(IntPtr jarg1);

		// Token: 0x06000097 RID: 151
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_SetValue")]
		public static extern IntPtr RailArrayRailAssetItem_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000098 RID: 152
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailAssetItem")]
		public static extern void delete_RailArrayRailAssetItem(IntPtr jarg1);

		// Token: 0x06000099 RID: 153
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_assign")]
		public static extern void RailArrayRailAssetItem_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600009A RID: 154
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_buf")]
		public static extern IntPtr RailArrayRailAssetItem_buf(IntPtr jarg1);

		// Token: 0x0600009B RID: 155
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_size")]
		public static extern uint RailArrayRailAssetItem_size(IntPtr jarg1);

		// Token: 0x0600009C RID: 156
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_Item")]
		public static extern IntPtr RailArrayRailAssetItem_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600009D RID: 157
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_resize")]
		public static extern void RailArrayRailAssetItem_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600009E RID: 158
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_push_back")]
		public static extern void RailArrayRailAssetItem_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600009F RID: 159
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_clear")]
		public static extern void RailArrayRailAssetItem_clear(IntPtr jarg1);

		// Token: 0x060000A0 RID: 160
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetItem_erase")]
		public static extern void RailArrayRailAssetItem_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060000A1 RID: 161
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayPlayerPersonalInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayPlayerPersonalInfo__SWIG_0();

		// Token: 0x060000A2 RID: 162
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayPlayerPersonalInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayPlayerPersonalInfo__SWIG_1(uint jarg1);

		// Token: 0x060000A3 RID: 163
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayPlayerPersonalInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayPlayerPersonalInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060000A4 RID: 164
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayPlayerPersonalInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayPlayerPersonalInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x060000A5 RID: 165
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_SetValue")]
		public static extern IntPtr RailArrayPlayerPersonalInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000A6 RID: 166
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayPlayerPersonalInfo")]
		public static extern void delete_RailArrayPlayerPersonalInfo(IntPtr jarg1);

		// Token: 0x060000A7 RID: 167
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_assign")]
		public static extern void RailArrayPlayerPersonalInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060000A8 RID: 168
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_buf")]
		public static extern IntPtr RailArrayPlayerPersonalInfo_buf(IntPtr jarg1);

		// Token: 0x060000A9 RID: 169
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_size")]
		public static extern uint RailArrayPlayerPersonalInfo_size(IntPtr jarg1);

		// Token: 0x060000AA RID: 170
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_Item")]
		public static extern IntPtr RailArrayPlayerPersonalInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060000AB RID: 171
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_resize")]
		public static extern void RailArrayPlayerPersonalInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060000AC RID: 172
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_push_back")]
		public static extern void RailArrayPlayerPersonalInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000AD RID: 173
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_clear")]
		public static extern void RailArrayPlayerPersonalInfo_clear(IntPtr jarg1);

		// Token: 0x060000AE RID: 174
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayPlayerPersonalInfo_erase")]
		public static extern void RailArrayPlayerPersonalInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060000AF RID: 175
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_0")]
		public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_0();

		// Token: 0x060000B0 RID: 176
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_1")]
		public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_1(uint jarg1);

		// Token: 0x060000B1 RID: 177
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_2")]
		public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060000B2 RID: 178
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_3")]
		public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_3(IntPtr jarg1);

		// Token: 0x060000B3 RID: 179
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_SetValue")]
		public static extern IntPtr RailArrayEnumRailWorkFileClass_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000B4 RID: 180
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayEnumRailWorkFileClass")]
		public static extern void delete_RailArrayEnumRailWorkFileClass(IntPtr jarg1);

		// Token: 0x060000B5 RID: 181
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_assign")]
		public static extern void RailArrayEnumRailWorkFileClass_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060000B6 RID: 182
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_buf")]
		public static extern IntPtr RailArrayEnumRailWorkFileClass_buf(IntPtr jarg1);

		// Token: 0x060000B7 RID: 183
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_size")]
		public static extern uint RailArrayEnumRailWorkFileClass_size(IntPtr jarg1);

		// Token: 0x060000B8 RID: 184
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_Item")]
		public static extern IntPtr RailArrayEnumRailWorkFileClass_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060000B9 RID: 185
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_resize")]
		public static extern void RailArrayEnumRailWorkFileClass_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060000BA RID: 186
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_push_back")]
		public static extern void RailArrayEnumRailWorkFileClass_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000BB RID: 187
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_clear")]
		public static extern void RailArrayEnumRailWorkFileClass_clear(IntPtr jarg1);

		// Token: 0x060000BC RID: 188
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailWorkFileClass_erase")]
		public static extern void RailArrayEnumRailWorkFileClass_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060000BD RID: 189
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailWorkFileClass__SWIG_4")]
		public static extern IntPtr new_RailArrayEnumRailWorkFileClass__SWIG_4(IntPtr jarg1);

		// Token: 0x060000BE RID: 190
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetProperty__SWIG_0")]
		public static extern IntPtr new_RailArrayRailAssetProperty__SWIG_0();

		// Token: 0x060000BF RID: 191
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetProperty__SWIG_1")]
		public static extern IntPtr new_RailArrayRailAssetProperty__SWIG_1(uint jarg1);

		// Token: 0x060000C0 RID: 192
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetProperty__SWIG_2")]
		public static extern IntPtr new_RailArrayRailAssetProperty__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060000C1 RID: 193
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailAssetProperty__SWIG_3")]
		public static extern IntPtr new_RailArrayRailAssetProperty__SWIG_3(IntPtr jarg1);

		// Token: 0x060000C2 RID: 194
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_SetValue")]
		public static extern IntPtr RailArrayRailAssetProperty_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000C3 RID: 195
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailAssetProperty")]
		public static extern void delete_RailArrayRailAssetProperty(IntPtr jarg1);

		// Token: 0x060000C4 RID: 196
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_assign")]
		public static extern void RailArrayRailAssetProperty_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060000C5 RID: 197
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_buf")]
		public static extern IntPtr RailArrayRailAssetProperty_buf(IntPtr jarg1);

		// Token: 0x060000C6 RID: 198
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_size")]
		public static extern uint RailArrayRailAssetProperty_size(IntPtr jarg1);

		// Token: 0x060000C7 RID: 199
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_Item")]
		public static extern IntPtr RailArrayRailAssetProperty_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060000C8 RID: 200
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_resize")]
		public static extern void RailArrayRailAssetProperty_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060000C9 RID: 201
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_push_back")]
		public static extern void RailArrayRailAssetProperty_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000CA RID: 202
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_clear")]
		public static extern void RailArrayRailAssetProperty_clear(IntPtr jarg1);

		// Token: 0x060000CB RID: 203
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailAssetProperty_erase")]
		public static extern void RailArrayRailAssetProperty_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060000CC RID: 204
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilterKey__SWIG_0")]
		public static extern IntPtr new_RailArrayGameServerListFilterKey__SWIG_0();

		// Token: 0x060000CD RID: 205
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilterKey__SWIG_1")]
		public static extern IntPtr new_RailArrayGameServerListFilterKey__SWIG_1(uint jarg1);

		// Token: 0x060000CE RID: 206
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilterKey__SWIG_2")]
		public static extern IntPtr new_RailArrayGameServerListFilterKey__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060000CF RID: 207
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilterKey__SWIG_3")]
		public static extern IntPtr new_RailArrayGameServerListFilterKey__SWIG_3(IntPtr jarg1);

		// Token: 0x060000D0 RID: 208
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_SetValue")]
		public static extern IntPtr RailArrayGameServerListFilterKey_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000D1 RID: 209
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerListFilterKey")]
		public static extern void delete_RailArrayGameServerListFilterKey(IntPtr jarg1);

		// Token: 0x060000D2 RID: 210
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_assign")]
		public static extern void RailArrayGameServerListFilterKey_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060000D3 RID: 211
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_buf")]
		public static extern IntPtr RailArrayGameServerListFilterKey_buf(IntPtr jarg1);

		// Token: 0x060000D4 RID: 212
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_size")]
		public static extern uint RailArrayGameServerListFilterKey_size(IntPtr jarg1);

		// Token: 0x060000D5 RID: 213
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_Item")]
		public static extern IntPtr RailArrayGameServerListFilterKey_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060000D6 RID: 214
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_resize")]
		public static extern void RailArrayGameServerListFilterKey_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060000D7 RID: 215
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_push_back")]
		public static extern void RailArrayGameServerListFilterKey_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000D8 RID: 216
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_clear")]
		public static extern void RailArrayGameServerListFilterKey_clear(IntPtr jarg1);

		// Token: 0x060000D9 RID: 217
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilterKey_erase")]
		public static extern void RailArrayGameServerListFilterKey_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060000DA RID: 218
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailStreamFileInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayRailStreamFileInfo__SWIG_0();

		// Token: 0x060000DB RID: 219
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailStreamFileInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayRailStreamFileInfo__SWIG_1(uint jarg1);

		// Token: 0x060000DC RID: 220
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailStreamFileInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayRailStreamFileInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060000DD RID: 221
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailStreamFileInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayRailStreamFileInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x060000DE RID: 222
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_SetValue")]
		public static extern IntPtr RailArrayRailStreamFileInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000DF RID: 223
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailStreamFileInfo")]
		public static extern void delete_RailArrayRailStreamFileInfo(IntPtr jarg1);

		// Token: 0x060000E0 RID: 224
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_assign")]
		public static extern void RailArrayRailStreamFileInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060000E1 RID: 225
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_buf")]
		public static extern IntPtr RailArrayRailStreamFileInfo_buf(IntPtr jarg1);

		// Token: 0x060000E2 RID: 226
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_size")]
		public static extern uint RailArrayRailStreamFileInfo_size(IntPtr jarg1);

		// Token: 0x060000E3 RID: 227
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_Item")]
		public static extern IntPtr RailArrayRailStreamFileInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060000E4 RID: 228
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_resize")]
		public static extern void RailArrayRailStreamFileInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060000E5 RID: 229
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_push_back")]
		public static extern void RailArrayRailStreamFileInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000E6 RID: 230
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_clear")]
		public static extern void RailArrayRailStreamFileInfo_clear(IntPtr jarg1);

		// Token: 0x060000E7 RID: 231
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailStreamFileInfo_erase")]
		public static extern void RailArrayRailStreamFileInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060000E8 RID: 232
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilter__SWIG_0")]
		public static extern IntPtr new_RailArrayRoomInfoListFilter__SWIG_0();

		// Token: 0x060000E9 RID: 233
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilter__SWIG_1")]
		public static extern IntPtr new_RailArrayRoomInfoListFilter__SWIG_1(uint jarg1);

		// Token: 0x060000EA RID: 234
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilter__SWIG_2")]
		public static extern IntPtr new_RailArrayRoomInfoListFilter__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060000EB RID: 235
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilter__SWIG_3")]
		public static extern IntPtr new_RailArrayRoomInfoListFilter__SWIG_3(IntPtr jarg1);

		// Token: 0x060000EC RID: 236
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_SetValue")]
		public static extern IntPtr RailArrayRoomInfoListFilter_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000ED RID: 237
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRoomInfoListFilter")]
		public static extern void delete_RailArrayRoomInfoListFilter(IntPtr jarg1);

		// Token: 0x060000EE RID: 238
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_assign")]
		public static extern void RailArrayRoomInfoListFilter_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060000EF RID: 239
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_buf")]
		public static extern IntPtr RailArrayRoomInfoListFilter_buf(IntPtr jarg1);

		// Token: 0x060000F0 RID: 240
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_size")]
		public static extern uint RailArrayRoomInfoListFilter_size(IntPtr jarg1);

		// Token: 0x060000F1 RID: 241
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_Item")]
		public static extern IntPtr RailArrayRoomInfoListFilter_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060000F2 RID: 242
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_resize")]
		public static extern void RailArrayRoomInfoListFilter_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060000F3 RID: 243
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_push_back")]
		public static extern void RailArrayRoomInfoListFilter_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000F4 RID: 244
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_clear")]
		public static extern void RailArrayRoomInfoListFilter_clear(IntPtr jarg1);

		// Token: 0x060000F5 RID: 245
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilter_erase")]
		public static extern void RailArrayRoomInfoListFilter_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060000F6 RID: 246
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilter__SWIG_0")]
		public static extern IntPtr new_RailArrayGameServerListFilter__SWIG_0();

		// Token: 0x060000F7 RID: 247
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilter__SWIG_1")]
		public static extern IntPtr new_RailArrayGameServerListFilter__SWIG_1(uint jarg1);

		// Token: 0x060000F8 RID: 248
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilter__SWIG_2")]
		public static extern IntPtr new_RailArrayGameServerListFilter__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060000F9 RID: 249
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListFilter__SWIG_3")]
		public static extern IntPtr new_RailArrayGameServerListFilter__SWIG_3(IntPtr jarg1);

		// Token: 0x060000FA RID: 250
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_SetValue")]
		public static extern IntPtr RailArrayGameServerListFilter_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060000FB RID: 251
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerListFilter")]
		public static extern void delete_RailArrayGameServerListFilter(IntPtr jarg1);

		// Token: 0x060000FC RID: 252
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_assign")]
		public static extern void RailArrayGameServerListFilter_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060000FD RID: 253
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_buf")]
		public static extern IntPtr RailArrayGameServerListFilter_buf(IntPtr jarg1);

		// Token: 0x060000FE RID: 254
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_size")]
		public static extern uint RailArrayGameServerListFilter_size(IntPtr jarg1);

		// Token: 0x060000FF RID: 255
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_Item")]
		public static extern IntPtr RailArrayGameServerListFilter_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000100 RID: 256
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_resize")]
		public static extern void RailArrayGameServerListFilter_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000101 RID: 257
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_push_back")]
		public static extern void RailArrayGameServerListFilter_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000102 RID: 258
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_clear")]
		public static extern void RailArrayGameServerListFilter_clear(IntPtr jarg1);

		// Token: 0x06000103 RID: 259
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListFilter_erase")]
		public static extern void RailArrayGameServerListFilter_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000104 RID: 260
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_0")]
		public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_0();

		// Token: 0x06000105 RID: 261
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_1")]
		public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_1(uint jarg1);

		// Token: 0x06000106 RID: 262
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_2")]
		public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000107 RID: 263
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_3")]
		public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_3(IntPtr jarg1);

		// Token: 0x06000108 RID: 264
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_SetValue")]
		public static extern IntPtr RailArrayEnumRailSpaceWorkType_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000109 RID: 265
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayEnumRailSpaceWorkType")]
		public static extern void delete_RailArrayEnumRailSpaceWorkType(IntPtr jarg1);

		// Token: 0x0600010A RID: 266
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_assign")]
		public static extern void RailArrayEnumRailSpaceWorkType_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600010B RID: 267
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_buf")]
		public static extern IntPtr RailArrayEnumRailSpaceWorkType_buf(IntPtr jarg1);

		// Token: 0x0600010C RID: 268
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_size")]
		public static extern uint RailArrayEnumRailSpaceWorkType_size(IntPtr jarg1);

		// Token: 0x0600010D RID: 269
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_Item")]
		public static extern IntPtr RailArrayEnumRailSpaceWorkType_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600010E RID: 270
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_resize")]
		public static extern void RailArrayEnumRailSpaceWorkType_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600010F RID: 271
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_push_back")]
		public static extern void RailArrayEnumRailSpaceWorkType_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000110 RID: 272
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_clear")]
		public static extern void RailArrayEnumRailSpaceWorkType_clear(IntPtr jarg1);

		// Token: 0x06000111 RID: 273
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailSpaceWorkType_erase")]
		public static extern void RailArrayEnumRailSpaceWorkType_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000112 RID: 274
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailSpaceWorkType__SWIG_4")]
		public static extern IntPtr new_RailArrayEnumRailSpaceWorkType__SWIG_4(IntPtr jarg1);

		// Token: 0x06000113 RID: 275
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailUsersLimits__SWIG_0")]
		public static extern IntPtr new_RailArrayEnumRailUsersLimits__SWIG_0();

		// Token: 0x06000114 RID: 276
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailUsersLimits__SWIG_1")]
		public static extern IntPtr new_RailArrayEnumRailUsersLimits__SWIG_1(uint jarg1);

		// Token: 0x06000115 RID: 277
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailUsersLimits__SWIG_2")]
		public static extern IntPtr new_RailArrayEnumRailUsersLimits__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000116 RID: 278
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailUsersLimits__SWIG_3")]
		public static extern IntPtr new_RailArrayEnumRailUsersLimits__SWIG_3(IntPtr jarg1);

		// Token: 0x06000117 RID: 279
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_SetValue")]
		public static extern IntPtr RailArrayEnumRailUsersLimits_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000118 RID: 280
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayEnumRailUsersLimits")]
		public static extern void delete_RailArrayEnumRailUsersLimits(IntPtr jarg1);

		// Token: 0x06000119 RID: 281
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_assign")]
		public static extern void RailArrayEnumRailUsersLimits_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600011A RID: 282
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_buf")]
		public static extern IntPtr RailArrayEnumRailUsersLimits_buf(IntPtr jarg1);

		// Token: 0x0600011B RID: 283
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_size")]
		public static extern uint RailArrayEnumRailUsersLimits_size(IntPtr jarg1);

		// Token: 0x0600011C RID: 284
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_Item")]
		public static extern IntPtr RailArrayEnumRailUsersLimits_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600011D RID: 285
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_resize")]
		public static extern void RailArrayEnumRailUsersLimits_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600011E RID: 286
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_push_back")]
		public static extern void RailArrayEnumRailUsersLimits_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600011F RID: 287
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_clear")]
		public static extern void RailArrayEnumRailUsersLimits_clear(IntPtr jarg1);

		// Token: 0x06000120 RID: 288
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayEnumRailUsersLimits_erase")]
		public static extern void RailArrayEnumRailUsersLimits_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000121 RID: 289
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayEnumRailUsersLimits__SWIG_4")]
		public static extern IntPtr new_RailArrayEnumRailUsersLimits__SWIG_4(IntPtr jarg1);

		// Token: 0x06000122 RID: 290
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPlayedWithFriendsGameItem__SWIG_0")]
		public static extern IntPtr new_RailArrayRailPlayedWithFriendsGameItem__SWIG_0();

		// Token: 0x06000123 RID: 291
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPlayedWithFriendsGameItem__SWIG_1")]
		public static extern IntPtr new_RailArrayRailPlayedWithFriendsGameItem__SWIG_1(uint jarg1);

		// Token: 0x06000124 RID: 292
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPlayedWithFriendsGameItem__SWIG_2")]
		public static extern IntPtr new_RailArrayRailPlayedWithFriendsGameItem__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000125 RID: 293
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPlayedWithFriendsGameItem__SWIG_3")]
		public static extern IntPtr new_RailArrayRailPlayedWithFriendsGameItem__SWIG_3(IntPtr jarg1);

		// Token: 0x06000126 RID: 294
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_SetValue")]
		public static extern IntPtr RailArrayRailPlayedWithFriendsGameItem_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000127 RID: 295
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailPlayedWithFriendsGameItem")]
		public static extern void delete_RailArrayRailPlayedWithFriendsGameItem(IntPtr jarg1);

		// Token: 0x06000128 RID: 296
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_assign")]
		public static extern void RailArrayRailPlayedWithFriendsGameItem_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000129 RID: 297
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_buf")]
		public static extern IntPtr RailArrayRailPlayedWithFriendsGameItem_buf(IntPtr jarg1);

		// Token: 0x0600012A RID: 298
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_size")]
		public static extern uint RailArrayRailPlayedWithFriendsGameItem_size(IntPtr jarg1);

		// Token: 0x0600012B RID: 299
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_Item")]
		public static extern IntPtr RailArrayRailPlayedWithFriendsGameItem_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600012C RID: 300
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_resize")]
		public static extern void RailArrayRailPlayedWithFriendsGameItem_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600012D RID: 301
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_push_back")]
		public static extern void RailArrayRailPlayedWithFriendsGameItem_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600012E RID: 302
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_clear")]
		public static extern void RailArrayRailPlayedWithFriendsGameItem_clear(IntPtr jarg1);

		// Token: 0x0600012F RID: 303
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsGameItem_erase")]
		public static extern void RailArrayRailPlayedWithFriendsGameItem_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000130 RID: 304
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArraySpaceWorkID__SWIG_0")]
		public static extern IntPtr new_RailArraySpaceWorkID__SWIG_0();

		// Token: 0x06000131 RID: 305
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArraySpaceWorkID__SWIG_1")]
		public static extern IntPtr new_RailArraySpaceWorkID__SWIG_1(uint jarg1);

		// Token: 0x06000132 RID: 306
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArraySpaceWorkID__SWIG_2")]
		public static extern IntPtr new_RailArraySpaceWorkID__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000133 RID: 307
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArraySpaceWorkID__SWIG_3")]
		public static extern IntPtr new_RailArraySpaceWorkID__SWIG_3(IntPtr jarg1);

		// Token: 0x06000134 RID: 308
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_SetValue")]
		public static extern IntPtr RailArraySpaceWorkID_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000135 RID: 309
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArraySpaceWorkID")]
		public static extern void delete_RailArraySpaceWorkID(IntPtr jarg1);

		// Token: 0x06000136 RID: 310
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_assign")]
		public static extern void RailArraySpaceWorkID_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000137 RID: 311
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_buf")]
		public static extern IntPtr RailArraySpaceWorkID_buf(IntPtr jarg1);

		// Token: 0x06000138 RID: 312
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_size")]
		public static extern uint RailArraySpaceWorkID_size(IntPtr jarg1);

		// Token: 0x06000139 RID: 313
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_Item")]
		public static extern IntPtr RailArraySpaceWorkID_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600013A RID: 314
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_resize")]
		public static extern void RailArraySpaceWorkID_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600013B RID: 315
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_push_back")]
		public static extern void RailArraySpaceWorkID_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600013C RID: 316
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_clear")]
		public static extern void RailArraySpaceWorkID_clear(IntPtr jarg1);

		// Token: 0x0600013D RID: 317
		[DllImport("rail_api", EntryPoint = "CSharp_RailArraySpaceWorkID_erase")]
		public static extern void RailArraySpaceWorkID_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x0600013E RID: 318
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListSorter__SWIG_0")]
		public static extern IntPtr new_RailArrayRoomInfoListSorter__SWIG_0();

		// Token: 0x0600013F RID: 319
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListSorter__SWIG_1")]
		public static extern IntPtr new_RailArrayRoomInfoListSorter__SWIG_1(uint jarg1);

		// Token: 0x06000140 RID: 320
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListSorter__SWIG_2")]
		public static extern IntPtr new_RailArrayRoomInfoListSorter__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000141 RID: 321
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListSorter__SWIG_3")]
		public static extern IntPtr new_RailArrayRoomInfoListSorter__SWIG_3(IntPtr jarg1);

		// Token: 0x06000142 RID: 322
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_SetValue")]
		public static extern IntPtr RailArrayRoomInfoListSorter_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000143 RID: 323
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRoomInfoListSorter")]
		public static extern void delete_RailArrayRoomInfoListSorter(IntPtr jarg1);

		// Token: 0x06000144 RID: 324
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_assign")]
		public static extern void RailArrayRoomInfoListSorter_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000145 RID: 325
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_buf")]
		public static extern IntPtr RailArrayRoomInfoListSorter_buf(IntPtr jarg1);

		// Token: 0x06000146 RID: 326
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_size")]
		public static extern uint RailArrayRoomInfoListSorter_size(IntPtr jarg1);

		// Token: 0x06000147 RID: 327
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_Item")]
		public static extern IntPtr RailArrayRoomInfoListSorter_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000148 RID: 328
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_resize")]
		public static extern void RailArrayRoomInfoListSorter_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000149 RID: 329
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_push_back")]
		public static extern void RailArrayRoomInfoListSorter_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600014A RID: 330
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_clear")]
		public static extern void RailArrayRoomInfoListSorter_clear(IntPtr jarg1);

		// Token: 0x0600014B RID: 331
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListSorter_erase")]
		public static extern void RailArrayRoomInfoListSorter_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x0600014C RID: 332
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValueResult__SWIG_0")]
		public static extern IntPtr new_RailArrayRailKeyValueResult__SWIG_0();

		// Token: 0x0600014D RID: 333
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValueResult__SWIG_1")]
		public static extern IntPtr new_RailArrayRailKeyValueResult__SWIG_1(uint jarg1);

		// Token: 0x0600014E RID: 334
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValueResult__SWIG_2")]
		public static extern IntPtr new_RailArrayRailKeyValueResult__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600014F RID: 335
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValueResult__SWIG_3")]
		public static extern IntPtr new_RailArrayRailKeyValueResult__SWIG_3(IntPtr jarg1);

		// Token: 0x06000150 RID: 336
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_SetValue")]
		public static extern IntPtr RailArrayRailKeyValueResult_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000151 RID: 337
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailKeyValueResult")]
		public static extern void delete_RailArrayRailKeyValueResult(IntPtr jarg1);

		// Token: 0x06000152 RID: 338
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_assign")]
		public static extern void RailArrayRailKeyValueResult_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000153 RID: 339
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_buf")]
		public static extern IntPtr RailArrayRailKeyValueResult_buf(IntPtr jarg1);

		// Token: 0x06000154 RID: 340
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_size")]
		public static extern uint RailArrayRailKeyValueResult_size(IntPtr jarg1);

		// Token: 0x06000155 RID: 341
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_Item")]
		public static extern IntPtr RailArrayRailKeyValueResult_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000156 RID: 342
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_resize")]
		public static extern void RailArrayRailKeyValueResult_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000157 RID: 343
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_push_back")]
		public static extern void RailArrayRailKeyValueResult_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000158 RID: 344
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_clear")]
		public static extern void RailArrayRailKeyValueResult_clear(IntPtr jarg1);

		// Token: 0x06000159 RID: 345
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValueResult_erase")]
		public static extern void RailArrayRailKeyValueResult_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x0600015A RID: 346
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_0")]
		public static extern IntPtr new_RailArrayRailString__SWIG_0();

		// Token: 0x0600015B RID: 347
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_1")]
		public static extern IntPtr new_RailArrayRailString__SWIG_1(uint jarg1);

		// Token: 0x0600015C RID: 348
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_2")]
		public static extern IntPtr new_RailArrayRailString__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600015D RID: 349
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_3")]
		public static extern IntPtr new_RailArrayRailString__SWIG_3(IntPtr jarg1);

		// Token: 0x0600015E RID: 350
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_SetValue")]
		public static extern IntPtr RailArrayRailString_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600015F RID: 351
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailString")]
		public static extern void delete_RailArrayRailString(IntPtr jarg1);

		// Token: 0x06000160 RID: 352
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_assign")]
		public static extern void RailArrayRailString_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000161 RID: 353
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_buf")]
		public static extern IntPtr RailArrayRailString_buf(IntPtr jarg1);

		// Token: 0x06000162 RID: 354
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_size")]
		public static extern uint RailArrayRailString_size(IntPtr jarg1);

		// Token: 0x06000163 RID: 355
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_Item")]
		public static extern IntPtr RailArrayRailString_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000164 RID: 356
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_resize")]
		public static extern void RailArrayRailString_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000165 RID: 357
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_push_back")]
		public static extern void RailArrayRailString_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000166 RID: 358
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_clear")]
		public static extern void RailArrayRailString_clear(IntPtr jarg1);

		// Token: 0x06000167 RID: 359
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailString_erase")]
		public static extern void RailArrayRailString_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000168 RID: 360
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailString__SWIG_4")]
		public static extern IntPtr new_RailArrayRailString__SWIG_4(IntPtr jarg1);

		// Token: 0x06000169 RID: 361
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailFriendInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayRailFriendInfo__SWIG_0();

		// Token: 0x0600016A RID: 362
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailFriendInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayRailFriendInfo__SWIG_1(uint jarg1);

		// Token: 0x0600016B RID: 363
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailFriendInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayRailFriendInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600016C RID: 364
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailFriendInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayRailFriendInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x0600016D RID: 365
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_SetValue")]
		public static extern IntPtr RailArrayRailFriendInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600016E RID: 366
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailFriendInfo")]
		public static extern void delete_RailArrayRailFriendInfo(IntPtr jarg1);

		// Token: 0x0600016F RID: 367
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_assign")]
		public static extern void RailArrayRailFriendInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000170 RID: 368
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_buf")]
		public static extern IntPtr RailArrayRailFriendInfo_buf(IntPtr jarg1);

		// Token: 0x06000171 RID: 369
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_size")]
		public static extern uint RailArrayRailFriendInfo_size(IntPtr jarg1);

		// Token: 0x06000172 RID: 370
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_Item")]
		public static extern IntPtr RailArrayRailFriendInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000173 RID: 371
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_resize")]
		public static extern void RailArrayRailFriendInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000174 RID: 372
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_push_back")]
		public static extern void RailArrayRailFriendInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000175 RID: 373
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_clear")]
		public static extern void RailArrayRailFriendInfo_clear(IntPtr jarg1);

		// Token: 0x06000176 RID: 374
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendInfo_erase")]
		public static extern void RailArrayRailFriendInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000177 RID: 375
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailZoneID__SWIG_0")]
		public static extern IntPtr new_RailArrayRailZoneID__SWIG_0();

		// Token: 0x06000178 RID: 376
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailZoneID__SWIG_1")]
		public static extern IntPtr new_RailArrayRailZoneID__SWIG_1(uint jarg1);

		// Token: 0x06000179 RID: 377
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailZoneID__SWIG_2")]
		public static extern IntPtr new_RailArrayRailZoneID__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600017A RID: 378
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailZoneID__SWIG_3")]
		public static extern IntPtr new_RailArrayRailZoneID__SWIG_3(IntPtr jarg1);

		// Token: 0x0600017B RID: 379
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_SetValue")]
		public static extern IntPtr RailArrayRailZoneID_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600017C RID: 380
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailZoneID")]
		public static extern void delete_RailArrayRailZoneID(IntPtr jarg1);

		// Token: 0x0600017D RID: 381
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_assign")]
		public static extern void RailArrayRailZoneID_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600017E RID: 382
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_buf")]
		public static extern IntPtr RailArrayRailZoneID_buf(IntPtr jarg1);

		// Token: 0x0600017F RID: 383
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_size")]
		public static extern uint RailArrayRailZoneID_size(IntPtr jarg1);

		// Token: 0x06000180 RID: 384
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_Item")]
		public static extern IntPtr RailArrayRailZoneID_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000181 RID: 385
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_resize")]
		public static extern void RailArrayRailZoneID_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000182 RID: 386
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_push_back")]
		public static extern void RailArrayRailZoneID_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000183 RID: 387
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_clear")]
		public static extern void RailArrayRailZoneID_clear(IntPtr jarg1);

		// Token: 0x06000184 RID: 388
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailZoneID_erase")]
		public static extern void RailArrayRailZoneID_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000185 RID: 389
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValue__SWIG_0")]
		public static extern IntPtr new_RailArrayRailKeyValue__SWIG_0();

		// Token: 0x06000186 RID: 390
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValue__SWIG_1")]
		public static extern IntPtr new_RailArrayRailKeyValue__SWIG_1(uint jarg1);

		// Token: 0x06000187 RID: 391
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValue__SWIG_2")]
		public static extern IntPtr new_RailArrayRailKeyValue__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000188 RID: 392
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailKeyValue__SWIG_3")]
		public static extern IntPtr new_RailArrayRailKeyValue__SWIG_3(IntPtr jarg1);

		// Token: 0x06000189 RID: 393
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_SetValue")]
		public static extern IntPtr RailArrayRailKeyValue_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600018A RID: 394
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailKeyValue")]
		public static extern void delete_RailArrayRailKeyValue(IntPtr jarg1);

		// Token: 0x0600018B RID: 395
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_assign")]
		public static extern void RailArrayRailKeyValue_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600018C RID: 396
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_buf")]
		public static extern IntPtr RailArrayRailKeyValue_buf(IntPtr jarg1);

		// Token: 0x0600018D RID: 397
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_size")]
		public static extern uint RailArrayRailKeyValue_size(IntPtr jarg1);

		// Token: 0x0600018E RID: 398
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_Item")]
		public static extern IntPtr RailArrayRailKeyValue_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600018F RID: 399
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_resize")]
		public static extern void RailArrayRailKeyValue_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000190 RID: 400
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_push_back")]
		public static extern void RailArrayRailKeyValue_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000191 RID: 401
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_clear")]
		public static extern void RailArrayRailKeyValue_clear(IntPtr jarg1);

		// Token: 0x06000192 RID: 402
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailKeyValue_erase")]
		public static extern void RailArrayRailKeyValue_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000193 RID: 403
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkVoteDetail__SWIG_0")]
		public static extern IntPtr new_RailArrayRailSpaceWorkVoteDetail__SWIG_0();

		// Token: 0x06000194 RID: 404
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkVoteDetail__SWIG_1")]
		public static extern IntPtr new_RailArrayRailSpaceWorkVoteDetail__SWIG_1(uint jarg1);

		// Token: 0x06000195 RID: 405
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkVoteDetail__SWIG_2")]
		public static extern IntPtr new_RailArrayRailSpaceWorkVoteDetail__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000196 RID: 406
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkVoteDetail__SWIG_3")]
		public static extern IntPtr new_RailArrayRailSpaceWorkVoteDetail__SWIG_3(IntPtr jarg1);

		// Token: 0x06000197 RID: 407
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_SetValue")]
		public static extern IntPtr RailArrayRailSpaceWorkVoteDetail_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000198 RID: 408
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailSpaceWorkVoteDetail")]
		public static extern void delete_RailArrayRailSpaceWorkVoteDetail(IntPtr jarg1);

		// Token: 0x06000199 RID: 409
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_assign")]
		public static extern void RailArrayRailSpaceWorkVoteDetail_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600019A RID: 410
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_buf")]
		public static extern IntPtr RailArrayRailSpaceWorkVoteDetail_buf(IntPtr jarg1);

		// Token: 0x0600019B RID: 411
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_size")]
		public static extern uint RailArrayRailSpaceWorkVoteDetail_size(IntPtr jarg1);

		// Token: 0x0600019C RID: 412
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_Item")]
		public static extern IntPtr RailArrayRailSpaceWorkVoteDetail_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600019D RID: 413
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_resize")]
		public static extern void RailArrayRailSpaceWorkVoteDetail_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600019E RID: 414
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_push_back")]
		public static extern void RailArrayRailSpaceWorkVoteDetail_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600019F RID: 415
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_clear")]
		public static extern void RailArrayRailSpaceWorkVoteDetail_clear(IntPtr jarg1);

		// Token: 0x060001A0 RID: 416
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkVoteDetail_erase")]
		public static extern void RailArrayRailSpaceWorkVoteDetail_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060001A1 RID: 417
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSmallObjectState__SWIG_0")]
		public static extern IntPtr new_RailArrayRailSmallObjectState__SWIG_0();

		// Token: 0x060001A2 RID: 418
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSmallObjectState__SWIG_1")]
		public static extern IntPtr new_RailArrayRailSmallObjectState__SWIG_1(uint jarg1);

		// Token: 0x060001A3 RID: 419
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSmallObjectState__SWIG_2")]
		public static extern IntPtr new_RailArrayRailSmallObjectState__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060001A4 RID: 420
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSmallObjectState__SWIG_3")]
		public static extern IntPtr new_RailArrayRailSmallObjectState__SWIG_3(IntPtr jarg1);

		// Token: 0x060001A5 RID: 421
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_SetValue")]
		public static extern IntPtr RailArrayRailSmallObjectState_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001A6 RID: 422
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailSmallObjectState")]
		public static extern void delete_RailArrayRailSmallObjectState(IntPtr jarg1);

		// Token: 0x060001A7 RID: 423
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_assign")]
		public static extern void RailArrayRailSmallObjectState_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060001A8 RID: 424
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_buf")]
		public static extern IntPtr RailArrayRailSmallObjectState_buf(IntPtr jarg1);

		// Token: 0x060001A9 RID: 425
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_size")]
		public static extern uint RailArrayRailSmallObjectState_size(IntPtr jarg1);

		// Token: 0x060001AA RID: 426
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_Item")]
		public static extern IntPtr RailArrayRailSmallObjectState_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060001AB RID: 427
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_resize")]
		public static extern void RailArrayRailSmallObjectState_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060001AC RID: 428
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_push_back")]
		public static extern void RailArrayRailSmallObjectState_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001AD RID: 429
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_clear")]
		public static extern void RailArrayRailSmallObjectState_clear(IntPtr jarg1);

		// Token: 0x060001AE RID: 430
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectState_erase")]
		public static extern void RailArrayRailSmallObjectState_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060001AF RID: 431
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailFriendPlayedGameInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayRailFriendPlayedGameInfo__SWIG_0();

		// Token: 0x060001B0 RID: 432
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailFriendPlayedGameInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayRailFriendPlayedGameInfo__SWIG_1(uint jarg1);

		// Token: 0x060001B1 RID: 433
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailFriendPlayedGameInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayRailFriendPlayedGameInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060001B2 RID: 434
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailFriendPlayedGameInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayRailFriendPlayedGameInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x060001B3 RID: 435
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_SetValue")]
		public static extern IntPtr RailArrayRailFriendPlayedGameInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001B4 RID: 436
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailFriendPlayedGameInfo")]
		public static extern void delete_RailArrayRailFriendPlayedGameInfo(IntPtr jarg1);

		// Token: 0x060001B5 RID: 437
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_assign")]
		public static extern void RailArrayRailFriendPlayedGameInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060001B6 RID: 438
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_buf")]
		public static extern IntPtr RailArrayRailFriendPlayedGameInfo_buf(IntPtr jarg1);

		// Token: 0x060001B7 RID: 439
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_size")]
		public static extern uint RailArrayRailFriendPlayedGameInfo_size(IntPtr jarg1);

		// Token: 0x060001B8 RID: 440
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_Item")]
		public static extern IntPtr RailArrayRailFriendPlayedGameInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060001B9 RID: 441
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_resize")]
		public static extern void RailArrayRailFriendPlayedGameInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060001BA RID: 442
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_push_back")]
		public static extern void RailArrayRailFriendPlayedGameInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001BB RID: 443
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_clear")]
		public static extern void RailArrayRailFriendPlayedGameInfo_clear(IntPtr jarg1);

		// Token: 0x060001BC RID: 444
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailFriendPlayedGameInfo_erase")]
		public static extern void RailArrayRailFriendPlayedGameInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060001BD RID: 445
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_0")]
		public static extern IntPtr new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_0();

		// Token: 0x060001BE RID: 446
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_1")]
		public static extern IntPtr new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_1(uint jarg1);

		// Token: 0x060001BF RID: 447
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_2")]
		public static extern IntPtr new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060001C0 RID: 448
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_3")]
		public static extern IntPtr new_RailArrayRailVoiceChannelUserSpeakingState__SWIG_3(IntPtr jarg1);

		// Token: 0x060001C1 RID: 449
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_SetValue")]
		public static extern IntPtr RailArrayRailVoiceChannelUserSpeakingState_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001C2 RID: 450
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailVoiceChannelUserSpeakingState")]
		public static extern void delete_RailArrayRailVoiceChannelUserSpeakingState(IntPtr jarg1);

		// Token: 0x060001C3 RID: 451
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_assign")]
		public static extern void RailArrayRailVoiceChannelUserSpeakingState_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060001C4 RID: 452
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_buf")]
		public static extern IntPtr RailArrayRailVoiceChannelUserSpeakingState_buf(IntPtr jarg1);

		// Token: 0x060001C5 RID: 453
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_size")]
		public static extern uint RailArrayRailVoiceChannelUserSpeakingState_size(IntPtr jarg1);

		// Token: 0x060001C6 RID: 454
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_Item")]
		public static extern IntPtr RailArrayRailVoiceChannelUserSpeakingState_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060001C7 RID: 455
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_resize")]
		public static extern void RailArrayRailVoiceChannelUserSpeakingState_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060001C8 RID: 456
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_push_back")]
		public static extern void RailArrayRailVoiceChannelUserSpeakingState_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001C9 RID: 457
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_clear")]
		public static extern void RailArrayRailVoiceChannelUserSpeakingState_clear(IntPtr jarg1);

		// Token: 0x060001CA RID: 458
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailVoiceChannelUserSpeakingState_erase")]
		public static extern void RailArrayRailVoiceChannelUserSpeakingState_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060001CB RID: 459
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailGameID__SWIG_0")]
		public static extern IntPtr new_RailArrayRailGameID__SWIG_0();

		// Token: 0x060001CC RID: 460
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailGameID__SWIG_1")]
		public static extern IntPtr new_RailArrayRailGameID__SWIG_1(uint jarg1);

		// Token: 0x060001CD RID: 461
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailGameID__SWIG_2")]
		public static extern IntPtr new_RailArrayRailGameID__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060001CE RID: 462
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailGameID__SWIG_3")]
		public static extern IntPtr new_RailArrayRailGameID__SWIG_3(IntPtr jarg1);

		// Token: 0x060001CF RID: 463
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_SetValue")]
		public static extern IntPtr RailArrayRailGameID_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001D0 RID: 464
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailGameID")]
		public static extern void delete_RailArrayRailGameID(IntPtr jarg1);

		// Token: 0x060001D1 RID: 465
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_assign")]
		public static extern void RailArrayRailGameID_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060001D2 RID: 466
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_buf")]
		public static extern IntPtr RailArrayRailGameID_buf(IntPtr jarg1);

		// Token: 0x060001D3 RID: 467
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_size")]
		public static extern uint RailArrayRailGameID_size(IntPtr jarg1);

		// Token: 0x060001D4 RID: 468
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_Item")]
		public static extern IntPtr RailArrayRailGameID_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060001D5 RID: 469
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_resize")]
		public static extern void RailArrayRailGameID_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060001D6 RID: 470
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_push_back")]
		public static extern void RailArrayRailGameID_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001D7 RID: 471
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_clear")]
		public static extern void RailArrayRailGameID_clear(IntPtr jarg1);

		// Token: 0x060001D8 RID: 472
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameID_erase")]
		public static extern void RailArrayRailGameID_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060001D9 RID: 473
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPurchaseProductInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayRailPurchaseProductInfo__SWIG_0();

		// Token: 0x060001DA RID: 474
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPurchaseProductInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayRailPurchaseProductInfo__SWIG_1(uint jarg1);

		// Token: 0x060001DB RID: 475
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPurchaseProductInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayRailPurchaseProductInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060001DC RID: 476
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPurchaseProductInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayRailPurchaseProductInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x060001DD RID: 477
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_SetValue")]
		public static extern IntPtr RailArrayRailPurchaseProductInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001DE RID: 478
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailPurchaseProductInfo")]
		public static extern void delete_RailArrayRailPurchaseProductInfo(IntPtr jarg1);

		// Token: 0x060001DF RID: 479
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_assign")]
		public static extern void RailArrayRailPurchaseProductInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060001E0 RID: 480
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_buf")]
		public static extern IntPtr RailArrayRailPurchaseProductInfo_buf(IntPtr jarg1);

		// Token: 0x060001E1 RID: 481
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_size")]
		public static extern uint RailArrayRailPurchaseProductInfo_size(IntPtr jarg1);

		// Token: 0x060001E2 RID: 482
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_Item")]
		public static extern IntPtr RailArrayRailPurchaseProductInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060001E3 RID: 483
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_resize")]
		public static extern void RailArrayRailPurchaseProductInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060001E4 RID: 484
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_push_back")]
		public static extern void RailArrayRailPurchaseProductInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001E5 RID: 485
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_clear")]
		public static extern void RailArrayRailPurchaseProductInfo_clear(IntPtr jarg1);

		// Token: 0x060001E6 RID: 486
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPurchaseProductInfo_erase")]
		public static extern void RailArrayRailPurchaseProductInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060001E7 RID: 487
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayRoomInfo__SWIG_0();

		// Token: 0x060001E8 RID: 488
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayRoomInfo__SWIG_1(uint jarg1);

		// Token: 0x060001E9 RID: 489
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayRoomInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060001EA RID: 490
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayRoomInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x060001EB RID: 491
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_SetValue")]
		public static extern IntPtr RailArrayRoomInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001EC RID: 492
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRoomInfo")]
		public static extern void delete_RailArrayRoomInfo(IntPtr jarg1);

		// Token: 0x060001ED RID: 493
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_assign")]
		public static extern void RailArrayRoomInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060001EE RID: 494
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_buf")]
		public static extern IntPtr RailArrayRoomInfo_buf(IntPtr jarg1);

		// Token: 0x060001EF RID: 495
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_size")]
		public static extern uint RailArrayRoomInfo_size(IntPtr jarg1);

		// Token: 0x060001F0 RID: 496
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_Item")]
		public static extern IntPtr RailArrayRoomInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060001F1 RID: 497
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_resize")]
		public static extern void RailArrayRoomInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060001F2 RID: 498
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_push_back")]
		public static extern void RailArrayRoomInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001F3 RID: 499
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_clear")]
		public static extern void RailArrayRoomInfo_clear(IntPtr jarg1);

		// Token: 0x060001F4 RID: 500
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfo_erase")]
		public static extern void RailArrayRoomInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060001F5 RID: 501
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListSorter__SWIG_0")]
		public static extern IntPtr new_RailArrayGameServerListSorter__SWIG_0();

		// Token: 0x060001F6 RID: 502
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListSorter__SWIG_1")]
		public static extern IntPtr new_RailArrayGameServerListSorter__SWIG_1(uint jarg1);

		// Token: 0x060001F7 RID: 503
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListSorter__SWIG_2")]
		public static extern IntPtr new_RailArrayGameServerListSorter__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060001F8 RID: 504
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerListSorter__SWIG_3")]
		public static extern IntPtr new_RailArrayGameServerListSorter__SWIG_3(IntPtr jarg1);

		// Token: 0x060001F9 RID: 505
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_SetValue")]
		public static extern IntPtr RailArrayGameServerListSorter_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060001FA RID: 506
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerListSorter")]
		public static extern void delete_RailArrayGameServerListSorter(IntPtr jarg1);

		// Token: 0x060001FB RID: 507
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_assign")]
		public static extern void RailArrayGameServerListSorter_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060001FC RID: 508
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_buf")]
		public static extern IntPtr RailArrayGameServerListSorter_buf(IntPtr jarg1);

		// Token: 0x060001FD RID: 509
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_size")]
		public static extern uint RailArrayGameServerListSorter_size(IntPtr jarg1);

		// Token: 0x060001FE RID: 510
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_Item")]
		public static extern IntPtr RailArrayGameServerListSorter_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060001FF RID: 511
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_resize")]
		public static extern void RailArrayGameServerListSorter_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000200 RID: 512
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_push_back")]
		public static extern void RailArrayGameServerListSorter_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000201 RID: 513
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_clear")]
		public static extern void RailArrayGameServerListSorter_clear(IntPtr jarg1);

		// Token: 0x06000202 RID: 514
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerListSorter_erase")]
		public static extern void RailArrayGameServerListSorter_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000203 RID: 515
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcID__SWIG_0")]
		public static extern IntPtr new_RailArrayRailDlcID__SWIG_0();

		// Token: 0x06000204 RID: 516
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcID__SWIG_1")]
		public static extern IntPtr new_RailArrayRailDlcID__SWIG_1(uint jarg1);

		// Token: 0x06000205 RID: 517
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcID__SWIG_2")]
		public static extern IntPtr new_RailArrayRailDlcID__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000206 RID: 518
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcID__SWIG_3")]
		public static extern IntPtr new_RailArrayRailDlcID__SWIG_3(IntPtr jarg1);

		// Token: 0x06000207 RID: 519
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_SetValue")]
		public static extern IntPtr RailArrayRailDlcID_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000208 RID: 520
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailDlcID")]
		public static extern void delete_RailArrayRailDlcID(IntPtr jarg1);

		// Token: 0x06000209 RID: 521
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_assign")]
		public static extern void RailArrayRailDlcID_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600020A RID: 522
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_buf")]
		public static extern IntPtr RailArrayRailDlcID_buf(IntPtr jarg1);

		// Token: 0x0600020B RID: 523
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_size")]
		public static extern uint RailArrayRailDlcID_size(IntPtr jarg1);

		// Token: 0x0600020C RID: 524
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_Item")]
		public static extern IntPtr RailArrayRailDlcID_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600020D RID: 525
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_resize")]
		public static extern void RailArrayRailDlcID_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600020E RID: 526
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_push_back")]
		public static extern void RailArrayRailDlcID_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600020F RID: 527
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_clear")]
		public static extern void RailArrayRailDlcID_clear(IntPtr jarg1);

		// Token: 0x06000210 RID: 528
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcID_erase")]
		public static extern void RailArrayRailDlcID_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000211 RID: 529
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailID__SWIG_0")]
		public static extern IntPtr new_RailArrayRailID__SWIG_0();

		// Token: 0x06000212 RID: 530
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailID__SWIG_1")]
		public static extern IntPtr new_RailArrayRailID__SWIG_1(uint jarg1);

		// Token: 0x06000213 RID: 531
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailID__SWIG_2")]
		public static extern IntPtr new_RailArrayRailID__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000214 RID: 532
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailID__SWIG_3")]
		public static extern IntPtr new_RailArrayRailID__SWIG_3(IntPtr jarg1);

		// Token: 0x06000215 RID: 533
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_SetValue")]
		public static extern IntPtr RailArrayRailID_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000216 RID: 534
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailID")]
		public static extern void delete_RailArrayRailID(IntPtr jarg1);

		// Token: 0x06000217 RID: 535
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_assign")]
		public static extern void RailArrayRailID_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000218 RID: 536
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_buf")]
		public static extern IntPtr RailArrayRailID_buf(IntPtr jarg1);

		// Token: 0x06000219 RID: 537
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_size")]
		public static extern uint RailArrayRailID_size(IntPtr jarg1);

		// Token: 0x0600021A RID: 538
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_Item")]
		public static extern IntPtr RailArrayRailID_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600021B RID: 539
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_resize")]
		public static extern void RailArrayRailID_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600021C RID: 540
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_push_back")]
		public static extern void RailArrayRailID_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600021D RID: 541
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_clear")]
		public static extern void RailArrayRailID_clear(IntPtr jarg1);

		// Token: 0x0600021E RID: 542
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailID_erase")]
		public static extern void RailArrayRailID_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x0600021F RID: 543
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailUserPlayedWith__SWIG_0")]
		public static extern IntPtr new_RailArrayRailUserPlayedWith__SWIG_0();

		// Token: 0x06000220 RID: 544
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailUserPlayedWith__SWIG_1")]
		public static extern IntPtr new_RailArrayRailUserPlayedWith__SWIG_1(uint jarg1);

		// Token: 0x06000221 RID: 545
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailUserPlayedWith__SWIG_2")]
		public static extern IntPtr new_RailArrayRailUserPlayedWith__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000222 RID: 546
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailUserPlayedWith__SWIG_3")]
		public static extern IntPtr new_RailArrayRailUserPlayedWith__SWIG_3(IntPtr jarg1);

		// Token: 0x06000223 RID: 547
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_SetValue")]
		public static extern IntPtr RailArrayRailUserPlayedWith_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000224 RID: 548
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailUserPlayedWith")]
		public static extern void delete_RailArrayRailUserPlayedWith(IntPtr jarg1);

		// Token: 0x06000225 RID: 549
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_assign")]
		public static extern void RailArrayRailUserPlayedWith_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000226 RID: 550
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_buf")]
		public static extern IntPtr RailArrayRailUserPlayedWith_buf(IntPtr jarg1);

		// Token: 0x06000227 RID: 551
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_size")]
		public static extern uint RailArrayRailUserPlayedWith_size(IntPtr jarg1);

		// Token: 0x06000228 RID: 552
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_Item")]
		public static extern IntPtr RailArrayRailUserPlayedWith_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000229 RID: 553
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_resize")]
		public static extern void RailArrayRailUserPlayedWith_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600022A RID: 554
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_push_back")]
		public static extern void RailArrayRailUserPlayedWith_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600022B RID: 555
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_clear")]
		public static extern void RailArrayRailUserPlayedWith_clear(IntPtr jarg1);

		// Token: 0x0600022C RID: 556
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailUserPlayedWith_erase")]
		public static extern void RailArrayRailUserPlayedWith_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x0600022D RID: 557
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailGameDefineGamePlayingState__SWIG_0")]
		public static extern IntPtr new_RailArrayRailGameDefineGamePlayingState__SWIG_0();

		// Token: 0x0600022E RID: 558
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailGameDefineGamePlayingState__SWIG_1")]
		public static extern IntPtr new_RailArrayRailGameDefineGamePlayingState__SWIG_1(uint jarg1);

		// Token: 0x0600022F RID: 559
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailGameDefineGamePlayingState__SWIG_2")]
		public static extern IntPtr new_RailArrayRailGameDefineGamePlayingState__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000230 RID: 560
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailGameDefineGamePlayingState__SWIG_3")]
		public static extern IntPtr new_RailArrayRailGameDefineGamePlayingState__SWIG_3(IntPtr jarg1);

		// Token: 0x06000231 RID: 561
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_SetValue")]
		public static extern IntPtr RailArrayRailGameDefineGamePlayingState_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000232 RID: 562
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailGameDefineGamePlayingState")]
		public static extern void delete_RailArrayRailGameDefineGamePlayingState(IntPtr jarg1);

		// Token: 0x06000233 RID: 563
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_assign")]
		public static extern void RailArrayRailGameDefineGamePlayingState_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000234 RID: 564
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_buf")]
		public static extern IntPtr RailArrayRailGameDefineGamePlayingState_buf(IntPtr jarg1);

		// Token: 0x06000235 RID: 565
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_size")]
		public static extern uint RailArrayRailGameDefineGamePlayingState_size(IntPtr jarg1);

		// Token: 0x06000236 RID: 566
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_Item")]
		public static extern IntPtr RailArrayRailGameDefineGamePlayingState_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000237 RID: 567
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_resize")]
		public static extern void RailArrayRailGameDefineGamePlayingState_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000238 RID: 568
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_push_back")]
		public static extern void RailArrayRailGameDefineGamePlayingState_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000239 RID: 569
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_clear")]
		public static extern void RailArrayRailGameDefineGamePlayingState_clear(IntPtr jarg1);

		// Token: 0x0600023A RID: 570
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailGameDefineGamePlayingState_erase")]
		public static extern void RailArrayRailGameDefineGamePlayingState_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x0600023B RID: 571
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayGameServerInfo__SWIG_0();

		// Token: 0x0600023C RID: 572
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayGameServerInfo__SWIG_1(uint jarg1);

		// Token: 0x0600023D RID: 573
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayGameServerInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600023E RID: 574
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayGameServerInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x0600023F RID: 575
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_SetValue")]
		public static extern IntPtr RailArrayGameServerInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000240 RID: 576
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerInfo")]
		public static extern void delete_RailArrayGameServerInfo(IntPtr jarg1);

		// Token: 0x06000241 RID: 577
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_assign")]
		public static extern void RailArrayGameServerInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000242 RID: 578
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_buf")]
		public static extern IntPtr RailArrayGameServerInfo_buf(IntPtr jarg1);

		// Token: 0x06000243 RID: 579
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_size")]
		public static extern uint RailArrayGameServerInfo_size(IntPtr jarg1);

		// Token: 0x06000244 RID: 580
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_Item")]
		public static extern IntPtr RailArrayGameServerInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000245 RID: 581
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_resize")]
		public static extern void RailArrayGameServerInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000246 RID: 582
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_push_back")]
		public static extern void RailArrayGameServerInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000247 RID: 583
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_clear")]
		public static extern void RailArrayGameServerInfo_clear(IntPtr jarg1);

		// Token: 0x06000248 RID: 584
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerInfo_erase")]
		public static extern void RailArrayGameServerInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000249 RID: 585
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPlayedWithFriendsTimeItem__SWIG_0")]
		public static extern IntPtr new_RailArrayRailPlayedWithFriendsTimeItem__SWIG_0();

		// Token: 0x0600024A RID: 586
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPlayedWithFriendsTimeItem__SWIG_1")]
		public static extern IntPtr new_RailArrayRailPlayedWithFriendsTimeItem__SWIG_1(uint jarg1);

		// Token: 0x0600024B RID: 587
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPlayedWithFriendsTimeItem__SWIG_2")]
		public static extern IntPtr new_RailArrayRailPlayedWithFriendsTimeItem__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600024C RID: 588
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailPlayedWithFriendsTimeItem__SWIG_3")]
		public static extern IntPtr new_RailArrayRailPlayedWithFriendsTimeItem__SWIG_3(IntPtr jarg1);

		// Token: 0x0600024D RID: 589
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_SetValue")]
		public static extern IntPtr RailArrayRailPlayedWithFriendsTimeItem_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600024E RID: 590
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailPlayedWithFriendsTimeItem")]
		public static extern void delete_RailArrayRailPlayedWithFriendsTimeItem(IntPtr jarg1);

		// Token: 0x0600024F RID: 591
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_assign")]
		public static extern void RailArrayRailPlayedWithFriendsTimeItem_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000250 RID: 592
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_buf")]
		public static extern IntPtr RailArrayRailPlayedWithFriendsTimeItem_buf(IntPtr jarg1);

		// Token: 0x06000251 RID: 593
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_size")]
		public static extern uint RailArrayRailPlayedWithFriendsTimeItem_size(IntPtr jarg1);

		// Token: 0x06000252 RID: 594
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_Item")]
		public static extern IntPtr RailArrayRailPlayedWithFriendsTimeItem_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000253 RID: 595
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_resize")]
		public static extern void RailArrayRailPlayedWithFriendsTimeItem_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000254 RID: 596
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_push_back")]
		public static extern void RailArrayRailPlayedWithFriendsTimeItem_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000255 RID: 597
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_clear")]
		public static extern void RailArrayRailPlayedWithFriendsTimeItem_clear(IntPtr jarg1);

		// Token: 0x06000256 RID: 598
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailPlayedWithFriendsTimeItem_erase")]
		public static extern void RailArrayRailPlayedWithFriendsTimeItem_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000257 RID: 599
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilterKey__SWIG_0")]
		public static extern IntPtr new_RailArrayRoomInfoListFilterKey__SWIG_0();

		// Token: 0x06000258 RID: 600
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilterKey__SWIG_1")]
		public static extern IntPtr new_RailArrayRoomInfoListFilterKey__SWIG_1(uint jarg1);

		// Token: 0x06000259 RID: 601
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilterKey__SWIG_2")]
		public static extern IntPtr new_RailArrayRoomInfoListFilterKey__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x0600025A RID: 602
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRoomInfoListFilterKey__SWIG_3")]
		public static extern IntPtr new_RailArrayRoomInfoListFilterKey__SWIG_3(IntPtr jarg1);

		// Token: 0x0600025B RID: 603
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_SetValue")]
		public static extern IntPtr RailArrayRoomInfoListFilterKey_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600025C RID: 604
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRoomInfoListFilterKey")]
		public static extern void delete_RailArrayRoomInfoListFilterKey(IntPtr jarg1);

		// Token: 0x0600025D RID: 605
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_assign")]
		public static extern void RailArrayRoomInfoListFilterKey_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600025E RID: 606
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_buf")]
		public static extern IntPtr RailArrayRoomInfoListFilterKey_buf(IntPtr jarg1);

		// Token: 0x0600025F RID: 607
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_size")]
		public static extern uint RailArrayRoomInfoListFilterKey_size(IntPtr jarg1);

		// Token: 0x06000260 RID: 608
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_Item")]
		public static extern IntPtr RailArrayRoomInfoListFilterKey_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000261 RID: 609
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_resize")]
		public static extern void RailArrayRoomInfoListFilterKey_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000262 RID: 610
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_push_back")]
		public static extern void RailArrayRoomInfoListFilterKey_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000263 RID: 611
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_clear")]
		public static extern void RailArrayRoomInfoListFilterKey_clear(IntPtr jarg1);

		// Token: 0x06000264 RID: 612
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRoomInfoListFilterKey_erase")]
		public static extern void RailArrayRoomInfoListFilterKey_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000265 RID: 613
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayMemberInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayMemberInfo__SWIG_0();

		// Token: 0x06000266 RID: 614
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayMemberInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayMemberInfo__SWIG_1(uint jarg1);

		// Token: 0x06000267 RID: 615
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayMemberInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayMemberInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000268 RID: 616
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayMemberInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayMemberInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x06000269 RID: 617
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_SetValue")]
		public static extern IntPtr RailArrayMemberInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600026A RID: 618
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayMemberInfo")]
		public static extern void delete_RailArrayMemberInfo(IntPtr jarg1);

		// Token: 0x0600026B RID: 619
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_assign")]
		public static extern void RailArrayMemberInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600026C RID: 620
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_buf")]
		public static extern IntPtr RailArrayMemberInfo_buf(IntPtr jarg1);

		// Token: 0x0600026D RID: 621
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_size")]
		public static extern uint RailArrayMemberInfo_size(IntPtr jarg1);

		// Token: 0x0600026E RID: 622
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_Item")]
		public static extern IntPtr RailArrayMemberInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600026F RID: 623
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_resize")]
		public static extern void RailArrayMemberInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x06000270 RID: 624
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_push_back")]
		public static extern void RailArrayMemberInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000271 RID: 625
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_clear")]
		public static extern void RailArrayMemberInfo_clear(IntPtr jarg1);

		// Token: 0x06000272 RID: 626
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayMemberInfo_erase")]
		public static extern void RailArrayMemberInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000273 RID: 627
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSmallObjectDownloadInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayRailSmallObjectDownloadInfo__SWIG_0();

		// Token: 0x06000274 RID: 628
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSmallObjectDownloadInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayRailSmallObjectDownloadInfo__SWIG_1(uint jarg1);

		// Token: 0x06000275 RID: 629
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSmallObjectDownloadInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayRailSmallObjectDownloadInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000276 RID: 630
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSmallObjectDownloadInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayRailSmallObjectDownloadInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x06000277 RID: 631
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_SetValue")]
		public static extern IntPtr RailArrayRailSmallObjectDownloadInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000278 RID: 632
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailSmallObjectDownloadInfo")]
		public static extern void delete_RailArrayRailSmallObjectDownloadInfo(IntPtr jarg1);

		// Token: 0x06000279 RID: 633
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_assign")]
		public static extern void RailArrayRailSmallObjectDownloadInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x0600027A RID: 634
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_buf")]
		public static extern IntPtr RailArrayRailSmallObjectDownloadInfo_buf(IntPtr jarg1);

		// Token: 0x0600027B RID: 635
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_size")]
		public static extern uint RailArrayRailSmallObjectDownloadInfo_size(IntPtr jarg1);

		// Token: 0x0600027C RID: 636
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_Item")]
		public static extern IntPtr RailArrayRailSmallObjectDownloadInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600027D RID: 637
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_resize")]
		public static extern void RailArrayRailSmallObjectDownloadInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600027E RID: 638
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_push_back")]
		public static extern void RailArrayRailSmallObjectDownloadInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600027F RID: 639
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_clear")]
		public static extern void RailArrayRailSmallObjectDownloadInfo_clear(IntPtr jarg1);

		// Token: 0x06000280 RID: 640
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSmallObjectDownloadInfo_erase")]
		public static extern void RailArrayRailSmallObjectDownloadInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x06000281 RID: 641
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcOwned__SWIG_0")]
		public static extern IntPtr new_RailArrayRailDlcOwned__SWIG_0();

		// Token: 0x06000282 RID: 642
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcOwned__SWIG_1")]
		public static extern IntPtr new_RailArrayRailDlcOwned__SWIG_1(uint jarg1);

		// Token: 0x06000283 RID: 643
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcOwned__SWIG_2")]
		public static extern IntPtr new_RailArrayRailDlcOwned__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000284 RID: 644
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailDlcOwned__SWIG_3")]
		public static extern IntPtr new_RailArrayRailDlcOwned__SWIG_3(IntPtr jarg1);

		// Token: 0x06000285 RID: 645
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_SetValue")]
		public static extern IntPtr RailArrayRailDlcOwned_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000286 RID: 646
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailDlcOwned")]
		public static extern void delete_RailArrayRailDlcOwned(IntPtr jarg1);

		// Token: 0x06000287 RID: 647
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_assign")]
		public static extern void RailArrayRailDlcOwned_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000288 RID: 648
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_buf")]
		public static extern IntPtr RailArrayRailDlcOwned_buf(IntPtr jarg1);

		// Token: 0x06000289 RID: 649
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_size")]
		public static extern uint RailArrayRailDlcOwned_size(IntPtr jarg1);

		// Token: 0x0600028A RID: 650
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_Item")]
		public static extern IntPtr RailArrayRailDlcOwned_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x0600028B RID: 651
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_resize")]
		public static extern void RailArrayRailDlcOwned_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600028C RID: 652
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_push_back")]
		public static extern void RailArrayRailDlcOwned_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600028D RID: 653
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_clear")]
		public static extern void RailArrayRailDlcOwned_clear(IntPtr jarg1);

		// Token: 0x0600028E RID: 654
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailDlcOwned_erase")]
		public static extern void RailArrayRailDlcOwned_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x0600028F RID: 655
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkDescriptor__SWIG_0")]
		public static extern IntPtr new_RailArrayRailSpaceWorkDescriptor__SWIG_0();

		// Token: 0x06000290 RID: 656
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkDescriptor__SWIG_1")]
		public static extern IntPtr new_RailArrayRailSpaceWorkDescriptor__SWIG_1(uint jarg1);

		// Token: 0x06000291 RID: 657
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkDescriptor__SWIG_2")]
		public static extern IntPtr new_RailArrayRailSpaceWorkDescriptor__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x06000292 RID: 658
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayRailSpaceWorkDescriptor__SWIG_3")]
		public static extern IntPtr new_RailArrayRailSpaceWorkDescriptor__SWIG_3(IntPtr jarg1);

		// Token: 0x06000293 RID: 659
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_SetValue")]
		public static extern IntPtr RailArrayRailSpaceWorkDescriptor_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000294 RID: 660
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayRailSpaceWorkDescriptor")]
		public static extern void delete_RailArrayRailSpaceWorkDescriptor(IntPtr jarg1);

		// Token: 0x06000295 RID: 661
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_assign")]
		public static extern void RailArrayRailSpaceWorkDescriptor_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000296 RID: 662
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_buf")]
		public static extern IntPtr RailArrayRailSpaceWorkDescriptor_buf(IntPtr jarg1);

		// Token: 0x06000297 RID: 663
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_size")]
		public static extern uint RailArrayRailSpaceWorkDescriptor_size(IntPtr jarg1);

		// Token: 0x06000298 RID: 664
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_Item")]
		public static extern IntPtr RailArrayRailSpaceWorkDescriptor_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x06000299 RID: 665
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_resize")]
		public static extern void RailArrayRailSpaceWorkDescriptor_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x0600029A RID: 666
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_push_back")]
		public static extern void RailArrayRailSpaceWorkDescriptor_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600029B RID: 667
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_clear")]
		public static extern void RailArrayRailSpaceWorkDescriptor_clear(IntPtr jarg1);

		// Token: 0x0600029C RID: 668
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayRailSpaceWorkDescriptor_erase")]
		public static extern void RailArrayRailSpaceWorkDescriptor_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x0600029D RID: 669
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerPlayerInfo__SWIG_0")]
		public static extern IntPtr new_RailArrayGameServerPlayerInfo__SWIG_0();

		// Token: 0x0600029E RID: 670
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerPlayerInfo__SWIG_1")]
		public static extern IntPtr new_RailArrayGameServerPlayerInfo__SWIG_1(uint jarg1);

		// Token: 0x0600029F RID: 671
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerPlayerInfo__SWIG_2")]
		public static extern IntPtr new_RailArrayGameServerPlayerInfo__SWIG_2(IntPtr jarg1, uint jarg2);

		// Token: 0x060002A0 RID: 672
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailArrayGameServerPlayerInfo__SWIG_3")]
		public static extern IntPtr new_RailArrayGameServerPlayerInfo__SWIG_3(IntPtr jarg1);

		// Token: 0x060002A1 RID: 673
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_SetValue")]
		public static extern IntPtr RailArrayGameServerPlayerInfo_SetValue(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060002A2 RID: 674
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailArrayGameServerPlayerInfo")]
		public static extern void delete_RailArrayGameServerPlayerInfo(IntPtr jarg1);

		// Token: 0x060002A3 RID: 675
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_assign")]
		public static extern void RailArrayGameServerPlayerInfo_assign(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x060002A4 RID: 676
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_buf")]
		public static extern IntPtr RailArrayGameServerPlayerInfo_buf(IntPtr jarg1);

		// Token: 0x060002A5 RID: 677
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_size")]
		public static extern uint RailArrayGameServerPlayerInfo_size(IntPtr jarg1);

		// Token: 0x060002A6 RID: 678
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_Item")]
		public static extern IntPtr RailArrayGameServerPlayerInfo_Item(IntPtr jarg1, uint jarg2);

		// Token: 0x060002A7 RID: 679
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_resize")]
		public static extern void RailArrayGameServerPlayerInfo_resize(IntPtr jarg1, uint jarg2);

		// Token: 0x060002A8 RID: 680
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_push_back")]
		public static extern void RailArrayGameServerPlayerInfo_push_back(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060002A9 RID: 681
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_clear")]
		public static extern void RailArrayGameServerPlayerInfo_clear(IntPtr jarg1);

		// Token: 0x060002AA RID: 682
		[DllImport("rail_api", EntryPoint = "CSharp_RailArrayGameServerPlayerInfo_erase")]
		public static extern void RailArrayGameServerPlayerInfo_erase(IntPtr jarg1, uint jarg2);

		// Token: 0x060002AB RID: 683
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsRequestAllAssetsFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsRequestAllAssetsFinished__SWIG_0();

		// Token: 0x060002AC RID: 684
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsRequestAllAssetsFinished")]
		public static extern void delete_RailEventkRailEventAssetsRequestAllAssetsFinished(IntPtr jarg1);

		// Token: 0x060002AD RID: 685
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsRequestAllAssetsFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsRequestAllAssetsFinished_kInternalRailEventEventId_get();

		// Token: 0x060002AE RID: 686
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsRequestAllAssetsFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsRequestAllAssetsFinished_get_event_id(IntPtr jarg1);

		// Token: 0x060002AF RID: 687
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsRequestAllAssetsFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsRequestAllAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x060002B0 RID: 688
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsMergeToFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsMergeToFinished__SWIG_0();

		// Token: 0x060002B1 RID: 689
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsMergeToFinished")]
		public static extern void delete_RailEventkRailEventAssetsMergeToFinished(IntPtr jarg1);

		// Token: 0x060002B2 RID: 690
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeToFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsMergeToFinished_kInternalRailEventEventId_get();

		// Token: 0x060002B3 RID: 691
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeToFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsMergeToFinished_get_event_id(IntPtr jarg1);

		// Token: 0x060002B4 RID: 692
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsMergeToFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsMergeToFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x060002B5 RID: 693
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSubscribeResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceSubscribeResult__SWIG_0();

		// Token: 0x060002B6 RID: 694
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceSubscribeResult")]
		public static extern void delete_RailEventkRailEventUserSpaceSubscribeResult(IntPtr jarg1);

		// Token: 0x060002B7 RID: 695
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSubscribeResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceSubscribeResult_kInternalRailEventEventId_get();

		// Token: 0x060002B8 RID: 696
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSubscribeResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceSubscribeResult_get_event_id(IntPtr jarg1);

		// Token: 0x060002B9 RID: 697
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSubscribeResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceSubscribeResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060002BA RID: 698
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMemberChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyMemberChanged__SWIG_0();

		// Token: 0x060002BB RID: 699
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyMemberChanged")]
		public static extern void delete_RailEventkRailEventRoomNotifyMemberChanged(IntPtr jarg1);

		// Token: 0x060002BC RID: 700
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomNotifyMemberChanged_kInternalRailEventEventId_get();

		// Token: 0x060002BD RID: 701
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberChanged_get_event_id")]
		public static extern int RailEventkRailEventRoomNotifyMemberChanged_get_event_id(IntPtr jarg1);

		// Token: 0x060002BE RID: 702
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMemberChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyMemberChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x060002BF RID: 703
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchasePurchaseProductsResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchasePurchaseProductsResult__SWIG_0();

		// Token: 0x060002C0 RID: 704
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchasePurchaseProductsResult")]
		public static extern void delete_RailEventkRailEventInGamePurchasePurchaseProductsResult(IntPtr jarg1);

		// Token: 0x060002C1 RID: 705
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventInGamePurchasePurchaseProductsResult_kInternalRailEventEventId_get();

		// Token: 0x060002C2 RID: 706
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsResult_get_event_id")]
		public static extern int RailEventkRailEventInGamePurchasePurchaseProductsResult_get_event_id(IntPtr jarg1);

		// Token: 0x060002C3 RID: 707
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchasePurchaseProductsResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchasePurchaseProductsResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060002C4 RID: 708
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerListResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerListResult__SWIG_0();

		// Token: 0x060002C5 RID: 709
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerListResult")]
		public static extern void delete_RailEventkRailEventGameServerListResult(IntPtr jarg1);

		// Token: 0x060002C6 RID: 710
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerListResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerListResult_kInternalRailEventEventId_get();

		// Token: 0x060002C7 RID: 711
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerListResult_get_event_id")]
		public static extern int RailEventkRailEventGameServerListResult_get_event_id(IntPtr jarg1);

		// Token: 0x060002C8 RID: 712
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerListResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060002C9 RID: 713
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomSetRoomMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomSetRoomMetadataResult__SWIG_0();

		// Token: 0x060002CA RID: 714
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomSetRoomMetadataResult")]
		public static extern void delete_RailEventkRailEventRoomSetRoomMetadataResult(IntPtr jarg1);

		// Token: 0x060002CB RID: 715
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetRoomMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomSetRoomMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x060002CC RID: 716
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetRoomMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventRoomSetRoomMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x060002CD RID: 717
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomSetRoomMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomSetRoomMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060002CE RID: 718
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserCloseResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserCloseResult__SWIG_0();

		// Token: 0x060002CF RID: 719
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserCloseResult")]
		public static extern void delete_RailEventkRailEventBrowserCloseResult(IntPtr jarg1);

		// Token: 0x060002D0 RID: 720
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCloseResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserCloseResult_kInternalRailEventEventId_get();

		// Token: 0x060002D1 RID: 721
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCloseResult_get_event_id")]
		public static extern int RailEventkRailEventBrowserCloseResult_get_event_id(IntPtr jarg1);

		// Token: 0x060002D2 RID: 722
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserCloseResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserCloseResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060002D3 RID: 723
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetworkCreateSessionRequest__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventNetworkCreateSessionRequest__SWIG_0();

		// Token: 0x060002D4 RID: 724
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetworkCreateSessionRequest")]
		public static extern void delete_RailEventkRailEventNetworkCreateSessionRequest(IntPtr jarg1);

		// Token: 0x060002D5 RID: 725
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionRequest_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventNetworkCreateSessionRequest_kInternalRailEventEventId_get();

		// Token: 0x060002D6 RID: 726
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionRequest_get_event_id")]
		public static extern int RailEventkRailEventNetworkCreateSessionRequest_get_event_id(IntPtr jarg1);

		// Token: 0x060002D7 RID: 727
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetworkCreateSessionRequest__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventNetworkCreateSessionRequest__SWIG_1(IntPtr jarg1);

		// Token: 0x060002D8 RID: 728
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsExchangeAssetsToFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsExchangeAssetsToFinished__SWIG_0();

		// Token: 0x060002D9 RID: 729
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsExchangeAssetsToFinished")]
		public static extern void delete_RailEventkRailEventAssetsExchangeAssetsToFinished(IntPtr jarg1);

		// Token: 0x060002DA RID: 730
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsToFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsExchangeAssetsToFinished_kInternalRailEventEventId_get();

		// Token: 0x060002DB RID: 731
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsToFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsExchangeAssetsToFinished_get_event_id(IntPtr jarg1);

		// Token: 0x060002DC RID: 732
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsExchangeAssetsToFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsExchangeAssetsToFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x060002DD RID: 733
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByUser__SWIG_0")]
		public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByUser__SWIG_0();

		// Token: 0x060002DE RID: 734
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailPlatformNotifyEventJoinGameByUser")]
		public static extern void delete_RailEventkRailPlatformNotifyEventJoinGameByUser(IntPtr jarg1);

		// Token: 0x060002DF RID: 735
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByUser_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailPlatformNotifyEventJoinGameByUser_kInternalRailEventEventId_get();

		// Token: 0x060002E0 RID: 736
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByUser_get_event_id")]
		public static extern int RailEventkRailPlatformNotifyEventJoinGameByUser_get_event_id(IntPtr jarg1);

		// Token: 0x060002E1 RID: 737
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByUser__SWIG_1")]
		public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByUser__SWIG_1(IntPtr jarg1);

		// Token: 0x060002E2 RID: 738
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallProgress__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcInstallProgress__SWIG_0();

		// Token: 0x060002E3 RID: 739
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcInstallProgress")]
		public static extern void delete_RailEventkRailEventDlcInstallProgress(IntPtr jarg1);

		// Token: 0x060002E4 RID: 740
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallProgress_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcInstallProgress_kInternalRailEventEventId_get();

		// Token: 0x060002E5 RID: 741
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallProgress_get_event_id")]
		public static extern int RailEventkRailEventDlcInstallProgress_get_event_id(IntPtr jarg1);

		// Token: 0x060002E6 RID: 742
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallProgress__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcInstallProgress__SWIG_1(IntPtr jarg1);

		// Token: 0x060002E7 RID: 743
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersNotifyInviter__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersNotifyInviter__SWIG_0();

		// Token: 0x060002E8 RID: 744
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersNotifyInviter")]
		public static extern void delete_RailEventkRailEventUsersNotifyInviter(IntPtr jarg1);

		// Token: 0x060002E9 RID: 745
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersNotifyInviter_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersNotifyInviter_kInternalRailEventEventId_get();

		// Token: 0x060002EA RID: 746
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersNotifyInviter_get_event_id")]
		public static extern int RailEventkRailEventUsersNotifyInviter_get_event_id(IntPtr jarg1);

		// Token: 0x060002EB RID: 747
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersNotifyInviter__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersNotifyInviter__SWIG_1(IntPtr jarg1);

		// Token: 0x060002EC RID: 748
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult__SWIG_0();

		// Token: 0x060002ED RID: 749
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult")]
		public static extern void delete_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult(IntPtr jarg1);

		// Token: 0x060002EE RID: 750
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsQueryPlayedWithFriendsListResult_kInternalRailEventEventId_get();

		// Token: 0x060002EF RID: 751
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsQueryPlayedWithFriendsListResult_get_event_id(IntPtr jarg1);

		// Token: 0x060002F0 RID: 752
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060002F1 RID: 753
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardAsyncCreated__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardAsyncCreated__SWIG_0();

		// Token: 0x060002F2 RID: 754
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardAsyncCreated")]
		public static extern void delete_RailEventkRailEventLeaderboardAsyncCreated(IntPtr jarg1);

		// Token: 0x060002F3 RID: 755
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAsyncCreated_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventLeaderboardAsyncCreated_kInternalRailEventEventId_get();

		// Token: 0x060002F4 RID: 756
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAsyncCreated_get_event_id")]
		public static extern int RailEventkRailEventLeaderboardAsyncCreated_get_event_id(IntPtr jarg1);

		// Token: 0x060002F5 RID: 757
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardAsyncCreated__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardAsyncCreated__SWIG_1(IntPtr jarg1);

		// Token: 0x060002F6 RID: 758
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardAttachSpaceWork__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardAttachSpaceWork__SWIG_0();

		// Token: 0x060002F7 RID: 759
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardAttachSpaceWork")]
		public static extern void delete_RailEventkRailEventLeaderboardAttachSpaceWork(IntPtr jarg1);

		// Token: 0x060002F8 RID: 760
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAttachSpaceWork_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventLeaderboardAttachSpaceWork_kInternalRailEventEventId_get();

		// Token: 0x060002F9 RID: 761
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAttachSpaceWork_get_event_id")]
		public static extern int RailEventkRailEventLeaderboardAttachSpaceWork_get_event_id(IntPtr jarg1);

		// Token: 0x060002FA RID: 762
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardAttachSpaceWork__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardAttachSpaceWork__SWIG_1(IntPtr jarg1);

		// Token: 0x060002FB RID: 763
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetUserRoomListResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomGetUserRoomListResult__SWIG_0();

		// Token: 0x060002FC RID: 764
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGetUserRoomListResult")]
		public static extern void delete_RailEventkRailEventRoomGetUserRoomListResult(IntPtr jarg1);

		// Token: 0x060002FD RID: 765
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetUserRoomListResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomGetUserRoomListResult_kInternalRailEventEventId_get();

		// Token: 0x060002FE RID: 766
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetUserRoomListResult_get_event_id")]
		public static extern int RailEventkRailEventRoomGetUserRoomListResult_get_event_id(IntPtr jarg1);

		// Token: 0x060002FF RID: 767
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetUserRoomListResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomGetUserRoomListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000300 RID: 768
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserTryNavigateNewPageRequest__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserTryNavigateNewPageRequest__SWIG_0();

		// Token: 0x06000301 RID: 769
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserTryNavigateNewPageRequest")]
		public static extern void delete_RailEventkRailEventBrowserTryNavigateNewPageRequest(IntPtr jarg1);

		// Token: 0x06000302 RID: 770
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTryNavigateNewPageRequest_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserTryNavigateNewPageRequest_kInternalRailEventEventId_get();

		// Token: 0x06000303 RID: 771
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTryNavigateNewPageRequest_get_event_id")]
		public static extern int RailEventkRailEventBrowserTryNavigateNewPageRequest_get_event_id(IntPtr jarg1);

		// Token: 0x06000304 RID: 772
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserTryNavigateNewPageRequest__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserTryNavigateNewPageRequest__SWIG_1(IntPtr jarg1);

		// Token: 0x06000305 RID: 773
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcUninstallFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcUninstallFinished__SWIG_0();

		// Token: 0x06000306 RID: 774
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcUninstallFinished")]
		public static extern void delete_RailEventkRailEventDlcUninstallFinished(IntPtr jarg1);

		// Token: 0x06000307 RID: 775
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcUninstallFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcUninstallFinished_kInternalRailEventEventId_get();

		// Token: 0x06000308 RID: 776
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcUninstallFinished_get_event_id")]
		public static extern int RailEventkRailEventDlcUninstallFinished_get_event_id(IntPtr jarg1);

		// Token: 0x06000309 RID: 777
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcUninstallFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcUninstallFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600030A RID: 778
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsSplitFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsSplitFinished__SWIG_0();

		// Token: 0x0600030B RID: 779
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsSplitFinished")]
		public static extern void delete_RailEventkRailEventAssetsSplitFinished(IntPtr jarg1);

		// Token: 0x0600030C RID: 780
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsSplitFinished_kInternalRailEventEventId_get();

		// Token: 0x0600030D RID: 781
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsSplitFinished_get_event_id(IntPtr jarg1);

		// Token: 0x0600030E RID: 782
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsSplitFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsSplitFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600030F RID: 783
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult__SWIG_0();

		// Token: 0x06000310 RID: 784
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult")]
		public static extern void delete_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult(IntPtr jarg1);

		// Token: 0x06000311 RID: 785
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult_kInternalRailEventEventId_get();

		// Token: 0x06000312 RID: 786
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000313 RID: 787
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000314 RID: 788
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardUploaded__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardUploaded__SWIG_0();

		// Token: 0x06000315 RID: 789
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardUploaded")]
		public static extern void delete_RailEventkRailEventLeaderboardUploaded(IntPtr jarg1);

		// Token: 0x06000316 RID: 790
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardUploaded_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventLeaderboardUploaded_kInternalRailEventEventId_get();

		// Token: 0x06000317 RID: 791
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardUploaded_get_event_id")]
		public static extern int RailEventkRailEventLeaderboardUploaded_get_event_id(IntPtr jarg1);

		// Token: 0x06000318 RID: 792
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardUploaded__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardUploaded__SWIG_1(IntPtr jarg1);

		// Token: 0x06000319 RID: 793
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelRemoveUsersResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelRemoveUsersResult__SWIG_0();

		// Token: 0x0600031A RID: 794
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelRemoveUsersResult")]
		public static extern void delete_RailEventkRailEventVoiceChannelRemoveUsersResult(IntPtr jarg1);

		// Token: 0x0600031B RID: 795
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelRemoveUsersResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelRemoveUsersResult_kInternalRailEventEventId_get();

		// Token: 0x0600031C RID: 796
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelRemoveUsersResult_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelRemoveUsersResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600031D RID: 797
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelRemoveUsersResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelRemoveUsersResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600031E RID: 798
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSmallObjectServiceQueryObjectStateResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventSmallObjectServiceQueryObjectStateResult__SWIG_0();

		// Token: 0x0600031F RID: 799
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventSmallObjectServiceQueryObjectStateResult")]
		public static extern void delete_RailEventkRailEventSmallObjectServiceQueryObjectStateResult(IntPtr jarg1);

		// Token: 0x06000320 RID: 800
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSmallObjectServiceQueryObjectStateResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventSmallObjectServiceQueryObjectStateResult_kInternalRailEventEventId_get();

		// Token: 0x06000321 RID: 801
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSmallObjectServiceQueryObjectStateResult_get_event_id")]
		public static extern int RailEventkRailEventSmallObjectServiceQueryObjectStateResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000322 RID: 802
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSmallObjectServiceQueryObjectStateResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventSmallObjectServiceQueryObjectStateResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000323 RID: 803
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerCreated__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerCreated__SWIG_0();

		// Token: 0x06000324 RID: 804
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerCreated")]
		public static extern void delete_RailEventkRailEventGameServerCreated(IntPtr jarg1);

		// Token: 0x06000325 RID: 805
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerCreated_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerCreated_kInternalRailEventEventId_get();

		// Token: 0x06000326 RID: 806
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerCreated_get_event_id")]
		public static extern int RailEventkRailEventGameServerCreated_get_event_id(IntPtr jarg1);

		// Token: 0x06000327 RID: 807
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerCreated__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerCreated__SWIG_1(IntPtr jarg1);

		// Token: 0x06000328 RID: 808
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallStart__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcInstallStart__SWIG_0();

		// Token: 0x06000329 RID: 809
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcInstallStart")]
		public static extern void delete_RailEventkRailEventDlcInstallStart(IntPtr jarg1);

		// Token: 0x0600032A RID: 810
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStart_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcInstallStart_kInternalRailEventEventId_get();

		// Token: 0x0600032B RID: 811
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStart_get_event_id")]
		public static extern int RailEventkRailEventDlcInstallStart_get_event_id(IntPtr jarg1);

		// Token: 0x0600032C RID: 812
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallStart__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcInstallStart__SWIG_1(IntPtr jarg1);

		// Token: 0x0600032D RID: 813
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetInviteDetailResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersGetInviteDetailResult__SWIG_0();

		// Token: 0x0600032E RID: 814
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersGetInviteDetailResult")]
		public static extern void delete_RailEventkRailEventUsersGetInviteDetailResult(IntPtr jarg1);

		// Token: 0x0600032F RID: 815
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetInviteDetailResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersGetInviteDetailResult_kInternalRailEventEventId_get();

		// Token: 0x06000330 RID: 816
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetInviteDetailResult_get_event_id")]
		public static extern int RailEventkRailEventUsersGetInviteDetailResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000331 RID: 817
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetInviteDetailResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersGetInviteDetailResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000332 RID: 818
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceQuerySpaceWorksResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceQuerySpaceWorksResult__SWIG_0();

		// Token: 0x06000333 RID: 819
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceQuerySpaceWorksResult")]
		public static extern void delete_RailEventkRailEventUserSpaceQuerySpaceWorksResult(IntPtr jarg1);

		// Token: 0x06000334 RID: 820
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceQuerySpaceWorksResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceQuerySpaceWorksResult_kInternalRailEventEventId_get();

		// Token: 0x06000335 RID: 821
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceQuerySpaceWorksResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceQuerySpaceWorksResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000336 RID: 822
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceQuerySpaceWorksResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceQuerySpaceWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000337 RID: 823
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementGlobalAchievementReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAchievementGlobalAchievementReceived__SWIG_0();

		// Token: 0x06000338 RID: 824
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAchievementGlobalAchievementReceived")]
		public static extern void delete_RailEventkRailEventAchievementGlobalAchievementReceived(IntPtr jarg1);

		// Token: 0x06000339 RID: 825
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementGlobalAchievementReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAchievementGlobalAchievementReceived_kInternalRailEventEventId_get();

		// Token: 0x0600033A RID: 826
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementGlobalAchievementReceived_get_event_id")]
		public static extern int RailEventkRailEventAchievementGlobalAchievementReceived_get_event_id(IntPtr jarg1);

		// Token: 0x0600033B RID: 827
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementGlobalAchievementReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAchievementGlobalAchievementReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x0600033C RID: 828
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncRenameStreamFileResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncRenameStreamFileResult__SWIG_0();

		// Token: 0x0600033D RID: 829
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncRenameStreamFileResult")]
		public static extern void delete_RailEventkRailEventStorageAsyncRenameStreamFileResult(IntPtr jarg1);

		// Token: 0x0600033E RID: 830
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncRenameStreamFileResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageAsyncRenameStreamFileResult_kInternalRailEventEventId_get();

		// Token: 0x0600033F RID: 831
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncRenameStreamFileResult_get_event_id")]
		public static extern int RailEventkRailEventStorageAsyncRenameStreamFileResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000340 RID: 832
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncRenameStreamFileResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncRenameStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000341 RID: 833
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsReportPlayedWithUserListResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsReportPlayedWithUserListResult__SWIG_0();

		// Token: 0x06000342 RID: 834
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsReportPlayedWithUserListResult")]
		public static extern void delete_RailEventkRailEventFriendsReportPlayedWithUserListResult(IntPtr jarg1);

		// Token: 0x06000343 RID: 835
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsReportPlayedWithUserListResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsReportPlayedWithUserListResult_kInternalRailEventEventId_get();

		// Token: 0x06000344 RID: 836
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsReportPlayedWithUserListResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsReportPlayedWithUserListResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000345 RID: 837
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsReportPlayedWithUserListResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsReportPlayedWithUserListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000346 RID: 838
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncWriteStreamFileResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncWriteStreamFileResult__SWIG_0();

		// Token: 0x06000347 RID: 839
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncWriteStreamFileResult")]
		public static extern void delete_RailEventkRailEventStorageAsyncWriteStreamFileResult(IntPtr jarg1);

		// Token: 0x06000348 RID: 840
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteStreamFileResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageAsyncWriteStreamFileResult_kInternalRailEventEventId_get();

		// Token: 0x06000349 RID: 841
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteStreamFileResult_get_event_id")]
		public static extern int RailEventkRailEventStorageAsyncWriteStreamFileResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600034A RID: 842
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncWriteStreamFileResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncWriteStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600034B RID: 843
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomKickOffMemberResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomKickOffMemberResult__SWIG_0();

		// Token: 0x0600034C RID: 844
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomKickOffMemberResult")]
		public static extern void delete_RailEventkRailEventRoomKickOffMemberResult(IntPtr jarg1);

		// Token: 0x0600034D RID: 845
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomKickOffMemberResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomKickOffMemberResult_kInternalRailEventEventId_get();

		// Token: 0x0600034E RID: 846
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomKickOffMemberResult_get_event_id")]
		public static extern int RailEventkRailEventRoomKickOffMemberResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600034F RID: 847
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomKickOffMemberResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomKickOffMemberResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000350 RID: 848
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventShowFloatingWindow__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventShowFloatingWindow__SWIG_0();

		// Token: 0x06000351 RID: 849
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventShowFloatingWindow")]
		public static extern void delete_RailEventkRailEventShowFloatingWindow(IntPtr jarg1);

		// Token: 0x06000352 RID: 850
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingWindow_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventShowFloatingWindow_kInternalRailEventEventId_get();

		// Token: 0x06000353 RID: 851
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingWindow_get_event_id")]
		public static extern int RailEventkRailEventShowFloatingWindow_get_event_id(IntPtr jarg1);

		// Token: 0x06000354 RID: 852
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventShowFloatingWindow__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventShowFloatingWindow__SWIG_1(IntPtr jarg1);

		// Token: 0x06000355 RID: 853
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomOwnerChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomOwnerChanged__SWIG_0();

		// Token: 0x06000356 RID: 854
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyRoomOwnerChanged")]
		public static extern void delete_RailEventkRailEventRoomNotifyRoomOwnerChanged(IntPtr jarg1);

		// Token: 0x06000357 RID: 855
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomOwnerChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomNotifyRoomOwnerChanged_kInternalRailEventEventId_get();

		// Token: 0x06000358 RID: 856
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomOwnerChanged_get_event_id")]
		public static extern int RailEventkRailEventRoomNotifyRoomOwnerChanged_get_event_id(IntPtr jarg1);

		// Token: 0x06000359 RID: 857
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomOwnerChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomOwnerChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x0600035A RID: 858
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFinalize__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFinalize__SWIG_0();

		// Token: 0x0600035B RID: 859
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFinalize")]
		public static extern void delete_RailEventkRailEventFinalize(IntPtr jarg1);

		// Token: 0x0600035C RID: 860
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFinalize_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFinalize_kInternalRailEventEventId_get();

		// Token: 0x0600035D RID: 861
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFinalize_get_event_id")]
		public static extern int RailEventkRailEventFinalize_get_event_id(IntPtr jarg1);

		// Token: 0x0600035E RID: 862
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFinalize__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFinalize__SWIG_1(IntPtr jarg1);

		// Token: 0x0600035F RID: 863
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersCancelInviteResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersCancelInviteResult__SWIG_0();

		// Token: 0x06000360 RID: 864
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersCancelInviteResult")]
		public static extern void delete_RailEventkRailEventUsersCancelInviteResult(IntPtr jarg1);

		// Token: 0x06000361 RID: 865
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersCancelInviteResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersCancelInviteResult_kInternalRailEventEventId_get();

		// Token: 0x06000362 RID: 866
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersCancelInviteResult_get_event_id")]
		public static extern int RailEventkRailEventUsersCancelInviteResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000363 RID: 867
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersCancelInviteResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersCancelInviteResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000364 RID: 868
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomListResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomListResult__SWIG_0();

		// Token: 0x06000365 RID: 869
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomListResult")]
		public static extern void delete_RailEventkRailEventRoomListResult(IntPtr jarg1);

		// Token: 0x06000366 RID: 870
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomListResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomListResult_kInternalRailEventEventId_get();

		// Token: 0x06000367 RID: 871
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomListResult_get_event_id")]
		public static extern int RailEventkRailEventRoomListResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000368 RID: 872
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomListResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000369 RID: 873
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserDamageRectPaint__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserDamageRectPaint__SWIG_0();

		// Token: 0x0600036A RID: 874
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserDamageRectPaint")]
		public static extern void delete_RailEventkRailEventBrowserDamageRectPaint(IntPtr jarg1);

		// Token: 0x0600036B RID: 875
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserDamageRectPaint_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserDamageRectPaint_kInternalRailEventEventId_get();

		// Token: 0x0600036C RID: 876
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserDamageRectPaint_get_event_id")]
		public static extern int RailEventkRailEventBrowserDamageRectPaint_get_event_id(IntPtr jarg1);

		// Token: 0x0600036D RID: 877
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserDamageRectPaint__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserDamageRectPaint__SWIG_1(IntPtr jarg1);

		// Token: 0x0600036E RID: 878
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsUpdateAssetPropertyFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsUpdateAssetPropertyFinished__SWIG_0();

		// Token: 0x0600036F RID: 879
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsUpdateAssetPropertyFinished")]
		public static extern void delete_RailEventkRailEventAssetsUpdateAssetPropertyFinished(IntPtr jarg1);

		// Token: 0x06000370 RID: 880
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateAssetPropertyFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsUpdateAssetPropertyFinished_kInternalRailEventEventId_get();

		// Token: 0x06000371 RID: 881
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateAssetPropertyFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsUpdateAssetPropertyFinished_get_event_id(IntPtr jarg1);

		// Token: 0x06000372 RID: 882
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsUpdateAssetPropertyFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsUpdateAssetPropertyFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000373 RID: 883
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult__SWIG_0();

		// Token: 0x06000374 RID: 884
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult")]
		public static extern void delete_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult(IntPtr jarg1);

		// Token: 0x06000375 RID: 885
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult_kInternalRailEventEventId_get();

		// Token: 0x06000376 RID: 886
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult_get_event_id")]
		public static extern int RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000377 RID: 887
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000378 RID: 888
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventPlayerGetGamePurchaseKey__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventPlayerGetGamePurchaseKey__SWIG_0();

		// Token: 0x06000379 RID: 889
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventPlayerGetGamePurchaseKey")]
		public static extern void delete_RailEventkRailEventPlayerGetGamePurchaseKey(IntPtr jarg1);

		// Token: 0x0600037A RID: 890
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetGamePurchaseKey_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventPlayerGetGamePurchaseKey_kInternalRailEventEventId_get();

		// Token: 0x0600037B RID: 891
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetGamePurchaseKey_get_event_id")]
		public static extern int RailEventkRailEventPlayerGetGamePurchaseKey_get_event_id(IntPtr jarg1);

		// Token: 0x0600037C RID: 892
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventPlayerGetGamePurchaseKey__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventPlayerGetGamePurchaseKey__SWIG_1(IntPtr jarg1);

		// Token: 0x0600037D RID: 893
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByRoom__SWIG_0")]
		public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByRoom__SWIG_0();

		// Token: 0x0600037E RID: 894
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailPlatformNotifyEventJoinGameByRoom")]
		public static extern void delete_RailEventkRailPlatformNotifyEventJoinGameByRoom(IntPtr jarg1);

		// Token: 0x0600037F RID: 895
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByRoom_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailPlatformNotifyEventJoinGameByRoom_kInternalRailEventEventId_get();

		// Token: 0x06000380 RID: 896
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByRoom_get_event_id")]
		public static extern int RailEventkRailPlatformNotifyEventJoinGameByRoom_get_event_id(IntPtr jarg1);

		// Token: 0x06000381 RID: 897
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByRoom__SWIG_1")]
		public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByRoom__SWIG_1(IntPtr jarg1);

		// Token: 0x06000382 RID: 898
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomSetMemberMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomSetMemberMetadataResult__SWIG_0();

		// Token: 0x06000383 RID: 899
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomSetMemberMetadataResult")]
		public static extern void delete_RailEventkRailEventRoomSetMemberMetadataResult(IntPtr jarg1);

		// Token: 0x06000384 RID: 900
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetMemberMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomSetMemberMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x06000385 RID: 901
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetMemberMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventRoomSetMemberMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000386 RID: 902
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomSetMemberMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomSetMemberMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000387 RID: 903
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceModifyFavoritesWorksResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceModifyFavoritesWorksResult__SWIG_0();

		// Token: 0x06000388 RID: 904
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceModifyFavoritesWorksResult")]
		public static extern void delete_RailEventkRailEventUserSpaceModifyFavoritesWorksResult(IntPtr jarg1);

		// Token: 0x06000389 RID: 905
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceModifyFavoritesWorksResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceModifyFavoritesWorksResult_kInternalRailEventEventId_get();

		// Token: 0x0600038A RID: 906
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceModifyFavoritesWorksResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceModifyFavoritesWorksResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600038B RID: 907
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceModifyFavoritesWorksResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceModifyFavoritesWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600038C RID: 908
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent__SWIG_0();

		// Token: 0x0600038D RID: 909
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent")]
		public static extern void delete_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent(IntPtr jarg1);

		// Token: 0x0600038E RID: 910
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent_kInternalRailEventEventId_get();

		// Token: 0x0600038F RID: 911
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent_get_event_id(IntPtr jarg1);

		// Token: 0x06000390 RID: 912
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000391 RID: 913
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersRespondInvitation__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersRespondInvitation__SWIG_0();

		// Token: 0x06000392 RID: 914
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersRespondInvitation")]
		public static extern void delete_RailEventkRailEventUsersRespondInvitation(IntPtr jarg1);

		// Token: 0x06000393 RID: 915
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersRespondInvitation_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersRespondInvitation_kInternalRailEventEventId_get();

		// Token: 0x06000394 RID: 916
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersRespondInvitation_get_event_id")]
		public static extern int RailEventkRailEventUsersRespondInvitation_get_event_id(IntPtr jarg1);

		// Token: 0x06000395 RID: 917
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersRespondInvitation__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersRespondInvitation__SWIG_1(IntPtr jarg1);

		// Token: 0x06000396 RID: 918
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsDirectConsumeFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsDirectConsumeFinished__SWIG_0();

		// Token: 0x06000397 RID: 919
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsDirectConsumeFinished")]
		public static extern void delete_RailEventkRailEventAssetsDirectConsumeFinished(IntPtr jarg1);

		// Token: 0x06000398 RID: 920
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsDirectConsumeFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsDirectConsumeFinished_kInternalRailEventEventId_get();

		// Token: 0x06000399 RID: 921
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsDirectConsumeFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsDirectConsumeFinished_get_event_id(IntPtr jarg1);

		// Token: 0x0600039A RID: 922
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsDirectConsumeFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsDirectConsumeFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600039B RID: 923
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomZoneListResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomZoneListResult__SWIG_0();

		// Token: 0x0600039C RID: 924
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomZoneListResult")]
		public static extern void delete_RailEventkRailEventRoomZoneListResult(IntPtr jarg1);

		// Token: 0x0600039D RID: 925
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomZoneListResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomZoneListResult_kInternalRailEventEventId_get();

		// Token: 0x0600039E RID: 926
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomZoneListResult_get_event_id")]
		public static extern int RailEventkRailEventRoomZoneListResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600039F RID: 927
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomZoneListResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomZoneListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003A0 RID: 928
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult__SWIG_0();

		// Token: 0x060003A1 RID: 929
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult")]
		public static extern void delete_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult(IntPtr jarg1);

		// Token: 0x060003A2 RID: 930
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_kInternalRailEventEventId_get();

		// Token: 0x060003A3 RID: 931
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003A4 RID: 932
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003A5 RID: 933
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallStartResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcInstallStartResult__SWIG_0();

		// Token: 0x060003A6 RID: 934
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcInstallStartResult")]
		public static extern void delete_RailEventkRailEventDlcInstallStartResult(IntPtr jarg1);

		// Token: 0x060003A7 RID: 935
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStartResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcInstallStartResult_kInternalRailEventEventId_get();

		// Token: 0x060003A8 RID: 936
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStartResult_get_event_id")]
		public static extern int RailEventkRailEventDlcInstallStartResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003A9 RID: 937
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallStartResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcInstallStartResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003AA RID: 938
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomDestroyed__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomDestroyed__SWIG_0();

		// Token: 0x060003AB RID: 939
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyRoomDestroyed")]
		public static extern void delete_RailEventkRailEventRoomNotifyRoomDestroyed(IntPtr jarg1);

		// Token: 0x060003AC RID: 940
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDestroyed_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomNotifyRoomDestroyed_kInternalRailEventEventId_get();

		// Token: 0x060003AD RID: 941
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDestroyed_get_event_id")]
		public static extern int RailEventkRailEventRoomNotifyRoomDestroyed_get_event_id(IntPtr jarg1);

		// Token: 0x060003AE RID: 942
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomDestroyed__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomDestroyed__SWIG_1(IntPtr jarg1);

		// Token: 0x060003AF RID: 943
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserPaint__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserPaint__SWIG_0();

		// Token: 0x060003B0 RID: 944
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserPaint")]
		public static extern void delete_RailEventkRailEventBrowserPaint(IntPtr jarg1);

		// Token: 0x060003B1 RID: 945
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserPaint_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserPaint_kInternalRailEventEventId_get();

		// Token: 0x060003B2 RID: 946
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserPaint_get_event_id")]
		public static extern int RailEventkRailEventBrowserPaint_get_event_id(IntPtr jarg1);

		// Token: 0x060003B3 RID: 947
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserPaint__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserPaint__SWIG_1(IntPtr jarg1);

		// Token: 0x060003B4 RID: 948
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsExchangeAssetsFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsExchangeAssetsFinished__SWIG_0();

		// Token: 0x060003B5 RID: 949
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsExchangeAssetsFinished")]
		public static extern void delete_RailEventkRailEventAssetsExchangeAssetsFinished(IntPtr jarg1);

		// Token: 0x060003B6 RID: 950
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsExchangeAssetsFinished_kInternalRailEventEventId_get();

		// Token: 0x060003B7 RID: 951
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsExchangeAssetsFinished_get_event_id(IntPtr jarg1);

		// Token: 0x060003B8 RID: 952
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsExchangeAssetsFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsExchangeAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x060003B9 RID: 953
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived__SWIG_0();

		// Token: 0x060003BA RID: 954
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived")]
		public static extern void delete_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived(IntPtr jarg1);

		// Token: 0x060003BB RID: 955
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_kInternalRailEventEventId_get();

		// Token: 0x060003BC RID: 956
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_get_event_id")]
		public static extern int RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_get_event_id(IntPtr jarg1);

		// Token: 0x060003BD RID: 957
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x060003BE RID: 958
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetFriendPlayedGamesResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsGetFriendPlayedGamesResult__SWIG_0();

		// Token: 0x060003BF RID: 959
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsGetFriendPlayedGamesResult")]
		public static extern void delete_RailEventkRailEventFriendsGetFriendPlayedGamesResult(IntPtr jarg1);

		// Token: 0x060003C0 RID: 960
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetFriendPlayedGamesResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsGetFriendPlayedGamesResult_kInternalRailEventEventId_get();

		// Token: 0x060003C1 RID: 961
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetFriendPlayedGamesResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsGetFriendPlayedGamesResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003C2 RID: 962
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetFriendPlayedGamesResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsGetFriendPlayedGamesResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003C3 RID: 963
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsClearMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsClearMetadataResult__SWIG_0();

		// Token: 0x060003C4 RID: 964
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsClearMetadataResult")]
		public static extern void delete_RailEventkRailEventFriendsClearMetadataResult(IntPtr jarg1);

		// Token: 0x060003C5 RID: 965
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsClearMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsClearMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x060003C6 RID: 966
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsClearMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsClearMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003C7 RID: 967
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsClearMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsClearMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003C8 RID: 968
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelMemberChangedEvent__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelMemberChangedEvent__SWIG_0();

		// Token: 0x060003C9 RID: 969
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelMemberChangedEvent")]
		public static extern void delete_RailEventkRailEventVoiceChannelMemberChangedEvent(IntPtr jarg1);

		// Token: 0x060003CA RID: 970
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelMemberChangedEvent_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelMemberChangedEvent_kInternalRailEventEventId_get();

		// Token: 0x060003CB RID: 971
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelMemberChangedEvent_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelMemberChangedEvent_get_event_id(IntPtr jarg1);

		// Token: 0x060003CC RID: 972
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelMemberChangedEvent__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelMemberChangedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x060003CD RID: 973
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsAddFriendResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsAddFriendResult__SWIG_0();

		// Token: 0x060003CE RID: 974
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsAddFriendResult")]
		public static extern void delete_RailEventkRailEventFriendsAddFriendResult(IntPtr jarg1);

		// Token: 0x060003CF RID: 975
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsAddFriendResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsAddFriendResult_kInternalRailEventEventId_get();

		// Token: 0x060003D0 RID: 976
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsAddFriendResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsAddFriendResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003D1 RID: 977
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsAddFriendResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsAddFriendResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003D2 RID: 978
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseAllProductsInfoReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchaseAllProductsInfoReceived__SWIG_0();

		// Token: 0x060003D3 RID: 979
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchaseAllProductsInfoReceived")]
		public static extern void delete_RailEventkRailEventInGamePurchaseAllProductsInfoReceived(IntPtr jarg1);

		// Token: 0x060003D4 RID: 980
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllProductsInfoReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventInGamePurchaseAllProductsInfoReceived_kInternalRailEventEventId_get();

		// Token: 0x060003D5 RID: 981
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllProductsInfoReceived_get_event_id")]
		public static extern int RailEventkRailEventInGamePurchaseAllProductsInfoReceived_get_event_id(IntPtr jarg1);

		// Token: 0x060003D6 RID: 982
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseAllProductsInfoReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchaseAllProductsInfoReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x060003D7 RID: 983
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsMergeFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsMergeFinished__SWIG_0();

		// Token: 0x060003D8 RID: 984
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsMergeFinished")]
		public static extern void delete_RailEventkRailEventAssetsMergeFinished(IntPtr jarg1);

		// Token: 0x060003D9 RID: 985
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsMergeFinished_kInternalRailEventEventId_get();

		// Token: 0x060003DA RID: 986
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsMergeFinished_get_event_id(IntPtr jarg1);

		// Token: 0x060003DB RID: 987
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsMergeFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsMergeFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x060003DC RID: 988
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMemberkicked__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyMemberkicked__SWIG_0();

		// Token: 0x060003DD RID: 989
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyMemberkicked")]
		public static extern void delete_RailEventkRailEventRoomNotifyMemberkicked(IntPtr jarg1);

		// Token: 0x060003DE RID: 990
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberkicked_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomNotifyMemberkicked_kInternalRailEventEventId_get();

		// Token: 0x060003DF RID: 991
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberkicked_get_event_id")]
		public static extern int RailEventkRailEventRoomNotifyMemberkicked_get_event_id(IntPtr jarg1);

		// Token: 0x060003E0 RID: 992
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMemberkicked__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyMemberkicked__SWIG_1(IntPtr jarg1);

		// Token: 0x060003E1 RID: 993
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomJoinRoomResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomJoinRoomResult__SWIG_0();

		// Token: 0x060003E2 RID: 994
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomJoinRoomResult")]
		public static extern void delete_RailEventkRailEventRoomJoinRoomResult(IntPtr jarg1);

		// Token: 0x060003E3 RID: 995
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomJoinRoomResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomJoinRoomResult_kInternalRailEventEventId_get();

		// Token: 0x060003E4 RID: 996
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomJoinRoomResult_get_event_id")]
		public static extern int RailEventkRailEventRoomJoinRoomResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003E5 RID: 997
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomJoinRoomResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomJoinRoomResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003E6 RID: 998
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerAuthSessionTicket__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerAuthSessionTicket__SWIG_0();

		// Token: 0x060003E7 RID: 999
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerAuthSessionTicket")]
		public static extern void delete_RailEventkRailEventGameServerAuthSessionTicket(IntPtr jarg1);

		// Token: 0x060003E8 RID: 1000
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAuthSessionTicket_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerAuthSessionTicket_kInternalRailEventEventId_get();

		// Token: 0x060003E9 RID: 1001
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAuthSessionTicket_get_event_id")]
		public static extern int RailEventkRailEventGameServerAuthSessionTicket_get_event_id(IntPtr jarg1);

		// Token: 0x060003EA RID: 1002
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerAuthSessionTicket__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerAuthSessionTicket__SWIG_1(IntPtr jarg1);

		// Token: 0x060003EB RID: 1003
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult__SWIG_0();

		// Token: 0x060003EC RID: 1004
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult")]
		public static extern void delete_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult(IntPtr jarg1);

		// Token: 0x060003ED RID: 1005
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult_kInternalRailEventEventId_get();

		// Token: 0x060003EE RID: 1006
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003EF RID: 1007
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003F0 RID: 1008
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerRegisterToServerListResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerRegisterToServerListResult__SWIG_0();

		// Token: 0x060003F1 RID: 1009
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerRegisterToServerListResult")]
		public static extern void delete_RailEventkRailEventGameServerRegisterToServerListResult(IntPtr jarg1);

		// Token: 0x060003F2 RID: 1010
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRegisterToServerListResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerRegisterToServerListResult_kInternalRailEventEventId_get();

		// Token: 0x060003F3 RID: 1011
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRegisterToServerListResult_get_event_id")]
		public static extern int RailEventkRailEventGameServerRegisterToServerListResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003F4 RID: 1012
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerRegisterToServerListResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerRegisterToServerListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003F5 RID: 1013
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePaymentResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePaymentResult__SWIG_0();

		// Token: 0x060003F6 RID: 1014
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGameStorePurchasePaymentResult")]
		public static extern void delete_RailEventkRailEventInGameStorePurchasePaymentResult(IntPtr jarg1);

		// Token: 0x060003F7 RID: 1015
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePaymentResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventInGameStorePurchasePaymentResult_kInternalRailEventEventId_get();

		// Token: 0x060003F8 RID: 1016
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePaymentResult_get_event_id")]
		public static extern int RailEventkRailEventInGameStorePurchasePaymentResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003F9 RID: 1017
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePaymentResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePaymentResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003FA RID: 1018
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcCheckAllDlcsStateReadyResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcCheckAllDlcsStateReadyResult__SWIG_0();

		// Token: 0x060003FB RID: 1019
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcCheckAllDlcsStateReadyResult")]
		public static extern void delete_RailEventkRailEventDlcCheckAllDlcsStateReadyResult(IntPtr jarg1);

		// Token: 0x060003FC RID: 1020
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcCheckAllDlcsStateReadyResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcCheckAllDlcsStateReadyResult_kInternalRailEventEventId_get();

		// Token: 0x060003FD RID: 1021
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcCheckAllDlcsStateReadyResult_get_event_id")]
		public static extern int RailEventkRailEventDlcCheckAllDlcsStateReadyResult_get_event_id(IntPtr jarg1);

		// Token: 0x060003FE RID: 1022
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcCheckAllDlcsStateReadyResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcCheckAllDlcsStateReadyResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060003FF RID: 1023
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotTakeScreenshotFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventScreenshotTakeScreenshotFinished__SWIG_0();

		// Token: 0x06000400 RID: 1024
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventScreenshotTakeScreenshotFinished")]
		public static extern void delete_RailEventkRailEventScreenshotTakeScreenshotFinished(IntPtr jarg1);

		// Token: 0x06000401 RID: 1025
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventScreenshotTakeScreenshotFinished_kInternalRailEventEventId_get();

		// Token: 0x06000402 RID: 1026
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotFinished_get_event_id")]
		public static extern int RailEventkRailEventScreenshotTakeScreenshotFinished_get_event_id(IntPtr jarg1);

		// Token: 0x06000403 RID: 1027
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotTakeScreenshotFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventScreenshotTakeScreenshotFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000404 RID: 1028
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserReloadResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserReloadResult__SWIG_0();

		// Token: 0x06000405 RID: 1029
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserReloadResult")]
		public static extern void delete_RailEventkRailEventBrowserReloadResult(IntPtr jarg1);

		// Token: 0x06000406 RID: 1030
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserReloadResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserReloadResult_kInternalRailEventEventId_get();

		// Token: 0x06000407 RID: 1031
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserReloadResult_get_event_id")]
		public static extern int RailEventkRailEventBrowserReloadResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000408 RID: 1032
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserReloadResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserReloadResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000409 RID: 1033
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventShowFloatingNotifyWindow__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventShowFloatingNotifyWindow__SWIG_0();

		// Token: 0x0600040A RID: 1034
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventShowFloatingNotifyWindow")]
		public static extern void delete_RailEventkRailEventShowFloatingNotifyWindow(IntPtr jarg1);

		// Token: 0x0600040B RID: 1035
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingNotifyWindow_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventShowFloatingNotifyWindow_kInternalRailEventEventId_get();

		// Token: 0x0600040C RID: 1036
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingNotifyWindow_get_event_id")]
		public static extern int RailEventkRailEventShowFloatingNotifyWindow_get_event_id(IntPtr jarg1);

		// Token: 0x0600040D RID: 1037
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventShowFloatingNotifyWindow__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventShowFloatingNotifyWindow__SWIG_1(IntPtr jarg1);

		// Token: 0x0600040E RID: 1038
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult__SWIG_0();

		// Token: 0x0600040F RID: 1039
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult")]
		public static extern void delete_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult(IntPtr jarg1);

		// Token: 0x06000410 RID: 1040
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_kInternalRailEventEventId_get();

		// Token: 0x06000411 RID: 1041
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_get_event_id")]
		public static extern int RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000412 RID: 1042
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000413 RID: 1043
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetRoomMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomGetRoomMetadataResult__SWIG_0();

		// Token: 0x06000414 RID: 1044
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGetRoomMetadataResult")]
		public static extern void delete_RailEventkRailEventRoomGetRoomMetadataResult(IntPtr jarg1);

		// Token: 0x06000415 RID: 1045
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetRoomMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomGetRoomMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x06000416 RID: 1046
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetRoomMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventRoomGetRoomMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000417 RID: 1047
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetRoomMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomGetRoomMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000418 RID: 1048
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersShowUserHomepageWindowResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersShowUserHomepageWindowResult__SWIG_0();

		// Token: 0x06000419 RID: 1049
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersShowUserHomepageWindowResult")]
		public static extern void delete_RailEventkRailEventUsersShowUserHomepageWindowResult(IntPtr jarg1);

		// Token: 0x0600041A RID: 1050
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersShowUserHomepageWindowResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersShowUserHomepageWindowResult_kInternalRailEventEventId_get();

		// Token: 0x0600041B RID: 1051
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersShowUserHomepageWindowResult_get_event_id")]
		public static extern int RailEventkRailEventUsersShowUserHomepageWindowResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600041C RID: 1052
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersShowUserHomepageWindowResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersShowUserHomepageWindowResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600041D RID: 1053
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePayWindowDisplayed__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePayWindowDisplayed__SWIG_0();

		// Token: 0x0600041E RID: 1054
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGameStorePurchasePayWindowDisplayed")]
		public static extern void delete_RailEventkRailEventInGameStorePurchasePayWindowDisplayed(IntPtr jarg1);

		// Token: 0x0600041F RID: 1055
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowDisplayed_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventInGameStorePurchasePayWindowDisplayed_kInternalRailEventEventId_get();

		// Token: 0x06000420 RID: 1056
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowDisplayed_get_event_id")]
		public static extern int RailEventkRailEventInGameStorePurchasePayWindowDisplayed_get_event_id(IntPtr jarg1);

		// Token: 0x06000421 RID: 1057
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePayWindowDisplayed__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePayWindowDisplayed__SWIG_1(IntPtr jarg1);

		// Token: 0x06000422 RID: 1058
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceVoteSpaceWorkResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceVoteSpaceWorkResult__SWIG_0();

		// Token: 0x06000423 RID: 1059
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceVoteSpaceWorkResult")]
		public static extern void delete_RailEventkRailEventUserSpaceVoteSpaceWorkResult(IntPtr jarg1);

		// Token: 0x06000424 RID: 1060
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceVoteSpaceWorkResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceVoteSpaceWorkResult_kInternalRailEventEventId_get();

		// Token: 0x06000425 RID: 1061
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceVoteSpaceWorkResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceVoteSpaceWorkResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000426 RID: 1062
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceVoteSpaceWorkResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceVoteSpaceWorkResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000427 RID: 1063
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventIMEHelperTextInputSelectedResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventIMEHelperTextInputSelectedResult__SWIG_0();

		// Token: 0x06000428 RID: 1064
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventIMEHelperTextInputSelectedResult")]
		public static extern void delete_RailEventkRailEventIMEHelperTextInputSelectedResult(IntPtr jarg1);

		// Token: 0x06000429 RID: 1065
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventIMEHelperTextInputSelectedResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventIMEHelperTextInputSelectedResult_kInternalRailEventEventId_get();

		// Token: 0x0600042A RID: 1066
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventIMEHelperTextInputSelectedResult_get_event_id")]
		public static extern int RailEventkRailEventIMEHelperTextInputSelectedResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600042B RID: 1067
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventIMEHelperTextInputSelectedResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventIMEHelperTextInputSelectedResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600042C RID: 1068
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMetadataChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyMetadataChanged__SWIG_0();

		// Token: 0x0600042D RID: 1069
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyMetadataChanged")]
		public static extern void delete_RailEventkRailEventRoomNotifyMetadataChanged(IntPtr jarg1);

		// Token: 0x0600042E RID: 1070
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMetadataChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomNotifyMetadataChanged_kInternalRailEventEventId_get();

		// Token: 0x0600042F RID: 1071
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMetadataChanged_get_event_id")]
		public static extern int RailEventkRailEventRoomNotifyMetadataChanged_get_event_id(IntPtr jarg1);

		// Token: 0x06000430 RID: 1072
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyMetadataChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyMetadataChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x06000431 RID: 1073
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventTextInputShowTextInputWindowResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventTextInputShowTextInputWindowResult__SWIG_0();

		// Token: 0x06000432 RID: 1074
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventTextInputShowTextInputWindowResult")]
		public static extern void delete_RailEventkRailEventTextInputShowTextInputWindowResult(IntPtr jarg1);

		// Token: 0x06000433 RID: 1075
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventTextInputShowTextInputWindowResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventTextInputShowTextInputWindowResult_kInternalRailEventEventId_get();

		// Token: 0x06000434 RID: 1076
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventTextInputShowTextInputWindowResult_get_event_id")]
		public static extern int RailEventkRailEventTextInputShowTextInputWindowResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000435 RID: 1077
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventTextInputShowTextInputWindowResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventTextInputShowTextInputWindowResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000436 RID: 1078
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerSetMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerSetMetadataResult__SWIG_0();

		// Token: 0x06000437 RID: 1079
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerSetMetadataResult")]
		public static extern void delete_RailEventkRailEventGameServerSetMetadataResult(IntPtr jarg1);

		// Token: 0x06000438 RID: 1080
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerSetMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerSetMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x06000439 RID: 1081
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerSetMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventGameServerSetMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600043A RID: 1082
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerSetMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerSetMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600043B RID: 1083
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersInviteUsersResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersInviteUsersResult__SWIG_0();

		// Token: 0x0600043C RID: 1084
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersInviteUsersResult")]
		public static extern void delete_RailEventkRailEventUsersInviteUsersResult(IntPtr jarg1);

		// Token: 0x0600043D RID: 1085
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteUsersResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersInviteUsersResult_kInternalRailEventEventId_get();

		// Token: 0x0600043E RID: 1086
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteUsersResult_get_event_id")]
		public static extern int RailEventkRailEventUsersInviteUsersResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600043F RID: 1087
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersInviteUsersResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersInviteUsersResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000440 RID: 1088
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsOnlineStateChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsOnlineStateChanged__SWIG_0();

		// Token: 0x06000441 RID: 1089
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsOnlineStateChanged")]
		public static extern void delete_RailEventkRailEventFriendsOnlineStateChanged(IntPtr jarg1);

		// Token: 0x06000442 RID: 1090
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsOnlineStateChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsOnlineStateChanged_kInternalRailEventEventId_get();

		// Token: 0x06000443 RID: 1091
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsOnlineStateChanged_get_event_id")]
		public static extern int RailEventkRailEventFriendsOnlineStateChanged_get_event_id(IntPtr jarg1);

		// Token: 0x06000444 RID: 1092
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsOnlineStateChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsOnlineStateChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x06000445 RID: 1093
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceUpdateMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceUpdateMetadataResult__SWIG_0();

		// Token: 0x06000446 RID: 1094
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceUpdateMetadataResult")]
		public static extern void delete_RailEventkRailEventUserSpaceUpdateMetadataResult(IntPtr jarg1);

		// Token: 0x06000447 RID: 1095
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceUpdateMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceUpdateMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x06000448 RID: 1096
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceUpdateMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceUpdateMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000449 RID: 1097
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceUpdateMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceUpdateMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600044A RID: 1098
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsUpdateConsumeFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsUpdateConsumeFinished__SWIG_0();

		// Token: 0x0600044B RID: 1099
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsUpdateConsumeFinished")]
		public static extern void delete_RailEventkRailEventAssetsUpdateConsumeFinished(IntPtr jarg1);

		// Token: 0x0600044C RID: 1100
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateConsumeFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsUpdateConsumeFinished_kInternalRailEventEventId_get();

		// Token: 0x0600044D RID: 1101
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateConsumeFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsUpdateConsumeFinished_get_event_id(IntPtr jarg1);

		// Token: 0x0600044E RID: 1102
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsUpdateConsumeFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsUpdateConsumeFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600044F RID: 1103
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelJoinedResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelJoinedResult__SWIG_0();

		// Token: 0x06000450 RID: 1104
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelJoinedResult")]
		public static extern void delete_RailEventkRailEventVoiceChannelJoinedResult(IntPtr jarg1);

		// Token: 0x06000451 RID: 1105
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelJoinedResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelJoinedResult_kInternalRailEventEventId_get();

		// Token: 0x06000452 RID: 1106
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelJoinedResult_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelJoinedResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000453 RID: 1107
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelJoinedResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelJoinedResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000454 RID: 1108
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseFinishOrderResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchaseFinishOrderResult__SWIG_0();

		// Token: 0x06000455 RID: 1109
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGamePurchaseFinishOrderResult")]
		public static extern void delete_RailEventkRailEventInGamePurchaseFinishOrderResult(IntPtr jarg1);

		// Token: 0x06000456 RID: 1110
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseFinishOrderResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventInGamePurchaseFinishOrderResult_kInternalRailEventEventId_get();

		// Token: 0x06000457 RID: 1111
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseFinishOrderResult_get_event_id")]
		public static extern int RailEventkRailEventInGamePurchaseFinishOrderResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000458 RID: 1112
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGamePurchaseFinishOrderResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventInGamePurchaseFinishOrderResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000459 RID: 1113
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsStartConsumeFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsStartConsumeFinished__SWIG_0();

		// Token: 0x0600045A RID: 1114
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsStartConsumeFinished")]
		public static extern void delete_RailEventkRailEventAssetsStartConsumeFinished(IntPtr jarg1);

		// Token: 0x0600045B RID: 1115
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsStartConsumeFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsStartConsumeFinished_kInternalRailEventEventId_get();

		// Token: 0x0600045C RID: 1116
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsStartConsumeFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsStartConsumeFinished_get_event_id(IntPtr jarg1);

		// Token: 0x0600045D RID: 1117
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsStartConsumeFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsStartConsumeFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600045E RID: 1118
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerFavoriteGameServers__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerFavoriteGameServers__SWIG_0();

		// Token: 0x0600045F RID: 1119
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerFavoriteGameServers")]
		public static extern void delete_RailEventkRailEventGameServerFavoriteGameServers(IntPtr jarg1);

		// Token: 0x06000460 RID: 1120
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerFavoriteGameServers_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerFavoriteGameServers_kInternalRailEventEventId_get();

		// Token: 0x06000461 RID: 1121
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerFavoriteGameServers_get_event_id")]
		public static extern int RailEventkRailEventGameServerFavoriteGameServers_get_event_id(IntPtr jarg1);

		// Token: 0x06000462 RID: 1122
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerFavoriteGameServers__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerFavoriteGameServers__SWIG_1(IntPtr jarg1);

		// Token: 0x06000463 RID: 1123
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserJavascriptEvent__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserJavascriptEvent__SWIG_0();

		// Token: 0x06000464 RID: 1124
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserJavascriptEvent")]
		public static extern void delete_RailEventkRailEventBrowserJavascriptEvent(IntPtr jarg1);

		// Token: 0x06000465 RID: 1125
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserJavascriptEvent_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserJavascriptEvent_kInternalRailEventEventId_get();

		// Token: 0x06000466 RID: 1126
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserJavascriptEvent_get_event_id")]
		public static extern int RailEventkRailEventBrowserJavascriptEvent_get_event_id(IntPtr jarg1);

		// Token: 0x06000467 RID: 1127
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserJavascriptEvent__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserJavascriptEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000468 RID: 1128
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementPlayerAchievementReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAchievementPlayerAchievementReceived__SWIG_0();

		// Token: 0x06000469 RID: 1129
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAchievementPlayerAchievementReceived")]
		public static extern void delete_RailEventkRailEventAchievementPlayerAchievementReceived(IntPtr jarg1);

		// Token: 0x0600046A RID: 1130
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAchievementPlayerAchievementReceived_kInternalRailEventEventId_get();

		// Token: 0x0600046B RID: 1131
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementReceived_get_event_id")]
		public static extern int RailEventkRailEventAchievementPlayerAchievementReceived_get_event_id(IntPtr jarg1);

		// Token: 0x0600046C RID: 1132
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementPlayerAchievementReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAchievementPlayerAchievementReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x0600046D RID: 1133
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomLeaveRoomResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomLeaveRoomResult__SWIG_0();

		// Token: 0x0600046E RID: 1134
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomLeaveRoomResult")]
		public static extern void delete_RailEventkRailEventRoomLeaveRoomResult(IntPtr jarg1);

		// Token: 0x0600046F RID: 1135
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomLeaveRoomResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomLeaveRoomResult_kInternalRailEventEventId_get();

		// Token: 0x06000470 RID: 1136
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomLeaveRoomResult_get_event_id")]
		public static extern int RailEventkRailEventRoomLeaveRoomResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000471 RID: 1137
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomLeaveRoomResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomLeaveRoomResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000472 RID: 1138
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomCreated__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomCreated__SWIG_0();

		// Token: 0x06000473 RID: 1139
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomCreated")]
		public static extern void delete_RailEventkRailEventRoomCreated(IntPtr jarg1);

		// Token: 0x06000474 RID: 1140
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomCreated_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomCreated_kInternalRailEventEventId_get();

		// Token: 0x06000475 RID: 1141
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomCreated_get_event_id")]
		public static extern int RailEventkRailEventRoomCreated_get_event_id(IntPtr jarg1);

		// Token: 0x06000476 RID: 1142
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomCreated__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomCreated__SWIG_1(IntPtr jarg1);

		// Token: 0x06000477 RID: 1143
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageQueryQuotaResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageQueryQuotaResult__SWIG_0();

		// Token: 0x06000478 RID: 1144
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageQueryQuotaResult")]
		public static extern void delete_RailEventkRailEventStorageQueryQuotaResult(IntPtr jarg1);

		// Token: 0x06000479 RID: 1145
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageQueryQuotaResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageQueryQuotaResult_kInternalRailEventEventId_get();

		// Token: 0x0600047A RID: 1146
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageQueryQuotaResult_get_event_id")]
		public static extern int RailEventkRailEventStorageQueryQuotaResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600047B RID: 1147
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageQueryQuotaResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageQueryQuotaResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600047C RID: 1148
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcOwnershipChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcOwnershipChanged__SWIG_0();

		// Token: 0x0600047D RID: 1149
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcOwnershipChanged")]
		public static extern void delete_RailEventkRailEventDlcOwnershipChanged(IntPtr jarg1);

		// Token: 0x0600047E RID: 1150
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcOwnershipChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcOwnershipChanged_kInternalRailEventEventId_get();

		// Token: 0x0600047F RID: 1151
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcOwnershipChanged_get_event_id")]
		public static extern int RailEventkRailEventDlcOwnershipChanged_get_event_id(IntPtr jarg1);

		// Token: 0x06000480 RID: 1152
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcOwnershipChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcOwnershipChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x06000481 RID: 1153
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserNavigeteResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserNavigeteResult__SWIG_0();

		// Token: 0x06000482 RID: 1154
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserNavigeteResult")]
		public static extern void delete_RailEventkRailEventBrowserNavigeteResult(IntPtr jarg1);

		// Token: 0x06000483 RID: 1155
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserNavigeteResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserNavigeteResult_kInternalRailEventEventId_get();

		// Token: 0x06000484 RID: 1156
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserNavigeteResult_get_event_id")]
		public static extern int RailEventkRailEventBrowserNavigeteResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000485 RID: 1157
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserNavigeteResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserNavigeteResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000486 RID: 1158
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetInviteCommandLine__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsGetInviteCommandLine__SWIG_0();

		// Token: 0x06000487 RID: 1159
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsGetInviteCommandLine")]
		public static extern void delete_RailEventkRailEventFriendsGetInviteCommandLine(IntPtr jarg1);

		// Token: 0x06000488 RID: 1160
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetInviteCommandLine_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsGetInviteCommandLine_kInternalRailEventEventId_get();

		// Token: 0x06000489 RID: 1161
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetInviteCommandLine_get_event_id")]
		public static extern int RailEventkRailEventFriendsGetInviteCommandLine_get_event_id(IntPtr jarg1);

		// Token: 0x0600048A RID: 1162
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetInviteCommandLine__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsGetInviteCommandLine__SWIG_1(IntPtr jarg1);

		// Token: 0x0600048B RID: 1163
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent__SWIG_0();

		// Token: 0x0600048C RID: 1164
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent")]
		public static extern void delete_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent(IntPtr jarg1);

		// Token: 0x0600048D RID: 1165
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent_kInternalRailEventEventId_get();

		// Token: 0x0600048E RID: 1166
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent_get_event_id(IntPtr jarg1);

		// Token: 0x0600048F RID: 1167
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000490 RID: 1168
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsNumOfPlayerReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStatsNumOfPlayerReceived__SWIG_0();

		// Token: 0x06000491 RID: 1169
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStatsNumOfPlayerReceived")]
		public static extern void delete_RailEventkRailEventStatsNumOfPlayerReceived(IntPtr jarg1);

		// Token: 0x06000492 RID: 1170
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsNumOfPlayerReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStatsNumOfPlayerReceived_kInternalRailEventEventId_get();

		// Token: 0x06000493 RID: 1171
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsNumOfPlayerReceived_get_event_id")]
		public static extern int RailEventkRailEventStatsNumOfPlayerReceived_get_event_id(IntPtr jarg1);

		// Token: 0x06000494 RID: 1172
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsNumOfPlayerReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStatsNumOfPlayerReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000495 RID: 1173
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcInstallFinished__SWIG_0();

		// Token: 0x06000496 RID: 1174
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcInstallFinished")]
		public static extern void delete_RailEventkRailEventDlcInstallFinished(IntPtr jarg1);

		// Token: 0x06000497 RID: 1175
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcInstallFinished_kInternalRailEventEventId_get();

		// Token: 0x06000498 RID: 1176
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallFinished_get_event_id")]
		public static extern int RailEventkRailEventDlcInstallFinished_get_event_id(IntPtr jarg1);

		// Token: 0x06000499 RID: 1177
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcInstallFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcInstallFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600049A RID: 1178
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsGlobalStatsReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStatsGlobalStatsReceived__SWIG_0();

		// Token: 0x0600049B RID: 1179
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStatsGlobalStatsReceived")]
		public static extern void delete_RailEventkRailEventStatsGlobalStatsReceived(IntPtr jarg1);

		// Token: 0x0600049C RID: 1180
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsGlobalStatsReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStatsGlobalStatsReceived_kInternalRailEventEventId_get();

		// Token: 0x0600049D RID: 1181
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsGlobalStatsReceived_get_event_id")]
		public static extern int RailEventkRailEventStatsGlobalStatsReceived_get_event_id(IntPtr jarg1);

		// Token: 0x0600049E RID: 1182
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsGlobalStatsReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStatsGlobalStatsReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x0600049F RID: 1183
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSearchSpaceWorkResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceSearchSpaceWorkResult__SWIG_0();

		// Token: 0x060004A0 RID: 1184
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceSearchSpaceWorkResult")]
		public static extern void delete_RailEventkRailEventUserSpaceSearchSpaceWorkResult(IntPtr jarg1);

		// Token: 0x060004A1 RID: 1185
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSearchSpaceWorkResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceSearchSpaceWorkResult_kInternalRailEventEventId_get();

		// Token: 0x060004A2 RID: 1186
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSearchSpaceWorkResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceSearchSpaceWorkResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004A3 RID: 1187
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSearchSpaceWorkResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceSearchSpaceWorkResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004A4 RID: 1188
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelCreateResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelCreateResult__SWIG_0();

		// Token: 0x060004A5 RID: 1189
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelCreateResult")]
		public static extern void delete_RailEventkRailEventVoiceChannelCreateResult(IntPtr jarg1);

		// Token: 0x060004A6 RID: 1190
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelCreateResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelCreateResult_kInternalRailEventEventId_get();

		// Token: 0x060004A7 RID: 1191
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelCreateResult_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelCreateResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004A8 RID: 1192
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelCreateResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelCreateResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004A9 RID: 1193
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished__SWIG_0();

		// Token: 0x060004AA RID: 1194
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished")]
		public static extern void delete_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished(IntPtr jarg1);

		// Token: 0x060004AB RID: 1195
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_kInternalRailEventEventId_get();

		// Token: 0x060004AC RID: 1196
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_get_event_id(IntPtr jarg1);

		// Token: 0x060004AD RID: 1197
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x060004AE RID: 1198
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetMemberMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomGetMemberMetadataResult__SWIG_0();

		// Token: 0x060004AF RID: 1199
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGetMemberMetadataResult")]
		public static extern void delete_RailEventkRailEventRoomGetMemberMetadataResult(IntPtr jarg1);

		// Token: 0x060004B0 RID: 1200
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetMemberMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomGetMemberMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x060004B1 RID: 1201
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetMemberMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventRoomGetMemberMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004B2 RID: 1202
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetMemberMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomGetMemberMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004B3 RID: 1203
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsGetMetadataResult__SWIG_0();

		// Token: 0x060004B4 RID: 1204
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsGetMetadataResult")]
		public static extern void delete_RailEventkRailEventFriendsGetMetadataResult(IntPtr jarg1);

		// Token: 0x060004B5 RID: 1205
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsGetMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x060004B6 RID: 1206
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsGetMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004B7 RID: 1207
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsGetMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsGetMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004B8 RID: 1208
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncWriteFileResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncWriteFileResult__SWIG_0();

		// Token: 0x060004B9 RID: 1209
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncWriteFileResult")]
		public static extern void delete_RailEventkRailEventStorageAsyncWriteFileResult(IntPtr jarg1);

		// Token: 0x060004BA RID: 1210
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteFileResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageAsyncWriteFileResult_kInternalRailEventEventId_get();

		// Token: 0x060004BB RID: 1211
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteFileResult_get_event_id")]
		public static extern int RailEventkRailEventStorageAsyncWriteFileResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004BC RID: 1212
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncWriteFileResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncWriteFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004BD RID: 1213
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsPlayerStatsReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStatsPlayerStatsReceived__SWIG_0();

		// Token: 0x060004BE RID: 1214
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStatsPlayerStatsReceived")]
		public static extern void delete_RailEventkRailEventStatsPlayerStatsReceived(IntPtr jarg1);

		// Token: 0x060004BF RID: 1215
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStatsPlayerStatsReceived_kInternalRailEventEventId_get();

		// Token: 0x060004C0 RID: 1216
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsReceived_get_event_id")]
		public static extern int RailEventkRailEventStatsPlayerStatsReceived_get_event_id(IntPtr jarg1);

		// Token: 0x060004C1 RID: 1217
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsPlayerStatsReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStatsPlayerStatsReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x060004C2 RID: 1218
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsCompleteConsumeFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsCompleteConsumeFinished__SWIG_0();

		// Token: 0x060004C3 RID: 1219
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsCompleteConsumeFinished")]
		public static extern void delete_RailEventkRailEventAssetsCompleteConsumeFinished(IntPtr jarg1);

		// Token: 0x060004C4 RID: 1220
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsCompleteConsumeFinished_kInternalRailEventEventId_get();

		// Token: 0x060004C5 RID: 1221
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsCompleteConsumeFinished_get_event_id(IntPtr jarg1);

		// Token: 0x060004C6 RID: 1222
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsCompleteConsumeFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsCompleteConsumeFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x060004C7 RID: 1223
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerGetMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerGetMetadataResult__SWIG_0();

		// Token: 0x060004C8 RID: 1224
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerGetMetadataResult")]
		public static extern void delete_RailEventkRailEventGameServerGetMetadataResult(IntPtr jarg1);

		// Token: 0x060004C9 RID: 1225
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerGetMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x060004CA RID: 1226
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventGameServerGetMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004CB RID: 1227
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerGetMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerGetMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004CC RID: 1228
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventPlayerGetAuthenticateURL__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventPlayerGetAuthenticateURL__SWIG_0();

		// Token: 0x060004CD RID: 1229
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventPlayerGetAuthenticateURL")]
		public static extern void delete_RailEventkRailEventPlayerGetAuthenticateURL(IntPtr jarg1);

		// Token: 0x060004CE RID: 1230
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetAuthenticateURL_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventPlayerGetAuthenticateURL_kInternalRailEventEventId_get();

		// Token: 0x060004CF RID: 1231
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetAuthenticateURL_get_event_id")]
		public static extern int RailEventkRailEventPlayerGetAuthenticateURL_get_event_id(IntPtr jarg1);

		// Token: 0x060004D0 RID: 1232
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventPlayerGetAuthenticateURL__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventPlayerGetAuthenticateURL__SWIG_1(IntPtr jarg1);

		// Token: 0x060004D1 RID: 1233
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelLeaveResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelLeaveResult__SWIG_0();

		// Token: 0x060004D2 RID: 1234
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelLeaveResult")]
		public static extern void delete_RailEventkRailEventVoiceChannelLeaveResult(IntPtr jarg1);

		// Token: 0x060004D3 RID: 1235
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelLeaveResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelLeaveResult_kInternalRailEventEventId_get();

		// Token: 0x060004D4 RID: 1236
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelLeaveResult_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelLeaveResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004D5 RID: 1237
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelLeaveResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelLeaveResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004D6 RID: 1238
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementPlayerAchievementStored__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAchievementPlayerAchievementStored__SWIG_0();

		// Token: 0x060004D7 RID: 1239
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAchievementPlayerAchievementStored")]
		public static extern void delete_RailEventkRailEventAchievementPlayerAchievementStored(IntPtr jarg1);

		// Token: 0x060004D8 RID: 1240
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementStored_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAchievementPlayerAchievementStored_kInternalRailEventEventId_get();

		// Token: 0x060004D9 RID: 1241
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementStored_get_event_id")]
		public static extern int RailEventkRailEventAchievementPlayerAchievementStored_get_event_id(IntPtr jarg1);

		// Token: 0x060004DA RID: 1242
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAchievementPlayerAchievementStored__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAchievementPlayerAchievementStored__SWIG_1(IntPtr jarg1);

		// Token: 0x060004DB RID: 1243
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePayWindowClosed__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePayWindowClosed__SWIG_0();

		// Token: 0x060004DC RID: 1244
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventInGameStorePurchasePayWindowClosed")]
		public static extern void delete_RailEventkRailEventInGameStorePurchasePayWindowClosed(IntPtr jarg1);

		// Token: 0x060004DD RID: 1245
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowClosed_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventInGameStorePurchasePayWindowClosed_kInternalRailEventEventId_get();

		// Token: 0x060004DE RID: 1246
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowClosed_get_event_id")]
		public static extern int RailEventkRailEventInGameStorePurchasePayWindowClosed_get_event_id(IntPtr jarg1);

		// Token: 0x060004DF RID: 1247
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventInGameStorePurchasePayWindowClosed__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventInGameStorePurchasePayWindowClosed__SWIG_1(IntPtr jarg1);

		// Token: 0x060004E0 RID: 1248
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelInviteEvent__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelInviteEvent__SWIG_0();

		// Token: 0x060004E1 RID: 1249
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelInviteEvent")]
		public static extern void delete_RailEventkRailEventVoiceChannelInviteEvent(IntPtr jarg1);

		// Token: 0x060004E2 RID: 1250
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelInviteEvent_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelInviteEvent_kInternalRailEventEventId_get();

		// Token: 0x060004E3 RID: 1251
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelInviteEvent_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelInviteEvent_get_event_id(IntPtr jarg1);

		// Token: 0x060004E4 RID: 1252
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelInviteEvent__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelInviteEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x060004E5 RID: 1253
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelDataCaptured__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelDataCaptured__SWIG_0();

		// Token: 0x060004E6 RID: 1254
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelDataCaptured")]
		public static extern void delete_RailEventkRailEventVoiceChannelDataCaptured(IntPtr jarg1);

		// Token: 0x060004E7 RID: 1255
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelDataCaptured_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelDataCaptured_kInternalRailEventEventId_get();

		// Token: 0x060004E8 RID: 1256
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelDataCaptured_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelDataCaptured_get_event_id(IntPtr jarg1);

		// Token: 0x060004E9 RID: 1257
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelDataCaptured__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelDataCaptured__SWIG_1(IntPtr jarg1);

		// Token: 0x060004EA RID: 1258
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged__SWIG_0();

		// Token: 0x060004EB RID: 1259
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged")]
		public static extern void delete_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged(IntPtr jarg1);

		// Token: 0x060004EC RID: 1260
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged_kInternalRailEventEventId_get();

		// Token: 0x060004ED RID: 1261
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged_get_event_id")]
		public static extern int RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged_get_event_id(IntPtr jarg1);

		// Token: 0x060004EE RID: 1262
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x060004EF RID: 1263
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncReadStreamFileResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncReadStreamFileResult__SWIG_0();

		// Token: 0x060004F0 RID: 1264
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncReadStreamFileResult")]
		public static extern void delete_RailEventkRailEventStorageAsyncReadStreamFileResult(IntPtr jarg1);

		// Token: 0x060004F1 RID: 1265
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadStreamFileResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageAsyncReadStreamFileResult_kInternalRailEventEventId_get();

		// Token: 0x060004F2 RID: 1266
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadStreamFileResult_get_event_id")]
		public static extern int RailEventkRailEventStorageAsyncReadStreamFileResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004F3 RID: 1267
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncReadStreamFileResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncReadStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004F4 RID: 1268
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserTitleChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserTitleChanged__SWIG_0();

		// Token: 0x060004F5 RID: 1269
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserTitleChanged")]
		public static extern void delete_RailEventkRailEventBrowserTitleChanged(IntPtr jarg1);

		// Token: 0x060004F6 RID: 1270
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTitleChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserTitleChanged_kInternalRailEventEventId_get();

		// Token: 0x060004F7 RID: 1271
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTitleChanged_get_event_id")]
		public static extern int RailEventkRailEventBrowserTitleChanged_get_event_id(IntPtr jarg1);

		// Token: 0x060004F8 RID: 1272
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserTitleChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserTitleChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x060004F9 RID: 1273
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersShowChatWindowWithFriendResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersShowChatWindowWithFriendResult__SWIG_0();

		// Token: 0x060004FA RID: 1274
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersShowChatWindowWithFriendResult")]
		public static extern void delete_RailEventkRailEventUsersShowChatWindowWithFriendResult(IntPtr jarg1);

		// Token: 0x060004FB RID: 1275
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersShowChatWindowWithFriendResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersShowChatWindowWithFriendResult_kInternalRailEventEventId_get();

		// Token: 0x060004FC RID: 1276
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersShowChatWindowWithFriendResult_get_event_id")]
		public static extern int RailEventkRailEventUsersShowChatWindowWithFriendResult_get_event_id(IntPtr jarg1);

		// Token: 0x060004FD RID: 1277
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersShowChatWindowWithFriendResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersShowChatWindowWithFriendResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060004FE RID: 1278
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceGetMySubscribedWorksResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceGetMySubscribedWorksResult__SWIG_0();

		// Token: 0x060004FF RID: 1279
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceGetMySubscribedWorksResult")]
		public static extern void delete_RailEventkRailEventUserSpaceGetMySubscribedWorksResult(IntPtr jarg1);

		// Token: 0x06000500 RID: 1280
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMySubscribedWorksResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceGetMySubscribedWorksResult_kInternalRailEventEventId_get();

		// Token: 0x06000501 RID: 1281
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMySubscribedWorksResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceGetMySubscribedWorksResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000502 RID: 1282
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceGetMySubscribedWorksResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceGetMySubscribedWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000503 RID: 1283
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageShareToSpaceWorkResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageShareToSpaceWorkResult__SWIG_0();

		// Token: 0x06000504 RID: 1284
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageShareToSpaceWorkResult")]
		public static extern void delete_RailEventkRailEventStorageShareToSpaceWorkResult(IntPtr jarg1);

		// Token: 0x06000505 RID: 1285
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageShareToSpaceWorkResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageShareToSpaceWorkResult_kInternalRailEventEventId_get();

		// Token: 0x06000506 RID: 1286
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageShareToSpaceWorkResult_get_event_id")]
		public static extern int RailEventkRailEventStorageShareToSpaceWorkResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000507 RID: 1287
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageShareToSpaceWorkResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageShareToSpaceWorkResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000508 RID: 1288
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent__SWIG_0();

		// Token: 0x06000509 RID: 1289
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent")]
		public static extern void delete_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent(IntPtr jarg1);

		// Token: 0x0600050A RID: 1290
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent_kInternalRailEventEventId_get();

		// Token: 0x0600050B RID: 1291
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent_get_event_id(IntPtr jarg1);

		// Token: 0x0600050C RID: 1292
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x0600050D RID: 1293
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUtilsGetImageDataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUtilsGetImageDataResult__SWIG_0();

		// Token: 0x0600050E RID: 1294
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUtilsGetImageDataResult")]
		public static extern void delete_RailEventkRailEventUtilsGetImageDataResult(IntPtr jarg1);

		// Token: 0x0600050F RID: 1295
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUtilsGetImageDataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUtilsGetImageDataResult_kInternalRailEventEventId_get();

		// Token: 0x06000510 RID: 1296
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUtilsGetImageDataResult_get_event_id")]
		public static extern int RailEventkRailEventUtilsGetImageDataResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000511 RID: 1297
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUtilsGetImageDataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUtilsGetImageDataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000512 RID: 1298
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetUserLimitsResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersGetUserLimitsResult__SWIG_0();

		// Token: 0x06000513 RID: 1299
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersGetUserLimitsResult")]
		public static extern void delete_RailEventkRailEventUsersGetUserLimitsResult(IntPtr jarg1);

		// Token: 0x06000514 RID: 1300
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUserLimitsResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersGetUserLimitsResult_kInternalRailEventEventId_get();

		// Token: 0x06000515 RID: 1301
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUserLimitsResult_get_event_id")]
		public static extern int RailEventkRailEventUsersGetUserLimitsResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000516 RID: 1302
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetUserLimitsResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersGetUserLimitsResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000517 RID: 1303
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSystemStateChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventSystemStateChanged__SWIG_0();

		// Token: 0x06000518 RID: 1304
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventSystemStateChanged")]
		public static extern void delete_RailEventkRailEventSystemStateChanged(IntPtr jarg1);

		// Token: 0x06000519 RID: 1305
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSystemStateChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventSystemStateChanged_kInternalRailEventEventId_get();

		// Token: 0x0600051A RID: 1306
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSystemStateChanged_get_event_id")]
		public static extern int RailEventkRailEventSystemStateChanged_get_event_id(IntPtr jarg1);

		// Token: 0x0600051B RID: 1307
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSystemStateChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventSystemStateChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x0600051C RID: 1308
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardReceived__SWIG_0();

		// Token: 0x0600051D RID: 1309
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardReceived")]
		public static extern void delete_RailEventkRailEventLeaderboardReceived(IntPtr jarg1);

		// Token: 0x0600051E RID: 1310
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventLeaderboardReceived_kInternalRailEventEventId_get();

		// Token: 0x0600051F RID: 1311
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardReceived_get_event_id")]
		public static extern int RailEventkRailEventLeaderboardReceived_get_event_id(IntPtr jarg1);

		// Token: 0x06000520 RID: 1312
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000521 RID: 1313
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventHttpSessionResponseResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventHttpSessionResponseResult__SWIG_0();

		// Token: 0x06000522 RID: 1314
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventHttpSessionResponseResult")]
		public static extern void delete_RailEventkRailEventHttpSessionResponseResult(IntPtr jarg1);

		// Token: 0x06000523 RID: 1315
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventHttpSessionResponseResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventHttpSessionResponseResult_kInternalRailEventEventId_get();

		// Token: 0x06000524 RID: 1316
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventHttpSessionResponseResult_get_event_id")]
		public static extern int RailEventkRailEventHttpSessionResponseResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000525 RID: 1317
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventHttpSessionResponseResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventHttpSessionResponseResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000526 RID: 1318
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomClearRoomMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomClearRoomMetadataResult__SWIG_0();

		// Token: 0x06000527 RID: 1319
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomClearRoomMetadataResult")]
		public static extern void delete_RailEventkRailEventRoomClearRoomMetadataResult(IntPtr jarg1);

		// Token: 0x06000528 RID: 1320
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomClearRoomMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomClearRoomMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x06000529 RID: 1321
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomClearRoomMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventRoomClearRoomMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600052A RID: 1322
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomClearRoomMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomClearRoomMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600052B RID: 1323
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSmallObjectServiceDownloadResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventSmallObjectServiceDownloadResult__SWIG_0();

		// Token: 0x0600052C RID: 1324
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventSmallObjectServiceDownloadResult")]
		public static extern void delete_RailEventkRailEventSmallObjectServiceDownloadResult(IntPtr jarg1);

		// Token: 0x0600052D RID: 1325
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSmallObjectServiceDownloadResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventSmallObjectServiceDownloadResult_kInternalRailEventEventId_get();

		// Token: 0x0600052E RID: 1326
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSmallObjectServiceDownloadResult_get_event_id")]
		public static extern int RailEventkRailEventSmallObjectServiceDownloadResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600052F RID: 1327
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSmallObjectServiceDownloadResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventSmallObjectServiceDownloadResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000530 RID: 1328
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetAllDataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomGetAllDataResult__SWIG_0();

		// Token: 0x06000531 RID: 1329
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGetAllDataResult")]
		public static extern void delete_RailEventkRailEventRoomGetAllDataResult(IntPtr jarg1);

		// Token: 0x06000532 RID: 1330
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetAllDataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomGetAllDataResult_kInternalRailEventEventId_get();

		// Token: 0x06000533 RID: 1331
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetAllDataResult_get_event_id")]
		public static extern int RailEventkRailEventRoomGetAllDataResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000534 RID: 1332
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGetAllDataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomGetAllDataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000535 RID: 1333
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncListStreamFileResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncListStreamFileResult__SWIG_0();

		// Token: 0x06000536 RID: 1334
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncListStreamFileResult")]
		public static extern void delete_RailEventkRailEventStorageAsyncListStreamFileResult(IntPtr jarg1);

		// Token: 0x06000537 RID: 1335
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncListStreamFileResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageAsyncListStreamFileResult_kInternalRailEventEventId_get();

		// Token: 0x06000538 RID: 1336
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncListStreamFileResult_get_event_id")]
		public static extern int RailEventkRailEventStorageAsyncListStreamFileResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000539 RID: 1337
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncListStreamFileResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncListStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600053A RID: 1338
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsFriendsListChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsFriendsListChanged__SWIG_0();

		// Token: 0x0600053B RID: 1339
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsFriendsListChanged")]
		public static extern void delete_RailEventkRailEventFriendsFriendsListChanged(IntPtr jarg1);

		// Token: 0x0600053C RID: 1340
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsFriendsListChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsFriendsListChanged_kInternalRailEventEventId_get();

		// Token: 0x0600053D RID: 1341
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsFriendsListChanged_get_event_id")]
		public static extern int RailEventkRailEventFriendsFriendsListChanged_get_event_id(IntPtr jarg1);

		// Token: 0x0600053E RID: 1342
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsFriendsListChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsFriendsListChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x0600053F RID: 1343
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardEntryReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardEntryReceived__SWIG_0();

		// Token: 0x06000540 RID: 1344
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventLeaderboardEntryReceived")]
		public static extern void delete_RailEventkRailEventLeaderboardEntryReceived(IntPtr jarg1);

		// Token: 0x06000541 RID: 1345
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardEntryReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventLeaderboardEntryReceived_kInternalRailEventEventId_get();

		// Token: 0x06000542 RID: 1346
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardEntryReceived_get_event_id")]
		public static extern int RailEventkRailEventLeaderboardEntryReceived_get_event_id(IntPtr jarg1);

		// Token: 0x06000543 RID: 1347
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventLeaderboardEntryReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventLeaderboardEntryReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000544 RID: 1348
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomGameServerChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomGameServerChanged__SWIG_0();

		// Token: 0x06000545 RID: 1349
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyRoomGameServerChanged")]
		public static extern void delete_RailEventkRailEventRoomNotifyRoomGameServerChanged(IntPtr jarg1);

		// Token: 0x06000546 RID: 1350
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomGameServerChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomNotifyRoomGameServerChanged_kInternalRailEventEventId_get();

		// Token: 0x06000547 RID: 1351
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomGameServerChanged_get_event_id")]
		public static extern int RailEventkRailEventRoomNotifyRoomGameServerChanged_get_event_id(IntPtr jarg1);

		// Token: 0x06000548 RID: 1352
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomGameServerChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomGameServerChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x06000549 RID: 1353
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomDataReceived__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomDataReceived__SWIG_0();

		// Token: 0x0600054A RID: 1354
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomNotifyRoomDataReceived")]
		public static extern void delete_RailEventkRailEventRoomNotifyRoomDataReceived(IntPtr jarg1);

		// Token: 0x0600054B RID: 1355
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDataReceived_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomNotifyRoomDataReceived_kInternalRailEventEventId_get();

		// Token: 0x0600054C RID: 1356
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDataReceived_get_event_id")]
		public static extern int RailEventkRailEventRoomNotifyRoomDataReceived_get_event_id(IntPtr jarg1);

		// Token: 0x0600054D RID: 1357
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomNotifyRoomDataReceived__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomNotifyRoomDataReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x0600054E RID: 1358
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcQueryIsOwnedDlcsResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcQueryIsOwnedDlcsResult__SWIG_0();

		// Token: 0x0600054F RID: 1359
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcQueryIsOwnedDlcsResult")]
		public static extern void delete_RailEventkRailEventDlcQueryIsOwnedDlcsResult(IntPtr jarg1);

		// Token: 0x06000550 RID: 1360
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcQueryIsOwnedDlcsResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcQueryIsOwnedDlcsResult_kInternalRailEventEventId_get();

		// Token: 0x06000551 RID: 1361
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcQueryIsOwnedDlcsResult_get_event_id")]
		public static extern int RailEventkRailEventDlcQueryIsOwnedDlcsResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000552 RID: 1362
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcQueryIsOwnedDlcsResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcQueryIsOwnedDlcsResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000553 RID: 1363
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSyncResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceSyncResult__SWIG_0();

		// Token: 0x06000554 RID: 1364
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceSyncResult")]
		public static extern void delete_RailEventkRailEventUserSpaceSyncResult(IntPtr jarg1);

		// Token: 0x06000555 RID: 1365
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSyncResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceSyncResult_kInternalRailEventEventId_get();

		// Token: 0x06000556 RID: 1366
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSyncResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceSyncResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000557 RID: 1367
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceSyncResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceSyncResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000558 RID: 1368
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcRefundChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventDlcRefundChanged__SWIG_0();

		// Token: 0x06000559 RID: 1369
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventDlcRefundChanged")]
		public static extern void delete_RailEventkRailEventDlcRefundChanged(IntPtr jarg1);

		// Token: 0x0600055A RID: 1370
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcRefundChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventDlcRefundChanged_kInternalRailEventEventId_get();

		// Token: 0x0600055B RID: 1371
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcRefundChanged_get_event_id")]
		public static extern int RailEventkRailEventDlcRefundChanged_get_event_id(IntPtr jarg1);

		// Token: 0x0600055C RID: 1372
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventDlcRefundChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventDlcRefundChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x0600055D RID: 1373
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerPlayerListResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerPlayerListResult__SWIG_0();

		// Token: 0x0600055E RID: 1374
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerPlayerListResult")]
		public static extern void delete_RailEventkRailEventGameServerPlayerListResult(IntPtr jarg1);

		// Token: 0x0600055F RID: 1375
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerPlayerListResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerPlayerListResult_kInternalRailEventEventId_get();

		// Token: 0x06000560 RID: 1376
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerPlayerListResult_get_event_id")]
		public static extern int RailEventkRailEventGameServerPlayerListResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000561 RID: 1377
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerPlayerListResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerPlayerListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000562 RID: 1378
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetUsersInfo__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersGetUsersInfo__SWIG_0();

		// Token: 0x06000563 RID: 1379
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersGetUsersInfo")]
		public static extern void delete_RailEventkRailEventUsersGetUsersInfo(IntPtr jarg1);

		// Token: 0x06000564 RID: 1380
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUsersInfo_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersGetUsersInfo_kInternalRailEventEventId_get();

		// Token: 0x06000565 RID: 1381
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUsersInfo_get_event_id")]
		public static extern int RailEventkRailEventUsersGetUsersInfo_get_event_id(IntPtr jarg1);

		// Token: 0x06000566 RID: 1382
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersGetUsersInfo__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersGetUsersInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000567 RID: 1383
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncDeleteStreamFileResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncDeleteStreamFileResult__SWIG_0();

		// Token: 0x06000568 RID: 1384
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncDeleteStreamFileResult")]
		public static extern void delete_RailEventkRailEventStorageAsyncDeleteStreamFileResult(IntPtr jarg1);

		// Token: 0x06000569 RID: 1385
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncDeleteStreamFileResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageAsyncDeleteStreamFileResult_kInternalRailEventEventId_get();

		// Token: 0x0600056A RID: 1386
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncDeleteStreamFileResult_get_event_id")]
		public static extern int RailEventkRailEventStorageAsyncDeleteStreamFileResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600056B RID: 1387
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncDeleteStreamFileResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncDeleteStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600056C RID: 1388
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsSetMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventFriendsSetMetadataResult__SWIG_0();

		// Token: 0x0600056D RID: 1389
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventFriendsSetMetadataResult")]
		public static extern void delete_RailEventkRailEventFriendsSetMetadataResult(IntPtr jarg1);

		// Token: 0x0600056E RID: 1390
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsSetMetadataResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventFriendsSetMetadataResult_kInternalRailEventEventId_get();

		// Token: 0x0600056F RID: 1391
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsSetMetadataResult_get_event_id")]
		public static extern int RailEventkRailEventFriendsSetMetadataResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000570 RID: 1392
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventFriendsSetMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventFriendsSetMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000571 RID: 1393
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserCreateResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserCreateResult__SWIG_0();

		// Token: 0x06000572 RID: 1394
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserCreateResult")]
		public static extern void delete_RailEventkRailEventBrowserCreateResult(IntPtr jarg1);

		// Token: 0x06000573 RID: 1395
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCreateResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserCreateResult_kInternalRailEventEventId_get();

		// Token: 0x06000574 RID: 1396
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCreateResult_get_event_id")]
		public static extern int RailEventkRailEventBrowserCreateResult_get_event_id(IntPtr jarg1);

		// Token: 0x06000575 RID: 1397
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserCreateResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserCreateResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000576 RID: 1398
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsSplitToFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAssetsSplitToFinished__SWIG_0();

		// Token: 0x06000577 RID: 1399
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAssetsSplitToFinished")]
		public static extern void delete_RailEventkRailEventAssetsSplitToFinished(IntPtr jarg1);

		// Token: 0x06000578 RID: 1400
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitToFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAssetsSplitToFinished_kInternalRailEventEventId_get();

		// Token: 0x06000579 RID: 1401
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitToFinished_get_event_id")]
		public static extern int RailEventkRailEventAssetsSplitToFinished_get_event_id(IntPtr jarg1);

		// Token: 0x0600057A RID: 1402
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAssetsSplitToFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAssetsSplitToFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600057B RID: 1403
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotPublishScreenshotFinished__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventScreenshotPublishScreenshotFinished__SWIG_0();

		// Token: 0x0600057C RID: 1404
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventScreenshotPublishScreenshotFinished")]
		public static extern void delete_RailEventkRailEventScreenshotPublishScreenshotFinished(IntPtr jarg1);

		// Token: 0x0600057D RID: 1405
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotPublishScreenshotFinished_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventScreenshotPublishScreenshotFinished_kInternalRailEventEventId_get();

		// Token: 0x0600057E RID: 1406
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotPublishScreenshotFinished_get_event_id")]
		public static extern int RailEventkRailEventScreenshotPublishScreenshotFinished_get_event_id(IntPtr jarg1);

		// Token: 0x0600057F RID: 1407
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotPublishScreenshotFinished__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventScreenshotPublishScreenshotFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000580 RID: 1408
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerAddFavoriteGameServer__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerAddFavoriteGameServer__SWIG_0();

		// Token: 0x06000581 RID: 1409
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerAddFavoriteGameServer")]
		public static extern void delete_RailEventkRailEventGameServerAddFavoriteGameServer(IntPtr jarg1);

		// Token: 0x06000582 RID: 1410
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAddFavoriteGameServer_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerAddFavoriteGameServer_kInternalRailEventEventId_get();

		// Token: 0x06000583 RID: 1411
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAddFavoriteGameServer_get_event_id")]
		public static extern int RailEventkRailEventGameServerAddFavoriteGameServer_get_event_id(IntPtr jarg1);

		// Token: 0x06000584 RID: 1412
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerAddFavoriteGameServer__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerAddFavoriteGameServer__SWIG_1(IntPtr jarg1);

		// Token: 0x06000585 RID: 1413
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSessionTicketGetSessionTicket__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventSessionTicketGetSessionTicket__SWIG_0();

		// Token: 0x06000586 RID: 1414
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventSessionTicketGetSessionTicket")]
		public static extern void delete_RailEventkRailEventSessionTicketGetSessionTicket(IntPtr jarg1);

		// Token: 0x06000587 RID: 1415
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketGetSessionTicket_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventSessionTicketGetSessionTicket_kInternalRailEventEventId_get();

		// Token: 0x06000588 RID: 1416
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketGetSessionTicket_get_event_id")]
		public static extern int RailEventkRailEventSessionTicketGetSessionTicket_get_event_id(IntPtr jarg1);

		// Token: 0x06000589 RID: 1417
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSessionTicketGetSessionTicket__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventSessionTicketGetSessionTicket__SWIG_1(IntPtr jarg1);

		// Token: 0x0600058A RID: 1418
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerRemoveFavoriteGameServer__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerRemoveFavoriteGameServer__SWIG_0();

		// Token: 0x0600058B RID: 1419
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerRemoveFavoriteGameServer")]
		public static extern void delete_RailEventkRailEventGameServerRemoveFavoriteGameServer(IntPtr jarg1);

		// Token: 0x0600058C RID: 1420
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRemoveFavoriteGameServer_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerRemoveFavoriteGameServer_kInternalRailEventEventId_get();

		// Token: 0x0600058D RID: 1421
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRemoveFavoriteGameServer_get_event_id")]
		public static extern int RailEventkRailEventGameServerRemoveFavoriteGameServer_get_event_id(IntPtr jarg1);

		// Token: 0x0600058E RID: 1422
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerRemoveFavoriteGameServer__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerRemoveFavoriteGameServer__SWIG_1(IntPtr jarg1);

		// Token: 0x0600058F RID: 1423
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGotRoomMembers__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventRoomGotRoomMembers__SWIG_0();

		// Token: 0x06000590 RID: 1424
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventRoomGotRoomMembers")]
		public static extern void delete_RailEventkRailEventRoomGotRoomMembers(IntPtr jarg1);

		// Token: 0x06000591 RID: 1425
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGotRoomMembers_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventRoomGotRoomMembers_kInternalRailEventEventId_get();

		// Token: 0x06000592 RID: 1426
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGotRoomMembers_get_event_id")]
		public static extern int RailEventkRailEventRoomGotRoomMembers_get_event_id(IntPtr jarg1);

		// Token: 0x06000593 RID: 1427
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventRoomGotRoomMembers__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventRoomGotRoomMembers__SWIG_1(IntPtr jarg1);

		// Token: 0x06000594 RID: 1428
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsPlayerStatsStored__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStatsPlayerStatsStored__SWIG_0();

		// Token: 0x06000595 RID: 1429
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStatsPlayerStatsStored")]
		public static extern void delete_RailEventkRailEventStatsPlayerStatsStored(IntPtr jarg1);

		// Token: 0x06000596 RID: 1430
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsStored_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStatsPlayerStatsStored_kInternalRailEventEventId_get();

		// Token: 0x06000597 RID: 1431
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsStored_get_event_id")]
		public static extern int RailEventkRailEventStatsPlayerStatsStored_get_event_id(IntPtr jarg1);

		// Token: 0x06000598 RID: 1432
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStatsPlayerStatsStored__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStatsPlayerStatsStored__SWIG_1(IntPtr jarg1);

		// Token: 0x06000599 RID: 1433
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncReadFileResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncReadFileResult__SWIG_0();

		// Token: 0x0600059A RID: 1434
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventStorageAsyncReadFileResult")]
		public static extern void delete_RailEventkRailEventStorageAsyncReadFileResult(IntPtr jarg1);

		// Token: 0x0600059B RID: 1435
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadFileResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventStorageAsyncReadFileResult_kInternalRailEventEventId_get();

		// Token: 0x0600059C RID: 1436
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadFileResult_get_event_id")]
		public static extern int RailEventkRailEventStorageAsyncReadFileResult_get_event_id(IntPtr jarg1);

		// Token: 0x0600059D RID: 1437
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventStorageAsyncReadFileResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventStorageAsyncReadFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600059E RID: 1438
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSessionTicketAuthSessionTicket__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventSessionTicketAuthSessionTicket__SWIG_0();

		// Token: 0x0600059F RID: 1439
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventSessionTicketAuthSessionTicket")]
		public static extern void delete_RailEventkRailEventSessionTicketAuthSessionTicket(IntPtr jarg1);

		// Token: 0x060005A0 RID: 1440
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketAuthSessionTicket_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventSessionTicketAuthSessionTicket_kInternalRailEventEventId_get();

		// Token: 0x060005A1 RID: 1441
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketAuthSessionTicket_get_event_id")]
		public static extern int RailEventkRailEventSessionTicketAuthSessionTicket_get_event_id(IntPtr jarg1);

		// Token: 0x060005A2 RID: 1442
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventSessionTicketAuthSessionTicket__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventSessionTicketAuthSessionTicket__SWIG_1(IntPtr jarg1);

		// Token: 0x060005A3 RID: 1443
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotTakeScreenshotRequest__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventScreenshotTakeScreenshotRequest__SWIG_0();

		// Token: 0x060005A4 RID: 1444
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventScreenshotTakeScreenshotRequest")]
		public static extern void delete_RailEventkRailEventScreenshotTakeScreenshotRequest(IntPtr jarg1);

		// Token: 0x060005A5 RID: 1445
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotRequest_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventScreenshotTakeScreenshotRequest_kInternalRailEventEventId_get();

		// Token: 0x060005A6 RID: 1446
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotRequest_get_event_id")]
		public static extern int RailEventkRailEventScreenshotTakeScreenshotRequest_get_event_id(IntPtr jarg1);

		// Token: 0x060005A7 RID: 1447
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventScreenshotTakeScreenshotRequest__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventScreenshotTakeScreenshotRequest__SWIG_1(IntPtr jarg1);

		// Token: 0x060005A8 RID: 1448
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceRemoveSpaceWorkResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceRemoveSpaceWorkResult__SWIG_0();

		// Token: 0x060005A9 RID: 1449
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUserSpaceRemoveSpaceWorkResult")]
		public static extern void delete_RailEventkRailEventUserSpaceRemoveSpaceWorkResult(IntPtr jarg1);

		// Token: 0x060005AA RID: 1450
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceRemoveSpaceWorkResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUserSpaceRemoveSpaceWorkResult_kInternalRailEventEventId_get();

		// Token: 0x060005AB RID: 1451
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceRemoveSpaceWorkResult_get_event_id")]
		public static extern int RailEventkRailEventUserSpaceRemoveSpaceWorkResult_get_event_id(IntPtr jarg1);

		// Token: 0x060005AC RID: 1452
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUserSpaceRemoveSpaceWorkResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUserSpaceRemoveSpaceWorkResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060005AD RID: 1453
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserStateChanged__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventBrowserStateChanged__SWIG_0();

		// Token: 0x060005AE RID: 1454
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventBrowserStateChanged")]
		public static extern void delete_RailEventkRailEventBrowserStateChanged(IntPtr jarg1);

		// Token: 0x060005AF RID: 1455
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserStateChanged_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventBrowserStateChanged_kInternalRailEventEventId_get();

		// Token: 0x060005B0 RID: 1456
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserStateChanged_get_event_id")]
		public static extern int RailEventkRailEventBrowserStateChanged_get_event_id(IntPtr jarg1);

		// Token: 0x060005B1 RID: 1457
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventBrowserStateChanged__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventBrowserStateChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x060005B2 RID: 1458
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerGetSessionTicket__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventGameServerGetSessionTicket__SWIG_0();

		// Token: 0x060005B3 RID: 1459
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventGameServerGetSessionTicket")]
		public static extern void delete_RailEventkRailEventGameServerGetSessionTicket(IntPtr jarg1);

		// Token: 0x060005B4 RID: 1460
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetSessionTicket_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventGameServerGetSessionTicket_kInternalRailEventEventId_get();

		// Token: 0x060005B5 RID: 1461
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetSessionTicket_get_event_id")]
		public static extern int RailEventkRailEventGameServerGetSessionTicket_get_event_id(IntPtr jarg1);

		// Token: 0x060005B6 RID: 1462
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventGameServerGetSessionTicket__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventGameServerGetSessionTicket__SWIG_1(IntPtr jarg1);

		// Token: 0x060005B7 RID: 1463
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersInviteJoinGameResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventUsersInviteJoinGameResult__SWIG_0();

		// Token: 0x060005B8 RID: 1464
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventUsersInviteJoinGameResult")]
		public static extern void delete_RailEventkRailEventUsersInviteJoinGameResult(IntPtr jarg1);

		// Token: 0x060005B9 RID: 1465
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteJoinGameResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventUsersInviteJoinGameResult_kInternalRailEventEventId_get();

		// Token: 0x060005BA RID: 1466
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteJoinGameResult_get_event_id")]
		public static extern int RailEventkRailEventUsersInviteJoinGameResult_get_event_id(IntPtr jarg1);

		// Token: 0x060005BB RID: 1467
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventUsersInviteJoinGameResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventUsersInviteJoinGameResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060005BC RID: 1468
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelAddUsersResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelAddUsersResult__SWIG_0();

		// Token: 0x060005BD RID: 1469
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventVoiceChannelAddUsersResult")]
		public static extern void delete_RailEventkRailEventVoiceChannelAddUsersResult(IntPtr jarg1);

		// Token: 0x060005BE RID: 1470
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelAddUsersResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventVoiceChannelAddUsersResult_kInternalRailEventEventId_get();

		// Token: 0x060005BF RID: 1471
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelAddUsersResult_get_event_id")]
		public static extern int RailEventkRailEventVoiceChannelAddUsersResult_get_event_id(IntPtr jarg1);

		// Token: 0x060005C0 RID: 1472
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventVoiceChannelAddUsersResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventVoiceChannelAddUsersResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060005C1 RID: 1473
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAppQuerySubscribeWishPlayStateResult__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventAppQuerySubscribeWishPlayStateResult__SWIG_0();

		// Token: 0x060005C2 RID: 1474
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventAppQuerySubscribeWishPlayStateResult")]
		public static extern void delete_RailEventkRailEventAppQuerySubscribeWishPlayStateResult(IntPtr jarg1);

		// Token: 0x060005C3 RID: 1475
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAppQuerySubscribeWishPlayStateResult_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventAppQuerySubscribeWishPlayStateResult_kInternalRailEventEventId_get();

		// Token: 0x060005C4 RID: 1476
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAppQuerySubscribeWishPlayStateResult_get_event_id")]
		public static extern int RailEventkRailEventAppQuerySubscribeWishPlayStateResult_get_event_id(IntPtr jarg1);

		// Token: 0x060005C5 RID: 1477
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventAppQuerySubscribeWishPlayStateResult__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventAppQuerySubscribeWishPlayStateResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060005C6 RID: 1478
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetworkCreateSessionFailed__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventNetworkCreateSessionFailed__SWIG_0();

		// Token: 0x060005C7 RID: 1479
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventNetworkCreateSessionFailed")]
		public static extern void delete_RailEventkRailEventNetworkCreateSessionFailed(IntPtr jarg1);

		// Token: 0x060005C8 RID: 1480
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionFailed_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventNetworkCreateSessionFailed_kInternalRailEventEventId_get();

		// Token: 0x060005C9 RID: 1481
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionFailed_get_event_id")]
		public static extern int RailEventkRailEventNetworkCreateSessionFailed_get_event_id(IntPtr jarg1);

		// Token: 0x060005CA RID: 1482
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventNetworkCreateSessionFailed__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventNetworkCreateSessionFailed__SWIG_1(IntPtr jarg1);

		// Token: 0x060005CB RID: 1483
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventQueryPlayerBannedStatus__SWIG_0")]
		public static extern IntPtr new_RailEventkRailEventQueryPlayerBannedStatus__SWIG_0();

		// Token: 0x060005CC RID: 1484
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailEventQueryPlayerBannedStatus")]
		public static extern void delete_RailEventkRailEventQueryPlayerBannedStatus(IntPtr jarg1);

		// Token: 0x060005CD RID: 1485
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventQueryPlayerBannedStatus_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailEventQueryPlayerBannedStatus_kInternalRailEventEventId_get();

		// Token: 0x060005CE RID: 1486
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventQueryPlayerBannedStatus_get_event_id")]
		public static extern int RailEventkRailEventQueryPlayerBannedStatus_get_event_id(IntPtr jarg1);

		// Token: 0x060005CF RID: 1487
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailEventQueryPlayerBannedStatus__SWIG_1")]
		public static extern IntPtr new_RailEventkRailEventQueryPlayerBannedStatus__SWIG_1(IntPtr jarg1);

		// Token: 0x060005D0 RID: 1488
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByGameServer__SWIG_0")]
		public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByGameServer__SWIG_0();

		// Token: 0x060005D1 RID: 1489
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailEventkRailPlatformNotifyEventJoinGameByGameServer")]
		public static extern void delete_RailEventkRailPlatformNotifyEventJoinGameByGameServer(IntPtr jarg1);

		// Token: 0x060005D2 RID: 1490
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByGameServer_kInternalRailEventEventId_get")]
		public static extern int RailEventkRailPlatformNotifyEventJoinGameByGameServer_kInternalRailEventEventId_get();

		// Token: 0x060005D3 RID: 1491
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByGameServer_get_event_id")]
		public static extern int RailEventkRailPlatformNotifyEventJoinGameByGameServer_get_event_id(IntPtr jarg1);

		// Token: 0x060005D4 RID: 1492
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailEventkRailPlatformNotifyEventJoinGameByGameServer__SWIG_1")]
		public static extern IntPtr new_RailEventkRailPlatformNotifyEventJoinGameByGameServer__SWIG_1(IntPtr jarg1);

		// Token: 0x060005D5 RID: 1493
		[DllImport("rail_api", EntryPoint = "CSharp_new_SpaceWorkID__SWIG_0")]
		public static extern IntPtr new_SpaceWorkID__SWIG_0();

		// Token: 0x060005D6 RID: 1494
		[DllImport("rail_api", EntryPoint = "CSharp_new_SpaceWorkID__SWIG_1")]
		public static extern IntPtr new_SpaceWorkID__SWIG_1(ulong jarg1);

		// Token: 0x060005D7 RID: 1495
		[DllImport("rail_api", EntryPoint = "CSharp_SpaceWorkID_set_id")]
		public static extern void SpaceWorkID_set_id(IntPtr jarg1, ulong jarg2);

		// Token: 0x060005D8 RID: 1496
		[DllImport("rail_api", EntryPoint = "CSharp_SpaceWorkID_get_id")]
		public static extern ulong SpaceWorkID_get_id(IntPtr jarg1);

		// Token: 0x060005D9 RID: 1497
		[DllImport("rail_api", EntryPoint = "CSharp_SpaceWorkID_IsValid")]
		public static extern bool SpaceWorkID_IsValid(IntPtr jarg1);

		// Token: 0x060005DA RID: 1498
		[DllImport("rail_api", EntryPoint = "CSharp_new_SpaceWorkID__SWIG_2")]
		public static extern IntPtr new_SpaceWorkID__SWIG_2(IntPtr jarg1);

		// Token: 0x060005DB RID: 1499
		[DllImport("rail_api", EntryPoint = "CSharp_delete_SpaceWorkID")]
		public static extern void delete_SpaceWorkID(IntPtr jarg1);

		// Token: 0x060005DC RID: 1500
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_subscriber_list_set")]
		public static extern void RailSpaceWorkFilter_subscriber_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060005DD RID: 1501
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_subscriber_list_get")]
		public static extern IntPtr RailSpaceWorkFilter_subscriber_list_get(IntPtr jarg1);

		// Token: 0x060005DE RID: 1502
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_creator_list_set")]
		public static extern void RailSpaceWorkFilter_creator_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060005DF RID: 1503
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_creator_list_get")]
		public static extern IntPtr RailSpaceWorkFilter_creator_list_get(IntPtr jarg1);

		// Token: 0x060005E0 RID: 1504
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_type_set")]
		public static extern void RailSpaceWorkFilter_type_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060005E1 RID: 1505
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_type_get")]
		public static extern IntPtr RailSpaceWorkFilter_type_get(IntPtr jarg1);

		// Token: 0x060005E2 RID: 1506
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_collector_list_set")]
		public static extern void RailSpaceWorkFilter_collector_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060005E3 RID: 1507
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_collector_list_get")]
		public static extern IntPtr RailSpaceWorkFilter_collector_list_get(IntPtr jarg1);

		// Token: 0x060005E4 RID: 1508
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_classes_set")]
		public static extern void RailSpaceWorkFilter_classes_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060005E5 RID: 1509
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkFilter_classes_get")]
		public static extern IntPtr RailSpaceWorkFilter_classes_get(IntPtr jarg1);

		// Token: 0x060005E6 RID: 1510
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkFilter__SWIG_0")]
		public static extern IntPtr new_RailSpaceWorkFilter__SWIG_0();

		// Token: 0x060005E7 RID: 1511
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkFilter__SWIG_1")]
		public static extern IntPtr new_RailSpaceWorkFilter__SWIG_1(IntPtr jarg1);

		// Token: 0x060005E8 RID: 1512
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkFilter")]
		public static extern void delete_RailSpaceWorkFilter(IntPtr jarg1);

		// Token: 0x060005E9 RID: 1513
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_url_set")]
		public static extern void RailQueryWorkFileOptions_with_url_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060005EA RID: 1514
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_url_get")]
		public static extern bool RailQueryWorkFileOptions_with_url_get(IntPtr jarg1);

		// Token: 0x060005EB RID: 1515
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_description_set")]
		public static extern void RailQueryWorkFileOptions_with_description_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060005EC RID: 1516
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_description_get")]
		public static extern bool RailQueryWorkFileOptions_with_description_get(IntPtr jarg1);

		// Token: 0x060005ED RID: 1517
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_query_total_only_set")]
		public static extern void RailQueryWorkFileOptions_query_total_only_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060005EE RID: 1518
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_query_total_only_get")]
		public static extern bool RailQueryWorkFileOptions_query_total_only_get(IntPtr jarg1);

		// Token: 0x060005EF RID: 1519
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_uploader_ids_set")]
		public static extern void RailQueryWorkFileOptions_with_uploader_ids_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060005F0 RID: 1520
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_uploader_ids_get")]
		public static extern bool RailQueryWorkFileOptions_with_uploader_ids_get(IntPtr jarg1);

		// Token: 0x060005F1 RID: 1521
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_preview_url_set")]
		public static extern void RailQueryWorkFileOptions_with_preview_url_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060005F2 RID: 1522
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_preview_url_get")]
		public static extern bool RailQueryWorkFileOptions_with_preview_url_get(IntPtr jarg1);

		// Token: 0x060005F3 RID: 1523
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_vote_detail_set")]
		public static extern void RailQueryWorkFileOptions_with_vote_detail_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060005F4 RID: 1524
		[DllImport("rail_api", EntryPoint = "CSharp_RailQueryWorkFileOptions_with_vote_detail_get")]
		public static extern bool RailQueryWorkFileOptions_with_vote_detail_get(IntPtr jarg1);

		// Token: 0x060005F5 RID: 1525
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailQueryWorkFileOptions__SWIG_0")]
		public static extern IntPtr new_RailQueryWorkFileOptions__SWIG_0();

		// Token: 0x060005F6 RID: 1526
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailQueryWorkFileOptions__SWIG_1")]
		public static extern IntPtr new_RailQueryWorkFileOptions__SWIG_1(IntPtr jarg1);

		// Token: 0x060005F7 RID: 1527
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailQueryWorkFileOptions")]
		public static extern void delete_RailQueryWorkFileOptions(IntPtr jarg1);

		// Token: 0x060005F8 RID: 1528
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_finished_bytes_set")]
		public static extern void RailSpaceWorkSyncProgress_finished_bytes_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x060005F9 RID: 1529
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_finished_bytes_get")]
		public static extern ulong RailSpaceWorkSyncProgress_finished_bytes_get(IntPtr jarg1);

		// Token: 0x060005FA RID: 1530
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_total_bytes_set")]
		public static extern void RailSpaceWorkSyncProgress_total_bytes_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x060005FB RID: 1531
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_total_bytes_get")]
		public static extern ulong RailSpaceWorkSyncProgress_total_bytes_get(IntPtr jarg1);

		// Token: 0x060005FC RID: 1532
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_progress_set")]
		public static extern void RailSpaceWorkSyncProgress_progress_set(IntPtr jarg1, float jarg2);

		// Token: 0x060005FD RID: 1533
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_progress_get")]
		public static extern float RailSpaceWorkSyncProgress_progress_get(IntPtr jarg1);

		// Token: 0x060005FE RID: 1534
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_current_state_set")]
		public static extern void RailSpaceWorkSyncProgress_current_state_set(IntPtr jarg1, int jarg2);

		// Token: 0x060005FF RID: 1535
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSyncProgress_current_state_get")]
		public static extern int RailSpaceWorkSyncProgress_current_state_get(IntPtr jarg1);

		// Token: 0x06000600 RID: 1536
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkSyncProgress__SWIG_0")]
		public static extern IntPtr new_RailSpaceWorkSyncProgress__SWIG_0();

		// Token: 0x06000601 RID: 1537
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkSyncProgress__SWIG_1")]
		public static extern IntPtr new_RailSpaceWorkSyncProgress__SWIG_1(IntPtr jarg1);

		// Token: 0x06000602 RID: 1538
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkSyncProgress")]
		public static extern void delete_RailSpaceWorkSyncProgress(IntPtr jarg1);

		// Token: 0x06000603 RID: 1539
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkVoteDetail_vote_value_set")]
		public static extern void RailSpaceWorkVoteDetail_vote_value_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000604 RID: 1540
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkVoteDetail_vote_value_get")]
		public static extern int RailSpaceWorkVoteDetail_vote_value_get(IntPtr jarg1);

		// Token: 0x06000605 RID: 1541
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkVoteDetail_voted_players_set")]
		public static extern void RailSpaceWorkVoteDetail_voted_players_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000606 RID: 1542
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkVoteDetail_voted_players_get")]
		public static extern uint RailSpaceWorkVoteDetail_voted_players_get(IntPtr jarg1);

		// Token: 0x06000607 RID: 1543
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkVoteDetail__SWIG_0")]
		public static extern IntPtr new_RailSpaceWorkVoteDetail__SWIG_0();

		// Token: 0x06000608 RID: 1544
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkVoteDetail__SWIG_1")]
		public static extern IntPtr new_RailSpaceWorkVoteDetail__SWIG_1(IntPtr jarg1);

		// Token: 0x06000609 RID: 1545
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkVoteDetail")]
		public static extern void delete_RailSpaceWorkVoteDetail(IntPtr jarg1);

		// Token: 0x0600060A RID: 1546
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_id_set")]
		public static extern void RailSpaceWorkDescriptor_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600060B RID: 1547
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_id_get")]
		public static extern IntPtr RailSpaceWorkDescriptor_id_get(IntPtr jarg1);

		// Token: 0x0600060C RID: 1548
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_name_set")]
		public static extern void RailSpaceWorkDescriptor_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600060D RID: 1549
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailSpaceWorkDescriptor_name_get(IntPtr jarg1);

		// Token: 0x0600060E RID: 1550
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_description_set")]
		public static extern void RailSpaceWorkDescriptor_description_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600060F RID: 1551
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_description_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailSpaceWorkDescriptor_description_get(IntPtr jarg1);

		// Token: 0x06000610 RID: 1552
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_detail_url_set")]
		public static extern void RailSpaceWorkDescriptor_detail_url_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000611 RID: 1553
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_detail_url_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailSpaceWorkDescriptor_detail_url_get(IntPtr jarg1);

		// Token: 0x06000612 RID: 1554
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_uploader_ids_set")]
		public static extern void RailSpaceWorkDescriptor_uploader_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000613 RID: 1555
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_uploader_ids_get")]
		public static extern IntPtr RailSpaceWorkDescriptor_uploader_ids_get(IntPtr jarg1);

		// Token: 0x06000614 RID: 1556
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_preview_url_set")]
		public static extern void RailSpaceWorkDescriptor_preview_url_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000615 RID: 1557
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_preview_url_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailSpaceWorkDescriptor_preview_url_get(IntPtr jarg1);

		// Token: 0x06000616 RID: 1558
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_vote_details_set")]
		public static extern void RailSpaceWorkDescriptor_vote_details_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000617 RID: 1559
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkDescriptor_vote_details_get")]
		public static extern IntPtr RailSpaceWorkDescriptor_vote_details_get(IntPtr jarg1);

		// Token: 0x06000618 RID: 1560
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkDescriptor__SWIG_0")]
		public static extern IntPtr new_RailSpaceWorkDescriptor__SWIG_0();

		// Token: 0x06000619 RID: 1561
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkDescriptor__SWIG_1")]
		public static extern IntPtr new_RailSpaceWorkDescriptor__SWIG_1(IntPtr jarg1);

		// Token: 0x0600061A RID: 1562
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkDescriptor")]
		public static extern void delete_RailSpaceWorkDescriptor(IntPtr jarg1);

		// Token: 0x0600061B RID: 1563
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_spacework_descriptors_set")]
		public static extern void AsyncGetMySubscribedWorksResult_spacework_descriptors_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600061C RID: 1564
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_spacework_descriptors_get")]
		public static extern IntPtr AsyncGetMySubscribedWorksResult_spacework_descriptors_get(IntPtr jarg1);

		// Token: 0x0600061D RID: 1565
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_total_available_works_set")]
		public static extern void AsyncGetMySubscribedWorksResult_total_available_works_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600061E RID: 1566
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_total_available_works_get")]
		public static extern uint AsyncGetMySubscribedWorksResult_total_available_works_get(IntPtr jarg1);

		// Token: 0x0600061F RID: 1567
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetMySubscribedWorksResult__SWIG_0")]
		public static extern IntPtr new_AsyncGetMySubscribedWorksResult__SWIG_0();

		// Token: 0x06000620 RID: 1568
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetMySubscribedWorksResult__SWIG_1")]
		public static extern IntPtr new_AsyncGetMySubscribedWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000621 RID: 1569
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncGetMySubscribedWorksResult")]
		public static extern void delete_AsyncGetMySubscribedWorksResult(IntPtr jarg1);

		// Token: 0x06000622 RID: 1570
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_spacework_descriptors_set")]
		public static extern void AsyncGetMyFavoritesWorksResult_spacework_descriptors_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000623 RID: 1571
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_spacework_descriptors_get")]
		public static extern IntPtr AsyncGetMyFavoritesWorksResult_spacework_descriptors_get(IntPtr jarg1);

		// Token: 0x06000624 RID: 1572
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_total_available_works_set")]
		public static extern void AsyncGetMyFavoritesWorksResult_total_available_works_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000625 RID: 1573
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_total_available_works_get")]
		public static extern uint AsyncGetMyFavoritesWorksResult_total_available_works_get(IntPtr jarg1);

		// Token: 0x06000626 RID: 1574
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetMyFavoritesWorksResult__SWIG_0")]
		public static extern IntPtr new_AsyncGetMyFavoritesWorksResult__SWIG_0();

		// Token: 0x06000627 RID: 1575
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetMyFavoritesWorksResult__SWIG_1")]
		public static extern IntPtr new_AsyncGetMyFavoritesWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000628 RID: 1576
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncGetMyFavoritesWorksResult")]
		public static extern void delete_AsyncGetMyFavoritesWorksResult(IntPtr jarg1);

		// Token: 0x06000629 RID: 1577
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_spacework_descriptors_set")]
		public static extern void AsyncQuerySpaceWorksResult_spacework_descriptors_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600062A RID: 1578
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_spacework_descriptors_get")]
		public static extern IntPtr AsyncQuerySpaceWorksResult_spacework_descriptors_get(IntPtr jarg1);

		// Token: 0x0600062B RID: 1579
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_total_available_works_set")]
		public static extern void AsyncQuerySpaceWorksResult_total_available_works_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600062C RID: 1580
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_total_available_works_get")]
		public static extern uint AsyncQuerySpaceWorksResult_total_available_works_get(IntPtr jarg1);

		// Token: 0x0600062D RID: 1581
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncQuerySpaceWorksResult__SWIG_0")]
		public static extern IntPtr new_AsyncQuerySpaceWorksResult__SWIG_0();

		// Token: 0x0600062E RID: 1582
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncQuerySpaceWorksResult__SWIG_1")]
		public static extern IntPtr new_AsyncQuerySpaceWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600062F RID: 1583
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncQuerySpaceWorksResult")]
		public static extern void delete_AsyncQuerySpaceWorksResult(IntPtr jarg1);

		// Token: 0x06000630 RID: 1584
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_id_set")]
		public static extern void AsyncUpdateMetadataResult_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000631 RID: 1585
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_id_get")]
		public static extern IntPtr AsyncUpdateMetadataResult_id_get(IntPtr jarg1);

		// Token: 0x06000632 RID: 1586
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_type_set")]
		public static extern void AsyncUpdateMetadataResult_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000633 RID: 1587
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_type_get")]
		public static extern int AsyncUpdateMetadataResult_type_get(IntPtr jarg1);

		// Token: 0x06000634 RID: 1588
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncUpdateMetadataResult__SWIG_0")]
		public static extern IntPtr new_AsyncUpdateMetadataResult__SWIG_0();

		// Token: 0x06000635 RID: 1589
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncUpdateMetadataResult__SWIG_1")]
		public static extern IntPtr new_AsyncUpdateMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000636 RID: 1590
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncUpdateMetadataResult")]
		public static extern void delete_AsyncUpdateMetadataResult(IntPtr jarg1);

		// Token: 0x06000637 RID: 1591
		[DllImport("rail_api", EntryPoint = "CSharp_SyncSpaceWorkResult_id_set")]
		public static extern void SyncSpaceWorkResult_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000638 RID: 1592
		[DllImport("rail_api", EntryPoint = "CSharp_SyncSpaceWorkResult_id_get")]
		public static extern IntPtr SyncSpaceWorkResult_id_get(IntPtr jarg1);

		// Token: 0x06000639 RID: 1593
		[DllImport("rail_api", EntryPoint = "CSharp_new_SyncSpaceWorkResult__SWIG_0")]
		public static extern IntPtr new_SyncSpaceWorkResult__SWIG_0();

		// Token: 0x0600063A RID: 1594
		[DllImport("rail_api", EntryPoint = "CSharp_new_SyncSpaceWorkResult__SWIG_1")]
		public static extern IntPtr new_SyncSpaceWorkResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600063B RID: 1595
		[DllImport("rail_api", EntryPoint = "CSharp_delete_SyncSpaceWorkResult")]
		public static extern void delete_SyncSpaceWorkResult(IntPtr jarg1);

		// Token: 0x0600063C RID: 1596
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_success_ids_set")]
		public static extern void AsyncSubscribeSpaceWorksResult_success_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600063D RID: 1597
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_success_ids_get")]
		public static extern IntPtr AsyncSubscribeSpaceWorksResult_success_ids_get(IntPtr jarg1);

		// Token: 0x0600063E RID: 1598
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_failure_ids_set")]
		public static extern void AsyncSubscribeSpaceWorksResult_failure_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600063F RID: 1599
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_failure_ids_get")]
		public static extern IntPtr AsyncSubscribeSpaceWorksResult_failure_ids_get(IntPtr jarg1);

		// Token: 0x06000640 RID: 1600
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_subscribe_set")]
		public static extern void AsyncSubscribeSpaceWorksResult_subscribe_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000641 RID: 1601
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_subscribe_get")]
		public static extern bool AsyncSubscribeSpaceWorksResult_subscribe_get(IntPtr jarg1);

		// Token: 0x06000642 RID: 1602
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncSubscribeSpaceWorksResult__SWIG_0")]
		public static extern IntPtr new_AsyncSubscribeSpaceWorksResult__SWIG_0();

		// Token: 0x06000643 RID: 1603
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncSubscribeSpaceWorksResult__SWIG_1")]
		public static extern IntPtr new_AsyncSubscribeSpaceWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000644 RID: 1604
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncSubscribeSpaceWorksResult")]
		public static extern void delete_AsyncSubscribeSpaceWorksResult(IntPtr jarg1);

		// Token: 0x06000645 RID: 1605
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_success_ids_set")]
		public static extern void AsyncModifyFavoritesWorksResult_success_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000646 RID: 1606
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_success_ids_get")]
		public static extern IntPtr AsyncModifyFavoritesWorksResult_success_ids_get(IntPtr jarg1);

		// Token: 0x06000647 RID: 1607
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_failure_ids_set")]
		public static extern void AsyncModifyFavoritesWorksResult_failure_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000648 RID: 1608
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_failure_ids_get")]
		public static extern IntPtr AsyncModifyFavoritesWorksResult_failure_ids_get(IntPtr jarg1);

		// Token: 0x06000649 RID: 1609
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_modify_flag_set")]
		public static extern void AsyncModifyFavoritesWorksResult_modify_flag_set(IntPtr jarg1, int jarg2);

		// Token: 0x0600064A RID: 1610
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_modify_flag_get")]
		public static extern int AsyncModifyFavoritesWorksResult_modify_flag_get(IntPtr jarg1);

		// Token: 0x0600064B RID: 1611
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncModifyFavoritesWorksResult__SWIG_0")]
		public static extern IntPtr new_AsyncModifyFavoritesWorksResult__SWIG_0();

		// Token: 0x0600064C RID: 1612
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncModifyFavoritesWorksResult__SWIG_1")]
		public static extern IntPtr new_AsyncModifyFavoritesWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600064D RID: 1613
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncModifyFavoritesWorksResult")]
		public static extern void delete_AsyncModifyFavoritesWorksResult(IntPtr jarg1);

		// Token: 0x0600064E RID: 1614
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveSpaceWorkResult_id_set")]
		public static extern void AsyncRemoveSpaceWorkResult_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600064F RID: 1615
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveSpaceWorkResult_id_get")]
		public static extern IntPtr AsyncRemoveSpaceWorkResult_id_get(IntPtr jarg1);

		// Token: 0x06000650 RID: 1616
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRemoveSpaceWorkResult__SWIG_0")]
		public static extern IntPtr new_AsyncRemoveSpaceWorkResult__SWIG_0();

		// Token: 0x06000651 RID: 1617
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRemoveSpaceWorkResult__SWIG_1")]
		public static extern IntPtr new_AsyncRemoveSpaceWorkResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000652 RID: 1618
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncRemoveSpaceWorkResult")]
		public static extern void delete_AsyncRemoveSpaceWorkResult(IntPtr jarg1);

		// Token: 0x06000653 RID: 1619
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncVoteSpaceWorkResult_id_set")]
		public static extern void AsyncVoteSpaceWorkResult_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000654 RID: 1620
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncVoteSpaceWorkResult_id_get")]
		public static extern IntPtr AsyncVoteSpaceWorkResult_id_get(IntPtr jarg1);

		// Token: 0x06000655 RID: 1621
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncVoteSpaceWorkResult__SWIG_0")]
		public static extern IntPtr new_AsyncVoteSpaceWorkResult__SWIG_0();

		// Token: 0x06000656 RID: 1622
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncVoteSpaceWorkResult__SWIG_1")]
		public static extern IntPtr new_AsyncVoteSpaceWorkResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000657 RID: 1623
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncVoteSpaceWorkResult")]
		public static extern void delete_AsyncVoteSpaceWorkResult(IntPtr jarg1);

		// Token: 0x06000658 RID: 1624
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_spacework_descriptors_set")]
		public static extern void AsyncSearchSpaceWorksResult_spacework_descriptors_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000659 RID: 1625
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_spacework_descriptors_get")]
		public static extern IntPtr AsyncSearchSpaceWorksResult_spacework_descriptors_get(IntPtr jarg1);

		// Token: 0x0600065A RID: 1626
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_total_available_works_set")]
		public static extern void AsyncSearchSpaceWorksResult_total_available_works_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600065B RID: 1627
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_total_available_works_get")]
		public static extern uint AsyncSearchSpaceWorksResult_total_available_works_get(IntPtr jarg1);

		// Token: 0x0600065C RID: 1628
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncSearchSpaceWorksResult__SWIG_0")]
		public static extern IntPtr new_AsyncSearchSpaceWorksResult__SWIG_0();

		// Token: 0x0600065D RID: 1629
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncSearchSpaceWorksResult__SWIG_1")]
		public static extern IntPtr new_AsyncSearchSpaceWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600065E RID: 1630
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncSearchSpaceWorksResult")]
		public static extern void delete_AsyncSearchSpaceWorksResult(IntPtr jarg1);

		// Token: 0x0600065F RID: 1631
		[DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_spacework_descriptors_set")]
		public static extern void QueryMySubscribedSpaceWorksResult_spacework_descriptors_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000660 RID: 1632
		[DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_spacework_descriptors_get")]
		public static extern IntPtr QueryMySubscribedSpaceWorksResult_spacework_descriptors_get(IntPtr jarg1);

		// Token: 0x06000661 RID: 1633
		[DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_spacework_type_set")]
		public static extern void QueryMySubscribedSpaceWorksResult_spacework_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000662 RID: 1634
		[DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_spacework_type_get")]
		public static extern int QueryMySubscribedSpaceWorksResult_spacework_type_get(IntPtr jarg1);

		// Token: 0x06000663 RID: 1635
		[DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_total_available_works_set")]
		public static extern void QueryMySubscribedSpaceWorksResult_total_available_works_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000664 RID: 1636
		[DllImport("rail_api", EntryPoint = "CSharp_QueryMySubscribedSpaceWorksResult_total_available_works_get")]
		public static extern uint QueryMySubscribedSpaceWorksResult_total_available_works_get(IntPtr jarg1);

		// Token: 0x06000665 RID: 1637
		[DllImport("rail_api", EntryPoint = "CSharp_new_QueryMySubscribedSpaceWorksResult__SWIG_0")]
		public static extern IntPtr new_QueryMySubscribedSpaceWorksResult__SWIG_0();

		// Token: 0x06000666 RID: 1638
		[DllImport("rail_api", EntryPoint = "CSharp_new_QueryMySubscribedSpaceWorksResult__SWIG_1")]
		public static extern IntPtr new_QueryMySubscribedSpaceWorksResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000667 RID: 1639
		[DllImport("rail_api", EntryPoint = "CSharp_delete_QueryMySubscribedSpaceWorksResult")]
		public static extern void delete_QueryMySubscribedSpaceWorksResult(IntPtr jarg1);

		// Token: 0x06000668 RID: 1640
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_detail_set")]
		public static extern void RailSpaceWorkUpdateOptions_with_detail_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000669 RID: 1641
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_detail_get")]
		public static extern bool RailSpaceWorkUpdateOptions_with_detail_get(IntPtr jarg1);

		// Token: 0x0600066A RID: 1642
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_metadata_set")]
		public static extern void RailSpaceWorkUpdateOptions_with_metadata_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600066B RID: 1643
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_metadata_get")]
		public static extern bool RailSpaceWorkUpdateOptions_with_metadata_get(IntPtr jarg1);

		// Token: 0x0600066C RID: 1644
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_check_has_subscribed_set")]
		public static extern void RailSpaceWorkUpdateOptions_check_has_subscribed_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600066D RID: 1645
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_check_has_subscribed_get")]
		public static extern bool RailSpaceWorkUpdateOptions_check_has_subscribed_get(IntPtr jarg1);

		// Token: 0x0600066E RID: 1646
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_check_has_favorited_set")]
		public static extern void RailSpaceWorkUpdateOptions_check_has_favorited_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600066F RID: 1647
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_check_has_favorited_get")]
		public static extern bool RailSpaceWorkUpdateOptions_check_has_favorited_get(IntPtr jarg1);

		// Token: 0x06000670 RID: 1648
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_my_vote_set")]
		public static extern void RailSpaceWorkUpdateOptions_with_my_vote_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000671 RID: 1649
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_my_vote_get")]
		public static extern bool RailSpaceWorkUpdateOptions_with_my_vote_get(IntPtr jarg1);

		// Token: 0x06000672 RID: 1650
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_vote_detail_set")]
		public static extern void RailSpaceWorkUpdateOptions_with_vote_detail_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000673 RID: 1651
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkUpdateOptions_with_vote_detail_get")]
		public static extern bool RailSpaceWorkUpdateOptions_with_vote_detail_get(IntPtr jarg1);

		// Token: 0x06000674 RID: 1652
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkUpdateOptions__SWIG_0")]
		public static extern IntPtr new_RailSpaceWorkUpdateOptions__SWIG_0();

		// Token: 0x06000675 RID: 1653
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkUpdateOptions__SWIG_1")]
		public static extern IntPtr new_RailSpaceWorkUpdateOptions__SWIG_1(IntPtr jarg1);

		// Token: 0x06000676 RID: 1654
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkUpdateOptions")]
		public static extern void delete_RailSpaceWorkUpdateOptions(IntPtr jarg1);

		// Token: 0x06000677 RID: 1655
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkSearchFilter__SWIG_0")]
		public static extern IntPtr new_RailSpaceWorkSearchFilter__SWIG_0();

		// Token: 0x06000678 RID: 1656
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_search_text_set")]
		public static extern void RailSpaceWorkSearchFilter_search_text_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000679 RID: 1657
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_search_text_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailSpaceWorkSearchFilter_search_text_get(IntPtr jarg1);

		// Token: 0x0600067A RID: 1658
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_required_tags_set")]
		public static extern void RailSpaceWorkSearchFilter_required_tags_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600067B RID: 1659
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_required_tags_get")]
		public static extern IntPtr RailSpaceWorkSearchFilter_required_tags_get(IntPtr jarg1);

		// Token: 0x0600067C RID: 1660
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_excluded_tags_set")]
		public static extern void RailSpaceWorkSearchFilter_excluded_tags_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600067D RID: 1661
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_excluded_tags_get")]
		public static extern IntPtr RailSpaceWorkSearchFilter_excluded_tags_get(IntPtr jarg1);

		// Token: 0x0600067E RID: 1662
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_required_metadata_set")]
		public static extern void RailSpaceWorkSearchFilter_required_metadata_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600067F RID: 1663
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_required_metadata_get")]
		public static extern IntPtr RailSpaceWorkSearchFilter_required_metadata_get(IntPtr jarg1);

		// Token: 0x06000680 RID: 1664
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_excluded_metadata_set")]
		public static extern void RailSpaceWorkSearchFilter_excluded_metadata_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000681 RID: 1665
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_excluded_metadata_get")]
		public static extern IntPtr RailSpaceWorkSearchFilter_excluded_metadata_get(IntPtr jarg1);

		// Token: 0x06000682 RID: 1666
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_match_all_required_metadata_set")]
		public static extern void RailSpaceWorkSearchFilter_match_all_required_metadata_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000683 RID: 1667
		[DllImport("rail_api", EntryPoint = "CSharp_RailSpaceWorkSearchFilter_match_all_required_metadata_get")]
		public static extern bool RailSpaceWorkSearchFilter_match_all_required_metadata_get(IntPtr jarg1);

		// Token: 0x06000684 RID: 1668
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSpaceWorkSearchFilter__SWIG_1")]
		public static extern IntPtr new_RailSpaceWorkSearchFilter__SWIG_1(IntPtr jarg1);

		// Token: 0x06000685 RID: 1669
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSpaceWorkSearchFilter")]
		public static extern void delete_RailSpaceWorkSearchFilter(IntPtr jarg1);

		// Token: 0x06000686 RID: 1670
		[DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_invite_type_set")]
		public static extern void RailInviteOptions_invite_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000687 RID: 1671
		[DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_invite_type_get")]
		public static extern int RailInviteOptions_invite_type_get(IntPtr jarg1);

		// Token: 0x06000688 RID: 1672
		[DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_need_respond_in_game_set")]
		public static extern void RailInviteOptions_need_respond_in_game_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000689 RID: 1673
		[DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_need_respond_in_game_get")]
		public static extern bool RailInviteOptions_need_respond_in_game_get(IntPtr jarg1);

		// Token: 0x0600068A RID: 1674
		[DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_additional_message_set")]
		public static extern void RailInviteOptions_additional_message_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600068B RID: 1675
		[DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_additional_message_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailInviteOptions_additional_message_get(IntPtr jarg1);

		// Token: 0x0600068C RID: 1676
		[DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_expire_time_set")]
		public static extern void RailInviteOptions_expire_time_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600068D RID: 1677
		[DllImport("rail_api", EntryPoint = "CSharp_RailInviteOptions_expire_time_get")]
		public static extern uint RailInviteOptions_expire_time_get(IntPtr jarg1);

		// Token: 0x0600068E RID: 1678
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInviteOptions__SWIG_0")]
		public static extern IntPtr new_RailInviteOptions__SWIG_0();

		// Token: 0x0600068F RID: 1679
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInviteOptions__SWIG_1")]
		public static extern IntPtr new_RailInviteOptions__SWIG_1(IntPtr jarg1);

		// Token: 0x06000690 RID: 1680
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInviteOptions")]
		public static extern void delete_RailInviteOptions(IntPtr jarg1);

		// Token: 0x06000691 RID: 1681
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInfoData__SWIG_0")]
		public static extern IntPtr new_RailUsersInfoData__SWIG_0();

		// Token: 0x06000692 RID: 1682
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInfoData_user_info_list_set")]
		public static extern void RailUsersInfoData_user_info_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000693 RID: 1683
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInfoData_user_info_list_get")]
		public static extern IntPtr RailUsersInfoData_user_info_list_get(IntPtr jarg1);

		// Token: 0x06000694 RID: 1684
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInfoData__SWIG_1")]
		public static extern IntPtr new_RailUsersInfoData__SWIG_1(IntPtr jarg1);

		// Token: 0x06000695 RID: 1685
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersInfoData")]
		public static extern void delete_RailUsersInfoData(IntPtr jarg1);

		// Token: 0x06000696 RID: 1686
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersNotifyInviter__SWIG_0")]
		public static extern IntPtr new_RailUsersNotifyInviter__SWIG_0();

		// Token: 0x06000697 RID: 1687
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersNotifyInviter_invitee_id_set")]
		public static extern void RailUsersNotifyInviter_invitee_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000698 RID: 1688
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersNotifyInviter_invitee_id_get")]
		public static extern IntPtr RailUsersNotifyInviter_invitee_id_get(IntPtr jarg1);

		// Token: 0x06000699 RID: 1689
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersNotifyInviter__SWIG_1")]
		public static extern IntPtr new_RailUsersNotifyInviter__SWIG_1(IntPtr jarg1);

		// Token: 0x0600069A RID: 1690
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersNotifyInviter")]
		public static extern void delete_RailUsersNotifyInviter(IntPtr jarg1);

		// Token: 0x0600069B RID: 1691
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersRespondInvitation__SWIG_0")]
		public static extern IntPtr new_RailUsersRespondInvitation__SWIG_0();

		// Token: 0x0600069C RID: 1692
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvitation_inviter_id_set")]
		public static extern void RailUsersRespondInvitation_inviter_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600069D RID: 1693
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvitation_inviter_id_get")]
		public static extern IntPtr RailUsersRespondInvitation_inviter_id_get(IntPtr jarg1);

		// Token: 0x0600069E RID: 1694
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvitation_response_set")]
		public static extern void RailUsersRespondInvitation_response_set(IntPtr jarg1, int jarg2);

		// Token: 0x0600069F RID: 1695
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvitation_response_get")]
		public static extern int RailUsersRespondInvitation_response_get(IntPtr jarg1);

		// Token: 0x060006A0 RID: 1696
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvitation_original_invite_option_set")]
		public static extern void RailUsersRespondInvitation_original_invite_option_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060006A1 RID: 1697
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvitation_original_invite_option_get")]
		public static extern IntPtr RailUsersRespondInvitation_original_invite_option_get(IntPtr jarg1);

		// Token: 0x060006A2 RID: 1698
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersRespondInvitation__SWIG_1")]
		public static extern IntPtr new_RailUsersRespondInvitation__SWIG_1(IntPtr jarg1);

		// Token: 0x060006A3 RID: 1699
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersRespondInvitation")]
		public static extern void delete_RailUsersRespondInvitation(IntPtr jarg1);

		// Token: 0x060006A4 RID: 1700
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInviteJoinGameResult__SWIG_0")]
		public static extern IntPtr new_RailUsersInviteJoinGameResult__SWIG_0();

		// Token: 0x060006A5 RID: 1701
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_invitee_id_set")]
		public static extern void RailUsersInviteJoinGameResult_invitee_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060006A6 RID: 1702
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_invitee_id_get")]
		public static extern IntPtr RailUsersInviteJoinGameResult_invitee_id_get(IntPtr jarg1);

		// Token: 0x060006A7 RID: 1703
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_response_value_set")]
		public static extern void RailUsersInviteJoinGameResult_response_value_set(IntPtr jarg1, int jarg2);

		// Token: 0x060006A8 RID: 1704
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_response_value_get")]
		public static extern int RailUsersInviteJoinGameResult_response_value_get(IntPtr jarg1);

		// Token: 0x060006A9 RID: 1705
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_invite_type_set")]
		public static extern void RailUsersInviteJoinGameResult_invite_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060006AA RID: 1706
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_invite_type_get")]
		public static extern int RailUsersInviteJoinGameResult_invite_type_get(IntPtr jarg1);

		// Token: 0x060006AB RID: 1707
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInviteJoinGameResult__SWIG_1")]
		public static extern IntPtr new_RailUsersInviteJoinGameResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006AC RID: 1708
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersInviteJoinGameResult")]
		public static extern void delete_RailUsersInviteJoinGameResult(IntPtr jarg1);

		// Token: 0x060006AD RID: 1709
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersGetInviteDetailResult__SWIG_0")]
		public static extern IntPtr new_RailUsersGetInviteDetailResult__SWIG_0();

		// Token: 0x060006AE RID: 1710
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_inviter_id_set")]
		public static extern void RailUsersGetInviteDetailResult_inviter_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060006AF RID: 1711
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_inviter_id_get")]
		public static extern IntPtr RailUsersGetInviteDetailResult_inviter_id_get(IntPtr jarg1);

		// Token: 0x060006B0 RID: 1712
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_command_line_set")]
		public static extern void RailUsersGetInviteDetailResult_command_line_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060006B1 RID: 1713
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_command_line_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailUsersGetInviteDetailResult_command_line_get(IntPtr jarg1);

		// Token: 0x060006B2 RID: 1714
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_invite_type_set")]
		public static extern void RailUsersGetInviteDetailResult_invite_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060006B3 RID: 1715
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_invite_type_get")]
		public static extern int RailUsersGetInviteDetailResult_invite_type_get(IntPtr jarg1);

		// Token: 0x060006B4 RID: 1716
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersGetInviteDetailResult__SWIG_1")]
		public static extern IntPtr new_RailUsersGetInviteDetailResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006B5 RID: 1717
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersGetInviteDetailResult")]
		public static extern void delete_RailUsersGetInviteDetailResult(IntPtr jarg1);

		// Token: 0x060006B6 RID: 1718
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersCancelInviteResult__SWIG_0")]
		public static extern IntPtr new_RailUsersCancelInviteResult__SWIG_0();

		// Token: 0x060006B7 RID: 1719
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersCancelInviteResult_invite_type_set")]
		public static extern void RailUsersCancelInviteResult_invite_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060006B8 RID: 1720
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersCancelInviteResult_invite_type_get")]
		public static extern int RailUsersCancelInviteResult_invite_type_get(IntPtr jarg1);

		// Token: 0x060006B9 RID: 1721
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersCancelInviteResult__SWIG_1")]
		public static extern IntPtr new_RailUsersCancelInviteResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006BA RID: 1722
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersCancelInviteResult")]
		public static extern void delete_RailUsersCancelInviteResult(IntPtr jarg1);

		// Token: 0x060006BB RID: 1723
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInviteUsersResult__SWIG_0")]
		public static extern IntPtr new_RailUsersInviteUsersResult__SWIG_0();

		// Token: 0x060006BC RID: 1724
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteUsersResult_invite_type_set")]
		public static extern void RailUsersInviteUsersResult_invite_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060006BD RID: 1725
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteUsersResult_invite_type_get")]
		public static extern int RailUsersInviteUsersResult_invite_type_get(IntPtr jarg1);

		// Token: 0x060006BE RID: 1726
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersInviteUsersResult__SWIG_1")]
		public static extern IntPtr new_RailUsersInviteUsersResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006BF RID: 1727
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersInviteUsersResult")]
		public static extern void delete_RailUsersInviteUsersResult(IntPtr jarg1);

		// Token: 0x060006C0 RID: 1728
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersGetUserLimitsResult__SWIG_0")]
		public static extern IntPtr new_RailUsersGetUserLimitsResult__SWIG_0();

		// Token: 0x060006C1 RID: 1729
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetUserLimitsResult_user_id_set")]
		public static extern void RailUsersGetUserLimitsResult_user_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060006C2 RID: 1730
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetUserLimitsResult_user_id_get")]
		public static extern IntPtr RailUsersGetUserLimitsResult_user_id_get(IntPtr jarg1);

		// Token: 0x060006C3 RID: 1731
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetUserLimitsResult_user_limits_set")]
		public static extern void RailUsersGetUserLimitsResult_user_limits_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060006C4 RID: 1732
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetUserLimitsResult_user_limits_get")]
		public static extern IntPtr RailUsersGetUserLimitsResult_user_limits_get(IntPtr jarg1);

		// Token: 0x060006C5 RID: 1733
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUsersGetUserLimitsResult__SWIG_1")]
		public static extern IntPtr new_RailUsersGetUserLimitsResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006C6 RID: 1734
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUsersGetUserLimitsResult")]
		public static extern void delete_RailUsersGetUserLimitsResult(IntPtr jarg1);

		// Token: 0x060006C7 RID: 1735
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailShowChatWindowWithFriendResult__SWIG_0")]
		public static extern IntPtr new_RailShowChatWindowWithFriendResult__SWIG_0();

		// Token: 0x060006C8 RID: 1736
		[DllImport("rail_api", EntryPoint = "CSharp_RailShowChatWindowWithFriendResult_is_show_set")]
		public static extern void RailShowChatWindowWithFriendResult_is_show_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060006C9 RID: 1737
		[DllImport("rail_api", EntryPoint = "CSharp_RailShowChatWindowWithFriendResult_is_show_get")]
		public static extern bool RailShowChatWindowWithFriendResult_is_show_get(IntPtr jarg1);

		// Token: 0x060006CA RID: 1738
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailShowChatWindowWithFriendResult__SWIG_1")]
		public static extern IntPtr new_RailShowChatWindowWithFriendResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006CB RID: 1739
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailShowChatWindowWithFriendResult")]
		public static extern void delete_RailShowChatWindowWithFriendResult(IntPtr jarg1);

		// Token: 0x060006CC RID: 1740
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailShowUserHomepageWindowResult__SWIG_0")]
		public static extern IntPtr new_RailShowUserHomepageWindowResult__SWIG_0();

		// Token: 0x060006CD RID: 1741
		[DllImport("rail_api", EntryPoint = "CSharp_RailShowUserHomepageWindowResult_is_show_set")]
		public static extern void RailShowUserHomepageWindowResult_is_show_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060006CE RID: 1742
		[DllImport("rail_api", EntryPoint = "CSharp_RailShowUserHomepageWindowResult_is_show_get")]
		public static extern bool RailShowUserHomepageWindowResult_is_show_get(IntPtr jarg1);

		// Token: 0x060006CF RID: 1743
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailShowUserHomepageWindowResult__SWIG_1")]
		public static extern IntPtr new_RailShowUserHomepageWindowResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006D0 RID: 1744
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailShowUserHomepageWindowResult")]
		public static extern void delete_RailShowUserHomepageWindowResult(IntPtr jarg1);

		// Token: 0x060006D1 RID: 1745
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailImageDataDescriptor__SWIG_0")]
		public static extern IntPtr new_RailImageDataDescriptor__SWIG_0();

		// Token: 0x060006D2 RID: 1746
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_image_width_set")]
		public static extern void RailImageDataDescriptor_image_width_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060006D3 RID: 1747
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_image_width_get")]
		public static extern uint RailImageDataDescriptor_image_width_get(IntPtr jarg1);

		// Token: 0x060006D4 RID: 1748
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_image_height_set")]
		public static extern void RailImageDataDescriptor_image_height_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060006D5 RID: 1749
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_image_height_get")]
		public static extern uint RailImageDataDescriptor_image_height_get(IntPtr jarg1);

		// Token: 0x060006D6 RID: 1750
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_stride_in_bytes_set")]
		public static extern void RailImageDataDescriptor_stride_in_bytes_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060006D7 RID: 1751
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_stride_in_bytes_get")]
		public static extern uint RailImageDataDescriptor_stride_in_bytes_get(IntPtr jarg1);

		// Token: 0x060006D8 RID: 1752
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_bits_per_pixel_set")]
		public static extern void RailImageDataDescriptor_bits_per_pixel_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060006D9 RID: 1753
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_bits_per_pixel_get")]
		public static extern uint RailImageDataDescriptor_bits_per_pixel_get(IntPtr jarg1);

		// Token: 0x060006DA RID: 1754
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_pixel_format_set")]
		public static extern void RailImageDataDescriptor_pixel_format_set(IntPtr jarg1, int jarg2);

		// Token: 0x060006DB RID: 1755
		[DllImport("rail_api", EntryPoint = "CSharp_RailImageDataDescriptor_pixel_format_get")]
		public static extern int RailImageDataDescriptor_pixel_format_get(IntPtr jarg1);

		// Token: 0x060006DC RID: 1756
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailImageDataDescriptor__SWIG_1")]
		public static extern IntPtr new_RailImageDataDescriptor__SWIG_1(IntPtr jarg1);

		// Token: 0x060006DD RID: 1757
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailImageDataDescriptor")]
		public static extern void delete_RailImageDataDescriptor(IntPtr jarg1);

		// Token: 0x060006DE RID: 1758
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDirtyWordsCheckResult__SWIG_0")]
		public static extern IntPtr new_RailDirtyWordsCheckResult__SWIG_0();

		// Token: 0x060006DF RID: 1759
		[DllImport("rail_api", EntryPoint = "CSharp_RailDirtyWordsCheckResult_replace_string_set")]
		public static extern void RailDirtyWordsCheckResult_replace_string_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060006E0 RID: 1760
		[DllImport("rail_api", EntryPoint = "CSharp_RailDirtyWordsCheckResult_replace_string_get")]
		public static extern IntPtr RailDirtyWordsCheckResult_replace_string_get(IntPtr jarg1);

		// Token: 0x060006E1 RID: 1761
		[DllImport("rail_api", EntryPoint = "CSharp_RailDirtyWordsCheckResult_dirty_type_set")]
		public static extern void RailDirtyWordsCheckResult_dirty_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060006E2 RID: 1762
		[DllImport("rail_api", EntryPoint = "CSharp_RailDirtyWordsCheckResult_dirty_type_get")]
		public static extern int RailDirtyWordsCheckResult_dirty_type_get(IntPtr jarg1);

		// Token: 0x060006E3 RID: 1763
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDirtyWordsCheckResult__SWIG_1")]
		public static extern IntPtr new_RailDirtyWordsCheckResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006E4 RID: 1764
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailDirtyWordsCheckResult")]
		public static extern void delete_RailDirtyWordsCheckResult(IntPtr jarg1);

		// Token: 0x060006E5 RID: 1765
		[DllImport("rail_api", EntryPoint = "CSharp_RailCrashInfo_exception_type_set")]
		public static extern void RailCrashInfo_exception_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060006E6 RID: 1766
		[DllImport("rail_api", EntryPoint = "CSharp_RailCrashInfo_exception_type_get")]
		public static extern int RailCrashInfo_exception_type_get(IntPtr jarg1);

		// Token: 0x060006E7 RID: 1767
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailCrashInfo__SWIG_0")]
		public static extern IntPtr new_RailCrashInfo__SWIG_0();

		// Token: 0x060006E8 RID: 1768
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailCrashInfo__SWIG_1")]
		public static extern IntPtr new_RailCrashInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x060006E9 RID: 1769
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailCrashInfo")]
		public static extern void delete_RailCrashInfo(IntPtr jarg1);

		// Token: 0x060006EA RID: 1770
		[DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_GetData")]
		public static extern string RailCrashBuffer_GetData(IntPtr jarg1);

		// Token: 0x060006EB RID: 1771
		[DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_GetBufferLength")]
		public static extern uint RailCrashBuffer_GetBufferLength(IntPtr jarg1);

		// Token: 0x060006EC RID: 1772
		[DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_GetValidLength")]
		public static extern uint RailCrashBuffer_GetValidLength(IntPtr jarg1);

		// Token: 0x060006ED RID: 1773
		[DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_SetData__SWIG_0")]
		public static extern uint RailCrashBuffer_SetData__SWIG_0(IntPtr jarg1, string jarg2, uint jarg3, uint jarg4);

		// Token: 0x060006EE RID: 1774
		[DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_SetData__SWIG_1")]
		public static extern uint RailCrashBuffer_SetData__SWIG_1(IntPtr jarg1, string jarg2, uint jarg3);

		// Token: 0x060006EF RID: 1775
		[DllImport("rail_api", EntryPoint = "CSharp_RailCrashBuffer_AppendData")]
		public static extern uint RailCrashBuffer_AppendData(IntPtr jarg1, string jarg2, uint jarg3);

		// Token: 0x060006F0 RID: 1776
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailCrashBuffer")]
		public static extern void delete_RailCrashBuffer(IntPtr jarg1);

		// Token: 0x060006F1 RID: 1777
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailGetImageDataResult__SWIG_0")]
		public static extern IntPtr new_RailGetImageDataResult__SWIG_0();

		// Token: 0x060006F2 RID: 1778
		[DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_image_data_set")]
		public static extern void RailGetImageDataResult_image_data_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060006F3 RID: 1779
		[DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_image_data_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailGetImageDataResult_image_data_get(IntPtr jarg1);

		// Token: 0x060006F4 RID: 1780
		[DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_image_data_descriptor_set")]
		public static extern void RailGetImageDataResult_image_data_descriptor_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060006F5 RID: 1781
		[DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_image_data_descriptor_get")]
		public static extern IntPtr RailGetImageDataResult_image_data_descriptor_get(IntPtr jarg1);

		// Token: 0x060006F6 RID: 1782
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailGetImageDataResult__SWIG_1")]
		public static extern IntPtr new_RailGetImageDataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060006F7 RID: 1783
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailGetImageDataResult")]
		public static extern void delete_RailGetImageDataResult(IntPtr jarg1);

		// Token: 0x060006F8 RID: 1784
		[DllImport("rail_api", EntryPoint = "CSharp_new_TakeScreenshotResult__SWIG_0")]
		public static extern IntPtr new_TakeScreenshotResult__SWIG_0();

		// Token: 0x060006F9 RID: 1785
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_image_file_path_set")]
		public static extern void TakeScreenshotResult_image_file_path_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060006FA RID: 1786
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_image_file_path_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string TakeScreenshotResult_image_file_path_get(IntPtr jarg1);

		// Token: 0x060006FB RID: 1787
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_image_file_size_set")]
		public static extern void TakeScreenshotResult_image_file_size_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060006FC RID: 1788
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_image_file_size_get")]
		public static extern uint TakeScreenshotResult_image_file_size_get(IntPtr jarg1);

		// Token: 0x060006FD RID: 1789
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_thumbnail_filepath_set")]
		public static extern void TakeScreenshotResult_thumbnail_filepath_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060006FE RID: 1790
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_thumbnail_filepath_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string TakeScreenshotResult_thumbnail_filepath_get(IntPtr jarg1);

		// Token: 0x060006FF RID: 1791
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_thumbnail_file_size_set")]
		public static extern void TakeScreenshotResult_thumbnail_file_size_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000700 RID: 1792
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_thumbnail_file_size_get")]
		public static extern uint TakeScreenshotResult_thumbnail_file_size_get(IntPtr jarg1);

		// Token: 0x06000701 RID: 1793
		[DllImport("rail_api", EntryPoint = "CSharp_new_TakeScreenshotResult__SWIG_1")]
		public static extern IntPtr new_TakeScreenshotResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000702 RID: 1794
		[DllImport("rail_api", EntryPoint = "CSharp_delete_TakeScreenshotResult")]
		public static extern void delete_TakeScreenshotResult(IntPtr jarg1);

		// Token: 0x06000703 RID: 1795
		[DllImport("rail_api", EntryPoint = "CSharp_new_ScreenshotRequestInfo__SWIG_0")]
		public static extern IntPtr new_ScreenshotRequestInfo__SWIG_0();

		// Token: 0x06000704 RID: 1796
		[DllImport("rail_api", EntryPoint = "CSharp_new_ScreenshotRequestInfo__SWIG_1")]
		public static extern IntPtr new_ScreenshotRequestInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000705 RID: 1797
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ScreenshotRequestInfo")]
		public static extern void delete_ScreenshotRequestInfo(IntPtr jarg1);

		// Token: 0x06000706 RID: 1798
		[DllImport("rail_api", EntryPoint = "CSharp_new_PublishScreenshotResult__SWIG_0")]
		public static extern IntPtr new_PublishScreenshotResult__SWIG_0();

		// Token: 0x06000707 RID: 1799
		[DllImport("rail_api", EntryPoint = "CSharp_PublishScreenshotResult_work_id_set")]
		public static extern void PublishScreenshotResult_work_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000708 RID: 1800
		[DllImport("rail_api", EntryPoint = "CSharp_PublishScreenshotResult_work_id_get")]
		public static extern IntPtr PublishScreenshotResult_work_id_get(IntPtr jarg1);

		// Token: 0x06000709 RID: 1801
		[DllImport("rail_api", EntryPoint = "CSharp_new_PublishScreenshotResult__SWIG_1")]
		public static extern IntPtr new_PublishScreenshotResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600070A RID: 1802
		[DllImport("rail_api", EntryPoint = "CSharp_delete_PublishScreenshotResult")]
		public static extern void delete_PublishScreenshotResult(IntPtr jarg1);

		// Token: 0x0600070B RID: 1803
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSystemStateChanged__SWIG_0")]
		public static extern IntPtr new_RailSystemStateChanged__SWIG_0();

		// Token: 0x0600070C RID: 1804
		[DllImport("rail_api", EntryPoint = "CSharp_RailSystemStateChanged_state_set")]
		public static extern void RailSystemStateChanged_state_set(IntPtr jarg1, int jarg2);

		// Token: 0x0600070D RID: 1805
		[DllImport("rail_api", EntryPoint = "CSharp_RailSystemStateChanged_state_get")]
		public static extern int RailSystemStateChanged_state_get(IntPtr jarg1);

		// Token: 0x0600070E RID: 1806
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSystemStateChanged__SWIG_1")]
		public static extern IntPtr new_RailSystemStateChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x0600070F RID: 1807
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSystemStateChanged")]
		public static extern void delete_RailSystemStateChanged(IntPtr jarg1);

		// Token: 0x06000710 RID: 1808
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByGameServer__SWIG_0")]
		public static extern IntPtr new_RailPlatformNotifyEventJoinGameByGameServer__SWIG_0();

		// Token: 0x06000711 RID: 1809
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_set")]
		public static extern void RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000712 RID: 1810
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get")]
		public static extern IntPtr RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get(IntPtr jarg1);

		// Token: 0x06000713 RID: 1811
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_commandline_info_set")]
		public static extern void RailPlatformNotifyEventJoinGameByGameServer_commandline_info_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000714 RID: 1812
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_commandline_info_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPlatformNotifyEventJoinGameByGameServer_commandline_info_get(IntPtr jarg1);

		// Token: 0x06000715 RID: 1813
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByGameServer__SWIG_1")]
		public static extern IntPtr new_RailPlatformNotifyEventJoinGameByGameServer__SWIG_1(IntPtr jarg1);

		// Token: 0x06000716 RID: 1814
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailPlatformNotifyEventJoinGameByGameServer")]
		public static extern void delete_RailPlatformNotifyEventJoinGameByGameServer(IntPtr jarg1);

		// Token: 0x06000717 RID: 1815
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByRoom__SWIG_0")]
		public static extern IntPtr new_RailPlatformNotifyEventJoinGameByRoom__SWIG_0();

		// Token: 0x06000718 RID: 1816
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_zone_id_set")]
		public static extern void RailPlatformNotifyEventJoinGameByRoom_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000719 RID: 1817
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_zone_id_get")]
		public static extern ulong RailPlatformNotifyEventJoinGameByRoom_zone_id_get(IntPtr jarg1);

		// Token: 0x0600071A RID: 1818
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_room_id_set")]
		public static extern void RailPlatformNotifyEventJoinGameByRoom_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600071B RID: 1819
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_room_id_get")]
		public static extern ulong RailPlatformNotifyEventJoinGameByRoom_room_id_get(IntPtr jarg1);

		// Token: 0x0600071C RID: 1820
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_commandline_info_set")]
		public static extern void RailPlatformNotifyEventJoinGameByRoom_commandline_info_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600071D RID: 1821
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_commandline_info_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPlatformNotifyEventJoinGameByRoom_commandline_info_get(IntPtr jarg1);

		// Token: 0x0600071E RID: 1822
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByRoom__SWIG_1")]
		public static extern IntPtr new_RailPlatformNotifyEventJoinGameByRoom__SWIG_1(IntPtr jarg1);

		// Token: 0x0600071F RID: 1823
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailPlatformNotifyEventJoinGameByRoom")]
		public static extern void delete_RailPlatformNotifyEventJoinGameByRoom(IntPtr jarg1);

		// Token: 0x06000720 RID: 1824
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByUser__SWIG_0")]
		public static extern IntPtr new_RailPlatformNotifyEventJoinGameByUser__SWIG_0();

		// Token: 0x06000721 RID: 1825
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_set")]
		public static extern void RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000722 RID: 1826
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get")]
		public static extern IntPtr RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get(IntPtr jarg1);

		// Token: 0x06000723 RID: 1827
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_commandline_info_set")]
		public static extern void RailPlatformNotifyEventJoinGameByUser_commandline_info_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000724 RID: 1828
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_commandline_info_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPlatformNotifyEventJoinGameByUser_commandline_info_get(IntPtr jarg1);

		// Token: 0x06000725 RID: 1829
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlatformNotifyEventJoinGameByUser__SWIG_1")]
		public static extern IntPtr new_RailPlatformNotifyEventJoinGameByUser__SWIG_1(IntPtr jarg1);

		// Token: 0x06000726 RID: 1830
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailPlatformNotifyEventJoinGameByUser")]
		public static extern void delete_RailPlatformNotifyEventJoinGameByUser(IntPtr jarg1);

		// Token: 0x06000727 RID: 1831
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFinalize__SWIG_0")]
		public static extern IntPtr new_RailFinalize__SWIG_0();

		// Token: 0x06000728 RID: 1832
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFinalize__SWIG_1")]
		public static extern IntPtr new_RailFinalize__SWIG_1(IntPtr jarg1);

		// Token: 0x06000729 RID: 1833
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFinalize")]
		public static extern void delete_RailFinalize(IntPtr jarg1);

		// Token: 0x0600072A RID: 1834
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetInfo__SWIG_0")]
		public static extern IntPtr new_RailAssetInfo__SWIG_0();

		// Token: 0x0600072B RID: 1835
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_asset_id_set")]
		public static extern void RailAssetInfo_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600072C RID: 1836
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_asset_id_get")]
		public static extern ulong RailAssetInfo_asset_id_get(IntPtr jarg1);

		// Token: 0x0600072D RID: 1837
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_product_id_set")]
		public static extern void RailAssetInfo_product_id_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600072E RID: 1838
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_product_id_get")]
		public static extern uint RailAssetInfo_product_id_get(IntPtr jarg1);

		// Token: 0x0600072F RID: 1839
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_product_name_set")]
		public static extern void RailAssetInfo_product_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000730 RID: 1840
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_product_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailAssetInfo_product_name_get(IntPtr jarg1);

		// Token: 0x06000731 RID: 1841
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_position_set")]
		public static extern void RailAssetInfo_position_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000732 RID: 1842
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_position_get")]
		public static extern int RailAssetInfo_position_get(IntPtr jarg1);

		// Token: 0x06000733 RID: 1843
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_progress_set")]
		public static extern void RailAssetInfo_progress_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000734 RID: 1844
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_progress_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailAssetInfo_progress_get(IntPtr jarg1);

		// Token: 0x06000735 RID: 1845
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_quantity_set")]
		public static extern void RailAssetInfo_quantity_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000736 RID: 1846
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_quantity_get")]
		public static extern uint RailAssetInfo_quantity_get(IntPtr jarg1);

		// Token: 0x06000737 RID: 1847
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_state_set")]
		public static extern void RailAssetInfo_state_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000738 RID: 1848
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_state_get")]
		public static extern uint RailAssetInfo_state_get(IntPtr jarg1);

		// Token: 0x06000739 RID: 1849
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_flag_set")]
		public static extern void RailAssetInfo_flag_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600073A RID: 1850
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_flag_get")]
		public static extern uint RailAssetInfo_flag_get(IntPtr jarg1);

		// Token: 0x0600073B RID: 1851
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_origin_set")]
		public static extern void RailAssetInfo_origin_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600073C RID: 1852
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_origin_get")]
		public static extern uint RailAssetInfo_origin_get(IntPtr jarg1);

		// Token: 0x0600073D RID: 1853
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_expire_time_set")]
		public static extern void RailAssetInfo_expire_time_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600073E RID: 1854
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetInfo_expire_time_get")]
		public static extern uint RailAssetInfo_expire_time_get(IntPtr jarg1);

		// Token: 0x0600073F RID: 1855
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetInfo__SWIG_1")]
		public static extern IntPtr new_RailAssetInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000740 RID: 1856
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailAssetInfo")]
		public static extern void delete_RailAssetInfo(IntPtr jarg1);

		// Token: 0x06000741 RID: 1857
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetItem__SWIG_0")]
		public static extern IntPtr new_RailAssetItem__SWIG_0();

		// Token: 0x06000742 RID: 1858
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetItem_asset_id_set")]
		public static extern void RailAssetItem_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000743 RID: 1859
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetItem_asset_id_get")]
		public static extern ulong RailAssetItem_asset_id_get(IntPtr jarg1);

		// Token: 0x06000744 RID: 1860
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetItem_quantity_set")]
		public static extern void RailAssetItem_quantity_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000745 RID: 1861
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetItem_quantity_get")]
		public static extern uint RailAssetItem_quantity_get(IntPtr jarg1);

		// Token: 0x06000746 RID: 1862
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetItem__SWIG_1")]
		public static extern IntPtr new_RailAssetItem__SWIG_1(IntPtr jarg1);

		// Token: 0x06000747 RID: 1863
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailAssetItem")]
		public static extern void delete_RailAssetItem(IntPtr jarg1);

		// Token: 0x06000748 RID: 1864
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailProductItem__SWIG_0")]
		public static extern IntPtr new_RailProductItem__SWIG_0();

		// Token: 0x06000749 RID: 1865
		[DllImport("rail_api", EntryPoint = "CSharp_RailProductItem_product_id_set")]
		public static extern void RailProductItem_product_id_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600074A RID: 1866
		[DllImport("rail_api", EntryPoint = "CSharp_RailProductItem_product_id_get")]
		public static extern uint RailProductItem_product_id_get(IntPtr jarg1);

		// Token: 0x0600074B RID: 1867
		[DllImport("rail_api", EntryPoint = "CSharp_RailProductItem_quantity_set")]
		public static extern void RailProductItem_quantity_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600074C RID: 1868
		[DllImport("rail_api", EntryPoint = "CSharp_RailProductItem_quantity_get")]
		public static extern uint RailProductItem_quantity_get(IntPtr jarg1);

		// Token: 0x0600074D RID: 1869
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailProductItem__SWIG_1")]
		public static extern IntPtr new_RailProductItem__SWIG_1(IntPtr jarg1);

		// Token: 0x0600074E RID: 1870
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailProductItem")]
		public static extern void delete_RailProductItem(IntPtr jarg1);

		// Token: 0x0600074F RID: 1871
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetProperty__SWIG_0")]
		public static extern IntPtr new_RailAssetProperty__SWIG_0();

		// Token: 0x06000750 RID: 1872
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetProperty_asset_id_set")]
		public static extern void RailAssetProperty_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000751 RID: 1873
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetProperty_asset_id_get")]
		public static extern ulong RailAssetProperty_asset_id_get(IntPtr jarg1);

		// Token: 0x06000752 RID: 1874
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetProperty_position_set")]
		public static extern void RailAssetProperty_position_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000753 RID: 1875
		[DllImport("rail_api", EntryPoint = "CSharp_RailAssetProperty_position_get")]
		public static extern uint RailAssetProperty_position_get(IntPtr jarg1);

		// Token: 0x06000754 RID: 1876
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailAssetProperty__SWIG_1")]
		public static extern IntPtr new_RailAssetProperty__SWIG_1(IntPtr jarg1);

		// Token: 0x06000755 RID: 1877
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailAssetProperty")]
		public static extern void delete_RailAssetProperty(IntPtr jarg1);

		// Token: 0x06000756 RID: 1878
		[DllImport("rail_api", EntryPoint = "CSharp_new_RequestAllAssetsFinished__SWIG_0")]
		public static extern IntPtr new_RequestAllAssetsFinished__SWIG_0();

		// Token: 0x06000757 RID: 1879
		[DllImport("rail_api", EntryPoint = "CSharp_RequestAllAssetsFinished_assetinfo_list_set")]
		public static extern void RequestAllAssetsFinished_assetinfo_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000758 RID: 1880
		[DllImport("rail_api", EntryPoint = "CSharp_RequestAllAssetsFinished_assetinfo_list_get")]
		public static extern IntPtr RequestAllAssetsFinished_assetinfo_list_get(IntPtr jarg1);

		// Token: 0x06000759 RID: 1881
		[DllImport("rail_api", EntryPoint = "CSharp_new_RequestAllAssetsFinished__SWIG_1")]
		public static extern IntPtr new_RequestAllAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600075A RID: 1882
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RequestAllAssetsFinished")]
		public static extern void delete_RequestAllAssetsFinished(IntPtr jarg1);

		// Token: 0x0600075B RID: 1883
		[DllImport("rail_api", EntryPoint = "CSharp_new_UpdateAssetsPropertyFinished__SWIG_0")]
		public static extern IntPtr new_UpdateAssetsPropertyFinished__SWIG_0();

		// Token: 0x0600075C RID: 1884
		[DllImport("rail_api", EntryPoint = "CSharp_UpdateAssetsPropertyFinished_asset_property_list_set")]
		public static extern void UpdateAssetsPropertyFinished_asset_property_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600075D RID: 1885
		[DllImport("rail_api", EntryPoint = "CSharp_UpdateAssetsPropertyFinished_asset_property_list_get")]
		public static extern IntPtr UpdateAssetsPropertyFinished_asset_property_list_get(IntPtr jarg1);

		// Token: 0x0600075E RID: 1886
		[DllImport("rail_api", EntryPoint = "CSharp_new_UpdateAssetsPropertyFinished__SWIG_1")]
		public static extern IntPtr new_UpdateAssetsPropertyFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600075F RID: 1887
		[DllImport("rail_api", EntryPoint = "CSharp_delete_UpdateAssetsPropertyFinished")]
		public static extern void delete_UpdateAssetsPropertyFinished(IntPtr jarg1);

		// Token: 0x06000760 RID: 1888
		[DllImport("rail_api", EntryPoint = "CSharp_new_DirectConsumeAssetsFinished__SWIG_0")]
		public static extern IntPtr new_DirectConsumeAssetsFinished__SWIG_0();

		// Token: 0x06000761 RID: 1889
		[DllImport("rail_api", EntryPoint = "CSharp_DirectConsumeAssetsFinished_assets_set")]
		public static extern void DirectConsumeAssetsFinished_assets_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000762 RID: 1890
		[DllImport("rail_api", EntryPoint = "CSharp_DirectConsumeAssetsFinished_assets_get")]
		public static extern IntPtr DirectConsumeAssetsFinished_assets_get(IntPtr jarg1);

		// Token: 0x06000763 RID: 1891
		[DllImport("rail_api", EntryPoint = "CSharp_new_DirectConsumeAssetsFinished__SWIG_1")]
		public static extern IntPtr new_DirectConsumeAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000764 RID: 1892
		[DllImport("rail_api", EntryPoint = "CSharp_delete_DirectConsumeAssetsFinished")]
		public static extern void delete_DirectConsumeAssetsFinished(IntPtr jarg1);

		// Token: 0x06000765 RID: 1893
		[DllImport("rail_api", EntryPoint = "CSharp_new_StartConsumeAssetsFinished__SWIG_0")]
		public static extern IntPtr new_StartConsumeAssetsFinished__SWIG_0();

		// Token: 0x06000766 RID: 1894
		[DllImport("rail_api", EntryPoint = "CSharp_StartConsumeAssetsFinished_asset_id_set")]
		public static extern void StartConsumeAssetsFinished_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000767 RID: 1895
		[DllImport("rail_api", EntryPoint = "CSharp_StartConsumeAssetsFinished_asset_id_get")]
		public static extern ulong StartConsumeAssetsFinished_asset_id_get(IntPtr jarg1);

		// Token: 0x06000768 RID: 1896
		[DllImport("rail_api", EntryPoint = "CSharp_new_StartConsumeAssetsFinished__SWIG_1")]
		public static extern IntPtr new_StartConsumeAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000769 RID: 1897
		[DllImport("rail_api", EntryPoint = "CSharp_delete_StartConsumeAssetsFinished")]
		public static extern void delete_StartConsumeAssetsFinished(IntPtr jarg1);

		// Token: 0x0600076A RID: 1898
		[DllImport("rail_api", EntryPoint = "CSharp_new_UpdateConsumeAssetsFinished__SWIG_0")]
		public static extern IntPtr new_UpdateConsumeAssetsFinished__SWIG_0();

		// Token: 0x0600076B RID: 1899
		[DllImport("rail_api", EntryPoint = "CSharp_UpdateConsumeAssetsFinished_asset_id_set")]
		public static extern void UpdateConsumeAssetsFinished_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600076C RID: 1900
		[DllImport("rail_api", EntryPoint = "CSharp_UpdateConsumeAssetsFinished_asset_id_get")]
		public static extern ulong UpdateConsumeAssetsFinished_asset_id_get(IntPtr jarg1);

		// Token: 0x0600076D RID: 1901
		[DllImport("rail_api", EntryPoint = "CSharp_new_UpdateConsumeAssetsFinished__SWIG_1")]
		public static extern IntPtr new_UpdateConsumeAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600076E RID: 1902
		[DllImport("rail_api", EntryPoint = "CSharp_delete_UpdateConsumeAssetsFinished")]
		public static extern void delete_UpdateConsumeAssetsFinished(IntPtr jarg1);

		// Token: 0x0600076F RID: 1903
		[DllImport("rail_api", EntryPoint = "CSharp_new_CompleteConsumeAssetsFinished__SWIG_0")]
		public static extern IntPtr new_CompleteConsumeAssetsFinished__SWIG_0();

		// Token: 0x06000770 RID: 1904
		[DllImport("rail_api", EntryPoint = "CSharp_CompleteConsumeAssetsFinished_asset_item_set")]
		public static extern void CompleteConsumeAssetsFinished_asset_item_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000771 RID: 1905
		[DllImport("rail_api", EntryPoint = "CSharp_CompleteConsumeAssetsFinished_asset_item_get")]
		public static extern IntPtr CompleteConsumeAssetsFinished_asset_item_get(IntPtr jarg1);

		// Token: 0x06000772 RID: 1906
		[DllImport("rail_api", EntryPoint = "CSharp_new_CompleteConsumeAssetsFinished__SWIG_1")]
		public static extern IntPtr new_CompleteConsumeAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000773 RID: 1907
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CompleteConsumeAssetsFinished")]
		public static extern void delete_CompleteConsumeAssetsFinished(IntPtr jarg1);

		// Token: 0x06000774 RID: 1908
		[DllImport("rail_api", EntryPoint = "CSharp_new_SplitAssetsFinished__SWIG_0")]
		public static extern IntPtr new_SplitAssetsFinished__SWIG_0();

		// Token: 0x06000775 RID: 1909
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_source_asset_set")]
		public static extern void SplitAssetsFinished_source_asset_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000776 RID: 1910
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_source_asset_get")]
		public static extern ulong SplitAssetsFinished_source_asset_get(IntPtr jarg1);

		// Token: 0x06000777 RID: 1911
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_to_quantity_set")]
		public static extern void SplitAssetsFinished_to_quantity_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000778 RID: 1912
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_to_quantity_get")]
		public static extern uint SplitAssetsFinished_to_quantity_get(IntPtr jarg1);

		// Token: 0x06000779 RID: 1913
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_new_asset_id_set")]
		public static extern void SplitAssetsFinished_new_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600077A RID: 1914
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_new_asset_id_get")]
		public static extern ulong SplitAssetsFinished_new_asset_id_get(IntPtr jarg1);

		// Token: 0x0600077B RID: 1915
		[DllImport("rail_api", EntryPoint = "CSharp_new_SplitAssetsFinished__SWIG_1")]
		public static extern IntPtr new_SplitAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600077C RID: 1916
		[DllImport("rail_api", EntryPoint = "CSharp_delete_SplitAssetsFinished")]
		public static extern void delete_SplitAssetsFinished(IntPtr jarg1);

		// Token: 0x0600077D RID: 1917
		[DllImport("rail_api", EntryPoint = "CSharp_new_SplitAssetsToFinished__SWIG_0")]
		public static extern IntPtr new_SplitAssetsToFinished__SWIG_0();

		// Token: 0x0600077E RID: 1918
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_source_asset_set")]
		public static extern void SplitAssetsToFinished_source_asset_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600077F RID: 1919
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_source_asset_get")]
		public static extern ulong SplitAssetsToFinished_source_asset_get(IntPtr jarg1);

		// Token: 0x06000780 RID: 1920
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_to_quantity_set")]
		public static extern void SplitAssetsToFinished_to_quantity_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000781 RID: 1921
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_to_quantity_get")]
		public static extern uint SplitAssetsToFinished_to_quantity_get(IntPtr jarg1);

		// Token: 0x06000782 RID: 1922
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_split_to_asset_id_set")]
		public static extern void SplitAssetsToFinished_split_to_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000783 RID: 1923
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_split_to_asset_id_get")]
		public static extern ulong SplitAssetsToFinished_split_to_asset_id_get(IntPtr jarg1);

		// Token: 0x06000784 RID: 1924
		[DllImport("rail_api", EntryPoint = "CSharp_new_SplitAssetsToFinished__SWIG_1")]
		public static extern IntPtr new_SplitAssetsToFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000785 RID: 1925
		[DllImport("rail_api", EntryPoint = "CSharp_delete_SplitAssetsToFinished")]
		public static extern void delete_SplitAssetsToFinished(IntPtr jarg1);

		// Token: 0x06000786 RID: 1926
		[DllImport("rail_api", EntryPoint = "CSharp_new_MergeAssetsFinished__SWIG_0")]
		public static extern IntPtr new_MergeAssetsFinished__SWIG_0();

		// Token: 0x06000787 RID: 1927
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_source_assets_set")]
		public static extern void MergeAssetsFinished_source_assets_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000788 RID: 1928
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_source_assets_get")]
		public static extern IntPtr MergeAssetsFinished_source_assets_get(IntPtr jarg1);

		// Token: 0x06000789 RID: 1929
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_new_asset_id_set")]
		public static extern void MergeAssetsFinished_new_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600078A RID: 1930
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_new_asset_id_get")]
		public static extern ulong MergeAssetsFinished_new_asset_id_get(IntPtr jarg1);

		// Token: 0x0600078B RID: 1931
		[DllImport("rail_api", EntryPoint = "CSharp_new_MergeAssetsFinished__SWIG_1")]
		public static extern IntPtr new_MergeAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600078C RID: 1932
		[DllImport("rail_api", EntryPoint = "CSharp_delete_MergeAssetsFinished")]
		public static extern void delete_MergeAssetsFinished(IntPtr jarg1);

		// Token: 0x0600078D RID: 1933
		[DllImport("rail_api", EntryPoint = "CSharp_new_MergeAssetsToFinished__SWIG_0")]
		public static extern IntPtr new_MergeAssetsToFinished__SWIG_0();

		// Token: 0x0600078E RID: 1934
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_source_assets_set")]
		public static extern void MergeAssetsToFinished_source_assets_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600078F RID: 1935
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_source_assets_get")]
		public static extern IntPtr MergeAssetsToFinished_source_assets_get(IntPtr jarg1);

		// Token: 0x06000790 RID: 1936
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_merge_to_asset_id_set")]
		public static extern void MergeAssetsToFinished_merge_to_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000791 RID: 1937
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_merge_to_asset_id_get")]
		public static extern ulong MergeAssetsToFinished_merge_to_asset_id_get(IntPtr jarg1);

		// Token: 0x06000792 RID: 1938
		[DllImport("rail_api", EntryPoint = "CSharp_new_MergeAssetsToFinished__SWIG_1")]
		public static extern IntPtr new_MergeAssetsToFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000793 RID: 1939
		[DllImport("rail_api", EntryPoint = "CSharp_delete_MergeAssetsToFinished")]
		public static extern void delete_MergeAssetsToFinished(IntPtr jarg1);

		// Token: 0x06000794 RID: 1940
		[DllImport("rail_api", EntryPoint = "CSharp_new_CompleteConsumeByExchangeAssetsToFinished__SWIG_0")]
		public static extern IntPtr new_CompleteConsumeByExchangeAssetsToFinished__SWIG_0();

		// Token: 0x06000795 RID: 1941
		[DllImport("rail_api", EntryPoint = "CSharp_new_CompleteConsumeByExchangeAssetsToFinished__SWIG_1")]
		public static extern IntPtr new_CompleteConsumeByExchangeAssetsToFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000796 RID: 1942
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CompleteConsumeByExchangeAssetsToFinished")]
		public static extern void delete_CompleteConsumeByExchangeAssetsToFinished(IntPtr jarg1);

		// Token: 0x06000797 RID: 1943
		[DllImport("rail_api", EntryPoint = "CSharp_new_ExchangeAssetsFinished__SWIG_0")]
		public static extern IntPtr new_ExchangeAssetsFinished__SWIG_0();

		// Token: 0x06000798 RID: 1944
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_old_assets_set")]
		public static extern void ExchangeAssetsFinished_old_assets_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000799 RID: 1945
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_old_assets_get")]
		public static extern IntPtr ExchangeAssetsFinished_old_assets_get(IntPtr jarg1);

		// Token: 0x0600079A RID: 1946
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_to_product_info_set")]
		public static extern void ExchangeAssetsFinished_to_product_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600079B RID: 1947
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_to_product_info_get")]
		public static extern IntPtr ExchangeAssetsFinished_to_product_info_get(IntPtr jarg1);

		// Token: 0x0600079C RID: 1948
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_new_asset_id_set")]
		public static extern void ExchangeAssetsFinished_new_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600079D RID: 1949
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_new_asset_id_get")]
		public static extern ulong ExchangeAssetsFinished_new_asset_id_get(IntPtr jarg1);

		// Token: 0x0600079E RID: 1950
		[DllImport("rail_api", EntryPoint = "CSharp_new_ExchangeAssetsFinished__SWIG_1")]
		public static extern IntPtr new_ExchangeAssetsFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x0600079F RID: 1951
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ExchangeAssetsFinished")]
		public static extern void delete_ExchangeAssetsFinished(IntPtr jarg1);

		// Token: 0x060007A0 RID: 1952
		[DllImport("rail_api", EntryPoint = "CSharp_new_ExchangeAssetsToFinished__SWIG_0")]
		public static extern IntPtr new_ExchangeAssetsToFinished__SWIG_0();

		// Token: 0x060007A1 RID: 1953
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_old_assets_set")]
		public static extern void ExchangeAssetsToFinished_old_assets_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060007A2 RID: 1954
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_old_assets_get")]
		public static extern IntPtr ExchangeAssetsToFinished_old_assets_get(IntPtr jarg1);

		// Token: 0x060007A3 RID: 1955
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_to_product_info_set")]
		public static extern void ExchangeAssetsToFinished_to_product_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060007A4 RID: 1956
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_to_product_info_get")]
		public static extern IntPtr ExchangeAssetsToFinished_to_product_info_get(IntPtr jarg1);

		// Token: 0x060007A5 RID: 1957
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_exchange_to_asset_id_set")]
		public static extern void ExchangeAssetsToFinished_exchange_to_asset_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x060007A6 RID: 1958
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_exchange_to_asset_id_get")]
		public static extern ulong ExchangeAssetsToFinished_exchange_to_asset_id_get(IntPtr jarg1);

		// Token: 0x060007A7 RID: 1959
		[DllImport("rail_api", EntryPoint = "CSharp_new_ExchangeAssetsToFinished__SWIG_1")]
		public static extern IntPtr new_ExchangeAssetsToFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x060007A8 RID: 1960
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ExchangeAssetsToFinished")]
		public static extern void delete_ExchangeAssetsToFinished(IntPtr jarg1);

		// Token: 0x060007A9 RID: 1961
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateBrowserOptions__SWIG_0")]
		public static extern IntPtr new_CreateBrowserOptions__SWIG_0();

		// Token: 0x060007AA RID: 1962
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_maximum_button_set")]
		public static extern void CreateBrowserOptions_has_maximum_button_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060007AB RID: 1963
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_maximum_button_get")]
		public static extern bool CreateBrowserOptions_has_maximum_button_get(IntPtr jarg1);

		// Token: 0x060007AC RID: 1964
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_minimum_button_set")]
		public static extern void CreateBrowserOptions_has_minimum_button_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060007AD RID: 1965
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_minimum_button_get")]
		public static extern bool CreateBrowserOptions_has_minimum_button_get(IntPtr jarg1);

		// Token: 0x060007AE RID: 1966
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_border_set")]
		public static extern void CreateBrowserOptions_has_border_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060007AF RID: 1967
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_has_border_get")]
		public static extern bool CreateBrowserOptions_has_border_get(IntPtr jarg1);

		// Token: 0x060007B0 RID: 1968
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_is_movable_set")]
		public static extern void CreateBrowserOptions_is_movable_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060007B1 RID: 1969
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_is_movable_get")]
		public static extern bool CreateBrowserOptions_is_movable_get(IntPtr jarg1);

		// Token: 0x060007B2 RID: 1970
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_margin_top_set")]
		public static extern void CreateBrowserOptions_margin_top_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007B3 RID: 1971
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_margin_top_get")]
		public static extern int CreateBrowserOptions_margin_top_get(IntPtr jarg1);

		// Token: 0x060007B4 RID: 1972
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_margin_left_set")]
		public static extern void CreateBrowserOptions_margin_left_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007B5 RID: 1973
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserOptions_margin_left_get")]
		public static extern int CreateBrowserOptions_margin_left_get(IntPtr jarg1);

		// Token: 0x060007B6 RID: 1974
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateBrowserOptions__SWIG_1")]
		public static extern IntPtr new_CreateBrowserOptions__SWIG_1(IntPtr jarg1);

		// Token: 0x060007B7 RID: 1975
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateBrowserOptions")]
		public static extern void delete_CreateBrowserOptions(IntPtr jarg1);

		// Token: 0x060007B8 RID: 1976
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateCustomerDrawBrowserOptions__SWIG_0")]
		public static extern IntPtr new_CreateCustomerDrawBrowserOptions__SWIG_0();

		// Token: 0x060007B9 RID: 1977
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_offset_x_set")]
		public static extern void CreateCustomerDrawBrowserOptions_content_offset_x_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007BA RID: 1978
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_offset_x_get")]
		public static extern int CreateCustomerDrawBrowserOptions_content_offset_x_get(IntPtr jarg1);

		// Token: 0x060007BB RID: 1979
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_offset_y_set")]
		public static extern void CreateCustomerDrawBrowserOptions_content_offset_y_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007BC RID: 1980
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_offset_y_get")]
		public static extern int CreateCustomerDrawBrowserOptions_content_offset_y_get(IntPtr jarg1);

		// Token: 0x060007BD RID: 1981
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_window_width_set")]
		public static extern void CreateCustomerDrawBrowserOptions_content_window_width_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007BE RID: 1982
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_window_width_get")]
		public static extern uint CreateCustomerDrawBrowserOptions_content_window_width_get(IntPtr jarg1);

		// Token: 0x060007BF RID: 1983
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_window_height_set")]
		public static extern void CreateCustomerDrawBrowserOptions_content_window_height_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007C0 RID: 1984
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_content_window_height_get")]
		public static extern uint CreateCustomerDrawBrowserOptions_content_window_height_get(IntPtr jarg1);

		// Token: 0x060007C1 RID: 1985
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_has_scroll_set")]
		public static extern void CreateCustomerDrawBrowserOptions_has_scroll_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060007C2 RID: 1986
		[DllImport("rail_api", EntryPoint = "CSharp_CreateCustomerDrawBrowserOptions_has_scroll_get")]
		public static extern bool CreateCustomerDrawBrowserOptions_has_scroll_get(IntPtr jarg1);

		// Token: 0x060007C3 RID: 1987
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateCustomerDrawBrowserOptions__SWIG_1")]
		public static extern IntPtr new_CreateCustomerDrawBrowserOptions__SWIG_1(IntPtr jarg1);

		// Token: 0x060007C4 RID: 1988
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateCustomerDrawBrowserOptions")]
		public static extern void delete_CreateCustomerDrawBrowserOptions(IntPtr jarg1);

		// Token: 0x060007C5 RID: 1989
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateBrowserResult__SWIG_0")]
		public static extern IntPtr new_CreateBrowserResult__SWIG_0();

		// Token: 0x060007C6 RID: 1990
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateBrowserResult__SWIG_1")]
		public static extern IntPtr new_CreateBrowserResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060007C7 RID: 1991
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateBrowserResult")]
		public static extern void delete_CreateBrowserResult(IntPtr jarg1);

		// Token: 0x060007C8 RID: 1992
		[DllImport("rail_api", EntryPoint = "CSharp_new_ReloadBrowserResult__SWIG_0")]
		public static extern IntPtr new_ReloadBrowserResult__SWIG_0();

		// Token: 0x060007C9 RID: 1993
		[DllImport("rail_api", EntryPoint = "CSharp_new_ReloadBrowserResult__SWIG_1")]
		public static extern IntPtr new_ReloadBrowserResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060007CA RID: 1994
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ReloadBrowserResult")]
		public static extern void delete_ReloadBrowserResult(IntPtr jarg1);

		// Token: 0x060007CB RID: 1995
		[DllImport("rail_api", EntryPoint = "CSharp_new_CloseBrowserResult__SWIG_0")]
		public static extern IntPtr new_CloseBrowserResult__SWIG_0();

		// Token: 0x060007CC RID: 1996
		[DllImport("rail_api", EntryPoint = "CSharp_new_CloseBrowserResult__SWIG_1")]
		public static extern IntPtr new_CloseBrowserResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060007CD RID: 1997
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CloseBrowserResult")]
		public static extern void delete_CloseBrowserResult(IntPtr jarg1);

		// Token: 0x060007CE RID: 1998
		[DllImport("rail_api", EntryPoint = "CSharp_new_JavascriptEventResult__SWIG_0")]
		public static extern IntPtr new_JavascriptEventResult__SWIG_0();

		// Token: 0x060007CF RID: 1999
		[DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_event_name_set")]
		public static extern void JavascriptEventResult_event_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060007D0 RID: 2000
		[DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_event_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string JavascriptEventResult_event_name_get(IntPtr jarg1);

		// Token: 0x060007D1 RID: 2001
		[DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_event_value_set")]
		public static extern void JavascriptEventResult_event_value_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060007D2 RID: 2002
		[DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_event_value_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string JavascriptEventResult_event_value_get(IntPtr jarg1);

		// Token: 0x060007D3 RID: 2003
		[DllImport("rail_api", EntryPoint = "CSharp_new_JavascriptEventResult__SWIG_1")]
		public static extern IntPtr new_JavascriptEventResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060007D4 RID: 2004
		[DllImport("rail_api", EntryPoint = "CSharp_delete_JavascriptEventResult")]
		public static extern void delete_JavascriptEventResult(IntPtr jarg1);

		// Token: 0x060007D5 RID: 2005
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserNeedsPaintRequest__SWIG_0")]
		public static extern IntPtr new_BrowserNeedsPaintRequest__SWIG_0();

		// Token: 0x060007D6 RID: 2006
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_data_set")]
		public static extern void BrowserNeedsPaintRequest_bgra_data_set(IntPtr jarg1, string jarg2);

		// Token: 0x060007D7 RID: 2007
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_data_get")]
		public static extern string BrowserNeedsPaintRequest_bgra_data_get(IntPtr jarg1);

		// Token: 0x060007D8 RID: 2008
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_offset_x_set")]
		public static extern void BrowserNeedsPaintRequest_offset_x_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007D9 RID: 2009
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_offset_x_get")]
		public static extern int BrowserNeedsPaintRequest_offset_x_get(IntPtr jarg1);

		// Token: 0x060007DA RID: 2010
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_offset_y_set")]
		public static extern void BrowserNeedsPaintRequest_offset_y_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007DB RID: 2011
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_offset_y_get")]
		public static extern int BrowserNeedsPaintRequest_offset_y_get(IntPtr jarg1);

		// Token: 0x060007DC RID: 2012
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_width_set")]
		public static extern void BrowserNeedsPaintRequest_bgra_width_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007DD RID: 2013
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_width_get")]
		public static extern uint BrowserNeedsPaintRequest_bgra_width_get(IntPtr jarg1);

		// Token: 0x060007DE RID: 2014
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_height_set")]
		public static extern void BrowserNeedsPaintRequest_bgra_height_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007DF RID: 2015
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_bgra_height_get")]
		public static extern uint BrowserNeedsPaintRequest_bgra_height_get(IntPtr jarg1);

		// Token: 0x060007E0 RID: 2016
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_scroll_x_pos_set")]
		public static extern void BrowserNeedsPaintRequest_scroll_x_pos_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007E1 RID: 2017
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_scroll_x_pos_get")]
		public static extern uint BrowserNeedsPaintRequest_scroll_x_pos_get(IntPtr jarg1);

		// Token: 0x060007E2 RID: 2018
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_scroll_y_pos_set")]
		public static extern void BrowserNeedsPaintRequest_scroll_y_pos_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007E3 RID: 2019
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_scroll_y_pos_get")]
		public static extern uint BrowserNeedsPaintRequest_scroll_y_pos_get(IntPtr jarg1);

		// Token: 0x060007E4 RID: 2020
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_page_scale_factor_set")]
		public static extern void BrowserNeedsPaintRequest_page_scale_factor_set(IntPtr jarg1, float jarg2);

		// Token: 0x060007E5 RID: 2021
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_page_scale_factor_get")]
		public static extern float BrowserNeedsPaintRequest_page_scale_factor_get(IntPtr jarg1);

		// Token: 0x060007E6 RID: 2022
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserNeedsPaintRequest__SWIG_1")]
		public static extern IntPtr new_BrowserNeedsPaintRequest__SWIG_1(IntPtr jarg1);

		// Token: 0x060007E7 RID: 2023
		[DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserNeedsPaintRequest")]
		public static extern void delete_BrowserNeedsPaintRequest(IntPtr jarg1);

		// Token: 0x060007E8 RID: 2024
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserDamageRectNeedsPaintRequest__SWIG_0")]
		public static extern IntPtr new_BrowserDamageRectNeedsPaintRequest__SWIG_0();

		// Token: 0x060007E9 RID: 2025
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_data_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_bgra_data_set(IntPtr jarg1, string jarg2);

		// Token: 0x060007EA RID: 2026
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_data_get")]
		public static extern string BrowserDamageRectNeedsPaintRequest_bgra_data_get(IntPtr jarg1);

		// Token: 0x060007EB RID: 2027
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_offset_x_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_offset_x_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007EC RID: 2028
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_offset_x_get")]
		public static extern int BrowserDamageRectNeedsPaintRequest_offset_x_get(IntPtr jarg1);

		// Token: 0x060007ED RID: 2029
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_offset_y_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_offset_y_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007EE RID: 2030
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_offset_y_get")]
		public static extern int BrowserDamageRectNeedsPaintRequest_offset_y_get(IntPtr jarg1);

		// Token: 0x060007EF RID: 2031
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_width_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_bgra_width_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007F0 RID: 2032
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_width_get")]
		public static extern uint BrowserDamageRectNeedsPaintRequest_bgra_width_get(IntPtr jarg1);

		// Token: 0x060007F1 RID: 2033
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_height_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_bgra_height_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007F2 RID: 2034
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_bgra_height_get")]
		public static extern uint BrowserDamageRectNeedsPaintRequest_bgra_height_get(IntPtr jarg1);

		// Token: 0x060007F3 RID: 2035
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_offset_x_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_update_offset_x_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007F4 RID: 2036
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_offset_x_get")]
		public static extern int BrowserDamageRectNeedsPaintRequest_update_offset_x_get(IntPtr jarg1);

		// Token: 0x060007F5 RID: 2037
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_offset_y_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_update_offset_y_set(IntPtr jarg1, int jarg2);

		// Token: 0x060007F6 RID: 2038
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_offset_y_get")]
		public static extern int BrowserDamageRectNeedsPaintRequest_update_offset_y_get(IntPtr jarg1);

		// Token: 0x060007F7 RID: 2039
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_bgra_width_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_update_bgra_width_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007F8 RID: 2040
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_bgra_width_get")]
		public static extern uint BrowserDamageRectNeedsPaintRequest_update_bgra_width_get(IntPtr jarg1);

		// Token: 0x060007F9 RID: 2041
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_bgra_height_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_update_bgra_height_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007FA RID: 2042
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_update_bgra_height_get")]
		public static extern uint BrowserDamageRectNeedsPaintRequest_update_bgra_height_get(IntPtr jarg1);

		// Token: 0x060007FB RID: 2043
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_scroll_x_pos_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_scroll_x_pos_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007FC RID: 2044
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_scroll_x_pos_get")]
		public static extern uint BrowserDamageRectNeedsPaintRequest_scroll_x_pos_get(IntPtr jarg1);

		// Token: 0x060007FD RID: 2045
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_scroll_y_pos_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_scroll_y_pos_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060007FE RID: 2046
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_scroll_y_pos_get")]
		public static extern uint BrowserDamageRectNeedsPaintRequest_scroll_y_pos_get(IntPtr jarg1);

		// Token: 0x060007FF RID: 2047
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_page_scale_factor_set")]
		public static extern void BrowserDamageRectNeedsPaintRequest_page_scale_factor_set(IntPtr jarg1, float jarg2);

		// Token: 0x06000800 RID: 2048
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_page_scale_factor_get")]
		public static extern float BrowserDamageRectNeedsPaintRequest_page_scale_factor_get(IntPtr jarg1);

		// Token: 0x06000801 RID: 2049
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserDamageRectNeedsPaintRequest__SWIG_1")]
		public static extern IntPtr new_BrowserDamageRectNeedsPaintRequest__SWIG_1(IntPtr jarg1);

		// Token: 0x06000802 RID: 2050
		[DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserDamageRectNeedsPaintRequest")]
		public static extern void delete_BrowserDamageRectNeedsPaintRequest(IntPtr jarg1);

		// Token: 0x06000803 RID: 2051
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderNavigateResult__SWIG_0")]
		public static extern IntPtr new_BrowserRenderNavigateResult__SWIG_0();

		// Token: 0x06000804 RID: 2052
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderNavigateResult_url_set")]
		public static extern void BrowserRenderNavigateResult_url_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000805 RID: 2053
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderNavigateResult_url_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string BrowserRenderNavigateResult_url_get(IntPtr jarg1);

		// Token: 0x06000806 RID: 2054
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderNavigateResult__SWIG_1")]
		public static extern IntPtr new_BrowserRenderNavigateResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000807 RID: 2055
		[DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserRenderNavigateResult")]
		public static extern void delete_BrowserRenderNavigateResult(IntPtr jarg1);

		// Token: 0x06000808 RID: 2056
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderStateChanged__SWIG_0")]
		public static extern IntPtr new_BrowserRenderStateChanged__SWIG_0();

		// Token: 0x06000809 RID: 2057
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_can_go_back_set")]
		public static extern void BrowserRenderStateChanged_can_go_back_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600080A RID: 2058
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_can_go_back_get")]
		public static extern bool BrowserRenderStateChanged_can_go_back_get(IntPtr jarg1);

		// Token: 0x0600080B RID: 2059
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_can_go_forward_set")]
		public static extern void BrowserRenderStateChanged_can_go_forward_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600080C RID: 2060
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_can_go_forward_get")]
		public static extern bool BrowserRenderStateChanged_can_go_forward_get(IntPtr jarg1);

		// Token: 0x0600080D RID: 2061
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderStateChanged__SWIG_1")]
		public static extern IntPtr new_BrowserRenderStateChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x0600080E RID: 2062
		[DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserRenderStateChanged")]
		public static extern void delete_BrowserRenderStateChanged(IntPtr jarg1);

		// Token: 0x0600080F RID: 2063
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderTitleChanged__SWIG_0")]
		public static extern IntPtr new_BrowserRenderTitleChanged__SWIG_0();

		// Token: 0x06000810 RID: 2064
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderTitleChanged_new_title_set")]
		public static extern void BrowserRenderTitleChanged_new_title_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000811 RID: 2065
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderTitleChanged_new_title_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string BrowserRenderTitleChanged_new_title_get(IntPtr jarg1);

		// Token: 0x06000812 RID: 2066
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserRenderTitleChanged__SWIG_1")]
		public static extern IntPtr new_BrowserRenderTitleChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x06000813 RID: 2067
		[DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserRenderTitleChanged")]
		public static extern void delete_BrowserRenderTitleChanged(IntPtr jarg1);

		// Token: 0x06000814 RID: 2068
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserTryNavigateNewPageRequest__SWIG_0")]
		public static extern IntPtr new_BrowserTryNavigateNewPageRequest__SWIG_0();

		// Token: 0x06000815 RID: 2069
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_url_set")]
		public static extern void BrowserTryNavigateNewPageRequest_url_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000816 RID: 2070
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_url_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string BrowserTryNavigateNewPageRequest_url_get(IntPtr jarg1);

		// Token: 0x06000817 RID: 2071
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_target_type_set")]
		public static extern void BrowserTryNavigateNewPageRequest_target_type_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000818 RID: 2072
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_target_type_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string BrowserTryNavigateNewPageRequest_target_type_get(IntPtr jarg1);

		// Token: 0x06000819 RID: 2073
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_is_redirect_request_set")]
		public static extern void BrowserTryNavigateNewPageRequest_is_redirect_request_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600081A RID: 2074
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_is_redirect_request_get")]
		public static extern bool BrowserTryNavigateNewPageRequest_is_redirect_request_get(IntPtr jarg1);

		// Token: 0x0600081B RID: 2075
		[DllImport("rail_api", EntryPoint = "CSharp_new_BrowserTryNavigateNewPageRequest__SWIG_1")]
		public static extern IntPtr new_BrowserTryNavigateNewPageRequest__SWIG_1(IntPtr jarg1);

		// Token: 0x0600081C RID: 2076
		[DllImport("rail_api", EntryPoint = "CSharp_delete_BrowserTryNavigateNewPageRequest")]
		public static extern void delete_BrowserTryNavigateNewPageRequest(IntPtr jarg1);

		// Token: 0x0600081D RID: 2077
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_dlc_id_set")]
		public static extern void RailDlcInfo_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600081E RID: 2078
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_dlc_id_get")]
		public static extern IntPtr RailDlcInfo_dlc_id_get(IntPtr jarg1);

		// Token: 0x0600081F RID: 2079
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_game_id_set")]
		public static extern void RailDlcInfo_game_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000820 RID: 2080
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_game_id_get")]
		public static extern IntPtr RailDlcInfo_game_id_get(IntPtr jarg1);

		// Token: 0x06000821 RID: 2081
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_version_set")]
		public static extern void RailDlcInfo_version_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000822 RID: 2082
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_version_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailDlcInfo_version_get(IntPtr jarg1);

		// Token: 0x06000823 RID: 2083
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_name_set")]
		public static extern void RailDlcInfo_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000824 RID: 2084
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailDlcInfo_name_get(IntPtr jarg1);

		// Token: 0x06000825 RID: 2085
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_description_set")]
		public static extern void RailDlcInfo_description_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000826 RID: 2086
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_description_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailDlcInfo_description_get(IntPtr jarg1);

		// Token: 0x06000827 RID: 2087
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_original_price_set")]
		public static extern void RailDlcInfo_original_price_set(IntPtr jarg1, double jarg2);

		// Token: 0x06000828 RID: 2088
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_original_price_get")]
		public static extern double RailDlcInfo_original_price_get(IntPtr jarg1);

		// Token: 0x06000829 RID: 2089
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_discount_price_set")]
		public static extern void RailDlcInfo_discount_price_set(IntPtr jarg1, double jarg2);

		// Token: 0x0600082A RID: 2090
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInfo_discount_price_get")]
		public static extern double RailDlcInfo_discount_price_get(IntPtr jarg1);

		// Token: 0x0600082B RID: 2091
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcInfo__SWIG_0")]
		public static extern IntPtr new_RailDlcInfo__SWIG_0();

		// Token: 0x0600082C RID: 2092
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcInfo__SWIG_1")]
		public static extern IntPtr new_RailDlcInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x0600082D RID: 2093
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailDlcInfo")]
		public static extern void delete_RailDlcInfo(IntPtr jarg1);

		// Token: 0x0600082E RID: 2094
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_progress_set")]
		public static extern void RailDlcInstallProgress_progress_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600082F RID: 2095
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_progress_get")]
		public static extern uint RailDlcInstallProgress_progress_get(IntPtr jarg1);

		// Token: 0x06000830 RID: 2096
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_finished_bytes_set")]
		public static extern void RailDlcInstallProgress_finished_bytes_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000831 RID: 2097
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_finished_bytes_get")]
		public static extern ulong RailDlcInstallProgress_finished_bytes_get(IntPtr jarg1);

		// Token: 0x06000832 RID: 2098
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_total_bytes_set")]
		public static extern void RailDlcInstallProgress_total_bytes_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000833 RID: 2099
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_total_bytes_get")]
		public static extern ulong RailDlcInstallProgress_total_bytes_get(IntPtr jarg1);

		// Token: 0x06000834 RID: 2100
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_speed_set")]
		public static extern void RailDlcInstallProgress_speed_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000835 RID: 2101
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcInstallProgress_speed_get")]
		public static extern uint RailDlcInstallProgress_speed_get(IntPtr jarg1);

		// Token: 0x06000836 RID: 2102
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcInstallProgress__SWIG_0")]
		public static extern IntPtr new_RailDlcInstallProgress__SWIG_0();

		// Token: 0x06000837 RID: 2103
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcInstallProgress__SWIG_1")]
		public static extern IntPtr new_RailDlcInstallProgress__SWIG_1(IntPtr jarg1);

		// Token: 0x06000838 RID: 2104
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailDlcInstallProgress")]
		public static extern void delete_RailDlcInstallProgress(IntPtr jarg1);

		// Token: 0x06000839 RID: 2105
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcOwned_is_owned_set")]
		public static extern void RailDlcOwned_is_owned_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600083A RID: 2106
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcOwned_is_owned_get")]
		public static extern bool RailDlcOwned_is_owned_get(IntPtr jarg1);

		// Token: 0x0600083B RID: 2107
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcOwned_dlc_id_set")]
		public static extern void RailDlcOwned_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600083C RID: 2108
		[DllImport("rail_api", EntryPoint = "CSharp_RailDlcOwned_dlc_id_get")]
		public static extern IntPtr RailDlcOwned_dlc_id_get(IntPtr jarg1);

		// Token: 0x0600083D RID: 2109
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcOwned__SWIG_0")]
		public static extern IntPtr new_RailDlcOwned__SWIG_0();

		// Token: 0x0600083E RID: 2110
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDlcOwned__SWIG_1")]
		public static extern IntPtr new_RailDlcOwned__SWIG_1(IntPtr jarg1);

		// Token: 0x0600083F RID: 2111
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailDlcOwned")]
		public static extern void delete_RailDlcOwned(IntPtr jarg1);

		// Token: 0x06000840 RID: 2112
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStart_dlc_id_set")]
		public static extern void DlcInstallStart_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000841 RID: 2113
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStart_dlc_id_get")]
		public static extern IntPtr DlcInstallStart_dlc_id_get(IntPtr jarg1);

		// Token: 0x06000842 RID: 2114
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallStart__SWIG_0")]
		public static extern IntPtr new_DlcInstallStart__SWIG_0();

		// Token: 0x06000843 RID: 2115
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallStart__SWIG_1")]
		public static extern IntPtr new_DlcInstallStart__SWIG_1(IntPtr jarg1);

		// Token: 0x06000844 RID: 2116
		[DllImport("rail_api", EntryPoint = "CSharp_delete_DlcInstallStart")]
		public static extern void delete_DlcInstallStart(IntPtr jarg1);

		// Token: 0x06000845 RID: 2117
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_dlc_id_set")]
		public static extern void DlcInstallStartResult_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000846 RID: 2118
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_dlc_id_get")]
		public static extern IntPtr DlcInstallStartResult_dlc_id_get(IntPtr jarg1);

		// Token: 0x06000847 RID: 2119
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_result_set")]
		public static extern void DlcInstallStartResult_result_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000848 RID: 2120
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_result_get")]
		public static extern int DlcInstallStartResult_result_get(IntPtr jarg1);

		// Token: 0x06000849 RID: 2121
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallStartResult__SWIG_0")]
		public static extern IntPtr new_DlcInstallStartResult__SWIG_0();

		// Token: 0x0600084A RID: 2122
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallStartResult__SWIG_1")]
		public static extern IntPtr new_DlcInstallStartResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600084B RID: 2123
		[DllImport("rail_api", EntryPoint = "CSharp_delete_DlcInstallStartResult")]
		public static extern void delete_DlcInstallStartResult(IntPtr jarg1);

		// Token: 0x0600084C RID: 2124
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_dlc_id_set")]
		public static extern void DlcInstallProgress_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600084D RID: 2125
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_dlc_id_get")]
		public static extern IntPtr DlcInstallProgress_dlc_id_get(IntPtr jarg1);

		// Token: 0x0600084E RID: 2126
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_progress_set")]
		public static extern void DlcInstallProgress_progress_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600084F RID: 2127
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_progress_get")]
		public static extern IntPtr DlcInstallProgress_progress_get(IntPtr jarg1);

		// Token: 0x06000850 RID: 2128
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallProgress__SWIG_0")]
		public static extern IntPtr new_DlcInstallProgress__SWIG_0();

		// Token: 0x06000851 RID: 2129
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallProgress__SWIG_1")]
		public static extern IntPtr new_DlcInstallProgress__SWIG_1(IntPtr jarg1);

		// Token: 0x06000852 RID: 2130
		[DllImport("rail_api", EntryPoint = "CSharp_delete_DlcInstallProgress")]
		public static extern void delete_DlcInstallProgress(IntPtr jarg1);

		// Token: 0x06000853 RID: 2131
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_dlc_id_set")]
		public static extern void DlcInstallFinished_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000854 RID: 2132
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_dlc_id_get")]
		public static extern IntPtr DlcInstallFinished_dlc_id_get(IntPtr jarg1);

		// Token: 0x06000855 RID: 2133
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_result_set")]
		public static extern void DlcInstallFinished_result_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000856 RID: 2134
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_result_get")]
		public static extern int DlcInstallFinished_result_get(IntPtr jarg1);

		// Token: 0x06000857 RID: 2135
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallFinished__SWIG_0")]
		public static extern IntPtr new_DlcInstallFinished__SWIG_0();

		// Token: 0x06000858 RID: 2136
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcInstallFinished__SWIG_1")]
		public static extern IntPtr new_DlcInstallFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000859 RID: 2137
		[DllImport("rail_api", EntryPoint = "CSharp_delete_DlcInstallFinished")]
		public static extern void delete_DlcInstallFinished(IntPtr jarg1);

		// Token: 0x0600085A RID: 2138
		[DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_dlc_id_set")]
		public static extern void DlcUninstallFinished_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600085B RID: 2139
		[DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_dlc_id_get")]
		public static extern IntPtr DlcUninstallFinished_dlc_id_get(IntPtr jarg1);

		// Token: 0x0600085C RID: 2140
		[DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_result_set")]
		public static extern void DlcUninstallFinished_result_set(IntPtr jarg1, int jarg2);

		// Token: 0x0600085D RID: 2141
		[DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_result_get")]
		public static extern int DlcUninstallFinished_result_get(IntPtr jarg1);

		// Token: 0x0600085E RID: 2142
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcUninstallFinished__SWIG_0")]
		public static extern IntPtr new_DlcUninstallFinished__SWIG_0();

		// Token: 0x0600085F RID: 2143
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcUninstallFinished__SWIG_1")]
		public static extern IntPtr new_DlcUninstallFinished__SWIG_1(IntPtr jarg1);

		// Token: 0x06000860 RID: 2144
		[DllImport("rail_api", EntryPoint = "CSharp_delete_DlcUninstallFinished")]
		public static extern void delete_DlcUninstallFinished(IntPtr jarg1);

		// Token: 0x06000861 RID: 2145
		[DllImport("rail_api", EntryPoint = "CSharp_new_CheckAllDlcsStateReadyResult__SWIG_0")]
		public static extern IntPtr new_CheckAllDlcsStateReadyResult__SWIG_0();

		// Token: 0x06000862 RID: 2146
		[DllImport("rail_api", EntryPoint = "CSharp_new_CheckAllDlcsStateReadyResult__SWIG_1")]
		public static extern IntPtr new_CheckAllDlcsStateReadyResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000863 RID: 2147
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CheckAllDlcsStateReadyResult")]
		public static extern void delete_CheckAllDlcsStateReadyResult(IntPtr jarg1);

		// Token: 0x06000864 RID: 2148
		[DllImport("rail_api", EntryPoint = "CSharp_QueryIsOwnedDlcsResult_dlc_owned_list_set")]
		public static extern void QueryIsOwnedDlcsResult_dlc_owned_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000865 RID: 2149
		[DllImport("rail_api", EntryPoint = "CSharp_QueryIsOwnedDlcsResult_dlc_owned_list_get")]
		public static extern IntPtr QueryIsOwnedDlcsResult_dlc_owned_list_get(IntPtr jarg1);

		// Token: 0x06000866 RID: 2150
		[DllImport("rail_api", EntryPoint = "CSharp_new_QueryIsOwnedDlcsResult__SWIG_0")]
		public static extern IntPtr new_QueryIsOwnedDlcsResult__SWIG_0();

		// Token: 0x06000867 RID: 2151
		[DllImport("rail_api", EntryPoint = "CSharp_new_QueryIsOwnedDlcsResult__SWIG_1")]
		public static extern IntPtr new_QueryIsOwnedDlcsResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000868 RID: 2152
		[DllImport("rail_api", EntryPoint = "CSharp_delete_QueryIsOwnedDlcsResult")]
		public static extern void delete_QueryIsOwnedDlcsResult(IntPtr jarg1);

		// Token: 0x06000869 RID: 2153
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcOwnershipChanged__SWIG_0")]
		public static extern IntPtr new_DlcOwnershipChanged__SWIG_0();

		// Token: 0x0600086A RID: 2154
		[DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_dlc_id_set")]
		public static extern void DlcOwnershipChanged_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600086B RID: 2155
		[DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_dlc_id_get")]
		public static extern IntPtr DlcOwnershipChanged_dlc_id_get(IntPtr jarg1);

		// Token: 0x0600086C RID: 2156
		[DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_is_active_set")]
		public static extern void DlcOwnershipChanged_is_active_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600086D RID: 2157
		[DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_is_active_get")]
		public static extern bool DlcOwnershipChanged_is_active_get(IntPtr jarg1);

		// Token: 0x0600086E RID: 2158
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcOwnershipChanged__SWIG_1")]
		public static extern IntPtr new_DlcOwnershipChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x0600086F RID: 2159
		[DllImport("rail_api", EntryPoint = "CSharp_delete_DlcOwnershipChanged")]
		public static extern void delete_DlcOwnershipChanged(IntPtr jarg1);

		// Token: 0x06000870 RID: 2160
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcRefundChanged__SWIG_0")]
		public static extern IntPtr new_DlcRefundChanged__SWIG_0();

		// Token: 0x06000871 RID: 2161
		[DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_dlc_id_set")]
		public static extern void DlcRefundChanged_dlc_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000872 RID: 2162
		[DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_dlc_id_get")]
		public static extern IntPtr DlcRefundChanged_dlc_id_get(IntPtr jarg1);

		// Token: 0x06000873 RID: 2163
		[DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_refund_state_set")]
		public static extern void DlcRefundChanged_refund_state_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000874 RID: 2164
		[DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_refund_state_get")]
		public static extern int DlcRefundChanged_refund_state_get(IntPtr jarg1);

		// Token: 0x06000875 RID: 2165
		[DllImport("rail_api", EntryPoint = "CSharp_new_DlcRefundChanged__SWIG_1")]
		public static extern IntPtr new_DlcRefundChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x06000876 RID: 2166
		[DllImport("rail_api", EntryPoint = "CSharp_delete_DlcRefundChanged")]
		public static extern void delete_DlcRefundChanged(IntPtr jarg1);

		// Token: 0x06000877 RID: 2167
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailWindowLayout__SWIG_0")]
		public static extern IntPtr new_RailWindowLayout__SWIG_0();

		// Token: 0x06000878 RID: 2168
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowLayout_x_margin_set")]
		public static extern void RailWindowLayout_x_margin_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000879 RID: 2169
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowLayout_x_margin_get")]
		public static extern uint RailWindowLayout_x_margin_get(IntPtr jarg1);

		// Token: 0x0600087A RID: 2170
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowLayout_y_margin_set")]
		public static extern void RailWindowLayout_y_margin_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600087B RID: 2171
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowLayout_y_margin_get")]
		public static extern uint RailWindowLayout_y_margin_get(IntPtr jarg1);

		// Token: 0x0600087C RID: 2172
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowLayout_position_type_set")]
		public static extern void RailWindowLayout_position_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x0600087D RID: 2173
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowLayout_position_type_get")]
		public static extern int RailWindowLayout_position_type_get(IntPtr jarg1);

		// Token: 0x0600087E RID: 2174
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailWindowLayout__SWIG_1")]
		public static extern IntPtr new_RailWindowLayout__SWIG_1(IntPtr jarg1);

		// Token: 0x0600087F RID: 2175
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailWindowLayout")]
		public static extern void delete_RailWindowLayout(IntPtr jarg1);

		// Token: 0x06000880 RID: 2176
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailStoreOptions__SWIG_0")]
		public static extern IntPtr new_RailStoreOptions__SWIG_0();

		// Token: 0x06000881 RID: 2177
		[DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_store_type_set")]
		public static extern void RailStoreOptions_store_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000882 RID: 2178
		[DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_store_type_get")]
		public static extern int RailStoreOptions_store_type_get(IntPtr jarg1);

		// Token: 0x06000883 RID: 2179
		[DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_window_margin_top_set")]
		public static extern void RailStoreOptions_window_margin_top_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000884 RID: 2180
		[DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_window_margin_top_get")]
		public static extern int RailStoreOptions_window_margin_top_get(IntPtr jarg1);

		// Token: 0x06000885 RID: 2181
		[DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_window_margin_left_set")]
		public static extern void RailStoreOptions_window_margin_left_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000886 RID: 2182
		[DllImport("rail_api", EntryPoint = "CSharp_RailStoreOptions_window_margin_left_get")]
		public static extern int RailStoreOptions_window_margin_left_get(IntPtr jarg1);

		// Token: 0x06000887 RID: 2183
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailStoreOptions__SWIG_1")]
		public static extern IntPtr new_RailStoreOptions__SWIG_1(IntPtr jarg1);

		// Token: 0x06000888 RID: 2184
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailStoreOptions")]
		public static extern void delete_RailStoreOptions(IntPtr jarg1);

		// Token: 0x06000889 RID: 2185
		[DllImport("rail_api", EntryPoint = "CSharp_new_ShowFloatingWindowResult__SWIG_0")]
		public static extern IntPtr new_ShowFloatingWindowResult__SWIG_0();

		// Token: 0x0600088A RID: 2186
		[DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_is_show_set")]
		public static extern void ShowFloatingWindowResult_is_show_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600088B RID: 2187
		[DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_is_show_get")]
		public static extern bool ShowFloatingWindowResult_is_show_get(IntPtr jarg1);

		// Token: 0x0600088C RID: 2188
		[DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_window_type_set")]
		public static extern void ShowFloatingWindowResult_window_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x0600088D RID: 2189
		[DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_window_type_get")]
		public static extern int ShowFloatingWindowResult_window_type_get(IntPtr jarg1);

		// Token: 0x0600088E RID: 2190
		[DllImport("rail_api", EntryPoint = "CSharp_new_ShowFloatingWindowResult__SWIG_1")]
		public static extern IntPtr new_ShowFloatingWindowResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600088F RID: 2191
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ShowFloatingWindowResult")]
		public static extern void delete_ShowFloatingWindowResult(IntPtr jarg1);

		// Token: 0x06000890 RID: 2192
		[DllImport("rail_api", EntryPoint = "CSharp_new_ShowNotifyWindow__SWIG_0")]
		public static extern IntPtr new_ShowNotifyWindow__SWIG_0();

		// Token: 0x06000891 RID: 2193
		[DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyWindow_window_type_set")]
		public static extern void ShowNotifyWindow_window_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000892 RID: 2194
		[DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyWindow_window_type_get")]
		public static extern int ShowNotifyWindow_window_type_get(IntPtr jarg1);

		// Token: 0x06000893 RID: 2195
		[DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyWindow_json_content_set")]
		public static extern void ShowNotifyWindow_json_content_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000894 RID: 2196
		[DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyWindow_json_content_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string ShowNotifyWindow_json_content_get(IntPtr jarg1);

		// Token: 0x06000895 RID: 2197
		[DllImport("rail_api", EntryPoint = "CSharp_new_ShowNotifyWindow__SWIG_1")]
		public static extern IntPtr new_ShowNotifyWindow__SWIG_1(IntPtr jarg1);

		// Token: 0x06000896 RID: 2198
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ShowNotifyWindow")]
		public static extern void delete_ShowNotifyWindow(IntPtr jarg1);

		// Token: 0x06000897 RID: 2199
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerInfo__SWIG_0")]
		public static extern IntPtr new_GameServerInfo__SWIG_0();

		// Token: 0x06000898 RID: 2200
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_Reset")]
		public static extern void GameServerInfo_Reset(IntPtr jarg1);

		// Token: 0x06000899 RID: 2201
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_rail_id_set")]
		public static extern void GameServerInfo_game_server_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600089A RID: 2202
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_rail_id_get")]
		public static extern IntPtr GameServerInfo_game_server_rail_id_get(IntPtr jarg1);

		// Token: 0x0600089B RID: 2203
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_owner_rail_id_set")]
		public static extern void GameServerInfo_owner_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600089C RID: 2204
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_owner_rail_id_get")]
		public static extern IntPtr GameServerInfo_owner_rail_id_get(IntPtr jarg1);

		// Token: 0x0600089D RID: 2205
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_is_dedicated_set")]
		public static extern void GameServerInfo_is_dedicated_set(IntPtr jarg1, bool jarg2);

		// Token: 0x0600089E RID: 2206
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_is_dedicated_get")]
		public static extern bool GameServerInfo_is_dedicated_get(IntPtr jarg1);

		// Token: 0x0600089F RID: 2207
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_name_set")]
		public static extern void GameServerInfo_game_server_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008A0 RID: 2208
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_game_server_name_get(IntPtr jarg1);

		// Token: 0x060008A1 RID: 2209
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_map_set")]
		public static extern void GameServerInfo_game_server_map_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008A2 RID: 2210
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_game_server_map_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_game_server_map_get(IntPtr jarg1);

		// Token: 0x060008A3 RID: 2211
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_has_password_set")]
		public static extern void GameServerInfo_has_password_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060008A4 RID: 2212
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_has_password_get")]
		public static extern bool GameServerInfo_has_password_get(IntPtr jarg1);

		// Token: 0x060008A5 RID: 2213
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_is_friend_only_set")]
		public static extern void GameServerInfo_is_friend_only_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060008A6 RID: 2214
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_is_friend_only_get")]
		public static extern bool GameServerInfo_is_friend_only_get(IntPtr jarg1);

		// Token: 0x060008A7 RID: 2215
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_max_players_set")]
		public static extern void GameServerInfo_max_players_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060008A8 RID: 2216
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_max_players_get")]
		public static extern uint GameServerInfo_max_players_get(IntPtr jarg1);

		// Token: 0x060008A9 RID: 2217
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_current_players_set")]
		public static extern void GameServerInfo_current_players_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060008AA RID: 2218
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_current_players_get")]
		public static extern uint GameServerInfo_current_players_get(IntPtr jarg1);

		// Token: 0x060008AB RID: 2219
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_bot_players_set")]
		public static extern void GameServerInfo_bot_players_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060008AC RID: 2220
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_bot_players_get")]
		public static extern uint GameServerInfo_bot_players_get(IntPtr jarg1);

		// Token: 0x060008AD RID: 2221
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_zone_id_set")]
		public static extern void GameServerInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x060008AE RID: 2222
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_zone_id_get")]
		public static extern ulong GameServerInfo_zone_id_get(IntPtr jarg1);

		// Token: 0x060008AF RID: 2223
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_host_set")]
		public static extern void GameServerInfo_server_host_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008B0 RID: 2224
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_host_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_server_host_get(IntPtr jarg1);

		// Token: 0x060008B1 RID: 2225
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_fullname_set")]
		public static extern void GameServerInfo_server_fullname_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008B2 RID: 2226
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_fullname_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_server_fullname_get(IntPtr jarg1);

		// Token: 0x060008B3 RID: 2227
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_description_set")]
		public static extern void GameServerInfo_server_description_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008B4 RID: 2228
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_description_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_server_description_get(IntPtr jarg1);

		// Token: 0x060008B5 RID: 2229
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_tags_set")]
		public static extern void GameServerInfo_server_tags_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008B6 RID: 2230
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_tags_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_server_tags_get(IntPtr jarg1);

		// Token: 0x060008B7 RID: 2231
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_version_set")]
		public static extern void GameServerInfo_server_version_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008B8 RID: 2232
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_version_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_server_version_get(IntPtr jarg1);

		// Token: 0x060008B9 RID: 2233
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_spectator_host_set")]
		public static extern void GameServerInfo_spectator_host_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008BA RID: 2234
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_spectator_host_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_spectator_host_get(IntPtr jarg1);

		// Token: 0x060008BB RID: 2235
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_info_set")]
		public static extern void GameServerInfo_server_info_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008BC RID: 2236
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_info_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerInfo_server_info_get(IntPtr jarg1);

		// Token: 0x060008BD RID: 2237
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_mods_set")]
		public static extern void GameServerInfo_server_mods_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060008BE RID: 2238
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_mods_get")]
		public static extern IntPtr GameServerInfo_server_mods_get(IntPtr jarg1);

		// Token: 0x060008BF RID: 2239
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_kvs_set")]
		public static extern void GameServerInfo_server_kvs_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060008C0 RID: 2240
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerInfo_server_kvs_get")]
		public static extern IntPtr GameServerInfo_server_kvs_get(IntPtr jarg1);

		// Token: 0x060008C1 RID: 2241
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerInfo__SWIG_1")]
		public static extern IntPtr new_GameServerInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x060008C2 RID: 2242
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerInfo")]
		public static extern void delete_GameServerInfo(IntPtr jarg1);

		// Token: 0x060008C3 RID: 2243
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateGameServerOptions__SWIG_0")]
		public static extern IntPtr new_CreateGameServerOptions__SWIG_0();

		// Token: 0x060008C4 RID: 2244
		[DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerOptions_enable_team_voice_set")]
		public static extern void CreateGameServerOptions_enable_team_voice_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060008C5 RID: 2245
		[DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerOptions_enable_team_voice_get")]
		public static extern bool CreateGameServerOptions_enable_team_voice_get(IntPtr jarg1);

		// Token: 0x060008C6 RID: 2246
		[DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerOptions_has_password_set")]
		public static extern void CreateGameServerOptions_has_password_set(IntPtr jarg1, bool jarg2);

		// Token: 0x060008C7 RID: 2247
		[DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerOptions_has_password_get")]
		public static extern bool CreateGameServerOptions_has_password_get(IntPtr jarg1);

		// Token: 0x060008C8 RID: 2248
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateGameServerOptions__SWIG_1")]
		public static extern IntPtr new_CreateGameServerOptions__SWIG_1(IntPtr jarg1);

		// Token: 0x060008C9 RID: 2249
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateGameServerOptions")]
		public static extern void delete_CreateGameServerOptions(IntPtr jarg1);

		// Token: 0x060008CA RID: 2250
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListSorter__SWIG_0")]
		public static extern IntPtr new_GameServerListSorter__SWIG_0();

		// Token: 0x060008CB RID: 2251
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sorter_key_type_set")]
		public static extern void GameServerListSorter_sorter_key_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060008CC RID: 2252
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sorter_key_type_get")]
		public static extern int GameServerListSorter_sorter_key_type_get(IntPtr jarg1);

		// Token: 0x060008CD RID: 2253
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_key_set")]
		public static extern void GameServerListSorter_sort_key_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008CE RID: 2254
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_key_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerListSorter_sort_key_get(IntPtr jarg1);

		// Token: 0x060008CF RID: 2255
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_value_type_set")]
		public static extern void GameServerListSorter_sort_value_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060008D0 RID: 2256
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_value_type_get")]
		public static extern int GameServerListSorter_sort_value_type_get(IntPtr jarg1);

		// Token: 0x060008D1 RID: 2257
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_type_set")]
		public static extern void GameServerListSorter_sort_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060008D2 RID: 2258
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListSorter_sort_type_get")]
		public static extern int GameServerListSorter_sort_type_get(IntPtr jarg1);

		// Token: 0x060008D3 RID: 2259
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListSorter__SWIG_1")]
		public static extern IntPtr new_GameServerListSorter__SWIG_1(IntPtr jarg1);

		// Token: 0x060008D4 RID: 2260
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerListSorter")]
		public static extern void delete_GameServerListSorter(IntPtr jarg1);

		// Token: 0x060008D5 RID: 2261
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListFilterKey__SWIG_0")]
		public static extern IntPtr new_GameServerListFilterKey__SWIG_0();

		// Token: 0x060008D6 RID: 2262
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_key_name_set")]
		public static extern void GameServerListFilterKey_key_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008D7 RID: 2263
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_key_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerListFilterKey_key_name_get(IntPtr jarg1);

		// Token: 0x060008D8 RID: 2264
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_value_type_set")]
		public static extern void GameServerListFilterKey_value_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060008D9 RID: 2265
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_value_type_get")]
		public static extern int GameServerListFilterKey_value_type_get(IntPtr jarg1);

		// Token: 0x060008DA RID: 2266
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_comparison_type_set")]
		public static extern void GameServerListFilterKey_comparison_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060008DB RID: 2267
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_comparison_type_get")]
		public static extern int GameServerListFilterKey_comparison_type_get(IntPtr jarg1);

		// Token: 0x060008DC RID: 2268
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_filter_value_set")]
		public static extern void GameServerListFilterKey_filter_value_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008DD RID: 2269
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilterKey_filter_value_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerListFilterKey_filter_value_get(IntPtr jarg1);

		// Token: 0x060008DE RID: 2270
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListFilterKey__SWIG_1")]
		public static extern IntPtr new_GameServerListFilterKey__SWIG_1(IntPtr jarg1);

		// Token: 0x060008DF RID: 2271
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerListFilterKey")]
		public static extern void delete_GameServerListFilterKey(IntPtr jarg1);

		// Token: 0x060008E0 RID: 2272
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListFilter__SWIG_0")]
		public static extern IntPtr new_GameServerListFilter__SWIG_0();

		// Token: 0x060008E1 RID: 2273
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filters_set")]
		public static extern void GameServerListFilter_filters_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060008E2 RID: 2274
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filters_get")]
		public static extern IntPtr GameServerListFilter_filters_get(IntPtr jarg1);

		// Token: 0x060008E3 RID: 2275
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_owner_id_set")]
		public static extern void GameServerListFilter_owner_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060008E4 RID: 2276
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_owner_id_get")]
		public static extern IntPtr GameServerListFilter_owner_id_get(IntPtr jarg1);

		// Token: 0x060008E5 RID: 2277
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_dedicated_server_set")]
		public static extern void GameServerListFilter_filter_dedicated_server_set(IntPtr jarg1, int jarg2);

		// Token: 0x060008E6 RID: 2278
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_dedicated_server_get")]
		public static extern int GameServerListFilter_filter_dedicated_server_get(IntPtr jarg1);

		// Token: 0x060008E7 RID: 2279
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_name_set")]
		public static extern void GameServerListFilter_filter_game_server_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008E8 RID: 2280
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerListFilter_filter_game_server_name_get(IntPtr jarg1);

		// Token: 0x060008E9 RID: 2281
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_zone_id_set")]
		public static extern void GameServerListFilter_filter_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x060008EA RID: 2282
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_zone_id_get")]
		public static extern ulong GameServerListFilter_filter_zone_id_get(IntPtr jarg1);

		// Token: 0x060008EB RID: 2283
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_map_set")]
		public static extern void GameServerListFilter_filter_game_server_map_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008EC RID: 2284
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_map_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerListFilter_filter_game_server_map_get(IntPtr jarg1);

		// Token: 0x060008ED RID: 2285
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_host_set")]
		public static extern void GameServerListFilter_filter_game_server_host_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008EE RID: 2286
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_game_server_host_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerListFilter_filter_game_server_host_get(IntPtr jarg1);

		// Token: 0x060008EF RID: 2287
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_password_set")]
		public static extern void GameServerListFilter_filter_password_set(IntPtr jarg1, int jarg2);

		// Token: 0x060008F0 RID: 2288
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_password_get")]
		public static extern int GameServerListFilter_filter_password_get(IntPtr jarg1);

		// Token: 0x060008F1 RID: 2289
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_friends_created_set")]
		public static extern void GameServerListFilter_filter_friends_created_set(IntPtr jarg1, int jarg2);

		// Token: 0x060008F2 RID: 2290
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_filter_friends_created_get")]
		public static extern int GameServerListFilter_filter_friends_created_get(IntPtr jarg1);

		// Token: 0x060008F3 RID: 2291
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_tags_contained_set")]
		public static extern void GameServerListFilter_tags_contained_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008F4 RID: 2292
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_tags_contained_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerListFilter_tags_contained_get(IntPtr jarg1);

		// Token: 0x060008F5 RID: 2293
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_tags_not_contained_set")]
		public static extern void GameServerListFilter_tags_not_contained_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008F6 RID: 2294
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerListFilter_tags_not_contained_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerListFilter_tags_not_contained_get(IntPtr jarg1);

		// Token: 0x060008F7 RID: 2295
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerListFilter__SWIG_1")]
		public static extern IntPtr new_GameServerListFilter__SWIG_1(IntPtr jarg1);

		// Token: 0x060008F8 RID: 2296
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerListFilter")]
		public static extern void delete_GameServerListFilter(IntPtr jarg1);

		// Token: 0x060008F9 RID: 2297
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerPlayerInfo__SWIG_0")]
		public static extern IntPtr new_GameServerPlayerInfo__SWIG_0();

		// Token: 0x060008FA RID: 2298
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_id_set")]
		public static extern void GameServerPlayerInfo_member_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060008FB RID: 2299
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_id_get")]
		public static extern IntPtr GameServerPlayerInfo_member_id_get(IntPtr jarg1);

		// Token: 0x060008FC RID: 2300
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_nickname_set")]
		public static extern void GameServerPlayerInfo_member_nickname_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060008FD RID: 2301
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_nickname_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GameServerPlayerInfo_member_nickname_get(IntPtr jarg1);

		// Token: 0x060008FE RID: 2302
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_score_set")]
		public static extern void GameServerPlayerInfo_member_score_set(IntPtr jarg1, long jarg2);

		// Token: 0x060008FF RID: 2303
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerPlayerInfo_member_score_get")]
		public static extern long GameServerPlayerInfo_member_score_get(IntPtr jarg1);

		// Token: 0x06000900 RID: 2304
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerPlayerInfo__SWIG_1")]
		public static extern IntPtr new_GameServerPlayerInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000901 RID: 2305
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerPlayerInfo")]
		public static extern void delete_GameServerPlayerInfo(IntPtr jarg1);

		// Token: 0x06000902 RID: 2306
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncAcquireGameServerSessionTicketResponse__SWIG_0")]
		public static extern IntPtr new_AsyncAcquireGameServerSessionTicketResponse__SWIG_0();

		// Token: 0x06000903 RID: 2307
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncAcquireGameServerSessionTicketResponse_session_ticket_set")]
		public static extern void AsyncAcquireGameServerSessionTicketResponse_session_ticket_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000904 RID: 2308
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncAcquireGameServerSessionTicketResponse_session_ticket_get")]
		public static extern IntPtr AsyncAcquireGameServerSessionTicketResponse_session_ticket_get(IntPtr jarg1);

		// Token: 0x06000905 RID: 2309
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncAcquireGameServerSessionTicketResponse__SWIG_1")]
		public static extern IntPtr new_AsyncAcquireGameServerSessionTicketResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000906 RID: 2310
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncAcquireGameServerSessionTicketResponse")]
		public static extern void delete_AsyncAcquireGameServerSessionTicketResponse(IntPtr jarg1);

		// Token: 0x06000907 RID: 2311
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerStartSessionWithPlayerResponse__SWIG_0")]
		public static extern IntPtr new_GameServerStartSessionWithPlayerResponse__SWIG_0();

		// Token: 0x06000908 RID: 2312
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerStartSessionWithPlayerResponse_remote_rail_id_set")]
		public static extern void GameServerStartSessionWithPlayerResponse_remote_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000909 RID: 2313
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerStartSessionWithPlayerResponse_remote_rail_id_get")]
		public static extern IntPtr GameServerStartSessionWithPlayerResponse_remote_rail_id_get(IntPtr jarg1);

		// Token: 0x0600090A RID: 2314
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerStartSessionWithPlayerResponse__SWIG_1")]
		public static extern IntPtr new_GameServerStartSessionWithPlayerResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x0600090B RID: 2315
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerStartSessionWithPlayerResponse")]
		public static extern void delete_GameServerStartSessionWithPlayerResponse(IntPtr jarg1);

		// Token: 0x0600090C RID: 2316
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateGameServerResult__SWIG_0")]
		public static extern IntPtr new_CreateGameServerResult__SWIG_0();

		// Token: 0x0600090D RID: 2317
		[DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerResult_game_server_id_set")]
		public static extern void CreateGameServerResult_game_server_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600090E RID: 2318
		[DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerResult_game_server_id_get")]
		public static extern IntPtr CreateGameServerResult_game_server_id_get(IntPtr jarg1);

		// Token: 0x0600090F RID: 2319
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateGameServerResult__SWIG_1")]
		public static extern IntPtr new_CreateGameServerResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000910 RID: 2320
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateGameServerResult")]
		public static extern void delete_CreateGameServerResult(IntPtr jarg1);

		// Token: 0x06000911 RID: 2321
		[DllImport("rail_api", EntryPoint = "CSharp_new_SetGameServerMetadataResult__SWIG_0")]
		public static extern IntPtr new_SetGameServerMetadataResult__SWIG_0();

		// Token: 0x06000912 RID: 2322
		[DllImport("rail_api", EntryPoint = "CSharp_SetGameServerMetadataResult_game_server_id_set")]
		public static extern void SetGameServerMetadataResult_game_server_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000913 RID: 2323
		[DllImport("rail_api", EntryPoint = "CSharp_SetGameServerMetadataResult_game_server_id_get")]
		public static extern IntPtr SetGameServerMetadataResult_game_server_id_get(IntPtr jarg1);

		// Token: 0x06000914 RID: 2324
		[DllImport("rail_api", EntryPoint = "CSharp_new_SetGameServerMetadataResult__SWIG_1")]
		public static extern IntPtr new_SetGameServerMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000915 RID: 2325
		[DllImport("rail_api", EntryPoint = "CSharp_delete_SetGameServerMetadataResult")]
		public static extern void delete_SetGameServerMetadataResult(IntPtr jarg1);

		// Token: 0x06000916 RID: 2326
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerMetadataResult__SWIG_0")]
		public static extern IntPtr new_GetGameServerMetadataResult__SWIG_0();

		// Token: 0x06000917 RID: 2327
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_game_server_id_set")]
		public static extern void GetGameServerMetadataResult_game_server_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000918 RID: 2328
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_game_server_id_get")]
		public static extern IntPtr GetGameServerMetadataResult_game_server_id_get(IntPtr jarg1);

		// Token: 0x06000919 RID: 2329
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_key_value_set")]
		public static extern void GetGameServerMetadataResult_key_value_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600091A RID: 2330
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_key_value_get")]
		public static extern IntPtr GetGameServerMetadataResult_key_value_get(IntPtr jarg1);

		// Token: 0x0600091B RID: 2331
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerMetadataResult__SWIG_1")]
		public static extern IntPtr new_GetGameServerMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600091C RID: 2332
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GetGameServerMetadataResult")]
		public static extern void delete_GetGameServerMetadataResult(IntPtr jarg1);

		// Token: 0x0600091D RID: 2333
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerRegisterToServerListResult__SWIG_0")]
		public static extern IntPtr new_GameServerRegisterToServerListResult__SWIG_0();

		// Token: 0x0600091E RID: 2334
		[DllImport("rail_api", EntryPoint = "CSharp_new_GameServerRegisterToServerListResult__SWIG_1")]
		public static extern IntPtr new_GameServerRegisterToServerListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600091F RID: 2335
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GameServerRegisterToServerListResult")]
		public static extern void delete_GameServerRegisterToServerListResult(IntPtr jarg1);

		// Token: 0x06000920 RID: 2336
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerPlayerListResult__SWIG_0")]
		public static extern IntPtr new_GetGameServerPlayerListResult__SWIG_0();

		// Token: 0x06000921 RID: 2337
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_game_server_id_set")]
		public static extern void GetGameServerPlayerListResult_game_server_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000922 RID: 2338
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_game_server_id_get")]
		public static extern IntPtr GetGameServerPlayerListResult_game_server_id_get(IntPtr jarg1);

		// Token: 0x06000923 RID: 2339
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_server_player_info_set")]
		public static extern void GetGameServerPlayerListResult_server_player_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000924 RID: 2340
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_server_player_info_get")]
		public static extern IntPtr GetGameServerPlayerListResult_server_player_info_get(IntPtr jarg1);

		// Token: 0x06000925 RID: 2341
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerPlayerListResult__SWIG_1")]
		public static extern IntPtr new_GetGameServerPlayerListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000926 RID: 2342
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GetGameServerPlayerListResult")]
		public static extern void delete_GetGameServerPlayerListResult(IntPtr jarg1);

		// Token: 0x06000927 RID: 2343
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerListResult__SWIG_0")]
		public static extern IntPtr new_GetGameServerListResult__SWIG_0();

		// Token: 0x06000928 RID: 2344
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_start_index_set")]
		public static extern void GetGameServerListResult_start_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000929 RID: 2345
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_start_index_get")]
		public static extern uint GetGameServerListResult_start_index_get(IntPtr jarg1);

		// Token: 0x0600092A RID: 2346
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_end_index_set")]
		public static extern void GetGameServerListResult_end_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600092B RID: 2347
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_end_index_get")]
		public static extern uint GetGameServerListResult_end_index_get(IntPtr jarg1);

		// Token: 0x0600092C RID: 2348
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_total_num_set")]
		public static extern void GetGameServerListResult_total_num_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600092D RID: 2349
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_total_num_get")]
		public static extern uint GetGameServerListResult_total_num_get(IntPtr jarg1);

		// Token: 0x0600092E RID: 2350
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_server_info_set")]
		public static extern void GetGameServerListResult_server_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600092F RID: 2351
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_server_info_get")]
		public static extern IntPtr GetGameServerListResult_server_info_get(IntPtr jarg1);

		// Token: 0x06000930 RID: 2352
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetGameServerListResult__SWIG_1")]
		public static extern IntPtr new_GetGameServerListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000931 RID: 2353
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GetGameServerListResult")]
		public static extern void delete_GetGameServerListResult(IntPtr jarg1);

		// Token: 0x06000932 RID: 2354
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetFavoriteGameServersResult__SWIG_0")]
		public static extern IntPtr new_AsyncGetFavoriteGameServersResult__SWIG_0();

		// Token: 0x06000933 RID: 2355
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetFavoriteGameServersResult_server_id_array_set")]
		public static extern void AsyncGetFavoriteGameServersResult_server_id_array_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000934 RID: 2356
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetFavoriteGameServersResult_server_id_array_get")]
		public static extern IntPtr AsyncGetFavoriteGameServersResult_server_id_array_get(IntPtr jarg1);

		// Token: 0x06000935 RID: 2357
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncGetFavoriteGameServersResult__SWIG_1")]
		public static extern IntPtr new_AsyncGetFavoriteGameServersResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000936 RID: 2358
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncGetFavoriteGameServersResult")]
		public static extern void delete_AsyncGetFavoriteGameServersResult(IntPtr jarg1);

		// Token: 0x06000937 RID: 2359
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncAddFavoriteGameServerResult__SWIG_0")]
		public static extern IntPtr new_AsyncAddFavoriteGameServerResult__SWIG_0();

		// Token: 0x06000938 RID: 2360
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncAddFavoriteGameServerResult_server_id_set")]
		public static extern void AsyncAddFavoriteGameServerResult_server_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000939 RID: 2361
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncAddFavoriteGameServerResult_server_id_get")]
		public static extern IntPtr AsyncAddFavoriteGameServerResult_server_id_get(IntPtr jarg1);

		// Token: 0x0600093A RID: 2362
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncAddFavoriteGameServerResult__SWIG_1")]
		public static extern IntPtr new_AsyncAddFavoriteGameServerResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600093B RID: 2363
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncAddFavoriteGameServerResult")]
		public static extern void delete_AsyncAddFavoriteGameServerResult(IntPtr jarg1);

		// Token: 0x0600093C RID: 2364
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRemoveFavoriteGameServerResult__SWIG_0")]
		public static extern IntPtr new_AsyncRemoveFavoriteGameServerResult__SWIG_0();

		// Token: 0x0600093D RID: 2365
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveFavoriteGameServerResult_server_id_set")]
		public static extern void AsyncRemoveFavoriteGameServerResult_server_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600093E RID: 2366
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveFavoriteGameServerResult_server_id_get")]
		public static extern IntPtr AsyncRemoveFavoriteGameServerResult_server_id_get(IntPtr jarg1);

		// Token: 0x0600093F RID: 2367
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRemoveFavoriteGameServerResult__SWIG_1")]
		public static extern IntPtr new_AsyncRemoveFavoriteGameServerResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000940 RID: 2368
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncRemoveFavoriteGameServerResult")]
		public static extern void delete_AsyncRemoveFavoriteGameServerResult(IntPtr jarg1);

		// Token: 0x06000941 RID: 2369
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerPersonalInfo__SWIG_0")]
		public static extern IntPtr new_PlayerPersonalInfo__SWIG_0();

		// Token: 0x06000942 RID: 2370
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_id_set")]
		public static extern void PlayerPersonalInfo_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000943 RID: 2371
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_id_get")]
		public static extern IntPtr PlayerPersonalInfo_rail_id_get(IntPtr jarg1);

		// Token: 0x06000944 RID: 2372
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_error_code_set")]
		public static extern void PlayerPersonalInfo_error_code_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000945 RID: 2373
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_error_code_get")]
		public static extern int PlayerPersonalInfo_error_code_get(IntPtr jarg1);

		// Token: 0x06000946 RID: 2374
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_level_set")]
		public static extern void PlayerPersonalInfo_rail_level_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000947 RID: 2375
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_level_get")]
		public static extern uint PlayerPersonalInfo_rail_level_get(IntPtr jarg1);

		// Token: 0x06000948 RID: 2376
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_name_set")]
		public static extern void PlayerPersonalInfo_rail_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000949 RID: 2377
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_rail_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string PlayerPersonalInfo_rail_name_get(IntPtr jarg1);

		// Token: 0x0600094A RID: 2378
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_avatar_url_set")]
		public static extern void PlayerPersonalInfo_avatar_url_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600094B RID: 2379
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_avatar_url_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string PlayerPersonalInfo_avatar_url_get(IntPtr jarg1);

		// Token: 0x0600094C RID: 2380
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_email_address_set")]
		public static extern void PlayerPersonalInfo_email_address_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600094D RID: 2381
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerPersonalInfo_email_address_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string PlayerPersonalInfo_email_address_get(IntPtr jarg1);

		// Token: 0x0600094E RID: 2382
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerPersonalInfo__SWIG_1")]
		public static extern IntPtr new_PlayerPersonalInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x0600094F RID: 2383
		[DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerPersonalInfo")]
		public static extern void delete_PlayerPersonalInfo(IntPtr jarg1);

		// Token: 0x06000950 RID: 2384
		[DllImport("rail_api", EntryPoint = "CSharp_new_AcquireSessionTicketResponse__SWIG_0")]
		public static extern IntPtr new_AcquireSessionTicketResponse__SWIG_0();

		// Token: 0x06000951 RID: 2385
		[DllImport("rail_api", EntryPoint = "CSharp_AcquireSessionTicketResponse_session_ticket_set")]
		public static extern void AcquireSessionTicketResponse_session_ticket_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000952 RID: 2386
		[DllImport("rail_api", EntryPoint = "CSharp_AcquireSessionTicketResponse_session_ticket_get")]
		public static extern IntPtr AcquireSessionTicketResponse_session_ticket_get(IntPtr jarg1);

		// Token: 0x06000953 RID: 2387
		[DllImport("rail_api", EntryPoint = "CSharp_new_AcquireSessionTicketResponse__SWIG_1")]
		public static extern IntPtr new_AcquireSessionTicketResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000954 RID: 2388
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AcquireSessionTicketResponse")]
		public static extern void delete_AcquireSessionTicketResponse(IntPtr jarg1);

		// Token: 0x06000955 RID: 2389
		[DllImport("rail_api", EntryPoint = "CSharp_new_StartSessionWithPlayerResponse__SWIG_0")]
		public static extern IntPtr new_StartSessionWithPlayerResponse__SWIG_0();

		// Token: 0x06000956 RID: 2390
		[DllImport("rail_api", EntryPoint = "CSharp_StartSessionWithPlayerResponse_remote_rail_id_set")]
		public static extern void StartSessionWithPlayerResponse_remote_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000957 RID: 2391
		[DllImport("rail_api", EntryPoint = "CSharp_StartSessionWithPlayerResponse_remote_rail_id_get")]
		public static extern IntPtr StartSessionWithPlayerResponse_remote_rail_id_get(IntPtr jarg1);

		// Token: 0x06000958 RID: 2392
		[DllImport("rail_api", EntryPoint = "CSharp_new_StartSessionWithPlayerResponse__SWIG_1")]
		public static extern IntPtr new_StartSessionWithPlayerResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000959 RID: 2393
		[DllImport("rail_api", EntryPoint = "CSharp_delete_StartSessionWithPlayerResponse")]
		public static extern void delete_StartSessionWithPlayerResponse(IntPtr jarg1);

		// Token: 0x0600095A RID: 2394
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerGetGamePurchaseKeyResult__SWIG_0")]
		public static extern IntPtr new_PlayerGetGamePurchaseKeyResult__SWIG_0();

		// Token: 0x0600095B RID: 2395
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerGetGamePurchaseKeyResult_purchase_key_set")]
		public static extern void PlayerGetGamePurchaseKeyResult_purchase_key_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600095C RID: 2396
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerGetGamePurchaseKeyResult_purchase_key_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string PlayerGetGamePurchaseKeyResult_purchase_key_get(IntPtr jarg1);

		// Token: 0x0600095D RID: 2397
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerGetGamePurchaseKeyResult__SWIG_1")]
		public static extern IntPtr new_PlayerGetGamePurchaseKeyResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600095E RID: 2398
		[DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerGetGamePurchaseKeyResult")]
		public static extern void delete_PlayerGetGamePurchaseKeyResult(IntPtr jarg1);

		// Token: 0x0600095F RID: 2399
		[DllImport("rail_api", EntryPoint = "CSharp_new_QueryPlayerBannedStatus__SWIG_0")]
		public static extern IntPtr new_QueryPlayerBannedStatus__SWIG_0();

		// Token: 0x06000960 RID: 2400
		[DllImport("rail_api", EntryPoint = "CSharp_QueryPlayerBannedStatus_status_set")]
		public static extern void QueryPlayerBannedStatus_status_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000961 RID: 2401
		[DllImport("rail_api", EntryPoint = "CSharp_QueryPlayerBannedStatus_status_get")]
		public static extern int QueryPlayerBannedStatus_status_get(IntPtr jarg1);

		// Token: 0x06000962 RID: 2402
		[DllImport("rail_api", EntryPoint = "CSharp_new_QueryPlayerBannedStatus__SWIG_1")]
		public static extern IntPtr new_QueryPlayerBannedStatus__SWIG_1(IntPtr jarg1);

		// Token: 0x06000963 RID: 2403
		[DllImport("rail_api", EntryPoint = "CSharp_delete_QueryPlayerBannedStatus")]
		public static extern void delete_QueryPlayerBannedStatus(IntPtr jarg1);

		// Token: 0x06000964 RID: 2404
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetAuthenticateURLResult__SWIG_0")]
		public static extern IntPtr new_GetAuthenticateURLResult__SWIG_0();

		// Token: 0x06000965 RID: 2405
		[DllImport("rail_api", EntryPoint = "CSharp_GetAuthenticateURLResult_source_url_set")]
		public static extern void GetAuthenticateURLResult_source_url_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000966 RID: 2406
		[DllImport("rail_api", EntryPoint = "CSharp_GetAuthenticateURLResult_source_url_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GetAuthenticateURLResult_source_url_get(IntPtr jarg1);

		// Token: 0x06000967 RID: 2407
		[DllImport("rail_api", EntryPoint = "CSharp_GetAuthenticateURLResult_authenticate_url_set")]
		public static extern void GetAuthenticateURLResult_authenticate_url_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000968 RID: 2408
		[DllImport("rail_api", EntryPoint = "CSharp_GetAuthenticateURLResult_authenticate_url_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string GetAuthenticateURLResult_authenticate_url_get(IntPtr jarg1);

		// Token: 0x06000969 RID: 2409
		[DllImport("rail_api", EntryPoint = "CSharp_GetAuthenticateURLResult_ticket_expire_time_set")]
		public static extern void GetAuthenticateURLResult_ticket_expire_time_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600096A RID: 2410
		[DllImport("rail_api", EntryPoint = "CSharp_GetAuthenticateURLResult_ticket_expire_time_get")]
		public static extern uint GetAuthenticateURLResult_ticket_expire_time_get(IntPtr jarg1);

		// Token: 0x0600096B RID: 2411
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetAuthenticateURLResult__SWIG_1")]
		public static extern IntPtr new_GetAuthenticateURLResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600096C RID: 2412
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GetAuthenticateURLResult")]
		public static extern void delete_GetAuthenticateURLResult(IntPtr jarg1);

		// Token: 0x0600096D RID: 2413
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailAntiAddictionGameOnlineTimeChanged__SWIG_0")]
		public static extern IntPtr new_RailAntiAddictionGameOnlineTimeChanged__SWIG_0();

		// Token: 0x0600096E RID: 2414
		[DllImport("rail_api", EntryPoint = "CSharp_RailAntiAddictionGameOnlineTimeChanged_game_online_time_count_minutes_set")]
		public static extern void RailAntiAddictionGameOnlineTimeChanged_game_online_time_count_minutes_set(IntPtr jarg1, uint jarg2);

		// Token: 0x0600096F RID: 2415
		[DllImport("rail_api", EntryPoint = "CSharp_RailAntiAddictionGameOnlineTimeChanged_game_online_time_count_minutes_get")]
		public static extern uint RailAntiAddictionGameOnlineTimeChanged_game_online_time_count_minutes_get(IntPtr jarg1);

		// Token: 0x06000970 RID: 2416
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailAntiAddictionGameOnlineTimeChanged__SWIG_1")]
		public static extern IntPtr new_RailAntiAddictionGameOnlineTimeChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x06000971 RID: 2417
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailAntiAddictionGameOnlineTimeChanged")]
		public static extern void delete_RailAntiAddictionGameOnlineTimeChanged(IntPtr jarg1);

		// Token: 0x06000972 RID: 2418
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailKeyValueResult__SWIG_0")]
		public static extern IntPtr new_RailKeyValueResult__SWIG_0();

		// Token: 0x06000973 RID: 2419
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_error_code_set")]
		public static extern void RailKeyValueResult_error_code_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000974 RID: 2420
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_error_code_get")]
		public static extern int RailKeyValueResult_error_code_get(IntPtr jarg1);

		// Token: 0x06000975 RID: 2421
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_key_set")]
		public static extern void RailKeyValueResult_key_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000976 RID: 2422
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_key_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailKeyValueResult_key_get(IntPtr jarg1);

		// Token: 0x06000977 RID: 2423
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_value_set")]
		public static extern void RailKeyValueResult_value_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000978 RID: 2424
		[DllImport("rail_api", EntryPoint = "CSharp_RailKeyValueResult_value_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailKeyValueResult_value_get(IntPtr jarg1);

		// Token: 0x06000979 RID: 2425
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailKeyValueResult__SWIG_1")]
		public static extern IntPtr new_RailKeyValueResult__SWIG_1(IntPtr jarg1);

		// Token: 0x0600097A RID: 2426
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailKeyValueResult")]
		public static extern void delete_RailKeyValueResult(IntPtr jarg1);

		// Token: 0x0600097B RID: 2427
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUserPlayedWith__SWIG_0")]
		public static extern IntPtr new_RailUserPlayedWith__SWIG_0();

		// Token: 0x0600097C RID: 2428
		[DllImport("rail_api", EntryPoint = "CSharp_RailUserPlayedWith_rail_id_set")]
		public static extern void RailUserPlayedWith_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600097D RID: 2429
		[DllImport("rail_api", EntryPoint = "CSharp_RailUserPlayedWith_rail_id_get")]
		public static extern IntPtr RailUserPlayedWith_rail_id_get(IntPtr jarg1);

		// Token: 0x0600097E RID: 2430
		[DllImport("rail_api", EntryPoint = "CSharp_RailUserPlayedWith_user_rich_content_set")]
		public static extern void RailUserPlayedWith_user_rich_content_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x0600097F RID: 2431
		[DllImport("rail_api", EntryPoint = "CSharp_RailUserPlayedWith_user_rich_content_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailUserPlayedWith_user_rich_content_get(IntPtr jarg1);

		// Token: 0x06000980 RID: 2432
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailUserPlayedWith__SWIG_1")]
		public static extern IntPtr new_RailUserPlayedWith__SWIG_1(IntPtr jarg1);

		// Token: 0x06000981 RID: 2433
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailUserPlayedWith")]
		public static extern void delete_RailUserPlayedWith(IntPtr jarg1);

		// Token: 0x06000982 RID: 2434
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendPlayedGameInfo__SWIG_0")]
		public static extern IntPtr new_RailFriendPlayedGameInfo__SWIG_0();

		// Token: 0x06000983 RID: 2435
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_in_game_server_set")]
		public static extern void RailFriendPlayedGameInfo_in_game_server_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000984 RID: 2436
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_in_game_server_get")]
		public static extern bool RailFriendPlayedGameInfo_in_game_server_get(IntPtr jarg1);

		// Token: 0x06000985 RID: 2437
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_in_room_set")]
		public static extern void RailFriendPlayedGameInfo_in_room_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000986 RID: 2438
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_in_room_get")]
		public static extern bool RailFriendPlayedGameInfo_in_room_get(IntPtr jarg1);

		// Token: 0x06000987 RID: 2439
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_game_server_id_set")]
		public static extern void RailFriendPlayedGameInfo_game_server_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000988 RID: 2440
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_game_server_id_get")]
		public static extern ulong RailFriendPlayedGameInfo_game_server_id_get(IntPtr jarg1);

		// Token: 0x06000989 RID: 2441
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_room_id_set")]
		public static extern void RailFriendPlayedGameInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x0600098A RID: 2442
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_room_id_get")]
		public static extern ulong RailFriendPlayedGameInfo_room_id_get(IntPtr jarg1);

		// Token: 0x0600098B RID: 2443
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_friend_id_set")]
		public static extern void RailFriendPlayedGameInfo_friend_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600098C RID: 2444
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_friend_id_get")]
		public static extern IntPtr RailFriendPlayedGameInfo_friend_id_get(IntPtr jarg1);

		// Token: 0x0600098D RID: 2445
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_game_id_set")]
		public static extern void RailFriendPlayedGameInfo_game_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600098E RID: 2446
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_game_id_get")]
		public static extern IntPtr RailFriendPlayedGameInfo_game_id_get(IntPtr jarg1);

		// Token: 0x0600098F RID: 2447
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_friend_played_game_play_state_set")]
		public static extern void RailFriendPlayedGameInfo_friend_played_game_play_state_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000990 RID: 2448
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendPlayedGameInfo_friend_played_game_play_state_get")]
		public static extern int RailFriendPlayedGameInfo_friend_played_game_play_state_get(IntPtr jarg1);

		// Token: 0x06000991 RID: 2449
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendPlayedGameInfo__SWIG_1")]
		public static extern IntPtr new_RailFriendPlayedGameInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000992 RID: 2450
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendPlayedGameInfo")]
		public static extern void delete_RailFriendPlayedGameInfo(IntPtr jarg1);

		// Token: 0x06000993 RID: 2451
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlayedWithFriendsTimeItem__SWIG_0")]
		public static extern IntPtr new_RailPlayedWithFriendsTimeItem__SWIG_0();

		// Token: 0x06000994 RID: 2452
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlayedWithFriendsTimeItem_play_time_set")]
		public static extern void RailPlayedWithFriendsTimeItem_play_time_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000995 RID: 2453
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlayedWithFriendsTimeItem_play_time_get")]
		public static extern uint RailPlayedWithFriendsTimeItem_play_time_get(IntPtr jarg1);

		// Token: 0x06000996 RID: 2454
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlayedWithFriendsTimeItem_rail_id_set")]
		public static extern void RailPlayedWithFriendsTimeItem_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000997 RID: 2455
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlayedWithFriendsTimeItem_rail_id_get")]
		public static extern IntPtr RailPlayedWithFriendsTimeItem_rail_id_get(IntPtr jarg1);

		// Token: 0x06000998 RID: 2456
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlayedWithFriendsTimeItem__SWIG_1")]
		public static extern IntPtr new_RailPlayedWithFriendsTimeItem__SWIG_1(IntPtr jarg1);

		// Token: 0x06000999 RID: 2457
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailPlayedWithFriendsTimeItem")]
		public static extern void delete_RailPlayedWithFriendsTimeItem(IntPtr jarg1);

		// Token: 0x0600099A RID: 2458
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlayedWithFriendsGameItem__SWIG_0")]
		public static extern IntPtr new_RailPlayedWithFriendsGameItem__SWIG_0();

		// Token: 0x0600099B RID: 2459
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlayedWithFriendsGameItem_game_ids_set")]
		public static extern void RailPlayedWithFriendsGameItem_game_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600099C RID: 2460
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlayedWithFriendsGameItem_game_ids_get")]
		public static extern IntPtr RailPlayedWithFriendsGameItem_game_ids_get(IntPtr jarg1);

		// Token: 0x0600099D RID: 2461
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlayedWithFriendsGameItem_rail_id_set")]
		public static extern void RailPlayedWithFriendsGameItem_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x0600099E RID: 2462
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlayedWithFriendsGameItem_rail_id_get")]
		public static extern IntPtr RailPlayedWithFriendsGameItem_rail_id_get(IntPtr jarg1);

		// Token: 0x0600099F RID: 2463
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPlayedWithFriendsGameItem__SWIG_1")]
		public static extern IntPtr new_RailPlayedWithFriendsGameItem__SWIG_1(IntPtr jarg1);

		// Token: 0x060009A0 RID: 2464
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailPlayedWithFriendsGameItem")]
		public static extern void delete_RailPlayedWithFriendsGameItem(IntPtr jarg1);

		// Token: 0x060009A1 RID: 2465
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsAddFriendRequest__SWIG_0")]
		public static extern IntPtr new_RailFriendsAddFriendRequest__SWIG_0();

		// Token: 0x060009A2 RID: 2466
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsAddFriendRequest_target_rail_id_set")]
		public static extern void RailFriendsAddFriendRequest_target_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009A3 RID: 2467
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsAddFriendRequest_target_rail_id_get")]
		public static extern IntPtr RailFriendsAddFriendRequest_target_rail_id_get(IntPtr jarg1);

		// Token: 0x060009A4 RID: 2468
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsAddFriendRequest__SWIG_1")]
		public static extern IntPtr new_RailFriendsAddFriendRequest__SWIG_1(IntPtr jarg1);

		// Token: 0x060009A5 RID: 2469
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsAddFriendRequest")]
		public static extern void delete_RailFriendsAddFriendRequest(IntPtr jarg1);

		// Token: 0x060009A6 RID: 2470
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendOnLineState__SWIG_0")]
		public static extern IntPtr new_RailFriendOnLineState__SWIG_0();

		// Token: 0x060009A7 RID: 2471
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendOnLineState_friend_rail_id_set")]
		public static extern void RailFriendOnLineState_friend_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009A8 RID: 2472
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendOnLineState_friend_rail_id_get")]
		public static extern IntPtr RailFriendOnLineState_friend_rail_id_get(IntPtr jarg1);

		// Token: 0x060009A9 RID: 2473
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendOnLineState_friend_online_state_set")]
		public static extern void RailFriendOnLineState_friend_online_state_set(IntPtr jarg1, int jarg2);

		// Token: 0x060009AA RID: 2474
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendOnLineState_friend_online_state_get")]
		public static extern int RailFriendOnLineState_friend_online_state_get(IntPtr jarg1);

		// Token: 0x060009AB RID: 2475
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendOnLineState_game_define_game_playing_state_set")]
		public static extern void RailFriendOnLineState_game_define_game_playing_state_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060009AC RID: 2476
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendOnLineState_game_define_game_playing_state_get")]
		public static extern uint RailFriendOnLineState_game_define_game_playing_state_get(IntPtr jarg1);

		// Token: 0x060009AD RID: 2477
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendOnLineState__SWIG_1")]
		public static extern IntPtr new_RailFriendOnLineState__SWIG_1(IntPtr jarg1);

		// Token: 0x060009AE RID: 2478
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendOnLineState")]
		public static extern void delete_RailFriendOnLineState(IntPtr jarg1);

		// Token: 0x060009AF RID: 2479
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendInfo__SWIG_0")]
		public static extern IntPtr new_RailFriendInfo__SWIG_0();

		// Token: 0x060009B0 RID: 2480
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendInfo_friend_rail_id_set")]
		public static extern void RailFriendInfo_friend_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009B1 RID: 2481
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendInfo_friend_rail_id_get")]
		public static extern IntPtr RailFriendInfo_friend_rail_id_get(IntPtr jarg1);

		// Token: 0x060009B2 RID: 2482
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendInfo_friend_type_set")]
		public static extern void RailFriendInfo_friend_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060009B3 RID: 2483
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendInfo_friend_type_get")]
		public static extern int RailFriendInfo_friend_type_get(IntPtr jarg1);

		// Token: 0x060009B4 RID: 2484
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendInfo_online_state_set")]
		public static extern void RailFriendInfo_online_state_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009B5 RID: 2485
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendInfo_online_state_get")]
		public static extern IntPtr RailFriendInfo_online_state_get(IntPtr jarg1);

		// Token: 0x060009B6 RID: 2486
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendInfo__SWIG_1")]
		public static extern IntPtr new_RailFriendInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x060009B7 RID: 2487
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendInfo")]
		public static extern void delete_RailFriendInfo(IntPtr jarg1);

		// Token: 0x060009B8 RID: 2488
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsSetMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsSetMetadataResult__SWIG_0();

		// Token: 0x060009B9 RID: 2489
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsSetMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsSetMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009BA RID: 2490
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsSetMetadataResult")]
		public static extern void delete_RailFriendsSetMetadataResult(IntPtr jarg1);

		// Token: 0x060009BB RID: 2491
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsGetMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsGetMetadataResult__SWIG_0();

		// Token: 0x060009BC RID: 2492
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_friend_id_set")]
		public static extern void RailFriendsGetMetadataResult_friend_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009BD RID: 2493
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_friend_id_get")]
		public static extern IntPtr RailFriendsGetMetadataResult_friend_id_get(IntPtr jarg1);

		// Token: 0x060009BE RID: 2494
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_friend_kvs_set")]
		public static extern void RailFriendsGetMetadataResult_friend_kvs_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009BF RID: 2495
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_friend_kvs_get")]
		public static extern IntPtr RailFriendsGetMetadataResult_friend_kvs_get(IntPtr jarg1);

		// Token: 0x060009C0 RID: 2496
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsGetMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsGetMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009C1 RID: 2497
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsGetMetadataResult")]
		public static extern void delete_RailFriendsGetMetadataResult(IntPtr jarg1);

		// Token: 0x060009C2 RID: 2498
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsClearMetadataResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsClearMetadataResult__SWIG_0();

		// Token: 0x060009C3 RID: 2499
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsClearMetadataResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsClearMetadataResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009C4 RID: 2500
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsClearMetadataResult")]
		public static extern void delete_RailFriendsClearMetadataResult(IntPtr jarg1);

		// Token: 0x060009C5 RID: 2501
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsGetInviteCommandLine__SWIG_0")]
		public static extern IntPtr new_RailFriendsGetInviteCommandLine__SWIG_0();

		// Token: 0x060009C6 RID: 2502
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_friend_id_set")]
		public static extern void RailFriendsGetInviteCommandLine_friend_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009C7 RID: 2503
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_friend_id_get")]
		public static extern IntPtr RailFriendsGetInviteCommandLine_friend_id_get(IntPtr jarg1);

		// Token: 0x060009C8 RID: 2504
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_invite_command_line_set")]
		public static extern void RailFriendsGetInviteCommandLine_invite_command_line_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060009C9 RID: 2505
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_invite_command_line_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailFriendsGetInviteCommandLine_invite_command_line_get(IntPtr jarg1);

		// Token: 0x060009CA RID: 2506
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsGetInviteCommandLine__SWIG_1")]
		public static extern IntPtr new_RailFriendsGetInviteCommandLine__SWIG_1(IntPtr jarg1);

		// Token: 0x060009CB RID: 2507
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsGetInviteCommandLine")]
		public static extern void delete_RailFriendsGetInviteCommandLine(IntPtr jarg1);

		// Token: 0x060009CC RID: 2508
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsReportPlayedWithUserListResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsReportPlayedWithUserListResult__SWIG_0();

		// Token: 0x060009CD RID: 2509
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsReportPlayedWithUserListResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsReportPlayedWithUserListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009CE RID: 2510
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsReportPlayedWithUserListResult")]
		public static extern void delete_RailFriendsReportPlayedWithUserListResult(IntPtr jarg1);

		// Token: 0x060009CF RID: 2511
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsListChanged__SWIG_0")]
		public static extern IntPtr new_RailFriendsListChanged__SWIG_0();

		// Token: 0x060009D0 RID: 2512
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsListChanged__SWIG_1")]
		public static extern IntPtr new_RailFriendsListChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x060009D1 RID: 2513
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsListChanged")]
		public static extern void delete_RailFriendsListChanged(IntPtr jarg1);

		// Token: 0x060009D2 RID: 2514
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsQueryFriendPlayedGamesResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsQueryFriendPlayedGamesResult__SWIG_0();

		// Token: 0x060009D3 RID: 2515
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryFriendPlayedGamesResult_friend_played_games_info_list_set")]
		public static extern void RailFriendsQueryFriendPlayedGamesResult_friend_played_games_info_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009D4 RID: 2516
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryFriendPlayedGamesResult_friend_played_games_info_list_get")]
		public static extern IntPtr RailFriendsQueryFriendPlayedGamesResult_friend_played_games_info_list_get(IntPtr jarg1);

		// Token: 0x060009D5 RID: 2517
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsQueryFriendPlayedGamesResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsQueryFriendPlayedGamesResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009D6 RID: 2518
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsQueryFriendPlayedGamesResult")]
		public static extern void delete_RailFriendsQueryFriendPlayedGamesResult(IntPtr jarg1);

		// Token: 0x060009D7 RID: 2519
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsQueryPlayedWithFriendsListResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsQueryPlayedWithFriendsListResult__SWIG_0();

		// Token: 0x060009D8 RID: 2520
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsListResult_played_with_friends_list_set")]
		public static extern void RailFriendsQueryPlayedWithFriendsListResult_played_with_friends_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009D9 RID: 2521
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsListResult_played_with_friends_list_get")]
		public static extern IntPtr RailFriendsQueryPlayedWithFriendsListResult_played_with_friends_list_get(IntPtr jarg1);

		// Token: 0x060009DA RID: 2522
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsQueryPlayedWithFriendsListResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsQueryPlayedWithFriendsListResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009DB RID: 2523
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsQueryPlayedWithFriendsListResult")]
		public static extern void delete_RailFriendsQueryPlayedWithFriendsListResult(IntPtr jarg1);

		// Token: 0x060009DC RID: 2524
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsQueryPlayedWithFriendsTimeResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsQueryPlayedWithFriendsTimeResult__SWIG_0();

		// Token: 0x060009DD RID: 2525
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsTimeResult_played_with_friends_time_list_set")]
		public static extern void RailFriendsQueryPlayedWithFriendsTimeResult_played_with_friends_time_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009DE RID: 2526
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsTimeResult_played_with_friends_time_list_get")]
		public static extern IntPtr RailFriendsQueryPlayedWithFriendsTimeResult_played_with_friends_time_list_get(IntPtr jarg1);

		// Token: 0x060009DF RID: 2527
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsQueryPlayedWithFriendsTimeResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsQueryPlayedWithFriendsTimeResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009E0 RID: 2528
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsQueryPlayedWithFriendsTimeResult")]
		public static extern void delete_RailFriendsQueryPlayedWithFriendsTimeResult(IntPtr jarg1);

		// Token: 0x060009E1 RID: 2529
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsQueryPlayedWithFriendsGamesResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsQueryPlayedWithFriendsGamesResult__SWIG_0();

		// Token: 0x060009E2 RID: 2530
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsGamesResult_played_with_friends_game_list_set")]
		public static extern void RailFriendsQueryPlayedWithFriendsGamesResult_played_with_friends_game_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009E3 RID: 2531
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsGamesResult_played_with_friends_game_list_get")]
		public static extern IntPtr RailFriendsQueryPlayedWithFriendsGamesResult_played_with_friends_game_list_get(IntPtr jarg1);

		// Token: 0x060009E4 RID: 2532
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsQueryPlayedWithFriendsGamesResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsQueryPlayedWithFriendsGamesResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009E5 RID: 2533
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsQueryPlayedWithFriendsGamesResult")]
		public static extern void delete_RailFriendsQueryPlayedWithFriendsGamesResult(IntPtr jarg1);

		// Token: 0x060009E6 RID: 2534
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsAddFriendResult__SWIG_0")]
		public static extern IntPtr new_RailFriendsAddFriendResult__SWIG_0();

		// Token: 0x060009E7 RID: 2535
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsAddFriendResult_target_rail_id_set")]
		public static extern void RailFriendsAddFriendResult_target_rail_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009E8 RID: 2536
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsAddFriendResult_target_rail_id_get")]
		public static extern IntPtr RailFriendsAddFriendResult_target_rail_id_get(IntPtr jarg1);

		// Token: 0x060009E9 RID: 2537
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsAddFriendResult__SWIG_1")]
		public static extern IntPtr new_RailFriendsAddFriendResult__SWIG_1(IntPtr jarg1);

		// Token: 0x060009EA RID: 2538
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsAddFriendResult")]
		public static extern void delete_RailFriendsAddFriendResult(IntPtr jarg1);

		// Token: 0x060009EB RID: 2539
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsOnlineStateChanged__SWIG_0")]
		public static extern IntPtr new_RailFriendsOnlineStateChanged__SWIG_0();

		// Token: 0x060009EC RID: 2540
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsOnlineStateChanged_friend_online_state_set")]
		public static extern void RailFriendsOnlineStateChanged_friend_online_state_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x060009ED RID: 2541
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsOnlineStateChanged_friend_online_state_get")]
		public static extern IntPtr RailFriendsOnlineStateChanged_friend_online_state_get(IntPtr jarg1);

		// Token: 0x060009EE RID: 2542
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailFriendsOnlineStateChanged__SWIG_1")]
		public static extern IntPtr new_RailFriendsOnlineStateChanged__SWIG_1(IntPtr jarg1);

		// Token: 0x060009EF RID: 2543
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailFriendsOnlineStateChanged")]
		public static extern void delete_RailFriendsOnlineStateChanged(IntPtr jarg1);

		// Token: 0x060009F0 RID: 2544
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDiscountInfo__SWIG_0")]
		public static extern IntPtr new_RailDiscountInfo__SWIG_0();

		// Token: 0x060009F1 RID: 2545
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_off_set")]
		public static extern void RailDiscountInfo_off_set(IntPtr jarg1, float jarg2);

		// Token: 0x060009F2 RID: 2546
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_off_get")]
		public static extern float RailDiscountInfo_off_get(IntPtr jarg1);

		// Token: 0x060009F3 RID: 2547
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_discount_price_set")]
		public static extern void RailDiscountInfo_discount_price_set(IntPtr jarg1, float jarg2);

		// Token: 0x060009F4 RID: 2548
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_discount_price_get")]
		public static extern float RailDiscountInfo_discount_price_get(IntPtr jarg1);

		// Token: 0x060009F5 RID: 2549
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_type_set")]
		public static extern void RailDiscountInfo_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x060009F6 RID: 2550
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_type_get")]
		public static extern int RailDiscountInfo_type_get(IntPtr jarg1);

		// Token: 0x060009F7 RID: 2551
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_start_time_set")]
		public static extern void RailDiscountInfo_start_time_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060009F8 RID: 2552
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_start_time_get")]
		public static extern uint RailDiscountInfo_start_time_get(IntPtr jarg1);

		// Token: 0x060009F9 RID: 2553
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_end_time_set")]
		public static extern void RailDiscountInfo_end_time_set(IntPtr jarg1, uint jarg2);

		// Token: 0x060009FA RID: 2554
		[DllImport("rail_api", EntryPoint = "CSharp_RailDiscountInfo_end_time_get")]
		public static extern uint RailDiscountInfo_end_time_get(IntPtr jarg1);

		// Token: 0x060009FB RID: 2555
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailDiscountInfo__SWIG_1")]
		public static extern IntPtr new_RailDiscountInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x060009FC RID: 2556
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailDiscountInfo")]
		public static extern void delete_RailDiscountInfo(IntPtr jarg1);

		// Token: 0x060009FD RID: 2557
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPurchaseProductExtraInfo__SWIG_0")]
		public static extern IntPtr new_RailPurchaseProductExtraInfo__SWIG_0();

		// Token: 0x060009FE RID: 2558
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductExtraInfo_exchange_rule_set")]
		public static extern void RailPurchaseProductExtraInfo_exchange_rule_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x060009FF RID: 2559
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductExtraInfo_exchange_rule_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPurchaseProductExtraInfo_exchange_rule_get(IntPtr jarg1);

		// Token: 0x06000A00 RID: 2560
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductExtraInfo_bundle_rule_set")]
		public static extern void RailPurchaseProductExtraInfo_bundle_rule_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A01 RID: 2561
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductExtraInfo_bundle_rule_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPurchaseProductExtraInfo_bundle_rule_get(IntPtr jarg1);

		// Token: 0x06000A02 RID: 2562
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPurchaseProductExtraInfo__SWIG_1")]
		public static extern IntPtr new_RailPurchaseProductExtraInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A03 RID: 2563
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailPurchaseProductExtraInfo")]
		public static extern void delete_RailPurchaseProductExtraInfo(IntPtr jarg1);

		// Token: 0x06000A04 RID: 2564
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPurchaseProductInfo__SWIG_0")]
		public static extern IntPtr new_RailPurchaseProductInfo__SWIG_0();

		// Token: 0x06000A05 RID: 2565
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_product_id_set")]
		public static extern void RailPurchaseProductInfo_product_id_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000A06 RID: 2566
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_product_id_get")]
		public static extern uint RailPurchaseProductInfo_product_id_get(IntPtr jarg1);

		// Token: 0x06000A07 RID: 2567
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_is_purchasable_set")]
		public static extern void RailPurchaseProductInfo_is_purchasable_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000A08 RID: 2568
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_is_purchasable_get")]
		public static extern bool RailPurchaseProductInfo_is_purchasable_get(IntPtr jarg1);

		// Token: 0x06000A09 RID: 2569
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_name_set")]
		public static extern void RailPurchaseProductInfo_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A0A RID: 2570
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPurchaseProductInfo_name_get(IntPtr jarg1);

		// Token: 0x06000A0B RID: 2571
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_description_set")]
		public static extern void RailPurchaseProductInfo_description_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A0C RID: 2572
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_description_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPurchaseProductInfo_description_get(IntPtr jarg1);

		// Token: 0x06000A0D RID: 2573
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_category_set")]
		public static extern void RailPurchaseProductInfo_category_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A0E RID: 2574
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_category_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPurchaseProductInfo_category_get(IntPtr jarg1);

		// Token: 0x06000A0F RID: 2575
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_product_thumbnail_set")]
		public static extern void RailPurchaseProductInfo_product_thumbnail_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A10 RID: 2576
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_product_thumbnail_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPurchaseProductInfo_product_thumbnail_get(IntPtr jarg1);

		// Token: 0x06000A11 RID: 2577
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_extra_info_set")]
		public static extern void RailPurchaseProductInfo_extra_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A12 RID: 2578
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_extra_info_get")]
		public static extern IntPtr RailPurchaseProductInfo_extra_info_get(IntPtr jarg1);

		// Token: 0x06000A13 RID: 2579
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_original_price_set")]
		public static extern void RailPurchaseProductInfo_original_price_set(IntPtr jarg1, float jarg2);

		// Token: 0x06000A14 RID: 2580
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_original_price_get")]
		public static extern float RailPurchaseProductInfo_original_price_get(IntPtr jarg1);

		// Token: 0x06000A15 RID: 2581
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_currency_type_set")]
		public static extern void RailPurchaseProductInfo_currency_type_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A16 RID: 2582
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_currency_type_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPurchaseProductInfo_currency_type_get(IntPtr jarg1);

		// Token: 0x06000A17 RID: 2583
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_discount_set")]
		public static extern void RailPurchaseProductInfo_discount_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A18 RID: 2584
		[DllImport("rail_api", EntryPoint = "CSharp_RailPurchaseProductInfo_discount_get")]
		public static extern IntPtr RailPurchaseProductInfo_discount_get(IntPtr jarg1);

		// Token: 0x06000A19 RID: 2585
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPurchaseProductInfo__SWIG_1")]
		public static extern IntPtr new_RailPurchaseProductInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A1A RID: 2586
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailPurchaseProductInfo")]
		public static extern void delete_RailPurchaseProductInfo(IntPtr jarg1);

		// Token: 0x06000A1B RID: 2587
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseRequestAllPurchasableProductsResponse__SWIG_0")]
		public static extern IntPtr new_RailInGamePurchaseRequestAllPurchasableProductsResponse__SWIG_0();

		// Token: 0x06000A1C RID: 2588
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_set")]
		public static extern void RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A1D RID: 2589
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get")]
		public static extern IntPtr RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get(IntPtr jarg1);

		// Token: 0x06000A1E RID: 2590
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseRequestAllPurchasableProductsResponse__SWIG_1")]
		public static extern IntPtr new_RailInGamePurchaseRequestAllPurchasableProductsResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A1F RID: 2591
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchaseRequestAllPurchasableProductsResponse")]
		public static extern void delete_RailInGamePurchaseRequestAllPurchasableProductsResponse(IntPtr jarg1);

		// Token: 0x06000A20 RID: 2592
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseRequestAllProductsResponse__SWIG_0")]
		public static extern IntPtr new_RailInGamePurchaseRequestAllProductsResponse__SWIG_0();

		// Token: 0x06000A21 RID: 2593
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllProductsResponse_all_products_set")]
		public static extern void RailInGamePurchaseRequestAllProductsResponse_all_products_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A22 RID: 2594
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllProductsResponse_all_products_get")]
		public static extern IntPtr RailInGamePurchaseRequestAllProductsResponse_all_products_get(IntPtr jarg1);

		// Token: 0x06000A23 RID: 2595
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseRequestAllProductsResponse__SWIG_1")]
		public static extern IntPtr new_RailInGamePurchaseRequestAllProductsResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A24 RID: 2596
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchaseRequestAllProductsResponse")]
		public static extern void delete_RailInGamePurchaseRequestAllProductsResponse(IntPtr jarg1);

		// Token: 0x06000A25 RID: 2597
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchasePurchaseProductsResponse__SWIG_0")]
		public static extern IntPtr new_RailInGamePurchasePurchaseProductsResponse__SWIG_0();

		// Token: 0x06000A26 RID: 2598
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_order_id_set")]
		public static extern void RailInGamePurchasePurchaseProductsResponse_order_id_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A27 RID: 2599
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_order_id_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailInGamePurchasePurchaseProductsResponse_order_id_get(IntPtr jarg1);

		// Token: 0x06000A28 RID: 2600
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_delivered_products_set")]
		public static extern void RailInGamePurchasePurchaseProductsResponse_delivered_products_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A29 RID: 2601
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_delivered_products_get")]
		public static extern IntPtr RailInGamePurchasePurchaseProductsResponse_delivered_products_get(IntPtr jarg1);

		// Token: 0x06000A2A RID: 2602
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchasePurchaseProductsResponse__SWIG_1")]
		public static extern IntPtr new_RailInGamePurchasePurchaseProductsResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A2B RID: 2603
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchasePurchaseProductsResponse")]
		public static extern void delete_RailInGamePurchasePurchaseProductsResponse(IntPtr jarg1);

		// Token: 0x06000A2C RID: 2604
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchasePurchaseProductsToAssetsResponse__SWIG_0")]
		public static extern IntPtr new_RailInGamePurchasePurchaseProductsToAssetsResponse__SWIG_0();

		// Token: 0x06000A2D RID: 2605
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_set")]
		public static extern void RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A2E RID: 2606
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_get(IntPtr jarg1);

		// Token: 0x06000A2F RID: 2607
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_delivered_assets_set")]
		public static extern void RailInGamePurchasePurchaseProductsToAssetsResponse_delivered_assets_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A30 RID: 2608
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_delivered_assets_get")]
		public static extern IntPtr RailInGamePurchasePurchaseProductsToAssetsResponse_delivered_assets_get(IntPtr jarg1);

		// Token: 0x06000A31 RID: 2609
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchasePurchaseProductsToAssetsResponse__SWIG_1")]
		public static extern IntPtr new_RailInGamePurchasePurchaseProductsToAssetsResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A32 RID: 2610
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchasePurchaseProductsToAssetsResponse")]
		public static extern void delete_RailInGamePurchasePurchaseProductsToAssetsResponse(IntPtr jarg1);

		// Token: 0x06000A33 RID: 2611
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseFinishOrderResponse__SWIG_0")]
		public static extern IntPtr new_RailInGamePurchaseFinishOrderResponse__SWIG_0();

		// Token: 0x06000A34 RID: 2612
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseFinishOrderResponse_order_id_set")]
		public static extern void RailInGamePurchaseFinishOrderResponse_order_id_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A35 RID: 2613
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseFinishOrderResponse_order_id_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailInGamePurchaseFinishOrderResponse_order_id_get(IntPtr jarg1);

		// Token: 0x06000A36 RID: 2614
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGamePurchaseFinishOrderResponse__SWIG_1")]
		public static extern IntPtr new_RailInGamePurchaseFinishOrderResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A37 RID: 2615
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGamePurchaseFinishOrderResponse")]
		public static extern void delete_RailInGamePurchaseFinishOrderResponse(IntPtr jarg1);

		// Token: 0x06000A38 RID: 2616
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchasePayWindowDisplayed__SWIG_0")]
		public static extern IntPtr new_RailInGameStorePurchasePayWindowDisplayed__SWIG_0();

		// Token: 0x06000A39 RID: 2617
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowDisplayed_order_id_set")]
		public static extern void RailInGameStorePurchasePayWindowDisplayed_order_id_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A3A RID: 2618
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowDisplayed_order_id_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailInGameStorePurchasePayWindowDisplayed_order_id_get(IntPtr jarg1);

		// Token: 0x06000A3B RID: 2619
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchasePayWindowDisplayed__SWIG_1")]
		public static extern IntPtr new_RailInGameStorePurchasePayWindowDisplayed__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A3C RID: 2620
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGameStorePurchasePayWindowDisplayed")]
		public static extern void delete_RailInGameStorePurchasePayWindowDisplayed(IntPtr jarg1);

		// Token: 0x06000A3D RID: 2621
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchasePayWindowClosed__SWIG_0")]
		public static extern IntPtr new_RailInGameStorePurchasePayWindowClosed__SWIG_0();

		// Token: 0x06000A3E RID: 2622
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowClosed_order_id_set")]
		public static extern void RailInGameStorePurchasePayWindowClosed_order_id_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A3F RID: 2623
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowClosed_order_id_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailInGameStorePurchasePayWindowClosed_order_id_get(IntPtr jarg1);

		// Token: 0x06000A40 RID: 2624
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchasePayWindowClosed__SWIG_1")]
		public static extern IntPtr new_RailInGameStorePurchasePayWindowClosed__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A41 RID: 2625
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGameStorePurchasePayWindowClosed")]
		public static extern void delete_RailInGameStorePurchasePayWindowClosed(IntPtr jarg1);

		// Token: 0x06000A42 RID: 2626
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchaseResult__SWIG_0")]
		public static extern IntPtr new_RailInGameStorePurchaseResult__SWIG_0();

		// Token: 0x06000A43 RID: 2627
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchaseResult_order_id_set")]
		public static extern void RailInGameStorePurchaseResult_order_id_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A44 RID: 2628
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchaseResult_order_id_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailInGameStorePurchaseResult_order_id_get(IntPtr jarg1);

		// Token: 0x06000A45 RID: 2629
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailInGameStorePurchaseResult__SWIG_1")]
		public static extern IntPtr new_RailInGameStorePurchaseResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A46 RID: 2630
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailInGameStorePurchaseResult")]
		public static extern void delete_RailInGameStorePurchaseResult(IntPtr jarg1);

		// Token: 0x06000A47 RID: 2631
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailNetworkSessionState__SWIG_0")]
		public static extern IntPtr new_RailNetworkSessionState__SWIG_0();

		// Token: 0x06000A48 RID: 2632
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_is_connection_active_set")]
		public static extern void RailNetworkSessionState_is_connection_active_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000A49 RID: 2633
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_is_connection_active_get")]
		public static extern bool RailNetworkSessionState_is_connection_active_get(IntPtr jarg1);

		// Token: 0x06000A4A RID: 2634
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_is_connecting_set")]
		public static extern void RailNetworkSessionState_is_connecting_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000A4B RID: 2635
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_is_connecting_get")]
		public static extern bool RailNetworkSessionState_is_connecting_get(IntPtr jarg1);

		// Token: 0x06000A4C RID: 2636
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_is_using_relay_set")]
		public static extern void RailNetworkSessionState_is_using_relay_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000A4D RID: 2637
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_is_using_relay_get")]
		public static extern bool RailNetworkSessionState_is_using_relay_get(IntPtr jarg1);

		// Token: 0x06000A4E RID: 2638
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_session_error_set")]
		public static extern void RailNetworkSessionState_session_error_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000A4F RID: 2639
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_session_error_get")]
		public static extern int RailNetworkSessionState_session_error_get(IntPtr jarg1);

		// Token: 0x06000A50 RID: 2640
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_bytes_in_send_buffer_set")]
		public static extern void RailNetworkSessionState_bytes_in_send_buffer_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000A51 RID: 2641
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_bytes_in_send_buffer_get")]
		public static extern uint RailNetworkSessionState_bytes_in_send_buffer_get(IntPtr jarg1);

		// Token: 0x06000A52 RID: 2642
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_packets_in_send_buffer_set")]
		public static extern void RailNetworkSessionState_packets_in_send_buffer_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000A53 RID: 2643
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_packets_in_send_buffer_get")]
		public static extern uint RailNetworkSessionState_packets_in_send_buffer_get(IntPtr jarg1);

		// Token: 0x06000A54 RID: 2644
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_remote_ip_set")]
		public static extern void RailNetworkSessionState_remote_ip_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000A55 RID: 2645
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_remote_ip_get")]
		public static extern uint RailNetworkSessionState_remote_ip_get(IntPtr jarg1);

		// Token: 0x06000A56 RID: 2646
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_remote_port_set")]
		public static extern void RailNetworkSessionState_remote_port_set(IntPtr jarg1, ushort jarg2);

		// Token: 0x06000A57 RID: 2647
		[DllImport("rail_api", EntryPoint = "CSharp_RailNetworkSessionState_remote_port_get")]
		public static extern ushort RailNetworkSessionState_remote_port_get(IntPtr jarg1);

		// Token: 0x06000A58 RID: 2648
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailNetworkSessionState__SWIG_1")]
		public static extern IntPtr new_RailNetworkSessionState__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A59 RID: 2649
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailNetworkSessionState")]
		public static extern void delete_RailNetworkSessionState(IntPtr jarg1);

		// Token: 0x06000A5A RID: 2650
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateSessionRequest__SWIG_0")]
		public static extern IntPtr new_CreateSessionRequest__SWIG_0();

		// Token: 0x06000A5B RID: 2651
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_local_peer_set")]
		public static extern void CreateSessionRequest_local_peer_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A5C RID: 2652
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_local_peer_get")]
		public static extern IntPtr CreateSessionRequest_local_peer_get(IntPtr jarg1);

		// Token: 0x06000A5D RID: 2653
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_remote_peer_set")]
		public static extern void CreateSessionRequest_remote_peer_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A5E RID: 2654
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_remote_peer_get")]
		public static extern IntPtr CreateSessionRequest_remote_peer_get(IntPtr jarg1);

		// Token: 0x06000A5F RID: 2655
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateSessionRequest__SWIG_1")]
		public static extern IntPtr new_CreateSessionRequest__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A60 RID: 2656
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateSessionRequest")]
		public static extern void delete_CreateSessionRequest(IntPtr jarg1);

		// Token: 0x06000A61 RID: 2657
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateSessionFailed__SWIG_0")]
		public static extern IntPtr new_CreateSessionFailed__SWIG_0();

		// Token: 0x06000A62 RID: 2658
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionFailed_local_peer_set")]
		public static extern void CreateSessionFailed_local_peer_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A63 RID: 2659
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionFailed_local_peer_get")]
		public static extern IntPtr CreateSessionFailed_local_peer_get(IntPtr jarg1);

		// Token: 0x06000A64 RID: 2660
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionFailed_remote_peer_set")]
		public static extern void CreateSessionFailed_remote_peer_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A65 RID: 2661
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionFailed_remote_peer_get")]
		public static extern IntPtr CreateSessionFailed_remote_peer_get(IntPtr jarg1);

		// Token: 0x06000A66 RID: 2662
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateSessionFailed__SWIG_1")]
		public static extern IntPtr new_CreateSessionFailed__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A67 RID: 2663
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateSessionFailed")]
		public static extern void delete_CreateSessionFailed(IntPtr jarg1);

		// Token: 0x06000A68 RID: 2664
		[DllImport("rail_api", EntryPoint = "CSharp_RAIL_DEFAULT_MAX_ROOM_MEMBERS_get")]
		public static extern int RAIL_DEFAULT_MAX_ROOM_MEMBERS_get();

		// Token: 0x06000A69 RID: 2665
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomOptions__SWIG_0")]
		public static extern IntPtr new_RoomOptions__SWIG_0(ulong jarg1);

		// Token: 0x06000A6A RID: 2666
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_type_set")]
		public static extern void RoomOptions_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000A6B RID: 2667
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_type_get")]
		public static extern int RoomOptions_type_get(IntPtr jarg1);

		// Token: 0x06000A6C RID: 2668
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_max_members_set")]
		public static extern void RoomOptions_max_members_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000A6D RID: 2669
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_max_members_get")]
		public static extern uint RoomOptions_max_members_get(IntPtr jarg1);

		// Token: 0x06000A6E RID: 2670
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_zone_id_set")]
		public static extern void RoomOptions_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000A6F RID: 2671
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_zone_id_get")]
		public static extern ulong RoomOptions_zone_id_get(IntPtr jarg1);

		// Token: 0x06000A70 RID: 2672
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_password_set")]
		public static extern void RoomOptions_password_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A71 RID: 2673
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_password_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RoomOptions_password_get(IntPtr jarg1);

		// Token: 0x06000A72 RID: 2674
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_enable_team_voice_set")]
		public static extern void RoomOptions_enable_team_voice_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000A73 RID: 2675
		[DllImport("rail_api", EntryPoint = "CSharp_RoomOptions_enable_team_voice_get")]
		public static extern bool RoomOptions_enable_team_voice_get(IntPtr jarg1);

		// Token: 0x06000A74 RID: 2676
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomOptions__SWIG_1")]
		public static extern IntPtr new_RoomOptions__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A75 RID: 2677
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomOptions")]
		public static extern void delete_RoomOptions(IntPtr jarg1);

		// Token: 0x06000A76 RID: 2678
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListSorter__SWIG_0")]
		public static extern IntPtr new_RoomInfoListSorter__SWIG_0();

		// Token: 0x06000A77 RID: 2679
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_value_type_set")]
		public static extern void RoomInfoListSorter_property_value_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000A78 RID: 2680
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_value_type_get")]
		public static extern int RoomInfoListSorter_property_value_type_get(IntPtr jarg1);

		// Token: 0x06000A79 RID: 2681
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_sort_type_set")]
		public static extern void RoomInfoListSorter_property_sort_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000A7A RID: 2682
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_sort_type_get")]
		public static extern int RoomInfoListSorter_property_sort_type_get(IntPtr jarg1);

		// Token: 0x06000A7B RID: 2683
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_key_set")]
		public static extern void RoomInfoListSorter_property_key_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A7C RID: 2684
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_property_key_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RoomInfoListSorter_property_key_get(IntPtr jarg1);

		// Token: 0x06000A7D RID: 2685
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_close_to_value_set")]
		public static extern void RoomInfoListSorter_close_to_value_set(IntPtr jarg1, double jarg2);

		// Token: 0x06000A7E RID: 2686
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListSorter_close_to_value_get")]
		public static extern double RoomInfoListSorter_close_to_value_get(IntPtr jarg1);

		// Token: 0x06000A7F RID: 2687
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListSorter__SWIG_1")]
		public static extern IntPtr new_RoomInfoListSorter__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A80 RID: 2688
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfoListSorter")]
		public static extern void delete_RoomInfoListSorter(IntPtr jarg1);

		// Token: 0x06000A81 RID: 2689
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListFilterKey__SWIG_0")]
		public static extern IntPtr new_RoomInfoListFilterKey__SWIG_0();

		// Token: 0x06000A82 RID: 2690
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_key_name_set")]
		public static extern void RoomInfoListFilterKey_key_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A83 RID: 2691
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_key_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RoomInfoListFilterKey_key_name_get(IntPtr jarg1);

		// Token: 0x06000A84 RID: 2692
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_value_type_set")]
		public static extern void RoomInfoListFilterKey_value_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000A85 RID: 2693
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_value_type_get")]
		public static extern int RoomInfoListFilterKey_value_type_get(IntPtr jarg1);

		// Token: 0x06000A86 RID: 2694
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_comparison_type_set")]
		public static extern void RoomInfoListFilterKey_comparison_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000A87 RID: 2695
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_comparison_type_get")]
		public static extern int RoomInfoListFilterKey_comparison_type_get(IntPtr jarg1);

		// Token: 0x06000A88 RID: 2696
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_filter_value_set")]
		public static extern void RoomInfoListFilterKey_filter_value_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A89 RID: 2697
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilterKey_filter_value_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RoomInfoListFilterKey_filter_value_get(IntPtr jarg1);

		// Token: 0x06000A8A RID: 2698
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListFilterKey__SWIG_1")]
		public static extern IntPtr new_RoomInfoListFilterKey__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A8B RID: 2699
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfoListFilterKey")]
		public static extern void delete_RoomInfoListFilterKey(IntPtr jarg1);

		// Token: 0x06000A8C RID: 2700
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListFilter__SWIG_0")]
		public static extern IntPtr new_RoomInfoListFilter__SWIG_0();

		// Token: 0x06000A8D RID: 2701
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_key_filters_set")]
		public static extern void RoomInfoListFilter_key_filters_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000A8E RID: 2702
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_key_filters_get")]
		public static extern IntPtr RoomInfoListFilter_key_filters_get(IntPtr jarg1);

		// Token: 0x06000A8F RID: 2703
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_room_name_contained_set")]
		public static extern void RoomInfoListFilter_room_name_contained_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000A90 RID: 2704
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_room_name_contained_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RoomInfoListFilter_room_name_contained_get(IntPtr jarg1);

		// Token: 0x06000A91 RID: 2705
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_filter_password_set")]
		public static extern void RoomInfoListFilter_filter_password_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000A92 RID: 2706
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_filter_password_get")]
		public static extern int RoomInfoListFilter_filter_password_get(IntPtr jarg1);

		// Token: 0x06000A93 RID: 2707
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_filter_friends_owned_set")]
		public static extern void RoomInfoListFilter_filter_friends_owned_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000A94 RID: 2708
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_filter_friends_owned_get")]
		public static extern int RoomInfoListFilter_filter_friends_owned_get(IntPtr jarg1);

		// Token: 0x06000A95 RID: 2709
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_available_slot_at_least_set")]
		public static extern void RoomInfoListFilter_available_slot_at_least_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000A96 RID: 2710
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoListFilter_available_slot_at_least_get")]
		public static extern uint RoomInfoListFilter_available_slot_at_least_get(IntPtr jarg1);

		// Token: 0x06000A97 RID: 2711
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoListFilter__SWIG_1")]
		public static extern IntPtr new_RoomInfoListFilter__SWIG_1(IntPtr jarg1);

		// Token: 0x06000A98 RID: 2712
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfoListFilter")]
		public static extern void delete_RoomInfoListFilter(IntPtr jarg1);

		// Token: 0x06000A99 RID: 2713
		[DllImport("rail_api", EntryPoint = "CSharp_new_ZoneInfo__SWIG_0")]
		public static extern IntPtr new_ZoneInfo__SWIG_0();

		// Token: 0x06000A9A RID: 2714
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_zone_id_set")]
		public static extern void ZoneInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000A9B RID: 2715
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_zone_id_get")]
		public static extern ulong ZoneInfo_zone_id_get(IntPtr jarg1);

		// Token: 0x06000A9C RID: 2716
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_idc_id_set")]
		public static extern void ZoneInfo_idc_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000A9D RID: 2717
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_idc_id_get")]
		public static extern ulong ZoneInfo_idc_id_get(IntPtr jarg1);

		// Token: 0x06000A9E RID: 2718
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_country_code_set")]
		public static extern void ZoneInfo_country_code_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000A9F RID: 2719
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_country_code_get")]
		public static extern uint ZoneInfo_country_code_get(IntPtr jarg1);

		// Token: 0x06000AA0 RID: 2720
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_status_set")]
		public static extern void ZoneInfo_status_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000AA1 RID: 2721
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_status_get")]
		public static extern int ZoneInfo_status_get(IntPtr jarg1);

		// Token: 0x06000AA2 RID: 2722
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_name_set")]
		public static extern void ZoneInfo_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000AA3 RID: 2723
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string ZoneInfo_name_get(IntPtr jarg1);

		// Token: 0x06000AA4 RID: 2724
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_description_set")]
		public static extern void ZoneInfo_description_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000AA5 RID: 2725
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfo_description_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string ZoneInfo_description_get(IntPtr jarg1);

		// Token: 0x06000AA6 RID: 2726
		[DllImport("rail_api", EntryPoint = "CSharp_new_ZoneInfo__SWIG_1")]
		public static extern IntPtr new_ZoneInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000AA7 RID: 2727
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ZoneInfo")]
		public static extern void delete_ZoneInfo(IntPtr jarg1);

		// Token: 0x06000AA8 RID: 2728
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfo__SWIG_0")]
		public static extern IntPtr new_RoomInfo__SWIG_0();

		// Token: 0x06000AA9 RID: 2729
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_zone_id_set")]
		public static extern void RoomInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AAA RID: 2730
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_zone_id_get")]
		public static extern ulong RoomInfo_zone_id_get(IntPtr jarg1);

		// Token: 0x06000AAB RID: 2731
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_id_set")]
		public static extern void RoomInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AAC RID: 2732
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_id_get")]
		public static extern ulong RoomInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000AAD RID: 2733
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_owner_id_set")]
		public static extern void RoomInfo_owner_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000AAE RID: 2734
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_owner_id_get")]
		public static extern IntPtr RoomInfo_owner_id_get(IntPtr jarg1);

		// Token: 0x06000AAF RID: 2735
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_state_set")]
		public static extern void RoomInfo_room_state_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000AB0 RID: 2736
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_state_get")]
		public static extern int RoomInfo_room_state_get(IntPtr jarg1);

		// Token: 0x06000AB1 RID: 2737
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_max_members_set")]
		public static extern void RoomInfo_max_members_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000AB2 RID: 2738
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_max_members_get")]
		public static extern uint RoomInfo_max_members_get(IntPtr jarg1);

		// Token: 0x06000AB3 RID: 2739
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_current_members_set")]
		public static extern void RoomInfo_current_members_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000AB4 RID: 2740
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_current_members_get")]
		public static extern uint RoomInfo_current_members_get(IntPtr jarg1);

		// Token: 0x06000AB5 RID: 2741
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_create_time_set")]
		public static extern void RoomInfo_create_time_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000AB6 RID: 2742
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_create_time_get")]
		public static extern uint RoomInfo_create_time_get(IntPtr jarg1);

		// Token: 0x06000AB7 RID: 2743
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_name_set")]
		public static extern void RoomInfo_room_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000AB8 RID: 2744
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RoomInfo_room_name_get(IntPtr jarg1);

		// Token: 0x06000AB9 RID: 2745
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_has_password_set")]
		public static extern void RoomInfo_has_password_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000ABA RID: 2746
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_has_password_get")]
		public static extern bool RoomInfo_has_password_get(IntPtr jarg1);

		// Token: 0x06000ABB RID: 2747
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_is_joinable_set")]
		public static extern void RoomInfo_is_joinable_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000ABC RID: 2748
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_is_joinable_get")]
		public static extern bool RoomInfo_is_joinable_get(IntPtr jarg1);

		// Token: 0x06000ABD RID: 2749
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_type_set")]
		public static extern void RoomInfo_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000ABE RID: 2750
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_type_get")]
		public static extern int RoomInfo_type_get(IntPtr jarg1);

		// Token: 0x06000ABF RID: 2751
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_game_server_rail_id_set")]
		public static extern void RoomInfo_game_server_rail_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AC0 RID: 2752
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_game_server_rail_id_get")]
		public static extern ulong RoomInfo_game_server_rail_id_get(IntPtr jarg1);

		// Token: 0x06000AC1 RID: 2753
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_kvs_set")]
		public static extern void RoomInfo_room_kvs_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000AC2 RID: 2754
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfo_room_kvs_get")]
		public static extern IntPtr RoomInfo_room_kvs_get(IntPtr jarg1);

		// Token: 0x06000AC3 RID: 2755
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfo__SWIG_1")]
		public static extern IntPtr new_RoomInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000AC4 RID: 2756
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfo")]
		public static extern void delete_RoomInfo(IntPtr jarg1);

		// Token: 0x06000AC5 RID: 2757
		[DllImport("rail_api", EntryPoint = "CSharp_new_MemberInfo__SWIG_0")]
		public static extern IntPtr new_MemberInfo__SWIG_0();

		// Token: 0x06000AC6 RID: 2758
		[DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_room_id_set")]
		public static extern void MemberInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AC7 RID: 2759
		[DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_room_id_get")]
		public static extern ulong MemberInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000AC8 RID: 2760
		[DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_id_set")]
		public static extern void MemberInfo_member_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AC9 RID: 2761
		[DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_id_get")]
		public static extern ulong MemberInfo_member_id_get(IntPtr jarg1);

		// Token: 0x06000ACA RID: 2762
		[DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_index_set")]
		public static extern void MemberInfo_member_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000ACB RID: 2763
		[DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_index_get")]
		public static extern uint MemberInfo_member_index_get(IntPtr jarg1);

		// Token: 0x06000ACC RID: 2764
		[DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_name_set")]
		public static extern void MemberInfo_member_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000ACD RID: 2765
		[DllImport("rail_api", EntryPoint = "CSharp_MemberInfo_member_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string MemberInfo_member_name_get(IntPtr jarg1);

		// Token: 0x06000ACE RID: 2766
		[DllImport("rail_api", EntryPoint = "CSharp_new_MemberInfo__SWIG_1")]
		public static extern IntPtr new_MemberInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000ACF RID: 2767
		[DllImport("rail_api", EntryPoint = "CSharp_delete_MemberInfo")]
		public static extern void delete_MemberInfo(IntPtr jarg1);

		// Token: 0x06000AD0 RID: 2768
		[DllImport("rail_api", EntryPoint = "CSharp_new_ZoneInfoList__SWIG_0")]
		public static extern IntPtr new_ZoneInfoList__SWIG_0();

		// Token: 0x06000AD1 RID: 2769
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfoList_zone_info_set")]
		public static extern void ZoneInfoList_zone_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000AD2 RID: 2770
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfoList_zone_info_get")]
		public static extern IntPtr ZoneInfoList_zone_info_get(IntPtr jarg1);

		// Token: 0x06000AD3 RID: 2771
		[DllImport("rail_api", EntryPoint = "CSharp_new_ZoneInfoList__SWIG_1")]
		public static extern IntPtr new_ZoneInfoList__SWIG_1(IntPtr jarg1);

		// Token: 0x06000AD4 RID: 2772
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ZoneInfoList")]
		public static extern void delete_ZoneInfoList(IntPtr jarg1);

		// Token: 0x06000AD5 RID: 2773
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoList__SWIG_0")]
		public static extern IntPtr new_RoomInfoList__SWIG_0();

		// Token: 0x06000AD6 RID: 2774
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_zone_id_set")]
		public static extern void RoomInfoList_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AD7 RID: 2775
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_zone_id_get")]
		public static extern ulong RoomInfoList_zone_id_get(IntPtr jarg1);

		// Token: 0x06000AD8 RID: 2776
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_begin_index_set")]
		public static extern void RoomInfoList_begin_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000AD9 RID: 2777
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_begin_index_get")]
		public static extern uint RoomInfoList_begin_index_get(IntPtr jarg1);

		// Token: 0x06000ADA RID: 2778
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_end_index_set")]
		public static extern void RoomInfoList_end_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000ADB RID: 2779
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_end_index_get")]
		public static extern uint RoomInfoList_end_index_get(IntPtr jarg1);

		// Token: 0x06000ADC RID: 2780
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_total_room_num_in_zone_set")]
		public static extern void RoomInfoList_total_room_num_in_zone_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000ADD RID: 2781
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_total_room_num_in_zone_get")]
		public static extern uint RoomInfoList_total_room_num_in_zone_get(IntPtr jarg1);

		// Token: 0x06000ADE RID: 2782
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_room_info_set")]
		public static extern void RoomInfoList_room_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ADF RID: 2783
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_room_info_get")]
		public static extern IntPtr RoomInfoList_room_info_get(IntPtr jarg1);

		// Token: 0x06000AE0 RID: 2784
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomInfoList__SWIG_1")]
		public static extern IntPtr new_RoomInfoList__SWIG_1(IntPtr jarg1);

		// Token: 0x06000AE1 RID: 2785
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomInfoList")]
		public static extern void delete_RoomInfoList(IntPtr jarg1);

		// Token: 0x06000AE2 RID: 2786
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomAllData__SWIG_0")]
		public static extern IntPtr new_RoomAllData__SWIG_0();

		// Token: 0x06000AE3 RID: 2787
		[DllImport("rail_api", EntryPoint = "CSharp_RoomAllData_room_info_set")]
		public static extern void RoomAllData_room_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000AE4 RID: 2788
		[DllImport("rail_api", EntryPoint = "CSharp_RoomAllData_room_info_get")]
		public static extern IntPtr RoomAllData_room_info_get(IntPtr jarg1);

		// Token: 0x06000AE5 RID: 2789
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomAllData__SWIG_1")]
		public static extern IntPtr new_RoomAllData__SWIG_1(IntPtr jarg1);

		// Token: 0x06000AE6 RID: 2790
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomAllData")]
		public static extern void delete_RoomAllData(IntPtr jarg1);

		// Token: 0x06000AE7 RID: 2791
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateRoomInfo__SWIG_0")]
		public static extern IntPtr new_CreateRoomInfo__SWIG_0();

		// Token: 0x06000AE8 RID: 2792
		[DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_zone_id_set")]
		public static extern void CreateRoomInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AE9 RID: 2793
		[DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_zone_id_get")]
		public static extern ulong CreateRoomInfo_zone_id_get(IntPtr jarg1);

		// Token: 0x06000AEA RID: 2794
		[DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_room_id_set")]
		public static extern void CreateRoomInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AEB RID: 2795
		[DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_room_id_get")]
		public static extern ulong CreateRoomInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000AEC RID: 2796
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateRoomInfo__SWIG_1")]
		public static extern IntPtr new_CreateRoomInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000AED RID: 2797
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateRoomInfo")]
		public static extern void delete_CreateRoomInfo(IntPtr jarg1);

		// Token: 0x06000AEE RID: 2798
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomMembersInfo__SWIG_0")]
		public static extern IntPtr new_RoomMembersInfo__SWIG_0();

		// Token: 0x06000AEF RID: 2799
		[DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_room_id_set")]
		public static extern void RoomMembersInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AF0 RID: 2800
		[DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_room_id_get")]
		public static extern ulong RoomMembersInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000AF1 RID: 2801
		[DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_member_num_set")]
		public static extern void RoomMembersInfo_member_num_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000AF2 RID: 2802
		[DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_member_num_get")]
		public static extern uint RoomMembersInfo_member_num_get(IntPtr jarg1);

		// Token: 0x06000AF3 RID: 2803
		[DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_member_info_set")]
		public static extern void RoomMembersInfo_member_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000AF4 RID: 2804
		[DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_member_info_get")]
		public static extern IntPtr RoomMembersInfo_member_info_get(IntPtr jarg1);

		// Token: 0x06000AF5 RID: 2805
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomMembersInfo__SWIG_1")]
		public static extern IntPtr new_RoomMembersInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000AF6 RID: 2806
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomMembersInfo")]
		public static extern void delete_RoomMembersInfo(IntPtr jarg1);

		// Token: 0x06000AF7 RID: 2807
		[DllImport("rail_api", EntryPoint = "CSharp_new_JoinRoomInfo__SWIG_0")]
		public static extern IntPtr new_JoinRoomInfo__SWIG_0();

		// Token: 0x06000AF8 RID: 2808
		[DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_zone_id_set")]
		public static extern void JoinRoomInfo_zone_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AF9 RID: 2809
		[DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_zone_id_get")]
		public static extern ulong JoinRoomInfo_zone_id_get(IntPtr jarg1);

		// Token: 0x06000AFA RID: 2810
		[DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_room_id_set")]
		public static extern void JoinRoomInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000AFB RID: 2811
		[DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_room_id_get")]
		public static extern ulong JoinRoomInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000AFC RID: 2812
		[DllImport("rail_api", EntryPoint = "CSharp_new_JoinRoomInfo__SWIG_1")]
		public static extern IntPtr new_JoinRoomInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000AFD RID: 2813
		[DllImport("rail_api", EntryPoint = "CSharp_delete_JoinRoomInfo")]
		public static extern void delete_JoinRoomInfo(IntPtr jarg1);

		// Token: 0x06000AFE RID: 2814
		[DllImport("rail_api", EntryPoint = "CSharp_new_KickOffMemberInfo__SWIG_0")]
		public static extern IntPtr new_KickOffMemberInfo__SWIG_0();

		// Token: 0x06000AFF RID: 2815
		[DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_room_id_set")]
		public static extern void KickOffMemberInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B00 RID: 2816
		[DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_room_id_get")]
		public static extern ulong KickOffMemberInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000B01 RID: 2817
		[DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_kicked_id_set")]
		public static extern void KickOffMemberInfo_kicked_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B02 RID: 2818
		[DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_kicked_id_get")]
		public static extern ulong KickOffMemberInfo_kicked_id_get(IntPtr jarg1);

		// Token: 0x06000B03 RID: 2819
		[DllImport("rail_api", EntryPoint = "CSharp_new_KickOffMemberInfo__SWIG_1")]
		public static extern IntPtr new_KickOffMemberInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B04 RID: 2820
		[DllImport("rail_api", EntryPoint = "CSharp_delete_KickOffMemberInfo")]
		public static extern void delete_KickOffMemberInfo(IntPtr jarg1);

		// Token: 0x06000B05 RID: 2821
		[DllImport("rail_api", EntryPoint = "CSharp_new_SetRoomMetadataInfo__SWIG_0")]
		public static extern IntPtr new_SetRoomMetadataInfo__SWIG_0();

		// Token: 0x06000B06 RID: 2822
		[DllImport("rail_api", EntryPoint = "CSharp_SetRoomMetadataInfo_room_id_set")]
		public static extern void SetRoomMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B07 RID: 2823
		[DllImport("rail_api", EntryPoint = "CSharp_SetRoomMetadataInfo_room_id_get")]
		public static extern ulong SetRoomMetadataInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000B08 RID: 2824
		[DllImport("rail_api", EntryPoint = "CSharp_new_SetRoomMetadataInfo__SWIG_1")]
		public static extern IntPtr new_SetRoomMetadataInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B09 RID: 2825
		[DllImport("rail_api", EntryPoint = "CSharp_delete_SetRoomMetadataInfo")]
		public static extern void delete_SetRoomMetadataInfo(IntPtr jarg1);

		// Token: 0x06000B0A RID: 2826
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetRoomMetadataInfo__SWIG_0")]
		public static extern IntPtr new_GetRoomMetadataInfo__SWIG_0();

		// Token: 0x06000B0B RID: 2827
		[DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_room_id_set")]
		public static extern void GetRoomMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B0C RID: 2828
		[DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_room_id_get")]
		public static extern ulong GetRoomMetadataInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000B0D RID: 2829
		[DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_key_value_set")]
		public static extern void GetRoomMetadataInfo_key_value_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000B0E RID: 2830
		[DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_key_value_get")]
		public static extern IntPtr GetRoomMetadataInfo_key_value_get(IntPtr jarg1);

		// Token: 0x06000B0F RID: 2831
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetRoomMetadataInfo__SWIG_1")]
		public static extern IntPtr new_GetRoomMetadataInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B10 RID: 2832
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GetRoomMetadataInfo")]
		public static extern void delete_GetRoomMetadataInfo(IntPtr jarg1);

		// Token: 0x06000B11 RID: 2833
		[DllImport("rail_api", EntryPoint = "CSharp_new_ClearRoomMetadataInfo__SWIG_0")]
		public static extern IntPtr new_ClearRoomMetadataInfo__SWIG_0();

		// Token: 0x06000B12 RID: 2834
		[DllImport("rail_api", EntryPoint = "CSharp_ClearRoomMetadataInfo_room_id_set")]
		public static extern void ClearRoomMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B13 RID: 2835
		[DllImport("rail_api", EntryPoint = "CSharp_ClearRoomMetadataInfo_room_id_get")]
		public static extern ulong ClearRoomMetadataInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000B14 RID: 2836
		[DllImport("rail_api", EntryPoint = "CSharp_new_ClearRoomMetadataInfo__SWIG_1")]
		public static extern IntPtr new_ClearRoomMetadataInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B15 RID: 2837
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ClearRoomMetadataInfo")]
		public static extern void delete_ClearRoomMetadataInfo(IntPtr jarg1);

		// Token: 0x06000B16 RID: 2838
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetMemberMetadataInfo__SWIG_0")]
		public static extern IntPtr new_GetMemberMetadataInfo__SWIG_0();

		// Token: 0x06000B17 RID: 2839
		[DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_room_id_set")]
		public static extern void GetMemberMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B18 RID: 2840
		[DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_room_id_get")]
		public static extern ulong GetMemberMetadataInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000B19 RID: 2841
		[DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_member_id_set")]
		public static extern void GetMemberMetadataInfo_member_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B1A RID: 2842
		[DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_member_id_get")]
		public static extern ulong GetMemberMetadataInfo_member_id_get(IntPtr jarg1);

		// Token: 0x06000B1B RID: 2843
		[DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_key_value_set")]
		public static extern void GetMemberMetadataInfo_key_value_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000B1C RID: 2844
		[DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_key_value_get")]
		public static extern IntPtr GetMemberMetadataInfo_key_value_get(IntPtr jarg1);

		// Token: 0x06000B1D RID: 2845
		[DllImport("rail_api", EntryPoint = "CSharp_new_GetMemberMetadataInfo__SWIG_1")]
		public static extern IntPtr new_GetMemberMetadataInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B1E RID: 2846
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GetMemberMetadataInfo")]
		public static extern void delete_GetMemberMetadataInfo(IntPtr jarg1);

		// Token: 0x06000B1F RID: 2847
		[DllImport("rail_api", EntryPoint = "CSharp_new_SetMemberMetadataInfo__SWIG_0")]
		public static extern IntPtr new_SetMemberMetadataInfo__SWIG_0();

		// Token: 0x06000B20 RID: 2848
		[DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_room_id_set")]
		public static extern void SetMemberMetadataInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B21 RID: 2849
		[DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_room_id_get")]
		public static extern ulong SetMemberMetadataInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000B22 RID: 2850
		[DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_member_id_set")]
		public static extern void SetMemberMetadataInfo_member_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B23 RID: 2851
		[DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_member_id_get")]
		public static extern ulong SetMemberMetadataInfo_member_id_get(IntPtr jarg1);

		// Token: 0x06000B24 RID: 2852
		[DllImport("rail_api", EntryPoint = "CSharp_new_SetMemberMetadataInfo__SWIG_1")]
		public static extern IntPtr new_SetMemberMetadataInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B25 RID: 2853
		[DllImport("rail_api", EntryPoint = "CSharp_delete_SetMemberMetadataInfo")]
		public static extern void delete_SetMemberMetadataInfo(IntPtr jarg1);

		// Token: 0x06000B26 RID: 2854
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaveRoomInfo__SWIG_0")]
		public static extern IntPtr new_LeaveRoomInfo__SWIG_0();

		// Token: 0x06000B27 RID: 2855
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_room_id_set")]
		public static extern void LeaveRoomInfo_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B28 RID: 2856
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_room_id_get")]
		public static extern ulong LeaveRoomInfo_room_id_get(IntPtr jarg1);

		// Token: 0x06000B29 RID: 2857
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_reason_set")]
		public static extern void LeaveRoomInfo_reason_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000B2A RID: 2858
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_reason_get")]
		public static extern int LeaveRoomInfo_reason_get(IntPtr jarg1);

		// Token: 0x06000B2B RID: 2859
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaveRoomInfo__SWIG_1")]
		public static extern IntPtr new_LeaveRoomInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B2C RID: 2860
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaveRoomInfo")]
		public static extern void delete_LeaveRoomInfo(IntPtr jarg1);

		// Token: 0x06000B2D RID: 2861
		[DllImport("rail_api", EntryPoint = "CSharp_new_UserRoomListInfo__SWIG_0")]
		public static extern IntPtr new_UserRoomListInfo__SWIG_0();

		// Token: 0x06000B2E RID: 2862
		[DllImport("rail_api", EntryPoint = "CSharp_UserRoomListInfo_room_info_set")]
		public static extern void UserRoomListInfo_room_info_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000B2F RID: 2863
		[DllImport("rail_api", EntryPoint = "CSharp_UserRoomListInfo_room_info_get")]
		public static extern IntPtr UserRoomListInfo_room_info_get(IntPtr jarg1);

		// Token: 0x06000B30 RID: 2864
		[DllImport("rail_api", EntryPoint = "CSharp_new_UserRoomListInfo__SWIG_1")]
		public static extern IntPtr new_UserRoomListInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B31 RID: 2865
		[DllImport("rail_api", EntryPoint = "CSharp_delete_UserRoomListInfo")]
		public static extern void delete_UserRoomListInfo(IntPtr jarg1);

		// Token: 0x06000B32 RID: 2866
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyMetadataChange__SWIG_0")]
		public static extern IntPtr new_NotifyMetadataChange__SWIG_0();

		// Token: 0x06000B33 RID: 2867
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_room_id_set")]
		public static extern void NotifyMetadataChange_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B34 RID: 2868
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_room_id_get")]
		public static extern ulong NotifyMetadataChange_room_id_get(IntPtr jarg1);

		// Token: 0x06000B35 RID: 2869
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_changer_id_set")]
		public static extern void NotifyMetadataChange_changer_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B36 RID: 2870
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_changer_id_get")]
		public static extern ulong NotifyMetadataChange_changer_id_get(IntPtr jarg1);

		// Token: 0x06000B37 RID: 2871
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyMetadataChange__SWIG_1")]
		public static extern IntPtr new_NotifyMetadataChange__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B38 RID: 2872
		[DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyMetadataChange")]
		public static extern void delete_NotifyMetadataChange(IntPtr jarg1);

		// Token: 0x06000B39 RID: 2873
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomMemberChange__SWIG_0")]
		public static extern IntPtr new_NotifyRoomMemberChange__SWIG_0();

		// Token: 0x06000B3A RID: 2874
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_room_id_set")]
		public static extern void NotifyRoomMemberChange_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B3B RID: 2875
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_room_id_get")]
		public static extern ulong NotifyRoomMemberChange_room_id_get(IntPtr jarg1);

		// Token: 0x06000B3C RID: 2876
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_changer_id_set")]
		public static extern void NotifyRoomMemberChange_changer_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B3D RID: 2877
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_changer_id_get")]
		public static extern ulong NotifyRoomMemberChange_changer_id_get(IntPtr jarg1);

		// Token: 0x06000B3E RID: 2878
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_id_for_making_change_set")]
		public static extern void NotifyRoomMemberChange_id_for_making_change_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B3F RID: 2879
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_id_for_making_change_get")]
		public static extern ulong NotifyRoomMemberChange_id_for_making_change_get(IntPtr jarg1);

		// Token: 0x06000B40 RID: 2880
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_state_change_set")]
		public static extern void NotifyRoomMemberChange_state_change_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000B41 RID: 2881
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_state_change_get")]
		public static extern int NotifyRoomMemberChange_state_change_get(IntPtr jarg1);

		// Token: 0x06000B42 RID: 2882
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomMemberChange__SWIG_1")]
		public static extern IntPtr new_NotifyRoomMemberChange__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B43 RID: 2883
		[DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomMemberChange")]
		public static extern void delete_NotifyRoomMemberChange(IntPtr jarg1);

		// Token: 0x06000B44 RID: 2884
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomMemberKicked__SWIG_0")]
		public static extern IntPtr new_NotifyRoomMemberKicked__SWIG_0();

		// Token: 0x06000B45 RID: 2885
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_room_id_set")]
		public static extern void NotifyRoomMemberKicked_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B46 RID: 2886
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_room_id_get")]
		public static extern ulong NotifyRoomMemberKicked_room_id_get(IntPtr jarg1);

		// Token: 0x06000B47 RID: 2887
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_id_for_making_kick_set")]
		public static extern void NotifyRoomMemberKicked_id_for_making_kick_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B48 RID: 2888
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_id_for_making_kick_get")]
		public static extern ulong NotifyRoomMemberKicked_id_for_making_kick_get(IntPtr jarg1);

		// Token: 0x06000B49 RID: 2889
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_kicked_id_set")]
		public static extern void NotifyRoomMemberKicked_kicked_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B4A RID: 2890
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_kicked_id_get")]
		public static extern ulong NotifyRoomMemberKicked_kicked_id_get(IntPtr jarg1);

		// Token: 0x06000B4B RID: 2891
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_due_to_kicker_lost_connect_set")]
		public static extern void NotifyRoomMemberKicked_due_to_kicker_lost_connect_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000B4C RID: 2892
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_due_to_kicker_lost_connect_get")]
		public static extern uint NotifyRoomMemberKicked_due_to_kicker_lost_connect_get(IntPtr jarg1);

		// Token: 0x06000B4D RID: 2893
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomMemberKicked__SWIG_1")]
		public static extern IntPtr new_NotifyRoomMemberKicked__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B4E RID: 2894
		[DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomMemberKicked")]
		public static extern void delete_NotifyRoomMemberKicked(IntPtr jarg1);

		// Token: 0x06000B4F RID: 2895
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomDestroy__SWIG_0")]
		public static extern IntPtr new_NotifyRoomDestroy__SWIG_0();

		// Token: 0x06000B50 RID: 2896
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomDestroy_room_id_set")]
		public static extern void NotifyRoomDestroy_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B51 RID: 2897
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomDestroy_room_id_get")]
		public static extern ulong NotifyRoomDestroy_room_id_get(IntPtr jarg1);

		// Token: 0x06000B52 RID: 2898
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomDestroy__SWIG_1")]
		public static extern IntPtr new_NotifyRoomDestroy__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B53 RID: 2899
		[DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomDestroy")]
		public static extern void delete_NotifyRoomDestroy(IntPtr jarg1);

		// Token: 0x06000B54 RID: 2900
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomDataReceived__SWIG_0")]
		public static extern IntPtr new_RoomDataReceived__SWIG_0();

		// Token: 0x06000B55 RID: 2901
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_remote_peer_set")]
		public static extern void RoomDataReceived_remote_peer_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000B56 RID: 2902
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_remote_peer_get")]
		public static extern IntPtr RoomDataReceived_remote_peer_get(IntPtr jarg1);

		// Token: 0x06000B57 RID: 2903
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_message_type_set")]
		public static extern void RoomDataReceived_message_type_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000B58 RID: 2904
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_message_type_get")]
		public static extern uint RoomDataReceived_message_type_get(IntPtr jarg1);

		// Token: 0x06000B59 RID: 2905
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_data_len_set")]
		public static extern void RoomDataReceived_data_len_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000B5A RID: 2906
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_data_len_get")]
		public static extern uint RoomDataReceived_data_len_get(IntPtr jarg1);

		// Token: 0x06000B5B RID: 2907
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_data_buf_set")]
		public static extern void RoomDataReceived_data_buf_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000B5C RID: 2908
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_data_buf_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RoomDataReceived_data_buf_get(IntPtr jarg1);

		// Token: 0x06000B5D RID: 2909
		[DllImport("rail_api", EntryPoint = "CSharp_new_RoomDataReceived__SWIG_1")]
		public static extern IntPtr new_RoomDataReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B5E RID: 2910
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RoomDataReceived")]
		public static extern void delete_RoomDataReceived(IntPtr jarg1);

		// Token: 0x06000B5F RID: 2911
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomOwnerChange__SWIG_0")]
		public static extern IntPtr new_NotifyRoomOwnerChange__SWIG_0();

		// Token: 0x06000B60 RID: 2912
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_room_id_set")]
		public static extern void NotifyRoomOwnerChange_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B61 RID: 2913
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_room_id_get")]
		public static extern ulong NotifyRoomOwnerChange_room_id_get(IntPtr jarg1);

		// Token: 0x06000B62 RID: 2914
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_old_owner_id_set")]
		public static extern void NotifyRoomOwnerChange_old_owner_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B63 RID: 2915
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_old_owner_id_get")]
		public static extern ulong NotifyRoomOwnerChange_old_owner_id_get(IntPtr jarg1);

		// Token: 0x06000B64 RID: 2916
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_new_owner_id_set")]
		public static extern void NotifyRoomOwnerChange_new_owner_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B65 RID: 2917
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_new_owner_id_get")]
		public static extern ulong NotifyRoomOwnerChange_new_owner_id_get(IntPtr jarg1);

		// Token: 0x06000B66 RID: 2918
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_reason_set")]
		public static extern void NotifyRoomOwnerChange_reason_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000B67 RID: 2919
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_reason_get")]
		public static extern int NotifyRoomOwnerChange_reason_get(IntPtr jarg1);

		// Token: 0x06000B68 RID: 2920
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomOwnerChange__SWIG_1")]
		public static extern IntPtr new_NotifyRoomOwnerChange__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B69 RID: 2921
		[DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomOwnerChange")]
		public static extern void delete_NotifyRoomOwnerChange(IntPtr jarg1);

		// Token: 0x06000B6A RID: 2922
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomGameServerChange__SWIG_0")]
		public static extern IntPtr new_NotifyRoomGameServerChange__SWIG_0();

		// Token: 0x06000B6B RID: 2923
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_room_id_set")]
		public static extern void NotifyRoomGameServerChange_room_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B6C RID: 2924
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_room_id_get")]
		public static extern ulong NotifyRoomGameServerChange_room_id_get(IntPtr jarg1);

		// Token: 0x06000B6D RID: 2925
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_game_server_rail_id_set")]
		public static extern void NotifyRoomGameServerChange_game_server_rail_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B6E RID: 2926
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_game_server_rail_id_get")]
		public static extern ulong NotifyRoomGameServerChange_game_server_rail_id_get(IntPtr jarg1);

		// Token: 0x06000B6F RID: 2927
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_game_server_channel_id_set")]
		public static extern void NotifyRoomGameServerChange_game_server_channel_id_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B70 RID: 2928
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_game_server_channel_id_get")]
		public static extern ulong NotifyRoomGameServerChange_game_server_channel_id_get(IntPtr jarg1);

		// Token: 0x06000B71 RID: 2929
		[DllImport("rail_api", EntryPoint = "CSharp_new_NotifyRoomGameServerChange__SWIG_1")]
		public static extern IntPtr new_NotifyRoomGameServerChange__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B72 RID: 2930
		[DllImport("rail_api", EntryPoint = "CSharp_delete_NotifyRoomGameServerChange")]
		public static extern void delete_NotifyRoomGameServerChange(IntPtr jarg1);

		// Token: 0x06000B73 RID: 2931
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSyncFileOption__SWIG_0")]
		public static extern IntPtr new_RailSyncFileOption__SWIG_0();

		// Token: 0x06000B74 RID: 2932
		[DllImport("rail_api", EntryPoint = "CSharp_RailSyncFileOption_sync_file_not_to_remote_set")]
		public static extern void RailSyncFileOption_sync_file_not_to_remote_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000B75 RID: 2933
		[DllImport("rail_api", EntryPoint = "CSharp_RailSyncFileOption_sync_file_not_to_remote_get")]
		public static extern bool RailSyncFileOption_sync_file_not_to_remote_get(IntPtr jarg1);

		// Token: 0x06000B76 RID: 2934
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSyncFileOption__SWIG_1")]
		public static extern IntPtr new_RailSyncFileOption__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B77 RID: 2935
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSyncFileOption")]
		public static extern void delete_RailSyncFileOption(IntPtr jarg1);

		// Token: 0x06000B78 RID: 2936
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailStreamFileOption__SWIG_0")]
		public static extern IntPtr new_RailStreamFileOption__SWIG_0();

		// Token: 0x06000B79 RID: 2937
		[DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileOption_unavaliabe_when_new_file_writing_set")]
		public static extern void RailStreamFileOption_unavaliabe_when_new_file_writing_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000B7A RID: 2938
		[DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileOption_unavaliabe_when_new_file_writing_get")]
		public static extern bool RailStreamFileOption_unavaliabe_when_new_file_writing_get(IntPtr jarg1);

		// Token: 0x06000B7B RID: 2939
		[DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileOption_open_type_set")]
		public static extern void RailStreamFileOption_open_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000B7C RID: 2940
		[DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileOption_open_type_get")]
		public static extern int RailStreamFileOption_open_type_get(IntPtr jarg1);

		// Token: 0x06000B7D RID: 2941
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailStreamFileOption__SWIG_1")]
		public static extern IntPtr new_RailStreamFileOption__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B7E RID: 2942
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailStreamFileOption")]
		public static extern void delete_RailStreamFileOption(IntPtr jarg1);

		// Token: 0x06000B7F RID: 2943
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailListStreamFileOption__SWIG_0")]
		public static extern IntPtr new_RailListStreamFileOption__SWIG_0();

		// Token: 0x06000B80 RID: 2944
		[DllImport("rail_api", EntryPoint = "CSharp_RailListStreamFileOption_start_index_set")]
		public static extern void RailListStreamFileOption_start_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000B81 RID: 2945
		[DllImport("rail_api", EntryPoint = "CSharp_RailListStreamFileOption_start_index_get")]
		public static extern uint RailListStreamFileOption_start_index_get(IntPtr jarg1);

		// Token: 0x06000B82 RID: 2946
		[DllImport("rail_api", EntryPoint = "CSharp_RailListStreamFileOption_num_files_set")]
		public static extern void RailListStreamFileOption_num_files_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000B83 RID: 2947
		[DllImport("rail_api", EntryPoint = "CSharp_RailListStreamFileOption_num_files_get")]
		public static extern uint RailListStreamFileOption_num_files_get(IntPtr jarg1);

		// Token: 0x06000B84 RID: 2948
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailListStreamFileOption__SWIG_1")]
		public static extern IntPtr new_RailListStreamFileOption__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B85 RID: 2949
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailListStreamFileOption")]
		public static extern void delete_RailListStreamFileOption(IntPtr jarg1);

		// Token: 0x06000B86 RID: 2950
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPublishFileToUserSpaceOption__SWIG_0")]
		public static extern IntPtr new_RailPublishFileToUserSpaceOption__SWIG_0();

		// Token: 0x06000B87 RID: 2951
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_type_set")]
		public static extern void RailPublishFileToUserSpaceOption_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000B88 RID: 2952
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_type_get")]
		public static extern int RailPublishFileToUserSpaceOption_type_get(IntPtr jarg1);

		// Token: 0x06000B89 RID: 2953
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_space_work_name_set")]
		public static extern void RailPublishFileToUserSpaceOption_space_work_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000B8A RID: 2954
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_space_work_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPublishFileToUserSpaceOption_space_work_name_get(IntPtr jarg1);

		// Token: 0x06000B8B RID: 2955
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_description_set")]
		public static extern void RailPublishFileToUserSpaceOption_description_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000B8C RID: 2956
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_description_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPublishFileToUserSpaceOption_description_get(IntPtr jarg1);

		// Token: 0x06000B8D RID: 2957
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_preview_path_filename_set")]
		public static extern void RailPublishFileToUserSpaceOption_preview_path_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000B8E RID: 2958
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_preview_path_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPublishFileToUserSpaceOption_preview_path_filename_get(IntPtr jarg1);

		// Token: 0x06000B8F RID: 2959
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_version_set")]
		public static extern void RailPublishFileToUserSpaceOption_version_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000B90 RID: 2960
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_version_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailPublishFileToUserSpaceOption_version_get(IntPtr jarg1);

		// Token: 0x06000B91 RID: 2961
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_tags_set")]
		public static extern void RailPublishFileToUserSpaceOption_tags_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000B92 RID: 2962
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_tags_get")]
		public static extern IntPtr RailPublishFileToUserSpaceOption_tags_get(IntPtr jarg1);

		// Token: 0x06000B93 RID: 2963
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_level_set")]
		public static extern void RailPublishFileToUserSpaceOption_level_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000B94 RID: 2964
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_level_get")]
		public static extern int RailPublishFileToUserSpaceOption_level_get(IntPtr jarg1);

		// Token: 0x06000B95 RID: 2965
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_key_value_set")]
		public static extern void RailPublishFileToUserSpaceOption_key_value_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000B96 RID: 2966
		[DllImport("rail_api", EntryPoint = "CSharp_RailPublishFileToUserSpaceOption_key_value_get")]
		public static extern IntPtr RailPublishFileToUserSpaceOption_key_value_get(IntPtr jarg1);

		// Token: 0x06000B97 RID: 2967
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailPublishFileToUserSpaceOption__SWIG_1")]
		public static extern IntPtr new_RailPublishFileToUserSpaceOption__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B98 RID: 2968
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailPublishFileToUserSpaceOption")]
		public static extern void delete_RailPublishFileToUserSpaceOption(IntPtr jarg1);

		// Token: 0x06000B99 RID: 2969
		[DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileInfo_filename_set")]
		public static extern void RailStreamFileInfo_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000B9A RID: 2970
		[DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileInfo_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailStreamFileInfo_filename_get(IntPtr jarg1);

		// Token: 0x06000B9B RID: 2971
		[DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileInfo_file_size_set")]
		public static extern void RailStreamFileInfo_file_size_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000B9C RID: 2972
		[DllImport("rail_api", EntryPoint = "CSharp_RailStreamFileInfo_file_size_get")]
		public static extern ulong RailStreamFileInfo_file_size_get(IntPtr jarg1);

		// Token: 0x06000B9D RID: 2973
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailStreamFileInfo__SWIG_0")]
		public static extern IntPtr new_RailStreamFileInfo__SWIG_0();

		// Token: 0x06000B9E RID: 2974
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailStreamFileInfo__SWIG_1")]
		public static extern IntPtr new_RailStreamFileInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000B9F RID: 2975
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailStreamFileInfo")]
		public static extern void delete_RailStreamFileInfo(IntPtr jarg1);

		// Token: 0x06000BA0 RID: 2976
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncQueryQuotaResult__SWIG_0")]
		public static extern IntPtr new_AsyncQueryQuotaResult__SWIG_0();

		// Token: 0x06000BA1 RID: 2977
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_total_quota_set")]
		public static extern void AsyncQueryQuotaResult_total_quota_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000BA2 RID: 2978
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_total_quota_get")]
		public static extern ulong AsyncQueryQuotaResult_total_quota_get(IntPtr jarg1);

		// Token: 0x06000BA3 RID: 2979
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_available_quota_set")]
		public static extern void AsyncQueryQuotaResult_available_quota_set(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000BA4 RID: 2980
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_available_quota_get")]
		public static extern ulong AsyncQueryQuotaResult_available_quota_get(IntPtr jarg1);

		// Token: 0x06000BA5 RID: 2981
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncQueryQuotaResult__SWIG_1")]
		public static extern IntPtr new_AsyncQueryQuotaResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BA6 RID: 2982
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncQueryQuotaResult")]
		public static extern void delete_AsyncQueryQuotaResult(IntPtr jarg1);

		// Token: 0x06000BA7 RID: 2983
		[DllImport("rail_api", EntryPoint = "CSharp_new_ShareStorageToSpaceWorkResult__SWIG_0")]
		public static extern IntPtr new_ShareStorageToSpaceWorkResult__SWIG_0();

		// Token: 0x06000BA8 RID: 2984
		[DllImport("rail_api", EntryPoint = "CSharp_ShareStorageToSpaceWorkResult_space_work_id_set")]
		public static extern void ShareStorageToSpaceWorkResult_space_work_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000BA9 RID: 2985
		[DllImport("rail_api", EntryPoint = "CSharp_ShareStorageToSpaceWorkResult_space_work_id_get")]
		public static extern IntPtr ShareStorageToSpaceWorkResult_space_work_id_get(IntPtr jarg1);

		// Token: 0x06000BAA RID: 2986
		[DllImport("rail_api", EntryPoint = "CSharp_new_ShareStorageToSpaceWorkResult__SWIG_1")]
		public static extern IntPtr new_ShareStorageToSpaceWorkResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BAB RID: 2987
		[DllImport("rail_api", EntryPoint = "CSharp_delete_ShareStorageToSpaceWorkResult")]
		public static extern void delete_ShareStorageToSpaceWorkResult(IntPtr jarg1);

		// Token: 0x06000BAC RID: 2988
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncReadFileResult__SWIG_0")]
		public static extern IntPtr new_AsyncReadFileResult__SWIG_0();

		// Token: 0x06000BAD RID: 2989
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_filename_set")]
		public static extern void AsyncReadFileResult_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BAE RID: 2990
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncReadFileResult_filename_get(IntPtr jarg1);

		// Token: 0x06000BAF RID: 2991
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_data_set")]
		public static extern void AsyncReadFileResult_data_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BB0 RID: 2992
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_data_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncReadFileResult_data_get(IntPtr jarg1);

		// Token: 0x06000BB1 RID: 2993
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_offset_set")]
		public static extern void AsyncReadFileResult_offset_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000BB2 RID: 2994
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_offset_get")]
		public static extern int AsyncReadFileResult_offset_get(IntPtr jarg1);

		// Token: 0x06000BB3 RID: 2995
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_try_read_length_set")]
		public static extern void AsyncReadFileResult_try_read_length_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BB4 RID: 2996
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_try_read_length_get")]
		public static extern uint AsyncReadFileResult_try_read_length_get(IntPtr jarg1);

		// Token: 0x06000BB5 RID: 2997
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncReadFileResult__SWIG_1")]
		public static extern IntPtr new_AsyncReadFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BB6 RID: 2998
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncReadFileResult")]
		public static extern void delete_AsyncReadFileResult(IntPtr jarg1);

		// Token: 0x06000BB7 RID: 2999
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncWriteFileResult__SWIG_0")]
		public static extern IntPtr new_AsyncWriteFileResult__SWIG_0();

		// Token: 0x06000BB8 RID: 3000
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_filename_set")]
		public static extern void AsyncWriteFileResult_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BB9 RID: 3001
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncWriteFileResult_filename_get(IntPtr jarg1);

		// Token: 0x06000BBA RID: 3002
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_offset_set")]
		public static extern void AsyncWriteFileResult_offset_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000BBB RID: 3003
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_offset_get")]
		public static extern int AsyncWriteFileResult_offset_get(IntPtr jarg1);

		// Token: 0x06000BBC RID: 3004
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_try_write_length_set")]
		public static extern void AsyncWriteFileResult_try_write_length_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BBD RID: 3005
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_try_write_length_get")]
		public static extern uint AsyncWriteFileResult_try_write_length_get(IntPtr jarg1);

		// Token: 0x06000BBE RID: 3006
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_written_length_set")]
		public static extern void AsyncWriteFileResult_written_length_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BBF RID: 3007
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_written_length_get")]
		public static extern uint AsyncWriteFileResult_written_length_get(IntPtr jarg1);

		// Token: 0x06000BC0 RID: 3008
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncWriteFileResult__SWIG_1")]
		public static extern IntPtr new_AsyncWriteFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BC1 RID: 3009
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncWriteFileResult")]
		public static extern void delete_AsyncWriteFileResult(IntPtr jarg1);

		// Token: 0x06000BC2 RID: 3010
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncReadStreamFileResult__SWIG_0")]
		public static extern IntPtr new_AsyncReadStreamFileResult__SWIG_0();

		// Token: 0x06000BC3 RID: 3011
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_filename_set")]
		public static extern void AsyncReadStreamFileResult_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BC4 RID: 3012
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncReadStreamFileResult_filename_get(IntPtr jarg1);

		// Token: 0x06000BC5 RID: 3013
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_data_set")]
		public static extern void AsyncReadStreamFileResult_data_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BC6 RID: 3014
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_data_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncReadStreamFileResult_data_get(IntPtr jarg1);

		// Token: 0x06000BC7 RID: 3015
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_offset_set")]
		public static extern void AsyncReadStreamFileResult_offset_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000BC8 RID: 3016
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_offset_get")]
		public static extern int AsyncReadStreamFileResult_offset_get(IntPtr jarg1);

		// Token: 0x06000BC9 RID: 3017
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_try_read_length_set")]
		public static extern void AsyncReadStreamFileResult_try_read_length_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BCA RID: 3018
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_try_read_length_get")]
		public static extern uint AsyncReadStreamFileResult_try_read_length_get(IntPtr jarg1);

		// Token: 0x06000BCB RID: 3019
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncReadStreamFileResult__SWIG_1")]
		public static extern IntPtr new_AsyncReadStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BCC RID: 3020
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncReadStreamFileResult")]
		public static extern void delete_AsyncReadStreamFileResult(IntPtr jarg1);

		// Token: 0x06000BCD RID: 3021
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncWriteStreamFileResult__SWIG_0")]
		public static extern IntPtr new_AsyncWriteStreamFileResult__SWIG_0();

		// Token: 0x06000BCE RID: 3022
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_filename_set")]
		public static extern void AsyncWriteStreamFileResult_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BCF RID: 3023
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncWriteStreamFileResult_filename_get(IntPtr jarg1);

		// Token: 0x06000BD0 RID: 3024
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_offset_set")]
		public static extern void AsyncWriteStreamFileResult_offset_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000BD1 RID: 3025
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_offset_get")]
		public static extern int AsyncWriteStreamFileResult_offset_get(IntPtr jarg1);

		// Token: 0x06000BD2 RID: 3026
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_try_write_length_set")]
		public static extern void AsyncWriteStreamFileResult_try_write_length_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BD3 RID: 3027
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_try_write_length_get")]
		public static extern uint AsyncWriteStreamFileResult_try_write_length_get(IntPtr jarg1);

		// Token: 0x06000BD4 RID: 3028
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_written_length_set")]
		public static extern void AsyncWriteStreamFileResult_written_length_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BD5 RID: 3029
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_written_length_get")]
		public static extern uint AsyncWriteStreamFileResult_written_length_get(IntPtr jarg1);

		// Token: 0x06000BD6 RID: 3030
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncWriteStreamFileResult__SWIG_1")]
		public static extern IntPtr new_AsyncWriteStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BD7 RID: 3031
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncWriteStreamFileResult")]
		public static extern void delete_AsyncWriteStreamFileResult(IntPtr jarg1);

		// Token: 0x06000BD8 RID: 3032
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncListFileResult__SWIG_0")]
		public static extern IntPtr new_AsyncListFileResult__SWIG_0();

		// Token: 0x06000BD9 RID: 3033
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_file_list_set")]
		public static extern void AsyncListFileResult_file_list_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000BDA RID: 3034
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_file_list_get")]
		public static extern IntPtr AsyncListFileResult_file_list_get(IntPtr jarg1);

		// Token: 0x06000BDB RID: 3035
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_start_index_set")]
		public static extern void AsyncListFileResult_start_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BDC RID: 3036
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_start_index_get")]
		public static extern uint AsyncListFileResult_start_index_get(IntPtr jarg1);

		// Token: 0x06000BDD RID: 3037
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_try_list_file_num_set")]
		public static extern void AsyncListFileResult_try_list_file_num_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BDE RID: 3038
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_try_list_file_num_get")]
		public static extern uint AsyncListFileResult_try_list_file_num_get(IntPtr jarg1);

		// Token: 0x06000BDF RID: 3039
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_all_file_num_set")]
		public static extern void AsyncListFileResult_all_file_num_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BE0 RID: 3040
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_all_file_num_get")]
		public static extern uint AsyncListFileResult_all_file_num_get(IntPtr jarg1);

		// Token: 0x06000BE1 RID: 3041
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncListFileResult__SWIG_1")]
		public static extern IntPtr new_AsyncListFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BE2 RID: 3042
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncListFileResult")]
		public static extern void delete_AsyncListFileResult(IntPtr jarg1);

		// Token: 0x06000BE3 RID: 3043
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRenameStreamFileResult__SWIG_0")]
		public static extern IntPtr new_AsyncRenameStreamFileResult__SWIG_0();

		// Token: 0x06000BE4 RID: 3044
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_old_filename_set")]
		public static extern void AsyncRenameStreamFileResult_old_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BE5 RID: 3045
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_old_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncRenameStreamFileResult_old_filename_get(IntPtr jarg1);

		// Token: 0x06000BE6 RID: 3046
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_new_filename_set")]
		public static extern void AsyncRenameStreamFileResult_new_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BE7 RID: 3047
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_new_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncRenameStreamFileResult_new_filename_get(IntPtr jarg1);

		// Token: 0x06000BE8 RID: 3048
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncRenameStreamFileResult__SWIG_1")]
		public static extern IntPtr new_AsyncRenameStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BE9 RID: 3049
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncRenameStreamFileResult")]
		public static extern void delete_AsyncRenameStreamFileResult(IntPtr jarg1);

		// Token: 0x06000BEA RID: 3050
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncDeleteStreamFileResult__SWIG_0")]
		public static extern IntPtr new_AsyncDeleteStreamFileResult__SWIG_0();

		// Token: 0x06000BEB RID: 3051
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncDeleteStreamFileResult_filename_set")]
		public static extern void AsyncDeleteStreamFileResult_filename_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000BEC RID: 3052
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncDeleteStreamFileResult_filename_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string AsyncDeleteStreamFileResult_filename_get(IntPtr jarg1);

		// Token: 0x06000BED RID: 3053
		[DllImport("rail_api", EntryPoint = "CSharp_new_AsyncDeleteStreamFileResult__SWIG_1")]
		public static extern IntPtr new_AsyncDeleteStreamFileResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BEE RID: 3054
		[DllImport("rail_api", EntryPoint = "CSharp_delete_AsyncDeleteStreamFileResult")]
		public static extern void delete_AsyncDeleteStreamFileResult(IntPtr jarg1);

		// Token: 0x06000BEF RID: 3055
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceCaptureOption__SWIG_0")]
		public static extern IntPtr new_RailVoiceCaptureOption__SWIG_0();

		// Token: 0x06000BF0 RID: 3056
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureOption_voice_data_format_set")]
		public static extern void RailVoiceCaptureOption_voice_data_format_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000BF1 RID: 3057
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureOption_voice_data_format_get")]
		public static extern int RailVoiceCaptureOption_voice_data_format_get(IntPtr jarg1);

		// Token: 0x06000BF2 RID: 3058
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceCaptureOption__SWIG_1")]
		public static extern IntPtr new_RailVoiceCaptureOption__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BF3 RID: 3059
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailVoiceCaptureOption")]
		public static extern void delete_RailVoiceCaptureOption(IntPtr jarg1);

		// Token: 0x06000BF4 RID: 3060
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceCaptureSpecification__SWIG_0")]
		public static extern IntPtr new_RailVoiceCaptureSpecification__SWIG_0();

		// Token: 0x06000BF5 RID: 3061
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureSpecification_capture_format_set")]
		public static extern void RailVoiceCaptureSpecification_capture_format_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000BF6 RID: 3062
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureSpecification_capture_format_get")]
		public static extern int RailVoiceCaptureSpecification_capture_format_get(IntPtr jarg1);

		// Token: 0x06000BF7 RID: 3063
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureSpecification_bits_per_sample_set")]
		public static extern void RailVoiceCaptureSpecification_bits_per_sample_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BF8 RID: 3064
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureSpecification_bits_per_sample_get")]
		public static extern uint RailVoiceCaptureSpecification_bits_per_sample_get(IntPtr jarg1);

		// Token: 0x06000BF9 RID: 3065
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureSpecification_samples_per_second_set")]
		public static extern void RailVoiceCaptureSpecification_samples_per_second_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000BFA RID: 3066
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureSpecification_samples_per_second_get")]
		public static extern uint RailVoiceCaptureSpecification_samples_per_second_get(IntPtr jarg1);

		// Token: 0x06000BFB RID: 3067
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureSpecification_channels_set")]
		public static extern void RailVoiceCaptureSpecification_channels_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000BFC RID: 3068
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceCaptureSpecification_channels_get")]
		public static extern int RailVoiceCaptureSpecification_channels_get(IntPtr jarg1);

		// Token: 0x06000BFD RID: 3069
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceCaptureSpecification__SWIG_1")]
		public static extern IntPtr new_RailVoiceCaptureSpecification__SWIG_1(IntPtr jarg1);

		// Token: 0x06000BFE RID: 3070
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailVoiceCaptureSpecification")]
		public static extern void delete_RailVoiceCaptureSpecification(IntPtr jarg1);

		// Token: 0x06000BFF RID: 3071
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateVoiceChannelOption__SWIG_0")]
		public static extern IntPtr new_CreateVoiceChannelOption__SWIG_0();

		// Token: 0x06000C00 RID: 3072
		[DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelOption_join_channel_after_created_set")]
		public static extern void CreateVoiceChannelOption_join_channel_after_created_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000C01 RID: 3073
		[DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelOption_join_channel_after_created_get")]
		public static extern bool CreateVoiceChannelOption_join_channel_after_created_get(IntPtr jarg1);

		// Token: 0x06000C02 RID: 3074
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateVoiceChannelOption__SWIG_1")]
		public static extern IntPtr new_CreateVoiceChannelOption__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C03 RID: 3075
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateVoiceChannelOption")]
		public static extern void delete_CreateVoiceChannelOption(IntPtr jarg1);

		// Token: 0x06000C04 RID: 3076
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceChannelUserSpeakingState__SWIG_0")]
		public static extern IntPtr new_RailVoiceChannelUserSpeakingState__SWIG_0();

		// Token: 0x06000C05 RID: 3077
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelUserSpeakingState_user_id_set")]
		public static extern void RailVoiceChannelUserSpeakingState_user_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C06 RID: 3078
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelUserSpeakingState_user_id_get")]
		public static extern IntPtr RailVoiceChannelUserSpeakingState_user_id_get(IntPtr jarg1);

		// Token: 0x06000C07 RID: 3079
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelUserSpeakingState_speaking_limit_set")]
		public static extern void RailVoiceChannelUserSpeakingState_speaking_limit_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000C08 RID: 3080
		[DllImport("rail_api", EntryPoint = "CSharp_RailVoiceChannelUserSpeakingState_speaking_limit_get")]
		public static extern int RailVoiceChannelUserSpeakingState_speaking_limit_get(IntPtr jarg1);

		// Token: 0x06000C09 RID: 3081
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailVoiceChannelUserSpeakingState__SWIG_1")]
		public static extern IntPtr new_RailVoiceChannelUserSpeakingState__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C0A RID: 3082
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailVoiceChannelUserSpeakingState")]
		public static extern void delete_RailVoiceChannelUserSpeakingState(IntPtr jarg1);

		// Token: 0x06000C0B RID: 3083
		[DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelResult_voice_channel_id_set")]
		public static extern void CreateVoiceChannelResult_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C0C RID: 3084
		[DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelResult_voice_channel_id_get")]
		public static extern IntPtr CreateVoiceChannelResult_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C0D RID: 3085
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateVoiceChannelResult__SWIG_0")]
		public static extern IntPtr new_CreateVoiceChannelResult__SWIG_0();

		// Token: 0x06000C0E RID: 3086
		[DllImport("rail_api", EntryPoint = "CSharp_new_CreateVoiceChannelResult__SWIG_1")]
		public static extern IntPtr new_CreateVoiceChannelResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C0F RID: 3087
		[DllImport("rail_api", EntryPoint = "CSharp_delete_CreateVoiceChannelResult")]
		public static extern void delete_CreateVoiceChannelResult(IntPtr jarg1);

		// Token: 0x06000C10 RID: 3088
		[DllImport("rail_api", EntryPoint = "CSharp_JoinVoiceChannelResult_voice_channel_id_set")]
		public static extern void JoinVoiceChannelResult_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C11 RID: 3089
		[DllImport("rail_api", EntryPoint = "CSharp_JoinVoiceChannelResult_voice_channel_id_get")]
		public static extern IntPtr JoinVoiceChannelResult_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C12 RID: 3090
		[DllImport("rail_api", EntryPoint = "CSharp_JoinVoiceChannelResult_already_joined_channel_id_set")]
		public static extern void JoinVoiceChannelResult_already_joined_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C13 RID: 3091
		[DllImport("rail_api", EntryPoint = "CSharp_JoinVoiceChannelResult_already_joined_channel_id_get")]
		public static extern IntPtr JoinVoiceChannelResult_already_joined_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C14 RID: 3092
		[DllImport("rail_api", EntryPoint = "CSharp_new_JoinVoiceChannelResult__SWIG_0")]
		public static extern IntPtr new_JoinVoiceChannelResult__SWIG_0();

		// Token: 0x06000C15 RID: 3093
		[DllImport("rail_api", EntryPoint = "CSharp_new_JoinVoiceChannelResult__SWIG_1")]
		public static extern IntPtr new_JoinVoiceChannelResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C16 RID: 3094
		[DllImport("rail_api", EntryPoint = "CSharp_delete_JoinVoiceChannelResult")]
		public static extern void delete_JoinVoiceChannelResult(IntPtr jarg1);

		// Token: 0x06000C17 RID: 3095
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaveVoiceChannelResult__SWIG_0")]
		public static extern IntPtr new_LeaveVoiceChannelResult__SWIG_0();

		// Token: 0x06000C18 RID: 3096
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveVoiceChannelResult_voice_channel_id_set")]
		public static extern void LeaveVoiceChannelResult_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C19 RID: 3097
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveVoiceChannelResult_voice_channel_id_get")]
		public static extern IntPtr LeaveVoiceChannelResult_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C1A RID: 3098
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveVoiceChannelResult_reason_set")]
		public static extern void LeaveVoiceChannelResult_reason_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000C1B RID: 3099
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveVoiceChannelResult_reason_get")]
		public static extern int LeaveVoiceChannelResult_reason_get(IntPtr jarg1);

		// Token: 0x06000C1C RID: 3100
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaveVoiceChannelResult__SWIG_1")]
		public static extern IntPtr new_LeaveVoiceChannelResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C1D RID: 3101
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaveVoiceChannelResult")]
		public static extern void delete_LeaveVoiceChannelResult(IntPtr jarg1);

		// Token: 0x06000C1E RID: 3102
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelAddUsersResult_voice_channel_id_set")]
		public static extern void VoiceChannelAddUsersResult_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C1F RID: 3103
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelAddUsersResult_voice_channel_id_get")]
		public static extern IntPtr VoiceChannelAddUsersResult_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C20 RID: 3104
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelAddUsersResult_success_ids_set")]
		public static extern void VoiceChannelAddUsersResult_success_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C21 RID: 3105
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelAddUsersResult_success_ids_get")]
		public static extern IntPtr VoiceChannelAddUsersResult_success_ids_get(IntPtr jarg1);

		// Token: 0x06000C22 RID: 3106
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelAddUsersResult_failed_ids_set")]
		public static extern void VoiceChannelAddUsersResult_failed_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C23 RID: 3107
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelAddUsersResult_failed_ids_get")]
		public static extern IntPtr VoiceChannelAddUsersResult_failed_ids_get(IntPtr jarg1);

		// Token: 0x06000C24 RID: 3108
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelAddUsersResult__SWIG_0")]
		public static extern IntPtr new_VoiceChannelAddUsersResult__SWIG_0();

		// Token: 0x06000C25 RID: 3109
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelAddUsersResult__SWIG_1")]
		public static extern IntPtr new_VoiceChannelAddUsersResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C26 RID: 3110
		[DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceChannelAddUsersResult")]
		public static extern void delete_VoiceChannelAddUsersResult(IntPtr jarg1);

		// Token: 0x06000C27 RID: 3111
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelRemoveUsersResult_voice_channel_id_set")]
		public static extern void VoiceChannelRemoveUsersResult_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C28 RID: 3112
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelRemoveUsersResult_voice_channel_id_get")]
		public static extern IntPtr VoiceChannelRemoveUsersResult_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C29 RID: 3113
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelRemoveUsersResult_success_ids_set")]
		public static extern void VoiceChannelRemoveUsersResult_success_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C2A RID: 3114
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelRemoveUsersResult_success_ids_get")]
		public static extern IntPtr VoiceChannelRemoveUsersResult_success_ids_get(IntPtr jarg1);

		// Token: 0x06000C2B RID: 3115
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelRemoveUsersResult_failed_ids_set")]
		public static extern void VoiceChannelRemoveUsersResult_failed_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C2C RID: 3116
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelRemoveUsersResult_failed_ids_get")]
		public static extern IntPtr VoiceChannelRemoveUsersResult_failed_ids_get(IntPtr jarg1);

		// Token: 0x06000C2D RID: 3117
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelRemoveUsersResult__SWIG_0")]
		public static extern IntPtr new_VoiceChannelRemoveUsersResult__SWIG_0();

		// Token: 0x06000C2E RID: 3118
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelRemoveUsersResult__SWIG_1")]
		public static extern IntPtr new_VoiceChannelRemoveUsersResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C2F RID: 3119
		[DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceChannelRemoveUsersResult")]
		public static extern void delete_VoiceChannelRemoveUsersResult(IntPtr jarg1);

		// Token: 0x06000C30 RID: 3120
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelInviteEvent_inviter_name_set")]
		public static extern void VoiceChannelInviteEvent_inviter_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C31 RID: 3121
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelInviteEvent_inviter_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string VoiceChannelInviteEvent_inviter_name_get(IntPtr jarg1);

		// Token: 0x06000C32 RID: 3122
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelInviteEvent_channel_name_set")]
		public static extern void VoiceChannelInviteEvent_channel_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C33 RID: 3123
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelInviteEvent_channel_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string VoiceChannelInviteEvent_channel_name_get(IntPtr jarg1);

		// Token: 0x06000C34 RID: 3124
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelInviteEvent_voice_channel_id_set")]
		public static extern void VoiceChannelInviteEvent_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C35 RID: 3125
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelInviteEvent_voice_channel_id_get")]
		public static extern IntPtr VoiceChannelInviteEvent_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C36 RID: 3126
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelInviteEvent__SWIG_0")]
		public static extern IntPtr new_VoiceChannelInviteEvent__SWIG_0();

		// Token: 0x06000C37 RID: 3127
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelInviteEvent__SWIG_1")]
		public static extern IntPtr new_VoiceChannelInviteEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C38 RID: 3128
		[DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceChannelInviteEvent")]
		public static extern void delete_VoiceChannelInviteEvent(IntPtr jarg1);

		// Token: 0x06000C39 RID: 3129
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelMemeberChangedEvent_voice_channel_id_set")]
		public static extern void VoiceChannelMemeberChangedEvent_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C3A RID: 3130
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelMemeberChangedEvent_voice_channel_id_get")]
		public static extern IntPtr VoiceChannelMemeberChangedEvent_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C3B RID: 3131
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelMemeberChangedEvent_member_ids_set")]
		public static extern void VoiceChannelMemeberChangedEvent_member_ids_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C3C RID: 3132
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelMemeberChangedEvent_member_ids_get")]
		public static extern IntPtr VoiceChannelMemeberChangedEvent_member_ids_get(IntPtr jarg1);

		// Token: 0x06000C3D RID: 3133
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelMemeberChangedEvent__SWIG_0")]
		public static extern IntPtr new_VoiceChannelMemeberChangedEvent__SWIG_0();

		// Token: 0x06000C3E RID: 3134
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelMemeberChangedEvent__SWIG_1")]
		public static extern IntPtr new_VoiceChannelMemeberChangedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C3F RID: 3135
		[DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceChannelMemeberChangedEvent")]
		public static extern void delete_VoiceChannelMemeberChangedEvent(IntPtr jarg1);

		// Token: 0x06000C40 RID: 3136
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelPushToTalkKeyChangedEvent__SWIG_0")]
		public static extern IntPtr new_VoiceChannelPushToTalkKeyChangedEvent__SWIG_0();

		// Token: 0x06000C41 RID: 3137
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelPushToTalkKeyChangedEvent_push_to_talk_hot_key_set")]
		public static extern void VoiceChannelPushToTalkKeyChangedEvent_push_to_talk_hot_key_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000C42 RID: 3138
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelPushToTalkKeyChangedEvent_push_to_talk_hot_key_get")]
		public static extern uint VoiceChannelPushToTalkKeyChangedEvent_push_to_talk_hot_key_get(IntPtr jarg1);

		// Token: 0x06000C43 RID: 3139
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelPushToTalkKeyChangedEvent__SWIG_1")]
		public static extern IntPtr new_VoiceChannelPushToTalkKeyChangedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C44 RID: 3140
		[DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceChannelPushToTalkKeyChangedEvent")]
		public static extern void delete_VoiceChannelPushToTalkKeyChangedEvent(IntPtr jarg1);

		// Token: 0x06000C45 RID: 3141
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelUsersSpeakingStateChangedEvent_voice_channel_id_set")]
		public static extern void VoiceChannelUsersSpeakingStateChangedEvent_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C46 RID: 3142
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelUsersSpeakingStateChangedEvent_voice_channel_id_get")]
		public static extern IntPtr VoiceChannelUsersSpeakingStateChangedEvent_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C47 RID: 3143
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelUsersSpeakingStateChangedEvent_users_speaking_state_set")]
		public static extern void VoiceChannelUsersSpeakingStateChangedEvent_users_speaking_state_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C48 RID: 3144
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelUsersSpeakingStateChangedEvent_users_speaking_state_get")]
		public static extern IntPtr VoiceChannelUsersSpeakingStateChangedEvent_users_speaking_state_get(IntPtr jarg1);

		// Token: 0x06000C49 RID: 3145
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelUsersSpeakingStateChangedEvent__SWIG_0")]
		public static extern IntPtr new_VoiceChannelUsersSpeakingStateChangedEvent__SWIG_0();

		// Token: 0x06000C4A RID: 3146
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelUsersSpeakingStateChangedEvent__SWIG_1")]
		public static extern IntPtr new_VoiceChannelUsersSpeakingStateChangedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C4B RID: 3147
		[DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceChannelUsersSpeakingStateChangedEvent")]
		public static extern void delete_VoiceChannelUsersSpeakingStateChangedEvent(IntPtr jarg1);

		// Token: 0x06000C4C RID: 3148
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelSpeakingUsersChangedEvent_voice_channel_id_set")]
		public static extern void VoiceChannelSpeakingUsersChangedEvent_voice_channel_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C4D RID: 3149
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelSpeakingUsersChangedEvent_voice_channel_id_get")]
		public static extern IntPtr VoiceChannelSpeakingUsersChangedEvent_voice_channel_id_get(IntPtr jarg1);

		// Token: 0x06000C4E RID: 3150
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelSpeakingUsersChangedEvent_speaking_users_set")]
		public static extern void VoiceChannelSpeakingUsersChangedEvent_speaking_users_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C4F RID: 3151
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelSpeakingUsersChangedEvent_speaking_users_get")]
		public static extern IntPtr VoiceChannelSpeakingUsersChangedEvent_speaking_users_get(IntPtr jarg1);

		// Token: 0x06000C50 RID: 3152
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelSpeakingUsersChangedEvent_not_speaking_users_set")]
		public static extern void VoiceChannelSpeakingUsersChangedEvent_not_speaking_users_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000C51 RID: 3153
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelSpeakingUsersChangedEvent_not_speaking_users_get")]
		public static extern IntPtr VoiceChannelSpeakingUsersChangedEvent_not_speaking_users_get(IntPtr jarg1);

		// Token: 0x06000C52 RID: 3154
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelSpeakingUsersChangedEvent__SWIG_0")]
		public static extern IntPtr new_VoiceChannelSpeakingUsersChangedEvent__SWIG_0();

		// Token: 0x06000C53 RID: 3155
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceChannelSpeakingUsersChangedEvent__SWIG_1")]
		public static extern IntPtr new_VoiceChannelSpeakingUsersChangedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C54 RID: 3156
		[DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceChannelSpeakingUsersChangedEvent")]
		public static extern void delete_VoiceChannelSpeakingUsersChangedEvent(IntPtr jarg1);

		// Token: 0x06000C55 RID: 3157
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceDataCapturedEvent__SWIG_0")]
		public static extern IntPtr new_VoiceDataCapturedEvent__SWIG_0();

		// Token: 0x06000C56 RID: 3158
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceDataCapturedEvent_is_last_package_set")]
		public static extern void VoiceDataCapturedEvent_is_last_package_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000C57 RID: 3159
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceDataCapturedEvent_is_last_package_get")]
		public static extern bool VoiceDataCapturedEvent_is_last_package_get(IntPtr jarg1);

		// Token: 0x06000C58 RID: 3160
		[DllImport("rail_api", EntryPoint = "CSharp_new_VoiceDataCapturedEvent__SWIG_1")]
		public static extern IntPtr new_VoiceDataCapturedEvent__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C59 RID: 3161
		[DllImport("rail_api", EntryPoint = "CSharp_delete_VoiceDataCapturedEvent")]
		public static extern void delete_VoiceDataCapturedEvent(IntPtr jarg1);

		// Token: 0x06000C5A RID: 3162
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailTextInputWindowOption__SWIG_0")]
		public static extern IntPtr new_RailTextInputWindowOption__SWIG_0();

		// Token: 0x06000C5B RID: 3163
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_show_password_input_set")]
		public static extern void RailTextInputWindowOption_show_password_input_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000C5C RID: 3164
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_show_password_input_get")]
		public static extern bool RailTextInputWindowOption_show_password_input_get(IntPtr jarg1);

		// Token: 0x06000C5D RID: 3165
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_enable_multi_line_edit_set")]
		public static extern void RailTextInputWindowOption_enable_multi_line_edit_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000C5E RID: 3166
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_enable_multi_line_edit_get")]
		public static extern bool RailTextInputWindowOption_enable_multi_line_edit_get(IntPtr jarg1);

		// Token: 0x06000C5F RID: 3167
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_auto_cancel_set")]
		public static extern void RailTextInputWindowOption_auto_cancel_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000C60 RID: 3168
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_auto_cancel_get")]
		public static extern bool RailTextInputWindowOption_auto_cancel_get(IntPtr jarg1);

		// Token: 0x06000C61 RID: 3169
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_caption_text_set")]
		public static extern void RailTextInputWindowOption_caption_text_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C62 RID: 3170
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_caption_text_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailTextInputWindowOption_caption_text_get(IntPtr jarg1);

		// Token: 0x06000C63 RID: 3171
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_description_set")]
		public static extern void RailTextInputWindowOption_description_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C64 RID: 3172
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_description_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailTextInputWindowOption_description_get(IntPtr jarg1);

		// Token: 0x06000C65 RID: 3173
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_content_placeholder_set")]
		public static extern void RailTextInputWindowOption_content_placeholder_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C66 RID: 3174
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_content_placeholder_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailTextInputWindowOption_content_placeholder_get(IntPtr jarg1);

		// Token: 0x06000C67 RID: 3175
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_is_min_window_set")]
		public static extern void RailTextInputWindowOption_is_min_window_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000C68 RID: 3176
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_is_min_window_get")]
		public static extern bool RailTextInputWindowOption_is_min_window_get(IntPtr jarg1);

		// Token: 0x06000C69 RID: 3177
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_position_left_set")]
		public static extern void RailTextInputWindowOption_position_left_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000C6A RID: 3178
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_position_left_get")]
		public static extern uint RailTextInputWindowOption_position_left_get(IntPtr jarg1);

		// Token: 0x06000C6B RID: 3179
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_position_top_set")]
		public static extern void RailTextInputWindowOption_position_top_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000C6C RID: 3180
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputWindowOption_position_top_get")]
		public static extern uint RailTextInputWindowOption_position_top_get(IntPtr jarg1);

		// Token: 0x06000C6D RID: 3181
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailTextInputWindowOption__SWIG_1")]
		public static extern IntPtr new_RailTextInputWindowOption__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C6E RID: 3182
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailTextInputWindowOption")]
		public static extern void delete_RailTextInputWindowOption(IntPtr jarg1);

		// Token: 0x06000C6F RID: 3183
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailTextInputResult__SWIG_0")]
		public static extern IntPtr new_RailTextInputResult__SWIG_0();

		// Token: 0x06000C70 RID: 3184
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputResult_content_set")]
		public static extern void RailTextInputResult_content_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C71 RID: 3185
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputResult_content_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailTextInputResult_content_get(IntPtr jarg1);

		// Token: 0x06000C72 RID: 3186
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailTextInputResult__SWIG_1")]
		public static extern IntPtr new_RailTextInputResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C73 RID: 3187
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailTextInputResult")]
		public static extern void delete_RailTextInputResult(IntPtr jarg1);

		// Token: 0x06000C74 RID: 3188
		[DllImport("rail_api", EntryPoint = "CSharp_kRailMaxGameDefinePlayingStateValue_get")]
		public static extern uint kRailMaxGameDefinePlayingStateValue_get();

		// Token: 0x06000C75 RID: 3189
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailBranchInfo__SWIG_0")]
		public static extern IntPtr new_RailBranchInfo__SWIG_0();

		// Token: 0x06000C76 RID: 3190
		[DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_name_set")]
		public static extern void RailBranchInfo_branch_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C77 RID: 3191
		[DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailBranchInfo_branch_name_get(IntPtr jarg1);

		// Token: 0x06000C78 RID: 3192
		[DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_type_set")]
		public static extern void RailBranchInfo_branch_type_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C79 RID: 3193
		[DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_type_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailBranchInfo_branch_type_get(IntPtr jarg1);

		// Token: 0x06000C7A RID: 3194
		[DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_id_set")]
		public static extern void RailBranchInfo_branch_id_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C7B RID: 3195
		[DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_branch_id_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailBranchInfo_branch_id_get(IntPtr jarg1);

		// Token: 0x06000C7C RID: 3196
		[DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_build_number_set")]
		public static extern void RailBranchInfo_build_number_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C7D RID: 3197
		[DllImport("rail_api", EntryPoint = "CSharp_RailBranchInfo_build_number_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailBranchInfo_build_number_get(IntPtr jarg1);

		// Token: 0x06000C7E RID: 3198
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailBranchInfo__SWIG_1")]
		public static extern IntPtr new_RailBranchInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C7F RID: 3199
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailBranchInfo")]
		public static extern void delete_RailBranchInfo(IntPtr jarg1);

		// Token: 0x06000C80 RID: 3200
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailGameDefineGamePlayingState__SWIG_0")]
		public static extern IntPtr new_RailGameDefineGamePlayingState__SWIG_0();

		// Token: 0x06000C81 RID: 3201
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameDefineGamePlayingState_game_define_game_playing_state_set")]
		public static extern void RailGameDefineGamePlayingState_game_define_game_playing_state_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000C82 RID: 3202
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameDefineGamePlayingState_game_define_game_playing_state_get")]
		public static extern uint RailGameDefineGamePlayingState_game_define_game_playing_state_get(IntPtr jarg1);

		// Token: 0x06000C83 RID: 3203
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameDefineGamePlayingState_state_name_zh_cn_set")]
		public static extern void RailGameDefineGamePlayingState_state_name_zh_cn_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C84 RID: 3204
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameDefineGamePlayingState_state_name_zh_cn_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailGameDefineGamePlayingState_state_name_zh_cn_get(IntPtr jarg1);

		// Token: 0x06000C85 RID: 3205
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameDefineGamePlayingState_state_name_en_us_set")]
		public static extern void RailGameDefineGamePlayingState_state_name_en_us_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C86 RID: 3206
		[DllImport("rail_api", EntryPoint = "CSharp_RailGameDefineGamePlayingState_state_name_en_us_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailGameDefineGamePlayingState_state_name_en_us_get(IntPtr jarg1);

		// Token: 0x06000C87 RID: 3207
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailGameDefineGamePlayingState__SWIG_1")]
		public static extern IntPtr new_RailGameDefineGamePlayingState__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C88 RID: 3208
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailGameDefineGamePlayingState")]
		public static extern void delete_RailGameDefineGamePlayingState(IntPtr jarg1);

		// Token: 0x06000C89 RID: 3209
		[DllImport("rail_api", EntryPoint = "CSharp_new_QuerySubscribeWishPlayStateResult__SWIG_0")]
		public static extern IntPtr new_QuerySubscribeWishPlayStateResult__SWIG_0();

		// Token: 0x06000C8A RID: 3210
		[DllImport("rail_api", EntryPoint = "CSharp_QuerySubscribeWishPlayStateResult_is_subscribed_set")]
		public static extern void QuerySubscribeWishPlayStateResult_is_subscribed_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000C8B RID: 3211
		[DllImport("rail_api", EntryPoint = "CSharp_QuerySubscribeWishPlayStateResult_is_subscribed_get")]
		public static extern bool QuerySubscribeWishPlayStateResult_is_subscribed_get(IntPtr jarg1);

		// Token: 0x06000C8C RID: 3212
		[DllImport("rail_api", EntryPoint = "CSharp_new_QuerySubscribeWishPlayStateResult__SWIG_1")]
		public static extern IntPtr new_QuerySubscribeWishPlayStateResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C8D RID: 3213
		[DllImport("rail_api", EntryPoint = "CSharp_delete_QuerySubscribeWishPlayStateResult")]
		public static extern void delete_QuerySubscribeWishPlayStateResult(IntPtr jarg1);

		// Token: 0x06000C8E RID: 3214
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailWindowPosition__SWIG_0")]
		public static extern IntPtr new_RailWindowPosition__SWIG_0();

		// Token: 0x06000C8F RID: 3215
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowPosition_position_left_set")]
		public static extern void RailWindowPosition_position_left_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000C90 RID: 3216
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowPosition_position_left_get")]
		public static extern uint RailWindowPosition_position_left_get(IntPtr jarg1);

		// Token: 0x06000C91 RID: 3217
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowPosition_position_top_set")]
		public static extern void RailWindowPosition_position_top_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000C92 RID: 3218
		[DllImport("rail_api", EntryPoint = "CSharp_RailWindowPosition_position_top_get")]
		public static extern uint RailWindowPosition_position_top_get(IntPtr jarg1);

		// Token: 0x06000C93 RID: 3219
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailWindowPosition__SWIG_1")]
		public static extern IntPtr new_RailWindowPosition__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C94 RID: 3220
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailWindowPosition")]
		public static extern void delete_RailWindowPosition(IntPtr jarg1);

		// Token: 0x06000C95 RID: 3221
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailIMEHelperTextInputSelectedResult__SWIG_0")]
		public static extern IntPtr new_RailIMEHelperTextInputSelectedResult__SWIG_0();

		// Token: 0x06000C96 RID: 3222
		[DllImport("rail_api", EntryPoint = "CSharp_RailIMEHelperTextInputSelectedResult_content_set")]
		public static extern void RailIMEHelperTextInputSelectedResult_content_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C97 RID: 3223
		[DllImport("rail_api", EntryPoint = "CSharp_RailIMEHelperTextInputSelectedResult_content_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailIMEHelperTextInputSelectedResult_content_get(IntPtr jarg1);

		// Token: 0x06000C98 RID: 3224
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailIMEHelperTextInputSelectedResult__SWIG_1")]
		public static extern IntPtr new_RailIMEHelperTextInputSelectedResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C99 RID: 3225
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailIMEHelperTextInputSelectedResult")]
		public static extern void delete_RailIMEHelperTextInputSelectedResult(IntPtr jarg1);

		// Token: 0x06000C9A RID: 3226
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailHttpSessionResponse__SWIG_0")]
		public static extern IntPtr new_RailHttpSessionResponse__SWIG_0();

		// Token: 0x06000C9B RID: 3227
		[DllImport("rail_api", EntryPoint = "CSharp_RailHttpSessionResponse_http_response_data_set")]
		public static extern void RailHttpSessionResponse_http_response_data_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000C9C RID: 3228
		[DllImport("rail_api", EntryPoint = "CSharp_RailHttpSessionResponse_http_response_data_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string RailHttpSessionResponse_http_response_data_get(IntPtr jarg1);

		// Token: 0x06000C9D RID: 3229
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailHttpSessionResponse__SWIG_1")]
		public static extern IntPtr new_RailHttpSessionResponse__SWIG_1(IntPtr jarg1);

		// Token: 0x06000C9E RID: 3230
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailHttpSessionResponse")]
		public static extern void delete_RailHttpSessionResponse(IntPtr jarg1);

		// Token: 0x06000C9F RID: 3231
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSmallObjectState__SWIG_0")]
		public static extern IntPtr new_RailSmallObjectState__SWIG_0();

		// Token: 0x06000CA0 RID: 3232
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectState_update_state_set")]
		public static extern void RailSmallObjectState_update_state_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000CA1 RID: 3233
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectState_update_state_get")]
		public static extern int RailSmallObjectState_update_state_get(IntPtr jarg1);

		// Token: 0x06000CA2 RID: 3234
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectState_index_set")]
		public static extern void RailSmallObjectState_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000CA3 RID: 3235
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectState_index_get")]
		public static extern uint RailSmallObjectState_index_get(IntPtr jarg1);

		// Token: 0x06000CA4 RID: 3236
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSmallObjectState__SWIG_1")]
		public static extern IntPtr new_RailSmallObjectState__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CA5 RID: 3237
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSmallObjectState")]
		public static extern void delete_RailSmallObjectState(IntPtr jarg1);

		// Token: 0x06000CA6 RID: 3238
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSmallObjectDownloadInfo__SWIG_0")]
		public static extern IntPtr new_RailSmallObjectDownloadInfo__SWIG_0();

		// Token: 0x06000CA7 RID: 3239
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectDownloadInfo_index_set")]
		public static extern void RailSmallObjectDownloadInfo_index_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000CA8 RID: 3240
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectDownloadInfo_index_get")]
		public static extern uint RailSmallObjectDownloadInfo_index_get(IntPtr jarg1);

		// Token: 0x06000CA9 RID: 3241
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectDownloadInfo_result_set")]
		public static extern void RailSmallObjectDownloadInfo_result_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000CAA RID: 3242
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectDownloadInfo_result_get")]
		public static extern int RailSmallObjectDownloadInfo_result_get(IntPtr jarg1);

		// Token: 0x06000CAB RID: 3243
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSmallObjectDownloadInfo__SWIG_1")]
		public static extern IntPtr new_RailSmallObjectDownloadInfo__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CAC RID: 3244
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSmallObjectDownloadInfo")]
		public static extern void delete_RailSmallObjectDownloadInfo(IntPtr jarg1);

		// Token: 0x06000CAD RID: 3245
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectStateQueryResult_objects_state_set")]
		public static extern void RailSmallObjectStateQueryResult_objects_state_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000CAE RID: 3246
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectStateQueryResult_objects_state_get")]
		public static extern IntPtr RailSmallObjectStateQueryResult_objects_state_get(IntPtr jarg1);

		// Token: 0x06000CAF RID: 3247
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSmallObjectStateQueryResult__SWIG_0")]
		public static extern IntPtr new_RailSmallObjectStateQueryResult__SWIG_0();

		// Token: 0x06000CB0 RID: 3248
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSmallObjectStateQueryResult__SWIG_1")]
		public static extern IntPtr new_RailSmallObjectStateQueryResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CB1 RID: 3249
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSmallObjectStateQueryResult")]
		public static extern void delete_RailSmallObjectStateQueryResult(IntPtr jarg1);

		// Token: 0x06000CB2 RID: 3250
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectDownloadResult_download_infos_set")]
		public static extern void RailSmallObjectDownloadResult_download_infos_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000CB3 RID: 3251
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectDownloadResult_download_infos_get")]
		public static extern IntPtr RailSmallObjectDownloadResult_download_infos_get(IntPtr jarg1);

		// Token: 0x06000CB4 RID: 3252
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSmallObjectDownloadResult__SWIG_0")]
		public static extern IntPtr new_RailSmallObjectDownloadResult__SWIG_0();

		// Token: 0x06000CB5 RID: 3253
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSmallObjectDownloadResult__SWIG_1")]
		public static extern IntPtr new_RailSmallObjectDownloadResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CB6 RID: 3254
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSmallObjectDownloadResult")]
		public static extern void delete_RailSmallObjectDownloadResult(IntPtr jarg1);

		// Token: 0x06000CB7 RID: 3255
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSwitchPlayerSelectedZoneResult__SWIG_0")]
		public static extern IntPtr new_RailSwitchPlayerSelectedZoneResult__SWIG_0();

		// Token: 0x06000CB8 RID: 3256
		[DllImport("rail_api", EntryPoint = "CSharp_new_RailSwitchPlayerSelectedZoneResult__SWIG_1")]
		public static extern IntPtr new_RailSwitchPlayerSelectedZoneResult__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CB9 RID: 3257
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RailSwitchPlayerSelectedZoneResult")]
		public static extern void delete_RailSwitchPlayerSelectedZoneResult(IntPtr jarg1);

		// Token: 0x06000CBA RID: 3258
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAchievementHelper_CreatePlayerAchievement")]
		public static extern IntPtr IRailAchievementHelper_CreatePlayerAchievement(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000CBB RID: 3259
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAchievementHelper_GetGlobalAchievement")]
		public static extern IntPtr IRailAchievementHelper_GetGlobalAchievement(IntPtr jarg1);

		// Token: 0x06000CBC RID: 3260
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailAchievementHelper")]
		public static extern void delete_IRailAchievementHelper(IntPtr jarg1);

		// Token: 0x06000CBD RID: 3261
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_GetRailID")]
		public static extern IntPtr IRailPlayerAchievement_GetRailID(IntPtr jarg1);

		// Token: 0x06000CBE RID: 3262
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncRequestAchievement")]
		public static extern int IRailPlayerAchievement_AsyncRequestAchievement(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CBF RID: 3263
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_HasAchieved")]
		public static extern int IRailPlayerAchievement_HasAchieved(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out bool jarg3);

		// Token: 0x06000CC0 RID: 3264
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_GetAchievementInfo")]
		public static extern int IRailPlayerAchievement_GetAchievementInfo(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000CC1 RID: 3265
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_0")]
		public static extern int IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, uint jarg3, uint jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5);

		// Token: 0x06000CC2 RID: 3266
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_1")]
		public static extern int IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, uint jarg3, uint jarg4);

		// Token: 0x06000CC3 RID: 3267
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_2")]
		public static extern int IRailPlayerAchievement_AsyncTriggerAchievementProgress__SWIG_2(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, uint jarg3);

		// Token: 0x06000CC4 RID: 3268
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_MakeAchievement")]
		public static extern int IRailPlayerAchievement_MakeAchievement(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CC5 RID: 3269
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_CancelAchievement")]
		public static extern int IRailPlayerAchievement_CancelAchievement(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CC6 RID: 3270
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_AsyncStoreAchievement")]
		public static extern int IRailPlayerAchievement_AsyncStoreAchievement(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CC7 RID: 3271
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_ResetAllAchievements")]
		public static extern int IRailPlayerAchievement_ResetAllAchievements(IntPtr jarg1);

		// Token: 0x06000CC8 RID: 3272
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_GetAllAchievementsName")]
		public static extern int IRailPlayerAchievement_GetAllAchievementsName(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000CC9 RID: 3273
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailPlayerAchievement")]
		public static extern void delete_IRailPlayerAchievement(IntPtr jarg1);

		// Token: 0x06000CCA RID: 3274
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalAchievement_AsyncRequestAchievement")]
		public static extern int IRailGlobalAchievement_AsyncRequestAchievement(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CCB RID: 3275
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalAchievement_GetGlobalAchievedPercent")]
		public static extern int IRailGlobalAchievement_GetGlobalAchievedPercent(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out double jarg3);

		// Token: 0x06000CCC RID: 3276
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalAchievement_GetGlobalAchievedPercentDescending")]
		public static extern int IRailGlobalAchievement_GetGlobalAchievedPercentDescending(IntPtr jarg1, int jarg2, IntPtr jarg3, out double jarg4);

		// Token: 0x06000CCD RID: 3277
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGlobalAchievement")]
		public static extern void delete_IRailGlobalAchievement(IntPtr jarg1);

		// Token: 0x06000CCE RID: 3278
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerAchievementReceived__SWIG_0")]
		public static extern IntPtr new_PlayerAchievementReceived__SWIG_0();

		// Token: 0x06000CCF RID: 3279
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerAchievementReceived__SWIG_1")]
		public static extern IntPtr new_PlayerAchievementReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CD0 RID: 3280
		[DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerAchievementReceived")]
		public static extern void delete_PlayerAchievementReceived(IntPtr jarg1);

		// Token: 0x06000CD1 RID: 3281
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerAchievementStored__SWIG_0")]
		public static extern IntPtr new_PlayerAchievementStored__SWIG_0();

		// Token: 0x06000CD2 RID: 3282
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_group_achievement_set")]
		public static extern void PlayerAchievementStored_group_achievement_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000CD3 RID: 3283
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_group_achievement_get")]
		public static extern bool PlayerAchievementStored_group_achievement_get(IntPtr jarg1);

		// Token: 0x06000CD4 RID: 3284
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_achievement_name_set")]
		public static extern void PlayerAchievementStored_achievement_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CD5 RID: 3285
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_achievement_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string PlayerAchievementStored_achievement_name_get(IntPtr jarg1);

		// Token: 0x06000CD6 RID: 3286
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_current_progress_set")]
		public static extern void PlayerAchievementStored_current_progress_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000CD7 RID: 3287
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_current_progress_get")]
		public static extern uint PlayerAchievementStored_current_progress_get(IntPtr jarg1);

		// Token: 0x06000CD8 RID: 3288
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_max_progress_set")]
		public static extern void PlayerAchievementStored_max_progress_set(IntPtr jarg1, uint jarg2);

		// Token: 0x06000CD9 RID: 3289
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_max_progress_get")]
		public static extern uint PlayerAchievementStored_max_progress_get(IntPtr jarg1);

		// Token: 0x06000CDA RID: 3290
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerAchievementStored__SWIG_1")]
		public static extern IntPtr new_PlayerAchievementStored__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CDB RID: 3291
		[DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerAchievementStored")]
		public static extern void delete_PlayerAchievementStored(IntPtr jarg1);

		// Token: 0x06000CDC RID: 3292
		[DllImport("rail_api", EntryPoint = "CSharp_new_GlobalAchievementReceived__SWIG_0")]
		public static extern IntPtr new_GlobalAchievementReceived__SWIG_0();

		// Token: 0x06000CDD RID: 3293
		[DllImport("rail_api", EntryPoint = "CSharp_GlobalAchievementReceived_count_set")]
		public static extern void GlobalAchievementReceived_count_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000CDE RID: 3294
		[DllImport("rail_api", EntryPoint = "CSharp_GlobalAchievementReceived_count_get")]
		public static extern int GlobalAchievementReceived_count_get(IntPtr jarg1);

		// Token: 0x06000CDF RID: 3295
		[DllImport("rail_api", EntryPoint = "CSharp_new_GlobalAchievementReceived__SWIG_1")]
		public static extern IntPtr new_GlobalAchievementReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CE0 RID: 3296
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GlobalAchievementReceived")]
		public static extern void delete_GlobalAchievementReceived(IntPtr jarg1);

		// Token: 0x06000CE1 RID: 3297
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssetsHelper_OpenAssets")]
		public static extern IntPtr IRailAssetsHelper_OpenAssets(IntPtr jarg1);

		// Token: 0x06000CE2 RID: 3298
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailAssetsHelper")]
		public static extern void delete_IRailAssetsHelper(IntPtr jarg1);

		// Token: 0x06000CE3 RID: 3299
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncRequestAllAssets")]
		public static extern int IRailAssets_AsyncRequestAllAssets(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CE4 RID: 3300
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_QueryAssetInfo")]
		public static extern int IRailAssets_QueryAssetInfo(IntPtr jarg1, ulong jarg2, IntPtr jarg3);

		// Token: 0x06000CE5 RID: 3301
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncUpdateAssetsProperty")]
		public static extern int IRailAssets_AsyncUpdateAssetsProperty(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000CE6 RID: 3302
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncDirectConsumeAssets")]
		public static extern int IRailAssets_AsyncDirectConsumeAssets(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000CE7 RID: 3303
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncStartConsumeAsset")]
		public static extern int IRailAssets_AsyncStartConsumeAsset(IntPtr jarg1, ulong jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000CE8 RID: 3304
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncUpdateConsumeProgress")]
		public static extern int IRailAssets_AsyncUpdateConsumeProgress(IntPtr jarg1, ulong jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000CE9 RID: 3305
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncCompleteConsumeAsset")]
		public static extern int IRailAssets_AsyncCompleteConsumeAsset(IntPtr jarg1, ulong jarg2, uint jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000CEA RID: 3306
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncExchangeAssets")]
		public static extern int IRailAssets_AsyncExchangeAssets(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000CEB RID: 3307
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncExchangeAssetsTo")]
		public static extern int IRailAssets_AsyncExchangeAssetsTo(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, ulong jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5);

		// Token: 0x06000CEC RID: 3308
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncSplitAsset")]
		public static extern int IRailAssets_AsyncSplitAsset(IntPtr jarg1, ulong jarg2, uint jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000CED RID: 3309
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncSplitAssetTo")]
		public static extern int IRailAssets_AsyncSplitAssetTo(IntPtr jarg1, ulong jarg2, uint jarg3, ulong jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5);

		// Token: 0x06000CEE RID: 3310
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncMergeAsset")]
		public static extern int IRailAssets_AsyncMergeAsset(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000CEF RID: 3311
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_AsyncMergeAssetTo")]
		public static extern int IRailAssets_AsyncMergeAssetTo(IntPtr jarg1, IntPtr jarg2, ulong jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000CF0 RID: 3312
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailAssets")]
		public static extern void delete_IRailAssets(IntPtr jarg1);

		// Token: 0x06000CF1 RID: 3313
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_AsyncCreateBrowser__SWIG_0")]
		public static extern IntPtr IRailBrowserHelper_AsyncCreateBrowser__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, uint jarg3, uint jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5, IntPtr jarg6, out RailResult jarg7);

		// Token: 0x06000CF2 RID: 3314
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_AsyncCreateBrowser__SWIG_1")]
		public static extern IntPtr IRailBrowserHelper_AsyncCreateBrowser__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, uint jarg3, uint jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5, IntPtr jarg6);

		// Token: 0x06000CF3 RID: 3315
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_AsyncCreateBrowser__SWIG_2")]
		public static extern IntPtr IRailBrowserHelper_AsyncCreateBrowser__SWIG_2(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, uint jarg3, uint jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5);

		// Token: 0x06000CF4 RID: 3316
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_0")]
		public static extern IntPtr IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, IntPtr jarg4, out RailResult jarg5);

		// Token: 0x06000CF5 RID: 3317
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_1")]
		public static extern IntPtr IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, IntPtr jarg4);

		// Token: 0x06000CF6 RID: 3318
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_2")]
		public static extern IntPtr IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_2(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000CF7 RID: 3319
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserHelper_NavigateWebPage")]
		public static extern int IRailBrowserHelper_NavigateWebPage(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, bool jarg3);

		// Token: 0x06000CF8 RID: 3320
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailBrowserHelper")]
		public static extern void delete_IRailBrowserHelper(IntPtr jarg1);

		// Token: 0x06000CF9 RID: 3321
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_GetCurrentUrl")]
		public static extern bool IRailBrowser_GetCurrentUrl(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000CFA RID: 3322
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_ReloadWithUrl__SWIG_0")]
		public static extern bool IRailBrowser_ReloadWithUrl__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CFB RID: 3323
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_ReloadWithUrl__SWIG_1")]
		public static extern bool IRailBrowser_ReloadWithUrl__SWIG_1(IntPtr jarg1);

		// Token: 0x06000CFC RID: 3324
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_StopLoad")]
		public static extern void IRailBrowser_StopLoad(IntPtr jarg1);

		// Token: 0x06000CFD RID: 3325
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_AddJavascriptEventListener")]
		public static extern bool IRailBrowser_AddJavascriptEventListener(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000CFE RID: 3326
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_RemoveAllJavascriptEventListener")]
		public static extern bool IRailBrowser_RemoveAllJavascriptEventListener(IntPtr jarg1);

		// Token: 0x06000CFF RID: 3327
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_AllowNavigateNewPage")]
		public static extern void IRailBrowser_AllowNavigateNewPage(IntPtr jarg1, bool jarg2);

		// Token: 0x06000D00 RID: 3328
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_Close")]
		public static extern void IRailBrowser_Close(IntPtr jarg1);

		// Token: 0x06000D01 RID: 3329
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailBrowser")]
		public static extern void delete_IRailBrowser(IntPtr jarg1);

		// Token: 0x06000D02 RID: 3330
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_GetCurrentUrl")]
		public static extern bool IRailBrowserRender_GetCurrentUrl(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D03 RID: 3331
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_ReloadWithUrl__SWIG_0")]
		public static extern bool IRailBrowserRender_ReloadWithUrl__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D04 RID: 3332
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_ReloadWithUrl__SWIG_1")]
		public static extern bool IRailBrowserRender_ReloadWithUrl__SWIG_1(IntPtr jarg1);

		// Token: 0x06000D05 RID: 3333
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_StopLoad")]
		public static extern void IRailBrowserRender_StopLoad(IntPtr jarg1);

		// Token: 0x06000D06 RID: 3334
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_AddJavascriptEventListener")]
		public static extern bool IRailBrowserRender_AddJavascriptEventListener(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D07 RID: 3335
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_RemoveAllJavascriptEventListener")]
		public static extern bool IRailBrowserRender_RemoveAllJavascriptEventListener(IntPtr jarg1);

		// Token: 0x06000D08 RID: 3336
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_AllowNavigateNewPage")]
		public static extern void IRailBrowserRender_AllowNavigateNewPage(IntPtr jarg1, bool jarg2);

		// Token: 0x06000D09 RID: 3337
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_Close")]
		public static extern void IRailBrowserRender_Close(IntPtr jarg1);

		// Token: 0x06000D0A RID: 3338
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_UpdateCustomDrawWindowPos")]
		public static extern void IRailBrowserRender_UpdateCustomDrawWindowPos(IntPtr jarg1, int jarg2, int jarg3, uint jarg4, uint jarg5);

		// Token: 0x06000D0B RID: 3339
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_SetBrowserActive")]
		public static extern void IRailBrowserRender_SetBrowserActive(IntPtr jarg1, bool jarg2);

		// Token: 0x06000D0C RID: 3340
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_GoBack")]
		public static extern void IRailBrowserRender_GoBack(IntPtr jarg1);

		// Token: 0x06000D0D RID: 3341
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_GoForward")]
		public static extern void IRailBrowserRender_GoForward(IntPtr jarg1);

		// Token: 0x06000D0E RID: 3342
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_ExecuteJavascript")]
		public static extern bool IRailBrowserRender_ExecuteJavascript(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D0F RID: 3343
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_DispatchWindowsMessage")]
		public static extern void IRailBrowserRender_DispatchWindowsMessage(IntPtr jarg1, uint jarg2, uint jarg3, uint jarg4);

		// Token: 0x06000D10 RID: 3344
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_DispatchMouseMessage")]
		public static extern void IRailBrowserRender_DispatchMouseMessage(IntPtr jarg1, int jarg2, uint jarg3, uint jarg4, uint jarg5);

		// Token: 0x06000D11 RID: 3345
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_MouseWheel")]
		public static extern void IRailBrowserRender_MouseWheel(IntPtr jarg1, int jarg2, uint jarg3, uint jarg4, uint jarg5);

		// Token: 0x06000D12 RID: 3346
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_SetFocus")]
		public static extern void IRailBrowserRender_SetFocus(IntPtr jarg1, bool jarg2);

		// Token: 0x06000D13 RID: 3347
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_KeyDown")]
		public static extern void IRailBrowserRender_KeyDown(IntPtr jarg1, uint jarg2);

		// Token: 0x06000D14 RID: 3348
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_KeyUp")]
		public static extern void IRailBrowserRender_KeyUp(IntPtr jarg1, uint jarg2);

		// Token: 0x06000D15 RID: 3349
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_KeyChar")]
		public static extern void IRailBrowserRender_KeyChar(IntPtr jarg1, uint jarg2, bool jarg3);

		// Token: 0x06000D16 RID: 3350
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailBrowserRender")]
		public static extern void delete_IRailBrowserRender(IntPtr jarg1);

		// Token: 0x06000D17 RID: 3351
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_AsyncQueryIsOwnedDlcsOnServer")]
		public static extern int IRailDlcHelper_AsyncQueryIsOwnedDlcsOnServer(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D18 RID: 3352
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_AsyncCheckAllDlcsStateReady")]
		public static extern int IRailDlcHelper_AsyncCheckAllDlcsStateReady(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D19 RID: 3353
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_IsDlcInstalled__SWIG_0")]
		public static extern bool IRailDlcHelper_IsDlcInstalled__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);

		// Token: 0x06000D1A RID: 3354
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_IsDlcInstalled__SWIG_1")]
		public static extern bool IRailDlcHelper_IsDlcInstalled__SWIG_1(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D1B RID: 3355
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_IsOwnedDlc")]
		public static extern bool IRailDlcHelper_IsOwnedDlc(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D1C RID: 3356
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_GetDlcCount")]
		public static extern uint IRailDlcHelper_GetDlcCount(IntPtr jarg1);

		// Token: 0x06000D1D RID: 3357
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_GetDlcInfo")]
		public static extern bool IRailDlcHelper_GetDlcInfo(IntPtr jarg1, uint jarg2, IntPtr jarg3);

		// Token: 0x06000D1E RID: 3358
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_AsyncInstallDlc")]
		public static extern bool IRailDlcHelper_AsyncInstallDlc(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D1F RID: 3359
		[DllImport("rail_api", EntryPoint = "CSharp_IRailDlcHelper_AsyncRemoveDlc")]
		public static extern bool IRailDlcHelper_AsyncRemoveDlc(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D20 RID: 3360
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailDlcHelper")]
		public static extern void delete_IRailDlcHelper(IntPtr jarg1);

		// Token: 0x06000D21 RID: 3361
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_AsyncShowRailFloatingWindow")]
		public static extern int IRailFloatingWindow_AsyncShowRailFloatingWindow(IntPtr jarg1, int jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D22 RID: 3362
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_AsyncCloseRailFloatingWindow")]
		public static extern int IRailFloatingWindow_AsyncCloseRailFloatingWindow(IntPtr jarg1, int jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D23 RID: 3363
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_SetNotifyWindowPosition")]
		public static extern int IRailFloatingWindow_SetNotifyWindowPosition(IntPtr jarg1, int jarg2, IntPtr jarg3);

		// Token: 0x06000D24 RID: 3364
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_AsyncShowStoreWindow")]
		public static extern int IRailFloatingWindow_AsyncShowStoreWindow(IntPtr jarg1, ulong jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000D25 RID: 3365
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_IsFloatingWindowAvailable")]
		public static extern bool IRailFloatingWindow_IsFloatingWindowAvailable(IntPtr jarg1);

		// Token: 0x06000D26 RID: 3366
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_AsyncShowDefaultGameStoreWindow")]
		public static extern int IRailFloatingWindow_AsyncShowDefaultGameStoreWindow(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D27 RID: 3367
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFloatingWindow_SetNotifyWindowEnable")]
		public static extern int IRailFloatingWindow_SetNotifyWindowEnable(IntPtr jarg1, int jarg2, bool jarg3);

		// Token: 0x06000D28 RID: 3368
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailFloatingWindow")]
		public static extern void delete_IRailFloatingWindow(IntPtr jarg1);

		// Token: 0x06000D29 RID: 3369
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncGetPersonalInfo")]
		public static extern int IRailFriends_AsyncGetPersonalInfo(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D2A RID: 3370
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncGetFriendMetadata")]
		public static extern int IRailFriends_AsyncGetFriendMetadata(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000D2B RID: 3371
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncSetMyMetadata")]
		public static extern int IRailFriends_AsyncSetMyMetadata(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D2C RID: 3372
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncClearAllMyMetadata")]
		public static extern int IRailFriends_AsyncClearAllMyMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D2D RID: 3373
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncSetInviteCommandLine")]
		public static extern int IRailFriends_AsyncSetInviteCommandLine(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D2E RID: 3374
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncGetInviteCommandLine")]
		public static extern int IRailFriends_AsyncGetInviteCommandLine(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D2F RID: 3375
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncReportPlayedWithUserList")]
		public static extern int IRailFriends_AsyncReportPlayedWithUserList(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D30 RID: 3376
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_GetFriendsList")]
		public static extern int IRailFriends_GetFriendsList(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D31 RID: 3377
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncQueryFriendPlayedGamesInfo")]
		public static extern int IRailFriends_AsyncQueryFriendPlayedGamesInfo(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D32 RID: 3378
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncQueryPlayedWithFriendsList")]
		public static extern int IRailFriends_AsyncQueryPlayedWithFriendsList(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D33 RID: 3379
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncQueryPlayedWithFriendsTime")]
		public static extern int IRailFriends_AsyncQueryPlayedWithFriendsTime(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D34 RID: 3380
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncQueryPlayedWithFriendsGames")]
		public static extern int IRailFriends_AsyncQueryPlayedWithFriendsGames(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D35 RID: 3381
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncAddFriend")]
		public static extern int IRailFriends_AsyncAddFriend(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D36 RID: 3382
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFriends_AsyncUpdateFriendsData")]
		public static extern int IRailFriends_AsyncUpdateFriendsData(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D37 RID: 3383
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailFriends")]
		public static extern void delete_IRailFriends(IntPtr jarg1);

		// Token: 0x06000D38 RID: 3384
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncGetGameServerPlayerList")]
		public static extern int IRailGameServerHelper_AsyncGetGameServerPlayerList(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D39 RID: 3385
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncGetGameServerList")]
		public static extern int IRailGameServerHelper_AsyncGetGameServerList(IntPtr jarg1, uint jarg2, uint jarg3, IntPtr jarg4, IntPtr jarg5, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg6);

		// Token: 0x06000D3A RID: 3386
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncCreateGameServer__SWIG_0")]
		public static extern IntPtr IRailGameServerHelper_AsyncCreateGameServer__SWIG_0(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000D3B RID: 3387
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncCreateGameServer__SWIG_1")]
		public static extern IntPtr IRailGameServerHelper_AsyncCreateGameServer__SWIG_1(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D3C RID: 3388
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncCreateGameServer__SWIG_2")]
		public static extern IntPtr IRailGameServerHelper_AsyncCreateGameServer__SWIG_2(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D3D RID: 3389
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncCreateGameServer__SWIG_3")]
		public static extern IntPtr IRailGameServerHelper_AsyncCreateGameServer__SWIG_3(IntPtr jarg1);

		// Token: 0x06000D3E RID: 3390
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_0")]
		public static extern int IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D3F RID: 3391
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_1")]
		public static extern int IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_1(IntPtr jarg1);

		// Token: 0x06000D40 RID: 3392
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_0")]
		public static extern int IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_0(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D41 RID: 3393
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_1")]
		public static extern int IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_1(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D42 RID: 3394
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_0")]
		public static extern int IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_0(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D43 RID: 3395
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_1")]
		public static extern int IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_1(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D44 RID: 3396
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGameServerHelper")]
		public static extern void delete_IRailGameServerHelper(IntPtr jarg1);

		// Token: 0x06000D45 RID: 3397
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerRailID")]
		public static extern IntPtr IRailGameServer_GetGameServerRailID(IntPtr jarg1);

		// Token: 0x06000D46 RID: 3398
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerName")]
		public static extern int IRailGameServer_GetGameServerName(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D47 RID: 3399
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerFullName")]
		public static extern int IRailGameServer_GetGameServerFullName(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D48 RID: 3400
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetOwnerRailID")]
		public static extern IntPtr IRailGameServer_GetOwnerRailID(IntPtr jarg1);

		// Token: 0x06000D49 RID: 3401
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetZoneID")]
		public static extern bool IRailGameServer_SetZoneID(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000D4A RID: 3402
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetZoneID")]
		public static extern ulong IRailGameServer_GetZoneID(IntPtr jarg1);

		// Token: 0x06000D4B RID: 3403
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetHost")]
		public static extern bool IRailGameServer_SetHost(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D4C RID: 3404
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetHost")]
		public static extern bool IRailGameServer_GetHost(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D4D RID: 3405
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetMapName")]
		public static extern bool IRailGameServer_SetMapName(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D4E RID: 3406
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetMapName")]
		public static extern bool IRailGameServer_GetMapName(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D4F RID: 3407
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetPasswordProtect")]
		public static extern bool IRailGameServer_SetPasswordProtect(IntPtr jarg1, bool jarg2);

		// Token: 0x06000D50 RID: 3408
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetPasswordProtect")]
		public static extern bool IRailGameServer_GetPasswordProtect(IntPtr jarg1);

		// Token: 0x06000D51 RID: 3409
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetMaxPlayers")]
		public static extern bool IRailGameServer_SetMaxPlayers(IntPtr jarg1, uint jarg2);

		// Token: 0x06000D52 RID: 3410
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetMaxPlayers")]
		public static extern uint IRailGameServer_GetMaxPlayers(IntPtr jarg1);

		// Token: 0x06000D53 RID: 3411
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetBotPlayers")]
		public static extern bool IRailGameServer_SetBotPlayers(IntPtr jarg1, uint jarg2);

		// Token: 0x06000D54 RID: 3412
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetBotPlayers")]
		public static extern uint IRailGameServer_GetBotPlayers(IntPtr jarg1);

		// Token: 0x06000D55 RID: 3413
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetGameServerDescription")]
		public static extern bool IRailGameServer_SetGameServerDescription(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D56 RID: 3414
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerDescription")]
		public static extern bool IRailGameServer_GetGameServerDescription(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D57 RID: 3415
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetGameServerTags")]
		public static extern bool IRailGameServer_SetGameServerTags(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D58 RID: 3416
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerTags")]
		public static extern bool IRailGameServer_GetGameServerTags(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D59 RID: 3417
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetMods")]
		public static extern bool IRailGameServer_SetMods(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D5A RID: 3418
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetMods")]
		public static extern bool IRailGameServer_GetMods(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D5B RID: 3419
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetSpectatorHost")]
		public static extern bool IRailGameServer_SetSpectatorHost(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D5C RID: 3420
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetSpectatorHost")]
		public static extern bool IRailGameServer_GetSpectatorHost(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D5D RID: 3421
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetGameServerVersion")]
		public static extern bool IRailGameServer_SetGameServerVersion(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D5E RID: 3422
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetGameServerVersion")]
		public static extern bool IRailGameServer_GetGameServerVersion(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D5F RID: 3423
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetIsFriendOnly")]
		public static extern bool IRailGameServer_SetIsFriendOnly(IntPtr jarg1, bool jarg2);

		// Token: 0x06000D60 RID: 3424
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetIsFriendOnly")]
		public static extern bool IRailGameServer_GetIsFriendOnly(IntPtr jarg1);

		// Token: 0x06000D61 RID: 3425
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_ClearAllMetadata")]
		public static extern bool IRailGameServer_ClearAllMetadata(IntPtr jarg1);

		// Token: 0x06000D62 RID: 3426
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetMetadata")]
		public static extern int IRailGameServer_GetMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000D63 RID: 3427
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetMetadata")]
		public static extern int IRailGameServer_SetMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D64 RID: 3428
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncSetMetadata")]
		public static extern int IRailGameServer_AsyncSetMetadata(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D65 RID: 3429
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncGetMetadata")]
		public static extern int IRailGameServer_AsyncGetMetadata(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D66 RID: 3430
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncGetAllMetadata")]
		public static extern int IRailGameServer_AsyncGetAllMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D67 RID: 3431
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncAcquireGameServerSessionTicket")]
		public static extern int IRailGameServer_AsyncAcquireGameServerSessionTicket(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D68 RID: 3432
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AsyncStartSessionWithPlayer")]
		public static extern int IRailGameServer_AsyncStartSessionWithPlayer(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000D69 RID: 3433
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_TerminateSessionOfPlayer")]
		public static extern void IRailGameServer_TerminateSessionOfPlayer(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D6A RID: 3434
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_AbandonGameServerSessionTicket")]
		public static extern void IRailGameServer_AbandonGameServerSessionTicket(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D6B RID: 3435
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_ReportPlayerJoinGameServer")]
		public static extern int IRailGameServer_ReportPlayerJoinGameServer(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D6C RID: 3436
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_ReportPlayerQuitGameServer")]
		public static extern int IRailGameServer_ReportPlayerQuitGameServer(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D6D RID: 3437
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_UpdateGameServerPlayerList")]
		public static extern int IRailGameServer_UpdateGameServerPlayerList(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D6E RID: 3438
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetCurrentPlayers")]
		public static extern uint IRailGameServer_GetCurrentPlayers(IntPtr jarg1);

		// Token: 0x06000D6F RID: 3439
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_RemoveAllPlayers")]
		public static extern void IRailGameServer_RemoveAllPlayers(IntPtr jarg1);

		// Token: 0x06000D70 RID: 3440
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_RegisterToGameServerList")]
		public static extern int IRailGameServer_RegisterToGameServerList(IntPtr jarg1);

		// Token: 0x06000D71 RID: 3441
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_UnregisterFromGameServerList")]
		public static extern int IRailGameServer_UnregisterFromGameServerList(IntPtr jarg1);

		// Token: 0x06000D72 RID: 3442
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_CloseGameServer")]
		public static extern int IRailGameServer_CloseGameServer(IntPtr jarg1);

		// Token: 0x06000D73 RID: 3443
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetFriendsInGameServer")]
		public static extern int IRailGameServer_GetFriendsInGameServer(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D74 RID: 3444
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_IsUserInGameServer")]
		public static extern bool IRailGameServer_IsUserInGameServer(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D75 RID: 3445
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SetServerInfo")]
		public static extern bool IRailGameServer_SetServerInfo(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D76 RID: 3446
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_GetServerInfo")]
		public static extern bool IRailGameServer_GetServerInfo(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D77 RID: 3447
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_EnableTeamVoice")]
		public static extern int IRailGameServer_EnableTeamVoice(IntPtr jarg1, bool jarg2);

		// Token: 0x06000D78 RID: 3448
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGameServer")]
		public static extern void delete_IRailGameServer(IntPtr jarg1);

		// Token: 0x06000D79 RID: 3449
		[DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncRequestAllPurchasableProducts")]
		public static extern int IRailInGamePurchase_AsyncRequestAllPurchasableProducts(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D7A RID: 3450
		[DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncRequestAllProducts")]
		public static extern int IRailInGamePurchase_AsyncRequestAllProducts(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D7B RID: 3451
		[DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_GetProductInfo")]
		public static extern int IRailInGamePurchase_GetProductInfo(IntPtr jarg1, uint jarg2, IntPtr jarg3);

		// Token: 0x06000D7C RID: 3452
		[DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncPurchaseProducts")]
		public static extern int IRailInGamePurchase_AsyncPurchaseProducts(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D7D RID: 3453
		[DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncFinishOrder")]
		public static extern int IRailInGamePurchase_AsyncFinishOrder(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D7E RID: 3454
		[DllImport("rail_api", EntryPoint = "CSharp_IRailInGamePurchase_AsyncPurchaseProductsToAssets")]
		public static extern int IRailInGamePurchase_AsyncPurchaseProductsToAssets(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000D7F RID: 3455
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailInGamePurchase")]
		public static extern void delete_IRailInGamePurchase(IntPtr jarg1);

		// Token: 0x06000D80 RID: 3456
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardParameters__SWIG_0")]
		public static extern IntPtr new_LeaderboardParameters__SWIG_0();

		// Token: 0x06000D81 RID: 3457
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardParameters_param_set")]
		public static extern void LeaderboardParameters_param_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D82 RID: 3458
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardParameters_param_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string LeaderboardParameters_param_get(IntPtr jarg1);

		// Token: 0x06000D83 RID: 3459
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardParameters__SWIG_1")]
		public static extern IntPtr new_LeaderboardParameters__SWIG_1(IntPtr jarg1);

		// Token: 0x06000D84 RID: 3460
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardParameters")]
		public static extern void delete_LeaderboardParameters(IntPtr jarg1);

		// Token: 0x06000D85 RID: 3461
		[DllImport("rail_api", EntryPoint = "CSharp_new_RequestLeaderboardEntryParam__SWIG_0")]
		public static extern IntPtr new_RequestLeaderboardEntryParam__SWIG_0();

		// Token: 0x06000D86 RID: 3462
		[DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_type_set")]
		public static extern void RequestLeaderboardEntryParam_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000D87 RID: 3463
		[DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_type_get")]
		public static extern int RequestLeaderboardEntryParam_type_get(IntPtr jarg1);

		// Token: 0x06000D88 RID: 3464
		[DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_range_start_set")]
		public static extern void RequestLeaderboardEntryParam_range_start_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000D89 RID: 3465
		[DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_range_start_get")]
		public static extern int RequestLeaderboardEntryParam_range_start_get(IntPtr jarg1);

		// Token: 0x06000D8A RID: 3466
		[DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_range_end_set")]
		public static extern void RequestLeaderboardEntryParam_range_end_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000D8B RID: 3467
		[DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_range_end_get")]
		public static extern int RequestLeaderboardEntryParam_range_end_get(IntPtr jarg1);

		// Token: 0x06000D8C RID: 3468
		[DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_user_coordinate_set")]
		public static extern void RequestLeaderboardEntryParam_user_coordinate_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000D8D RID: 3469
		[DllImport("rail_api", EntryPoint = "CSharp_RequestLeaderboardEntryParam_user_coordinate_get")]
		public static extern bool RequestLeaderboardEntryParam_user_coordinate_get(IntPtr jarg1);

		// Token: 0x06000D8E RID: 3470
		[DllImport("rail_api", EntryPoint = "CSharp_new_RequestLeaderboardEntryParam__SWIG_1")]
		public static extern IntPtr new_RequestLeaderboardEntryParam__SWIG_1(IntPtr jarg1);

		// Token: 0x06000D8F RID: 3471
		[DllImport("rail_api", EntryPoint = "CSharp_delete_RequestLeaderboardEntryParam")]
		public static extern void delete_RequestLeaderboardEntryParam(IntPtr jarg1);

		// Token: 0x06000D90 RID: 3472
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardData__SWIG_0")]
		public static extern IntPtr new_LeaderboardData__SWIG_0();

		// Token: 0x06000D91 RID: 3473
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_score_set")]
		public static extern void LeaderboardData_score_set(IntPtr jarg1, double jarg2);

		// Token: 0x06000D92 RID: 3474
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_score_get")]
		public static extern double LeaderboardData_score_get(IntPtr jarg1);

		// Token: 0x06000D93 RID: 3475
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_rank_set")]
		public static extern void LeaderboardData_rank_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000D94 RID: 3476
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_rank_get")]
		public static extern int LeaderboardData_rank_get(IntPtr jarg1);

		// Token: 0x06000D95 RID: 3477
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_spacework_id_set")]
		public static extern void LeaderboardData_spacework_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D96 RID: 3478
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_spacework_id_get")]
		public static extern IntPtr LeaderboardData_spacework_id_get(IntPtr jarg1);

		// Token: 0x06000D97 RID: 3479
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_additional_infomation_set")]
		public static extern void LeaderboardData_additional_infomation_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000D98 RID: 3480
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardData_additional_infomation_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string LeaderboardData_additional_infomation_get(IntPtr jarg1);

		// Token: 0x06000D99 RID: 3481
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardData__SWIG_1")]
		public static extern IntPtr new_LeaderboardData__SWIG_1(IntPtr jarg1);

		// Token: 0x06000D9A RID: 3482
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardData")]
		public static extern void delete_LeaderboardData(IntPtr jarg1);

		// Token: 0x06000D9B RID: 3483
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardEntry__SWIG_0")]
		public static extern IntPtr new_LeaderboardEntry__SWIG_0();

		// Token: 0x06000D9C RID: 3484
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntry_player_id_set")]
		public static extern void LeaderboardEntry_player_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D9D RID: 3485
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntry_player_id_get")]
		public static extern IntPtr LeaderboardEntry_player_id_get(IntPtr jarg1);

		// Token: 0x06000D9E RID: 3486
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntry_data_set")]
		public static extern void LeaderboardEntry_data_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000D9F RID: 3487
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntry_data_get")]
		public static extern IntPtr LeaderboardEntry_data_get(IntPtr jarg1);

		// Token: 0x06000DA0 RID: 3488
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardEntry__SWIG_1")]
		public static extern IntPtr new_LeaderboardEntry__SWIG_1(IntPtr jarg1);

		// Token: 0x06000DA1 RID: 3489
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardEntry")]
		public static extern void delete_LeaderboardEntry(IntPtr jarg1);

		// Token: 0x06000DA2 RID: 3490
		[DllImport("rail_api", EntryPoint = "CSharp_new_UploadLeaderboardParam__SWIG_0")]
		public static extern IntPtr new_UploadLeaderboardParam__SWIG_0();

		// Token: 0x06000DA3 RID: 3491
		[DllImport("rail_api", EntryPoint = "CSharp_UploadLeaderboardParam_type_set")]
		public static extern void UploadLeaderboardParam_type_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000DA4 RID: 3492
		[DllImport("rail_api", EntryPoint = "CSharp_UploadLeaderboardParam_type_get")]
		public static extern int UploadLeaderboardParam_type_get(IntPtr jarg1);

		// Token: 0x06000DA5 RID: 3493
		[DllImport("rail_api", EntryPoint = "CSharp_UploadLeaderboardParam_data_set")]
		public static extern void UploadLeaderboardParam_data_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000DA6 RID: 3494
		[DllImport("rail_api", EntryPoint = "CSharp_UploadLeaderboardParam_data_get")]
		public static extern IntPtr UploadLeaderboardParam_data_get(IntPtr jarg1);

		// Token: 0x06000DA7 RID: 3495
		[DllImport("rail_api", EntryPoint = "CSharp_new_UploadLeaderboardParam__SWIG_1")]
		public static extern IntPtr new_UploadLeaderboardParam__SWIG_1(IntPtr jarg1);

		// Token: 0x06000DA8 RID: 3496
		[DllImport("rail_api", EntryPoint = "CSharp_delete_UploadLeaderboardParam")]
		public static extern void delete_UploadLeaderboardParam(IntPtr jarg1);

		// Token: 0x06000DA9 RID: 3497
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailLeaderboardHelper")]
		public static extern void delete_IRailLeaderboardHelper(IntPtr jarg1);

		// Token: 0x06000DAA RID: 3498
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardHelper_OpenLeaderboard")]
		public static extern IntPtr IRailLeaderboardHelper_OpenLeaderboard(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DAB RID: 3499
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardHelper_AsyncCreateLeaderboard")]
		public static extern IntPtr IRailLeaderboardHelper_AsyncCreateLeaderboard(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, int jarg3, int jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5, out RailResult jarg6);

		// Token: 0x06000DAC RID: 3500
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetLeaderboardName")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailLeaderboard_GetLeaderboardName(IntPtr jarg1);

		// Token: 0x06000DAD RID: 3501
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetTotalEntriesCount")]
		public static extern int IRailLeaderboard_GetTotalEntriesCount(IntPtr jarg1);

		// Token: 0x06000DAE RID: 3502
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_AsyncGetLeaderboard")]
		public static extern int IRailLeaderboard_AsyncGetLeaderboard(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DAF RID: 3503
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetLeaderboardParameters")]
		public static extern int IRailLeaderboard_GetLeaderboardParameters(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000DB0 RID: 3504
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_CreateLeaderboardEntries")]
		public static extern IntPtr IRailLeaderboard_CreateLeaderboardEntries(IntPtr jarg1);

		// Token: 0x06000DB1 RID: 3505
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_AsyncUploadLeaderboard")]
		public static extern int IRailLeaderboard_AsyncUploadLeaderboard(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000DB2 RID: 3506
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetLeaderboardSortType")]
		public static extern int IRailLeaderboard_GetLeaderboardSortType(IntPtr jarg1, out int jarg2);

		// Token: 0x06000DB3 RID: 3507
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_GetLeaderboardDisplayType")]
		public static extern int IRailLeaderboard_GetLeaderboardDisplayType(IntPtr jarg1, out int jarg2);

		// Token: 0x06000DB4 RID: 3508
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_AsyncAttachSpaceWork")]
		public static extern int IRailLeaderboard_AsyncAttachSpaceWork(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000DB5 RID: 3509
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailLeaderboard")]
		public static extern void delete_IRailLeaderboard(IntPtr jarg1);

		// Token: 0x06000DB6 RID: 3510
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetRailID")]
		public static extern IntPtr IRailLeaderboardEntries_GetRailID(IntPtr jarg1);

		// Token: 0x06000DB7 RID: 3511
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetLeaderboardName")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailLeaderboardEntries_GetLeaderboardName(IntPtr jarg1);

		// Token: 0x06000DB8 RID: 3512
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_AsyncRequestLeaderboardEntries")]
		public static extern int IRailLeaderboardEntries_AsyncRequestLeaderboardEntries(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000DB9 RID: 3513
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetEntriesParam")]
		public static extern IntPtr IRailLeaderboardEntries_GetEntriesParam(IntPtr jarg1);

		// Token: 0x06000DBA RID: 3514
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetEntriesCount")]
		public static extern int IRailLeaderboardEntries_GetEntriesCount(IntPtr jarg1);

		// Token: 0x06000DBB RID: 3515
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_GetLeaderboardEntry")]
		public static extern int IRailLeaderboardEntries_GetLeaderboardEntry(IntPtr jarg1, int jarg2, IntPtr jarg3);

		// Token: 0x06000DBC RID: 3516
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailLeaderboardEntries")]
		public static extern void delete_IRailLeaderboardEntries(IntPtr jarg1);

		// Token: 0x06000DBD RID: 3517
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardReceived__SWIG_0")]
		public static extern IntPtr new_LeaderboardReceived__SWIG_0();

		// Token: 0x06000DBE RID: 3518
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_leaderboard_name_set")]
		public static extern void LeaderboardReceived_leaderboard_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DBF RID: 3519
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_leaderboard_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string LeaderboardReceived_leaderboard_name_get(IntPtr jarg1);

		// Token: 0x06000DC0 RID: 3520
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_does_exist_set")]
		public static extern void LeaderboardReceived_does_exist_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000DC1 RID: 3521
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_does_exist_get")]
		public static extern bool LeaderboardReceived_does_exist_get(IntPtr jarg1);

		// Token: 0x06000DC2 RID: 3522
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardReceived__SWIG_1")]
		public static extern IntPtr new_LeaderboardReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000DC3 RID: 3523
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardReceived")]
		public static extern void delete_LeaderboardReceived(IntPtr jarg1);

		// Token: 0x06000DC4 RID: 3524
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardCreated__SWIG_0")]
		public static extern IntPtr new_LeaderboardCreated__SWIG_0();

		// Token: 0x06000DC5 RID: 3525
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardCreated_leaderboard_name_set")]
		public static extern void LeaderboardCreated_leaderboard_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DC6 RID: 3526
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardCreated_leaderboard_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string LeaderboardCreated_leaderboard_name_get(IntPtr jarg1);

		// Token: 0x06000DC7 RID: 3527
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardCreated__SWIG_1")]
		public static extern IntPtr new_LeaderboardCreated__SWIG_1(IntPtr jarg1);

		// Token: 0x06000DC8 RID: 3528
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardCreated")]
		public static extern void delete_LeaderboardCreated(IntPtr jarg1);

		// Token: 0x06000DC9 RID: 3529
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardEntryReceived__SWIG_0")]
		public static extern IntPtr new_LeaderboardEntryReceived__SWIG_0();

		// Token: 0x06000DCA RID: 3530
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntryReceived_leaderboard_name_set")]
		public static extern void LeaderboardEntryReceived_leaderboard_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DCB RID: 3531
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntryReceived_leaderboard_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string LeaderboardEntryReceived_leaderboard_name_get(IntPtr jarg1);

		// Token: 0x06000DCC RID: 3532
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardEntryReceived__SWIG_1")]
		public static extern IntPtr new_LeaderboardEntryReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000DCD RID: 3533
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardEntryReceived")]
		public static extern void delete_LeaderboardEntryReceived(IntPtr jarg1);

		// Token: 0x06000DCE RID: 3534
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardUploaded__SWIG_0")]
		public static extern IntPtr new_LeaderboardUploaded__SWIG_0();

		// Token: 0x06000DCF RID: 3535
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_leaderboard_name_set")]
		public static extern void LeaderboardUploaded_leaderboard_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DD0 RID: 3536
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_leaderboard_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string LeaderboardUploaded_leaderboard_name_get(IntPtr jarg1);

		// Token: 0x06000DD1 RID: 3537
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_score_set")]
		public static extern void LeaderboardUploaded_score_set(IntPtr jarg1, double jarg2);

		// Token: 0x06000DD2 RID: 3538
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_score_get")]
		public static extern double LeaderboardUploaded_score_get(IntPtr jarg1);

		// Token: 0x06000DD3 RID: 3539
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_better_score_set")]
		public static extern void LeaderboardUploaded_better_score_set(IntPtr jarg1, bool jarg2);

		// Token: 0x06000DD4 RID: 3540
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_better_score_get")]
		public static extern bool LeaderboardUploaded_better_score_get(IntPtr jarg1);

		// Token: 0x06000DD5 RID: 3541
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_new_rank_set")]
		public static extern void LeaderboardUploaded_new_rank_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000DD6 RID: 3542
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_new_rank_get")]
		public static extern int LeaderboardUploaded_new_rank_get(IntPtr jarg1);

		// Token: 0x06000DD7 RID: 3543
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_old_rank_set")]
		public static extern void LeaderboardUploaded_old_rank_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000DD8 RID: 3544
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_old_rank_get")]
		public static extern int LeaderboardUploaded_old_rank_get(IntPtr jarg1);

		// Token: 0x06000DD9 RID: 3545
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardUploaded__SWIG_1")]
		public static extern IntPtr new_LeaderboardUploaded__SWIG_1(IntPtr jarg1);

		// Token: 0x06000DDA RID: 3546
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardUploaded")]
		public static extern void delete_LeaderboardUploaded(IntPtr jarg1);

		// Token: 0x06000DDB RID: 3547
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardAttachSpaceWork__SWIG_0")]
		public static extern IntPtr new_LeaderboardAttachSpaceWork__SWIG_0();

		// Token: 0x06000DDC RID: 3548
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_leaderboard_name_set")]
		public static extern void LeaderboardAttachSpaceWork_leaderboard_name_set(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DDD RID: 3549
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_leaderboard_name_get")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string LeaderboardAttachSpaceWork_leaderboard_name_get(IntPtr jarg1);

		// Token: 0x06000DDE RID: 3550
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_spacework_id_set")]
		public static extern void LeaderboardAttachSpaceWork_spacework_id_set(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000DDF RID: 3551
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_spacework_id_get")]
		public static extern IntPtr LeaderboardAttachSpaceWork_spacework_id_get(IntPtr jarg1);

		// Token: 0x06000DE0 RID: 3552
		[DllImport("rail_api", EntryPoint = "CSharp_new_LeaderboardAttachSpaceWork__SWIG_1")]
		public static extern IntPtr new_LeaderboardAttachSpaceWork__SWIG_1(IntPtr jarg1);

		// Token: 0x06000DE1 RID: 3553
		[DllImport("rail_api", EntryPoint = "CSharp_delete_LeaderboardAttachSpaceWork")]
		public static extern void delete_LeaderboardAttachSpaceWork(IntPtr jarg1);

		// Token: 0x06000DE2 RID: 3554
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_AcceptSessionRequest")]
		public static extern int IRailNetwork_AcceptSessionRequest(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);

		// Token: 0x06000DE3 RID: 3555
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_SendData__SWIG_0")]
		public static extern int IRailNetwork_SendData__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(42)] [In] [Out] byte[] jarg4, uint jarg5, uint jarg6);

		// Token: 0x06000DE4 RID: 3556
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_SendData__SWIG_1")]
		public static extern int IRailNetwork_SendData__SWIG_1(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(42)] [In] [Out] byte[] jarg4, uint jarg5);

		// Token: 0x06000DE5 RID: 3557
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_SendReliableData__SWIG_0")]
		public static extern int IRailNetwork_SendReliableData__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(42)] [In] [Out] byte[] jarg4, uint jarg5, uint jarg6);

		// Token: 0x06000DE6 RID: 3558
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_SendReliableData__SWIG_1")]
		public static extern int IRailNetwork_SendReliableData__SWIG_1(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(42)] [In] [Out] byte[] jarg4, uint jarg5);

		// Token: 0x06000DE7 RID: 3559
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_IsDataReady__SWIG_0")]
		public static extern bool IRailNetwork_IsDataReady__SWIG_0(IntPtr jarg1, IntPtr jarg2, out uint jarg3, out uint jarg4);

		// Token: 0x06000DE8 RID: 3560
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_IsDataReady__SWIG_1")]
		public static extern bool IRailNetwork_IsDataReady__SWIG_1(IntPtr jarg1, IntPtr jarg2, out uint jarg3);

		// Token: 0x06000DE9 RID: 3561
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_ReadData__SWIG_0")]
		public static extern int IRailNetwork_ReadData__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(42)] [In] [Out] byte[] jarg4, uint jarg5, uint jarg6);

		// Token: 0x06000DEA RID: 3562
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_ReadData__SWIG_1")]
		public static extern int IRailNetwork_ReadData__SWIG_1(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(42)] [In] [Out] byte[] jarg4, uint jarg5);

		// Token: 0x06000DEB RID: 3563
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_BlockMessageType")]
		public static extern int IRailNetwork_BlockMessageType(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000DEC RID: 3564
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_UnblockMessageType")]
		public static extern int IRailNetwork_UnblockMessageType(IntPtr jarg1, IntPtr jarg2, uint jarg3);

		// Token: 0x06000DED RID: 3565
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_CloseSession")]
		public static extern int IRailNetwork_CloseSession(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);

		// Token: 0x06000DEE RID: 3566
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_ResolveHostname")]
		public static extern int IRailNetwork_ResolveHostname(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000DEF RID: 3567
		[DllImport("rail_api", EntryPoint = "CSharp_IRailNetwork_GetSessionState")]
		public static extern int IRailNetwork_GetSessionState(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);

		// Token: 0x06000DF0 RID: 3568
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailNetwork")]
		public static extern void delete_IRailNetwork(IntPtr jarg1);

		// Token: 0x06000DF1 RID: 3569
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AlreadyLoggedIn")]
		public static extern bool IRailPlayer_AlreadyLoggedIn(IntPtr jarg1);

		// Token: 0x06000DF2 RID: 3570
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetRailID")]
		public static extern IntPtr IRailPlayer_GetRailID(IntPtr jarg1);

		// Token: 0x06000DF3 RID: 3571
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetPlayerDataPath")]
		public static extern int IRailPlayer_GetPlayerDataPath(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000DF4 RID: 3572
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AsyncAcquireSessionTicket")]
		public static extern int IRailPlayer_AsyncAcquireSessionTicket(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DF5 RID: 3573
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AsyncStartSessionWithPlayer")]
		public static extern int IRailPlayer_AsyncStartSessionWithPlayer(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000DF6 RID: 3574
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_TerminateSessionOfPlayer")]
		public static extern void IRailPlayer_TerminateSessionOfPlayer(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000DF7 RID: 3575
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AbandonSessionTicket")]
		public static extern void IRailPlayer_AbandonSessionTicket(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000DF8 RID: 3576
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetPlayerName")]
		public static extern int IRailPlayer_GetPlayerName(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000DF9 RID: 3577
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetPlayerOwnershipType")]
		public static extern int IRailPlayer_GetPlayerOwnershipType(IntPtr jarg1);

		// Token: 0x06000DFA RID: 3578
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AsyncGetGamePurchaseKey")]
		public static extern int IRailPlayer_AsyncGetGamePurchaseKey(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DFB RID: 3579
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_IsGameRevenueLimited")]
		public static extern bool IRailPlayer_IsGameRevenueLimited(IntPtr jarg1);

		// Token: 0x06000DFC RID: 3580
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetRateOfGameRevenue")]
		public static extern float IRailPlayer_GetRateOfGameRevenue(IntPtr jarg1);

		// Token: 0x06000DFD RID: 3581
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AsyncQueryPlayerBannedStatus")]
		public static extern int IRailPlayer_AsyncQueryPlayerBannedStatus(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000DFE RID: 3582
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_AsyncGetAuthenticateURL")]
		public static extern int IRailPlayer_AsyncGetAuthenticateURL(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000DFF RID: 3583
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayer_GetPlayerMetadata")]
		public static extern int IRailPlayer_GetPlayerMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000E00 RID: 3584
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailPlayer")]
		public static extern void delete_IRailPlayer(IntPtr jarg1);

		// Token: 0x06000E01 RID: 3585
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneHelper_AsyncGetZoneList")]
		public static extern int IRailZoneHelper_AsyncGetZoneList(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E02 RID: 3586
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneHelper_AsyncGetRoomListInZone")]
		public static extern int IRailZoneHelper_AsyncGetRoomListInZone(IntPtr jarg1, ulong jarg2, uint jarg3, uint jarg4, IntPtr jarg5, IntPtr jarg6, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg7);

		// Token: 0x06000E03 RID: 3587
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailZoneHelper")]
		public static extern void delete_IRailZoneHelper(IntPtr jarg1);

		// Token: 0x06000E04 RID: 3588
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_set_current_zone_id")]
		public static extern void IRailRoomHelper_set_current_zone_id(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000E05 RID: 3589
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_get_current_zone_id")]
		public static extern ulong IRailRoomHelper_get_current_zone_id(IntPtr jarg1);

		// Token: 0x06000E06 RID: 3590
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_CreateRoom")]
		public static extern IntPtr IRailRoomHelper_CreateRoom(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, out RailResult jarg4);

		// Token: 0x06000E07 RID: 3591
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_AsyncCreateRoom")]
		public static extern IntPtr IRailRoomHelper_AsyncCreateRoom(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E08 RID: 3592
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_OpenRoom")]
		public static extern IntPtr IRailRoomHelper_OpenRoom(IntPtr jarg1, ulong jarg2, ulong jarg3, out RailResult jarg4);

		// Token: 0x06000E09 RID: 3593
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoomHelper_AsyncGetUserRoomList")]
		public static extern int IRailRoomHelper_AsyncGetUserRoomList(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E0A RID: 3594
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailRoomHelper")]
		public static extern void delete_IRailRoomHelper(IntPtr jarg1);

		// Token: 0x06000E0B RID: 3595
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomId")]
		public static extern ulong IRailRoom_GetRoomId(IntPtr jarg1);

		// Token: 0x06000E0C RID: 3596
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomName")]
		public static extern int IRailRoom_GetRoomName(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E0D RID: 3597
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetZoneId")]
		public static extern ulong IRailRoom_GetZoneId(IntPtr jarg1);

		// Token: 0x06000E0E RID: 3598
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetOwnerId")]
		public static extern IntPtr IRailRoom_GetOwnerId(IntPtr jarg1);

		// Token: 0x06000E0F RID: 3599
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetHasPassword")]
		public static extern int IRailRoom_GetHasPassword(IntPtr jarg1, out bool jarg2);

		// Token: 0x06000E10 RID: 3600
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomType")]
		public static extern int IRailRoom_GetRoomType(IntPtr jarg1);

		// Token: 0x06000E11 RID: 3601
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetNewOwner")]
		public static extern bool IRailRoom_SetNewOwner(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E12 RID: 3602
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncGetRoomMembers")]
		public static extern int IRailRoom_AsyncGetRoomMembers(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E13 RID: 3603
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_Leave")]
		public static extern void IRailRoom_Leave(IntPtr jarg1);

		// Token: 0x06000E14 RID: 3604
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncJoinRoom")]
		public static extern int IRailRoom_AsyncJoinRoom(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E15 RID: 3605
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncGetAllRoomData")]
		public static extern int IRailRoom_AsyncGetAllRoomData(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E16 RID: 3606
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncKickOffMember")]
		public static extern int IRailRoom_AsyncKickOffMember(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E17 RID: 3607
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomMetadata")]
		public static extern bool IRailRoom_GetRoomMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000E18 RID: 3608
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetRoomMetadata")]
		public static extern bool IRailRoom_SetRoomMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E19 RID: 3609
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncSetRoomMetadata")]
		public static extern int IRailRoom_AsyncSetRoomMetadata(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E1A RID: 3610
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncGetRoomMetadata")]
		public static extern int IRailRoom_AsyncGetRoomMetadata(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E1B RID: 3611
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncClearRoomMetadata")]
		public static extern int IRailRoom_AsyncClearRoomMetadata(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E1C RID: 3612
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetMemberMetadata")]
		public static extern bool IRailRoom_GetMemberMetadata(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, IntPtr jarg4);

		// Token: 0x06000E1D RID: 3613
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetMemberMetadata")]
		public static extern bool IRailRoom_SetMemberMetadata(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E1E RID: 3614
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncGetMemberMetadata")]
		public static extern int IRailRoom_AsyncGetMemberMetadata(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E1F RID: 3615
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_AsyncSetMemberMetadata")]
		public static extern int IRailRoom_AsyncSetMemberMetadata(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E20 RID: 3616
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SendDataToMember__SWIG_0")]
		public static extern int IRailRoom_SendDataToMember__SWIG_0(IntPtr jarg1, IntPtr jarg2, [MarshalAs(42)] [In] [Out] byte[] jarg3, uint jarg4, uint jarg5);

		// Token: 0x06000E21 RID: 3617
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SendDataToMember__SWIG_1")]
		public static extern int IRailRoom_SendDataToMember__SWIG_1(IntPtr jarg1, IntPtr jarg2, [MarshalAs(42)] [In] [Out] byte[] jarg3, uint jarg4);

		// Token: 0x06000E22 RID: 3618
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetNumOfMembers")]
		public static extern uint IRailRoom_GetNumOfMembers(IntPtr jarg1);

		// Token: 0x06000E23 RID: 3619
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetMemberByIndex")]
		public static extern IntPtr IRailRoom_GetMemberByIndex(IntPtr jarg1, uint jarg2);

		// Token: 0x06000E24 RID: 3620
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetMemberNameByIndex")]
		public static extern int IRailRoom_GetMemberNameByIndex(IntPtr jarg1, uint jarg2, IntPtr jarg3);

		// Token: 0x06000E25 RID: 3621
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetMaxMembers")]
		public static extern uint IRailRoom_GetMaxMembers(IntPtr jarg1);

		// Token: 0x06000E26 RID: 3622
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetGameServerID")]
		public static extern bool IRailRoom_SetGameServerID(IntPtr jarg1, ulong jarg2);

		// Token: 0x06000E27 RID: 3623
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetGameServerID")]
		public static extern bool IRailRoom_GetGameServerID(IntPtr jarg1, out ulong jarg2);

		// Token: 0x06000E28 RID: 3624
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SetRoomJoinable")]
		public static extern bool IRailRoom_SetRoomJoinable(IntPtr jarg1, bool jarg2);

		// Token: 0x06000E29 RID: 3625
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetRoomJoinable")]
		public static extern bool IRailRoom_GetRoomJoinable(IntPtr jarg1);

		// Token: 0x06000E2A RID: 3626
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_GetFriendsInRoom")]
		public static extern int IRailRoom_GetFriendsInRoom(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E2B RID: 3627
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_IsUserInRoom")]
		public static extern bool IRailRoom_IsUserInRoom(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E2C RID: 3628
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_EnableTeamVoice")]
		public static extern int IRailRoom_EnableTeamVoice(IntPtr jarg1, bool jarg2);

		// Token: 0x06000E2D RID: 3629
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailRoom")]
		public static extern void delete_IRailRoom(IntPtr jarg1);

		// Token: 0x06000E2E RID: 3630
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_CreateScreenshotWithRawData")]
		public static extern IntPtr IRailScreenshotHelper_CreateScreenshotWithRawData(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3, uint jarg4, uint jarg5);

		// Token: 0x06000E2F RID: 3631
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_CreateScreenshotWithLocalImage")]
		public static extern IntPtr IRailScreenshotHelper_CreateScreenshotWithLocalImage(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E30 RID: 3632
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_AsyncTakeScreenshot")]
		public static extern void IRailScreenshotHelper_AsyncTakeScreenshot(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E31 RID: 3633
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_HookScreenshotHotKey")]
		public static extern void IRailScreenshotHelper_HookScreenshotHotKey(IntPtr jarg1, bool jarg2);

		// Token: 0x06000E32 RID: 3634
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshotHelper_IsScreenshotHotKeyHooked")]
		public static extern bool IRailScreenshotHelper_IsScreenshotHotKeyHooked(IntPtr jarg1);

		// Token: 0x06000E33 RID: 3635
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailScreenshotHelper")]
		public static extern void delete_IRailScreenshotHelper(IntPtr jarg1);

		// Token: 0x06000E34 RID: 3636
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_SetLocation")]
		public static extern bool IRailScreenshot_SetLocation(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E35 RID: 3637
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_SetUsers")]
		public static extern bool IRailScreenshot_SetUsers(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E36 RID: 3638
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_AssociatePublishedFiles")]
		public static extern bool IRailScreenshot_AssociatePublishedFiles(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E37 RID: 3639
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_AsyncPublishScreenshot")]
		public static extern int IRailScreenshot_AsyncPublishScreenshot(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E38 RID: 3640
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailScreenshot")]
		public static extern void delete_IRailScreenshot(IntPtr jarg1);

		// Token: 0x06000E39 RID: 3641
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStatisticHelper_CreatePlayerStats")]
		public static extern IntPtr IRailStatisticHelper_CreatePlayerStats(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E3A RID: 3642
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStatisticHelper_GetGlobalStats")]
		public static extern IntPtr IRailStatisticHelper_GetGlobalStats(IntPtr jarg1);

		// Token: 0x06000E3B RID: 3643
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStatisticHelper_AsyncGetNumberOfPlayer")]
		public static extern int IRailStatisticHelper_AsyncGetNumberOfPlayer(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E3C RID: 3644
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailStatisticHelper")]
		public static extern void delete_IRailStatisticHelper(IntPtr jarg1);

		// Token: 0x06000E3D RID: 3645
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_GetRailID")]
		public static extern IntPtr IRailPlayerStats_GetRailID(IntPtr jarg1);

		// Token: 0x06000E3E RID: 3646
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_AsyncRequestStats")]
		public static extern int IRailPlayerStats_AsyncRequestStats(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E3F RID: 3647
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_GetStatValue__SWIG_0")]
		public static extern int IRailPlayerStats_GetStatValue__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out int jarg3);

		// Token: 0x06000E40 RID: 3648
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_GetStatValue__SWIG_1")]
		public static extern int IRailPlayerStats_GetStatValue__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out double jarg3);

		// Token: 0x06000E41 RID: 3649
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_SetStatValue__SWIG_0")]
		public static extern int IRailPlayerStats_SetStatValue__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, int jarg3);

		// Token: 0x06000E42 RID: 3650
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_SetStatValue__SWIG_1")]
		public static extern int IRailPlayerStats_SetStatValue__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, double jarg3);

		// Token: 0x06000E43 RID: 3651
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_UpdateAverageStatValue")]
		public static extern int IRailPlayerStats_UpdateAverageStatValue(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, double jarg3);

		// Token: 0x06000E44 RID: 3652
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_AsyncStoreStats")]
		public static extern int IRailPlayerStats_AsyncStoreStats(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E45 RID: 3653
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_ResetAllStats")]
		public static extern int IRailPlayerStats_ResetAllStats(IntPtr jarg1);

		// Token: 0x06000E46 RID: 3654
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailPlayerStats")]
		public static extern void delete_IRailPlayerStats(IntPtr jarg1);

		// Token: 0x06000E47 RID: 3655
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_AsyncRequestGlobalStats")]
		public static extern int IRailGlobalStats_AsyncRequestGlobalStats(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E48 RID: 3656
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_GetGlobalStatValue__SWIG_0")]
		public static extern int IRailGlobalStats_GetGlobalStatValue__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out long jarg3);

		// Token: 0x06000E49 RID: 3657
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_GetGlobalStatValue__SWIG_1")]
		public static extern int IRailGlobalStats_GetGlobalStatValue__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out double jarg3);

		// Token: 0x06000E4A RID: 3658
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_GetGlobalStatValueHistory__SWIG_0")]
		public static extern int IRailGlobalStats_GetGlobalStatValueHistory__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(42)] [Out] long[] jarg3, uint jarg4, out int jarg5);

		// Token: 0x06000E4B RID: 3659
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_GetGlobalStatValueHistory__SWIG_1")]
		public static extern int IRailGlobalStats_GetGlobalStatValueHistory__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(42)] [Out] double[] jarg3, uint jarg4, out int jarg5);

		// Token: 0x06000E4C RID: 3660
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGlobalStats")]
		public static extern void delete_IRailGlobalStats(IntPtr jarg1);

		// Token: 0x06000E4D RID: 3661
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerStatsReceived__SWIG_0")]
		public static extern IntPtr new_PlayerStatsReceived__SWIG_0();

		// Token: 0x06000E4E RID: 3662
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerStatsReceived__SWIG_1")]
		public static extern IntPtr new_PlayerStatsReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000E4F RID: 3663
		[DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerStatsReceived")]
		public static extern void delete_PlayerStatsReceived(IntPtr jarg1);

		// Token: 0x06000E50 RID: 3664
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerStatsStored__SWIG_0")]
		public static extern IntPtr new_PlayerStatsStored__SWIG_0();

		// Token: 0x06000E51 RID: 3665
		[DllImport("rail_api", EntryPoint = "CSharp_new_PlayerStatsStored__SWIG_1")]
		public static extern IntPtr new_PlayerStatsStored__SWIG_1(IntPtr jarg1);

		// Token: 0x06000E52 RID: 3666
		[DllImport("rail_api", EntryPoint = "CSharp_delete_PlayerStatsStored")]
		public static extern void delete_PlayerStatsStored(IntPtr jarg1);

		// Token: 0x06000E53 RID: 3667
		[DllImport("rail_api", EntryPoint = "CSharp_new_NumberOfPlayerReceived__SWIG_0")]
		public static extern IntPtr new_NumberOfPlayerReceived__SWIG_0();

		// Token: 0x06000E54 RID: 3668
		[DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_online_number_set")]
		public static extern void NumberOfPlayerReceived_online_number_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000E55 RID: 3669
		[DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_online_number_get")]
		public static extern int NumberOfPlayerReceived_online_number_get(IntPtr jarg1);

		// Token: 0x06000E56 RID: 3670
		[DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_offline_number_set")]
		public static extern void NumberOfPlayerReceived_offline_number_set(IntPtr jarg1, int jarg2);

		// Token: 0x06000E57 RID: 3671
		[DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_offline_number_get")]
		public static extern int NumberOfPlayerReceived_offline_number_get(IntPtr jarg1);

		// Token: 0x06000E58 RID: 3672
		[DllImport("rail_api", EntryPoint = "CSharp_new_NumberOfPlayerReceived__SWIG_1")]
		public static extern IntPtr new_NumberOfPlayerReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000E59 RID: 3673
		[DllImport("rail_api", EntryPoint = "CSharp_delete_NumberOfPlayerReceived")]
		public static extern void delete_NumberOfPlayerReceived(IntPtr jarg1);

		// Token: 0x06000E5A RID: 3674
		[DllImport("rail_api", EntryPoint = "CSharp_new_GlobalStatsRequestReceived__SWIG_0")]
		public static extern IntPtr new_GlobalStatsRequestReceived__SWIG_0();

		// Token: 0x06000E5B RID: 3675
		[DllImport("rail_api", EntryPoint = "CSharp_new_GlobalStatsRequestReceived__SWIG_1")]
		public static extern IntPtr new_GlobalStatsRequestReceived__SWIG_1(IntPtr jarg1);

		// Token: 0x06000E5C RID: 3676
		[DllImport("rail_api", EntryPoint = "CSharp_delete_GlobalStatsRequestReceived")]
		public static extern void delete_GlobalStatsRequestReceived(IntPtr jarg1);

		// Token: 0x06000E5D RID: 3677
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailStorageHelper")]
		public static extern void delete_IRailStorageHelper(IntPtr jarg1);

		// Token: 0x06000E5E RID: 3678
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_OpenFile__SWIG_0")]
		public static extern IntPtr IRailStorageHelper_OpenFile__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out RailResult jarg3);

		// Token: 0x06000E5F RID: 3679
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_OpenFile__SWIG_1")]
		public static extern IntPtr IRailStorageHelper_OpenFile__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E60 RID: 3680
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_CreateFile__SWIG_0")]
		public static extern IntPtr IRailStorageHelper_CreateFile__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out RailResult jarg3);

		// Token: 0x06000E61 RID: 3681
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_CreateFile__SWIG_1")]
		public static extern IntPtr IRailStorageHelper_CreateFile__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E62 RID: 3682
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_IsFileExist")]
		public static extern bool IRailStorageHelper_IsFileExist(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E63 RID: 3683
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_ListFiles")]
		public static extern bool IRailStorageHelper_ListFiles(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E64 RID: 3684
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_RemoveFile")]
		public static extern int IRailStorageHelper_RemoveFile(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E65 RID: 3685
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_IsFileSyncedToCloud")]
		public static extern bool IRailStorageHelper_IsFileSyncedToCloud(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E66 RID: 3686
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_GetFileTimestamp")]
		public static extern int IRailStorageHelper_GetFileTimestamp(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, out ulong jarg3);

		// Token: 0x06000E67 RID: 3687
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_GetFileCount")]
		public static extern uint IRailStorageHelper_GetFileCount(IntPtr jarg1);

		// Token: 0x06000E68 RID: 3688
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_GetFileNameAndSize")]
		public static extern int IRailStorageHelper_GetFileNameAndSize(IntPtr jarg1, uint jarg2, IntPtr jarg3, out ulong jarg4);

		// Token: 0x06000E69 RID: 3689
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncQueryQuota")]
		public static extern int IRailStorageHelper_AsyncQueryQuota(IntPtr jarg1);

		// Token: 0x06000E6A RID: 3690
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_SetSyncFileOption")]
		public static extern int IRailStorageHelper_SetSyncFileOption(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000E6B RID: 3691
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_IsCloudStorageEnabledForApp")]
		public static extern bool IRailStorageHelper_IsCloudStorageEnabledForApp(IntPtr jarg1);

		// Token: 0x06000E6C RID: 3692
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_IsCloudStorageEnabledForPlayer")]
		public static extern bool IRailStorageHelper_IsCloudStorageEnabledForPlayer(IntPtr jarg1);

		// Token: 0x06000E6D RID: 3693
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncPublishFileToUserSpace")]
		public static extern int IRailStorageHelper_AsyncPublishFileToUserSpace(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E6E RID: 3694
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_OpenStreamFile__SWIG_0")]
		public static extern IntPtr IRailStorageHelper_OpenStreamFile__SWIG_0(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3, out RailResult jarg4);

		// Token: 0x06000E6F RID: 3695
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_OpenStreamFile__SWIG_1")]
		public static extern IntPtr IRailStorageHelper_OpenStreamFile__SWIG_1(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000E70 RID: 3696
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncListStreamFiles")]
		public static extern int IRailStorageHelper_AsyncListStreamFiles(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E71 RID: 3697
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncRenameStreamFile")]
		public static extern int IRailStorageHelper_AsyncRenameStreamFile(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E72 RID: 3698
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_AsyncDeleteStreamFile")]
		public static extern int IRailStorageHelper_AsyncDeleteStreamFile(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E73 RID: 3699
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_GetRailFileEnabledOS")]
		public static extern uint IRailStorageHelper_GetRailFileEnabledOS(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E74 RID: 3700
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStorageHelper_SetRailFileEnabledOS")]
		public static extern int IRailStorageHelper_SetRailFileEnabledOS(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, int jarg3);

		// Token: 0x06000E75 RID: 3701
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailFile")]
		public static extern void delete_IRailFile(IntPtr jarg1);

		// Token: 0x06000E76 RID: 3702
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_GetFilename")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailFile_GetFilename(IntPtr jarg1);

		// Token: 0x06000E77 RID: 3703
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Read__SWIG_0")]
		public static extern uint IRailFile_Read__SWIG_0(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3, out RailResult jarg4);

		// Token: 0x06000E78 RID: 3704
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Read__SWIG_1")]
		public static extern uint IRailFile_Read__SWIG_1(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3);

		// Token: 0x06000E79 RID: 3705
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Write__SWIG_0")]
		public static extern uint IRailFile_Write__SWIG_0(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3, out RailResult jarg4);

		// Token: 0x06000E7A RID: 3706
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Write__SWIG_1")]
		public static extern uint IRailFile_Write__SWIG_1(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3);

		// Token: 0x06000E7B RID: 3707
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_AsyncRead")]
		public static extern int IRailFile_AsyncRead(IntPtr jarg1, uint jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E7C RID: 3708
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_AsyncWrite")]
		public static extern int IRailFile_AsyncWrite(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E7D RID: 3709
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_GetSize")]
		public static extern uint IRailFile_GetSize(IntPtr jarg1);

		// Token: 0x06000E7E RID: 3710
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_Close")]
		public static extern void IRailFile_Close(IntPtr jarg1);

		// Token: 0x06000E7F RID: 3711
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailStreamFile")]
		public static extern void delete_IRailStreamFile(IntPtr jarg1);

		// Token: 0x06000E80 RID: 3712
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_GetFilename")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailStreamFile_GetFilename(IntPtr jarg1);

		// Token: 0x06000E81 RID: 3713
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_AsyncRead")]
		public static extern int IRailStreamFile_AsyncRead(IntPtr jarg1, int jarg2, uint jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E82 RID: 3714
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_AsyncWrite")]
		public static extern int IRailStreamFile_AsyncWrite(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E83 RID: 3715
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_GetSize")]
		public static extern ulong IRailStreamFile_GetSize(IntPtr jarg1);

		// Token: 0x06000E84 RID: 3716
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_Close")]
		public static extern int IRailStreamFile_Close(IntPtr jarg1);

		// Token: 0x06000E85 RID: 3717
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_Cancel")]
		public static extern void IRailStreamFile_Cancel(IntPtr jarg1);

		// Token: 0x06000E86 RID: 3718
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncGetUsersInfo")]
		public static extern int IRailUsersHelper_AsyncGetUsersInfo(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E87 RID: 3719
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncInviteUsers")]
		public static extern int IRailUsersHelper_AsyncInviteUsers(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3, IntPtr jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5);

		// Token: 0x06000E88 RID: 3720
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncGetInviteDetail")]
		public static extern int IRailUsersHelper_AsyncGetInviteDetail(IntPtr jarg1, IntPtr jarg2, int jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E89 RID: 3721
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncCancelInvite")]
		public static extern int IRailUsersHelper_AsyncCancelInvite(IntPtr jarg1, int jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E8A RID: 3722
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncCancelAllInvites")]
		public static extern int IRailUsersHelper_AsyncCancelAllInvites(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000E8B RID: 3723
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncGetUserLimits")]
		public static extern int IRailUsersHelper_AsyncGetUserLimits(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E8C RID: 3724
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncShowChatWindowWithFriend")]
		public static extern int IRailUsersHelper_AsyncShowChatWindowWithFriend(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E8D RID: 3725
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUsersHelper_AsyncShowUserHomepageWindow")]
		public static extern int IRailUsersHelper_AsyncShowUserHomepageWindow(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E8E RID: 3726
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailUsersHelper")]
		public static extern void delete_IRailUsersHelper(IntPtr jarg1);

		// Token: 0x06000E8F RID: 3727
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_0")]
		public static extern int IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_0(IntPtr jarg1, uint jarg2, uint jarg3, int jarg4, IntPtr jarg5, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg6);

		// Token: 0x06000E90 RID: 3728
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_1")]
		public static extern int IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_1(IntPtr jarg1, uint jarg2, uint jarg3, int jarg4, IntPtr jarg5);

		// Token: 0x06000E91 RID: 3729
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_2")]
		public static extern int IRailUserSpaceHelper_AsyncGetMySubscribedWorks__SWIG_2(IntPtr jarg1, uint jarg2, uint jarg3, int jarg4);

		// Token: 0x06000E92 RID: 3730
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_0")]
		public static extern int IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_0(IntPtr jarg1, uint jarg2, uint jarg3, int jarg4, IntPtr jarg5, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg6);

		// Token: 0x06000E93 RID: 3731
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_1")]
		public static extern int IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_1(IntPtr jarg1, uint jarg2, uint jarg3, int jarg4, IntPtr jarg5);

		// Token: 0x06000E94 RID: 3732
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_2")]
		public static extern int IRailUserSpaceHelper_AsyncGetMyFavoritesWorks__SWIG_2(IntPtr jarg1, uint jarg2, uint jarg3, int jarg4);

		// Token: 0x06000E95 RID: 3733
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_0")]
		public static extern int IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_0(IntPtr jarg1, IntPtr jarg2, uint jarg3, uint jarg4, int jarg5, IntPtr jarg6, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg7);

		// Token: 0x06000E96 RID: 3734
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_1")]
		public static extern int IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_1(IntPtr jarg1, IntPtr jarg2, uint jarg3, uint jarg4, int jarg5, IntPtr jarg6);

		// Token: 0x06000E97 RID: 3735
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_2")]
		public static extern int IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_2(IntPtr jarg1, IntPtr jarg2, uint jarg3, uint jarg4, int jarg5);

		// Token: 0x06000E98 RID: 3736
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_3")]
		public static extern int IRailUserSpaceHelper_AsyncQuerySpaceWorks__SWIG_3(IntPtr jarg1, IntPtr jarg2, uint jarg3, uint jarg4);

		// Token: 0x06000E99 RID: 3737
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncSubscribeSpaceWorks")]
		public static extern int IRailUserSpaceHelper_AsyncSubscribeSpaceWorks(IntPtr jarg1, IntPtr jarg2, bool jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000E9A RID: 3738
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_OpenSpaceWork")]
		public static extern IntPtr IRailUserSpaceHelper_OpenSpaceWork(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000E9B RID: 3739
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_CreateSpaceWork")]
		public static extern IntPtr IRailUserSpaceHelper_CreateSpaceWork(IntPtr jarg1, int jarg2);

		// Token: 0x06000E9C RID: 3740
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_GetMySubscribedWorks")]
		public static extern int IRailUserSpaceHelper_GetMySubscribedWorks(IntPtr jarg1, uint jarg2, uint jarg3, int jarg4, IntPtr jarg5);

		// Token: 0x06000E9D RID: 3741
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_GetMySubscribedWorksCount")]
		public static extern uint IRailUserSpaceHelper_GetMySubscribedWorksCount(IntPtr jarg1, int jarg2, out RailResult jarg3);

		// Token: 0x06000E9E RID: 3742
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncRemoveSpaceWork")]
		public static extern int IRailUserSpaceHelper_AsyncRemoveSpaceWork(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000E9F RID: 3743
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncModifyFavoritesWorks")]
		public static extern int IRailUserSpaceHelper_AsyncModifyFavoritesWorks(IntPtr jarg1, IntPtr jarg2, int jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000EA0 RID: 3744
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncVoteSpaceWork")]
		public static extern int IRailUserSpaceHelper_AsyncVoteSpaceWork(IntPtr jarg1, IntPtr jarg2, int jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4);

		// Token: 0x06000EA1 RID: 3745
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUserSpaceHelper_AsyncSearchSpaceWork")]
		public static extern int IRailUserSpaceHelper_AsyncSearchSpaceWork(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4, uint jarg5, uint jarg6, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg7);

		// Token: 0x06000EA2 RID: 3746
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailUserSpaceHelper")]
		public static extern void delete_IRailUserSpaceHelper(IntPtr jarg1);

		// Token: 0x06000EA3 RID: 3747
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_Close")]
		public static extern void IRailSpaceWork_Close(IntPtr jarg1);

		// Token: 0x06000EA4 RID: 3748
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetSpaceWorkID")]
		public static extern IntPtr IRailSpaceWork_GetSpaceWorkID(IntPtr jarg1);

		// Token: 0x06000EA5 RID: 3749
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_Editable")]
		public static extern bool IRailSpaceWork_Editable(IntPtr jarg1);

		// Token: 0x06000EA6 RID: 3750
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_StartSync")]
		public static extern int IRailSpaceWork_StartSync(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000EA7 RID: 3751
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetSyncProgress")]
		public static extern int IRailSpaceWork_GetSyncProgress(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EA8 RID: 3752
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_CancelSync")]
		public static extern int IRailSpaceWork_CancelSync(IntPtr jarg1);

		// Token: 0x06000EA9 RID: 3753
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetWorkLocalFolder")]
		public static extern int IRailSpaceWork_GetWorkLocalFolder(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EAA RID: 3754
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_AsyncUpdateMetadata")]
		public static extern int IRailSpaceWork_AsyncUpdateMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000EAB RID: 3755
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetName")]
		public static extern int IRailSpaceWork_GetName(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EAC RID: 3756
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetDescription")]
		public static extern int IRailSpaceWork_GetDescription(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EAD RID: 3757
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetUrl")]
		public static extern int IRailSpaceWork_GetUrl(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EAE RID: 3758
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetCreateTime")]
		public static extern uint IRailSpaceWork_GetCreateTime(IntPtr jarg1);

		// Token: 0x06000EAF RID: 3759
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetLastUpdateTime")]
		public static extern uint IRailSpaceWork_GetLastUpdateTime(IntPtr jarg1);

		// Token: 0x06000EB0 RID: 3760
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetWorkFileSize")]
		public static extern ulong IRailSpaceWork_GetWorkFileSize(IntPtr jarg1);

		// Token: 0x06000EB1 RID: 3761
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetTags")]
		public static extern int IRailSpaceWork_GetTags(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EB2 RID: 3762
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetPreviewImage")]
		public static extern int IRailSpaceWork_GetPreviewImage(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EB3 RID: 3763
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetVersion")]
		public static extern int IRailSpaceWork_GetVersion(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EB4 RID: 3764
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetDownloadCount")]
		public static extern ulong IRailSpaceWork_GetDownloadCount(IntPtr jarg1);

		// Token: 0x06000EB5 RID: 3765
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetSubscribedCount")]
		public static extern ulong IRailSpaceWork_GetSubscribedCount(IntPtr jarg1);

		// Token: 0x06000EB6 RID: 3766
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetShareLevel")]
		public static extern int IRailSpaceWork_GetShareLevel(IntPtr jarg1);

		// Token: 0x06000EB7 RID: 3767
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetScore")]
		public static extern ulong IRailSpaceWork_GetScore(IntPtr jarg1);

		// Token: 0x06000EB8 RID: 3768
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetMetadata")]
		public static extern int IRailSpaceWork_GetMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000EB9 RID: 3769
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetMyVote")]
		public static extern int IRailSpaceWork_GetMyVote(IntPtr jarg1);

		// Token: 0x06000EBA RID: 3770
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_IsFavorite")]
		public static extern bool IRailSpaceWork_IsFavorite(IntPtr jarg1);

		// Token: 0x06000EBB RID: 3771
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_IsSubscribed")]
		public static extern bool IRailSpaceWork_IsSubscribed(IntPtr jarg1);

		// Token: 0x06000EBC RID: 3772
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetName")]
		public static extern int IRailSpaceWork_SetName(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000EBD RID: 3773
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetDescription")]
		public static extern int IRailSpaceWork_SetDescription(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000EBE RID: 3774
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetTags")]
		public static extern int IRailSpaceWork_SetTags(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EBF RID: 3775
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetPreviewImage")]
		public static extern int IRailSpaceWork_SetPreviewImage(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000EC0 RID: 3776
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetVersion")]
		public static extern int IRailSpaceWork_SetVersion(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000EC1 RID: 3777
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetShareLevel__SWIG_0")]
		public static extern int IRailSpaceWork_SetShareLevel__SWIG_0(IntPtr jarg1, int jarg2);

		// Token: 0x06000EC2 RID: 3778
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetShareLevel__SWIG_1")]
		public static extern int IRailSpaceWork_SetShareLevel__SWIG_1(IntPtr jarg1);

		// Token: 0x06000EC3 RID: 3779
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetMetadata")]
		public static extern int IRailSpaceWork_SetMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000EC4 RID: 3780
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetContentFromFolder")]
		public static extern int IRailSpaceWork_SetContentFromFolder(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000EC5 RID: 3781
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetAllMetadata")]
		public static extern int IRailSpaceWork_GetAllMetadata(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EC6 RID: 3782
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetAdditionalPreviewUrls")]
		public static extern int IRailSpaceWork_GetAdditionalPreviewUrls(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EC7 RID: 3783
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetAssociatedSpaceWorks")]
		public static extern int IRailSpaceWork_GetAssociatedSpaceWorks(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EC8 RID: 3784
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetLanguages")]
		public static extern int IRailSpaceWork_GetLanguages(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EC9 RID: 3785
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_RemoveMetadata")]
		public static extern int IRailSpaceWork_RemoveMetadata(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000ECA RID: 3786
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetAdditionalPreviews")]
		public static extern int IRailSpaceWork_SetAdditionalPreviews(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ECB RID: 3787
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetAssociatedSpaceWorks")]
		public static extern int IRailSpaceWork_SetAssociatedSpaceWorks(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ECC RID: 3788
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetLanguages")]
		public static extern int IRailSpaceWork_SetLanguages(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ECD RID: 3789
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetPreviewUrl")]
		public static extern int IRailSpaceWork_GetPreviewUrl(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ECE RID: 3790
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetVoteDetail")]
		public static extern int IRailSpaceWork_GetVoteDetail(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ECF RID: 3791
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetUploaderIDs")]
		public static extern int IRailSpaceWork_GetUploaderIDs(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ED0 RID: 3792
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SetUpdateOptions")]
		public static extern int IRailSpaceWork_SetUpdateOptions(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ED1 RID: 3793
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetStatistic")]
		public static extern int IRailSpaceWork_GetStatistic(IntPtr jarg1, int jarg2, out ulong jarg3);

		// Token: 0x06000ED2 RID: 3794
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_RemovePreviewImage")]
		public static extern int IRailSpaceWork_RemovePreviewImage(IntPtr jarg1);

		// Token: 0x06000ED3 RID: 3795
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetState")]
		public static extern uint IRailSpaceWork_GetState(IntPtr jarg1);

		// Token: 0x06000ED4 RID: 3796
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_AddAssociatedGameIDs")]
		public static extern int IRailSpaceWork_AddAssociatedGameIDs(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ED5 RID: 3797
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_RemoveAssociatedGameIDs")]
		public static extern int IRailSpaceWork_RemoveAssociatedGameIDs(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ED6 RID: 3798
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetAssociatedGameIDs")]
		public static extern int IRailSpaceWork_GetAssociatedGameIDs(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ED7 RID: 3799
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_GetLocalVersion")]
		public static extern int IRailSpaceWork_GetLocalVersion(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000ED8 RID: 3800
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailSpaceWork")]
		public static extern void delete_IRailSpaceWork(IntPtr jarg1);

		// Token: 0x06000ED9 RID: 3801
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetTimeCountSinceGameLaunch")]
		public static extern uint IRailUtils_GetTimeCountSinceGameLaunch(IntPtr jarg1);

		// Token: 0x06000EDA RID: 3802
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetTimeCountSinceComputerLaunch")]
		public static extern uint IRailUtils_GetTimeCountSinceComputerLaunch(IntPtr jarg1);

		// Token: 0x06000EDB RID: 3803
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetTimeFromServer")]
		public static extern uint IRailUtils_GetTimeFromServer(IntPtr jarg1);

		// Token: 0x06000EDC RID: 3804
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_AsyncGetImageData")]
		public static extern int IRailUtils_AsyncGetImageData(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, uint jarg3, uint jarg4, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg5);

		// Token: 0x06000EDD RID: 3805
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetErrorString")]
		public static extern void IRailUtils_GetErrorString(IntPtr jarg1, int jarg2, IntPtr jarg3);

		// Token: 0x06000EDE RID: 3806
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_DirtyWordsFilter")]
		public static extern int IRailUtils_DirtyWordsFilter(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, bool jarg3, IntPtr jarg4);

		// Token: 0x06000EDF RID: 3807
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetRailPlatformType")]
		public static extern int IRailUtils_GetRailPlatformType(IntPtr jarg1);

		// Token: 0x06000EE0 RID: 3808
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetLaunchAppParameters")]
		public static extern int IRailUtils_GetLaunchAppParameters(IntPtr jarg1, int jarg2, IntPtr jarg3);

		// Token: 0x06000EE1 RID: 3809
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetPlatformLanguageCode")]
		public static extern int IRailUtils_GetPlatformLanguageCode(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EE2 RID: 3810
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_SetWarningMessageCallback")]
		public static extern int IRailUtils_SetWarningMessageCallback(IntPtr jarg1, RailWarningMessageCallbackFunction jarg2);

		// Token: 0x06000EE3 RID: 3811
		[DllImport("rail_api", EntryPoint = "CSharp_IRailUtils_GetCountryCodeOfCurrentLoggedInIP")]
		public static extern int IRailUtils_GetCountryCodeOfCurrentLoggedInIP(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EE4 RID: 3812
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailUtils")]
		public static extern void delete_IRailUtils(IntPtr jarg1);

		// Token: 0x06000EE5 RID: 3813
		[DllImport("rail_api", EntryPoint = "CSharp_IRailApps_IsGameInstalled")]
		public static extern bool IRailApps_IsGameInstalled(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EE6 RID: 3814
		[DllImport("rail_api", EntryPoint = "CSharp_IRailApps_AsyncQuerySubscribeWishPlayState")]
		public static extern int IRailApps_AsyncQuerySubscribeWishPlayState(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000EE7 RID: 3815
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailApps")]
		public static extern void delete_IRailApps(IntPtr jarg1);

		// Token: 0x06000EE8 RID: 3816
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailVoiceHelper")]
		public static extern void delete_IRailVoiceHelper(IntPtr jarg1);

		// Token: 0x06000EE9 RID: 3817
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_AsyncCreateVoiceChannel")]
		public static extern IntPtr IRailVoiceHelper_AsyncCreateVoiceChannel(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg4, out RailResult jarg5);

		// Token: 0x06000EEA RID: 3818
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_OpenVoiceChannel")]
		public static extern IntPtr IRailVoiceHelper_OpenVoiceChannel(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3, out RailResult jarg4);

		// Token: 0x06000EEB RID: 3819
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_GetSpeakerState")]
		public static extern int IRailVoiceHelper_GetSpeakerState(IntPtr jarg1);

		// Token: 0x06000EEC RID: 3820
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_MuteSpeaker")]
		public static extern int IRailVoiceHelper_MuteSpeaker(IntPtr jarg1);

		// Token: 0x06000EED RID: 3821
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_ResumeSpeaker")]
		public static extern int IRailVoiceHelper_ResumeSpeaker(IntPtr jarg1);

		// Token: 0x06000EEE RID: 3822
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_SetupVoiceCapture__SWIG_0")]
		public static extern int IRailVoiceHelper_SetupVoiceCapture__SWIG_0(IntPtr jarg1, IntPtr jarg2, RailCaptureVoiceCallback jarg3);

		// Token: 0x06000EEF RID: 3823
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_SetupVoiceCapture__SWIG_1")]
		public static extern int IRailVoiceHelper_SetupVoiceCapture__SWIG_1(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EF0 RID: 3824
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_StartVoiceCapturing__SWIG_0")]
		public static extern int IRailVoiceHelper_StartVoiceCapturing__SWIG_0(IntPtr jarg1, uint jarg2, bool jarg3);

		// Token: 0x06000EF1 RID: 3825
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_StartVoiceCapturing__SWIG_1")]
		public static extern int IRailVoiceHelper_StartVoiceCapturing__SWIG_1(IntPtr jarg1, uint jarg2);

		// Token: 0x06000EF2 RID: 3826
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_StartVoiceCapturing__SWIG_2")]
		public static extern int IRailVoiceHelper_StartVoiceCapturing__SWIG_2(IntPtr jarg1);

		// Token: 0x06000EF3 RID: 3827
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_StopVoiceCapturing")]
		public static extern int IRailVoiceHelper_StopVoiceCapturing(IntPtr jarg1);

		// Token: 0x06000EF4 RID: 3828
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_GetCapturedVoiceData")]
		public static extern int IRailVoiceHelper_GetCapturedVoiceData(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3, out uint jarg4);

		// Token: 0x06000EF5 RID: 3829
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_DecodeVoice")]
		public static extern int IRailVoiceHelper_DecodeVoice(IntPtr jarg1, [MarshalAs(42)] [In] [Out] byte[] jarg2, uint jarg3, [MarshalAs(42)] [In] [Out] byte[] jarg4, uint jarg5, out uint jarg6);

		// Token: 0x06000EF6 RID: 3830
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_GetVoiceCaptureSpecification")]
		public static extern int IRailVoiceHelper_GetVoiceCaptureSpecification(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000EF7 RID: 3831
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_EnableInGameVoiceSpeaking")]
		public static extern int IRailVoiceHelper_EnableInGameVoiceSpeaking(IntPtr jarg1, bool jarg2);

		// Token: 0x06000EF8 RID: 3832
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_SetPlayerNicknameInVoiceChannel")]
		public static extern int IRailVoiceHelper_SetPlayerNicknameInVoiceChannel(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000EF9 RID: 3833
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_SetPushToTalkKeyInVoiceChannel")]
		public static extern int IRailVoiceHelper_SetPushToTalkKeyInVoiceChannel(IntPtr jarg1, uint jarg2);

		// Token: 0x06000EFA RID: 3834
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_GetPushToTalkKeyInVoiceChannel")]
		public static extern uint IRailVoiceHelper_GetPushToTalkKeyInVoiceChannel(IntPtr jarg1);

		// Token: 0x06000EFB RID: 3835
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceHelper_ShowOverlayUI")]
		public static extern int IRailVoiceHelper_ShowOverlayUI(IntPtr jarg1, bool jarg2);

		// Token: 0x06000EFC RID: 3836
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailVoiceChannel")]
		public static extern void delete_IRailVoiceChannel(IntPtr jarg1);

		// Token: 0x06000EFD RID: 3837
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetVoiceChannelID")]
		public static extern IntPtr IRailVoiceChannel_GetVoiceChannelID(IntPtr jarg1);

		// Token: 0x06000EFE RID: 3838
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetVoiceChannelName")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailVoiceChannel_GetVoiceChannelName(IntPtr jarg1);

		// Token: 0x06000EFF RID: 3839
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetJoinState")]
		public static extern int IRailVoiceChannel_GetJoinState(IntPtr jarg1);

		// Token: 0x06000F00 RID: 3840
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_AsyncJoinVoiceChannel")]
		public static extern int IRailVoiceChannel_AsyncJoinVoiceChannel(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F01 RID: 3841
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_AsyncLeaveVoiceChannel")]
		public static extern int IRailVoiceChannel_AsyncLeaveVoiceChannel(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F02 RID: 3842
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetUsers")]
		public static extern int IRailVoiceChannel_GetUsers(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F03 RID: 3843
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_AsyncAddUsers")]
		public static extern int IRailVoiceChannel_AsyncAddUsers(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000F04 RID: 3844
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_AsyncRemoveUsers")]
		public static extern int IRailVoiceChannel_AsyncRemoveUsers(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000F05 RID: 3845
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_CloseChannel")]
		public static extern int IRailVoiceChannel_CloseChannel(IntPtr jarg1);

		// Token: 0x06000F06 RID: 3846
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_SetSelfSpeaking")]
		public static extern int IRailVoiceChannel_SetSelfSpeaking(IntPtr jarg1, bool jarg2);

		// Token: 0x06000F07 RID: 3847
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_IsSelfSpeaking")]
		public static extern bool IRailVoiceChannel_IsSelfSpeaking(IntPtr jarg1);

		// Token: 0x06000F08 RID: 3848
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_AsyncSetUsersSpeakingState")]
		public static extern int IRailVoiceChannel_AsyncSetUsersSpeakingState(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000F09 RID: 3849
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetUsersSpeakingState")]
		public static extern int IRailVoiceChannel_GetUsersSpeakingState(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F0A RID: 3850
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_GetSpeakingUsers")]
		public static extern int IRailVoiceChannel_GetSpeakingUsers(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);

		// Token: 0x06000F0B RID: 3851
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_IsOwner")]
		public static extern bool IRailVoiceChannel_IsOwner(IntPtr jarg1);

		// Token: 0x06000F0C RID: 3852
		[DllImport("rail_api", EntryPoint = "CSharp_IRailTextInputHelper_ShowTextInputWindow")]
		public static extern int IRailTextInputHelper_ShowTextInputWindow(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F0D RID: 3853
		[DllImport("rail_api", EntryPoint = "CSharp_IRailTextInputHelper_GetTextInputContent")]
		public static extern void IRailTextInputHelper_GetTextInputContent(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F0E RID: 3854
		[DllImport("rail_api", EntryPoint = "CSharp_IRailTextInputHelper_HideTextInputWindow")]
		public static extern int IRailTextInputHelper_HideTextInputWindow(IntPtr jarg1);

		// Token: 0x06000F0F RID: 3855
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailTextInputHelper")]
		public static extern void delete_IRailTextInputHelper(IntPtr jarg1);

		// Token: 0x06000F10 RID: 3856
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetGameID")]
		public static extern IntPtr IRailGame_GetGameID(IntPtr jarg1);

		// Token: 0x06000F11 RID: 3857
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_ReportGameContentDamaged")]
		public static extern int IRailGame_ReportGameContentDamaged(IntPtr jarg1, int jarg2);

		// Token: 0x06000F12 RID: 3858
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetGameInstallPath")]
		public static extern int IRailGame_GetGameInstallPath(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F13 RID: 3859
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_AsyncQuerySubscribeWishPlayState")]
		public static extern int IRailGame_AsyncQuerySubscribeWishPlayState(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F14 RID: 3860
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetPlayerSelectedLanguageCode")]
		public static extern int IRailGame_GetPlayerSelectedLanguageCode(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F15 RID: 3861
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetGameSupportedLanguageCodes")]
		public static extern int IRailGame_GetGameSupportedLanguageCodes(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F16 RID: 3862
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_SetGameState")]
		public static extern int IRailGame_SetGameState(IntPtr jarg1, int jarg2);

		// Token: 0x06000F17 RID: 3863
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetGameState")]
		public static extern int IRailGame_GetGameState(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F18 RID: 3864
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_RegisterGameDefineGamePlayingState")]
		public static extern int IRailGame_RegisterGameDefineGamePlayingState(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F19 RID: 3865
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_SetGameDefineGamePlayingState")]
		public static extern int IRailGame_SetGameDefineGamePlayingState(IntPtr jarg1, uint jarg2);

		// Token: 0x06000F1A RID: 3866
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetGameDefineGamePlayingState")]
		public static extern int IRailGame_GetGameDefineGamePlayingState(IntPtr jarg1, out uint jarg2);

		// Token: 0x06000F1B RID: 3867
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetBranchBuildNumber")]
		public static extern uint IRailGame_GetBranchBuildNumber(IntPtr jarg1);

		// Token: 0x06000F1C RID: 3868
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetCurrentBranchInfo")]
		public static extern int IRailGame_GetCurrentBranchInfo(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F1D RID: 3869
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_StartGameTimeCounting")]
		public static extern int IRailGame_StartGameTimeCounting(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F1E RID: 3870
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_EndGameTimeCounting")]
		public static extern int IRailGame_EndGameTimeCounting(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F1F RID: 3871
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetGamePurchasePlayerRailID")]
		public static extern IntPtr IRailGame_GetGamePurchasePlayerRailID(IntPtr jarg1);

		// Token: 0x06000F20 RID: 3872
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetGameEarliestPurchaseTime")]
		public static extern uint IRailGame_GetGameEarliestPurchaseTime(IntPtr jarg1);

		// Token: 0x06000F21 RID: 3873
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetTimeCountSinceGameActivated")]
		public static extern uint IRailGame_GetTimeCountSinceGameActivated(IntPtr jarg1);

		// Token: 0x06000F22 RID: 3874
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGame_GetTimeCountSinceLastMouseMoved")]
		public static extern uint IRailGame_GetTimeCountSinceLastMouseMoved(IntPtr jarg1);

		// Token: 0x06000F23 RID: 3875
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailGame")]
		public static extern void delete_IRailGame(IntPtr jarg1);

		// Token: 0x06000F24 RID: 3876
		[DllImport("rail_api", EntryPoint = "CSharp_IRailIMEHelper_EnableIMEHelperTextInputWindow")]
		public static extern int IRailIMEHelper_EnableIMEHelperTextInputWindow(IntPtr jarg1, bool jarg2, IntPtr jarg3);

		// Token: 0x06000F25 RID: 3877
		[DllImport("rail_api", EntryPoint = "CSharp_IRailIMEHelper_UpdateIMEHelperTextInputWindowPosition")]
		public static extern int IRailIMEHelper_UpdateIMEHelperTextInputWindowPosition(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F26 RID: 3878
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailIMEHelper")]
		public static extern void delete_IRailIMEHelper(IntPtr jarg1);

		// Token: 0x06000F27 RID: 3879
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailSmallObjectServiceHelper")]
		public static extern void delete_IRailSmallObjectServiceHelper(IntPtr jarg1);

		// Token: 0x06000F28 RID: 3880
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSmallObjectServiceHelper_AsyncDownloadObjects")]
		public static extern int IRailSmallObjectServiceHelper_AsyncDownloadObjects(IntPtr jarg1, IntPtr jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000F29 RID: 3881
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSmallObjectServiceHelper_GetObjectContent")]
		public static extern int IRailSmallObjectServiceHelper_GetObjectContent(IntPtr jarg1, uint jarg2, IntPtr jarg3);

		// Token: 0x06000F2A RID: 3882
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSmallObjectServiceHelper_AsyncQueryObjectState")]
		public static extern int IRailSmallObjectServiceHelper_AsyncQueryObjectState(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F2B RID: 3883
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSystemHelper_SetTerminationTimeoutOwnershipExpired")]
		public static extern int IRailSystemHelper_SetTerminationTimeoutOwnershipExpired(IntPtr jarg1, int jarg2);

		// Token: 0x06000F2C RID: 3884
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSystemHelper_GetPlatformSystemState")]
		public static extern int IRailSystemHelper_GetPlatformSystemState(IntPtr jarg1);

		// Token: 0x06000F2D RID: 3885
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailSystemHelper")]
		public static extern void delete_IRailSystemHelper(IntPtr jarg1);

		// Token: 0x06000F2E RID: 3886
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSessionHelper_CreateHttpSession")]
		public static extern IntPtr IRailHttpSessionHelper_CreateHttpSession(IntPtr jarg1);

		// Token: 0x06000F2F RID: 3887
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSessionHelper_CreateHttpResponse")]
		public static extern IntPtr IRailHttpSessionHelper_CreateHttpResponse(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F30 RID: 3888
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailHttpSessionHelper")]
		public static extern void delete_IRailHttpSessionHelper(IntPtr jarg1);

		// Token: 0x06000F31 RID: 3889
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSession_SetRequestMethod")]
		public static extern int IRailHttpSession_SetRequestMethod(IntPtr jarg1, int jarg2);

		// Token: 0x06000F32 RID: 3890
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSession_SetParameters")]
		public static extern int IRailHttpSession_SetParameters(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F33 RID: 3891
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSession_SetPostBodyContent")]
		public static extern int IRailHttpSession_SetPostBodyContent(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F34 RID: 3892
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSession_SetRequestTimeOut")]
		public static extern int IRailHttpSession_SetRequestTimeOut(IntPtr jarg1, uint jarg2);

		// Token: 0x06000F35 RID: 3893
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSession_SetRequestHeaders")]
		public static extern int IRailHttpSession_SetRequestHeaders(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F36 RID: 3894
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSession_AsyncSendRequest")]
		public static extern int IRailHttpSession_AsyncSendRequest(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg3);

		// Token: 0x06000F37 RID: 3895
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailHttpSession")]
		public static extern void delete_IRailHttpSession(IntPtr jarg1);

		// Token: 0x06000F38 RID: 3896
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetHttpResponseCode")]
		public static extern int IRailHttpResponse_GetHttpResponseCode(IntPtr jarg1);

		// Token: 0x06000F39 RID: 3897
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetResponseHeaderKeys")]
		public static extern int IRailHttpResponse_GetResponseHeaderKeys(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F3A RID: 3898
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetResponseHeaderValue")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailHttpResponse_GetResponseHeaderValue(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2);

		// Token: 0x06000F3B RID: 3899
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetResponseBodyData")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailHttpResponse_GetResponseBodyData(IntPtr jarg1);

		// Token: 0x06000F3C RID: 3900
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetContentLength")]
		public static extern uint IRailHttpResponse_GetContentLength(IntPtr jarg1);

		// Token: 0x06000F3D RID: 3901
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetContentType")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailHttpResponse_GetContentType(IntPtr jarg1);

		// Token: 0x06000F3E RID: 3902
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetContentRange")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailHttpResponse_GetContentRange(IntPtr jarg1);

		// Token: 0x06000F3F RID: 3903
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetContentLanguage")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailHttpResponse_GetContentLanguage(IntPtr jarg1);

		// Token: 0x06000F40 RID: 3904
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetContentEncoding")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailHttpResponse_GetContentEncoding(IntPtr jarg1);

		// Token: 0x06000F41 RID: 3905
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_GetLastModified")]
		[return: MarshalAs(44, MarshalTypeRef = UTF8Marshaler)]
		public static extern string IRailHttpResponse_GetLastModified(IntPtr jarg1);

		// Token: 0x06000F42 RID: 3906
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailHttpResponse")]
		public static extern void delete_IRailHttpResponse(IntPtr jarg1);

		// Token: 0x06000F43 RID: 3907
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServerHelper_GetPlayerSelectedZoneID")]
		public static extern IntPtr IRailZoneServerHelper_GetPlayerSelectedZoneID(IntPtr jarg1);

		// Token: 0x06000F44 RID: 3908
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServerHelper_GetRootZoneID")]
		public static extern IntPtr IRailZoneServerHelper_GetRootZoneID(IntPtr jarg1);

		// Token: 0x06000F45 RID: 3909
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServerHelper_OpenZoneServer")]
		public static extern IntPtr IRailZoneServerHelper_OpenZoneServer(IntPtr jarg1, IntPtr jarg2, out RailResult jarg3);

		// Token: 0x06000F46 RID: 3910
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServerHelper_AsyncSwitchPlayerSelectedZone")]
		public static extern int IRailZoneServerHelper_AsyncSwitchPlayerSelectedZone(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F47 RID: 3911
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailZoneServerHelper")]
		public static extern void delete_IRailZoneServerHelper(IntPtr jarg1);

		// Token: 0x06000F48 RID: 3912
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetZoneID")]
		public static extern IntPtr IRailZoneServer_GetZoneID(IntPtr jarg1);

		// Token: 0x06000F49 RID: 3913
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetZoneNameLanguages")]
		public static extern int IRailZoneServer_GetZoneNameLanguages(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F4A RID: 3914
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetZoneName")]
		public static extern int IRailZoneServer_GetZoneName(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000F4B RID: 3915
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetZoneDescriptionLanguages")]
		public static extern int IRailZoneServer_GetZoneDescriptionLanguages(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F4C RID: 3916
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetZoneDescription")]
		public static extern int IRailZoneServer_GetZoneDescription(IntPtr jarg1, [MarshalAs(44, MarshalTypeRef = UTF8Marshaler)] string jarg2, IntPtr jarg3);

		// Token: 0x06000F4D RID: 3917
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetGameServerAddresses")]
		public static extern int IRailZoneServer_GetGameServerAddresses(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F4E RID: 3918
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetZoneMetadatas")]
		public static extern int IRailZoneServer_GetZoneMetadatas(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F4F RID: 3919
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetChildrenZoneIDs")]
		public static extern int IRailZoneServer_GetChildrenZoneIDs(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F50 RID: 3920
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_IsZoneVisiable")]
		public static extern bool IRailZoneServer_IsZoneVisiable(IntPtr jarg1);

		// Token: 0x06000F51 RID: 3921
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_IsZoneJoinable")]
		public static extern bool IRailZoneServer_IsZoneJoinable(IntPtr jarg1);

		// Token: 0x06000F52 RID: 3922
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetZoneEnableStartTime")]
		public static extern uint IRailZoneServer_GetZoneEnableStartTime(IntPtr jarg1);

		// Token: 0x06000F53 RID: 3923
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_GetZoneEnableEndTime")]
		public static extern uint IRailZoneServer_GetZoneEnableEndTime(IntPtr jarg1);

		// Token: 0x06000F54 RID: 3924
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailZoneServer")]
		public static extern void delete_IRailZoneServer(IntPtr jarg1);

		// Token: 0x06000F55 RID: 3925
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailPlayer")]
		public static extern IntPtr IRailFactory_RailPlayer(IntPtr jarg1);

		// Token: 0x06000F56 RID: 3926
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailUsersHelper")]
		public static extern IntPtr IRailFactory_RailUsersHelper(IntPtr jarg1);

		// Token: 0x06000F57 RID: 3927
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailFriends")]
		public static extern IntPtr IRailFactory_RailFriends(IntPtr jarg1);

		// Token: 0x06000F58 RID: 3928
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailFloatingWindow")]
		public static extern IntPtr IRailFactory_RailFloatingWindow(IntPtr jarg1);

		// Token: 0x06000F59 RID: 3929
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailBrowserHelper")]
		public static extern IntPtr IRailFactory_RailBrowserHelper(IntPtr jarg1);

		// Token: 0x06000F5A RID: 3930
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailInGamePurchase")]
		public static extern IntPtr IRailFactory_RailInGamePurchase(IntPtr jarg1);

		// Token: 0x06000F5B RID: 3931
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailZoneHelper")]
		public static extern IntPtr IRailFactory_RailZoneHelper(IntPtr jarg1);

		// Token: 0x06000F5C RID: 3932
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailRoomHelper")]
		public static extern IntPtr IRailFactory_RailRoomHelper(IntPtr jarg1);

		// Token: 0x06000F5D RID: 3933
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailGameServerHelper")]
		public static extern IntPtr IRailFactory_RailGameServerHelper(IntPtr jarg1);

		// Token: 0x06000F5E RID: 3934
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailStorageHelper")]
		public static extern IntPtr IRailFactory_RailStorageHelper(IntPtr jarg1);

		// Token: 0x06000F5F RID: 3935
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailUserSpaceHelper")]
		public static extern IntPtr IRailFactory_RailUserSpaceHelper(IntPtr jarg1);

		// Token: 0x06000F60 RID: 3936
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailStatisticHelper")]
		public static extern IntPtr IRailFactory_RailStatisticHelper(IntPtr jarg1);

		// Token: 0x06000F61 RID: 3937
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailLeaderboardHelper")]
		public static extern IntPtr IRailFactory_RailLeaderboardHelper(IntPtr jarg1);

		// Token: 0x06000F62 RID: 3938
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailAchievementHelper")]
		public static extern IntPtr IRailFactory_RailAchievementHelper(IntPtr jarg1);

		// Token: 0x06000F63 RID: 3939
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailNetworkHelper")]
		public static extern IntPtr IRailFactory_RailNetworkHelper(IntPtr jarg1);

		// Token: 0x06000F64 RID: 3940
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailApps")]
		public static extern IntPtr IRailFactory_RailApps(IntPtr jarg1);

		// Token: 0x06000F65 RID: 3941
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailGame")]
		public static extern IntPtr IRailFactory_RailGame(IntPtr jarg1);

		// Token: 0x06000F66 RID: 3942
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailUtils")]
		public static extern IntPtr IRailFactory_RailUtils(IntPtr jarg1);

		// Token: 0x06000F67 RID: 3943
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailAssetsHelper")]
		public static extern IntPtr IRailFactory_RailAssetsHelper(IntPtr jarg1);

		// Token: 0x06000F68 RID: 3944
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailDlcHelper")]
		public static extern IntPtr IRailFactory_RailDlcHelper(IntPtr jarg1);

		// Token: 0x06000F69 RID: 3945
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailScreenshotHelper")]
		public static extern IntPtr IRailFactory_RailScreenshotHelper(IntPtr jarg1);

		// Token: 0x06000F6A RID: 3946
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailVoiceHelper")]
		public static extern IntPtr IRailFactory_RailVoiceHelper(IntPtr jarg1);

		// Token: 0x06000F6B RID: 3947
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailSystemHelper")]
		public static extern IntPtr IRailFactory_RailSystemHelper(IntPtr jarg1);

		// Token: 0x06000F6C RID: 3948
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailTextInputHelper")]
		public static extern IntPtr IRailFactory_RailTextInputHelper(IntPtr jarg1);

		// Token: 0x06000F6D RID: 3949
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailIMETextInputHelper")]
		public static extern IntPtr IRailFactory_RailIMETextInputHelper(IntPtr jarg1);

		// Token: 0x06000F6E RID: 3950
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailHttpSessionHelper")]
		public static extern IntPtr IRailFactory_RailHttpSessionHelper(IntPtr jarg1);

		// Token: 0x06000F6F RID: 3951
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailSmallObjectServiceHelper")]
		public static extern IntPtr IRailFactory_RailSmallObjectServiceHelper(IntPtr jarg1);

		// Token: 0x06000F70 RID: 3952
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFactory_RailZoneServerHelper")]
		public static extern IntPtr IRailFactory_RailZoneServerHelper(IntPtr jarg1);

		// Token: 0x06000F71 RID: 3953
		[DllImport("rail_api", EntryPoint = "CSharp_delete_IRailFactory")]
		public static extern void delete_IRailFactory(IntPtr jarg1);

		// Token: 0x06000F72 RID: 3954
		[DllImport("rail_api", EntryPoint = "CSharp_RailNeedRestartAppForCheckingEnvironment")]
		public static extern bool RailNeedRestartAppForCheckingEnvironment(IntPtr jarg1, int jarg2, [MarshalAs(42)] [In] string[] jarg3);

		// Token: 0x06000F73 RID: 3955
		[DllImport("rail_api", EntryPoint = "CSharp_RailInitialize")]
		public static extern bool RailInitialize();

		// Token: 0x06000F74 RID: 3956
		[DllImport("rail_api", EntryPoint = "CSharp_RailFinalize")]
		public static extern void RailFinalize();

		// Token: 0x06000F75 RID: 3957
		[DllImport("rail_api", EntryPoint = "CSharp_RailFireEvents")]
		public static extern void RailFireEvents();

		// Token: 0x06000F76 RID: 3958
		[DllImport("rail_api", EntryPoint = "CSharp_RailFactory")]
		public static extern IntPtr RailFactory();

		// Token: 0x06000F77 RID: 3959
		[DllImport("rail_api", EntryPoint = "CSharp_RailGetSdkVersion")]
		public static extern void RailGetSdkVersion(IntPtr jarg1, IntPtr jarg2);

		// Token: 0x06000F78 RID: 3960
		[DllImport("rail_api", EntryPoint = "CSharp_CSharpRailRegisterEvent")]
		public static extern void CSharpRailRegisterEvent(int jarg1, RailEventCallBackFunction jarg2);

		// Token: 0x06000F79 RID: 3961
		[DllImport("rail_api", EntryPoint = "CSharp_CSharpRailUnRegisterEvent")]
		public static extern void CSharpRailUnRegisterEvent(int jarg1, RailEventCallBackFunction jarg2);

		// Token: 0x06000F7A RID: 3962
		[DllImport("rail_api", EntryPoint = "CSharp_CSharpRailUnRegisterAllEvent")]
		public static extern void CSharpRailUnRegisterAllEvent();

		// Token: 0x06000F7B RID: 3963
		[DllImport("rail_api", EntryPoint = "CSharp_NewInt")]
		public static extern IntPtr NewInt();

		// Token: 0x06000F7C RID: 3964
		[DllImport("rail_api", EntryPoint = "CSharp_DeleteInt")]
		public static extern void DeleteInt(IntPtr jarg1);

		// Token: 0x06000F7D RID: 3965
		[DllImport("rail_api", EntryPoint = "CSharp_GetInt")]
		public static extern int GetInt(IntPtr jarg1);

		// Token: 0x06000F7E RID: 3966
		[DllImport("rail_api", EntryPoint = "CSharp_SetInt")]
		public static extern void SetInt(IntPtr jarg1, int jarg2);

		// Token: 0x06000F7F RID: 3967
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsRequestAllAssetsFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsRequestAllAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F80 RID: 3968
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeToFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsMergeToFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F81 RID: 3969
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSubscribeResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceSubscribeResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F82 RID: 3970
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomNotifyMemberChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F83 RID: 3971
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventInGamePurchasePurchaseProductsResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F84 RID: 3972
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerListResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F85 RID: 3973
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetRoomMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomSetRoomMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F86 RID: 3974
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCloseResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserCloseResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F87 RID: 3975
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionRequest_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventNetworkCreateSessionRequest_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F88 RID: 3976
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsToFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsExchangeAssetsToFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F89 RID: 3977
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByUser_SWIGUpcast")]
		public static extern IntPtr RailEventkRailPlatformNotifyEventJoinGameByUser_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F8A RID: 3978
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallProgress_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcInstallProgress_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F8B RID: 3979
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersNotifyInviter_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersNotifyInviter_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F8C RID: 3980
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsListResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsQueryPlayedWithFriendsListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F8D RID: 3981
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAsyncCreated_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventLeaderboardAsyncCreated_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F8E RID: 3982
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardAttachSpaceWork_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventLeaderboardAttachSpaceWork_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F8F RID: 3983
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetUserRoomListResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomGetUserRoomListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F90 RID: 3984
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTryNavigateNewPageRequest_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserTryNavigateNewPageRequest_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F91 RID: 3985
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcUninstallFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcUninstallFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F92 RID: 3986
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsSplitFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F93 RID: 3987
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsQueryPlayedWithFriendsGamesResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F94 RID: 3988
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardUploaded_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventLeaderboardUploaded_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F95 RID: 3989
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelRemoveUsersResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelRemoveUsersResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F96 RID: 3990
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSmallObjectServiceQueryObjectStateResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventSmallObjectServiceQueryObjectStateResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F97 RID: 3991
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerCreated_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerCreated_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F98 RID: 3992
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStart_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcInstallStart_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F99 RID: 3993
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetInviteDetailResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersGetInviteDetailResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F9A RID: 3994
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceQuerySpaceWorksResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceQuerySpaceWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F9B RID: 3995
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementGlobalAchievementReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAchievementGlobalAchievementReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F9C RID: 3996
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncRenameStreamFileResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageAsyncRenameStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F9D RID: 3997
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsReportPlayedWithUserListResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsReportPlayedWithUserListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F9E RID: 3998
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteStreamFileResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageAsyncWriteStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000F9F RID: 3999
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomKickOffMemberResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomKickOffMemberResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA0 RID: 4000
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingWindow_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventShowFloatingWindow_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA1 RID: 4001
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomOwnerChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomNotifyRoomOwnerChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA2 RID: 4002
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFinalize_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFinalize_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA3 RID: 4003
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersCancelInviteResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersCancelInviteResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA4 RID: 4004
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomListResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA5 RID: 4005
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserDamageRectPaint_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserDamageRectPaint_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA6 RID: 4006
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateAssetPropertyFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsUpdateAssetPropertyFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA7 RID: 4007
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventZoneServerSwitchPlayerSelectedZoneResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA8 RID: 4008
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetGamePurchaseKey_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventPlayerGetGamePurchaseKey_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FA9 RID: 4009
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByRoom_SWIGUpcast")]
		public static extern IntPtr RailEventkRailPlatformNotifyEventJoinGameByRoom_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FAA RID: 4010
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomSetMemberMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomSetMemberMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FAB RID: 4011
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceModifyFavoritesWorksResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceModifyFavoritesWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FAC RID: 4012
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelSpeakingUsersChangedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FAD RID: 4013
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersRespondInvitation_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersRespondInvitation_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FAE RID: 4014
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsDirectConsumeFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsDirectConsumeFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FAF RID: 4015
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomZoneListResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomZoneListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB0 RID: 4016
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceGetMyFavoritesWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB1 RID: 4017
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallStartResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcInstallStartResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB2 RID: 4018
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDestroyed_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomNotifyRoomDestroyed_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB3 RID: 4019
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserPaint_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserPaint_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB4 RID: 4020
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsExchangeAssetsFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsExchangeAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB5 RID: 4021
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventInGamePurchaseAllPurchasableProductsInfoReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB6 RID: 4022
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetFriendPlayedGamesResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsGetFriendPlayedGamesResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB7 RID: 4023
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsClearMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsClearMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB8 RID: 4024
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelMemberChangedEvent_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelMemberChangedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FB9 RID: 4025
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsAddFriendResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsAddFriendResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FBA RID: 4026
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseAllProductsInfoReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventInGamePurchaseAllProductsInfoReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FBB RID: 4027
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsMergeFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsMergeFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FBC RID: 4028
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMemberkicked_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomNotifyMemberkicked_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FBD RID: 4029
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomJoinRoomResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomJoinRoomResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FBE RID: 4030
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAuthSessionTicket_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerAuthSessionTicket_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FBF RID: 4031
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsQueryPlayedWithFriendsTimeResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC0 RID: 4032
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRegisterToServerListResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerRegisterToServerListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC1 RID: 4033
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePaymentResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventInGameStorePurchasePaymentResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC2 RID: 4034
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcCheckAllDlcsStateReadyResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcCheckAllDlcsStateReadyResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC3 RID: 4035
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventScreenshotTakeScreenshotFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC4 RID: 4036
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserReloadResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserReloadResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC5 RID: 4037
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventShowFloatingNotifyWindow_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventShowFloatingNotifyWindow_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC6 RID: 4038
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventInGamePurchasePurchaseProductsToAssetsResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC7 RID: 4039
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetRoomMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomGetRoomMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC8 RID: 4040
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersShowUserHomepageWindowResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersShowUserHomepageWindowResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FC9 RID: 4041
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowDisplayed_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventInGameStorePurchasePayWindowDisplayed_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FCA RID: 4042
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceVoteSpaceWorkResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceVoteSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FCB RID: 4043
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventIMEHelperTextInputSelectedResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventIMEHelperTextInputSelectedResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FCC RID: 4044
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyMetadataChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomNotifyMetadataChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FCD RID: 4045
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventTextInputShowTextInputWindowResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventTextInputShowTextInputWindowResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FCE RID: 4046
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerSetMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerSetMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FCF RID: 4047
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteUsersResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersInviteUsersResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD0 RID: 4048
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsOnlineStateChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsOnlineStateChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD1 RID: 4049
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceUpdateMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceUpdateMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD2 RID: 4050
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsUpdateConsumeFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsUpdateConsumeFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD3 RID: 4051
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelJoinedResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelJoinedResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD4 RID: 4052
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGamePurchaseFinishOrderResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventInGamePurchaseFinishOrderResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD5 RID: 4053
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsStartConsumeFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsStartConsumeFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD6 RID: 4054
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerFavoriteGameServers_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerFavoriteGameServers_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD7 RID: 4055
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserJavascriptEvent_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserJavascriptEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD8 RID: 4056
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAchievementPlayerAchievementReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FD9 RID: 4057
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomLeaveRoomResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomLeaveRoomResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FDA RID: 4058
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomCreated_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomCreated_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FDB RID: 4059
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageQueryQuotaResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageQueryQuotaResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FDC RID: 4060
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcOwnershipChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcOwnershipChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FDD RID: 4061
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserNavigeteResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserNavigeteResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FDE RID: 4062
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetInviteCommandLine_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsGetInviteCommandLine_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FDF RID: 4063
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelUsersSpeakingStateChangedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE0 RID: 4064
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsNumOfPlayerReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStatsNumOfPlayerReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE1 RID: 4065
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcInstallFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcInstallFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE2 RID: 4066
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsGlobalStatsReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStatsGlobalStatsReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE3 RID: 4067
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSearchSpaceWorkResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceSearchSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE4 RID: 4068
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelCreateResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelCreateResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE5 RID: 4069
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsCompleteConsumeByExchangeAssetsToFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE6 RID: 4070
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetMemberMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomGetMemberMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE7 RID: 4071
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsGetMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsGetMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE8 RID: 4072
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncWriteFileResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageAsyncWriteFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FE9 RID: 4073
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStatsPlayerStatsReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FEA RID: 4074
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsCompleteConsumeFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsCompleteConsumeFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FEB RID: 4075
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerGetMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FEC RID: 4076
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerGetAuthenticateURL_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventPlayerGetAuthenticateURL_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FED RID: 4077
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelLeaveResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelLeaveResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FEE RID: 4078
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAchievementPlayerAchievementStored_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAchievementPlayerAchievementStored_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FEF RID: 4079
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventInGameStorePurchasePayWindowClosed_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventInGameStorePurchasePayWindowClosed_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF0 RID: 4080
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelInviteEvent_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelInviteEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF1 RID: 4081
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelDataCaptured_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelDataCaptured_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF2 RID: 4082
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventPlayerAntiAddictionGameOnlineTimeChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF3 RID: 4083
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadStreamFileResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageAsyncReadStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF4 RID: 4084
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserTitleChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserTitleChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF5 RID: 4085
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersShowChatWindowWithFriendResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersShowChatWindowWithFriendResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF6 RID: 4086
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceGetMySubscribedWorksResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceGetMySubscribedWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF7 RID: 4087
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageShareToSpaceWorkResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageShareToSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF8 RID: 4088
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelPushToTalkKeyChangedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FF9 RID: 4089
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUtilsGetImageDataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUtilsGetImageDataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FFA RID: 4090
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUserLimitsResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersGetUserLimitsResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FFB RID: 4091
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSystemStateChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventSystemStateChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FFC RID: 4092
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventLeaderboardReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FFD RID: 4093
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventHttpSessionResponseResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventHttpSessionResponseResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FFE RID: 4094
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomClearRoomMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomClearRoomMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06000FFF RID: 4095
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSmallObjectServiceDownloadResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventSmallObjectServiceDownloadResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001000 RID: 4096
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGetAllDataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomGetAllDataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001001 RID: 4097
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncListStreamFileResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageAsyncListStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001002 RID: 4098
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsFriendsListChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsFriendsListChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001003 RID: 4099
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventLeaderboardEntryReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventLeaderboardEntryReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001004 RID: 4100
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomGameServerChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomNotifyRoomGameServerChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001005 RID: 4101
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomNotifyRoomDataReceived_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomNotifyRoomDataReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001006 RID: 4102
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcQueryIsOwnedDlcsResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcQueryIsOwnedDlcsResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001007 RID: 4103
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceSyncResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceSyncResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001008 RID: 4104
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventDlcRefundChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventDlcRefundChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001009 RID: 4105
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerPlayerListResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerPlayerListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600100A RID: 4106
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersGetUsersInfo_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersGetUsersInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600100B RID: 4107
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncDeleteStreamFileResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageAsyncDeleteStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600100C RID: 4108
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventFriendsSetMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventFriendsSetMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600100D RID: 4109
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserCreateResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserCreateResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600100E RID: 4110
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAssetsSplitToFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAssetsSplitToFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600100F RID: 4111
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotPublishScreenshotFinished_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventScreenshotPublishScreenshotFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001010 RID: 4112
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerAddFavoriteGameServer_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerAddFavoriteGameServer_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001011 RID: 4113
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketGetSessionTicket_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventSessionTicketGetSessionTicket_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001012 RID: 4114
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerRemoveFavoriteGameServer_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerRemoveFavoriteGameServer_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001013 RID: 4115
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventRoomGotRoomMembers_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventRoomGotRoomMembers_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001014 RID: 4116
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStatsPlayerStatsStored_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStatsPlayerStatsStored_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001015 RID: 4117
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventStorageAsyncReadFileResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventStorageAsyncReadFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001016 RID: 4118
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventSessionTicketAuthSessionTicket_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventSessionTicketAuthSessionTicket_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001017 RID: 4119
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventScreenshotTakeScreenshotRequest_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventScreenshotTakeScreenshotRequest_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001018 RID: 4120
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUserSpaceRemoveSpaceWorkResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUserSpaceRemoveSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001019 RID: 4121
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventBrowserStateChanged_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventBrowserStateChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600101A RID: 4122
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventGameServerGetSessionTicket_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventGameServerGetSessionTicket_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600101B RID: 4123
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventUsersInviteJoinGameResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventUsersInviteJoinGameResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600101C RID: 4124
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventVoiceChannelAddUsersResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventVoiceChannelAddUsersResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600101D RID: 4125
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventAppQuerySubscribeWishPlayStateResult_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventAppQuerySubscribeWishPlayStateResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600101E RID: 4126
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventNetworkCreateSessionFailed_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventNetworkCreateSessionFailed_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600101F RID: 4127
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailEventQueryPlayerBannedStatus_SWIGUpcast")]
		public static extern IntPtr RailEventkRailEventQueryPlayerBannedStatus_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001020 RID: 4128
		[DllImport("rail_api", EntryPoint = "CSharp_RailEventkRailPlatformNotifyEventJoinGameByGameServer_SWIGUpcast")]
		public static extern IntPtr RailEventkRailPlatformNotifyEventJoinGameByGameServer_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001021 RID: 4129
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMySubscribedWorksResult_SWIGUpcast")]
		public static extern IntPtr AsyncGetMySubscribedWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001022 RID: 4130
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetMyFavoritesWorksResult_SWIGUpcast")]
		public static extern IntPtr AsyncGetMyFavoritesWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001023 RID: 4131
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQuerySpaceWorksResult_SWIGUpcast")]
		public static extern IntPtr AsyncQuerySpaceWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001024 RID: 4132
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncUpdateMetadataResult_SWIGUpcast")]
		public static extern IntPtr AsyncUpdateMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001025 RID: 4133
		[DllImport("rail_api", EntryPoint = "CSharp_SyncSpaceWorkResult_SWIGUpcast")]
		public static extern IntPtr SyncSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001026 RID: 4134
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSubscribeSpaceWorksResult_SWIGUpcast")]
		public static extern IntPtr AsyncSubscribeSpaceWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001027 RID: 4135
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncModifyFavoritesWorksResult_SWIGUpcast")]
		public static extern IntPtr AsyncModifyFavoritesWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001028 RID: 4136
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveSpaceWorkResult_SWIGUpcast")]
		public static extern IntPtr AsyncRemoveSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001029 RID: 4137
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncVoteSpaceWorkResult_SWIGUpcast")]
		public static extern IntPtr AsyncVoteSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600102A RID: 4138
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncSearchSpaceWorksResult_SWIGUpcast")]
		public static extern IntPtr AsyncSearchSpaceWorksResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600102B RID: 4139
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInfoData_SWIGUpcast")]
		public static extern IntPtr RailUsersInfoData_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600102C RID: 4140
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersNotifyInviter_SWIGUpcast")]
		public static extern IntPtr RailUsersNotifyInviter_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600102D RID: 4141
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersRespondInvitation_SWIGUpcast")]
		public static extern IntPtr RailUsersRespondInvitation_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600102E RID: 4142
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteJoinGameResult_SWIGUpcast")]
		public static extern IntPtr RailUsersInviteJoinGameResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600102F RID: 4143
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetInviteDetailResult_SWIGUpcast")]
		public static extern IntPtr RailUsersGetInviteDetailResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001030 RID: 4144
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersCancelInviteResult_SWIGUpcast")]
		public static extern IntPtr RailUsersCancelInviteResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001031 RID: 4145
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersInviteUsersResult_SWIGUpcast")]
		public static extern IntPtr RailUsersInviteUsersResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001032 RID: 4146
		[DllImport("rail_api", EntryPoint = "CSharp_RailUsersGetUserLimitsResult_SWIGUpcast")]
		public static extern IntPtr RailUsersGetUserLimitsResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001033 RID: 4147
		[DllImport("rail_api", EntryPoint = "CSharp_RailShowChatWindowWithFriendResult_SWIGUpcast")]
		public static extern IntPtr RailShowChatWindowWithFriendResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001034 RID: 4148
		[DllImport("rail_api", EntryPoint = "CSharp_RailShowUserHomepageWindowResult_SWIGUpcast")]
		public static extern IntPtr RailShowUserHomepageWindowResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001035 RID: 4149
		[DllImport("rail_api", EntryPoint = "CSharp_RailGetImageDataResult_SWIGUpcast")]
		public static extern IntPtr RailGetImageDataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001036 RID: 4150
		[DllImport("rail_api", EntryPoint = "CSharp_TakeScreenshotResult_SWIGUpcast")]
		public static extern IntPtr TakeScreenshotResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001037 RID: 4151
		[DllImport("rail_api", EntryPoint = "CSharp_ScreenshotRequestInfo_SWIGUpcast")]
		public static extern IntPtr ScreenshotRequestInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001038 RID: 4152
		[DllImport("rail_api", EntryPoint = "CSharp_PublishScreenshotResult_SWIGUpcast")]
		public static extern IntPtr PublishScreenshotResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001039 RID: 4153
		[DllImport("rail_api", EntryPoint = "CSharp_RailSystemStateChanged_SWIGUpcast")]
		public static extern IntPtr RailSystemStateChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600103A RID: 4154
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByGameServer_SWIGUpcast")]
		public static extern IntPtr RailPlatformNotifyEventJoinGameByGameServer_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600103B RID: 4155
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByRoom_SWIGUpcast")]
		public static extern IntPtr RailPlatformNotifyEventJoinGameByRoom_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600103C RID: 4156
		[DllImport("rail_api", EntryPoint = "CSharp_RailPlatformNotifyEventJoinGameByUser_SWIGUpcast")]
		public static extern IntPtr RailPlatformNotifyEventJoinGameByUser_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600103D RID: 4157
		[DllImport("rail_api", EntryPoint = "CSharp_RailFinalize_SWIGUpcast")]
		public static extern IntPtr RailFinalize_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600103E RID: 4158
		[DllImport("rail_api", EntryPoint = "CSharp_RequestAllAssetsFinished_SWIGUpcast")]
		public static extern IntPtr RequestAllAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600103F RID: 4159
		[DllImport("rail_api", EntryPoint = "CSharp_UpdateAssetsPropertyFinished_SWIGUpcast")]
		public static extern IntPtr UpdateAssetsPropertyFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001040 RID: 4160
		[DllImport("rail_api", EntryPoint = "CSharp_DirectConsumeAssetsFinished_SWIGUpcast")]
		public static extern IntPtr DirectConsumeAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001041 RID: 4161
		[DllImport("rail_api", EntryPoint = "CSharp_StartConsumeAssetsFinished_SWIGUpcast")]
		public static extern IntPtr StartConsumeAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001042 RID: 4162
		[DllImport("rail_api", EntryPoint = "CSharp_UpdateConsumeAssetsFinished_SWIGUpcast")]
		public static extern IntPtr UpdateConsumeAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001043 RID: 4163
		[DllImport("rail_api", EntryPoint = "CSharp_CompleteConsumeAssetsFinished_SWIGUpcast")]
		public static extern IntPtr CompleteConsumeAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001044 RID: 4164
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsFinished_SWIGUpcast")]
		public static extern IntPtr SplitAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001045 RID: 4165
		[DllImport("rail_api", EntryPoint = "CSharp_SplitAssetsToFinished_SWIGUpcast")]
		public static extern IntPtr SplitAssetsToFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001046 RID: 4166
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsFinished_SWIGUpcast")]
		public static extern IntPtr MergeAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001047 RID: 4167
		[DllImport("rail_api", EntryPoint = "CSharp_MergeAssetsToFinished_SWIGUpcast")]
		public static extern IntPtr MergeAssetsToFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001048 RID: 4168
		[DllImport("rail_api", EntryPoint = "CSharp_CompleteConsumeByExchangeAssetsToFinished_SWIGUpcast")]
		public static extern IntPtr CompleteConsumeByExchangeAssetsToFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001049 RID: 4169
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsFinished_SWIGUpcast")]
		public static extern IntPtr ExchangeAssetsFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600104A RID: 4170
		[DllImport("rail_api", EntryPoint = "CSharp_ExchangeAssetsToFinished_SWIGUpcast")]
		public static extern IntPtr ExchangeAssetsToFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600104B RID: 4171
		[DllImport("rail_api", EntryPoint = "CSharp_CreateBrowserResult_SWIGUpcast")]
		public static extern IntPtr CreateBrowserResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600104C RID: 4172
		[DllImport("rail_api", EntryPoint = "CSharp_ReloadBrowserResult_SWIGUpcast")]
		public static extern IntPtr ReloadBrowserResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600104D RID: 4173
		[DllImport("rail_api", EntryPoint = "CSharp_CloseBrowserResult_SWIGUpcast")]
		public static extern IntPtr CloseBrowserResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600104E RID: 4174
		[DllImport("rail_api", EntryPoint = "CSharp_JavascriptEventResult_SWIGUpcast")]
		public static extern IntPtr JavascriptEventResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600104F RID: 4175
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserNeedsPaintRequest_SWIGUpcast")]
		public static extern IntPtr BrowserNeedsPaintRequest_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001050 RID: 4176
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserDamageRectNeedsPaintRequest_SWIGUpcast")]
		public static extern IntPtr BrowserDamageRectNeedsPaintRequest_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001051 RID: 4177
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderNavigateResult_SWIGUpcast")]
		public static extern IntPtr BrowserRenderNavigateResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001052 RID: 4178
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderStateChanged_SWIGUpcast")]
		public static extern IntPtr BrowserRenderStateChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001053 RID: 4179
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserRenderTitleChanged_SWIGUpcast")]
		public static extern IntPtr BrowserRenderTitleChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001054 RID: 4180
		[DllImport("rail_api", EntryPoint = "CSharp_BrowserTryNavigateNewPageRequest_SWIGUpcast")]
		public static extern IntPtr BrowserTryNavigateNewPageRequest_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001055 RID: 4181
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStart_SWIGUpcast")]
		public static extern IntPtr DlcInstallStart_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001056 RID: 4182
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallStartResult_SWIGUpcast")]
		public static extern IntPtr DlcInstallStartResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001057 RID: 4183
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallProgress_SWIGUpcast")]
		public static extern IntPtr DlcInstallProgress_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001058 RID: 4184
		[DllImport("rail_api", EntryPoint = "CSharp_DlcInstallFinished_SWIGUpcast")]
		public static extern IntPtr DlcInstallFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001059 RID: 4185
		[DllImport("rail_api", EntryPoint = "CSharp_DlcUninstallFinished_SWIGUpcast")]
		public static extern IntPtr DlcUninstallFinished_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600105A RID: 4186
		[DllImport("rail_api", EntryPoint = "CSharp_CheckAllDlcsStateReadyResult_SWIGUpcast")]
		public static extern IntPtr CheckAllDlcsStateReadyResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600105B RID: 4187
		[DllImport("rail_api", EntryPoint = "CSharp_QueryIsOwnedDlcsResult_SWIGUpcast")]
		public static extern IntPtr QueryIsOwnedDlcsResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600105C RID: 4188
		[DllImport("rail_api", EntryPoint = "CSharp_DlcOwnershipChanged_SWIGUpcast")]
		public static extern IntPtr DlcOwnershipChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600105D RID: 4189
		[DllImport("rail_api", EntryPoint = "CSharp_DlcRefundChanged_SWIGUpcast")]
		public static extern IntPtr DlcRefundChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600105E RID: 4190
		[DllImport("rail_api", EntryPoint = "CSharp_ShowFloatingWindowResult_SWIGUpcast")]
		public static extern IntPtr ShowFloatingWindowResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600105F RID: 4191
		[DllImport("rail_api", EntryPoint = "CSharp_ShowNotifyWindow_SWIGUpcast")]
		public static extern IntPtr ShowNotifyWindow_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001060 RID: 4192
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncAcquireGameServerSessionTicketResponse_SWIGUpcast")]
		public static extern IntPtr AsyncAcquireGameServerSessionTicketResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001061 RID: 4193
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerStartSessionWithPlayerResponse_SWIGUpcast")]
		public static extern IntPtr GameServerStartSessionWithPlayerResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001062 RID: 4194
		[DllImport("rail_api", EntryPoint = "CSharp_CreateGameServerResult_SWIGUpcast")]
		public static extern IntPtr CreateGameServerResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001063 RID: 4195
		[DllImport("rail_api", EntryPoint = "CSharp_SetGameServerMetadataResult_SWIGUpcast")]
		public static extern IntPtr SetGameServerMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001064 RID: 4196
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerMetadataResult_SWIGUpcast")]
		public static extern IntPtr GetGameServerMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001065 RID: 4197
		[DllImport("rail_api", EntryPoint = "CSharp_GameServerRegisterToServerListResult_SWIGUpcast")]
		public static extern IntPtr GameServerRegisterToServerListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001066 RID: 4198
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerPlayerListResult_SWIGUpcast")]
		public static extern IntPtr GetGameServerPlayerListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001067 RID: 4199
		[DllImport("rail_api", EntryPoint = "CSharp_GetGameServerListResult_SWIGUpcast")]
		public static extern IntPtr GetGameServerListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001068 RID: 4200
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncGetFavoriteGameServersResult_SWIGUpcast")]
		public static extern IntPtr AsyncGetFavoriteGameServersResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001069 RID: 4201
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncAddFavoriteGameServerResult_SWIGUpcast")]
		public static extern IntPtr AsyncAddFavoriteGameServerResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600106A RID: 4202
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRemoveFavoriteGameServerResult_SWIGUpcast")]
		public static extern IntPtr AsyncRemoveFavoriteGameServerResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600106B RID: 4203
		[DllImport("rail_api", EntryPoint = "CSharp_AcquireSessionTicketResponse_SWIGUpcast")]
		public static extern IntPtr AcquireSessionTicketResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600106C RID: 4204
		[DllImport("rail_api", EntryPoint = "CSharp_StartSessionWithPlayerResponse_SWIGUpcast")]
		public static extern IntPtr StartSessionWithPlayerResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600106D RID: 4205
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerGetGamePurchaseKeyResult_SWIGUpcast")]
		public static extern IntPtr PlayerGetGamePurchaseKeyResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600106E RID: 4206
		[DllImport("rail_api", EntryPoint = "CSharp_QueryPlayerBannedStatus_SWIGUpcast")]
		public static extern IntPtr QueryPlayerBannedStatus_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600106F RID: 4207
		[DllImport("rail_api", EntryPoint = "CSharp_GetAuthenticateURLResult_SWIGUpcast")]
		public static extern IntPtr GetAuthenticateURLResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001070 RID: 4208
		[DllImport("rail_api", EntryPoint = "CSharp_RailAntiAddictionGameOnlineTimeChanged_SWIGUpcast")]
		public static extern IntPtr RailAntiAddictionGameOnlineTimeChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001071 RID: 4209
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsSetMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsSetMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001072 RID: 4210
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsGetMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001073 RID: 4211
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsClearMetadataResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsClearMetadataResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001074 RID: 4212
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsGetInviteCommandLine_SWIGUpcast")]
		public static extern IntPtr RailFriendsGetInviteCommandLine_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001075 RID: 4213
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsReportPlayedWithUserListResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsReportPlayedWithUserListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001076 RID: 4214
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsListChanged_SWIGUpcast")]
		public static extern IntPtr RailFriendsListChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001077 RID: 4215
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryFriendPlayedGamesResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsQueryFriendPlayedGamesResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001078 RID: 4216
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsListResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsQueryPlayedWithFriendsListResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001079 RID: 4217
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsTimeResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsQueryPlayedWithFriendsTimeResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600107A RID: 4218
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsQueryPlayedWithFriendsGamesResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsQueryPlayedWithFriendsGamesResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600107B RID: 4219
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsAddFriendResult_SWIGUpcast")]
		public static extern IntPtr RailFriendsAddFriendResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600107C RID: 4220
		[DllImport("rail_api", EntryPoint = "CSharp_RailFriendsOnlineStateChanged_SWIGUpcast")]
		public static extern IntPtr RailFriendsOnlineStateChanged_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600107D RID: 4221
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllPurchasableProductsResponse_SWIGUpcast")]
		public static extern IntPtr RailInGamePurchaseRequestAllPurchasableProductsResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600107E RID: 4222
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseRequestAllProductsResponse_SWIGUpcast")]
		public static extern IntPtr RailInGamePurchaseRequestAllProductsResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600107F RID: 4223
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsResponse_SWIGUpcast")]
		public static extern IntPtr RailInGamePurchasePurchaseProductsResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001080 RID: 4224
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchasePurchaseProductsToAssetsResponse_SWIGUpcast")]
		public static extern IntPtr RailInGamePurchasePurchaseProductsToAssetsResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001081 RID: 4225
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGamePurchaseFinishOrderResponse_SWIGUpcast")]
		public static extern IntPtr RailInGamePurchaseFinishOrderResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001082 RID: 4226
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowDisplayed_SWIGUpcast")]
		public static extern IntPtr RailInGameStorePurchasePayWindowDisplayed_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001083 RID: 4227
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchasePayWindowClosed_SWIGUpcast")]
		public static extern IntPtr RailInGameStorePurchasePayWindowClosed_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001084 RID: 4228
		[DllImport("rail_api", EntryPoint = "CSharp_RailInGameStorePurchaseResult_SWIGUpcast")]
		public static extern IntPtr RailInGameStorePurchaseResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001085 RID: 4229
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionRequest_SWIGUpcast")]
		public static extern IntPtr CreateSessionRequest_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001086 RID: 4230
		[DllImport("rail_api", EntryPoint = "CSharp_CreateSessionFailed_SWIGUpcast")]
		public static extern IntPtr CreateSessionFailed_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001087 RID: 4231
		[DllImport("rail_api", EntryPoint = "CSharp_ZoneInfoList_SWIGUpcast")]
		public static extern IntPtr ZoneInfoList_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001088 RID: 4232
		[DllImport("rail_api", EntryPoint = "CSharp_RoomInfoList_SWIGUpcast")]
		public static extern IntPtr RoomInfoList_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001089 RID: 4233
		[DllImport("rail_api", EntryPoint = "CSharp_RoomAllData_SWIGUpcast")]
		public static extern IntPtr RoomAllData_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600108A RID: 4234
		[DllImport("rail_api", EntryPoint = "CSharp_CreateRoomInfo_SWIGUpcast")]
		public static extern IntPtr CreateRoomInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600108B RID: 4235
		[DllImport("rail_api", EntryPoint = "CSharp_RoomMembersInfo_SWIGUpcast")]
		public static extern IntPtr RoomMembersInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600108C RID: 4236
		[DllImport("rail_api", EntryPoint = "CSharp_JoinRoomInfo_SWIGUpcast")]
		public static extern IntPtr JoinRoomInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600108D RID: 4237
		[DllImport("rail_api", EntryPoint = "CSharp_KickOffMemberInfo_SWIGUpcast")]
		public static extern IntPtr KickOffMemberInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600108E RID: 4238
		[DllImport("rail_api", EntryPoint = "CSharp_SetRoomMetadataInfo_SWIGUpcast")]
		public static extern IntPtr SetRoomMetadataInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600108F RID: 4239
		[DllImport("rail_api", EntryPoint = "CSharp_GetRoomMetadataInfo_SWIGUpcast")]
		public static extern IntPtr GetRoomMetadataInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001090 RID: 4240
		[DllImport("rail_api", EntryPoint = "CSharp_ClearRoomMetadataInfo_SWIGUpcast")]
		public static extern IntPtr ClearRoomMetadataInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001091 RID: 4241
		[DllImport("rail_api", EntryPoint = "CSharp_GetMemberMetadataInfo_SWIGUpcast")]
		public static extern IntPtr GetMemberMetadataInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001092 RID: 4242
		[DllImport("rail_api", EntryPoint = "CSharp_SetMemberMetadataInfo_SWIGUpcast")]
		public static extern IntPtr SetMemberMetadataInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001093 RID: 4243
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveRoomInfo_SWIGUpcast")]
		public static extern IntPtr LeaveRoomInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001094 RID: 4244
		[DllImport("rail_api", EntryPoint = "CSharp_UserRoomListInfo_SWIGUpcast")]
		public static extern IntPtr UserRoomListInfo_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001095 RID: 4245
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyMetadataChange_SWIGUpcast")]
		public static extern IntPtr NotifyMetadataChange_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001096 RID: 4246
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberChange_SWIGUpcast")]
		public static extern IntPtr NotifyRoomMemberChange_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001097 RID: 4247
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomMemberKicked_SWIGUpcast")]
		public static extern IntPtr NotifyRoomMemberKicked_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001098 RID: 4248
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomDestroy_SWIGUpcast")]
		public static extern IntPtr NotifyRoomDestroy_SWIGUpcast(IntPtr jarg1);

		// Token: 0x06001099 RID: 4249
		[DllImport("rail_api", EntryPoint = "CSharp_RoomDataReceived_SWIGUpcast")]
		public static extern IntPtr RoomDataReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600109A RID: 4250
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomOwnerChange_SWIGUpcast")]
		public static extern IntPtr NotifyRoomOwnerChange_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600109B RID: 4251
		[DllImport("rail_api", EntryPoint = "CSharp_NotifyRoomGameServerChange_SWIGUpcast")]
		public static extern IntPtr NotifyRoomGameServerChange_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600109C RID: 4252
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncQueryQuotaResult_SWIGUpcast")]
		public static extern IntPtr AsyncQueryQuotaResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600109D RID: 4253
		[DllImport("rail_api", EntryPoint = "CSharp_ShareStorageToSpaceWorkResult_SWIGUpcast")]
		public static extern IntPtr ShareStorageToSpaceWorkResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600109E RID: 4254
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadFileResult_SWIGUpcast")]
		public static extern IntPtr AsyncReadFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x0600109F RID: 4255
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteFileResult_SWIGUpcast")]
		public static extern IntPtr AsyncWriteFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A0 RID: 4256
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncReadStreamFileResult_SWIGUpcast")]
		public static extern IntPtr AsyncReadStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A1 RID: 4257
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncWriteStreamFileResult_SWIGUpcast")]
		public static extern IntPtr AsyncWriteStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A2 RID: 4258
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncListFileResult_SWIGUpcast")]
		public static extern IntPtr AsyncListFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A3 RID: 4259
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncRenameStreamFileResult_SWIGUpcast")]
		public static extern IntPtr AsyncRenameStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A4 RID: 4260
		[DllImport("rail_api", EntryPoint = "CSharp_AsyncDeleteStreamFileResult_SWIGUpcast")]
		public static extern IntPtr AsyncDeleteStreamFileResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A5 RID: 4261
		[DllImport("rail_api", EntryPoint = "CSharp_CreateVoiceChannelResult_SWIGUpcast")]
		public static extern IntPtr CreateVoiceChannelResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A6 RID: 4262
		[DllImport("rail_api", EntryPoint = "CSharp_JoinVoiceChannelResult_SWIGUpcast")]
		public static extern IntPtr JoinVoiceChannelResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A7 RID: 4263
		[DllImport("rail_api", EntryPoint = "CSharp_LeaveVoiceChannelResult_SWIGUpcast")]
		public static extern IntPtr LeaveVoiceChannelResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A8 RID: 4264
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelAddUsersResult_SWIGUpcast")]
		public static extern IntPtr VoiceChannelAddUsersResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010A9 RID: 4265
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelRemoveUsersResult_SWIGUpcast")]
		public static extern IntPtr VoiceChannelRemoveUsersResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010AA RID: 4266
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelInviteEvent_SWIGUpcast")]
		public static extern IntPtr VoiceChannelInviteEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010AB RID: 4267
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelMemeberChangedEvent_SWIGUpcast")]
		public static extern IntPtr VoiceChannelMemeberChangedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010AC RID: 4268
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelPushToTalkKeyChangedEvent_SWIGUpcast")]
		public static extern IntPtr VoiceChannelPushToTalkKeyChangedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010AD RID: 4269
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelUsersSpeakingStateChangedEvent_SWIGUpcast")]
		public static extern IntPtr VoiceChannelUsersSpeakingStateChangedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010AE RID: 4270
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceChannelSpeakingUsersChangedEvent_SWIGUpcast")]
		public static extern IntPtr VoiceChannelSpeakingUsersChangedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010AF RID: 4271
		[DllImport("rail_api", EntryPoint = "CSharp_VoiceDataCapturedEvent_SWIGUpcast")]
		public static extern IntPtr VoiceDataCapturedEvent_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B0 RID: 4272
		[DllImport("rail_api", EntryPoint = "CSharp_RailTextInputResult_SWIGUpcast")]
		public static extern IntPtr RailTextInputResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B1 RID: 4273
		[DllImport("rail_api", EntryPoint = "CSharp_QuerySubscribeWishPlayStateResult_SWIGUpcast")]
		public static extern IntPtr QuerySubscribeWishPlayStateResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B2 RID: 4274
		[DllImport("rail_api", EntryPoint = "CSharp_RailIMEHelperTextInputSelectedResult_SWIGUpcast")]
		public static extern IntPtr RailIMEHelperTextInputSelectedResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B3 RID: 4275
		[DllImport("rail_api", EntryPoint = "CSharp_RailHttpSessionResponse_SWIGUpcast")]
		public static extern IntPtr RailHttpSessionResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B4 RID: 4276
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectStateQueryResult_SWIGUpcast")]
		public static extern IntPtr RailSmallObjectStateQueryResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B5 RID: 4277
		[DllImport("rail_api", EntryPoint = "CSharp_RailSmallObjectDownloadResult_SWIGUpcast")]
		public static extern IntPtr RailSmallObjectDownloadResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B6 RID: 4278
		[DllImport("rail_api", EntryPoint = "CSharp_RailSwitchPlayerSelectedZoneResult_SWIGUpcast")]
		public static extern IntPtr RailSwitchPlayerSelectedZoneResult_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B7 RID: 4279
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerAchievement_SWIGUpcast")]
		public static extern IntPtr IRailPlayerAchievement_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B8 RID: 4280
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalAchievement_SWIGUpcast")]
		public static extern IntPtr IRailGlobalAchievement_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010B9 RID: 4281
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementReceived_SWIGUpcast")]
		public static extern IntPtr PlayerAchievementReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010BA RID: 4282
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerAchievementStored_SWIGUpcast")]
		public static extern IntPtr PlayerAchievementStored_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010BB RID: 4283
		[DllImport("rail_api", EntryPoint = "CSharp_GlobalAchievementReceived_SWIGUpcast")]
		public static extern IntPtr GlobalAchievementReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010BC RID: 4284
		[DllImport("rail_api", EntryPoint = "CSharp_IRailAssets_SWIGUpcast")]
		public static extern IntPtr IRailAssets_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010BD RID: 4285
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowser_SWIGUpcast")]
		public static extern IntPtr IRailBrowser_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010BE RID: 4286
		[DllImport("rail_api", EntryPoint = "CSharp_IRailBrowserRender_SWIGUpcast")]
		public static extern IntPtr IRailBrowserRender_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010BF RID: 4287
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGameServer_SWIGUpcast")]
		public static extern IntPtr IRailGameServer_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C0 RID: 4288
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboard_SWIGUpcast")]
		public static extern IntPtr IRailLeaderboard_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C1 RID: 4289
		[DllImport("rail_api", EntryPoint = "CSharp_IRailLeaderboardEntries_SWIGUpcast")]
		public static extern IntPtr IRailLeaderboardEntries_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C2 RID: 4290
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardReceived_SWIGUpcast")]
		public static extern IntPtr LeaderboardReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C3 RID: 4291
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardCreated_SWIGUpcast")]
		public static extern IntPtr LeaderboardCreated_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C4 RID: 4292
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardEntryReceived_SWIGUpcast")]
		public static extern IntPtr LeaderboardEntryReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C5 RID: 4293
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardUploaded_SWIGUpcast")]
		public static extern IntPtr LeaderboardUploaded_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C6 RID: 4294
		[DllImport("rail_api", EntryPoint = "CSharp_LeaderboardAttachSpaceWork_SWIGUpcast")]
		public static extern IntPtr LeaderboardAttachSpaceWork_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C7 RID: 4295
		[DllImport("rail_api", EntryPoint = "CSharp_IRailRoom_SWIGUpcast")]
		public static extern IntPtr IRailRoom_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C8 RID: 4296
		[DllImport("rail_api", EntryPoint = "CSharp_IRailScreenshot_SWIGUpcast")]
		public static extern IntPtr IRailScreenshot_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010C9 RID: 4297
		[DllImport("rail_api", EntryPoint = "CSharp_IRailPlayerStats_SWIGUpcast")]
		public static extern IntPtr IRailPlayerStats_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010CA RID: 4298
		[DllImport("rail_api", EntryPoint = "CSharp_IRailGlobalStats_SWIGUpcast")]
		public static extern IntPtr IRailGlobalStats_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010CB RID: 4299
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerStatsReceived_SWIGUpcast")]
		public static extern IntPtr PlayerStatsReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010CC RID: 4300
		[DllImport("rail_api", EntryPoint = "CSharp_PlayerStatsStored_SWIGUpcast")]
		public static extern IntPtr PlayerStatsStored_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010CD RID: 4301
		[DllImport("rail_api", EntryPoint = "CSharp_NumberOfPlayerReceived_SWIGUpcast")]
		public static extern IntPtr NumberOfPlayerReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010CE RID: 4302
		[DllImport("rail_api", EntryPoint = "CSharp_GlobalStatsRequestReceived_SWIGUpcast")]
		public static extern IntPtr GlobalStatsRequestReceived_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010CF RID: 4303
		[DllImport("rail_api", EntryPoint = "CSharp_IRailFile_SWIGUpcast")]
		public static extern IntPtr IRailFile_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010D0 RID: 4304
		[DllImport("rail_api", EntryPoint = "CSharp_IRailStreamFile_SWIGUpcast")]
		public static extern IntPtr IRailStreamFile_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010D1 RID: 4305
		[DllImport("rail_api", EntryPoint = "CSharp_IRailSpaceWork_SWIGUpcast")]
		public static extern IntPtr IRailSpaceWork_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010D2 RID: 4306
		[DllImport("rail_api", EntryPoint = "CSharp_IRailVoiceChannel_SWIGUpcast")]
		public static extern IntPtr IRailVoiceChannel_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010D3 RID: 4307
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpSession_SWIGUpcast")]
		public static extern IntPtr IRailHttpSession_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010D4 RID: 4308
		[DllImport("rail_api", EntryPoint = "CSharp_IRailHttpResponse_SWIGUpcast")]
		public static extern IntPtr IRailHttpResponse_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010D5 RID: 4309
		[DllImport("rail_api", EntryPoint = "CSharp_IRailZoneServer_SWIGUpcast")]
		public static extern IntPtr IRailZoneServer_SWIGUpcast(IntPtr jarg1);

		// Token: 0x060010D6 RID: 4310 RVA: 0x00002119 File Offset: 0x00000319
		public RAIL_API_PINVOKE()
		{
		}

		// Token: 0x04000002 RID: 2
		public const string dll_path = "rail_api";

		// Token: 0x04000003 RID: 3
		protected static RAIL_API_PINVOKE.SWIGExceptionHelper swigExceptionHelper = new RAIL_API_PINVOKE.SWIGExceptionHelper();

		// Token: 0x04000004 RID: 4
		protected static RAIL_API_PINVOKE.SWIGStringHelper swigStringHelper = new RAIL_API_PINVOKE.SWIGStringHelper();

		// Token: 0x020001B2 RID: 434
		protected class SWIGExceptionHelper
		{
			// Token: 0x06001903 RID: 6403
			[DllImport("rail_api")]
			public static extern void SWIGRegisterExceptionCallbacks_rail_api(RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate applicationDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate arithmeticDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate divideByZeroDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate indexOutOfRangeDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate invalidCastDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate invalidOperationDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate ioDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate nullReferenceDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate outOfMemoryDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate overflowDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate systemExceptionDelegate);

			// Token: 0x06001904 RID: 6404
			[DllImport("rail_api", EntryPoint = "SWIGRegisterExceptionArgumentCallbacks_rail_api")]
			public static extern void SWIGRegisterExceptionCallbacksArgument_rail_api(RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentNullDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentOutOfRangeDelegate);

			// Token: 0x06001905 RID: 6405 RVA: 0x00011277 File Offset: 0x0000F477
			private static void SetPendingApplicationException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new ApplicationException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06001906 RID: 6406 RVA: 0x00011289 File Offset: 0x0000F489
			private static void SetPendingArithmeticException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new ArithmeticException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06001907 RID: 6407 RVA: 0x0001129B File Offset: 0x0000F49B
			private static void SetPendingDivideByZeroException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new DivideByZeroException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06001908 RID: 6408 RVA: 0x000112AD File Offset: 0x0000F4AD
			private static void SetPendingIndexOutOfRangeException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new IndexOutOfRangeException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06001909 RID: 6409 RVA: 0x000112BF File Offset: 0x0000F4BF
			private static void SetPendingInvalidCastException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new InvalidCastException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x0600190A RID: 6410 RVA: 0x000112D1 File Offset: 0x0000F4D1
			private static void SetPendingInvalidOperationException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new InvalidOperationException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x0600190B RID: 6411 RVA: 0x000112E3 File Offset: 0x0000F4E3
			private static void SetPendingIOException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new IOException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x0600190C RID: 6412 RVA: 0x000112F5 File Offset: 0x0000F4F5
			private static void SetPendingNullReferenceException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new NullReferenceException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x0600190D RID: 6413 RVA: 0x00011307 File Offset: 0x0000F507
			private static void SetPendingOutOfMemoryException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new OutOfMemoryException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x0600190E RID: 6414 RVA: 0x00011319 File Offset: 0x0000F519
			private static void SetPendingOverflowException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new OverflowException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x0600190F RID: 6415 RVA: 0x0001132B File Offset: 0x0000F52B
			private static void SetPendingSystemException(string message)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new SystemException(message, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06001910 RID: 6416 RVA: 0x0001133D File Offset: 0x0000F53D
			private static void SetPendingArgumentException(string message, string paramName)
			{
				RAIL_API_PINVOKE.SWIGPendingException.Set(new ArgumentException(message, paramName, RAIL_API_PINVOKE.SWIGPendingException.Retrieve()));
			}

			// Token: 0x06001911 RID: 6417 RVA: 0x00011350 File Offset: 0x0000F550
			private static void SetPendingArgumentNullException(string message, string paramName)
			{
				Exception ex = RAIL_API_PINVOKE.SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				RAIL_API_PINVOKE.SWIGPendingException.Set(new ArgumentNullException(paramName, message));
			}

			// Token: 0x06001912 RID: 6418 RVA: 0x00011388 File Offset: 0x0000F588
			private static void SetPendingArgumentOutOfRangeException(string message, string paramName)
			{
				Exception ex = RAIL_API_PINVOKE.SWIGPendingException.Retrieve();
				if (ex != null)
				{
					message = message + " Inner Exception: " + ex.Message;
				}
				RAIL_API_PINVOKE.SWIGPendingException.Set(new ArgumentOutOfRangeException(paramName, message));
			}

			// Token: 0x06001913 RID: 6419 RVA: 0x000113C0 File Offset: 0x0000F5C0
			static SWIGExceptionHelper()
			{
				RAIL_API_PINVOKE.SWIGExceptionHelper.SWIGRegisterExceptionCallbacks_rail_api(RAIL_API_PINVOKE.SWIGExceptionHelper.applicationDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.arithmeticDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.divideByZeroDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.indexOutOfRangeDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.invalidCastDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.invalidOperationDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.ioDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.nullReferenceDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.outOfMemoryDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.overflowDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.systemDelegate);
				RAIL_API_PINVOKE.SWIGExceptionHelper.SWIGRegisterExceptionCallbacksArgument_rail_api(RAIL_API_PINVOKE.SWIGExceptionHelper.argumentDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.argumentNullDelegate, RAIL_API_PINVOKE.SWIGExceptionHelper.argumentOutOfRangeDelegate);
			}

			// Token: 0x06001914 RID: 6420 RVA: 0x00002119 File Offset: 0x00000319
			public SWIGExceptionHelper()
			{
			}

			// Token: 0x040005EA RID: 1514
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate applicationDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingApplicationException);

			// Token: 0x040005EB RID: 1515
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate arithmeticDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingArithmeticException);

			// Token: 0x040005EC RID: 1516
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate divideByZeroDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingDivideByZeroException);

			// Token: 0x040005ED RID: 1517
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate indexOutOfRangeDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingIndexOutOfRangeException);

			// Token: 0x040005EE RID: 1518
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate invalidCastDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingInvalidCastException);

			// Token: 0x040005EF RID: 1519
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate invalidOperationDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingInvalidOperationException);

			// Token: 0x040005F0 RID: 1520
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate ioDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingIOException);

			// Token: 0x040005F1 RID: 1521
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate nullReferenceDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingNullReferenceException);

			// Token: 0x040005F2 RID: 1522
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate outOfMemoryDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingOutOfMemoryException);

			// Token: 0x040005F3 RID: 1523
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate overflowDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingOverflowException);

			// Token: 0x040005F4 RID: 1524
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate systemDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingSystemException);

			// Token: 0x040005F5 RID: 1525
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingArgumentException);

			// Token: 0x040005F6 RID: 1526
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentNullDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingArgumentNullException);

			// Token: 0x040005F7 RID: 1527
			private static RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate argumentOutOfRangeDelegate = new RAIL_API_PINVOKE.SWIGExceptionHelper.ExceptionArgumentDelegate(RAIL_API_PINVOKE.SWIGExceptionHelper.SetPendingArgumentOutOfRangeException);

			// Token: 0x020001B5 RID: 437
			// (Invoke) Token: 0x0600191F RID: 6431
			public delegate void ExceptionDelegate(string message);

			// Token: 0x020001B6 RID: 438
			// (Invoke) Token: 0x06001923 RID: 6435
			public delegate void ExceptionArgumentDelegate(string message, string paramName);
		}

		// Token: 0x020001B3 RID: 435
		public class SWIGPendingException
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06001915 RID: 6421 RVA: 0x0001150C File Offset: 0x0000F70C
			public static bool Pending
			{
				get
				{
					bool flag = false;
					if (RAIL_API_PINVOKE.SWIGPendingException.numExceptionsPending > 0 && RAIL_API_PINVOKE.SWIGPendingException.pendingException != null)
					{
						flag = true;
					}
					return flag;
				}
			}

			// Token: 0x06001916 RID: 6422 RVA: 0x00011530 File Offset: 0x0000F730
			public static void Set(Exception e)
			{
				if (RAIL_API_PINVOKE.SWIGPendingException.pendingException != null)
				{
					throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + RAIL_API_PINVOKE.SWIGPendingException.pendingException.ToString() + ")", e);
				}
				RAIL_API_PINVOKE.SWIGPendingException.pendingException = e;
				Type typeFromHandle = typeof(RAIL_API_PINVOKE);
				lock (typeFromHandle)
				{
					RAIL_API_PINVOKE.SWIGPendingException.numExceptionsPending++;
				}
			}

			// Token: 0x06001917 RID: 6423 RVA: 0x000115A8 File Offset: 0x0000F7A8
			public static Exception Retrieve()
			{
				Exception ex = null;
				if (RAIL_API_PINVOKE.SWIGPendingException.numExceptionsPending > 0 && RAIL_API_PINVOKE.SWIGPendingException.pendingException != null)
				{
					ex = RAIL_API_PINVOKE.SWIGPendingException.pendingException;
					RAIL_API_PINVOKE.SWIGPendingException.pendingException = null;
					Type typeFromHandle = typeof(RAIL_API_PINVOKE);
					lock (typeFromHandle)
					{
						RAIL_API_PINVOKE.SWIGPendingException.numExceptionsPending--;
					}
				}
				return ex;
			}

			// Token: 0x06001918 RID: 6424 RVA: 0x00002119 File Offset: 0x00000319
			public SWIGPendingException()
			{
			}

			// Token: 0x06001919 RID: 6425 RVA: 0x000020FA File Offset: 0x000002FA
			// Note: this type is marked as 'beforefieldinit'.
			static SWIGPendingException()
			{
			}

			// Token: 0x040005F8 RID: 1528
			[ThreadStatic]
			private static Exception pendingException;

			// Token: 0x040005F9 RID: 1529
			private static int numExceptionsPending;
		}

		// Token: 0x020001B4 RID: 436
		protected class SWIGStringHelper
		{
			// Token: 0x0600191A RID: 6426
			[DllImport("rail_api")]
			public static extern void SWIGRegisterStringCallback_rail_api(RAIL_API_PINVOKE.SWIGStringHelper.SWIGStringDelegate stringDelegate);

			// Token: 0x0600191B RID: 6427 RVA: 0x00011610 File Offset: 0x0000F810
			private static string CreateString(string cString)
			{
				return cString;
			}

			// Token: 0x0600191C RID: 6428 RVA: 0x00011613 File Offset: 0x0000F813
			static SWIGStringHelper()
			{
				RAIL_API_PINVOKE.SWIGStringHelper.SWIGRegisterStringCallback_rail_api(RAIL_API_PINVOKE.SWIGStringHelper.stringDelegate);
			}

			// Token: 0x0600191D RID: 6429 RVA: 0x00002119 File Offset: 0x00000319
			public SWIGStringHelper()
			{
			}

			// Token: 0x040005FA RID: 1530
			private static RAIL_API_PINVOKE.SWIGStringHelper.SWIGStringDelegate stringDelegate = new RAIL_API_PINVOKE.SWIGStringHelper.SWIGStringDelegate(RAIL_API_PINVOKE.SWIGStringHelper.CreateString);

			// Token: 0x020001B7 RID: 439
			// (Invoke) Token: 0x06001927 RID: 6439
			public delegate string SWIGStringDelegate(string message);
		}
	}
}
