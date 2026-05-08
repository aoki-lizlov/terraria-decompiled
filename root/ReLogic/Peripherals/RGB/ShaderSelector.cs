using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200002D RID: 45
	internal class ShaderSelector
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00005620 File Offset: 0x00003820
		public ShaderSelector()
		{
			for (int i = 0; i < this._operationsByDetailLevel.Length; i++)
			{
				this._operationsByDetailLevel[i] = new List<ShaderOperation>();
			}
			for (int j = 0; j < 11; j++)
			{
				this._shaderGroups.Add(new ShaderSelector.ShaderGroup());
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005687 File Offset: 0x00003887
		public void Register(ChromaShader shader, ChromaCondition condition, ShaderLayer layer)
		{
			this._shaderGroups[(int)layer].Add(shader, condition);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000569C File Offset: 0x0000389C
		public void Unregister(ChromaShader shader)
		{
			for (int i = 0; i < 11; i++)
			{
				this._shaderGroups[i].Remove(shader);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000056C8 File Offset: 0x000038C8
		public ICollection<ShaderOperation> AtDetailLevel(EffectDetailLevel quality)
		{
			return this._operationsByDetailLevel[(int)quality];
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000056D2 File Offset: 0x000038D2
		public void Update(float timeElapsed)
		{
			this.UpdateShaderVisibility(timeElapsed);
			this.UpdateShaders(timeElapsed);
			this.BuildOperationsList();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000056E8 File Offset: 0x000038E8
		private void UpdateShaderVisibility(float timeElapsed)
		{
			foreach (ShaderSelector.ShaderGroup shaderGroup in this._shaderGroups)
			{
				shaderGroup.UpdateVisibility(timeElapsed);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000573C File Offset: 0x0000393C
		private void UpdateShaders(float timeElapsed)
		{
			int num = this._shaderGroups.Count - 1;
			while (num >= 0 && !this._shaderGroups[num].UpdateShaders(timeElapsed))
			{
				num--;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005778 File Offset: 0x00003978
		private void BuildOperationsList()
		{
			for (int i = 0; i <= 1; i++)
			{
				List<ShaderOperation> list = this._operationsByDetailLevel[i];
				list.Clear();
				int num = this._shaderGroups.Count - 1;
				while (num >= 0 && !this._shaderGroups[num].AppendOperations((EffectDetailLevel)i, list))
				{
					num--;
				}
				list.Reverse();
				if (list.Count > 0)
				{
					list[0] = list[0].WithBlendState(new ShaderBlendState(BlendMode.None, 1f));
				}
			}
		}

		// Token: 0x04000078 RID: 120
		private readonly List<ShaderSelector.ShaderGroup> _shaderGroups = new List<ShaderSelector.ShaderGroup>();

		// Token: 0x04000079 RID: 121
		private readonly List<ShaderOperation>[] _operationsByDetailLevel = new List<ShaderOperation>[2];

		// Token: 0x020000BD RID: 189
		private class ConditionalShader
		{
			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000E155 File Offset: 0x0000C355
			// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000E15D File Offset: 0x0000C35D
			public bool IsActive
			{
				[CompilerGenerated]
				get
				{
					return this.<IsActive>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IsActive>k__BackingField = value;
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000E166 File Offset: 0x0000C366
			public bool IsVisible
			{
				get
				{
					return this.Opacity > 0f;
				}
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x0000E175 File Offset: 0x0000C375
			public ConditionalShader(ChromaShader shader, ChromaCondition condition)
			{
				this.Shader = shader;
				this.Condition = condition;
				this.IsActive = false;
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0000E194 File Offset: 0x0000C394
			public void UpdateVisibility(float timeElapsed)
			{
				this.IsActive = this.Condition.IsActive();
				if (this.IsActive)
				{
					this.Opacity = Math.Min(1f, this.Opacity + timeElapsed);
					return;
				}
				this.Opacity = Math.Max(0f, this.Opacity - timeElapsed);
			}

			// Token: 0x0400056C RID: 1388
			public readonly ChromaShader Shader;

			// Token: 0x0400056D RID: 1389
			public readonly ChromaCondition Condition;

			// Token: 0x0400056E RID: 1390
			public float Opacity;

			// Token: 0x0400056F RID: 1391
			[CompilerGenerated]
			private bool <IsActive>k__BackingField;
		}

		// Token: 0x020000BE RID: 190
		private class ShaderGroup
		{
			// Token: 0x06000439 RID: 1081 RVA: 0x0000E1EB File Offset: 0x0000C3EB
			public void Add(ChromaShader shader, ChromaCondition condition)
			{
				this.Shaders.AddLast(new ShaderSelector.ConditionalShader(shader, condition));
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x0000E200 File Offset: 0x0000C400
			public void Remove(ChromaShader shader)
			{
				LinkedListNode<ShaderSelector.ConditionalShader> next;
				for (LinkedListNode<ShaderSelector.ConditionalShader> linkedListNode = this.Shaders.First; linkedListNode != null; linkedListNode = next)
				{
					next = linkedListNode.Next;
					if (linkedListNode.Value.Shader == shader)
					{
						this.Shaders.Remove(linkedListNode);
					}
				}
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x0000E240 File Offset: 0x0000C440
			public void UpdateVisibility(float timeElapsed)
			{
				LinkedListNode<ShaderSelector.ConditionalShader> next;
				for (LinkedListNode<ShaderSelector.ConditionalShader> linkedListNode = this.Shaders.First; linkedListNode != null; linkedListNode = next)
				{
					next = linkedListNode.Next;
					ShaderSelector.ConditionalShader value = linkedListNode.Value;
					bool isVisible = value.IsVisible;
					value.UpdateVisibility(timeElapsed);
					if (!isVisible && value.IsVisible)
					{
						this.Shaders.Remove(linkedListNode);
						this.Shaders.AddFirst(value);
					}
				}
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x0000E29C File Offset: 0x0000C49C
			public bool UpdateShaders(float timeElapsed)
			{
				foreach (ShaderSelector.ConditionalShader conditionalShader in this.Shaders)
				{
					conditionalShader.Shader.Update(timeElapsed);
					if (conditionalShader.IsVisible && conditionalShader.Opacity >= 1f)
					{
						return !conditionalShader.Shader.TransparentAtAnyDetailLevel;
					}
				}
				return false;
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x0000E320 File Offset: 0x0000C520
			public bool AppendOperations(EffectDetailLevel quality, List<ShaderOperation> operations)
			{
				foreach (ShaderSelector.ConditionalShader conditionalShader in this.Shaders)
				{
					if (conditionalShader.IsVisible)
					{
						bool flag = conditionalShader.Shader.IsTransparentAt(quality);
						ShaderBlendState shaderBlendState = new ShaderBlendState(flag ? BlendMode.PerPixelOpacity : BlendMode.GlobalOpacityOnly, conditionalShader.Opacity);
						operations.Add(new ShaderOperation(conditionalShader.Shader, shaderBlendState, quality));
						if (conditionalShader.Opacity >= 1f)
						{
							return !flag;
						}
					}
				}
				return false;
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
			public ShaderGroup()
			{
			}

			// Token: 0x04000570 RID: 1392
			public readonly LinkedList<ShaderSelector.ConditionalShader> Shaders = new LinkedList<ShaderSelector.ConditionalShader>();
		}
	}
}
