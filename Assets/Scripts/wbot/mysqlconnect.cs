using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mysqlconnect : MonoBehaviour {

    public string url = "http://anomal3.16mb.com/user/display2.php";
    public string savemoney = "http://anomal3.16mb.com/user/savemoney.php";//
    public string pass;
    public string user;
    public static string name = "";
    private string rePass = "", message = "";
    public string money;
    public int mon;
    private string secretKey = "mySecretKey";
    public bool doWindowlog = true;
    public bool doWindowreg = false;



    void Start()
    {
    }
    void DoWindowlog(int windowID)
    {
        if (message != "")
            GUILayout.Box(message);
        GUILayout.Label("Логин:");
        user = GUILayout.TextField(user);
        GUILayout.Label("Пароль:");
        pass = GUILayout.PasswordField(pass, "*"[0]);


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Войти"))
        {
            message = "";

            if (user == "" || pass == "")
                message += "Введите верные данные \n";
            else
            {
                WWWForm form = new WWWForm();
                form.AddField("user", user);
                form.AddField("pass", pass);
                form.AddField("money", money);
                WWW w = new WWW("http://anomal3.16mb.com/user/display.php", form);
                StartCoroutine(login(w));
                StartCoroutine(Wait());
            }
        }

        if (GUILayout.Button("Регистрация"))
        {
            doWindowreg = true;
            doWindowlog = false;
        }
        GUILayout.EndHorizontal();
        //GUILayout.EndHorizontal();
    }





    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        if (pass != "" && user != "")
        {
            var form = new WWWForm();
            form.AddField("pass", pass);
            form.AddField("user", user);
            //ОТПРАВЛЯЕМ ЗАПРОС
            var w = new WWW(url, form);
            yield return w;
            ///ПОЛУЧАЕМ и разбиваем ОТВЕТ
            string otvet = w.text;
            string[] userinfo = otvet.Split(new string[] { ";" },
            StringSplitOptions.None);
            money = userinfo[0];
            if (int.TryParse(money, out mon)) // or float.TryParse, or double.TryParse etc
            {
                Debug.Log("The value is " + mon);
            }
            else
            {
                Debug.Log("Not a valid integer");
            }
        }
        else
        {
            print("Поля:Логин или Пароль - пустые");
        }
    }

    void DoWindowreg(int windowID)
    {
        if (message != "")
            GUILayout.Box(message);
        GUILayout.Label("Логин");
        user = GUILayout.TextField(user);
        GUILayout.Label("Как вас зовут?");
        name = GUILayout.TextField(name);
        GUILayout.Label("Пароль");
        pass = GUILayout.PasswordField(pass, "*"[0]);
        GUILayout.Label("Повторите пароль");
        rePass = GUILayout.PasswordField(rePass, "*"[0]);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Назад"))
        {
            doWindowreg = false;
            doWindowlog = true;
        }
        if (GUILayout.Button("Создать"))
        {
            message = "";

            if (user == "" || name == "" || pass == "")
                message += "Логин уже занят! \n";
            else
            {
                if (pass == rePass)
                {
                    WWWForm form = new WWWForm();
                    form.AddField("user", user);
                    form.AddField("name", name);
                    form.AddField("pass", pass);
                    form.AddField("money", money);
                    WWW w = new WWW("http://anomal3.16mb.com/user/register.php", form);
                    StartCoroutine(registerFunc(w));
                }
                else
                    message += "Неправильный повторный пароль \n";
            }
        }

        GUILayout.EndHorizontal();
    }

    void Update()
    {
    }
    void OnGUI()
    {
        if (doWindowlog)
            GUI.Window(1, new Rect(320, 110, 300, 400), DoWindowlog, "Логин");
        if (doWindowreg)
            GUI.Window(0, new Rect(320, 110, 300, 400), DoWindowreg, "Регистрация");
        //Поля для ввода данных сделай сам!
        GUI.Label(new Rect(100, 100, 100, 100), "Деньги:" + mon);
        if (GUI.Button(new Rect(0, 0, 150, 150), "Отправить"))
        {

            mon -= 1;
            StartCoroutine(Wait2());
        }
    }


    IEnumerator registerFunc(WWW w)
    {
        yield return w;
        if (w.error == null)
        {
            message += w.text;
        }
        else
        {
            message += "ERROR: " + w.error + "\n";
        }
    }


    IEnumerator login(WWW w)
    {
        yield return w;
        if (w.error == null)
        {
            if (w.text == "ok")
            {
                print("WOOOOOOOOOOOOOOO!");
                doWindowlog = false;
                //doWindowhead = true;
                //StartCoroutine(GetScores());
            }
            else
                message += w.text;

        }
        else
        {
            message += "ERROR: " + w.error + "\n";
        }
    }




    public IEnumerator Wait2()
    {
        ////данные для запроса в DataBase
        yield return new WaitForSeconds(1);
        if (pass != "" && user != "")
        {
            var form = new WWWForm();
            form.AddField("pass", pass);
            form.AddField("user", user);
            form.AddField("mon", mon);
            //ОТПРАВЛЯЕМ ЗАПРОС
            var w = new WWW("http://anomal3.16mb.com/user/savemoney.php", form);
            yield return w;
            ///ПОЛУЧАЕМ и разбиваем ОТВЕТ
            string otvet = w.text;
            string[] userinfo = otvet.Split(new string[] { ";" },
            StringSplitOptions.None);
            money = userinfo[0];
        }
    }
}