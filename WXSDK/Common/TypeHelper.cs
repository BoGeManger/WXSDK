using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;

namespace WXSDK.Common
{
    public static class TypeHelper
    {
        /// <summary>
        /// 转换为 int，如果转换失败，返回 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToInt<T>(this T source)
        {
            bool flag = source == null;
            int result;
            if (flag)
            {
                result = 0;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = 0;
                }
                else
                {
                    int num;
                    bool flag3 = int.TryParse(source.ToString(), out num);
                    if (flag3)
                    {
                        result = num;
                    }
                    else
                    {
                        result = 0;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为 int?
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int? ToIntNull<T>(this T source)
        {
            bool flag = source == null;
            int? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    result = new int?(source.ToInt<T>());
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为 long，如果转换失败，返回 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToLong<T>(this T source)
        {
            bool flag = source == null;
            long result;
            if (flag)
            {
                result = 0L;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = 0L;
                }
                else
                {
                    long num;
                    bool flag3 = long.TryParse(source.ToString(), out num);
                    if (flag3)
                    {
                        result = num;
                    }
                    else
                    {
                        result = 0L;
                    }
                }
            }
            return result;
        }

        public static bool ToBool<T>(this T source)
        {
            bool flag = source == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    bool flag4;
                    bool flag3 = bool.TryParse(source.ToString(), out flag4);
                    result = (flag3 && flag4);
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为 decimal，如果转换失败，返回 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToDecimal<T>(this T source)
        {
            bool flag = source == null;
            decimal result;
            if (flag)
            {
                result = decimal.Zero;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = decimal.Zero;
                }
                else
                {
                    decimal num;
                    bool flag3 = decimal.TryParse(source.ToString(), out num);
                    if (flag3)
                    {
                        result = num;
                    }
                    else
                    {
                        result = decimal.Zero;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为 decimal?
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal? ToDecimalNull<T>(this T source)
        {
            bool flag = source == null;
            decimal? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    result = new decimal?(source.ToDecimal<T>());
                }
            }
            return result;
        }

        /// <summary>
        /// 格式化金额
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static decimal FormatAmount<T>(this T amount)
        {
            decimal amt = amount.ToDecimal();
            return FormatAmount(amt, 2);
        }

        public static decimal FormatAmount(decimal amount, int decimals)
        {
            if (decimals >= 0)
            {
                return Math.Round(amount, decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                int n = (int)Math.Pow(10, decimals * -1);
                return Math.Round(amount / n, 0, MidpointRounding.AwayFromZero) * n;
            }
        }

        /// <summary>
        /// 转换为字符串，会做处理将单引号'转换为两个单引号''
        /// 会进行Anti XSS消毒处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStr<T>(this T source)
        {
            bool flag = source == null;
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = string.Empty;
                }
                else
                {
                    result = Convert.ToString(source);
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为日期，如果转换失败，返回 null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime? ToDateNull<T>(this T source)
        {
            bool flag = source == null;
            DateTime? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    DateTime value;
                    bool flag3 = DateTime.TryParse(source.ToString(), out value);
                    if (flag3)
                    {
                        result = new DateTime?(value);
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为日期，如果转换失败，返回Now
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateNow<T>(this T source)
        {
            DateTime result;
            if (source == null)
            {
                result = DateTime.Now;
            }
            else
            {
                try
                {
                    result = Convert.ToDateTime(source);
                }
                catch
                {
                    result = DateTime.Now;
                }
            }
            return result;
        }

        public static Guid ToGuid(this object obj)
        {
            bool flag = obj == null || obj.ToString().IsEmpty();
            Guid result;
            if (flag)
            {
                result = Guid.Empty;
            }
            else
            {
                Guid guid;
                bool flag2 = Guid.TryParse(obj.ToString(), out guid);
                if (flag2)
                {
                    result = guid;
                }
                else
                {
                    result = Guid.Empty;
                }
            }
            return result;
        }

        public static Guid? ToGuidNull(this object obj)
        {
            bool flag = obj == null || obj.ToString().IsEmpty();
            Guid? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                Guid value;
                bool flag2 = Guid.TryParse(obj.ToString(), out value);
                if (flag2)
                {
                    result = new Guid?(value);
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }

        public static T ToEnum<T>(this object obj) where T : struct
        {
            bool flag = !typeof(T).IsEnum;
            if (flag)
            {
                throw new ArgumentException("Type must is a enum");
            }
            bool flag2 = obj == null || obj == DBNull.Value || obj.ToString() == string.Empty;
            T result;
            if (flag2)
            {
                result = default(T);
            }
            else
            {
                T t = default(T);
                Enum.TryParse<T>(obj.ToStr<object>(), out t);
                result = t;
            }
            return result;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsId(this int source)
        {
            return source > 0;
        }

        public static bool EqualsIgnoreCase(this string str, string compareStr)
        {
            return str.ToStr<string>().Equals(compareStr, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsGuid(this string str)
        {
            Match match = Regex.Match(str, "^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", RegexOptions.IgnoreCase);
            bool success = match.Success;
            bool result;
            if (success)
            {
                try
                {
                    Guid guid = new Guid(str);
                    result = true;
                    return result;
                }
                catch
                {
                    result = false;
                    return result;
                }
            }
            result = false;
            return result;
        }

        /// <summary>
        /// 复制行集合到 Datable
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable CopyToDataTable(this IEnumerable<DataRow> rows, DataTable table)
        {
            bool flag = rows == null;
            DataTable result;
            if (flag)
            {
                result = table.Clone();
            }
            else
            {
                result = (rows.Any<DataRow>() ? rows.CopyToDataTable<DataRow>() : table.Clone());
            }
            return result;
        }

        /// <summary>
        /// 转换为 int，如果转换失败，返回 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToSafeSqlInt<T>(this T source)
        {
            bool flag = source == null;
            int result;
            if (flag)
            {
                result = 0;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = 0;
                }
                else
                {
                    int num;
                    bool flag3 = int.TryParse(source.ToString(), out num);
                    if (flag3)
                    {
                        result = num;
                    }
                    else
                    {
                        result = 0;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为 decimal，如果转换失败，返回 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToSafeSqlDecimal<T>(this T source)
        {
            bool flag = source == null;
            decimal result;
            if (flag)
            {
                result = decimal.Zero;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = decimal.Zero;
                }
                else
                {
                    decimal num;
                    bool flag3 = decimal.TryParse(source.ToString(), out num);
                    if (flag3)
                    {
                        result = num;
                    }
                    else
                    {
                        result = decimal.Zero;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为字符串，会做处理将单引号'转换为两个单引号''
        /// 会进行Anti XSS消毒处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToSafeSqlStr<T>(this T source)
        {
            bool flag = source == null;
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = string.Empty;
                }
                else
                {
                    result = Convert.ToString(source).Replace("'", "''");
                }
            }
            return result;
        }

        /// <summary>
        /// 转换为日期，如果转换失败，返回 null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime? ToSafeDateNull<T>(this T source)
        {
            bool flag = source == null;
            DateTime? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = source.Equals(DBNull.Value);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    DateTime value;
                    bool flag3 = DateTime.TryParse(source.ToString(), out value);
                    if (flag3)
                    {
                        result = new DateTime?(value);
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取字典的值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
        {
            bool flag = source.ContainsKey(key);
            TValue result;
            if (flag)
            {
                result = source[key];
            }
            else
            {
                result = default(TValue);
            }
            return result;
        }

        /// <summary>
        /// Guid转换为大写的字符串当
        /// 数据库为Oracle时会使用到
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToUpperString(this Guid source)
        {
            return source.ToString().ToUpper();
        }

        /// <summary>
        ///实体拷贝,用","号分隔忽略属性
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        /// <param name="ignorePoperties"></param>
        /// <returns></returns>
        public static TTarget EntityCopy<TSource, TTarget>(TSource source, TTarget target, string ignorePoperties)
        {
            List<string> ignoreP = new List<string>();
            if (!string.IsNullOrEmpty(ignorePoperties))
            {
                ignoreP = ignorePoperties.ToLower().Split(',').ToList();
            }
            var tFields = target.GetType().GetProperties();
            var sFields = source.GetType().GetProperties();

            foreach (var item in tFields)
            {
                if (!ignoreP.Contains(item.Name.ToLower()))
                {
                    foreach (var si in sFields)
                    {
                        if (si.Name == item.Name)
                        {
                            object svalue = si.GetValue(source, null);
                            object tvalue = item.GetValue(target, null);
                            if (svalue != null && !svalue.Equals(tvalue))
                            {
                                item.SetValue(target, svalue, null);
                            }
                        }
                    }
                }
            }
            return target;
        }



        /// <summary>
        ///用","号分隔忽略属性
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        /// <returns></returns>
        public static TTarget EntityCopy<TSource, TTarget>(TSource source, TTarget target)
        {
            return EntityCopy(source, target, "");

        }
    }
}
