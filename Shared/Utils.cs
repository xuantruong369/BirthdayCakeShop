public class Utils
{
    public static string FormatNumber(decimal value)
    {
        if (value >= 1000000000)
            return (value / 1000000000).ToString("0.#") + "B"; // Tỷ (Billion)
        if (value >= 1000000)
            return (value / 1000000).ToString("0.#") + "M";    // Triệu (Million)
        if (value >= 1000)
            return (value / 1000).ToString("0.#") + "K";       // Ngàn (Kilo)

        return value.ToString("N0"); // Dưới 1000 thì hiện số bình thường
    }

    public static decimal ScaleToK(decimal value)
    {
        if (value == 0) return 0;
        return value / 1000m;
    }

}