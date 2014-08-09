using UnityEngine;
using System.Collections;



public class MainCamera : MonoBehaviour
{
	private Transform player;

	private Transform rcam;
	private Transform pcam;

	private float rotation = 0.0f;
	private float distance = 4.5f;

	public bool isTargeting = false;
	private AudioSource zTargetSound;



	void Start()
	{
		GameObject po = GameObject.FindGameObjectWithTag("Player");
		if (po != null)
			player = po.transform;
		pcam = transform.FindChild("PositionCam");
		rcam = transform.FindChild("RotationCam");
		zTargetSound = gameObject.AddComponent<AudioSource>();
		zTargetSound.clip = Resources.Load<AudioClip>("Audio/SFX/Menus/OOT_ZTarget_Center1");
	}
	
	void Update()
	{
		if (!isTargeting)
		{
			if (Input.GetKey(KeyCode.J))
				rotation += 100.0f * Time.deltaTime;
			else if (Input.GetKey(KeyCode.L))
				rotation -= 100.0f * Time.deltaTime;
			if (rotation < 0.0f)
				rotation += 360.0f;
			else if (rotation >= 360.0f)
				rotation -= 360.0f;
		}
		if (Input.GetKey(KeyCode.I))
			distance -= 5.0f * Time.deltaTime;
		else if (Input.GetKey(KeyCode.K))
			distance += 5.0f * Time.deltaTime;
		if (distance < 2.0f)
			distance = 2.0f;
		else if (distance > 7.5f)
			distance = 7.5f;
		if (Input.GetKeyDown(KeyCode.Z))
		{
			zTargetSound.Play();
			isTargeting = true;
			rotation = player.eulerAngles.y;
		}
		else if (Input.GetKeyUp(KeyCode.Z))
			isTargeting = false;

		if (player != null && pcam != null)
		{
			float upvec = distance / 7.5f;
			Vector3 targetPos = player.position + 2.0f * upvec * Vector3.up - distance * (Quaternion.AngleAxis(rotation, Vector3.up) * Vector3.forward);
			Quaternion targetRot = Quaternion.LookRotation(player.position + (.175f + upvec) * Vector3.up - targetPos);

			pcam.position = targetPos;
			pcam.rotation = targetRot;
			if (rcam != null)
				rcam.rotation = pcam.rotation;
		}
	}
}
