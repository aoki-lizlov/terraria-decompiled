using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005F6 RID: 1526
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class MethodCall : IMethodCallMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage, ISerializationRootObject
	{
		// Token: 0x06003AB2 RID: 15026 RVA: 0x000CDD98 File Offset: 0x000CBF98
		public MethodCall(Header[] h1)
		{
			this.Init();
			if (h1 == null || h1.Length == 0)
			{
				return;
			}
			foreach (Header header in h1)
			{
				this.InitMethodProperty(header.Name, header.Value);
			}
			this.ResolveMethod();
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000CDDE8 File Offset: 0x000CBFE8
		internal MethodCall(SerializationInfo info, StreamingContext context)
		{
			this.Init();
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000CDE30 File Offset: 0x000CC030
		internal MethodCall(CADMethodCallMessage msg)
		{
			this._uri = string.Copy(msg.Uri);
			ArrayList arguments = msg.GetArguments();
			this._args = msg.GetArgs(arguments);
			this._callContext = msg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			this._methodBase = msg.GetMethod();
			this.Init();
			if (msg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, msg.PropertiesCount, arguments);
			}
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000CDEB8 File Offset: 0x000CC0B8
		public MethodCall(IMessage msg)
		{
			if (msg is IMethodMessage)
			{
				this.CopyFrom((IMethodMessage)msg);
				return;
			}
			foreach (object obj in msg.Properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.InitMethodProperty((string)dictionaryEntry.Key, dictionaryEntry.Value);
			}
			this.Init();
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000CDF44 File Offset: 0x000CC144
		internal MethodCall(string uri, string typeName, string methodName, object[] args)
		{
			this._uri = uri;
			this._typeName = typeName;
			this._methodName = methodName;
			this._args = args;
			this.Init();
			this.ResolveMethod();
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000CDF78 File Offset: 0x000CC178
		internal MethodCall(object handlerObject, BinaryMethodCallMessage smuggledMsg)
		{
			if (handlerObject != null)
			{
				this._uri = handlerObject as string;
				if (this._uri == null && handlerObject is MarshalByRefObject)
				{
					throw new NotImplementedException("MarshalByRefObject.GetIdentity");
				}
			}
			this._typeName = smuggledMsg.TypeName;
			this._methodName = smuggledMsg.MethodName;
			this._methodSignature = (Type[])smuggledMsg.MethodSignature;
			this._args = smuggledMsg.Args;
			this._genericArguments = smuggledMsg.InstantiationArgs;
			this._callContext = smuggledMsg.LogicalCallContext;
			this.ResolveMethod();
			if (smuggledMsg.HasProperties)
			{
				smuggledMsg.PopulateMessageProperties(this.Properties);
			}
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000025BE File Offset: 0x000007BE
		internal MethodCall()
		{
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000CE01C File Offset: 0x000CC21C
		internal void CopyFrom(IMethodMessage call)
		{
			this._uri = call.Uri;
			this._typeName = call.TypeName;
			this._methodName = call.MethodName;
			this._args = call.Args;
			this._methodSignature = (Type[])call.MethodSignature;
			this._methodBase = call.MethodBase;
			this._callContext = call.LogicalCallContext;
			this.Init();
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000CE088 File Offset: 0x000CC288
		internal virtual void InitMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1619225942U)
			{
				if (num != 990701179U)
				{
					if (num != 1201911322U)
					{
						if (num == 1619225942U)
						{
							if (key == "__Args")
							{
								this._args = (object[])value;
								return;
							}
						}
					}
					else if (key == "__CallContext")
					{
						this._callContext = (LogicalCallContext)value;
						return;
					}
				}
				else if (key == "__Uri")
				{
					this._uri = (string)value;
					return;
				}
			}
			else if (num <= 2850677384U)
			{
				if (num != 2010141056U)
				{
					if (num == 2850677384U)
					{
						if (key == "__GenericArguments")
						{
							this._genericArguments = (Type[])value;
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
			else if (num != 3166241401U)
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
			else if (key == "__MethodName")
			{
				this._methodName = (string)value;
				return;
			}
			this.Properties[key] = value;
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000CE1DC File Offset: 0x000CC3DC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("__TypeName", this._typeName);
			info.AddValue("__MethodName", this._methodName);
			info.AddValue("__MethodSignature", this._methodSignature);
			info.AddValue("__Args", this._args);
			info.AddValue("__CallContext", this._callContext);
			info.AddValue("__Uri", this._uri);
			info.AddValue("__GenericArguments", this._genericArguments);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000CE2C8 File Offset: 0x000CC4C8
		public int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000CE2D2 File Offset: 0x000CC4D2
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000CE2DA File Offset: 0x000CC4DA
		public bool HasVarArgs
		{
			get
			{
				return (this.MethodBase.CallingConvention | CallingConventions.VarArgs) > (CallingConventions)0;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000CE2EC File Offset: 0x000CC4EC
		public int InArgCount
		{
			get
			{
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x000CE313 File Offset: 0x000CC513
		public object[] InArgs
		{
			get
			{
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
				}
				return this._inArgInfo.GetInOutArgs(this._args);
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000CE340 File Offset: 0x000CC540
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

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000CE35B File Offset: 0x000CC55B
		public MethodBase MethodBase
		{
			get
			{
				if (this._methodBase == null)
				{
					this.ResolveMethod();
				}
				return this._methodBase;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000CE377 File Offset: 0x000CC577
		public string MethodName
		{
			get
			{
				if (this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000CE398 File Offset: 0x000CC598
		public object MethodSignature
		{
			get
			{
				if (this._methodSignature == null && this._methodBase != null)
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

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x000CE3F9 File Offset: 0x000CC5F9
		public virtual IDictionary Properties
		{
			get
			{
				if (this.ExternalProperties == null)
				{
					this.InitDictionary();
				}
				return this.ExternalProperties;
			}
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000CE410 File Offset: 0x000CC610
		internal virtual void InitDictionary()
		{
			MCMDictionary mcmdictionary = new MCMDictionary(this);
			this.ExternalProperties = mcmdictionary;
			this.InternalProperties = mcmdictionary.GetInternalProperties();
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x000CE437 File Offset: 0x000CC637
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000CE45D File Offset: 0x000CC65D
		// (set) Token: 0x06003AC9 RID: 15049 RVA: 0x000CE465 File Offset: 0x000CC665
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

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000CE46E File Offset: 0x000CC66E
		// (set) Token: 0x06003ACB RID: 15051 RVA: 0x000CE476 File Offset: 0x000CC676
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

		// Token: 0x06003ACC RID: 15052 RVA: 0x000CE47F File Offset: 0x000CC67F
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000CE489 File Offset: 0x000CC689
		public string GetArgName(int index)
		{
			return this._methodBase.GetParameters()[index].Name;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000CE49D File Offset: 0x000CC69D
		public object GetInArg(int argNum)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000CE4CC File Offset: 0x000CC6CC
		public string GetInArgName(int index)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this._methodBase, ArgInfoType.In);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public virtual object HeaderHandler(Header[] h)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void Init()
		{
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000CE4F4 File Offset: 0x000CC6F4
		public void ResolveMethod()
		{
			if (this._uri != null)
			{
				Type serverTypeForUri = RemotingServices.GetServerTypeForUri(this._uri);
				if (serverTypeForUri == null)
				{
					string text = ((this._typeName != null) ? (" (" + this._typeName + ")") : "");
					throw new RemotingException("Requested service not found" + text + ". No receiver for uri " + this._uri);
				}
				Type type = this.CastTo(this._typeName, serverTypeForUri);
				if (type == null)
				{
					throw new RemotingException(string.Concat(new string[] { "Cannot cast from client type '", this._typeName, "' to server type '", serverTypeForUri.FullName, "'" }));
				}
				this._methodBase = RemotingServices.GetMethodBaseFromName(type, this._methodName, this._methodSignature);
				if (this._methodBase == null)
				{
					string text2 = "Method ";
					string methodName = this._methodName;
					string text3 = " not found in ";
					Type type2 = type;
					throw new RemotingException(text2 + methodName + text3 + ((type2 != null) ? type2.ToString() : null));
				}
				if (type != serverTypeForUri && type.IsInterface && !serverTypeForUri.IsInterface)
				{
					this._methodBase = RemotingServices.GetVirtualMethod(serverTypeForUri, this._methodBase);
					if (this._methodBase == null)
					{
						string text4 = "Method ";
						string methodName2 = this._methodName;
						string text5 = " not found in ";
						Type type3 = serverTypeForUri;
						throw new RemotingException(text4 + methodName2 + text5 + ((type3 != null) ? type3.ToString() : null));
					}
				}
			}
			else
			{
				this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
				if (this._methodBase == null)
				{
					throw new RemotingException("Method " + this._methodName + " not found in " + this.TypeName);
				}
			}
			if (this._methodBase.IsGenericMethod && this._methodBase.ContainsGenericParameters)
			{
				if (this.GenericArguments == null)
				{
					throw new RemotingException("The remoting infrastructure does not support open generic methods.");
				}
				this._methodBase = ((MethodInfo)this._methodBase).MakeGenericMethod(this.GenericArguments);
			}
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000CE6F0 File Offset: 0x000CC8F0
		private Type CastTo(string clientType, Type serverType)
		{
			clientType = MethodCall.GetTypeNameFromAssemblyQualifiedName(clientType);
			if (clientType == serverType.FullName)
			{
				return serverType;
			}
			Type type = serverType.BaseType;
			while (type != null)
			{
				if (clientType == type.FullName)
				{
					return type;
				}
				type = type.BaseType;
			}
			foreach (Type type2 in serverType.GetInterfaces())
			{
				if (clientType == type2.FullName)
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000CE768 File Offset: 0x000CC968
		private static string GetTypeNameFromAssemblyQualifiedName(string aqname)
		{
			int num = aqname.IndexOf("]]");
			int num2 = aqname.IndexOf(',', (num == -1) ? 0 : (num + 2));
			if (num2 != -1)
			{
				aqname = aqname.Substring(0, num2).Trim();
			}
			return aqname;
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000CE7A8 File Offset: 0x000CC9A8
		// (set) Token: 0x06003AD7 RID: 15063 RVA: 0x000CE7B0 File Offset: 0x000CC9B0
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

		// Token: 0x06003AD8 RID: 15064 RVA: 0x000CE7B9 File Offset: 0x000CC9B9
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x000CE7D0 File Offset: 0x000CC9D0
		private Type[] GenericArguments
		{
			get
			{
				if (this._genericArguments != null)
				{
					return this._genericArguments;
				}
				return this._genericArguments = this.MethodBase.GetGenericArguments();
			}
		}

		// Token: 0x04002617 RID: 9751
		private string _uri;

		// Token: 0x04002618 RID: 9752
		private string _typeName;

		// Token: 0x04002619 RID: 9753
		private string _methodName;

		// Token: 0x0400261A RID: 9754
		private object[] _args;

		// Token: 0x0400261B RID: 9755
		private Type[] _methodSignature;

		// Token: 0x0400261C RID: 9756
		private MethodBase _methodBase;

		// Token: 0x0400261D RID: 9757
		private LogicalCallContext _callContext;

		// Token: 0x0400261E RID: 9758
		private ArgInfo _inArgInfo;

		// Token: 0x0400261F RID: 9759
		private Identity _targetIdentity;

		// Token: 0x04002620 RID: 9760
		private Type[] _genericArguments;

		// Token: 0x04002621 RID: 9761
		protected IDictionary ExternalProperties;

		// Token: 0x04002622 RID: 9762
		protected IDictionary InternalProperties;
	}
}
