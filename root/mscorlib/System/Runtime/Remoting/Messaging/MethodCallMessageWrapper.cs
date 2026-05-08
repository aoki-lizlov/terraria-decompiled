using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005F8 RID: 1528
	[ComVisible(true)]
	public class MethodCallMessageWrapper : InternalMessageWrapper, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06003ADC RID: 15068 RVA: 0x000CE851 File Offset: 0x000CCA51
		public MethodCallMessageWrapper(IMethodCallMessage msg)
			: base(msg)
		{
			this._args = ((IMethodCallMessage)this.WrappedMessage).Args;
			this._inArgInfo = new ArgInfo(msg.MethodBase, ArgInfoType.In);
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x000CE882 File Offset: 0x000CCA82
		public virtual int ArgCount
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).ArgCount;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000CE894 File Offset: 0x000CCA94
		// (set) Token: 0x06003ADF RID: 15071 RVA: 0x000CE89C File Offset: 0x000CCA9C
		public virtual object[] Args
		{
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x000CE8A5 File Offset: 0x000CCAA5
		public virtual bool HasVarArgs
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).HasVarArgs;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x000CE8B7 File Offset: 0x000CCAB7
		public virtual int InArgCount
		{
			get
			{
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000CE8C4 File Offset: 0x000CCAC4
		public virtual object[] InArgs
		{
			get
			{
				return this._inArgInfo.GetInOutArgs(this._args);
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000CE8D7 File Offset: 0x000CCAD7
		public virtual LogicalCallContext LogicalCallContext
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).LogicalCallContext;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x000CE8E9 File Offset: 0x000CCAE9
		public virtual MethodBase MethodBase
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodBase;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000CE8FB File Offset: 0x000CCAFB
		public virtual string MethodName
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodName;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000CE90D File Offset: 0x000CCB0D
		public virtual object MethodSignature
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).MethodSignature;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x000CE91F File Offset: 0x000CCB1F
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodCallMessageWrapper.DictionaryWrapper(this, this.WrappedMessage.Properties);
				}
				return this._properties;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000CE946 File Offset: 0x000CCB46
		public virtual string TypeName
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).TypeName;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000CE958 File Offset: 0x000CCB58
		// (set) Token: 0x06003AEA RID: 15082 RVA: 0x000CE96C File Offset: 0x000CCB6C
		public virtual string Uri
		{
			get
			{
				return ((IMethodCallMessage)this.WrappedMessage).Uri;
			}
			set
			{
				IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
				if (internalMessage != null)
				{
					internalMessage.Uri = value;
					return;
				}
				this.Properties["__Uri"] = value;
			}
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000CE9A1 File Offset: 0x000CCBA1
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000CE9AB File Offset: 0x000CCBAB
		public virtual string GetArgName(int index)
		{
			return ((IMethodCallMessage)this.WrappedMessage).GetArgName(index);
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x000CE9BE File Offset: 0x000CCBBE
		public virtual object GetInArg(int argNum)
		{
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x000CE9D3 File Offset: 0x000CCBD3
		public virtual string GetInArgName(int index)
		{
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x04002624 RID: 9764
		private object[] _args;

		// Token: 0x04002625 RID: 9765
		private ArgInfo _inArgInfo;

		// Token: 0x04002626 RID: 9766
		private MethodCallMessageWrapper.DictionaryWrapper _properties;

		// Token: 0x020005F9 RID: 1529
		private class DictionaryWrapper : MCMDictionary
		{
			// Token: 0x06003AEF RID: 15087 RVA: 0x000CE9E1 File Offset: 0x000CCBE1
			public DictionaryWrapper(IMethodMessage message, IDictionary wrappedDictionary)
				: base(message)
			{
				this._wrappedDictionary = wrappedDictionary;
				base.MethodKeys = MethodCallMessageWrapper.DictionaryWrapper._keys;
			}

			// Token: 0x06003AF0 RID: 15088 RVA: 0x000CE9FC File Offset: 0x000CCBFC
			protected override IDictionary AllocInternalProperties()
			{
				return this._wrappedDictionary;
			}

			// Token: 0x06003AF1 RID: 15089 RVA: 0x000CEA04 File Offset: 0x000CCC04
			protected override void SetMethodProperty(string key, object value)
			{
				if (key == "__Args")
				{
					((MethodCallMessageWrapper)this._message)._args = (object[])value;
					return;
				}
				base.SetMethodProperty(key, value);
			}

			// Token: 0x06003AF2 RID: 15090 RVA: 0x000CEA32 File Offset: 0x000CCC32
			protected override object GetMethodProperty(string key)
			{
				if (key == "__Args")
				{
					return ((MethodCallMessageWrapper)this._message)._args;
				}
				return base.GetMethodProperty(key);
			}

			// Token: 0x06003AF3 RID: 15091 RVA: 0x000CEA59 File Offset: 0x000CCC59
			// Note: this type is marked as 'beforefieldinit'.
			static DictionaryWrapper()
			{
			}

			// Token: 0x04002627 RID: 9767
			private IDictionary _wrappedDictionary;

			// Token: 0x04002628 RID: 9768
			private static string[] _keys = new string[] { "__Args" };
		}
	}
}
