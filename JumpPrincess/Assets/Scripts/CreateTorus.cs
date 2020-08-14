using System.Collections.Generic;
using UnityEngine;

public class CreateTorus : MonoBehaviour
{

    [SerializeField]
    private Material _material;

    private Mesh _mesh;

    [SerializeField]
    private float radius = 1.0f;
    [SerializeField]
    private float thickness = 1.0f;
    [SerializeField]
    private float expandSpeed = 1.0f;

    void Update()
    {
        radius += Time.deltaTime * expandSpeed;

        transform.localEulerAngles += new Vector3(0f, 10f, 0f);

        if (radius > 17f)
        {
            Destroy(this.gameObject);
        }

        var r1 = radius;
        var r2 = thickness;
        var n = Random.Range(12, 30);

        _mesh = new Mesh();

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();

        // (1) トーラスの計算
        for (int i = 0; i <= n; i++)
        {
            var phi = Mathf.PI * 2.0f * i / n;
            var tr = Mathf.Cos(phi) * r2;
            var y = Mathf.Sin(phi) * r2;

            for (int j = 0; j <= n; j++)
            {
                var theta = 2.0f * Mathf.PI * j / n;
                var x = Mathf.Cos(theta) * (r1 + tr);
                var z = Mathf.Sin(theta) * (r1 + tr);

                vertices.Add(new Vector3(x, y, z));
                // (2) 法線の計算
                normals.Add(new Vector3(tr * Mathf.Cos(theta), y, tr * Mathf.Sin(theta)));
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var count = (n + 1) * j + i;
                // (3) 頂点インデックスを指定
                triangles.Add(count);
                triangles.Add(count + n + 2);
                triangles.Add(count + 1);

                triangles.Add(count);
                triangles.Add(count + n + 1);
                triangles.Add(count + n + 2);
            }
        }

        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = triangles.ToArray();
        _mesh.normals = normals.ToArray();

        _mesh.RecalculateBounds();

        Graphics.DrawMesh(_mesh, transform.position, transform.rotation, _material, 0);
        //Graphics.DrawMeshNow(_mesh, transform.localToWorldMatrix, 0);
    }
}