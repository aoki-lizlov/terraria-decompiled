using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000605 RID: 1541
	[ComVisible(true)]
	public class RemotingSurrogateSelector : ISurrogateSelector
	{
		// Token: 0x06003B81 RID: 15233 RVA: 0x000025BE File Offset: 0x000007BE
		public RemotingSurrogateSelector()
		{
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x000D03F1 File Offset: 0x000CE5F1
		// (set) Token: 0x06003B83 RID: 15235 RVA: 0x000D03F9 File Offset: 0x000CE5F9
		public MessageSurrogateFilter Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000D0402 File Offset: 0x000CE602
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			if (this._next != null)
			{
				selector.ChainSelector(this._next);
			}
			this._next = selector;
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000D041F File Offset: 0x000CE61F
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this._next;
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000D0427 File Offset: 0x000CE627
		public object GetRootObject()
		{
			return this._rootObj;
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000D0430 File Offset: 0x000CE630
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
		{
			if (type.IsMarshalByRef)
			{
				ssout = this;
				return RemotingSurrogateSelector._objRemotingSurrogate;
			}
			if (RemotingSurrogateSelector.s_cachedTypeObjRef.IsAssignableFrom(type))
			{
				ssout = this;
				return RemotingSurrogateSelector._objRefSurrogate;
			}
			if (this._next != null)
			{
				return this._next.GetSurrogate(type, context, out ssout);
			}
			ssout = null;
			return null;
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000D047F File Offset: 0x000CE67F
		public void SetRootObject(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			this._rootObj = obj;
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public virtual void UseSoapFormat()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000D0491 File Offset: 0x000CE691
		// Note: this type is marked as 'beforefieldinit'.
		static RemotingSurrogateSelector()
		{
		}

		// Token: 0x0400265A RID: 9818
		private static Type s_cachedTypeObjRef = typeof(ObjRef);

		// Token: 0x0400265B RID: 9819
		private static ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();

		// Token: 0x0400265C RID: 9820
		private static RemotingSurrogate _objRemotingSurrogate = new RemotingSurrogate();

		// Token: 0x0400265D RID: 9821
		private object _rootObj;

		// Token: 0x0400265E RID: 9822
		private MessageSurrogateFilter _filter;

		// Token: 0x0400265F RID: 9823
		private ISurrogateSelector _next;
	}
}
