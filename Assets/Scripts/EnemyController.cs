using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float enemySpeed = 15.0f;
	public Transform target;
	public int enemyHealth = 10; 

	public float rotationSpeed = 20.0f;

	private Transform myTransform;
	public GameObject player;

	public GameObject Splosion;
	public GameObject hitSpark;

	public ParticleSystem[] smokeEffects;


	private bool canShoot;
	public GameObject weaponSpawn;
	private Transform weaponSpawnT;
	public GameObject projectile;
	public float projectileSpeed;

	public GameController gController;

	public bool isaPrick;
	public int prickLuck;



	void Awake() {
		myTransform = transform;
		GameObject eCache = GameObject.FindGameObjectWithTag ("eCache");
		transform.parent = eCache.transform;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		target = player.transform;
		weaponSpawnT = weaponSpawn.transform;
		canShoot = false;
		gController = GameObject.Find ("GameManager").GetComponent<GameController> ();
		prickLuck = Random.Range (1, 10);
		if (prickLuck == 5) {
			isaPrick = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardsTarget ();
		EnemyPositionCheck ();
		DistanceCheck ();	
	}

	void MoveTowardsTarget () {
		if (GameController.gameStarted && !gController.gameOver) {
			transform.LookAt(target.position);
			if (!isaPrick) {
				transform.Rotate(new Vector3(0,-90,0),Space.World);
			}
			if (isaPrick) {
				transform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position + transform.position), rotationSpeed * Time.deltaTime);
			}
			myTransform.Translate (Vector3.forward.normalized * enemySpeed * Time.deltaTime);
		}
	}

	void EnemyDeath() {
		Vector3 deathLocation;
		deathLocation = transform.position;
		Destroy (this.gameObject);
		GameObject kaboom = Instantiate(Splosion, deathLocation, Quaternion.identity) as GameObject;
		kaboom.particleSystem.Play();
		gController.enemyCount--;
		gController.currentScore++;  
	}

	void EnemyPositionCheck() {
		float posX = transform.position.x;
		float posY = transform.position.y;
		float posZ = transform.position.z;
		if (posZ > 24.0f) {
			transform.position = new Vector3(posX, posY, -posZ);		
		}
		if (posZ < -24.0f) {
			transform.position = new Vector3(posX, posY, -posZ);		
		}
		if (posX > 38.0f) {
			transform.position = new Vector3(-posX, posY, posZ);		
		}
		if (posX < -38.0f) {
			transform.position = new Vector3(-posX, posY, posZ);		
		}
	}
	void PlaySMoke() {
		smokeEffects = GetComponentsInChildren<ParticleSystem> ();
		foreach (ParticleSystem smoke in smokeEffects) {
			smoke.Play(withChildren:true);
		}
	}

	void PlaySpark() {
		GameObject spark = Instantiate (hitSpark, transform.position, Quaternion.identity) as GameObject;
		spark.particleSystem.Play ();
	}

	void DistanceCheck () {
		float dist = Vector3.Distance(target.position, transform.position);
		if (dist < 10) {
			canShoot = true;
			Shoot ();		
		}
	}

	void Shoot() {
		if (canShoot && GameController.gameStarted) {
			GameObject projectileClone = Instantiate(projectile, weaponSpawnT.position, transform.rotation) as GameObject;
			audio.Play ();	
			GameObject bCache = GameObject.FindGameObjectWithTag ("bCache");
			projectileClone.transform.parent = bCache.transform;
		}
	}
	

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PlayerBullet") {
			enemyHealth -= 1;
			PlaySpark();
			if (enemyHealth < 5) {
				PlaySMoke();
			}
			if (enemyHealth < 1) {
				EnemyDeath();
			}
		}
		if (other.tag == "Player") {
			EnemyDeath();		
		}
	}

}
