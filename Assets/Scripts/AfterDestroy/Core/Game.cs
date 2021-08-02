using UnityEngine;

namespace AfterDestroy.Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        private void OnValidate()
        {
            if (playerController == null)
            {
                playerController = FindObjectOfType<PlayerController>();
            }
        }

        void Start()
        {
            playerController.Init();
        }

        void Update()
        {
        
        }
    }
}
