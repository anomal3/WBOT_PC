using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pause_script : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;
   

    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;

        }
    }

    public void OnGUI()
    {
        if (guipuse == true)
        {
            Cursor.visible = true;// включаем отображение курсора




            /*if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 250f, 45f), "Продолжить"))
            {
                ispuse = false;
                timer = 0;
                Cursor.visible = false;
            }*/


            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 250f, 45f), "Начать заного"))
            {
                ispuse = false;
                timer = 0;
                SceneManager.LoadScene("Apocalipt");
                Cursor.visible = false;// вbIключаем отображение курсора
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 250f, 45f), "Выйти из игры"))
            {
                Application.Quit();
            }
            /* if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 200f, 150f, 45f), "Выйти из игры"))
             {
                 Application.Quit();
             }*/

        }
    }
}
