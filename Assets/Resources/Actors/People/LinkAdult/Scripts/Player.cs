using UnityEngine;
using System.Collections;

// Script by Jack Walker
public class Player : MonoBehaviour 
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
    private MainCamera cameraController;
    public LinkStates state;
    public Vector3 lastPosition;
    private AudioSource[] linkSounds = new AudioSource[8];
    private int stepCounter = 0;
    public int health, maxHealth;
	// Use this for initialization
	void Start() 
    {
        mainCamera = GameObject.Find("PositionCam/DefaultCam").camera;
        cameraController = GameObject.Find("SC_Camera").GetComponent<MainCamera>();
        lastPosition = transform.position;
        maxHealth = 12;
        health = maxHealth;
        
        // Load sound effects
        linkSounds[(int)LinkSoundsEnum.Step0] = gameObject.AddComponent<AudioSource>();
        linkSounds[(int)LinkSoundsEnum.Step0].clip = Resources.Load<AudioClip>("Audio/SFX/Footsteps/OOT_Steps_Dirt1");
        linkSounds[(int)LinkSoundsEnum.Step1] = gameObject.AddComponent<AudioSource>();
        linkSounds[(int)LinkSoundsEnum.Step1].clip = Resources.Load<AudioClip>("Audio/SFX/Footsteps/OOT_Steps_Dirt2");
        linkSounds[(int)LinkSoundsEnum.Step2] = gameObject.AddComponent<AudioSource>();
        linkSounds[(int)LinkSoundsEnum.Step2].clip = Resources.Load<AudioClip>("Audio/SFX/Footsteps/OOT_Steps_Dirt3");
	}
	
	// Update is called once per frame
	void FixedUpdate() 
    {
        FallCheck();
	    switch (state)
        {
            case LinkStates.Idle:
                UpdateIdle();
                break;
            case LinkStates.Running:
                UpdateRunning();
                break;
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
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            state = LinkStates.Running;
        ObstacleTest();
    }
    void UpdateRunning()
    {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

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
				transform.position += (mainCamera.transform.forward * v * 4.0f) * Time.deltaTime;
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
                transform.position += (mainCamera.transform.right * h * 4.0f) * Time.deltaTime;
            else
			{
				if (h < 0.0f)
					transform.eulerAngles = cameraEulerAngles - new Vector3(0, 90, 0);
				else
					transform.eulerAngles = cameraEulerAngles + new Vector3(0, 90, 0);
			}
        }
        if (!cameraController.isTargeting)
            transform.position += (transform.forward * 4.0f) * Time.deltaTime;
    }
}
