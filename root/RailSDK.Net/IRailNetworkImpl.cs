using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000021 RID: 33
	public class IRailNetworkImpl : RailObject, IRailNetwork
	{
		// Token: 0x0600121C RID: 4636 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailNetworkImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0000514C File Offset: 0x0000334C
		~IRailNetworkImpl()
		{
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00005174 File Offset: 0x00003374
		public virtual RailResult AcceptSessionRequest(RailID local_peer, RailID remote_peer)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_AcceptSessionRequest(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x000051FC File Offset: 0x000033FC
		public virtual RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendData__SWIG_0(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0000528C File Offset: 0x0000348C
		public virtual RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendData__SWIG_1(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00005318 File Offset: 0x00003518
		public virtual RailResult SendReliableData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendReliableData__SWIG_0(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000053A8 File Offset: 0x000035A8
		public virtual RailResult SendReliableData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendReliableData__SWIG_1(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00005434 File Offset: 0x00003634
		public virtual bool IsDataReady(RailID local_peer, out uint data_len, out uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailNetwork_IsDataReady__SWIG_0(this.swigCPtr_, intPtr, out data_len, out message_type);
			}
			finally
			{
				if (local_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr, local_peer);
				}
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00005490 File Offset: 0x00003690
		public virtual bool IsDataReady(RailID local_peer, out uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailNetwork_IsDataReady__SWIG_1(this.swigCPtr_, intPtr, out data_len);
			}
			finally
			{
				if (local_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr, local_peer);
				}
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x000054EC File Offset: 0x000036EC
		public virtual RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_ReadData__SWIG_0(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (remote_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, remote_peer);
				}
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00005578 File Offset: 0x00003778
		public virtual RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_ReadData__SWIG_1(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (remote_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, remote_peer);
				}
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00005604 File Offset: 0x00003804
		public virtual RailResult BlockMessageType(RailID local_peer, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_BlockMessageType(this.swigCPtr_, intPtr, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00005660 File Offset: 0x00003860
		public virtual RailResult UnblockMessageType(RailID local_peer, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_UnblockMessageType(this.swigCPtr_, intPtr, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000056BC File Offset: 0x000038BC
		public virtual RailResult CloseSession(RailID local_peer, RailID remote_peer)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_CloseSession(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00005744 File Offset: 0x00003944
		public virtual RailResult ResolveHostname(string domain, List<string> ip_list)
		{
			IntPtr intPtr = ((ip_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_ResolveHostname(this.swigCPtr_, domain, intPtr);
			}
			finally
			{
				if (ip_list != null)
				{
					RailConverter.Cpp2Csharp(intPtr, ip_list);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00005794 File Offset: 0x00003994
		public virtual RailResult GetSessionState(RailID remote_peer, RailNetworkSessionState session_state)
		{
			IntPtr intPtr = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr);
			}
			IntPtr intPtr2 = ((session_state == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailNetworkSessionState__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_GetSessionState(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (session_state != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, session_state);
				}
				RAIL_API_PINVOKE.delete_RailNetworkSessionState(intPtr2);
			}
			return railResult;
		}
	}
}
