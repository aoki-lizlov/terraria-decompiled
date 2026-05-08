using System;

namespace Steamworks
{
	// Token: 0x0200016B RID: 363
	public enum ESteamNetworkingConfigValue
	{
		// Token: 0x0400097A RID: 2426
		k_ESteamNetworkingConfig_Invalid,
		// Token: 0x0400097B RID: 2427
		k_ESteamNetworkingConfig_TimeoutInitial = 24,
		// Token: 0x0400097C RID: 2428
		k_ESteamNetworkingConfig_TimeoutConnected,
		// Token: 0x0400097D RID: 2429
		k_ESteamNetworkingConfig_SendBufferSize = 9,
		// Token: 0x0400097E RID: 2430
		k_ESteamNetworkingConfig_RecvBufferSize = 47,
		// Token: 0x0400097F RID: 2431
		k_ESteamNetworkingConfig_RecvBufferMessages,
		// Token: 0x04000980 RID: 2432
		k_ESteamNetworkingConfig_RecvMaxMessageSize,
		// Token: 0x04000981 RID: 2433
		k_ESteamNetworkingConfig_RecvMaxSegmentsPerPacket,
		// Token: 0x04000982 RID: 2434
		k_ESteamNetworkingConfig_ConnectionUserData = 40,
		// Token: 0x04000983 RID: 2435
		k_ESteamNetworkingConfig_SendRateMin = 10,
		// Token: 0x04000984 RID: 2436
		k_ESteamNetworkingConfig_SendRateMax,
		// Token: 0x04000985 RID: 2437
		k_ESteamNetworkingConfig_NagleTime,
		// Token: 0x04000986 RID: 2438
		k_ESteamNetworkingConfig_IP_AllowWithoutAuth = 23,
		// Token: 0x04000987 RID: 2439
		k_ESteamNetworkingConfig_IPLocalHost_AllowWithoutAuth = 52,
		// Token: 0x04000988 RID: 2440
		k_ESteamNetworkingConfig_MTU_PacketSize = 32,
		// Token: 0x04000989 RID: 2441
		k_ESteamNetworkingConfig_MTU_DataSize,
		// Token: 0x0400098A RID: 2442
		k_ESteamNetworkingConfig_Unencrypted,
		// Token: 0x0400098B RID: 2443
		k_ESteamNetworkingConfig_SymmetricConnect = 37,
		// Token: 0x0400098C RID: 2444
		k_ESteamNetworkingConfig_LocalVirtualPort,
		// Token: 0x0400098D RID: 2445
		k_ESteamNetworkingConfig_DualWifi_Enable,
		// Token: 0x0400098E RID: 2446
		k_ESteamNetworkingConfig_EnableDiagnosticsUI = 46,
		// Token: 0x0400098F RID: 2447
		k_ESteamNetworkingConfig_FakePacketLoss_Send = 2,
		// Token: 0x04000990 RID: 2448
		k_ESteamNetworkingConfig_FakePacketLoss_Recv,
		// Token: 0x04000991 RID: 2449
		k_ESteamNetworkingConfig_FakePacketLag_Send,
		// Token: 0x04000992 RID: 2450
		k_ESteamNetworkingConfig_FakePacketLag_Recv,
		// Token: 0x04000993 RID: 2451
		k_ESteamNetworkingConfig_FakePacketReorder_Send,
		// Token: 0x04000994 RID: 2452
		k_ESteamNetworkingConfig_FakePacketReorder_Recv,
		// Token: 0x04000995 RID: 2453
		k_ESteamNetworkingConfig_FakePacketReorder_Time,
		// Token: 0x04000996 RID: 2454
		k_ESteamNetworkingConfig_FakePacketDup_Send = 26,
		// Token: 0x04000997 RID: 2455
		k_ESteamNetworkingConfig_FakePacketDup_Recv,
		// Token: 0x04000998 RID: 2456
		k_ESteamNetworkingConfig_FakePacketDup_TimeMax,
		// Token: 0x04000999 RID: 2457
		k_ESteamNetworkingConfig_PacketTraceMaxBytes = 41,
		// Token: 0x0400099A RID: 2458
		k_ESteamNetworkingConfig_FakeRateLimit_Send_Rate,
		// Token: 0x0400099B RID: 2459
		k_ESteamNetworkingConfig_FakeRateLimit_Send_Burst,
		// Token: 0x0400099C RID: 2460
		k_ESteamNetworkingConfig_FakeRateLimit_Recv_Rate,
		// Token: 0x0400099D RID: 2461
		k_ESteamNetworkingConfig_FakeRateLimit_Recv_Burst,
		// Token: 0x0400099E RID: 2462
		k_ESteamNetworkingConfig_OutOfOrderCorrectionWindowMicroseconds = 51,
		// Token: 0x0400099F RID: 2463
		k_ESteamNetworkingConfig_Callback_ConnectionStatusChanged = 201,
		// Token: 0x040009A0 RID: 2464
		k_ESteamNetworkingConfig_Callback_AuthStatusChanged,
		// Token: 0x040009A1 RID: 2465
		k_ESteamNetworkingConfig_Callback_RelayNetworkStatusChanged,
		// Token: 0x040009A2 RID: 2466
		k_ESteamNetworkingConfig_Callback_MessagesSessionRequest,
		// Token: 0x040009A3 RID: 2467
		k_ESteamNetworkingConfig_Callback_MessagesSessionFailed,
		// Token: 0x040009A4 RID: 2468
		k_ESteamNetworkingConfig_Callback_CreateConnectionSignaling,
		// Token: 0x040009A5 RID: 2469
		k_ESteamNetworkingConfig_Callback_FakeIPResult,
		// Token: 0x040009A6 RID: 2470
		k_ESteamNetworkingConfig_P2P_STUN_ServerList = 103,
		// Token: 0x040009A7 RID: 2471
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Enable,
		// Token: 0x040009A8 RID: 2472
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Penalty,
		// Token: 0x040009A9 RID: 2473
		k_ESteamNetworkingConfig_P2P_Transport_SDR_Penalty,
		// Token: 0x040009AA RID: 2474
		k_ESteamNetworkingConfig_P2P_TURN_ServerList,
		// Token: 0x040009AB RID: 2475
		k_ESteamNetworkingConfig_P2P_TURN_UserList,
		// Token: 0x040009AC RID: 2476
		k_ESteamNetworkingConfig_P2P_TURN_PassList,
		// Token: 0x040009AD RID: 2477
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Implementation,
		// Token: 0x040009AE RID: 2478
		k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,
		// Token: 0x040009AF RID: 2479
		k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFail,
		// Token: 0x040009B0 RID: 2480
		k_ESteamNetworkingConfig_SDRClient_MinPingsBeforePingAccurate,
		// Token: 0x040009B1 RID: 2481
		k_ESteamNetworkingConfig_SDRClient_SingleSocket,
		// Token: 0x040009B2 RID: 2482
		k_ESteamNetworkingConfig_SDRClient_ForceRelayCluster = 29,
		// Token: 0x040009B3 RID: 2483
		k_ESteamNetworkingConfig_SDRClient_DevTicket,
		// Token: 0x040009B4 RID: 2484
		k_ESteamNetworkingConfig_SDRClient_ForceProxyAddr,
		// Token: 0x040009B5 RID: 2485
		k_ESteamNetworkingConfig_SDRClient_FakeClusterPing = 36,
		// Token: 0x040009B6 RID: 2486
		k_ESteamNetworkingConfig_SDRClient_LimitPingProbesToNearestN = 60,
		// Token: 0x040009B7 RID: 2487
		k_ESteamNetworkingConfig_LogLevel_AckRTT = 13,
		// Token: 0x040009B8 RID: 2488
		k_ESteamNetworkingConfig_LogLevel_PacketDecode,
		// Token: 0x040009B9 RID: 2489
		k_ESteamNetworkingConfig_LogLevel_Message,
		// Token: 0x040009BA RID: 2490
		k_ESteamNetworkingConfig_LogLevel_PacketGaps,
		// Token: 0x040009BB RID: 2491
		k_ESteamNetworkingConfig_LogLevel_P2PRendezvous,
		// Token: 0x040009BC RID: 2492
		k_ESteamNetworkingConfig_LogLevel_SDRRelayPings,
		// Token: 0x040009BD RID: 2493
		k_ESteamNetworkingConfig_ECN = 999,
		// Token: 0x040009BE RID: 2494
		k_ESteamNetworkingConfig_DELETED_EnumerateDevVars = 35,
		// Token: 0x040009BF RID: 2495
		k_ESteamNetworkingConfigValue__Force32Bit = 2147483647
	}
}
