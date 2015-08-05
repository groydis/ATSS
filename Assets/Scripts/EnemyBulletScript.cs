using UnityEngine;
using System.Collections;

public class EnemyBulletScript : MonoBehaviour {
	public float projectileSpeed;

	
	// Use this for initialization
	void Start () {
		
	}
	
	void FixedUpdate() {
		transform.Translate (Vector3.forward * Time.deltaTime * projectileSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		BulletCheck ();
	}
	
	void BulletCheck() {
		float posX = transform.position.x;
		float posZ = transform.position.z;
		if (posZ >= 24.5f || posZ <= -24.5 || posX >= 38.0f || posX <= -38.0f) {
			Destroy(this.gameObject);		
		}
	}
	
	void OnTriggerEnter(Collider other) {
		Destroy (this.gameObject);
	}
}
