using UnityEngine;
using System.Collections;



public class HUD : MonoBehaviour
{
	private Player player;
	private Camera guiCam;
	private Transform menu;

	public bool isShowing = false;
	public float rotSpeed = 0.0f;
	private float rotation = 0.0f;
	private float targetRot = 0.0f;

    AudioSource[] menuSounds;

	void Start()
	{
		GameObject co = GameObject.Find("GUICam");
		if (co != null)
			guiCam = (Camera)co.GetComponent("Camera");
		co = GameObject.FindGameObjectWithTag("Player");
		if (co != null)
			player = (Player)co.GetComponent("Player");
		co = GameObject.Find("PauseMenu");
		if (co != null)
			menu = co.transform;
		if (menu != null)
		{
			menu.gameObject.SetActive(false);
			Color c1 = Color.white, c2 = Color.white;
			for (int i = 0; i < 4; i++)
			{
				Transform ss = menu.GetChild(i);
				if (i == 0)	// Equip
				{
					c1 = new Color(.039216f, .196078f, .156863f);
					c2 = new Color(.352941f, .392157f, .235294f);
				}
				else if (i == 1) // Select
				{
					c1 = new Color(.039216f, .196078f, .313725f);
					c2 = new Color(.327451f, .392157f, .509804f);
				}
				else if (i == 2) // Map
				{
					c1 = new Color(.313725f, .156863f, .117647f);
					c2 = new Color(.549020f, .156863f, .156863f);
				}
				else if (i == 3) // Quest
				{
					c1 = new Color(.313725f, .313725f, .196078f);
					c2 = new Color(.470588f, .470588f, .327451f);
				}
				for (int j = 0; j < 3; j++)
				{
					Transform p = ss.GetChild(j);
					MeshFilter mf = (MeshFilter)p.GetComponent("MeshFilter");
					mf.mesh = new Mesh();
					mf.mesh.vertices = new Vector3[]
					{
						new Vector3(5f, 0f,  5f),
						new Vector3(5f, 0f, -5f),
						new Vector3(-5f, 0f,-5f),
						new Vector3(-5f, 0f, 5f)
					};
					if (j == 0)
						mf.mesh.colors = new Color[] { c1, c1, c2, c2 };
					else if (j == 1)
						mf.mesh.colors = new Color[] { c2, c2, c2, c2 };
					else
						mf.mesh.colors = new Color[] { c2, c2, c1, c1 };
					mf.mesh.uv = new Vector2[]
					{
						new Vector2(0f, 0f),
						new Vector2(0f, 1f),
						new Vector2(1f, 1f),
						new Vector2(1f, 0f)
					};
					mf.mesh.triangles = new int[]
					{
						0, 1, 2,
						2, 3, 0
					};
					mf.mesh.Optimize();
				}
			}
		}

        // Initialize sound effects
        menuSounds = new AudioSource[4];
		menuSounds[0] = gameObject.AddComponent<AudioSource>();
		menuSounds[0].clip = Resources.Load<AudioClip>("Audio/SFX/Menus/OOT_PauseMenu_Open");
		menuSounds[1] = gameObject.AddComponent<AudioSource>();
		menuSounds[1].clip = Resources.Load<AudioClip>("Audio/SFX/Menus/OOT_PauseMenu_Close");
		menuSounds[2] = gameObject.AddComponent<AudioSource>();
        menuSounds[2].clip = Resources.Load<AudioClip>("Audio/SFX/Menus/OOT_PauseMenu_Turn_Left");
        menuSounds[3] = gameObject.AddComponent<AudioSource>();
        menuSounds[3].clip = Resources.Load<AudioClip>("Audio/SFX/Menus/OOT_PauseMenu_Turn_Right");
	}
	
	void Update()
	{
		if (guiCam != null)
		{
			transform.localScale = new Vector3(guiCam.aspect, 1, 1);
			if (menu != null)
				menu.localScale = new Vector3(guiCam.aspect, 1, guiCam.aspect);
		}
		if (menu != null)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				isShowing = !isShowing;
				player.enabled = !isShowing;
				menu.gameObject.SetActive(isShowing);
				if (isShowing)
					menuSounds[0].Play();
				else
					menuSounds[1].Play();
			}
			if (menu.gameObject.activeSelf)
			{
				if (rotSpeed == 0.0f)
				{
					if (Input.GetKeyDown(KeyCode.A))
					{
						rotSpeed = 175.0f;
						targetRot = rotation + 90.0f;
						menuSounds[2].Play();
					}
					else if (Input.GetKeyDown(KeyCode.S))
					{
						rotSpeed = -175.0f;
						targetRot = rotation - 90.0f;
						menuSounds[3].Play();
					}
				}
				else if (!Mathf.Approximately(rotation, targetRot))
				{
					rotation += rotSpeed * Time.deltaTime;
					if ((rotSpeed > 0.0f && rotation > targetRot) || (rotSpeed < 0.0f && rotation < targetRot))
					{
						rotSpeed = 0.0f;
						rotation = targetRot;
						if (rotation >= 360.0f)
							rotation -= 360.0f;
						else if (rotation < 0.0f)
							rotation += 360.0f;
					}
					menu.localRotation = Quaternion.AngleAxis((float)(rotation), Vector3.up);
				}
				else
					rotSpeed = 0.0f;
			}
		}
	}
}
