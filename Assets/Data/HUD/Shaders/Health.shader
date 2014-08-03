Shader "Custom/Health"
{
	Properties
	{
		_Tex0("0 / 4", 2D) = "white" {}
		_Tex1("1 / 4", 2D) = "white" {}
		_Tex2("2 / 4", 2D) = "white" {}
		_Tex3("3 / 4", 2D) = "white" {}
		_Tex4("4 / 4", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Cull Back
		Fog { Mode Off }
		Lighting Off
		
		Pass
		{
	        SetTexture[_Tex4] { combine texture }
		}
	} 

	FallBack "Diffuse"
}
