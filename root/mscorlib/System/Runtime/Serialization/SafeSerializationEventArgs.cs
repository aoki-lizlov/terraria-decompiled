using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x0200063A RID: 1594
	public sealed class SafeSerializationEventArgs : EventArgs
	{
		// Token: 0x06003CEB RID: 15595 RVA: 0x000D413E File Offset: 0x000D233E
		internal SafeSerializationEventArgs(StreamingContext streamingContext)
		{
			this.m_streamingContext = streamingContext;
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000D4158 File Offset: 0x000D2358
		public void AddSerializedState(ISafeSerializationData serializedState)
		{
			if (serializedState == null)
			{
				throw new ArgumentNullException("serializedState");
			}
			if (!serializedState.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Type '{0}' in Assembly '{1}' is not marked as serializable.", new object[]
				{
					serializedState.GetType(),
					serializedState.GetType().Assembly.FullName
				}));
			}
			this.m_serializedStates.Add(serializedState);
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x000D41BE File Offset: 0x000D23BE
		internal IList<object> SerializedStates
		{
			get
			{
				return this.m_serializedStates;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000D41C6 File Offset: 0x000D23C6
		public StreamingContext StreamingContext
		{
			get
			{
				return this.m_streamingContext;
			}
		}

		// Token: 0x040026FD RID: 9981
		private StreamingContext m_streamingContext;

		// Token: 0x040026FE RID: 9982
		private List<object> m_serializedStates = new List<object>();
	}
}
