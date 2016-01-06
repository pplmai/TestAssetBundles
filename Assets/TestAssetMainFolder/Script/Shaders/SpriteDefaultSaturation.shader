Shader "Sprites/Default - Saturation"
{
	Properties
	{
		[PerRendererData]_MainTex ("Main Texture", 2D) = "white" {}
		_Sat ("Saturation", Range (0, 1)) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent"
			"IgnoreProjector"="True" 
			"RenderType"="Transparent"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

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
				fixed4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				fixed2 texcoord  : TEXCOORD0;
			};
			

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;

				return OUT;
			}

			sampler2D _MainTex;
			fixed _Sat;
			
			fixed4 frag(v2f IN) : COLOR
			{
				fixed4 main = tex2D(_MainTex,IN.texcoord);
				 //magic number to achieve photoshop's desaturate effect.
				 //ref: http://stackoverflow.com/questions/9320953/what-algorithm-does-photoshop-use-to-desaturate-an-image
				fixed desatValue = dot(main.rgb,fixed3(.3f,.59f,.11f));
				
				main = main*IN.color;
				fixed4 fullDesat = fixed4(desatValue*IN.color.r,desatValue*IN.color.g,desatValue*IN.color.b,main.a*IN.color.a);
				
				return lerp(main,fullDesat,1-_Sat);
			}
		ENDCG
		}
	}
}