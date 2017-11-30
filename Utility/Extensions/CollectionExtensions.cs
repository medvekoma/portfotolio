using System;
using System.Collections.Generic;

namespace Portfotolio.Utility.Extensions
{
    public static class CollectionExtensions
    {
         public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
         {
             var random = new Random();
             var sourceIndices = new List<T>(source);
             while(sourceIndices.Count > 0)
             {
                 var index = random.Next(sourceIndices.Count);
                 yield return sourceIndices[index];
                 sourceIndices.RemoveAt(index);
             }
         }
    }
}