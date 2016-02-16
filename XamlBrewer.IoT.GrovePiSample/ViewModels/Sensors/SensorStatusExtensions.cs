using GrovePi.Sensors;

namespace XamlBrewer.IoT.Sensors
{
    public static class SensorStatusExtensions
    {
        public static string AsMotionState(this SensorStatus status)
        {
            switch (status)
            {
                case SensorStatus.Off:
                    return "All quiet";
                case SensorStatus.On:
                    return "Motion detected";
                default:
                    return "Confused";
            }
        }
    }
}
