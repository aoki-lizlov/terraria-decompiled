using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200067E RID: 1662
	[Serializable]
	internal class BinaryMethodReturnMessage
	{
		// Token: 0x06003E88 RID: 16008 RVA: 0x000D920C File Offset: 0x000D740C
		[SecurityCritical]
		internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
		{
			this._returnValue = returnValue;
			if (args == null)
			{
				args = new object[0];
			}
			this._outargs = args;
			this._args = args;
			this._exception = e;
			if (callContext == null)
			{
				this._logicalCallContext = new LogicalCallContext();
			}
			else
			{
				this._logicalCallContext = callContext;
			}
			this._properties = properties;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000D9267 File Offset: 0x000D7467
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000D926F File Offset: 0x000D746F
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003E8B RID: 16011 RVA: 0x000D9277 File Offset: 0x000D7477
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x000D927F File Offset: 0x000D747F
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000D9287 File Offset: 0x000D7487
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x000D9294 File Offset: 0x000D7494
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x0400287B RID: 10363
		private object[] _outargs;

		// Token: 0x0400287C RID: 10364
		private Exception _exception;

		// Token: 0x0400287D RID: 10365
		private object _returnValue;

		// Token: 0x0400287E RID: 10366
		private object[] _args;

		// Token: 0x0400287F RID: 10367
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04002880 RID: 10368
		private object[] _properties;
	}
}
