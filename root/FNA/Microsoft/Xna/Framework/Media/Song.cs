using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000051 RID: 81
	public sealed class Song : IEquatable<Song>, IDisposable
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0001FC00 File Offset: 0x0001DE00
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x0001FC08 File Offset: 0x0001DE08
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

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0001FC11 File Offset: 0x0001DE11
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x0001FC19 File Offset: 0x0001DE19
		public TimeSpan Duration
		{
			[CompilerGenerated]
			get
			{
				return this.<Duration>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Duration>k__BackingField = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x000136EB File Offset: 0x000118EB
		public bool IsProtected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x000136EB File Offset: 0x000118EB
		public bool IsRated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0001FC22 File Offset: 0x0001DE22
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0001FC2A File Offset: 0x0001DE2A
		public int PlayCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PlayCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PlayCount>k__BackingField = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x000136EB File Offset: 0x000118EB
		public int Rating
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x000136EB File Offset: 0x000118EB
		public int TrackNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0001FC33 File Offset: 0x0001DE33
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0001FC3B File Offset: 0x0001DE3B
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

		// Token: 0x06000F40 RID: 3904 RVA: 0x0001FC44 File Offset: 0x0001DE44
		internal Song(string fileName, string name = null)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException(fileName);
			}
			this.handle = fileName;
			this.Name = name;
			this.IsDisposed = false;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0001FC70 File Offset: 0x0001DE70
		internal Song(string fileName, string assetName, int durationMS)
			: this(fileName, assetName)
		{
			this.Duration = TimeSpan.FromMilliseconds((double)durationMS);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0001FC88 File Offset: 0x0001DE88
		~Song()
		{
			this.Dispose();
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		public void Dispose()
		{
			this.IsDisposed = true;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0001FCBD File Offset: 0x0001DEBD
		public bool Equals(Song song)
		{
			return song != null && this.handle == song.handle;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0001FCD5 File Offset: 0x0001DED5
		public override bool Equals(object obj)
		{
			return obj != null && this.Equals(obj as Song);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0001FCE8 File Offset: 0x0001DEE8
		public static bool operator ==(Song song1, Song song2)
		{
			if (song1 == null)
			{
				return song2 == null;
			}
			return song1.Equals(song2);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0001FCF9 File Offset: 0x0001DEF9
		public static bool operator !=(Song song1, Song song2)
		{
			return !(song1 == song2);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0001FD05 File Offset: 0x0001DF05
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0001FD10 File Offset: 0x0001DF10
		public static Song FromUri(string name, Uri uri)
		{
			string text;
			if (uri.IsAbsoluteUri)
			{
				if (!uri.IsFile)
				{
					throw new InvalidOperationException("Only local file URIs are supported for now");
				}
				text = uri.LocalPath;
			}
			else
			{
				text = Path.Combine(TitleLocation.Path, uri.ToString());
			}
			return new Song(text, name);
		}

		// Token: 0x040005F9 RID: 1529
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040005FA RID: 1530
		[CompilerGenerated]
		private TimeSpan <Duration>k__BackingField;

		// Token: 0x040005FB RID: 1531
		[CompilerGenerated]
		private int <PlayCount>k__BackingField;

		// Token: 0x040005FC RID: 1532
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x040005FD RID: 1533
		internal string handle;
	}
}
