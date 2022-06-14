#ifndef OSDEPTHUPSCALE_INCLUDE
#define OSDEPTHUPSCALE_INCLUDE

void DepthAwareUpscale_half(half2 UV, Texture2D FogTex, SamplerState Sampler, half2 Dimensions, Texture2D Depth_LowRes, half Depth_HighRes, out half3 Color, out half Alpha)
{
	// Out Value Setup
	Color = half3(0.0, 0.0, 0.0);
	Alpha = 1.0;
	
	#ifndef SHADERGRAPH_PREVIEW
	// Sample TexCoords UV + 00, 01, 10, and 11. Then record the distance diff.
	half2 tc = 1.0 / (Dimensions);
	half2 texcoords[4] = {
		half2(0, 0),
		half2(tc.x, tc.y),
		half2(0, tc.y),
		half2(tc.x, 0)
	};
	half distances[4];
	
	for (int a = 0; a <= 3; a++)
	{
		half s = SAMPLE_TEXTURE2D_LOD(Depth_LowRes, Sampler, UV + texcoords[a], 0).r;
		distances[a] = abs(Depth_HighRes - s);
	}
	
	
	// Evaluate for the Minimum Distance diff. Then sample that UV.
	half distanceMin = min(min(distances[0], distances[1]), min(distances[2], distances[3]));
	half4 r = half4(0, 0, 0, 0);
	
	for (int b = 0; b <= 3; b++)
	{
		if (distanceMin == distances[b])
			r = SAMPLE_TEXTURE2D_LOD(FogTex, Sampler, UV + texcoords[b], 0);
	}
	
	
	Color = r.rgb;
	Alpha = r.a;
	#endif
}
#endif