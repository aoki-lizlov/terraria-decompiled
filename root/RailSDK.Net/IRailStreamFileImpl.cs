using System;

namespace rail
{
	// Token: 0x0200002D RID: 45
	public class IRailStreamFileImpl : RailObject, IRailStreamFile, IRailComponent
	{
		// Token: 0x060012F3 RID: 4851 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailStreamFileImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000075C0 File Offset: 0x000057C0
		~IRailStreamFileImpl()
		{
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000075E8 File Offset: 0x000057E8
		public virtual string GetFilename()
		{
			return RAIL_API_PINVOKE.IRailStreamFile_GetFilename(this.swigCPtr_);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000075F5 File Offset: 0x000057F5
		public virtual RailResult AsyncRead(int offset, uint bytes_to_read, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStreamFile_AsyncRead(this.swigCPtr_, offset, bytes_to_read, user_data);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00007605 File Offset: 0x00005805
		public virtual RailResult AsyncWrite(byte[] buff, uint bytes_to_write, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStreamFile_AsyncWrite(this.swigCPtr_, buff, bytes_to_write, user_data);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00007615 File Offset: 0x00005815
		public virtual ulong GetSize()
		{
			return RAIL_API_PINVOKE.IRailStreamFile_GetSize(this.swigCPtr_);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00007622 File Offset: 0x00005822
		public virtual RailResult Close()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStreamFile_Close(this.swigCPtr_);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0000762F File Offset: 0x0000582F
		public virtual void Cancel()
		{
			RAIL_API_PINVOKE.IRailStreamFile_Cancel(this.swigCPtr_);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
