using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Vector2 startDirection;

    public bool shouldChngeDirection;
    public float changeDirectionXPoint;
    public Vector2 changeDirection;

    public GameObject shotToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    public bool canShoot;
    private bool allowShooting;

    public int currentHealth;
    public GameObject deatheffect;
    public GameObject enemymoredeatheffect;


    public int scoreValue = 100; //kathe enemy dinei 100 points

    public GameObject[] powerUps; //Gia na petaei powerups
    public int dropSuccessRate = 75; //droprate

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = timeBetweenShots; //shots deley
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        if (!shouldChngeDirection)  //enemy movment
        {


            transform.position += new Vector3(startDirection.x * moveSpeed * Time.deltaTime, startDirection.y * moveSpeed * Time.deltaTime, 0f);
        } else
        {
            if (transform.position.x > changeDirectionXPoint) // an paei sto X simio na alla3ei kateuthinsi
            {
                transform.position += new Vector3(startDirection.x * moveSpeed * Time.deltaTime, startDirection.y * moveSpeed * Time.deltaTime, 0f); //euthia
            } else // alios na tin allaksi
            {
                transform.position += new Vector3(changeDirection.x * moveSpeed * Time.deltaTime, changeDirection.y * moveSpeed * Time.deltaTime, 0f);
            }
        }

        if (allowShooting) //an to afinei na pirobolaei
        {
            shotCounter -= Time.deltaTime; //enemy shots
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots; //shots Deley
                Instantiate(shotToFire, firePoint.position, firePoint.rotation);
            }
        }

    }

    public void HurtEnemy() //Enemy Health gia na pethanei
    {
        currentHealth--;
        if(currentHealth <=0)
        {
            GameManager.instance.AddScore(scoreValue); // score

            int randomChance = Random.Range(10, 100);
            if (randomChance < dropSuccessRate)
            {
                int randomPick = Random.Range(0, powerUps.Length - 1); //na diale3ei ena PowerUp apo auta p exoume balei,[BUGFIX] -1 giati einai 3 kai epd 3ekina apo to 0 blepoume mono 2
                Instantiate(powerUps[randomPick], transform.position, transform.rotation);
            }

            Destroy(gameObject);
            Instantiate(deatheffect, transform.position, transform.rotation); //death effect
            Instantiate(enemymoredeatheffect, transform.position, transform.rotation); //death particle system

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnBecameVisible()  //na pirovola afou mpei mesa stin othoni gia na mporei na to dei k o xristis
    {
        if(canShoot)
        {
            allowShooting = true;
        }
    }
}
