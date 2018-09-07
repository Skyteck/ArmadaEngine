sampler TextureSampler : register(s0);

float4 VirtualBoy(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 Color;
    Color = tex2D(TextureSampler, texCoord.xy);
    Color.rgb /= Color.a;
    Color.gb = 0;
    return Color;
}


technique Technique1
{
    pass VirtualBoy
    {
        #if SM4
		        PixelShader = compile ps_4_0 VirtualBoy();
        #elif SM3
		        PixelShader = compile ps_3_0 VirtualBoy();
        #else SM2
                PixelShader = compile ps_2_0 VirtualBoy();
        #endif
    }
}