using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class LoadScreenManager : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private Text _progressValue;
        [SerializeField] private string _targetScene = "Main Island";


        private void Start()
        {
            StartCoroutine(LoadAsync());
        }


        //Corutina: método base que vamos a llamar en el inicio de la corutina
        IEnumerator LoadAsync()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_targetScene);
            operation.allowSceneActivation = false; //Se tiene el control cuando se quiere pasar a la proxima scene

            while (!operation.isDone)
            {
                float progress = operation.progress;
                _progressBar.fillAmount = progress;
                _progressValue.text = $"Loading... {progress * 100}";

                if (progress >= .9f)
                {
                    _progressValue.text = "Press space to continue";
                    _progressBar.fillAmount = 1;
                    if (Input.GetKeyDown(KeyCode.Space)) operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}