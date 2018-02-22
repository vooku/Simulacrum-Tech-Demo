Shader "Custom/VideoShader"
{
	Properties
	{
		[NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
		_Layer1 ("Choke layer 1", 2D) = "white" {}
        _Layer2 ("Choke layer 2", 2D) = "white" {}
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
				float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
			};

			struct v2f
			{
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv1 = v.uv1;
                o.uv2 = v.uv2;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _Layer1;
            sampler2D _Layer2;

			fixed4 frag (v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);

                return clamp(0, 1, tex2D(_Layer1, i.uv1) + tex2D(_Layer2, i.uv2));
                //return tex2D(_Layer2, i.uv1);
			}
			ENDCG
		}
	}
}
