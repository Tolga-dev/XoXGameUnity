using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour {
   [Header ("Mark Sprites : ")]
   [SerializeField] private Sprite spriteX;
   [SerializeField] private Sprite spriteO;
   
   [Header ("Components")]
   private Camera _cam;
   private LineRenderer _lineRenderer;
   
   [Header ("Input Settings : ")]
   [SerializeField] private LayerMask boxesLayerMask;
   [SerializeField] private float touchRadius;

   [Header ("Mark Colors : ")]
   [SerializeField] private Color colorX;
   [SerializeField] private Color colorO;

   public UnityAction<Mark,Color> OnWinAction;

   public Mark[] marks;
   private Mark _currentMark;
   private int _marksCount = 0;
   
   private bool _canPlay;

   private void Start() {
      _cam = Camera.main;
      _lineRenderer = GetComponent<LineRenderer>();

      InitGame();
   }

   private void InitGame()
   {
      _lineRenderer.enabled = false;
      _currentMark = Mark.X;
      marks = new Mark[9];
      _canPlay = true;
   }

   private void Update() {
      if (CanPlay()) {
         var touchPosition = _cam.ScreenToWorldPoint (Input.mousePosition);
         var hit = Physics2D.OverlapCircle (touchPosition, touchRadius, boxesLayerMask);
         
         if (hit)
            HitBox (hit.GetComponent <Box>());
      }
   }

   private bool CanPlay()
   {
      return _canPlay && Input.GetMouseButtonUp(0);
   }

   private void HitBox (Box box) {
      if (!box.CanUseBox())
      {
         SetCurrentBox(box);
         CheckWin();
      }
   }

   private void SetCurrentBox(Box box)
   {
      marks[box.index] = _currentMark;
      box.SetAsMarked (GetSprite(), _currentMark, GetColor());
      _marksCount++;
   }

   private void CheckWin()
   {
      var won = CheckIfWin();
      if (won)
      {
         SetGameFinished();
         return;
      }
      if (_marksCount == 9) {
         SetGameNoWinner();
         return;
      }
      
      SwitchPlayer();
   }

   private void SetGameNoWinner()
   {
      OnWinAction?.Invoke (Mark.None, Color.white);
      Debug.Log ("Nobody Wins.");
      _canPlay = false;
   }

   private void SetGameFinished()
   {
      OnWinAction?.Invoke (_currentMark, GetColor());
      _canPlay = false;
      Debug.Log (_currentMark + " Wins.");
   }

   private bool CheckIfWin() {
      return
      AreBoxesMatched (0, 1, 2) || AreBoxesMatched (3, 4, 5) || AreBoxesMatched (6, 7, 8) ||
      AreBoxesMatched (0, 3, 6) || AreBoxesMatched (1, 4, 7) || AreBoxesMatched (2, 5, 8) ||
      AreBoxesMatched (0, 4, 8) || AreBoxesMatched (2, 4, 6);
   }

   private bool AreBoxesMatched (int i, int j, int k) {
      var m = _currentMark;
      var matched = (marks [ i ] == m && marks [ j ] == m && marks [ k ] == m);

      if (matched)
         DrawLine (i, k);

      return matched;
   }

   private void DrawLine (int i, int k) {
      _lineRenderer.SetPosition (0, transform.GetChild (i).position);
      _lineRenderer.SetPosition (1, transform.GetChild (k).position);
      Color color = GetColor();
      color.a = .3f;
      
      _lineRenderer.startColor = color;
      _lineRenderer.endColor = color;
      _lineRenderer.enabled = true;
   }

   private void SwitchPlayer() {
      _currentMark = (_currentMark == Mark.X) ? Mark.O : Mark.X;
   }

   private Color GetColor() {
      return (_currentMark == Mark.X) ? colorX : colorO;
   }

   private Sprite GetSprite() {
      return (_currentMark == Mark.X) ? spriteX : spriteO;
   }
}
