using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Terraria.IO
{
	// Token: 0x0200006D RID: 109
	[DebuggerDisplay("Version {Major}.{Minor}")]
	public struct ResourcePackVersion : IComparable, IComparable<ResourcePackVersion>
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x004BC270 File Offset: 0x004BA470
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x004BC278 File Offset: 0x004BA478
		[JsonProperty("major")]
		public int Major
		{
			[CompilerGenerated]
			get
			{
				return this.<Major>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Major>k__BackingField = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x004BC281 File Offset: 0x004BA481
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x004BC289 File Offset: 0x004BA489
		[JsonProperty("minor")]
		public int Minor
		{
			[CompilerGenerated]
			get
			{
				return this.<Minor>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Minor>k__BackingField = value;
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x004BC294 File Offset: 0x004BA494
		public static ResourcePackVersion Create(int major, int minor)
		{
			return new ResourcePackVersion
			{
				Major = major,
				Minor = minor
			};
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x004BC2BA File Offset: 0x004BA4BA
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is ResourcePackVersion))
			{
				throw new ArgumentException("A RatingInformation object is required for comparison.", "obj");
			}
			return this.CompareTo((ResourcePackVersion)obj);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x004BC2E8 File Offset: 0x004BA4E8
		public int CompareTo(ResourcePackVersion other)
		{
			int num = this.Major.CompareTo(other.Major);
			if (num != 0)
			{
				return num;
			}
			return this.Minor.CompareTo(other.Minor);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x004BC325 File Offset: 0x004BA525
		public static bool operator ==(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) == 0;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x004BC332 File Offset: 0x004BA532
		public static bool operator !=(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x004BC33E File Offset: 0x004BA53E
		public static bool operator <(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x004BC34B File Offset: 0x004BA54B
		public static bool operator >(ResourcePackVersion lhs, ResourcePackVersion rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x004BC358 File Offset: 0x004BA558
		public override bool Equals(object obj)
		{
			return obj is ResourcePackVersion && this.CompareTo((ResourcePackVersion)obj) == 0;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x004BC374 File Offset: 0x004BA574
		public override int GetHashCode()
		{
			long num = (long)this.Major;
			long num2 = (long)this.Minor;
			return ((num << 32) | num2).GetHashCode();
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x004BC39D File Offset: 0x004BA59D
		public string GetFormattedVersion()
		{
			return this.Major + "." + this.Minor;
		}

		// Token: 0x04001093 RID: 4243
		[CompilerGenerated]
		private int <Major>k__BackingField;

		// Token: 0x04001094 RID: 4244
		[CompilerGenerated]
		private int <Minor>k__BackingField;
	}
}
