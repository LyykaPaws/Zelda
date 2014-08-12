#pragma strict

public class Item
{
   public var icon : String;
   function Item(id : int, icon : String) {
   	  if (id < 56) {
      	this.icon = "OoT/Actors/Interface/PauseMenu/Textures/SI_"+icon;
      } else if (id < 59) {
      	this.icon = "OoT/Actors/Interface/PauseMenu/Textures/SI_"+icon+"-E";
      } else {
      	this.icon = "OoT/Actors/Interface/PauseMenu/Textures/EI_"+icon;
      }
   }
}

var items : Item[];
var cursorPosition : int;
var cursorChangeSound : AudioSource;

var qsX : int[] = new Array(234, 234, 206, 178, 178, 206, 52, 70, 88, 106, 124, 142, 52, 70, 88, 106, 124, 142, 180, 206, 232, 50, 74, 50, 106).ToBuiltin(int) as int[];
var qsY : int[] = new Array(82, 114, 132, 114, 82, 64, 140, 140, 140, 140, 140, 140, 118, 118, 118, 118, 118, 118, 166, 166, 166, 62, 62, 86, 62).ToBuiltin(int) as int[];
	


function Start () {
	var i : int;

	var equipmentPanel : GameObject = GameObject.Find("UI_PauseMenu/Equip/Items");
	var itemPanel : GameObject = GameObject.Find("UI_PauseMenu/Select/Items");
	var questPanel : GameObject = GameObject.Find("UI_PauseMenu/Quest/Items");
	
	InitCursor();
	
	items = new Item[90];
	items[0] = new Item(0,"DekuStick");
	items[1] = new Item(1,"DekuNut");
	items[2] = new Item(2,"Bomb");
	items[3] = new Item(3,"BowFairy");
	items[4] = new Item(4,"ArrowFire1");
	items[5] = new Item(5,"MagicDinsFire");
	items[6] = new Item(6,"SlingshotFairy");
	items[7] = new Item(7,"OcarinaFairy");
	items[8] = new Item(8,"OcarinaOfTime");
	items[9] = new Item(9,"Bombchu");
	items[10] = new Item(10,"Hookshot1");
	items[11] = new Item(11,"Longshot");
	items[12] = new Item(12,"ArrowIce1");
	items[13] = new Item(13,"MagicFaroresWind");
	items[14] = new Item(14,"Boomerang");
	items[15] = new Item(15,"LensOfTruth");
	items[16] = new Item(16,"MagicBeans");
	items[17] = new Item(17,"HammerMegaton");
	items[18] = new Item(18,"ArrowLight1");
	items[19] = new Item(19,"MagicNayrusLove");
	items[20] = new Item(20,"Bottle");
	items[21] = new Item(21,"BottlePotionRed");
	items[22] = new Item(22,"BottlePotionGreen");
	items[23] = new Item(23,"BottlePotionBlue");
	items[24] = new Item(24,"BottleFairy");
	items[25] = new Item(25,"BottleFish");
	items[26] = new Item(26,"BottleMilkFull");
	items[27] = new Item(27,"BottleLetter");
	items[28] = new Item(28,"BottleBlueFire");
	items[29] = new Item(29,"BottleBug");
	items[30] = new Item(30,"BottlePoeBig");
	items[31] = new Item(31,"BottleMilkHalf");
	items[32] = new Item(32,"BottlePoe");
	items[33] = new Item(33,"EggWeird");
	items[34] = new Item(34,"Cucco");
	items[35] = new Item(35,"ZeldasLetter");
	items[36] = new Item(36,"MaskKeaton");
	items[37] = new Item(37,"MaskSkull");
	items[38] = new Item(38,"MaskSpooky");
	items[39] = new Item(39,"MaskBunny");
	items[40] = new Item(40,"MaskGoron");
	items[41] = new Item(41,"MaskZora");
	items[42] = new Item(42,"MaskGerudo");
	items[43] = new Item(43,"MaskOfTruth");
	items[44] = new Item(44,"SoldOut");
	items[45] = new Item(45,"EggPocket");
	items[46] = new Item(46,"CuccoPocket");
	items[47] = new Item(47,"Cojiro");
	items[48] = new Item(48,"OddMushroom");
	items[49] = new Item(49,"OddPotion");
	items[50] = new Item(50,"PoachersSaw");
	items[51] = new Item(51,"BrokenGoronsSword");
	items[52] = new Item(52,"Prescription");
	items[53] = new Item(53,"EyeballFrag");
	items[54] = new Item(54,"Eyedrops");
	items[55] = new Item(55,"ClaimCheck");
	items[56] = new Item(56,"ArrowFire1");
	items[57] = new Item(57,"ArrowIce1");
	items[58] = new Item(58,"ArrowLight1");
	items[59] = new Item(59,"SwordKokiri1");
	items[60] = new Item(60,"SwordMaster");
	items[61] = new Item(61,"SwordBiggorons");
	items[62] = new Item(62,"ShieldDeku");
	items[63] = new Item(63,"ShieldHylian");
	items[64] = new Item(64,"ShieldMirror2");
	items[65] = new Item(65,"TunicKokiri");
	items[66] = new Item(66,"TunicGoron");
	items[67] = new Item(67,"TunicZora");
	items[68] = new Item(68,"BootsKokiri");
	items[69] = new Item(69,"BootsIron");
	items[70] = new Item(70,"BootsHover");
	items[71] = new Item(71,"BulletBagSmall");
	items[72] = new Item(72,"BulletBagMedium");
	items[73] = new Item(73,"BulletBagLarge");
	items[74] = new Item(74,"QuiverSmall");
	items[75] = new Item(75,"QuiverMedium");
	items[76] = new Item(76,"QuiverLarge");
	items[77] = new Item(77,"BombBagSmall");
	items[78] = new Item(78,"BombBagMedium");
	items[79] = new Item(79,"BombBagLarge");
	items[80] = new Item(80,"BraceletGorons");
	items[81] = new Item(81,"GauntletsSilver");
	items[82] = new Item(82,"GauntletsGolden");
	items[83] = new Item(83,"ScaleSilver");
	items[84] = new Item(84,"ScaleGolden");
	items[85] = new Item(85,"BrokenGiantsKnife");
	items[86] = new Item(86,"WalletAdults");
	items[87] = new Item(87,"WalletGiants");
	items[88] = new Item(88,"DekuSeeds");
	items[89] = new Item(89,"FishingPole");

	var equipment : int[] = new Array(72,59,60,61,77,62,63,64,80,65,66,67,84,68,69,70).ToBuiltin(int) as int[];
	var inventory : int[] = new Array(0,1,2,3,4,5,6,8,9,11,12,13,14,15,16,17,18,19,20,21,22,23,55,44).ToBuiltin(int) as int[];

	var item : GameObject;
	var xOffset : float;
	var texture : Texture2D;
	var sprite : Sprite;
	var renderer : SpriteRenderer;
	for(i = 0; i < equipment.length; i++) {
		if (equipment[i] == 0xFF) {
			continue;
		}
		if (i%4 == 0) {
			xOffset = 6;
		} else {
			xOffset = 134+(33*((i%4)-1));
		}
		item = new GameObject();
		item.name = "Slot_"+(i%4)+"_"+Mathf.Floor(i/4);
		item.transform.parent = equipmentPanel.transform;
		item.transform.localPosition = Vector3(xOffset,-24-(32*(Mathf.Floor(i/4))),0);
		item.transform.localRotation = Quaternion.identity;
		item.transform.localScale = Vector3(28,28,1);
		item.layer = LayerMask.NameToLayer("TransparentFX");

		texture = Resources.Load(items[equipment[i]].icon, Texture2D);
		sprite = Sprite.Create(texture, new Rect(0, 0, 32, 32),new Vector2(0, 1),32);
		renderer = item.AddComponent(SpriteRenderer);
		renderer.sprite = sprite;
	}

	for(i = 0; i < inventory.length; i++) {
		if (inventory[i] == 0xFF) {
			continue;
		}
		item = new GameObject();
		item.name = "Slot_"+(i%6)+"_"+Mathf.Floor(i/6);
		item.transform.parent = itemPanel.transform;
		item.transform.localPosition = Vector3(-28-(32*(i%6)),-24-(32*(Mathf.Floor(i/6))),0);
		item.transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
		item.transform.localScale = Vector3(28,28,1);
		item.layer = LayerMask.NameToLayer("TransparentFX");
			
		texture = Resources.Load(items[inventory[i]].icon, Texture2D);
		sprite = Sprite.Create(texture, new Rect(0, 0, 32, 32),new Vector2(0, 1),32);
		renderer = item.AddComponent(SpriteRenderer);
			renderer.sprite = sprite;
	}
	
	var qsName : String[] = new Array("MedallionForest", "MedallionFire", "MedallionWater", "MedallionSpirit", "MedallionShadow", "MedallionLight", "", "", "", "", "", "", "", "", "", "", "", "", "SSKokirisEmerald", "SSGoronsRuby", "SSZorasSapphire", "StoneOfAgony", "GerudoCard", "GoldSkulltula", "").ToBuiltin(String) as String[];
	var itemSize : int;
	for(i = 0; i < 25; i++) {
		if (i < 24) {
			itemSize = 24;
		} else if (i == 24) {
			itemSize = 48;
		} else {
			itemSize = 16;
		}
		item = new GameObject();
		item.name = "Slot_"+i;
		item.transform.parent = questPanel.transform;
		item.transform.localPosition = Vector3(qsX[i]-40,-1*(qsY[i]-40),0);
		item.transform.localRotation = Quaternion.identity;
		item.transform.localScale = Vector3(itemSize,itemSize,1);
		item.layer = LayerMask.NameToLayer("TransparentFX");
			
		texture = Resources.Load("OoT/Actors/Interface/PauseMenu/Textures/QI_"+qsName[i], Texture2D);
		sprite = Sprite.Create(texture, new Rect(0, 0, itemSize, itemSize),new Vector2(0, 1),itemSize);
		renderer = item.AddComponent(SpriteRenderer);
		renderer.sprite = sprite;
	}
}

