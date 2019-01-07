using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSegment : MonoBehaviour 
{
	[SerializeField]
	int maxSegments = 10;
	static int currentSegment = 0;
	GameObject nextSegment;
	TransformOffset scaleOffset;
	HingeJoint2D joint;
	public Vector3 Anchor
	{
		get
		{
			return transform.position - Vector3.Scale(transform.right / 2, transform.localScale);
		}
	}
	// Use this for initialization
	void Start () 
	{
		scaleOffset = GetComponent<TransformOffset>();
		joint = GetComponent<HingeJoint2D>();
		if (currentSegment < maxSegments)
		{
			nextSegment = Instantiate(this.gameObject, transform.position + new Vector3(transform.localScale.x / 2, 0, 0), Quaternion.identity);
			//nextSegment = Instantiate(this.gameObject, transform.localPosition + new Vector3(.03f, 0, 0), Quaternion.identity);
			currentSegment += 1;
			nextSegment.GetComponent<HingeJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		scaleOffset.offset = Anchor;
		 joint.anchor = new Vector3(0, 0);
		joint.connectedAnchor = Vector2.zero;

		if (nextSegment != null)
		{
			// joint.anchor = new Vector3(-1, 0);
			//joint.connectedAnchor = Vector2.zero;
			
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			// GetComponent<TransformOffset>().ScaleOffset(new Vector3(1.1f, 1, 1));
			transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z);
		}
	}

	IEnumerator ShootGrapple()
	{
		yield return new WaitForEndOfFrame();
	}
}
