using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000D7 RID: 215
	public class VertexDeclaration : GraphicsResource
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x000344A9 File Offset: 0x000326A9
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x000344B1 File Offset: 0x000326B1
		public int VertexStride
		{
			[CompilerGenerated]
			get
			{
				return this.<VertexStride>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VertexStride>k__BackingField = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		protected internal override bool IsHarmlessToLeakInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x000344BA File Offset: 0x000326BA
		public VertexDeclaration(params VertexElement[] elements)
			: this(VertexDeclaration.GetVertexStride(elements), elements)
		{
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000344CC File Offset: 0x000326CC
		public VertexDeclaration(int vertexStride, params VertexElement[] elements)
		{
			if (elements == null || elements.Length == 0)
			{
				throw new ArgumentNullException("elements", "Elements cannot be empty");
			}
			this.elements = (VertexElement[])elements.Clone();
			this.handle = GCHandle.Alloc(this.elements, GCHandleType.Pinned);
			this.elementsPin = this.handle.AddrOfPinnedObject();
			this.VertexStride = vertexStride;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00034534 File Offset: 0x00032734
		~VertexDeclaration()
		{
			this.handle.Free();
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00034568 File Offset: 0x00032768
		public VertexElement[] GetVertexElements()
		{
			return (VertexElement[])this.elements.Clone();
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0003457C File Offset: 0x0003277C
		internal static VertexDeclaration FromType(Type vertexType)
		{
			if (vertexType == null)
			{
				throw new ArgumentNullException("vertexType", "Cannot be null");
			}
			if (!vertexType.IsValueType)
			{
				throw new ArgumentException("vertexType", "Must be value type");
			}
			IVertexType vertexType2 = Activator.CreateInstance(vertexType) as IVertexType;
			if (vertexType2 == null)
			{
				throw new ArgumentException("vertexData does not inherit IVertexType");
			}
			VertexDeclaration vertexDeclaration = vertexType2.VertexDeclaration;
			if (vertexDeclaration == null)
			{
				throw new ArgumentException("vertexType's VertexDeclaration cannot be null");
			}
			return vertexDeclaration;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000345E8 File Offset: 0x000327E8
		private static int GetVertexStride(VertexElement[] elements)
		{
			int num = 0;
			for (int i = 0; i < elements.Length; i++)
			{
				int num2 = elements[i].Offset + VertexDeclaration.GetTypeSize(elements[i].VertexElementFormat);
				if (num < num2)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0003462C File Offset: 0x0003282C
		private static int GetTypeSize(VertexElementFormat elementFormat)
		{
			switch (elementFormat)
			{
			case VertexElementFormat.Single:
				return 4;
			case VertexElementFormat.Vector2:
				return 8;
			case VertexElementFormat.Vector3:
				return 12;
			case VertexElementFormat.Vector4:
				return 16;
			case VertexElementFormat.Color:
				return 4;
			case VertexElementFormat.Byte4:
				return 4;
			case VertexElementFormat.Short2:
				return 4;
			case VertexElementFormat.Short4:
				return 8;
			case VertexElementFormat.NormalizedShort2:
				return 4;
			case VertexElementFormat.NormalizedShort4:
				return 8;
			case VertexElementFormat.HalfVector2:
				return 4;
			case VertexElementFormat.HalfVector4:
				return 8;
			default:
				return 0;
			}
		}

		// Token: 0x04000A55 RID: 2645
		[CompilerGenerated]
		private int <VertexStride>k__BackingField;

		// Token: 0x04000A56 RID: 2646
		internal VertexElement[] elements;

		// Token: 0x04000A57 RID: 2647
		internal IntPtr elementsPin;

		// Token: 0x04000A58 RID: 2648
		private GCHandle handle;
	}
}
