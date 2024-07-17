namespace TicTacToeBackend.Commands
{
    public class AddSymbolCommand : Command
    {
        private readonly Symbol symbol;
        private readonly int gridPositionX;
        private readonly int gridPositionY;

        public AddSymbolCommand(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            this.symbol = symbol;
            this.gridPositionX = gridPositionX;
            this.gridPositionY = gridPositionY;
        }

        public override void Execute(TicTacToeGame game)
        {
            game.Grid.AddSymbol(symbol, gridPositionX, gridPositionY);
        }

        public override void Undo(TicTacToeGame game)
        {
            game.Grid.RemoveSymbol(gridPositionX, gridPositionY);
        }
    }
}
