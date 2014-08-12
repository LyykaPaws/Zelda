using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;



public class GameEngine : MonoBehaviour
{
	public static GameEngine context;

	public string[] sceneTable;

	private AudioSource audioSource;



#if UNITY_EDITOR
	[UnityEditor.MenuItem("CONTEXT/GameEngine/Update Scene Table")]
	private static void UpdateSceneTable(UnityEditor.MenuCommand command)
	{
		GameEngine context = (GameEngine)command.context;
		List<string> scenes = new List<string>();
		string path;
		foreach (UnityEditor.EditorBuildSettingsScene s in UnityEditor.EditorBuildSettings.scenes)
		{
			if (s.enabled)
			{
				int sep = s.path.IndexOf("Assets/Resources/");
				if (sep == 0)
					path = s.path.Substring(17, s.path.IndexOf('/', 17) - 17) + ":";
				else
					path = "";
				sep = s.path.LastIndexOf('/');
				scenes.Add(path + s.path.Substring(sep + 1, s.path.LastIndexOf('.') - sep - 1));
			}
		}
		context.sceneTable = scenes.ToArray();
	}
#endif // UNITY_EDITOR



	void Start()
	{
		if (GameObject.FindGameObjectsWithTag("Global Context").Length > 1)
			GameObject.Destroy(this);
		context = this;
		audioSource = gameObject.GetComponent<AudioSource>();
		GameObject.DontDestroyOnLoad(gameObject);
//		GameEngine.LoadScene("OoT:HyruleField");
		Debug.Log(GameEngine.GetScenePath("OoT:HyruleField"));
		GameEngine.SetBGM("OoT:HyruleCastleCourtyard");
		GameEngine.SpawnActor("OoT:CH_AdultLink");
	}
	
	void Update()
	{
	}


	public static string GetScenePath(string name)
	{
		int sep = name.IndexOf(':');
		if (sep < 0)
		{
			name = "_All_:" + name;
			sep = 5;
		}
		return string.Format("{0}/Scenes/{1}", name.Substring(0, sep), name.Substring(sep + 1));
	}

	public static int GetScene(string name)
	{
		if (name.IndexOf(':') < 0)
			name = "_All_:" + name;
		for (int i = 0; i < context.sceneTable.Length; i++)
		{
			if (context.sceneTable[i] == name)
				return i;
		}
		return -1;
	}

	public static string GetActorPath(string name)
	{
		int sep = name.IndexOf(':');
		if (sep < 0)
		{
			name = "_All_:" + name;
			sep = 5;
		}
		string[] actorGroups =
		{
			"CH", "Characters",
			"CO", "Common",
			"CR", "Creatures",
			"EN", "Enemies",
			"UI", "Interface",
			"SC", "Scenery",
			"UN", "Uncommon"
		};
		string groupPrefix = name.Substring(sep + 1, 2);
		string group = null;
		string actorName = name.Substring(sep + 4);
		for (int i = 0; i < actorGroups.Length; i += 2)
		{
			if (groupPrefix == actorGroups[i])
				group = actorGroups[i + 1];
		}
		if (group != null)
			return string.Format("{0}/Actors/{2}/{3}", name.Substring(0, sep), groupPrefix, group, actorName);
		else
			return null;
	}

	public static GameObject GetActor(string name)
	{
		string path = GameEngine.GetActorPath(name);
		if (path != null)
			return Resources.Load<GameObject>(path + "/" + name.Substring(name.IndexOf(':') + 1));
		else
			return null;
	}

	public static AudioClip GetMusic(string name)
	{
		return GameEngine.GetMusic(name, true);
	}

	public static AudioClip GetMusic(string name, bool localized)
	{
		int sep = name.IndexOf(':');
		if (sep < 0)
		{
			name = "_All_:" + name;
			sep = 5;
		}
		name = string.Format("{0}/Audio/BGM/{1}", name.Substring(0, sep), name.Substring(sep + 1));
		AudioClip clip = null;
		if (localized)
		{
			CultureInfo ci = CultureInfo.CurrentCulture;
			do
			{
				clip = Resources.Load<AudioClip>(name + "-" + ci.Name);
				if (clip != null)
					break;
				else
					ci = ci.Parent;
			}
			while (ci.Parent != null && ci.Name != "");
		}
		if (clip == null)
			clip = Resources.Load<AudioClip>(name);
		return clip;
	}

	public static AudioClip GetSound(string name)
	{
		return GameEngine.GetSound(name, true);
	}

