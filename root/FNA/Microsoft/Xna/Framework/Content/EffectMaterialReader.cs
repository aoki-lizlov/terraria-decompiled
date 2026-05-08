using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000117 RID: 279
	internal class EffectMaterialReader : ContentTypeReader<EffectMaterial>
	{
		// Token: 0x06001739 RID: 5945 RVA: 0x0003909C File Offset: 0x0003729C
		protected internal override EffectMaterial Read(ContentReader input, EffectMaterial existingInstance)
		{
			EffectMaterial effectMaterial = new EffectMaterial(input.ReadExternalReference<Effect>());
			foreach (KeyValuePair<string, object> keyValuePair in input.ReadObject<Dictionary<string, object>>())
			{
				EffectParameter effectParameter = effectMaterial.Parameters[keyValuePair.Key];
				if (effectParameter != null)
				{
					Type type = keyValuePair.Value.GetType();
					if (typeof(Texture).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Texture)keyValuePair.Value);
					}
					else if (typeof(int).IsAssignableFrom(type))
					{
						effectParameter.SetValue((int)keyValuePair.Value);
					}
					else if (typeof(int[]).IsAssignableFrom(type))
					{
						effectParameter.SetValue((int[])keyValuePair.Value);
					}
					else if (typeof(bool).IsAssignableFrom(type))
					{
						effectParameter.SetValue((bool)keyValuePair.Value);
					}
					else if (typeof(float).IsAssignableFrom(type))
					{
						effectParameter.SetValue((float)keyValuePair.Value);
					}
					else if (typeof(float[]).IsAssignableFrom(type))
					{
						effectParameter.SetValue((float[])keyValuePair.Value);
					}
					else if (typeof(Vector2).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Vector2)keyValuePair.Value);
					}
					else if (typeof(Vector2[]).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Vector2[])keyValuePair.Value);
					}
					else if (typeof(Vector3).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Vector3)keyValuePair.Value);
					}
					else if (typeof(Vector3[]).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Vector3[])keyValuePair.Value);
					}
					else if (typeof(Vector4).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Vector4)keyValuePair.Value);
					}
					else if (typeof(Vector4[]).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Vector4[])keyValuePair.Value);
					}
					else if (typeof(Matrix).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Matrix)keyValuePair.Value);
					}
					else if (typeof(Matrix[]).IsAssignableFrom(type))
					{
						effectParameter.SetValue((Matrix[])keyValuePair.Value);
					}
					else
					{
						if (!typeof(Quaternion).IsAssignableFrom(type))
						{
							throw new NotSupportedException("Parameter type is not supported");
						}
						effectParameter.SetValue((Quaternion)keyValuePair.Value);
					}
				}
			}
			return effectMaterial;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000393A8 File Offset: 0x000375A8
		public EffectMaterialReader()
		{
		}
	}
}
