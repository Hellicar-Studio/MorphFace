Shader "Custom/BarrelDistortCenter"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_StrengthMin("StrengthMin", Float) = 1.0
		_StrengthMax("StrengthMax", Float) = 1.0
		_MaxDistort("MaxDistort", Float) = 0.05

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			
			#include "UnityCG.cginc"
			#define PI 3.14159
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Faces[12];

			float _StrengthMin;
			float _StrengthMax;
			float _MaxDistort;


			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float map(float value, float low1, float high1, float low2, float high2) {
				return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 pos = i.uv;
				float maxDistort = _MaxDistort;
				float strength;
				for (int j = 0; j < 12; j+=2) {
					float2 center = float2(_Faces[j], _Faces[j+1]);
					pos.x *= 16.0 / 9.0;
					float2 dir = pos - center;
					float dist = distance(pos, center);
					strength = smoothstep(_StrengthMin, 0.0, dist);
					strength = map(strength, 0.0, 1.0, 0.0, _MaxDistort);
					if (dist > 0.0)
					{
						dist = 1.0 - dist;
						dir *= dist * strength;

						pos.x -= dir.x;
						pos.y -= dir.y;
					}
					pos.x *= 9.0 / 16.0;
				}
				
				float4 c = tex2D(_MainTex, pos);
				UNITY_APPLY_FOG(i.fogCoord, col);
				return c;
			}
			ENDCG
		}
	}
}
