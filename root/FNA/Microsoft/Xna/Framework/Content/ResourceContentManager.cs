using System;
using System.IO;
using System.Resources;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200014A RID: 330
	public class ResourceContentManager : ContentManager
	{
		// Token: 0x060017C6 RID: 6086 RVA: 0x0003BE18 File Offset: 0x0003A018
		public ResourceContentManager(IServiceProvider servicesProvider, ResourceManager resource)
			: base(servicesProvider)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			this.resource = resource;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0003BE38 File Offset: 0x0003A038
		protected override Stream OpenStream(string assetName)
		{
			object @object = this.resource.GetObject(assetName);
			if (@object == null)
			{
				throw new ContentLoadException("Resource not found");
			}
			byte[] array = @object as byte[];
			if (array == null)
			{
				throw new ContentLoadException("Resource is not in binary format");
			}
			return new MemoryStream(array, 0, array.Length, true, true);
		}

		// Token: 0x04000AE4 RID: 2788
		private ResourceManager resource;
	}
}
