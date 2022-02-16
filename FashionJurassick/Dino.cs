using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New_Dino", menuName = "Dino/Profile")]
public class Dino : ScriptableObject
{


    new public string name = "Dino";
    public Sprite face = null;
    public float relax;
    public float sexy;
    public float classe; 
    public float cute;
    public float rich;
    public float score;

    public YarnProgram scriptToLoad; 


    public virtual void Compare(Item item)
    {
        score = score + (relax * item.relax);
        score = score + (sexy * item.sexy);
        score = score + (classe * item.classe);
        score = score + (cute * item.cute);
        score = score + (rich * item.rich); 
    }

    public virtual void ResetScore()
    {
        score = 0;
    }

}
