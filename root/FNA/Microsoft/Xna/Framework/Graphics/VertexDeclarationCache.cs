using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000D8 RID: 216
	internal static class VertexDeclarationCache<T> where T : struct, IVertexType
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0003468C File Offset: 0x0003288C
		public static VertexDeclaration VertexDeclaration
		{
			get
			{
				if (VertexDeclarationCache<T>.cached == null)
				{
					VertexDeclarationCache<T>.cached = VertexDeclaration.FromType(typeof(T));
				}
				return VertexDeclarationCache<T>.cached;
			}
		}

		// Token: 0x04000A59 RID: 2649
		private static VertexDeclaration cached;
	}
}
