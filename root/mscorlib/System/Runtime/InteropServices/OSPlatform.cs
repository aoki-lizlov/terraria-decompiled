using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200069D RID: 1693
	public readonly struct OSPlatform : IEquatable<OSPlatform>
	{
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x000DFF0A File Offset: 0x000DE10A
		public static OSPlatform Linux
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<Linux>k__BackingField;
			}
		} = new OSPlatform("LINUX");

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x000DFF11 File Offset: 0x000DE111
		public static OSPlatform OSX
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<OSX>k__BackingField;
			}
		} = new OSPlatform("OSX");

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06003F9A RID: 16282 RVA: 0x000DFF18 File Offset: 0x000DE118
		public static OSPlatform Windows
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<Windows>k__BackingField;
			}
		} = new OSPlatform("WINDOWS");

		// Token: 0x06003F9B RID: 16283 RVA: 0x000DFF1F File Offset: 0x000DE11F
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException("Value cannot be empty.", "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000DFF4E File Offset: 0x000DE14E
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000DFF56 File Offset: 0x000DE156
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000DFF64 File Offset: 0x000DE164
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000DFF73 File Offset: 0x000DE173
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000DFF8B File Offset: 0x000DE18B
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x000DFFA2 File Offset: 0x000DE1A2
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x000DFFB3 File Offset: 0x000DE1B3
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000DFFBD File Offset: 0x000DE1BD
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000DFFC9 File Offset: 0x000DE1C9
		// Note: this type is marked as 'beforefieldinit'.
		static OSPlatform()
		{
		}

		// Token: 0x04002988 RID: 10632
		private readonly string _osPlatform;

		// Token: 0x04002989 RID: 10633
		[CompilerGenerated]
		private static readonly OSPlatform <Linux>k__BackingField;

		// Token: 0x0400298A RID: 10634
		[CompilerGenerated]
		private static readonly OSPlatform <OSX>k__BackingField;

		// Token: 0x0400298B RID: 10635
		[CompilerGenerated]
		private static readonly OSPlatform <Windows>k__BackingField;
	}
}
