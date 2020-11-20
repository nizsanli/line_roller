using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelChunk : MonoBehaviour {

	public Item paintUpPrefab;
	public Item boostPrefab;

	public BoxCollider2D stopperPrefab;

	public void init(LevelManager manager)
	{
		System.Random rand = manager.rand;

		for (int y = 0; y < manager.chunkSize; y++)
		{
			for (int x = 0; x < manager.chunkSize; x++)
			{
				Vector3 spawnPos = new Vector3(transform.position.x + x, transform.position.y + y, 0f);

				float chance = .01f;
				float chance2 = .02f;

				if (transform.position.y > 150f)
				{
					chance *= 0.1f;
					chance2 *= 0.1f;
				}

				int level = (int)(1000 * chance);

				int randNum = rand.Next(1001);
				if (randNum < level)
				{
					// put fuel item
					Item item = (Item) Instantiate(paintUpPrefab, spawnPos, Quaternion.identity);
					item.transform.parent = transform;

					item.init(manager.player);
				}
		
				int level2 = (int)(1000 * chance2);

				randNum = rand.Next(1001);
				if (randNum < level2)
				{
					// put boost item
					Item item = (Item) Instantiate(boostPrefab, spawnPos, Quaternion.identity);
					item.transform.parent = transform;
					
					item.init(manager.player);
				}

				float chance3 = .006f;
				int level3 = (int)(1000 * chance3);

				randNum = rand.Next(1001);
				if (randNum < level3)
				{
					// put stopper
					BoxCollider2D collider = (BoxCollider2D) Instantiate(stopperPrefab, spawnPos, Quaternion.identity);
					collider.transform.parent = transform;
					
					int widthScale = rand.Next(1, 2);
					int heightScale = rand.Next(1, 5);
					collider.transform.localScale = new Vector3(widthScale, heightScale, 1f);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
