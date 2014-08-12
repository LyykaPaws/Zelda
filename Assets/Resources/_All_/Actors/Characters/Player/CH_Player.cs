using UnityEngine;
using System.Collections;



public class CH_Player : MonoBehaviour
{
	public enum LinkStates
	{
		Idle,
		Running,
		Rolling,
		Falling
	};

	public enum LinkSoundsEnum
	{
		Step0,
		Step1,
		Step2
	};

	public UnityEngine.Camera mainCamera;
	private SC_Camera cameraController;
	public LinkStates state;
	public Vector3 lastPosition;
	private AudioSource[] linkSounds = new AudioSource[8];
	private int stepCounter = 0;
	public int health, maxHealth;



	void Start()
	{
		if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
		{
			GameObject.Destroy(gameObject);
			return;
		}
		mainCamera = GameObject.Find("Position/DefaultCam").camera;
		cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SC_Camera>();
		lastPosition = transform.position;
		maxHealth = 76;
		health = maxHealth - 3;
		
		// Load sound effects
		linkSounds[(int)LinkSoundsEnum.Step0] = gameObject.AddComponent<AudioSource>();
		linkSounds[(int)LinkSoundsEnum.Step0].clip = GameEngine.GetSound("OoT:Footsteps/Dirt1");
		linkSounds[(int)LinkSoundsEnum.Step1] = gameObject.AddComponent<AudioSource>();
		linkSounds[(int)LinkSoundsEnum.Step1].clip = GameEngine.GetSound("OoT:Footsteps/Dirt2");
		linkSounds[(int)LinkSoundsEnum.Step2] = gameObject.AddComponent<AudioSource>();
		linkSounds[(int)LinkSoundsEnum.Step2].clip = GameEngine.GetSound("OoT:Footsteps/Dirt3");
	}
	
	void FixedUpdate() 
	{
		FallCheck();
		switch (state)
		{
			case LinkStates.Idle:
				transform.GetChild(0).animation["Idle"].speed = 1.0f;
				transform.GetChild(0).animation.Play("Idle");
				UpdateIdle();
				break;
			case LinkStates.Running:
			{
				if (cameraController.isTargeting)
				{
					float h = Input.GetAxis("H-Axis");
					if (h < -.5f)
					{
						transform.GetChild(0).animation["SidestepL"].speed = 1.5f;
						transform.GetChild(0).animation.Play("SidestepL");
					}
					else if (h > .5f)
					{
						transform.GetChild(0).animation["SidestepR"].speed = 1.5f;
						transform.GetChild(0).animation.Play("SidestepR");
					}
					else
					{
						transform.GetChild(0).animation["Run"].speed = 1.5f;
						transform.GetChild(0).animation.Play("Run");
					}
				}
				else
				{
					transform.GetChild(0).animation["Run"].speed = 1.5f;
					transform.GetChild(0).animation.Play("Run");
				}
				UpdateRunning();
				break;
			}
			case LinkStates.Rolling:
				
				break;
		}
	}
	
	void OnCollisionEnter(Collision other)
	{   
	}
	
	void FallCheck()
	{
		if (Mathf.Abs(lastPosition.y - transform.position.y) >= 0.01f && lastPosition.y > transform.position.y)
		{
			state = LinkStates.Falling;
		}
		else
		{
			if (state == LinkStates.Falling)
				state = LinkStates.Idle;
		}
		lastPosition = transform.position;
	}
	
	void ObstacleTest()
	{
		RaycastHit hitTopMost;
		RaycastHit hitTop;
		RaycastHit hitMid;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		// Top-most vector
		bool topmostRaycast = Physics.Raycast(transform.position + new Vector3(0, 2f, 0), fwd, out hitTopMost, 100);
		bool topRaycast = Physics.Raycast(transform.position + new Vector3(0, 1, 0), fwd, out hitTop, 100);
		bool midRaycast = Physics.Raycast(transform.position + new Vector3(0, 0, 0), fwd, out hitMid, 100);
		Debug.DrawRay(transform.position + new Vector3(0, 2f, 0), fwd * 50, Color.blue);
		Debug.DrawRay(transform.position + new Vector3(0, 1, 0), fwd * 50, Color.blue);
		Debug.DrawRay(transform.position + new Vector3(0, 0, 0), fwd * 50, Color.blue);
		// Top ray
		if ((hitTopMost.collider == null || hitTopMost.distance >= 0.7f) && (hitTop.collider != null && hitTop.distance <= 0.65f) && hitMid.distance <= 0.65f)
		{
			if (hitTop.collider.gameObject.name.Contains("ob"))
			{
				Debug.Log("toppt");
				Debug.Log("Topmost " + hitTopMost.distance);
				Debug.Log("Top " + hitTop.distance);
				Debug.Log("Mid " + hitMid.distance);
				transform.position += (transform.forward * 1.0f) + (transform.up * 2.5f);
			}
		}
		// Mid ray
		if ((hitTopMost.collider == null || hitTopMost.distance >= 0.7f) && (hitTop.collider == null || hitTop.distance >= 0.7f) && (hitMid.collider != null && hitMid.distance <= 0.6f))
		{
			if (hitMid.collider.gameObject.name.Contains("ob"))
			{
				Debug.Log("midpt");
				transform.position += (transform.forward * 1.0f) + (transform.up * 0.80f);
			}
		}
		//Debug.Log("Topmost " + hitTopMost.distance);
		//Debug.Log("Top " + hitTop.distance);
		//Debug.Log("Mid " + hitMid.distance);
		
		
		
		//Debug.Log("Topmost " + topmostRaycast);
		//Debug.Log("Top " + topRaycast);
		//Debug.Log("Mid " + midRaycast);
	}
	
	void StepSFX()
	{
		stepCounter++;
		if (linkSounds[stepCounter % 2].isPlaying == false)
			linkSounds[stepCounter % 2].Play();
	}
	
	void UpdateIdle()
	{
		if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
			state = LinkStates.Running;
		ObstacleTest();
	}
	void UpdateRunning()
	{
		float h = Input.GetAxis("H-Axis");
		float v = Input.GetAxis("V-Axis");
		
		if (h == 0.0f && v == 0.0f)
			state = LinkStates.Idle;
		StepSFX();
		ObstacleTest();
		Vector3 cameraEulerAngles = mainCamera.transform.eulerAngles;
		cameraEulerAngles.x = 0;
		cameraEulerAngles.z = 0;
		if (v != 0.0f)
		{
			if (cameraController.isTargeting)
				transform.position += (mainCamera.transform.forward * v * 5.5f) * Time.deltaTime;
			else
			{
				if (v < 0.0f)
					transform.eulerAngles = cameraEulerAngles + new Vector3(0, 180, 0);
				else
					transform.eulerAngles = cameraEulerAngles;
			}
		}
		if (h != 0.0f)
		{
			if (cameraController.isTargeting)
				transform.position += (mainCamera.transform.right * h * 5.5f) * Time.deltaTime;
			else
			{
				if (h < 0.0f)
					transform.eulerAngles = cameraEulerAngles - new Vector3(0, 90, 0);
				else
					transform.eulerAngles = cameraEulerAngles + new Vector3(0, 90, 0);
			}
		}
		if (!cameraController.isTargeting)
			transform.position += (transform.forward * 5.5f) * Time.deltaTime;
	}
}
