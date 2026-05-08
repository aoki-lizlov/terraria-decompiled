using System;
using System.Collections;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005A4 RID: 1444
	internal class RemoteActivationAttribute : Attribute, IContextAttribute
	{
		// Token: 0x06003879 RID: 14457 RVA: 0x00002050 File Offset: 0x00000250
		public RemoteActivationAttribute()
		{
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000CA820 File Offset: 0x000C8A20
		public RemoteActivationAttribute(IList contextProperties)
		{
			this._contextProperties = contextProperties;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsContextOK(Context ctx, IConstructionCallMessage ctor)
		{
			return false;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000CA830 File Offset: 0x000C8A30
		public void GetPropertiesForNewContext(IConstructionCallMessage ctor)
		{
			if (this._contextProperties != null)
			{
				foreach (object obj in this._contextProperties)
				{
					ctor.ContextProperties.Add(obj);
				}
			}
		}

		// Token: 0x04002580 RID: 9600
		private IList _contextProperties;
	}
}
