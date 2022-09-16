using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class GameOverUI : MonoBehaviour
{
    public Button Restart;
    public Button Exit;
    public Health PlayerHealth;
    public Animator Animator;
    public SceneLoader Loader;

    private void Start() {
        PlayerHealth.OnDied += this.OnDied;

        Restart.interactable = false;
        Exit.interactable = false; 
    }

    private void OnEnableButtons() {
        Restart.interactable = true;
        Exit.interactable = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Restart.gameObject);
    }

    private void OnDied() {
        Animator.SetTrigger("gameover");
    }
}
