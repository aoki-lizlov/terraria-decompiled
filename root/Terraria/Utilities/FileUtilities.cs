using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using ReLogic.OS;
using Terraria.Social;

namespace Terraria.Utilities
{
	// Token: 0x020000D8 RID: 216
	public static class FileUtilities
	{
		// Token: 0x06001872 RID: 6258 RVA: 0x004E28CA File Offset: 0x004E0ACA
		public static bool Exists(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.HasFile(path);
			}
			return File.Exists(path);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x004E28E8 File Offset: 0x004E0AE8
		public static void Delete(string path, bool cloud, bool forceDeleteFile = false)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				SocialAPI.Cloud.Delete(path);
				return;
			}
			if (forceDeleteFile)
			{
				File.Delete(path);
				return;
			}
			Platform.Get<IPathService>().MoveToRecycleBin(path);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x004E2916 File Offset: 0x004E0B16
		public static string GetFullPath(string path, bool cloud)
		{
			if (!cloud)
			{
				return Path.GetFullPath(path);
			}
			return path;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x004E2924 File Offset: 0x004E0B24
		public static void Copy(string source, string destination, bool cloud)
		{
			if (!cloud)
			{
				try
				{
					File.Copy(source, destination, true);
				}
				catch (IOException ex)
				{
					if (ex.GetType() != typeof(IOException))
					{
						throw;
					}
					using (FileStream fileStream = File.OpenRead(source))
					{
						using (FileStream fileStream2 = File.Create(destination))
						{
							fileStream.CopyTo(fileStream2);
						}
					}
				}
				return;
			}
			if (SocialAPI.Cloud == null)
			{
				return;
			}
			SocialAPI.Cloud.Write(destination, SocialAPI.Cloud.Read(source));
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x004E29D0 File Offset: 0x004E0BD0
		public static void Move(string source, string destination, bool cloud)
		{
			if (!cloud)
			{
				try
				{
					if (File.Exists(destination))
					{
						File.Delete(destination);
					}
					File.Move(source, destination);
					return;
				}
				catch (IOException)
				{
				}
			}
			FileUtilities.Copy(source, destination, cloud);
			FileUtilities.Delete(source, cloud, true);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x004E2A1C File Offset: 0x004E0C1C
		public static int GetFileSize(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.GetFileSize(path);
			}
			return (int)new FileInfo(path).Length;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x004E2A40 File Offset: 0x004E0C40
		public static void Read(string path, byte[] buffer, int length, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				SocialAPI.Cloud.Read(path, buffer, length);
				return;
			}
			using (FileStream fileStream = File.OpenRead(path))
			{
				fileStream.Read(buffer, 0, length);
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x004E2A94 File Offset: 0x004E0C94
		public static byte[] ReadAllBytes(string path, bool cloud)
		{
			if (cloud && SocialAPI.Cloud != null)
			{
				return SocialAPI.Cloud.Read(path);
			}
			return File.ReadAllBytes(path);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x004E2AB2 File Offset: 0x004E0CB2
		public static bool WriteAllBytes(string path, byte[] data, bool cloud)
		{
			return FileUtilities.Write(path, data, data.Length, cloud);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x004E2AC0 File Offset: 0x004E0CC0
		public static bool Write(string path, byte[] data, int length, bool cloud)
		{
			if (cloud)
			{
				return SocialAPI.Cloud != null && SocialAPI.Cloud.Write(path, data, length);
			}
			string parentFolderPath = FileUtilities.GetParentFolderPath(path, true);
			if (parentFolderPath != "")
			{
				Utils.TryCreatingDirectory(parentFolderPath);
			}
			FileUtilities.RemoveReadOnlyAttribute(path);
			using (FileStream fileStream = File.Open(path, FileMode.Create))
			{
				while (fileStream.Position < (long)length)
				{
					fileStream.Write(data, (int)fileStream.Position, Math.Min(length - (int)fileStream.Position, 2048));
				}
			}
			return true;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x004E2B5C File Offset: 0x004E0D5C
		public static void RemoveReadOnlyAttribute(string path)
		{
			if (!File.Exists(path))
			{
				return;
			}
			try
			{
				FileAttributes fileAttributes = File.GetAttributes(path);
				if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				{
					fileAttributes &= ~FileAttributes.ReadOnly;
					File.SetAttributes(path, fileAttributes);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x004E2BA4 File Offset: 0x004E0DA4
		public static bool MoveToCloud(string localPath, string cloudPath)
		{
			if (SocialAPI.Cloud == null)
			{
				return false;
			}
			bool flag = FileUtilities.WriteAllBytes(cloudPath, FileUtilities.ReadAllBytes(localPath, false), true);
			if (flag)
			{
				FileUtilities.Delete(localPath, false, false);
			}
			return flag;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x004E2BC8 File Offset: 0x004E0DC8
		public static bool MoveToLocal(string cloudPath, string localPath)
		{
			if (SocialAPI.Cloud == null)
			{
				return false;
			}
			if (FileUtilities.WriteAllBytes(localPath, FileUtilities.ReadAllBytes(cloudPath, true), false))
			{
				FileUtilities.Delete(cloudPath, true, false);
			}
			return true;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x004E2BEC File Offset: 0x004E0DEC
		public static bool CopyToLocal(string cloudPath, string localPath)
		{
			if (SocialAPI.Cloud == null)
			{
				return false;
			}
			FileUtilities.WriteAllBytes(localPath, FileUtilities.ReadAllBytes(cloudPath, true), false);
			return true;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x004E2C08 File Offset: 0x004E0E08
		public static string GetFileName(string path, bool includeExtension = true)
		{
			Match match = FileUtilities.FileNameRegex.Match(path);
			if (match == null || match.Groups["fileName"] == null)
			{
				return "";
			}
			includeExtension &= match.Groups["extension"] != null;
			return match.Groups["fileName"].Value + (includeExtension ? match.Groups["extension"].Value : "");
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x004E2C8C File Offset: 0x004E0E8C
		public static string GetParentFolderPath(string path, bool includeExtension = true)
		{
			Match match = FileUtilities.FileNameRegex.Match(path);
			if (match == null || match.Groups["path"] == null)
			{
				return "";
			}
			return match.Groups["path"].Value;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x004E2CD8 File Offset: 0x004E0ED8
		public static void CopyFolder(string sourcePath, string destinationPath)
		{
			Directory.CreateDirectory(destinationPath);
			string[] array = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
			for (int i = 0; i < array.Length; i++)
			{
				Directory.CreateDirectory(array[i].Replace(sourcePath, destinationPath));
			}
			foreach (string text in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
			{
				File.Copy(text, text.Replace(sourcePath, destinationPath), true);
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x004E2D44 File Offset: 0x004E0F44
		public static void ProtectedInvoke(Action action)
		{
			bool isBackground = Thread.CurrentThread.IsBackground;
			try
			{
				Thread.CurrentThread.IsBackground = false;
				action();
			}
			finally
			{
				Thread.CurrentThread.IsBackground = isBackground;
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x004E2D8C File Offset: 0x004E0F8C
		// Note: this type is marked as 'beforefieldinit'.
		static FileUtilities()
		{
		}

		// Token: 0x040012DC RID: 4828
		private static Regex FileNameRegex = new Regex("^(?<path>.*[\\\\\\/])?(?:$|(?<fileName>.+?)(?:(?<extension>\\.[^.]*$)|$))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
