﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	[System.Serializable]
	public class CameraRig
	{
		public Vector3 CameraOffset;
		public float CrouchHeight;
		public float Damping;
	}
	//0.68, 0.19, -4.52
	//10
	[SerializeField]CameraRig defaultCamera;
	[SerializeField]CameraRig aimCamera;


	Transform cameraLookTarget;
	Player localPlayer;

	void Awake (){
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
		;
	}

	void HandleOnLocalPlayerJoined (Player player)
	{
		localPlayer = player;
		cameraLookTarget = localPlayer.transform.Find ("AimPivot");
		if (cameraLookTarget == null)
			cameraLookTarget = localPlayer.transform;
	}

	void Start () {

	}

	// Update is called once per frame
	void LateUpdate () {
		if (localPlayer == null)
			return;
		CameraRig cameraRig = defaultCamera;

		if (localPlayer.PlayerState.WeaponState == PlayerState.EweaponState.AIMING || localPlayer.PlayerState.WeaponState == PlayerState.EweaponState.AIMEDFIRING)
			cameraRig = aimCamera;

		float targetHeight = cameraRig.CameraOffset.y + (localPlayer.PlayerState.MoveState == PlayerState.EMoveState.CROUCHING ? cameraRig.CrouchHeight : 0);

		Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraRig.CameraOffset.z +
			localPlayer.transform.up * targetHeight +
			localPlayer.transform.right * cameraRig.CameraOffset.x;
		//Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

		Vector3 collisionDestination= cameraLookTarget.position + localPlayer.transform.up * targetHeight- localPlayer.transform.forward*.5f;
		HandleCameraCollision (collisionDestination, ref targetPosition);
	

		transform.position = Vector3.Lerp (transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, cameraLookTarget.rotation, cameraRig.Damping * Time.deltaTime);
	}

	private void HandleCameraCollision(Vector3 toTarget, ref Vector3 fromTarget)
	{
		RaycastHit hit;
		if (Physics.Linecast (toTarget, fromTarget, out hit)) 
		{
			Vector3 hitPoint = new Vector3 (hit.point.x + hit.normal.x * .2f, hit.point.y, hit.point.z + hit.normal.z * .2f);
			fromTarget = new Vector3 (hitPoint.x, fromTarget.y, hitPoint.z);
		}

	}
}
