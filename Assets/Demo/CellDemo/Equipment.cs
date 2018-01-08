using UnityEngine;
using ZergRush;
using ZergRush.ReactiveCore;

namespace Demo.CellDemo
{
    public class Equipment
    {
        public UnitBuffType type;
        public Cell<int> buff = new Cell<int>(3);

        public void Upgrade()
        {
            buff.value += 1;
        }

        public static Equipment RandomOne()
        {
            return new Equipment
            {
                type = RandomExtensions.GetRandomEnum<UnitBuffType>(),
                buff = {value = Random.Range(1, 5)}
            };
        }
    }
}