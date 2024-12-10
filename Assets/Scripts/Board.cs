using System;
using GameStates;
using Managers;
using PopUps;
using UI.PopUps;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Player
{
   public Sprite sprite;   
   public Color color;
   public Mark mark;
}

public class Board : MonoBehaviour
{
   private GameManager _gameManager;
   private InGameState _inGameState;
   
   [Header ("Components")]
   private LineRenderer _lineRenderer;

   public Mark[] marks;
   private int _marksCount = 0;

   public void Starter() {
      _lineRenderer = GetComponent<LineRenderer>();
      _gameManager = GameManager.Instance;
      _inGameState = (InGameState)_gameManager.GetState<InGameState>();

      InitBoard();
   }
   
   private void InitBoard()
   {
      _lineRenderer.enabled = false;
      marks = new Mark[9];
   }

   public void HitBox (Box box) {
      
      Debug.Log("Play Hit Music");
      
      if (!box.CanUseBox())
      {
         Debug.Log("Play Draw Music");
      
         SetCurrentBox(box);
         CheckWin();
      }
      
   }

   private void SetCurrentBox(Box box)
   {
      marks[box.index] = CurrentPlayer.mark;
      _marksCount++;
      
      box.SetAsMarked (CurrentPlayer);
   }

   private void CheckWin()
   {
      var won = CheckIfWin();
      if (won)
      {
         SetGameWinner();
         return;
      }
      if (_marksCount == 9) {
         SetGameNoWinner();
         return;
      }
      _inGameState.SwitchPlayer();
   }

   private void SetGameNoWinner()
   {
      _inGameState.SetGameNoWinner();
      SetGameFinished();
   }
   private void SetGameWinner()
   {
      _inGameState.SetGameWinner();
      SetGameFinished();
   }

   private void SetGameFinished()
   {
      InitBoard();
      Debug.Log("Exit From Game State");
      Debug.Log("Play Game Finished Music");
   }

   private bool CheckIfWin() {
      return
      AreBoxesMatched (0, 1, 2) || AreBoxesMatched (3, 4, 5) || AreBoxesMatched (6, 7, 8) ||
      AreBoxesMatched (0, 3, 6) || AreBoxesMatched (1, 4, 7) || AreBoxesMatched (2, 5, 8) ||
      AreBoxesMatched (0, 4, 8) || AreBoxesMatched (2, 4, 6);
   }

   private bool AreBoxesMatched (int i, int j, int k) {
      var m = CurrentMark;
      var matched = (marks [ i ] == m && marks [ j ] == m && marks [ k ] == m);

      if (matched)
         DrawLine (i, k);

      return matched;
   }

   private void DrawLine (int i, int k) {
      _lineRenderer.SetPosition (0, transform.GetChild (i).position);
      _lineRenderer.SetPosition (1, transform.GetChild (k).position);
      Color color = CurrentPlayer.color;
      color.a = .3f;
      
      _lineRenderer.startColor = color;
      _lineRenderer.endColor = color;
      _lineRenderer.enabled = true;
   }

   private Player CurrentPlayer => _inGameState.currentPlayer;
   private Mark CurrentMark => _inGameState.currentPlayer.mark;
   private PopUpController popUpController => _gameManager.popUpController;

}
