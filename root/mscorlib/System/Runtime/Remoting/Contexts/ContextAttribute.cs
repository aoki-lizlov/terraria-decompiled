using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000560 RID: 1376
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[Serializable]
	public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
	{
		// Token: 0x06003750 RID: 14160 RVA: 0x000C8440 File Offset: 0x000C6640
		public ContextAttribute(string name)
		{
			this.AttributeName = name;
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x000C844F File Offset: 0x000C664F
		public virtual string Name
		{
			get
			{
				return this.AttributeName;
			}
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000C8457 File Offset: 0x000C6657
		public override bool Equals(object o)
		{
			return o != null && o is ContextAttribute && !(((ContextAttribute)o).AttributeName != this.AttributeName);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void Freeze(Context newContext)
		{
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000C8483 File Offset: 0x000C6683
		public override int GetHashCode()
		{
			if (this.AttributeName == null)
			{
				return 0;
			}
			return this.AttributeName.GetHashCode();
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000C849A File Offset: 0x000C669A
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.ContextProperties.Add(this);
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000C84B8 File Offset: 0x000C66B8
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (!ctorMsg.ActivationType.IsContextful)
			{
				return true;
			}
			IContextProperty property = ctx.GetProperty(this.AttributeName);
			return property != null && this == property;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		// Token: 0x04002536 RID: 9526
		protected string AttributeName;
	}
}
