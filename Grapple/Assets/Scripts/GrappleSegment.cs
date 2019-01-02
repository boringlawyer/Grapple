using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSegment : MonoBehaviour 
{
	[SerializeField]
	int maxSegments = 10;
	static int currentSegment = 0;
	GameObject nextSegment;
	// Use this for initialization
	void Start () 
	{
		if (currentSegment < maxSegments)
		{
			nextSegment = Instantiate(this.gameObject, transform.localPosition + new Vector3(transform.localScale.x / 2, 0, 0), Quaternion.identity);
			currentSegment += 1;
			nextSegment.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator ShootGrapple()
	{
		yield return new WaitForEndOfFrame();
	}
}
