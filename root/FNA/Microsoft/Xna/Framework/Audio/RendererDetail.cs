using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x02000159 RID: 345
	[Serializable]
	public struct RendererDetail
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0003DA38 File Offset: 0x0003BC38
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x0003DA40 File Offset: 0x0003BC40
		public string FriendlyName
		{
			[CompilerGenerated]
			get
			{
				return this.<FriendlyName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FriendlyName>k__BackingField = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0003DA49 File Offset: 0x0003BC49
		// (set) Token: 0x06001844 RID: 6212 RVA: 0x0003DA51 File Offset: 0x0003BC51
		public string RendererId
		{
			[CompilerGenerated]
			get
			{
				return this.<RendererId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RendererId>k__BackingField = value;
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0003DA5A File Offset: 0x0003BC5A
		internal RendererDetail(string name, string id)
		{
			this = default(RendererDetail);
			this.FriendlyName = name;
			this.RendererId = id;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0003DA74 File Offset: 0x0003BC74
		public override bool Equals(object obj)
		{
			return obj is RendererDetail && this.RendererId.Equals(((RendererDetail)obj).RendererId);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0003DAA4 File Offset: 0x0003BCA4
		public override int GetHashCode()
		{
			return this.RendererId.GetHashCode();
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0003DAB1 File Offset: 0x0003BCB1
		public override string ToString()
		{
			return this.FriendlyName;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0003DAB9 File Offset: 0x0003BCB9
		public static bool operator ==(RendererDetail left, RendererDetail right)
		{
			return left.RendererId.Equals(right.RendererId);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0003DACE File Offset: 0x0003BCCE
		public static bool operator !=(RendererDetail left, RendererDetail right)
		{
			return !left.RendererId.Equals(right.RendererId);
		}

		// Token: 0x04000B16 RID: 2838
		[CompilerGenerated]
		private string <FriendlyName>k__BackingField;

		// Token: 0x04000B17 RID: 2839
		[CompilerGenerated]
		private string <RendererId>k__BackingField;
	}
}
