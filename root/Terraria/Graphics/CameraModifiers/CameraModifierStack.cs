using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x0200021A RID: 538
	public class CameraModifierStack
	{
		// Token: 0x060021C4 RID: 8644 RVA: 0x005328BC File Offset: 0x00530ABC
		public void Add(ICameraModifier modifier)
		{
			this.RemoveIdenticalModifiers(modifier);
			if (!Main.UseScreenShake && modifier.IsAScreenShake)
			{
				return;
			}
			this._modifiers.Add(modifier);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x005328E4 File Offset: 0x00530AE4
		private void RemoveIdenticalModifiers(ICameraModifier modifier)
		{
			string uniqueIdentity = modifier.UniqueIdentity;
			if (uniqueIdentity == null)
			{
				return;
			}
			for (int i = this._modifiers.Count - 1; i >= 0; i--)
			{
				if (this._modifiers[i].UniqueIdentity == uniqueIdentity)
				{
					this._modifiers.RemoveAt(i);
				}
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0053293C File Offset: 0x00530B3C
		public void ApplyTo(ref Vector2 cameraPosition)
		{
			CameraInfo cameraInfo = new CameraInfo(cameraPosition);
			this.ClearFinishedModifiers();
			for (int i = 0; i < this._modifiers.Count; i++)
			{
				this._modifiers[i].Update(ref cameraInfo);
			}
			cameraPosition = cameraInfo.CameraPosition;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x00532994 File Offset: 0x00530B94
		private void ClearFinishedModifiers()
		{
			for (int i = this._modifiers.Count - 1; i >= 0; i--)
			{
				if (this._modifiers[i].Finished)
				{
					this._modifiers.RemoveAt(i);
				}
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x005329D8 File Offset: 0x00530BD8
		public CameraModifierStack()
		{
		}

		// Token: 0x04004C38 RID: 19512
		private List<ICameraModifier> _modifiers = new List<ICameraModifier>();
	}
}
