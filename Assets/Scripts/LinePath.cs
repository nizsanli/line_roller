using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinePath : MonoBehaviour {

	private int currIndex;

	List<Vector3> vertices;

	LineRenderer lineRenderer;
	EdgeCollider2D edgeCollider;

	Vector2[] colliderPoints;

	public EdgeCollider2D colliderPrefab;

	public void init(Vector3 pos)
	{
		lineRenderer = gameObject.GetComponent<LineRenderer>();
		edgeCollider = gameObject.GetComponent<EdgeCollider2D>();

		//renderer.material = new Material (Shader.Find("Particles/Additive"));
		GetComponent<Renderer>().material = (Material) Resources.Load("ParticlesAdditive");

		lineRenderer.SetWidth(0.2f, 0.2f);

		vertices = new List<Vector3>();
		vertices.Add(pos);
		currIndex = 1;

		edgeCollider = (EdgeCollider2D) Instantiate(colliderPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
		edgeCollider.transform.parent = transform;

		Vector2[] initialPoints = new Vector2[2];
		initialPoints[0] = new Vector2(-1f, 0f);
		initialPoints[1] = new Vector2(-2f, 0f);

		edgeCollider.points = initialPoints;
	}

	public void append(Vector3 pos, Player player)
	{
		if (Vector3.Distance(vertices[currIndex-1], pos) > 0.015f)
		{
			vertices.Add(pos);
			currIndex++;
			player.useSupply();
		}
	}

	public void makeLine()
	{
		lineRenderer.SetVertexCount(vertices.Count);
		colliderPoints = new Vector2[vertices.Count];
		for (int i = 0; i < vertices.Count; i++)
		{
			Vector3 pos = vertices[i];
			lineRenderer.SetPosition(i, pos);

			colliderPoints[i] = pos;
		}

		if (vertices.Count > 1)
		{
			edgeCollider.points = colliderPoints;
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
