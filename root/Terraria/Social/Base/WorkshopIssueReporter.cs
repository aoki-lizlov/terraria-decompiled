using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Terraria.Social.Base
{
	// Token: 0x02000159 RID: 345
	public class WorkshopIssueReporter : IProvideReports
	{
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06001D4C RID: 7500 RVA: 0x0050196C File Offset: 0x004FFB6C
		// (remove) Token: 0x06001D4D RID: 7501 RVA: 0x005019A4 File Offset: 0x004FFBA4
		public event Action OnNeedToOpenUI
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnNeedToOpenUI;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnNeedToOpenUI, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnNeedToOpenUI;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnNeedToOpenUI, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001D4E RID: 7502 RVA: 0x005019DC File Offset: 0x004FFBDC
		// (remove) Token: 0x06001D4F RID: 7503 RVA: 0x00501A14 File Offset: 0x004FFC14
		public event Action OnNeedToNotifyUI
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnNeedToNotifyUI;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnNeedToNotifyUI, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnNeedToNotifyUI;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnNeedToNotifyUI, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00501A4C File Offset: 0x004FFC4C
		private void AddReport(string reportText)
		{
			IssueReport issueReport = new IssueReport(reportText);
			this._reports.Add(issueReport);
			while (this._reports.Count > this._maxReports)
			{
				this._reports.RemoveAt(0);
			}
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00501A8D File Offset: 0x004FFC8D
		public List<IssueReport> GetReports()
		{
			return this._reports;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00501A95 File Offset: 0x004FFC95
		private void OpenReportsScreen()
		{
			if (this.OnNeedToOpenUI != null)
			{
				this.OnNeedToOpenUI();
			}
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00501AAA File Offset: 0x004FFCAA
		private void NotifyReportsScreen()
		{
			if (this.OnNeedToNotifyUI != null)
			{
				this.OnNeedToNotifyUI();
			}
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00501AC0 File Offset: 0x004FFCC0
		public void ReportInstantUploadProblem(string textKey)
		{
			string textValue = Language.GetTextValue(textKey);
			this.AddReport(textValue);
			this.OpenReportsScreen();
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00501AE1 File Offset: 0x004FFCE1
		public void ReportInstantUploadProblemFromValue(string text)
		{
			this.AddReport(text);
			this.OpenReportsScreen();
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00501AF0 File Offset: 0x004FFCF0
		public void ReportDelayedUploadProblem(string textKey)
		{
			string textValue = Language.GetTextValue(textKey);
			this.AddReport(textValue);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00501B14 File Offset: 0x004FFD14
		public void ReportDelayedUploadProblemWithoutKnownReason(string textKey, string reasonValue)
		{
			object obj = new
			{
				Reason = reasonValue
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x00501B40 File Offset: 0x004FFD40
		public void ReportDownloadProblem(string textKey, string path, Exception exception)
		{
			object obj = new
			{
				FilePath = path,
				Reason = exception.ToString()
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00501B70 File Offset: 0x004FFD70
		public void ReportManifestCreationProblem(string textKey, Exception exception)
		{
			object obj = new
			{
				Reason = exception.ToString()
			};
			string textValueWith = Language.GetTextValueWith(textKey, obj);
			this.AddReport(textValueWith);
			this.NotifyReportsScreen();
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00501B9E File Offset: 0x004FFD9E
		public WorkshopIssueReporter()
		{
		}

		// Token: 0x04001646 RID: 5702
		[CompilerGenerated]
		private Action OnNeedToOpenUI;

		// Token: 0x04001647 RID: 5703
		[CompilerGenerated]
		private Action OnNeedToNotifyUI;

		// Token: 0x04001648 RID: 5704
		private int _maxReports = 1000;

		// Token: 0x04001649 RID: 5705
		private List<IssueReport> _reports = new List<IssueReport>();
	}
}
