//For Opaque 3D Mesh

Shader "Custom/SolidTex_Tint"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		_ExtraTint ("Extra Tint",COLOR) = (1,1,1,1)
	}

	SubShader
	{
		Tags
		{ 
			"IgnoreProjector"="True" "RenderType"="Opaque"
		}
		Blend Off Lighting Off Cull Off Fog { Mode Off }

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
				fixed2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				fixed4 vertex   : SV_POSITION;
				fixed2 texcoord  : TEXCOORD0;
			};
			

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;

				return OUT;
			}

			sampler2D _MainTex;
			fixed4 _ExtraTint;

			fixed4 frag(v2f IN) : COLOR
			{
				fixed4 value = tex2D(_MainTex,IN.texcoord);
				return value*_ExtraTint;
			}
		ENDCG
		}
	}
}