using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DreamcatcherManager : MonoBehaviour
{
    public int dreamcatcherCount = 0;
    public int totalDreamcatcherCount = 5;
    public Text dreamcatcherCountText;
    public string sceneToLoad;
    void Start()
    {

    }

    void Update()
    {
        dreamcatcherCountText.text = "Count: " + dreamcatcherCount.ToString() + "/" + totalDreamcatcherCount.ToString();
        if (dreamcatcherCount == totalDreamcatcherCount)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
