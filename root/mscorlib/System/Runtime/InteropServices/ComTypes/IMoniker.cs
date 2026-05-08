using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200077A RID: 1914
	[Guid("0000000f-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMoniker
	{
		// Token: 0x060044B6 RID: 17590
		void GetClassID(out Guid pClassID);

		// Token: 0x060044B7 RID: 17591
		[PreserveSig]
		int IsDirty();

		// Token: 0x060044B8 RID: 17592
		void Load(IStream pStm);

		// Token: 0x060044B9 RID: 17593
		void Save(IStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

		// Token: 0x060044BA RID: 17594
		void GetSizeMax(out long pcbSize);

		// Token: 0x060044BB RID: 17595
		void BindToObject(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		// Token: 0x060044BC RID: 17596
		void BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

		// Token: 0x060044BD RID: 17597
		void Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

		// Token: 0x060044BE RID: 17598
		void ComposeWith(IMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out IMoniker ppmkComposite);

		// Token: 0x060044BF RID: 17599
		void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out IEnumMoniker ppenumMoniker);

		// Token: 0x060044C0 RID: 17600
		[PreserveSig]
		int IsEqual(IMoniker pmkOtherMoniker);

		// Token: 0x060044C1 RID: 17601
		void Hash(out int pdwHash);

		// Token: 0x060044C2 RID: 17602
		[PreserveSig]
		int IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

		// Token: 0x060044C3 RID: 17603
		void GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out FILETIME pFileTime);

		// Token: 0x060044C4 RID: 17604
		void Inverse(out IMoniker ppmk);

		// Token: 0x060044C5 RID: 17605
		void CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

		// Token: 0x060044C6 RID: 17606
		void RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

		// Token: 0x060044C7 RID: 17607
		void GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

		// Token: 0x060044C8 RID: 17608
		void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out IMoniker ppmkOut);

		// Token: 0x060044C9 RID: 17609
		[PreserveSig]
		int IsSystemMoniker(out int pdwMksys);
	}
}
