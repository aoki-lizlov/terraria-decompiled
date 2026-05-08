using System;
using System.Reflection;

namespace Internal.Runtime.Augments
{
	// Token: 0x02000B61 RID: 2913
	internal class ReflectionExecutionDomainCallbacks
	{
		// Token: 0x06006AAE RID: 27310 RVA: 0x0016ECDC File Offset: 0x0016CEDC
		internal Exception CreateMissingMetadataException(Type attributeType)
		{
			return new MissingMetadataException();
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x000025BE File Offset: 0x000007BE
		public ReflectionExecutionDomainCallbacks()
		{
		}
	}
}
