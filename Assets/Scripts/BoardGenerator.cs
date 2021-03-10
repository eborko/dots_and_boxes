using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class BoardGenerator : MonoBehaviour
    {
        private GameObject[,] _horisontalCellBorders;
        private GameObject[,] _verticalCellBorders;
        private GameObject[,] _cells;

        public ColorPalete ColorPalete;
        public GameObject X_sign;
        public GameObject O_sign;
        public GameObject VerticalBorder;
        public GameObject HorisontalBoorder;
        public GameObject StaticRect;
        public GameObject CellPrefab;
        public int XLenght;
        public int YLenght;

        public void Start()
        {
            GenerateCellBorders();
            DrawStaticRects();
            ActivateEdgeBorders();
            InitCells();
        }

        private void ActivateEdgeBorders()
        {
            ActivateVerticalBorders();
            ActivateHorisontalBorders();
        }

        private void ActivateHorisontalBorders()
        {
            int i = 0;
            for (int j = 0; j < XLenght; j++)
            {
                _horisontalCellBorders[i, j].GetComponent<Scripts.CellBorder>().IsActivated = true;
            }
            i = YLenght -1;
            for (int j = 0; j < XLenght; j++)
            {
                _horisontalCellBorders[i, j].GetComponent<Scripts.CellBorder>().IsActivated = true;
            }
        }

        private void ActivateVerticalBorders()
        {
            int j = 0;
            for (int i = 0; i < YLenght - 1; i++)
            {
                _verticalCellBorders[i, j].GetComponent<Scripts.CellBorder>().IsActivated = true;
            }
            j = XLenght;
            for (int i = 0; i < YLenght - 1; i++)
            {
                _verticalCellBorders[i, j].GetComponent<Scripts.CellBorder>().IsActivated = true;
            }
        }

        private void InitCells() // ERROR in method - not adding horisontal coowners
        {
            _cells = new GameObject[YLenght - 1, XLenght];

            // Link Horisontal borders
            for (int i = 0; i < YLenght - 1; i++)
            {
                for (int j = 0; j < XLenght; j++)
                {
                    //Debug.Log($"Cell[{i}, {j}]");
                    GameObject tempCell = Instantiate(CellPrefab);

                    tempCell.GetComponent<Scripts.Cell>().TopBorder = _horisontalCellBorders[i, j].GetComponent<Scripts.CellBorder>();
                    tempCell.GetComponent<Scripts.Cell>().TopBorder.AddCoowner(tempCell.GetComponent<Scripts.Cell>());
                    //Debug.Log($"Top Border h[{i}, {j}]");

                    if (i < YLenght - 1)
                    {
                        tempCell.GetComponent<Scripts.Cell>().BottomBorder = _horisontalCellBorders[i + 1, j].GetComponent<Scripts.CellBorder>();
                        tempCell.GetComponent<Scripts.Cell>().BottomBorder.AddCoowner(tempCell.GetComponent<Scripts.Cell>());
                        //Debug.Log($"Bottom Border h[{i + 1}, {j}]");
                    }

                    tempCell.GetComponent<Scripts.Cell>().X_sign = X_sign;
                    tempCell.GetComponent<Scripts.Cell>().O_sign = O_sign;
                    _cells[i, j] = tempCell;
                }
            }

            // Link Vertical borders
            for (int i = 0; i < YLenght - 1; i++)
            {
                for (int j = 0; j < XLenght; j++)
                {
                    //Debug.Log($"Cell[{i}, {j}]");
                    _cells[i, j].GetComponent<Scripts.Cell>().LeftBorder = _verticalCellBorders[i, j].GetComponent<Scripts.CellBorder>();
                    _cells[i, j].GetComponent<Scripts.Cell>().LeftBorder.AddCoowner(_cells[i, j].GetComponent<Scripts.Cell>());
                    //Debug.Log($"Left Border v[{i}, {j}]");

                    if (i < YLenght - 1)
                    {
                        _cells[i, j].GetComponent<Scripts.Cell>().RightBorder = _verticalCellBorders[i, j + 1].GetComponent<Scripts.CellBorder>();
                        _cells[i, j].GetComponent<Scripts.Cell>().RightBorder.AddCoowner(_cells[i, j].GetComponent<Scripts.Cell>());
                        //Debug.Log($"Right Border v[{i}, {j + 1}]");
                    }                    
                }
                
            }
        }

        private void DrawStaticRects()
        {
            for (int i = 0; i < YLenght; i++)
            {
                for (int j = 0; j < XLenght + 1; j++)
                {
                    GameObject tempGO = Instantiate(StaticRect);
                    tempGO.transform.Translate(new Vector3(j - 0.5f, i, 0));
                    tempGO.GetComponent<SpriteRenderer>().color = new Color(ColorPalete.ActiveColor.r, ColorPalete.ActiveColor.g, ColorPalete.ActiveColor.b, 1);
                }
            }
        }

        private void GenerateCellBorders()
        {
            _horisontalCellBorders = new GameObject[YLenght, XLenght];
            for (int i = 0; i < YLenght; i++)
            {
                for (int j = 0; j < XLenght; j++)
                {
                    GameObject tempGO = Instantiate(HorisontalBoorder);
                    tempGO.transform.Translate(new Vector3(i, -j, 0));
                    tempGO.GetComponent<Scripts.CellBorder>().ColorPalete = this.ColorPalete;

                    _horisontalCellBorders[i, j] = tempGO;
                }
            }

            _verticalCellBorders = new GameObject[YLenght - 1, XLenght + 1];
            for (int i = 0; i < YLenght - 1; i++)
            {
                for (int j = 0; j < XLenght + 1; j++)
                {
                    GameObject tempGO = Instantiate(VerticalBorder);
                    tempGO.transform.Translate(new Vector3(j - 0.5f, i + 0.5f, 0));
                    tempGO.GetComponent<Scripts.CellBorder>().ColorPalete = this.ColorPalete;

                    _verticalCellBorders[i, j] = tempGO;
                }
            }
        }
    }
}
