using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x02000597 RID: 1431
	public class MethodSequenceListItem
	{
		// Token: 0x06003867 RID: 14439 RVA: 0x0063248E File Offset: 0x0063068E
		public MethodSequenceListItem(string name, Func<bool> method, MethodSequenceListItem parent = null)
		{
			this.Name = name;
			this.Method = method;
			this.Parent = parent;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x006324AB File Offset: 0x006306AB
		public bool ShouldAct(List<MethodSequenceListItem> sequence)
		{
			return !this.Skip && sequence.Contains(this) && (this.Parent == null || this.Parent.ShouldAct(sequence));
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x006324D8 File Offset: 0x006306D8
		public bool Act()
		{
			return this.Method();
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x006324E8 File Offset: 0x006306E8
		public static void ExecuteSequence(List<MethodSequenceListItem> sequence)
		{
			foreach (MethodSequenceListItem methodSequenceListItem in sequence)
			{
				if (methodSequenceListItem.ShouldAct(sequence) && !methodSequenceListItem.Act())
				{
					break;
				}
			}
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x00632548 File Offset: 0x00630748
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"name: ",
				this.Name,
				" skip: ",
				this.Skip.ToString(),
				" parent: ",
				this.Parent
			});
		}

		// Token: 0x04005C91 RID: 23697
		public string Name;

		// Token: 0x04005C92 RID: 23698
		public MethodSequenceListItem Parent;

		// Token: 0x04005C93 RID: 23699
		public Func<bool> Method;

		// Token: 0x04005C94 RID: 23700
		public bool Skip;
	}
}
