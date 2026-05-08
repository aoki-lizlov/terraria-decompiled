using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000101 RID: 257
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x000307DA File Offset: 0x0002E9DA
		public byte[] Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000307E2 File Offset: 0x0002E9E2
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			this.Value = value;
		}

		// Token: 0x04000408 RID: 1032
		[CompilerGenerated]
		private readonly byte[] <Value>k__BackingField;
	}
}
