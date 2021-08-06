using System;
using System.Collections.Generic;

namespace Engine_simulation
{

    /// <summary>
    ///The ICEngine class simulates the operation 
    ///of an internal combustion engine.
    ///Calculates overheating time
    /// </summary>
    class ICEngine : IEngine
    {
        public double I { get; private set; }                   //Value of engine moment of inertia   

        public List<double> torqueList { get; private set; }    //List of torque values

        public List<double> velocityList { get; private set; }  //List of crankshaft rotation speeds

        public double ambientTemperature { get; private set; }  //Value of ambient temperature (Celsius)

        public double engineTemperature { get; private set; }   //Value of current engine temperature (Celsius)

        public double superheatTemperature { get; private set; }//Engine overheat temperature value (Celsius)    

        public double Hm { get; private set; }                  //The coefficient of the dependence of the heating rate on the torque

        public double Hv { get; private set; }                  //The coefficient of the dependence of the heating rate on the crankshaft rotation speed

        public double C { get; private set; }                   //Coefficient of dependence of cooling rate on engine temperature and ambient temperature

        /// <summary>
        ///ICEngine's main constructor
        /// </summary>
        public ICEngine(
            double I,                   
            List<double> torqueList,    
            List<double> velocityList,   
            double superheatTemperature,
            double Hm,                  
            double Hv,                  
            double C,                   
            double ambientTemperature            
        ) {
            this.I = I;
            this.torqueList = torqueList;
            this.velocityList = velocityList;
            this.superheatTemperature = superheatTemperature;
            this.Hm = Hm;
            this.Hv = Hv;
            this.C = C;
            this.ambientTemperature = (ambientTemperature < -50 || ambientTemperature > 50) ? 0 : ambientTemperature; // if the value does not meet the condition, ambient is 0

            engineTemperature = this.ambientTemperature;
        }


        /// <summary>
        /// GetHeatingSpeed method counts
        /// engine heating speed
        /// </summary>
        /// <param name="torque">Value of current torque</param>
        /// <param name="velocity">Value of current velocity</param>
        public virtual double GetHeatingSpeed(double torque, double velocity)
        {
            return torque * Hm + Math.Pow(velocity, 2) * Hv;
        }


        /// <summary>
        /// GetCoolingSpeed method counts
        /// engine cooling speed
        /// </summary>
        /// <param name="ambientTemperature">Value of ambient temperature</param>
        /// <param name="engineTemperature">Value of current engine temperature</param>
        public virtual double GetCoolingSpeed(double ambientTemperature, double engineTemperature)
        {
            return C * (ambientTemperature - engineTemperature);
        }


        /// <summary>
        /// GetCrankshaftAcceleration method counts
        /// crankshaft acceleration
        /// </summary>
        /// <param name="torque">Value of current torque</param>
        public virtual double GetCrankshaftAcceleration(double torque)
        {
            return torque / this.I;
        }


        /// <summary>
        /// GetSuperheatTime method counts
        /// time to superheat. 
        /// If the test time exceeds max time
        /// then test stop.
        /// </summary>
        /// <param name="testTime">Value of max time of test</param>
        public int GetSuperheatTime(int testTime)
        {
            int index = 0, time = 0;

            if (torqueList.Count != velocityList.Count) return -1;
            double torque = torqueList[index];
            double acceleration = GetCrankshaftAcceleration(torque);
            double velocity = 0;


            while (time <= testTime)
            {
                velocity += acceleration;

                if (index < torqueList.Count - 2)
                    if (velocity > velocityList[index + 1]) index++;

                torque = GetCurrentTorque(velocityList[index], torqueList[index], velocityList[index + 1], torqueList[index + 1], velocity);

                engineTemperature += GetHeatingSpeed(torque, velocity) + GetCoolingSpeed(ambientTemperature, engineTemperature);
                acceleration = GetCrankshaftAcceleration(torque);

                time++;
                if (engineTemperature >= superheatTemperature) break;
            }
            return time;
        }


        /// <summary>
        /// GetCurrentTorque method equation 
        /// of a straight line at two points.
        /// 
        /// (y - y1)/(y2 - y1) = (x - x1)/(x2 - x1)
        /// 
        /// </summary>
        /// <param name="v1">Value of velocityList[index]</param>
        /// <param name="t1">Value of torqueList[index]</param>
        /// <param name="v2">Value of velocityList[index + 1]</param>
        /// <param name="t2">Value of torqueList[index + 1]</param>
        /// <param name="v">Value of current velocity</param>
        private double GetCurrentTorque(double v1, double t1, double v2, double t2, double v) 
        {
            return (v - v1) / (v2 - v1) * (t2 - t1) + t1;
        }
    }
}
