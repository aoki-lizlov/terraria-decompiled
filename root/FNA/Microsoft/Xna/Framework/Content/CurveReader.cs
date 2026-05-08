using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000111 RID: 273
	internal class CurveReader : ContentTypeReader<Curve>
	{
		// Token: 0x0600172B RID: 5931 RVA: 0x00038E18 File Offset: 0x00037018
		protected internal override Curve Read(ContentReader input, Curve existingInstance)
		{
			Curve curve = existingInstance;
			if (curve == null)
			{
				curve = new Curve();
			}
			curve.PreLoop = (CurveLoopType)input.ReadInt32();
			curve.PostLoop = (CurveLoopType)input.ReadInt32();
			int num = input.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				float num2 = input.ReadSingle();
				float num3 = input.ReadSingle();
				float num4 = input.ReadSingle();
				float num5 = input.ReadSingle();
				CurveContinuity curveContinuity = (CurveContinuity)input.ReadInt32();
				curve.Keys.Add(new CurveKey(num2, num3, num4, num5, curveContinuity));
			}
			return curve;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00038E9C File Offset: 0x0003709C
		public CurveReader()
		{
		}
	}
}
