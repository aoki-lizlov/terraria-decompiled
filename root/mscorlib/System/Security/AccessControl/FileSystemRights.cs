using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004F8 RID: 1272
	[Flags]
	public enum FileSystemRights
	{
		// Token: 0x04002413 RID: 9235
		ListDirectory = 1,
		// Token: 0x04002414 RID: 9236
		ReadData = 1,
		// Token: 0x04002415 RID: 9237
		CreateFiles = 2,
		// Token: 0x04002416 RID: 9238
		WriteData = 2,
		// Token: 0x04002417 RID: 9239
		AppendData = 4,
		// Token: 0x04002418 RID: 9240
		CreateDirectories = 4,
		// Token: 0x04002419 RID: 9241
		ReadExtendedAttributes = 8,
		// Token: 0x0400241A RID: 9242
		WriteExtendedAttributes = 16,
		// Token: 0x0400241B RID: 9243
		ExecuteFile = 32,
		// Token: 0x0400241C RID: 9244
		Traverse = 32,
		// Token: 0x0400241D RID: 9245
		DeleteSubdirectoriesAndFiles = 64,
		// Token: 0x0400241E RID: 9246
		ReadAttributes = 128,
		// Token: 0x0400241F RID: 9247
		WriteAttributes = 256,
		// Token: 0x04002420 RID: 9248
		Write = 278,
		// Token: 0x04002421 RID: 9249
		Delete = 65536,
		// Token: 0x04002422 RID: 9250
		ReadPermissions = 131072,
		// Token: 0x04002423 RID: 9251
		Read = 131209,
		// Token: 0x04002424 RID: 9252
		ReadAndExecute = 131241,
		// Token: 0x04002425 RID: 9253
		Modify = 197055,
		// Token: 0x04002426 RID: 9254
		ChangePermissions = 262144,
		// Token: 0x04002427 RID: 9255
		TakeOwnership = 524288,
		// Token: 0x04002428 RID: 9256
		Synchronize = 1048576,
		// Token: 0x04002429 RID: 9257
		FullControl = 2032127
	}
}
