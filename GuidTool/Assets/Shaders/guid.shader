// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/guid" {
	Properties {
		[PerRendererData]_MainTex("Main Tex",2D)="white"{}
		_Color("Color",Color)=(1,1,1,1)
		_Center("Center",vector)=(0,0,0,0)
		_Slider("Slider",Float)=100
	}
	SubShader {

		Tags { "RenderType"="Transparent" "Queue"="Geometry"}

		Pass{

			Blend SrcAlpha OneMinusSrcAlpha 

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag 
			#include "UnityCG.cginc" 
			#include "Lighting.cginc" 

			fixed4 _Color;
			float4 _Center;
			float _Slider;

			struct v2f{
				float4 pos:sv_position;
				float3 worldPos:texcoord0;
			}; 

			v2f vert( appdata_base  v){
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex );
				o.worldPos =mul(unity_ObjectToWorld,v.vertex );

				return o;
			}

			fixed4 frag(v2f i):sv_target{
				fixed4 color=_Color;

				color.a*=(distance(i.worldPos.xy,_Center.xy)>_Slider);


				return color;
			}

			ENDCG
		}
	}
	FallBack Off
}
