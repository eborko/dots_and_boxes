using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void CellClosed(Cell sender);
    public class Cell : MonoBehaviour
    {
        public static CellBorder FirstSender { get; set; }
        public static CellBorder SecondSender { get; set; }

        public CellBorder TopBorder { get; set; }
        public CellBorder RightBorder { get; set; }
        public CellBorder BottomBorder { get; set; }
        public CellBorder LeftBorder { get; set; }

        public GameObject X_sign { get; set; }
        public GameObject O_sign { get; set; }

        public event CellClosed OnCellClosed;
        public bool IsClosed { get; private set; }

        public void Start()
        {
            IsClosed = false;
            TopBorder.OnActivated += Border_Activated;
            BottomBorder.OnActivated += Border_Activated;
            LeftBorder.OnActivated += Border_Activated;
            RightBorder.OnActivated += Border_Activated;
        }

        private void Border_Activated(CellBorder sender)
        {
            if (FirstSender == null)
            {
                FirstSender = sender;
            }
            else if (SecondSender == null)
            {
                SecondSender = sender;
            }
            else
            {
                SecondSender = null;
                FirstSender = sender;
            }

            if (TopBorder.IsActivated && RightBorder.IsActivated && BottomBorder.IsActivated && LeftBorder.IsActivated)
            {
                GameObject temp;
                if (!GameManager.IsPlayerXTurn)
                {
                    temp = GameObject.Instantiate(O_sign);
                }
                else
                {
                    temp = GameObject.Instantiate(X_sign);
                }
                temp.transform.Translate(new Vector3(TopBorder.transform.position.x, LeftBorder.transform.position.y, 0));

                AddOnePointToPlayer();
                OnCellClosed?.Invoke(this);
                
            }
            else
            {
                if (FirstSender != null && SecondSender != null)
                {
                    if (FirstSender.FirstCoowner.IsClosed || FirstSender.SeccondCoowner.IsClosed)
                    {
                        Debug.Log("Play again.");
                    }
                    else
                    {
                        GameManager.IsPlayerXTurn = !GameManager.IsPlayerXTurn;
                        Debug.Log($"Is X turn: {GameManager.IsPlayerXTurn}");
                    }
                }
            }
        }

        private void AddOnePointToPlayer()
        {
            // Display message
            Debug.Log("+1");
            if (GameManager.IsPlayerXTurn && !IsClosed)
            {
                GameManager.PlayerXScore++;
                IsClosed = true;
                Debug.Log($"Player X score: {GameManager.PlayerXScore}");
            }
            else if(!IsClosed)
            {
                GameManager.PlayerOScore++;
                IsClosed = true;
                Debug.Log($"Player O score: {GameManager.PlayerOScore}");
            }
        }
    }
}
