using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    static float t = 0.0f;
    public float distance, speed;
    private float originalPos;
    bool FacingRight = false;

    void Start()
    {
        originalPos = transform.position.x;

    }

    void Update()
    {
        transform.position = new Vector3(originalPos + Mathf.Sin(t) * distance,transform.position.y,transform.position.z);
        t += speed * Time.deltaTime;

        if (Mathf.Sin(t) < 0 && !FacingRight)
        {
            Flip();
        }
        else if (Mathf.Sin(t) > 0 && FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
	{
        FacingRight = !FacingRight;

		transform.Rotate(0, 180, 0);
	}
}