using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Steamworks;
using Terraria.IO;
using Terraria.Social.Base;
using Terraria.Utilities;

namespace Terraria.Social.Steam
{
	// Token: 0x02000140 RID: 320
	public class WorkshopHelper
	{
		// Token: 0x06001C8F RID: 7311 RVA: 0x0000357B File Offset: 0x0000177B
		public WorkshopHelper()
		{
		}

		// Token: 0x02000738 RID: 1848
		public class UGCBased
		{
			// Token: 0x060040AB RID: 16555 RVA: 0x0000357B File Offset: 0x0000177B
			public UGCBased()
			{
			}

			// Token: 0x040069AF RID: 27055
			public const string ManifestFileName = "workshop.json";

			// Token: 0x02000A8A RID: 2698
			public struct SteamWorkshopItem
			{
				// Token: 0x0400776A RID: 30570
				public string ContentFolderPath;

				// Token: 0x0400776B RID: 30571
				public string Description;

				// Token: 0x0400776C RID: 30572
				public string PreviewImagePath;

				// Token: 0x0400776D RID: 30573
				public string[] Tags;

				// Token: 0x0400776E RID: 30574
				public string Title;

				// Token: 0x0400776F RID: 30575
				public ERemoteStoragePublishedFileVisibility? Visibility;
			}

			// Token: 0x02000A8B RID: 2699
			public class Downloader
			{
				// Token: 0x170005BC RID: 1468
				// (get) Token: 0x06004BAC RID: 19372 RVA: 0x006D8C74 File Offset: 0x006D6E74
				// (set) Token: 0x06004BAD RID: 19373 RVA: 0x006D8C7C File Offset: 0x006D6E7C
				public List<string> ResourcePackPaths
				{
					[CompilerGenerated]
					get
					{
						return this.<ResourcePackPaths>k__BackingField;
					}
					[CompilerGenerated]
					private set
					{
						this.<ResourcePackPaths>k__BackingField = value;
					}
				}

				// Token: 0x170005BD RID: 1469
				// (get) Token: 0x06004BAE RID: 19374 RVA: 0x006D8C85 File Offset: 0x006D6E85
				// (set) Token: 0x06004BAF RID: 19375 RVA: 0x006D8C8D File Offset: 0x006D6E8D
				public List<string> WorldPaths
				{
					[CompilerGenerated]
					get
					{
						return this.<WorldPaths>k__BackingField;
					}
					[CompilerGenerated]
					private set
					{
						this.<WorldPaths>k__BackingField = value;
					}
				}

				// Token: 0x06004BB0 RID: 19376 RVA: 0x006D8C96 File Offset: 0x006D6E96
				public Downloader()
				{
					this.ResourcePackPaths = new List<string>();
					this.WorldPaths = new List<string>();
				}

				// Token: 0x06004BB1 RID: 19377 RVA: 0x006D8CB4 File Offset: 0x006D6EB4
				public static WorkshopHelper.UGCBased.Downloader Create()
				{
					return new WorkshopHelper.UGCBased.Downloader();
				}

				// Token: 0x06004BB2 RID: 19378 RVA: 0x006D8CBC File Offset: 0x006D6EBC
				public List<string> GetListOfSubscribedItemsPaths()
				{
					PublishedFileId_t[] array = new PublishedFileId_t[SteamUGC.GetNumSubscribedItems()];
					SteamUGC.GetSubscribedItems(array, (uint)array.Length);
					ulong num = 0UL;
					string empty = string.Empty;
					uint num2 = 0U;
					List<string> list = new List<string>();
					PublishedFileId_t[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						if (SteamUGC.GetItemInstallInfo(array2[i], ref num, ref empty, 1024U, ref num2))
						{
							list.Add(empty);
						}
					}
					return list;
				}

				// Token: 0x06004BB3 RID: 19379 RVA: 0x006D8D26 File Offset: 0x006D6F26
				public bool Prepare(WorkshopIssueReporter issueReporter)
				{
					return this.Refresh(issueReporter);
				}

