using UnityEngine;
using System.Collections;
public delegate void AbilityDelegate (GameObject me) ;
public delegate void CollisionDelegate (GameObject me, GameObject other) ;
public delegate void GimmickDelegate (GameObject me, int mode) ;
public class globalVariables : MonoBehaviour {
	
	public float rowHeight = 0.6f ;
	[Range(0.0001f, 1.0f)] 
	public float baseCooldown ;
	public int raceAmount ;
	public int[] raceUnits ;
	public float previewScale ;
	public float[] baseUnitCooldowns ;
	public string[] unitNames ;
	public string[] unitDescriptions ;
	public GameObject[] raceArray ;
	int unitAmount ;
	//AbilityDelegate defaultPassiveAbility (GameObject me) ;
	
	
	
	void Awake () {
		Application.targetFrameRate = 60;
		raceAmount = 2;
		raceArray = new GameObject[raceAmount];
		
		for (int i = 0; i < raceAmount; i++) {
			raceArray[i] = new GameObject() ;
			raceArray[i].AddComponent<Race>();
			raceArray[i].transform.parent = transform;
		}
		//baseCooldown = 0.174f ;
		//raceUnits = new int[raceAmount] ;
		//raceArray = new GameObject[raceAmount];
		
		AbilityDelegate defaultPassiveAbility = delegate(GameObject me) {
			me.GetComponent<Rigidbody2D>().MovePosition(new Vector2 
				(me.GetComponent<Rigidbody2D>().position.x + (me.GetComponent<UnitBrain>().hspeed* Time.fixedDeltaTime * me.GetComponent<UnitBrain>().moveDirection * Mathf.Cos(me.transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), 
			 	 me.GetComponent<Rigidbody2D>().position.y + (me.GetComponent<UnitBrain>().hspeed* Time.fixedDeltaTime * me.GetComponent<UnitBrain>().moveDirection * Mathf.Sin(me.transform.rotation.eulerAngles.z * Mathf.Deg2Rad)))) ;
			
			me.GetComponent<UnitBrain> ().atkCooldown -= 1000 * Time.deltaTime;
			if (me.GetComponent<UnitBrain> ().enemy != null) {
				me.GetComponent<UnitBrain> ().hspeed = 0f;
				if (me.GetComponent<UnitBrain> ().atkCooldown <= 0) {
					me.GetComponent<UnitBrain> ().atkCooldown = me.GetComponent<UnitBrain> ().atkDelay;
					me.GetComponent<UnitBrain> ().enemy.GetComponent<UnitBrain> ().currentHealth -= me.GetComponent<UnitBrain> ().damage - me.GetComponent<UnitBrain> ().enemy.GetComponent<UnitBrain> ().armor;
					if (me.GetComponent<UnitBrain> ().enemy.GetComponent<UnitBrain> ().currentHealth <= 0) {
						me.GetComponent<UnitBrain> ().enemy = null;
					}
				}
				
			} else {
				me.GetComponent<UnitBrain> ().hspeed = me.GetComponent<UnitBrain> ().speed;
			}
			if (Mathf.Abs (me.GetComponent<UnitBrain> ().transform.position.x) > 5.41) {
				Destroy (me);
			}
			if (Mathf.Abs (me.GetComponent<UnitBrain> ().transform.position.y) > 5.41) {
				Destroy (me);
			}
		};
		
		unitAmount = 9;
		raceArray [0].GetComponent<Race>().unitAmount = unitAmount ;
		
		raceArray[0].GetComponent<Race>().unitName = new string[unitAmount] ;
		raceArray[0].GetComponent<Race>().unitSpeed = new float[unitAmount] ;
		raceArray[0].GetComponent<Race>().unitAtkDelay = new float[unitAmount] ;
		raceArray[0].GetComponent<Race>().unitAtkDamage = new float[unitAmount] ;
		raceArray [0].GetComponent<Race> ().unitRange = new float[unitAmount];
		raceArray[0].GetComponent<Race>().unitMaxHealth = new float[unitAmount] ;
		raceArray[0].GetComponent<Race>().unitArmor = new float[unitAmount] ;
		raceArray[0].GetComponent<Race>().unitCooldown = new float[unitAmount] ;
		raceArray[0].GetComponent<Race>().unitSprite = new Sprite[unitAmount] ;
		raceArray[0].GetComponent<Race>().passiveAbility = new AbilityDelegate[unitAmount] ;
		raceArray[0].GetComponent<Race>().createAbility = new AbilityDelegate[unitAmount] ;
		raceArray[0].GetComponent<Race>().collisionAbility = new CollisionDelegate[unitAmount] ;
		raceArray[0].GetComponent<Race>().deathAbility = new AbilityDelegate[unitAmount] ;
		
		
		
		raceArray [0].GetComponent<Race>().raceName = "Chronowarpers";
		raceArray [0].transform.name = "Chronowarpers";
		
		raceArray [0].GetComponent<Race>().raceGimmick = delegate(GameObject me,int mode) {
			switch (mode) {
			case 0:
				me.GetComponent<UnitBrain>().abilityNumbers[0] = 0 ;
				break ;
			case 1:
				if (me.GetComponent<UnitBrain>().currentHealth<me.GetComponent<UnitBrain>().maxHealth/5&&me.GetComponent<UnitBrain>().abilityNumbers[0]==0) {
					if (Random.value<0.3) {
						if (me.GetComponent<UnitBrain>().enemy!=null) {
							if (me.GetComponent<UnitBrain>().axis%2==0) {
								me.transform.position = new Vector3 (me.GetComponent<UnitBrain>().enemy.transform.position.x+(0.3f*me.GetComponent<UnitBrain>().moveDirection),me.transform.position.y,me.transform.position.z) ;
							} else {
								me.transform.position = new Vector3 (me.transform.position.x,me.GetComponent<UnitBrain>().enemy.transform.position.y+(0.3f*me.GetComponent<UnitBrain>().moveDirection),me.transform.position.z) ;
							}
						}
					}
					me.GetComponent<UnitBrain>().abilityNumbers[0] =1 ;
				}
				break ;
			default :
				break;
			}
		}; 
		
		
		
		raceArray[0].GetComponent<Race>().unitName[0] = "Moment" ;
		raceArray [0].GetComponent<Race>().unitSpeed [0] = 0.8f; 	
		raceArray [0].GetComponent<Race>().unitAtkDelay [0] = 1000f;	
		raceArray [0].GetComponent<Race>().unitAtkDamage [0] = 20f;		
		raceArray [0].GetComponent<Race>().unitMaxHealth [0] = 140f;		
		raceArray [0].GetComponent<Race>().unitArmor [0] = 0f;	
		raceArray [0].GetComponent<Race>().unitSprite [0] = Resources.Load<Sprite> ("unit1");	
		raceArray [0].GetComponent<Race>().unitCooldown [0] = 2.2f;	
		raceArray [0].GetComponent<Race>().unitRange [0] = 0.87f;	
		
		raceArray[0].GetComponent<Race>().unitName[1] = "Jiffy" ;		 
		raceArray [0].GetComponent<Race>().unitSpeed [1] = 1.2f; 	
		raceArray [0].GetComponent<Race>().unitAtkDelay [1] = 500f;		
		raceArray [0].GetComponent<Race>().unitAtkDamage [1] = 10f;		
		raceArray [0].GetComponent<Race>().unitMaxHealth [1] = 150f;		
		raceArray [0].GetComponent<Race>().unitArmor [1] = 0f;	
		raceArray [0].GetComponent<Race>().unitSprite [1] = Resources.Load<Sprite> ("unit2");	
		raceArray [0].GetComponent<Race>().unitCooldown [1] = 3.0f; 
		raceArray [0].GetComponent<Race>().unitRange [1] = 1.0f;	
		
		raceArray[0].GetComponent<Race>().unitName[2] = "Century" ;		 
		raceArray [0].GetComponent<Race>().unitSpeed [2] = 0.3f; 	
		raceArray [0].GetComponent<Race>().unitAtkDelay [2] = 100f;		
		raceArray [0].GetComponent<Race>().unitAtkDamage [2] = 5.5f;	
		raceArray [0].GetComponent<Race>().unitMaxHealth [2] = 350f;		
		raceArray [0].GetComponent<Race>().unitArmor [2] = 3f;	
		raceArray [0].GetComponent<Race>().unitSprite [2] = Resources.Load<Sprite> ("unit3");	
		raceArray [0].GetComponent<Race>().unitCooldown [2] = 4.0f; 
		raceArray [0].GetComponent<Race>().unitRange [2] = 0.5f;	
		
		raceArray[0].GetComponent<Race>().unitName[3] = "Eon" ;		 	
		raceArray [0].GetComponent<Race>().unitSpeed [3] = 0.35f;	
		raceArray [0].GetComponent<Race>().unitAtkDelay [3] = 4200f;	
		raceArray [0].GetComponent<Race>().unitAtkDamage [3] = 100f;	
		raceArray [0].GetComponent<Race>().unitMaxHealth [3] = 100f;		
		raceArray [0].GetComponent<Race>().unitArmor [3] = 2f;	
		raceArray [0].GetComponent<Race>().unitSprite [3] = Resources.Load<Sprite> ("unit4");	
		raceArray [0].GetComponent<Race>().unitCooldown [3] = 5.0f; 
		raceArray [0].GetComponent<Race>().unitRange [3] = 3.6f;	
		
		raceArray[0].GetComponent<Race>().unitName[4] = "Leap Year" ;	 
		raceArray [0].GetComponent<Race>().unitSpeed [4] = 0.25f;	
		raceArray [0].GetComponent<Race>().unitAtkDelay [4] = 400f;		
		raceArray [0].GetComponent<Race>().unitAtkDamage [4] = 20f;		
		raceArray [0].GetComponent<Race>().unitMaxHealth [4] = 160f;		
		raceArray [0].GetComponent<Race>().unitArmor [4] = 3f;	
		raceArray [0].GetComponent<Race>().unitSprite [4] = Resources.Load<Sprite> ("unit5");	
		raceArray [0].GetComponent<Race>().unitCooldown [4] = 3.7f; raceArray [0].GetComponent<Race>().unitRange [4] = 0.9f;
		
		raceArray[0].GetComponent<Race>().unitName[5] = "NanoSecond" ;	
		raceArray [0].GetComponent<Race>().unitSpeed [5] = 0.9f;	
		raceArray [0].GetComponent<Race>().unitAtkDelay [5] = 600f;		
		raceArray [0].GetComponent<Race>().unitAtkDamage [5] = 10f;		
		raceArray [0].GetComponent<Race>().unitMaxHealth [5] = 80f;			
		raceArray [0].GetComponent<Race>().unitArmor [5] = 0f;	
		raceArray [0].GetComponent<Race>().unitSprite [5] = Resources.Load<Sprite> ("unit6");	
		raceArray [0].GetComponent<Race>().unitCooldown [5] = 2.8f; raceArray [0].GetComponent<Race>().unitRange [5] = 0.4f;
		
		raceArray[0].GetComponent<Race>().unitName[6] = "Lifetime" ;	 
		raceArray [0].GetComponent<Race>().unitSpeed [6] = 0.6f;	
		raceArray [0].GetComponent<Race>().unitAtkDelay [6] = 1700f;	
		raceArray [0].GetComponent<Race>().unitAtkDamage [6] = 20f;		
		raceArray [0].GetComponent<Race>().unitMaxHealth [6] = 220f;		
		raceArray [0].GetComponent<Race>().unitArmor [6] = 0f;	
		raceArray [0].GetComponent<Race>().unitSprite [6] = Resources.Load<Sprite> ("unit7");	
		raceArray [0].GetComponent<Race>().unitCooldown [6] = 3.7f; 
		raceArray [0].GetComponent<Race>().unitRange [6] = 1.1f;
		
		raceArray[0].GetComponent<Race>().unitName[7] = "Meridian" ;	 
		raceArray [0].GetComponent<Race>().unitSpeed [7] = 1.0f;	
		raceArray [0].GetComponent<Race>().unitAtkDelay [7] = 1000f;	
		raceArray [0].GetComponent<Race>().unitAtkDamage [7] = 20f;		
		raceArray [0].GetComponent<Race>().unitMaxHealth [7] = 420f;		
		raceArray [0].GetComponent<Race>().unitArmor [7] = 4f;	
		raceArray [0].GetComponent<Race>().unitSprite [7] = Resources.Load<Sprite> ("unit8");	
		raceArray [0].GetComponent<Race>().unitCooldown [7] = 6.5f; 
		raceArray [0].GetComponent<Race>().unitRange [7] = 0.8f;
		
		raceArray[0].GetComponent<Race>().unitName[8] = "Millenium" ;	 
		raceArray [0].GetComponent<Race>().unitSpeed [8] = 0.1f;	
		raceArray [0].GetComponent<Race>().unitAtkDelay [8] = 60f;		
		raceArray [0].GetComponent<Race>().unitAtkDamage [8] = 5f;		
		raceArray [0].GetComponent<Race>().unitMaxHealth [8] = 700f;	
		raceArray [0].GetComponent<Race>().unitArmor [8] = 6f;	
		raceArray [0].GetComponent<Race>().unitSprite [8] = Resources.Load<Sprite> ("unit9");
		raceArray [0].GetComponent<Race>().unitCooldown [8] = 10f;
		raceArray [0].GetComponent<Race>().unitRange [8] = 0.6f;
		
		
		
		raceArray[0].GetComponent<Race>().passiveAbility[0] = defaultPassiveAbility ; raceArray [0].GetComponent<Race>().createAbility[0] =  delegate(GameObject me) {} ; raceArray [0].GetComponent<Race>().collisionAbility[0] = delegate(GameObject me, GameObject other) {} ; raceArray[0].GetComponent<Race>().deathAbility[0] = delegate(GameObject me) {} ;
		raceArray[0].GetComponent<Race>().passiveAbility[1] = defaultPassiveAbility ; raceArray [0].GetComponent<Race>().createAbility[1] =  delegate(GameObject me) {} ; raceArray [0].GetComponent<Race>().collisionAbility[1] = delegate(GameObject me, GameObject other) {} ; raceArray[0].GetComponent<Race>().deathAbility[1] = delegate(GameObject me) {} ;
		raceArray[0].GetComponent<Race>().passiveAbility[2] = defaultPassiveAbility ; raceArray [0].GetComponent<Race>().createAbility[2] =  delegate(GameObject me) {} ; raceArray [0].GetComponent<Race>().collisionAbility[2] = delegate(GameObject me, GameObject other) {} ; raceArray[0].GetComponent<Race>().deathAbility[2] = delegate(GameObject me) {} ;
		raceArray[0].GetComponent<Race>().passiveAbility[3] = defaultPassiveAbility ; raceArray [0].GetComponent<Race>().createAbility[3] =  delegate(GameObject me) {} ; raceArray [0].GetComponent<Race>().collisionAbility[3] = delegate(GameObject me, GameObject other) {} ; raceArray[0].GetComponent<Race>().deathAbility[3] = delegate(GameObject me) {} ;
		
		raceArray [0].GetComponent<Race>().createAbility[4] = delegate(GameObject me) {
			me.GetComponent<UnitBrain>().abilityNumbers[0] = 0.2f ;
		};
		raceArray[0].GetComponent<Race>().passiveAbility[4] = delegate(GameObject me) {
			defaultPassiveAbility(me) ;
			if (me.GetComponent<UnitBrain>().enemy != null) {
				if ((Random.value<me.GetComponent<UnitBrain>().abilityNumbers[0])) {
					if (me.GetComponent<UnitBrain>().axis%2==0) {
						me.transform.position = new Vector3 (me.GetComponent<UnitBrain>().enemy.transform.position.x+(0.3f*me.GetComponent<UnitBrain>().moveDirection),me.transform.position.y,me.transform.position.z) ;
					} else {
						me.transform.position = new Vector3 (me.transform.position.x,me.GetComponent<UnitBrain>().enemy.transform.position.y+(0.3f*me.GetComponent<UnitBrain>().moveDirection),me.transform.position.z) ;
					}
				}
			}
			
		};
		raceArray [0].GetComponent<Race>().collisionAbility[4] = delegate(GameObject me, GameObject other) {} ; raceArray[0].GetComponent<Race>().deathAbility[4] = delegate(GameObject me) {} ;
		
		raceArray [0].GetComponent<Race>().createAbility[5] = delegate(GameObject me) {
			me.GetComponent<UnitBrain>().abilityNumbers[0] = 0.2f ;
		};
		raceArray[0].GetComponent<Race>().passiveAbility[5] = delegate(GameObject me) {
			defaultPassiveAbility(me) ;
			if (me.GetComponent<UnitBrain>().enemy != null) {
				if ((Random.value<me.GetComponent<UnitBrain>().abilityNumbers[0])) {
					if (me.GetComponent<UnitBrain>().axis%2==0) {
						me.transform.position = new Vector3 (me.GetComponent<UnitBrain>().enemy.transform.position.x+(0.3f*me.GetComponent<UnitBrain>().moveDirection),me.transform.position.y,me.transform.position.z) ;
					} else {
						me.transform.position = new Vector3 (me.transform.position.x,me.GetComponent<UnitBrain>().enemy.transform.position.y+(0.3f*me.GetComponent<UnitBrain>().moveDirection),me.transform.position.z) ;
					}
				}
			}
			
		};
		raceArray [0].GetComponent<Race>().collisionAbility[5] = delegate(GameObject me, GameObject other) {} ; raceArray[0].GetComponent<Race>().deathAbility[5] = delegate(GameObject me) {} ;
		
		
		
		raceArray [0].GetComponent<Race>().createAbility[6] = delegate(GameObject me) {
			me.GetComponent<UnitBrain>().abilityNumbers[0] = 0.5f ;
		};
		raceArray[0].GetComponent<Race>().passiveAbility[6] = delegate(GameObject me) {
			defaultPassiveAbility(me) ;
			me.GetComponent<UnitBrain>().currentHealth+=me.GetComponent<UnitBrain>().abilityNumbers[0] ;
		};
		raceArray [0].GetComponent<Race>().collisionAbility[6] = delegate(GameObject me, GameObject other) {}; raceArray[0].GetComponent<Race>().deathAbility[6] = delegate(GameObject me) {} ;
		
		
		
		raceArray [0].GetComponent<Race> ().createAbility [7] = delegate(GameObject me) {};
		raceArray[0].GetComponent<Race>().passiveAbility[7] = defaultPassiveAbility ; 
		raceArray [0].GetComponent<Race>().collisionAbility[7] = delegate(GameObject me, GameObject other) {
			if((other.GetComponent<colliderBrain>().type == 0)) {
				if (other.GetComponent<UnitBrain>().team == me.GetComponent<UnitBrain>().team) {
					if (me.GetComponent<UnitBrain>().axis%2==0) {
						other.transform.position = new Vector3 (me.transform.position.x-(0.3f*other.GetComponent<UnitBrain>().moveDirection),other.transform.position.y,other.transform.position.z) ;
					} else {
						other.transform.position = new Vector3 (other.transform.position.x,me.transform.position.y-(0.3f*other.GetComponent<UnitBrain>().moveDirection),other.transform.position.z) ;
					}
				}
			}
		} ; 
		raceArray[0].GetComponent<Race>().deathAbility[7] = delegate(GameObject me) {} ;
		
		
		raceArray[0].GetComponent<Race>().passiveAbility[8] = defaultPassiveAbility ; raceArray [0].GetComponent<Race>().createAbility[8] =  delegate(GameObject me) {} ; raceArray [0].GetComponent<Race>().collisionAbility[8] = delegate(GameObject me, GameObject other) {} ; 
		raceArray[0].GetComponent<Race>().deathAbility[8] = delegate(GameObject me) {
			GameObject explo = Instantiate(Resources.Load<GameObject>("explosion")) ;
			explo.GetComponent<explosionBrain>().explosionDamage = 100f ;
			explo.GetComponent<explosionBrain>().explosionSize = 1.2f ;
			explo.GetComponent<explosionBrain>().explosionTime = 0.2f ;
			explo.transform.position = me.transform.position ;
		} ;
		
		
		
		
		
		unitAmount = 9;
		raceArray [1].GetComponent<Race>().unitAmount = unitAmount ;
		
		raceArray[1].GetComponent<Race>().unitName = new string[unitAmount] ;
		raceArray[1].GetComponent<Race>().unitSpeed = new float[unitAmount] ;
		raceArray[1].GetComponent<Race>().unitAtkDelay = new float[unitAmount] ;
		raceArray[1].GetComponent<Race>().unitAtkDamage = new float[unitAmount] ;
		raceArray [1].GetComponent<Race> ().unitRange = new float[unitAmount];
		raceArray[1].GetComponent<Race>().unitMaxHealth = new float[unitAmount] ;
		raceArray[1].GetComponent<Race>().unitArmor = new float[unitAmount] ;
		raceArray[1].GetComponent<Race>().unitCooldown = new float[unitAmount] ;
		raceArray[1].GetComponent<Race>().unitSprite = new Sprite[unitAmount] ;
		raceArray[1].GetComponent<Race>().passiveAbility = new AbilityDelegate[unitAmount] ;
		raceArray[1].GetComponent<Race>().createAbility = new AbilityDelegate[unitAmount] ;
		raceArray[1].GetComponent<Race>().collisionAbility = new CollisionDelegate[unitAmount] ;
		raceArray[1].GetComponent<Race>().deathAbility = new AbilityDelegate[unitAmount] ;
		
		raceArray [1].GetComponent<Race>().raceName = "Nedrex";
		raceArray [1].transform.name = "Nedrex";
		
		raceArray[1].GetComponent<Race>().unitName[0] = "Buzzgrunt" ;		
		raceArray [1].GetComponent<Race>().unitSpeed [0] = 1.1f; 	
		raceArray [1].GetComponent<Race>().unitAtkDelay [0] = 50f;	 
		raceArray [1].GetComponent<Race>().unitAtkDamage [0] = 2.5f;		
		raceArray [1].GetComponent<Race>().unitMaxHealth [0] = 150f;		
		raceArray [1].GetComponent<Race>().unitArmor [0] = 0f;	
		raceArray [1].GetComponent<Race>().unitSprite [0] = Resources.Load<Sprite> ("unit10");
		raceArray [1].GetComponent<Race>().unitCooldown [0] = 2.6f;
		raceArray [1].GetComponent<Race>().unitRange [0] = 0.15f;	
		
		raceArray[1].GetComponent<Race>().unitName[1] = "Buzzdodger" ;		
		raceArray [1].GetComponent<Race>().unitSpeed [1] = 1.0f; 	
		raceArray [1].GetComponent<Race>().unitAtkDelay [1] = 80f;	 
		raceArray [1].GetComponent<Race>().unitAtkDamage [1] = 3.2f;		
		raceArray [1].GetComponent<Race>().unitMaxHealth [1] = 120f;	
		raceArray [1].GetComponent<Race>().unitArmor [1] = 0f;	
		raceArray [1].GetComponent<Race>().unitSprite [1] = Resources.Load<Sprite> ("unit11");
		raceArray [1].GetComponent<Race>().unitCooldown [1] = 3.0f;	
		raceArray [1].GetComponent<Race>().unitRange [1] = 0.15f;	
		
		raceArray[1].GetComponent<Race>().unitName[2] = "Boomstick" ;		
		raceArray [1].GetComponent<Race>().unitSpeed [2] = 2.5f; 
		raceArray [1].GetComponent<Race>().unitAtkDelay [2] = 1000f;
		raceArray [1].GetComponent<Race>().unitAtkDamage [2] = 0f;			
		raceArray [1].GetComponent<Race>().unitMaxHealth [2] = 40f;			
		raceArray [1].GetComponent<Race>().unitArmor [2] = 2f;	
		raceArray [1].GetComponent<Race>().unitSprite [2] = Resources.Load<Sprite> ("unit12");	
		raceArray [1].GetComponent<Race>().unitCooldown [2] = 2.2f;	
		raceArray [1].GetComponent<Race>().unitRange [2] = 0.05f;	
		
		raceArray[1].GetComponent<Race>().unitName[3] = "Junker" ;		 	 
		raceArray [1].GetComponent<Race>().unitSpeed [3] = 0.7f; 	
		raceArray [1].GetComponent<Race>().unitAtkDelay [3] = 500f; 
		raceArray [1].GetComponent<Race>().unitAtkDamage [3] = 12f;	
		raceArray [1].GetComponent<Race>().unitMaxHealth [3] = 130f;	
		raceArray [1].GetComponent<Race>().unitArmor [3] = 0f;
		raceArray [1].GetComponent<Race>().unitSprite [3] = Resources.Load<Sprite> ("unit13");	
		raceArray [1].GetComponent<Race>().unitCooldown [3] = 3.0f;	
		raceArray [1].GetComponent<Race>().unitRange [3] = 0.4f;
		
		raceArray[1].GetComponent<Race>().unitName[4] = "Buckshooter" ;		
		raceArray [1].GetComponent<Race>().unitSpeed [4] = 0.8f; 
		raceArray [1].GetComponent<Race>().unitAtkDelay [4] = 1500f;
		raceArray [1].GetComponent<Race>().unitAtkDamage [4] = 140f;	
		raceArray [1].GetComponent<Race>().unitMaxHealth [4] = 200f;		
		raceArray [1].GetComponent<Race>().unitArmor [4] = 0f;	
		raceArray [1].GetComponent<Race>().unitSprite [4] = Resources.Load<Sprite> ("unit14");
		raceArray [1].GetComponent<Race>().unitCooldown [4] = 4.0f;
		raceArray [1].GetComponent<Race>().unitRange [4] = 0.25f;		
		
		raceArray[1].GetComponent<Race>().unitName[5] = "SandBagger" ;		
		raceArray [1].GetComponent<Race>().unitSpeed [5] = 0.6f; 
		raceArray [1].GetComponent<Race>().unitAtkDelay [5] = 1000f;
		raceArray [1].GetComponent<Race>().unitAtkDamage [5] = 100f;	
		raceArray [1].GetComponent<Race>().unitMaxHealth [5] = 160f;		
		raceArray [1].GetComponent<Race>().unitArmor [5] = 0f;	
		raceArray [1].GetComponent<Race>().unitSprite [5] = Resources.Load<Sprite> ("unit15");
		raceArray [1].GetComponent<Race>().unitCooldown [5] = 4.2f;
		raceArray [1].GetComponent<Race>().unitRange [5] = 0.48f;
		
		raceArray[1].GetComponent<Race>().unitName[6] = "SandBags" ;		
		raceArray [1].GetComponent<Race>().unitSpeed [6] = 0.0f; 
		raceArray [1].GetComponent<Race>().unitAtkDelay [6] = 1000f;
		raceArray [1].GetComponent<Race>().unitAtkDamage [6] = 0;	
		raceArray [1].GetComponent<Race>().unitMaxHealth [6] = 100f;		
		raceArray [1].GetComponent<Race>().unitArmor [6] = 1f;	
		raceArray [1].GetComponent<Race>().unitSprite [6] = Resources.Load<Sprite> ("unit16");
		raceArray [1].GetComponent<Race>().unitCooldown [6] = 0.5f;
		raceArray [1].GetComponent<Race>().unitRange [6] = 0.1f;
		
		raceArray[1].GetComponent<Race>().unitName[7] = "Dozer" ;		
		raceArray [1].GetComponent<Race>().unitSpeed [7] = 1.2f; 
		raceArray [1].GetComponent<Race>().unitAtkDelay [7] = 1000f;
		raceArray [1].GetComponent<Race>().unitAtkDamage [7] = 22;	
		raceArray [1].GetComponent<Race>().unitMaxHealth [7] = 70f;		
		raceArray [1].GetComponent<Race>().unitArmor [7] = 0f;	
		raceArray [1].GetComponent<Race>().unitSprite [7] = Resources.Load<Sprite> ("unit17");
		raceArray [1].GetComponent<Race>().unitCooldown [7] = 4.0f;
		raceArray [1].GetComponent<Race>().unitRange [7] = 0.38f;
		
		raceArray[1].GetComponent<Race>().unitName[8] = "Triple Boomstick" ;		
		raceArray [1].GetComponent<Race>().unitSpeed [8] = 2.5f; 
		raceArray [1].GetComponent<Race>().unitAtkDelay [8] = 1000f;
		raceArray [1].GetComponent<Race>().unitAtkDamage [8] = 0f;			
		raceArray [1].GetComponent<Race>().unitMaxHealth [8] = 40f;			
		raceArray [1].GetComponent<Race>().unitArmor [8] = 2f;	
		raceArray [1].GetComponent<Race>().unitSprite [8] = Resources.Load<Sprite> ("unit12");	
		raceArray [1].GetComponent<Race>().unitCooldown [8] = 6.5f;	
		raceArray [1].GetComponent<Race>().unitRange [8] = 0.05f;	
	}
	//int fps = Mathf.RoundToInt(1.0f/Time.deltaTime) ;
}
