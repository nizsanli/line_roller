﻿using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		player.gameOver();
	}
}
