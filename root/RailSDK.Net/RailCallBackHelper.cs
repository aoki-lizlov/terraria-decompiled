using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200006C RID: 108
	public class RailCallBackHelper
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x00002119 File Offset: 0x00000319
		public RailCallBackHelper()
		{
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0000F20C File Offset: 0x0000D40C
		~RailCallBackHelper()
		{
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0000F234 File Offset: 0x0000D434
		public void RegisterCallback(RAILEventID event_id, RailEventCallBackHandler handler)
		{
			object obj = RailCallBackHelper.locker_;
			lock (obj)
			{
				if (RailCallBackHelper.eventHandlers_.ContainsKey(event_id))
				{
					Dictionary<RAILEventID, RailEventCallBackHandler> dictionary = RailCallBackHelper.eventHandlers_;
					dictionary[event_id] = (RailEventCallBackHandler)Delegate.Combine(dictionary[event_id], handler);
				}
				else
				{
					RailCallBackHelper.eventHandlers_.Add(event_id, handler);
					RAIL_API_PINVOKE.CSharpRailRegisterEvent((int)event_id, RailCallBackHelper.delegate_);
				}
			}
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		public void UnregisterCallback(RAILEventID event_id, RailEventCallBackHandler handler)
		{
			object obj = RailCallBackHelper.locker_;
			lock (obj)
			{
				if (RailCallBackHelper.eventHandlers_.ContainsKey(event_id))
				{
					Dictionary<RAILEventID, RailEventCallBackHandler> dictionary = RailCallBackHelper.eventHandlers_;
					dictionary[event_id] = (RailEventCallBackHandler)Delegate.Remove(dictionary[event_id], handler);
					if (RailCallBackHelper.eventHandlers_[event_id] == null)
					{
						RAIL_API_PINVOKE.CSharpRailUnRegisterEvent((int)event_id, RailCallBackHelper.delegate_);
						RailCallBackHelper.eventHandlers_.Remove(event_id);
					}
				}
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0000F340 File Offset: 0x0000D540
		public void UnregisterCallback(RAILEventID event_id)
		{
			object obj = RailCallBackHelper.locker_;
			lock (obj)
			{
				RAIL_API_PINVOKE.CSharpRailUnRegisterEvent((int)event_id, RailCallBackHelper.delegate_);
				if (RailCallBackHelper.eventHandlers_[event_id] != null)
				{
					RailCallBackHelper.eventHandlers_.Remove(event_id);
				}
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public void UnregisterAllCallback()
		{
			object obj = RailCallBackHelper.locker_;
			lock (obj)
			{
				RAIL_API_PINVOKE.CSharpRailUnRegisterAllEvent();
				RailCallBackHelper.eventHandlers_.Clear();
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public static void OnRailCallBack(RAILEventID event_id, IntPtr data)
		{
			RailEventCallBackHandler railEventCallBackHandler = RailCallBackHelper.eventHandlers_[event_id];
			if (railEventCallBackHandler != null)
			{
				if (event_id <= RAILEventID.kRailEventFriendsOnlineStateChanged)
				{
					if (event_id <= RAILEventID.kRailEventUserSpaceSearchSpaceWorkResult)
					{
						if (event_id <= RAILEventID.kRailEventStatsGlobalStatsReceived)
						{
							if (event_id <= RAILEventID.kRailEventSystemStateChanged)
							{
								if (event_id == RAILEventID.kRailEventFinalize)
								{
									RailFinalize railFinalize = new RailFinalize();
									RailConverter.Cpp2Csharp(data, railFinalize);
									railEventCallBackHandler(event_id, railFinalize);
									return;
								}
								if (event_id != RAILEventID.kRailEventSystemStateChanged)
								{
									return;
								}
								RailSystemStateChanged railSystemStateChanged = new RailSystemStateChanged();
								RailConverter.Cpp2Csharp(data, railSystemStateChanged);
								railEventCallBackHandler(event_id, railSystemStateChanged);
								return;
							}
							else
							{
								switch (event_id)
								{
								case RAILEventID.kRailPlatformNotifyEventJoinGameByGameServer:
								{
									RailPlatformNotifyEventJoinGameByGameServer railPlatformNotifyEventJoinGameByGameServer = new RailPlatformNotifyEventJoinGameByGameServer();
									RailConverter.Cpp2Csharp(data, railPlatformNotifyEventJoinGameByGameServer);
									railEventCallBackHandler(event_id, railPlatformNotifyEventJoinGameByGameServer);
									return;
								}
								case RAILEventID.kRailPlatformNotifyEventJoinGameByRoom:
								{
									RailPlatformNotifyEventJoinGameByRoom railPlatformNotifyEventJoinGameByRoom = new RailPlatformNotifyEventJoinGameByRoom();
									RailConverter.Cpp2Csharp(data, railPlatformNotifyEventJoinGameByRoom);
									railEventCallBackHandler(event_id, railPlatformNotifyEventJoinGameByRoom);
									return;
								}
								case RAILEventID.kRailPlatformNotifyEventJoinGameByUser:
								{
									RailPlatformNotifyEventJoinGameByUser railPlatformNotifyEventJoinGameByUser = new RailPlatformNotifyEventJoinGameByUser();
									RailConverter.Cpp2Csharp(data, railPlatformNotifyEventJoinGameByUser);
									railEventCallBackHandler(event_id, railPlatformNotifyEventJoinGameByUser);
									return;
								}
								default:
									switch (event_id)
									{
									case RAILEventID.kRailEventStatsPlayerStatsReceived:
									{
										PlayerStatsReceived playerStatsReceived = new PlayerStatsReceived();
										RailConverter.Cpp2Csharp(data, playerStatsReceived);
										railEventCallBackHandler(event_id, playerStatsReceived);
										return;
									}
									case RAILEventID.kRailEventStatsPlayerStatsStored:
									{
										PlayerStatsStored playerStatsStored = new PlayerStatsStored();
										RailConverter.Cpp2Csharp(data, playerStatsStored);
										railEventCallBackHandler(event_id, playerStatsStored);
										return;
									}
									case RAILEventID.kRailEventStatsNumOfPlayerReceived:
									{
										NumberOfPlayerReceived numberOfPlayerReceived = new NumberOfPlayerReceived();
										RailConverter.Cpp2Csharp(data, numberOfPlayerReceived);
										railEventCallBackHandler(event_id, numberOfPlayerReceived);
										return;
									}
									case RAILEventID.kRailEventStatsGlobalStatsReceived:
									{
										GlobalStatsRequestReceived globalStatsRequestReceived = new GlobalStatsRequestReceived();
										RailConverter.Cpp2Csharp(data, globalStatsRequestReceived);
										railEventCallBackHandler(event_id, globalStatsRequestReceived);
										return;
									}
									default:
										return;
									}
									break;
								}
							}
						}
						else if (event_id <= RAILEventID.kRailEventLeaderboardAsyncCreated)
						{
							switch (event_id)
							{
							case RAILEventID.kRailEventAchievementPlayerAchievementReceived:
							{
								PlayerAchievementReceived playerAchievementReceived = new PlayerAchievementReceived();
								RailConverter.Cpp2Csharp(data, playerAchievementReceived);
								railEventCallBackHandler(event_id, playerAchievementReceived);
								return;
							}
							case RAILEventID.kRailEventAchievementPlayerAchievementStored:
							{
								PlayerAchievementStored playerAchievementStored = new PlayerAchievementStored();
								RailConverter.Cpp2Csharp(data, playerAchievementStored);
								railEventCallBackHandler(event_id, playerAchievementStored);
								return;
							}
							case RAILEventID.kRailEventAchievementGlobalAchievementReceived:
							{
								GlobalAchievementReceived globalAchievementReceived = new GlobalAchievementReceived();
								RailConverter.Cpp2Csharp(data, globalAchievementReceived);
								railEventCallBackHandler(event_id, globalAchievementReceived);
								return;
							}
							default:
								switch (event_id)
								{
								case RAILEventID.kRailEventLeaderboardReceived:
								{
									LeaderboardReceived leaderboardReceived = new LeaderboardReceived();
									RailConverter.Cpp2Csharp(data, leaderboardReceived);
									railEventCallBackHandler(event_id, leaderboardReceived);
									return;
								}
								case RAILEventID.kRailEventLeaderboardEntryReceived:
								{
									LeaderboardEntryReceived leaderboardEntryReceived = new LeaderboardEntryReceived();
									RailConverter.Cpp2Csharp(data, leaderboardEntryReceived);
									railEventCallBackHandler(event_id, leaderboardEntryReceived);
									return;
								}
								case RAILEventID.kRailEventLeaderboardUploaded:
								{
									LeaderboardUploaded leaderboardUploaded = new LeaderboardUploaded();
									RailConverter.Cpp2Csharp(data, leaderboardUploaded);
									railEventCallBackHandler(event_id, leaderboardUploaded);
									return;
								}
								case RAILEventID.kRailEventLeaderboardAttachSpaceWork:
								{
									LeaderboardAttachSpaceWork leaderboardAttachSpaceWork = new LeaderboardAttachSpaceWork();
									RailConverter.Cpp2Csharp(data, leaderboardAttachSpaceWork);
									railEventCallBackHandler(event_id, leaderboardAttachSpaceWork);
									return;
								}
								case RAILEventID.kRailEventLeaderboardAsyncCreated:
								{
									LeaderboardCreated leaderboardCreated = new LeaderboardCreated();
									RailConverter.Cpp2Csharp(data, leaderboardCreated);
									railEventCallBackHandler(event_id, leaderboardCreated);
									return;
								}
								default:
									return;
								}
								break;
							}
						}
						else
						{
							switch (event_id)
							{
							case RAILEventID.kRailEventGameServerListResult:
							{
								GetGameServerListResult getGameServerListResult = new GetGameServerListResult();
								RailConverter.Cpp2Csharp(data, getGameServerListResult);
								railEventCallBackHandler(event_id, getGameServerListResult);
								return;
							}
							case RAILEventID.kRailEventGameServerCreated:
							{
								CreateGameServerResult createGameServerResult = new CreateGameServerResult();
								RailConverter.Cpp2Csharp(data, createGameServerResult);
								railEventCallBackHandler(event_id, createGameServerResult);
								return;
							}
							case RAILEventID.kRailEventGameServerSetMetadataResult:
							{
								SetGameServerMetadataResult setGameServerMetadataResult = new SetGameServerMetadataResult();
								RailConverter.Cpp2Csharp(data, setGameServerMetadataResult);
								railEventCallBackHandler(event_id, setGameServerMetadataResult);
								return;
							}
							case RAILEventID.kRailEventGameServerGetMetadataResult:
							{
								GetGameServerMetadataResult getGameServerMetadataResult = new GetGameServerMetadataResult();
								RailConverter.Cpp2Csharp(data, getGameServerMetadataResult);
								railEventCallBackHandler(event_id, getGameServerMetadataResult);
								return;
							}
							case RAILEventID.kRailEventGameServerGetSessionTicket:
							{
								AsyncAcquireGameServerSessionTicketResponse asyncAcquireGameServerSessionTicketResponse = new AsyncAcquireGameServerSessionTicketResponse();
								RailConverter.Cpp2Csharp(data, asyncAcquireGameServerSessionTicketResponse);
								railEventCallBackHandler(event_id, asyncAcquireGameServerSessionTicketResponse);
								return;
							}
							case RAILEventID.kRailEventGameServerAuthSessionTicket:
							{
								GameServerStartSessionWithPlayerResponse gameServerStartSessionWithPlayerResponse = new GameServerStartSessionWithPlayerResponse();
								RailConverter.Cpp2Csharp(data, gameServerStartSessionWithPlayerResponse);
								railEventCallBackHandler(event_id, gameServerStartSessionWithPlayerResponse);
								return;
							}
							case RAILEventID.kRailEventGameServerPlayerListResult:
							{
								GetGameServerPlayerListResult getGameServerPlayerListResult = new GetGameServerPlayerListResult();
								RailConverter.Cpp2Csharp(data, getGameServerPlayerListResult);
								railEventCallBackHandler(event_id, getGameServerPlayerListResult);
								return;
							}
							case RAILEventID.kRailEventGameServerRegisterToServerListResult:
							{
								GameServerRegisterToServerListResult gameServerRegisterToServerListResult = new GameServerRegisterToServerListResult();
								RailConverter.Cpp2Csharp(data, gameServerRegisterToServerListResult);
								railEventCallBackHandler(event_id, gameServerRegisterToServerListResult);
								return;
							}
							case RAILEventID.kRailEventGameServerFavoriteGameServers:
							{
								AsyncGetFavoriteGameServersResult asyncGetFavoriteGameServersResult = new AsyncGetFavoriteGameServersResult();
								RailConverter.Cpp2Csharp(data, asyncGetFavoriteGameServersResult);
								railEventCallBackHandler(event_id, asyncGetFavoriteGameServersResult);
								return;
							}
							case RAILEventID.kRailEventGameServerAddFavoriteGameServer:
							{
								AsyncAddFavoriteGameServerResult asyncAddFavoriteGameServerResult = new AsyncAddFavoriteGameServerResult();
								RailConverter.Cpp2Csharp(data, asyncAddFavoriteGameServerResult);
								railEventCallBackHandler(event_id, asyncAddFavoriteGameServerResult);
								return;
							}
							case RAILEventID.kRailEventGameServerRemoveFavoriteGameServer:
							{
								AsyncRemoveFavoriteGameServerResult asyncRemoveFavoriteGameServerResult = new AsyncRemoveFavoriteGameServerResult();
								RailConverter.Cpp2Csharp(data, asyncRemoveFavoriteGameServerResult);
								railEventCallBackHandler(event_id, asyncRemoveFavoriteGameServerResult);
								return;
							}
							default:
								switch (event_id)
								{
								case RAILEventID.kRailEventUserSpaceGetMySubscribedWorksResult:
								{
									AsyncGetMySubscribedWorksResult asyncGetMySubscribedWorksResult = new AsyncGetMySubscribedWorksResult();
									RailConverter.Cpp2Csharp(data, asyncGetMySubscribedWorksResult);
									railEventCallBackHandler(event_id, asyncGetMySubscribedWorksResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceGetMyFavoritesWorksResult:
								{
									AsyncGetMyFavoritesWorksResult asyncGetMyFavoritesWorksResult = new AsyncGetMyFavoritesWorksResult();
									RailConverter.Cpp2Csharp(data, asyncGetMyFavoritesWorksResult);
									railEventCallBackHandler(event_id, asyncGetMyFavoritesWorksResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceQuerySpaceWorksResult:
								{
									AsyncQuerySpaceWorksResult asyncQuerySpaceWorksResult = new AsyncQuerySpaceWorksResult();
									RailConverter.Cpp2Csharp(data, asyncQuerySpaceWorksResult);
									railEventCallBackHandler(event_id, asyncQuerySpaceWorksResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceUpdateMetadataResult:
								{
									AsyncUpdateMetadataResult asyncUpdateMetadataResult = new AsyncUpdateMetadataResult();
									RailConverter.Cpp2Csharp(data, asyncUpdateMetadataResult);
									railEventCallBackHandler(event_id, asyncUpdateMetadataResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceSyncResult:
								{
									SyncSpaceWorkResult syncSpaceWorkResult = new SyncSpaceWorkResult();
									RailConverter.Cpp2Csharp(data, syncSpaceWorkResult);
									railEventCallBackHandler(event_id, syncSpaceWorkResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceSubscribeResult:
								{
									AsyncSubscribeSpaceWorksResult asyncSubscribeSpaceWorksResult = new AsyncSubscribeSpaceWorksResult();
									RailConverter.Cpp2Csharp(data, asyncSubscribeSpaceWorksResult);
									railEventCallBackHandler(event_id, asyncSubscribeSpaceWorksResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceModifyFavoritesWorksResult:
								{
									AsyncModifyFavoritesWorksResult asyncModifyFavoritesWorksResult = new AsyncModifyFavoritesWorksResult();
									RailConverter.Cpp2Csharp(data, asyncModifyFavoritesWorksResult);
									railEventCallBackHandler(event_id, asyncModifyFavoritesWorksResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceRemoveSpaceWorkResult:
								{
									AsyncRemoveSpaceWorkResult asyncRemoveSpaceWorkResult = new AsyncRemoveSpaceWorkResult();
									RailConverter.Cpp2Csharp(data, asyncRemoveSpaceWorkResult);
									railEventCallBackHandler(event_id, asyncRemoveSpaceWorkResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceVoteSpaceWorkResult:
								{
									AsyncVoteSpaceWorkResult asyncVoteSpaceWorkResult = new AsyncVoteSpaceWorkResult();
									RailConverter.Cpp2Csharp(data, asyncVoteSpaceWorkResult);
									railEventCallBackHandler(event_id, asyncVoteSpaceWorkResult);
									return;
								}
								case RAILEventID.kRailEventUserSpaceSearchSpaceWorkResult:
								{
									AsyncSearchSpaceWorksResult asyncSearchSpaceWorksResult = new AsyncSearchSpaceWorksResult();
									RailConverter.Cpp2Csharp(data, asyncSearchSpaceWorksResult);
									railEventCallBackHandler(event_id, asyncSearchSpaceWorksResult);
									return;
								}
								default:
									return;
								}
								break;
							}
						}
					}
					else if (event_id <= RAILEventID.kRailEventInGamePurchasePurchaseProductsToAssetsResult)
					{
						if (event_id <= RAILEventID.kRailEventAssetsUpdateAssetPropertyFinished)
						{
							switch (event_id)
							{
							case RAILEventID.kRailEventStorageQueryQuotaResult:
							{
								AsyncQueryQuotaResult asyncQueryQuotaResult = new AsyncQueryQuotaResult();
								RailConverter.Cpp2Csharp(data, asyncQueryQuotaResult);
								railEventCallBackHandler(event_id, asyncQueryQuotaResult);
								return;
							}
							case RAILEventID.kRailEventStorageShareToSpaceWorkResult:
							{
								ShareStorageToSpaceWorkResult shareStorageToSpaceWorkResult = new ShareStorageToSpaceWorkResult();
								RailConverter.Cpp2Csharp(data, shareStorageToSpaceWorkResult);
								railEventCallBackHandler(event_id, shareStorageToSpaceWorkResult);
								return;
							}
							case RAILEventID.kRailEventStorageAsyncReadFileResult:
							{
								AsyncReadFileResult asyncReadFileResult = new AsyncReadFileResult();
								RailConverter.Cpp2Csharp(data, asyncReadFileResult);
								railEventCallBackHandler(event_id, asyncReadFileResult);
								return;
							}
							case RAILEventID.kRailEventStorageAsyncWriteFileResult:
							{
								AsyncWriteFileResult asyncWriteFileResult = new AsyncWriteFileResult();
								RailConverter.Cpp2Csharp(data, asyncWriteFileResult);
								railEventCallBackHandler(event_id, asyncWriteFileResult);
								return;
							}
							case RAILEventID.kRailEventStorageAsyncListStreamFileResult:
							{
								AsyncListFileResult asyncListFileResult = new AsyncListFileResult();
								RailConverter.Cpp2Csharp(data, asyncListFileResult);
								railEventCallBackHandler(event_id, asyncListFileResult);
								return;
							}
							case RAILEventID.kRailEventStorageAsyncRenameStreamFileResult:
							{
								AsyncRenameStreamFileResult asyncRenameStreamFileResult = new AsyncRenameStreamFileResult();
								RailConverter.Cpp2Csharp(data, asyncRenameStreamFileResult);
								railEventCallBackHandler(event_id, asyncRenameStreamFileResult);
								return;
							}
							case RAILEventID.kRailEventStorageAsyncDeleteStreamFileResult:
							{
								AsyncDeleteStreamFileResult asyncDeleteStreamFileResult = new AsyncDeleteStreamFileResult();
								RailConverter.Cpp2Csharp(data, asyncDeleteStreamFileResult);
								railEventCallBackHandler(event_id, asyncDeleteStreamFileResult);
								return;
							}
							case RAILEventID.kRailEventStorageAsyncReadStreamFileResult:
							{
								AsyncReadStreamFileResult asyncReadStreamFileResult = new AsyncReadStreamFileResult();
								RailConverter.Cpp2Csharp(data, asyncReadStreamFileResult);
								railEventCallBackHandler(event_id, asyncReadStreamFileResult);
								return;
							}
							case RAILEventID.kRailEventStorageAsyncWriteStreamFileResult:
							{
								AsyncWriteStreamFileResult asyncWriteStreamFileResult = new AsyncWriteStreamFileResult();
								RailConverter.Cpp2Csharp(data, asyncWriteStreamFileResult);
								railEventCallBackHandler(event_id, asyncWriteStreamFileResult);
								return;
							}
							default:
								switch (event_id)
								{
								case RAILEventID.kRailEventAssetsRequestAllAssetsFinished:
								{
									RequestAllAssetsFinished requestAllAssetsFinished = new RequestAllAssetsFinished();
									RailConverter.Cpp2Csharp(data, requestAllAssetsFinished);
									railEventCallBackHandler(event_id, requestAllAssetsFinished);
									break;
								}
								case RAILEventID.kRailEventAssetsCompleteConsumeByExchangeAssetsToFinished:
								{
									CompleteConsumeByExchangeAssetsToFinished completeConsumeByExchangeAssetsToFinished = new CompleteConsumeByExchangeAssetsToFinished();
									RailConverter.Cpp2Csharp(data, completeConsumeByExchangeAssetsToFinished);
									railEventCallBackHandler(event_id, completeConsumeByExchangeAssetsToFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsExchangeAssetsFinished:
								{
									ExchangeAssetsFinished exchangeAssetsFinished = new ExchangeAssetsFinished();
									RailConverter.Cpp2Csharp(data, exchangeAssetsFinished);
									railEventCallBackHandler(event_id, exchangeAssetsFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsExchangeAssetsToFinished:
								{
									ExchangeAssetsToFinished exchangeAssetsToFinished = new ExchangeAssetsToFinished();
									RailConverter.Cpp2Csharp(data, exchangeAssetsToFinished);
									railEventCallBackHandler(event_id, exchangeAssetsToFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsDirectConsumeFinished:
								{
									DirectConsumeAssetsFinished directConsumeAssetsFinished = new DirectConsumeAssetsFinished();
									RailConverter.Cpp2Csharp(data, directConsumeAssetsFinished);
									railEventCallBackHandler(event_id, directConsumeAssetsFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsStartConsumeFinished:
								{
									StartConsumeAssetsFinished startConsumeAssetsFinished = new StartConsumeAssetsFinished();
									RailConverter.Cpp2Csharp(data, startConsumeAssetsFinished);
									railEventCallBackHandler(event_id, startConsumeAssetsFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsUpdateConsumeFinished:
								{
									UpdateConsumeAssetsFinished updateConsumeAssetsFinished = new UpdateConsumeAssetsFinished();
									RailConverter.Cpp2Csharp(data, updateConsumeAssetsFinished);
									railEventCallBackHandler(event_id, updateConsumeAssetsFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsCompleteConsumeFinished:
								{
									CompleteConsumeAssetsFinished completeConsumeAssetsFinished = new CompleteConsumeAssetsFinished();
									RailConverter.Cpp2Csharp(data, completeConsumeAssetsFinished);
									railEventCallBackHandler(event_id, completeConsumeAssetsFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsSplitFinished:
								{
									SplitAssetsFinished splitAssetsFinished = new SplitAssetsFinished();
									RailConverter.Cpp2Csharp(data, splitAssetsFinished);
									railEventCallBackHandler(event_id, splitAssetsFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsSplitToFinished:
								{
									SplitAssetsToFinished splitAssetsToFinished = new SplitAssetsToFinished();
									RailConverter.Cpp2Csharp(data, splitAssetsToFinished);
									railEventCallBackHandler(event_id, splitAssetsToFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsMergeFinished:
								{
									MergeAssetsFinished mergeAssetsFinished = new MergeAssetsFinished();
									RailConverter.Cpp2Csharp(data, mergeAssetsFinished);
									railEventCallBackHandler(event_id, mergeAssetsFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsMergeToFinished:
								{
									MergeAssetsToFinished mergeAssetsToFinished = new MergeAssetsToFinished();
									RailConverter.Cpp2Csharp(data, mergeAssetsToFinished);
									railEventCallBackHandler(event_id, mergeAssetsToFinished);
									return;
								}
								case RAILEventID.kRailEventAssetsRequestAllProductFinished:
									break;
								case RAILEventID.kRailEventAssetsUpdateAssetPropertyFinished:
								{
									UpdateAssetsPropertyFinished updateAssetsPropertyFinished = new UpdateAssetsPropertyFinished();
									RailConverter.Cpp2Csharp(data, updateAssetsPropertyFinished);
									railEventCallBackHandler(event_id, updateAssetsPropertyFinished);
									return;
								}
								default:
									return;
								}
								break;
							}
						}
						else
						{
							if (event_id == RAILEventID.kRailEventUtilsGetImageDataResult)
							{
								RailGetImageDataResult railGetImageDataResult = new RailGetImageDataResult();
								RailConverter.Cpp2Csharp(data, railGetImageDataResult);
								railEventCallBackHandler(event_id, railGetImageDataResult);
								return;
							}
							switch (event_id)
							{
							case RAILEventID.kRailEventInGamePurchaseAllProductsInfoReceived:
							{
								RailInGamePurchaseRequestAllProductsResponse railInGamePurchaseRequestAllProductsResponse = new RailInGamePurchaseRequestAllProductsResponse();
								RailConverter.Cpp2Csharp(data, railInGamePurchaseRequestAllProductsResponse);
								railEventCallBackHandler(event_id, railInGamePurchaseRequestAllProductsResponse);
								return;
							}
							case RAILEventID.kRailEventInGamePurchaseAllPurchasableProductsInfoReceived:
							{
								RailInGamePurchaseRequestAllPurchasableProductsResponse railInGamePurchaseRequestAllPurchasableProductsResponse = new RailInGamePurchaseRequestAllPurchasableProductsResponse();
								RailConverter.Cpp2Csharp(data, railInGamePurchaseRequestAllPurchasableProductsResponse);
								railEventCallBackHandler(event_id, railInGamePurchaseRequestAllPurchasableProductsResponse);
								return;
							}
							case RAILEventID.kRailEventInGamePurchasePurchaseProductsResult:
							{
								RailInGamePurchasePurchaseProductsResponse railInGamePurchasePurchaseProductsResponse = new RailInGamePurchasePurchaseProductsResponse();
								RailConverter.Cpp2Csharp(data, railInGamePurchasePurchaseProductsResponse);
								railEventCallBackHandler(event_id, railInGamePurchasePurchaseProductsResponse);
								return;
							}
							case RAILEventID.kRailEventInGamePurchaseFinishOrderResult:
							{
								RailInGamePurchaseFinishOrderResponse railInGamePurchaseFinishOrderResponse = new RailInGamePurchaseFinishOrderResponse();
								RailConverter.Cpp2Csharp(data, railInGamePurchaseFinishOrderResponse);
								railEventCallBackHandler(event_id, railInGamePurchaseFinishOrderResponse);
								return;
							}
							case RAILEventID.kRailEventInGamePurchasePurchaseProductsToAssetsResult:
							{
								RailInGamePurchasePurchaseProductsToAssetsResponse railInGamePurchasePurchaseProductsToAssetsResponse = new RailInGamePurchasePurchaseProductsToAssetsResponse();
								RailConverter.Cpp2Csharp(data, railInGamePurchasePurchaseProductsToAssetsResponse);
								railEventCallBackHandler(event_id, railInGamePurchasePurchaseProductsToAssetsResponse);
								return;
							}
							default:
								return;
							}
						}
					}
					else if (event_id <= RAILEventID.kRailEventRoomClearRoomMetadataResult)
					{
						switch (event_id)
						{
						case RAILEventID.kRailEventInGameStorePurchasePayWindowDisplayed:
						{
							RailInGameStorePurchasePayWindowDisplayed railInGameStorePurchasePayWindowDisplayed = new RailInGameStorePurchasePayWindowDisplayed();
							RailConverter.Cpp2Csharp(data, railInGameStorePurchasePayWindowDisplayed);
							railEventCallBackHandler(event_id, railInGameStorePurchasePayWindowDisplayed);
							return;
						}
						case RAILEventID.kRailEventInGameStorePurchasePayWindowClosed:
						{
							RailInGameStorePurchasePayWindowClosed railInGameStorePurchasePayWindowClosed = new RailInGameStorePurchasePayWindowClosed();
							RailConverter.Cpp2Csharp(data, railInGameStorePurchasePayWindowClosed);
							railEventCallBackHandler(event_id, railInGameStorePurchasePayWindowClosed);
							return;
						}
						case RAILEventID.kRailEventInGameStorePurchasePaymentResult:
						{
							RailInGameStorePurchaseResult railInGameStorePurchaseResult = new RailInGameStorePurchaseResult();
							RailConverter.Cpp2Csharp(data, railInGameStorePurchaseResult);
							railEventCallBackHandler(event_id, railInGameStorePurchaseResult);
							return;
						}
						default:
							switch (event_id)
							{
							case RAILEventID.kRailEventRoomZoneListResult:
							{
								ZoneInfoList zoneInfoList = new ZoneInfoList();
								RailConverter.Cpp2Csharp(data, zoneInfoList);
								railEventCallBackHandler(event_id, zoneInfoList);
								return;
							}
							case RAILEventID.kRailEventRoomListResult:
							{
								RoomInfoList roomInfoList = new RoomInfoList();
								RailConverter.Cpp2Csharp(data, roomInfoList);
								railEventCallBackHandler(event_id, roomInfoList);
								return;
							}
							case RAILEventID.kRailEventRoomCreated:
							{
								CreateRoomInfo createRoomInfo = new CreateRoomInfo();
								RailConverter.Cpp2Csharp(data, createRoomInfo);
								railEventCallBackHandler(event_id, createRoomInfo);
								return;
							}
							case RAILEventID.kRailEventRoomGotRoomMembers:
							{
								RoomMembersInfo roomMembersInfo = new RoomMembersInfo();
								RailConverter.Cpp2Csharp(data, roomMembersInfo);
								railEventCallBackHandler(event_id, roomMembersInfo);
								return;
							}
							case RAILEventID.kRailEventRoomJoinRoomResult:
							{
								JoinRoomInfo joinRoomInfo = new JoinRoomInfo();
								RailConverter.Cpp2Csharp(data, joinRoomInfo);
								railEventCallBackHandler(event_id, joinRoomInfo);
								return;
							}
							case RAILEventID.kRailEventRoomKickOffMemberResult:
							{
								KickOffMemberInfo kickOffMemberInfo = new KickOffMemberInfo();
								RailConverter.Cpp2Csharp(data, kickOffMemberInfo);
								railEventCallBackHandler(event_id, kickOffMemberInfo);
								return;
							}
							case RAILEventID.kRailEventRoomSetRoomMetadataResult:
							{
								SetRoomMetadataInfo setRoomMetadataInfo = new SetRoomMetadataInfo();
								RailConverter.Cpp2Csharp(data, setRoomMetadataInfo);
								railEventCallBackHandler(event_id, setRoomMetadataInfo);
								return;
							}
							case RAILEventID.kRailEventRoomGetRoomMetadataResult:
							{
								GetRoomMetadataInfo getRoomMetadataInfo = new GetRoomMetadataInfo();
								RailConverter.Cpp2Csharp(data, getRoomMetadataInfo);
								railEventCallBackHandler(event_id, getRoomMetadataInfo);
								return;
							}
							case RAILEventID.kRailEventRoomGetMemberMetadataResult:
							{
								GetMemberMetadataInfo getMemberMetadataInfo = new GetMemberMetadataInfo();
								RailConverter.Cpp2Csharp(data, getMemberMetadataInfo);
								railEventCallBackHandler(event_id, getMemberMetadataInfo);
								return;
							}
							case RAILEventID.kRailEventRoomSetMemberMetadataResult:
							{
								SetMemberMetadataInfo setMemberMetadataInfo = new SetMemberMetadataInfo();
								RailConverter.Cpp2Csharp(data, setMemberMetadataInfo);
								railEventCallBackHandler(event_id, setMemberMetadataInfo);
								return;
							}
							case RAILEventID.kRailEventRoomLeaveRoomResult:
							{
								LeaveRoomInfo leaveRoomInfo = new LeaveRoomInfo();
								RailConverter.Cpp2Csharp(data, leaveRoomInfo);
								railEventCallBackHandler(event_id, leaveRoomInfo);
								return;
							}
							case RAILEventID.kRailEventRoomGetAllDataResult:
							{
								RoomAllData roomAllData = new RoomAllData();
								RailConverter.Cpp2Csharp(data, roomAllData);
								railEventCallBackHandler(event_id, roomAllData);
								return;
							}
							case RAILEventID.kRailEventRoomGetUserRoomListResult:
							{
								UserRoomListInfo userRoomListInfo = new UserRoomListInfo();
								RailConverter.Cpp2Csharp(data, userRoomListInfo);
								railEventCallBackHandler(event_id, userRoomListInfo);
								return;
							}
							case RAILEventID.kRailEventRoomClearRoomMetadataResult:
							{
								ClearRoomMetadataInfo clearRoomMetadataInfo = new ClearRoomMetadataInfo();
								RailConverter.Cpp2Csharp(data, clearRoomMetadataInfo);
								railEventCallBackHandler(event_id, clearRoomMetadataInfo);
								return;
							}
							default:
								return;
							}
							break;
						}
					}
					else
					{
						switch (event_id)
						{
						case RAILEventID.kRailEventRoomNotifyMetadataChanged:
						{
							NotifyMetadataChange notifyMetadataChange = new NotifyMetadataChange();
							RailConverter.Cpp2Csharp(data, notifyMetadataChange);
							railEventCallBackHandler(event_id, notifyMetadataChange);
							return;
						}
						case RAILEventID.kRailEventRoomNotifyMemberChanged:
						{
							NotifyRoomMemberChange notifyRoomMemberChange = new NotifyRoomMemberChange();
							RailConverter.Cpp2Csharp(data, notifyRoomMemberChange);
							railEventCallBackHandler(event_id, notifyRoomMemberChange);
							return;
						}
						case RAILEventID.kRailEventRoomNotifyMemberkicked:
						{
							NotifyRoomMemberKicked notifyRoomMemberKicked = new NotifyRoomMemberKicked();
							RailConverter.Cpp2Csharp(data, notifyRoomMemberKicked);
							railEventCallBackHandler(event_id, notifyRoomMemberKicked);
							return;
						}
						case RAILEventID.kRailEventRoomNotifyRoomDestroyed:
						{
							NotifyRoomDestroy notifyRoomDestroy = new NotifyRoomDestroy();
							RailConverter.Cpp2Csharp(data, notifyRoomDestroy);
							railEventCallBackHandler(event_id, notifyRoomDestroy);
							return;
						}
						case RAILEventID.kRailEventRoomNotifyRoomOwnerChanged:
						{
							NotifyRoomOwnerChange notifyRoomOwnerChange = new NotifyRoomOwnerChange();
							RailConverter.Cpp2Csharp(data, notifyRoomOwnerChange);
							railEventCallBackHandler(event_id, notifyRoomOwnerChange);
							return;
						}
						case RAILEventID.kRailEventRoomNotifyRoomDataReceived:
						{
							RoomDataReceived roomDataReceived = new RoomDataReceived();
							RailConverter.Cpp2Csharp(data, roomDataReceived);
							railEventCallBackHandler(event_id, roomDataReceived);
							return;
						}
						case RAILEventID.kRailEventRoomNotifyRoomGameServerChanged:
						{
							NotifyRoomGameServerChange notifyRoomGameServerChange = new NotifyRoomGameServerChange();
							RailConverter.Cpp2Csharp(data, notifyRoomGameServerChange);
							railEventCallBackHandler(event_id, notifyRoomGameServerChange);
							return;
						}
						default:
							switch (event_id)
							{
							case RAILEventID.kRailEventFriendsSetMetadataResult:
							{
								RailFriendsSetMetadataResult railFriendsSetMetadataResult = new RailFriendsSetMetadataResult();
								RailConverter.Cpp2Csharp(data, railFriendsSetMetadataResult);
								railEventCallBackHandler(event_id, railFriendsSetMetadataResult);
								return;
							}
							case RAILEventID.kRailEventFriendsGetMetadataResult:
							{
								RailFriendsGetMetadataResult railFriendsGetMetadataResult = new RailFriendsGetMetadataResult();
								RailConverter.Cpp2Csharp(data, railFriendsGetMetadataResult);
								railEventCallBackHandler(event_id, railFriendsGetMetadataResult);
								return;
							}
							case RAILEventID.kRailEventFriendsClearMetadataResult:
							{
								RailFriendsClearMetadataResult railFriendsClearMetadataResult = new RailFriendsClearMetadataResult();
								RailConverter.Cpp2Csharp(data, railFriendsClearMetadataResult);
								railEventCallBackHandler(event_id, railFriendsClearMetadataResult);
								return;
							}
							case RAILEventID.kRailEventFriendsGetInviteCommandLine:
							{
								RailFriendsGetInviteCommandLine railFriendsGetInviteCommandLine = new RailFriendsGetInviteCommandLine();
								RailConverter.Cpp2Csharp(data, railFriendsGetInviteCommandLine);
								railEventCallBackHandler(event_id, railFriendsGetInviteCommandLine);
								return;
							}
							case RAILEventID.kRailEventFriendsReportPlayedWithUserListResult:
							{
								RailFriendsReportPlayedWithUserListResult railFriendsReportPlayedWithUserListResult = new RailFriendsReportPlayedWithUserListResult();
								RailConverter.Cpp2Csharp(data, railFriendsReportPlayedWithUserListResult);
								railEventCallBackHandler(event_id, railFriendsReportPlayedWithUserListResult);
								return;
							}
							case (RAILEventID)12007:
							case (RAILEventID)12008:
							case (RAILEventID)12009:
								break;
							case RAILEventID.kRailEventFriendsFriendsListChanged:
							{
								RailFriendsListChanged railFriendsListChanged = new RailFriendsListChanged();
								RailConverter.Cpp2Csharp(data, railFriendsListChanged);
								railEventCallBackHandler(event_id, railFriendsListChanged);
								return;
							}
							case RAILEventID.kRailEventFriendsGetFriendPlayedGamesResult:
							{
								RailFriendsQueryFriendPlayedGamesResult railFriendsQueryFriendPlayedGamesResult = new RailFriendsQueryFriendPlayedGamesResult();
								RailConverter.Cpp2Csharp(data, railFriendsQueryFriendPlayedGamesResult);
								railEventCallBackHandler(event_id, railFriendsQueryFriendPlayedGamesResult);
								return;
							}
							case RAILEventID.kRailEventFriendsQueryPlayedWithFriendsListResult:
							{
								RailFriendsQueryPlayedWithFriendsListResult railFriendsQueryPlayedWithFriendsListResult = new RailFriendsQueryPlayedWithFriendsListResult();
								RailConverter.Cpp2Csharp(data, railFriendsQueryPlayedWithFriendsListResult);
								railEventCallBackHandler(event_id, railFriendsQueryPlayedWithFriendsListResult);
								return;
							}
							case RAILEventID.kRailEventFriendsQueryPlayedWithFriendsTimeResult:
							{
								RailFriendsQueryPlayedWithFriendsTimeResult railFriendsQueryPlayedWithFriendsTimeResult = new RailFriendsQueryPlayedWithFriendsTimeResult();
								RailConverter.Cpp2Csharp(data, railFriendsQueryPlayedWithFriendsTimeResult);
								railEventCallBackHandler(event_id, railFriendsQueryPlayedWithFriendsTimeResult);
								return;
							}
							case RAILEventID.kRailEventFriendsQueryPlayedWithFriendsGamesResult:
							{
								RailFriendsQueryPlayedWithFriendsGamesResult railFriendsQueryPlayedWithFriendsGamesResult = new RailFriendsQueryPlayedWithFriendsGamesResult();
								RailConverter.Cpp2Csharp(data, railFriendsQueryPlayedWithFriendsGamesResult);
								railEventCallBackHandler(event_id, railFriendsQueryPlayedWithFriendsGamesResult);
								return;
							}
							case RAILEventID.kRailEventFriendsAddFriendResult:
							{
								RailFriendsAddFriendResult railFriendsAddFriendResult = new RailFriendsAddFriendResult();
								RailConverter.Cpp2Csharp(data, railFriendsAddFriendResult);
								railEventCallBackHandler(event_id, railFriendsAddFriendResult);
								return;
							}
							case RAILEventID.kRailEventFriendsOnlineStateChanged:
							{
								RailFriendsOnlineStateChanged railFriendsOnlineStateChanged = new RailFriendsOnlineStateChanged();
								RailConverter.Cpp2Csharp(data, railFriendsOnlineStateChanged);
								railEventCallBackHandler(event_id, railFriendsOnlineStateChanged);
								return;
							}
							default:
								return;
							}
							break;
						}
					}
				}
				else if (event_id <= RAILEventID.kRailEventDlcRefundChanged)
				{
					if (event_id <= RAILEventID.kRailEventShowFloatingNotifyWindow)
					{
						if (event_id <= RAILEventID.kRailEventUsersShowUserHomepageWindowResult)
						{
							switch (event_id)
							{
							case RAILEventID.kRailEventSessionTicketGetSessionTicket:
							{
								AcquireSessionTicketResponse acquireSessionTicketResponse = new AcquireSessionTicketResponse();
								RailConverter.Cpp2Csharp(data, acquireSessionTicketResponse);
								railEventCallBackHandler(event_id, acquireSessionTicketResponse);
								return;
							}
							case RAILEventID.kRailEventSessionTicketAuthSessionTicket:
							{
								StartSessionWithPlayerResponse startSessionWithPlayerResponse = new StartSessionWithPlayerResponse();
								RailConverter.Cpp2Csharp(data, startSessionWithPlayerResponse);
								railEventCallBackHandler(event_id, startSessionWithPlayerResponse);
								return;
							}
							case RAILEventID.kRailEventPlayerGetGamePurchaseKey:
							{
								PlayerGetGamePurchaseKeyResult playerGetGamePurchaseKeyResult = new PlayerGetGamePurchaseKeyResult();
								RailConverter.Cpp2Csharp(data, playerGetGamePurchaseKeyResult);
								railEventCallBackHandler(event_id, playerGetGamePurchaseKeyResult);
								return;
							}
							case RAILEventID.kRailEventQueryPlayerBannedStatus:
							{
								QueryPlayerBannedStatus queryPlayerBannedStatus = new QueryPlayerBannedStatus();
								RailConverter.Cpp2Csharp(data, queryPlayerBannedStatus);
								railEventCallBackHandler(event_id, queryPlayerBannedStatus);
								return;
							}
							case RAILEventID.kRailEventPlayerGetAuthenticateURL:
							{
								GetAuthenticateURLResult getAuthenticateURLResult = new GetAuthenticateURLResult();
								RailConverter.Cpp2Csharp(data, getAuthenticateURLResult);
								railEventCallBackHandler(event_id, getAuthenticateURLResult);
								return;
							}
							case RAILEventID.kRailEventPlayerAntiAddictionGameOnlineTimeChanged:
							{
								RailAntiAddictionGameOnlineTimeChanged railAntiAddictionGameOnlineTimeChanged = new RailAntiAddictionGameOnlineTimeChanged();
								RailConverter.Cpp2Csharp(data, railAntiAddictionGameOnlineTimeChanged);
								railEventCallBackHandler(event_id, railAntiAddictionGameOnlineTimeChanged);
								return;
							}
							default:
								switch (event_id)
								{
								case RAILEventID.kRailEventUsersGetUsersInfo:
								{
									RailUsersInfoData railUsersInfoData = new RailUsersInfoData();
									RailConverter.Cpp2Csharp(data, railUsersInfoData);
									railEventCallBackHandler(event_id, railUsersInfoData);
									return;
								}
								case RAILEventID.kRailEventUsersNotifyInviter:
								{
									RailUsersNotifyInviter railUsersNotifyInviter = new RailUsersNotifyInviter();
									RailConverter.Cpp2Csharp(data, railUsersNotifyInviter);
									railEventCallBackHandler(event_id, railUsersNotifyInviter);
									return;
								}
								case RAILEventID.kRailEventUsersRespondInvitation:
								{
									RailUsersRespondInvitation railUsersRespondInvitation = new RailUsersRespondInvitation();
									RailConverter.Cpp2Csharp(data, railUsersRespondInvitation);
									railEventCallBackHandler(event_id, railUsersRespondInvitation);
									return;
								}
								case RAILEventID.kRailEventUsersInviteJoinGameResult:
								{
									RailUsersInviteJoinGameResult railUsersInviteJoinGameResult = new RailUsersInviteJoinGameResult();
									RailConverter.Cpp2Csharp(data, railUsersInviteJoinGameResult);
									railEventCallBackHandler(event_id, railUsersInviteJoinGameResult);
									return;
								}
								case RAILEventID.kRailEventUsersInviteUsersResult:
								{
									RailUsersInviteUsersResult railUsersInviteUsersResult = new RailUsersInviteUsersResult();
									RailConverter.Cpp2Csharp(data, railUsersInviteUsersResult);
									railEventCallBackHandler(event_id, railUsersInviteUsersResult);
									return;
								}
								case RAILEventID.kRailEventUsersGetInviteDetailResult:
								{
									RailUsersGetInviteDetailResult railUsersGetInviteDetailResult = new RailUsersGetInviteDetailResult();
									RailConverter.Cpp2Csharp(data, railUsersGetInviteDetailResult);
									railEventCallBackHandler(event_id, railUsersGetInviteDetailResult);
									return;
								}
								case RAILEventID.kRailEventUsersCancelInviteResult:
								{
									RailUsersCancelInviteResult railUsersCancelInviteResult = new RailUsersCancelInviteResult();
									RailConverter.Cpp2Csharp(data, railUsersCancelInviteResult);
									railEventCallBackHandler(event_id, railUsersCancelInviteResult);
									return;
								}
								case RAILEventID.kRailEventUsersGetUserLimitsResult:
								{
									RailUsersGetUserLimitsResult railUsersGetUserLimitsResult = new RailUsersGetUserLimitsResult();
									RailConverter.Cpp2Csharp(data, railUsersGetUserLimitsResult);
									railEventCallBackHandler(event_id, railUsersGetUserLimitsResult);
									return;
								}
								case RAILEventID.kRailEventUsersShowChatWindowWithFriendResult:
								{
									RailShowChatWindowWithFriendResult railShowChatWindowWithFriendResult = new RailShowChatWindowWithFriendResult();
									RailConverter.Cpp2Csharp(data, railShowChatWindowWithFriendResult);
									railEventCallBackHandler(event_id, railShowChatWindowWithFriendResult);
									return;
								}
								case RAILEventID.kRailEventUsersShowUserHomepageWindowResult:
								{
									RailShowUserHomepageWindowResult railShowUserHomepageWindowResult = new RailShowUserHomepageWindowResult();
									RailConverter.Cpp2Csharp(data, railShowUserHomepageWindowResult);
									railEventCallBackHandler(event_id, railShowUserHomepageWindowResult);
									return;
								}
								default:
									return;
								}
								break;
							}
						}
						else
						{
							if (event_id == RAILEventID.kRailEventShowFloatingWindow)
							{
								ShowFloatingWindowResult showFloatingWindowResult = new ShowFloatingWindowResult();
								RailConverter.Cpp2Csharp(data, showFloatingWindowResult);
								railEventCallBackHandler(event_id, showFloatingWindowResult);
								return;
							}
							if (event_id != RAILEventID.kRailEventShowFloatingNotifyWindow)
							{
								return;
							}
							ShowNotifyWindow showNotifyWindow = new ShowNotifyWindow();
							RailConverter.Cpp2Csharp(data, showNotifyWindow);
							railEventCallBackHandler(event_id, showNotifyWindow);
							return;
						}
					}
					else if (event_id <= RAILEventID.kRailEventNetworkCreateSessionRequest)
					{
						switch (event_id)
						{
						case RAILEventID.kRailEventBrowserCreateResult:
						{
							CreateBrowserResult createBrowserResult = new CreateBrowserResult();
							RailConverter.Cpp2Csharp(data, createBrowserResult);
							railEventCallBackHandler(event_id, createBrowserResult);
							return;
						}
						case RAILEventID.kRailEventBrowserReloadResult:
						{
							ReloadBrowserResult reloadBrowserResult = new ReloadBrowserResult();
							RailConverter.Cpp2Csharp(data, reloadBrowserResult);
							railEventCallBackHandler(event_id, reloadBrowserResult);
							return;
						}
						case RAILEventID.kRailEventBrowserCloseResult:
						{
							CloseBrowserResult closeBrowserResult = new CloseBrowserResult();
							RailConverter.Cpp2Csharp(data, closeBrowserResult);
							railEventCallBackHandler(event_id, closeBrowserResult);
							return;
						}
						case RAILEventID.kRailEventBrowserJavascriptEvent:
						{
							JavascriptEventResult javascriptEventResult = new JavascriptEventResult();
							RailConverter.Cpp2Csharp(data, javascriptEventResult);
							railEventCallBackHandler(event_id, javascriptEventResult);
							return;
						}
						case RAILEventID.kRailEventBrowserTryNavigateNewPageRequest:
						{
							BrowserTryNavigateNewPageRequest browserTryNavigateNewPageRequest = new BrowserTryNavigateNewPageRequest();
							RailConverter.Cpp2Csharp(data, browserTryNavigateNewPageRequest);
							railEventCallBackHandler(event_id, browserTryNavigateNewPageRequest);
							return;
						}
						case RAILEventID.kRailEventBrowserPaint:
						{
							BrowserNeedsPaintRequest browserNeedsPaintRequest = new BrowserNeedsPaintRequest();
							RailConverter.Cpp2Csharp(data, browserNeedsPaintRequest);
							railEventCallBackHandler(event_id, browserNeedsPaintRequest);
							return;
						}
						case RAILEventID.kRailEventBrowserDamageRectPaint:
						{
							BrowserDamageRectNeedsPaintRequest browserDamageRectNeedsPaintRequest = new BrowserDamageRectNeedsPaintRequest();
							RailConverter.Cpp2Csharp(data, browserDamageRectNeedsPaintRequest);
							railEventCallBackHandler(event_id, browserDamageRectNeedsPaintRequest);
							return;
						}
						case RAILEventID.kRailEventBrowserNavigeteResult:
						{
							BrowserRenderNavigateResult browserRenderNavigateResult = new BrowserRenderNavigateResult();
							RailConverter.Cpp2Csharp(data, browserRenderNavigateResult);
							railEventCallBackHandler(event_id, browserRenderNavigateResult);
							return;
						}
						case RAILEventID.kRailEventBrowserStateChanged:
						{
							BrowserRenderStateChanged browserRenderStateChanged = new BrowserRenderStateChanged();
							RailConverter.Cpp2Csharp(data, browserRenderStateChanged);
							railEventCallBackHandler(event_id, browserRenderStateChanged);
							return;
						}
						case RAILEventID.kRailEventBrowserTitleChanged:
						{
							BrowserRenderTitleChanged browserRenderTitleChanged = new BrowserRenderTitleChanged();
							RailConverter.Cpp2Csharp(data, browserRenderTitleChanged);
							railEventCallBackHandler(event_id, browserRenderTitleChanged);
							return;
						}
						default:
						{
							if (event_id != RAILEventID.kRailEventNetworkCreateSessionRequest)
							{
								return;
							}
							CreateSessionRequest createSessionRequest = new CreateSessionRequest();
							RailConverter.Cpp2Csharp(data, createSessionRequest);
							railEventCallBackHandler(event_id, createSessionRequest);
							return;
						}
						}
					}
					else
					{
						if (event_id == RAILEventID.kRailEventNetworkCreateSessionFailed)
						{
							CreateSessionFailed createSessionFailed = new CreateSessionFailed();
							RailConverter.Cpp2Csharp(data, createSessionFailed);
							railEventCallBackHandler(event_id, createSessionFailed);
							return;
						}
						switch (event_id)
						{
						case RAILEventID.kRailEventDlcInstallStart:
						{
							DlcInstallStart dlcInstallStart = new DlcInstallStart();
							RailConverter.Cpp2Csharp(data, dlcInstallStart);
							railEventCallBackHandler(event_id, dlcInstallStart);
							return;
						}
						case RAILEventID.kRailEventDlcInstallStartResult:
						{
							DlcInstallStartResult dlcInstallStartResult = new DlcInstallStartResult();
							RailConverter.Cpp2Csharp(data, dlcInstallStartResult);
							railEventCallBackHandler(event_id, dlcInstallStartResult);
							return;
						}
						case RAILEventID.kRailEventDlcInstallProgress:
						{
							DlcInstallProgress dlcInstallProgress = new DlcInstallProgress();
							RailConverter.Cpp2Csharp(data, dlcInstallProgress);
							railEventCallBackHandler(event_id, dlcInstallProgress);
							return;
						}
						case RAILEventID.kRailEventDlcInstallFinished:
						{
							DlcInstallFinished dlcInstallFinished = new DlcInstallFinished();
							RailConverter.Cpp2Csharp(data, dlcInstallFinished);
							railEventCallBackHandler(event_id, dlcInstallFinished);
							return;
						}
						case RAILEventID.kRailEventDlcUninstallFinished:
						{
							DlcUninstallFinished dlcUninstallFinished = new DlcUninstallFinished();
							RailConverter.Cpp2Csharp(data, dlcUninstallFinished);
							railEventCallBackHandler(event_id, dlcUninstallFinished);
							return;
						}
						case RAILEventID.kRailEventDlcCheckAllDlcsStateReadyResult:
						{
							CheckAllDlcsStateReadyResult checkAllDlcsStateReadyResult = new CheckAllDlcsStateReadyResult();
							RailConverter.Cpp2Csharp(data, checkAllDlcsStateReadyResult);
							railEventCallBackHandler(event_id, checkAllDlcsStateReadyResult);
							return;
						}
						case RAILEventID.kRailEventDlcQueryIsOwnedDlcsResult:
						{
							QueryIsOwnedDlcsResult queryIsOwnedDlcsResult = new QueryIsOwnedDlcsResult();
							RailConverter.Cpp2Csharp(data, queryIsOwnedDlcsResult);
							railEventCallBackHandler(event_id, queryIsOwnedDlcsResult);
							return;
						}
						case RAILEventID.kRailEventDlcOwnershipChanged:
						{
							DlcOwnershipChanged dlcOwnershipChanged = new DlcOwnershipChanged();
							RailConverter.Cpp2Csharp(data, dlcOwnershipChanged);
							railEventCallBackHandler(event_id, dlcOwnershipChanged);
							return;
						}
						case RAILEventID.kRailEventDlcRefundChanged:
						{
							DlcRefundChanged dlcRefundChanged = new DlcRefundChanged();
							RailConverter.Cpp2Csharp(data, dlcRefundChanged);
							railEventCallBackHandler(event_id, dlcRefundChanged);
							return;
						}
						default:
							return;
						}
					}
				}
				else if (event_id <= RAILEventID.kRailEventTextInputShowTextInputWindowResult)
				{
					if (event_id <= RAILEventID.kRailEventVoiceChannelSpeakingUsersChangedEvent)
					{
						switch (event_id)
						{
						case RAILEventID.kRailEventScreenshotTakeScreenshotFinished:
						{
							TakeScreenshotResult takeScreenshotResult = new TakeScreenshotResult();
							RailConverter.Cpp2Csharp(data, takeScreenshotResult);
							railEventCallBackHandler(event_id, takeScreenshotResult);
							return;
						}
						case RAILEventID.kRailEventScreenshotTakeScreenshotRequest:
						{
							ScreenshotRequestInfo screenshotRequestInfo = new ScreenshotRequestInfo();
							RailConverter.Cpp2Csharp(data, screenshotRequestInfo);
							railEventCallBackHandler(event_id, screenshotRequestInfo);
							return;
						}
						case RAILEventID.kRailEventScreenshotPublishScreenshotFinished:
						{
							PublishScreenshotResult publishScreenshotResult = new PublishScreenshotResult();
							RailConverter.Cpp2Csharp(data, publishScreenshotResult);
							railEventCallBackHandler(event_id, publishScreenshotResult);
							return;
						}
						default:
							switch (event_id)
							{
							case RAILEventID.kRailEventVoiceChannelCreateResult:
							{
								CreateVoiceChannelResult createVoiceChannelResult = new CreateVoiceChannelResult();
								RailConverter.Cpp2Csharp(data, createVoiceChannelResult);
								railEventCallBackHandler(event_id, createVoiceChannelResult);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelDataCaptured:
							{
								VoiceDataCapturedEvent voiceDataCapturedEvent = new VoiceDataCapturedEvent();
								RailConverter.Cpp2Csharp(data, voiceDataCapturedEvent);
								railEventCallBackHandler(event_id, voiceDataCapturedEvent);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelJoinedResult:
							{
								JoinVoiceChannelResult joinVoiceChannelResult = new JoinVoiceChannelResult();
								RailConverter.Cpp2Csharp(data, joinVoiceChannelResult);
								railEventCallBackHandler(event_id, joinVoiceChannelResult);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelLeaveResult:
							{
								LeaveVoiceChannelResult leaveVoiceChannelResult = new LeaveVoiceChannelResult();
								RailConverter.Cpp2Csharp(data, leaveVoiceChannelResult);
								railEventCallBackHandler(event_id, leaveVoiceChannelResult);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelAddUsersResult:
							{
								VoiceChannelAddUsersResult voiceChannelAddUsersResult = new VoiceChannelAddUsersResult();
								RailConverter.Cpp2Csharp(data, voiceChannelAddUsersResult);
								railEventCallBackHandler(event_id, voiceChannelAddUsersResult);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelRemoveUsersResult:
							{
								VoiceChannelRemoveUsersResult voiceChannelRemoveUsersResult = new VoiceChannelRemoveUsersResult();
								RailConverter.Cpp2Csharp(data, voiceChannelRemoveUsersResult);
								railEventCallBackHandler(event_id, voiceChannelRemoveUsersResult);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelInviteEvent:
							{
								VoiceChannelInviteEvent voiceChannelInviteEvent = new VoiceChannelInviteEvent();
								RailConverter.Cpp2Csharp(data, voiceChannelInviteEvent);
								railEventCallBackHandler(event_id, voiceChannelInviteEvent);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelMemberChangedEvent:
							{
								VoiceChannelMemeberChangedEvent voiceChannelMemeberChangedEvent = new VoiceChannelMemeberChangedEvent();
								RailConverter.Cpp2Csharp(data, voiceChannelMemeberChangedEvent);
								railEventCallBackHandler(event_id, voiceChannelMemeberChangedEvent);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelUsersSpeakingStateChangedEvent:
							{
								VoiceChannelUsersSpeakingStateChangedEvent voiceChannelUsersSpeakingStateChangedEvent = new VoiceChannelUsersSpeakingStateChangedEvent();
								RailConverter.Cpp2Csharp(data, voiceChannelUsersSpeakingStateChangedEvent);
								railEventCallBackHandler(event_id, voiceChannelUsersSpeakingStateChangedEvent);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelPushToTalkKeyChangedEvent:
							{
								VoiceChannelPushToTalkKeyChangedEvent voiceChannelPushToTalkKeyChangedEvent = new VoiceChannelPushToTalkKeyChangedEvent();
								RailConverter.Cpp2Csharp(data, voiceChannelPushToTalkKeyChangedEvent);
								railEventCallBackHandler(event_id, voiceChannelPushToTalkKeyChangedEvent);
								return;
							}
							case RAILEventID.kRailEventVoiceChannelSpeakingUsersChangedEvent:
							{
								VoiceChannelSpeakingUsersChangedEvent voiceChannelSpeakingUsersChangedEvent = new VoiceChannelSpeakingUsersChangedEvent();
								RailConverter.Cpp2Csharp(data, voiceChannelSpeakingUsersChangedEvent);
								railEventCallBackHandler(event_id, voiceChannelSpeakingUsersChangedEvent);
								return;
							}
							default:
								return;
							}
							break;
						}
					}
					else
					{
						if (event_id == RAILEventID.kRailEventAppQuerySubscribeWishPlayStateResult)
						{
							QuerySubscribeWishPlayStateResult querySubscribeWishPlayStateResult = new QuerySubscribeWishPlayStateResult();
							RailConverter.Cpp2Csharp(data, querySubscribeWishPlayStateResult);
							railEventCallBackHandler(event_id, querySubscribeWishPlayStateResult);
							return;
						}
						if (event_id != RAILEventID.kRailEventTextInputShowTextInputWindowResult)
						{
							return;
						}
						RailTextInputResult railTextInputResult = new RailTextInputResult();
						RailConverter.Cpp2Csharp(data, railTextInputResult);
						railEventCallBackHandler(event_id, railTextInputResult);
						return;
					}
				}
				else if (event_id <= RAILEventID.kRailEventHttpSessionResponseResult)
				{
					if (event_id == RAILEventID.kRailEventIMEHelperTextInputSelectedResult)
					{
						RailIMEHelperTextInputSelectedResult railIMEHelperTextInputSelectedResult = new RailIMEHelperTextInputSelectedResult();
						RailConverter.Cpp2Csharp(data, railIMEHelperTextInputSelectedResult);
						railEventCallBackHandler(event_id, railIMEHelperTextInputSelectedResult);
						return;
					}
					if (event_id != RAILEventID.kRailEventHttpSessionResponseResult)
					{
						return;
					}
					RailHttpSessionResponse railHttpSessionResponse = new RailHttpSessionResponse();
					RailConverter.Cpp2Csharp(data, railHttpSessionResponse);
					railEventCallBackHandler(event_id, railHttpSessionResponse);
					return;
				}
				else
				{
					if (event_id == RAILEventID.kRailEventSmallObjectServiceQueryObjectStateResult)
					{
						RailSmallObjectStateQueryResult railSmallObjectStateQueryResult = new RailSmallObjectStateQueryResult();
						RailConverter.Cpp2Csharp(data, railSmallObjectStateQueryResult);
						railEventCallBackHandler(event_id, railSmallObjectStateQueryResult);
						return;
					}
					if (event_id == RAILEventID.kRailEventSmallObjectServiceDownloadResult)
					{
						RailSmallObjectDownloadResult railSmallObjectDownloadResult = new RailSmallObjectDownloadResult();
						RailConverter.Cpp2Csharp(data, railSmallObjectDownloadResult);
						railEventCallBackHandler(event_id, railSmallObjectDownloadResult);
						return;
					}
					if (event_id != RAILEventID.kRailEventZoneServerSwitchPlayerSelectedZoneResult)
					{
						return;
					}
					RailSwitchPlayerSelectedZoneResult railSwitchPlayerSelectedZoneResult = new RailSwitchPlayerSelectedZoneResult();
					RailConverter.Cpp2Csharp(data, railSwitchPlayerSelectedZoneResult);
					railEventCallBackHandler(event_id, railSwitchPlayerSelectedZoneResult);
					return;
				}
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00010832 File Offset: 0x0000EA32
		// Note: this type is marked as 'beforefieldinit'.
		static RailCallBackHelper()
		{
		}

		// Token: 0x04000077 RID: 119
		private static readonly object locker_ = new object();

		// Token: 0x04000078 RID: 120
		private static Dictionary<RAILEventID, RailEventCallBackHandler> eventHandlers_ = new Dictionary<RAILEventID, RailEventCallBackHandler>();

		// Token: 0x04000079 RID: 121
		private static RailEventCallBackFunction delegate_ = new RailEventCallBackFunction(RailCallBackHelper.OnRailCallBack);
	}
}
