using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject dirNode;
    public float speed;
    private Rigidbody2D rb;
    private Vector3 startDir;
    private Vector3 currentDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();;
        startDir = dirNode.transform.position.normalized;
        currentDir = startDir;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        rb.velocity = currentDir * speed * Time.fixedDeltaTime;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        currentDir = changeDir(currentDir);
        Debug.Log("Collided");
	}

    private Vector3 changeDir(Vector3 curDir)
	{
        Vector3 finalDir = Vector3.zero;
       /*if(rb.velocity.x <= 0)
		{
            finalDir = Vector3.Cross(Vector3.forward, curDir);
        }
		else { 
            finalDir = Vector3.Cross(curDir, Vector3.forward);      
        }*/
        finalDir = Vector3.Cross(curDir, Vector3.forward);
        if(curDir.x < 0 && curDir.y > 0)
		{
            finalDir = Vector3.Cross(Vector3.forward, curDir);
        }
        return finalDir;
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;
        Ray ray = new Ray(transform.position, currentDir);
        Gizmos.DrawRay(ray);
	}
}
