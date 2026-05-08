using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200055C RID: 1372
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class Context
	{
		// Token: 0x06003723 RID: 14115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterContext(Context ctx);

		// Token: 0x06003724 RID: 14116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseContext(Context ctx);

		// Token: 0x06003725 RID: 14117 RVA: 0x000C7B0C File Offset: 0x000C5D0C
		public Context()
		{
			this.domain_id = Thread.GetDomainID();
			this.context_id = Interlocked.Increment(ref Context.global_count);
			Context.RegisterContext(this);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000C7B38 File Offset: 0x000C5D38
		~Context()
		{
			Context.ReleaseContext(this);
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000C7B64 File Offset: 0x000C5D64
		public static Context DefaultContext
		{
			get
			{
				return AppDomain.InternalGetDefaultContext();
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x000C7B6B File Offset: 0x000C5D6B
		public virtual int ContextID
		{
			get
			{
				return this.context_id;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x000C7B73 File Offset: 0x000C5D73
		public virtual IContextProperty[] ContextProperties
		{
			get
			{
				if (this.context_properties == null)
				{
					return new IContextProperty[0];
				}
				return this.context_properties.ToArray();
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x000C7B8F File Offset: 0x000C5D8F
		internal bool IsDefaultContext
		{
			get
			{
				return this.context_id == 0;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x000C7B9A File Offset: 0x000C5D9A
		internal bool NeedsContextSink
		{
			get
			{
				return this.context_id != 0 || (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties) || (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties);
			}
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000C7BCE File Offset: 0x000C5DCE
		public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
		{
			return Context.GetDynamicPropertyCollection(obj, ctx).RegisterDynamicProperty(prop);
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000C7BDD File Offset: 0x000C5DDD
		public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
		{
			return Context.GetDynamicPropertyCollection(obj, ctx).UnregisterDynamicProperty(name);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000C7BEC File Offset: 0x000C5DEC
		private static DynamicPropertyCollection GetDynamicPropertyCollection(ContextBoundObject obj, Context ctx)
		{
			if (ctx == null && obj != null)
			{
				if (RemotingServices.IsTransparentProxy(obj))
				{
					return RemotingServices.GetRealProxy(obj).ObjectIdentity.ClientDynamicProperties;
				}
				return obj.ObjectIdentity.ServerDynamicProperties;
			}
			else
			{
				if (ctx != null && obj == null)
				{
					if (ctx.context_dynamic_properties == null)
					{
						ctx.context_dynamic_properties = new DynamicPropertyCollection();
					}
					return ctx.context_dynamic_properties;
				}
				if (ctx == null && obj == null)
				{
					if (Context.global_dynamic_properties == null)
					{
						Context.global_dynamic_properties = new DynamicPropertyCollection();
					}
					return Context.global_dynamic_properties;
				}
				throw new ArgumentException("Either obj or ctx must be null");
			}
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000C7C6B File Offset: 0x000C5E6B
		internal static void NotifyGlobalDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties)
			{
				Context.global_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x000C7C8E File Offset: 0x000C5E8E
		internal static bool HasGlobalDynamicSinks
		{
			get
			{
				return Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000C7CA3 File Offset: 0x000C5EA3
		internal void NotifyDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties)
			{
				this.context_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x000C7CCA File Offset: 0x000C5ECA
		internal bool HasDynamicSinks
		{
			get
			{
				return this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000C7CE1 File Offset: 0x000C5EE1
		internal bool HasExitSinks
		{
			get
			{
				return !(this.GetClientContextSinkChain() is ClientContextTerminatorSink) || this.HasDynamicSinks || Context.HasGlobalDynamicSinks;
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000C7D00 File Offset: 0x000C5F00
		public virtual IContextProperty GetProperty(string name)
		{
			if (this.context_properties == null)
			{
				return null;
			}
			foreach (IContextProperty contextProperty in this.context_properties)
			{
				if (contextProperty.Name == name)
				{
					return contextProperty;
				}
			}
			return null;
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000C7D6C File Offset: 0x000C5F6C
		public virtual void SetProperty(IContextProperty prop)
		{
			if (prop == null)
			{
				throw new ArgumentNullException("IContextProperty");
			}
			if (this == Context.DefaultContext)
			{
				throw new InvalidOperationException("Can not add properties to default context");
			}
			if (this.context_properties == null)
			{
				this.context_properties = new List<IContextProperty>();
			}
			this.context_properties.Add(prop);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000C7DBC File Offset: 0x000C5FBC
		public virtual void Freeze()
		{
			if (this.context_properties != null)
			{
				foreach (IContextProperty contextProperty in this.context_properties)
				{
					contextProperty.Freeze(this);
				}
			}
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000C7E18 File Offset: 0x000C6018
		public override string ToString()
		{
			return "ContextID: " + this.context_id.ToString();
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000C7E30 File Offset: 0x000C6030
		internal IMessageSink GetServerContextSinkChain()
		{
			if (this.server_context_sink_chain == null)
			{
				if (Context.default_server_context_sink == null)
				{
					Context.default_server_context_sink = new ServerContextTerminatorSink();
				}
				this.server_context_sink_chain = Context.default_server_context_sink;
				if (this.context_properties != null)
				{
					for (int i = this.context_properties.Count - 1; i >= 0; i--)
					{
						IContributeServerContextSink contributeServerContextSink = this.context_properties[i] as IContributeServerContextSink;
						if (contributeServerContextSink != null)
						{
							this.server_context_sink_chain = contributeServerContextSink.GetServerContextSink(this.server_context_sink_chain);
						}
					}
				}
			}
			return this.server_context_sink_chain;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000C7EB0 File Offset: 0x000C60B0
		internal IMessageSink GetClientContextSinkChain()
		{
			if (this.client_context_sink_chain == null)
			{
				this.client_context_sink_chain = new ClientContextTerminatorSink(this);
				if (this.context_properties != null)
				{
					foreach (IContextProperty contextProperty in this.context_properties)
					{
						IContributeClientContextSink contributeClientContextSink = contextProperty as IContributeClientContextSink;
						if (contributeClientContextSink != null)
						{
							this.client_context_sink_chain = contributeClientContextSink.GetClientContextSink(this.client_context_sink_chain);
						}
					}
				}
			}
			return this.client_context_sink_chain;
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000C7F38 File Offset: 0x000C6138
		internal IMessageSink CreateServerObjectSinkChain(MarshalByRefObject obj, bool forceInternalExecute)
		{
			IMessageSink messageSink = new StackBuilderSink(obj, forceInternalExecute);
			messageSink = new ServerObjectTerminatorSink(messageSink);
			messageSink = new LeaseSink(messageSink);
			if (this.context_properties != null)
			{
				for (int i = this.context_properties.Count - 1; i >= 0; i--)
				{
					IContributeObjectSink contributeObjectSink = this.context_properties[i] as IContributeObjectSink;
					if (contributeObjectSink != null)
					{
						messageSink = contributeObjectSink.GetObjectSink(obj, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000C7F9C File Offset: 0x000C619C
		internal IMessageSink CreateEnvoySink(MarshalByRefObject serverObject)
		{
			IMessageSink messageSink = EnvoyTerminatorSink.Instance;
			if (this.context_properties != null)
			{
				foreach (IContextProperty contextProperty in this.context_properties)
				{
					IContributeEnvoySink contributeEnvoySink = contextProperty as IContributeEnvoySink;
					if (contributeEnvoySink != null)
					{
						messageSink = contributeEnvoySink.GetEnvoySink(serverObject, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000C8008 File Offset: 0x000C6208
		internal static Context SwitchToContext(Context newContext)
		{
			return AppDomain.InternalSetContext(newContext);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000C8010 File Offset: 0x000C6210
		internal static Context CreateNewContext(IConstructionCallMessage msg)
		{
			Context context = new Context();
			foreach (object obj in msg.ContextProperties)
			{
				IContextProperty contextProperty = (IContextProperty)obj;
				if (context.GetProperty(contextProperty.Name) == null)
				{
					context.SetProperty(contextProperty);
				}
			}
			context.Freeze();
			using (IEnumerator enumerator = msg.ContextProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((IContextProperty)enumerator.Current).IsNewContextOK(context))
					{
						throw new RemotingException("A context property did not approve the candidate context for activating the object");
					}
				}
			}
			return context;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000C80D8 File Offset: 0x000C62D8
		public void DoCallBack(CrossContextDelegate deleg)
		{
			lock (this)
			{
				if (this.callback_object == null)
				{
					Context context = Context.SwitchToContext(this);
					this.callback_object = new ContextCallbackObject();
					Context.SwitchToContext(context);
				}
			}
			this.callback_object.DoCallBack(deleg);
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000C8138 File Offset: 0x000C6338
		private LocalDataStore MyLocalStore
		{
			get
			{
				if (this._localDataStore == null)
				{
					LocalDataStoreMgr localDataStoreMgr = Context._localDataStoreMgr;
					lock (localDataStoreMgr)
					{
						if (this._localDataStore == null)
						{
							this._localDataStore = Context._localDataStoreMgr.CreateLocalDataStore();
						}
					}
				}
				return this._localDataStore.Store;
			}
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000C81A4 File Offset: 0x000C63A4
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Context._localDataStoreMgr.AllocateDataSlot();
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000C81B0 File Offset: 0x000C63B0
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.AllocateNamedDataSlot(name);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000C81BD File Offset: 0x000C63BD
		public static void FreeNamedDataSlot(string name)
		{
			Context._localDataStoreMgr.FreeNamedDataSlot(name);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000C81CA File Offset: 0x000C63CA
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.GetNamedDataSlot(name);
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000C81D7 File Offset: 0x000C63D7
		public static object GetData(LocalDataStoreSlot slot)
		{
			return Thread.CurrentContext.MyLocalStore.GetData(slot);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000C81E9 File Offset: 0x000C63E9
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			Thread.CurrentContext.MyLocalStore.SetData(slot, data);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000C81FC File Offset: 0x000C63FC
		// Note: this type is marked as 'beforefieldinit'.
		static Context()
		{
		}

		// Token: 0x04002524 RID: 9508
		private int domain_id;

		// Token: 0x04002525 RID: 9509
		private int context_id;

		// Token: 0x04002526 RID: 9510
		private UIntPtr static_data;

		// Token: 0x04002527 RID: 9511
		private UIntPtr data;

		// Token: 0x04002528 RID: 9512
		[ContextStatic]
		private static object[] local_slots;

		// Token: 0x04002529 RID: 9513
		private static IMessageSink default_server_context_sink;

		// Token: 0x0400252A RID: 9514
		private IMessageSink server_context_sink_chain;

		// Token: 0x0400252B RID: 9515
		private IMessageSink client_context_sink_chain;

		// Token: 0x0400252C RID: 9516
		private List<IContextProperty> context_properties;

		// Token: 0x0400252D RID: 9517
		private static int global_count;

		// Token: 0x0400252E RID: 9518
		private volatile LocalDataStoreHolder _localDataStore;

		// Token: 0x0400252F RID: 9519
		private static LocalDataStoreMgr _localDataStoreMgr = new LocalDataStoreMgr();

		// Token: 0x04002530 RID: 9520
		private static DynamicPropertyCollection global_dynamic_properties;

		// Token: 0x04002531 RID: 9521
		private DynamicPropertyCollection context_dynamic_properties;

		// Token: 0x04002532 RID: 9522
		private ContextCallbackObject callback_object;
	}
}
