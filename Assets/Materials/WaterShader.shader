// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/WaterShader" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Strength("Strength", Range(0, 1)) = 0.01
        _Speed("Speed", Range(-200, 200)) = 100
    }
    SubShader{
        Tags { "RenderType" = "Transparent" }
        
        Pass {

        Cull Off

        CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma vertex vertFunc
            #pragma fragment fragFunc

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 4.0

            struct Input
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertexOutput
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _Strength;
            float _Speed;
            sampler2D _MainTex;

            vertexOutput vertFunc(Input IN)
            {
                vertexOutput o;
                
                float4 worldPos = IN.vertex;

                float displacement = (cos(worldPos.y) + cos(worldPos.x + (_Speed * _Time[0]) ) );
                
                worldPos.y = worldPos.y + displacement * _Strength;
                

                o.vertex = UnityObjectToClipPos(worldPos);
                o.uv = IN.uv;

                return o;
            }

            float4 fragFunc(vertexOutput IN) : COLOR
            {
                float4 col = tex2D(_MainTex, IN.uv + float2(_Time[0], 0)) * _Color;
                return col;
            }


            ENDCG
                }
    }
    FallBack "Diffuse"
}