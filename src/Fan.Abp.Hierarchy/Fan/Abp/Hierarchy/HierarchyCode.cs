using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Fan.Abp.Hierarchy
{
    [Serializable]
    public struct HierarchyCode : IComparable, ISerializable
    {
        private HierarchyId _imp;

        public bool IsNull { get; }

        /// <summary>
        /// Gets a <see cref="HierarchyCode"/> with a hierarchy identification of null.
        /// </summary>
        public static HierarchyCode Null { get; } = new HierarchyCode(new HierarchyId(), true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imp"></param>
        /// <param name="isNull"></param>

        private HierarchyCode(HierarchyId imp, bool isNull = false)
        {
            IsNull = isNull;
            _imp = imp;
        }

        public static HierarchyCode GetRoot() => new HierarchyCode(HierarchyId.GetRoot());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static HierarchyCode Parse(string input, HierarchyCodeFormat format = HierarchyCodeFormat.Default)
        {
            var codeStr = input;

            if (input != null && format == HierarchyCodeFormat.LevelCode)
            {
                codeStr = LevelCodeHelper.ToHierarchyCode(input);
            }

            if (codeStr.IsNullOrEmpty())
            {
                codeStr = "/";
            }

            return new HierarchyCode(HierarchyId.Parse(codeStr));
        }


        /// <summary>
        /// 获取父集
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public HierarchyCode GetAncestor(int n)
        {
            if (IsNull || _imp.GetLevel() < n)
            {
                return Null;
            }
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("24011: HierarchyCode.GetAncestor failed because 'n' was negative.");
            }
            return new HierarchyCode(_imp.GetAncestor(n));
        }


        /// <summary>
        /// Gets the value of a descendant <see cref="HierarchyCode"/> node that is greater than <paramref name="child1"/> and less than <paramref name="child2"/>.
        /// </summary>
        /// <param name="child1">The lower bound.</param>
        /// <param name="child2">The upper bound.</param>
        /// <returns>A HierarchyCode with a value greater than the lower bound and less than the upper bound.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If parent is <c>null</c>, returns <c>null</c>.</item>
        /// <item>If parent is not null, and both <paramref name="child1"/> and <paramref name="child2"/> are <c>null</c>, returns a descendant of parent.</item>
        /// <item>If parent and <paramref name="child1"/> are not <c>null</c>, and <paramref name="child2"/> is <c>null</c>, returns a descendant of parent greater than <paramref name="child1"/>.</item>
        /// <item>If parent and <paramref name="child2"/> are not <c>null</c> and <paramref name="child1"/> is <c>null</c>, returns a descendant of parent less than <paramref name="child2"/>.</item>
        /// <item>If parent, <paramref name="child1"/>, and child2 are not <c>null</c>, returns a descendant of parent greater than <paramref name="child1"/> and less than <paramref name="child2"/>.</item>
        /// <item>An exception is raised if <paramref name="child1"/> or <paramref name="child2"/> are not <c>null</c> and are not a descendant of parent.</item>
        /// <item>If <paramref name="child1"/> >= <paramref name="child2"/>, an exception is raised.</item>
        /// </list>
        /// </remarks>

        public HierarchyCode GetDescendant(HierarchyCode child1, HierarchyCode child2) => new HierarchyCode(_imp.GetDescendant(child1.IsNull ? default(HierarchyId?) : child1._imp, child2.IsNull ? default(HierarchyId?) : child2._imp));


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => _imp.GetHashCode();


        public int GetLevel() => _imp.GetLevel();


        public HierarchyCode GetReparentedValue(HierarchyCode oldRoot, HierarchyCode newRoot)
        {
            if (!IsNull && !oldRoot.IsNull && !newRoot.IsNull)
            {
                if (!IsDescendantOf(oldRoot))
                {
                    throw new HierarchyIdException("Instance is not a descendant of 'oldRoot'");
                }
                return new HierarchyCode(_imp.GetReparentedValue(oldRoot._imp, newRoot._imp));
            }

            return Null;
        }

        public bool IsDescendantOf(HierarchyCode parent) => _imp.IsDescendantOf(parent._imp);


        public override string ToString()
        {
            // TODO 需要特殊处理
            return _imp.ToString();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("hierarchyId", _imp.ToString());
        }

        public HierarchyCode(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            var hierarchyId = info.GetString("hierarchyId");

            if (hierarchyId.IsNullOrEmpty())
            {
                IsNull = true;
                _imp = new HierarchyId();
            }
            else
            {
                IsNull = false;
                _imp = new HierarchyId(hierarchyId);
            }
        }

        public string ToString(HierarchyCodeFormat format)
        {
            if (!IsNull && format == HierarchyCodeFormat.LevelCode)
            {
                return ToLevelCodeString();
            }

            return _imp.ToString();
        }

        public string ToLevelCodeString()
        {
            if (IsNull)
            {
                return _imp.ToString();
            }

            var unitCodes = _imp.ToString().Split("/",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            return LevelCodeHelper.CreateLevelCode(unitCodes);
        }

        #region CompareTo

        public int CompareTo(object obj) => this.CompareTo((HierarchyCode)obj);
        public int CompareTo(HierarchyCode hid)
        {
            if (IsNull)
            {
                if (!hid.IsNull)
                {
                    return -1;
                }

                return 0;
            }

            if (hid.IsNull)
            {
                return 1;
            }

            if (this < hid)
            {
                return -1;
            }

            if (this > hid)
            {
                return 1;
            }

            return 0;
        }

        #endregion

        #region Equals


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is HierarchyCode code && Equals(code);

        private bool Equals(HierarchyCode other) => (IsNull && other.IsNull) || (this == other);

        #endregion

        #region Operator

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hid1"></param>
        /// <param name="hid2"></param>
        /// <returns></returns>
        public static bool operator ==(HierarchyCode hid1, HierarchyCode hid2) => hid1._imp == hid2._imp;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hid1"></param>
        /// <param name="hid2"></param>
        /// <returns></returns>
        public static bool operator !=(HierarchyCode hid1, HierarchyCode hid2) => hid1._imp != hid2._imp;

        /// <summary>
        /// Evaluates whether one specified <see cref="HierarchyCode"/> node is less than another.
        /// </summary>
        /// <param name="hid1">First node to compare.</param>
        /// <param name="hid2">Second node to compare.</param>
        /// <returns></returns>
        /// <remarks>Returns null if either <paramref name="hid1"/> or <paramref name="hid2"/> are null.</remarks>
        public static bool operator <(HierarchyCode hid1, HierarchyCode hid2) => hid1._imp < hid2._imp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hid1"></param>
        /// <param name="hid2"></param>
        /// <returns></returns>
        public static bool operator >(HierarchyCode hid1, HierarchyCode hid2) => hid1._imp > hid2._imp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hid1"></param>
        /// <param name="hid2"></param>
        /// <returns></returns>
        public static bool operator <=(HierarchyCode hid1, HierarchyCode hid2) => hid1._imp <= hid2._imp;

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hid1"></param>
        /// <param name="hid2"></param>
        /// <returns></returns>
        public static bool operator >=(HierarchyCode hid1, HierarchyCode hid2) => hid1._imp >= hid2._imp;

        #endregion
    }
}
