using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public LayerMask floorWallMask;

    float floorCheckOffset = 0.4f;
    float wallCheckDistance = 0.1f;

    Rigidbody2D rigidbody;
    CircleCollider2D collider;

    Vector2 movement = Vector2.left;

    public GameObject target; //the enemy's target
    public bool moveRight = false;
    public float moveSpeed = 5; //move speed
    public float rayTrace;
    //public Transform groundDectection;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {/*
        //move();
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDectection.position, Vector2.down, rayTrace);
        RaycastHit2D WallInfo = Physics2D.Raycast(groundDectection.position, Vector2.left, rayTrace);
        //Floor Check
        if (groundInfo.collider == false)
        {
            if (moveRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        }
        //Wall Check
        if (WallInfo.collider == false)
        {
            if (moveRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        } */
    }
    private void FixedUpdate()
    {
        move();
        if (!CanMoveInDirection(movement))
        {
            movement *= -1;
        }
        rigidbody.position += movement * moveSpeed * Time.fixedDeltaTime;
    }
    private bool CanMoveInDirection(Vector2 direction)
    {
        RaycastHit2D hitWall = Physics2D.Raycast(
            transform.position,
            direction,
            collider.radius + wallCheckDistance,
            floorWallMask);
        RaycastHit2D hitFloor = Physics2D.Raycast(
            (Vector2)transform.position + direction * floorCheckOffset,
            Vector2.down,
            collider.radius * 2f,
            floorWallMask);
        return hitWall.collider == null && hitFloor.collider != null;
    }

    
    void move()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    } 
}
