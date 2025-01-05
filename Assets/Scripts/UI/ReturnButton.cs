
using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    
    public void Return() {
        Destroy(transform.parent.gameObject);
    }


}
