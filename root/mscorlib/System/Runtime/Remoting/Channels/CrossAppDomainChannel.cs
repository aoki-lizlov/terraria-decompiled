using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200057E RID: 1406
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelSender, IChannelReceiver
	{
		// Token: 0x060037EB RID: 14315 RVA: 0x000C9D10 File Offset: 0x000C7F10
		internal static void RegisterCrossAppDomainChannel()
		{
			object obj = CrossAppDomainChannel.s_lock;
			lock (obj)
			{
				ChannelServices.RegisterChannel(new CrossAppDomainChannel());
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000C9D54 File Offset: 0x000C7F54
		public virtual string ChannelName
		{
			get
			{
				return "MONOCAD";
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x000C9D5B File Offset: 0x000C7F5B
		public virtual int ChannelPriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000C9D5F File Offset: 0x000C7F5F
		public string Parse(string url, out string objectURI)
		{
			objectURI = url;
			return null;
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x000C9D65 File Offset: 0x000C7F65
		public virtual object ChannelData
		{
			get
			{
				return new CrossAppDomainData(Thread.GetDomainID());
			}
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000C9D71 File Offset: 0x000C7F71
		public virtual string[] GetUrlsForUri(string objectURI)
		{
			throw new NotSupportedException("CrossAppdomain channel dont support UrlsForUri");
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void StopListening(object data)
		{
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000C9D80 File Offset: 0x000C7F80
		public virtual IMessageSink CreateMessageSink(string url, object data, out string uri)
		{
			uri = null;
			if (data != null)
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessID == RemotingConfiguration.ProcessId)
				{
					return CrossAppDomainSink.GetSink(crossAppDomainData.DomainID);
				}
			}
			if (url != null && url.StartsWith("MONOCAD"))
			{
				throw new NotSupportedException("Can't create a named channel via crossappdomain");
			}
			return null;
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000025BE File Offset: 0x000007BE
		public CrossAppDomainChannel()
		{
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000C9DD7 File Offset: 0x000C7FD7
		// Note: this type is marked as 'beforefieldinit'.
		static CrossAppDomainChannel()
		{
		}

		// Token: 0x04002564 RID: 9572
		private const string _strName = "MONOCAD";

		// Token: 0x04002565 RID: 9573
		private static object s_lock = new object();
	}
}
