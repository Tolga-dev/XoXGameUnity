using UnityEngine;

public enum Mark {
   None,
   X,
   O
}
public class Box : MonoBehaviour {
   public int index;
   
   private SpriteRenderer _spriteRenderer;
   private CircleCollider2D _circleCollider2D;
   
   private bool _isMarked;
   private void Awake() {
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _circleCollider2D = GetComponent<CircleCollider2D>();
      
      index = transform.GetSiblingIndex();
      _isMarked = false;
   }

   public void SetAsMarked (Player player) {
      _isMarked = true;
      _spriteRenderer.color = player.color;
      _spriteRenderer.sprite = player.sprite;
      _circleCollider2D.enabled = false;
   }

   public bool CanUseBox()
   {
      return _isMarked;
   }
}
