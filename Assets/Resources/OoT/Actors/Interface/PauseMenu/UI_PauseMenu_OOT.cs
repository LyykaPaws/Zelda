using UnityEngine;
using System.Collections;



public class UI_PauseMenu_OOT : MonoBehaviour
{
	private struct MenuColor
	{
		public Color color1, color2;


		public MenuColor(Color c1, Color c2)
		{
			color1 = c1;
			color2 = c2;
		}
	}



	public bool isOpen = false;


	private Camera uiCam;

	private MenuColor[] menuColor =
	{
		// Select Item Subscreen
		new MenuColor(new Color(.039216f, .196078f, .313725f), new Color(.327451f, .392157f, .509804f)),

		// Map Subscreen
		new MenuColor(new Color(.313725f, .156863f, .117647f), new Color(.549020f, .156863f, .156863f)),

		// Quest Status Subscreen
		new MenuColor(new Color(.313725f, .313725f, .196078f), new Color(.470588f, .470588f, .327451f)),

		// Equipment Subscreen
		new MenuColor(new Color(.039216f, .196078f, .156863f), new Color(.352941f, .392157f, .235294f)),

		// Save Subscreen
		new MenuColor(Color.white, Color.white)
	};

	

	void Start()
	{
		// Set this object's parent
		GameObject go = GameObject.Find("SC_Camera/Rotation");
		if (go != null)
			transform.parent = go.transform;
		transform.localPosition = Vector3.zero;

		uiCam = go.transform.FindChild("UICam").gameObject.GetComponent<Camera>();

		string[] menuName =
		{
			"Select",
			"Map",
			"Quest",
			"Equip",
			"Save"
		};
		string[] panelName =
		{
			"LPanel",
			"CPanel",
			"RPanel"
		};
		Transform menu, panel;
		Color c1, c2;
		MeshFilter mf;
		MeshRenderer mr;
		string tp = "OoT/Actors/Interface/PauseMenu/Textures/{0}/IO_PauseMenu{0}{1}";
		for (int i = 0; i < 5; i++)
		{
			// Create root object for menu
			menu = new GameObject(menuName[i]).transform;
			menu.parent = transform;
			menu.localPosition = Vector3.zero;
			menu.localScale = Vector3.one;

			// Create background object for menu
			go = new GameObject("Background");
			go.transform.parent = menu;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.Euler(new Vector3(90, 180, 0));
			go.transform.localScale = Vector3.one;

			c1 = menuColor[i].color1;
			c2 = menuColor[i].color2;
			for (int j = 0; j < 3; j++)
			{
				// Create the object for 1 of the 3 panels of the menu
				panel = new GameObject(panelName[j]).transform;
				panel.gameObject.layer = LayerMask.NameToLayer("UI");
				panel.parent = go.transform;
				panel.localPosition = Vector3.zero;
				panel.localRotation = Quaternion.identity;
				panel.localScale = Vector3.one;

				// Create the mesh for 1 of the 3 panels of the menu
				mf = panel.gameObject.AddComponent<MeshFilter>();
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

				// Create the material for 1 of the 3 panels of the menu
				mr = panel.gameObject.AddComponent<MeshRenderer>();
				mr.material = new Material(Resources.Load<Shader>("_All_/Data/Shared/Shaders/Sprite"));
				mr.material.mainTexture = Resources.Load<Texture>(string.Format(tp, menuName[i], panelName[j][0]));
				if (i == 4)
					mr.enabled = false;
			}

			// Create the items object for the menu
			go = new GameObject("Items");
			go.transform.parent = menu;
			go.transform.localPosition = Vector3.zero;
		}
	}


	void Update()
	{
		if (uiCam)
		{
			// Adjust the scale to match the aspect ratio
			transform.localScale = new Vector3(uiCam.aspect, 1.0f, 1.0f);
		}
	}
}
