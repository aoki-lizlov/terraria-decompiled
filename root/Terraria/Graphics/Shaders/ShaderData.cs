using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001E6 RID: 486
	public class ShaderData
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x00522CAF File Offset: 0x00520EAF
		public Effect Shader
		{
			get
			{
				if (this._shader != null)
				{
					return this._shader.Value;
				}
				return null;
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00522CC6 File Offset: 0x00520EC6
		public ShaderData(Asset<Effect> shader, string passName)
		{
			this._passName = passName;
			this._shader = shader;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00522CDC File Offset: 0x00520EDC
		public virtual void Apply()
		{
			if (this._effect == null || this._effect != this.Shader)
			{
				this._effect = this.Shader;
				this._effectPass = this.Shader.CurrentTechnique.Passes[this._passName];
			}
			this._effectPass.Apply();
		}

		// Token: 0x04004B02 RID: 19202
		private readonly Asset<Effect> _shader;

		// Token: 0x04004B03 RID: 19203
		private readonly string _passName;

		// Token: 0x04004B04 RID: 19204
		private Effect _effect;

		// Token: 0x04004B05 RID: 19205
		private EffectPass _effectPass;

		// Token: 0x020007A0 RID: 1952
		public class EffectParameter<T>
		{
			// Token: 0x060041A5 RID: 16805 RVA: 0x006BC4D2 File Offset: 0x006BA6D2
			private EffectParameter(Action<T> setValue)
			{
				this._setValue = setValue;
			}

			// Token: 0x060041A6 RID: 16806 RVA: 0x006BC4E1 File Offset: 0x006BA6E1
			public void SetValue(T value)
			{
				if (this._hasValue && EqualityComparer<T>.Default.Equals(this._value, value))
				{
					return;
				}
				this._hasValue = true;
				this._value = value;
				this._setValue(value);
			}

			// Token: 0x060041A7 RID: 16807 RVA: 0x006BC519 File Offset: 0x006BA719
			public static ShaderData.EffectParameter<T> Get(EffectParameter param)
			{
				if (param == null)
				{
					return null;
				}
				return (ShaderData.EffectParameter<T>)ShaderData.EffectParameter<T>._cachedParameters.GetValue(param, new ConditionalWeakTable<EffectParameter, object>.CreateValueCallback(ShaderData.EffectParameter<T>._Create));
			}

			// Token: 0x060041A8 RID: 16808 RVA: 0x006BC53C File Offset: 0x006BA73C
			private static object _Create(EffectParameter param)
			{
				ShaderData.EffectParameter<Matrix> effectParameter = ((typeof(T) == typeof(Matrix)) ? new ShaderData.EffectParameter<Matrix>(new Action<Matrix>(param.SetValue)) : ((typeof(T) == typeof(Quaternion)) ? new ShaderData.EffectParameter<Quaternion>(new Action<Quaternion>(param.SetValue)) : ((typeof(T) == typeof(Vector4)) ? new ShaderData.EffectParameter<Vector4>(new Action<Vector4>(param.SetValue)) : ((typeof(T) == typeof(Vector3)) ? new ShaderData.EffectParameter<Vector3>(new Action<Vector3>(param.SetValue)) : ((typeof(T) == typeof(Vector2)) ? new ShaderData.EffectParameter<Vector2>(new Action<Vector2>(param.SetValue)) : ((typeof(T) == typeof(float)) ? new ShaderData.EffectParameter<float>(new Action<float>(param.SetValue)) : ((typeof(T) == typeof(int)) ? new ShaderData.EffectParameter<int>(new Action<int>(param.SetValue)) : ((typeof(T) == typeof(bool)) ? new ShaderData.EffectParameter<bool>(new Action<bool>(param.SetValue)) : ((typeof(T) == typeof(string)) ? new ShaderData.EffectParameter<string>(new Action<string>(param.SetValue)) : ((typeof(T) == typeof(Texture)) ? new ShaderData.EffectParameter<Texture>(new Action<Texture>(param.SetValue)) : null))))))))));
				if (effectParameter == null)
				{
					throw new ArgumentOutOfRangeException("Unsupported type: " + typeof(T));
				}
				return effectParameter;
			}

			// Token: 0x060041A9 RID: 16809 RVA: 0x006BC754 File Offset: 0x006BA954
			// Note: this type is marked as 'beforefieldinit'.
			static EffectParameter()
			{
			}

			// Token: 0x0400707D RID: 28797
			private readonly Action<T> _setValue;

			// Token: 0x0400707E RID: 28798
			private T _value;

			// Token: 0x0400707F RID: 28799
			private bool _hasValue;

			// Token: 0x04007080 RID: 28800
			private static ConditionalWeakTable<EffectParameter, object> _cachedParameters = new ConditionalWeakTable<EffectParameter, object>();
		}
	}
}
