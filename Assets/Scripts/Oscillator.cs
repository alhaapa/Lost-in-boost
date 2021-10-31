using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField]Vector3 movementVector;
    [SerializeField] [Range (0,1)]float movementFactor;
    [SerializeField] float period = 2f;
    Material oldMaterial;

    Renderer Rend;
    [SerializeField] Material newMaterial;
    [SerializeField] float bound = 1f;

    float currentZ;

    [SerializeField]bool changeMaterial;
    bool materialChanged = false;



    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

        Rend = GetComponent<Renderer>();
        oldMaterial = Rend.material;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (period <= Mathf.Epsilon) {return;} //eliminate NaN error
        
        float cycles = Time.time / period; //continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283 or 2 PI
        float rawSinWave = Mathf.Sin(cycles * tau); //goes from -1 to 1

        movementFactor = (rawSinWave +1f) / 2f; //recalculated into  0 to 1


        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

        currentZ = transform.position.z;
        //Debug.Log(currentZ); This code seems to work and print out Z position

        if(changeMaterial)
        {
            if(isBetweenBounds(currentZ, -bound, bound) )
            {
                if(materialChanged == false)
                {
                    ChangeMaterial(newMaterial);
                    materialChanged = true;
                }
            }

            else
            {
                if (materialChanged == true)
                {
                    ChangeMaterial(oldMaterial);
                    materialChanged = false;
                }

            }

        }





    }


    bool isBetweenBounds(float number, float bound1, float bound2)
    {
        
        if(number>= bound1 && number <=bound2)
        {
            return true;
        }

        else    
        {
            return false;
        }

        
     

    }


    void ChangeMaterial(Material newMat)
    {
        Rend.material = newMat;
        //Rend.sharedMaterial = newMat;
        //Debug.Log("Material changed");
    }
    
}
