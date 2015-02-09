using UnityEngine;
using System.Collections;

public class FxRemover : MonoBehaviour {


	public float RemoveTime = 1;
	float counter = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		counter+=Time.deltaTime;
		if(counter>RemoveTime)
			GameObject.Destroy(gameObject);
	}
}
