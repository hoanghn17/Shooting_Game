using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeFire()
    {
        SceneManager.LoadScene("Fire");
    }
}
