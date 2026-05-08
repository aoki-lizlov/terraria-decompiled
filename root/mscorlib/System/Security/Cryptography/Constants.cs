using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000493 RID: 1171
	internal static class Constants
	{
		// Token: 0x040020C7 RID: 8391
		internal const int S_OK = 0;

		// Token: 0x040020C8 RID: 8392
		internal const int NTE_FILENOTFOUND = -2147024894;

		// Token: 0x040020C9 RID: 8393
		internal const int NTE_NO_KEY = -2146893811;

		// Token: 0x040020CA RID: 8394
		internal const int NTE_BAD_KEYSET = -2146893802;

		// Token: 0x040020CB RID: 8395
		internal const int NTE_KEYSET_NOT_DEF = -2146893799;

		// Token: 0x040020CC RID: 8396
		internal const int KP_IV = 1;

		// Token: 0x040020CD RID: 8397
		internal const int KP_MODE = 4;

		// Token: 0x040020CE RID: 8398
		internal const int KP_MODE_BITS = 5;

		// Token: 0x040020CF RID: 8399
		internal const int KP_EFFECTIVE_KEYLEN = 19;

		// Token: 0x040020D0 RID: 8400
		internal const int ALG_CLASS_SIGNATURE = 8192;

		// Token: 0x040020D1 RID: 8401
		internal const int ALG_CLASS_DATA_ENCRYPT = 24576;

		// Token: 0x040020D2 RID: 8402
		internal const int ALG_CLASS_HASH = 32768;

		// Token: 0x040020D3 RID: 8403
		internal const int ALG_CLASS_KEY_EXCHANGE = 40960;

		// Token: 0x040020D4 RID: 8404
		internal const int ALG_TYPE_DSS = 512;

		// Token: 0x040020D5 RID: 8405
		internal const int ALG_TYPE_RSA = 1024;

		// Token: 0x040020D6 RID: 8406
		internal const int ALG_TYPE_BLOCK = 1536;

		// Token: 0x040020D7 RID: 8407
		internal const int ALG_TYPE_STREAM = 2048;

		// Token: 0x040020D8 RID: 8408
		internal const int ALG_TYPE_ANY = 0;

		// Token: 0x040020D9 RID: 8409
		internal const int CALG_MD5 = 32771;

		// Token: 0x040020DA RID: 8410
		internal const int CALG_SHA1 = 32772;

		// Token: 0x040020DB RID: 8411
		internal const int CALG_SHA_256 = 32780;

		// Token: 0x040020DC RID: 8412
		internal const int CALG_SHA_384 = 32781;

		// Token: 0x040020DD RID: 8413
		internal const int CALG_SHA_512 = 32782;

		// Token: 0x040020DE RID: 8414
		internal const int CALG_RSA_KEYX = 41984;

		// Token: 0x040020DF RID: 8415
		internal const int CALG_RSA_SIGN = 9216;

		// Token: 0x040020E0 RID: 8416
		internal const int CALG_DSS_SIGN = 8704;

		// Token: 0x040020E1 RID: 8417
		internal const int CALG_DES = 26113;

		// Token: 0x040020E2 RID: 8418
		internal const int CALG_RC2 = 26114;

		// Token: 0x040020E3 RID: 8419
		internal const int CALG_3DES = 26115;

		// Token: 0x040020E4 RID: 8420
		internal const int CALG_3DES_112 = 26121;

		// Token: 0x040020E5 RID: 8421
		internal const int CALG_AES_128 = 26126;

		// Token: 0x040020E6 RID: 8422
		internal const int CALG_AES_192 = 26127;

		// Token: 0x040020E7 RID: 8423
		internal const int CALG_AES_256 = 26128;

		// Token: 0x040020E8 RID: 8424
		internal const int CALG_RC4 = 26625;

		// Token: 0x040020E9 RID: 8425
		internal const int PROV_RSA_FULL = 1;

		// Token: 0x040020EA RID: 8426
		internal const int PROV_DSS_DH = 13;

		// Token: 0x040020EB RID: 8427
		internal const int PROV_RSA_AES = 24;

		// Token: 0x040020EC RID: 8428
		internal const int AT_KEYEXCHANGE = 1;

		// Token: 0x040020ED RID: 8429
		internal const int AT_SIGNATURE = 2;

		// Token: 0x040020EE RID: 8430
		internal const int PUBLICKEYBLOB = 6;

		// Token: 0x040020EF RID: 8431
		internal const int PRIVATEKEYBLOB = 7;

		// Token: 0x040020F0 RID: 8432
		internal const int CRYPT_OAEP = 64;

		// Token: 0x040020F1 RID: 8433
		internal const uint CRYPT_VERIFYCONTEXT = 4026531840U;

		// Token: 0x040020F2 RID: 8434
		internal const uint CRYPT_NEWKEYSET = 8U;

		// Token: 0x040020F3 RID: 8435
		internal const uint CRYPT_DELETEKEYSET = 16U;

		// Token: 0x040020F4 RID: 8436
		internal const uint CRYPT_MACHINE_KEYSET = 32U;

		// Token: 0x040020F5 RID: 8437
		internal const uint CRYPT_SILENT = 64U;

		// Token: 0x040020F6 RID: 8438
		internal const uint CRYPT_EXPORTABLE = 1U;

		// Token: 0x040020F7 RID: 8439
		internal const uint CLR_KEYLEN = 1U;

		// Token: 0x040020F8 RID: 8440
		internal const uint CLR_PUBLICKEYONLY = 2U;

		// Token: 0x040020F9 RID: 8441
		internal const uint CLR_EXPORTABLE = 3U;

		// Token: 0x040020FA RID: 8442
		internal const uint CLR_REMOVABLE = 4U;

		// Token: 0x040020FB RID: 8443
		internal const uint CLR_HARDWARE = 5U;

		// Token: 0x040020FC RID: 8444
		internal const uint CLR_ACCESSIBLE = 6U;

		// Token: 0x040020FD RID: 8445
		internal const uint CLR_PROTECTED = 7U;

		// Token: 0x040020FE RID: 8446
		internal const uint CLR_UNIQUE_CONTAINER = 8U;

		// Token: 0x040020FF RID: 8447
		internal const uint CLR_ALGID = 9U;

		// Token: 0x04002100 RID: 8448
		internal const uint CLR_PP_CLIENT_HWND = 10U;

		// Token: 0x04002101 RID: 8449
		internal const uint CLR_PP_PIN = 11U;

		// Token: 0x04002102 RID: 8450
		internal const string OID_RSA_SMIMEalgCMS3DESwrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x04002103 RID: 8451
		internal const string OID_RSA_MD5 = "1.2.840.113549.2.5";

		// Token: 0x04002104 RID: 8452
		internal const string OID_RSA_RC2CBC = "1.2.840.113549.3.2";

		// Token: 0x04002105 RID: 8453
		internal const string OID_RSA_DES_EDE3_CBC = "1.2.840.113549.3.7";

		// Token: 0x04002106 RID: 8454
		internal const string OID_OIWSEC_desCBC = "1.3.14.3.2.7";

		// Token: 0x04002107 RID: 8455
		internal const string OID_OIWSEC_SHA1 = "1.3.14.3.2.26";

		// Token: 0x04002108 RID: 8456
		internal const string OID_OIWSEC_SHA256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x04002109 RID: 8457
		internal const string OID_OIWSEC_SHA384 = "2.16.840.1.101.3.4.2.2";

		// Token: 0x0400210A RID: 8458
		internal const string OID_OIWSEC_SHA512 = "2.16.840.1.101.3.4.2.3";

		// Token: 0x0400210B RID: 8459
		internal const string OID_OIWSEC_RIPEMD160 = "1.3.36.3.2.1";
	}
}
