Shader "Custom/UI/Skybox"
{
	Properties
	{
	    _Blend("Blend", Range(0.0,1.0)) = 0.5
		_MainTex("Texture 1", 2D) = "white" {}
		_SubTex("Texture 2", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Background"
		}

		ZWrite Off
		Cull Off
		Fog { Mode Off }
		Lighting Off
		
		Pass
		{
			SetTexture[_MainTex] { combine texture }
			SetTexture[_SubTex] { constantColor(0, 0, 0, [_Blend]) combine texture lerp(constant) previous }
		}
	} 
	FallBack "Diffuse"
}
