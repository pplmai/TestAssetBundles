Shader "Custom/River Flow"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
//		_DetailTex ("Detail Texture",2D) = "white" {}
//		_DetailTex2 ("Detail Texture 2",2D) = "white" {}
		_WavePow ("Wave Power", Vector) = (0,0,0,0)
//		_ScrollSpeed ("Scroll Speed",Vector) = (0,0,0,0)
//		_Tint ("Tint",Color) = (1,1,1,1)
//		_Tint2 ("Tint2",Color) = (1,1,1,1)
		_Multiplier ("River Multiplier",Range(0,1)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		
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
				fixed2 uv2 : TEXCOORD1;
				fixed4 col : COLOR;
			};

			struct v2f
			{
				fixed2 uv : TEXCOORD0;
//				fixed2 uv2 : TEXCOORD1;
//				fixed2 uv3 : TEXCOORD2;
				fixed4 vertex : SV_POSITION;
				fixed4 col : COLOR;
			};

			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			fixed4 _WavePow;
//			sampler2D _DetailTex;
//			fixed4 _DetailTex_ST;
//			sampler2D _DetailTex2;
//			fixed4 _DetailTex2_ST;
//			fixed4 _ScrollSpeed;
//			fixed4 _Tint;
//			fixed4 _Tint2;
			fixed _Multiplier;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.col = v.col;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				
				//calculate uv offset per vertex
				fixed time = frac(_Time);
				fixed sum = v.vertex.x+v.vertex.y;
				fixed sinX = sin(time*_WavePow.x+sum)*_WavePow.z;
				fixed sinY = sin(time*_WavePow.y+sum)*_WavePow.w;
				fixed2 offset = fixed2(sinX,sinY)*o.col.r;
				
				//scroll texture
//				_DetailTex_ST.zw += _ScrollSpeed.xy*time;
//				_DetailTex2_ST.zw += _ScrollSpeed.zw*time;
				
				o.uv = TRANSFORM_TEX(v.uv , _MainTex) - offset;
//				o.uv2 = TRANSFORM_TEX(v.uv2 , _DetailTex) - offset;
//				o.uv3 = TRANSFORM_TEX(v.uv2 , _DetailTex2) - offset;
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
//				fixed2 offset = tex2D(_DistortTex,i.uv3).rg*i.col.rg*_DistortPow;
//				fixed4 DetailCol = tex2D(_DetailTex,i.uv2);
//				fixed4 DetailCol2 = tex2D(_DetailTex2,i.uv3);
				fixed4 mainCol = tex2D(_MainTex, i.uv);
				return mainCol*_Multiplier;//+DetailCol*_Tint+DetailCol2*_Tint2;
			}
			ENDCG
		}
	}
}