				// Token: 0x06004BB4 RID: 19380 RVA: 0x006D8D30 File Offset: 0x006D6F30
				public bool Refresh(WorkshopIssueReporter issueReporter)
				{
					this.ResourcePackPaths.Clear();
					this.WorldPaths.Clear();
					foreach (string text in this.GetListOfSubscribedItemsPaths())
					{
						if (text != null)
						{
							try
							{
								string text2 = text + Path.DirectorySeparatorChar.ToString() + "workshop.json";
								if (File.Exists(text2))
								{
									string text3 = AWorkshopEntry.ReadHeader(File.ReadAllText(text2));
									if (!(text3 == "World"))
									{
										if (text3 == "ResourcePack")
										{
											this.ResourcePackPaths.Add(text);
										}
									}
									else
									{
										this.WorldPaths.Add(text);
									}
								}
							}
							catch (Exception ex)
							{
								issueReporter.ReportDownloadProblem("Workshop.ReportIssue_FailedToLoadSubscribedFile", text, ex);
								return false;
							}
						}
					}
					return true;
				}

				// Token: 0x04007770 RID: 30576
				[CompilerGenerated]
				private List<string> <ResourcePackPaths>k__BackingField;

				// Token: 0x04007771 RID: 30577
				[CompilerGenerated]
				private List<string> <WorldPaths>k__BackingField;
			}

			// Token: 0x02000A8C RID: 2700
			public class PublishedItemsFinder
			{
				// Token: 0x06004BB5 RID: 19381 RVA: 0x006D8E2C File Offset: 0x006D702C
				public bool HasItemOfId(ulong id)
				{
					return this._items.ContainsKey(id);
				}

				// Token: 0x06004BB6 RID: 19382 RVA: 0x006D8E3A File Offset: 0x006D703A
				public static WorkshopHelper.UGCBased.PublishedItemsFinder Create()
				{
					WorkshopHelper.UGCBased.PublishedItemsFinder publishedItemsFinder = new WorkshopHelper.UGCBased.PublishedItemsFinder();
					publishedItemsFinder.LoadHooks();
					return publishedItemsFinder;
				}

				// Token: 0x06004BB7 RID: 19383 RVA: 0x006D8E47 File Offset: 0x006D7047
				private void LoadHooks()
				{
					this.OnSteamUGCQueryCompletedCallResult = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCQueryCompleted));
					this.OnSteamUGCRequestUGCDetailsResultCallResult = CallResult<SteamUGCRequestUGCDetailsResult_t>.Create(new CallResult<SteamUGCRequestUGCDetailsResult_t>.APIDispatchDelegate(this.OnSteamUGCRequestUGCDetailsResult));
				}

				// Token: 0x06004BB8 RID: 19384 RVA: 0x006D8E77 File Offset: 0x006D7077
				public void Prepare()
				{
					this.Refresh();
				}

