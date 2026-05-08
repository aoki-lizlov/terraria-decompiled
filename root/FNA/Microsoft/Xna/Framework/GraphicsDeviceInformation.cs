using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000024 RID: 36
	public class GraphicsDeviceInformation
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x000138BD File Offset: 0x00011ABD
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x000138C5 File Offset: 0x00011AC5
		public GraphicsAdapter Adapter
		{
			[CompilerGenerated]
			get
			{
				return this.<Adapter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Adapter>k__BackingField = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x000138CE File Offset: 0x00011ACE
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x000138D6 File Offset: 0x00011AD6
		public GraphicsProfile GraphicsProfile
		{
			[CompilerGenerated]
			get
			{
				return this.<GraphicsProfile>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GraphicsProfile>k__BackingField = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x000138DF File Offset: 0x00011ADF
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x000138E7 File Offset: 0x00011AE7
		public PresentationParameters PresentationParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<PresentationParameters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PresentationParameters>k__BackingField = value;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000138F0 File Offset: 0x00011AF0
		public GraphicsDeviceInformation Clone()
		{
			return new GraphicsDeviceInformation
			{
				Adapter = this.Adapter,
				GraphicsProfile = this.GraphicsProfile,
				PresentationParameters = this.PresentationParameters.Clone()
			};
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000136F5 File Offset: 0x000118F5
		public GraphicsDeviceInformation()
		{
		}

		// Token: 0x0400056A RID: 1386
		[CompilerGenerated]
		private GraphicsAdapter <Adapter>k__BackingField;

		// Token: 0x0400056B RID: 1387
		[CompilerGenerated]
		private GraphicsProfile <GraphicsProfile>k__BackingField;

		// Token: 0x0400056C RID: 1388
		[CompilerGenerated]
		private PresentationParameters <PresentationParameters>k__BackingField;
	}
}
