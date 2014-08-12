Shader "Custom/N64/PrimShadeBlend"
{
	Properties
	{
		_Color ("Primitive Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_SubTex ("Base (RGB) Trans (A)", 2D) = "white" {}
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
			float2 uv_SubTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = ((tex2D(_MainTex, IN.uv_MainTex) / 2) + tex2D(_SubTex, IN.uv_SubTex) * IN.color / 2.25) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