function SetCursorColor(desiredColor : Color32)
{
	var spriteRenderer1 : SpriteRenderer = GameObject.Find("Item Menu Cursor/Cursor 1").GetComponent(SpriteRenderer);
	var spriteRenderer2 : SpriteRenderer = GameObject.Find("Item Menu Cursor/Cursor 2").GetComponent(SpriteRenderer);
	var spriteRenderer3 : SpriteRenderer = GameObject.Find("Item Menu Cursor/Cursor 3").GetComponent(SpriteRenderer);
	var spriteRenderer4 : SpriteRenderer = GameObject.Find("Item Menu Cursor/Cursor 4").GetComponent(SpriteRenderer);
	spriteRenderer1.color = desiredColor;
   	spriteRenderer2.color = desiredColor;
   	spriteRenderer3.color = desiredColor;
   	spriteRenderer4.color = desiredColor;
}

function InitCursor()
{
	var itemPanel : GameObject = GameObject.Find("UI_PauseMenu/Select/Items");
	var cursorObjectParent : GameObject = new GameObject();
	cursorObjectParent.transform.parent = itemPanel.transform;
	cursorObjectParent.name = "Item Menu Cursor";
	cursorObjectParent.transform.localScale = Vector3(112, 122, 1);
    cursorObjectParent.transform.localEulerAngles = Vector3(180, 180, 0);
    
	var cursorObject1 : GameObject = new GameObject();
	cursorObject1.transform.parent = cursorObjectParent.transform;
	cursorObject1.transform.localEulerAngles = Vector3(0, 0, 0);
	cursorObject1.transform.localScale = Vector3(1, 1, 1);
    var spriteRenderer1 : SpriteRenderer = cursorObject1.AddComponent(SpriteRenderer);
    spriteRenderer1.sprite = Resources.Load("OoT/Actors/Interface/PauseMenu/Textures/ItemCursor", Sprite);
    cursorObject1.name = "Cursor 1";
    cursorObject1.layer = 1;
    
    var cursorObject2 : GameObject = new GameObject();
    cursorObject2.name = "Cursor 2";
    cursorObject2.transform.parent = cursorObjectParent.transform;
    cursorObject2.transform.localEulerAngles = Vector3(0, 0, 0);
    var spriteRenderer2 : SpriteRenderer = cursorObject2.AddComponent(SpriteRenderer);
    spriteRenderer2.sprite = Resources.Load("OoT/Actors/Interface/PauseMenu/Textures/ItemCursor", Sprite);
    cursorObject2.transform.localScale = Vector3(1, 1, 1);
    cursorObject2.transform.localEulerAngles = Vector3(180, 0, 0);
    cursorObject2.transform.localPosition = Vector3(0, 0.1, 0);
    cursorObject2.layer = 1;
    
    var cursorObject3 : GameObject = new GameObject();
    cursorObject3.name = "Cursor 3";
    cursorObject3.transform.parent = cursorObjectParent.transform;
    var spriteRenderer3 : SpriteRenderer = cursorObject3.AddComponent(SpriteRenderer);
    spriteRenderer3.sprite = Resources.Load("OoT/Actors/Interface/PauseMenu/Textures/ItemCursor", Sprite);
    cursorObject3.transform.localScale = Vector3(1, 1, 1);
    cursorObject3.transform.localEulerAngles = Vector3(0, 180, 0);
    cursorObject3.transform.localPosition = Vector3(-0.1, 0.0, 0);
    cursorObject3.layer = 1;
    
    var cursorObject4 : GameObject = new GameObject();
    cursorObject4.name = "Cursor 4";
    cursorObject4.transform.parent = cursorObjectParent.transform;
    var spriteRenderer4 : SpriteRenderer = cursorObject4.AddComponent(SpriteRenderer);
    spriteRenderer4.sprite = Resources.Load("OoT/Actors/Interface/PauseMenu/Textures/ItemCursor", Sprite);
    cursorObject4.transform.localScale = Vector3(1, 1, 1);
    cursorObject4.transform.localEulerAngles = Vector3(0, 0, 180);
    cursorObject4.transform.localPosition = Vector3(-0.1, 0.1, 0);
    cursorObject4.layer = 1;
    
    // Load the cursor change sound.
    cursorChangeSound = cursorObjectParent.AddComponent(AudioSource);
    cursorChangeSound.clip = Resources.Load("OoT/Audio/SFX/Interface/PauseMenu_Cursor", AudioClip);
}

