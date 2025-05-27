using UnityEngine;
using Shy.Unit;

namespace Shy.Select
{
    public class SelectManager : MonoBehaviour
    {
        public static SelectManager Instance;

        private bool dragState = false;
        private Character selectedCharacter = null;
        private Vector3 gizmoPos;

        private Collider2D diceUiCol = null;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                Input.multiTouchEnabled = false;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void DragBegin(Character _character)
        {
            Debug.Log("Drag Start");

            dragState = true;
            selectedCharacter = _character;
        }

        private void DragEnd()
        {
            Debug.Log("Drag End");

            dragState = false;

            ChangeDiceUi(diceUiCol);
            BattleManager.Instance.EndCheck();

            selectedCharacter = null;
            diceUiCol = null;
        }

        private void ChangeDiceUi(Collider2D _hit)
        {
            if (_hit == null) return;

            _hit.GetComponent<DiceUi>().SelectUser(selectedCharacter);
        }

        private void Update()
        {
            if (dragState)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                gizmoPos = mousePos;

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, LayerMask.GetMask("DiceUi"));
                if (hit.collider != diceUiCol) diceUiCol = hit.collider;

                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    DragEnd();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(gizmoPos, Vector3.one);
        }
    }
}
