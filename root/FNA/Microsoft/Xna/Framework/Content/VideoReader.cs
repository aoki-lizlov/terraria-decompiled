using System;
using System.IO;
using Microsoft.Xna.Framework.Media;
using MonoGame.Utilities;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200013E RID: 318
	internal class VideoReader : ContentTypeReader<Video>
	{
		// Token: 0x06001793 RID: 6035 RVA: 0x0003A868 File Offset: 0x00038A68
		protected internal override Video Read(ContentReader input, Video existingInstance)
		{
			string text = FileHelpers.ResolveRelativePath(Path.Combine(input.ContentManager.RootDirectoryFullPath, input.AssetName), input.ReadObject<string>());
			string text2 = VideoReader.Normalize(text.Substring(0, text.Length - 4));
			if (!string.IsNullOrEmpty(text2))
			{
				text = text2;
			}
			int num = input.ReadObject<int>();
			int num2 = input.ReadObject<int>();
			int num3 = input.ReadObject<int>();
			float num4 = input.ReadObject<float>();
			VideoSoundtrackType videoSoundtrackType = (VideoSoundtrackType)input.ReadObject<int>();
			return new Video(text, input.ContentManager.GetGraphicsDevice(), num, num2, num3, num4, videoSoundtrackType);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0003A8F8 File Offset: 0x00038AF8
		private static string Normalize(string fileName)
		{
			if (File.Exists(fileName))
			{
				return fileName;
			}
			foreach (string text in VideoReader.supportedExtensions)
			{
				string text2 = fileName + text;
				if (File.Exists(text2))
				{
					return text2;
				}
			}
			return null;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0003A93A File Offset: 0x00038B3A
		public VideoReader()
		{
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0003A942 File Offset: 0x00038B42
		// Note: this type is marked as 'beforefieldinit'.
		static VideoReader()
		{
		}

		// Token: 0x04000ABF RID: 2751
		internal static readonly string[] supportedExtensions = new string[] { ".ogv", ".ogg" };
	}
}
