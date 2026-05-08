using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200009A RID: 154
	public class GraphicsDevice : IDisposable
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0002BC2A File Offset: 0x00029E2A
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x0002BC32 File Offset: 0x00029E32
		public bool IsDisposed
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDisposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsDisposed>k__BackingField = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x000136EB File Offset: 0x000118EB
		public GraphicsDeviceStatus GraphicsDeviceStatus
		{
			get
			{
				return GraphicsDeviceStatus.Normal;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0002BC3B File Offset: 0x00029E3B
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0002BC43 File Offset: 0x00029E43
		public GraphicsAdapter Adapter
		{
			[CompilerGenerated]
			get
			{
				return this.<Adapter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Adapter>k__BackingField = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0002BC4C File Offset: 0x00029E4C
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0002BC54 File Offset: 0x00029E54
		public GraphicsProfile GraphicsProfile
		{
			[CompilerGenerated]
			get
			{
				return this.<GraphicsProfile>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GraphicsProfile>k__BackingField = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0002BC5D File Offset: 0x00029E5D
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0002BC65 File Offset: 0x00029E65
		public PresentationParameters PresentationParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<PresentationParameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PresentationParameters>k__BackingField = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0002BC70 File Offset: 0x00029E70
		public DisplayMode DisplayMode
		{
			get
			{
				if (this.PresentationParameters.IsFullScreen)
				{
					int num;
					int num2;
					FNA3D.FNA3D_GetBackbufferSize(this.GLDevice, out num, out num2);
					return new DisplayMode(num, num2, FNA3D.FNA3D_GetBackbufferSurfaceFormat(this.GLDevice));
				}
				return this.Adapter.CurrentDisplayMode;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0002BCB7 File Offset: 0x00029EB7
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x0002BCBF File Offset: 0x00029EBF
		public TextureCollection Textures
		{
			[CompilerGenerated]
			get
			{
				return this.<Textures>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Textures>k__BackingField = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0002BCC8 File Offset: 0x00029EC8
		// (set) Token: 0x06001330 RID: 4912 RVA: 0x0002BCD0 File Offset: 0x00029ED0
		public SamplerStateCollection SamplerStates
		{
			[CompilerGenerated]
			get
			{
				return this.<SamplerStates>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SamplerStates>k__BackingField = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0002BCD9 File Offset: 0x00029ED9
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x0002BCE1 File Offset: 0x00029EE1
		public TextureCollection VertexTextures
		{
			[CompilerGenerated]
			get
			{
				return this.<VertexTextures>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VertexTextures>k__BackingField = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0002BCEA File Offset: 0x00029EEA
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x0002BCF2 File Offset: 0x00029EF2
		public SamplerStateCollection VertexSamplerStates
		{
			[CompilerGenerated]
			get
			{
				return this.<VertexSamplerStates>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VertexSamplerStates>k__BackingField = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0002BCFB File Offset: 0x00029EFB
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x0002BD03 File Offset: 0x00029F03
		public BlendState BlendState
		{
			get
			{
				return this.nextBlend;
			}
			set
			{
				this.nextBlend = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0002BD0C File Offset: 0x00029F0C
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x0002BD14 File Offset: 0x00029F14
		public DepthStencilState DepthStencilState
		{
			get
			{
				return this.nextDepthStencil;
			}
			set
			{
				this.nextDepthStencil = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0002BD1D File Offset: 0x00029F1D
		// (set) Token: 0x0600133A RID: 4922 RVA: 0x0002BD25 File Offset: 0x00029F25
		public RasterizerState RasterizerState
		{
			[CompilerGenerated]
			get
			{
				return this.<RasterizerState>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RasterizerState>k__BackingField = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0002BD2E File Offset: 0x00029F2E
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x0002BD36 File Offset: 0x00029F36
		public Rectangle ScissorRectangle
		{
			get
			{
				return this.INTERNAL_scissorRectangle;
			}
			set
			{
				this.INTERNAL_scissorRectangle = value;
				FNA3D.FNA3D_SetScissorRect(this.GLDevice, ref value);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0002BD4C File Offset: 0x00029F4C
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x0002BD54 File Offset: 0x00029F54
		public Viewport Viewport
		{
			get
			{
				return this.INTERNAL_viewport;
			}
			set
			{
				this.INTERNAL_viewport = value;
				FNA3D.FNA3D_SetViewport(this.GLDevice, ref value.viewport);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0002BD70 File Offset: 0x00029F70
		// (set) Token: 0x06001340 RID: 4928 RVA: 0x0002BD8B File Offset: 0x00029F8B
		public Color BlendFactor
		{
			get
			{
				Color color;
				FNA3D.FNA3D_GetBlendFactor(this.GLDevice, out color);
				return color;
			}
			set
			{
				FNA3D.FNA3D_SetBlendFactor(this.GLDevice, ref value);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0002BD9A File Offset: 0x00029F9A
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x0002BDA7 File Offset: 0x00029FA7
		public int MultiSampleMask
		{
			get
			{
				return FNA3D.FNA3D_GetMultiSampleMask(this.GLDevice);
			}
			set
			{
				FNA3D.FNA3D_SetMultiSampleMask(this.GLDevice, value);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0002BDB5 File Offset: 0x00029FB5
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x0002BDC2 File Offset: 0x00029FC2
		public int ReferenceStencil
		{
			get
			{
				return FNA3D.FNA3D_GetReferenceStencil(this.GLDevice);
			}
			set
			{
				FNA3D.FNA3D_SetReferenceStencil(this.GLDevice, value);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0002BDD0 File Offset: 0x00029FD0
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x0002BDD8 File Offset: 0x00029FD8
		public IndexBuffer Indices
		{
			[CompilerGenerated]
			get
			{
				return this.<Indices>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Indices>k__BackingField = value;
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001347 RID: 4935 RVA: 0x0002BDE4 File Offset: 0x00029FE4
		// (remove) Token: 0x06001348 RID: 4936 RVA: 0x0002BE1C File Offset: 0x0002A01C
		public event EventHandler<EventArgs> DeviceLost
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.DeviceLost;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceLost, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.DeviceLost;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceLost, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001349 RID: 4937 RVA: 0x0002BE54 File Offset: 0x0002A054
		// (remove) Token: 0x0600134A RID: 4938 RVA: 0x0002BE8C File Offset: 0x0002A08C
		public event EventHandler<EventArgs> DeviceReset
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.DeviceReset;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceReset, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.DeviceReset;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceReset, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600134B RID: 4939 RVA: 0x0002BEC4 File Offset: 0x0002A0C4
		// (remove) Token: 0x0600134C RID: 4940 RVA: 0x0002BEFC File Offset: 0x0002A0FC
		public event EventHandler<EventArgs> DeviceResetting
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.DeviceResetting;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceResetting, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.DeviceResetting;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.DeviceResetting, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600134D RID: 4941 RVA: 0x0002BF34 File Offset: 0x0002A134
		// (remove) Token: 0x0600134E RID: 4942 RVA: 0x0002BF6C File Offset: 0x0002A16C
		public event EventHandler<ResourceCreatedEventArgs> ResourceCreated
		{
			[CompilerGenerated]
			add
			{
				EventHandler<ResourceCreatedEventArgs> eventHandler = this.ResourceCreated;
				EventHandler<ResourceCreatedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ResourceCreatedEventArgs> eventHandler3 = (EventHandler<ResourceCreatedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ResourceCreatedEventArgs>>(ref this.ResourceCreated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<ResourceCreatedEventArgs> eventHandler = this.ResourceCreated;
				EventHandler<ResourceCreatedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ResourceCreatedEventArgs> eventHandler3 = (EventHandler<ResourceCreatedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ResourceCreatedEventArgs>>(ref this.ResourceCreated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600134F RID: 4943 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
		// (remove) Token: 0x06001350 RID: 4944 RVA: 0x0002BFDC File Offset: 0x0002A1DC
		public event EventHandler<ResourceDestroyedEventArgs> ResourceDestroyed
		{
			[CompilerGenerated]
			add
			{
				EventHandler<ResourceDestroyedEventArgs> eventHandler = this.ResourceDestroyed;
				EventHandler<ResourceDestroyedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ResourceDestroyedEventArgs> eventHandler3 = (EventHandler<ResourceDestroyedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ResourceDestroyedEventArgs>>(ref this.ResourceDestroyed, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<ResourceDestroyedEventArgs> eventHandler = this.ResourceDestroyed;
				EventHandler<ResourceDestroyedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ResourceDestroyedEventArgs> eventHandler3 = (EventHandler<ResourceDestroyedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ResourceDestroyedEventArgs>>(ref this.ResourceDestroyed, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001351 RID: 4945 RVA: 0x0002C014 File Offset: 0x0002A214
		// (remove) Token: 0x06001352 RID: 4946 RVA: 0x0002C04C File Offset: 0x0002A24C
		public event EventHandler<EventArgs> Disposing
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.Disposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.Disposing;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.Disposing, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0002C081 File Offset: 0x0002A281
		internal void OnResourceCreated(object resource)
		{
			if (this.ResourceCreated != null)
			{
				this.ResourceCreated(this, new ResourceCreatedEventArgs(resource));
			}
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0002C09D File Offset: 0x0002A29D
		internal void OnResourceDestroyed(string name, object tag)
		{
			if (this.ResourceDestroyed != null)
			{
				this.ResourceDestroyed(this, new ResourceDestroyedEventArgs(name, tag));
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0002C0BC File Offset: 0x0002A2BC
		public unsafe GraphicsDevice(GraphicsAdapter adapter, GraphicsProfile graphicsProfile, PresentationParameters presentationParameters)
		{
			if (presentationParameters == null)
			{
				throw new ArgumentNullException("presentationParameters");
			}
			this.Adapter = adapter;
			this.PresentationParameters = presentationParameters;
			this.GraphicsProfile = graphicsProfile;
			this.PresentationParameters.MultiSampleCount = MathHelper.ClosestMSAAPower(this.PresentationParameters.MultiSampleCount);
			try
			{
				this.GLDevice = FNA3D.FNA3D_CreateDevice(ref this.PresentationParameters.parameters, 0);
			}
			catch (Exception ex)
			{
				throw new NoSuitableGraphicsDeviceException(ex.Message);
			}
			Mouse.INTERNAL_BackBufferWidth = this.PresentationParameters.BackBufferWidth;
			Mouse.INTERNAL_BackBufferHeight = this.PresentationParameters.BackBufferHeight;
			TouchPanel.DisplayWidth = this.PresentationParameters.BackBufferWidth;
			TouchPanel.DisplayHeight = this.PresentationParameters.BackBufferHeight;
			this.BlendState = BlendState.Opaque;
			this.DepthStencilState = DepthStencilState.Default;
			this.RasterizerState = RasterizerState.CullCounterClockwise;
			int num;
			int num2;
			FNA3D.FNA3D_GetMaxTextureSlots(this.GLDevice, out num, out num2);
			this.Textures = new TextureCollection(num, this.modifiedSamplers);
			this.SamplerStates = new SamplerStateCollection(num, this.modifiedSamplers);
			this.VertexTextures = new TextureCollection(num2, this.modifiedVertexSamplers);
			this.VertexSamplerStates = new SamplerStateCollection(num2, this.modifiedVertexSamplers);
			this.Viewport = new Viewport(this.PresentationParameters.Bounds);
			this.ScissorRectangle = this.Viewport.Bounds;
			this.PipelineCache = new PipelineCache(this);
			this.effectStateChangesPtr = FNAPlatform.Malloc(sizeof(Effect.MOJOSHADER_effectStateChanges));
			Effect.MOJOSHADER_effectStateChanges* ptr = (Effect.MOJOSHADER_effectStateChanges*)(void*)this.effectStateChangesPtr;
			ptr->render_state_change_count = 0U;
			ptr->sampler_state_change_count = 0U;
			ptr->vertex_sampler_state_change_count = 0U;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		~GraphicsDevice()
		{
			this.Dispose(false);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0002C310 File Offset: 0x0002A510
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0002C320 File Offset: 0x0002A520
		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				this.IsDisposed = true;
				if (disposing)
				{
					if (this.Disposing != null)
					{
						this.Disposing(this, EventArgs.Empty);
					}
					object obj = this.resourcesLock;
					lock (obj)
					{
						GCHandle[] array = this.resources.ToArray();
						this.resources.Clear();
						foreach (GCHandle gchandle in array)
						{
							object target = gchandle.Target;
							if (target != null)
							{
								(target as IDisposable).Dispose();
							}
						}
					}
					if (this.userVertexBuffer != IntPtr.Zero)
					{
						FNA3D.FNA3D_AddDisposeVertexBuffer(this.GLDevice, this.userVertexBuffer);
					}
					if (this.userIndexBuffer != IntPtr.Zero)
					{
						FNA3D.FNA3D_AddDisposeIndexBuffer(this.GLDevice, this.userIndexBuffer);
					}
					FNAPlatform.Free(this.effectStateChangesPtr);
					FNA3D.FNA3D_DestroyDevice(this.GLDevice);
				}
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0002C434 File Offset: 0x0002A634
		internal void AddResourceReference(GCHandle resourceReference)
		{
			object obj = this.resourcesLock;
			lock (obj)
			{
				this.resources.Add(resourceReference);
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0002C47C File Offset: 0x0002A67C
		internal bool RemoveResourceReference(GCHandle resourceReference)
		{
			object obj = this.resourcesLock;
			lock (obj)
			{
				int i = 0;
				int count = this.resources.Count;
				while (i < count)
				{
					if (!(this.resources[i] != resourceReference))
					{
						this.resources[i] = this.resources[this.resources.Count - 1];
						this.resources.RemoveAt(this.resources.Count - 1);
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0002C528 File Offset: 0x0002A728
		internal void QuietlyUpdateAdapter(GraphicsAdapter adapter)
		{
			this.Adapter = adapter;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0002C531 File Offset: 0x0002A731
		public void Present()
		{
			if (this.renderTargetCount > 0)
			{
				throw new InvalidOperationException("Cannot present while render targets are bound");
			}
			FNA3D.FNA3D_SwapBuffers(this.GLDevice, IntPtr.Zero, IntPtr.Zero, this.PresentationParameters.DeviceWindowHandle);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0002C568 File Offset: 0x0002A768
		public void Present(Rectangle? sourceRectangle, Rectangle? destinationRectangle, IntPtr overrideWindowHandle)
		{
			if (this.renderTargetCount > 0)
			{
				throw new InvalidOperationException("Cannot present while render targets are bound");
			}
			if (overrideWindowHandle == IntPtr.Zero)
			{
				overrideWindowHandle = this.PresentationParameters.DeviceWindowHandle;
			}
			if (sourceRectangle != null && destinationRectangle != null)
			{
				Rectangle value = sourceRectangle.Value;
				Rectangle value2 = destinationRectangle.Value;
				FNA3D.FNA3D_SwapBuffers(this.GLDevice, ref value, ref value2, overrideWindowHandle);
				return;
			}
			if (sourceRectangle != null)
			{
				Rectangle value3 = sourceRectangle.Value;
				FNA3D.FNA3D_SwapBuffers(this.GLDevice, ref value3, IntPtr.Zero, overrideWindowHandle);
				return;
			}
			if (destinationRectangle != null)
			{
				Rectangle value4 = destinationRectangle.Value;
				FNA3D.FNA3D_SwapBuffers(this.GLDevice, IntPtr.Zero, ref value4, overrideWindowHandle);
				return;
			}
			FNA3D.FNA3D_SwapBuffers(this.GLDevice, IntPtr.Zero, IntPtr.Zero, overrideWindowHandle);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0002C636 File Offset: 0x0002A836
		public void Reset()
		{
			this.Reset(this.PresentationParameters, this.Adapter);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0002C64A File Offset: 0x0002A84A
		public void Reset(PresentationParameters presentationParameters)
		{
			this.Reset(presentationParameters, this.Adapter);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0002C65C File Offset: 0x0002A85C
		public void Reset(PresentationParameters presentationParameters, GraphicsAdapter graphicsAdapter)
		{
			if (presentationParameters == null)
			{
				throw new ArgumentNullException("presentationParameters");
			}
			this.PresentationParameters = presentationParameters;
			this.Adapter = graphicsAdapter;
			this.PresentationParameters.MultiSampleCount = FNA3D.FNA3D_GetMaxMultiSampleCount(this.GLDevice, this.PresentationParameters.BackBufferFormat, MathHelper.ClosestMSAAPower(this.PresentationParameters.MultiSampleCount));
			if (this.DeviceResetting != null)
			{
				this.DeviceResetting(this, EventArgs.Empty);
			}
			FNA3D.FNA3D_ResetBackbuffer(this.GLDevice, ref this.PresentationParameters.parameters);
			Mouse.INTERNAL_BackBufferWidth = this.PresentationParameters.BackBufferWidth;
			Mouse.INTERNAL_BackBufferHeight = this.PresentationParameters.BackBufferHeight;
			TouchPanel.DisplayWidth = this.PresentationParameters.BackBufferWidth;
			TouchPanel.DisplayHeight = this.PresentationParameters.BackBufferHeight;
			this.Viewport = new Viewport(0, 0, this.PresentationParameters.BackBufferWidth, this.PresentationParameters.BackBufferHeight);
			this.ScissorRectangle = new Rectangle(0, 0, this.PresentationParameters.BackBufferWidth, this.PresentationParameters.BackBufferHeight);
			if (this.DeviceReset != null)
			{
				this.DeviceReset(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0002C784 File Offset: 0x0002A984
		public void Clear(Color color)
		{
			this.Clear(ClearOptions.Target | ClearOptions.DepthBuffer | ClearOptions.Stencil, color.ToVector4(), this.Viewport.MaxDepth, 0);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0002C7AE File Offset: 0x0002A9AE
		public void Clear(ClearOptions options, Color color, float depth, int stencil)
		{
			this.Clear(options, color.ToVector4(), depth, stencil);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
		public void Clear(ClearOptions options, Vector4 color, float depth, int stencil)
		{
			DepthFormat depthFormat;
			if (this.renderTargetCount == 0)
			{
				depthFormat = FNA3D.FNA3D_GetBackbufferDepthFormat(this.GLDevice);
			}
			else
			{
				depthFormat = (this.renderTargetBindings[0].RenderTarget as IRenderTarget).DepthStencilFormat;
			}
			if (depthFormat == DepthFormat.None)
			{
				options &= ClearOptions.Target;
			}
			else if (depthFormat != DepthFormat.Depth24Stencil8)
			{
				options &= ~ClearOptions.Stencil;
			}
			FNA3D.FNA3D_Clear(this.GLDevice, options, ref color, depth, stencil);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0002C828 File Offset: 0x0002AA28
		public void GetBackBufferData<T>(T[] data) where T : struct
		{
			this.GetBackBufferData<T>(null, data, 0, data.Length);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0002C84C File Offset: 0x0002AA4C
		public void GetBackBufferData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			this.GetBackBufferData<T>(null, data, startIndex, elementCount);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0002C86C File Offset: 0x0002AA6C
		public void GetBackBufferData<T>(Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			int num;
			int num2;
			int width;
			int height;
			if (rect == null)
			{
				num = 0;
				num2 = 0;
				FNA3D.FNA3D_GetBackbufferSize(this.GLDevice, out width, out height);
			}
			else
			{
				num = rect.Value.X;
				num2 = rect.Value.Y;
				width = rect.Value.Width;
				height = rect.Value.Height;
			}
			int num3 = MarshalHelper.SizeOf<T>();
			Texture.ValidateGetDataFormat(FNA3D.FNA3D_GetBackbufferSurfaceFormat(this.GLDevice), num3);
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_ReadBackbuffer(this.GLDevice, num, num2, width, height, gchandle.AddrOfPinnedObject() + startIndex * num3, data.Length * num3);
			gchandle.Free();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0002C919 File Offset: 0x0002AB19
		public void SetRenderTarget(RenderTarget2D renderTarget)
		{
			if (renderTarget == null)
			{
				this.SetRenderTargets(null);
				return;
			}
			this.singleTargetCache[0] = new RenderTargetBinding(renderTarget);
			this.SetRenderTargets(this.singleTargetCache);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0002C944 File Offset: 0x0002AB44
		public void SetRenderTarget(RenderTargetCube renderTarget, CubeMapFace cubeMapFace)
		{
			if (renderTarget == null)
			{
				this.SetRenderTargets(null);
				return;
			}
			this.singleTargetCache[0] = new RenderTargetBinding(renderTarget, cubeMapFace);
			this.SetRenderTargets(this.singleTargetCache);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0002C970 File Offset: 0x0002AB70
		public unsafe void SetRenderTargets(params RenderTargetBinding[] renderTargets)
		{
			FNA3D.FNA3D_ApplyRasterizerState(this.GLDevice, ref this.RasterizerState.state);
			this.ApplySamplers();
			if (renderTargets == null && this.renderTargetCount == 0)
			{
				return;
			}
			if (renderTargets != null && renderTargets.Length == this.renderTargetCount)
			{
				bool flag = true;
				for (int i = 0; i < renderTargets.Length; i++)
				{
					if (renderTargets[i].RenderTarget != this.renderTargetBindings[i].RenderTarget || renderTargets[i].CubeMapFace != this.renderTargetBindings[i].CubeMapFace)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return;
				}
			}
			int num;
			int num2;
			RenderTargetUsage renderTargetUsage;
			if (renderTargets == null || renderTargets.Length == 0)
			{
				FNA3D.FNA3D_SetRenderTargets(this.GLDevice, IntPtr.Zero, 0, IntPtr.Zero, DepthFormat.None, (this.PresentationParameters.RenderTargetUsage > RenderTargetUsage.DiscardContents) ? 1 : 0);
				num = this.PresentationParameters.BackBufferWidth;
				num2 = this.PresentationParameters.BackBufferHeight;
				renderTargetUsage = this.PresentationParameters.RenderTargetUsage;
				for (int j = 0; j < this.renderTargetCount; j++)
				{
					FNA3D.FNA3D_ResolveTarget(this.GLDevice, ref this.nativeTargetBindings[j]);
				}
				Array.Clear(this.renderTargetBindings, 0, this.renderTargetBindings.Length);
				Array.Clear(this.nativeTargetBindings, 0, this.nativeTargetBindings.Length);
				this.renderTargetCount = 0;
			}
			else
			{
				IRenderTarget renderTarget = renderTargets[0].RenderTarget as IRenderTarget;
				fixed (FNA3D.FNA3D_RenderTargetBinding* ptr = &this.nativeTargetBindingsNext[0])
				{
					FNA3D.FNA3D_RenderTargetBinding* ptr2 = ptr;
					GraphicsDevice.PrepareRenderTargetBindings(ptr2, renderTargets);
					FNA3D.FNA3D_SetRenderTargets(this.GLDevice, ptr2, renderTargets.Length, renderTarget.DepthStencilBuffer, renderTarget.DepthStencilFormat, (renderTarget.RenderTargetUsage > RenderTargetUsage.DiscardContents) ? 1 : 0);
				}
				num = renderTarget.Width;
				num2 = renderTarget.Height;
				renderTargetUsage = renderTarget.RenderTargetUsage;
				for (int k = 0; k < this.renderTargetCount; k++)
				{
					bool flag2 = false;
					for (int l = 0; l < renderTargets.Length; l++)
					{
						if (this.renderTargetBindings[k].RenderTarget == renderTargets[l].RenderTarget)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						FNA3D.FNA3D_ResolveTarget(this.GLDevice, ref this.nativeTargetBindings[k]);
					}
				}
				Array.Clear(this.renderTargetBindings, 0, this.renderTargetBindings.Length);
				Array.Copy(renderTargets, this.renderTargetBindings, renderTargets.Length);
				Array.Clear(this.nativeTargetBindings, 0, this.nativeTargetBindings.Length);
				Array.Copy(this.nativeTargetBindingsNext, this.nativeTargetBindings, renderTargets.Length);
				this.renderTargetCount = renderTargets.Length;
			}
			this.Viewport = new Viewport(0, 0, num, num2);
			this.ScissorRectangle = new Rectangle(0, 0, num, num2);
			if (renderTargetUsage == RenderTargetUsage.DiscardContents)
			{
				this.Clear(ClearOptions.Target | ClearOptions.DepthBuffer | ClearOptions.Stencil, GraphicsDevice.DiscardColor, this.Viewport.MaxDepth, 0);
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0002CC38 File Offset: 0x0002AE38
		public RenderTargetBinding[] GetRenderTargets()
		{
			RenderTargetBinding[] array = new RenderTargetBinding[this.renderTargetCount];
			Array.Copy(this.renderTargetBindings, array, this.renderTargetCount);
			return array;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0002CC64 File Offset: 0x0002AE64
		public int GetRenderTargetsNoAllocEXT(RenderTargetBinding[] output)
		{
			if (output == null)
			{
				return this.renderTargetCount;
			}
			if (output.Length < this.renderTargetCount)
			{
				throw new ArgumentException("Output buffer size incorrect");
			}
			Array.Copy(this.renderTargetBindings, output, this.renderTargetCount);
			return this.renderTargetCount;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0002CC9E File Offset: 0x0002AE9E
		public void SetVertexBuffer(VertexBuffer vertexBuffer)
		{
			this.SetVertexBuffer(vertexBuffer, 0);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0002CCA8 File Offset: 0x0002AEA8
		public void SetVertexBuffer(VertexBuffer vertexBuffer, int vertexOffset)
		{
			if (vertexBuffer != null)
			{
				if (this.vertexBufferBindings[0].VertexBuffer != vertexBuffer || this.vertexBufferBindings[0].VertexOffset != vertexOffset)
				{
					this.vertexBufferBindings[0] = new VertexBufferBinding(vertexBuffer, vertexOffset);
					this.vertexBuffersUpdated = true;
				}
				if (this.vertexBufferCount > 1)
				{
					for (int i = 1; i < this.vertexBufferCount; i++)
					{
						this.vertexBufferBindings[i] = VertexBufferBinding.None;
					}
					this.vertexBuffersUpdated = true;
				}
				this.vertexBufferCount = 1;
				return;
			}
			if (this.vertexBufferCount == 0)
			{
				return;
			}
			for (int j = 0; j < this.vertexBufferCount; j++)
			{
				this.vertexBufferBindings[j] = VertexBufferBinding.None;
			}
			this.vertexBufferCount = 0;
			this.vertexBuffersUpdated = true;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0002CD70 File Offset: 0x0002AF70
		public void SetVertexBuffers(params VertexBufferBinding[] vertexBuffers)
		{
			if (vertexBuffers == null)
			{
				if (this.vertexBufferCount == 0)
				{
					return;
				}
				for (int i = 0; i < this.vertexBufferCount; i++)
				{
					this.vertexBufferBindings[i] = VertexBufferBinding.None;
				}
				this.vertexBufferCount = 0;
				this.vertexBuffersUpdated = true;
				return;
			}
			else
			{
				if (vertexBuffers.Length > this.vertexBufferBindings.Length)
				{
					throw new ArgumentOutOfRangeException("vertexBuffers", string.Format("Max Vertex Buffers supported is {0}", this.vertexBufferBindings.Length));
				}
				int j;
				for (j = 0; j < vertexBuffers.Length; j++)
				{
					if (this.vertexBufferBindings[j].VertexBuffer != vertexBuffers[j].VertexBuffer || this.vertexBufferBindings[j].VertexOffset != vertexBuffers[j].VertexOffset || this.vertexBufferBindings[j].InstanceFrequency != vertexBuffers[j].InstanceFrequency)
					{
						this.vertexBufferBindings[j] = vertexBuffers[j];
						this.vertexBuffersUpdated = true;
					}
				}
				if (vertexBuffers.Length < this.vertexBufferCount)
				{
					while (j < this.vertexBufferCount)
					{
						this.vertexBufferBindings[j] = VertexBufferBinding.None;
						j++;
					}
					this.vertexBuffersUpdated = true;
				}
				this.vertexBufferCount = vertexBuffers.Length;
				return;
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0002CEAC File Offset: 0x0002B0AC
		public VertexBufferBinding[] GetVertexBuffers()
		{
			VertexBufferBinding[] array = new VertexBufferBinding[this.vertexBufferCount];
			Array.Copy(this.vertexBufferBindings, array, this.vertexBufferCount);
			return array;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0002CED8 File Offset: 0x0002B0D8
		public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount)
		{
			this.ApplyState();
			this.PrepareVertexBindingArray(baseVertex);
			FNA3D.FNA3D_DrawIndexedPrimitives(this.GLDevice, primitiveType, baseVertex, minVertexIndex, numVertices, startIndex, primitiveCount, this.Indices.buffer, this.Indices.IndexElementSize);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0002CF1C File Offset: 0x0002B11C
		public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, int instanceCount)
		{
			if (FNA3D.FNA3D_SupportsHardwareInstancing(this.GLDevice) == 0)
			{
				throw new NoSuitableGraphicsDeviceException("Your hardware does not support hardware instancing!");
			}
			this.ApplyState();
			this.PrepareVertexBindingArray(baseVertex);
			FNA3D.FNA3D_DrawInstancedPrimitives(this.GLDevice, primitiveType, baseVertex, minVertexIndex, numVertices, startIndex, primitiveCount, instanceCount, this.Indices.buffer, this.Indices.IndexElementSize);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0002CF7A File Offset: 0x0002B17A
		public void DrawPrimitives(PrimitiveType primitiveType, int vertexStart, int primitiveCount)
		{
			this.ApplyState();
			this.PrepareVertexBindingArray(0);
			FNA3D.FNA3D_DrawPrimitives(this.GLDevice, primitiveType, vertexStart, primitiveCount);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0002CF98 File Offset: 0x0002B198
		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, short[] indexData, int indexOffset, int primitiveCount) where T : struct, IVertexType
		{
			this.ApplyState();
			GCHandle gchandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(indexData, GCHandleType.Pinned);
			this.PrepareUserVertexBuffer(gchandle.AddrOfPinnedObject(), numVertices, vertexOffset, VertexDeclarationCache<T>.VertexDeclaration);
			this.PrepareUserIndexBuffer(gchandle2.AddrOfPinnedObject(), GraphicsDevice.PrimitiveVerts(primitiveType, primitiveCount), indexOffset, 2);
			gchandle2.Free();
			gchandle.Free();
			FNA3D.FNA3D_DrawIndexedPrimitives(this.GLDevice, primitiveType, 0, 0, numVertices, 0, primitiveCount, this.userIndexBuffer, IndexElementSize.SixteenBits);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0002D014 File Offset: 0x0002B214
		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, short[] indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct
		{
			this.ApplyState();
			GCHandle gchandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(indexData, GCHandleType.Pinned);
			this.PrepareUserVertexBuffer(gchandle.AddrOfPinnedObject(), numVertices, vertexOffset, vertexDeclaration);
			this.PrepareUserIndexBuffer(gchandle2.AddrOfPinnedObject(), GraphicsDevice.PrimitiveVerts(primitiveType, primitiveCount), indexOffset, 2);
			gchandle2.Free();
			gchandle.Free();
			FNA3D.FNA3D_DrawIndexedPrimitives(this.GLDevice, primitiveType, 0, 0, numVertices, 0, primitiveCount, this.userIndexBuffer, IndexElementSize.SixteenBits);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0002D08C File Offset: 0x0002B28C
		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, int[] indexData, int indexOffset, int primitiveCount) where T : struct, IVertexType
		{
			this.ApplyState();
			GCHandle gchandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(indexData, GCHandleType.Pinned);
			this.PrepareUserVertexBuffer(gchandle.AddrOfPinnedObject(), numVertices, vertexOffset, VertexDeclarationCache<T>.VertexDeclaration);
			this.PrepareUserIndexBuffer(gchandle2.AddrOfPinnedObject(), GraphicsDevice.PrimitiveVerts(primitiveType, primitiveCount), indexOffset, 4);
			gchandle2.Free();
			gchandle.Free();
			FNA3D.FNA3D_DrawIndexedPrimitives(this.GLDevice, primitiveType, 0, 0, numVertices, 0, primitiveCount, this.userIndexBuffer, IndexElementSize.ThirtyTwoBits);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0002D108 File Offset: 0x0002B308
		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, int[] indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct
		{
			this.ApplyState();
			GCHandle gchandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(indexData, GCHandleType.Pinned);
			this.PrepareUserVertexBuffer(gchandle.AddrOfPinnedObject(), numVertices, vertexOffset, vertexDeclaration);
			this.PrepareUserIndexBuffer(gchandle2.AddrOfPinnedObject(), GraphicsDevice.PrimitiveVerts(primitiveType, primitiveCount), indexOffset, 4);
			gchandle2.Free();
			gchandle.Free();
			FNA3D.FNA3D_DrawIndexedPrimitives(this.GLDevice, primitiveType, 0, 0, numVertices, 0, primitiveCount, this.userIndexBuffer, IndexElementSize.ThirtyTwoBits);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0002D180 File Offset: 0x0002B380
		public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount) where T : struct, IVertexType
		{
			this.ApplyState();
			GCHandle gchandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
			this.PrepareUserVertexBuffer(gchandle.AddrOfPinnedObject(), GraphicsDevice.PrimitiveVerts(primitiveType, primitiveCount), vertexOffset, VertexDeclarationCache<T>.VertexDeclaration);
			gchandle.Free();
			FNA3D.FNA3D_DrawPrimitives(this.GLDevice, primitiveType, 0, primitiveCount);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct
		{
			this.ApplyState();
			GCHandle gchandle = GCHandle.Alloc(vertexData, GCHandleType.Pinned);
			this.PrepareUserVertexBuffer(gchandle.AddrOfPinnedObject(), GraphicsDevice.PrimitiveVerts(primitiveType, primitiveCount), vertexOffset, vertexDeclaration);
			gchandle.Free();
			FNA3D.FNA3D_DrawPrimitives(this.GLDevice, primitiveType, 0, primitiveCount);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0002D215 File Offset: 0x0002B415
		public void SetStringMarkerEXT(string text)
		{
			FNA3D.FNA3D_SetStringMarker(this.GLDevice, text);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0002D224 File Offset: 0x0002B424
		private void ApplyState()
		{
			if (this.currentBlend != this.nextBlend)
			{
				FNA3D.FNA3D_SetBlendState(this.GLDevice, ref this.nextBlend.state);
				this.currentBlend = this.nextBlend;
			}
			if (this.currentDepthStencil != this.nextDepthStencil)
			{
				FNA3D.FNA3D_SetDepthStencilState(this.GLDevice, ref this.nextDepthStencil.state);
				this.currentDepthStencil = this.nextDepthStencil;
			}
			FNA3D.FNA3D_ApplyRasterizerState(this.GLDevice, ref this.RasterizerState.state);
			this.ApplySamplers();
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0002D2B0 File Offset: 0x0002B4B0
		private void ApplySamplers()
		{
			for (int i = 0; i < this.modifiedSamplers.Length; i++)
			{
				if (this.modifiedSamplers[i])
				{
					this.modifiedSamplers[i] = false;
					FNA3D.FNA3D_VerifySampler(this.GLDevice, i, (this.Textures[i] != null) ? this.Textures[i].texture : IntPtr.Zero, ref this.SamplerStates[i].state);
				}
			}
			for (int j = 0; j < this.modifiedVertexSamplers.Length; j++)
			{
				if (this.modifiedVertexSamplers[j])
				{
					this.modifiedVertexSamplers[j] = false;
					FNA3D.FNA3D_VerifyVertexSampler(this.GLDevice, j, (this.VertexTextures[j] != null) ? this.VertexTextures[j].texture : IntPtr.Zero, ref this.VertexSamplerStates[j].state);
				}
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0002D390 File Offset: 0x0002B590
		private unsafe void PrepareVertexBindingArray(int baseVertex)
		{
			fixed (FNA3D.FNA3D_VertexBufferBinding* ptr = &this.nativeBufferBindings[0])
			{
				FNA3D.FNA3D_VertexBufferBinding* ptr2 = ptr;
				for (int i = 0; i < this.vertexBufferCount; i++)
				{
					VertexBuffer vertexBuffer = this.vertexBufferBindings[i].VertexBuffer;
					ptr2[i].vertexBuffer = vertexBuffer.buffer;
					ptr2[i].vertexDeclaration.vertexStride = vertexBuffer.VertexDeclaration.VertexStride;
					ptr2[i].vertexDeclaration.elementCount = vertexBuffer.VertexDeclaration.elements.Length;
					ptr2[i].vertexDeclaration.elements = vertexBuffer.VertexDeclaration.elementsPin;
					ptr2[i].vertexOffset = this.vertexBufferBindings[i].VertexOffset;
					ptr2[i].instanceFrequency = this.vertexBufferBindings[i].InstanceFrequency;
				}
				FNA3D.FNA3D_ApplyVertexBufferBindings(this.GLDevice, ptr2, this.vertexBufferCount, (this.vertexBuffersUpdated > false) ? 1 : 0, baseVertex);
			}
			this.vertexBuffersUpdated = false;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0002D4B8 File Offset: 0x0002B6B8
		private unsafe void PrepareUserVertexBuffer(IntPtr vertexData, int numVertices, int vertexOffset, VertexDeclaration vertexDeclaration)
		{
			int num = numVertices * vertexDeclaration.VertexStride;
			int num2 = vertexOffset * vertexDeclaration.VertexStride;
			vertexDeclaration.GraphicsDevice = this;
			if (num > this.userVertexBufferSize)
			{
				if (this.userVertexBuffer != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeVertexBuffer(this.GLDevice, this.userVertexBuffer);
				}
				this.userVertexBuffer = FNA3D.FNA3D_GenVertexBuffer(this.GLDevice, 1, BufferUsage.WriteOnly, num);
				this.userVertexBufferSize = num;
			}
			FNA3D.FNA3D_SetVertexBufferData(this.GLDevice, this.userVertexBuffer, 0, vertexData + num2, num, 1, 1, SetDataOptions.Discard);
			fixed (FNA3D.FNA3D_VertexBufferBinding* ptr = &this.nativeBufferBindings[0])
			{
				FNA3D.FNA3D_VertexBufferBinding* ptr2 = ptr;
				ptr2->vertexBuffer = this.userVertexBuffer;
				ptr2->vertexDeclaration.vertexStride = vertexDeclaration.VertexStride;
				ptr2->vertexDeclaration.elementCount = vertexDeclaration.elements.Length;
				ptr2->vertexDeclaration.elements = vertexDeclaration.elementsPin;
				ptr2->vertexOffset = 0;
				ptr2->instanceFrequency = 0;
				FNA3D.FNA3D_ApplyVertexBufferBindings(this.GLDevice, ptr2, 1, 1, 0);
			}
			this.vertexBuffersUpdated = true;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
		private void PrepareUserIndexBuffer(IntPtr indexData, int numIndices, int indexOffset, int indexElementSizeInBytes)
		{
			int num = numIndices * indexElementSizeInBytes;
			if (num > this.userIndexBufferSize)
			{
				if (this.userIndexBuffer != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeIndexBuffer(this.GLDevice, this.userIndexBuffer);
				}
				this.userIndexBuffer = FNA3D.FNA3D_GenIndexBuffer(this.GLDevice, 1, BufferUsage.WriteOnly, num);
				this.userIndexBufferSize = num;
			}
			FNA3D.FNA3D_SetIndexBufferData(this.GLDevice, this.userIndexBuffer, 0, indexData + indexOffset * indexElementSizeInBytes, num, SetDataOptions.Discard);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0002D638 File Offset: 0x0002B838
		internal unsafe static void PrepareRenderTargetBindings(FNA3D.FNA3D_RenderTargetBinding* b, RenderTargetBinding[] bindings)
		{
			int i = 0;
			while (i < bindings.Length)
			{
				Texture renderTarget = bindings[i].RenderTarget;
				IRenderTarget renderTarget2 = renderTarget as IRenderTarget;
				if (renderTarget is RenderTargetCube)
				{
					b->type = 1;
					b->data1 = renderTarget2.Width;
					b->data2 = (int)bindings[i].CubeMapFace;
				}
				else
				{
					b->type = 0;
					b->data1 = renderTarget2.Width;
					b->data2 = renderTarget2.Height;
				}
				b->levelCount = renderTarget2.LevelCount;
				b->multiSampleCount = renderTarget2.MultiSampleCount;
				b->texture = renderTarget.texture;
				b->colorBuffer = renderTarget2.ColorBuffer;
				i++;
				b++;
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0002D6F5 File Offset: 0x0002B8F5
		private static int PrimitiveVerts(PrimitiveType primitiveType, int primitiveCount)
		{
			switch (primitiveType)
			{
			case PrimitiveType.TriangleList:
				return primitiveCount * 3;
			case PrimitiveType.TriangleStrip:
				return primitiveCount + 2;
			case PrimitiveType.LineList:
				return primitiveCount * 2;
			case PrimitiveType.LineStrip:
				return primitiveCount + 1;
			case PrimitiveType.PointListEXT:
				return primitiveCount;
			default:
				throw new InvalidOperationException("Unrecognized primitive type!");
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0002D72F File Offset: 0x0002B92F
		// Note: this type is marked as 'beforefieldinit'.
		static GraphicsDevice()
		{
		}

		// Token: 0x040008BE RID: 2238
		internal const int MAX_TEXTURE_SAMPLERS = 16;

		// Token: 0x040008BF RID: 2239
		internal const int MAX_VERTEX_ATTRIBUTES = 16;

		// Token: 0x040008C0 RID: 2240
		internal const int MAX_RENDERTARGET_BINDINGS = 4;

		// Token: 0x040008C1 RID: 2241
		internal const int MAX_VERTEXTEXTURE_SAMPLERS = 4;

		// Token: 0x040008C2 RID: 2242
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x040008C3 RID: 2243
		[CompilerGenerated]
		private GraphicsAdapter <Adapter>k__BackingField;

		// Token: 0x040008C4 RID: 2244
		[CompilerGenerated]
		private GraphicsProfile <GraphicsProfile>k__BackingField;

		// Token: 0x040008C5 RID: 2245
		[CompilerGenerated]
		private PresentationParameters <PresentationParameters>k__BackingField;

		// Token: 0x040008C6 RID: 2246
		[CompilerGenerated]
		private TextureCollection <Textures>k__BackingField;

		// Token: 0x040008C7 RID: 2247
		[CompilerGenerated]
		private SamplerStateCollection <SamplerStates>k__BackingField;

		// Token: 0x040008C8 RID: 2248
		[CompilerGenerated]
		private TextureCollection <VertexTextures>k__BackingField;

		// Token: 0x040008C9 RID: 2249
		[CompilerGenerated]
		private SamplerStateCollection <VertexSamplerStates>k__BackingField;

		// Token: 0x040008CA RID: 2250
		[CompilerGenerated]
		private RasterizerState <RasterizerState>k__BackingField;

		// Token: 0x040008CB RID: 2251
		private Rectangle INTERNAL_scissorRectangle;

		// Token: 0x040008CC RID: 2252
		private Viewport INTERNAL_viewport;

		// Token: 0x040008CD RID: 2253
		[CompilerGenerated]
		private IndexBuffer <Indices>k__BackingField;

		// Token: 0x040008CE RID: 2254
		internal readonly IntPtr GLDevice;

		// Token: 0x040008CF RID: 2255
		internal readonly PipelineCache PipelineCache;

		// Token: 0x040008D0 RID: 2256
		private BlendState currentBlend;

		// Token: 0x040008D1 RID: 2257
		private BlendState nextBlend;

		// Token: 0x040008D2 RID: 2258
		private DepthStencilState currentDepthStencil;

		// Token: 0x040008D3 RID: 2259
		private DepthStencilState nextDepthStencil;

		// Token: 0x040008D4 RID: 2260
		private readonly bool[] modifiedSamplers = new bool[16];

		// Token: 0x040008D5 RID: 2261
		private readonly bool[] modifiedVertexSamplers = new bool[4];

		// Token: 0x040008D6 RID: 2262
		internal IntPtr effectStateChangesPtr;

		// Token: 0x040008D7 RID: 2263
		private readonly List<GCHandle> resources = new List<GCHandle>();

		// Token: 0x040008D8 RID: 2264
		private readonly object resourcesLock = new object();

		// Token: 0x040008D9 RID: 2265
		private static Vector4 DiscardColor = new Vector4(0f, 0f, 0f, 1f);

		// Token: 0x040008DA RID: 2266
		internal readonly RenderTargetBinding[] renderTargetBindings = new RenderTargetBinding[4];

		// Token: 0x040008DB RID: 2267
		private FNA3D.FNA3D_RenderTargetBinding[] nativeTargetBindings = new FNA3D.FNA3D_RenderTargetBinding[4];

		// Token: 0x040008DC RID: 2268
		private FNA3D.FNA3D_RenderTargetBinding[] nativeTargetBindingsNext = new FNA3D.FNA3D_RenderTargetBinding[4];

		// Token: 0x040008DD RID: 2269
		internal int renderTargetCount;

		// Token: 0x040008DE RID: 2270
		private readonly RenderTargetBinding[] singleTargetCache = new RenderTargetBinding[1];

		// Token: 0x040008DF RID: 2271
		private readonly VertexBufferBinding[] vertexBufferBindings = new VertexBufferBinding[16];

		// Token: 0x040008E0 RID: 2272
		private readonly FNA3D.FNA3D_VertexBufferBinding[] nativeBufferBindings = new FNA3D.FNA3D_VertexBufferBinding[16];

		// Token: 0x040008E1 RID: 2273
		private int vertexBufferCount;

		// Token: 0x040008E2 RID: 2274
		private bool vertexBuffersUpdated;

		// Token: 0x040008E3 RID: 2275
		private IntPtr userVertexBuffer;

		// Token: 0x040008E4 RID: 2276
		private IntPtr userIndexBuffer;

		// Token: 0x040008E5 RID: 2277
		private int userVertexBufferSize;

		// Token: 0x040008E6 RID: 2278
		private int userIndexBufferSize;

		// Token: 0x040008E7 RID: 2279
		[CompilerGenerated]
		private EventHandler<EventArgs> DeviceLost;

		// Token: 0x040008E8 RID: 2280
		[CompilerGenerated]
		private EventHandler<EventArgs> DeviceReset;

		// Token: 0x040008E9 RID: 2281
		[CompilerGenerated]
		private EventHandler<EventArgs> DeviceResetting;

		// Token: 0x040008EA RID: 2282
		[CompilerGenerated]
		private EventHandler<ResourceCreatedEventArgs> ResourceCreated;

		// Token: 0x040008EB RID: 2283
		[CompilerGenerated]
		private EventHandler<ResourceDestroyedEventArgs> ResourceDestroyed;

		// Token: 0x040008EC RID: 2284
		[CompilerGenerated]
		private EventHandler<EventArgs> Disposing;
	}
}