function UpdateCursor()
{
	// TODO: Clean up this code. It's really messy.
	var pauseMenu : GameObject = GameObject.Find("UI_PauseMenu");
	var cursorObjectParent : GameObject = GameObject.Find("Item Menu Cursor");
	if (pauseMenu.transform.localEulerAngles.y == 270) // Item menu
    {
    	var itemPanel : GameObject = GameObject.Find("UI_PauseMenu/Select/Items");
    	cursorObjectParent.transform.parent = itemPanel.transform;
    	cursorObjectParent.transform.localPosition = Vector3(-48-(32*(cursorPosition%6)),-34-(32*(Mathf.Floor(cursorPosition/6))),0.1);
	    cursorObjectParent.transform.localScale = Vector3(112, 122, 1);
   	    cursorObjectParent.transform.localEulerAngles = Vector3(180, 180, 0);
   	    
   	    SetCursorColor(Color32(254, 254, 0, 255));
   	    if (Input.GetKeyDown(KeyCode.LeftArrow))
	    {
	    	cursorPosition--;
		    cursorChangeSound.Play();
		    if (cursorPosition < 0)
		    {
		    	cursorPosition = 0;
		    }
	    }
	    if (Input.GetKeyDown(KeyCode.RightArrow))
	    {
		    cursorPosition++;
		    cursorChangeSound.Play();
		    if (cursorPosition > 23)
		    	cursorPosition = 0;
	    }
	    if (Input.GetKeyDown(KeyCode.UpArrow))
	    {
		    
		    cursorPosition -= 6;
		    cursorChangeSound.Play();
		    if (cursorPosition < 0)
			    cursorPosition = 23;
	    }
	    if (Input.GetKeyDown(KeyCode.DownArrow))
	    {
		    cursorPosition += 6;
		    cursorChangeSound.Play();
		    if (cursorPosition > 23)
		    	cursorPosition = 0;
	    }
    }
    if (pauseMenu.transform.localEulerAngles.y == 180) // Map menu
    {
    	
    }
    if (pauseMenu.transform.localEulerAngles.y == 90.00001) // Quest menu
    {
    	var questPanel : GameObject = GameObject.Find("UI_PauseMenu/Quest/Items");
		var itemSize : int;
		if (cursorPosition < 24) {
			itemSize = 24;
		} else if (cursorPosition == 24) {
			itemSize = 48;
		} else {
			itemSize = 16;
		}
		
    	cursorObjectParent.transform.parent = questPanel.transform;
		cursorObjectParent.transform.localPosition = Vector3(qsX[cursorPosition]-35,-1*(qsY[cursorPosition]-25),-0.1);
		cursorObjectParent.transform.localEulerAngles = Vector3(0,180,0);
		cursorObjectParent.transform.localScale = Vector3(itemSize * 4,itemSize * 4,1);
		SetCursorColor(Color32(255, 255, 255, 255));
		
		if (cursorPosition > 24)
		    		cursorPosition = 0;
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
	    {
	    	cursorPosition--;
		    cursorChangeSound.Play();
		    if (cursorPosition < 0)
		    {
		    	cursorPosition = 0;
		    }
	    	
	    }
	    if (Input.GetKeyDown(KeyCode.RightArrow))
	    {
		    cursorPosition++;
		    cursorChangeSound.Play();
		    if (cursorPosition > 24)
		    	cursorPosition = 0;
	    }
    }
    if (pauseMenu.transform.localEulerAngles.y == 0) // Equip menu
    {
    	var equipmentPanel : GameObject = GameObject.Find("UI_PauseMenu/Equip/Items");
    	var xOffset = 2;
    	SetCursorColor(Color32(0, 254, 50, 255));
  
    	if (cursorPosition%4 == 0) {
			xOffset = 26;
			SetCursorColor(Color32(255, 255, 255, 255));
		} else {
			xOffset = 154+(33*((cursorPosition%4)-1));
		}
    	cursorObjectParent.transform.parent = equipmentPanel.transform;
    	cursorObjectParent.transform.localPosition = Vector3(xOffset,-44-(32*(Mathf.Floor(cursorPosition/4))),0);
    	cursorObjectParent.transform.localEulerAngles = Vector3(0, 0, 0);
    	cursorObjectParent.transform.localScale = Vector3(112, 112, 1);
    	
    	if (cursorPosition > 15)
    		cursorPosition = 1;
    	
    	if (Input.GetKeyDown(KeyCode.LeftArrow))
	    {
	    	cursorPosition--;
		    cursorChangeSound.Play();
		    if (cursorPosition < 0)
		    {
		    	cursorPosition = 0;
		    }
	    }
	    if (Input.GetKeyDown(KeyCode.RightArrow))
	    {
	    	cursorPosition++;
	    	cursorChangeSound.Play();
	    	if (cursorPosition > 15)
	    		cursorPosition = 0;
	    }
	    if (Input.GetKeyDown(KeyCode.UpArrow))
	    {
		    cursorPosition -= 4;
		    cursorChangeSound.Play();
		    if (cursorPosition < 0)
			    cursorPosition = 15;
	    }
	    if (Input.GetKeyDown(KeyCode.DownArrow))
	    {
		    cursorPosition += 4;
		    cursorChangeSound.Play();
		    if (cursorPosition > 15)
		    	cursorPosition = 0;
	    }
    //Debug.Log(cursorPosition);
	}
}

function Update () {
	
		UpdateCursor();
}