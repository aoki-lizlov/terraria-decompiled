using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using MonoGame.Utilities;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000106 RID: 262
	public sealed class ContentReader : BinaryReader
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00038570 File Offset: 0x00036770
		public ContentManager ContentManager
		{
			get
			{
				return this.contentManager;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00038578 File Offset: 0x00036778
		public string AssetName
		{
			get
			{
				return this.assetName;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00038580 File Offset: 0x00036780
		internal ContentTypeReader[] TypeReaders
		{
			get
			{
				return this.typeReaders;
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00038588 File Offset: 0x00036788
		internal ContentReader(ContentManager manager, Stream stream, string assetName, int version, char platform, Action<IDisposable> recordDisposableObject)
			: base(stream)
		{
			this.recordDisposableObject = recordDisposableObject;
			this.contentManager = manager;
			this.assetName = assetName;
			this.version = version;
			this.platform = platform;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000385B8 File Offset: 0x000367B8
		public T ReadExternalReference<T>()
		{
			string text = this.ReadString();
			if (!string.IsNullOrEmpty(text))
			{
				return this.contentManager.Load<T>(FileHelpers.ResolveRelativePath(this.assetName, text));
			}
			return default(T);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000385F8 File Offset: 0x000367F8
		public Matrix ReadMatrix()
		{
			return new Matrix
			{
				M11 = this.ReadSingle(),
				M12 = this.ReadSingle(),
				M13 = this.ReadSingle(),
				M14 = this.ReadSingle(),
				M21 = this.ReadSingle(),
				M22 = this.ReadSingle(),
				M23 = this.ReadSingle(),
				M24 = this.ReadSingle(),
				M31 = this.ReadSingle(),
				M32 = this.ReadSingle(),
				M33 = this.ReadSingle(),
				M34 = this.ReadSingle(),
				M41 = this.ReadSingle(),
				M42 = this.ReadSingle(),
				M43 = this.ReadSingle(),
				M44 = this.ReadSingle()
			};
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000386E0 File Offset: 0x000368E0
		public T ReadObject<T>()
		{
			return this.ReadObject<T>(default(T));
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000386FC File Offset: 0x000368FC
		public T ReadObject<T>(ContentTypeReader typeReader)
		{
			T t = (T)((object)typeReader.Read(this, default(T)));
			this.RecordDisposable<T>(t);
			return t;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0003872C File Offset: 0x0003692C
		public T ReadObject<T>(T existingInstance)
		{
			return this.InnerReadObject<T>(existingInstance);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00038738 File Offset: 0x00036938
		public T ReadObject<T>(ContentTypeReader typeReader, T existingInstance)
		{
			if (!typeReader.TargetType.IsValueType)
			{
				return this.ReadObject<T>(existingInstance);
			}
			T t = (T)((object)typeReader.Read(this, existingInstance));
			this.RecordDisposable<T>(t);
			return t;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00038778 File Offset: 0x00036978
		public Quaternion ReadQuaternion()
		{
			return new Quaternion
			{
				X = this.ReadSingle(),
				Y = this.ReadSingle(),
				Z = this.ReadSingle(),
				W = this.ReadSingle()
			};
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000387C4 File Offset: 0x000369C4
		public T ReadRawObject<T>()
		{
			return this.ReadRawObject<T>(default(T));
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000387E0 File Offset: 0x000369E0
		public T ReadRawObject<T>(ContentTypeReader typeReader)
		{
			return this.ReadRawObject<T>(typeReader, default(T));
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00038800 File Offset: 0x00036A00
		public T ReadRawObject<T>(T existingInstance)
		{
			Type typeFromHandle = typeof(T);
			foreach (ContentTypeReader contentTypeReader in this.typeReaders)
			{
				if (contentTypeReader.TargetType == typeFromHandle)
				{
					return this.ReadRawObject<T>(contentTypeReader, existingInstance);
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0003884D File Offset: 0x00036A4D
		public T ReadRawObject<T>(ContentTypeReader typeReader, T existingInstance)
		{
			return (T)((object)typeReader.Read(this, existingInstance));
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00038864 File Offset: 0x00036A64
		public void ReadSharedResource<T>(Action<T> fixup)
		{
			int num = this.Read7BitEncodedInt();
			if (num > 0)
			{
				this.sharedResourceFixups[num - 1].Add(delegate(object v)
				{
					if (!(v is T))
					{
						throw new ContentLoadException(string.Format("Error loading shared resource. Expected type {0}, received type {1}", typeof(T).Name, v.GetType().Name));
					}
					fixup((T)((object)v));
				});
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x000388A4 File Offset: 0x00036AA4
		public Vector2 ReadVector2()
		{
			return new Vector2
			{
				X = this.ReadSingle(),
				Y = this.ReadSingle()
			};
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000388D4 File Offset: 0x00036AD4
		public Vector3 ReadVector3()
		{
			return new Vector3
			{
				X = this.ReadSingle(),
				Y = this.ReadSingle(),
				Z = this.ReadSingle()
			};
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00038914 File Offset: 0x00036B14
		public Vector4 ReadVector4()
		{
			return new Vector4
			{
				X = this.ReadSingle(),
				Y = this.ReadSingle(),
				Z = this.ReadSingle(),
				W = this.ReadSingle()
			};
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00038960 File Offset: 0x00036B60
		public Color ReadColor()
		{
			return new Color
			{
				R = this.ReadByte(),
				G = this.ReadByte(),
				B = this.ReadByte(),
				A = this.ReadByte()
			};
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000389AA File Offset: 0x00036BAA
		internal object ReadAsset<T>()
		{
			this.InitializeTypeReaders();
			object obj = this.ReadObject<T>();
			this.ReadSharedResources();
			return obj;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000389C4 File Offset: 0x00036BC4
		internal void InitializeTypeReaders()
		{
			this.typeReaderManager = new ContentTypeReaderManager();
			this.typeReaders = this.typeReaderManager.LoadAssetReaders(this);
			this.sharedResourceCount = this.Read7BitEncodedInt();
			this.sharedResources = new object[this.sharedResourceCount];
			this.sharedResourceFixups = new List<Action<object>>[this.sharedResourceCount];
			for (int i = 0; i < this.sharedResourceCount; i++)
			{
				this.sharedResourceFixups[i] = new List<Action<object>>();
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00038A3C File Offset: 0x00036C3C
		internal void ReadSharedResources()
		{
			for (int i = 0; i < this.sharedResourceCount; i++)
			{
				this.sharedResources[i] = this.InnerReadObject<object>(null);
			}
			for (int j = 0; j < this.sharedResourceCount; j++)
			{
				object obj = this.sharedResources[j];
				foreach (Action<object> action in this.sharedResourceFixups[j])
				{
					action(obj);
				}
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00038ACC File Offset: 0x00036CCC
		internal new int Read7BitEncodedInt()
		{
			return base.Read7BitEncodedInt();
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00038AD4 File Offset: 0x00036CD4
		internal BoundingSphere ReadBoundingSphere()
		{
			Vector3 vector = this.ReadVector3();
			float num = this.ReadSingle();
			return new BoundingSphere(vector, num);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00038AF4 File Offset: 0x00036CF4
		private T InnerReadObject<T>(T existingInstance)
		{
			int num = this.Read7BitEncodedInt();
			if (num == 0)
			{
				return existingInstance;
			}
			if (num > this.typeReaders.Length)
			{
				throw new ContentLoadException("Incorrect type reader index found!");
			}
			T t = (T)((object)this.typeReaders[num - 1].Read(this, default(T)));
			this.RecordDisposable<T>(t);
			return t;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00038B50 File Offset: 0x00036D50
		private void RecordDisposable<T>(T result)
		{
			IDisposable disposable = result as IDisposable;
			if (disposable == null)
			{
				return;
			}
			if (this.recordDisposableObject != null)
			{
				this.recordDisposableObject(disposable);
				return;
			}
			this.contentManager.RecordDisposable(disposable);
		}

		// Token: 0x04000AA9 RID: 2729
		internal int version;

		// Token: 0x04000AAA RID: 2730
		internal char platform;

		// Token: 0x04000AAB RID: 2731
		private ContentManager contentManager;

		// Token: 0x04000AAC RID: 2732
		private Action<IDisposable> recordDisposableObject;

		// Token: 0x04000AAD RID: 2733
		private ContentTypeReaderManager typeReaderManager;

		// Token: 0x04000AAE RID: 2734
		private ContentTypeReader[] typeReaders;

		// Token: 0x04000AAF RID: 2735
		private string assetName;

		// Token: 0x04000AB0 RID: 2736
		private int sharedResourceCount;

		// Token: 0x04000AB1 RID: 2737
		private object[] sharedResources;

		// Token: 0x04000AB2 RID: 2738
		private List<Action<object>>[] sharedResourceFixups;

		// Token: 0x020003DA RID: 986
		[CompilerGenerated]
		private sealed class <>c__DisplayClass28_0<T>
		{
			// Token: 0x06001AF4 RID: 6900 RVA: 0x000136F5 File Offset: 0x000118F5
			public <>c__DisplayClass28_0()
			{
			}

			// Token: 0x06001AF5 RID: 6901 RVA: 0x0003F998 File Offset: 0x0003DB98
			internal void <ReadSharedResource>b__0(object v)
			{
				if (!(v is T))
				{
					throw new ContentLoadException(string.Format("Error loading shared resource. Expected type {0}, received type {1}", typeof(T).Name, v.GetType().Name));
				}
				this.fixup((T)((object)v));
			}

			// Token: 0x04001DE5 RID: 7653
			public Action<T> fixup;
		}
	}
}
