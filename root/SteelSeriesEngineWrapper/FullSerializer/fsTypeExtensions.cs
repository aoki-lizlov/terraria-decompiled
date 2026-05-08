using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FullSerializer
{
	// Token: 0x0200001A RID: 26
	public static class fsTypeExtensions
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000051C7 File Offset: 0x000033C7
		public static string CSharpName(this Type type)
		{
			return type.CSharpName(false);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000051D0 File Offset: 0x000033D0
		public static string CSharpName(this Type type, bool includeNamespace, bool ensureSafeDeclarationName)
		{
			string text = type.CSharpName(includeNamespace);
			if (ensureSafeDeclarationName)
			{
				text = text.Replace('>', '_').Replace('<', '_').Replace('.', '_');
			}
			return text;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005208 File Offset: 0x00003408
		public static string CSharpName(this Type type, bool includeNamespace)
		{
			if (type == typeof(void))
			{
				return "void";
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(float))
			{
				return "float";
			}
			if (type == typeof(bool))
			{
				return "bool";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(string))
			{
				return "string";
			}
			if (type.IsGenericParameter)
			{
				return type.ToString();
			}
			string text = "";
			IEnumerable<Type> enumerable = type.GetGenericArguments();
			if (type.IsNested)
			{
				text = text + type.DeclaringType.CSharpName() + ".";
				if (type.DeclaringType.GetGenericArguments().Length != 0)
				{
					enumerable = Enumerable.Skip<Type>(enumerable, type.DeclaringType.GetGenericArguments().Length);
				}
			}
			if (!Enumerable.Any<Type>(enumerable))
			{
				text += type.Name;
			}
			else
			{
				text += type.Name.Substring(0, type.Name.IndexOf('`'));
				text = text + "<" + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<Type, string>(enumerable, (Type t) => t.CSharpName(includeNamespace)))) + ">";
			}
			if (includeNamespace && type.Namespace != null)
			{
				text = type.Namespace + "." + text;
			}
			return text;
		}

		// Token: 0x020000B5 RID: 181
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x06000296 RID: 662 RVA: 0x00002493 File Offset: 0x00000693
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06000297 RID: 663 RVA: 0x00009C51 File Offset: 0x00007E51
			internal string <CSharpName>b__0(Type t)
			{
				return t.CSharpName(this.includeNamespace);
			}

			// Token: 0x0400024E RID: 590
			public bool includeNamespace;
		}
	}
}
