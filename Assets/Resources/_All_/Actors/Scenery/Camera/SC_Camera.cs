using UnityEngine;
using System.Collections;



public class SC_Camera : MonoBehaviour
{
	private Transform player;
	
	private Transform rcam;
	private Transform pcam;
	
	private float rotation = 0.0f;
	private float distance = 4.5f;
	
	private float zTargetDistance = 0.0f;
	private float zTargetRotation = 4.5f;

	public bool isTargeting = false;
	private AudioSource[] zTargetSounds;
	private AudioSource battleMusic;
	private GameObject oldEnemy = null;



	void Start()
	{
		if (GameObject.FindGameObjectsWithTag("MainCamera").Length > 1)
		{
			GameObject.Destroy(gameObject);
			return;
		}
		GameObject po = GameObject.FindGameObjectWithTag("Player");
		if (po != null)
			player = po.transform;
		pcam = transform.FindChild("Position");
		rcam = transform.FindChild("Rotation");
		zTargetSounds = new AudioSource[4];
		zTargetSounds[0] = gameObject.AddComponent<AudioSource>();
		zTargetSounds[0].clip = GameEngine.GetSound("OoT:Interface/ZTarget_Center1");
		zTargetSounds[1] = gameObject.AddComponent<AudioSource>();
		zTargetSounds[1].clip = GameEngine.GetSound("OoT:Interface/ZTarget_Enemy");
		zTargetSounds[2] = gameObject.AddComponent<AudioSource>();
		zTargetSounds[2].clip = GameEngine.GetSound("OoT:Interface/ZTarget_Cancel");
		battleMusic = gameObject.AddComponent<AudioSource>();
		battleMusic.clip = GameEngine.GetMusic("OoT:Normal Battle");
	}
	
	void EnemyBGMCheck()
	{
		foreach (GameObject otherObject in GameObject.FindGameObjectsWithTag("ZTarget_Enemy"))
		{
			float dist = Vector3.Distance(otherObject.transform.position, player.transform.position);
			if (dist < 8.0f)
			{
				if (!battleMusic.isPlaying)
					battleMusic.Play();
				float volume = 1.4f / dist;
				battleMusic.volume = volume;
				//Debug.Log(volume);            
			}
			else
			{
				if (battleMusic.isPlaying)
					battleMusic.Stop();
			}
		}
	}
	
	void ZTargetCheck()
	{
		foreach (GameObject otherObject in GameObject.FindGameObjectsWithTag("ZTarget_Enemy"))
		{
			float dist = Vector3.Distance(otherObject.transform.position, player.transform.position);
			if (dist < 4.0f)
			{
				if (oldEnemy != otherObject)
				{
					oldEnemy = otherObject;
					zTargetSounds[1].Play();
				}
				player.LookAt(otherObject.transform);
				zTargetRotation = player.eulerAngles.y;
				zTargetRotation -= 80;
				zTargetDistance = dist * 1.5f;
				if (zTargetDistance <= 3.5f)
					zTargetDistance = 3.5f;
				Debug.Log(zTargetDistance);
				//Debug.Log("Found an object.");
			}
			else
			{
				
			}
		}
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.J))
			rotation += 100.0f * Time.deltaTime;
		else if (Input.GetKey(KeyCode.L))
			rotation -= 100.0f * Time.deltaTime;
		if (rotation < 0.0f)
			rotation += 360.0f;
		else if (rotation >= 360.0f)
			rotation -= 360.0f;
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
			if (oldEnemy != null)
			{
				oldEnemy = null;
				zTargetSounds[2].Play();
			}
			
		}
		else
		{
			ZTargetCheck();
		}
		EnemyBGMCheck();
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
			zTargetSounds[0].Play();
			isTargeting = true;
			rotation = player.eulerAngles.y;
		}
		else if (Input.GetKeyUp(KeyCode.Z))
		{
			isTargeting = false;
		}
		
		if (player != null && pcam != null)
		{
			float upvec = 0.0f;
			Vector3 targetPos;
			Quaternion targetRot;
			if (isTargeting && oldEnemy != null)
			{
				upvec = zTargetDistance / 7.5f;
				targetPos = player.position + 2.0f * upvec * Vector3.up - zTargetDistance * (Quaternion.AngleAxis(zTargetRotation, Vector3.up) * Vector3.forward);
				targetRot = Quaternion.LookRotation(player.position + (.175f + upvec) * Vector3.up - targetPos);
			}
			else
			{
				upvec = distance / 7.5f;
				targetPos = player.position + 2.0f * upvec * Vector3.up - distance * (Quaternion.AngleAxis(rotation, Vector3.up) * Vector3.forward);
				targetRot = Quaternion.LookRotation(player.position + (.175f + upvec) * Vector3.up - targetPos);
			}
			
			
			
			pcam.position = targetPos;
			pcam.rotation = targetRot;
			if (rcam != null)
				rcam.rotation = pcam.rotation;
		}
	}
}
