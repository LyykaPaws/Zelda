using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;



public class AnimationFix : AssetPostprocessor
{
	private AnimationClip clip;



	void OnPostprocessModel(GameObject go)
	{
		ModelImporter mi = (ModelImporter)assetImporter;
		if (mi.animationType == ModelImporterAnimationType.Legacy)
		{
			clip = go.animation.clip;
			RemoveKeys(go.transform.GetChild(0), "", true);
		}
	}

	void RemoveKeys(Transform t, string path, bool isRoot)
	{
		if (!isRoot)
			clip.SetCurve(path + t.name, typeof(Transform), "m_LocalPosition", null);
		clip.SetCurve(path + t.name, typeof(Transform), "m_LocalScale", null);
		for (int i = 0; i < t.childCount; i++)
			RemoveKeys(t.GetChild(i), path + t.name + "/", false);
	}
}
