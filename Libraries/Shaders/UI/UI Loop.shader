Shader "Rune/UI/Loop"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _ScrollSpeed ("Scroll Speed (XY)", Vector) = (0.1, 0.0, 0.0, 0.0)

        _Color ("Tint", Color) = (1,1,1,1)

        [HideInInspector] _Stencil ("Stencil ID", Float) = 0
        [HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
        [HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
        [HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
        [HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255
        [HideInInspector] _ColorMask ("Color Mask", Float) = 15
    }



    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalPipeline" }


        Pass
        {
            Tags { "LightMode"="Universal2D" }

            Stencil
            {
                Ref [_Stencil]
                Comp [_StencilComp]
                Pass [_StencilOp]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
            }

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            ZTest Always
            ColorMask [_ColorMask]


            HLSLPROGRAM
            

            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"


            TEXTURE2D(_MainTex);
            
            SAMPLER(sampler_MainTex);
            
            float4 _MainTex_ST;
            float4 _ScrollSpeed;
            float4 _Color;


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
                float2 scrolledUV = IN.uv + _ScrollSpeed.xy * _Time.y;
                
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, scrolledUV);
                
                return texColor * IN.color;
            }
            
            
            ENDHLSL
        }
    }



    FallBack "Hidden/InternalErrorShader"
}