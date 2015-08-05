using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayScore : MonoBehaviour {
	
	Text txt;

	public GameController gController;
	
	// Use this for initialization
	void Start () {
		gController = GameObject.Find ("GameManager").GetComponent<GameController> ();
		txt = gameObject.GetComponent<Text>(); 
		txt.text="Score : " + gController.currentScore;
	}
	
	// Update is called once per frame
	void Update () {
		txt.text="Score : " + gController.currentScore;  
	}
}
