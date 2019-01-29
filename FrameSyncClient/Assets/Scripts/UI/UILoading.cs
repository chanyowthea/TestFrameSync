using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    static string _TargetSceneName;

    [SerializeField] Text _ProgressText;
    float _MinLoadingTime = 2f;
    float _CurConsumeTime;
    AsyncOperation _LoadingOperation;

    public static void LoadSceneAsync(string sceneName)
    {
        _TargetSceneName = sceneName;
        UIManager.Instance.OpenLoading();
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(_TargetSceneName))
        {
            return;
        }

        SceneManager.LoadScene(ConstValue._LoadingScene);
        _LoadingOperation = SceneManager.LoadSceneAsync(_TargetSceneName);
        _LoadingOperation.allowSceneActivation = false;

        _ProgressText.text = string.Format("Loading...{0}%", 0);
    }

    private void OnDestroy()
    {
        _TargetSceneName = null;
    }

    private void Update()
    {
        int progress = 0;
        if (_CurConsumeTime < _MinLoadingTime)
        {
            _CurConsumeTime += Time.deltaTime;
            float realProgress = _LoadingOperation.progress;
            float timeProgress = _CurConsumeTime / _MinLoadingTime;
            progress = Mathf.RoundToInt(Mathf.Min(realProgress, timeProgress) * 100);
        }
        else
        {
            progress = Mathf.RoundToInt(_LoadingOperation.progress * 100);
        }

        _ProgressText.text = string.Format("Loading...{0}%", progress);
        if (progress == 100)
        {
            _LoadingOperation.allowSceneActivation = true;
        }
    }
}
