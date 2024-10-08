namespace TicTacToe.Gameplay.MVC.Model.Commands
{
    public abstract class Command
    {
        public abstract void Execute(TicTacToeGame game);
        public abstract void Undo(TicTacToeGame game);
    }
}
