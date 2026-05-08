using System;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E5 RID: 1509
	internal class ConstructionCallDictionary : MessageDictionary
	{
		// Token: 0x06003A64 RID: 14948 RVA: 0x000CDB0D File Offset: 0x000CBD0D
		public ConstructionCallDictionary(IConstructionCallMessage message)
			: base(message)
		{
			base.MethodKeys = ConstructionCallDictionary.InternalKeys;
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000CDB24 File Offset: 0x000CBD24
		protected override object GetMethodProperty(string key)
		{
			if (key == "__Activator")
			{
				return ((IConstructionCallMessage)this._message).Activator;
			}
			if (key == "__CallSiteActivationAttributes")
			{
				return ((IConstructionCallMessage)this._message).CallSiteActivationAttributes;
			}
			if (key == "__ActivationType")
			{
				return ((IConstructionCallMessage)this._message).ActivationType;
			}
			if (key == "__ContextProperties")
			{
				return ((IConstructionCallMessage)this._message).ContextProperties;
			}
			if (!(key == "__ActivationTypeName"))
			{
				return base.GetMethodProperty(key);
			}
			return ((IConstructionCallMessage)this._message).ActivationTypeName;
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000CDBD0 File Offset: 0x000CBDD0
		protected override void SetMethodProperty(string key, object value)
		{
			if (key == "__Activator")
			{
				((IConstructionCallMessage)this._message).Activator = (IActivator)value;
				return;
			}
			if (!(key == "__CallSiteActivationAttributes") && !(key == "__ActivationType") && !(key == "__ContextProperties") && !(key == "__ActivationTypeName"))
			{
				base.SetMethodProperty(key, value);
				return;
			}
			throw new ArgumentException("key was invalid");
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000CDC4C File Offset: 0x000CBE4C
		// Note: this type is marked as 'beforefieldinit'.
		static ConstructionCallDictionary()
		{
		}

		// Token: 0x0400260F RID: 9743
		public static string[] InternalKeys = new string[]
		{
			"__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__Args", "__CallContext", "__CallSiteActivationAttributes", "__ActivationType", "__ContextProperties", "__Activator",
			"__ActivationTypeName"
		};
	}
}
