using GameStates;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUps
{
    public class PopUpInGame : Popup
    {
        public Image turnPlayerImage;

        private InGameState _inGameState;
        public override void Start()
        {
            base.Start();
            _inGameState = (InGameState)GameManager.GetState<InGameState>();
            SetInitPlayerTurn();
        }
     
        protected override void OnInstantiate()
        {
            Debug.Log("Play Init Sound");
        }
        
        private void SetInitPlayerTurn()
        {
            SetCurrentTurnPlayer(_inGameState.currentPlayer);
        }
        public void SetCurrentTurnPlayer(Player randPlay)
        {
            turnPlayerImage.sprite = randPlay.sprite;
            turnPlayerImage.color = randPlay.color;
        }
    }
}