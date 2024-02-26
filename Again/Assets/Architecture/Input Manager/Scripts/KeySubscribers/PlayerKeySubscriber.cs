using Creature.Player.Weapon;
using InputControll.KeyReaders;
using InventorySystem;
using UnityEngine;

namespace InputControll.KeySubscriber
{
    public class PlayerKeySubscriber : MonoBehaviour
    {
        [SerializeField] private PhysicModuleSystem2D.PMController2D _physicController;
        [SerializeField] private CastSystem2D.Abstract.CastComponent _interactCast;
        public void OnSub()
        {
            PlayerKeyReader.MovementCallback += _physicController.SetDirection;
            PlayerKeyReader.SelectItemCallback += SelectItemManager.SelectItem;
            PlayerKeyReader.UseItemCallback += SelectItemManager.UseItem;
            PlayerKeyReader.InteractCallback += _interactCast.Cast;
            PlayerKeyReader.AttackCallback += AttackManager.Attack;
        }

        public void OnUnsub()
        {
            PlayerKeyReader.MovementCallback -= _physicController.SetDirection;
            PlayerKeyReader.SelectItemCallback -= SelectItemManager.SelectItem;
            PlayerKeyReader.UseItemCallback -= SelectItemManager.UseItem;
            PlayerKeyReader.InteractCallback -= _interactCast.Cast;
            PlayerKeyReader.AttackCallback -= AttackManager.Attack;
        }
    }
}
