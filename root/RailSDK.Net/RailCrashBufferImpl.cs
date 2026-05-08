using System;

namespace rail
{
	// Token: 0x02000038 RID: 56
	public class RailCrashBufferImpl : RailObject, RailCrashBuffer
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x00002137 File Offset: 0x00000337
		internal RailCrashBufferImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00008C0C File Offset: 0x00006E0C
		~RailCrashBufferImpl()
		{
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00008C34 File Offset: 0x00006E34
		public virtual string GetData()
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_GetData(this.swigCPtr_);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00008C41 File Offset: 0x00006E41
		public virtual uint GetBufferLength()
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_GetBufferLength(this.swigCPtr_);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual uint GetValidLength()
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_GetValidLength(this.swigCPtr_);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00008C5B File Offset: 0x00006E5B
		public virtual uint SetData(string data, uint length, uint offset)
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_SetData__SWIG_0(this.swigCPtr_, data, length, offset);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00008C6B File Offset: 0x00006E6B
		public virtual uint SetData(string data, uint length)
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_SetData__SWIG_1(this.swigCPtr_, data, length);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00008C7A File Offset: 0x00006E7A
		public virtual uint AppendData(string data, uint length)
		{
			return RAIL_API_PINVOKE.RailCrashBuffer_AppendData(this.swigCPtr_, data, length);
		}
	}
}
