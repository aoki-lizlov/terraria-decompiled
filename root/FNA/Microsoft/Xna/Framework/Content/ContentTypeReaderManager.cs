using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000146 RID: 326
	public sealed class ContentTypeReaderManager
	{
		// Token: 0x060017B9 RID: 6073 RVA: 0x0003AB14 File Offset: 0x00038D14
		static ContentTypeReaderManager()
		{
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000136F5 File Offset: 0x000118F5
		internal ContentTypeReaderManager()
		{
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0003AB78 File Offset: 0x00038D78
		public ContentTypeReader GetTypeReader(Type targetType)
		{
			ContentTypeReader contentTypeReader;
			if (this.contentReaders.TryGetValue(targetType, out contentTypeReader))
			{
				return contentTypeReader;
			}
			Type type = Type.GetType(ContentTypeReaderManager.PrepareType(targetType.FullName), false);
			if (type != null && this.contentReaders.TryGetValue(type, out contentTypeReader))
			{
				return contentTypeReader;
			}
			return null;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0003ABC8 File Offset: 0x00038DC8
		internal ContentTypeReader[] LoadAssetReaders(ContentReader reader)
		{
			if (ContentTypeReaderManager.falseflag)
			{
				new ByteReader();
				new SByteReader();
				new DateTimeReader();
				new DecimalReader();
				new BoundingSphereReader();
				new BoundingFrustumReader();
				new RayReader();
				new ListReader<char>();
				new ListReader<Rectangle>();
				new ArrayReader<Rectangle>();
				new ListReader<Vector3>();
				new ListReader<StringReader>();
				new ListReader<int>();
				new SpriteFontReader();
				new Texture2DReader();
				new CharReader();
				new RectangleReader();
				new StringReader();
				new Vector2Reader();
				new Vector3Reader();
				new Vector4Reader();
				new CurveReader();
				new IndexBufferReader();
				new BoundingBoxReader();
				new MatrixReader();
				new BasicEffectReader();
				new VertexBufferReader();
				new AlphaTestEffectReader();
				new EnumReader<SpriteEffects>();
				new ArrayReader<float>();
				new ArrayReader<Vector2>();
				new ListReader<Vector2>();
				new ArrayReader<Matrix>();
				new EnumReader<Blend>();
				new NullableReader<Rectangle>();
				new EffectMaterialReader();
				new ExternalReferenceReader();
				new SoundEffectReader();
				new SongReader();
				new ModelReader();
				new Int32Reader();
			}
			int num = reader.Read7BitEncodedInt();
			ContentTypeReader[] array = new ContentTypeReader[num];
			BitArray bitArray = new BitArray(num);
			this.contentReaders = new Dictionary<Type, ContentTypeReader>(num);
			object obj = ContentTypeReaderManager.locker;
			lock (obj)
			{
				for (int i = 0; i < num; i++)
				{
					string text = reader.ReadString();
					Func<ContentTypeReader> func;
					if (ContentTypeReaderManager.typeCreators.TryGetValue(text, out func))
					{
						array[i] = func();
						bitArray[i] = true;
					}
					else
					{
						string text2 = text;
						text2 = ContentTypeReaderManager.PrepareType(text2);
						Type type = Type.GetType(text2);
						if (!(type != null))
						{
							throw new ContentLoadException(string.Concat(new string[] { "Could not find ContentTypeReader Type. Please ensure the name of the Assembly that contains the Type matches the assembly in the full type name: ", text, " (", text2, ")" }));
						}
						ContentTypeReader contentTypeReader;
						if (!ContentTypeReaderManager.contentReadersCache.TryGetValue(type, out contentTypeReader))
						{
							try
							{
								contentTypeReader = type.GetDefaultConstructor().Invoke(null) as ContentTypeReader;
							}
							catch (TargetInvocationException ex)
							{
								throw new InvalidOperationException("Failed to get default constructor for ContentTypeReader. To work around, add a creation function to ContentTypeReaderManager.AddTypeCreator() with the following failed type string: " + text, ex);
							}
							catch (NullReferenceException ex2)
							{
								throw new InvalidOperationException("Failed to get default constructor for ContentTypeReader. If you're using .NET Native AOT, ensure your rd.xml contains the following type: " + text, ex2);
							}
							bitArray[i] = true;
							ContentTypeReaderManager.contentReadersCache.Add(type, contentTypeReader);
						}
						array[i] = contentTypeReader;
					}
					if (array[i].TargetType != null)
					{
						this.contentReaders.Add(array[i].TargetType, array[i]);
					}
					reader.ReadInt32();
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (bitArray.Get(j))
					{
						array[j].Initialize(this);
					}
				}
			}
			return array;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0003AEC4 File Offset: 0x000390C4
		internal static void AddTypeCreator(string typeString, Func<ContentTypeReader> createFunction)
		{
			if (!ContentTypeReaderManager.typeCreators.ContainsKey(typeString))
			{
				ContentTypeReaderManager.typeCreators.Add(typeString, createFunction);
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0003AEDF File Offset: 0x000390DF
		internal static void ClearTypeCreators()
		{
			ContentTypeReaderManager.typeCreators.Clear();
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0003AEEC File Offset: 0x000390EC
		internal static string PrepareType(string type)
		{
			int num = type.Split(ContentTypeReaderManager.nestedMark, StringSplitOptions.None).Length - 1;
			string text = type;
			for (int i = 0; i < num; i++)
			{
				text = Regex.Replace(text, "\\[(.+?), Version=.+?\\]", "[$1]");
			}
			if (text.Contains("PublicKeyToken"))
			{
				text = Regex.Replace(text, "(.+?), Version=.+?$", "$1");
			}
			text = text.Replace(", Microsoft.Xna.Framework.Graphics", string.Format(", {0}", ContentTypeReaderManager.assemblyName));
			text = text.Replace(", Microsoft.Xna.Framework.Video", string.Format(", {0}", ContentTypeReaderManager.assemblyName));
			text = text.Replace(", Microsoft.Xna.Framework", string.Format(", {0}", ContentTypeReaderManager.assemblyName));
			return text.Replace(", MonoGame.Framework", string.Format(", {0}", ContentTypeReaderManager.assemblyName));
		}

		// Token: 0x04000ACA RID: 2762
		private Dictionary<Type, ContentTypeReader> contentReaders;

		// Token: 0x04000ACB RID: 2763
		private static readonly object locker = new object();

		// Token: 0x04000ACC RID: 2764
		private static readonly string assemblyName = typeof(ContentTypeReaderManager).Assembly.FullName;

		// Token: 0x04000ACD RID: 2765
		private static readonly Dictionary<Type, ContentTypeReader> contentReadersCache = new Dictionary<Type, ContentTypeReader>(255);

		// Token: 0x04000ACE RID: 2766
		private static readonly string[] nestedMark = new string[] { "[[" };

		// Token: 0x04000ACF RID: 2767
		private static bool falseflag = false;

		// Token: 0x04000AD0 RID: 2768
		private static Dictionary<string, Func<ContentTypeReader>> typeCreators = new Dictionary<string, Func<ContentTypeReader>>();
	}
}
