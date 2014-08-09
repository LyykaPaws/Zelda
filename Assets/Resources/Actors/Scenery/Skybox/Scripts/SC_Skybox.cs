using UnityEngine;
using System.Collections;



public class SC_Skybox : MonoBehaviour
{
	public float speed = 1.0f;

	public float blend = 0.0f;
	public Material skybox1;
	public Material skybox2;



	void Start()
	{
		GameObject go = GameObject.Find("SC_Camera");
		if (go != null)
			transform.parent = go.transform;
	}	


	void Update()
	{
		SetSkybox(blend, skybox1, skybox2);
		transform.localRotation *= Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.forward);
	}


	public void SetSkybox(float blend, Material skybox1, Material skybox2)
	{
		foreach (Material mtl in transform.renderer.materials)
		{
			string texName = "_" + mtl.name.Replace(" (Instance)","").Substring(6) + "Tex";
			mtl.SetFloat("_Blend", blend);
			mtl.SetTexture("_MainTex", skybox1.GetTexture(texName));
			mtl.SetTexture("_SubTex", skybox2.GetTexture(texName));
		}
	}
}
