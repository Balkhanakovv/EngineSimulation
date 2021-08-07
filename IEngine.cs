namespace Engine_simulation
{
    /// <summary>
    ///  IEngine interface is used to implement engine parameters.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// GetHeatingSpeed() method 
        /// should return the heating rate of the engine.
        /// </summary>
        /// <param name="torque">Value of torque</param>
        /// /// <param name="velocity">Value of crankshaft rotation velocity</param>
        double GetHeatingSpeed(double torque, double velocity);


        /// <summary>
        /// GetCoolingSpeed() method 
        /// should return the cooling rate of the engine.
        /// </summary>
        /// <param name="ambientTemperature">Value of ambient temperature</param>
        /// <param name="engineTemperature">Value of engine temperature</param>
        double GetCoolingSpeed(double ambientTemperature, double engineTemperature);


        /// <summary>
        /// GetCrankshaftAcceleration() method 
        /// should return the acceleration of crankshaft.
        /// </summary>
        /// <param name="torque">Value of torque</param>
        double GetCrankshaftAcceleration(double torque);


        /// <summary>
        /// GetSuperheatTime() method 
        /// should return time to superheat.
        /// </summary>
        /// <param name="testTime">Value of max time of test</param>
        int GetSuperheatTime(int testTime);
    }
}
