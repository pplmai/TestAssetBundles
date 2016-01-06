// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/Transparent-RippleFX" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Color ("Color of the Ripples", Color) = (1,1,1,1)
	_USpeed ("U speed", Float) = 1
	_VSpeed ("V speed", Float) = 1
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
				//UNITY_FOG_COORDS(1)
			};

			sampler2D _MainTex;
			half4 _MainTex_ST;
			half _USpeed;
			half _VSpeed;
			fixed3 _Color;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.vertex.x += cos(2.5*_Time.w+v.vertex.x+v.vertex.y)*.003;
				//float2 anim = float2(_USpeed,_VSpeed);
				v.texcoord += fmod(half2(_USpeed,_VSpeed)*_Time.x,1);
				//v.texcoord += float2(v.vertex.y,v.vertex.x+v.vertex.y)*.005;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				//o.texcoord = v.texcoord;
				o.color = v.color;
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				col.a = dot(half2(saturate (col.r*i.color.r-.2),saturate (i.color.r-.95)),half2(6,20));
				//col.a = 
				col.rgb = _Color;
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
		ENDCG
	}
}

}
