Shader "Custom/NewImageEffectShader"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
            sampler2D _NoiseTex;

			fixed4 frag (v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv + float2(0 , sin(i.vertex.x / 200 + _Time[1]) / 100));    // Drunk effect
                fixed4 col = tex2D(_MainTex, i.uv) + tex2D(_NoiseTex, i.uv + float2(_Time[0], 0));        // Foggy effect
                

				return col;
			}
			ENDCG
		}
	}
}
