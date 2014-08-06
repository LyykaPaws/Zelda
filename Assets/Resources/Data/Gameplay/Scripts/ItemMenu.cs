using UnityEngine;
using System.Collections;

// Script by Jack Walker
public class ItemMenu : MonoBehaviour 
{
    private HUD hud;
    private Player player;
	// Use this for initialization
	void Start () 
    {
        hud = GameObject.Find("RotationCam/GUICam/HUD").GetComponent<HUD>();
        player = GameObject.Find("/Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
}
