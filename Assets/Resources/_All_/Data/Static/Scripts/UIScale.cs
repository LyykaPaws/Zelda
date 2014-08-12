using UnityEngine;
using System.Collections;



public class UIScale : MonoBehaviour
{

	private static readonly int xRes = 320, yRes = 240;
	private static Camera uiCam;



	public static void TransformRect(Transform t, Rect r)
	{
		TransformRect(t, r, false, false);
	}

	public static void TransformRect(Transform t, Rect r, bool isPlane)
	{
		TransformRect(t, r, isPlane, false);
	}

	public static void TransformRect(Transform t, Rect r, bool isPlane, bool zup)
	{
		if (uiCam == null)
		{
			GameObject go = GameObject.Find("UICam");
			if (go != null)
				uiCam = go.GetComponent<Camera>();
		}
		if (uiCam != null)
		{
			float xr = uiCam.aspect / (float)xRes;
			float yr = 1.0f / (float)yRes;

			Vector3 rv = Vector3.right, fv, uv;
			if (zup)
			{
				fv = Vector3.up;
				uv = Vector3.forward;
			}
			else
			{
				fv = Vector3.forward;
				uv = Vector3.up;
			}

			float xp = (float)xRes - r.width / 2.0f - r.x + 1.0f;
			float yp = (float)yRes - r.height / 2.0f - r.y + 1.0f;
			if (isPlane)
			{
				r.width /= 2.0f;
				r.height /= 2.0f;
			}

			t.localPosition =
				xp * xr * uiCam.orthographicSize * Vector3.right +
				yp * yr * uiCam.orthographicSize * Vector3.back;
			t.localScale = r.width * xr * rv + r.height * yr * uv + fv;
		}
	}
}
