using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFormatter {

    public static string Format(double input) {
        int log = (int)Mathf.Log10((float)input);
        int numThousands = log / 3;
        int trailingDigits = TrailingDigits(log);
        string cap = Units(numThousands);

        float divisor = Mathf.Pow(1000f, numThousands);
        float formattableNumber = (float)(input / divisor);
        return formattableNumber.ToString(DecimalFormat(trailingDigits)) + cap;
    }

    private static string Units(int numDivisions) {
        switch (numDivisions) {
            case 0: return "";
            case 1: return "k";
            case 2: return "m";
            case 3: return "b";
            case 4: return "t";
            case 5: return "qa";
            case 6: return "qi";
            default: return "u";
        }
    }

    private static int TrailingDigits(int log) {
        if (log < 3) return 0;
        return 2 - (log % 3);
    }

    private static string DecimalFormat(int trailingDigits) {
        switch (trailingDigits) {
            case 0: return "0";
            case 1: return "0.0";
            case 2: return "0.00";
            default: return "0";
        }
    }
}
