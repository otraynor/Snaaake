using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
    
    public enum StateMachine
    {
        Play,
        Pause
    }

    private StateMachine _state;

    public StateMachine State
    {
        get => _state;

        set
        {
            _state = value;
        }
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        ResetGame();
    }
    
    private void Update()
    {
        // Toggle state between Play and Pause
        if (Input.GetKeyDown(KeyCode.Space)) // Example key to toggle state
        {
            ToggleState();
        }
    }

    public void ResetGame()
    {
        // Reset game state as needed
        State = StateMachine.Play;
    }

    private void ToggleState()
    {
        // Toggle between Play and Pause states
        State = State == StateMachine.Play ? StateMachine.Pause : StateMachine.Play;
    }
}