using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    //Vars
    #region variables
    //Publics
    public Transform SunTransform;
    public string CurentState;

    //Privates
    [SerializeField] private float RotationSpeed;
    private bool ChangeCycle = true;
    //Scripts

    #endregion
 

    // Update is called once per frame
    void Update()
    {
        SunTransform.Rotate(RotationSpeed * Time.deltaTime, 0, 0);
            if (ChangeCycle == true)
        {
            ChangeCycle = false;
            StartCoroutine(ChangeTheCycle());
        }
    }
    IEnumerator ChangeTheCycle()
    {
        yield return new WaitForSeconds(900);
        if (CurentState == "Day")
        {
            CurentState = "Night";
        }
        else
        {
            CurentState = "Day";
        }
        ChangeCycle= true;
    }
}
