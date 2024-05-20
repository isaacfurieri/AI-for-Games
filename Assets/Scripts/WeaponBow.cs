using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float fireForce = 20f;
    public Vector3 ArrowGenerationPoint;

    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        // IF MOUSE CLICK
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            Vector2 lookDir = mousePos - transform.position;
            Debug.Log("mouse pos: " +mousePos);
            Debug.Log("look dir: " +lookDir);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            Debug.Log("Angle: " + angle);
            Attack(angle, lookDir);
        }
        
    }

    public void Attack(float angle, Vector2 lookDir)
    {
        if (lookDir.x > 0)
        {
            ArrowGenerationPoint.x = transform.position.x + 0.49f;
        }
        else
        {
            ArrowGenerationPoint.x = transform.position.x - 0.49f;
        }
        if (lookDir.y > 0)
        {
            ArrowGenerationPoint.y = transform.position.y + 0.39f;
        }
        else
        {
            ArrowGenerationPoint.y = transform.position.y - 0.39f;
        }
            Debug.Log("Arrow generation point: " +ArrowGenerationPoint);
        
        //ArrowGenerationPoint.position = new Vector3(0.1f * Mathf.Cos(angle), 0.1f * Mathf.Sin(angle), 0);
        GameObject bullet = Instantiate(arrowPrefab, ArrowGenerationPoint, Quaternion.Euler(0, 0, angle));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(lookDir.normalized * fireForce, ForceMode2D.Impulse);
    }
}
