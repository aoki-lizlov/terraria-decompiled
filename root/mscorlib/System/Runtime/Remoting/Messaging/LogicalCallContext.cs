using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005D5 RID: 1493
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class LogicalCallContext : ISerializable, ICloneable
	{
		// Token: 0x060039E6 RID: 14822 RVA: 0x000025BE File Offset: 0x000007BE
		internal LogicalCallContext()
		{
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000CC248 File Offset: 0x000CA448
		[SecurityCritical]
		internal LogicalCallContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("__RemotingData"))
				{
					this.m_RemotingData = (CallContextRemotingData)enumerator.Value;
				}
				else if (enumerator.Name.Equals("__SecurityData"))
				{
					if (context.State == StreamingContextStates.CrossAppDomain)
					{
						this.m_SecurityData = (CallContextSecurityData)enumerator.Value;
					}
				}
				else if (enumerator.Name.Equals("__HostContext"))
				{
					this.m_HostContext = enumerator.Value;
				}
				else if (enumerator.Name.Equals("__CorrelationMgrSlotPresent"))
				{
					this.m_IsCorrelationMgr = (bool)enumerator.Value;
				}
				else
				{
					this.Datastore[enumerator.Name] = enumerator.Value;
				}
			}
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000CC32C File Offset: 0x000CA52C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(LogicalCallContext.s_callContextType);
			if (this.m_RemotingData != null)
			{
				info.AddValue("__RemotingData", this.m_RemotingData);
			}
			if (this.m_SecurityData != null && context.State == StreamingContextStates.CrossAppDomain)
			{
				info.AddValue("__SecurityData", this.m_SecurityData);
			}
			if (this.m_HostContext != null)
			{
				info.AddValue("__HostContext", this.m_HostContext);
			}
			if (this.m_IsCorrelationMgr)
			{
				info.AddValue("__CorrelationMgrSlotPresent", this.m_IsCorrelationMgr);
			}
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					info.AddValue((string)enumerator.Key, enumerator.Value);
				}
			}
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000CC3FC File Offset: 0x000CA5FC
		[SecuritySafeCritical]
		public object Clone()
		{
			LogicalCallContext logicalCallContext = new LogicalCallContext();
			if (this.m_RemotingData != null)
			{
				logicalCallContext.m_RemotingData = (CallContextRemotingData)this.m_RemotingData.Clone();
			}
			if (this.m_SecurityData != null)
			{
				logicalCallContext.m_SecurityData = (CallContextSecurityData)this.m_SecurityData.Clone();
			}
			if (this.m_HostContext != null)
			{
				logicalCallContext.m_HostContext = this.m_HostContext;
			}
			logicalCallContext.m_IsCorrelationMgr = this.m_IsCorrelationMgr;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				if (!this.m_IsCorrelationMgr)
				{
					while (enumerator.MoveNext())
					{
						logicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
					}
				}
				else
				{
					while (enumerator.MoveNext())
					{
						string text = (string)enumerator.Key;
						if (text.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
						{
							logicalCallContext.Datastore[text] = ((ICloneable)enumerator.Value).Clone();
						}
						else
						{
							logicalCallContext.Datastore[text] = enumerator.Value;
						}
					}
				}
			}
			return logicalCallContext;
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000CC504 File Offset: 0x000CA704
		[SecurityCritical]
		internal void Merge(LogicalCallContext lc)
		{
			if (lc != null && this != lc && lc.HasUserData)
			{
				IDictionaryEnumerator enumerator = lc.Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000CC554 File Offset: 0x000CA754
		public bool HasInfo
		{
			[SecurityCritical]
			get
			{
				bool flag = false;
				if ((this.m_RemotingData != null && this.m_RemotingData.HasInfo) || (this.m_SecurityData != null && this.m_SecurityData.HasInfo) || this.m_HostContext != null || this.HasUserData)
				{
					flag = true;
				}
				return flag;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000CC5A0 File Offset: 0x000CA7A0
		private bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000CC5BA File Offset: 0x000CA7BA
		internal CallContextRemotingData RemotingData
		{
			get
			{
				if (this.m_RemotingData == null)
				{
					this.m_RemotingData = new CallContextRemotingData();
				}
				return this.m_RemotingData;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000CC5D5 File Offset: 0x000CA7D5
		internal CallContextSecurityData SecurityData
		{
			get
			{
				if (this.m_SecurityData == null)
				{
					this.m_SecurityData = new CallContextSecurityData();
				}
				return this.m_SecurityData;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x000CC5F0 File Offset: 0x000CA7F0
		// (set) Token: 0x060039F0 RID: 14832 RVA: 0x000CC5F8 File Offset: 0x000CA7F8
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000CC601 File Offset: 0x000CA801
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x000CC61C File Offset: 0x000CA81C
		// (set) Token: 0x060039F3 RID: 14835 RVA: 0x000CC633 File Offset: 0x000CA833
		internal IPrincipal Principal
		{
			get
			{
				if (this.m_SecurityData != null)
				{
					return this.m_SecurityData.Principal;
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				this.SecurityData.Principal = value;
			}
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000CC641 File Offset: 0x000CA841
		[SecurityCritical]
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000CC64F File Offset: 0x000CA84F
		[SecurityCritical]
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000CC65D File Offset: 0x000CA85D
		[SecurityCritical]
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
			if (name.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
			{
				this.m_IsCorrelationMgr = true;
			}
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000CC680 File Offset: 0x000CA880
		private Header[] InternalGetOutgoingHeaders()
		{
			Header[] sendHeaders = this._sendHeaders;
			this._sendHeaders = null;
			this._recvHeaders = null;
			return sendHeaders;
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000CC696 File Offset: 0x000CA896
		internal void InternalSetHeaders(Header[] headers)
		{
			this._sendHeaders = headers;
			this._recvHeaders = null;
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000CC6A6 File Offset: 0x000CA8A6
		internal Header[] InternalGetHeaders()
		{
			if (this._sendHeaders != null)
			{
				return this._sendHeaders;
			}
			return this._recvHeaders;
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000CC6C0 File Offset: 0x000CA8C0
		[SecurityCritical]
		internal IPrincipal RemovePrincipalIfNotSerializable()
		{
			IPrincipal principal = this.Principal;
			if (principal != null && !principal.GetType().IsSerializable)
			{
				this.Principal = null;
			}
			return principal;
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000CC6EC File Offset: 0x000CA8EC
		[SecurityCritical]
		internal void PropagateOutgoingHeadersToMessage(IMessage msg)
		{
			Header[] array = this.InternalGetOutgoingHeaders();
			if (array != null)
			{
				IDictionary properties = msg.Properties;
				foreach (Header header in array)
				{
					if (header != null)
					{
						string propertyKeyForHeader = LogicalCallContext.GetPropertyKeyForHeader(header);
						properties[propertyKeyForHeader] = header;
					}
				}
			}
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000CC736 File Offset: 0x000CA936
		internal static string GetPropertyKeyForHeader(Header header)
		{
			if (header == null)
			{
				return null;
			}
			if (header.HeaderNamespace != null)
			{
				return header.Name + ", " + header.HeaderNamespace;
			}
			return header.Name;
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000CC764 File Offset: 0x000CA964
		[SecurityCritical]
		internal void PropagateIncomingHeadersToCallContext(IMessage msg)
		{
			IInternalMessage internalMessage = msg as IInternalMessage;
			if (internalMessage != null && !internalMessage.HasProperties())
			{
				return;
			}
			IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				if (!((string)enumerator.Key).StartsWith("__", StringComparison.Ordinal) && enumerator.Value is Header)
				{
					num++;
				}
			}
			Header[] array = null;
			if (num > 0)
			{
				array = new Header[num];
				num = 0;
				enumerator.Reset();
				while (enumerator.MoveNext())
				{
					if (!((string)enumerator.Key).StartsWith("__", StringComparison.Ordinal))
					{
						Header header = enumerator.Value as Header;
						if (header != null)
						{
							array[num++] = header;
						}
					}
				}
			}
			this._recvHeaders = array;
			this._sendHeaders = null;
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000CC824 File Offset: 0x000CAA24
		// Note: this type is marked as 'beforefieldinit'.
		static LogicalCallContext()
		{
		}

		// Token: 0x040025D1 RID: 9681
		private static Type s_callContextType = typeof(LogicalCallContext);

		// Token: 0x040025D2 RID: 9682
		private const string s_CorrelationMgrSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x040025D3 RID: 9683
		private Hashtable m_Datastore;

		// Token: 0x040025D4 RID: 9684
		private CallContextRemotingData m_RemotingData;

		// Token: 0x040025D5 RID: 9685
		private CallContextSecurityData m_SecurityData;

		// Token: 0x040025D6 RID: 9686
		private object m_HostContext;

		// Token: 0x040025D7 RID: 9687
		private bool m_IsCorrelationMgr;

		// Token: 0x040025D8 RID: 9688
		private Header[] _sendHeaders;

		// Token: 0x040025D9 RID: 9689
		private Header[] _recvHeaders;

		// Token: 0x020005D6 RID: 1494
		internal struct Reader
		{
			// Token: 0x060039FF RID: 14847 RVA: 0x000CC835 File Offset: 0x000CAA35
			public Reader(LogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000CC83E File Offset: 0x000CAA3E
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x1700087E RID: 2174
			// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000CC849 File Offset: 0x000CAA49
			public bool HasInfo
			{
				get
				{
					return !this.IsNull && this.m_ctx.HasInfo;
				}
			}

			// Token: 0x06003A02 RID: 14850 RVA: 0x000CC860 File Offset: 0x000CAA60
			public LogicalCallContext Clone()
			{
				return (LogicalCallContext)this.m_ctx.Clone();
			}

			// Token: 0x1700087F RID: 2175
			// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000CC872 File Offset: 0x000CAA72
			public IPrincipal Principal
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.Principal;
					}
					return null;
				}
			}

			// Token: 0x06003A04 RID: 14852 RVA: 0x000CC889 File Offset: 0x000CAA89
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x17000880 RID: 2176
			// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000CC8A1 File Offset: 0x000CAAA1
			public object HostContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.HostContext;
					}
					return null;
				}
			}

			// Token: 0x040025DA RID: 9690
			private LogicalCallContext m_ctx;
		}
	}
}
