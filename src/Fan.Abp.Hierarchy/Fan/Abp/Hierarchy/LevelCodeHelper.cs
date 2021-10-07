using System;
using System.Collections.Generic;
using System.Linq;

namespace Fan.Abp.Hierarchy
{
    public static class LevelCodeHelper
    {
        /// <summary>
        /// LevelCode 的最大深度 
        /// </summary>
        public const int LevelCodeMaxDepth = 10;

        /// <summary>
        /// LevelCode 单位长度
        /// </summary>
        public const int LevelCodeUnitLength = 10;

        /// <summary>
        /// LevelCode 分割符
        /// </summary>

        public const string LevelCodeSplitter = "";


        public static string ToHierarchyCode(string levelCode)
        {
            if (levelCode.IsNullOrWhiteSpace())
            {
                return "/";
            } 
            
            var level = levelCode.Length / 10;

            var unitCodes = new int[level];

            for (var i = 0; i < level; i++)
            {
                unitCodes[i] = int.Parse(levelCode.Substring(i * 10, 10));
            }

            return $"/{unitCodes.JoinAsString("/")}/";
        }

        public static string CreateLevelCode(params int[] numbers)
        {
            if (numbers == null)
            {
                return string.Empty;
            }

            return string.Join(LevelCodeSplitter,
                numbers.Select(number => number.ToString(new string('0', LevelCodeUnitLength))));
        }

        public static string AppendCode(string parentCode, string childCode)
        {
            if (string.IsNullOrEmpty(childCode))
            {
                throw new ArgumentNullException(nameof(childCode), "childCode can not be null or empty.");
            }

            if (string.IsNullOrEmpty(childCode))
            {
                return childCode;
            }

            return parentCode + LevelCodeSplitter + childCode;
        }

        public static string GetRelativeCode(string code, string parentCode)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
            }

            if (string.IsNullOrEmpty(parentCode))
            {
                return code;
            }

            if (code.Length == parentCode.Length)
            {
                return null;
            }

            return code.Substring(parentCode.Length + LevelCodeSplitter.Length);
        }
    }
}
