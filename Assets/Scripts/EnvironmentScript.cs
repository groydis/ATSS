using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironmentScript : MonoBehaviour {

	private List<Vector3> forceDirList = new List<Vector3> ();
	private Vector3 forDir;

	private int forceAmount;
	// Use this for initialization
	void Start () {
	
		forceAmount = Random.Range (1, 15);
		forceDirList.Add (Vector3.forward);
		forceDirList.Add (Vector3.left);
		forceDirList.Add (Vector3.right);
		forceDirList.Add (Vector3.back);
		forDir = forceDirList [Random.Range (0, forceDirList.Count)];


	}

	void FixedUpdate() {
		RotateInSpace() ;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void RotateInSpace() {
		transform.Rotate(forDir.normalized * forceAmount * Time.deltaTime, Space.World);
	}

		
}
