Shader "Custom/Carpet"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue"="Transparent" }
		
		Cull Back
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			struct appdata
			{
				fixed4 vertex : POSITION;
				fixed2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				fixed2 uv : TEXCOORD0;
				fixed4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			fixed _T;
			fixed _Frequency;
			fixed _Strength;
			fixed4 _Color;
			fixed _Pivot;
			fixed _UseTime;
			
			v2f vert (appdata v)
			{
				v2f o;
				
				fixed time = _Time.x;
				fixed offset = (v.uv.y-.5)*2+_Pivot;
				
				if(_UseTime == 1)
					v.vertex.y += sin(-_Time.y*5 + offset*_Frequency)*_Strength*(offset*offset);
				else
					v.vertex.y += sin(_T*3.1416*-2 + offset*_Frequency)*_Strength*(offset*offset);

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv)*_Color;
				return col;
			}
			ENDCG
		}
	}
}
