using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]


public class TopDownMovement : MonoBehaviour
{
    private float playerSpeed = 5f;
    
    // To set gravity and fall 
    private float gravityValue = -9.81f;
    
    // To fetch game data from GlobalGameSate
    private GlobalGameState gameState;

    private CharacterController characterController;
    private PlayerControls playerControls;
    private PlayerInput playerInput;

    // This is used to display current score on
    // top of player
    private TextMeshPro scoreText;

    // For movement and velocity
    private Vector2 movement;
    private Vector3 playerVelocity;

    // To display the game overscreen when the 
    // scripts detects that player fell down
    public GameOverScreen gameOverScreen;

    private void Awake()
    {
        // Intializing the fields
        characterController = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        gameState = GameObject.FindWithTag("GlobalGameState").GetComponent<GlobalGameState>();
        scoreText = GetComponentInChildren<TextMeshPro>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with the score volume
        // then increment the score in the GlobalGameState
        // and destroy the score volume so that is not used
        // again. Then update the Score on the head.
        if (other.gameObject.tag.Equals("ScoreVolume"))
        {
            Destroy(other.gameObject);
            gameState.score++;
            scoreText.text = "Score: " + (gameState.score - 1).ToString();
        }

    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = gameState.game_data.player_data.speed;
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();

        // Get current position of object
        // If below a certain level then death 
        // occurs and GamePverScreen is shown.
        float y = gameObject.transform.position.y;
        if(y < -10)
        {
            // Called the setup function to
            // make the game over screen visible and 
            // passsed the final score
            gameOverScreen.Setup(gameState.score);
        }
    }
    void HandleInput()
    {
        // To capture the input and read movement
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
    }
    void HandleMovement()
    {
        // To move the cube in the direction
        // accroding to the input handeled by the 
        // HanldeInput() function
        
        // For WASD movement
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        characterController.Move(move * Time.deltaTime * playerSpeed);
        
        // For Gravity movement
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
