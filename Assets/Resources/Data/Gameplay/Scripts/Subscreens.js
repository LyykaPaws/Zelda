#pragma strict

public class Item
{
   public var icon : String;
   function Item(id : int, icon : String) {
   	  if (id < 56) {
      	this.icon = "Actors/Items/Select/"+icon+"/Textures/SI_"+icon;
      } else if (id < 59) {
      	this.icon = "Actors/Items/Select/"+icon+"/Textures/SI_"+icon+"-E";
      } else {
      	this.icon = "Actors/Items/Equip/"+icon+"/Textures/EI_"+icon;
      }
   }
}

function Start () {
	var i : int;

	var equipmentPanel : GameObject = GameObject.Find("PauseMenu/Equip/Items");
	var itemPanel : GameObject = GameObject.Find("PauseMenu/Select/Items");
	var questPanel : GameObject = GameObject.Find("PauseMenu/Quest/Items");
	
	var items : Item[];
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
		item.transform.Rotate(Vector3(0,90,0));
		item.transform.localScale = Vector3(28,28,1);
		item.layer = LayerMask.NameToLayer("TransparentFX");
			
		texture = Resources.Load(items[inventory[i]].icon, Texture2D);
		sprite = Sprite.Create(texture, new Rect(0, 0, 32, 32),new Vector2(0, 1),32);
		renderer = item.AddComponent(SpriteRenderer);
			renderer.sprite = sprite;
	}
	
	var qsX : int[] = new Array(234, 234, 206, 178, 178, 206, 52, 70, 88, 106, 124, 142, 52, 70, 88, 106, 124, 142, 180, 206, 232, 50, 74, 50, 106).ToBuiltin(int) as int[];
	var qsY : int[] = new Array(82, 114, 132, 114, 82, 64, 140, 140, 140, 140, 140, 140, 118, 118, 118, 118, 118, 118, 166, 166, 166, 62, 62, 86, 62).ToBuiltin(int) as int[];
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
		item.transform.Rotate(Vector3(0,-90,0));
		item.transform.localScale = Vector3(itemSize,itemSize,1);
		item.layer = LayerMask.NameToLayer("TransparentFX");
			
		texture = Resources.Load("Actors/Items/Quest/"+qsName[i]+"/Textures/QI_"+qsName[i], Texture2D);
		sprite = Sprite.Create(texture, new Rect(0, 0, itemSize, itemSize),new Vector2(0, 1),itemSize);
		renderer = item.AddComponent(SpriteRenderer);
		renderer.sprite = sprite;
	}
	
}

function Update () {

}