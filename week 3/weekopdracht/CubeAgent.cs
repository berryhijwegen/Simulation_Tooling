using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAgent : MonoBehaviour
{
    public Vector3 nextPosition; 
    public int health_points = 100;
    public int damage = 1;
    public float speed = 1f;
    public float margin = 2.5f;
    public int viewRange = 10;
    public float NextPositionMargin = 10.0f;

    public Terrain terrain;

    public int getHealth(){
        return health_points;
    }
    public void setHealth(int health){
        health_points = health;
    }

    public LayerMask mask;
    void Start(){
        GetNextPosition();
    }

    void Update()
    {
        CheckHealth();

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit Hitinfo;

        if (Physics.Raycast(ray, out Hitinfo, mask, viewRange)){
            Debug.Log(Hitinfo.transform.name);
            CubeAgent otherAgentScript = Hitinfo.transform.GetComponent<CubeAgent>();
            Debug.Log(otherAgentScript);
            otherAgentScript.setHealth(otherAgentScript.health_points - damage);
            Debug.DrawLine(ray.origin, Hitinfo.point, Color.red);
        }
        else {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * viewRange, Color.green);
        }

        Transform temp = OtherAgentInRange();
        if (temp != null){
            transform.LookAt(new Vector3(temp.transform.position.x, 0, temp.transform.position.y));
            nextPosition = temp.transform.position;
        }
        if (AgentAtNextPosition()){
            GetNextPosition();
        }

        moveToNextPosition();
        Debug.Log(nextPosition);
    }

    void moveToNextPosition(){
        float step = speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, step); 
        transform.LookAt(new Vector3(nextPosition.x, 0, nextPosition.z));
    }

    bool AgentAtNextPosition(){
        return (Vector3.Distance(transform.position, nextPosition) - margin) <= 0;
    }

    void GetNextPosition(){
        nextPosition = transform.position + new Vector3(Random.Range(-NextPositionMargin, NextPositionMargin), 0, Random.Range(-NextPositionMargin, NextPositionMargin));
        while(nextPosition.x >= terrain.terrainData.size.x || nextPosition.x <= 0 || nextPosition.y >= terrain.terrainData.size.y || nextPosition.y <= 0){
            nextPosition = transform.position + new Vector3(Random.Range(-NextPositionMargin, NextPositionMargin), 0, Random.Range(-NextPositionMargin, NextPositionMargin));
        }
    }

    void CheckHealth(){
        if (getHealth() <= 0){
            Destroy(transform.gameObject);
        }
    }

    Transform OtherAgentInRange(){
        foreach (Transform eachChild in transform.parent.transform) {
            if (Vector3.Distance(transform.position, eachChild.transform.position) <= viewRange && !eachChild.Equals(transform)){
                return eachChild;
            }
        }
        return null; 
    }
}
