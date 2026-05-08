using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000097 RID: 151
	public class WorldManifest
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x004DD3BF File Offset: 0x004DB5BF
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x004DD3C7 File Offset: 0x004DB5C7
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Version>k__BackingField = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x004DD3D0 File Offset: 0x004DB5D0
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x004DD3D8 File Offset: 0x004DB5D8
		public string GitSHA
		{
			[CompilerGenerated]
			get
			{
				return this.<GitSHA>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GitSHA>k__BackingField = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x004DD3E4 File Offset: 0x004DB5E4
		public uint? FinalHash
		{
			get
			{
				if (this.GenPassResults.Count <= 0)
				{
					return null;
				}
				return this.GenPassResults[this.GenPassResults.Count - 1].Hash;
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x004DD428 File Offset: 0x004DB628
		public static WorldManifest Deserialize(string json)
		{
			try
			{
				if (!string.IsNullOrEmpty(json))
				{
					return JsonConvert.DeserializeObject<WorldManifest>(json, WorldManifest.SerializerSettings);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}
			return new WorldManifest();
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x004DD46C File Offset: 0x004DB66C
		public string Serialize()
		{
			return JsonConvert.SerializeObject(this, WorldManifest.SerializerSettings);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x004DD479 File Offset: 0x004DB679
		public WorldManifest Clone()
		{
			return JsonConvert.DeserializeObject<WorldManifest>(JsonConvert.SerializeObject(this, WorldManifest.SerializerSettings), WorldManifest.SerializerSettings);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x004DD490 File Offset: 0x004DB690
		public WorldManifest()
		{
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x004DD4A3 File Offset: 0x004DB6A3
		// Note: this type is marked as 'beforefieldinit'.
		static WorldManifest()
		{
		}

		// Token: 0x040011C8 RID: 4552
		[CompilerGenerated]
		private string <Version>k__BackingField;

		// Token: 0x040011C9 RID: 4553
		[CompilerGenerated]
		private string <GitSHA>k__BackingField;

		// Token: 0x040011CA RID: 4554
		public List<GenPassResult> GenPassResults = new List<GenPassResult>();

		// Token: 0x040011CB RID: 4555
		public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = 4
		};
	}
}
