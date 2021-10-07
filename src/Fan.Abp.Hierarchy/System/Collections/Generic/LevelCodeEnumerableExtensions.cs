using System.Linq;
using Fan.Abp;
using Fan.Abp.Hierarchy;


namespace System.Collections.Generic
{
    public static class LevelCodeEnumerableExtensions
    {
        /// <summary>
        /// 获取根
        /// </summary>
        /// <typeparam name="TLevelCode"></typeparam>
        /// <param name="levelCodes"></param>
        /// <returns></returns>
        public static TLevelCode GetRoot<TLevelCode>(
            this IEnumerable<TLevelCode> levelCodes)
            where TLevelCode : class, IHasLevelCode<string>
        {
            return levelCodes.SingleOrDefault(org => org.LevelCode.IsNullOrWhiteSpace());
        }

        /// <summary>
        /// 获取所有子
        /// </summary>
        /// <typeparam name="TLevelCode"></typeparam>
        /// <param name="levelCodes"></param>
        /// <param name="levelCode"></param>
        /// <returns></returns>
        public static IEnumerable<TLevelCode> GetChild<TLevelCode>(
            this IEnumerable<TLevelCode> levelCodes, string levelCode)
            where TLevelCode : class, IHasLevelCode<string>
        {
            if (levelCode.IsNullOrWhiteSpace())
            {
                return levelCodes;
            }

            return levelCodes
                .Where(org => !org.LevelCode.IsNullOrWhiteSpace())
                .Where(org => org.LevelCode != levelCode && org.LevelCode.StartsWith(levelCode));
        }

        /// <summary>
        /// 获取所有子
        /// </summary>
        /// <typeparam name="TLevelCode"></typeparam>
        /// <param name="levelCodes"></param>
        /// <param name="levelCodeList"></param>
        /// <param name="isNext"></param>
        /// <returns></returns>
        public static List<TLevelCode> GetChild<TLevelCode>(this IEnumerable<TLevelCode> levelCodes, IEnumerable<string> levelCodeList, bool isNext = false)
            where TLevelCode : class, IHasLevelCode<string>
        {
            var levelCodesArray = levelCodeList as string[] ?? levelCodeList.ToArray();
            if (!levelCodesArray.Any())
            {
                return new List<TLevelCode>();
            }

            if (isNext)
            {
                return levelCodes
                   // .Where(org => !org.LevelCode.IsNullOrWhiteSpace())
                    .Where(org =>
                        levelCodesArray.Any(parentLevelCode =>
                            org.LevelCode != parentLevelCode && // 不等于自己
                            org.LevelCode.Length ==  ((parentLevelCode?.Length ?? 0) + 10) && //
                            (parentLevelCode.IsNullOrEmpty() || org.LevelCode.StartsWith(parentLevelCode))// 当前层级下面
                        )).ToList();
            }

            return levelCodes
                .Where(org => !org.LevelCode.IsNullOrWhiteSpace())
                .Where(org =>
                    levelCodesArray.Any(parentLevelCode =>
                        org.LevelCode != parentLevelCode && org.LevelCode.StartsWith(parentLevelCode))).ToList();
        }

        #region HierarchyCode

        /// <summary>
        /// 获取根
        /// </summary>
        /// <param name="organizationUnits"></param>
        /// <returns></returns>
        public static IHasLevelCode<HierarchyCode> GetRoot(
            this IEnumerable<IHasLevelCode<HierarchyCode>> organizationUnits)
        {
            return organizationUnits.SingleOrDefault(org => org.LevelCode == HierarchyCode.GetRoot());
        }

        /// <summary>
        /// 获取所有子
        /// </summary>
        /// <param name="organizationUnits"></param>
        /// <param name="levelCode"></param>
        /// <returns></returns>
        public static IEnumerable<IHasLevelCode<HierarchyCode>> GetChild(
            this IEnumerable<IHasLevelCode<HierarchyCode>> organizationUnits, HierarchyCode levelCode)
        {
            if (levelCode.IsNull)
            {
                return Array.Empty<IHasLevelCode<HierarchyCode>>();
            }

            if (levelCode == HierarchyCode.GetRoot())
            {
                return organizationUnits;
            }

            return organizationUnits
                .Where(c => !c.LevelCode.IsNull)
                .Where(c => c.LevelCode != levelCode && c.LevelCode.IsDescendantOf(levelCode));
        }

        #endregion
    }
}
