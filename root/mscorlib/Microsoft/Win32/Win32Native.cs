using System;
using System.IO;

namespace Microsoft.Win32
{
	// Token: 0x02000092 RID: 146
	internal static class Win32Native
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0001809A File Offset: 0x0001629A
		public static string GetMessage(int hr)
		{
			return "Error " + hr.ToString();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000180AD File Offset: 0x000162AD
		public static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}

		// Token: 0x04000E91 RID: 3729
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04000E92 RID: 3730
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x04000E93 RID: 3731
		internal const int ERROR_INVALID_FUNCTION = 1;

		// Token: 0x04000E94 RID: 3732
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04000E95 RID: 3733
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x04000E96 RID: 3734
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000E97 RID: 3735
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000E98 RID: 3736
		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x04000E99 RID: 3737
		internal const int ERROR_INVALID_DATA = 13;

		// Token: 0x04000E9A RID: 3738
		internal const int ERROR_INVALID_DRIVE = 15;

		// Token: 0x04000E9B RID: 3739
		internal const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x04000E9C RID: 3740
		internal const int ERROR_NOT_READY = 21;

		// Token: 0x04000E9D RID: 3741
		internal const int ERROR_BAD_LENGTH = 24;

		// Token: 0x04000E9E RID: 3742
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04000E9F RID: 3743
		internal const int ERROR_NOT_SUPPORTED = 50;

		// Token: 0x04000EA0 RID: 3744
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x04000EA1 RID: 3745
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04000EA2 RID: 3746
		internal const int ERROR_BROKEN_PIPE = 109;

		// Token: 0x04000EA3 RID: 3747
		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		// Token: 0x04000EA4 RID: 3748
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04000EA5 RID: 3749
		internal const int ERROR_INVALID_NAME = 123;

		// Token: 0x04000EA6 RID: 3750
		internal const int ERROR_BAD_PATHNAME = 161;

		// Token: 0x04000EA7 RID: 3751
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000EA8 RID: 3752
		internal const int ERROR_ENVVAR_NOT_FOUND = 203;

		// Token: 0x04000EA9 RID: 3753
		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04000EAA RID: 3754
		internal const int ERROR_NO_DATA = 232;

		// Token: 0x04000EAB RID: 3755
		internal const int ERROR_PIPE_NOT_CONNECTED = 233;

		// Token: 0x04000EAC RID: 3756
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x04000EAD RID: 3757
		internal const int ERROR_DIRECTORY = 267;

		// Token: 0x04000EAE RID: 3758
		internal const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x04000EAF RID: 3759
		internal const int ERROR_NOT_FOUND = 1168;

		// Token: 0x04000EB0 RID: 3760
		internal const int ERROR_NO_TOKEN = 1008;

		// Token: 0x04000EB1 RID: 3761
		internal const int ERROR_DLL_INIT_FAILED = 1114;

		// Token: 0x04000EB2 RID: 3762
		internal const int ERROR_NON_ACCOUNT_SID = 1257;

		// Token: 0x04000EB3 RID: 3763
		internal const int ERROR_NOT_ALL_ASSIGNED = 1300;

		// Token: 0x04000EB4 RID: 3764
		internal const int ERROR_UNKNOWN_REVISION = 1305;

		// Token: 0x04000EB5 RID: 3765
		internal const int ERROR_INVALID_OWNER = 1307;

		// Token: 0x04000EB6 RID: 3766
		internal const int ERROR_INVALID_PRIMARY_GROUP = 1308;

		// Token: 0x04000EB7 RID: 3767
		internal const int ERROR_NO_SUCH_PRIVILEGE = 1313;

		// Token: 0x04000EB8 RID: 3768
		internal const int ERROR_PRIVILEGE_NOT_HELD = 1314;

		// Token: 0x04000EB9 RID: 3769
		internal const int ERROR_NONE_MAPPED = 1332;

		// Token: 0x04000EBA RID: 3770
		internal const int ERROR_INVALID_ACL = 1336;

		// Token: 0x04000EBB RID: 3771
		internal const int ERROR_INVALID_SID = 1337;

		// Token: 0x04000EBC RID: 3772
		internal const int ERROR_INVALID_SECURITY_DESCR = 1338;

		// Token: 0x04000EBD RID: 3773
		internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;

		// Token: 0x04000EBE RID: 3774
		internal const int ERROR_CANT_OPEN_ANONYMOUS = 1347;

		// Token: 0x04000EBF RID: 3775
		internal const int ERROR_NO_SECURITY_ON_OBJECT = 1350;

		// Token: 0x04000EC0 RID: 3776
		internal const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		// Token: 0x04000EC1 RID: 3777
		internal const FileAttributes FILE_ATTRIBUTE_DIRECTORY = FileAttributes.Directory;

		// Token: 0x02000093 RID: 147
		public class SECURITY_ATTRIBUTES
		{
			// Token: 0x06000493 RID: 1171 RVA: 0x000025BE File Offset: 0x000007BE
			public SECURITY_ATTRIBUTES()
			{
			}
		}

		// Token: 0x02000094 RID: 148
		internal class WIN32_FIND_DATA
		{
			// Token: 0x06000494 RID: 1172 RVA: 0x000025BE File Offset: 0x000007BE
			public WIN32_FIND_DATA()
			{
			}

			// Token: 0x04000EC2 RID: 3778
			internal int dwFileAttributes;

			// Token: 0x04000EC3 RID: 3779
			internal string cFileName;
		}
	}
}
