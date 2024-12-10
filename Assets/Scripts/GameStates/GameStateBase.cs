using System;
using Managers;
using UnityEngine;

namespace GameStates
{
    [Serializable]
    public class GameStateBase
    {
        protected GameManager GameManager;
        public virtual void Starter(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        
        public GameManager GameManagerFromBase => GameManager;

        public virtual void Enter()
        {
        }
        public virtual void Update()
        {
        }
        public virtual void Exit()
        {
        }
    }
}