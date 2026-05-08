using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020008BC RID: 2236
	[ComVisible(true)]
	[Serializable]
	public class CustomAttributeData
	{
		// Token: 0x06004BF6 RID: 19446 RVA: 0x000025BE File Offset: 0x000007BE
		protected CustomAttributeData()
		{
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x000F2609 File Offset: 0x000F0809
		internal CustomAttributeData(ConstructorInfo ctorInfo, Assembly assembly, IntPtr data, uint data_length)
		{
			this.ctorInfo = ctorInfo;
			this.lazyData = new CustomAttributeData.LazyCAttrData();
			this.lazyData.assembly = assembly;
			this.lazyData.data = data;
			this.lazyData.data_length = data_length;
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x000F2648 File Offset: 0x000F0848
		internal CustomAttributeData(ConstructorInfo ctorInfo)
			: this(ctorInfo, Array.Empty<CustomAttributeTypedArgument>(), Array.Empty<CustomAttributeNamedArgument>())
		{
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x000F265B File Offset: 0x000F085B
		internal CustomAttributeData(ConstructorInfo ctorInfo, IList<CustomAttributeTypedArgument> ctorArgs, IList<CustomAttributeNamedArgument> namedArgs)
		{
			this.ctorInfo = ctorInfo;
			this.ctorArgs = ctorArgs;
			this.namedArgs = namedArgs;
		}

		// Token: 0x06004BFA RID: 19450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveArgumentsInternal(ConstructorInfo ctor, Assembly assembly, IntPtr data, uint data_length, out object[] ctorArgs, out object[] namedArgs);

		// Token: 0x06004BFB RID: 19451 RVA: 0x000F2678 File Offset: 0x000F0878
		private void ResolveArguments()
		{
			if (this.lazyData == null)
			{
				return;
			}
			object[] array;
			object[] array2;
			CustomAttributeData.ResolveArgumentsInternal(this.ctorInfo, this.lazyData.assembly, this.lazyData.data, this.lazyData.data_length, out array, out array2);
			this.ctorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>((array != null) ? CustomAttributeData.UnboxValues<CustomAttributeTypedArgument>(array) : Array.Empty<CustomAttributeTypedArgument>());
			this.namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>((array2 != null) ? CustomAttributeData.UnboxValues<CustomAttributeNamedArgument>(array2) : Array.Empty<CustomAttributeNamedArgument>());
			this.lazyData = null;
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004BFC RID: 19452 RVA: 0x000F26FB File Offset: 0x000F08FB
		[ComVisible(true)]
		public virtual ConstructorInfo Constructor
		{
			get
			{
				return this.ctorInfo;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004BFD RID: 19453 RVA: 0x000F2703 File Offset: 0x000F0903
		[ComVisible(true)]
		public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			get
			{
				this.ResolveArguments();
				return this.ctorArgs;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004BFE RID: 19454 RVA: 0x000F2711 File Offset: 0x000F0911
		public virtual IList<CustomAttributeNamedArgument> NamedArguments
		{
			get
			{
				this.ResolveArguments();
				return this.namedArgs;
			}
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x000F271F File Offset: 0x000F091F
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x000F271F File Offset: 0x000F091F
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x000F271F File Offset: 0x000F091F
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x000F271F File Offset: 0x000F091F
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x000F271F File Offset: 0x000F091F
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06004C04 RID: 19460 RVA: 0x000F2728 File Offset: 0x000F0928
		public Type AttributeType
		{
			get
			{
				return this.ctorInfo.DeclaringType;
			}
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x000F2738 File Offset: 0x000F0938
		public override string ToString()
		{
			this.ResolveArguments();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[" + this.ctorInfo.DeclaringType.FullName + "(");
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				stringBuilder.Append(this.ctorArgs[i].ToString());
				if (i + 1 < this.ctorArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			if (this.namedArgs.Count > 0)
			{
				stringBuilder.Append(", ");
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				stringBuilder.Append(this.namedArgs[j].ToString());
				if (j + 1 < this.namedArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.AppendFormat(")]", Array.Empty<object>());
			return stringBuilder.ToString();
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x000F2850 File Offset: 0x000F0A50
		private static T[] UnboxValues<T>(object[] values)
		{
			T[] array = new T[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = (T)((object)values[i]);
			}
			return array;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x000F2884 File Offset: 0x000F0A84
		public override bool Equals(object obj)
		{
			CustomAttributeData customAttributeData = obj as CustomAttributeData;
			if (customAttributeData == null || customAttributeData.ctorInfo != this.ctorInfo || customAttributeData.ctorArgs.Count != this.ctorArgs.Count || customAttributeData.namedArgs.Count != this.namedArgs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				if (this.ctorArgs[i].Equals(customAttributeData.ctorArgs[i]))
				{
					return false;
				}
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				bool flag = false;
				for (int k = 0; k < customAttributeData.namedArgs.Count; k++)
				{
					if (this.namedArgs[j].Equals(customAttributeData.namedArgs[k]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x000F2994 File Offset: 0x000F0B94
		public override int GetHashCode()
		{
			int num = ((this.ctorInfo == null) ? 13 : (this.ctorInfo.GetHashCode() << 16));
			if (this.ctorArgs != null)
			{
				for (int i = 0; i < this.ctorArgs.Count; i++)
				{
					num += num ^ (7 + this.ctorArgs[i].GetHashCode() << i * 4);
				}
			}
			if (this.namedArgs != null)
			{
				for (int j = 0; j < this.namedArgs.Count; j++)
				{
					num += this.namedArgs[j].GetHashCode() << 5;
				}
			}
			return num;
		}

		// Token: 0x04002FA3 RID: 12195
		private ConstructorInfo ctorInfo;

		// Token: 0x04002FA4 RID: 12196
		private IList<CustomAttributeTypedArgument> ctorArgs;

		// Token: 0x04002FA5 RID: 12197
		private IList<CustomAttributeNamedArgument> namedArgs;

		// Token: 0x04002FA6 RID: 12198
		private CustomAttributeData.LazyCAttrData lazyData;

		// Token: 0x020008BD RID: 2237
		private class LazyCAttrData
		{
			// Token: 0x06004C09 RID: 19465 RVA: 0x000025BE File Offset: 0x000007BE
			public LazyCAttrData()
			{
			}

			// Token: 0x04002FA7 RID: 12199
			internal Assembly assembly;

			// Token: 0x04002FA8 RID: 12200
			internal IntPtr data;

			// Token: 0x04002FA9 RID: 12201
			internal uint data_length;
		}
	}
}
