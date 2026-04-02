using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SingleSpriteComponent : MonoBehaviour
    {
        [SerializeField] SpriteRenderer sprite;

        public void SetSrprite(Sprite sp)
        {
            sprite.sprite = sp;
        }
    }

}