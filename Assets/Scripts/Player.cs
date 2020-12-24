using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject deathModel;
    [SerializeField] UIController uIController;
#pragma warning restore 0649
    int currentGameCoins;


    public bool GameStarted { get { return uIController.GameStarted; } }
    public bool IsDead { get; set; }

    private void Awake()
    {
        uIController = uIController == null ? FindObjectOfType<UIController>() : uIController;
    }

    private void Start()
    {
        IsDead = false;
        transform.position = Vector3.up;
        currentGameCoins = PlayerPrefs.GetInt("Coins", -1);
        if (currentGameCoins == -1)
        {
            PlayerPrefs.SetInt("Coins", 0);
            currentGameCoins = 0;
        }
    }

    public static bool CanMove(Vector3 position, Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(position, direction, out hit, 1.0f))
        {
            if (hit.collider.gameObject.GetComponent<Obstacle>())
            {
                return !hit.collider.gameObject.GetComponent<Obstacle>().IsObstacle;
            }
        }
        return true;
    }
    public static bool Drowned(Vector3 position)
    {
        RaycastHit hit;

        if (Physics.Raycast(position, Vector3.down, out hit, 0.75f))
        {
            if (hit.collider.gameObject.GetComponent<Obstacle>())
            {
                return hit.collider.gameObject.GetComponent<Obstacle>().IsWater;
            }
        }
        return false;
    }

    /* Used for Debugging*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.0f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (transform.position + (Vector3.down * 0.7f)));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (transform.position + Vector3.forward));
        Gizmos.DrawLine(transform.position, (transform.position + Vector3.back));
        Gizmos.DrawLine(transform.position, (transform.position + Vector3.left));
        Gizmos.DrawLine(transform.position, (transform.position + Vector3.right));
    }
    /**/

    void OnTriggerEnter(Collider other)
    {
        // If hit a car or touched obstacle and that obstacle is water
        if (other.gameObject.GetComponent<VehicleMovement>() ||
           (other.gameObject.GetComponent<Obstacle>() &&
            other.gameObject.GetComponent<Obstacle>().IsWater))
        {
            if (!IsDead)
            {
                OnDeath();
            }
        }

        // Coin pickup
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            currentGameCoins++;
            uIController.UpdateCoins(currentGameCoins);
        }
    }

    public void OnDeath()
    {
        IsDead = true;
        //Death stuff
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        playerModel.SetActive(false);
        deathModel.SetActive(true);
        Invoke("OnLoss", 2.0f);
    }

    void OnLoss()
    {
        uIController.OnLoss();
    }

}


