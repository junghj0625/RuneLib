Shader "Rune/Sprite/Loop With Gap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _ScrollSpeed ("Scroll Speed (XY)", Vector) = (0.1, 0.0, 0.0, 0.0)

        _UVOffset ("UV Offset (XY)", Vector) = (0.0, 0.0, 0.0, 0.0)

        _Gap ("Repeat Gap (XY)", Vector) = (0.2, 0.2, 0, 0)

        _Color ("Tint", Color) = (1,1,1,1)

        _Intensity ("Intensity", float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            Tags { "LightMode"="Universal2D" }
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float4 _MainTex_ST;
            float4 _ScrollSpeed;
            float4 _UVOffset;
            float4 _Gap;
            float4 _Color;
            float _Intensity;


            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };


            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color = IN.color * _Color;

                return OUT;
            }


            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv;

                uv += _ScrollSpeed.xy * _Time.y;
                uv += _UVOffset.xy;

                float2 period = 1.0.xx + _Gap.xy;

                float2 modUV = float2(
                    fmod(uv.x, period.x),
                    fmod(uv.y, period.y)
                );

                if (modUV.x < 0) modUV.x += period.x;
                if (modUV.y < 0) modUV.y += period.y;

                if (modUV.x > 1.0 || modUV.y > 1.0)
                {
                    return half4(0,0,0,0);
                }

                float2 finalUV = modUV;

                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, finalUV);
                
                return texColor * half4(_Intensity.xxx, 1) * IN.color;
            }

            ENDHLSL
        }
    }
}
