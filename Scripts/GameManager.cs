using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region Variables

    //General
    public static GameManager Instance;

    private float score;

    public InputActionReference restartButton;

    //Player 1
    public GameObject player1Go;
    private PlayerMovement player1Script;

    private Vector3 ogPositionPlayer1;


    //Player 2
    public GameObject player2Go;
    private PlayerMovement player2Script;

    private Vector3 ogPositionPlayer2;

    //Ball
    public GameObject ballGo;
    private Rigidbody ballRb;
    private BallMovement ballScript;

    private Vector3 ogPositionBall;

    //UI Canvas
    public TextMeshProUGUI scoreText;
    [HideInInspector] public int P1Score = 0;
    [HideInInspector] public int P2Score = 0;

    #endregion

    private void OnEnable()
    {
        restartButton.action.Enable();
        restartButton.action.started += ResetGame;
    }

    private void OnDisable()
    {
        restartButton!.action.Disable();
        restartButton.action.started -= ResetGame;
    }

    private void Update()
    {
        scoreText.text = $"{P1Score} : {P2Score}";
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        } 
        Instance = this;

        player1Script = player1Go.GetComponent<PlayerMovement>();
        player2Script = player2Go.GetComponent<PlayerMovement>();
        ballScript = ballGo.GetComponent<BallMovement>();

        ogPositionPlayer1 = player1Go.transform.position;
        ogPositionPlayer2 = player2Go.transform.position;
        ogPositionBall = ballGo.transform.position;

        ballRb = ballGo.GetComponent<Rigidbody>();
    }


    public void ResetRound()
    {
        //Reset ball velocity and position
        ballGo.transform.position = ogPositionBall;
        ballRb.angularVelocity = Vector3.zero;
        ballRb.linearVelocity = Vector3.zero;

        //Reset players positions
        player1Go.transform.position = ogPositionPlayer1;
        player2Go.transform.position = ogPositionPlayer2;
    }

    void ResetGame(InputAction.CallbackContext ctx)
    {
        ResetRound(); 
        P1Score = 0;
        P2Score = 0;    
    }
}
