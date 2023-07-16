Shader "Custom/GuideArrowShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
	}

		SubShader
	{
		Tags
		{
			"Queue" = "Overlay"
			"RenderType" = "Transparent"
		}

		Lighting Off
		ZTest Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata {
				float4 vertex   : POSITION;
				fixed4 color : COLOR;
			};

			struct v2f {
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
			};

			fixed4 _Color;

			v2f vert(appdata IN) {
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.color = _Color;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				return _Color;
			}
			ENDCG
		}
	}
}