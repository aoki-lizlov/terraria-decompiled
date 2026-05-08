using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000606 RID: 1542
	[ComVisible(true)]
	public class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage, IInternalMessage
	{
		// Token: 0x06003B8B RID: 15243 RVA: 0x000D04B8 File Offset: 0x000CE6B8
		public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			this._returnValue = ret;
			this._args = outArgs;
			this._callCtx = callCtx;
			if (mcm != null)
			{
				this._uri = mcm.Uri;
				this._methodBase = mcm.MethodBase;
			}
			if (this._args == null)
			{
				this._args = new object[outArgsCount];
			}
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000D0513 File Offset: 0x000CE713
		public ReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			this._exception = e;
			if (mcm != null)
			{
				this._methodBase = mcm.MethodBase;
				this._callCtx = mcm.LogicalCallContext;
			}
			this._args = new object[0];
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x000D0549 File Offset: 0x000CE749
		public int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06003B8E RID: 15246 RVA: 0x000D0553 File Offset: 0x000CE753
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x000D055B File Offset: 0x000CE75B
		public bool HasVarArgs
		{
			get
			{
				return !(this._methodBase == null) && (this._methodBase.CallingConvention | CallingConventions.VarArgs) > (CallingConventions)0;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x000D057D File Offset: 0x000CE77D
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._callCtx == null)
				{
					this._callCtx = new LogicalCallContext();
				}
				return this._callCtx;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000D0598 File Offset: 0x000CE798
		public MethodBase MethodBase
		{
			get
			{
				return this._methodBase;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x000D05A0 File Offset: 0x000CE7A0
		public string MethodName
		{
			get
			{
				if (this._methodBase != null && this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x000D05D0 File Offset: 0x000CE7D0
		public object MethodSignature
		{
			get
			{
				if (this._methodBase != null && this._methodSignature == null)
				{
					ParameterInfo[] parameters = this._methodBase.GetParameters();
					this._methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this._methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this._methodSignature;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x000D0631 File Offset: 0x000CE831
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnDictionary(this);
				}
				return this._properties;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000D064D File Offset: 0x000CE84D
		public string TypeName
		{
			get
			{
				if (this._methodBase != null && this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000D0681 File Offset: 0x000CE881
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x000D0689 File Offset: 0x000CE889
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x000D0692 File Offset: 0x000CE892
		// (set) Token: 0x06003B99 RID: 15257 RVA: 0x000D069A File Offset: 0x000CE89A
		string IInternalMessage.Uri
		{
			get
			{
				return this.Uri;
			}
			set
			{
				this.Uri = value;
			}
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000D06A3 File Offset: 0x000CE8A3
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x000D06AD File Offset: 0x000CE8AD
		public string GetArgName(int index)
		{
			return this._methodBase.GetParameters()[index].Name;
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x000D06C1 File Offset: 0x000CE8C1
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x000D06C9 File Offset: 0x000CE8C9
		public int OutArgCount
		{
			get
			{
				if (this._args == null || this._args.Length == 0)
				{
					return 0;
				}
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000D0704 File Offset: 0x000CE904
		public object[] OutArgs
		{
			get
			{
				if (this._outArgs == null && this._args != null)
				{
					if (this._inArgInfo == null)
					{
						this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
					}
					this._outArgs = this._inArgInfo.GetInOutArgs(this._args);
				}
				return this._outArgs;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x000D0758 File Offset: 0x000CE958
		public virtual object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000D0760 File Offset: 0x000CE960
		public object GetOutArg(int argNum)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000D078F File Offset: 0x000CE98F
		public string GetOutArgName(int index)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000D07B7 File Offset: 0x000CE9B7
		// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x000D07BF File Offset: 0x000CE9BF
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this._targetIdentity;
			}
			set
			{
				this._targetIdentity = value;
			}
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000D07C8 File Offset: 0x000CE9C8
		bool IInternalMessage.HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000D07C8 File Offset: 0x000CE9C8
		internal bool HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x04002660 RID: 9824
		private object[] _outArgs;

		// Token: 0x04002661 RID: 9825
		private object[] _args;

		// Token: 0x04002662 RID: 9826
		private LogicalCallContext _callCtx;

		// Token: 0x04002663 RID: 9827
		private object _returnValue;

		// Token: 0x04002664 RID: 9828
		private string _uri;

		// Token: 0x04002665 RID: 9829
		private Exception _exception;

		// Token: 0x04002666 RID: 9830
		private MethodBase _methodBase;

		// Token: 0x04002667 RID: 9831
		private string _methodName;

		// Token: 0x04002668 RID: 9832
		private Type[] _methodSignature;

		// Token: 0x04002669 RID: 9833
		private string _typeName;

		// Token: 0x0400266A RID: 9834
		private MethodReturnDictionary _properties;

		// Token: 0x0400266B RID: 9835
		private Identity _targetIdentity;

		// Token: 0x0400266C RID: 9836
		private ArgInfo _inArgInfo;
	}
}
