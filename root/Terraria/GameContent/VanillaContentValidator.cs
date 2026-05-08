using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent
{
	// Token: 0x02000270 RID: 624
	public class VanillaContentValidator : IContentValidator
	{
		// Token: 0x0600241D RID: 9245 RVA: 0x0054A8EC File Offset: 0x00548AEC
		public VanillaContentValidator(string infoFilePath)
		{
			foreach (string text in Regex.Split(Utils.ReadEmbeddedResource(infoFilePath), "\r\n|\r|\n"))
			{
				if (!text.StartsWith("//"))
				{
					string[] array2 = text.Split(new char[] { '\t' });
					int num;
					int num2;
					if (array2.Length >= 3 && int.TryParse(array2[1], out num) && int.TryParse(array2[2], out num2))
					{
						string text2 = array2[0].Replace('/', '\\');
						this._info[text2] = new VanillaContentValidator.TextureMetaData
						{
							Width = num,
							Height = num2
						};
					}
				}
			}
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x0054A9A4 File Offset: 0x00548BA4
		public bool AssetIsValid<T>(T content, string contentPath, out IRejectionReason rejectReason) where T : class
		{
			Texture2D texture2D = content as Texture2D;
			rejectReason = null;
			VanillaContentValidator.TextureMetaData textureMetaData;
			return texture2D == null || !this._info.TryGetValue(contentPath, out textureMetaData) || textureMetaData.Matches(texture2D, out rejectReason);
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0054A9E0 File Offset: 0x00548BE0
		public HashSet<string> GetValidImageFilePaths()
		{
			return new HashSet<string>(this._info.Select((KeyValuePair<string, VanillaContentValidator.TextureMetaData> x) => x.Key));
		}

		// Token: 0x04004DD5 RID: 19925
		public static VanillaContentValidator Instance;

		// Token: 0x04004DD6 RID: 19926
		private Dictionary<string, VanillaContentValidator.TextureMetaData> _info = new Dictionary<string, VanillaContentValidator.TextureMetaData>();

		// Token: 0x020007F2 RID: 2034
		private struct TextureMetaData
		{
			// Token: 0x06004292 RID: 17042 RVA: 0x006BF18C File Offset: 0x006BD38C
			public bool Matches(Texture2D texture, out IRejectionReason rejectReason)
			{
				if (texture.Width != this.Width || texture.Height != this.Height)
				{
					rejectReason = new ContentRejectionFromSize(this.Width, this.Height, texture.Width, texture.Height);
					return false;
				}
				rejectReason = null;
				return true;
			}

			// Token: 0x04007175 RID: 29045
			public int Width;

			// Token: 0x04007176 RID: 29046
			public int Height;
		}

		// Token: 0x020007F3 RID: 2035
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004293 RID: 17043 RVA: 0x006BF1DA File Offset: 0x006BD3DA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004294 RID: 17044 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004295 RID: 17045 RVA: 0x006BF1E6 File Offset: 0x006BD3E6
			internal string <GetValidImageFilePaths>b__4_0(KeyValuePair<string, VanillaContentValidator.TextureMetaData> x)
			{
				return x.Key;
			}

			// Token: 0x04007177 RID: 29047
			public static readonly VanillaContentValidator.<>c <>9 = new VanillaContentValidator.<>c();

			// Token: 0x04007178 RID: 29048
			public static Func<KeyValuePair<string, VanillaContentValidator.TextureMetaData>, string> <>9__4_0;
		}
	}
}
