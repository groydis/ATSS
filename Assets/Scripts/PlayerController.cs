using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float playerSpeed = 20.0f;
	public float rotationSpeed = 20.0f;
	public int playerHealth = 10;
	public GameObject weaponSpawn;
	private Transform weaponSpawnT;
	public GameObject projectile;
	public float projectileSpeed;
	public bool gameStarted;

	public GameController gController;



	// Use this for initialization
	void Start () {
		weaponSpawnT = weaponSpawn.transform;
		gController = GameObject.Find ("GameManager").GetComponent<GameController> ();
	}

	// Update is called once per frame
	void Update () {
		KeyboardController ();
		PlayerPositionCheck ();
	}

	void KeyboardController() {
		if (GameController.gameStarted) {
			// Rotates Player based on Left / Right Movement
			transform.Rotate(0, Input.GetAxis("Horizontal") * 100 * Time.deltaTime, 0);
			// Player Constantly Moving Forward
			transform.Translate(0.0f, 0.0f, Time.deltaTime * playerSpeed * 1);
			// Fires Bullets like a mad cunzi
			if (Input.GetKey(KeyCode.Space)) {
				ShootGun();
			}

		}
	}

	void ShootGun() {
		GameObject projectileClone = Instantiate(projectile, weaponSpawnT.position, transform.rotation) as GameObject;
		audio.Play ();
		GameObject bCache = GameObject.FindGameObjectWithTag ("bCache");
		projectileClone.transform.parent = bCache.transform;
	}

	void PlayerPositionCheck() {
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

	void PlayerDeath() {
		gController.gameOver = true;
		Destroy (this.gameObject);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bullet") {
			playerHealth -= 1;
			if (playerHealth < 1) {
				PlayerDeath();
			}
		}
		if (other.tag == "Enemy") {
			PlayerDeath();
		}
	}

}
