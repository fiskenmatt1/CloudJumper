using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    public void replay() 
    {
        Application.LoadLevel(Application.loadedLevel); //TODO: obselete
    }
}
