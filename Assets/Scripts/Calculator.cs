using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes_Math_Model
{
    class Calculator
    {
        private DiabetesModel dm;

        public Calculator(float gpl0, float ipl0, float d)
        {
            dm = new DiabetesModel(gpl0, ipl0, d);
        }

        //public float[] calculate(float steps)
        //{
        //    float[] state = (float[])dm.getState().Clone();
        //    float[] k1 = new float[dm.getDimension()];
        //    float[] k2 = new float[dm.getDimension()];
        //    float[] k3 = new float[dm.getDimension()];
        //    float[] k4 = new float[dm.getDimension()];

        //    k1 = state;

        //    for (int i = 0; i < dm.getDimension(); i++)
        //    {
        //        k2[i] = dm.compute(steps / 2, .5f * k1[i] * steps)[i];
        //    }

        //    for (int i = 0; i < dm.getDimension(); i++)
        //    {
        //        k3[i] = dm.compute(steps / 2, .5f * k2[i] * steps)[i];
        //    }

        //    for (int i = 0; i < dm.getDimension(); i++)
        //    {
        //        k4[i] = dm.compute(steps, k3[i] * steps)[i];
        //    }

        //    for (int i = 0; i < dm.getDimension(); i++)
        //    {
        //        state[i] += ((steps / 6) * (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]));
        //    }

        //    dm.setState(steps, state);
        //    return dm.getState();
        //}

        public float[] calculate(float steps)
        {
            return null;
        }
    }
}
