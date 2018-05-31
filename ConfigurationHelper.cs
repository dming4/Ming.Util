using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MCSoft.Application.Utility
{
    public class ConfigurationHelper
    {
        /// <summary>
        /// 获取配置信息，返回布尔型
        /// </summary>
        public static bool GetConfigBool(string key)
        {
            bool flag = false;
            string configString = GetConfig(key);
            if (!string.IsNullOrEmpty(configString))
            {
                try
                {
                    flag = bool.Parse(configString);
                }
                catch (Exception ex)
                {
                    EventLogHelper.WriteEventLog(ex.ToString());
                }
            }
            return flag;
        }
        /// <summary>
        /// 获取配置信息，返回数字
        /// </summary>
        public static decimal GetConfigDecimal(string key)
        {
            decimal num = 0M;
            string configString = GetConfig(key);
            if ((configString != null) && (string.Empty != configString))
            {
                try
                {
                    num = decimal.Parse(configString);
                }
                catch (Exception ex)
                {
                    EventLogHelper.WriteEventLog(ex.ToString());
                }
            }
            return num;
        }
        /// <summary>
        /// 获取配置信息，返回整形
        /// </summary>
        public static int GetConfigInt(string key)
        {
            int num = 0;
            string configString = GetConfig(key);
            if ((configString != null) && (string.Empty != configString))
            {
                try
                {
                    num = int.Parse(configString);
                }
                catch (Exception ex)
                {
                    EventLogHelper.WriteEventLog(ex.ToString());
                }
            }
            return num;
        }

        public static string GetConfig(string key)
        {
            string cacheKey = "AppSettings-" + key;
            object cache = CacheHelper.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = ConfigurationManager.AppSettings[key];
                    if (cache != null)
                    {
                        CacheHelper.SetCache(cacheKey, cache, DateTime.Now.AddMinutes(180.0), TimeSpan.Zero);
                    }
                }
                catch (Exception ex)
                {
                    EventLogHelper.WriteEventLog(ex.ToString());
                }
            }
            return cache.ToString();
        }
    }
}
