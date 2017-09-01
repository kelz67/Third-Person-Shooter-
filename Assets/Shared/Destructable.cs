﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Destructable : MonoBehaviour {
	[SerializeField]float hitsPoints;

	public event System.Action OnDeath;
	public event System.Action OnDamageReceived;

	float damageTaken;

	public float HitPointsRemaining{
		get {
			return hitsPoints - damageTaken;
		}
	}


	public bool IsAlive {
		get {
			return HitPointsRemaining > 0;
		}
	}

	public virtual void Die(){
		if (!IsAlive)
			return;

		if (OnDeath != null)
			OnDeath ();

	}

	public virtual void TakeDamage(float amount){
		damageTaken += amount;

		if(OnDamageReceived != null)
			OnDamageReceived ();

		if (HitPointsRemaining <= 0){
			Die ();
		}
	}

	public void Reset() {
		damageTaken = 0;
	}
}
