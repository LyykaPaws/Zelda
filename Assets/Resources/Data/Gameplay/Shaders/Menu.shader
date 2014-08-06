Shader "Custom/GUI/Menu" {
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
	
	Category
	{
		Tags { "Queue" = "Geometry" }
		LOD 200
				
		ZTest Off
		Cull Back
		Fog { Mode Off }
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha

		BindChannels
		{
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}
		
		SubShader
		{
			Pass
			{
				SetTexture[_MainTex] { combine texture * primary }
			}
		} 
	}
	FallBack "Diffuse"
}
