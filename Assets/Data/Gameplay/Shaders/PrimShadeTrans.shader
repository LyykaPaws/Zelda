Shader "Custom/N64/PrimShadeTrans"
{
	Properties
	{
		_Color ("Primitive Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input
		{
			float4 color : COLOR;
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = IN.color * tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Transparent/Diffuse"
}
