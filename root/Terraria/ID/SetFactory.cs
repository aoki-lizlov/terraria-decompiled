using System;
using System.Collections.Generic;

namespace Terraria.ID
{
	// Token: 0x020001B8 RID: 440
	public class SetFactory
	{
		// Token: 0x06001F3D RID: 7997 RVA: 0x00516A38 File Offset: 0x00514C38
		public SetFactory(int size)
		{
			if (size == 0)
			{
				throw new ArgumentOutOfRangeException("size cannot be 0, the intializer for Count must run first");
			}
			this._size = size;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00516A98 File Offset: 0x00514C98
		protected bool[] GetBoolBuffer()
		{
			object queueLock = this._queueLock;
			bool[] array;
			lock (queueLock)
			{
				if (this._boolBufferCache.Count == 0)
				{
					array = new bool[this._size];
				}
				else
				{
					array = this._boolBufferCache.Dequeue();
				}
			}
			return array;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00516AFC File Offset: 0x00514CFC
		protected int[] GetIntBuffer()
		{
			object queueLock = this._queueLock;
			int[] array;
			lock (queueLock)
			{
				if (this._intBufferCache.Count == 0)
				{
					array = new int[this._size];
				}
				else
				{
					array = this._intBufferCache.Dequeue();
				}
			}
			return array;
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00516B60 File Offset: 0x00514D60
		protected ushort[] GetUshortBuffer()
		{
			object queueLock = this._queueLock;
			ushort[] array;
			lock (queueLock)
			{
				if (this._ushortBufferCache.Count == 0)
				{
					array = new ushort[this._size];
				}
				else
				{
					array = this._ushortBufferCache.Dequeue();
				}
			}
			return array;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00516BC4 File Offset: 0x00514DC4
		protected float[] GetFloatBuffer()
		{
			object queueLock = this._queueLock;
			float[] array;
			lock (queueLock)
			{
				if (this._floatBufferCache.Count == 0)
				{
					array = new float[this._size];
				}
				else
				{
					array = this._floatBufferCache.Dequeue();
				}
			}
			return array;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00516C28 File Offset: 0x00514E28
		public void Recycle<T>(T[] buffer)
		{
			object queueLock = this._queueLock;
			lock (queueLock)
			{
				if (typeof(T).Equals(typeof(bool)))
				{
					this._boolBufferCache.Enqueue((bool[])buffer);
				}
				else if (typeof(T).Equals(typeof(int)))
				{
					this._intBufferCache.Enqueue((int[])buffer);
				}
			}
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00516CBC File Offset: 0x00514EBC
		public bool[] CreateBoolSet(params int[] types)
		{
			return this.CreateBoolSet(false, types);
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00516CC8 File Offset: 0x00514EC8
		public bool[] CreateBoolSet(bool defaultState, params int[] types)
		{
			bool[] boolBuffer = this.GetBoolBuffer();
			for (int i = 0; i < boolBuffer.Length; i++)
			{
				boolBuffer[i] = defaultState;
			}
			for (int j = 0; j < types.Length; j++)
			{
				boolBuffer[types[j]] = !defaultState;
			}
			return boolBuffer;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00516D06 File Offset: 0x00514F06
		public int[] CreateIntSet(params int[] types)
		{
			return this.CreateIntSet(-1, types);
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00516D10 File Offset: 0x00514F10
		public int[] CreateIntSet(int defaultState, params int[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			int[] intBuffer = this.GetIntBuffer();
			for (int i = 0; i < intBuffer.Length; i++)
			{
				intBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				intBuffer[inputs[j]] = inputs[j + 1];
			}
			return intBuffer;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00516D64 File Offset: 0x00514F64
		public ushort[] CreateUshortSet(ushort defaultState, params ushort[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			ushort[] ushortBuffer = this.GetUshortBuffer();
			for (int i = 0; i < ushortBuffer.Length; i++)
			{
				ushortBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				ushortBuffer[(int)inputs[j]] = inputs[j + 1];
			}
			return ushortBuffer;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00516DB8 File Offset: 0x00514FB8
		public float[] CreateFloatSet(float defaultState, params float[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateArraySet");
			}
			float[] floatBuffer = this.GetFloatBuffer();
			for (int i = 0; i < floatBuffer.Length; i++)
			{
				floatBuffer[i] = defaultState;
			}
			for (int j = 0; j < inputs.Length; j += 2)
			{
				floatBuffer[(int)inputs[j]] = inputs[j + 1];
			}
			return floatBuffer;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00516E0C File Offset: 0x0051500C
		public T[] CreateCustomSet<T>(T defaultState, params object[] inputs)
		{
			if (inputs.Length % 2 != 0)
			{
				throw new Exception("You have a bad length for inputs on CreateCustomSet");
			}
			T[] array = new T[this._size];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = defaultState;
			}
			if (inputs != null)
			{
				for (int j = 0; j < inputs.Length; j += 2)
				{
					T t;
					if (typeof(T).IsPrimitive)
					{
						t = (T)((object)inputs[j + 1]);
					}
					else if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						t = (T)((object)inputs[j + 1]);
					}
					else if (typeof(T).IsClass)
					{
						t = (T)((object)inputs[j + 1]);
					}
					else
					{
						t = (T)((object)Convert.ChangeType(inputs[j + 1], typeof(T)));
					}
					if (inputs[j] is ushort)
					{
						array[(int)((ushort)inputs[j])] = t;
					}
					else if (inputs[j] is int)
					{
						array[(int)inputs[j]] = t;
					}
					else
					{
						array[(int)((short)inputs[j])] = t;
					}
				}
			}
			return array;
		}

		// Token: 0x040020FA RID: 8442
		protected int _size;

		// Token: 0x040020FB RID: 8443
		private readonly Queue<int[]> _intBufferCache = new Queue<int[]>();

		// Token: 0x040020FC RID: 8444
		private readonly Queue<ushort[]> _ushortBufferCache = new Queue<ushort[]>();

		// Token: 0x040020FD RID: 8445
		private readonly Queue<bool[]> _boolBufferCache = new Queue<bool[]>();

		// Token: 0x040020FE RID: 8446
		private readonly Queue<float[]> _floatBufferCache = new Queue<float[]>();

		// Token: 0x040020FF RID: 8447
		private object _queueLock = new object();
	}
}
