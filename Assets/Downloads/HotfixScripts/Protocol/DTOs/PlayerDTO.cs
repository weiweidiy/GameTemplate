using JFramework.Unity;

namespace Game
{
    public class PlayerDTO
    {
        [ModelKey]
        public string Uid { get; set; }
        public string Username { get; set; }
    }
}