using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000121 RID: 289
	internal class MatrixReader : ContentTypeReader<Matrix>
	{
		// Token: 0x06001750 RID: 5968 RVA: 0x00039618 File Offset: 0x00037818
		protected internal override Matrix Read(ContentReader input, Matrix existingInstance)
		{
			return new Matrix(input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle(), input.ReadSingle());
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0003968A File Offset: 0x0003788A
		public MatrixReader()
		{
		}
	}
}
