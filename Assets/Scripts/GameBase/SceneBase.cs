using UnityEngine;

namespace TicTacToe.GameBase
{
    public class SceneBase : MonoBehaviour
    {
        protected GameBase gameBase;
        
        public virtual void Init(GameBase gameBase)
        {
            this.gameBase = gameBase;
        }
    }
}
