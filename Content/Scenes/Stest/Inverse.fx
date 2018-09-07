sampler TextureSampler : register(s0);
 
float4 Inverse(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 Color;
    Color = tex2D(TextureSampler, texCoord.xy);
    Color.rgb /= Color.a;
    Color.rgb = 1 - Color.rgb;
    Color.rgb *= Color.a;
    return Color;
}


technique Technique1
{
    pass Inverse
    { 
        #if SM4
		        PixelShader = compile ps_4_0 Inverse();
        #elif SM3
		        PixelShader = compile ps_3_0 Inverse();
        #else SM2
                PixelShader = compile ps_2_0 Inverse();
        #endif
    }
}