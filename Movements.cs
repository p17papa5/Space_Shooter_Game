using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public static Movements instance;

    public float moveSpeed; // Spaceshit movement speed
    public Rigidbody2D theRigidBody; // reference to rigitbody2d (our player)
    public Transform bottomLeftLimit, topRightLimit;

    public Transform shotPoint;
    public GameObject shot;

    public float timeBetweenShots = .1f;
    private float shotCounter;

    private float normalSpeed; //normal speed
    public float boostSpeed; //boost speed (powerUP)
    public float boostLength; //posi wra na einai to boost
    private float boostCounter;

    public bool doubleShotActive; //doubleshot
    public float doubleShotOffset;

    public bool stopMovement; //na stamatisi na kinite

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        normalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMovement) //gia meta to end screen na min kinite
        {
            // SpaceShip Movement , Vector2= moving to X and Y axis.
            theRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.position.x, topRightLimit.position.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.position.y, topRightLimit.position.y));

            if (Input.GetButtonDown("Fire1"))
            {
                if (!doubleShotActive)
                {
                    Instantiate(shot, shotPoint.position, shotPoint.rotation); //gia na pirobola to spaceship euthia.
                }
                else
                {
                    Instantiate(shot, shotPoint.position + new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation); //na pirobola euthia me doubleshots (panw shot).
                    Instantiate(shot, shotPoint.position - new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation); //na pirobola euthia me doubleshots (katw shot).
                }


                shotCounter = timeBetweenShots;
            }

            if (Input.GetButton("Fire1"))
            {
                shotCounter -= Time.deltaTime; // deley sta shots
                if (shotCounter <= 0)
                {
                    if (!doubleShotActive)
                    {
                        Instantiate(shot, shotPoint.position, shotPoint.rotation); //gia na pirobola to spaceship euthia.
                    }
                    else
                    {
                        Instantiate(shot, shotPoint.position + new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation); //na pirobola euthia me doubleshots (panw shot).
                        Instantiate(shot, shotPoint.position - new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation); //na pirobola euthia me doubleshots (katw shot).
                    }
                    shotCounter = timeBetweenShots; //test
                }
            }
            if (boostCounter > 0) //an to boostCounter einai megalitero tou 0
            {
                boostCounter -= Time.deltaTime; //3ekina to boost
                if (boostCounter <= 0) //an einai mikrotero t 0
                {
                    moveSpeed = normalSpeed; // bale kanoniki taxitita
                }
            }
        }
        else
        {
            theRigidBody.velocity = Vector2.zero; //rithmizoume tin taxitita na einai 0
        }
    }

    public void ActivateSpeedBoost() //speed boost
    {
        boostCounter = boostLength;
        moveSpeed = boostSpeed;
    }
}
