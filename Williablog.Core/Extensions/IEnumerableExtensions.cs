﻿namespace Williablog.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Merges two NameValueCollections.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <remarks>Used by <see cref="Williablog.Core.Configuration.ConfigSystem">ConfigSystem</c> to merge AppSettings</remarks>
        public static NameValueCollection Merge(this NameValueCollection first, NameValueCollection second)
        {
            if (second == null)
            {
                return first;
            }

            foreach (string item in second)
            {
                if (first.AllKeys.Contains(item))
                {
                    // if first already contains this item, update it to the value of second
                    first[item] = second[item];
                }
                else
                {
                    // otherwise add it
                    first.Add(item, second[item]);
                }
            }

            return first;
        }

        /// <summary>
        /// Merges two ConnectionStringSettingsCollections.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <remarks>Used by <see cref="Williablog.Core.Configuration.ConfigSystem">ConfigSystem</c> to merge ConnectionStrings</remarks>
        public static ConnectionStringSettingsCollection Merge(this ConnectionStringSettingsCollection first, ConnectionStringSettingsCollection second)
        {
            if (second == null)
            {
                return first;
            }

            foreach (ConnectionStringSettings item in second)
            {
                ConnectionStringSettings itemInSecond = item;
                ConnectionStringSettings existingItem = first.Cast<ConnectionStringSettings>().FirstOrDefault(x => x.Name == itemInSecond.Name);

                if (existingItem != null)
                {
                    first.Remove(item);
                }

                first.Add(item);
            }

            return first;
        }
    }
}
