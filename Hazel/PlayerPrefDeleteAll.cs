using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefDeleteAll : MonoBehaviour
{
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
