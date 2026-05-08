using System;

namespace System.Security.Claims
{
	// Token: 0x020004C0 RID: 1216
	public static class ClaimTypes
	{
		// Token: 0x040022CD RID: 8909
		internal const string ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

		// Token: 0x040022CE RID: 8910
		public const string AuthenticationInstant = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant";

		// Token: 0x040022CF RID: 8911
		public const string AuthenticationMethod = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";

		// Token: 0x040022D0 RID: 8912
		public const string CookiePath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath";

		// Token: 0x040022D1 RID: 8913
		public const string DenyOnlyPrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid";

		// Token: 0x040022D2 RID: 8914
		public const string DenyOnlyPrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid";

		// Token: 0x040022D3 RID: 8915
		public const string DenyOnlyWindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";

		// Token: 0x040022D4 RID: 8916
		public const string Dsa = "http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa";

		// Token: 0x040022D5 RID: 8917
		public const string Expiration = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration";

		// Token: 0x040022D6 RID: 8918
		public const string Expired = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expired";

		// Token: 0x040022D7 RID: 8919
		public const string GroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";

		// Token: 0x040022D8 RID: 8920
		public const string IsPersistent = "http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent";

		// Token: 0x040022D9 RID: 8921
		public const string PrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid";

		// Token: 0x040022DA RID: 8922
		public const string PrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";

		// Token: 0x040022DB RID: 8923
		public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		// Token: 0x040022DC RID: 8924
		public const string SerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";

		// Token: 0x040022DD RID: 8925
		public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";

		// Token: 0x040022DE RID: 8926
		public const string Version = "http://schemas.microsoft.com/ws/2008/06/identity/claims/version";

		// Token: 0x040022DF RID: 8927
		public const string WindowsAccountName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";

		// Token: 0x040022E0 RID: 8928
		public const string WindowsDeviceClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim";

		// Token: 0x040022E1 RID: 8929
		public const string WindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";

		// Token: 0x040022E2 RID: 8930
		public const string WindowsUserClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim";

		// Token: 0x040022E3 RID: 8931
		public const string WindowsFqbnVersion = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion";

		// Token: 0x040022E4 RID: 8932
		public const string WindowsSubAuthority = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority";

		// Token: 0x040022E5 RID: 8933
		internal const string ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

		// Token: 0x040022E6 RID: 8934
		public const string Anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";

		// Token: 0x040022E7 RID: 8935
		public const string Authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";

		// Token: 0x040022E8 RID: 8936
		public const string AuthorizationDecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";

		// Token: 0x040022E9 RID: 8937
		public const string Country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";

		// Token: 0x040022EA RID: 8938
		public const string DateOfBirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";

		// Token: 0x040022EB RID: 8939
		public const string Dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";

		// Token: 0x040022EC RID: 8940
		public const string DenyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";

		// Token: 0x040022ED RID: 8941
		public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

		// Token: 0x040022EE RID: 8942
		public const string Gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";

		// Token: 0x040022EF RID: 8943
		public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

		// Token: 0x040022F0 RID: 8944
		public const string Hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";

		// Token: 0x040022F1 RID: 8945
		public const string HomePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";

		// Token: 0x040022F2 RID: 8946
		public const string Locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";

		// Token: 0x040022F3 RID: 8947
		public const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";

		// Token: 0x040022F4 RID: 8948
		public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		// Token: 0x040022F5 RID: 8949
		public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

		// Token: 0x040022F6 RID: 8950
		public const string OtherPhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";

		// Token: 0x040022F7 RID: 8951
		public const string PostalCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";

		// Token: 0x040022F8 RID: 8952
		public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

		// Token: 0x040022F9 RID: 8953
		public const string Sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";

		// Token: 0x040022FA RID: 8954
		public const string Spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";

		// Token: 0x040022FB RID: 8955
		public const string StateOrProvince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";

		// Token: 0x040022FC RID: 8956
		public const string StreetAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";

		// Token: 0x040022FD RID: 8957
		public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

		// Token: 0x040022FE RID: 8958
		public const string System = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";

		// Token: 0x040022FF RID: 8959
		public const string Thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";

		// Token: 0x04002300 RID: 8960
		public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

		// Token: 0x04002301 RID: 8961
		public const string Uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";

		// Token: 0x04002302 RID: 8962
		public const string Webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";

		// Token: 0x04002303 RID: 8963
		public const string X500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";

		// Token: 0x04002304 RID: 8964
		internal const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";

		// Token: 0x04002305 RID: 8965
		public const string Actor = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";
	}
}
