using System;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x0200006F RID: 111
	public abstract class FileData
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x004BC5F4 File Offset: 0x004BA7F4
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x004BC5FC File Offset: 0x004BA7FC
		public bool IsCloudSave
		{
			get
			{
				return this._isCloudSave;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x004BC604 File Offset: 0x004BA804
		public bool IsFavorite
		{
			get
			{
				return this._isFavorite;
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x004BC60C File Offset: 0x004BA80C
		protected FileData(string type)
		{
			this.Type = type;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x004BC61B File Offset: 0x004BA81B
		protected FileData(string type, string path, bool isCloud)
		{
			this.Type = type;
			this._path = path;
			this._isCloudSave = isCloud;
			this._isFavorite = (isCloud ? Main.CloudFavoritesData : Main.LocalFavoriteData).IsFavorite(this);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x004BC653 File Offset: 0x004BA853
		public void ToggleFavorite()
		{
			this.SetFavorite(!this.IsFavorite, true);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x004BC665 File Offset: 0x004BA865
		public string GetFileName(bool includeExtension = true)
		{
			return FileUtilities.GetFileName(this.Path, includeExtension);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x004BC673 File Offset: 0x004BA873
		public void SetFavorite(bool favorite, bool saveChanges = true)
		{
			this._isFavorite = favorite;
			if (saveChanges)
			{
				(this.IsCloudSave ? Main.CloudFavoritesData : Main.LocalFavoriteData).SaveFavorite(this);
			}
		}

		// Token: 0x060014D1 RID: 5329
		public abstract void SetAsActive();

		// Token: 0x060014D2 RID: 5330
		public abstract void MoveToCloud();

		// Token: 0x060014D3 RID: 5331
		public abstract void MoveToLocal();

		// Token: 0x04001099 RID: 4249
		protected string _path;

		// Token: 0x0400109A RID: 4250
		protected bool _isCloudSave;

		// Token: 0x0400109B RID: 4251
		public FileMetadata Metadata;

		// Token: 0x0400109C RID: 4252
		public string Name;

		// Token: 0x0400109D RID: 4253
		public readonly string Type;

		// Token: 0x0400109E RID: 4254
		protected bool _isFavorite;
	}
}
