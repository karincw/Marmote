using karin;
using karin.Core;
using UnityEngine;

class Debuger : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneChanger.Instance.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneChanger.Instance.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneChanger.Instance.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneChanger.Instance.LoadScene(3);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            DataLinkManager.Instance.Gem.Value += 1000;
            DataLinkManager.Instance.Coin.Value += 1000;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {

        }
        if (Input.GetKeyDown(KeyCode.F7))
        {

        }
        if (Input.GetKeyDown(KeyCode.F8))
        {

        }
    }
}