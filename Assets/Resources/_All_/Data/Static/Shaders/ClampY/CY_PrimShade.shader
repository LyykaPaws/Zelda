Shader "Custom/N64/Clamp Y/PrimShade"
{
	Properties
	{
		_Color ("Primitive Color", Color) = (1,1,1,1)
		_MainTex ("Clamp Texture", 2D) = "white" {}
		_SubTex ("Repeat Texture", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Diffuse"
		}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _SubTex;
		fixed4 _Color;

		struct Input
		{
			float4 color : COLOR;
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			float2 uv = IN.uv_MainTex;
			fixed4 c = IN.color * _Color;
			if (uv.y <= 0 || uv.y >= 1)
				c *= tex2D(_SubTex, uv);
			else if (uv.y >= .9725)
			{
				uv.y -= .0275;
				c *= tex2D(_MainTex, uv);
			}
			else if (uv.y <= .0275)
			{
				uv.y += .0275;
				c *= tex2D(_MainTex, uv);
			}
			else
				c *= tex2D(_MainTex, uv);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
