using UnityEngine;

namespace Managers
{
    public class GlobalData : MonoBehaviour
    {
        static public GlobalData instance;

        public bool IsVictory => _isVictory;
        [SerializeField] private bool _isVictory;

        private void Awake()
        {
            if (instance != null) Destroy(this.gameObject);
            instance = this;

            DontDestroyOnLoad(this);
        }

        public void SetVictoryField(bool isVictory) => _isVictory = isVictory;
    }
}