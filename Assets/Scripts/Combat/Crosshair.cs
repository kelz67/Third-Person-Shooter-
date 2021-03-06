﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

	[SerializeField]Texture2D image;
	[SerializeField]int size;




	void OnGUI(){
		if (GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EweaponState.AIMING ||
		    GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EweaponState.AIMEDFIRING)
		{
			Vector3 screenPosition = Camera.main.WorldToScreenPoint (transform.position);
			screenPosition.y = Screen.height - screenPosition.y;
			GUI.DrawTexture (new Rect (screenPosition.x- size/2, screenPosition.y-size/2, size, size), image);
		}
	}
}
