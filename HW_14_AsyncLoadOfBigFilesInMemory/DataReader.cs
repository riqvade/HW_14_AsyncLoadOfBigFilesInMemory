using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_14_AsyncLoadOfBigFilesInMemory
{
    public class DataReader
    {
        /// <summary>
        /// Читает большой файл асинхронно с возможностью отмены загрузки 
        /// </summary>
        public static async Task ReadDataFromFileWithCanselTokenAsync(StreamReader streamReader, CancellationToken cancellationToken)
        {
            char[] result = new char[streamReader.BaseStream.Length];
            await streamReader.ReadAsync(result, cancellationToken);
        }
    }
}
