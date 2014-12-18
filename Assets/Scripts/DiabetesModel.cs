using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes_Math_Model
{
    class DiabetesModel
    {
        private float Gpl0; //Gpl0 [mmol/L]
        private float Ipl0; //Ipl0 [mU/L]
        private float fc; //fc [mmol/mg]
        private float vg; //vg [L/kg]
        private float g0liv; //g0liv [mmol/L]
        private float beta; //beta [(mmol/L)/(microU/mL)]
        private float taui; //taui [min]
        private float taud; //taud [min]
        private float D; //D [mg]
        private float Mb; //Mb [kg]
        private float k1; //k1
        private float k2; //k2
        private float k4; //k4
        private float k5; //k5
        private float k6; //k6
        private float k7; //k7
        private float k9; //k9
        private float k10; //k10
        private float k12; //k12
        private float k13; //k13
        private float k14; //k14
        private float sigma; //sigma
        private float KM; //KM
        private float k18; //k18
        private float[] statevar; // start state
        private float t; // current time

        public DiabetesModel(float gpl0, float ipl0, float d)
        {
            this.Gpl0 = gpl0;
            this.Ipl0 = ipl0;
            this.fc = 0.0056f;
            this.g0liv = 0.0661f;
            this.beta = 0.5f;
            this.taui = 31;
            this.taud = 3;
            this.D = d;
            this.Mb = 70;
            this.vg = 17 / Mb;
            this.k1 = 0.068723259761621f;
            this.k2 = 0.021147467821140f;
            this.k4 = 0.047426444780459f;
            this.k5 = 3.247074504867475f;
            this.k6 = 0.002633295922317f;
            this.k7 = 0.002795277515631f;
            this.k9 = 0.080437803751680f;
            this.k10 = 0.015149414626578f;
            this.k12 = 24.376217313635816f;
            this.k13 = 6.632351344644380f;
            this.k14 = 6.570170579847475f;
            this.sigma = 1.186634359183124f;
            this.KM = 27.394364275993492f;
            this.k18 = 0;

            statevar = new float[] { 0, gpl0, ipl0, 0, 0 };
            t = 0;
        }

        /// <summary>
        /// Computes deratives, and outputs them in ydot.
        /// </summary>
        /// <param name="t">The steps.</param>
        /// <param name="y">The current values. Mg, Gpl, Ipl, Irem and Gint (5 values)</param>
        /// <param name="ydot">An array with a length of 5 with the new values.</param>
        public float[] compute(float t, float adjustment = 0)
        {
            float[] ydot;
            t += this.t;

            // y[0] = Mg: glucose mass in gut [mg]
            // y[1] = Gpl: plasma glucose concentration [mmol/L]
            // y[2] = Ipl: plasma insulin concentration [mU/L]
            // y[3] = Irem: interstitium insulin concentration [mU/L]
            // y[4] = Gint: integrated plasma glucose increase (Gpl-Gpl0) [mmol/L]
            ydot = new float[5];
            float[] y = (float[])statevar.Clone();
            for (int i = 0; i < y.Length; i++)
            {
                y[i] += adjustment;
            }

            // dMg/dt -- glucose in gut
            float mgpl = k2 * y[0];
            float mgut = (float)(sigma * Math.Pow(k1, sigma) * Math.Pow(t, sigma - 1)
                * Math.Exp(-Math.Pow(k1 * t, sigma)) * Math.Exp(-k18 * t) * D);

            ydot[0] = mgut - mgpl;

            // dGpl/dt -- glucose in plasma
            float gliv = g0liv - (k9 * (y[1] - Gpl0) + k10 * beta * y[3]);
            float ggut = k2 * (fc / (vg * Mb)) * y[0];
            float gnonit = g0liv * ((KM + Gpl0) / Gpl0) * (y[1] / (KM + y[1]));
            float git = k4 * beta * y[3] * (y[1] / (KM + y[1]));
            float gthren = 0;
            ydot[1] = gliv + ggut - gnonit - git - gthren;
            // dIpl/dt -- insulin in plasma
            ydot[4] = y[1] - Gpl0;
            float ipnc = (1 / beta) * (k12 * (y[1] - Gpl0)
            + (k13 / taui) * y[4] + (k14 * taud) * ydot[1]);
            float isa = 0;
            float ila = 0;
            float irem = k5 * (y[2] - Ipl0);
            ydot[2] = ipnc + isa + ila - irem;
            // dIrem/dt -- insulin in interstitium
            float ipl = k6 * (y[2] - Ipl0);
            float iit = k7 * y[3];
            ydot[3] = ipl - iit;

            return ydot;
        }

        public void setState(float steps, float[] state)
        {
            this.statevar = state;
            this.t += steps;
        }

        public float[] getState()
        {
            return this.statevar;
        }

        public int getDimension()
        {
            return 5;
        }
    }
}
