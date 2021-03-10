using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class GameManager : MonoBehaviour
    {
        public static bool IsPlayerXTurn { get; set; }
        public static int PlayerXScore { get; set; }
        public static int PlayerOScore { get; set; }

        public void Start()
        {
            IsPlayerXTurn = true;
            PlayerOScore = 0;
            PlayerXScore = 0;
        }
    }
}
