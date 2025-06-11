using UnityEngine;
using Shy.Unit;
using Shy.Dice;

namespace Shy.Select
{
    public class SelectManager : MonoBehaviour
    {
        public static SelectManager Instance;

        private bool dragState = false, canDrag = true;
        private DiceUi selectedDice = null;
        private Vector3 gizmoPos;

        private Collider2D characterCol = null;

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

            DontDestroyOnLoad(gameObject);
        }

        public void DragSet(bool _drag) => canDrag = _drag;

        #region Drag Event
        public void DragBegin(DiceUi _dice)
        {
            if (dragState || !canDrag) return;

            dragState = true;
            selectedDice = _dice;
        }

        private void DragEnd()
        {
            dragState = false;

            if(characterCol != null)
            {
                var _user = characterCol.GetComponentInParent<Character>();
                if(_user.team == selectedDice.team)
                {
                    selectedDice.SelectUser(_user);
                }
            }

            BattleManager.Instance.EndCheck();

            selectedDice = null;
            characterCol = null;
        }
        #endregion

        #region Ray
        private void Update()
        {
            if (dragState)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                gizmoPos = mousePos;

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.one * 0.7f, 0, LayerMask.GetMask("Character"));
                if (hit.collider != characterCol) characterCol = hit.collider;

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
            Gizmos.DrawCube(gizmoPos, Vector2.one * 0.7f);
        }
        #endregion
    }
}
