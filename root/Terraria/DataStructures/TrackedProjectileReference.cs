using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	// Token: 0x02000560 RID: 1376
	public struct TrackedProjectileReference
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x0063022F File Offset: 0x0062E42F
		// (set) Token: 0x060037D1 RID: 14289 RVA: 0x00630237 File Offset: 0x0062E437
		public int ProjectileLocalIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<ProjectileLocalIndex>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProjectileLocalIndex>k__BackingField = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x00630240 File Offset: 0x0062E440
		// (set) Token: 0x060037D3 RID: 14291 RVA: 0x00630248 File Offset: 0x0062E448
		public int ProjectileOwnerIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<ProjectileOwnerIndex>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProjectileOwnerIndex>k__BackingField = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x00630251 File Offset: 0x0062E451
		// (set) Token: 0x060037D5 RID: 14293 RVA: 0x00630259 File Offset: 0x0062E459
		public int ProjectileIdentity
		{
			[CompilerGenerated]
			get
			{
				return this.<ProjectileIdentity>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProjectileIdentity>k__BackingField = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x00630262 File Offset: 0x0062E462
		// (set) Token: 0x060037D7 RID: 14295 RVA: 0x0063026A File Offset: 0x0062E46A
		public int ProjectileType
		{
			[CompilerGenerated]
			get
			{
				return this.<ProjectileType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProjectileType>k__BackingField = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x00630273 File Offset: 0x0062E473
		// (set) Token: 0x060037D9 RID: 14297 RVA: 0x0063027B File Offset: 0x0062E47B
		public bool IsTrackingSomething
		{
			[CompilerGenerated]
			get
			{
				return this.<IsTrackingSomething>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsTrackingSomething>k__BackingField = value;
			}
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x00630284 File Offset: 0x0062E484
		public void Set(Projectile proj)
		{
			this.ProjectileLocalIndex = proj.whoAmI;
			this.ProjectileOwnerIndex = proj.owner;
			this.ProjectileIdentity = proj.identity;
			this.ProjectileType = proj.type;
			this.IsTrackingSomething = true;
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x006302BD File Offset: 0x0062E4BD
		public void Clear()
		{
			this.ProjectileLocalIndex = -1;
			this.ProjectileOwnerIndex = -1;
			this.ProjectileIdentity = -1;
			this.ProjectileType = -1;
			this.IsTrackingSomething = false;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x006302E2 File Offset: 0x0062E4E2
		public void Write(BinaryWriter writer)
		{
			writer.Write((short)this.ProjectileOwnerIndex);
			if (this.ProjectileOwnerIndex == -1)
			{
				return;
			}
			writer.Write((short)this.ProjectileIdentity);
			writer.Write((short)this.ProjectileType);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x00630315 File Offset: 0x0062E515
		public bool IsTracking(Projectile proj)
		{
			return proj.whoAmI == this.ProjectileLocalIndex;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x00630328 File Offset: 0x0062E528
		public void TryReading(BinaryReader reader)
		{
			int num = (int)reader.ReadInt16();
			if (num == -1)
			{
				this.Clear();
				return;
			}
			int num2 = (int)reader.ReadInt16();
			int num3 = (int)reader.ReadInt16();
			Projectile projectile = this.FindMatchingProjectile(num, num2, num3);
			if (projectile == null)
			{
				this.Clear();
				return;
			}
			this.Set(projectile);
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x00630370 File Offset: 0x0062E570
		private Projectile FindMatchingProjectile(int expectedOwner, int expectedIdentity, int expectedType)
		{
			if (expectedOwner == -1)
			{
				return null;
			}
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.type == expectedType && projectile.owner == expectedOwner && projectile.identity == expectedIdentity)
				{
					return projectile;
				}
			}
			return null;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x006303BC File Offset: 0x0062E5BC
		public override bool Equals(object obj)
		{
			if (!(obj is TrackedProjectileReference))
			{
				return false;
			}
			TrackedProjectileReference trackedProjectileReference = (TrackedProjectileReference)obj;
			return this.Equals(trackedProjectileReference);
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x006303E1 File Offset: 0x0062E5E1
		public bool Equals(TrackedProjectileReference other)
		{
			return this.ProjectileLocalIndex == other.ProjectileLocalIndex && this.ProjectileOwnerIndex == other.ProjectileOwnerIndex && this.ProjectileIdentity == other.ProjectileIdentity && this.ProjectileType == other.ProjectileType;
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x00630421 File Offset: 0x0062E621
		public override int GetHashCode()
		{
			return (((((this.ProjectileLocalIndex * 397) ^ this.ProjectileOwnerIndex) * 397) ^ this.ProjectileIdentity) * 397) ^ this.ProjectileType;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x00630450 File Offset: 0x0062E650
		public static bool operator ==(TrackedProjectileReference c1, TrackedProjectileReference c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0063045A File Offset: 0x0062E65A
		public static bool operator !=(TrackedProjectileReference c1, TrackedProjectileReference c2)
		{
			return !c1.Equals(c2);
		}

		// Token: 0x04005BEA RID: 23530
		[CompilerGenerated]
		private int <ProjectileLocalIndex>k__BackingField;

		// Token: 0x04005BEB RID: 23531
		[CompilerGenerated]
		private int <ProjectileOwnerIndex>k__BackingField;

		// Token: 0x04005BEC RID: 23532
		[CompilerGenerated]
		private int <ProjectileIdentity>k__BackingField;

		// Token: 0x04005BED RID: 23533
		[CompilerGenerated]
		private int <ProjectileType>k__BackingField;

		// Token: 0x04005BEE RID: 23534
		[CompilerGenerated]
		private bool <IsTrackingSomething>k__BackingField;
	}
}
