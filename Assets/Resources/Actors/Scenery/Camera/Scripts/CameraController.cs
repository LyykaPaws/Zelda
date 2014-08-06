using UnityEngine;
using System.Collections;

// Script by Jack Walker
public class CameraController : MonoBehaviour 
{
    private Player player;
    private float cameraTurnDelay;
    public bool isZTargeting;
    public AudioSource zTargetSound;
    private bool hasPlayedZTargetSound;
	// Use this for initialization
	void Start() 
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        zTargetSound = gameObject.AddComponent<AudioSource>();
        zTargetSound.clip = Resources.Load<AudioClip>("Audio/SFX/Menus/OOT_ZTarget_Center1");
        hasPlayedZTargetSound = true;
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (!player.enabled)
            return;
        CameraFollowPlayer();
        CameraTurnCheck();
        CameraInputCheck();
        CameraZTarget();
	}

    void CameraInputCheck()
    {
        // Z-Targeting
        if (Input.GetKey(KeyCode.Z))
        {
            if (!hasPlayedZTargetSound)
            {
                zTargetSound.Play();
                hasPlayedZTargetSound = true;
            }
            isZTargeting = true;
        }
        else
        {
            hasPlayedZTargetSound = false;
            isZTargeting = false;
        }
    }

    void CameraZTarget()
    {
        if (isZTargeting == true)
        {
            transform.eulerAngles = player.transform.eulerAngles;
        }
    }


    void CameraFollowPlayer()
    {
        transform.position = player.transform.position + (transform.forward * -7.5f) + (transform.up * 1.0f);
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = 20 + (Mathf.Abs(player.transform.position.x - transform.position.x));
        transform.eulerAngles = eulerAngles;
    }

    void CameraTurnCheck()
    {
        Vector3 cameraEulerAngles = transform.eulerAngles;
        if (Time.time > cameraTurnDelay)
        {
            if (player.state != Player.LinkStates.Idle)
            {
                cameraTurnDelay = Time.time + 0.8f;
                return;
            }
            if (transform.eulerAngles.y > player.transform.eulerAngles.y)
            {
                transform.eulerAngles -= new Vector3(0, 2, 0);
            }
            if (transform.eulerAngles.y < player.transform.eulerAngles.y)
            {
                transform.eulerAngles += new Vector3(0, 2, 0);
            }
        }
    }
}
