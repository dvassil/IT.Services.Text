using System;
//using System.Linq;
using System.Text;
using System.Globalization;

namespace IT.Services.Text
{
    public static class Written
    {
        internal static string GetSubtext(double money, int index, bool showCurrency, SexForm form)
        {
            int iform = (int)form;
            if (index == 2 && money == 1)
            {
                return Strings.spc[iform, 3];
            }

            int dekmon      = (int)(money % 100);
            int monades     = (int)(dekmon % 10);
            int ekatontades = (int)Math.Floor(money / 100);
            int dekades     = (int)(dekmon / 10);

            StringBuilder str = new StringBuilder();

            //ΕΚΑΤΟΝΤΑΔΕΣ
            if (money == 100)
            {
                str.Append(Strings.spc[iform, 1]);
            }
            else if (money >= 200)
            {
                if      (index == 1) { str.Append(Strings.e[iform,                ekatontades]); }
                else if (index == 2) { str.Append(Strings.e[(int)SexForm.Female,  ekatontades]); }
                else                 { str.Append(Strings.e[(int)SexForm.Neutral, ekatontades]); }
            }
            else if (money > 100)
            {
                str.Append(Strings.spc[iform, 2]);
            }

            //ΔΕΚΑΔΕΣ
            switch (dekmon)
            {
                case 10:
                case 11:
                case 12:
                    str.Append(Strings.dm[iform, monades]);
                    monades = 0;
                    break;
                case 13:
                    if      (index == 1) { str.Append(Strings.dm[iform,                monades]); monades = 0; }
				    else if (index == 2) { str.Append(Strings.dm[(int)SexForm.Female,  monades]); monades = 0; }
                    else                 { str.Append(Strings.dm[(int)SexForm.Neutral, monades]); }
                    //else                 { str.Append(Strings.d[dekades]); }
                    break;
                default:
                    str.Append(Strings.d[dekades]);
                    break;
            }

            //ΜΟΝΑΔΕΣ
            if (index == 0 || index == 1)
            {
                str.Append(Strings.m[iform, monades]);
            }
            else if (index == 2)
            {
                str.Append(Strings.m[(int)SexForm.Female, monades]);
            }
            else
            {
                if (dekmon < 10 || dekmon > 12) { str.Append(Strings.m[(int)SexForm.Neutral, monades]); }
            }

            if ((str.Length > 0) || index == 1)
            {
                if (index == 0 && money == 1)
                {
                    if (showCurrency)
                    {
                        /*str.Append(Strings.spc[iform, 0]);*/
                        str.Append(Strings.idx[0]);
                    }
                }
                else
                {
                    if (index > 1 || showCurrency)
                    //if (index > 1)
                    {
                        str.Append(Strings.idx[index]);
                        if (index == 0 || index == 1)
                        {
                            if        (money != 1) { str.Append(Strings.idx[index]); }
                            //else if (money == 1)     { str.Append(Strings.idx[index]); }
                        }
                        //else
                        //{
                        //    str.Append(Strings.idx[index]);
                        //}

                        if (index > 2)
                        {
                            if (index >  5) { str.Append(Strings.spc[iform, 4]); }
                            if (index >  3) { str.Append(Strings.idx[3]); }
                            if (money >= 2)
                            {
                                str.Append(Strings.suffix[1]);
                            }
                            else
                            {
                                str.Append(Strings.suffix[0]);
                            }
                        }
                    }
                }
            }

            return str.ToString();
        }

        public static bool ValidValueFormat(string value)
        {
            return System.Text.RegularExpressions.Regex.Match(value, @"^[+-]?(\d*(\.\d+)?|\d+(\.\d*)?)$").Success;
        }

