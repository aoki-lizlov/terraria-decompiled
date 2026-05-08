using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public sealed class OperatingSystem : ISerializable, ICloneable
	{
		// Token: 0x060013A5 RID: 5029 RVA: 0x0004F2B4 File Offset: 0x0004D4B4
		public OperatingSystem(PlatformID platform, Version version)
			: this(platform, version, null)
		{
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004F2C0 File Offset: 0x0004D4C0
		internal OperatingSystem(PlatformID platform, Version version, string servicePack)
		{
			if (platform < PlatformID.Win32S || platform > PlatformID.MacOSX)
			{
				throw new ArgumentOutOfRangeException("platform", platform, SR.Format("Illegal enum value: {0}.", platform));
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._platform = platform;
			this._version = version;
			this._servicePack = servicePack;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0004F325 File Offset: 0x0004D525
		public PlatformID Platform
		{
			get
			{
				return this._platform;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0004F32D File Offset: 0x0004D52D
		public string ServicePack
		{
			get
			{
				return this._servicePack ?? string.Empty;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0004F33E File Offset: 0x0004D53E
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004F346 File Offset: 0x0004D546
		public object Clone()
		{
			return new OperatingSystem(this._platform, this._version, this._servicePack);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004F35F File Offset: 0x0004D55F
		public override string ToString()
		{
			return this.VersionString;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004F368 File Offset: 0x0004D568
		public string VersionString
		{
			get
			{
				if (this._versionString == null)
				{
					string text;
					switch (this._platform)
					{
					case PlatformID.Win32S:
						text = "Microsoft Win32S ";
						break;
					case PlatformID.Win32Windows:
						text = ((this._version.Major > 4 || (this._version.Major == 4 && this._version.Minor > 0)) ? "Microsoft Windows 98 " : "Microsoft Windows 95 ");
						break;
					case PlatformID.Win32NT:
						text = "Microsoft Windows NT ";
						break;
					case PlatformID.WinCE:
						text = "Microsoft Windows CE ";
						break;
					case PlatformID.Unix:
						text = "Unix ";
						break;
					case PlatformID.Xbox:
						text = "Xbox ";
						break;
					case PlatformID.MacOSX:
						text = "Mac OS X ";
						break;
					default:
						text = "<unknown> ";
						break;
					}
					this._versionString = (string.IsNullOrEmpty(this._servicePack) ? (text + this._version.ToString()) : (text + this._version.ToString(3) + " " + this._servicePack));
				}
				return this._versionString;
			}
		}

		// Token: 0x0400133D RID: 4925
		private readonly Version _version;

		// Token: 0x0400133E RID: 4926
		private readonly PlatformID _platform;

		// Token: 0x0400133F RID: 4927
		private readonly string _servicePack;

		// Token: 0x04001340 RID: 4928
		private string _versionString;
	}
}
