using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public LinePath pathPrefab;

	private LinePath currPath;

	private List<LinePath> paths;

	public Transform ballRef;

	private int capacity;

	public Text fuelText;
	public Text scoreText;
	public Text scoreResultText;

	private int initialCapacity;

	public LevelManager levelManager;

	public GameObject gameOverPanel;

	bool isGameOver = false;

	int distance = 0;

	// Use this for initialization
	void Start () {
		currPath = null;
		paths = new List<LinePath>();

		initialCapacity = 200;
		capacity = initialCapacity;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGameOver)
		{
			distance = (int) (ballRef.position.x * 0.5f);
		}

		fuelText.text = "Paint Fuel: " + capacity.ToString();
		scoreText.text = distance.ToString() + " m";

		if (Input.GetMouseButton(0) && capacity > 0 && !isGameOver)
		{
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseWorldPos.z = 0f;

			if (Input.GetMouseButtonDown(0))
			{	
				// make new path
				currPath = (LinePath) Instantiate(pathPrefab, Vector3.zero, Quaternion.identity);
				currPath.init(mouseWorldPos);

				paths.Add(currPath);
			}

			if (currPath != null)
			{
				// continue making path
				currPath.append(mouseWorldPos, this);
			}
		}

		foreach (LinePath path in paths)
		{
			path.makeLine();
		}

		Vector3 camPos = Camera.main.transform.position;
		camPos.z = 0f;
		float dist = Vector3.Distance(camPos, ballRef.position);
		
		if (dist > 2f)
		{
			Camera.main.transform.Translate((ballRef.position - camPos).normalized * (dist - 2f));
		}
	}

	public void boost(int amount)
	{
		Vector2 dir = ballRef.GetComponent<Rigidbody2D>().velocity;
		ballRef.GetComponent<Rigidbody2D>().AddForce(dir.normalized * amount);
	}

	public void useSupply()
	{
		capacity--;
	}

	public void refill(int amount)
	{
		capacity += amount;
	}

	public void gameOver()
	{
		scoreResultText.text = "You went " + distance + " m.";
		gameOverPanel.SetActive(true);

		isGameOver = true;
	}

	public void reset()
	{
		currPath = null;

		for (int i = 0; i < paths.Count; i++)
		{
			Destroy(paths[i].gameObject);
		}
		paths.Clear();

		ballRef.transform.position = new Vector3(0f, 3f, 0f);
		ballRef.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
		ballRef.GetComponent<Rigidbody2D>().angularVelocity = 0f;

		Camera.main.transform.position = new Vector3(0f, 0f, -10f);

		capacity = initialCapacity;
		distance = 0;

		for (int i = 0; i < levelManager.transform.childCount; i++)
		{
			Destroy(levelManager.transform.GetChild(i).gameObject);
		}
		levelManager.resetGrid();

		gameOverPanel.SetActive(false);
		isGameOver = false;
	}
}
