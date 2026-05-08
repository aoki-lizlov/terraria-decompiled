using System;

namespace rail
{
	// Token: 0x0200010B RID: 267
	public enum RailResult
	{
		// Token: 0x040002E8 RID: 744
		kSuccess,
		// Token: 0x040002E9 RID: 745
		kFailure,
		// Token: 0x040002EA RID: 746
		kErrorInvalidParam,
		// Token: 0x040002EB RID: 747
		kErrorImageNotFound,
		// Token: 0x040002EC RID: 748
		kErrorBufferTooSmall,
		// Token: 0x040002ED RID: 749
		kErrorNetworkError,
		// Token: 0x040002EE RID: 750
		kErrorUnimplemented,
		// Token: 0x040002EF RID: 751
		kErrorInvalidCustomKey,
		// Token: 0x040002F0 RID: 752
		kErrorClientInOfflineMode,
		// Token: 0x040002F1 RID: 753
		kErrorParameterLengthTooLong,
		// Token: 0x040002F2 RID: 754
		kErrorWebApiKeyNoAccessOnThisGame,
		// Token: 0x040002F3 RID: 755
		kErrorOperationTimeout,
		// Token: 0x040002F4 RID: 756
		kErrorServerResponseInvalid,
		// Token: 0x040002F5 RID: 757
		kErrorServerInternalError,
		// Token: 0x040002F6 RID: 758
		kErrorFileNotFound = 1000,
		// Token: 0x040002F7 RID: 759
		kErrorAccessDenied,
		// Token: 0x040002F8 RID: 760
		kErrorOpenFileFailed,
		// Token: 0x040002F9 RID: 761
		kErrorCreateFileFailed,
		// Token: 0x040002FA RID: 762
		kErrorReadFailed,
		// Token: 0x040002FB RID: 763
		kErrorWriteFailed,
		// Token: 0x040002FC RID: 764
		kErrorFileDestroyed,
		// Token: 0x040002FD RID: 765
		kErrorFileDelete,
		// Token: 0x040002FE RID: 766
		kErrorFileQueryIndexOutofRange,
		// Token: 0x040002FF RID: 767
		kErrorFileAvaiableQuotaMoreThanTotal,
		// Token: 0x04000300 RID: 768
		kErrorFileGetRemotePathError,
		// Token: 0x04000301 RID: 769
		kErrorFileIllegal,
		// Token: 0x04000302 RID: 770
		kErrorStreamFileWriteParamError,
		// Token: 0x04000303 RID: 771
		kErrorStreamFileReadParamError,
		// Token: 0x04000304 RID: 772
		kErrorStreamCloseErrorIOWritting,
		// Token: 0x04000305 RID: 773
		kErrorStreamCloseErrorIOReading,
		// Token: 0x04000306 RID: 774
		kErrorStreamDeleteFileOpenFileError,
		// Token: 0x04000307 RID: 775
		kErrorStreamRenameFileOpenFileError,
		// Token: 0x04000308 RID: 776
		kErrorStreamReadOnlyError,
		// Token: 0x04000309 RID: 777
		kErrorStreamCreateFileRemoveOldFileError,
		// Token: 0x0400030A RID: 778
		kErrorStreamCreateFileNameNotAvailable,
		// Token: 0x0400030B RID: 779
		kErrorStreamOpenFileErrorCloudStorageDisabledByPlatform,
		// Token: 0x0400030C RID: 780
		kErrorStreamOpenFileErrorCloudStorageDisabledByPlayer,
		// Token: 0x0400030D RID: 781
		kErrorStoragePathNotFound,
		// Token: 0x0400030E RID: 782
		kErrorStorageFileCantOpen,
		// Token: 0x0400030F RID: 783
		kErrorStorageFileRefuseAccess,
		// Token: 0x04000310 RID: 784
		kErrorStorageFileInvalidHandle,
		// Token: 0x04000311 RID: 785
		kErrorStorageFileInUsingByAnotherProcess,
		// Token: 0x04000312 RID: 786
		kErrorStorageFileLockedByAnotherProcess,
		// Token: 0x04000313 RID: 787
		kErrorStorageFileWriteDiskNotEnough,
		// Token: 0x04000314 RID: 788
		kErrorStorageFileCantCreateFileOrPath,
		// Token: 0x04000315 RID: 789
		kErrorNetChannel = 2000,
		// Token: 0x04000316 RID: 790
		kErrorNetChannelInitializeFailed,
		// Token: 0x04000317 RID: 791
		kErrorNetChannelMemberOnline,
		// Token: 0x04000318 RID: 792
		kErrorNetChannelMemberOffline,
		// Token: 0x04000319 RID: 793
		kErrorNetChannelServerTimout,
		// Token: 0x0400031A RID: 794
		kErrorNetChannelServerException,
		// Token: 0x0400031B RID: 795
		kErrorNetChannelChannelIsAlreadyExist,
		// Token: 0x0400031C RID: 796
		kErrorNetChannelChannelIsNotExist,
		// Token: 0x0400031D RID: 797
		kErrorNetChannelNoAvailableDataToRead,
		// Token: 0x0400031E RID: 798
		kErrorRoomCreateFailed = 3000,
		// Token: 0x0400031F RID: 799
		kErrorKickOffFailed,
		// Token: 0x04000320 RID: 800
		kErrorKickOffPlayerNotInRoom,
		// Token: 0x04000321 RID: 801
		kErrorKickOffNotRoomOwner,
		// Token: 0x04000322 RID: 802
		kErrorKickOffPlayingGame,
		// Token: 0x04000323 RID: 803
		kErrorRoomServerRequestInvalidData,
		// Token: 0x04000324 RID: 804
		kErrorRoomServerConnectTcaplusFail,
		// Token: 0x04000325 RID: 805
		kErrorRoomServerConnectTcaplusTimeOut,
		// Token: 0x04000326 RID: 806
		kErrorRoomServerWrongDataInTcaplus,
		// Token: 0x04000327 RID: 807
		kErrorRoomServerNoDataInTcaplus,
		// Token: 0x04000328 RID: 808
		kErrorRoomServerAllocateRoomIdFail,
		// Token: 0x04000329 RID: 809
		kErrorRoomServerCreateGroupInImCloudFail,
		// Token: 0x0400032A RID: 810
		kErrorRoomServerUserAlreadyInGame,
		// Token: 0x0400032B RID: 811
		kErrorRoomServerQueryResultEmpty,
		// Token: 0x0400032C RID: 812
		kErrorRoomServerRoomFull,
		// Token: 0x0400032D RID: 813
		kErrorRoomServerRoomNotExist,
		// Token: 0x0400032E RID: 814
		kErrorRoomServerUserAlreadyInRoom,
		// Token: 0x0400032F RID: 815
		kErrorRoomServerZoneFull,
		// Token: 0x04000330 RID: 816
		kErrorRoomServerQueryRailIdServiceFail,
		// Token: 0x04000331 RID: 817
		kErrorRoomServerImCloudFail,
		// Token: 0x04000332 RID: 818
		kErrorRoomServerPbSerializeFail,
		// Token: 0x04000333 RID: 819
		kErrorRoomServerDirtyWord,
		// Token: 0x04000334 RID: 820
		kErrorRoomServerNoPermission,
		// Token: 0x04000335 RID: 821
		kErrorRoomServerLeaveUserNotInRoom,
		// Token: 0x04000336 RID: 822
		kErrorRoomServerDestroiedRoomNotExist,
		// Token: 0x04000337 RID: 823
		kErrorRoomServerUserIsNotRoomMember,
		// Token: 0x04000338 RID: 824
		kErrorRoomServerLockFailed,
		// Token: 0x04000339 RID: 825
		kErrorRoomServerRouteMiss,
		// Token: 0x0400033A RID: 826
		kErrorRoomServerRetry,
		// Token: 0x0400033B RID: 827
		kErrorRoomSendDataNotImplemented,
		// Token: 0x0400033C RID: 828
		kErrorRoomInvokeFailed,
		// Token: 0x0400033D RID: 829
		kErrorRoomServerPasswordIncorrect,
		// Token: 0x0400033E RID: 830
		kErrorRoomServerRoomIsNotJoinable,
		// Token: 0x0400033F RID: 831
		kErrorStats = 4000,
		// Token: 0x04000340 RID: 832
		kErrorStatsDontSetOtherPlayerStat,
		// Token: 0x04000341 RID: 833
		kErrorAchievement = 5000,
		// Token: 0x04000342 RID: 834
		kErrorAchievementOutofRange,
		// Token: 0x04000343 RID: 835
		kErrorAchievementNotMyAchievement,
		// Token: 0x04000344 RID: 836
		kErrorLeaderboard = 6000,
		// Token: 0x04000345 RID: 837
		kErrorLeaderboardNotExists,
		// Token: 0x04000346 RID: 838
		kErrorLeaderboardNotBePrepared,
		// Token: 0x04000347 RID: 839
		kErrorLeaderboardCreattionNotSupport,
		// Token: 0x04000348 RID: 840
		kErrorAssets = 7000,
		// Token: 0x04000349 RID: 841
		kErrorAssetsPending,
		// Token: 0x0400034A RID: 842
		kErrorAssetsOK,
		// Token: 0x0400034B RID: 843
		kErrorAssetsExpired,
		// Token: 0x0400034C RID: 844
		kErrorAssetsInvalidParam,
		// Token: 0x0400034D RID: 845
		kErrorAssetsServiceUnavailable,
		// Token: 0x0400034E RID: 846
		kErrorAssetsLimitExceeded,
		// Token: 0x0400034F RID: 847
		kErrorAssetsFailed,
		// Token: 0x04000350 RID: 848
		kErrorAssetsRailIdInvalid,
		// Token: 0x04000351 RID: 849
		kErrorAssetsGameIdInvalid,
		// Token: 0x04000352 RID: 850
		kErrorAssetsRequestInvokeFailed,
		// Token: 0x04000353 RID: 851
		kErrorAssetsUpdateConsumeProgressNull,
		// Token: 0x04000354 RID: 852
		kErrorAssetsCanNotFindAssetId,
		// Token: 0x04000355 RID: 853
		kErrorAssetInvalidRequest,
		// Token: 0x04000356 RID: 854
		kErrorAssetDBFail,
		// Token: 0x04000357 RID: 855
		kErrorAssetDataTooOld,
		// Token: 0x04000358 RID: 856
		kErrorAssetInConsume,
		// Token: 0x04000359 RID: 857
		kErrorAssetNotExist,
		// Token: 0x0400035A RID: 858
		kErrorAssetExchangNotMatch,
		// Token: 0x0400035B RID: 859
		kErrorAssetSystemError,
		// Token: 0x0400035C RID: 860
		kErrorAssetBadJasonData,
		// Token: 0x0400035D RID: 861
		kErrorAssetStateNotConsuing,
		// Token: 0x0400035E RID: 862
		kErrorAssetStateConsuing,
		// Token: 0x0400035F RID: 863
		kErrorAssetDifferentProductId,
		// Token: 0x04000360 RID: 864
		kErrorAssetConsumeQuantityTooBig,
		// Token: 0x04000361 RID: 865
		kErrorInGamePurchase = 8000,
		// Token: 0x04000362 RID: 866
		kErrorInGamePurchaseProductInfoExpired,
		// Token: 0x04000363 RID: 867
		kErrorInGamePurchaseAcquireSessionTicketFailed,
		// Token: 0x04000364 RID: 868
		kErrorInGamePurchaseParseWebContentFaild,
		// Token: 0x04000365 RID: 869
		kErrorInGamePurchaseProductIsNotExist,
		// Token: 0x04000366 RID: 870
		kErrorInGamePurchaseOrderIDIsNotExist,
		// Token: 0x04000367 RID: 871
		kErrorInGamePurchasePreparePaymentRequestTimeout,
		// Token: 0x04000368 RID: 872
		kErrorInGamePurchaseCreateOrderFailed,
		// Token: 0x04000369 RID: 873
		kErrorInGamePurchaseQueryOrderFailed,
		// Token: 0x0400036A RID: 874
		kErrorInGamePurchaseFinishOrderFailed,
		// Token: 0x0400036B RID: 875
		kErrorInGamePurchasePaymentFailed,
		// Token: 0x0400036C RID: 876
		kErrorInGamePurchasePaymentCancle,
		// Token: 0x0400036D RID: 877
		kErrorInGamePurchaseCreatePaymentBrowserFailed,
		// Token: 0x0400036E RID: 878
		kErrorInGameStorePurchase = 8500,
		// Token: 0x0400036F RID: 879
		kErrorInGameStorePurchasePaymentSuccess,
		// Token: 0x04000370 RID: 880
		kErrorInGameStorePurchasePaymentFailure,
		// Token: 0x04000371 RID: 881
		kErrorInGameStorePurchasePaymentCancle,
		// Token: 0x04000372 RID: 882
		kErrorPlayer = 9000,
		// Token: 0x04000373 RID: 883
		kErrorPlayerUserFolderCreateFailed,
		// Token: 0x04000374 RID: 884
		kErrorPlayerUserFolderCanntFind,
		// Token: 0x04000375 RID: 885
		kErrorPlayerUserNotFriend,
		// Token: 0x04000376 RID: 886
		kErrorPlayerGameNotSupportPurchaseKey,
		// Token: 0x04000377 RID: 887
		kErrorPlayerGetAuthenticateURLFailed,
		// Token: 0x04000378 RID: 888
		kErrorPlayerGetAuthenticateURLServerError,
		// Token: 0x04000379 RID: 889
		kErrorPlayerGetAuthenticateURLInvalidURL,
		// Token: 0x0400037A RID: 890
		kErrorFriends = 10000,
		// Token: 0x0400037B RID: 891
		kErrorFriendsKeyFrontUseRail,
		// Token: 0x0400037C RID: 892
		kErrorFriendsMetadataSizeInvalid,
		// Token: 0x0400037D RID: 893
		kErrorFriendsMetadataKeyLenInvalid,
		// Token: 0x0400037E RID: 894
		kErrorFriendsMetadataValueLenInvalid,
		// Token: 0x0400037F RID: 895
		kErrorFriendsMetadataKeyInvalid,
		// Token: 0x04000380 RID: 896
		kErrorFriendsGetMetadataFailed,
		// Token: 0x04000381 RID: 897
		kErrorFriendsSetPlayTogetherSizeZero,
		// Token: 0x04000382 RID: 898
		kErrorFriendsSetPlayTogetherContentSizeInvalid,
		// Token: 0x04000383 RID: 899
		kErrorFriendsInviteResponseTypeInvalid,
		// Token: 0x04000384 RID: 900
		kErrorFriendsListUpdateFailed,
		// Token: 0x04000385 RID: 901
		kErrorFriendsAddFriendInvalidID,
		// Token: 0x04000386 RID: 902
		kErrorFriendsAddFriendNetworkError,
		// Token: 0x04000387 RID: 903
		kErrorFriendsServerBusy,
		// Token: 0x04000388 RID: 904
		kErrorFriendsUpdateFriendsListTooFrequent,
		// Token: 0x04000389 RID: 905
		kErrorSessionTicket = 11000,
		// Token: 0x0400038A RID: 906
		kErrorSessionTicketGetTicketFailed,
		// Token: 0x0400038B RID: 907
		kErrorSessionTicketAuthFailed,
		// Token: 0x0400038C RID: 908
		kErrorSessionTicketAuthTicketAbandoned,
		// Token: 0x0400038D RID: 909
		kErrorSessionTicketAuthTicketExpire,
		// Token: 0x0400038E RID: 910
		kErrorSessionTicketAuthTicketInvalid,
		// Token: 0x0400038F RID: 911
		kErrorSessionTicketInvalidParameter = 11500,
		// Token: 0x04000390 RID: 912
		kErrorSessionTicketInvalidTicket,
		// Token: 0x04000391 RID: 913
		kErrorSessionTicketIncorrectTicketOwner,
		// Token: 0x04000392 RID: 914
		kErrorSessionTicketHasBeenCanceledByTicketOwner,
		// Token: 0x04000393 RID: 915
		kErrorSessionTicketExpired,
		// Token: 0x04000394 RID: 916
		kErrorFloatWindow = 12000,
		// Token: 0x04000395 RID: 917
		kErrorFloatWindowInitFailed,
		// Token: 0x04000396 RID: 918
		kErrorFloatWindowShowStoreInvalidPara,
		// Token: 0x04000397 RID: 919
		kErrorFloatWindowShowStoreCreateBrowserFailed,
		// Token: 0x04000398 RID: 920
		kErrorUserSpace = 13000,
		// Token: 0x04000399 RID: 921
		kErrorUserSpaceGetWorkDetailFailed,
		// Token: 0x0400039A RID: 922
		kErrorUserSpaceDownloadError,
		// Token: 0x0400039B RID: 923
		kErrorUserSpaceDescFileInvalid,
		// Token: 0x0400039C RID: 924
		kErrorUserSpaceReplaceOldFileFailed,
		// Token: 0x0400039D RID: 925
		kErrorUserSpaceUserCancelSync,
		// Token: 0x0400039E RID: 926
		kErrorUserSpaceIDorUserdataPathInvalid,
		// Token: 0x0400039F RID: 927
		kErrorUserSpaceNoData,
		// Token: 0x040003A0 RID: 928
		kErrorUserSpaceSpaceWorkIDInvalid,
		// Token: 0x040003A1 RID: 929
		kErrorUserSpaceNoSyncingNow,
		// Token: 0x040003A2 RID: 930
		kErrorUserSpaceSpaceWorkAlreadySyncing,
		// Token: 0x040003A3 RID: 931
		kErrorUserSpaceSubscribePartialSuccess,
		// Token: 0x040003A4 RID: 932
		kErrorUserSpaceNoVersionField,
		// Token: 0x040003A5 RID: 933
		kErrorUserSpaceUpdateFailedWhenUploading,
		// Token: 0x040003A6 RID: 934
		kErrorUserSpaceGetTicketFailed,
		// Token: 0x040003A7 RID: 935
		kErrorUserSpaceVersionOccupied,
		// Token: 0x040003A8 RID: 936
		kErrorUserSpaceCallCreateMethodFailed,
		// Token: 0x040003A9 RID: 937
		kErrorUserSpaceCreateMethodRspFailed,
		// Token: 0x040003AA RID: 938
		kErrorUserSpaceGenerateDescFileFailed,
		// Token: 0x040003AB RID: 939
		kErrorUserSpaceUploadFailed,
		// Token: 0x040003AC RID: 940
		kErrorUserSpaceNoEditablePermission,
		// Token: 0x040003AD RID: 941
		kErrorUserSpaceCallEditMethodFailed,
		// Token: 0x040003AE RID: 942
		kErrorUserSpaceEditMethodRspFailed,
		// Token: 0x040003AF RID: 943
		kErrorUserSpaceMetadataHasInvalidKey,
		// Token: 0x040003B0 RID: 944
		kErrorUserSpaceModifyFavoritePartialSuccess,
		// Token: 0x040003B1 RID: 945
		kErrorUserSpaceFilePathTooLong,
		// Token: 0x040003B2 RID: 946
		kErrorUserSpaceInvalidContentFolder,
		// Token: 0x040003B3 RID: 947
		kErrorUserSpaceInvalidFilePath,
		// Token: 0x040003B4 RID: 948
		kErrorUserSpaceUploadFileNotMeetLimit,
		// Token: 0x040003B5 RID: 949
		kErrorUserSpaceCannotReadFileToBeUploaded,
		// Token: 0x040003B6 RID: 950
		kErrorUserSpaceUploadSpaceWorkHasNoVersionField,
		// Token: 0x040003B7 RID: 951
		kErrorUserSpaceDownloadCurrentDescFileFailed,
		// Token: 0x040003B8 RID: 952
		kErrorUserSpaceCannotGetSpaceWorkDownloadUrl,
		// Token: 0x040003B9 RID: 953
		kErrorUserSpaceCannotGetSpaceWorkUploadUrl,
		// Token: 0x040003BA RID: 954
		kErrorUserSpaceCannotReadFileWhenUploading,
		// Token: 0x040003BB RID: 955
		kErrorUserSpaceUploadFileTooLarge,
		// Token: 0x040003BC RID: 956
		kErrorUserSpaceUploadRequestTimeout,
		// Token: 0x040003BD RID: 957
		kErrorUserSpaceUploadRequestFailed,
		// Token: 0x040003BE RID: 958
		kErrorUserSpaceUploadInternalError,
		// Token: 0x040003BF RID: 959
		kErrorUserSpaceUploadCloudServerError,
		// Token: 0x040003C0 RID: 960
		kErrorUserSpaceUploadCloudServerRspInvalid,
		// Token: 0x040003C1 RID: 961
		kErrorUserSpaceUploadCopyNoExistCloudFile,
		// Token: 0x040003C2 RID: 962
		kErrorUserSpaceShareLevelNotSatisfied,
		// Token: 0x040003C3 RID: 963
		kErrorUserSpaceHasntBeenApproved,
		// Token: 0x040003C4 RID: 964
		kErrorGameServer = 14000,
		// Token: 0x040003C5 RID: 965
		kErrorGameServerCreateFailed,
		// Token: 0x040003C6 RID: 966
		kErrorGameServerDisconnectedServerlist,
		// Token: 0x040003C7 RID: 967
		kErrorGameServerConnectServerlistFailure,
		// Token: 0x040003C8 RID: 968
		kErrorGameServerSetMetadataFailed,
		// Token: 0x040003C9 RID: 969
		kErrorGameServerGetMetadataFailed,
		// Token: 0x040003CA RID: 970
		kErrorGameServerGetServerListFailed,
		// Token: 0x040003CB RID: 971
		kErrorGameServerGetPlayerListFailed,
		// Token: 0x040003CC RID: 972
		kErrorGameServerPlayerNotJoinGameserver,
		// Token: 0x040003CD RID: 973
		kErrorGameServerNeedGetFovariteFirst,
		// Token: 0x040003CE RID: 974
		kErrorGameServerAddFovariteFailed,
		// Token: 0x040003CF RID: 975
		kErrorGameServerRemoveFovariteFailed,
		// Token: 0x040003D0 RID: 976
		kErrorNetwork = 15000,
		// Token: 0x040003D1 RID: 977
		kErrorNetworkInitializeFailed,
		// Token: 0x040003D2 RID: 978
		kErrorNetworkSessionIsNotExist,
		// Token: 0x040003D3 RID: 979
		kErrorNetworkNoAvailableDataToRead,
		// Token: 0x040003D4 RID: 980
		kErrorNetworkUnReachable,
		// Token: 0x040003D5 RID: 981
		kErrorNetworkRemotePeerOffline,
		// Token: 0x040003D6 RID: 982
		kErrorNetworkServerUnavailabe,
		// Token: 0x040003D7 RID: 983
		kErrorNetworkConnectionDenied,
		// Token: 0x040003D8 RID: 984
		kErrorNetworkConnectionClosed,
		// Token: 0x040003D9 RID: 985
		kErrorNetworkConnectionReset,
		// Token: 0x040003DA RID: 986
		kErrorNetworkSendDataSizeTooLarge,
		// Token: 0x040003DB RID: 987
		kErrorNetworkSessioNotRegistered,
		// Token: 0x040003DC RID: 988
		kErrorNetworkSessionTimeout,
		// Token: 0x040003DD RID: 989
		kErrorDlc = 16000,
		// Token: 0x040003DE RID: 990
		kErrorDlcInstallFailed,
		// Token: 0x040003DF RID: 991
		kErrorDlcUninstallFailed,
		// Token: 0x040003E0 RID: 992
		kErrorDlcGetDlcListTimeout,
		// Token: 0x040003E1 RID: 993
		kErrorDlcRequestInvokeFailed,
		// Token: 0x040003E2 RID: 994
		kErrorDlcRequestToofrequently,
		// Token: 0x040003E3 RID: 995
		kErrorUtils = 17000,
		// Token: 0x040003E4 RID: 996
		kErrorUtilsImagePathNull,
		// Token: 0x040003E5 RID: 997
		kErrorUtilsImagePathInvalid,
		// Token: 0x040003E6 RID: 998
		kErrorUtilsImageDownloadFail,
		// Token: 0x040003E7 RID: 999
		kErrorUtilsImageOpenLocalFail,
		// Token: 0x040003E8 RID: 1000
		kErrorUtilsImageBufferAllocateFail,
		// Token: 0x040003E9 RID: 1001
		kErrorUtilsImageReadLocalFail,
		// Token: 0x040003EA RID: 1002
		kErrorUtilsImageParseFail,
		// Token: 0x040003EB RID: 1003
		kErrorUtilsImageScaleFail,
		// Token: 0x040003EC RID: 1004
		kErrorUtilsImageUnknownFormat,
		// Token: 0x040003ED RID: 1005
		kErrorUtilsImageNotNeedResize,
		// Token: 0x040003EE RID: 1006
		kErrorUtilsImageResizeParameterInvalid,
		// Token: 0x040003EF RID: 1007
		kErrorUtilsImageSaveFileFail,
		// Token: 0x040003F0 RID: 1008
		kErrorUtilsDirtyWordsFilterTooManyInput,
		// Token: 0x040003F1 RID: 1009
		kErrorUtilsDirtyWordsHasInvalidString,
		// Token: 0x040003F2 RID: 1010
		kErrorUtilsDirtyWordsNotReady,
		// Token: 0x040003F3 RID: 1011
		kErrorUtilsDirtyWordsDllUnloaded,
		// Token: 0x040003F4 RID: 1012
		kErrorUtilsCrashAllocateFailed,
		// Token: 0x040003F5 RID: 1013
		kErrorUtilsCrashCallbackSwitchOff,
		// Token: 0x040003F6 RID: 1014
		kErrorUsers = 18000,
		// Token: 0x040003F7 RID: 1015
		kErrorUsersInvalidInviteCommandLine,
		// Token: 0x040003F8 RID: 1016
		kErrorUsersSetCommandLineFailed,
		// Token: 0x040003F9 RID: 1017
		kErrorUsersInviteListEmpty,
		// Token: 0x040003FA RID: 1018
		kErrorUsersGenerateRequestFail,
		// Token: 0x040003FB RID: 1019
		kErrorUsersUnknownInviteType,
		// Token: 0x040003FC RID: 1020
		kErrorUsersInvalidInviteUsersSize,
		// Token: 0x040003FD RID: 1021
		kErrorScreenshot = 19000,
		// Token: 0x040003FE RID: 1022
		kErrorScreenshotWorkNotExist,
		// Token: 0x040003FF RID: 1023
		kErrorScreenshotCantConvertPng,
		// Token: 0x04000400 RID: 1024
		kErrorScreenshotCopyFileFailed,
		// Token: 0x04000401 RID: 1025
		kErrorScreenshotCantCreateThumbnail,
		// Token: 0x04000402 RID: 1026
		kErrorVoiceCapture = 20000,
		// Token: 0x04000403 RID: 1027
		kErrorVoiceCaptureInitializeFailed,
		// Token: 0x04000404 RID: 1028
		kErrorVoiceCaptureDeviceLost,
		// Token: 0x04000405 RID: 1029
		kErrorVoiceCaptureIsRecording,
		// Token: 0x04000406 RID: 1030
		kErrorVoiceCaptureNotRecording,
		// Token: 0x04000407 RID: 1031
		kErrorVoiceCaptureNoData,
		// Token: 0x04000408 RID: 1032
		kErrorVoiceCaptureMoreData,
		// Token: 0x04000409 RID: 1033
		kErrorVoiceCaptureDataCorrupted,
		// Token: 0x0400040A RID: 1034
		kErrorVoiceCapturekUnsupportedCodec,
		// Token: 0x0400040B RID: 1035
		kErrorVoiceChannelHelperNotReady,
		// Token: 0x0400040C RID: 1036
		kErrorVoiceChannelIsBusy,
		// Token: 0x0400040D RID: 1037
		kErrorVoiceChannelNotJoinedChannel,
		// Token: 0x0400040E RID: 1038
		kErrorVoiceChannelLostConnection,
		// Token: 0x0400040F RID: 1039
		kErrorVoiceChannelAlreadyJoinedAnotherChannel,
		// Token: 0x04000410 RID: 1040
		kErrorVoiceChannelPartialSuccess,
		// Token: 0x04000411 RID: 1041
		kErrorVoiceChannelNotTheChannelOwner,
		// Token: 0x04000412 RID: 1042
		kErrorTextInputTextInputSendMessageToPlatformFailed = 21000,
		// Token: 0x04000413 RID: 1043
		kErrorTextInputTextInputSendMessageToOverlayFailed,
		// Token: 0x04000414 RID: 1044
		kErrorTextInputTextInputUserCanceled,
		// Token: 0x04000415 RID: 1045
		kErrorTextInputTextInputEnableChineseFailed,
		// Token: 0x04000416 RID: 1046
		kErrorTextInputTextInputShowFailed,
		// Token: 0x04000417 RID: 1047
		kErrorTextInputEnableIMEHelperTextInputWindowFailed,
		// Token: 0x04000418 RID: 1048
		kErrorApps = 22000,
		// Token: 0x04000419 RID: 1049
		kErrorAppsCountingKeyExists,
		// Token: 0x0400041A RID: 1050
		kErrorAppsCountingKeyDoesNotExist,
		// Token: 0x0400041B RID: 1051
		kErrorHttpSession = 23000,
		// Token: 0x0400041C RID: 1052
		kErrorHttpSessionPostBodyContentConflictWithPostParameter,
		// Token: 0x0400041D RID: 1053
		kErrorHttpSessionRequestMehotdNotPost,
		// Token: 0x0400041E RID: 1054
		kErrorSmallObjectService = 24000,
		// Token: 0x0400041F RID: 1055
		kErrorSmallObjectServiceObjectNotExist,
		// Token: 0x04000420 RID: 1056
		kErrorSmallObjectServiceFailedToRequestDownload,
		// Token: 0x04000421 RID: 1057
		kErrorSmallObjectServiceDownloadFailed,
		// Token: 0x04000422 RID: 1058
		kErrorSmallObjectServiceFailedToWriteDisk,
		// Token: 0x04000423 RID: 1059
		kErrorSmallObjectServiceFailedToUpdateObject,
		// Token: 0x04000424 RID: 1060
		kErrorSmallObjectServicePartialDownloadSuccess,
		// Token: 0x04000425 RID: 1061
		kErrorSmallObjectServiceObjectNetworkIssue,
		// Token: 0x04000426 RID: 1062
		kErrorSmallObjectServiceObjectServerError,
		// Token: 0x04000427 RID: 1063
		kErrorSmallObjectServiceInvalidBranch,
		// Token: 0x04000428 RID: 1064
		kErrorZoneServer = 25000,
		// Token: 0x04000429 RID: 1065
		kErrorZoneServerValueDataIsNotExist,
		// Token: 0x0400042A RID: 1066
		kRailErrorServerBegin = 2000000,
		// Token: 0x0400042B RID: 1067
		kErrorPaymentSystem = 2080001,
		// Token: 0x0400042C RID: 1068
		kErrorPaymentParameterIlleage = 2080008,
		// Token: 0x0400042D RID: 1069
		kErrorPaymentOrderIlleage = 2080011,
		// Token: 0x0400042E RID: 1070
		kErrorAssetsInvalidParameter = 2230001,
		// Token: 0x0400042F RID: 1071
		kErrorAssetsSystemError = 2230007,
		// Token: 0x04000430 RID: 1072
		kErrorDirtyWordsFilterNoPermission = 2290028,
		// Token: 0x04000431 RID: 1073
		kErrorDirtyWordsFilterCheckFailed,
		// Token: 0x04000432 RID: 1074
		kErrorDirtyWordsFilterSystemBusy,
		// Token: 0x04000433 RID: 1075
		kRailErrorInnerServerBegin = 2500000,
		// Token: 0x04000434 RID: 1076
		kErrorGameGrayCheckSnowError,
		// Token: 0x04000435 RID: 1077
		kErrorGameGrayParameterIlleage,
		// Token: 0x04000436 RID: 1078
		kErrorGameGraySystemError,
		// Token: 0x04000437 RID: 1079
		kErrorGameGrayQQToWegameidError,
		// Token: 0x04000438 RID: 1080
		kRailErrorInnerServerEnd = 2699999,
		// Token: 0x04000439 RID: 1081
		kRailErrorServerEnd = 2999999
	}
}
