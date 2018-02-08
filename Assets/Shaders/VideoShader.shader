Shader "Custom/VideoShader"
{
	Properties
	{
		[NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
		_VideoTex ("Video texture", 2D) = "white" {}
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
				float2 vuv : TEXCOORD1;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 vuv : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.vuv = v.vuv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _VideoTex;

			fixed4 frag (v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);

                //col.xyz = float3(i.uv.x, i.uv.y, 0);
                return clamp(0, 1, tex2D(_MainTex, i.uv) + tex2D(_VideoTex, i.vuv));
				//return col;
			}
			ENDCG
		}
	}
}
