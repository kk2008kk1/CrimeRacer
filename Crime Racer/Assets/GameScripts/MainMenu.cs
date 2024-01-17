using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Privates

    //Publics
    public GameObject StartUI;
    public GameObject GuideUI;
    public GameObject[] OtherUI;
    public Animator MainMenuAnimator;
    public Animator GuideAnimator;
    public float DelayForChangeM;
    public float DelayForChangeG;
    public float DelayForChangeP;
    public GameObject Loading;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Loading.SetActive(false);
    }
    void Update()
    {
        
    }
    public void Anim()
    {
        MainMenuAnimator.SetTrigger("StartAnim");
    }
    public void Play()
    {
        MainMenuAnimator.SetTrigger("Hide");
        StartCoroutine(Load());
        

    }
    public void PlayLite()
    {
        
        Loading.SetActive(true);
        SceneManager.LoadScene(1);

    }
    public void Options()
    {
        MainMenuAnimator.SetTrigger("Hide");
        StartCoroutine(DelayO());
        SceneManager.LoadScene(2);
    }
    public void Guide()
    {
        MainMenuAnimator.SetTrigger("Hide");
        StartCoroutine(DelayM());
        
        
    }
    public void Back()
    {
        GuideAnimator.SetTrigger("FadeOut");
        StartCoroutine(DelayG());
        

    }

    public void Exit()
    {
        Application.Quit();
    }
    public void BackOnMainMenu()
    {
        SceneManager.LoadScene(0);

    }
   

    IEnumerator DelayM()
    {
        yield return new WaitForSeconds(DelayForChangeM);
        StartUI.SetActive(false);
            GuideUI.SetActive(true);
        GuideAnimator.SetTrigger("FadeIn");
    }

    IEnumerator DelayG()
    {
        yield return new WaitForSeconds(DelayForChangeG);
        SceneManager.LoadScene(0);
    }
    IEnumerator DelayO()
    {
        yield return new WaitForSeconds(DelayForChangeM);
        SceneManager.LoadScene(2);
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(DelayForChangeM);
        Loading.SetActive(true);
SceneManager.LoadScene(1);
    }

    

}
