using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200077F RID: 1919
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IStream
	{
		// Token: 0x060044E3 RID: 17635
		void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x060044E4 RID: 17636
		void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x060044E5 RID: 17637
		void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x060044E6 RID: 17638
		void SetSize(long libNewSize);

		// Token: 0x060044E7 RID: 17639
		void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x060044E8 RID: 17640
		void Commit(int grfCommitFlags);

		// Token: 0x060044E9 RID: 17641
		void Revert();

		// Token: 0x060044EA RID: 17642
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x060044EB RID: 17643
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x060044EC RID: 17644
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x060044ED RID: 17645
		void Clone(out IStream ppstm);
	}
}
