using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200002B RID: 43
	public abstract class ChromaShader
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00005438 File Offset: 0x00003638
		protected ChromaShader()
		{
			for (int i = 0; i < this._processors.Capacity; i++)
			{
				this._processors.Add(ChromaShader.BoundProcessor.None);
			}
			this.BindProcessors();
			for (int j = 0; j < this._processors.Count; j++)
			{
				this.TransparentAtAnyDetailLevel |= this._processors[j].Processor != null && this._processors[j].IsTransparent;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000054CD File Offset: 0x000036CD
		public virtual bool IsTransparentAt(EffectDetailLevel quality)
		{
			return this._processors[(int)quality].IsTransparent;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000046AD File Offset: 0x000028AD
		public virtual void Update(float elapsedTime)
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000054E0 File Offset: 0x000036E0
		public virtual void Process(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (this._processors[(int)quality].Processor != null)
			{
				this._processors[(int)quality].Processor(device, fragment, quality, time);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005510 File Offset: 0x00003710
		private void BindProcessors()
		{
			foreach (MethodInfo methodInfo in base.GetType().GetMethods(54))
			{
				RgbProcessorAttribute rgbProcessorAttribute = (RgbProcessorAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(RgbProcessorAttribute));
				if (rgbProcessorAttribute != null)
				{
					ChromaShader.Processor processor = (ChromaShader.Processor)Delegate.CreateDelegate(typeof(ChromaShader.Processor), this, methodInfo, false);
					if (processor != null)
					{
						this.BindProcessor(processor, rgbProcessorAttribute);
					}
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000557C File Offset: 0x0000377C
		private void BindProcessor(ChromaShader.Processor processor, RgbProcessorAttribute attribute)
		{
			foreach (EffectDetailLevel effectDetailLevel in attribute.SupportedDetailLevels)
			{
				this._processors[(int)effectDetailLevel] = new ChromaShader.BoundProcessor(processor, attribute.IsTransparent);
			}
		}

		// Token: 0x04000073 RID: 115
		public readonly bool TransparentAtAnyDetailLevel;

		// Token: 0x04000074 RID: 116
		private readonly List<ChromaShader.BoundProcessor> _processors = new List<ChromaShader.BoundProcessor>(2);

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x0600042F RID: 1071
		public delegate void Processor(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time);

		// Token: 0x020000BC RID: 188
		private struct BoundProcessor
		{
			// Token: 0x06000432 RID: 1074 RVA: 0x0000E137 File Offset: 0x0000C337
			public BoundProcessor(ChromaShader.Processor processor, bool isTransparent)
			{
				this.Processor = processor;
				this.IsTransparent = isTransparent;
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x0000E147 File Offset: 0x0000C347
			// Note: this type is marked as 'beforefieldinit'.
			static BoundProcessor()
			{
			}

			// Token: 0x04000569 RID: 1385
			public static readonly ChromaShader.BoundProcessor None = new ChromaShader.BoundProcessor(null, false);

			// Token: 0x0400056A RID: 1386
			public readonly ChromaShader.Processor Processor;

			// Token: 0x0400056B RID: 1387
			public readonly bool IsTransparent;
		}
	}
}
