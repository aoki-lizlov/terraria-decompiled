using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x020000F3 RID: 243
	[DebuggerDisplay("Snap Point - {Name} {Id}")]
	public class SnapPoint
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x004E7DB3 File Offset: 0x004E5FB3
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x004E7DBB File Offset: 0x004E5FBB
		public int Id
		{
			[CompilerGenerated]
			get
			{
				return this.<Id>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Id>k__BackingField = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x004E7DC4 File Offset: 0x004E5FC4
		// (set) Token: 0x0600193A RID: 6458 RVA: 0x004E7DCC File Offset: 0x004E5FCC
		public Vector2 Position
		{
			[CompilerGenerated]
			get
			{
				return this.<Position>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Position>k__BackingField = value;
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x004E7DD5 File Offset: 0x004E5FD5
		public SnapPoint(string name, int id, Vector2 anchor, Vector2 offset)
		{
			this.Name = name;
			this.Id = id;
			this._anchor = anchor;
			this._offset = offset;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x004E7DFC File Offset: 0x004E5FFC
		public void Calculate(UIElement element)
		{
			CalculatedStyle dimensions = element.GetDimensions();
			this.Position = dimensions.Position() + this._offset + this._anchor * new Vector2(dimensions.Width, dimensions.Height);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x004E7E49 File Offset: 0x004E6049
		public void ThisIsAHackThatChangesTheSnapPointsInfo(Vector2 anchor, Vector2 offset, int id)
		{
			this._anchor = anchor;
			this._offset = offset;
			this.Id = id;
		}

		// Token: 0x0400133B RID: 4923
		public string Name;

		// Token: 0x0400133C RID: 4924
		[CompilerGenerated]
		private int <Id>k__BackingField;

		// Token: 0x0400133D RID: 4925
		[CompilerGenerated]
		private Vector2 <Position>k__BackingField;

		// Token: 0x0400133E RID: 4926
		private Vector2 _anchor;

		// Token: 0x0400133F RID: 4927
		private Vector2 _offset;
	}
}
