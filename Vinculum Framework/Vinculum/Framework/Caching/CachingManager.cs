namespace Vinculum.Framework.Caching
{
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
    using System;
    using System.Reflection;
    using Vinculum.Framework.DataTypes;

    public class CachingManager
    {
        private CacheManager m_cacheManager;
        private string m_handle;

        public CachingManager(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            this.m_cacheManager = CacheFactory.GetCacheManager();
            this.m_handle = key + "-";
        }

        public CachingManager(string key, string manager)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (string.IsNullOrEmpty(manager))
            {
                throw new ArgumentNullException("manager");
            }
            this.m_cacheManager = CacheFactory.GetCacheManager(manager);
            this.m_handle = key;
        }

        public void Add(string key, object value)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                this.m_cacheManager.Add(this.m_handle + key, value);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, Constants.CACHE_EXCEPTION_POLICY))
                {
                    throw;
                }
            }
        }

        public void Add(string key, object value, CacheItemPriority scavendingPriority, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                this.m_cacheManager.Add(this.m_handle + key, value, scavendingPriority, refreshAction, expirations);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, Constants.CACHE_EXCEPTION_POLICY))
                {
                    throw;
                }
            }
        }

        public bool Contains(string key)
        {
            bool returnValue = false;
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                returnValue = this.m_cacheManager.Contains(this.m_handle + key);
            }
            catch (Exception ex)
            {
                Exception oex;
                if (ExceptionPolicy.HandleException(ex, Constants.CACHE_EXCEPTION_POLICY, out oex))
                {
                    throw oex;
                }
            }
            return returnValue;
        }

        public object GetData(string key)
        {
            object returnValue = null;
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                returnValue = this.m_cacheManager.GetData(this.m_handle + key);
            }
            catch (Exception ex)
            {
                Exception oex;
                if (ExceptionPolicy.HandleException(ex, Constants.CACHE_EXCEPTION_POLICY, out oex))
                {
                    throw oex;
                }
            }
            return returnValue;
        }

        public void Remove(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                this.m_cacheManager.Remove(this.m_handle + key);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, Constants.CACHE_EXCEPTION_POLICY))
                {
                    throw;
                }
            }
        }

        public object this[string key]
        {
            get
            {
                return this.m_cacheManager.GetData(this.m_handle + key);
            }
        }
    }
}

