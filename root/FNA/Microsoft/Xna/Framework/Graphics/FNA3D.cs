using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using ObjCRuntime;
using SDL2;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000098 RID: 152
	[SuppressUnmanagedCodeSecurity]
	public static class FNA3D
	{
		// Token: 0x060012AA RID: 4778
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint FNA3D_LinkedVersion();

		// Token: 0x060012AB RID: 4779
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_HookLogFunctions(FNA3D.FNA3D_LogFunc info, FNA3D.FNA3D_LogFunc warn, FNA3D.FNA3D_LogFunc error);

		// Token: 0x060012AC RID: 4780
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint FNA3D_PrepareWindowAttributes();

		// Token: 0x060012AD RID: 4781
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetDrawableSize(IntPtr window, out int w, out int h);

		// Token: 0x060012AE RID: 4782
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateDevice(ref FNA3D.FNA3D_PresentationParameters presentationParameters, byte debugMode);

		// Token: 0x060012AF RID: 4783
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_DestroyDevice(IntPtr device);

		// Token: 0x060012B0 RID: 4784
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SwapBuffers(IntPtr device, ref Rectangle sourceRectangle, ref Rectangle destinationRectangle, IntPtr overrideWindowHandle);

		// Token: 0x060012B1 RID: 4785
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SwapBuffers(IntPtr device, IntPtr sourceRectangle, IntPtr destinationRectangle, IntPtr overrideWindowHandle);

		// Token: 0x060012B2 RID: 4786
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SwapBuffers(IntPtr device, ref Rectangle sourceRectangle, IntPtr destinationRectangle, IntPtr overrideWindowHandle);

		// Token: 0x060012B3 RID: 4787
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SwapBuffers(IntPtr device, IntPtr sourceRectangle, ref Rectangle destinationRectangle, IntPtr overrideWindowHandle);

		// Token: 0x060012B4 RID: 4788
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_Clear(IntPtr device, ClearOptions options, ref Vector4 color, float depth, int stencil);

		// Token: 0x060012B5 RID: 4789
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_DrawIndexedPrimitives(IntPtr device, PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, IntPtr indices, IndexElementSize indexElementSize);

		// Token: 0x060012B6 RID: 4790
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_DrawInstancedPrimitives(IntPtr device, PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, int instanceCount, IntPtr indices, IndexElementSize indexElementSize);

		// Token: 0x060012B7 RID: 4791
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_DrawPrimitives(IntPtr device, PrimitiveType primitiveType, int vertexStart, int primitiveCount);

		// Token: 0x060012B8 RID: 4792
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetViewport(IntPtr device, ref FNA3D.FNA3D_Viewport viewport);

		// Token: 0x060012B9 RID: 4793
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetScissorRect(IntPtr device, ref Rectangle scissor);

		// Token: 0x060012BA RID: 4794
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetBlendFactor(IntPtr device, out Color blendFactor);

		// Token: 0x060012BB RID: 4795
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetBlendFactor(IntPtr device, ref Color blendFactor);

		// Token: 0x060012BC RID: 4796
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_GetMultiSampleMask(IntPtr device);

		// Token: 0x060012BD RID: 4797
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetMultiSampleMask(IntPtr device, int mask);

		// Token: 0x060012BE RID: 4798
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_GetReferenceStencil(IntPtr device);

		// Token: 0x060012BF RID: 4799
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetReferenceStencil(IntPtr device, int reference);

		// Token: 0x060012C0 RID: 4800
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetBlendState(IntPtr device, ref FNA3D.FNA3D_BlendState blendState);

		// Token: 0x060012C1 RID: 4801
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetDepthStencilState(IntPtr device, ref FNA3D.FNA3D_DepthStencilState depthStencilState);

		// Token: 0x060012C2 RID: 4802
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ApplyRasterizerState(IntPtr device, ref FNA3D.FNA3D_RasterizerState rasterizerState);

		// Token: 0x060012C3 RID: 4803
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_VerifySampler(IntPtr device, int index, IntPtr texture, ref FNA3D.FNA3D_SamplerState sampler);

		// Token: 0x060012C4 RID: 4804
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_VerifyVertexSampler(IntPtr device, int index, IntPtr texture, ref FNA3D.FNA3D_SamplerState sampler);

		// Token: 0x060012C5 RID: 4805
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern void FNA3D_ApplyVertexBufferBindings(IntPtr device, FNA3D.FNA3D_VertexBufferBinding* bindings, int numBindings, byte bindingsUpdated, int baseVertex);

		// Token: 0x060012C6 RID: 4806
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetRenderTargets(IntPtr device, IntPtr renderTargets, int numRenderTargets, IntPtr depthStencilBuffer, DepthFormat depthFormat, byte preserveDepthStencilContents);

		// Token: 0x060012C7 RID: 4807
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern void FNA3D_SetRenderTargets(IntPtr device, FNA3D.FNA3D_RenderTargetBinding* renderTargets, int numRenderTargets, IntPtr depthStencilBuffer, DepthFormat depthFormat, byte preserveDepthStencilContents);

		// Token: 0x060012C8 RID: 4808
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ResolveTarget(IntPtr device, ref FNA3D.FNA3D_RenderTargetBinding target);

		// Token: 0x060012C9 RID: 4809
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ResetBackbuffer(IntPtr device, ref FNA3D.FNA3D_PresentationParameters presentationParameters);

		// Token: 0x060012CA RID: 4810
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ReadBackbuffer(IntPtr device, int x, int y, int w, int h, IntPtr data, int dataLen);

		// Token: 0x060012CB RID: 4811
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetBackbufferSize(IntPtr device, out int w, out int h);

		// Token: 0x060012CC RID: 4812
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern SurfaceFormat FNA3D_GetBackbufferSurfaceFormat(IntPtr device);

		// Token: 0x060012CD RID: 4813
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern DepthFormat FNA3D_GetBackbufferDepthFormat(IntPtr device);

		// Token: 0x060012CE RID: 4814
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_GetBackbufferMultiSampleCount(IntPtr device);

		// Token: 0x060012CF RID: 4815
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateTexture2D(IntPtr device, SurfaceFormat format, int width, int height, int levelCount, byte isRenderTarget);

		// Token: 0x060012D0 RID: 4816
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateTexture3D(IntPtr device, SurfaceFormat format, int width, int height, int depth, int levelCount);

		// Token: 0x060012D1 RID: 4817
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateTextureCube(IntPtr device, SurfaceFormat format, int size, int levelCount, byte isRenderTarget);

		// Token: 0x060012D2 RID: 4818
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeTexture(IntPtr device, IntPtr texture);

		// Token: 0x060012D3 RID: 4819
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetTextureData2D(IntPtr device, IntPtr texture, int x, int y, int w, int h, int level, IntPtr data, int dataLength);

		// Token: 0x060012D4 RID: 4820
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetTextureData3D(IntPtr device, IntPtr texture, int x, int y, int z, int w, int h, int d, int level, IntPtr data, int dataLength);

		// Token: 0x060012D5 RID: 4821
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetTextureDataCube(IntPtr device, IntPtr texture, int x, int y, int w, int h, CubeMapFace cubeMapFace, int level, IntPtr data, int dataLength);

		// Token: 0x060012D6 RID: 4822
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetTextureDataYUV(IntPtr device, IntPtr y, IntPtr u, IntPtr v, int yWidth, int yHeight, int uvWidth, int uvHeight, IntPtr data, int dataLength);

		// Token: 0x060012D7 RID: 4823
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetTextureData2D(IntPtr device, IntPtr texture, int x, int y, int w, int h, int level, IntPtr data, int dataLength);

		// Token: 0x060012D8 RID: 4824
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetTextureData3D(IntPtr device, IntPtr texture, int x, int y, int z, int w, int h, int d, int level, IntPtr data, int dataLength);

		// Token: 0x060012D9 RID: 4825
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetTextureDataCube(IntPtr device, IntPtr texture, int x, int y, int w, int h, CubeMapFace cubeMapFace, int level, IntPtr data, int dataLength);

		// Token: 0x060012DA RID: 4826
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_GenColorRenderbuffer(IntPtr device, int width, int height, SurfaceFormat format, int multiSampleCount, IntPtr texture);

		// Token: 0x060012DB RID: 4827
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_GenDepthStencilRenderbuffer(IntPtr device, int width, int height, DepthFormat format, int multiSampleCount);

		// Token: 0x060012DC RID: 4828
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeRenderbuffer(IntPtr device, IntPtr renderbuffer);

		// Token: 0x060012DD RID: 4829
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_GenVertexBuffer(IntPtr device, byte dynamic, BufferUsage usage, int sizeInBytes);

		// Token: 0x060012DE RID: 4830
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeVertexBuffer(IntPtr device, IntPtr buffer);

		// Token: 0x060012DF RID: 4831
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetVertexBufferData(IntPtr device, IntPtr buffer, int offsetInBytes, IntPtr data, int elementCount, int elementSizeInBytes, int vertexStride, SetDataOptions options);

		// Token: 0x060012E0 RID: 4832
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetVertexBufferData(IntPtr device, IntPtr buffer, int offsetInBytes, IntPtr data, int elementCount, int elementSizeInBytes, int vertexStride);

		// Token: 0x060012E1 RID: 4833
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_GenIndexBuffer(IntPtr device, byte dynamic, BufferUsage usage, int sizeInBytes);

		// Token: 0x060012E2 RID: 4834
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeIndexBuffer(IntPtr device, IntPtr buffer);

		// Token: 0x060012E3 RID: 4835
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetIndexBufferData(IntPtr device, IntPtr buffer, int offsetInBytes, IntPtr data, int dataLength, SetDataOptions options);

		// Token: 0x060012E4 RID: 4836
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetIndexBufferData(IntPtr device, IntPtr buffer, int offsetInBytes, IntPtr data, int dataLength);

		// Token: 0x060012E5 RID: 4837
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_CreateEffect(IntPtr device, byte[] effectCode, int length, out IntPtr effect, out IntPtr effectData);

		// Token: 0x060012E6 RID: 4838
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_CloneEffect(IntPtr device, IntPtr cloneSource, out IntPtr effect, out IntPtr effectData);

		// Token: 0x060012E7 RID: 4839
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeEffect(IntPtr device, IntPtr effect);

		// Token: 0x060012E8 RID: 4840
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_SetEffectTechnique(IntPtr device, IntPtr effect, IntPtr technique);

		// Token: 0x060012E9 RID: 4841
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_ApplyEffect(IntPtr device, IntPtr effect, uint pass, IntPtr stateChanges);

		// Token: 0x060012EA RID: 4842
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_BeginPassRestore(IntPtr device, IntPtr effect, IntPtr stateChanges);

		// Token: 0x060012EB RID: 4843
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_EndPassRestore(IntPtr device, IntPtr effect);

		// Token: 0x060012EC RID: 4844
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr FNA3D_CreateQuery(IntPtr device);

		// Token: 0x060012ED RID: 4845
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_AddDisposeQuery(IntPtr device, IntPtr query);

		// Token: 0x060012EE RID: 4846
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_QueryBegin(IntPtr device, IntPtr query);

		// Token: 0x060012EF RID: 4847
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_QueryEnd(IntPtr device, IntPtr query);

		// Token: 0x060012F0 RID: 4848
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_QueryComplete(IntPtr device, IntPtr query);

		// Token: 0x060012F1 RID: 4849
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_QueryPixelCount(IntPtr device, IntPtr query);

		// Token: 0x060012F2 RID: 4850
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsDXT1(IntPtr device);

		// Token: 0x060012F3 RID: 4851
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsS3TC(IntPtr device);

		// Token: 0x060012F4 RID: 4852
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsBC7(IntPtr device);

		// Token: 0x060012F5 RID: 4853
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsHardwareInstancing(IntPtr device);

		// Token: 0x060012F6 RID: 4854
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsNoOverwrite(IntPtr device);

		// Token: 0x060012F7 RID: 4855
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte FNA3D_SupportsSRGBRenderTargets(IntPtr device);

		// Token: 0x060012F8 RID: 4856
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_GetMaxTextureSlots(IntPtr device, out int textures, out int vertexTextures);

		// Token: 0x060012F9 RID: 4857
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern int FNA3D_GetMaxMultiSampleCount(IntPtr device, SurfaceFormat format, int preferredMultiSampleCount);

		// Token: 0x060012FA RID: 4858
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		private unsafe static extern void FNA3D_SetStringMarker(IntPtr device, byte* text);

		// Token: 0x060012FB RID: 4859 RVA: 0x0002B680 File Offset: 0x00029880
		public unsafe static void FNA3D_SetStringMarker(IntPtr device, string text)
		{
			byte* ptr = SDL.Utf8EncodeHeap(text);
			FNA3D.FNA3D_SetStringMarker(device, ptr);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
		}

		// Token: 0x060012FC RID: 4860
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		private unsafe static extern void FNA3D_SetTextureName(IntPtr device, IntPtr texture, byte* text);

		// Token: 0x060012FD RID: 4861 RVA: 0x0002B6A8 File Offset: 0x000298A8
		public unsafe static void FNA3D_SetTextureName(IntPtr device, IntPtr texture, string text)
		{
			byte* ptr = SDL.Utf8EncodeHeap(text);
			FNA3D.FNA3D_SetTextureName(device, texture, ptr);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
		}

		// Token: 0x060012FE RID: 4862
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr FNA3D_Image_Load(FNA3D.FNA3D_Image_ReadFunc readFunc, FNA3D.FNA3D_Image_SkipFunc skipFunc, FNA3D.FNA3D_Image_EOFFunc eofFunc, IntPtr context, out int width, out int height, out int len, int forceW, int forceH, byte zoom);

		// Token: 0x060012FF RID: 4863
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		public static extern void FNA3D_Image_Free(IntPtr mem);

		// Token: 0x06001300 RID: 4864 RVA: 0x0002B6D0 File Offset: 0x000298D0
		[MonoPInvokeCallback(typeof(FNA3D.FNA3D_Image_ReadFunc))]
		private static int INTERNAL_Read(IntPtr context, IntPtr data, int size)
		{
			Dictionary<IntPtr, Stream> dictionary = FNA3D.readStreams;
			Stream stream;
			lock (dictionary)
			{
				stream = FNA3D.readStreams[context];
			}
			byte[] array = new byte[size];
			int num = stream.Read(array, 0, size);
			Marshal.Copy(array, 0, data, num);
			return num;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0002B734 File Offset: 0x00029934
		[MonoPInvokeCallback(typeof(FNA3D.FNA3D_Image_SkipFunc))]
		private static void INTERNAL_Skip(IntPtr context, int n)
		{
			Dictionary<IntPtr, Stream> dictionary = FNA3D.readStreams;
			Stream stream;
			lock (dictionary)
			{
				stream = FNA3D.readStreams[context];
			}
			stream.Seek((long)n, SeekOrigin.Current);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0002B784 File Offset: 0x00029984
		[MonoPInvokeCallback(typeof(FNA3D.FNA3D_Image_EOFFunc))]
		private static int INTERNAL_EOF(IntPtr context)
		{
			Dictionary<IntPtr, Stream> dictionary = FNA3D.readStreams;
			Stream stream;
			lock (dictionary)
			{
				stream = FNA3D.readStreams[context];
			}
			return (stream.Position == stream.Length) ? 1 : 0;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0002B7D8 File Offset: 0x000299D8
		public static IntPtr ReadImageStream(Stream stream, out int width, out int height, out int len, int forceW = -1, int forceH = -1, bool zoom = false)
		{
			Dictionary<IntPtr, Stream> dictionary = FNA3D.readStreams;
			IntPtr intPtr;
			lock (dictionary)
			{
				intPtr = (IntPtr)(FNA3D.readGlobal++);
				FNA3D.readStreams.Add(intPtr, stream);
			}
			IntPtr intPtr2 = FNA3D.FNA3D_Image_Load(FNA3D.readFunc, FNA3D.skipFunc, FNA3D.eofFunc, intPtr, out width, out height, out len, forceW, forceH, (zoom > false) ? 1 : 0);
			dictionary = FNA3D.readStreams;
			lock (dictionary)
			{
				FNA3D.readStreams.Remove(intPtr);
			}
			return intPtr2;
		}

		// Token: 0x06001304 RID: 4868
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		private static extern void FNA3D_Image_SavePNG(FNA3D.FNA3D_Image_WriteFunc writeFunc, IntPtr context, int srcW, int srcH, int dstW, int dstH, IntPtr data);

		// Token: 0x06001305 RID: 4869
		[DllImport("FNA3D", CallingConvention = CallingConvention.Cdecl)]
		private static extern void FNA3D_Image_SaveJPG(FNA3D.FNA3D_Image_WriteFunc writeFunc, IntPtr context, int srcW, int srcH, int dstW, int dstH, IntPtr data, int quality);

		// Token: 0x06001306 RID: 4870 RVA: 0x0002B888 File Offset: 0x00029A88
		[MonoPInvokeCallback(typeof(FNA3D.FNA3D_Image_WriteFunc))]
		private static void INTERNAL_Write(IntPtr context, IntPtr data, int size)
		{
			Dictionary<IntPtr, Stream> dictionary = FNA3D.writeStreams;
			Stream stream;
			lock (dictionary)
			{
				stream = FNA3D.writeStreams[context];
			}
			byte[] array = new byte[size];
			Marshal.Copy(data, array, 0, size);
			stream.Write(array, 0, size);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0002B8E8 File Offset: 0x00029AE8
		public static void WritePNGStream(Stream stream, int srcW, int srcH, int dstW, int dstH, IntPtr data)
		{
			Dictionary<IntPtr, Stream> dictionary = FNA3D.writeStreams;
			IntPtr intPtr;
			lock (dictionary)
			{
				intPtr = (IntPtr)(FNA3D.writeGlobal++);
				FNA3D.writeStreams.Add(intPtr, stream);
			}
			FNA3D.FNA3D_Image_SavePNG(FNA3D.writeFunc, intPtr, srcW, srcH, dstW, dstH, data);
			dictionary = FNA3D.writeStreams;
			lock (dictionary)
			{
				FNA3D.writeStreams.Remove(intPtr);
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0002B988 File Offset: 0x00029B88
		public static void WriteJPGStream(Stream stream, int srcW, int srcH, int dstW, int dstH, IntPtr data, int quality)
		{
			Dictionary<IntPtr, Stream> dictionary = FNA3D.writeStreams;
			IntPtr intPtr;
			lock (dictionary)
			{
				intPtr = (IntPtr)(FNA3D.writeGlobal++);
				FNA3D.writeStreams.Add(intPtr, stream);
			}
			FNA3D.FNA3D_Image_SaveJPG(FNA3D.writeFunc, intPtr, srcW, srcH, dstW, dstH, data, quality);
			dictionary = FNA3D.writeStreams;
			lock (dictionary)
			{
				FNA3D.writeStreams.Remove(intPtr);
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0002BA28 File Offset: 0x00029C28
		// Note: this type is marked as 'beforefieldinit'.
		static FNA3D()
		{
		}

		// Token: 0x040008AF RID: 2223
		private const string nativeLibName = "FNA3D";

		// Token: 0x040008B0 RID: 2224
		private static FNA3D.FNA3D_Image_ReadFunc readFunc = new FNA3D.FNA3D_Image_ReadFunc(FNA3D.INTERNAL_Read);

		// Token: 0x040008B1 RID: 2225
		private static FNA3D.FNA3D_Image_SkipFunc skipFunc = new FNA3D.FNA3D_Image_SkipFunc(FNA3D.INTERNAL_Skip);

		// Token: 0x040008B2 RID: 2226
		private static FNA3D.FNA3D_Image_EOFFunc eofFunc = new FNA3D.FNA3D_Image_EOFFunc(FNA3D.INTERNAL_EOF);

		// Token: 0x040008B3 RID: 2227
		private static int readGlobal = 0;

		// Token: 0x040008B4 RID: 2228
		private static Dictionary<IntPtr, Stream> readStreams = new Dictionary<IntPtr, Stream>();

		// Token: 0x040008B5 RID: 2229
		private static FNA3D.FNA3D_Image_WriteFunc writeFunc = new FNA3D.FNA3D_Image_WriteFunc(FNA3D.INTERNAL_Write);

		// Token: 0x040008B6 RID: 2230
		private static int writeGlobal = 0;

		// Token: 0x040008B7 RID: 2231
		private static Dictionary<IntPtr, Stream> writeStreams = new Dictionary<IntPtr, Stream>();

		// Token: 0x020003C2 RID: 962
		public struct FNA3D_Viewport
		{
			// Token: 0x04001D76 RID: 7542
			public int x;

			// Token: 0x04001D77 RID: 7543
			public int y;

			// Token: 0x04001D78 RID: 7544
			public int w;

			// Token: 0x04001D79 RID: 7545
			public int h;

			// Token: 0x04001D7A RID: 7546
			public float minDepth;

			// Token: 0x04001D7B RID: 7547
			public float maxDepth;
		}

		// Token: 0x020003C3 RID: 963
		public struct FNA3D_BlendState
		{
			// Token: 0x04001D7C RID: 7548
			public Blend colorSourceBlend;

			// Token: 0x04001D7D RID: 7549
			public Blend colorDestinationBlend;

			// Token: 0x04001D7E RID: 7550
			public BlendFunction colorBlendFunction;

			// Token: 0x04001D7F RID: 7551
			public Blend alphaSourceBlend;

			// Token: 0x04001D80 RID: 7552
			public Blend alphaDestinationBlend;

			// Token: 0x04001D81 RID: 7553
			public BlendFunction alphaBlendFunction;

			// Token: 0x04001D82 RID: 7554
			public ColorWriteChannels colorWriteEnable;

			// Token: 0x04001D83 RID: 7555
			public ColorWriteChannels colorWriteEnable1;

			// Token: 0x04001D84 RID: 7556
			public ColorWriteChannels colorWriteEnable2;

			// Token: 0x04001D85 RID: 7557
			public ColorWriteChannels colorWriteEnable3;

			// Token: 0x04001D86 RID: 7558
			public Color blendFactor;

			// Token: 0x04001D87 RID: 7559
			public int multiSampleMask;
		}

		// Token: 0x020003C4 RID: 964
		public struct FNA3D_DepthStencilState
		{
			// Token: 0x04001D88 RID: 7560
			public byte depthBufferEnable;

			// Token: 0x04001D89 RID: 7561
			public byte depthBufferWriteEnable;

			// Token: 0x04001D8A RID: 7562
			public CompareFunction depthBufferFunction;

			// Token: 0x04001D8B RID: 7563
			public byte stencilEnable;

			// Token: 0x04001D8C RID: 7564
			public int stencilMask;

			// Token: 0x04001D8D RID: 7565
			public int stencilWriteMask;

			// Token: 0x04001D8E RID: 7566
			public byte twoSidedStencilMode;

			// Token: 0x04001D8F RID: 7567
			public StencilOperation stencilFail;

			// Token: 0x04001D90 RID: 7568
			public StencilOperation stencilDepthBufferFail;

			// Token: 0x04001D91 RID: 7569
			public StencilOperation stencilPass;

			// Token: 0x04001D92 RID: 7570
			public CompareFunction stencilFunction;

			// Token: 0x04001D93 RID: 7571
			public StencilOperation ccwStencilFail;

			// Token: 0x04001D94 RID: 7572
			public StencilOperation ccwStencilDepthBufferFail;

			// Token: 0x04001D95 RID: 7573
			public StencilOperation ccwStencilPass;

			// Token: 0x04001D96 RID: 7574
			public CompareFunction ccwStencilFunction;

			// Token: 0x04001D97 RID: 7575
			public int referenceStencil;
		}

		// Token: 0x020003C5 RID: 965
		public struct FNA3D_RasterizerState
		{
			// Token: 0x04001D98 RID: 7576
			public FillMode fillMode;

			// Token: 0x04001D99 RID: 7577
			public CullMode cullMode;

			// Token: 0x04001D9A RID: 7578
			public float depthBias;

			// Token: 0x04001D9B RID: 7579
			public float slopeScaleDepthBias;

			// Token: 0x04001D9C RID: 7580
			public byte scissorTestEnable;

			// Token: 0x04001D9D RID: 7581
			public byte multiSampleAntiAlias;
		}

		// Token: 0x020003C6 RID: 966
		public struct FNA3D_SamplerState
		{
			// Token: 0x04001D9E RID: 7582
			public TextureFilter filter;

			// Token: 0x04001D9F RID: 7583
			public TextureAddressMode addressU;

			// Token: 0x04001DA0 RID: 7584
			public TextureAddressMode addressV;

			// Token: 0x04001DA1 RID: 7585
			public TextureAddressMode addressW;

			// Token: 0x04001DA2 RID: 7586
			public float mipMapLevelOfDetailBias;

			// Token: 0x04001DA3 RID: 7587
			public int maxAnisotropy;

			// Token: 0x04001DA4 RID: 7588
			public int maxMipLevel;
		}

		// Token: 0x020003C7 RID: 967
		public struct FNA3D_VertexDeclaration
		{
			// Token: 0x04001DA5 RID: 7589
			public int vertexStride;

			// Token: 0x04001DA6 RID: 7590
			public int elementCount;

			// Token: 0x04001DA7 RID: 7591
			public IntPtr elements;
		}

		// Token: 0x020003C8 RID: 968
		public struct FNA3D_VertexBufferBinding
		{
			// Token: 0x04001DA8 RID: 7592
			public IntPtr vertexBuffer;

			// Token: 0x04001DA9 RID: 7593
			public FNA3D.FNA3D_VertexDeclaration vertexDeclaration;

			// Token: 0x04001DAA RID: 7594
			public int vertexOffset;

			// Token: 0x04001DAB RID: 7595
			public int instanceFrequency;
		}

		// Token: 0x020003C9 RID: 969
		public struct FNA3D_RenderTargetBinding
		{
			// Token: 0x04001DAC RID: 7596
			public byte type;

			// Token: 0x04001DAD RID: 7597
			public int data1;

			// Token: 0x04001DAE RID: 7598
			public int data2;

			// Token: 0x04001DAF RID: 7599
			public int levelCount;

			// Token: 0x04001DB0 RID: 7600
			public int multiSampleCount;

			// Token: 0x04001DB1 RID: 7601
			public IntPtr texture;

			// Token: 0x04001DB2 RID: 7602
			public IntPtr colorBuffer;
		}

		// Token: 0x020003CA RID: 970
		public struct FNA3D_PresentationParameters
		{
			// Token: 0x04001DB3 RID: 7603
			public int backBufferWidth;

			// Token: 0x04001DB4 RID: 7604
			public int backBufferHeight;

			// Token: 0x04001DB5 RID: 7605
			public SurfaceFormat backBufferFormat;

			// Token: 0x04001DB6 RID: 7606
			public int multiSampleCount;

			// Token: 0x04001DB7 RID: 7607
			public IntPtr deviceWindowHandle;

			// Token: 0x04001DB8 RID: 7608
			public byte isFullScreen;

			// Token: 0x04001DB9 RID: 7609
			public DepthFormat depthStencilFormat;

			// Token: 0x04001DBA RID: 7610
			public PresentInterval presentationInterval;

			// Token: 0x04001DBB RID: 7611
			public DisplayOrientation displayOrientation;

			// Token: 0x04001DBC RID: 7612
			public RenderTargetUsage renderTargetUsage;
		}

		// Token: 0x020003CB RID: 971
		// (Invoke) Token: 0x06001AC2 RID: 6850
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void FNA3D_LogFunc(IntPtr msg);

		// Token: 0x020003CC RID: 972
		// (Invoke) Token: 0x06001AC6 RID: 6854
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int FNA3D_Image_ReadFunc(IntPtr context, IntPtr data, int size);

		// Token: 0x020003CD RID: 973
		// (Invoke) Token: 0x06001ACA RID: 6858
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void FNA3D_Image_SkipFunc(IntPtr context, int n);

		// Token: 0x020003CE RID: 974
		// (Invoke) Token: 0x06001ACE RID: 6862
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int FNA3D_Image_EOFFunc(IntPtr context);

		// Token: 0x020003CF RID: 975
		// (Invoke) Token: 0x06001AD2 RID: 6866
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void FNA3D_Image_WriteFunc(IntPtr context, IntPtr data, int size);
	}
}
