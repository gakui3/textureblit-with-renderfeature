Shader "Custom/Grayscale"
{
    Properties
    {
        // Blitの第一引数が _MainTex に入るため定義する
        _MainTex ("Main Tex", 2D) = "while" { }
    }

    SubShader
    {
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex); // sampler + Texture名 でそのテクスチャのサンプリング状態を取得できる

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;

                return OUT;
            }


            half4 frag(Varyings IN) : SV_Target
            {
                // SAMPLE_TEXTURE2D : Textureの特定の位置にあるピクセルの色を取得する
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                return half4(gray, gray, gray, col.a);
            }
            ENDHLSL
        }
    }
}