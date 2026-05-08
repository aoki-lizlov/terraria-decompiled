using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E1 RID: 1505
	internal class CADMethodReturnMessage : CADMessageBase
	{
		// Token: 0x06003A42 RID: 14914 RVA: 0x000CD648 File Offset: 0x000CB848
		internal static CADMethodReturnMessage Create(IMessage callMsg)
		{
			IMethodReturnMessage methodReturnMessage = callMsg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new CADMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000CD668 File Offset: 0x000CB868
		internal CADMethodReturnMessage(IMethodReturnMessage retMsg)
			: base(retMsg)
		{
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(retMsg.Properties, ref arrayList);
			this._returnValue = base.MarshalArgument(retMsg.ReturnValue, ref arrayList);
			this._args = base.MarshalArguments(retMsg.Args, ref arrayList);
			this._sig = CADMessageBase.GetSignature(base.GetMethod(), true);
			if (retMsg.Exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new CADArgHolder(arrayList.Count);
				arrayList.Add(retMsg.Exception);
			}
			base.SaveLogicalCallContext(retMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000CD720 File Offset: 0x000CB920
		internal ArrayList GetArguments()
		{
			ArrayList arrayList = null;
			if (this._serializedArgs != null)
			{
				byte[] array = new byte[this._serializedArgs.Length];
				Array.Copy(this._serializedArgs, array, this._serializedArgs.Length);
				arrayList = new ArrayList((object[])CADSerializer.DeserializeObject(new MemoryStream(array)));
				this._serializedArgs = null;
			}
			return arrayList;
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000CD62F File Offset: 0x000CB82F
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x000CD777 File Offset: 0x000CB977
		internal object GetReturnValue(ArrayList args)
		{
			return base.UnmarshalArgument(this._returnValue, args);
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x000CD786 File Offset: 0x000CB986
		internal Exception GetException(ArrayList args)
		{
			if (this._exception == null)
			{
				return null;
			}
			return (Exception)args[this._exception.index];
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06003A48 RID: 14920 RVA: 0x000CD63E File Offset: 0x000CB83E
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x04002602 RID: 9730
		private object _returnValue;

		// Token: 0x04002603 RID: 9731
		private CADArgHolder _exception;

		// Token: 0x04002604 RID: 9732
		private Type[] _sig;
	}
}
