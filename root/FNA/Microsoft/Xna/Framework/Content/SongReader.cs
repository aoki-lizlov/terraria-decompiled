using System;
using System.IO;
using Microsoft.Xna.Framework.Media;
using MonoGame.Utilities;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200012D RID: 301
	internal class SongReader : ContentTypeReader<Song>
	{
		// Token: 0x0600176D RID: 5997 RVA: 0x00039F7C File Offset: 0x0003817C
		protected internal override Song Read(ContentReader input, Song existingInstance)
		{
			string text = FileHelpers.ResolveRelativePath(Path.Combine(input.ContentManager.RootDirectoryFullPath, input.AssetName), input.ReadString());
			string text2 = SongReader.Normalize(text.Substring(0, text.Length - 4));
			if (!string.IsNullOrEmpty(text2))
			{
				text = text2;
			}
			int num = input.ReadInt32();
			return new Song(text, input.AssetName, num);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00039FE0 File Offset: 0x000381E0
		private static string Normalize(string fileName)
		{
			if (File.Exists(fileName))
			{
				return fileName;
			}
			foreach (string text in SongReader.supportedExtensions)
			{
				string text2 = fileName + text;
				if (File.Exists(text2))
				{
					return text2;
				}
			}
			return null;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0003A022 File Offset: 0x00038222
		public SongReader()
		{
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0003A02A File Offset: 0x0003822A
		// Note: this type is marked as 'beforefieldinit'.
		static SongReader()
		{
		}

		// Token: 0x04000ABE RID: 2750
		internal static readonly string[] supportedExtensions = new string[] { ".ogg", ".oga", ".qoa" };
	}
}
