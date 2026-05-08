using System;

namespace Steamworks
{
	// Token: 0x02000163 RID: 355
	public enum EHTTPStatusCode
	{
		// Token: 0x040008F5 RID: 2293
		k_EHTTPStatusCodeInvalid,
		// Token: 0x040008F6 RID: 2294
		k_EHTTPStatusCode100Continue = 100,
		// Token: 0x040008F7 RID: 2295
		k_EHTTPStatusCode101SwitchingProtocols,
		// Token: 0x040008F8 RID: 2296
		k_EHTTPStatusCode200OK = 200,
		// Token: 0x040008F9 RID: 2297
		k_EHTTPStatusCode201Created,
		// Token: 0x040008FA RID: 2298
		k_EHTTPStatusCode202Accepted,
		// Token: 0x040008FB RID: 2299
		k_EHTTPStatusCode203NonAuthoritative,
		// Token: 0x040008FC RID: 2300
		k_EHTTPStatusCode204NoContent,
		// Token: 0x040008FD RID: 2301
		k_EHTTPStatusCode205ResetContent,
		// Token: 0x040008FE RID: 2302
		k_EHTTPStatusCode206PartialContent,
		// Token: 0x040008FF RID: 2303
		k_EHTTPStatusCode300MultipleChoices = 300,
		// Token: 0x04000900 RID: 2304
		k_EHTTPStatusCode301MovedPermanently,
		// Token: 0x04000901 RID: 2305
		k_EHTTPStatusCode302Found,
		// Token: 0x04000902 RID: 2306
		k_EHTTPStatusCode303SeeOther,
		// Token: 0x04000903 RID: 2307
		k_EHTTPStatusCode304NotModified,
		// Token: 0x04000904 RID: 2308
		k_EHTTPStatusCode305UseProxy,
		// Token: 0x04000905 RID: 2309
		k_EHTTPStatusCode307TemporaryRedirect = 307,
		// Token: 0x04000906 RID: 2310
		k_EHTTPStatusCode308PermanentRedirect,
		// Token: 0x04000907 RID: 2311
		k_EHTTPStatusCode400BadRequest = 400,
		// Token: 0x04000908 RID: 2312
		k_EHTTPStatusCode401Unauthorized,
		// Token: 0x04000909 RID: 2313
		k_EHTTPStatusCode402PaymentRequired,
		// Token: 0x0400090A RID: 2314
		k_EHTTPStatusCode403Forbidden,
		// Token: 0x0400090B RID: 2315
		k_EHTTPStatusCode404NotFound,
		// Token: 0x0400090C RID: 2316
		k_EHTTPStatusCode405MethodNotAllowed,
		// Token: 0x0400090D RID: 2317
		k_EHTTPStatusCode406NotAcceptable,
		// Token: 0x0400090E RID: 2318
		k_EHTTPStatusCode407ProxyAuthRequired,
		// Token: 0x0400090F RID: 2319
		k_EHTTPStatusCode408RequestTimeout,
		// Token: 0x04000910 RID: 2320
		k_EHTTPStatusCode409Conflict,
		// Token: 0x04000911 RID: 2321
		k_EHTTPStatusCode410Gone,
		// Token: 0x04000912 RID: 2322
		k_EHTTPStatusCode411LengthRequired,
		// Token: 0x04000913 RID: 2323
		k_EHTTPStatusCode412PreconditionFailed,
		// Token: 0x04000914 RID: 2324
		k_EHTTPStatusCode413RequestEntityTooLarge,
		// Token: 0x04000915 RID: 2325
		k_EHTTPStatusCode414RequestURITooLong,
		// Token: 0x04000916 RID: 2326
		k_EHTTPStatusCode415UnsupportedMediaType,
		// Token: 0x04000917 RID: 2327
		k_EHTTPStatusCode416RequestedRangeNotSatisfiable,
		// Token: 0x04000918 RID: 2328
		k_EHTTPStatusCode417ExpectationFailed,
		// Token: 0x04000919 RID: 2329
		k_EHTTPStatusCode4xxUnknown,
		// Token: 0x0400091A RID: 2330
		k_EHTTPStatusCode429TooManyRequests = 429,
		// Token: 0x0400091B RID: 2331
		k_EHTTPStatusCode444ConnectionClosed = 444,
		// Token: 0x0400091C RID: 2332
		k_EHTTPStatusCode500InternalServerError = 500,
		// Token: 0x0400091D RID: 2333
		k_EHTTPStatusCode501NotImplemented,
		// Token: 0x0400091E RID: 2334
		k_EHTTPStatusCode502BadGateway,
		// Token: 0x0400091F RID: 2335
		k_EHTTPStatusCode503ServiceUnavailable,
		// Token: 0x04000920 RID: 2336
		k_EHTTPStatusCode504GatewayTimeout,
		// Token: 0x04000921 RID: 2337
		k_EHTTPStatusCode505HTTPVersionNotSupported,
		// Token: 0x04000922 RID: 2338
		k_EHTTPStatusCode5xxUnknown = 599
	}
}
