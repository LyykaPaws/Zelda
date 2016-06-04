using UnityEngine;
using System.Collections;



public class UI_HUD : MonoBehaviour
{

    private CH_Player player;
    private Camera guiCam;
    private Transform menu;

    public bool isShowing = false;
    public float rotSpeed = 0.0f;
    private float rotation = 0.0f;
    private float targetRot = 0.0f;

    private float scaleBoost, scaleDir;
    private Vector3 baseScale = new Vector3(15.0f, 15.0f, 1.0f);
    private int playerOldHearts;
    private int itemMenuCursor = 0;
    public CH_Player.LinkStates oldPlayerState;
    private int aBoost;
    private bool aFlip = false;

    AudioSource[] menuSounds;

    void Start()
    {
        GameObject co = GameObject.Find("UICam");
        if (co != null)
            guiCam = (Camera)co.GetComponent("Camera");
        co = GameObject.FindGameObjectWithTag("Player");
        if (co != null)
            player = (CH_Player)co.GetComponent("CH_Player");
        co = GameObject.Find("UI_PauseMenu");
        if (co != null)
        {
            menu = co.transform;
        }
        if (menu != null)
        {
            menu.gameObject.SetActive(false);
            Color c1 = Color.white, c2 = Color.white;
            for (int i = 0; i < 4; i++)
            {
                Transform ss = menu.GetChild(i).Find("Background");
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
		menuSounds[0].clip = GameEngine.GetSound("OoT:Interface/PauseMenu_Open");
        menuSounds[1] = gameObject.AddComponent<AudioSource>();
		menuSounds[1].clip = GameEngine.GetSound("OoT:Interface/PauseMenu_Close");
        menuSounds[2] = gameObject.AddComponent<AudioSource>();
		menuSounds[2].clip = GameEngine.GetSound("OoT:Interface/PauseMenu_Turn_Left");
        menuSounds[3] = gameObject.AddComponent<AudioSource>();
        menuSounds[3].clip = GameEngine.GetSound("OoT:Interface/PauseMenu_Turn_Right");

        scaleBoost = 1.0f;
        scaleDir = 0.5f;
        playerOldHearts = player.health;

        GameObject heartContainerObject = GameObject.Find("Health");
        for (int i = 0; i < heartContainerObject.transform.childCount; i++)
        {
            GameObject.DestroyObject(heartContainerObject.transform.GetChild(i).gameObject);
        }
    }


    void Update()
    {
        if (guiCam != null)
        {
            transform.localScale = new Vector3(guiCam.aspect, 1, 1);
            if (menu != null)
                menu.localScale = new Vector3(guiCam.aspect, 1, guiCam.aspect);
            DrawHearts();
            AButtonDraw();
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


    // Todo: Clean code.
    void AButtonDraw()
    {
        GameObject aButtonObject = GameObject.Find("AButton");
        //GameObject aButtonTextObject = GameObject.Find("AButtonText");

        if (aFlip == false)
        {
            if (player.state != oldPlayerState)
            {
                aFlip = true;
                aBoost = 2;
                aButtonObject.transform.Rotate(7, 0, 0);
                oldPlayerState = player.state;
            }
        }
        if (aFlip == true)
        {
            if (aButtonObject.transform.localEulerAngles.x >= 90)
            {
                aBoost = -4;
            }
            if (aButtonObject.transform.localEulerAngles.x <= 4)
            {
                aBoost = 4;
                aFlip = false;
                return;
            }
            aButtonObject.transform.Rotate(aBoost, 0, 0);
        }
        oldPlayerState = player.state;
    }


    void DrawHearts()
    {
        if (scaleBoost > 1.2f)
        {
            scaleDir = -0.5f;
        }
        if (scaleBoost < 1.0f)
        {
            scaleDir = 0.5f;
        }
        scaleBoost += scaleDir * Time.deltaTime;

        int desiredHearts = player.maxHealth / 4;
        // Check if we need to add heart objects
        // This code is a mess...
        GameObject heartContainerObject = GameObject.Find("Health");
        if (heartContainerObject.transform.childCount != desiredHearts || playerOldHearts != player.health)
        {
            for (int i = 0; i < heartContainerObject.transform.childCount; i++)
            {
                GameObject.DestroyObject(heartContainerObject.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < desiredHearts; i++)
            {
                GameObject heartObject = new GameObject("Heart " + i);
                heartObject.transform.parent = heartContainerObject.transform;
                heartObject.transform.localPosition = new Vector3(0.0f + ((i % 10) * 0.415f), 0.0f - ((i / 10) * 0.415f), 0.0f);
                heartObject.transform.localRotation = Quaternion.identity;
                heartObject.transform.localScale = baseScale;
                SpriteRenderer spriteRenderer = heartObject.AddComponent<SpriteRenderer>();
				if (i > (player.health / 4))
                {
                    spriteRenderer.sprite = GameEngine.GetSprite(GameEngine.GetActorPath("OoT:UI_HUD"), "0-4 Heart1");
                    Debug.Log("Upper " + i);
                }
                else if (i == (player.health / 4))
                    {
                        switch (player.health % 4)
                        {
                            case 0:
							spriteRenderer.sprite = GameEngine.GetSprite(GameEngine.GetActorPath("OoT:UI_HUD"), "0-4 Heart1");
                                break;
                            case 1:
							spriteRenderer.sprite = GameEngine.GetSprite(GameEngine.GetActorPath("OoT:UI_HUD"), "1-4 Heart1");
                                break;
                            case 2:
							spriteRenderer.sprite = GameEngine.GetSprite(GameEngine.GetActorPath("OoT:UI_HUD"), "2-4 Heart1");
								break;
                            case 3:
							spriteRenderer.sprite = GameEngine.GetSprite(GameEngine.GetActorPath("OoT:UI_HUD"), "3-4 Heart1");
                                break;
                            case 4:
							spriteRenderer.sprite = GameEngine.GetSprite(GameEngine.GetActorPath("OoT:UI_HUD"), "4-4 Heart1");
                                break;
                        }
                    }
                    else
                    {
					spriteRenderer.sprite = GameEngine.GetSprite(GameEngine.GetActorPath("OoT:UI_HUD"), "4-4 Heart1");
                        Debug.Log(i);
                    }
                spriteRenderer.GetComponent<Renderer>().material.shader = Resources.Load<Shader>("_All_/Data/Static/Shaders/Default/Sprite");
                spriteRenderer.GetComponent<Renderer>().material.SetColor("_Color", new Color32(253, 98, 98, 255));
                heartObject.layer = 5;
            }
        }
        playerOldHearts = player.health;
        // Make the last heart scale dynamically
        //Debug.Log(((float)player.health / 4));
        GameObject lastHeart = GameObject.Find("Heart " + Mathf.CeilToInt(((float)player.health / 4) - 1));
        if (lastHeart == null)
            return;
        lastHeart.transform.localScale = baseScale * scaleBoost;
    }
}