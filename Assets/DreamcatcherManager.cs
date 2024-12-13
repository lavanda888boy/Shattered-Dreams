using UnityEngine;
using UnityEngine.UI;

public class DreamcatcherManager : MonoBehaviour
{
    public int dreamcatcherCount = 0;
    public Text dreamcatcherCountText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dreamcatcherCountText.text = "Count: " + dreamcatcherCount.ToString();
    }
}
