Shader "Custom/BarrelDistortCenter"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Range("Range", Float) = 0.5
		_Center("Center", Vector) = (0.5, 0.5, 0.5, 0.5)
		_Strength("Strength", Float) = 1.0
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
			float _Range;
			float2 _Center;
			float _Strength;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 center = _Center.xy;
				float2 pos = i.uv;
				pos.x *= 16.0 / 9.0;
				float2 dir = pos - center;
				float dist = distance(pos, center);
				if (dist > 0.0 && dist < _Range)
				{
					dist = 1.0 - dist;
					dir *= dist * _Strength;

					pos.x -= dir.x;
					pos.y -= dir.y;
				}

				pos.x *= 9.0 / 16.0;

				float4 c = tex2D(_MainTex, pos);
				//gl_FragColor = c;
				//fixed4 col = tex2D(_MainTex, p);
				//col.r += dist;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return c;
			}
			ENDCG
		}
	}
}
