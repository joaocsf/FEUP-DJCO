Shader "Screen/Transition"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Texture("BlendTexture", 2D) = "white"{} 
		_Position("Position", Vector) = (0,0, 0.5,0.5)
		_Lerp("Lerp", range(0,1)) = 1
		_Color("Color", Color) = (0,0,0,0)
		_UseImage("UseImage",range(0,1)) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _Texture;
			float2 _Position;
			float _Lerp;
			float _UseImage;
			float4 _Color;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				float dist = distance(i.uv.xy, _Position.xy);

				if(_UseImage < 0.5){
					if(dist/2 > 1 - _Lerp)
						col.rgb = _Color;
				}else
					if(tex2D(_Texture,i.uv).r < _Lerp*1.01)
						col.rgb = _Color;

				return col;
			}
			ENDCG
		}
	}
}
