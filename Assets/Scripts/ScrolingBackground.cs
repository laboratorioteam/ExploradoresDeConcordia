using UnityEngine;
using System.Collections;

public class ScrolingBackground : MonoBehaviour
{
	public bool scrolling, paralax;

	private float backgroundSize;
	public float speed;
	public float paralaxSpeed;

	public Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 10;
	private int leftIndex;
	private int rightIndex;
	private float lastCamreX;

	void Start ()
	{
		
		cameraTransform = Camera.main.transform;
		lastCamreX = cameraTransform.position.x;
		layers = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
			layers [i] = transform.GetChild (i);

		leftIndex = 0;
		rightIndex = layers.Length - 1;

		backgroundSize = layers[0].GetComponent<SpriteRenderer> ().bounds.size.x;
	}

	void Update ()
	{
		if(paralax)
		{
			float deltaX = cameraTransform.position.x - lastCamreX;
			cameraTransform.position += Vector3.right * (deltaX * paralaxSpeed);
		}

		lastCamreX = cameraTransform.position.x;


		if(scrolling)
		{
		if(cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
			ScrollLeft ();

		if(cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
			ScrollRight ();
		}

		transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime),transform.position.y, transform.position.z ) ;

	}

	private void ScrollLeft()
	{
		int lastRight = rightIndex;
		layers [rightIndex].position = new Vector3( (layers [leftIndex].position.x - backgroundSize), layers [rightIndex].position.y, layers [rightIndex].position.z);
		leftIndex = rightIndex;
		rightIndex--;
		if (rightIndex < 0)
			rightIndex = layers.Length - 1;
	}

	private void ScrollRight()
	{
		int lastLeft = leftIndex;
		layers [leftIndex].position = new Vector3( layers [rightIndex].position.x + backgroundSize, layers [rightIndex].position.y, layers [rightIndex].position.z);
		rightIndex = leftIndex;
		leftIndex++;
		if (leftIndex == layers.Length)
			leftIndex = 0;
	}

}

