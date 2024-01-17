using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Privates


    //publics
    public GameObject GameUI;
    public GameObject MenuUI;
   // public GameObject OptionsUI;
    private bool Pause;
    //Scripts
    public PlayerMovement PM;

   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
  PM.PlayerFreeze = true;
            if (Pause == false)
            {                
                
                GameUI.SetActive(false);
                MenuUI.SetActive(true);
                Pause = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                
            }
            else
            {
                Resume();
                
            }
        }
       
        
      
    }
    public void Resume()
    {
        StartCoroutine(ResumeR());
        

    }
    IEnumerator ResumeR()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameUI.SetActive(true);
        MenuUI.SetActive(false);
        Pause = false;
        yield return new WaitForSeconds(0.3f);
        PM.PlayerFreeze = false;
    }

    public void MainMenu()
    {  
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
  //  public void Options()
  //  {
        
    //    MenuUI.SetActive(false);
    //     OptionsUI.SetActive(true); 
  //  }
  //  public void ExitOptions()
  //  {
   //     MenuUI.SetActive(true);
   //     OptionsUI.SetActive(false);
   // }
    public void Exit()
    {
Application.Quit();
    }
   

}
