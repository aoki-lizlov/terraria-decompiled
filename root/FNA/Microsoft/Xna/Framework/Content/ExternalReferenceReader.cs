using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200011B RID: 283
	internal class ExternalReferenceReader : ContentTypeReader
	{
		// Token: 0x06001742 RID: 5954 RVA: 0x000394BA File Offset: 0x000376BA
		public ExternalReferenceReader()
			: base(null)
		{
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000394C3 File Offset: 0x000376C3
		protected internal override object Read(ContentReader input, object existingInstance)
		{
			return input.ReadExternalReference<object>();
		}
	}
}
