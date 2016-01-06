Shader "Custom/Sprite Cave Fog"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_FogTex ("Fog Texture",2D) = "white" {}
		_FogDensity ("Fog Density",Range(1,10)) = 1
		_ScrollSpeedX ("Scroll X",Range(-10,10)) = 0
		_ScrollSpeedY ("Scroll Y",Range(-10,10)) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend DstColor OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				fixed4 vertex   : POSITION;
				fixed4 color    : COLOR;
				fixed2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				fixed2 texcoord  : TEXCOORD0;
				fixed2 texcoord2 : TEXCOORD1;
			};
			
			sampler2D _FogTex;
			fixed4 _FogTex_ST;
			fixed _FogDensity;
			fixed _ScrollSpeedX;
			fixed _ScrollSpeedY;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				
				fixed2 lapse = fixed2(fmod(_Time.x,1/_ScrollSpeedX),fmod(_Time.x,1/_ScrollSpeedY));
				_FogTex_ST.zw += half2(lapse.x*_ScrollSpeedX,lapse.y*_ScrollSpeedY);
				
				OUT.texcoord2 = TRANSFORM_TEX(IN.texcoord,_FogTex);
				OUT.color = IN.color;

				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : COLOR
			{
				fixed4 tex = tex2D(_MainTex, IN.texcoord);
				fixed4 fogValue = tex2D(_FogTex,IN.texcoord2);
				tex = fixed4(tex.rgb*IN.color.rgb,fogValue.g*_FogDensity*tex.a+tex.a);
//				tex.a = pow(tex.a,2);
				tex = lerp(fixed4(1,1,1,1),tex,tex.a*IN.color.a);
				return tex;
			}
		ENDCG
		}
	}
}