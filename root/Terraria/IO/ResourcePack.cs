using System;
using System.IO;
using System.Runtime.CompilerServices;
using Ionic.Zip;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Utilities;
using Terraria.GameContent;

namespace Terraria.IO
{
	// Token: 0x0200006B RID: 107
	public class ResourcePack
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x004BB941 File Offset: 0x004B9B41
		public Texture2D Icon
		{
			get
			{
				if (this._icon == null)
				{
					this._icon = this.CreateIcon();
				}
				return this._icon;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x004BB95D File Offset: 0x004B9B5D
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x004BB965 File Offset: 0x004B9B65
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x004BB96E File Offset: 0x004B9B6E
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x004BB976 File Offset: 0x004B9B76
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x004BB97F File Offset: 0x004B9B7F
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x004BB987 File Offset: 0x004B9B87
		public string Author
		{
			[CompilerGenerated]
			get
			{
				return this.<Author>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Author>k__BackingField = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x004BB990 File Offset: 0x004B9B90
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x004BB998 File Offset: 0x004B9B98
		public ResourcePackVersion Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Version>k__BackingField = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x004BB9A1 File Offset: 0x004B9BA1
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x004BB9A9 File Offset: 0x004B9BA9
		public bool IsEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.<IsEnabled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsEnabled>k__BackingField = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x004BB9B2 File Offset: 0x004B9BB2
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x004BB9BA File Offset: 0x004B9BBA
		public int SortingOrder
		{
			[CompilerGenerated]
			get
			{
				return this.<SortingOrder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SortingOrder>k__BackingField = value;
			}
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x004BB9C4 File Offset: 0x004B9BC4
		public ResourcePack(IServiceProvider services, string path, ResourcePack.BrandingType branding = ResourcePack.BrandingType.None)
		{
			if (File.Exists(path))
			{
				this.IsCompressed = true;
			}
			else if (!Directory.Exists(path))
			{
				throw new FileNotFoundException("Unable to find file or folder for resource pack at: " + path);
			}
			this.FileName = Path.GetFileName(path);
			this._services = services;
			this.FullPath = path;
			this.Branding = branding;
			if (this.IsCompressed)
			{
				this._zipFile = ZipFile.Read(path);
			}
			this.LoadManifest();
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x004BBA3C File Offset: 0x004B9C3C
		public IContentSource GetContentSource()
		{
			if (this._contentSource == null)
			{
				if (this.IsCompressed)
				{
					this._contentSource = new ZipContentSource(this.FullPath, "Content");
				}
				else
				{
					this._contentSource = new FileSystemContentSource(Path.Combine(this.FullPath, "Content"));
				}
				this._contentSource.ContentValidator = VanillaContentValidator.Instance;
			}
			return this._contentSource;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x004BBAA4 File Offset: 0x004B9CA4
		private Texture2D CreateIcon()
		{
			if (!this.HasFile("icon.png"))
			{
				return XnaExtensions.Get<IAssetRepository>(this._services).Request<Texture2D>("Images/UI/DefaultResourcePackIcon", 1).Value;
			}
			Texture2D texture2D;
			using (Stream stream = this.OpenStream("icon.png"))
			{
				texture2D = XnaExtensions.Get<AssetReaderCollection>(this._services).Read<Texture2D>(stream, ".png");
			}
			return texture2D;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x004BBB1C File Offset: 0x004B9D1C
		private void LoadManifest()
		{
			if (!this.HasFile("pack.json"))
			{
				throw new FileNotFoundException(string.Format("Resource Pack at \"{0}\" must contain a {1} file.", this.FullPath, "pack.json"));
			}
			JObject jobject;
			using (Stream stream = this.OpenStream("pack.json"))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					jobject = JObject.Parse(streamReader.ReadToEnd());
				}
			}
			this.Name = Extensions.Value<string>(jobject["Name"]);
			this.Description = Extensions.Value<string>(jobject["Description"]);
			this.Author = Extensions.Value<string>(jobject["Author"]);
			this.Version = jobject["Version"].ToObject<ResourcePackVersion>();
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x004BBBFC File Offset: 0x004B9DFC
		private Stream OpenStream(string fileName)
		{
			if (!this.IsCompressed)
			{
				return File.OpenRead(Path.Combine(this.FullPath, fileName));
			}
			ZipEntry zipEntry = this._zipFile[fileName];
			MemoryStream memoryStream = new MemoryStream((int)zipEntry.UncompressedSize);
			zipEntry.Extract(memoryStream);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x004BBC4B File Offset: 0x004B9E4B
		private bool HasFile(string fileName)
		{
			if (!this.IsCompressed)
			{
				return File.Exists(Path.Combine(this.FullPath, fileName));
			}
			return this._zipFile.ContainsEntry(fileName);
		}

		// Token: 0x04001082 RID: 4226
		public readonly string FullPath;

		// Token: 0x04001083 RID: 4227
		public readonly string FileName;

		// Token: 0x04001084 RID: 4228
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04001085 RID: 4229
		[CompilerGenerated]
		private string <Description>k__BackingField;

		// Token: 0x04001086 RID: 4230
		[CompilerGenerated]
		private string <Author>k__BackingField;

		// Token: 0x04001087 RID: 4231
		[CompilerGenerated]
		private ResourcePackVersion <Version>k__BackingField;

		// Token: 0x04001088 RID: 4232
		[CompilerGenerated]
		private bool <IsEnabled>k__BackingField;

		// Token: 0x04001089 RID: 4233
		[CompilerGenerated]
		private int <SortingOrder>k__BackingField;

		// Token: 0x0400108A RID: 4234
		private readonly IServiceProvider _services;

		// Token: 0x0400108B RID: 4235
		public readonly bool IsCompressed;

		// Token: 0x0400108C RID: 4236
		public readonly ResourcePack.BrandingType Branding;

		// Token: 0x0400108D RID: 4237
		private readonly ZipFile _zipFile;

		// Token: 0x0400108E RID: 4238
		private Texture2D _icon;

		// Token: 0x0400108F RID: 4239
		private IContentSource _contentSource;

		// Token: 0x04001090 RID: 4240
		private const string ICON_FILE_NAME = "icon.png";

		// Token: 0x04001091 RID: 4241
		private const string PACK_FILE_NAME = "pack.json";

		// Token: 0x0200065F RID: 1631
		public enum BrandingType
		{
			// Token: 0x0400665F RID: 26207
			None,
			// Token: 0x04006660 RID: 26208
			SteamWorkshop
		}
	}
}
