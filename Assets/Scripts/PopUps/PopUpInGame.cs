using GameStates;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopUps
{
    public class PopUpInGame : Popup
    {
        public Timer.Scripts.Timers.Timer timer;
        
        public Transform gotFirstInput;

        // upper part ui
        public TextMeshProUGUI turnPlayer;
        public Image turnPlayerImage;

        private InGameState _inGameState;
        public override void Start()
        {
            _inGameState = (InGameState)GameManager.GetState<InGameState>();
            SetInitPlayerTurn();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            ResetGameUI();
        }
        protected override void OnInstantiate()
        {
            Debug.Log("Play Init Sound");
        }
        
        public void HomeButton()
        {
        }

        public void ReloadButton()
        {
            
        }
        private void ResetGameUI()
        {
            Debug.Log("Timer Reseted");
            
        }
        private void SetInitPlayerTurn()
        {
            _inGameState.currentPlayer = _inGameState.GetRandomPlayer();
            SetCurrentTurnPlayer(_inGameState.currentPlayer);
        }
        private void SetCurrentTurnPlayer(Player randPlay)
        {
            turnPlayer.text = randPlay.mark.ToString();
            turnPlayerImage.sprite = randPlay.sprite;
            turnPlayerImage.color = randPlay.color;
        }
    }
}