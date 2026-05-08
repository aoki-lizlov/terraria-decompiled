using System;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x0200004F RID: 79
	public sealed class Playlist : IEquatable<Playlist>, IDisposable
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
		internal Playlist()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000136EE File Offset: 0x000118EE
		public bool Equals(Playlist other)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x000136EE File Offset: 0x000118EE
		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x000136EE File Offset: 0x000118EE
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0001FBE3 File Offset: 0x0001DDE3
		public static bool operator ==(Playlist first, Playlist second)
		{
			if (first == null)
			{
				return first == second;
			}
			return first.Equals(second);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		public static bool operator !=(Playlist first, Playlist second)
		{
			return !(first == second);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x000136EE File Offset: 0x000118EE
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000136EE File Offset: 0x000118EE
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x000136EE File Offset: 0x000118EE
		public TimeSpan Duration
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x000136EE File Offset: 0x000118EE
		public bool IsDisposed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x000136EE File Offset: 0x000118EE
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000136EE File Offset: 0x000118EE
		public SongCollection Songs
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
