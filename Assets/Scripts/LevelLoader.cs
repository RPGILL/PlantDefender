using UnityEngine;     // needed Unity basic classes like MonoBehavio
using UnityEngine.SceneManagement;    // needed this to change scenes load Start and  Gard

public class LevelLoader : MonoBehaviour    // This class handles moving between scenes leve
{
    public void PlayGame() => SceneManager.LoadScene("Garden");    // This function is called when we want to start the gam
    public void BackToMenu() => SceneManager.LoadScene("Start");   // It loads the Garden scen abd  function sends the player back to the main 
    public void QuitGame() => Application.Quit();     // This function closes the game applicatn
}
