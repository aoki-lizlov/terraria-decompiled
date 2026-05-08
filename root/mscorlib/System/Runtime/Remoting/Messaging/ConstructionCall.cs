using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E4 RID: 1508
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMessage, IMethodCallMessage, IMethodMessage
	{
		// Token: 0x06003A51 RID: 14929 RVA: 0x000CD8EF File Offset: 0x000CBAEF
		public ConstructionCall(IMessage m)
			: base(m)
		{
			this._activationTypeName = base.TypeName;
			this._isContextOk = true;
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000CD90B File Offset: 0x000CBB0B
		internal ConstructionCall(Type type)
		{
			this._activationType = type;
			this._activationTypeName = type.AssemblyQualifiedName;
			this._isContextOk = true;
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000CD92D File Offset: 0x000CBB2D
		public ConstructionCall(Header[] headers)
			: base(headers)
		{
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000CD936 File Offset: 0x000CBB36
		internal ConstructionCall(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000CD940 File Offset: 0x000CBB40
		internal override void InitDictionary()
		{
			ConstructionCallDictionary constructionCallDictionary = new ConstructionCallDictionary(this);
			this.ExternalProperties = constructionCallDictionary;
			this.InternalProperties = constructionCallDictionary.GetInternalProperties();
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x000CD967 File Offset: 0x000CBB67
		// (set) Token: 0x06003A57 RID: 14935 RVA: 0x000CD96F File Offset: 0x000CBB6F
		internal bool IsContextOk
		{
			get
			{
				return this._isContextOk;
			}
			set
			{
				this._isContextOk = value;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000CD978 File Offset: 0x000CBB78
		public Type ActivationType
		{
			get
			{
				if (this._activationType == null)
				{
					this._activationType = Type.GetType(this._activationTypeName);
				}
				return this._activationType;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000CD99F File Offset: 0x000CBB9F
		public string ActivationTypeName
		{
			get
			{
				return this._activationTypeName;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x000CD9A7 File Offset: 0x000CBBA7
		// (set) Token: 0x06003A5B RID: 14939 RVA: 0x000CD9AF File Offset: 0x000CBBAF
		public IActivator Activator
		{
			get
			{
				return this._activator;
			}
			set
			{
				this._activator = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x000CD9B8 File Offset: 0x000CBBB8
		public object[] CallSiteActivationAttributes
		{
			get
			{
				return this._activationAttributes;
			}
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000CD9C0 File Offset: 0x000CBBC0
		internal void SetActivationAttributes(object[] attributes)
		{
			this._activationAttributes = attributes;
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x000CD9C9 File Offset: 0x000CBBC9
		public IList ContextProperties
		{
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000CD9E4 File Offset: 0x000CBBE4
		internal override void InitMethodProperty(string key, object value)
		{
			if (key == "__Activator")
			{
				this._activator = (IActivator)value;
				return;
			}
			if (key == "__CallSiteActivationAttributes")
			{
				this._activationAttributes = (object[])value;
				return;
			}
			if (key == "__ActivationType")
			{
				this._activationType = (Type)value;
				return;
			}
			if (key == "__ContextProperties")
			{
				this._contextProperties = (IList)value;
				return;
			}
			if (!(key == "__ActivationTypeName"))
			{
				base.InitMethodProperty(key, value);
				return;
			}
			this._activationTypeName = (string)value;
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000CDA80 File Offset: 0x000CBC80
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IList list = this._contextProperties;
			if (list != null && list.Count == 0)
			{
				list = null;
			}
			info.AddValue("__Activator", this._activator);
			info.AddValue("__CallSiteActivationAttributes", this._activationAttributes);
			info.AddValue("__ActivationType", null);
			info.AddValue("__ContextProperties", list);
			info.AddValue("__ActivationTypeName", this._activationTypeName);
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x000CDAF4 File Offset: 0x000CBCF4
		public override IDictionary Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000CDAFC File Offset: 0x000CBCFC
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000CDB04 File Offset: 0x000CBD04
		internal RemotingProxy SourceProxy
		{
			get
			{
				return this._sourceProxy;
			}
			set
			{
				this._sourceProxy = value;
			}
		}

		// Token: 0x04002608 RID: 9736
		private IActivator _activator;

		// Token: 0x04002609 RID: 9737
		private object[] _activationAttributes;

		// Token: 0x0400260A RID: 9738
		private IList _contextProperties;

		// Token: 0x0400260B RID: 9739
		private Type _activationType;

		// Token: 0x0400260C RID: 9740
		private string _activationTypeName;

		// Token: 0x0400260D RID: 9741
		private bool _isContextOk;

		// Token: 0x0400260E RID: 9742
		[NonSerialized]
		private RemotingProxy _sourceProxy;
	}
}
