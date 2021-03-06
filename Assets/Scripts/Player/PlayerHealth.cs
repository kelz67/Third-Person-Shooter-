﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructable {

	[SerializeField]SpawnPoint[] spawnPoints;


	void SpawnAtNewSpawnpoint() {
		int spawnIndex= Random.Range(0,spawnPoints.Length);
		print (spawnIndex);
		transform.position = spawnPoints [spawnIndex].transform.position;
		transform.rotation = spawnPoints [spawnIndex].transform.rotation;
	}

	public override void Die()
	{
		base.Die ();
		SpawnAtNewSpawnpoint ();
	}
		
	[ContextMenu("Test Die!")]
	void TestDie(){
		Die ();
	}
}
