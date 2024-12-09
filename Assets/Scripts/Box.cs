using UnityEngine;

public enum Mark {
   None,
   X,
   O
}
public class Box : MonoBehaviour {
   public int index;
   private Mark _mark;
   
   
   private SpriteRenderer _spriteRenderer;
   private CircleCollider2D _circleCollider2D;
   
   private bool _isMarked;

   private void Awake() {
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _circleCollider2D = GetComponent<CircleCollider2D>();
      
      index = transform.GetSiblingIndex();
      
      _mark = Mark.None;
      _isMarked = false;
   }

   public void SetAsMarked (Sprite sprite, Mark mark, Color color) {
      _isMarked = true;
      _mark = mark;

      _spriteRenderer.color = color;
      _spriteRenderer.sprite = sprite;

      _circleCollider2D.enabled = false;
   }

   public bool CanUseBox()
   {
      return _isMarked;
   }
}
