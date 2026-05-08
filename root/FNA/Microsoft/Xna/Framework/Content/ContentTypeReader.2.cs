using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000145 RID: 325
	public abstract class ContentTypeReader<T> : ContentTypeReader
	{
		// Token: 0x060017B6 RID: 6070 RVA: 0x0003AAC9 File Offset: 0x00038CC9
		protected ContentTypeReader()
			: base(typeof(T))
		{
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0003AADC File Offset: 0x00038CDC
		protected internal override object Read(ContentReader input, object existingInstance)
		{
			if (existingInstance == null)
			{
				return this.Read(input, default(T));
			}
			return this.Read(input, (T)((object)existingInstance));
		}

		// Token: 0x060017B8 RID: 6072
		protected internal abstract T Read(ContentReader input, T existingInstance);
	}
}
