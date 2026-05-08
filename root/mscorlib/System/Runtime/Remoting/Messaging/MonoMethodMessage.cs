using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000600 RID: 1536
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoMethodMessage : IMethodCallMessage, IMethodMessage, IMessage, IMethodReturnMessage, IInternalMessage
	{
		// Token: 0x06003B56 RID: 15190 RVA: 0x000CFDF0 File Offset: 0x000CDFF0
		internal void InitMessage(RuntimeMethodInfo method, object[] out_args)
		{
			this.method = method;
			ParameterInfo[] parametersInternal = method.GetParametersInternal();
			int num = parametersInternal.Length;
			this.args = new object[num];
			this.arg_types = new byte[num];
			this.asyncResult = null;
			this.call_type = CallType.Sync;
			this.names = new string[num];
			for (int i = 0; i < num; i++)
			{
				this.names[i] = parametersInternal[i].Name;
			}
			bool flag = out_args != null;
			int num2 = 0;
			for (int j = 0; j < num; j++)
			{
				bool isOut = parametersInternal[j].IsOut;
				byte b;
				if (parametersInternal[j].ParameterType.IsByRef)
				{
					if (flag)
					{
						this.args[j] = out_args[num2++];
					}
					b = 2;
					if (!isOut)
					{
						b |= 1;
					}
				}
				else
				{
					b = 1;
					if (isOut)
					{
						b |= 4;
					}
				}
				this.arg_types[j] = b;
			}
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000CFED1 File Offset: 0x000CE0D1
		public MonoMethodMessage(MethodBase method, object[] out_args)
		{
			if (method != null)
			{
				this.InitMessage((RuntimeMethodInfo)method, out_args);
				return;
			}
			this.args = null;
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000CFEF8 File Offset: 0x000CE0F8
		internal MonoMethodMessage(MethodInfo minfo, object[] in_args, object[] out_args)
		{
			this.InitMessage((RuntimeMethodInfo)minfo, out_args);
			int num = in_args.Length;
			for (int i = 0; i < num; i++)
			{
				this.args[i] = in_args[i];
			}
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000CFF33 File Offset: 0x000CE133
		private static MethodInfo GetMethodInfo(Type type, string methodName)
		{
			MethodInfo methodInfo = type.GetMethod(methodName);
			if (methodInfo == null)
			{
				throw new ArgumentException(string.Format("Could not find '{0}' in {1}", methodName, type), "methodName");
			}
			return methodInfo;
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000CFF5C File Offset: 0x000CE15C
		public MonoMethodMessage(Type type, string methodName, object[] in_args)
			: this(MonoMethodMessage.GetMethodInfo(type, methodName), in_args, null)
		{
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000CFF6D File Offset: 0x000CE16D
		public IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new MCMDictionary(this);
				}
				return this.properties;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003B5C RID: 15196 RVA: 0x000CFF89 File Offset: 0x000CE189
		public int ArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				return this.args.Length;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000CFFA8 File Offset: 0x000CE1A8
		public object[] Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06003B5E RID: 15198 RVA: 0x0000408A File Offset: 0x0000228A
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x000CFFB0 File Offset: 0x000CE1B0
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x000CFFB8 File Offset: 0x000CE1B8
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this.ctx;
			}
			set
			{
				this.ctx = value;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x000CFFC1 File Offset: 0x000CE1C1
		public MethodBase MethodBase
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000CFFC9 File Offset: 0x000CE1C9
		public string MethodName
		{
			get
			{
				if (null == this.method)
				{
					return string.Empty;
				}
				return this.method.Name;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x000CFFEC File Offset: 0x000CE1EC
		public object MethodSignature
		{
			get
			{
				if (this.methodSignature == null)
				{
					ParameterInfo[] parameters = this.method.GetParameters();
					this.methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this.methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this.methodSignature;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003B64 RID: 15204 RVA: 0x000D003F File Offset: 0x000CE23F
		public string TypeName
		{
			get
			{
				if (null == this.method)
				{
					return string.Empty;
				}
				return this.method.DeclaringType.AssemblyQualifiedName;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x000D0065 File Offset: 0x000CE265
		// (set) Token: 0x06003B66 RID: 15206 RVA: 0x000D006D File Offset: 0x000CE26D
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000D0076 File Offset: 0x000CE276
		public object GetArg(int arg_num)
		{
			if (this.args == null)
			{
				return null;
			}
			return this.args[arg_num];
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000D008A File Offset: 0x000CE28A
		public string GetArgName(int arg_num)
		{
			if (this.args == null)
			{
				return string.Empty;
			}
			return this.names[arg_num];
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x000D00A4 File Offset: 0x000CE2A4
		public int InArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				byte[] array = this.arg_types;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] & 1) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000D00EC File Offset: 0x000CE2EC
		public object[] InArgs
		{
			get
			{
				object[] array = new object[this.InArgCount];
				int num2;
				int num = (num2 = 0);
				byte[] array2 = this.arg_types;
				for (int i = 0; i < array2.Length; i++)
				{
					if ((array2[i] & 1) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000D0140 File Offset: 0x000CE340
		public object GetInArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 1) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000D0184 File Offset: 0x000CE384
		public string GetInArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 1) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x000D01C7 File Offset: 0x000CE3C7
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06003B6E RID: 15214 RVA: 0x000D01D0 File Offset: 0x000CE3D0
		public int OutArgCount
		{
			get
			{
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				byte[] array = this.arg_types;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] & 2) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x000D020C File Offset: 0x000CE40C
		public object[] OutArgs
		{
			get
			{
				if (this.args == null)
				{
					return null;
				}
				object[] array = new object[this.OutArgCount];
				int num2;
				int num = (num2 = 0);
				byte[] array2 = this.arg_types;
				for (int i = 0; i < array2.Length; i++)
				{
					if ((array2[i] & 2) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06003B70 RID: 15216 RVA: 0x000D0268 File Offset: 0x000CE468
		public object ReturnValue
		{
			get
			{
				return this.rval;
			}
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000D0270 File Offset: 0x000CE470
		public object GetOutArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 2) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000D02B4 File Offset: 0x000CE4B4
		public string GetOutArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 2) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000D02F7 File Offset: 0x000CE4F7
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000D02FF File Offset: 0x000CE4FF
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000D0308 File Offset: 0x000CE508
		bool IInternalMessage.HasProperties()
		{
			return this.properties != null;
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06003B76 RID: 15222 RVA: 0x000D0313 File Offset: 0x000CE513
		public bool IsAsync
		{
			get
			{
				return this.asyncResult != null;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x000D031E File Offset: 0x000CE51E
		public AsyncResult AsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000D0326 File Offset: 0x000CE526
		internal CallType CallType
		{
			get
			{
				if (this.call_type == CallType.Sync && RemotingServices.IsOneWay(this.method))
				{
					this.call_type = CallType.OneWay;
				}
				return this.call_type;
			}
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x000D034C File Offset: 0x000CE54C
		public bool NeedsOutProcessing(out int outCount)
		{
			bool flag = false;
			outCount = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0)
				{
					outCount++;
				}
				else if ((b & 4) != 0)
				{
					flag = true;
				}
			}
			return outCount > 0 || flag;
		}

		// Token: 0x04002648 RID: 9800
		private RuntimeMethodInfo method;

		// Token: 0x04002649 RID: 9801
		private object[] args;

		// Token: 0x0400264A RID: 9802
		private string[] names;

		// Token: 0x0400264B RID: 9803
		private byte[] arg_types;

		// Token: 0x0400264C RID: 9804
		public LogicalCallContext ctx;

		// Token: 0x0400264D RID: 9805
		public object rval;

		// Token: 0x0400264E RID: 9806
		public Exception exc;

		// Token: 0x0400264F RID: 9807
		private AsyncResult asyncResult;

		// Token: 0x04002650 RID: 9808
		private CallType call_type;

		// Token: 0x04002651 RID: 9809
		private string uri;

		// Token: 0x04002652 RID: 9810
		private MCMDictionary properties;

		// Token: 0x04002653 RID: 9811
		private Identity identity;

		// Token: 0x04002654 RID: 9812
		private Type[] methodSignature;
	}
}
