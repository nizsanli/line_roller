using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public PaintUpItem paintUpPrefab;

	private List<Item> items;

	private Dictionary<Vector3, LevelChunk> grid;
	public int chunkSize = 10;
	int loadNumChunks = 2;

	public Player player;
	public LevelChunk chunkPrefab;

	public Transform ground;

	public System.Random rand;

	// Use this for initialization
	void Start () {
		grid = new Dictionary<Vector3, LevelChunk>();

		loadChunks();
	}

	private void loadChunks()
	{
		rand = new System.Random();

		Vector3 ballPos = player.ballRef.position;
		
		float loadRadius = chunkSize*loadNumChunks;
		Rect loadRect = new Rect(ballPos.x - loadRadius, ballPos.y - loadRadius, loadRadius*2f, loadRadius*2f);
		
		for (int y = 0; y < loadNumChunks*2; y++)
		{
			for (int x = 0; x < loadNumChunks*2; x++)
			{
				Vector3 currChunk = new Vector3(Mathf.Floor(loadRect.xMin/chunkSize) + x, Mathf.Floor(loadRect.yMin/chunkSize) + y, 0f);
				if (!grid.ContainsKey(currChunk) && (currChunk.y > ground.position.y/chunkSize))
				{
					LevelChunk chunk = (LevelChunk) Instantiate(chunkPrefab, currChunk*chunkSize, Quaternion.identity);
					grid.Add(currChunk, chunk);
					
					chunk.transform.parent = transform;
					
					chunk.init(this);
				}
			}
		}
	}

	public void resetGrid()
	{
		grid = new Dictionary<Vector3, LevelChunk>();
	}
	
	// Update is called once per frame
	void Update () {
		loadChunks();
	}
}
