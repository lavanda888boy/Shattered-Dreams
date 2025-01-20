using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoulPortionManager : MonoBehaviour
{
    public int soulPortionCount = 0;
    public int totalSoulPortionsCount = 15;
    public Text soulPortionCountText;
    public string sceneToLoad;

    void Start()
    {

    }

    void Update()
    {
        soulPortionCountText.text = "Count: " + soulPortionCount.ToString() + "/" + totalSoulPortionsCount.ToString();

        if (soulPortionCount == totalSoulPortionsCount)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
