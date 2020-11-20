using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public Player playerRef;

	public void clearLines()
	{
		playerRef.reset();
	}

	public void startGame()
	{
		Application.LoadLevel("Game");
	}
}
