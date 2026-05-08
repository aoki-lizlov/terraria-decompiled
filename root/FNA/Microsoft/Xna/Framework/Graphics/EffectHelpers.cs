using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000094 RID: 148
	internal static class EffectHelpers
	{
		// Token: 0x06001248 RID: 4680 RVA: 0x0002A254 File Offset: 0x00028454
		internal static Vector3 EnableDefaultLighting(DirectionalLight light0, DirectionalLight light1, DirectionalLight light2)
		{
			light0.Direction = new Vector3(-0.5265408f, -0.5735765f, -0.6275069f);
			light0.DiffuseColor = new Vector3(1f, 0.9607844f, 0.8078432f);
			light0.SpecularColor = new Vector3(1f, 0.9607844f, 0.8078432f);
			light0.Enabled = true;
			light1.Direction = new Vector3(0.7198464f, 0.3420201f, 0.6040227f);
			light1.DiffuseColor = new Vector3(0.9647059f, 0.7607844f, 0.4078432f);
			light1.SpecularColor = Vector3.Zero;
			light1.Enabled = true;
			light2.Direction = new Vector3(0.4545195f, -0.7660444f, 0.4545195f);
			light2.DiffuseColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);
			light2.SpecularColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);
			light2.Enabled = true;
			return new Vector3(0.05333332f, 0.09882354f, 0.1819608f);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0002A368 File Offset: 0x00028568
		internal static EffectDirtyFlags SetWorldViewProjAndFog(EffectDirtyFlags dirtyFlags, ref Matrix world, ref Matrix view, ref Matrix projection, ref Matrix worldView, bool fogEnabled, float fogStart, float fogEnd, EffectParameter worldViewProjParam, EffectParameter fogVectorParam)
		{
			if ((dirtyFlags & EffectDirtyFlags.WorldViewProj) != (EffectDirtyFlags)0)
			{
				Matrix.Multiply(ref world, ref view, out worldView);
				Matrix matrix;
				Matrix.Multiply(ref worldView, ref projection, out matrix);
				worldViewProjParam.SetValue(matrix);
				dirtyFlags &= ~EffectDirtyFlags.WorldViewProj;
			}
			if (fogEnabled)
			{
				if ((dirtyFlags & (EffectDirtyFlags.Fog | EffectDirtyFlags.FogEnable)) != (EffectDirtyFlags)0)
				{
					EffectHelpers.SetFogVector(ref worldView, fogStart, fogEnd, fogVectorParam);
					dirtyFlags &= ~(EffectDirtyFlags.Fog | EffectDirtyFlags.FogEnable);
				}
			}
			else if ((dirtyFlags & EffectDirtyFlags.FogEnable) != (EffectDirtyFlags)0)
			{
				fogVectorParam.SetValue(Vector4.Zero);
				dirtyFlags &= ~EffectDirtyFlags.FogEnable;
			}
			return dirtyFlags;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0002A3D4 File Offset: 0x000285D4
		private static void SetFogVector(ref Matrix worldView, float fogStart, float fogEnd, EffectParameter fogVectorParam)
		{
			if (fogStart == fogEnd)
			{
				fogVectorParam.SetValue(new Vector4(0f, 0f, 0f, 1f));
				return;
			}
			float num = 1f / (fogStart - fogEnd);
			fogVectorParam.SetValue(new Vector4
			{
				X = worldView.M13 * num,
				Y = worldView.M23 * num,
				Z = worldView.M33 * num,
				W = (worldView.M43 + fogStart) * num
			});
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0002A45C File Offset: 0x0002865C
		internal static EffectDirtyFlags SetLightingMatrices(EffectDirtyFlags dirtyFlags, ref Matrix world, ref Matrix view, EffectParameter worldParam, EffectParameter worldInverseTransposeParam, EffectParameter eyePositionParam)
		{
			if ((dirtyFlags & EffectDirtyFlags.World) != (EffectDirtyFlags)0)
			{
				Matrix matrix;
				Matrix.Invert(ref world, out matrix);
				Matrix matrix2;
				Matrix.Transpose(ref matrix, out matrix2);
				worldParam.SetValue(world);
				worldInverseTransposeParam.SetValue(matrix2);
				dirtyFlags &= ~EffectDirtyFlags.World;
			}
			if ((dirtyFlags & EffectDirtyFlags.EyePosition) != (EffectDirtyFlags)0)
			{
				Matrix matrix3;
				Matrix.Invert(ref view, out matrix3);
				eyePositionParam.SetValue(matrix3.Translation);
				dirtyFlags &= ~EffectDirtyFlags.EyePosition;
			}
			return dirtyFlags;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0002A4BC File Offset: 0x000286BC
		internal static void SetMaterialColor(bool lightingEnabled, float alpha, ref Vector3 diffuseColor, ref Vector3 emissiveColor, ref Vector3 ambientLightColor, EffectParameter diffuseColorParam, EffectParameter emissiveColorParam)
		{
			if (lightingEnabled)
			{
				Vector4 vector = default(Vector4);
				Vector3 vector2 = default(Vector3);
				vector.X = diffuseColor.X * alpha;
				vector.Y = diffuseColor.Y * alpha;
				vector.Z = diffuseColor.Z * alpha;
				vector.W = alpha;
				vector2.X = (emissiveColor.X + ambientLightColor.X * diffuseColor.X) * alpha;
				vector2.Y = (emissiveColor.Y + ambientLightColor.Y * diffuseColor.Y) * alpha;
				vector2.Z = (emissiveColor.Z + ambientLightColor.Z * diffuseColor.Z) * alpha;
				diffuseColorParam.SetValue(vector);
				emissiveColorParam.SetValue(vector2);
				return;
			}
			diffuseColorParam.SetValue(new Vector4
			{
				X = (diffuseColor.X + emissiveColor.X) * alpha,
				Y = (diffuseColor.Y + emissiveColor.Y) * alpha,
				Z = (diffuseColor.Z + emissiveColor.Z) * alpha,
				W = alpha
			});
		}
	}
}
