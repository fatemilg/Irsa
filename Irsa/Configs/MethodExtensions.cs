using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Irsa.Configs
{
    public static class MethodExtensions
    {
        /// <summary>
        /// Convert DateTime to Shamsi Date (YYYY/MM/DD)
        /// </summary>
        public static string ToShamsiDateYMD(this DateTime date)
        {
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            int intYear = PC.GetYear(date);
            int intMonth = PC.GetMonth(date);
            int intDay = PC.GetDayOfMonth(date);
            return (intYear.ToString() + "/" + intMonth.ToString() + "/" + intDay.ToString());
        }

        public static string get_val(this JToken json, string key)
        {
            try
            {
                if (key.Contains(".")) return json.SelectToken(key).ToString();

                if (json[key] == null) return "";

                return json[key].ToString();
            }
            catch (Exception)
            {
                return "";
            }


        }

        public static string FixAddess(this string input)
        {
            if (input == null)
                return null;
            return input.Fix().Replace("_", " - ").Replace("،", " - ").Replace(",", " - ").Replace(".", " - ");
        }

        public static string RemoveNumbers(this string input)
        {
            if (input == null)
                return null;

            var output = input;

            output = output
                .Replace("۰", "")
                .Replace("۱", "")
                .Replace("۲", "")
                .Replace("۳", "")
                .Replace("۴", "")
                .Replace("۵", "")
                .Replace("۶", "")
                .Replace("۷", "")
                .Replace("۸", "")
                .Replace("۹", "")
                .Replace("٠", "")
                .Replace("١", "")
                .Replace("٢", "")
                .Replace("٣", "")
                .Replace("٤", "")
                .Replace("٥", "")
                .Replace("٦", "")
                .Replace("٧", "")
                .Replace("٨", "")
                .Replace("٩", "")
                .Replace("0", "")
                .Replace("1", "")
                .Replace("2", "")
                .Replace("3", "")
                .Replace("4", "")
                .Replace("5", "")
                .Replace("6", "")
                .Replace("7", "")
                .Replace("8", "")
                .Replace("9", "");
            return output;
        }

        public static string Fix(this string input)
        {
            if (input == null)
                return null;

            var output = input;

            output = output
                .Replace("‎", "")//its a hidden character
                .Replace("‎", "")//ltr code
                .Replace("‎", "")//rtl code
                .Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9");
            return output.CorrectPersianUnicode();
        }

        public static string[] Split(this string text, string splitter)
        {
            if (text.Contains(splitter))
                return text.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
            else return new string[] { text };
        }


        #region "[ Persian Alphabet ]"

        private static string[] _digitNames = new string[1000];

        private const string PersianAnd = "و";

        public static string ToPersianAlphabet(this long value)
        {
            //Build array for number to words mapping
            BuildMapping();

            long tempNumber = 0;
            string result = string.Empty;

            if (value > 999999999999999L)
            {
                throw new ArgumentOutOfRangeException("Number is too large to process");
            }

            if (value > 999999999999L)
            {
                tempNumber = value / 1000000000000L;
                result += GetHundredPart(tempNumber) + " " + _digitNames[32];
                value = value - (tempNumber * 1000000000000L);

                if (value <= 0)
                {
                    return result;
                }
                else
                {
                    result += string.Format(" {0} ", PersianAnd);
                }
            }

            if (value > 999999999)
            {
                tempNumber = value / 1000000000;
                result += GetHundredPart(tempNumber) + " " + _digitNames[31];
                value = value - (tempNumber * 1000000000);

                if (value <= 0)
                {
                    return result;
                }
                else
                {
                    result += string.Format(" {0} ", PersianAnd);
                }
            }

            if (value > 999999)
            {
                tempNumber = value / 1000000;
                result += GetHundredPart(tempNumber) + " " + _digitNames[30];
                value = value - (tempNumber * 1000000);

                if (value <= 0)
                {
                    return result;
                }
                else
                {
                    result += string.Format(" {0} ", PersianAnd);
                }
            }

            if (value > 999)
            {
                tempNumber = value / 1000;
                result += GetHundredPart(tempNumber) + " " + _digitNames[29];
                value = value - (tempNumber * 1000);

                if (value <= 0)
                {
                    return result;
                }
                else
                {
                    result += string.Format(" {0} ", PersianAnd);

                }
            }

            if (value > 0)
            {
                result += GetHundredPart(value);
            }

            return result;
        }

        private static void BuildMapping()
        {
            _digitNames[1] = "یک";
            _digitNames[2] = "دو";
            _digitNames[3] = "سه";
            _digitNames[4] = "چهار";
            _digitNames[5] = "پنج";
            _digitNames[6] = "شش";
            _digitNames[7] = "هفت";
            _digitNames[8] = "هشت";
            _digitNames[9] = "نه";
            _digitNames[10] = "ده";
            _digitNames[11] = "یازده";
            _digitNames[12] = "دوازده";
            _digitNames[13] = "سیزده";
            _digitNames[14] = "چهارده";
            _digitNames[15] = "پانزده";
            _digitNames[16] = "شانزده";
            _digitNames[17] = "هفده";
            _digitNames[18] = "هجده";
            _digitNames[19] = "نوزده";
            _digitNames[20] = "بیست";
            _digitNames[21] = "سی";
            _digitNames[22] = "چهل";
            _digitNames[23] = "پنجاه";
            _digitNames[24] = "شصت";
            _digitNames[25] = "هفتاد";
            _digitNames[26] = "هشتاد";
            _digitNames[27] = "نود";
            _digitNames[28] = "صد";
            _digitNames[29] = "هزار";
            _digitNames[30] = "میلیون";
            _digitNames[31] = "میلیارد";
            _digitNames[32] = "تریلیون";
            _digitNames[100] = "یکصد";
            _digitNames[200] = "دویست";
            _digitNames[300] = "سیصد";
            _digitNames[400] = "چهارصد";
            _digitNames[500] = "پانصد";
            _digitNames[600] = "ششصد";
            _digitNames[700] = "هفتصد";
            _digitNames[800] = "هشتصد";
            _digitNames[900] = "نهصد";
        }

        private static string GetHundredPart(long Number)
        {
            var intValue = Convert.ToInt32(Number);
            int t = 0;
            var result = string.Empty;

            if (intValue > 999)
            {
                throw new ArgumentOutOfRangeException("Number is larger than 999");
            }

            if (intValue > 99)
            {
                t = intValue / 100;
                switch (t)
                {
                    case 1:
                        result = _digitNames[100];
                        break;
                    case 2:
                        result = _digitNames[200];
                        break;
                    case 3:
                        result = _digitNames[300];
                        break;
                    case 4:
                        result = _digitNames[400];
                        break;
                    case 5:
                        result = _digitNames[500];
                        break;
                    case 6:
                        result = _digitNames[600];
                        break;
                    case 7:
                        result = _digitNames[700];
                        break;
                    case 8:
                        result = _digitNames[800];
                        break;
                    case 9:
                        result = _digitNames[900];
                        break;
                }

                intValue = intValue - (t * 100);

                if (intValue <= 0)
                {
                    return result;
                }
                else
                {
                    result += string.Format(" {0} ", PersianAnd);

                }
            }

            if (intValue > 20)
            {
                t = intValue / 10;
                result = result + _digitNames[t + 18];
                intValue = intValue - (t * 10);

                if (intValue <= 0)
                {
                    return result;
                }
                else
                {
                    result += string.Format(" {0} ", PersianAnd);

                }
            }

            if (intValue > 0)
            {
                result += _digitNames[intValue];
            }

            return result;
        }

        #endregion

        #region "[ Date ]"

        public static string ToShortDateStringRTL(this DateTime _date)
        {
            var d = _date.ToShortDateString().Split('/');
            return string.Format("{2}/{1}/{0}", d[0], d[1], d[2]);
        }

        public static DateTime? ParseGaregorian(this string date)
        {
            if (date.HasText())
            {
                if (date.Length < 10)
                    throw new Exception("Invalid date length " + date);
                date = date.Substring(0, 10);
                var date_parts = date.Split(new char[] { '/', '-' });
                if (date_parts.Length != 3)
                    throw new Exception("Invalid date length " + date);
                return new DateTime(int.Parse(date_parts[0]), int.Parse(date_parts[1]), int.Parse(date_parts[2]));
            }
            return null;
        }

        public static double ToUnixTimeStamp(this DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return span.TotalSeconds * 1000;
        }

        public static string ToGaregorianDateString(this DateTime value)
        {
            return string.Format("{0}/{1}/{2}", value.Year, value.Month, value.Day);
        }

        public static string ToGaregorianDateTimeString(this DateTime value)
        {
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                value.Year, value.Month, value.Day,
                value.Hour.ToString("00"), value.Minute.ToString("00"), value.Second.ToString("00"));
        }

        #endregion

        #region "[ String ]"

      
        /// <summary>
        /// Get string as Integer
        /// </summary>
        /// <returns>If string has any value except Integer, this funciton return Nothing instead</returns>
        public static int? GetInteger(this string s, int? default_value = null)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return default_value;
                }
                int value = -1;
                if (int.TryParse(s, out value))
                {
                    return value;
                }
                return default_value;

            }
            catch
            {
                return default_value;
            }
        }

        /// <summary>
        /// Get string as Double
        /// </summary>
        /// <returns>If string has any value except Double, this funciton return Nothing instead</returns>
        public static double? GetDouble(this string s)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return null;
                }
                double value = -1;
                if (double.TryParse(s, out value))
                {
                    return value;
                }
                return null;

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get string as Long
        /// </summary>
        /// <returns>If string has any value except Long, this funciton return Nothing instead</returns>
        public static long? GetLong(this string s)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return null;
                }
                long value = -1;
                if (long.TryParse(s, out value))
                {
                    return value;
                }
                return null;

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get string as Decimal
        /// </summary>
        /// <returns>If string has any value except Decimal, this funciton return Nothing instead</returns>
        public static decimal? GetDecimal(this string s)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return null;
                }
                decimal value = -1;
                if (decimal.TryParse(s, out value))
                {
                    return value;
                }
                return null;

            }
            catch
            {
                return null;
            }
        }


        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// Not String.IsNullOrWhiteSpace(s)
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Return Not String.IsNullOrWhiteSpace(s)</returns>
        /// <remarks></remarks>
        public static bool HasText(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static bool HasNotText(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

       
        public static bool IsNumeric(this string s)
        {
            int n;
            return int.TryParse("123", out n);
        }

        public static bool IsNumber(this string s, bool BiggerThanZero)
        {
            if (s.IsNullOrWhiteSpace())
                return false;
            s = s.Trim();
            bool returnValue = IsNumeric(s);
            if (BiggerThanZero && returnValue)
            {
                returnValue = Convert.ToInt64(s) > 0;
            }
            return returnValue;
        }

        public static string Left(this string s, int lenght)
        {
            return s.Substring(0, lenght);
        }

        public static string Right(this string s, int lenght)
        {
            return s.Substring(s.Length - lenght, lenght);
        }

        public static string CorrectPersianUnicode(this string text)
        {
            return text.Replace("ي", "ی").Replace("ك", "ک");
        }

        public static string ConvertToArabicKeyboard(this string text)
        {
            return text.Replace("ی", "ي").Replace("ک", "ك");
        }

        #endregion

     

        #region "[ Object ]"

        public static bool IsNull(this object obj)
        {
            return (obj == null || DBNull.Value == obj);
        }

        public static bool IsNotNull(this object obj)
        {
            return (obj != null);
        }

        /// <summary>
        /// check object nullabality and return value for nul or not nul object
        /// </summary>
        /// <param name="obj">object to check</param>
        /// <param name="truePart">result if object is null</param>
        /// <param name="falsePart">result if object is not null</param>
        public static object IfNull(this object obj, object truePart, object falsePart)
        {
            if (IsNull(obj))
            {
                return truePart;
            }
            else
            {
                return falsePart;
            }
        }

        /// <summary>
        /// if object value is not null, it's return object itself and if object is null it's return a newVlaue as object value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="newValue">Replaced value if object is null</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static object OrIfNull(this object obj, object newValue)
        {
            if (IsNull(obj))
            {
                return newValue;
            }
            else
            {
                return obj;
            }
        }

        

        public static string GetKnowledgeBaseKey(this object obj)
        {

            dynamic type = obj.GetType();
            if (typeof(Enum).IsAssignableFrom(type))
            {
                dynamic typeName = type.Name;
                if (typeName.ToLower.EndsWith("service"))
                {
                    typeName = typeName.Substring(0, typeName.Length - 7);
                }
                return typeName + "." + System.Enum.GetName(type, obj);
            }

            throw new NotImplementedException();

        }

        public static string GetKnowledgeBaseValue(this object obj)
        {
            return obj.ToString();
            //TODO
            //return KnowledgeBaseProvider.GetItem(GetKnowledgeBaseKey(obj)).Value;
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            var names = propertyName.Trim().Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var resultObject = obj;

            System.Reflection.PropertyInfo prop = null;
            foreach (var pName in names)
            {
                prop = resultObject.GetType().GetProperty(pName);
                resultObject = prop.GetValue(resultObject, null);
            }
            return resultObject;

        }


        #endregion


        #region "[ DateTime ]"


        private static System.Globalization.PersianCalendar pcal = new System.Globalization.PersianCalendar();

        public static string ToSQLString(this DateTime _date)
        {
            return string.Format("{0}-{1}-{2}", _date.Year, _date.Month.ToString("00"), _date.Day.ToString("00"));
            //Return _date.ToString("yyyy-MM-dd")
        }

        public enum EmptyDateReturnType
        {
            MinDateValue = 1,
            MaxDateValue = 2,
            EmptyString = 3
        }

        public static string ToSQLString(this DateTime? _date, EmptyDateReturnType nullModeReturnValue)
        {
            if (_date.HasValue)
            {
                return _date.Value.ToSQLString();
            }
            else
            {
                switch (nullModeReturnValue)
                {
                    case EmptyDateReturnType.MaxDateValue:
                        return System.DateTime.MaxValue.ToSQLString();
                    case EmptyDateReturnType.MinDateValue:
                        return "1800-01-01";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary> 
        /// تبدیل تاریخ میلادی به تاریخ شمسی با فرمت راست به چپ 
        /// </summary> 
        /// <param name="gregorianDate">تاریخ میلادی</param> 
        /// <returns>تاریخ شمسی</returns> 
        /// 
        public static string ToPersianDate(this DateTime gregorianDate, bool YMD_format = true)
        {
            dynamic day = pcal.GetDayOfMonth(gregorianDate).ToString("00");
            dynamic month = pcal.GetMonth(gregorianDate).ToString("00");
            dynamic year = pcal.GetYear(gregorianDate);
            if (YMD_format)
                return string.Format("{0}/{1}/{2}", year, month, day);
            else
                return string.Format("{0}/{1}/{2}", day, month, year);
        }

       

        /// <summary> 
        /// دریافت تاریخ و ساعت شمسی از تاریخ میلادی 
        /// </summary> 
        /// <param name="gregorianDate">تاریخ میلادی</param> 
        /// <returns></returns> 
        /// 
        public static string ToPersianDateTime(this DateTime gregorianDate, bool show_time_first = false, bool YMD_format = true)
        {
            if (show_time_first)
                return gregorianDate.Hour.ToString("00") + ":" + gregorianDate.Minute.ToString("00") + " " + ToPersianDate(gregorianDate, YMD_format);
            else
                return ToPersianDate(gregorianDate, show_time_first) + " " + gregorianDate.Hour.ToString("00") + ":  " + gregorianDate.Minute.ToString("00");
        }

        /// <summary> 
        /// دریافت عدد ماه شمسی 
        /// </summary> 
        /// <param name="gregorianDate">تاریخ میلادی</param> 
        /// <returns>Persian month no</returns> 
        public static int GetPersianMonth(this DateTime gregorianDate)
        {
            return pcal.GetMonth(gregorianDate);
        }

        public static System.DateTime StartOfDay(this DateTime _date)
        {
            return new System.DateTime(_date.Year, _date.Month, _date.Day, 0, 0, 1);
        }

        public static System.DateTime EndOfDay(this DateTime _date)
        {
            return new System.DateTime(_date.Year, _date.Month, _date.Day, 23, 59, 59);
        }

        #endregion

        #region "[ Digital Grouping ]"

        //int
        public static string ToDigitalGroupString(this int? value, int decimalDigits = 0)
        {
            if (!value.HasValue)
                throw new Exception("Object not set to an instance of an object");
            return Convert.ToDecimal(value.Value).ToDigitalGroupString();
        }
        public static string ToDigitalGroupString(this int value, int decimalDigits = 0)
        {
            return Convert.ToDecimal(value).ToDigitalGroupString();
        }

        //double
        public static string ToDigitalGroupString(this double? value, int decimalDigits = 0)
        {
            if (!value.HasValue)
                throw new Exception("Object not set to an instance of an object");
            return value.Value.ToDigitalGroupString();
        }
        public static string ToDigitalGroupString(this double value, int decimalDigits = 0)
        {
            return Convert.ToDecimal(value).ToDigitalGroupString();
        }

        //long
        public static string ToDigitalGroupString(this long? value, int decimalDigits = 0)
        {
            if (!value.HasValue)
                throw new Exception("Object not set to an instance of an object");
            return value.Value.ToDigitalGroupString();
        }
        public static string ToDigitalGroupString(this long value, int decimalDigits = 0)
        {
            return Convert.ToDecimal(value).ToDigitalGroupString();
        }

        //decimal
        public static string ToDigitalGroupString(this decimal? value, int decimalDigits = 0, decimal? default_value = null)
        {
            if (!value.HasValue)
                if (default_value.HasValue)
                    default_value.Value.ToDigitalGroupString();
                else
                    throw new Exception("Object not set to an instance of an object");
            return value.Value.ToDigitalGroupString();
        }

        public static string ToDigitalGroupString(this decimal value, int decimalDigits = 0)
        {
            string decimalPoints = "";
            if (decimalDigits > 0)
            {
                decimalPoints = ".";
                for (int i = 1; i <= decimalDigits; i++)
                {
                    decimalPoints += "0";
                }
            }
            return value.ToString("#,0" + decimalPoints);
        }

        #endregion

        


    }
}
