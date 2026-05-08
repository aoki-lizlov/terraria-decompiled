using System;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005DA RID: 1498
	internal class ArgInfo
	{
		// Token: 0x06003A10 RID: 14864 RVA: 0x000CC918 File Offset: 0x000CAB18
		public ArgInfo(MethodBase method, ArgInfoType type)
		{
			this._method = method;
			ParameterInfo[] parameters = this._method.GetParameters();
			this._paramMap = new int[parameters.Length];
			this._inoutArgCount = 0;
			if (type == ArgInfoType.In)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (!parameters[i].ParameterType.IsByRef)
					{
						int[] paramMap = this._paramMap;
						int num = this._inoutArgCount;
						this._inoutArgCount = num + 1;
						paramMap[num] = i;
					}
				}
				return;
			}
			for (int j = 0; j < parameters.Length; j++)
			{
				if (parameters[j].ParameterType.IsByRef || parameters[j].IsOut)
				{
					int[] paramMap2 = this._paramMap;
					int num = this._inoutArgCount;
					this._inoutArgCount = num + 1;
					paramMap2[num] = j;
				}
			}
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000CC9CD File Offset: 0x000CABCD
		public int GetInOutArgIndex(int inoutArgNum)
		{
			return this._paramMap[inoutArgNum];
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000CC9D7 File Offset: 0x000CABD7
		public virtual string GetInOutArgName(int index)
		{
			return this._method.GetParameters()[this._paramMap[index]].Name;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000CC9F2 File Offset: 0x000CABF2
		public int GetInOutArgCount()
		{
			return this._inoutArgCount;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000CC9FC File Offset: 0x000CABFC
		public object[] GetInOutArgs(object[] args)
		{
			object[] array = new object[this._inoutArgCount];
			for (int i = 0; i < this._inoutArgCount; i++)
			{
				array[i] = args[this._paramMap[i]];
			}
			return array;
		}

		// Token: 0x040025E0 RID: 9696
		private int[] _paramMap;

		// Token: 0x040025E1 RID: 9697
		private int _inoutArgCount;

		// Token: 0x040025E2 RID: 9698
		private MethodBase _method;
	}
}
