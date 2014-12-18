using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes_Math_Model
{
    class Food
    {
        private string name;
        private float carbohydrates;
        private float absorbsionTime;
        private float time;
        private float carbConst;

        public Food(string name, float carbohydrates, float absorbsionTime)
        {
            this.name = name;
            this.carbohydrates = carbohydrates;
            this.absorbsionTime = absorbsionTime;
            carbConst = carbohydrates / 4;
        }

        public string getName()
        {
            return name;
        }

        public float getCarbs()
        {
            return carbohydrates;
        }

        public void setCarbs(float carbs)
        {
            this.carbohydrates = carbs;
        }

        public float getAbsorbionTime()
        {
            return absorbsionTime;
        }

        public float getTime()
        {
            return time;
        }

        public void setTime(float time)
        {
            this.time = time;
        }

        public float getCarbConst()
        {
            return this.carbConst;
        }
    }
}
