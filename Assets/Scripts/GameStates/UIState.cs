using System;
using Managers;
using PopUps;
using UI.PopUps;
using UnityEngine;

namespace GameStates
{
    [Serializable]
    public class UIState : GameStateBase
    {
        public override void Starter(GameManager gameManager)
        {
            base.Starter(gameManager);
            gameManager.popUpController.Show<PopUpInGame>();
        }

        public override void Enter()
        {
            GameManager.SwitchStates<InGameState>();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
        

    }
}