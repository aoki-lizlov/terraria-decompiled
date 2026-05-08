using System;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200059F RID: 1439
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x0001A197 File Offset: 0x00018397
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Construction;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		// (set) Token: 0x06003867 RID: 14439 RVA: 0x00004088 File Offset: 0x00002288
		public IActivator NextActivator
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000CA768 File Offset: 0x000C8968
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			return (IConstructionReturnMessage)Thread.CurrentContext.GetServerContextSinkChain().SyncProcessMessage(msg);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000025BE File Offset: 0x000007BE
		public ConstructionLevelActivator()
		{
		}
	}
}
