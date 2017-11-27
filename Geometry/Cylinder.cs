using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

	List<Vector3> vertices;
	List<int> triangles;
	Mesh mesh;
	MeshFilter meshFilter;
	MeshRenderer meshRenderer;
	
	public Material material;
	
	int segments = 16 +1;
	float width = 1.0f;
	float height = 1.0f;
	float x,y;
	
	float radius = 5.0f;
	Vector3 pos;
	float angle;
	float angleAmount;
	int levels = 3;

	// Use this for initialization
	void Start () {
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();
		
		meshRenderer.material = material;
		
		mesh = new Mesh();
		meshFilter.mesh = mesh;
		
		vertices = new List<Vector3>();
		
		pos = new Vector3(0,0,0);
		angleAmount = 2 * Mathf.PI / segments;
		
				
		for (y = 0; y < 4; y ++)
		{
			
			for (x = 0; x < segments; x ++)
			{
				pos.x = radius * Mathf.Sin(angle);
				pos.y = y * height;
				pos.z = radius * Mathf.Cos(angle);
				
				//vertices.Add(new Vector3(x * width, y * height, 0));
				vertices.Add(new Vector3(pos.x, pos.y, pos.z));
				
				angle -= angleAmount;
			}
		
		}
	
		
		mesh.vertices = vertices.ToArray();
			
		
		triangles = new List<int>();
			
		int ceil;
		int floor;
		
		for (int k = 0; k < 4 -1; k ++)
		{
			 ceil  = segments * (k+1);
			 floor = segments * k;
		
			for (int i = 0; i < segments-1; i ++)
			{
			
				triangles.Add(floor);
				triangles.Add(ceil);
				triangles.Add(floor + 1);

				triangles.Add(ceil);
				triangles.Add(ceil + 1);
				triangles.Add(floor + 1);
			
				ceil++;
				floor++;
			}
			
			//Cerrar el cilindro
			triangles.Add(floor + 0);
			triangles.Add(ceil + 0);
			triangles.Add(segments * k);
			
			triangles.Add(ceil + 0);
			triangles.Add(segments * (k + 1));
			triangles.Add(segments * k);
			
		}

	
	
		mesh.triangles = triangles.ToArray();
	}
	

}
