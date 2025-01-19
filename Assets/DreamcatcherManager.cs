using UnityEngine;
using UnityEngine.UI;

public class DreamcatcherManager : MonoBehaviour
{
    public int dreamcatcherCount = 0;
    public int totalDreamcatcherCount = 5;
    public Text dreamcatcherCountText;
    void Start()
    {

    }

    void Update()
    {
        dreamcatcherCountText.text = "Count: " + dreamcatcherCount.ToString() + "/" + totalDreamcatcherCount.ToString();
    }
}
