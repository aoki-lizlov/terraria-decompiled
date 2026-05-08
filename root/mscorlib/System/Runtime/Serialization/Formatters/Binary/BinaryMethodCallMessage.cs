using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200067D RID: 1661
	[Serializable]
	internal sealed class BinaryMethodCallMessage
	{
		// Token: 0x06003E7F RID: 15999 RVA: 0x000D9120 File Offset: 0x000D7320
		[SecurityCritical]
		internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
		{
			this._methodName = methodName;
			this._typeName = typeName;
			if (args == null)
			{
				args = new object[0];
			}
			this._inargs = args;
			this._args = args;
			this._instArgs = instArgs;
			this._methodSignature = methodSignature;
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

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003E80 RID: 16000 RVA: 0x000D918E File Offset: 0x000D738E
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003E81 RID: 16001 RVA: 0x000D9196 File Offset: 0x000D7396
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003E82 RID: 16002 RVA: 0x000D919E File Offset: 0x000D739E
		public Type[] InstantiationArgs
		{
			get
			{
				return this._instArgs;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003E83 RID: 16003 RVA: 0x000D91A6 File Offset: 0x000D73A6
		public object MethodSignature
		{
			get
			{
				return this._methodSignature;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x000D91AE File Offset: 0x000D73AE
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003E85 RID: 16005 RVA: 0x000D91B6 File Offset: 0x000D73B6
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x000D91BE File Offset: 0x000D73BE
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x000D91CC File Offset: 0x000D73CC
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x04002873 RID: 10355
		private object[] _inargs;

		// Token: 0x04002874 RID: 10356
		private string _methodName;

		// Token: 0x04002875 RID: 10357
		private string _typeName;

		// Token: 0x04002876 RID: 10358
		private object _methodSignature;

		// Token: 0x04002877 RID: 10359
		private Type[] _instArgs;

		// Token: 0x04002878 RID: 10360
		private object[] _args;

		// Token: 0x04002879 RID: 10361
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x0400287A RID: 10362
		private object[] _properties;
	}
}