				// Token: 0x06004BB9 RID: 19385 RVA: 0x006D8E80 File Offset: 0x006D7080
				public void Refresh()
				{
					this.m_UGCQueryHandle = SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(), 0, -1, 0, SteamUtils.GetAppID(), SteamUtils.GetAppID(), 1U);
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t steamAPICall_t = SteamUGC.SendQueryUGCRequest(this.m_UGCQueryHandle);
					this.OnSteamUGCQueryCompletedCallResult.Set(steamAPICall_t, new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCQueryCompleted));
					CoreSocialModule.SetSkipPulsing(false);
				}

				// Token: 0x06004BBA RID: 19386 RVA: 0x006D8EE4 File Offset: 0x006D70E4
				private void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
				{
					this._items.Clear();
					if (bIOFailure || pCallback.m_eResult != 1)
					{
						SteamUGC.ReleaseQueryUGCRequest(this.m_UGCQueryHandle);
						return;
					}
					for (uint num = 0U; num < pCallback.m_unNumResultsReturned; num += 1U)
					{
						SteamUGCDetails_t steamUGCDetails_t;
						SteamUGC.GetQueryUGCResult(this.m_UGCQueryHandle, num, ref steamUGCDetails_t);
						ulong publishedFileId = steamUGCDetails_t.m_nPublishedFileId.m_PublishedFileId;
						WorkshopHelper.UGCBased.SteamWorkshopItem steamWorkshopItem = new WorkshopHelper.UGCBased.SteamWorkshopItem
						{
							Title = steamUGCDetails_t.m_rgchTitle,
							Description = steamUGCDetails_t.m_rgchDescription
						};
						this._items.Add(publishedFileId, steamWorkshopItem);
					}
					SteamUGC.ReleaseQueryUGCRequest(this.m_UGCQueryHandle);
				}

				// Token: 0x06004BBB RID: 19387 RVA: 0x00009E46 File Offset: 0x00008046
				private void OnSteamUGCRequestUGCDetailsResult(SteamUGCRequestUGCDetailsResult_t pCallback, bool bIOFailure)
				{
				}

				// Token: 0x06004BBC RID: 19388 RVA: 0x006D8F89 File Offset: 0x006D7189
				public PublishedItemsFinder()
				{
				}

				// Token: 0x04007772 RID: 30578
				private Dictionary<ulong, WorkshopHelper.UGCBased.SteamWorkshopItem> _items = new Dictionary<ulong, WorkshopHelper.UGCBased.SteamWorkshopItem>();

				// Token: 0x04007773 RID: 30579
				private UGCQueryHandle_t m_UGCQueryHandle;

				// Token: 0x04007774 RID: 30580
				private CallResult<SteamUGCQueryCompleted_t> OnSteamUGCQueryCompletedCallResult;

				// Token: 0x04007775 RID: 30581
				private CallResult<SteamUGCRequestUGCDetailsResult_t> OnSteamUGCRequestUGCDetailsResultCallResult;
			}

			// Token: 0x02000A8D RID: 2701
			public abstract class APublisherInstance
			{
				// Token: 0x06004BBD RID: 19389 RVA: 0x006D8F9C File Offset: 0x006D719C
				public void PublishContent(WorkshopHelper.UGCBased.PublishedItemsFinder finder, WorkshopIssueReporter issueReporter, WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction endAction, string itemTitle, string itemDescription, string contentFolderPath, string previewImagePath, WorkshopItemPublicSettingId publicity, string[] tags)
				{
					this._issueReporter = issueReporter;
					this._endAction = endAction;
					this._createItemHook = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.CreateItemResult));
					this._updateItemHook = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.UpdateItemResult));
					ERemoteStoragePublishedFileVisibility visibility = this.GetVisibility(publicity);
					this._entryData = new WorkshopHelper.UGCBased.SteamWorkshopItem
					{
						Title = itemTitle,
						Description = itemDescription,
						ContentFolderPath = contentFolderPath,
						Tags = tags,
						PreviewImagePath = previewImagePath,
						Visibility = new ERemoteStoragePublishedFileVisibility?(visibility)
					};
					ulong? num = null;
					FoundWorkshopEntryInfo foundWorkshopEntryInfo;
					if (AWorkshopEntry.TryReadingManifest(contentFolderPath + Path.DirectorySeparatorChar.ToString() + "workshop.json", out foundWorkshopEntryInfo))
					{
						num = new ulong?(foundWorkshopEntryInfo.workshopEntryId);
					}
					if (num != null && finder.HasItemOfId(num.Value))
					{
						this._publishedFileID = new PublishedFileId_t(num.Value);
						this.PreventUpdatingCertainThings();
						this.UpdateItem();
						return;
					}
					this.CreateItem();
				}

				// Token: 0x06004BBE RID: 19390 RVA: 0x006D90AF File Offset: 0x006D72AF
				private void PreventUpdatingCertainThings()
				{
					this._entryData.Title = null;
					this._entryData.Description = null;
				}

				// Token: 0x06004BBF RID: 19391 RVA: 0x006D90C9 File Offset: 0x006D72C9
				private ERemoteStoragePublishedFileVisibility GetVisibility(WorkshopItemPublicSettingId publicityId)
				{
					switch (publicityId)
					{
					default:
						return 2;
					case WorkshopItemPublicSettingId.FriendsOnly:
						return 1;
					case WorkshopItemPublicSettingId.Public:
						return 0;
					}
				}

				// Token: 0x06004BC0 RID: 19392 RVA: 0x006D90E4 File Offset: 0x006D72E4
				private void CreateItem()
				{
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t steamAPICall_t = SteamUGC.CreateItem(SteamUtils.GetAppID(), 0);
					this._createItemHook.Set(steamAPICall_t, new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.CreateItemResult));
					CoreSocialModule.SetSkipPulsing(false);
				}

				// Token: 0x06004BC1 RID: 19393 RVA: 0x006D9124 File Offset: 0x006D7324
				private void CreateItemResult(CreateItemResult_t param, bool bIOFailure)
				{
					if (param.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_UserDidNotAcceptWorkshopTermsOfService");
						this._endAction(this);
						return;
					}
					if (param.m_eResult == 1)
					{
						this._publishedFileID = param.m_nPublishedFileId;
						this.UpdateItem();
						return;
					}
					this._issueReporter.ReportDelayedUploadProblemWithoutKnownReason("Workshop.ReportIssue_FailedToPublish_WithoutKnownReason", param.m_eResult.ToString());
					this._endAction(this);
				}

				// Token: 0x06004BC2 RID: 19394
				protected abstract string GetHeaderText();

				// Token: 0x06004BC3 RID: 19395
				protected abstract void PrepareContentForUpdate();

				// Token: 0x06004BC4 RID: 19396 RVA: 0x006D91A0 File Offset: 0x006D73A0
				private void UpdateItem()
				{
					string headerText = this.GetHeaderText();
					if (!this.TryWritingManifestToFolder(this._entryData.ContentFolderPath, headerText))
					{
						this._endAction(this);
						return;
					}
					this.PrepareContentForUpdate();
					UGCUpdateHandle_t ugcupdateHandle_t = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), this._publishedFileID);
					if (this._entryData.Title != null)
					{
						SteamUGC.SetItemTitle(ugcupdateHandle_t, this._entryData.Title);
					}
					if (this._entryData.Description != null)
					{
						SteamUGC.SetItemDescription(ugcupdateHandle_t, this._entryData.Description);
					}
					SteamUGC.SetItemContent(ugcupdateHandle_t, this._entryData.ContentFolderPath);
					SteamUGC.SetItemTags(ugcupdateHandle_t, this._entryData.Tags, false);
					if (this._entryData.PreviewImagePath != null)
					{
						SteamUGC.SetItemPreview(ugcupdateHandle_t, this._entryData.PreviewImagePath);
					}
					if (this._entryData.Visibility != null)
					{
						SteamUGC.SetItemVisibility(ugcupdateHandle_t, this._entryData.Visibility.Value);
					}
					CoreSocialModule.SetSkipPulsing(true);
					SteamAPICall_t steamAPICall_t = SteamUGC.SubmitItemUpdate(ugcupdateHandle_t, "");
					this._updateHandle = ugcupdateHandle_t;
					this._updateItemHook.Set(steamAPICall_t, new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.UpdateItemResult));
					CoreSocialModule.SetSkipPulsing(false);
				}

				// Token: 0x06004BC5 RID: 19397 RVA: 0x006D92D0 File Offset: 0x006D74D0
				private void UpdateItemResult(SubmitItemUpdateResult_t param, bool bIOFailure)
				{
					if (param.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_UserDidNotAcceptWorkshopTermsOfService");
						this._endAction(this);
						return;
					}
					EResult eResult = param.m_eResult;
					if (eResult <= 9)
					{
						if (eResult == 1)
						{
							SteamFriends.ActivateGameOverlayToWebPage("steam://url/CommunityFilePage/" + this._publishedFileID.m_PublishedFileId, 0);
							goto IL_00F5;
						}
						if (eResult == 8)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_InvalidParametersForPublishing");
							goto IL_00F5;
						}
						if (eResult == 9)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_CouldNotFindFolderToUpload");
							goto IL_00F5;
						}
					}
					else
					{
						if (eResult == 15)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_AccessDeniedBecauseUserDoesntOwnLicenseForApp");
							goto IL_00F5;
						}
						if (eResult == 25)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_LimitExceeded");
							goto IL_00F5;
						}
						if (eResult == 33)
						{
							this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_SteamFileLockFailed");
							goto IL_00F5;
						}
					}
					this._issueReporter.ReportDelayedUploadProblemWithoutKnownReason("Workshop.ReportIssue_FailedToPublish_WithoutKnownReason", param.m_eResult.ToString());
					IL_00F5:
					this._endAction(this);
				}

				// Token: 0x06004BC6 RID: 19398 RVA: 0x006D93E0 File Offset: 0x006D75E0
				private bool TryWritingManifestToFolder(string folderPath, string manifestText)
				{
					string text = folderPath + Path.DirectorySeparatorChar.ToString() + "workshop.json";
					bool flag = true;
					try
					{
						File.WriteAllText(text, manifestText);
					}
					catch (Exception ex)
					{
						this._issueReporter.ReportManifestCreationProblem("Workshop.ReportIssue_CouldNotCreateResourcePackManifestFile", ex);
						flag = false;
					}
					return flag;
				}

				// Token: 0x06004BC7 RID: 19399 RVA: 0x006D9438 File Offset: 0x006D7638
				public bool TryGetProgress(out float progress)
				{
					progress = 0f;
					if (this._updateHandle == default(UGCUpdateHandle_t))
					{
						return false;
					}
					ulong num;
					ulong num2;
					SteamUGC.GetItemUpdateProgress(this._updateHandle, ref num, ref num2);
					if (num2 == 0UL)
					{
						return false;
					}
					progress = (float)(num / num2);
					return true;
				}

				// Token: 0x06004BC8 RID: 19400 RVA: 0x0000357B File Offset: 0x0000177B
				protected APublisherInstance()
				{
				}

				// Token: 0x04007776 RID: 30582
				protected WorkshopItemPublicSettingId _publicity;

				// Token: 0x04007777 RID: 30583
				protected WorkshopHelper.UGCBased.SteamWorkshopItem _entryData;

				// Token: 0x04007778 RID: 30584
				protected PublishedFileId_t _publishedFileID;

				// Token: 0x04007779 RID: 30585
				private UGCUpdateHandle_t _updateHandle;

				// Token: 0x0400777A RID: 30586
				private CallResult<CreateItemResult_t> _createItemHook;

				// Token: 0x0400777B RID: 30587
				private CallResult<SubmitItemUpdateResult_t> _updateItemHook;

				// Token: 0x0400777C RID: 30588
				private WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction _endAction;

				// Token: 0x0400777D RID: 30589
				private WorkshopIssueReporter _issueReporter;

				// Token: 0x02000B14 RID: 2836
				// (Invoke) Token: 0x06004DB5 RID: 19893
				public delegate void FinishedPublishingAction(WorkshopHelper.UGCBased.APublisherInstance instance);
			}

			// Token: 0x02000A8E RID: 2702
			public class ResourcePackPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
			{
				// Token: 0x06004BC9 RID: 19401 RVA: 0x006D9484 File Offset: 0x006D7684
				public ResourcePackPublisherInstance(ResourcePack resourcePack)
				{
					this._resourcePack = resourcePack;
				}

				// Token: 0x06004BCA RID: 19402 RVA: 0x006D9493 File Offset: 0x006D7693
				protected override string GetHeaderText()
				{
					return TexturePackWorkshopEntry.GetHeaderTextFor(this._resourcePack, this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);
				}

				// Token: 0x06004BCB RID: 19403 RVA: 0x00009E46 File Offset: 0x00008046
				protected override void PrepareContentForUpdate()
				{
				}

				// Token: 0x0400777E RID: 30590
				private ResourcePack _resourcePack;
			}

			// Token: 0x02000A8F RID: 2703
			public class WorldPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
			{
				// Token: 0x06004BCC RID: 19404 RVA: 0x006D94C7 File Offset: 0x006D76C7
				public WorldPublisherInstance(WorldFileData world)
				{
					this._world = world;
				}

				// Token: 0x06004BCD RID: 19405 RVA: 0x006D94D6 File Offset: 0x006D76D6
				protected override string GetHeaderText()
				{
					return WorldWorkshopEntry.GetHeaderTextFor(this._world, this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);
				}

				// Token: 0x06004BCE RID: 19406 RVA: 0x006D950C File Offset: 0x006D770C
				protected override void PrepareContentForUpdate()
				{
					if (this._world.IsCloudSave)
					{
						FileUtilities.CopyToLocal(this._world.Path, this._entryData.ContentFolderPath + Path.DirectorySeparatorChar.ToString() + "world.wld");
						return;
					}
					FileUtilities.Copy(this._world.Path, this._entryData.ContentFolderPath + Path.DirectorySeparatorChar.ToString() + "world.wld", false);
				}

				// Token: 0x0400777F RID: 30591
				private WorldFileData _world;
			}
		}
	}
}
