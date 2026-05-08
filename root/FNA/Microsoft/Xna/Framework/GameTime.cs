using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000022 RID: 34
	public class GameTime
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00013633 File Offset: 0x00011833
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0001363B File Offset: 0x0001183B
		public TimeSpan TotalGameTime
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalGameTime>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<TotalGameTime>k__BackingField = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00013644 File Offset: 0x00011844
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0001364C File Offset: 0x0001184C
		public TimeSpan ElapsedGameTime
		{
			[CompilerGenerated]
			get
			{
				return this.<ElapsedGameTime>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ElapsedGameTime>k__BackingField = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00013655 File Offset: 0x00011855
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0001365D File Offset: 0x0001185D
		public bool IsRunningSlowly
		{
			[CompilerGenerated]
			get
			{
				return this.<IsRunningSlowly>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<IsRunningSlowly>k__BackingField = value;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00013666 File Offset: 0x00011866
		public GameTime()
		{
			this.TotalGameTime = TimeSpan.Zero;
			this.ElapsedGameTime = TimeSpan.Zero;
			this.IsRunningSlowly = false;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001368B File Offset: 0x0001188B
		public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
		{
			this.TotalGameTime = totalGameTime;
			this.ElapsedGameTime = elapsedGameTime;
			this.IsRunningSlowly = false;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000136A8 File Offset: 0x000118A8
		public GameTime(TimeSpan totalRealTime, TimeSpan elapsedRealTime, bool isRunningSlowly)
		{
			this.TotalGameTime = totalRealTime;
			this.ElapsedGameTime = elapsedRealTime;
			this.IsRunningSlowly = isRunningSlowly;
		}

		// Token: 0x04000563 RID: 1379
		[CompilerGenerated]
		private TimeSpan <TotalGameTime>k__BackingField;

		// Token: 0x04000564 RID: 1380
		[CompilerGenerated]
		private TimeSpan <ElapsedGameTime>k__BackingField;

		// Token: 0x04000565 RID: 1381
		[CompilerGenerated]
		private bool <IsRunningSlowly>k__BackingField;
	}
}
