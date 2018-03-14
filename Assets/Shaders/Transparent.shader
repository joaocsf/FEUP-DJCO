Shader "Library/Transparent"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "bump" {}
		_Transparency("Transparency", Range(0.0,1.0)) = 0.5
	}
	SubShader
	{
		Tags { "Queue"="Geometry+1"  }
		LOD 100
		ZTest Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
	

		GrabPass
		{}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 grabPos: TEXCOORD1;
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			float _Transparency;

			sampler2D _MainTex;
			sampler2D _GrabTexture;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.grabPos = ComputeGrabScreenPos(o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed3 displacement = UnpackNormal(tex2D(_MainTex, i.uv));
				float t = 1- clamp(distance(i.uv, float2(0.5, 0.5)), 0.0, 0.5)/0.5;
				fixed4 back = tex2D(_GrabTexture, i.grabPos + displacement.rg*_Transparency);

				back.a *= t;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				
				return back;
			}
			ENDCG
		}
	}
}
