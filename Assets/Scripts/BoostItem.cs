using UnityEngine;
using System.Collections;

public class BoostItem : Item {

	public Player playerRef;
	int boost;

	public override void activate()
	{
		playerRef.boost(boost);
	}
	
	public override void init(Player player)
	{
		System.Random rand = new System.Random();
		boost = rand.Next(150, 301);
		
		playerRef = player;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		activate();
		Destroy(this.gameObject);
	}
}
