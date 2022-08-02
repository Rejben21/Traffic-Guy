using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject car;
    public GameObject driver;
    public float carSpeed;
    public float turnSpeed;
    private Vector2 targetPos;
    public float timeToMove;
    private float moveTime;

    public int health;
    public Animator playerAnim;
    public bool canHit = true;

    public GameObject[] spawners;
    public GameObject[] obstacles;
    public float startTimeBtwSpawn;
    private float timeBtwSpawn;

    public GameObject road;
    public float roadSpeed;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = car.transform.position;
        playerAnim = car.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();

        RoadController();

        SpawnObjects();
    }

    void RoadController()
    {
        road.transform.Translate(Vector2.left * roadSpeed * Time.deltaTime);
        if (road.transform.position.x <= -20)
        {
            road.transform.position = new Vector3(20, road.transform.position.y, road.transform.position.z);
        }
    }

    void PlayerHealth()
    {

    }

    void PlayerController()
    {
        car.transform.position = Vector2.MoveTowards(car.transform.position, targetPos, turnSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow) && car.transform.position.y <= -1f && moveTime <= 0)
        {
            targetPos = new Vector2(car.transform.position.x, car.transform.position.y + 1f);
            moveTime = timeToMove;
            car.gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 3;
            driver.gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 3;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && car.transform.position.y >= -4f && moveTime <= 0)
        {
            targetPos = new Vector2(car.transform.position.x, car.transform.position.y - 1f);
            moveTime = timeToMove;
            car.gameObject.GetComponent<SpriteRenderer>().sortingOrder += 3;
            driver.gameObject.GetComponent<SpriteRenderer>().sortingOrder += 3;
        }

        if (moveTime > 0)
        {
            moveTime -= Time.deltaTime;
        }
    }

    void RoadsMoveing()
    {
        
    }

    public void PlayButton()
    {
        Debug.Log("Starting Game");
    }

    public  void OptionsButton()
    {
        Debug.Log("Options");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    void SpawnObjects()
    {
        if(timeBtwSpawn <= 0)
        {
            Instantiate(obstacles[Random.Range(0, 38)], spawners[Random.Range(0, 3)].transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
