using Assets.Scripts.Figures;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private Board board;
        public Board Board => board;

        [Space(10)]
        [SerializeField] private Transform figuresParent;
        [SerializeField] private GameObject whitePawnPrefab;
        [SerializeField] private GameObject whiteBishopPrefab;
        [SerializeField] private GameObject whiteKnightPrefab;
        [SerializeField] private GameObject whiteRookPrefab;
        [SerializeField] private GameObject whiteQueenPrefab;
        [SerializeField] private GameObject whiteKingPrefab;
        [SerializeField] private GameObject blackPawnPrefab;
        [SerializeField] private GameObject blackBishopPrefab;
        [SerializeField] private GameObject blackKnightPrefab;
        [SerializeField] private GameObject blackRookPrefab;
        [SerializeField] private GameObject blackQueenPrefab;
        [SerializeField] private GameObject blackKingPrefab;

        private GameState currentGameState;
        private FiguresMap figuresMap;

        private void Awake()
        {
            instance = this;
            Initialize();
        }

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        public void Initialize()
        {
            board.Initialize();
            currentGameState = GameState.None;
            figuresMap = new FiguresMap(figuresParent);
        }

        public IEnumerator StartGame()
        {
            yield return new WaitForEndOfFrame();//жду конца фрейма, т.к. лейауту нужно время чтобы распределить ячейки доски в нужном порядке.
            InstantiateFigures();
            SetGameState(GameState.WhiteStep);
        }

        public void GameEnd()
        {
            figuresMap.BlockAllFiguresOnBoard();
        }

        public void NextStep()
        {
            Debug.Log("NextStep");
            if (currentGameState == GameState.BlackStep) { SetGameState(GameState.WhiteStep); return; }
            if (currentGameState == GameState.WhiteStep) { SetGameState(GameState.BlackStep); return; }
        }

        private void InstantiateFigures()
        {
            for (var i = 0; i < 8; i++)
            {
                figuresMap.AddFigureOnBoard(whitePawnPrefab, 6, i);
                figuresMap.AddFigureOnBoard(blackPawnPrefab, 1, i);
            }
            figuresMap.AddFigureOnBoard(whiteRookPrefab, 7, 0);
            figuresMap.AddFigureOnBoard(whiteKnightPrefab, 7, 1);
            figuresMap.AddFigureOnBoard(whiteBishopPrefab, 7, 2);
            figuresMap.AddFigureOnBoard(whiteQueenPrefab, 7, 3);
            figuresMap.AddFigureOnBoard(whiteKingPrefab, 7, 4);
            figuresMap.AddFigureOnBoard(whiteBishopPrefab, 7, 5);
            figuresMap.AddFigureOnBoard(whiteKnightPrefab, 7, 6);
            figuresMap.AddFigureOnBoard(whiteRookPrefab, 7, 7);

            figuresMap.AddFigureOnBoard(blackRookPrefab, 0, 0);
            figuresMap.AddFigureOnBoard(blackKnightPrefab, 0, 1);
            figuresMap.AddFigureOnBoard(blackBishopPrefab, 0, 2);
            figuresMap.AddFigureOnBoard(blackQueenPrefab, 0, 3);
            figuresMap.AddFigureOnBoard(blackKingPrefab, 0, 4);
            figuresMap.AddFigureOnBoard(blackBishopPrefab, 0, 5);
            figuresMap.AddFigureOnBoard(blackKnightPrefab, 0, 6);
            figuresMap.AddFigureOnBoard(blackRookPrefab, 0, 7);
        }


        #region Drag & Drop 
        public void OnStartDraggingFigure(Figure figure)
        {
            board.HighlightBoard(figuresMap.GetMovingMap(figure.Row,figure.Column));
            Debug.Log("StartRelocate");
        }
        public void OnBreakDraggingFigure()
        {
            board.UnHighlightBoard();
            Debug.Log("BreakRelocate");
        }
        public void OnDroppedFigure(Figure figure, int targetRow, int targetColumn)
        {
            board.UnHighlightBoard();
            figuresMap.BlockAllFiguresOnBoard();
            figuresMap.MoveFigure(figure, targetRow, targetColumn);
            Debug.Log("EndRelocate");
        }
        #endregion
        

        private void CheckStalemate()
        {
            Debug.Log("StaleMate");
        }

        public void CheckCheckmate()
        {
            Debug.Log("CheckMate");
        }

        private void SetGameState(GameState gameState)
        {
            currentGameState = gameState;
            switch (gameState)
            {
                case GameState.None:
                    break;
                case GameState.Prepare:
                    break;
                case GameState.WhiteStep:
                    figuresMap.BlockFiguresByColor(false);
                    break;
                case GameState.BlackStep:
                    figuresMap.BlockFiguresByColor(true);
                    break;
                case GameState.GameEnd:
                    figuresMap.BlockAllFiguresOnBoard();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        public enum GameState
        {
            None = 0,
            Prepare = 1,
            WhiteStep = 2,
            BlackStep = 3,
            GameEnd = 4,
        }

    }
}
