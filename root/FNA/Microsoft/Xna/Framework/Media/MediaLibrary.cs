using System;
using System.IO;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000045 RID: 69
	public sealed class MediaLibrary : IDisposable
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x000136F5 File Offset: 0x000118F5
		public MediaLibrary()
		{
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
		public MediaLibrary(MediaSource mediaSource)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x000136EE File Offset: 0x000118EE
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x000136EE File Offset: 0x000118EE
		public Picture GetPictureFromToken(string token)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000136EE File Offset: 0x000118EE
		public Picture SavePicture(string name, byte[] imageBuffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000136EE File Offset: 0x000118EE
		public Picture SavePicture(string name, Stream source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000136EE File Offset: 0x000118EE
		public AlbumCollection Albums
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000136EE File Offset: 0x000118EE
		public ArtistCollection Artists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000136EE File Offset: 0x000118EE
		public GenreCollection Genres
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x000136EE File Offset: 0x000118EE
		public bool IsDisposed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x000136EE File Offset: 0x000118EE
		public MediaSource MediaSource
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x000136EE File Offset: 0x000118EE
		public PictureCollection Pictures
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x000136EE File Offset: 0x000118EE
		public PlaylistCollection Playlists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x000136EE File Offset: 0x000118EE
		public PictureAlbum RootPictureAlbum
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000136EE File Offset: 0x000118EE
		public PictureCollection SavedPictures
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x000136EE File Offset: 0x000118EE
		public SongCollection Songs
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
