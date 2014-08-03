using UnityEngine;
using System.Collections;



public class MainCamera : MonoBehaviour
{
	private Transform player;

	private Transform rcam;
	private Transform pcam;



	void Start()
	{
		GameObject po = GameObject.FindGameObjectWithTag("Player");
		if (po != null)
			player = po.transform;
		pcam = transform.FindChild("PositionCam");
		rcam = transform.FindChild("RotationCam");
	}
	
	void Update()
	{
		if (player != null && pcam != null)
		{
			pcam.position = Vector3.MoveTowards(pcam.position, player.position + 4.5f * Vector3.forward + 2.0f * Vector3.up, 3.0f * Time.deltaTime);
			pcam.rotation = Quaternion.LookRotation(player.position - pcam.position);
			if (rcam != null)
				rcam.rotation = pcam.rotation;
		}
	}
}
