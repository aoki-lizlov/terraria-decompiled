using System;

namespace rail
{
	// Token: 0x02000011 RID: 17
	public class IRailFileImpl : RailObject, IRailFile, IRailComponent
	{
		// Token: 0x06001149 RID: 4425 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailFileImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0000333C File Offset: 0x0000153C
		~IRailFileImpl()
		{
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00003364 File Offset: 0x00001564
		public virtual string GetFilename()
		{
			return RAIL_API_PINVOKE.IRailFile_GetFilename(this.swigCPtr_);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00003371 File Offset: 0x00001571
		public virtual uint Read(byte[] buff, uint bytes_to_read, out RailResult result)
		{
			return RAIL_API_PINVOKE.IRailFile_Read__SWIG_0(this.swigCPtr_, buff, bytes_to_read, out result);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00003381 File Offset: 0x00001581
		public virtual uint Read(byte[] buff, uint bytes_to_read)
		{
			return RAIL_API_PINVOKE.IRailFile_Read__SWIG_1(this.swigCPtr_, buff, bytes_to_read);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00003390 File Offset: 0x00001590
		public virtual uint Write(byte[] buff, uint bytes_to_write, out RailResult result)
		{
			return RAIL_API_PINVOKE.IRailFile_Write__SWIG_0(this.swigCPtr_, buff, bytes_to_write, out result);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000033A0 File Offset: 0x000015A0
		public virtual uint Write(byte[] buff, uint bytes_to_write)
		{
			return RAIL_API_PINVOKE.IRailFile_Write__SWIG_1(this.swigCPtr_, buff, bytes_to_write);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000033AF File Offset: 0x000015AF
		public virtual RailResult AsyncRead(uint bytes_to_read, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFile_AsyncRead(this.swigCPtr_, bytes_to_read, user_data);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000033BE File Offset: 0x000015BE
		public virtual RailResult AsyncWrite(byte[] buffer, uint bytes_to_write, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailFile_AsyncWrite(this.swigCPtr_, buffer, bytes_to_write, user_data);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000033CE File Offset: 0x000015CE
		public virtual uint GetSize()
		{
			return RAIL_API_PINVOKE.IRailFile_GetSize(this.swigCPtr_);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000033DB File Offset: 0x000015DB
		public virtual void Close()
		{
			RAIL_API_PINVOKE.IRailFile_Close(this.swigCPtr_);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
