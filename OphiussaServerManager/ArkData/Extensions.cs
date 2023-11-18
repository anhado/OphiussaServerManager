﻿using System.Collections.Generic;

namespace ArkData
{
    internal static class Extensions
    {
        private static readonly int[] Empty = new int[0];

        public static int LocateFirst(this byte[] self, byte[] candidate, int offset = 0)
        {
            if (Extensions.IsEmptyLocate(self, candidate, offset))
                return -1;
            for (int position = offset; position < self.Length; ++position)
            {
                if (Extensions.IsMatch(self, position, candidate))
                    return position;
            }
            return -1;
        }

        public static IEnumerable<int> Locate(this byte[] self, byte[] candidate)
        {
            if (Extensions.IsEmptyLocate(self, candidate, 0))
                return (IEnumerable<int>)Extensions.Empty;
            List<int> intList = new List<int>();
            for (int position = 0; position < self.Length; ++position)
            {
                if (Extensions.IsMatch(self, position, candidate))
                    intList.Add(position);
            }
            return intList.Count != 0 ? (IEnumerable<int>)intList : (IEnumerable<int>)Extensions.Empty;
        }

        private static bool IsMatch(byte[] array, int position, byte[] candidate)
        {
            if (candidate.Length > array.Length - position)
                return false;
            for (int index = 0; index < candidate.Length; ++index)
            {
                if ((int)array[position + index] != (int)candidate[index])
                    return false;
            }
            return true;
        }

        private static bool IsEmptyLocate(byte[] array, byte[] candidate, int offset) => array == null || candidate == null || array.Length == 0 || candidate.Length == 0 || candidate.Length > array.Length || offset == -1 || offset > array.Length;
    }
}