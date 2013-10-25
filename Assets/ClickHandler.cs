using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {
	private float fade;

	private Color initialColor;
	public Simon game;
	public int label;
	// Use this for initialization
	void Start () {
		fade = 1;
		initialColor = renderer.material.color;
	}
	
	public void Activate() {
		fade = 0;
		audio.Play();
	}
	
	IEnumerator fly() {
		transform.Translate(Vector3.forward * -5);
		yield return new WaitForSeconds(1f);
		transform.Translate(Vector3.forward * 5);
	}
	
	public void Die() {
		StartCoroutine(fly());
	}
	
	void OnMouseDown() {
		if (!game.isUserTurn()) {
			return;
		}
		
		Activate();
		game.Press(label);
    }
	
	void Update() {
		
		// Code for OnMouseDown in the iPhone. Unquote to test.
		RaycastHit hit = new RaycastHit();
		
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
				// Construct a ray from the current touch coordinates
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				if (Physics.Raycast(ray, out hit)) {
					hit.transform.gameObject.SendMessage("OnMouseDown");
		      	}
		  	}
		}
		
		if (fade < 1) {
			renderer.material.color = Color.Lerp(Color.white, initialColor, fade);
			fade = fade + Time.deltaTime;
		}
	}

}
