using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simon : MonoBehaviour {
	private List<int> botMoves;
	private List<int> userMoves;
	private bool allowUserInput;
	private GameObject red;
	private GameObject blue;
	private GameObject green;
	private GameObject yellow;
	
	public bool isUserTurn() {
		return allowUserInput;
	}
	
	public GameObject getCube(int label) {
		switch (label) {
		case 1:
			return blue;
		case 2:
			return yellow;
		case 3:
			return red;
		case 4:
			return green;
		}
		return null;
	}
	
	public void Press(int label) {
		userMoves.Add(label);
		
		if (userMoves.Count > botMoves.Count) {
			StartCoroutine(restart());
			return;
		}
		
		for (int i = 0; i < userMoves.Count; i++) {
			if (userMoves[i] != botMoves[i]) {
				StartCoroutine(restart());
				return;
			}
        }
		
		if (userMoves.Count == botMoves.Count) {
			allowUserInput = false;
			StartCoroutine(play());
		}
	}
	
	IEnumerator restart() {
		allowUserInput = false;
		userMoves.Clear();
		botMoves.Clear();
		
		yield return new WaitForSeconds(.5f);
		audio.Play();
		
		blue.SendMessage("Die");
		yield return new WaitForSeconds(.2f);
		
		yellow.SendMessage("Die");
		yield return new WaitForSeconds(.2f);
		
		red.SendMessage("Die");
		yield return new WaitForSeconds(.2f);
		
		green.SendMessage("Die");
		yield return new WaitForSeconds(.2f);
		
		yield return new WaitForSeconds(2f);
				
		StartCoroutine(play());
	}
	
	   // Define the schedule of events.
	IEnumerator play() {
		yield return new WaitForSeconds(1f);
		
		botMoves.Add(Random.Range(1, 5));
		
		foreach (int label in botMoves) {
			GameObject cube = getCube(label);
			cube.SendMessage("Activate");
			yield return new WaitForSeconds(.5f);
		}
	
		userMoves.Clear();
		allowUserInput = true;
   	}
	
	// Use this for initialization
	void Start () {
		botMoves = new List<int>();
		userMoves = new List<int>();

		red = GameObject.Find("redCube");
		blue = GameObject.Find("blueCube");
		green = GameObject.Find("greenCube");
		yellow = GameObject.Find("yellowCube");

		allowUserInput = false;
		StartCoroutine(play());
	}
	
	// Update is called once per frame
	void Update () {
	}	
}
