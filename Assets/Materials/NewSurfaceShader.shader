Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Normals ("Normals", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
        _Scale ("Scale", float) = 1
        _Speed ("Speed", float) = 1
        _Freq ("Frequency", float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
        sampler2D _Normals;

		struct Input {
			float2 uv_MainTex;
            float3 value;
		};

        float _Scale;
        float _Speed;
        float _Freq;

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full v, out Input o)
        {
            float offset = v.vertex.x * v.vertex.x + v.vertex.y * v.vertex.y ;
            float value = _Scale * sin(_Time[0] * _Speed + offset * _Freq);

            v.vertex.y = v.vertex.y + value;
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.value.y = value;
        }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
            o.Normal = tex2D(_Normals, IN.uv_MainTex);
            o.Normal.y += IN.value.y;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
