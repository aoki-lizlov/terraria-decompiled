using System;

namespace rail
{
	// Token: 0x0200008B RID: 139
	public enum RAILEventID
	{
		// Token: 0x040000CD RID: 205
		kRailEventBegin,
		// Token: 0x040000CE RID: 206
		kRailEventFinalize,
		// Token: 0x040000CF RID: 207
		kRailEventSystemStateChanged,
		// Token: 0x040000D0 RID: 208
		kRailPlatformNotifyEventJoinGameByGameServer = 100,
		// Token: 0x040000D1 RID: 209
		kRailPlatformNotifyEventJoinGameByRoom,
		// Token: 0x040000D2 RID: 210
		kRailPlatformNotifyEventJoinGameByUser,
		// Token: 0x040000D3 RID: 211
		kRailEventStats = 2000,
		// Token: 0x040000D4 RID: 212
		kRailEventStatsPlayerStatsReceived,
		// Token: 0x040000D5 RID: 213
		kRailEventStatsPlayerStatsStored,
		// Token: 0x040000D6 RID: 214
		kRailEventStatsNumOfPlayerReceived,
		// Token: 0x040000D7 RID: 215
		kRailEventStatsGlobalStatsReceived,
		// Token: 0x040000D8 RID: 216
		kRailEventAchievement = 2100,
		// Token: 0x040000D9 RID: 217
		kRailEventAchievementPlayerAchievementReceived,
		// Token: 0x040000DA RID: 218
		kRailEventAchievementPlayerAchievementStored,
		// Token: 0x040000DB RID: 219
		kRailEventAchievementGlobalAchievementReceived,
		// Token: 0x040000DC RID: 220
		kRailEventLeaderboard = 2200,
		// Token: 0x040000DD RID: 221
		kRailEventLeaderboardReceived,
		// Token: 0x040000DE RID: 222
		kRailEventLeaderboardEntryReceived,
		// Token: 0x040000DF RID: 223
		kRailEventLeaderboardUploaded,
		// Token: 0x040000E0 RID: 224
		kRailEventLeaderboardAttachSpaceWork,
		// Token: 0x040000E1 RID: 225
		kRailEventLeaderboardAsyncCreated,
		// Token: 0x040000E2 RID: 226
		kRailEventGameServer = 3000,
		// Token: 0x040000E3 RID: 227
		kRailEventGameServerListResult,
		// Token: 0x040000E4 RID: 228
		kRailEventGameServerCreated,
		// Token: 0x040000E5 RID: 229
		kRailEventGameServerSetMetadataResult,
		// Token: 0x040000E6 RID: 230
		kRailEventGameServerGetMetadataResult,
		// Token: 0x040000E7 RID: 231
		kRailEventGameServerGetSessionTicket,
		// Token: 0x040000E8 RID: 232
		kRailEventGameServerAuthSessionTicket,
		// Token: 0x040000E9 RID: 233
		kRailEventGameServerPlayerListResult,
		// Token: 0x040000EA RID: 234
		kRailEventGameServerRegisterToServerListResult,
		// Token: 0x040000EB RID: 235
		kRailEventGameServerFavoriteGameServers,
		// Token: 0x040000EC RID: 236
		kRailEventGameServerAddFavoriteGameServer,
		// Token: 0x040000ED RID: 237
		kRailEventGameServerRemoveFavoriteGameServer,
		// Token: 0x040000EE RID: 238
		kRailEventUserSpace = 4000,
		// Token: 0x040000EF RID: 239
		kRailEventUserSpaceGetMySubscribedWorksResult,
		// Token: 0x040000F0 RID: 240
		kRailEventUserSpaceGetMyFavoritesWorksResult,
		// Token: 0x040000F1 RID: 241
		kRailEventUserSpaceQuerySpaceWorksResult,
		// Token: 0x040000F2 RID: 242
		kRailEventUserSpaceUpdateMetadataResult,
		// Token: 0x040000F3 RID: 243
		kRailEventUserSpaceSyncResult,
		// Token: 0x040000F4 RID: 244
		kRailEventUserSpaceSubscribeResult,
		// Token: 0x040000F5 RID: 245
		kRailEventUserSpaceModifyFavoritesWorksResult,
		// Token: 0x040000F6 RID: 246
		kRailEventUserSpaceRemoveSpaceWorkResult,
		// Token: 0x040000F7 RID: 247
		kRailEventUserSpaceVoteSpaceWorkResult,
		// Token: 0x040000F8 RID: 248
		kRailEventUserSpaceSearchSpaceWorkResult,
		// Token: 0x040000F9 RID: 249
		kRailEventNetChannel = 5000,
		// Token: 0x040000FA RID: 250
		kRailEventNetChannelCreateChannelResult,
		// Token: 0x040000FB RID: 251
		kRailEventNetChannelInviteJoinChannelRequest,
		// Token: 0x040000FC RID: 252
		kRailEventNetChannelJoinChannelResult,
		// Token: 0x040000FD RID: 253
		kRailEventNetChannelChannelException,
		// Token: 0x040000FE RID: 254
		kRailEventNetChannelChannelNetDelay,
		// Token: 0x040000FF RID: 255
		kRailEventNetChannelInviteMemmberResult,
		// Token: 0x04000100 RID: 256
		kRailEventNetChannelMemberStateChanged,
		// Token: 0x04000101 RID: 257
		kRailEventStorageBegin = 6000,
		// Token: 0x04000102 RID: 258
		kRailEventStorageQueryQuotaResult,
		// Token: 0x04000103 RID: 259
		kRailEventStorageShareToSpaceWorkResult,
		// Token: 0x04000104 RID: 260
		kRailEventStorageAsyncReadFileResult,
		// Token: 0x04000105 RID: 261
		kRailEventStorageAsyncWriteFileResult,
		// Token: 0x04000106 RID: 262
		kRailEventStorageAsyncListStreamFileResult,
		// Token: 0x04000107 RID: 263
		kRailEventStorageAsyncRenameStreamFileResult,
		// Token: 0x04000108 RID: 264
		kRailEventStorageAsyncDeleteStreamFileResult,
		// Token: 0x04000109 RID: 265
		kRailEventStorageAsyncReadStreamFileResult,
		// Token: 0x0400010A RID: 266
		kRailEventStorageAsyncWriteStreamFileResult,
		// Token: 0x0400010B RID: 267
		kRailEventAssetsBegin = 7000,
		// Token: 0x0400010C RID: 268
		kRailEventAssetsRequestAllAssetsFinished,
		// Token: 0x0400010D RID: 269
		kRailEventAssetsCompleteConsumeByExchangeAssetsToFinished,
		// Token: 0x0400010E RID: 270
		kRailEventAssetsExchangeAssetsFinished,
		// Token: 0x0400010F RID: 271
		kRailEventAssetsExchangeAssetsToFinished,
		// Token: 0x04000110 RID: 272
		kRailEventAssetsDirectConsumeFinished,
		// Token: 0x04000111 RID: 273
		kRailEventAssetsStartConsumeFinished,
		// Token: 0x04000112 RID: 274
		kRailEventAssetsUpdateConsumeFinished,
		// Token: 0x04000113 RID: 275
		kRailEventAssetsCompleteConsumeFinished,
		// Token: 0x04000114 RID: 276
		kRailEventAssetsSplitFinished,
		// Token: 0x04000115 RID: 277
		kRailEventAssetsSplitToFinished,
		// Token: 0x04000116 RID: 278
		kRailEventAssetsMergeFinished,
		// Token: 0x04000117 RID: 279
		kRailEventAssetsMergeToFinished,
		// Token: 0x04000118 RID: 280
		kRailEventAssetsRequestAllProductFinished,
		// Token: 0x04000119 RID: 281
		kRailEventAssetsUpdateAssetPropertyFinished,
		// Token: 0x0400011A RID: 282
		kRailEventUtilsBegin = 8000,
		// Token: 0x0400011B RID: 283
		kRailEventUtilsSignatureResult = 8002,
		// Token: 0x0400011C RID: 284
		kRailEventUtilsGetImageDataResult,
		// Token: 0x0400011D RID: 285
		kRailEventInGamePurchaseBegin = 9000,
		// Token: 0x0400011E RID: 286
		kRailEventInGamePurchaseAllProductsInfoReceived,
		// Token: 0x0400011F RID: 287
		kRailEventInGamePurchaseAllPurchasableProductsInfoReceived,
		// Token: 0x04000120 RID: 288
		kRailEventInGamePurchasePurchaseProductsResult,
		// Token: 0x04000121 RID: 289
		kRailEventInGamePurchaseFinishOrderResult,
		// Token: 0x04000122 RID: 290
		kRailEventInGamePurchasePurchaseProductsToAssetsResult,
		// Token: 0x04000123 RID: 291
		kRailEventInGameStorePurchaseBegin = 9500,
		// Token: 0x04000124 RID: 292
		kRailEventInGameStorePurchasePayWindowDisplayed,
		// Token: 0x04000125 RID: 293
		kRailEventInGameStorePurchasePayWindowClosed,
		// Token: 0x04000126 RID: 294
		kRailEventInGameStorePurchasePaymentResult,
		// Token: 0x04000127 RID: 295
		kRailEventRoom = 10000,
		// Token: 0x04000128 RID: 296
		kRailEventRoomZoneListResult,
		// Token: 0x04000129 RID: 297
		kRailEventRoomListResult,
		// Token: 0x0400012A RID: 298
		kRailEventRoomCreated,
		// Token: 0x0400012B RID: 299
		kRailEventRoomGotRoomMembers,
		// Token: 0x0400012C RID: 300
		kRailEventRoomJoinRoomResult,
		// Token: 0x0400012D RID: 301
		kRailEventRoomKickOffMemberResult,
		// Token: 0x0400012E RID: 302
		kRailEventRoomSetRoomMetadataResult,
		// Token: 0x0400012F RID: 303
		kRailEventRoomGetRoomMetadataResult,
		// Token: 0x04000130 RID: 304
		kRailEventRoomGetMemberMetadataResult,
		// Token: 0x04000131 RID: 305
		kRailEventRoomSetMemberMetadataResult,
		// Token: 0x04000132 RID: 306
		kRailEventRoomLeaveRoomResult,
		// Token: 0x04000133 RID: 307
		kRailEventRoomGetAllDataResult,
		// Token: 0x04000134 RID: 308
		kRailEventRoomGetUserRoomListResult,
		// Token: 0x04000135 RID: 309
		kRailEventRoomClearRoomMetadataResult,
		// Token: 0x04000136 RID: 310
		kRailEventRoomNotify = 11000,
		// Token: 0x04000137 RID: 311
		kRailEventRoomNotifyMetadataChanged,
		// Token: 0x04000138 RID: 312
		kRailEventRoomNotifyMemberChanged,
		// Token: 0x04000139 RID: 313
		kRailEventRoomNotifyMemberkicked,
		// Token: 0x0400013A RID: 314
		kRailEventRoomNotifyRoomDestroyed,
		// Token: 0x0400013B RID: 315
		kRailEventRoomNotifyRoomOwnerChanged,
		// Token: 0x0400013C RID: 316
		kRailEventRoomNotifyRoomDataReceived,
		// Token: 0x0400013D RID: 317
		kRailEventRoomNotifyRoomGameServerChanged,
		// Token: 0x0400013E RID: 318
		kRailEventFriend = 12000,
		// Token: 0x0400013F RID: 319
		kRailEventFriendsDialogShow,
		// Token: 0x04000140 RID: 320
		kRailEventFriendsSetMetadataResult,
		// Token: 0x04000141 RID: 321
		kRailEventFriendsGetMetadataResult,
		// Token: 0x04000142 RID: 322
		kRailEventFriendsClearMetadataResult,
		// Token: 0x04000143 RID: 323
		kRailEventFriendsGetInviteCommandLine,
		// Token: 0x04000144 RID: 324
		kRailEventFriendsReportPlayedWithUserListResult,
		// Token: 0x04000145 RID: 325
		kRailEventFriendsFriendsListChanged = 12010,
		// Token: 0x04000146 RID: 326
		kRailEventFriendsGetFriendPlayedGamesResult,
		// Token: 0x04000147 RID: 327
		kRailEventFriendsQueryPlayedWithFriendsListResult,
		// Token: 0x04000148 RID: 328
		kRailEventFriendsQueryPlayedWithFriendsTimeResult,
		// Token: 0x04000149 RID: 329
		kRailEventFriendsQueryPlayedWithFriendsGamesResult,
		// Token: 0x0400014A RID: 330
		kRailEventFriendsAddFriendResult,
		// Token: 0x0400014B RID: 331
		kRailEventFriendsOnlineStateChanged,
		// Token: 0x0400014C RID: 332
		kRailEventSessionTicket = 13000,
		// Token: 0x0400014D RID: 333
		kRailEventSessionTicketGetSessionTicket,
		// Token: 0x0400014E RID: 334
		kRailEventSessionTicketAuthSessionTicket,
		// Token: 0x0400014F RID: 335
		kRailEventPlayerGetGamePurchaseKey,
		// Token: 0x04000150 RID: 336
		kRailEventQueryPlayerBannedStatus,
		// Token: 0x04000151 RID: 337
		kRailEventPlayerGetAuthenticateURL,
		// Token: 0x04000152 RID: 338
		kRailEventPlayerAntiAddictionGameOnlineTimeChanged,
		// Token: 0x04000153 RID: 339
		kRailEventUsersGetUsersInfo = 13501,
		// Token: 0x04000154 RID: 340
		kRailEventUsersNotifyInviter,
		// Token: 0x04000155 RID: 341
		kRailEventUsersRespondInvitation,
		// Token: 0x04000156 RID: 342
		kRailEventUsersInviteJoinGameResult,
		// Token: 0x04000157 RID: 343
		kRailEventUsersInviteUsersResult,
		// Token: 0x04000158 RID: 344
		kRailEventUsersGetInviteDetailResult,
		// Token: 0x04000159 RID: 345
		kRailEventUsersCancelInviteResult,
		// Token: 0x0400015A RID: 346
		kRailEventUsersGetUserLimitsResult,
		// Token: 0x0400015B RID: 347
		kRailEventUsersShowChatWindowWithFriendResult,
		// Token: 0x0400015C RID: 348
		kRailEventUsersShowUserHomepageWindowResult,
		// Token: 0x0400015D RID: 349
		kRailEventShowFloatingWindow = 14000,
		// Token: 0x0400015E RID: 350
		kRailEventShowFloatingNotifyWindow,
		// Token: 0x0400015F RID: 351
		kRailEventBrowser = 15000,
		// Token: 0x04000160 RID: 352
		kRailEventBrowserCreateResult,
		// Token: 0x04000161 RID: 353
		kRailEventBrowserReloadResult,
		// Token: 0x04000162 RID: 354
		kRailEventBrowserCloseResult,
		// Token: 0x04000163 RID: 355
		kRailEventBrowserJavascriptEvent,
		// Token: 0x04000164 RID: 356
		kRailEventBrowserTryNavigateNewPageRequest,
		// Token: 0x04000165 RID: 357
		kRailEventBrowserPaint,
		// Token: 0x04000166 RID: 358
		kRailEventBrowserDamageRectPaint,
		// Token: 0x04000167 RID: 359
		kRailEventBrowserNavigeteResult,
		// Token: 0x04000168 RID: 360
		kRailEventBrowserStateChanged,
		// Token: 0x04000169 RID: 361
		kRailEventBrowserTitleChanged,
		// Token: 0x0400016A RID: 362
		kRailEventNetwork = 16000,
		// Token: 0x0400016B RID: 363
		kRailEventNetworkCreateSessionRequest,
		// Token: 0x0400016C RID: 364
		kRailEventNetworkCreateSessionFailed,
		// Token: 0x0400016D RID: 365
		kRailEventDlcBegin = 17000,
		// Token: 0x0400016E RID: 366
		kRailEventDlcInstallStart,
		// Token: 0x0400016F RID: 367
		kRailEventDlcInstallStartResult,
		// Token: 0x04000170 RID: 368
		kRailEventDlcInstallProgress,
		// Token: 0x04000171 RID: 369
		kRailEventDlcInstallFinished,
		// Token: 0x04000172 RID: 370
		kRailEventDlcUninstallFinished,
		// Token: 0x04000173 RID: 371
		kRailEventDlcCheckAllDlcsStateReadyResult,
		// Token: 0x04000174 RID: 372
		kRailEventDlcQueryIsOwnedDlcsResult,
		// Token: 0x04000175 RID: 373
		kRailEventDlcOwnershipChanged,
		// Token: 0x04000176 RID: 374
		kRailEventDlcRefundChanged,
		// Token: 0x04000177 RID: 375
		kRailEventScreenshot = 18000,
		// Token: 0x04000178 RID: 376
		kRailEventScreenshotTakeScreenshotFinished,
		// Token: 0x04000179 RID: 377
		kRailEventScreenshotTakeScreenshotRequest,
		// Token: 0x0400017A RID: 378
		kRailEventScreenshotPublishScreenshotFinished,
		// Token: 0x0400017B RID: 379
		kRailEventVoiceChannel = 19000,
		// Token: 0x0400017C RID: 380
		kRailEventVoiceChannelCreateResult,
		// Token: 0x0400017D RID: 381
		kRailEventVoiceChannelDataCaptured,
		// Token: 0x0400017E RID: 382
		kRailEventVoiceChannelJoinedResult,
		// Token: 0x0400017F RID: 383
		kRailEventVoiceChannelLeaveResult,
		// Token: 0x04000180 RID: 384
		kRailEventVoiceChannelAddUsersResult,
		// Token: 0x04000181 RID: 385
		kRailEventVoiceChannelRemoveUsersResult,
		// Token: 0x04000182 RID: 386
		kRailEventVoiceChannelInviteEvent,
		// Token: 0x04000183 RID: 387
		kRailEventVoiceChannelMemberChangedEvent,
		// Token: 0x04000184 RID: 388
		kRailEventVoiceChannelUsersSpeakingStateChangedEvent,
		// Token: 0x04000185 RID: 389
		kRailEventVoiceChannelPushToTalkKeyChangedEvent,
		// Token: 0x04000186 RID: 390
		kRailEventVoiceChannelSpeakingUsersChangedEvent,
		// Token: 0x04000187 RID: 391
		kRailEventAppBegin = 20000,
		// Token: 0x04000188 RID: 392
		kRailEventAppQuerySubscribeWishPlayStateResult,
		// Token: 0x04000189 RID: 393
		kRailEventTextInputBegin = 21000,
		// Token: 0x0400018A RID: 394
		kRailEventTextInputShowTextInputWindowResult,
		// Token: 0x0400018B RID: 395
		kRailEventIMEHelperTextInputBegin = 22000,
		// Token: 0x0400018C RID: 396
		kRailEventIMEHelperTextInputSelectedResult,
		// Token: 0x0400018D RID: 397
		kRailEventHttpSessionBegin = 23000,
		// Token: 0x0400018E RID: 398
		kRailEventHttpSessionResponseResult,
		// Token: 0x0400018F RID: 399
		kRailEventSmallObjectServiceBegin = 24000,
		// Token: 0x04000190 RID: 400
		kRailEventSmallObjectServiceQueryObjectStateResult,
		// Token: 0x04000191 RID: 401
		kRailEventSmallObjectServiceDownloadResult,
		// Token: 0x04000192 RID: 402
		kRailEventZoneServerBegin = 25000,
		// Token: 0x04000193 RID: 403
		kRailEventZoneServerSwitchPlayerSelectedZoneResult,
		// Token: 0x04000194 RID: 404
		kCustomEventBegin = 10000000
	}
}
