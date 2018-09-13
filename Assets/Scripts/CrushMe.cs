using UnityEngine;
using System.Collections;

public class CrushMe : MonoBehaviour
{

    //Друзья, этот скрипт с сайта Devgam.com
    //Не используйте скрипт в коммерческих целях
    //Хотя бы переменные переименуйте, пробелы расставьте
    //Enter нажмите пару раз
    //И, конечно же, читайте новости на Девгаме(http://devgam.com)
    public Transform[] BrokenPrefabs;
    public AudioClip[] CollisionSounds;
    public AudioClip[] BreakingSounds;
    public bool ApplyCollisionForce;
    public float CollisionForce;
    public bool ApplyHp;
    public float MaximumHp;
    public float CurrentHp;
    public bool ApplyExplosionForce;
    public float ExplosionForce;
    public float ExplosionRadius;



    private bool broken;
    private float timer;
    private Transform BrokenObject;
    private Vector3 orgPos;
    private Quaternion orgRot;
    private bool spawned;
    private Vector3 orgVelocity;
    private bool kinematic;
    private Vector3 otherAddVelocity;
    private GameObject otherCol;

    void Start()
    {

        orgPos = transform.position;
        orgRot = transform.rotation;
        kinematic = transform.GetComponent<Rigidbody>().isKinematic;
        ResetObject();

        if (CollisionSounds.Length > 0 || BreakingSounds.Length > 0)
        {

            if (gameObject.GetComponent<AudioSource>() == null)
            {

                gameObject.AddComponent<AudioSource>();

            }

        }

    }

    void Update()
    {

        if (ApplyCollisionForce == true && broken == true || ApplyHp == true && CurrentHp <= 0)
        {

            if (spawned == false)
            {

                if (timer <= 0)
                {

                    orgVelocity = GetComponent<Rigidbody>().velocity;

                    if (transform.GetComponent<Collider>())
                    {

                        transform.GetComponent<Collider>().enabled = false;

                    }

                    if (transform.GetComponent<Rigidbody>())
                    {

                        transform.GetComponent<Rigidbody>().isKinematic = true;

                    }

                    if (gameObject.GetComponent<AudioSource>() != null && BreakingSounds.Length > 0)
                    {

                        gameObject.GetComponent<AudioSource>().clip = BreakingSounds[Random.Range(0, BreakingSounds.Length)];
                        gameObject.GetComponent<AudioSource>().Play();

                    }

                    if (BrokenPrefabs.Length > 1)
                    {

                        BrokenObject = (Transform)Instantiate(BrokenPrefabs[Random.Range(0, (BrokenPrefabs.Length - 1))], transform.position, transform.rotation);
                        spawned = true;

                    }
                    else
                    {

                        BrokenObject = (Transform)Instantiate(BrokenPrefabs[0], transform.position, transform.rotation);
                        spawned = true;

                    }

                    Rigidbody[] children;
                    children = BrokenObject.GetComponentsInChildren<Rigidbody>();

                    foreach (Rigidbody childRigidbody in children)
                    {
                        childRigidbody.velocity = orgVelocity;
                    }

                    if (transform.GetComponent<MeshRenderer>())
                    {

                        transform.GetComponent<MeshRenderer>().enabled = false;

                    }



                    if (ApplyExplosionForce == true)
                    {

                        Collider[] cols = Physics.OverlapSphere(transform.position, ExplosionRadius);

                        foreach (Collider hit in cols)
                        {

                            if (hit.GetComponent<Rigidbody>() != null)
                            {
                                hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);

                            }

                        }

                    }

                }
                else
                {

                    timer -= Time.deltaTime;

                }

            }

        }

    }

    void OnCollisionEnter(Collision collision)
    {

        Vector3 otherOrgVel;
        otherOrgVel = collision.relativeVelocity - gameObject.GetComponent<Rigidbody>().velocity;
        otherCol = collision.gameObject;

        if (gameObject.GetComponent<AudioSource>() != null && CollisionSounds.Length > 0)
        {

            gameObject.GetComponent<AudioSource>().clip = CollisionSounds[Random.Range(0, CollisionSounds.Length)];
            gameObject.GetComponent<AudioSource>().Play();

        }

        if (otherCol.GetComponent<Rigidbody>())
        {

            if (ApplyCollisionForce == true)
            {

                if (collision.relativeVelocity.magnitude * otherCol.GetComponent<Rigidbody>().mass >= CollisionForce)
                {

                    broken = true;

                    if (kinematic == true)
                    {

                        float kineticEnegyUsed = ((collision.relativeVelocity.magnitude * otherCol.GetComponent<Rigidbody>().mass - CollisionForce) / otherCol.GetComponent<Rigidbody>().mass) / otherOrgVel.magnitude;

                        otherAddVelocity = otherOrgVel * kineticEnegyUsed;

                    }

                }

            }
            else if (ApplyHp == true)
            {

                CurrentHp -= collision.relativeVelocity.magnitude * otherCol.GetComponent<Rigidbody>().mass;

            }

        }
        else
        {

            if (ApplyCollisionForce == true)
            {

                if (collision.relativeVelocity.magnitude * transform.GetComponent<Rigidbody>().mass >= CollisionForce)
                {

                    broken = true;

                }

            }
            else if (ApplyHp == true)
            {

                CurrentHp -= collision.relativeVelocity.magnitude * transform.GetComponent<Rigidbody>().mass;

            }

        }

    }

    public void ResetObject()
    {

        CurrentHp = MaximumHp;
        broken = false;
        spawned = false;

        if (BrokenObject != null)
        {

            Destroy(BrokenObject.gameObject);

        }

        BrokenObject = null;
        transform.position = orgPos;
        transform.rotation = orgRot;

        if (transform.GetComponent<Collider>())
        {

            transform.GetComponent<Collider>().enabled = true;

        }

        if (transform.GetComponent<Rigidbody>() && kinematic == false)
        {

            transform.GetComponent<Rigidbody>().isKinematic = false;

        }

        if (transform.GetComponent<MeshRenderer>())
        {

            transform.GetComponent<MeshRenderer>().enabled = true;

        }

    }

}