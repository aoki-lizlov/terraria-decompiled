using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E0 RID: 1504
	internal class CADMethodCallMessage : CADMessageBase
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06003A3C RID: 14908 RVA: 0x000CD53F File Offset: 0x000CB73F
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000CD548 File Offset: 0x000CB748
		internal static CADMethodCallMessage Create(IMessage callMsg)
		{
			IMethodCallMessage methodCallMessage = callMsg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new CADMethodCallMessage(methodCallMessage);
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000CD568 File Offset: 0x000CB768
		internal CADMethodCallMessage(IMethodCallMessage callMsg)
			: base(callMsg)
		{
			this._uri = callMsg.Uri;
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(callMsg.Properties, ref arrayList);
			this._args = base.MarshalArguments(callMsg.Args, ref arrayList);
			base.SaveLogicalCallContext(callMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000CD5D8 File Offset: 0x000CB7D8
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

		// Token: 0x06003A40 RID: 14912 RVA: 0x000CD62F File Offset: 0x000CB82F
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06003A41 RID: 14913 RVA: 0x000CD63E File Offset: 0x000CB83E
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x04002601 RID: 9729
		private string _uri;
	}
}
