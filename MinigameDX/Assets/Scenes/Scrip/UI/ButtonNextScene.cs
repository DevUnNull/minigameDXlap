using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonNextScene : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject settingPanel;

    [Header("UI Animations")]
    [SerializeField] private ShrinkAndRestoreDOTween mainMenuAnimation;     // group hiện tại
    [SerializeField] private ShrinkAndRestoreDOTween settingPanelAnimation; // group trong panel

    [Header("Animation Time")]
    [SerializeField] private float transitionTime = 0.25f;

    private Coroutine panelCoroutine;

    public void OnClick(string sceneName)
{
    RestartCoroutine(LoadSceneRoutine(sceneName));
}

private IEnumerator LoadSceneRoutine(string sceneName)
{
    // 1️⃣ Animation chuyển cảnh (thu nhỏ UI hiện tại)
    mainMenuAnimation.ShrinkAll();

    // 2️⃣ Chờ animation xong
    yield return new WaitForSeconds(transitionTime);

    // 3️⃣ Load scene
    SceneManager.LoadScene(sceneName);
}


    // 🔓 MỞ SETTING
    public void OpenPanel()
    {
        RestartCoroutine(OpenRoutine());
    }

    // 🔒 ĐÓNG SETTING
    public void ClosePanel()
    {
        RestartCoroutine(CloseRoutine());
    }

    private IEnumerator OpenRoutine()
    {
        // 1️⃣ Thu nhỏ UI hiện tại
        mainMenuAnimation.ShrinkAll();
        yield return new WaitForSeconds(transitionTime);

        // 2️⃣ Hiện panel
        settingPanel.SetActive(true);

        // 3️⃣ Phóng to UI trong panel
        settingPanelAnimation.RestoreAll();
    }

    private IEnumerator CloseRoutine()
    {
        // 1️⃣ Thu nhỏ UI trong panel
        settingPanelAnimation.ShrinkAll();
        yield return new WaitForSeconds(transitionTime);

        // 2️⃣ Tắt panel
        settingPanel.SetActive(false);

        // 3️⃣ Phóng to lại UI chính
        mainMenuAnimation.RestoreAll();
    }

    private void RestartCoroutine(IEnumerator routine)
    {
        if (panelCoroutine != null)
            StopCoroutine(panelCoroutine);

        panelCoroutine = StartCoroutine(routine);
    }
}
