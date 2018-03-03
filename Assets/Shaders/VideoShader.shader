Shader "Custom/VideoShader"
{
	Properties
	{
		_Layer0("Choke layer 0", 2D) = "white" {}
		_Layer1("Choke layer 1", 2D) = "white" {}
        _Layer2("Choke layer 2", 2D) = "white" {}
        _Layer3("Choke layer 3", 2D) = "white" {}
        _Activity("Bitwise info about layer activity", int) = 0
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
				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float2 uv3 : TEXCOORD3;
			};

			struct v2f
			{
                float4 vertex : SV_POSITION;
                float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float2 uv3 : TEXCOORD3;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv0 = v.uv0;
				o.uv1 = v.uv1;
                o.uv2 = v.uv2;
                o.uv3 = v.uv3;
				return o;
			}
			
			sampler2D _Layer0;
			sampler2D _Layer1;
            sampler2D _Layer2;
            sampler2D _Layer3;
            int _Activity;

			fixed4 frag (v2f i) : SV_Target
			{
                return clamp(0, 1,
                    tex2D(_Layer0, i.uv0) * (_Activity & 1) +
                    tex2D(_Layer1, i.uv1) * (_Activity & 2) +
                    tex2D(_Layer2, i.uv2) * (_Activity & 4) +
                    tex2D(_Layer3, i.uv3) * (_Activity & 8));
			}
			ENDCG
		}
	}
}
