using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Steamworks;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000142 RID: 322
	public class WorkshopSocialModule : WorkshopSocialModule
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x004FF068 File Offset: 0x004FD268
		public override void Initialize()
		{
			base.Branding = new WorkshopBranding
			{
				ResourcePackBrand = ResourcePack.BrandingType.SteamWorkshop
			};
			this._publisherInstances = new List<WorkshopHelper.UGCBased.APublisherInstance>();
			base.ProgressReporter = new WorkshopProgressReporter(this._publisherInstances);
			base.SupportedTags = new SupportedWorkshopTags();
			this._contentBaseFolder = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "Workshop";
			this._downloader = WorkshopHelper.UGCBased.Downloader.Create();
			this._publishedItems = WorkshopHelper.UGCBased.PublishedItemsFinder.Create();
			WorkshopIssueReporter workshopIssueReporter = new WorkshopIssueReporter();
			workshopIssueReporter.OnNeedToOpenUI += this._issueReporter_OnNeedToOpenUI;
			workshopIssueReporter.OnNeedToNotifyUI += this._issueReporter_OnNeedToNotifyUI;
			base.IssueReporter = workshopIssueReporter;
			UIWorkshopHub.OnWorkshopHubMenuOpened += this.RefreshSubscriptionsAndPublishings;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x004FF128 File Offset: 0x004FD328
		private void _issueReporter_OnNeedToNotifyUI()
		{
			Main.IssueReporterIndicator.AttemptLettingPlayerKnow();
			Main.WorkshopPublishingIndicator.Hide();
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x004FF13E File Offset: 0x004FD33E
		private void _issueReporter_OnNeedToOpenUI()
		{
			Main.OpenReportsMenu();
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Shutdown()
		{
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x004FF145 File Offset: 0x004FD345
		public override void LoadEarlyContent()
		{
			this.RefreshSubscriptionsAndPublishings();
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x004FF14D File Offset: 0x004FD34D
		private void RefreshSubscriptionsAndPublishings()
		{
			this._downloader.Refresh(base.IssueReporter);
			this._publishedItems.Refresh();
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x004FF16C File Offset: 0x004FD36C
		public override List<string> GetListOfSubscribedWorldPaths()
		{
			return this._downloader.WorldPaths.Select((string folderPath) => folderPath + Path.DirectorySeparatorChar.ToString() + "world.wld").ToList<string>();
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x004FF1A2 File Offset: 0x004FD3A2
		public override List<string> GetListOfSubscribedResourcePackPaths()
		{
			return this._downloader.ResourcePackPaths;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x004FF1B0 File Offset: 0x004FD3B0
		public override bool TryGetPath(string pathEnd, out string fullPathFound)
		{
			fullPathFound = null;
			string text = this._downloader.ResourcePackPaths.FirstOrDefault((string x) => x.EndsWith(pathEnd));
			if (text == null)
			{
				return false;
			}
			fullPathFound = text;
			return true;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x004FF1F3 File Offset: 0x004FD3F3
		private void Forget(WorkshopHelper.UGCBased.APublisherInstance instance)
		{
			this._publisherInstances.Remove(instance);
			this.RefreshSubscriptionsAndPublishings();
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x004FF208 File Offset: 0x004FD408
		public override void PublishWorld(WorldFileData world, WorkshopItemPublishSettings settings)
		{
			string name = world.Name;
			string textForWorld = this.GetTextForWorld(world);
			string[] usedTagsInternalNames = settings.GetUsedTagsInternalNames();
			string text = this.GetTemporaryFolderPath() + world.GetFileName(false);
			if (!this.MakeTemporaryFolder(text))
			{
				return;
			}
			WorkshopHelper.UGCBased.WorldPublisherInstance worldPublisherInstance = new WorkshopHelper.UGCBased.WorldPublisherInstance(world);
			this._publisherInstances.Add(worldPublisherInstance);
			worldPublisherInstance.PublishContent(this._publishedItems, base.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, textForWorld, text, settings.PreviewImagePath, settings.Publicity, usedTagsInternalNames);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x004FF28C File Offset: 0x004FD48C
		private string GetTextForWorld(WorldFileData world)
		{
			string text = "This is \"";
			text += world.Name;
			int worldSizeX = world.WorldSizeX;
			string text2;
			if (worldSizeX != 4200)
			{
				if (worldSizeX != 6400)
				{
					if (worldSizeX != 8400)
					{
						text2 = "custom";
					}
					else
					{
						text2 = "large";
					}
				}
				else
				{
					text2 = "medium";
				}
			}
			else
			{
				text2 = "small";
			}
			string text3;
			switch (world.GameMode)
			{
			case 0:
				text3 = "classic";
				break;
			case 1:
				text3 = "expert";
				break;
			case 2:
				text3 = "master";
				break;
			case 3:
				text3 = "journey";
				break;
			default:
				text3 = "custom";
				break;
			}
			text = string.Concat(new string[]
			{
				text,
				"\", a ",
				text2.ToLower(),
				" ",
				text3.ToLower(),
				" world"
			});
			text = text + " infected by the " + (world.HasCorruption ? "corruption" : "crimson");
			if (world.IsHardMode)
			{
				text += ", in hardmode";
			}
			return text + ".";
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x004FF3B8 File Offset: 0x004FD5B8
		public override void PublishResourcePack(ResourcePack resourcePack, WorkshopItemPublishSettings settings)
		{
			if (resourcePack.IsCompressed)
			{
				base.IssueReporter.ReportInstantUploadProblem("Workshop.ReportIssue_CannotPublishZips");
				return;
			}
			string name = resourcePack.Name;
			string text = resourcePack.Description;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = "";
			}
			string[] usedTagsInternalNames = settings.GetUsedTagsInternalNames();
			string fullPath = resourcePack.FullPath;
			WorkshopHelper.UGCBased.ResourcePackPublisherInstance resourcePackPublisherInstance = new WorkshopHelper.UGCBased.ResourcePackPublisherInstance(resourcePack);
			this._publisherInstances.Add(resourcePackPublisherInstance);
			resourcePackPublisherInstance.PublishContent(this._publishedItems, base.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, text, fullPath, settings.PreviewImagePath, settings.Publicity, usedTagsInternalNames);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x004FF44C File Offset: 0x004FD64C
		private string GetTemporaryFolderPath()
		{
			ulong steamID = SteamUser.GetSteamID().m_SteamID;
			return this._contentBaseFolder + Path.DirectorySeparatorChar.ToString() + steamID.ToString() + Path.DirectorySeparatorChar.ToString();
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x004FF490 File Offset: 0x004FD690
		private bool MakeTemporaryFolder(string temporaryFolderPath)
		{
			bool flag = true;
			if (!Utils.TryCreatingDirectory(temporaryFolderPath))
			{
				base.IssueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_CouldNotCreateTemporaryFolder!");
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x004FF4BA File Offset: 0x004FD6BA
		public override void ImportDownloadedWorldToLocalSaves(WorldFileData world, string newDisplayName, Action onCompleted)
		{
			Main.menuMode = 10;
			world.CopyToLocal(newDisplayName, onCompleted);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x004FF4CC File Offset: 0x004FD6CC
		public List<IssueReport> GetReports()
		{
			List<IssueReport> list = new List<IssueReport>();
			if (base.IssueReporter != null)
			{
				list.AddRange(base.IssueReporter.GetReports());
			}
			return list;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x004FF4FC File Offset: 0x004FD6FC
		public override bool TryGetInfoForWorld(WorldFileData world, out FoundWorkshopEntryInfo info)
		{
			info = null;
			string text = this.GetTemporaryFolderPath() + world.GetFileName(false);
			return Directory.Exists(text) && AWorkshopEntry.TryReadingManifest(text + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x004FF54C File Offset: 0x004FD74C
		public override bool TryGetInfoForResourcePack(ResourcePack resourcePack, out FoundWorkshopEntryInfo info)
		{
			info = null;
			string fullPath = resourcePack.FullPath;
			return Directory.Exists(fullPath) && AWorkshopEntry.TryReadingManifest(fullPath + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x004FF590 File Offset: 0x004FD790
		public WorkshopSocialModule()
		{
		}

		// Token: 0x040015E3 RID: 5603
		private WorkshopHelper.UGCBased.Downloader _downloader;

		// Token: 0x040015E4 RID: 5604
		private WorkshopHelper.UGCBased.PublishedItemsFinder _publishedItems;

		// Token: 0x040015E5 RID: 5605
		private List<WorkshopHelper.UGCBased.APublisherInstance> _publisherInstances;

		// Token: 0x040015E6 RID: 5606
		private string _contentBaseFolder;

		// Token: 0x02000739 RID: 1849
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060040AC RID: 16556 RVA: 0x0069EC02 File Offset: 0x0069CE02
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060040AD RID: 16557 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060040AE RID: 16558 RVA: 0x0069EC10 File Offset: 0x0069CE10
			internal string <GetListOfSubscribedWorldPaths>b__10_0(string folderPath)
			{
				return folderPath + Path.DirectorySeparatorChar.ToString() + "world.wld";
			}

			// Token: 0x040069B0 RID: 27056
			public static readonly WorkshopSocialModule.<>c <>9 = new WorkshopSocialModule.<>c();

			// Token: 0x040069B1 RID: 27057
			public static Func<string, string> <>9__10_0;
		}

		// Token: 0x0200073A RID: 1850
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x060040AF RID: 16559 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x060040B0 RID: 16560 RVA: 0x0069EC35 File Offset: 0x0069CE35
			internal bool <TryGetPath>b__0(string x)
			{
				return x.EndsWith(this.pathEnd);
			}

			// Token: 0x040069B2 RID: 27058
			public string pathEnd;
		}
	}
}
