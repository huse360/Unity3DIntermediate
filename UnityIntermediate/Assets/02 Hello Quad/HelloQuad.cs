using UnityEngine;
using System.Collections;

public class HelloQuad : MonoBehaviour {

	Mesh mesh;
	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	Vector3[] vertices;
	int[] triangles;
	
	public Material material;

	// Use this for initialization
	void Start () {
	
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		
		meshRenderer.material = material;
		
		mesh = new Mesh();
		meshFilter.mesh = mesh;
		
		vertices = new[] {
			new Vector3(0,0,0),
			new Vector3(0,1,0), 
			new Vector3(1,0,0),
			new Vector3(1,1,0),

		};
		
		mesh.vertices = vertices;
		
		triangles = new[]{
			0, 1, 2,
			2, 1, 3, 
		};
		
		mesh.triangles = triangles;
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
