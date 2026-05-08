using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200067A RID: 1658
	[ComVisible(true)]
	public sealed class BinaryFormatter : IRemotingFormatter, IFormatter
	{
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x000D8267 File Offset: 0x000D6467
		// (set) Token: 0x06003E32 RID: 15922 RVA: 0x000D826F File Offset: 0x000D646F
		public FormatterTypeStyle TypeFormat
		{
			get
			{
				return this.m_typeFormat;
			}
			set
			{
				this.m_typeFormat = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x000D8278 File Offset: 0x000D6478
		// (set) Token: 0x06003E34 RID: 15924 RVA: 0x000D8280 File Offset: 0x000D6480
		public FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this.m_assemblyFormat;
			}
			set
			{
				this.m_assemblyFormat = value;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x000D8289 File Offset: 0x000D6489
		// (set) Token: 0x06003E36 RID: 15926 RVA: 0x000D8291 File Offset: 0x000D6491
		public TypeFilterLevel FilterLevel
		{
			get
			{
				return this.m_securityLevel;
			}
			set
			{
				this.m_securityLevel = value;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x000D829A File Offset: 0x000D649A
		// (set) Token: 0x06003E38 RID: 15928 RVA: 0x000D82A2 File Offset: 0x000D64A2
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.m_surrogates;
			}
			set
			{
				this.m_surrogates = value;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003E39 RID: 15929 RVA: 0x000D82AB File Offset: 0x000D64AB
		// (set) Token: 0x06003E3A RID: 15930 RVA: 0x000D82B3 File Offset: 0x000D64B3
		public SerializationBinder Binder
		{
			get
			{
				return this.m_binder;
			}
			set
			{
				this.m_binder = value;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x000D82BC File Offset: 0x000D64BC
		// (set) Token: 0x06003E3C RID: 15932 RVA: 0x000D82C4 File Offset: 0x000D64C4
		public StreamingContext Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				this.m_context = value;
			}
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x000D82CD File Offset: 0x000D64CD
		public BinaryFormatter()
		{
			this.m_surrogates = null;
			this.m_context = new StreamingContext(StreamingContextStates.All);
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000D82FA File Offset: 0x000D64FA
		public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
		{
			this.m_surrogates = selector;
			this.m_context = context;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x000D831E File Offset: 0x000D651E
		public object Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream, null);
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x000D8328 File Offset: 0x000D6528
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck)
		{
			return this.Deserialize(serializationStream, handler, fCheck, null);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x000D8334 File Offset: 0x000D6534
		[SecuritySafeCritical]
		public object Deserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, true);
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x000D833F File Offset: 0x000D653F
		[SecuritySafeCritical]
		public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, true, methodCallMessage);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x000D834B File Offset: 0x000D654B
		[SecurityCritical]
		[ComVisible(false)]
		public object UnsafeDeserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, false);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000D8356 File Offset: 0x000D6556
		[SecurityCritical]
		[ComVisible(false)]
		public object UnsafeDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, false, methodCallMessage);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x000D8362 File Offset: 0x000D6562
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, fCheck, false, methodCallMessage);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000D8370 File Offset: 0x000D6570
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", Environment.GetResourceString("Parameter '{0}' cannot be null.", new object[] { serializationStream }));
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException(Environment.GetResourceString("Attempting to deserialize an empty stream."));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			internalFE.FEsecurityLevel = this.m_securityLevel;
			ObjectReader objectReader = new ObjectReader(serializationStream, this.m_surrogates, this.m_context, internalFE, this.m_binder);
			objectReader.crossAppDomainArray = this.m_crossAppDomainArray;
			return objectReader.Deserialize(handler, new __BinaryParser(serializationStream, objectReader), fCheck, isCrossAppDomain, methodCallMessage);
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000D8429 File Offset: 0x000D6629
		public void Serialize(Stream serializationStream, object graph)
		{
			this.Serialize(serializationStream, graph, null);
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x000D8434 File Offset: 0x000D6634
		[SecuritySafeCritical]
		public void Serialize(Stream serializationStream, object graph, Header[] headers)
		{
			this.Serialize(serializationStream, graph, headers, true);
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x000D8440 File Offset: 0x000D6640
		[SecurityCritical]
		internal void Serialize(Stream serializationStream, object graph, Header[] headers, bool fCheck)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", Environment.GetResourceString("Parameter '{0}' cannot be null.", new object[] { serializationStream }));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			ObjectWriter objectWriter = new ObjectWriter(this.m_surrogates, this.m_context, internalFE, this.m_binder);
			__BinaryWriter _BinaryWriter = new __BinaryWriter(serializationStream, objectWriter, this.m_typeFormat);
			objectWriter.Serialize(graph, headers, _BinaryWriter, fCheck);
			this.m_crossAppDomainArray = objectWriter.crossAppDomainArray;
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x000D84D4 File Offset: 0x000D66D4
		internal static TypeInformation GetTypeInformation(Type type)
		{
			Dictionary<Type, TypeInformation> dictionary = BinaryFormatter.typeNameCache;
			TypeInformation typeInformation2;
			lock (dictionary)
			{
				TypeInformation typeInformation = null;
				if (!BinaryFormatter.typeNameCache.TryGetValue(type, out typeInformation))
				{
					bool flag2;
					string clrAssemblyName = FormatterServices.GetClrAssemblyName(type, out flag2);
					typeInformation = new TypeInformation(FormatterServices.GetClrTypeFullName(type), clrAssemblyName, flag2);
					BinaryFormatter.typeNameCache.Add(type, typeInformation);
				}
				typeInformation2 = typeInformation;
			}
			return typeInformation2;
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x000D8548 File Offset: 0x000D6748
		// Note: this type is marked as 'beforefieldinit'.
		static BinaryFormatter()
		{
		}

		// Token: 0x04002852 RID: 10322
		internal ISurrogateSelector m_surrogates;

		// Token: 0x04002853 RID: 10323
		internal StreamingContext m_context;

		// Token: 0x04002854 RID: 10324
		internal SerializationBinder m_binder;

		// Token: 0x04002855 RID: 10325
		internal FormatterTypeStyle m_typeFormat = FormatterTypeStyle.TypesAlways;

		// Token: 0x04002856 RID: 10326
		internal FormatterAssemblyStyle m_assemblyFormat;

		// Token: 0x04002857 RID: 10327
		internal TypeFilterLevel m_securityLevel = TypeFilterLevel.Full;

		// Token: 0x04002858 RID: 10328
		internal object[] m_crossAppDomainArray;

		// Token: 0x04002859 RID: 10329
		private static Dictionary<Type, TypeInformation> typeNameCache = new Dictionary<Type, TypeInformation>();
	}
}
