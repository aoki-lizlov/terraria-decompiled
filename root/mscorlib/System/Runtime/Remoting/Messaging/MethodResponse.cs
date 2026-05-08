using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005FC RID: 1532
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage, ISerializationRootObject
	{
		// Token: 0x06003B16 RID: 15126 RVA: 0x000CF278 File Offset: 0x000CD478
		public MethodResponse(Header[] h1, IMethodCallMessage mcm)
		{
			if (mcm != null)
			{
				this._methodName = mcm.MethodName;
				this._uri = mcm.Uri;
				this._typeName = mcm.TypeName;
				this._methodBase = mcm.MethodBase;
				this._methodSignature = (Type[])mcm.MethodSignature;
				this._args = mcm.Args;
			}
			if (h1 != null)
			{
				foreach (Header header in h1)
				{
					this.InitMethodProperty(header.Name, header.Value);
				}
			}
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000CF304 File Offset: 0x000CD504
		internal MethodResponse(Exception e, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			if (msg != null)
			{
				this._uri = msg.Uri;
			}
			else
			{
				this._uri = string.Empty;
			}
			this._exception = e;
			this._returnValue = null;
			this._outArgs = new object[0];
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000CF354 File Offset: 0x000CD554
		internal MethodResponse(object returnValue, object[] outArgs, LogicalCallContext callCtx, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			this._uri = msg.Uri;
			this._exception = null;
			this._returnValue = returnValue;
			this._args = outArgs;
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000CF388 File Offset: 0x000CD588
		internal MethodResponse(IMethodCallMessage msg, CADMethodReturnMessage retmsg)
		{
			this._callMsg = msg;
			this._methodBase = msg.MethodBase;
			this._uri = msg.Uri;
			this._methodName = msg.MethodName;
			ArrayList arguments = retmsg.GetArguments();
			this._exception = retmsg.GetException(arguments);
			this._returnValue = retmsg.GetReturnValue(arguments);
			this._args = retmsg.GetArgs(arguments);
			this._callContext = retmsg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			if (retmsg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, retmsg.PropertiesCount, arguments);
			}
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000CF430 File Offset: 0x000CD630
		internal MethodResponse(IMethodCallMessage msg, object handlerObject, BinaryMethodReturnMessage smuggledMrm)
		{
			if (msg != null)
			{
				this._methodBase = msg.MethodBase;
				this._methodName = msg.MethodName;
				this._uri = msg.Uri;
			}
			this._returnValue = smuggledMrm.ReturnValue;
			this._args = smuggledMrm.Args;
			this._exception = smuggledMrm.Exception;
			this._callContext = smuggledMrm.LogicalCallContext;
			if (smuggledMrm.HasProperties)
			{
				smuggledMrm.PopulateMessageProperties(this.Properties);
			}
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000CF4B0 File Offset: 0x000CD6B0
		internal MethodResponse(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000CF4F0 File Offset: 0x000CD6F0
		internal void InitMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1960967436U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num == 1201911322U)
						{
							if (key == "__CallContext")
							{
								this._callContext = (LogicalCallContext)value;
								return;
							}
						}
					}
					else if (key == "__Uri")
					{
						this._uri = (string)value;
						return;
					}
				}
				else if (num != 1637783905U)
				{
					if (num == 1960967436U)
					{
						if (key == "__OutArgs")
						{
							this._args = (object[])value;
							return;
						}
					}
				}
				else if (key == "__Return")
				{
					this._returnValue = value;
					return;
				}
			}
			else if (num <= 3166241401U)
			{
				if (num != 2010141056U)
				{
					if (num == 3166241401U)
					{
						if (key == "__MethodName")
						{
							this._methodName = (string)value;
							return;
						}
					}
				}
				else if (key == "__TypeName")
				{
					this._typeName = (string)value;
					return;
				}
			}
			else if (num != 3626951189U)
			{
				if (num == 3679129400U)
				{
					if (key == "__MethodSignature")
					{
						this._methodSignature = (Type[])value;
						return;
					}
				}
			}
			else if (key == "__fault")
			{
				this._exception = (Exception)value;
				return;
			}
			this.Properties[key] = value;
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000CF676 File Offset: 0x000CD876
		public int ArgCount
		{
			get
			{
				if (this._args == null)
				{
					return 0;
				}
				return this._args.Length;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000CF68A File Offset: 0x000CD88A
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000CF692 File Offset: 0x000CD892
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x000CF69A File Offset: 0x000CD89A
		public bool HasVarArgs
		{
			get
			{
				return (this.MethodBase.CallingConvention | CallingConventions.VarArgs) > (CallingConventions)0;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000CF6AC File Offset: 0x000CD8AC
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._callContext == null)
				{
					this._callContext = new LogicalCallContext();
				}
				return this._callContext;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000CF6C8 File Offset: 0x000CD8C8
		public MethodBase MethodBase
		{
			get
			{
				if (null == this._methodBase)
				{
					if (this._callMsg != null)
					{
						this._methodBase = this._callMsg.MethodBase;
					}
					else if (this.MethodName != null && this.TypeName != null)
					{
						this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
					}
				}
				return this._methodBase;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000CF720 File Offset: 0x000CD920
		public string MethodName
		{
			get
			{
				if (this._methodName == null && this._callMsg != null)
				{
					this._methodName = this._callMsg.MethodName;
				}
				return this._methodName;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x000CF749 File Offset: 0x000CD949
		public object MethodSignature
		{
			get
			{
				if (this._methodSignature == null && this._callMsg != null)
				{
					this._methodSignature = (Type[])this._callMsg.MethodSignature;
				}
				return this._methodSignature;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000CF777 File Offset: 0x000CD977
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

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000CF7B4 File Offset: 0x000CD9B4
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

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000CF808 File Offset: 0x000CDA08
		public virtual IDictionary Properties
		{
			get
			{
				if (this.ExternalProperties == null)
				{
					MethodReturnDictionary methodReturnDictionary = new MethodReturnDictionary(this);
					this.ExternalProperties = methodReturnDictionary;
					this.InternalProperties = methodReturnDictionary.GetInternalProperties();
				}
				return this.ExternalProperties;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x000CF83D File Offset: 0x000CDA3D
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000CF845 File Offset: 0x000CDA45
		public string TypeName
		{
			get
			{
				if (this._typeName == null && this._callMsg != null)
				{
					this._typeName = this._callMsg.TypeName;
				}
				return this._typeName;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x000CF86E File Offset: 0x000CDA6E
		// (set) Token: 0x06003B2B RID: 15147 RVA: 0x000CF897 File Offset: 0x000CDA97
		public string Uri
		{
			get
			{
				if (this._uri == null && this._callMsg != null)
				{
					this._uri = this._callMsg.Uri;
				}
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x000CF8A0 File Offset: 0x000CDAA0
		// (set) Token: 0x06003B2D RID: 15149 RVA: 0x000CF8A8 File Offset: 0x000CDAA8
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

		// Token: 0x06003B2E RID: 15150 RVA: 0x000CF8B1 File Offset: 0x000CDAB1
		public object GetArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			return this._args[argNum];
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000CF8C5 File Offset: 0x000CDAC5
		public string GetArgName(int index)
		{
			return this.MethodBase.GetParameters()[index].Name;
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000CF8DC File Offset: 0x000CDADC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._exception == null)
			{
				info.AddValue("__TypeName", this._typeName);
				info.AddValue("__MethodName", this._methodName);
				info.AddValue("__MethodSignature", this._methodSignature);
				info.AddValue("__Uri", this._uri);
				info.AddValue("__Return", this._returnValue);
				info.AddValue("__OutArgs", this._args);
			}
			else
			{
				info.AddValue("__fault", this._exception);
			}
			info.AddValue("__CallContext", this._callContext);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000CF9E4 File Offset: 0x000CDBE4
		public object GetOutArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000CFA20 File Offset: 0x000CDC20
		public string GetOutArgName(int index)
		{
			if (null == this._methodBase)
			{
				return "__method_" + index.ToString();
			}
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public virtual object HeaderHandler(Header[] h)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003B35 RID: 15157 RVA: 0x000CFA73 File Offset: 0x000CDC73
		// (set) Token: 0x06003B36 RID: 15158 RVA: 0x000CFA7B File Offset: 0x000CDC7B
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

		// Token: 0x06003B37 RID: 15159 RVA: 0x000CFA84 File Offset: 0x000CDC84
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x04002630 RID: 9776
		private string _methodName;

		// Token: 0x04002631 RID: 9777
		private string _uri;

		// Token: 0x04002632 RID: 9778
		private string _typeName;

		// Token: 0x04002633 RID: 9779
		private MethodBase _methodBase;

		// Token: 0x04002634 RID: 9780
		private object _returnValue;

		// Token: 0x04002635 RID: 9781
		private Exception _exception;

		// Token: 0x04002636 RID: 9782
		private Type[] _methodSignature;

		// Token: 0x04002637 RID: 9783
		private ArgInfo _inArgInfo;

		// Token: 0x04002638 RID: 9784
		private object[] _args;

		// Token: 0x04002639 RID: 9785
		private object[] _outArgs;

		// Token: 0x0400263A RID: 9786
		private IMethodCallMessage _callMsg;

		// Token: 0x0400263B RID: 9787
		private LogicalCallContext _callContext;

		// Token: 0x0400263C RID: 9788
		private Identity _targetIdentity;

		// Token: 0x0400263D RID: 9789
		protected IDictionary ExternalProperties;

		// Token: 0x0400263E RID: 9790
		protected IDictionary InternalProperties;
	}
}
