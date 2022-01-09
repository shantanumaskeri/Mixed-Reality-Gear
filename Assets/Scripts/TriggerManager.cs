/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerManager : MonoBehaviour
{
	public static TriggerManager Instance;

    public enum TriggerType
    {
        VR_TRIGGER,
        AR_TRIGGER
    }

    #region PUBLIC_MEMBER_VARIABLES
    public TriggerType triggerType = TriggerType.VR_TRIGGER;
    public float activationTime = 1.5f;
    public bool doTransition = false;
    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES
    private float mFocusedTime = 0;
    private bool mTriggered = false;
	private bool goingBackToAR = true;
    private TransitionManager mTransitionManager;
    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
		Instance = this;

        mTransitionManager = FindObjectOfType<TransitionManager>();
        mTriggered = false;
        mFocusedTime = 0;
        doTransition = false;
    }

    void Update()
    {
		if (mTriggered) 
			return;

        //UpdateMaterials(Focused);
		bool startAction = false;
		if (Input.GetMouseButtonUp (0)) 
		{
			if (ApplicationManager.Instance.isTrackable)
			{
				startAction = true;	
			}
		}

		if (doTransition)
		{
			UpdateTransitions (startAction);
		}
    }
    #endregion // MONOBEHAVIOUR_METHODS

	 #region PRIVATE_METHODS
	private void UpdateTransitions (bool startAction)
	{
		// Update the "focused state" time
		mFocusedTime += Time.deltaTime;
		if ((mFocusedTime > activationTime) || startAction)
		{
			mTriggered = true;
			mFocusedTime = 0;

			// Activate transition from AR to VR or vice versa
			goingBackToAR = !goingBackToAR;
			mTransitionManager.Play(goingBackToAR);
			StartCoroutine(ResetAfter(0.3f*mTransitionManager.transitionDuration));
		}
	}

    private IEnumerator ResetAfter(float seconds)
    {
		this.gameObject.SetActive (true);

        Debug.Log("Resetting View trigger after: " + seconds);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Resetting View trigger: " + this.gameObject.name);

        // Reset variables
        mTriggered = false;
        mFocusedTime = 0;
        doTransition = false;
		ApplicationManager.Instance.isTrackable = true;
    }
    #endregion // PRIVATE_METHODS
}

