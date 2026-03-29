Shader "Rune/UI/Rainbow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Hue Shift Speed", Float) = 1.0
        _Saturation ("Saturation", Range(0, 1)) = 1.0
        _Value ("Value", Range(0, 1)) = 1.0
        _Alpha ("Alpha", Range(0,1)) = 1.0
    }



    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }


        LOD 100


        Pass
        {
            Name "UI"

            Tags
            {
                "LightMode" = "SRPDefaultUnlit"
            }

            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha


            HLSLPROGRAM
            
            
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"


            TEXTURE2D(_MainTex);
            
            SAMPLER(sampler_MainTex);

            float4 _MainTex_ST;
            
            float _Speed;
            float _Saturation;
            float _Value;

            
            struct Attributes
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };


            struct Varyings
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };


            float3 HSVToRGB(float h, float s, float v)
            {
                float c = v * s;
                float x = c * (1 - abs(fmod(h * 6.0, 2.0) - 1));
                float m = v - c;
                float3 rgb;

                if (h < 1.0 / 6.0) rgb = float3(c, x, 0);
                else if (h < 2.0 / 6.0) rgb = float3(x, c, 0);
                else if (h < 3.0 / 6.0) rgb = float3(0, c, x);
                else if (h < 4.0 / 6.0) rgb = float3(0, x, c);
                else if (h < 5.0 / 6.0) rgb = float3(x, 0, c);
                else rgb = float3(c, 0, x);

                return rgb + m;
            }


            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                
                OUT.position = TransformObjectToHClip(IN.vertex.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color = IN.color;
                
                return OUT;
            }


            half4 frag(Varyings IN) : SV_Target
            {
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);

                float3 hsvColor = HSVToRGB(fmod(_Time.y * _Speed, 1.0), _Saturation, _Value);
                
                return half4(texColor.rgb * hsvColor, texColor.a) * IN.color;
            }


            ENDHLSL
        }
    }



    FallBack "Hidden/InternalErrorShader"
}