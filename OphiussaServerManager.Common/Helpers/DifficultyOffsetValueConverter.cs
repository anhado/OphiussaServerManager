using System;

namespace OphiussaServerManager.Common.Helpers {
    public static class DifficultyOffsetValueConverter {
        public const double MinValue = 50;
        public const double MaxValue = 400;

        public static double Convert(double value) {
            double scaledValue = System.Convert.ToDouble(value);
            ;
            double sliderValue = MinValue + scaledValue * (MaxValue - MinValue);
            sliderValue = Math.Max(MinValue, sliderValue);
            return sliderValue;
        }

        public static double ConvertBack(double value) {
            double sliderValue = System.Convert.ToDouble(value);
            sliderValue = sliderValue - MinValue;
            double scaledValue = sliderValue / (MaxValue - MinValue);
            scaledValue = Math.Max(0.01, scaledValue);
            return scaledValue;
        }
    }
}