        public static string GetText(double money, SexForm form, bool showZero = true, bool showCurrency = false)
        {
            long value;
            int index         = 0;
            bool isZero       = true;
            bool isNegative   = false;
            StringBuilder str = new StringBuilder();

            if (money < 0)
            {
                money      = -money;
                isNegative = true;
            }

            if (money != Math.Floor(money))
            {
                value = (long)Math.Round(100 * money - 100 * Math.Floor(money), 0);
                if (value >= 100)
                {
                    value -= 100;
                    money += 1.0;
                }

                money = Math.Floor(money);
                if (value > 0)
                {
                    isZero = false;

                    if (money >= 1) { str.Append(Strings.prefix[2]); }
                    str.Append(GetSubtext(value, index, showCurrency, form));
                }
            }

            while (money >= 1E+17)
            {
                double money2 = Math.Floor(money / 1000);
                value         = (long)Math.Floor(1000 * (money - money2));
                money         = money2;
                index         += 1;
                isZero        = false;

                if (index > 1 && value == 0) continue;

                str.Append(GetSubtext(value, index, showCurrency, form));
            }

            long longMoney = (long)Math.Floor(money);
            while (longMoney >= 1)
            {
                value     = (longMoney % 1000);
                longMoney /= 1000;
                index     += 1;
                isZero    = false;
                if (index > 1 && value == 0)
                    continue;

                str.Insert(0, GetSubtext(value, index, showCurrency, form));
            }

            StringBuilder outStr = new StringBuilder();
            if (isZero) { if (showZero)   outStr.Append(Strings.prefix[1]); }
            else        { if (isNegative) outStr.Append(Strings.prefix[0]); }

            outStr.Append(str);

            return outStr.ToString().Trim();
        }

        public static string GetText(string numstr, SexForm form, bool showZero = true, bool showCurrency = false)
        {
            double value;
            bool isZero = true;
            bool isNegative = false;
            int index = 0;
            int carry = 0;

            StringBuilder str = new StringBuilder();
            StringBuilder tmp = new StringBuilder();

            char szSep = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            int pch = numstr.IndexOf(szSep);

            bool additionalFloatingValue = false;

            if (pch >= 0)
            {
                tmp.Append('.').Append(numstr.Substring(pch + 1, Math.Min(3, numstr.Length-pch-1)));
                value = Convert.ToDouble(tmp.ToString());
                value = Math.Round(100 * value);
                if (value >= 100)
                {
                    value -= 100;
                    carry = 1;
                }

                if (value > 0)
                {
                    isZero = false;
                    additionalFloatingValue = true;
                    str.Append(GetSubtext((double)value, index, showCurrency, form));
                }
            }
            else
            {
                int len = numstr.Length;
                if (len >= 67)
                {
                    return string.Empty; //L"\u221E";
                }
                pch = len;
            }

            StringBuilder str1 = new StringBuilder();
            int currencyitems = 0;
            while (pch > 0)
            {
                int        k = pch;
                if (k > 3) k = 3;
                pch -= k;
                tmp.Remove(0, tmp.Length);
                tmp.Append(numstr.Substring(pch, k));
                value = Convert.ToDouble(tmp.ToString()) + carry;
                carry = (int)value / 1000;
                value = (int)value % 1000;

                index += 1;

                //if (index > 1 && value == 0) continue;
                if (value == 0) continue;

                isZero = false;
                if (value < 0)
                {
                    value      = -value;
                    isNegative = true;
                }

                if (additionalFloatingValue)
                {
                    str.Insert(0, Strings.prefix[2]);
                    additionalFloatingValue = false;
                }

                currencyitems += (int)value;

                str1.Insert(0, GetSubtext((double)value, index, false, form));
            }

            if (isZero) {
                if (showZero)   str1.Append(Strings.prefix[1]).Append(Strings.idx[index]).Append(Strings.idx[index]);
            }
            else {
                if (isNegative) str1.Insert(0, Strings.prefix[0]);
            }

            if (showCurrency)
            {
                str1.Append(Strings.idx[1])
                    .Append((currencyitems > 1) ? Strings.idx[1] : string.Empty);
            }
            str1.Append(str);

            return str1.ToString().Trim();
        }

    }
}