	public static AudioClip GetSound(string name, bool localized)
	{
		int sep = name.IndexOf(':');
		if (sep < 0)
		{
			name = "_All_:" + name;
			sep = 5;
		}
		name = string.Format("{0}/Audio/SFX/{1}", name.Substring(0, sep), name.Substring(sep + 1));
		AudioClip clip = null;
		if (localized)
		{
			CultureInfo ci = CultureInfo.CurrentCulture;
			do
			{
				clip = Resources.Load<AudioClip>(name + "-" + ci.Name);
				if (clip != null)
					break;
				else
					ci = ci.Parent;
			}
			while (ci.Parent != null && ci.Name != "");
		}
		if (clip == null)
			clip = Resources.Load<AudioClip>(name);
		return clip;
	}
	
	public static AnimationClip GetAnimation(string root, string name)
	{
 		return GameEngine.GetAnimation(root, name, true);
	}

	public static AnimationClip GetAnimation(string root, string name, bool localized)
	{
		root += "/Animations/" + name;
		AnimationClip clip = null;
		if (localized)
		{
			CultureInfo ci = CultureInfo.CurrentCulture;
			do
			{
				clip = Resources.Load<AnimationClip>(root + "-" + ci.Name);
				if (clip != null)
					break;
				else
					ci = ci.Parent;
			}
			while (ci.Parent != null && ci.Name != "");
		}
		if (clip == null)
			clip = Resources.Load<AnimationClip>(root);
		return clip;
	}

	public static Material GetMaterial(string root, string name)
	{
		return GameEngine.GetMaterial(root, name, true);
	}

	public static Material GetMaterial(string root, string name, bool localized)
	{
		root += "/Materials/" + name;
		Material material = null;
		if (localized)
		{
			CultureInfo ci = CultureInfo.CurrentCulture;
			do
			{
				material = Resources.Load<Material>(root + "-" + ci.Name);
				if (material != null)
					break;
				else
					ci = ci.Parent;
			}
			while (ci.Parent != null && ci.Name != "");
		}
		if (material == null)
			material = Resources.Load<Material>(root);
		return material;
	}

	public static Texture GetTexture(string root, string name)
	{
		return GameEngine.GetTexture(root, name, true);
	}

	public static Texture GetTexture(string root, string name, bool localized)
	{
		root += "/Textures/" + name;
		Texture texture = null;
		if (localized)
		{
			CultureInfo ci = CultureInfo.CurrentCulture;
			do
			{
				texture = Resources.Load<Texture>(root + "-" + ci.Name);
				if (texture != null)
					break;
				else
					ci = ci.Parent;
			}
			while (ci.Parent != null && ci.Name != "");
		}
		if (texture == null)
			texture = Resources.Load<Texture>(root);
		return texture;
	}
	
	public static Sprite GetSprite(string root, string name)
	{
		return GameEngine.GetSprite(root, name, true);
	}
	
	public static Sprite GetSprite(string root, string name, bool localized)
	{
		root += "/Textures/" + name;
		Sprite sprite = null;
		if (localized)
		{
			CultureInfo ci = CultureInfo.CurrentCulture;
			do
			{
				sprite = Resources.Load<Sprite>(root + "-" + ci.Name);
				if (sprite != null)
					break;
				else
					ci = ci.Parent;
			}
			while (ci.Parent != null && ci.Name != "");
		}
		if (sprite == null)
			sprite = Resources.Load<Sprite>(root);
		return sprite;
	}

	public static string GetString(string root, string name)
	{
		return GameEngine.GetString(root, name, true);
	}

	public static string GetString(string root, string name, bool localized)
	{
		root += "/Strings/" + name;
		TextAsset textAsset = null;
		if (localized)
		{
			CultureInfo ci = CultureInfo.CurrentCulture;
			do
			{
				textAsset = Resources.Load<TextAsset>(root + "-" + ci.Name);
				if (textAsset != null)
					break;
				else
					ci = ci.Parent;
			}
			while (ci.Parent != null && ci.Name != "");
		}
		if (textAsset == null)
			textAsset = Resources.Load<TextAsset>(root);
		if (textAsset != null)
			return textAsset.text;
		else
			return null;
	}


	public static void LoadScene(string name)
	{
		int id = GameEngine.GetScene(name);
		if (id >= 0)
			GameEngine.LoadScene(id);
	}

	public static void LoadScene(int index)
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public static GameObject SpawnActor(string name)
	{
		GameObject go = GameEngine.GetActor(name);
		if (go != null)
		{
			go = (GameObject)GameObject.Instantiate(go);
			if (go != null)
			{
				go.name = name.Substring(name.IndexOf('_') + 1);
				return go;
			}
		}
		return null;
	}
	
	public static void SetBGM(string name)
	{
		if (name != null)
			SetBGM(GameEngine.GetMusic(name));
		else
			GameEngine.SetBGM((AudioClip)null);
	}

	public static void SetBGM(AudioClip clip)
	{
		if (context.audioSource != null)
		{
			if (clip != null)
			{
				context.audioSource.clip = clip;
				if (!context.audio.isPlaying)
					context.audioSource.Play();
			}
			else
				context.audioSource.Stop();
		}
	}
}
