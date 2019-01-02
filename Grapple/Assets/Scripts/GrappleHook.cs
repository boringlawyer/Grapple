using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour 
{
	[SerializeField]
	GameObject firstSegment;
	// Use this for initialization
	void Start () 
	{
		firstSegment = Instantiate(firstSegment, transform.position + new Vector3(.35f, 0, 0), Quaternion.identity);
		firstSegment.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
