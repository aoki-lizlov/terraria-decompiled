using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000115 RID: 277
	internal class DoubleReader : ContentTypeReader<double>
	{
		// Token: 0x06001735 RID: 5941 RVA: 0x00039020 File Offset: 0x00037220
		internal DoubleReader()
		{
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00039028 File Offset: 0x00037228
		protected internal override double Read(ContentReader input, double existingInstance)
		{
			return input.ReadDouble();
		}
	}
}
