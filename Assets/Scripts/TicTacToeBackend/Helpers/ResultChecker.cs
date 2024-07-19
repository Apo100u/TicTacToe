namespace TicTacToeBackend.Helpers
{
    public class ResultChecker
    {
        private readonly SymbolGrid grid;
        
        public ResultChecker(SymbolGrid grid)
        {
            this.grid = grid;
        }

        public bool IsSymbolWinning(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            bool isSymbolInWinningCombination =
                IsColumnWinningCombination(symbol, gridPositionX)
                || IsRowWinningCombination(symbol, gridPositionY)
                || IsDiagonalWinningCombination(symbol, gridPositionX, gridPositionY);

            return isSymbolInWinningCombination;
        }
        
        private bool IsColumnWinningCombination(Symbol symbol, int columnGridPosition)
        {
            bool isWinningCombination = true;

            for (int i = 0; i < grid.Size; i++)
            {
                Symbol? symbolToCheck = grid.GetSymbol(columnGridPosition, i);
                
                if (symbolToCheck == null || symbolToCheck != symbol)
                {
                    isWinningCombination = false;
                    break;
                }
            }

            return isWinningCombination;
        }
        
        private bool IsRowWinningCombination(Symbol symbol, int rowGridPosition)
        {
            bool isWinningCombination = true;

            for (int i = 0; i < grid.Size; i++)
            {
                Symbol? symbolToCheck = grid.GetSymbol(i, rowGridPosition);
                
                if (symbolToCheck == null || symbolToCheck != symbol)
                {
                    isWinningCombination = false;
                    break;
                }
            }

            return isWinningCombination;
        }
        
        private bool IsDiagonalWinningCombination(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            bool isWinningCombination = false;
            bool isOnIncreasingDiagonal = gridPositionX == gridPositionY;
            bool isOnDecreasingDiagonal = gridPositionY == grid.Size - gridPositionX - 1;

            if (isOnIncreasingDiagonal)
            {
                isWinningCombination = IsIncreasingDiagonalWinningCombination(symbol);
            }

            if (!isWinningCombination && isOnDecreasingDiagonal)
            {
                isWinningCombination = IsDecreasingDiagonalWinningCombination(symbol);
            }

            return isWinningCombination;
        }

        private bool IsIncreasingDiagonalWinningCombination(Symbol symbol)
        {
            bool isWinningCombination = true;

            for (int i = 0; i < grid.Size; i++)
            {
                Symbol? symbolToCheck = grid.GetSymbol(i, i);
                
                if (symbolToCheck == null || symbolToCheck != symbol)
                {
                    isWinningCombination = false;
                    break;
                }
            }

            return isWinningCombination;
        }

        private bool IsDecreasingDiagonalWinningCombination(Symbol symbol)
        {
            bool isWinningCombination = true;

            for (int i = 0; i < grid.Size; i++)
            {
                Symbol? symbolToCheck = grid.GetSymbol(i, grid.Size - i - 1);
                
                if (symbolToCheck == null || symbolToCheck != symbol)
                {
                    isWinningCombination = false;
                    break;
                }
            }

            return isWinningCombination;
        }
    }
}