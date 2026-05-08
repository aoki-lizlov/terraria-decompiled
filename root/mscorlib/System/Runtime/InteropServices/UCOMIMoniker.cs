using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200072C RID: 1836
	[Obsolete]
	[Guid("0000000f-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIMoniker
	{
		// Token: 0x060041FF RID: 16895
		void GetClassID(out Guid pClassID);

		// Token: 0x06004200 RID: 16896
		[PreserveSig]
		int IsDirty();

		// Token: 0x06004201 RID: 16897
		void Load(UCOMIStream pStm);

		// Token: 0x06004202 RID: 16898
		void Save(UCOMIStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

		// Token: 0x06004203 RID: 16899
		void GetSizeMax(out long pcbSize);

		// Token: 0x06004204 RID: 16900
		void BindToObject(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		// Token: 0x06004205 RID: 16901
		void BindToStorage(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

		// Token: 0x06004206 RID: 16902
		void Reduce(UCOMIBindCtx pbc, int dwReduceHowFar, ref UCOMIMoniker ppmkToLeft, out UCOMIMoniker ppmkReduced);

		// Token: 0x06004207 RID: 16903
		void ComposeWith(UCOMIMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out UCOMIMoniker ppmkComposite);

		// Token: 0x06004208 RID: 16904
		void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out UCOMIEnumMoniker ppenumMoniker);

		// Token: 0x06004209 RID: 16905
		void IsEqual(UCOMIMoniker pmkOtherMoniker);

		// Token: 0x0600420A RID: 16906
		void Hash(out int pdwHash);

		// Token: 0x0600420B RID: 16907
		void IsRunning(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, UCOMIMoniker pmkNewlyRunning);

		// Token: 0x0600420C RID: 16908
		void GetTimeOfLastChange(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, out FILETIME pFileTime);

		// Token: 0x0600420D RID: 16909
		void Inverse(out UCOMIMoniker ppmk);

		// Token: 0x0600420E RID: 16910
		void CommonPrefixWith(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkPrefix);

		// Token: 0x0600420F RID: 16911
		void RelativePathTo(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkRelPath);

		// Token: 0x06004210 RID: 16912
		void GetDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

		// Token: 0x06004211 RID: 16913
		void ParseDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out UCOMIMoniker ppmkOut);

		// Token: 0x06004212 RID: 16914
		void IsSystemMoniker(out int pdwMksys);
	}
}
