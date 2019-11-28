using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMethods
{
    public interface IPersistentStore
    {
        /// <summary> Clears the persistent store. </summary>
        void Clear();

        /// <summary> Verifies the data integrity of the persistent store. </summary>
        /// <returns> True if it succeeds, false if it fails. </returns>
        bool Verify();

        /// <summary> Stores the given value. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="key">   The key. </param>
        /// <param name="value"> The value. </param>
        /// <returns> True if it succeeds, false if it fails. </returns>
        bool Store<T>(string key, T value);

        /// <summary> Retrieves value of the given key. </summary>
        /// <param name="key"> The key. </param>
        /// <returns> A KeyValue. </returns>
        T Retrieve<T>(string key) where T: class;

        Nullable<T> Retrieve<T>(string key) where T: struct;

        /// <summary> Deletes the given key-value entry from the persistent storage. </summary>
        /// <param name="key"> The key for the key-value to be deleted. </param>
        void Erase(string key);

        /// <summary> Deletes the key-value entries from the storage for given keys. </summary>
        /// <param name="keys"> The enumerable collection of keys. </param>
        void Erase(IEnumerable<string> keys);
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
