Shader "Unlit/ThreeLinearGradient"
{
	Properties
	{
		_TopColor("Top color", Color) = (1,0,0,1)
		_MidColor("Middle color", Color) = (0,1,0,1)
		_BottomColor("Bottom color", Color) = (0,0,1,1)
		_Middle ("Middle", Range(0.001, 0.999)) = 0.5
	}
		SubShader
	{
		Tags {"Queue"="Background"  "IgnoreProjector"="True"}
        LOD 100
		ZWrite On
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag


			#include "UnityCG.cginc"

			fixed4 _TopColor;
			fixed4 _MidColor;
			fixed4 _BottomColor;
			float _Middle;

			struct v2f {
				float4 position : SV_POSITION;
				fixed4 texcoord : TEXCOORD0;
			};

			
			v2f vert(appdata_full v)
			{
				v2f o;
				o.position = mul(UNITY_MATRIX_MVP, v.vertex);
				//o.color = lerp(lerp(_TopColor, _MidColor, v.texcoord.x), lerp(_MidColor, _BottomColor, v.texcoord.x), v.texcoord.x);
				o.texcoord = v.texcoord;
				return o;
			}
			
			fixed4 frag(v2f i) : COLOR
			{
				float4 color = lerp(_BottomColor, _MidColor,i.texcoord.x/ _Middle)*step(i.texcoord.x,_Middle);
				color+=lerp(_MidColor,_TopColor,(i.texcoord.x- _Middle)/(1- _Middle))*step( _Middle,i.texcoord.x);
				//float4 color = lerp(_BottomColor, _MidColor,i.texcoord.x/ _Middle);
				//color+=lerp(_MidColor,_TopColor,(i.texcoord.x- _Middle)/(1- _Middle));
				color.a = 1;
				return color;
			}
			ENDCG
		}
	}
}
