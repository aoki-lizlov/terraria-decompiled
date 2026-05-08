using System;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x0200082A RID: 2090
	[Serializable]
	public class MissingManifestResourceException : SystemException
	{
		// Token: 0x060046C9 RID: 18121 RVA: 0x000E7A7F File Offset: 0x000E5C7F
		public MissingManifestResourceException()
			: base("Unable to find manifest resource.")
		{
			base.HResult = -2146233038;
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x000E7A97 File Offset: 0x000E5C97
		public MissingManifestResourceException(string message)
			: base(message)
		{
			base.HResult = -2146233038;
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x000E7AAB File Offset: 0x000E5CAB
		public MissingManifestResourceException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233038;
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x000183F5 File Offset: 0x000165F5
		protected MissingManifestResourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
