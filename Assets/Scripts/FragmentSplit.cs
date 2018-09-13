using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentSplit : MonoBehaviour {

    private bool broken = false;
    
    
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!broken)
        {
            Checking();
        }
        
        else { StartCoroutine(Wait()); }
        //здесь код, который должен выполняться ДО ожидания

        //здесь код, который должен выполняться ВО ВРЕМЯ ожидания
        
    }

     public IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);// Ждем 25 секунд. Конечно, можно в Wait() передать параметр - сколько времени ждать, а не хардкодить. 25 секунд написал для примера.
        //DestroyComponentCollider();
        //DestroyComponentRigidbody();
        DestroyObjectTime();
      

    }

    void Checking()
    {

        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.forward * .35f);

        if (Physics.Raycast(transform.position, -transform.forward, out hit))
        {
            if (hit.rigidbody && hit.rigidbody.isKinematic != true)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                broken = true;

                

            }
            
        }
       

    }

    Color _color;
    float step = 1;
    
    void DestroyObjectTime()
    {

        // var renderer = this.gameObject.GetComponent<Renderer>();
        //var renderer = gameObject.GetComponentInChildren<Renderer>(); // если родитель не имеет Renderer, то можно искать первый дочерний объект с этим компонентом
        //renderer.material.shader = Shader.Find("Transparent/Diffuse");
        //  renderer.material.color = Color.white * 0.25f; // белый с 25% не прозрачности 0.25f
        //Destroy(this.gameObject, 5);

       
        var renderer = this.gameObject.GetComponent<Renderer>();
         _color = renderer.material.color;
        // уменьшаем прозрачность
        if (_color.a > 0.05f) {
            _color.a = _color.a - step / 1000;
            renderer.material.color = _color;
        }
        else {
            //   this.gameObject.SetActive(false); //Скрывать объект
            Destroy(this.gameObject); //  прозрачным стало Destroy(this.gameObject, 5);
        }
    }


    void DestroyComponentRigidbody()
    {
        // Удаляет твердое тело из игрового объекта
        Destroy(this.GetComponent<Rigidbody>());
    }

    void DestroyComponentCollider()
    {
        // Удаляет коллайдер тело из игрового объекта
        Destroy(this.GetComponent<MeshCollider>());
    }



    void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude < 1.5f)
            return;

        if (collision.transform.gameObject.layer != LayerMask.NameToLayer("Fragment"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
       
        //DestroyObjectTime();
    }

   
}
