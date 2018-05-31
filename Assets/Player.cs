using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	private CharacterController cc;
	public bool walking = false;
	public bool continuGame = false;
	private Vector3 spawnPoint;


	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		spawnPoint = transform.position;	//設定重生點為起始點
	}
	
	// Update is called once per frame
	void Update () {
		if (continuGame) {
			if (walking) {	//持續往攝影機的前方走
				cc.SimpleMove (Camera.main.transform.forward * 130 * Time.deltaTime);
			}
		}

		if (transform.position.y < -10f) {	//判斷掉到場景外，則返回重生點
			transform.position = spawnPoint;
		}

		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (.5f, .5f, 0));	//設定射線
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {	//若射線打到Plane上,則停止前進
			if (hit.collider.name.Contains ("Plane")) {
				walking = false;
			} else {
				walking = true;
			}
		}
	}
}
