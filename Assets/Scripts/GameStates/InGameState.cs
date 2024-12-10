
using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using PopUps;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameStates
{
    [Serializable]
    public class InGameState : GameStateBase
    {
        private Camera _cam;
        
        [Header ("Input Settings : ")]
        [SerializeField] private LayerMask boxesLayerMask;
        [SerializeField] private float touchRadius;
        public Board board;
        
        [Header ("Player Configurations : ")]
        public List<Player> players = new List<Player>();
        public Player currentPlayer;
        public bool isGame = false;
        public override void Starter(GameManager gameManager)
        {
            base.Starter(gameManager);
            
            board.Starter();
            _cam = Camera.main;
        }

        public override void Enter()
        {
            isGame = true;
            currentPlayer = GetRandomPlayer();
        }

        public override void Update()
        {
            if (IsClicked() && isGame)
            {
                Debug.Log("Clicked");
                var hit = GetHit();
                if (hit)
                    board.HitBox (hit.GetComponent <Box>());
            }
        }

        public override void Exit()
        {
            
        }

        private Collider2D GetHit()
        {
            var touchPosition = _cam.ScreenToWorldPoint (Input.mousePosition);
            return Physics2D.OverlapCircle (touchPosition, touchRadius, boxesLayerMask);
        }
        
        public Player GetRandomPlayer()
        {
            return players[Random.Range(0, players.Count)];
        }
        
        public Player GetPlayerFromMark(Mark mark)
        {
            foreach (var player in players)
            {
                if (player.mark == mark)
                    return player;
            }
            return null;
        }
        public void SwitchPlayer() {
            
            currentPlayer = GetPlayerFromMark(currentPlayer.mark == Mark.O ? Mark.X : Mark.O);
            
            var popUpInGame = (PopUpInGame)GameManager.popUpController.Get<PopUpInGame>();
            popUpInGame.SetCurrentTurnPlayer(currentPlayer);
            
        }
        
        public bool IsClicked()
        {
            return Input.GetMouseButtonUp(0);
        }
        
        public void SetGameWinner()
        {
            isGame = false; 
            GameManager.StartCoroutine(PlayWinAnim());
        }
        public void SetGameNoWinner()
        {
            GameManager.StartCoroutine(PlayTeiAnim());
        }

        public IEnumerator PlayWinAnim()
        {
            yield return new WaitForSeconds(1);
            Debug.Log($"Play Winner Animation + {currentPlayer.mark}");
            ResetGame();
        }

        public IEnumerator PlayTeiAnim()
        {
            yield return new WaitForSeconds(1);
            Debug.Log($"Play Tie Animation + {currentPlayer.mark}");
            ResetGame();
        }

        public void ResetGame()
        {
            board.SetGameFinished();
            GameManager.SwitchStates<UIState>();
        }
    }
}