using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingManager : MonoBehaviour
{
    [SerializeField] Slider loadingBar;
    [SerializeField] private string[] gameTips;
    [SerializeField] private TMP_Text tipsText;
    [SerializeField] private TMP_Text progressText;
    private void Start()
    {
        tipsText.text = gameTips[0];
    }
    private void Update()
    {
        StartCoroutine(ChangeTip());
    }
    public void LoadGame(int scene)
    {
        StartCoroutine(LoadAsynchronously(scene));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            progressText.text = loadingBar.value.ToString();
            yield return null;
        }
    }
    IEnumerator ChangeTip()
    {
        yield return new WaitForSeconds(2);
        int randomTip = Random.Range(0, gameTips.Length);
        tipsText.text = gameTips[randomTip];
    }
}
