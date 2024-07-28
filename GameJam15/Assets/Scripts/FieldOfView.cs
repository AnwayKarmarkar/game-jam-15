
using UnityEngine;

public class FieldOfView : MonoBehaviour
{



    [SerializeField] private LayerMask viewMask;
    private float fov;
    private Vector3 origin;
    private float startingAngle;
    [SerializeField]
    float viewDistance = 50;
    int rayCount = 75;

    private Mesh mesh;

    //Subject to change
    public Transform player;
    float time;
    public float damageTick = 2f;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 360;






    }
    private void Update()
    {
        SetOrigin(player.position);

    }
    private void LateUpdate()
    {
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D rayCast = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, viewMask);
            if (rayCast.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;

            }
            else
            {

                

                vertex = rayCast.point;
                
               

              
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;

        }



        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));


    }

    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;

    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;

    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov / 2;

    }



}
