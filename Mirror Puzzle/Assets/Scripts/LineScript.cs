using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class LineScript : MonoBehaviour
{
    const int Infinity = 100;

    int maxReflections = 100;
    int currentReflections = 0;

    [SerializeField]
    Vector2 startPoint, direction;
    List<Vector3> Points;
    int defaultRayDistance = 100;
    LineRenderer lr;

    BoxCollider2D endPointCollider;

    BoxCollider2D MirrorCollider;

    int enemyLayer = 8;

    UnityEngine.Experimental.Rendering.Universal.Light2D endPointLight;
    UnityEngine.Experimental.Rendering.Universal.Light2D endPointLightShadow;

    // Use this for initialization
    void Start()
    {
        Points = new List<Vector3>();
        lr = transform.GetComponent<LineRenderer>();
        endPointCollider = GameObject.Find("End Point").GetComponent<BoxCollider2D>();

        endPointLight = GameObject.Find("EndPointLight").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        endPointLightShadow = GameObject.Find("EndPointLightShadow").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();

        MirrorCollider = GameObject.Find("Mirror").GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        var hitData = Physics2D.Raycast(startPoint, (direction - startPoint).normalized, defaultRayDistance);

        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData)
        {
            ReflectFurther(startPoint, hitData);
        }
        else
        {
            Points.Add(startPoint + (direction - startPoint).normalized * Infinity);
        }

        lr.positionCount = Points.Count;
        lr.SetPositions(Points.ToArray());
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;

        Points.Add(hitData.point);
        currentReflections++;
        if(hitData.collider != endPointCollider && hitData.transform.gameObject.layer != enemyLayer){
            Vector2 inDirection = (hitData.point - origin).normalized;
            Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

            var newHitData = Physics2D.Raycast(hitData.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance);
            if (newHitData)
            {
                ReflectFurther(hitData.point, newHitData);
            }
            else
            {
                Points.Add(hitData.point + newDirection * defaultRayDistance);
            }
        }
        else if(hitData.collider == endPointCollider){
            endPointLight.color = Color.green;
            endPointLightShadow.color = Color.green;
            GameObject.Find("LevelManager").GetComponent<LevelManager>().LevelUpProcess();
        }
        else if(hitData.transform.gameObject.layer == 8){
            Debug.Log("GAME OVER");
        }
    }
}
