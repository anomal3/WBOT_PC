using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {

    public Transform Tower;
    public Transform Gun;
    public float TowerRotateSpeed = 20;
    public float GunRotateSpeed = 20;
    public float MinGunAngle = -45;
    public float MaxGunAngle = +45;

    private Camera camera;
    private int layerMask;
    private Vector3 target;
    private float gunX;

    void Start()
    {
        camera = Camera.main;
        layerMask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 p;
        if (Physics.Raycast(new Ray(camera.transform.position, camera.transform.forward), out hit, 500, layerMask))
            p = hit.point;
        else
            p = camera.transform.position + camera.transform.forward * 500;

        //rotate tower
        var target = p - Tower.position;
        target.y = 0;//calc projection on plane XZ
        var q = Quaternion.LookRotation(target, Tower.up);
        Tower.localRotation = Quaternion.RotateTowards(Tower.localRotation, q, TowerRotateSpeed * Time.deltaTime);

        //rotate gun
        target = Gun.position - p;
        target.y = 0;
        var angle = Mathf.Atan2((Gun.position - p).y, target.magnitude) * Mathf.Rad2Deg;//calc vert angle
        if (angle < MinGunAngle) angle = MinGunAngle;
        if (angle > MaxGunAngle) angle = MaxGunAngle;
        q = Quaternion.Euler(angle, 0, 0);
        Gun.localRotation = Quaternion.RotateTowards(Gun.localRotation, q, GunRotateSpeed * Time.deltaTime);
    }
}