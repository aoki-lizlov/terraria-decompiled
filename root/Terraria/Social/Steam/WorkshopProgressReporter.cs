using System;
using System.Collections.Generic;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000141 RID: 321
	public class WorkshopProgressReporter : AWorkshopProgressReporter
	{
		// Token: 0x06001C90 RID: 7312 RVA: 0x004FEFDF File Offset: 0x004FD1DF
		public WorkshopProgressReporter(List<WorkshopHelper.UGCBased.APublisherInstance> publisherInstances)
		{
			this._publisherInstances = publisherInstances;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x004FEFEE File Offset: 0x004FD1EE
		public override bool HasOngoingTasks
		{
			get
			{
				return this._publisherInstances.Count > 0;
			}
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x004FF000 File Offset: 0x004FD200
		public override bool TryGetProgress(out float progress)
		{
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < this._publisherInstances.Count; i++)
			{
				float num3;
				if (this._publisherInstances[i].TryGetProgress(out num3))
				{
					num += num3;
					num2 += 1f;
				}
			}
			progress = 0f;
			if (num2 == 0f)
			{
				return false;
			}
			progress = num / num2;
			return true;
		}

		// Token: 0x040015E2 RID: 5602
		private List<WorkshopHelper.UGCBased.APublisherInstance> _publisherInstances;
	}
}
