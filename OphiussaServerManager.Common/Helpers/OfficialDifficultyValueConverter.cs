using System;

namespace OphiussaServerManager.Common.Helpers {
    public static class OfficialDifficultyValueConverter
    {
        public  const double DINO_LEVELS = 30.0f;

        public static int Convert(float value)
        {
            var scaledValue = System.Convert.ToDouble(value);
            scaledValue = Math.Max(1.0, scaledValue);

            var sliderValue = scaledValue * DINO_LEVELS;
            sliderValue = Math.Max(DINO_LEVELS, sliderValue);

            return (int)sliderValue;
        }

        public static float ConvertBack(float value)
        {
            double sliderValue = System.Convert.ToDouble(value);
            sliderValue = Math.Max(DINO_LEVELS, sliderValue);

            double scaledValue = sliderValue / DINO_LEVELS;
            scaledValue = Math.Max(1.0, scaledValue);

            return (float)scaledValue;
        }
    }
}