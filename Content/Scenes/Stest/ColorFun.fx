float Mode;
sampler TextureSampler : register(s0);
 
float4 ColorFun(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 Color;
    float4 NewColor;
    
    Color = tex2D(TextureSampler, texCoord.xy);

    if(Mode == 0.1f)
    {
        NewColor.r = Color.g;
        NewColor.g = Color.b;
        NewColor.b = Color.r;
        NewColor.a = Color.a;
    }
    if (Mode == 0.2f)
    {
        NewColor.r = Color.b;
        NewColor.g = Color.r;
        NewColor.b = Color.g;
        NewColor.a = Color.a;

    }
    return NewColor;
}


technique Technique1
{
    pass ColorFun
    { 
        #if SM4
		        PixelShader = compile ps_4_0 ColorFun();
        #elif SM3
		        PixelShader = compile ps_3_0 ColorFun();
        #else SM2
                PixelShader = compile ps_2_0 ColorFun();
        #endif
    }
}