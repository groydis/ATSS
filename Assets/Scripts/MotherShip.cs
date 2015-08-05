using UnityEngine;
using System.Collections;

public class MotherShip : MonoBehaviour {

	public GameController gController;

	// Use this for initialization
	void Start () {

		gController = GameObject.Find ("GameManager").GetComponent<GameController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		MotherShipPositionCheck ();
	}

	void MotherShipPositionCheck() {
		float posX = transform.position.x;
		if (posX < -80.0f) {
			gController.motherShipGone = true;
			//Destroy(this.gameObject);
			gController.mothershipMoving = false;
		}
	}
}
