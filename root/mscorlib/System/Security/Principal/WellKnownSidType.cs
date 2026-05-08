using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020004BA RID: 1210
	[ComVisible(false)]
	public enum WellKnownSidType
	{
		// Token: 0x04002252 RID: 8786
		NullSid,
		// Token: 0x04002253 RID: 8787
		WorldSid,
		// Token: 0x04002254 RID: 8788
		LocalSid,
		// Token: 0x04002255 RID: 8789
		CreatorOwnerSid,
		// Token: 0x04002256 RID: 8790
		CreatorGroupSid,
		// Token: 0x04002257 RID: 8791
		CreatorOwnerServerSid,
		// Token: 0x04002258 RID: 8792
		CreatorGroupServerSid,
		// Token: 0x04002259 RID: 8793
		NTAuthoritySid,
		// Token: 0x0400225A RID: 8794
		DialupSid,
		// Token: 0x0400225B RID: 8795
		NetworkSid,
		// Token: 0x0400225C RID: 8796
		BatchSid,
		// Token: 0x0400225D RID: 8797
		InteractiveSid,
		// Token: 0x0400225E RID: 8798
		ServiceSid,
		// Token: 0x0400225F RID: 8799
		AnonymousSid,
		// Token: 0x04002260 RID: 8800
		ProxySid,
		// Token: 0x04002261 RID: 8801
		EnterpriseControllersSid,
		// Token: 0x04002262 RID: 8802
		SelfSid,
		// Token: 0x04002263 RID: 8803
		AuthenticatedUserSid,
		// Token: 0x04002264 RID: 8804
		RestrictedCodeSid,
		// Token: 0x04002265 RID: 8805
		TerminalServerSid,
		// Token: 0x04002266 RID: 8806
		RemoteLogonIdSid,
		// Token: 0x04002267 RID: 8807
		LogonIdsSid,
		// Token: 0x04002268 RID: 8808
		LocalSystemSid,
		// Token: 0x04002269 RID: 8809
		LocalServiceSid,
		// Token: 0x0400226A RID: 8810
		NetworkServiceSid,
		// Token: 0x0400226B RID: 8811
		BuiltinDomainSid,
		// Token: 0x0400226C RID: 8812
		BuiltinAdministratorsSid,
		// Token: 0x0400226D RID: 8813
		BuiltinUsersSid,
		// Token: 0x0400226E RID: 8814
		BuiltinGuestsSid,
		// Token: 0x0400226F RID: 8815
		BuiltinPowerUsersSid,
		// Token: 0x04002270 RID: 8816
		BuiltinAccountOperatorsSid,
		// Token: 0x04002271 RID: 8817
		BuiltinSystemOperatorsSid,
		// Token: 0x04002272 RID: 8818
		BuiltinPrintOperatorsSid,
		// Token: 0x04002273 RID: 8819
		BuiltinBackupOperatorsSid,
		// Token: 0x04002274 RID: 8820
		BuiltinReplicatorSid,
		// Token: 0x04002275 RID: 8821
		BuiltinPreWindows2000CompatibleAccessSid,
		// Token: 0x04002276 RID: 8822
		BuiltinRemoteDesktopUsersSid,
		// Token: 0x04002277 RID: 8823
		BuiltinNetworkConfigurationOperatorsSid,
		// Token: 0x04002278 RID: 8824
		AccountAdministratorSid,
		// Token: 0x04002279 RID: 8825
		AccountGuestSid,
		// Token: 0x0400227A RID: 8826
		AccountKrbtgtSid,
		// Token: 0x0400227B RID: 8827
		AccountDomainAdminsSid,
		// Token: 0x0400227C RID: 8828
		AccountDomainUsersSid,
		// Token: 0x0400227D RID: 8829
		AccountDomainGuestsSid,
		// Token: 0x0400227E RID: 8830
		AccountComputersSid,
		// Token: 0x0400227F RID: 8831
		AccountControllersSid,
		// Token: 0x04002280 RID: 8832
		AccountCertAdminsSid,
		// Token: 0x04002281 RID: 8833
		AccountSchemaAdminsSid,
		// Token: 0x04002282 RID: 8834
		AccountEnterpriseAdminsSid,
		// Token: 0x04002283 RID: 8835
		AccountPolicyAdminsSid,
		// Token: 0x04002284 RID: 8836
		AccountRasAndIasServersSid,
		// Token: 0x04002285 RID: 8837
		NtlmAuthenticationSid,
		// Token: 0x04002286 RID: 8838
		DigestAuthenticationSid,
		// Token: 0x04002287 RID: 8839
		SChannelAuthenticationSid,
		// Token: 0x04002288 RID: 8840
		ThisOrganizationSid,
		// Token: 0x04002289 RID: 8841
		OtherOrganizationSid,
		// Token: 0x0400228A RID: 8842
		BuiltinIncomingForestTrustBuildersSid,
		// Token: 0x0400228B RID: 8843
		BuiltinPerformanceMonitoringUsersSid,
		// Token: 0x0400228C RID: 8844
		BuiltinPerformanceLoggingUsersSid,
		// Token: 0x0400228D RID: 8845
		BuiltinAuthorizationAccessSid,
		// Token: 0x0400228E RID: 8846
		WinBuiltinTerminalServerLicenseServersSid,
		// Token: 0x0400228F RID: 8847
		MaxDefined = 60,
		// Token: 0x04002290 RID: 8848
		WinBuiltinDCOMUsersSid,
		// Token: 0x04002291 RID: 8849
		WinBuiltinIUsersSid,
		// Token: 0x04002292 RID: 8850
		WinIUserSid,
		// Token: 0x04002293 RID: 8851
		WinBuiltinCryptoOperatorsSid,
		// Token: 0x04002294 RID: 8852
		WinUntrustedLabelSid,
		// Token: 0x04002295 RID: 8853
		WinLowLabelSid,
		// Token: 0x04002296 RID: 8854
		WinMediumLabelSid,
		// Token: 0x04002297 RID: 8855
		WinHighLabelSid,
		// Token: 0x04002298 RID: 8856
		WinSystemLabelSid,
		// Token: 0x04002299 RID: 8857
		WinWriteRestrictedCodeSid,
		// Token: 0x0400229A RID: 8858
		WinCreatorOwnerRightsSid,
		// Token: 0x0400229B RID: 8859
		WinCacheablePrincipalsGroupSid,
		// Token: 0x0400229C RID: 8860
		WinNonCacheablePrincipalsGroupSid,
		// Token: 0x0400229D RID: 8861
		WinEnterpriseReadonlyControllersSid,
		// Token: 0x0400229E RID: 8862
		WinAccountReadonlyControllersSid,
		// Token: 0x0400229F RID: 8863
		WinBuiltinEventLogReadersGroup,
		// Token: 0x040022A0 RID: 8864
		WinNewEnterpriseReadonlyControllersSid,
		// Token: 0x040022A1 RID: 8865
		WinBuiltinCertSvcDComAccessGroup,
		// Token: 0x040022A2 RID: 8866
		WinMediumPlusLabelSid,
		// Token: 0x040022A3 RID: 8867
		WinLocalLogonSid,
		// Token: 0x040022A4 RID: 8868
		WinConsoleLogonSid,
		// Token: 0x040022A5 RID: 8869
		WinThisOrganizationCertificateSid,
		// Token: 0x040022A6 RID: 8870
		WinApplicationPackageAuthoritySid,
		// Token: 0x040022A7 RID: 8871
		WinBuiltinAnyPackageSid,
		// Token: 0x040022A8 RID: 8872
		WinCapabilityInternetClientSid,
		// Token: 0x040022A9 RID: 8873
		WinCapabilityInternetClientServerSid,
		// Token: 0x040022AA RID: 8874
		WinCapabilityPrivateNetworkClientServerSid,
		// Token: 0x040022AB RID: 8875
		WinCapabilityPicturesLibrarySid,
		// Token: 0x040022AC RID: 8876
		WinCapabilityVideosLibrarySid,
		// Token: 0x040022AD RID: 8877
		WinCapabilityMusicLibrarySid,
		// Token: 0x040022AE RID: 8878
		WinCapabilityDocumentsLibrarySid,
		// Token: 0x040022AF RID: 8879
		WinCapabilitySharedUserCertificatesSid,
		// Token: 0x040022B0 RID: 8880
		WinCapabilityEnterpriseAuthenticationSid,
		// Token: 0x040022B1 RID: 8881
		WinCapabilityRemovableStorageSid
	}
}
