using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineTank : MonoBehaviour
{

    //Скорость движения танка
    float MoveSpeed = 1.5f;

    //Скорость поворота
    float RotateSpeed = 20;

    //Текущая скорость
    float CurrentSpeed = 0;

    //скорость танка
    int SpeedNum = 0;

    Rigidbody TankEngine;

    public GameObject Bullet;
    public GameObject StartStwol;
    public int speedBullet = 10000;


    void OnGUI()
    {
        
       
        GUI.Label(new Rect(new Vector2(10, 10), new Vector2(100, 20)), " передача : " + SpeedNum);
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //   SpeedNum = 0;
    //}


        void Fire()
    {
        //Fire1
        if(Input.GetButtonUp("Fire1"))
        {
            //Получаем текущую координату для создания снаряда
            Vector3 SpawnPoint = StartStwol.transform.position;
            //Какой угол поворота для старта снаряда
            Quaternion SpawnRoot = StartStwol.transform.rotation;
            //Создаем снаряд
            GameObject Pula_for_fear = Instantiate(Bullet, SpawnPoint, SpawnRoot) as GameObject;
            //Получаем копонент
            Rigidbody Run = Pula_for_fear.GetComponent<Rigidbody>();
            //Ускорение пули
            Run.AddForce(Pula_for_fear.transform.forward * speedBullet, ForceMode.Impulse);
            Destroy(Pula_for_fear, 15);

        }
    }

    void Move()
    {

        //Расчитываем куда будет двигаться танк
        Vector3 Move = transform.forward * CurrentSpeed * MoveSpeed * Time.deltaTime;
        //Получим текущую позицию танка
        Vector3 Poze = TankEngine.position + Move;
        //Двигаемся в указаном направлении
        TankEngine.MovePosition(Poze);

    }

    void Rotate()
    {
        //расчитываем поворот
        float R = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;
        //Создадим Новый угол поворота танка
        Quaternion RotateAngle = Quaternion.Euler(0, R, 0);
        //Получим текущий угол поворота танка
        Quaternion CurrentUgol = TankEngine.rotation * RotateAngle;
        //Поворачиваем танк
        TankEngine.MoveRotation(CurrentUgol);


    }

    void Start()
    {
        //Получим компонент Движения танка
        TankEngine = GetComponent<Rigidbody>();


    }

    public GameObject Tower;
   // public GameObject Gun;
    void RotateTower() //Поворот башни
    {
        //RotateSpeed
        float AngleRotate = Time.deltaTime * RotateSpeed * Input.GetAxis("MoveTower");
       // AngleRotate = Time.deltaTime * RotateSpeed * Input.GetAxis("Mouse X");
        //Поворот башни кнопками
        Tower.transform.Rotate(0,0,AngleRotate);

    }
   // void RotateGun()
    ///{
    //    float AngleRotateY = Time.deltaTime * RotateSpeed * Input.GetAxis("MoveGun");
        //AngleRotateY = Time.deltaTime * RotateSpeed * Input.GetAxis("Mouse Y");
        //Поворот орудия кнопками
      //  Gun.transform.Rotate(AngleRotateY, 0, 0 );
    //}

    void FixedUpdate()
    {
        Fire();
        Transmission();
        //Двигаем и поворачиваем
        Move();
        //Rotate();
        //поворот башни
       // RotateTower();
        //поворот орудия
      //  RotateGun();
    }



    void Update()
    {
        //запоминаем что нгажал юзер
        float Axis = Input.GetAxis("Vertical");
        //уменьшение или увиличение скорости
        if (Input.GetButtonUp("Vertical"))
        {
            if (Axis > 0) UpSpeed();
            if (Axis < 0) DownSpeed();

        }

    }

    void UpSpeed() //увеличение скорости
    {
        //если скорость не выше3 то будем ускорять танк
        if ((SpeedNum + 1) < 4) SpeedNum++;
    }

    void DownSpeed() //уменьшение скорости
    {
        if ((SpeedNum - 1) > -2) SpeedNum--;
    }



    void Transmission()
    {
        switch (SpeedNum)
        {
            case -1: CurrentSpeed = -1; RotateSpeed = 25; break; //Задний ход
            case 0: CurrentSpeed = 0; RotateSpeed = 20; break; //Parking
            case 1: CurrentSpeed = 0.5f; RotateSpeed = 25; break; //1 скорость
            case 2: CurrentSpeed = 1.5f; RotateSpeed = 35; break; //2 скорость
            case 3: CurrentSpeed = 9.0f; RotateSpeed = 180; break; //3 скорость
        }
    }
}
