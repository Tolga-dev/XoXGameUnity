
using System;
using System.Collections.Generic;
using Managers;
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
        public override void Starter(GameManager gameManager)
        {
            base.Starter(gameManager);
            board.Starter();
            _cam = Camera.main;
        }
        
        public override void Update()
        {
            if (CanPlay())
            {
                var hit = GetHit();
                if (hit)
                    board.HitBox (hit.GetComponent <Box>());
            }
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
            if (currentPlayer.mark == Mark.O)
            {
                currentPlayer = GetPlayerFromMark(Mark.X);
                return;
            }
            currentPlayer = GetPlayerFromMark(Mark.O);
        }
        
        public bool CanPlay()
        {
            return Input.GetMouseButtonUp(0);
        }
    }
}