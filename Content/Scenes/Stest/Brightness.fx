float Intensity;
sampler TextureSampler : register(s0);
 
float4 Brightness(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 Color;
    Color = tex2D(TextureSampler, texCoord.xy);
    Color.rgb /= Color.a;
    Color.rgb += Intensity;
    Color.rgb *= Color.a;
    return Color;
}
 
technique Technique1
{
    pass Brightness
    {
#if SM4
		PixelShader = compile ps_4_0 Brightness();
#elif SM3
		PixelShader = compile ps_3_0 Brightness();
#else SM2
        PixelShader = compile ps_2_0 Brightness();
#endif
    }
}