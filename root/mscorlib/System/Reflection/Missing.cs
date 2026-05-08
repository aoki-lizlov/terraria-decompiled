using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000880 RID: 2176
	public sealed class Missing : ISerializable
	{
		// Token: 0x060048E6 RID: 18662 RVA: 0x000025BE File Offset: 0x000007BE
		private Missing()
		{
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x0003CB93 File Offset: 0x0003AD93
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x000EE8DA File Offset: 0x000ECADA
		// Note: this type is marked as 'beforefieldinit'.
		static Missing()
		{
		}

		// Token: 0x04002E6E RID: 11886
		public static readonly Missing Value = new Missing();
	}
}
