using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ShowAppVersion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       this.gameObject.GetComponent<TMP_Text>().text = "Dragon Court Tactics\n"+Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
