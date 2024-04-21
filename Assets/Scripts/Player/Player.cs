using RDDependency;
using UnityEngine;

namespace AfterTomorrow
{
    public class Player : MonoBehaviour, ISetup
    {
        [field:SerializeField] public PlayerInput PlayerInput { get; set; }
        public void Setup()
        {
            
        }
    }
}
