using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x0200006E RID: 110
	public class FavoritesFile
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x004BC3BF File Offset: 0x004BA5BF
		public FavoritesFile(string path, bool isCloud)
		{
			this.Path = path;
			this.IsCloudSave = isCloud;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x004BC3F0 File Offset: 0x004BA5F0
		public void SaveFavorite(FileData fileData)
		{
			if (!this._data.ContainsKey(fileData.Type))
			{
				this._data.Add(fileData.Type, new Dictionary<string, bool>());
			}
			this._data[fileData.Type][fileData.GetFileName(true)] = fileData.IsFavorite;
			this.Save();
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x004BC44F File Offset: 0x004BA64F
		public void ClearEntry(FileData fileData)
		{
			if (!this._data.ContainsKey(fileData.Type))
			{
				return;
			}
			this._data[fileData.Type].Remove(fileData.GetFileName(true));
			this.Save();
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x004BC48C File Offset: 0x004BA68C
		public bool IsFavorite(FileData fileData)
		{
			if (!this._data.ContainsKey(fileData.Type))
			{
				return false;
			}
			string fileName = fileData.GetFileName(true);
			bool flag;
			return this._data[fileData.Type].TryGetValue(fileName, out flag) && flag;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x004BC4D4 File Offset: 0x004BA6D4
		public void Save()
		{
			try
			{
				string text = JsonConvert.SerializeObject(this._data, 1);
				byte[] bytes = this._ourEncoder.GetBytes(text);
				FileUtilities.WriteAllBytes(this.Path, bytes, this.IsCloudSave);
			}
			catch (Exception ex)
			{
				FancyErrorPrinter.ShowFileSavingFailError(ex, this.Path);
				throw;
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x004BC530 File Offset: 0x004BA730
		public void Load()
		{
			if (!FileUtilities.Exists(this.Path, this.IsCloudSave))
			{
				this._data.Clear();
				return;
			}
			try
			{
				byte[] array = FileUtilities.ReadAllBytes(this.Path, this.IsCloudSave);
				string text;
				try
				{
					text = this._ourEncoder.GetString(array);
				}
				catch
				{
					text = Encoding.ASCII.GetString(array);
				}
				this._data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, bool>>>(text);
				if (this._data == null)
				{
					this._data = new Dictionary<string, Dictionary<string, bool>>();
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Unable to load favorites.json file ({0} : {1})", this.Path, this.IsCloudSave ? "Cloud Save" : "Local Save");
			}
		}

		// Token: 0x04001095 RID: 4245
		public readonly string Path;

		// Token: 0x04001096 RID: 4246
		public readonly bool IsCloudSave;

		// Token: 0x04001097 RID: 4247
		private Dictionary<string, Dictionary<string, bool>> _data = new Dictionary<string, Dictionary<string, bool>>();

		// Token: 0x04001098 RID: 4248
		private UTF8Encoding _ourEncoder = new UTF8Encoding(true, true);
	}
}
