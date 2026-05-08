using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E6 RID: 1510
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06003A68 RID: 14952 RVA: 0x000CDCBF File Offset: 0x000CBEBF
		public ConstructionResponse(Header[] h, IMethodCallMessage mcm)
			: base(h, mcm)
		{
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000CDCC9 File Offset: 0x000CBEC9
		internal ConstructionResponse(object resultObject, LogicalCallContext callCtx, IMethodCallMessage msg)
			: base(resultObject, null, callCtx, msg)
		{
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000CDCD5 File Offset: 0x000CBED5
		internal ConstructionResponse(Exception e, IMethodCallMessage msg)
			: base(e, msg)
		{
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000CDCDF File Offset: 0x000CBEDF
		internal ConstructionResponse(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x000CDCE9 File Offset: 0x000CBEE9
		public override IDictionary Properties
		{
			get
			{
				return base.Properties;
			}
		}
	}
}
