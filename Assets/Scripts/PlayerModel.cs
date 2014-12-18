using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes_Math_Model
{
    class PlayerModel
    {
        private List<Food> digesting;
        private float insulinReserves;
        private float insulin;
        private float glucose;
        private int age;
        private const float glucosePerCarb = .007f;
        private const float glucoseDropSpeed = 1.25f;

        public float getGlucose()
        {
            return glucose;
        }

        public PlayerModel(int age, float insulin, float glucose)
        {
            digesting = new List<Food>();
            this.age = age;
            this.insulinReserves = insulin;
            this.glucose = glucose;
        }

        public void eat(Food f)
        {
            digesting.Add(f);
        }

        public void addInsulinReserves(float insulin)
        {
            this.insulinReserves += insulin;
        }

        public void useInsulinReserves(float amount)
        {
            this.insulinReserves -= amount;
            this.insulin += amount;
        }

        /// <summary>
        /// Shifts time for this player.
        /// </summary>
        /// <param name="time">The time to pass, in minutes.</param>
        public void shiftTime(float time)
        {
            #region food
            List<Food> ToRemove = new List<Food>();

            foreach (Food item in digesting)
            {
                float carbs = item.getCarbs();
                float duration = item.getAbsorbionTime();
                float itemTime = item.getTime();
                float carbConst = item.getCarbConst();

                Console.WriteLine(carbs);

                if (itemTime + time > duration)
                {
                    itemTime = duration - time;
                }
                else
                {
                    itemTime += time;
                }

                float carbsLeft = carbConst * (-(float)Math.Log10(itemTime / duration));
                float carbsAbsorbed = carbs - carbsLeft;

                glucose += carbsAbsorbed * glucosePerCarb;

                if (itemTime > duration)
                {
                    ToRemove.Add(item);
                }
                else
                {
                    item.setCarbs(carbsLeft);
                    item.setTime(itemTime);
                }
            }

            foreach (Food item in ToRemove)
            {
                digesting.Remove(item);
            }
            #endregion

            #region glucose
            glucose -= (time / 60) * glucoseDropSpeed;
            #endregion

            #region insulin
            if (insulin > 0)
            {
                float old = insulin;
                insulin -= .5f;

                if (insulin < 0)
                {
                    insulin = 0;
                }

                float used = old - insulin;
                glucose -= used * .2f;
                if (glucose < 0)
                {
                    glucose = 0;
                }
            }
            #endregion
        }
    }
}
