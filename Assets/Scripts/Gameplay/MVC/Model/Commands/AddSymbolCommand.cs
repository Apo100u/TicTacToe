namespace TicTacToe.Gameplay.MVC.Model.Commands
{
    public class AddSymbolCommand : Command
    {
        public readonly Symbol Symbol;
        public readonly int GridPositionX;
        public readonly int GridPositionY;

        public AddSymbolCommand(Symbol symbol, int gridPositionX, int gridPositionY)
        {
            Symbol = symbol;
            GridPositionX = gridPositionX;
            GridPositionY = gridPositionY;
        }

        public override void Execute(TicTacToeGame game)
        {
            game.Grid.AddSymbol(Symbol, GridPositionX, GridPositionY);
        }

        public override void Undo(TicTacToeGame game)
        {
            game.Grid.RemoveSymbol(GridPositionX, GridPositionY);
        }
    }
}
