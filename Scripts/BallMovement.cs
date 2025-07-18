using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovement : MonoBehaviour
{
    //GameManager
    public GameObject GameManagerGo;
    private GameManager gameManagerScript;

    public InputActionReference launchButton;

    //Ball general
    public GameObject ballGo;
    private Rigidbody ballRb;


    //Ball velocity
    private float angleVelocity = 10f;
    private List<Vector3> randomAnglesList;



    private void Start()
    {
        gameManagerScript = GameManagerGo.GetComponent<GameManager>();

        ballRb = GetComponent<Rigidbody>();

        //Lancement aléatoire de la boule
        randomAnglesList = new List<Vector3>
        {
        new Vector3(angleVelocity, angleVelocity, 0),
        new Vector3(- angleVelocity, angleVelocity, 0),
        new Vector3(angleVelocity, - angleVelocity, 0),
        new Vector3(- angleVelocity, - angleVelocity, 0),
        };

        GiveRandomImpulse();
    }

    private void OnEnable()
    {
        launchButton.action.Enable();
        launchButton.action.started += ctx => GiveRandomImpulse();
    }

    private void OnDisable()
    {
        launchButton.action.Disable();
        launchButton.action.started -= ctx => GiveRandomImpulse();
    }

    public void GiveRandomImpulse()
    {
        Vector3 randomVelocity = randomAnglesList[Random.Range(0, randomAnglesList.Count)];
        ballRb.linearVelocity = randomVelocity;
    }

    /// <summary>
    /// Permet de changer la direction de la boule. 
    /// Prend un vecteur avec des 1 et -1 pour changer de direction.
    /// </summary>
    /// <param name="velocityChange"></param>
    void GiveImpulse(Vector3 velocityChange)
    {
        ballRb.linearVelocity = new Vector3(ballRb.linearVelocity.x * velocityChange.x, ballRb.linearVelocity.y * velocityChange.y, ballRb.linearVelocity.z * velocityChange.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            GiveImpulse(new Vector3(1, -1, 1));
        }

        if (other.gameObject.CompareTag("Player"))
        {
            GiveImpulse(new Vector3(-1, 1, 1));
        }

        if (other.gameObject.CompareTag("P1Goal"))
        {
            GiveScoreToP2();
        }

        if (other.gameObject.CompareTag("P2Goal"))
        {
            GiveScoreToP1();
        }
    }

    void GiveScoreToP1()
    {
        gameManagerScript.P1Score += 1;
        gameManagerScript.ResetRound();
    }

    void GiveScoreToP2()
    {
        gameManagerScript.P2Score += 1;
        gameManagerScript.ResetRound();
    }

}
