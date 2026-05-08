using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200007E RID: 126
	public class DisplayModeCollection : IEnumerable<DisplayMode>, IEnumerable
	{
		// Token: 0x170001F5 RID: 501
		public IEnumerable<DisplayMode> this[SurfaceFormat format]
		{
			get
			{
				List<DisplayMode> list = new List<DisplayMode>();
				foreach (DisplayMode displayMode in this.modes)
				{
					if (displayMode.Format == format)
					{
						list.Add(displayMode);
					}
				}
				return list;
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000245B4 File Offset: 0x000227B4
		internal DisplayModeCollection(List<DisplayMode> setmodes)
		{
			this.modes = setmodes;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000245C3 File Offset: 0x000227C3
		public IEnumerator<DisplayMode> GetEnumerator()
		{
			return this.modes.GetEnumerator();
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000245C3 File Offset: 0x000227C3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.modes.GetEnumerator();
		}

		// Token: 0x040007CC RID: 1996
		private readonly List<DisplayMode> modes;
	}
}
