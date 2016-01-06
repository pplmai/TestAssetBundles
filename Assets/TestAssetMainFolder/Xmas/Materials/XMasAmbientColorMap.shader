Shader "Custom/XMasAmbientColorMap" {
	Properties 
	{
		_Color ("Color",COLOR) = (1,1,1,1)
		_ColorMap ("Color Map", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off Lighting Off Cull Off Fog { Mode Off } Blend DstColor OneMinusSrcAlpha
		
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert_vct
			#pragma fragment frag_mult 
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			sampler2D _ColorMap;
			fixed4 _Color;

			struct vin_vct 
			{
				fixed4 vertex : POSITION;
				fixed2 texcoord : TEXCOORD0;
			};

			struct v2f_vct
			{
				fixed4 vertex : SV_POSITION;
				fixed2 texcoord : TEXCOORD0;
			};

			v2f_vct vert_vct(vin_vct v)
			{
				v2f_vct o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			fixed4 frag_mult(v2f_vct i) : COLOR
			{
				fixed4 colorMap = tex2D(_ColorMap,i.texcoord);
//				return fixed4(alphaMap.rgb*_Light + _Color.rgb , 1 - alphaMap.a*_Light);
				return fixed4(colorMap.rgb + _Color.rgb , _Color.a - colorMap.a*_Color.a);
			}
			
			ENDCG
		} 
	}
}
