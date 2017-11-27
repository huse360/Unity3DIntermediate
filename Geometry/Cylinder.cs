Copyright (c) <2017> <HUSSEIN NAZARALA>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {

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
	int levels = 7;

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
		
				
		for (y = 0; y < levels; y ++)
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
		
		for (int k = 0; k < levels -1; k ++)
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
