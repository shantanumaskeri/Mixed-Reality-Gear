using UnityEngine;
using System.Collections;
using System.IO;

public class ApplicationManager : MonoBehaviour 
{
	public static ApplicationManager Instance;

	public bool isTrackable;
	public GameObject gyroText;

	private Vector3 pos = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		Instance = this;

		isTrackable = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{
			pos = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0))
		{
			var delta = Input.mousePosition-pos;
			if (delta == new Vector3 (0, 0, 0))
			{
				if (isTrackable)
				{
					TriggerManager.Instance.doTransition = true;

					Invoke ("ResetTracking", 0.1f);
				}
			}
		}

		//gyroText.GetComponent<TextMesh>().text = "A: "+Input.acceleration.x+" \n "+Input.acceleration.y+" \n "+Input.acceleration.z;
	}

	void ResetTracking ()
	{
		isTrackable = false;
	}
}
