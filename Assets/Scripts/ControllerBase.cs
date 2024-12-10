using Managers;
using UnityEngine;

namespace Controllers
{
    public class ControllerBase : MonoBehaviour
    {
        protected GameManager GameManager;
        
        public virtual void Starter(GameManager gameManagerInGame)
        {
            GameManager = gameManagerInGame;
        }
    }
}