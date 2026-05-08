using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Utilities;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000105 RID: 261
	public class ContentManager : IDisposable
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00037929 File Offset: 0x00035B29
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x00037931 File Offset: 0x00035B31
		public IServiceProvider ServiceProvider
		{
			[CompilerGenerated]
			get
			{
				return this.<ServiceProvider>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ServiceProvider>k__BackingField = value;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0003793A File Offset: 0x00035B3A
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x00037942 File Offset: 0x00035B42
		public string RootDirectory
		{
			[CompilerGenerated]
			get
			{
				return this.<RootDirectory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RootDirectory>k__BackingField = value;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0003794B File Offset: 0x00035B4B
		internal string RootDirectoryFullPath
		{
			get
			{
				if (Path.IsPathRooted(this.RootDirectory))
				{
					return this.RootDirectory;
				}
				return Path.Combine(TitleLocation.Path, this.RootDirectory);
			}
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00037974 File Offset: 0x00035B74
		public ContentManager(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this.ServiceProvider = serviceProvider;
			this.RootDirectory = string.Empty;
			ContentManager.AddContentManager(this);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000379C8 File Offset: 0x00035BC8
		public ContentManager(IServiceProvider serviceProvider, string rootDirectory)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			if (rootDirectory == null)
			{
				throw new ArgumentNullException("rootDirectory");
			}
			this.ServiceProvider = serviceProvider;
			this.RootDirectory = rootDirectory;
			ContentManager.AddContentManager(this);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00037A28 File Offset: 0x00035C28
		~ContentManager()
		{
			this.Dispose(false);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00037A58 File Offset: 0x00035C58
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			ContentManager.RemoveContentManager(this);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00037A6D File Offset: 0x00035C6D
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.Unload();
				}
				this.disposed = true;
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00037A88 File Offset: 0x00035C88
		public virtual T Load<T>(string assetName)
		{
			if (string.IsNullOrEmpty(assetName))
			{
				throw new ArgumentNullException("assetName");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException("ContentManager");
			}
			T t = default(T);
			string text = assetName.Replace('\\', '/');
			object obj = null;
			if (this.loadedAssets.TryGetValue(text, out obj) && obj is T)
			{
				return (T)((object)obj);
			}
			t = this.ReadAsset<T>(assetName, null);
			this.loadedAssets[text] = t;
			return t;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00037B0C File Offset: 0x00035D0C
		public virtual void Unload()
		{
			foreach (IDisposable disposable in this.disposableAssets)
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			this.disposableAssets.Clear();
			this.loadedAssets.Clear();
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00037B78 File Offset: 0x00035D78
		protected virtual Stream OpenStream(string assetName)
		{
			Stream stream;
			try
			{
				stream = TitleContainer.OpenStream(Path.Combine(this.RootDirectory, assetName) + ".xnb");
			}
			catch (FileNotFoundException ex)
			{
				throw new ContentLoadException("The content file was not found.", ex);
			}
			catch (DirectoryNotFoundException ex2)
			{
				throw new ContentLoadException("The directory was not found.", ex2);
			}
			catch (Exception ex3)
			{
				throw new ContentLoadException("Opening stream error.", ex3);
			}
			return stream;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00037BF4 File Offset: 0x00035DF4
		protected T ReadAsset<T>(string assetName, Action<IDisposable> recordDisposableObject)
		{
			if (string.IsNullOrEmpty(assetName))
			{
				throw new ArgumentNullException("assetName");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException("ContentManager");
			}
			object obj = null;
			Stream stream = null;
			try
			{
				stream = this.OpenStream(assetName);
			}
			catch (Exception ex)
			{
				stream = this.OpenStreamRaw<T>(assetName);
				if (stream == null)
				{
					throw new ContentLoadException("Could not load asset " + assetName + "! Error: " + ex.Message, ex);
				}
			}
			stream.Read(ContentManager.xnbHeader, 0, ContentManager.xnbHeader.Length);
			if (ContentManager.xnbHeader[0] == 88 && ContentManager.xnbHeader[1] == 78 && ContentManager.xnbHeader[2] == 66 && ContentManager.targetPlatformIdentifiers.Contains((char)ContentManager.xnbHeader[3]))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					using (ContentReader contentReaderFromXnb = this.GetContentReaderFromXnb(assetName, ref stream, binaryReader, (char)ContentManager.xnbHeader[3], recordDisposableObject))
					{
						obj = contentReaderFromXnb.ReadAsset<T>();
						GraphicsResource graphicsResource = obj as GraphicsResource;
						if (graphicsResource != null)
						{
							graphicsResource.Name = assetName;
						}
						goto IL_031F;
					}
				}
			}
			stream.Seek(0L, SeekOrigin.Begin);
			if (typeof(T) == typeof(Texture2D) || typeof(T) == typeof(Texture))
			{
				Texture2D texture2D;
				if (ContentManager.xnbHeader[0] == 68 && ContentManager.xnbHeader[1] == 68 && ContentManager.xnbHeader[2] == 83 && ContentManager.xnbHeader[3] == 32)
				{
					texture2D = Texture2D.DDSFromStreamEXT(this.GetGraphicsDevice(), stream);
				}
				else
				{
					texture2D = Texture2D.FromStream(this.GetGraphicsDevice(), stream);
				}
				texture2D.Name = assetName;
				obj = texture2D;
			}
			else if (typeof(T) == typeof(TextureCube))
			{
				TextureCube textureCube = TextureCube.DDSFromStreamEXT(this.GetGraphicsDevice(), stream);
				textureCube.Name = assetName;
				obj = textureCube;
			}
			else if (typeof(T) == typeof(SoundEffect))
			{
				SoundEffect soundEffect = SoundEffect.FromStream(stream);
				soundEffect.Name = assetName;
				obj = soundEffect;
			}
			else if (typeof(T) == typeof(Effect))
			{
				byte[] array = new byte[stream.Length];
				stream.Read(array, 0, (int)stream.Length);
				obj = new Effect(this.GetGraphicsDevice(), array)
				{
					Name = assetName
				};
			}
			else if (typeof(T) == typeof(Song))
			{
				string name = (stream as FileStream).Name;
				stream.Close();
				obj = new Song(name, null);
			}
			else
			{
				if (!(typeof(T) == typeof(Video)))
				{
					stream.Close();
					throw new ContentLoadException("Could not load " + assetName + " asset!");
				}
				string name2 = (stream as FileStream).Name;
				stream.Close();
				obj = new Video(name2, this.GetGraphicsDevice());
				FNALoggerEXT.LogWarn("Video " + name2 + " does not have an XNB file! Hacking Duration property!");
			}
			IDisposable disposable = obj as IDisposable;
			if (disposable != null)
			{
				if (recordDisposableObject != null)
				{
					recordDisposableObject(disposable);
				}
				else
				{
					this.disposableAssets.Add(disposable);
				}
			}
			stream.Close();
			IL_031F:
			return (T)((object)obj);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00037F50 File Offset: 0x00036150
		internal void RecordDisposable(IDisposable disposable)
		{
			if (!this.disposableAssets.Contains(disposable))
			{
				this.disposableAssets.Add(disposable);
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00037F6C File Offset: 0x0003616C
		internal GraphicsDevice GetGraphicsDevice()
		{
			if (this.graphicsDevice == null)
			{
				IGraphicsDeviceService graphicsDeviceService = this.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
				if (graphicsDeviceService == null)
				{
					throw new ContentLoadException("No Graphics Device Service");
				}
				this.graphicsDevice = graphicsDeviceService.GraphicsDevice;
			}
			return this.graphicsDevice;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00037FBC File Offset: 0x000361BC
		private ContentReader GetContentReaderFromXnb(string originalAssetName, ref Stream stream, BinaryReader xnbReader, char platform, Action<IDisposable> recordDisposableObject)
		{
			byte b = xnbReader.ReadByte();
			bool flag = (xnbReader.ReadByte() & 128) > 0;
			if (b != 5 && b != 4)
			{
				throw new ContentLoadException("Invalid XNB version");
			}
			int num = xnbReader.ReadInt32();
			ContentReader contentReader;
			if (flag)
			{
				int num2 = num - 14;
				int num3 = xnbReader.ReadInt32();
				MemoryStream memoryStream = new MemoryStream(new byte[num3], 0, num3, true, true);
				MemoryStream memoryStream2 = new MemoryStream(new byte[num2], 0, num2, true, true);
				stream.Read(memoryStream2.GetBuffer(), 0, num2);
				LzxDecoder lzxDecoder = new LzxDecoder(16);
				int num4 = 0;
				long num5 = 0L;
				while (num5 < (long)num2)
				{
					int num6 = memoryStream2.ReadByte();
					int num7 = memoryStream2.ReadByte();
					int num8 = (num6 << 8) | num7;
					int num9 = 32768;
					if (num6 == 255)
					{
						int num10 = num7;
						num7 = (int)((byte)memoryStream2.ReadByte());
						num9 = (num10 << 8) | num7;
						int num11 = (int)((byte)memoryStream2.ReadByte());
						num7 = (int)((byte)memoryStream2.ReadByte());
						num8 = (num11 << 8) | num7;
						num5 += 5L;
					}
					else
					{
						num5 += 2L;
					}
					if (num8 == 0 || num9 == 0)
					{
						break;
					}
					lzxDecoder.Decompress(memoryStream2, num8, memoryStream, num9);
					num5 += (long)num8;
					num4 += num9;
					memoryStream2.Seek(num5, SeekOrigin.Begin);
				}
				if (memoryStream.Position != (long)num3)
				{
					throw new ContentLoadException("Decompression of " + originalAssetName + " failed. ");
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				contentReader = new ContentReader(this, memoryStream, originalAssetName, (int)b, platform, recordDisposableObject);
			}
			else
			{
				contentReader = new ContentReader(this, stream, originalAssetName, (int)b, platform, recordDisposableObject);
			}
			return contentReader;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00038140 File Offset: 0x00036340
		private Stream CheckRawExtensions(string assetName, string[] extensions)
		{
			string text = FileHelpers.NormalizeFilePathSeparators(Path.Combine(this.RootDirectoryFullPath, assetName));
			if (File.Exists(text))
			{
				return TitleContainer.OpenStream(text);
			}
			foreach (string text2 in extensions)
			{
				string text3 = text + text2;
				if (File.Exists(text3))
				{
					return TitleContainer.OpenStream(text3);
				}
			}
			text = FileHelpers.NormalizeFilePathSeparators(assetName);
			try
			{
				return this.OpenStream(text);
			}
			catch
			{
				foreach (string text4 in extensions)
				{
					string text5 = text + text4;
					try
					{
						return this.OpenStream(text5);
					}
					catch
					{
					}
				}
			}
			return null;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00038200 File Offset: 0x00036400
		private Stream OpenStreamRaw<T>(string assetName)
		{
			if (typeof(T) == typeof(Texture2D) || typeof(T) == typeof(Texture))
			{
				return this.CheckRawExtensions(assetName, ContentManager.texture2DExtensions);
			}
			if (typeof(T) == typeof(TextureCube))
			{
				return this.CheckRawExtensions(assetName, ContentManager.textureCubeExtensions);
			}
			if (typeof(T) == typeof(SoundEffect))
			{
				return this.CheckRawExtensions(assetName, ContentManager.soundEffectExtensions);
			}
			if (typeof(T) == typeof(Effect))
			{
				return this.CheckRawExtensions(assetName, ContentManager.effectExtensions);
			}
			if (typeof(T) == typeof(Song))
			{
				return this.CheckRawExtensions(assetName, SongReader.supportedExtensions);
			}
			if (typeof(T) == typeof(Video))
			{
				return this.CheckRawExtensions(assetName, VideoReader.supportedExtensions);
			}
			return null;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0003831C File Offset: 0x0003651C
		private static void AddContentManager(ContentManager contentManager)
		{
			object contentManagerLock = ContentManager.ContentManagerLock;
			lock (contentManagerLock)
			{
				bool flag2 = false;
				for (int i = ContentManager.ContentManagers.Count - 1; i >= 0; i--)
				{
					WeakReference weakReference = ContentManager.ContentManagers[i];
					if (weakReference.Target == contentManager)
					{
						flag2 = true;
					}
					if (!weakReference.IsAlive)
					{
						ContentManager.ContentManagers.RemoveAt(i);
					}
				}
				if (!flag2)
				{
					ContentManager.ContentManagers.Add(new WeakReference(contentManager));
				}
			}
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x000383AC File Offset: 0x000365AC
		private static void RemoveContentManager(ContentManager contentManager)
		{
			object contentManagerLock = ContentManager.ContentManagerLock;
			lock (contentManagerLock)
			{
				for (int i = ContentManager.ContentManagers.Count - 1; i >= 0; i--)
				{
					WeakReference weakReference = ContentManager.ContentManagers[i];
					if (!weakReference.IsAlive || weakReference.Target == contentManager)
					{
						ContentManager.ContentManagers.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00038424 File Offset: 0x00036624
		// Note: this type is marked as 'beforefieldinit'.
		static ContentManager()
		{
		}

		// Token: 0x04000A9B RID: 2715
		[CompilerGenerated]
		private IServiceProvider <ServiceProvider>k__BackingField;

		// Token: 0x04000A9C RID: 2716
		[CompilerGenerated]
		private string <RootDirectory>k__BackingField;

		// Token: 0x04000A9D RID: 2717
		private GraphicsDevice graphicsDevice;

		// Token: 0x04000A9E RID: 2718
		private Dictionary<string, object> loadedAssets = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000A9F RID: 2719
		private List<IDisposable> disposableAssets = new List<IDisposable>();

		// Token: 0x04000AA0 RID: 2720
		private bool disposed;

		// Token: 0x04000AA1 RID: 2721
		private static object ContentManagerLock = new object();

		// Token: 0x04000AA2 RID: 2722
		private static List<WeakReference> ContentManagers = new List<WeakReference>();

		// Token: 0x04000AA3 RID: 2723
		private static readonly byte[] xnbHeader = new byte[4];

		// Token: 0x04000AA4 RID: 2724
		private static List<char> targetPlatformIdentifiers = new List<char>
		{
			'w', 'x', 'm', 'i', 'a', 'd', 'X', 'W', 'n', 'u',
			'p', 'M', 'r', 'P', 'g', 'l'
		};

		// Token: 0x04000AA5 RID: 2725
		private static readonly string[] effectExtensions = new string[] { ".fxb" };

		// Token: 0x04000AA6 RID: 2726
		private static readonly string[] texture2DExtensions = new string[] { ".png", ".jpg", ".jpeg", ".dds", ".qoi", ".bmp", ".gif", ".tga", ".tif", ".tiff" };

		// Token: 0x04000AA7 RID: 2727
		private static readonly string[] textureCubeExtensions = new string[] { ".dds" };

		// Token: 0x04000AA8 RID: 2728
		private static readonly string[] soundEffectExtensions = new string[] { ".wav" };
	}
}
