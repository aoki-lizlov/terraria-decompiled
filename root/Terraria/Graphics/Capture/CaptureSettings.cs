using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020001DE RID: 478
	public class CaptureSettings
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x00521194 File Offset: 0x0051F394
		public CaptureSettings()
		{
			DateTime dateTime = DateTime.Now.ToLocalTime();
			this.OutputName = string.Concat(new string[]
			{
				"Capture ",
				dateTime.Year.ToString("D4"),
				"-",
				dateTime.Month.ToString("D2"),
				"-",
				dateTime.Day.ToString("D2"),
				" ",
				dateTime.Hour.ToString("D2"),
				"_",
				dateTime.Minute.ToString("D2"),
				"_",
				dateTime.Second.ToString("D2")
			});
		}

		// Token: 0x04004A8C RID: 19084
		public Rectangle Area;

		// Token: 0x04004A8D RID: 19085
		public bool UseScaling = true;

		// Token: 0x04004A8E RID: 19086
		public string OutputName;

		// Token: 0x04004A8F RID: 19087
		public bool CaptureEntities = true;

		// Token: 0x04004A90 RID: 19088
		public CaptureBiome Biome = CaptureBiome.DefaultPurity;

		// Token: 0x04004A91 RID: 19089
		public bool CaptureMech;

		// Token: 0x04004A92 RID: 19090
		public bool CaptureBackground;

		// Token: 0x04004A93 RID: 19091
		public bool CameraSpaceEffects;
	}
}
