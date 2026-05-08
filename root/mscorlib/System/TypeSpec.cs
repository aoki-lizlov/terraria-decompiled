using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x02000238 RID: 568
	internal class TypeSpec
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0006880D File Offset: 0x00066A0D
		internal bool HasModifiers
		{
			get
			{
				return this.modifier_spec != null;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00068818 File Offset: 0x00066A18
		internal bool IsNested
		{
			get
			{
				return this.nested != null && this.nested.Count > 0;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00068832 File Offset: 0x00066A32
		internal bool IsByRef
		{
			get
			{
				return this.is_byref;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0006883A File Offset: 0x00066A3A
		internal TypeName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00068842 File Offset: 0x00066A42
		internal IEnumerable<TypeName> Nested
		{
			get
			{
				if (this.nested != null)
				{
					return this.nested;
				}
				return Array.Empty<TypeName>();
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x00068858 File Offset: 0x00066A58
		internal IEnumerable<ModifierSpec> Modifiers
		{
			get
			{
				if (this.modifier_spec != null)
				{
					return this.modifier_spec;
				}
				return Array.Empty<ModifierSpec>();
			}
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00068870 File Offset: 0x00066A70
		private string GetDisplayFullName(TypeSpec.DisplayNameFormat flags)
		{
			bool flag = (flags & TypeSpec.DisplayNameFormat.WANT_ASSEMBLY) > TypeSpec.DisplayNameFormat.Default;
			bool flag2 = (flags & TypeSpec.DisplayNameFormat.NO_MODIFIERS) == TypeSpec.DisplayNameFormat.Default;
			StringBuilder stringBuilder = new StringBuilder(this.name.DisplayName);
			if (this.nested != null)
			{
				foreach (TypeIdentifier typeIdentifier in this.nested)
				{
					stringBuilder.Append('+').Append(typeIdentifier.DisplayName);
				}
			}
			if (this.generic_params != null)
			{
				stringBuilder.Append('[');
				for (int i = 0; i < this.generic_params.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					if (this.generic_params[i].assembly_name != null)
					{
						stringBuilder.Append('[').Append(this.generic_params[i].DisplayFullName).Append(']');
					}
					else
					{
						stringBuilder.Append(this.generic_params[i].DisplayFullName);
					}
				}
				stringBuilder.Append(']');
			}
			if (flag2)
			{
				this.GetModifierString(stringBuilder);
			}
			if (this.assembly_name != null && flag)
			{
				stringBuilder.Append(", ").Append(this.assembly_name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000689CC File Offset: 0x00066BCC
		internal string ModifierString()
		{
			return this.GetModifierString(new StringBuilder()).ToString();
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000689E0 File Offset: 0x00066BE0
		private StringBuilder GetModifierString(StringBuilder sb)
		{
			if (this.modifier_spec != null)
			{
				foreach (ModifierSpec modifierSpec in this.modifier_spec)
				{
					modifierSpec.Append(sb);
				}
			}
			if (this.is_byref)
			{
				sb.Append('&');
			}
			return sb;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x00068A4C File Offset: 0x00066C4C
		internal string DisplayFullName
		{
			get
			{
				if (this.display_fullname == null)
				{
					this.display_fullname = this.GetDisplayFullName(TypeSpec.DisplayNameFormat.Default);
				}
				return this.display_fullname;
			}
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00068A6C File Offset: 0x00066C6C
		internal static TypeSpec Parse(string typeName)
		{
			int num = 0;
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			TypeSpec typeSpec = TypeSpec.Parse(typeName, ref num, false, true);
			if (num < typeName.Length)
			{
				throw new ArgumentException("Count not parse the whole type name", "typeName");
			}
			return typeSpec;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00068AAC File Offset: 0x00066CAC
		internal static string EscapeDisplayName(string internalName)
		{
			StringBuilder stringBuilder = new StringBuilder(internalName.Length);
			int i = 0;
			while (i < internalName.Length)
			{
				char c = internalName[i];
				switch (c)
				{
				case '&':
				case '*':
				case '+':
				case ',':
					goto IL_0056;
				case '\'':
				case '(':
				case ')':
					goto IL_0067;
				default:
					switch (c)
					{
					case '[':
					case '\\':
					case ']':
						goto IL_0056;
					default:
						goto IL_0067;
					}
					break;
				}
				IL_006F:
				i++;
				continue;
				IL_0056:
				stringBuilder.Append('\\').Append(c);
				goto IL_006F;
				IL_0067:
				stringBuilder.Append(c);
				goto IL_006F;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00068B3C File Offset: 0x00066D3C
		internal static string UnescapeInternalName(string displayName)
		{
			StringBuilder stringBuilder = new StringBuilder(displayName.Length);
			for (int i = 0; i < displayName.Length; i++)
			{
				char c = displayName[i];
				if (c == '\\' && ++i < displayName.Length)
				{
					c = displayName[i];
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00068B98 File Offset: 0x00066D98
		internal static bool NeedsEscaping(string internalName)
		{
			foreach (char c in internalName)
			{
				switch (c)
				{
				case '&':
				case '*':
				case '+':
				case ',':
					return true;
				case '\'':
				case '(':
				case ')':
					break;
				default:
					switch (c)
					{
					case '[':
					case '\\':
					case ']':
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00068C00 File Offset: 0x00066E00
		internal Type Resolve(Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			Assembly assembly = null;
			if (assemblyResolver == null && typeResolver == null)
			{
				return RuntimeType.GetType(this.DisplayFullName, throwOnError, ignoreCase, false, ref stackMark);
			}
			if (this.assembly_name != null)
			{
				if (assemblyResolver != null)
				{
					assembly = assemblyResolver(new AssemblyName(this.assembly_name));
				}
				else
				{
					assembly = Assembly.Load(this.assembly_name);
				}
				if (assembly == null)
				{
					if (throwOnError)
					{
						throw new FileNotFoundException("Could not resolve assembly '" + this.assembly_name + "'");
					}
					return null;
				}
			}
			Type type = null;
			if (typeResolver != null)
			{
				type = typeResolver(assembly, this.name.DisplayName, ignoreCase);
			}
			else
			{
				type = assembly.GetType(this.name.DisplayName, false, ignoreCase);
			}
			if (!(type == null))
			{
				if (this.nested != null)
				{
					foreach (TypeIdentifier typeIdentifier in this.nested)
					{
						Type nestedType = type.GetNestedType(typeIdentifier.DisplayName, BindingFlags.Public | BindingFlags.NonPublic);
						if (nestedType == null)
						{
							if (throwOnError)
							{
								string text = "Could not resolve type '";
								TypeIdentifier typeIdentifier2 = typeIdentifier;
								throw new TypeLoadException(text + ((typeIdentifier2 != null) ? typeIdentifier2.ToString() : null) + "'");
							}
							return null;
						}
						else
						{
							type = nestedType;
						}
					}
				}
				if (this.generic_params != null)
				{
					Type[] array = new Type[this.generic_params.Count];
					int i = 0;
					while (i < array.Length)
					{
						Type type2 = this.generic_params[i].Resolve(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
						if (type2 == null)
						{
							if (throwOnError)
							{
								string text2 = "Could not resolve type '";
								TypeIdentifier typeIdentifier3 = this.generic_params[i].name;
								throw new TypeLoadException(text2 + ((typeIdentifier3 != null) ? typeIdentifier3.ToString() : null) + "'");
							}
							return null;
						}
						else
						{
							array[i] = type2;
							i++;
						}
					}
					type = type.MakeGenericType(array);
				}
				if (this.modifier_spec != null)
				{
					foreach (ModifierSpec modifierSpec in this.modifier_spec)
					{
						type = modifierSpec.Resolve(type);
					}
				}
				if (this.is_byref)
				{
					type = type.MakeByRefType();
				}
				return type;
			}
			if (throwOnError)
			{
				string text3 = "Could not resolve type '";
				TypeIdentifier typeIdentifier4 = this.name;
				throw new TypeLoadException(text3 + ((typeIdentifier4 != null) ? typeIdentifier4.ToString() : null) + "'");
			}
			return null;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00068E6C File Offset: 0x0006706C
		private void AddName(string type_name)
		{
			if (this.name == null)
			{
				this.name = TypeSpec.ParsedTypeIdentifier(type_name);
				return;
			}
			if (this.nested == null)
			{
				this.nested = new List<TypeIdentifier>();
			}
			this.nested.Add(TypeSpec.ParsedTypeIdentifier(type_name));
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00068EA7 File Offset: 0x000670A7
		private void AddModifier(ModifierSpec md)
		{
			if (this.modifier_spec == null)
			{
				this.modifier_spec = new List<ModifierSpec>();
			}
			this.modifier_spec.Add(md);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00068EC8 File Offset: 0x000670C8
		private static void SkipSpace(string name, ref int pos)
		{
			int num = pos;
			while (num < name.Length && char.IsWhiteSpace(name[num]))
			{
				num++;
			}
			pos = num;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00068EF8 File Offset: 0x000670F8
		private static void BoundCheck(int idx, string s)
		{
			if (idx >= s.Length)
			{
				throw new ArgumentException("Invalid generic arguments spec", "typeName");
			}
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00068F13 File Offset: 0x00067113
		private static TypeIdentifier ParsedTypeIdentifier(string displayName)
		{
			return TypeIdentifiers.FromDisplay(displayName);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00068F1C File Offset: 0x0006711C
		private static TypeSpec Parse(string name, ref int p, bool is_recurse, bool allow_aqn)
		{
			int i = p;
			bool flag = false;
			TypeSpec typeSpec = new TypeSpec();
			TypeSpec.SkipSpace(name, ref i);
			int num = i;
			while (i < name.Length)
			{
				char c = name[i];
				switch (c)
				{
				case '&':
				case '*':
					goto IL_0098;
				case '\'':
				case '(':
				case ')':
					break;
				case '+':
					typeSpec.AddName(name.Substring(num, i - num));
					num = i + 1;
					break;
				case ',':
					goto IL_0077;
				default:
					switch (c)
					{
					case '[':
						goto IL_0098;
					case '\\':
						i++;
						break;
					case ']':
						goto IL_0077;
					}
					break;
				}
				IL_00D6:
				if (!flag)
				{
					i++;
					continue;
				}
				break;
				IL_0077:
				typeSpec.AddName(name.Substring(num, i - num));
				num = i + 1;
				flag = true;
				if (is_recurse && !allow_aqn)
				{
					p = i;
					return typeSpec;
				}
				goto IL_00D6;
				IL_0098:
				if (name[i] != '[' && is_recurse)
				{
					throw new ArgumentException("Generic argument can't be byref or pointer type", "typeName");
				}
				typeSpec.AddName(name.Substring(num, i - num));
				num = i + 1;
				flag = true;
				goto IL_00D6;
			}
			if (num < i)
			{
				typeSpec.AddName(name.Substring(num, i - num));
			}
			else if (num == i)
			{
				typeSpec.AddName(string.Empty);
			}
			if (flag)
			{
				while (i < name.Length)
				{
					char c = name[i];
					if (c <= '*')
					{
						if (c != '&')
						{
							if (c != '*')
							{
								goto IL_04BE;
							}
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Can't have a pointer to a byref type", "typeName");
							}
							int num2 = 1;
							while (i + 1 < name.Length && name[i + 1] == '*')
							{
								i++;
								num2++;
							}
							typeSpec.AddModifier(new PointerSpec(num2));
						}
						else
						{
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Can't have a byref of a byref", "typeName");
							}
							typeSpec.is_byref = true;
						}
					}
					else if (c != ',')
					{
						if (c != '[')
						{
							if (c != ']')
							{
								goto IL_04BE;
							}
							if (is_recurse)
							{
								p = i;
								return typeSpec;
							}
							throw new ArgumentException("Unmatched ']'", "typeName");
						}
						else
						{
							if (typeSpec.is_byref)
							{
								throw new ArgumentException("Byref qualifier must be the last one of a type", "typeName");
							}
							i++;
							if (i >= name.Length)
							{
								throw new ArgumentException("Invalid array/generic spec", "typeName");
							}
							TypeSpec.SkipSpace(name, ref i);
							if (name[i] != ',' && name[i] != '*' && name[i] != ']')
							{
								List<TypeSpec> list = new List<TypeSpec>();
								if (typeSpec.HasModifiers)
								{
									throw new ArgumentException("generic args after array spec or pointer type", "typeName");
								}
								while (i < name.Length)
								{
									TypeSpec.SkipSpace(name, ref i);
									bool flag2 = name[i] == '[';
									if (flag2)
									{
										i++;
									}
									list.Add(TypeSpec.Parse(name, ref i, true, flag2));
									TypeSpec.BoundCheck(i, name);
									if (flag2)
									{
										if (name[i] != ']')
										{
											throw new ArgumentException("Unclosed assembly-qualified type name at " + name[i].ToString(), "typeName");
										}
										i++;
										TypeSpec.BoundCheck(i, name);
									}
									if (name[i] == ']')
									{
										break;
									}
									if (name[i] != ',')
									{
										throw new ArgumentException("Invalid generic arguments separator " + name[i].ToString(), "typeName");
									}
									i++;
								}
								if (i >= name.Length || name[i] != ']')
								{
									throw new ArgumentException("Error parsing generic params spec", "typeName");
								}
								typeSpec.generic_params = list;
							}
							else
							{
								int num3 = 1;
								bool flag3 = false;
								while (i < name.Length && name[i] != ']')
								{
									if (name[i] == '*')
									{
										if (flag3)
										{
											throw new ArgumentException("Array spec cannot have 2 bound dimensions", "typeName");
										}
										flag3 = true;
									}
									else
									{
										if (name[i] != ',')
										{
											throw new ArgumentException("Invalid character in array spec " + name[i].ToString(), "typeName");
										}
										num3++;
									}
									i++;
									TypeSpec.SkipSpace(name, ref i);
								}
								if (i >= name.Length || name[i] != ']')
								{
									throw new ArgumentException("Error parsing array spec", "typeName");
								}
								if (num3 > 1 && flag3)
								{
									throw new ArgumentException("Invalid array spec, multi-dimensional array cannot be bound", "typeName");
								}
								typeSpec.AddModifier(new ArraySpec(num3, flag3));
							}
						}
					}
					else if (is_recurse && allow_aqn)
					{
						int num4 = i;
						while (num4 < name.Length && name[num4] != ']')
						{
							num4++;
						}
						if (num4 >= name.Length)
						{
							throw new ArgumentException("Unmatched ']' while parsing generic argument assembly name");
						}
						typeSpec.assembly_name = name.Substring(i + 1, num4 - i - 1).Trim();
						p = num4;
						return typeSpec;
					}
					else
					{
						if (is_recurse)
						{
							p = i;
							return typeSpec;
						}
						if (allow_aqn)
						{
							typeSpec.assembly_name = name.Substring(i + 1).Trim();
							i = name.Length;
						}
					}
					i++;
					continue;
					IL_04BE:
					throw new ArgumentException("Bad type def, can't handle '" + name[i].ToString() + "' at " + i.ToString(), "typeName");
				}
			}
			p = i;
			return typeSpec;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0006942C File Offset: 0x0006762C
		internal TypeName TypeNameWithoutModifiers()
		{
			return new TypeSpec.TypeSpecTypeName(this, false);
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x00069435 File Offset: 0x00067635
		internal TypeName TypeName
		{
			get
			{
				return new TypeSpec.TypeSpecTypeName(this, true);
			}
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000025BE File Offset: 0x000007BE
		public TypeSpec()
		{
		}

		// Token: 0x0400187B RID: 6267
		private TypeIdentifier name;

		// Token: 0x0400187C RID: 6268
		private string assembly_name;

		// Token: 0x0400187D RID: 6269
		private List<TypeIdentifier> nested;

		// Token: 0x0400187E RID: 6270
		private List<TypeSpec> generic_params;

		// Token: 0x0400187F RID: 6271
		private List<ModifierSpec> modifier_spec;

		// Token: 0x04001880 RID: 6272
		private bool is_byref;

		// Token: 0x04001881 RID: 6273
		private string display_fullname;

		// Token: 0x02000239 RID: 569
		[Flags]
		internal enum DisplayNameFormat
		{
			// Token: 0x04001883 RID: 6275
			Default = 0,
			// Token: 0x04001884 RID: 6276
			WANT_ASSEMBLY = 1,
			// Token: 0x04001885 RID: 6277
			NO_MODIFIERS = 2
		}

		// Token: 0x0200023A RID: 570
		private class TypeSpecTypeName : TypeNames.ATypeName, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001BCD RID: 7117 RVA: 0x0006943E File Offset: 0x0006763E
			internal TypeSpecTypeName(TypeSpec ts, bool wantModifiers)
			{
				this.ts = ts;
				this.want_modifiers = wantModifiers;
			}

			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00069454 File Offset: 0x00067654
			public override string DisplayName
			{
				get
				{
					if (this.want_modifiers)
					{
						return this.ts.DisplayFullName;
					}
					return this.ts.GetDisplayFullName(TypeSpec.DisplayNameFormat.NO_MODIFIERS);
				}
			}

			// Token: 0x06001BCF RID: 7119 RVA: 0x00068679 File Offset: 0x00066879
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04001886 RID: 6278
			private TypeSpec ts;

			// Token: 0x04001887 RID: 6279
			private bool want_modifiers;
		}
	}
}
