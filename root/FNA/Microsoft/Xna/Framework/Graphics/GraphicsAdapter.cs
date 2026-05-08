using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000099 RID: 153
	public sealed class GraphicsAdapter
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0002BA99 File Offset: 0x00029C99
		public DisplayMode CurrentDisplayMode
		{
			get
			{
				return FNAPlatform.GetCurrentDisplayMode(GraphicsAdapter.Adapters.IndexOf(this));
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0002BAB0 File Offset: 0x00029CB0
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		public DisplayModeCollection SupportedDisplayModes
		{
			[CompilerGenerated]
			get
			{
				return this.<SupportedDisplayModes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SupportedDisplayModes>k__BackingField = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x0002BAC1 File Offset: 0x00029CC1
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x0002BAC9 File Offset: 0x00029CC9
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Description>k__BackingField = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x000136EE File Offset: 0x000118EE
		public int DeviceId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x0002BAD2 File Offset: 0x00029CD2
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x0002BADA File Offset: 0x00029CDA
		public string DeviceName
		{
			[CompilerGenerated]
			get
			{
				return this.<DeviceName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DeviceName>k__BackingField = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0002BAE3 File Offset: 0x00029CE3
		public bool IsDefaultAdapter
		{
			get
			{
				return this == GraphicsAdapter.DefaultAdapter;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0002BAED File Offset: 0x00029CED
		public bool IsWideScreen
		{
			get
			{
				return this.CurrentDisplayMode.AspectRatio > 1.3333334f;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0002BB01 File Offset: 0x00029D01
		public IntPtr MonitorHandle
		{
			get
			{
				return FNAPlatform.GetMonitorHandle(GraphicsAdapter.Adapters.IndexOf(this));
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x000136EE File Offset: 0x000118EE
		public int Revision
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x000136EE File Offset: 0x000118EE
		public int SubSystemId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0002BB18 File Offset: 0x00029D18
		// (set) Token: 0x06001318 RID: 4888 RVA: 0x0002BB20 File Offset: 0x00029D20
		public bool UseNullDevice
		{
			[CompilerGenerated]
			get
			{
				return this.<UseNullDevice>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseNullDevice>k__BackingField = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0002BB29 File Offset: 0x00029D29
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x0002BB31 File Offset: 0x00029D31
		public bool UseReferenceDevice
		{
			[CompilerGenerated]
			get
			{
				return this.<UseReferenceDevice>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseReferenceDevice>k__BackingField = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x000136EE File Offset: 0x000118EE
		public int VendorId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0002BB3A File Offset: 0x00029D3A
		public static GraphicsAdapter DefaultAdapter
		{
			get
			{
				return GraphicsAdapter.Adapters[0];
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0002BB47 File Offset: 0x00029D47
		public static ReadOnlyCollection<GraphicsAdapter> Adapters
		{
			get
			{
				if (GraphicsAdapter.adapters == null)
				{
					GraphicsAdapter.AdaptersChanged();
				}
				return GraphicsAdapter.adapters;
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0002BB5A File Offset: 0x00029D5A
		internal GraphicsAdapter(DisplayModeCollection modes, string name, string description)
		{
			this.SupportedDisplayModes = modes;
			this.DeviceName = name;
			this.Description = description;
			this.UseNullDevice = false;
			this.UseReferenceDevice = false;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public bool IsProfileSupported(GraphicsProfile graphicsProfile)
		{
			return true;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0002BB88 File Offset: 0x00029D88
		public bool QueryRenderTargetFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat, int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat, out int selectedMultiSampleCount)
		{
			if (format != SurfaceFormat.Color && format != SurfaceFormat.Rgba1010102 && format != SurfaceFormat.Rg32 && format != SurfaceFormat.Rgba64 && format != SurfaceFormat.Single && format != SurfaceFormat.Vector2 && format != SurfaceFormat.Vector4 && format != SurfaceFormat.HalfSingle && format != SurfaceFormat.HalfVector2 && format != SurfaceFormat.HalfVector4 && format != SurfaceFormat.HdrBlendable)
			{
				selectedFormat = SurfaceFormat.Color;
			}
			else
			{
				selectedFormat = format;
			}
			selectedDepthFormat = depthFormat;
			selectedMultiSampleCount = 0;
			return format == selectedFormat && depthFormat == selectedDepthFormat && multiSampleCount == selectedMultiSampleCount;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0002BBF1 File Offset: 0x00029DF1
		public bool QueryBackBufferFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat, int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat, out int selectedMultiSampleCount)
		{
			selectedFormat = SurfaceFormat.Color;
			selectedDepthFormat = depthFormat;
			selectedMultiSampleCount = 0;
			return format == selectedFormat && depthFormat == selectedDepthFormat && multiSampleCount == selectedMultiSampleCount;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0002BC14 File Offset: 0x00029E14
		internal static void AdaptersChanged()
		{
			GraphicsAdapter.adapters = new ReadOnlyCollection<GraphicsAdapter>(FNAPlatform.GetGraphicsAdapters());
		}

		// Token: 0x040008B8 RID: 2232
		[CompilerGenerated]
		private DisplayModeCollection <SupportedDisplayModes>k__BackingField;

		// Token: 0x040008B9 RID: 2233
		[CompilerGenerated]
		private string <Description>k__BackingField;

		// Token: 0x040008BA RID: 2234
		[CompilerGenerated]
		private string <DeviceName>k__BackingField;

		// Token: 0x040008BB RID: 2235
		[CompilerGenerated]
		private bool <UseNullDevice>k__BackingField;

		// Token: 0x040008BC RID: 2236
		[CompilerGenerated]
		private bool <UseReferenceDevice>k__BackingField;

		// Token: 0x040008BD RID: 2237
		private static ReadOnlyCollection<GraphicsAdapter> adapters;
	}
}
