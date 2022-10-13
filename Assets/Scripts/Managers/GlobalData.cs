using UnityEngine;

namespace Managers
{
    public class GlobalData : MonoBehaviour
    {
        public static GlobalData instance;
        [SerializeField] private bool _isVictory;

        public bool IsVictory => _isVictory;

        private void Awake()
        {
            if (instance != null) Destroy(gameObject);
            instance = this;

            DontDestroyOnLoad(this);
        }

        public void SetVictoryField(bool isVictory)
        {
            _isVictory = isVictory;
        }
    }
}