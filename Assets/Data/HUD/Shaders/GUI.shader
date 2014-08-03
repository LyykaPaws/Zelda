Shader "Custom/GUI"
{
	Properties
	{
		_Tint("Tint Color", Color) = (.5, .5, .5, 1.0)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType" = "Transparent" }
		LOD 200
		
		ZTest Off
		ZWrite Off
		Cull Off
	    Fog { Mode Off }
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
		Color[_Tint]
		
		Pass
		{
			SetTexture[_MainTex] { combine texture * primary}
		}
	} 
	FallBack "Diffuse"
}
