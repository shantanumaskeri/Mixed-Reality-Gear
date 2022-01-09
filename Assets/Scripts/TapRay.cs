/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TapRay : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES
    public ViewTrigger[] viewTriggers;
    #endregion // PUBLIC_MEMBER_VARIABLES
	private Vector3 pos = Vector3.zero;

    #region MONOBEHAVIOUR_METHODS
    void Update()
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
				//CheckRaycastCollisions ();
			}
		}
    }

	void CheckRaycastCollisions ()
	{
		// Check if the Head gaze direction is intersecting any of the ViewTriggers
		RaycastHit hit;
		Ray cameraGaze = new Ray(this.transform.position, this.transform.forward);
		Physics.Raycast(cameraGaze, out hit, Mathf.Infinity);
		foreach (var trigger in viewTriggers)
		{
			trigger.Focused = hit.collider && (hit.collider.gameObject == trigger.gameObject);
		}
	}
    #endregion // MONOBEHAVIOUR_METHODS
}

