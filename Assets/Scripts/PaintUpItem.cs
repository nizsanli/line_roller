using UnityEngine;
using System.Collections;

public class PaintUpItem : Item {

	private int refuel;
	public Player playerRef;

	public override void activate()
	{
		playerRef.refill(refuel);
	}

	public override void init(Player player)
	{
		System.Random rand = new System.Random();
		refuel = rand.Next(10, 51);

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
