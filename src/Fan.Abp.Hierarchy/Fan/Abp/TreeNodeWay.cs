namespace Fan.Abp
{
    /// <summary>
    /// 树节点获取的方式
    /// </summary>
    public enum TreeNodeWay
    {
        /// <summary>
        /// 父节点
        /// </summary>
        Parent,

        /// <summary>
        /// 当前节点
        /// </summary>
        Current,

        /// <summary>
        /// 子节点
        /// </summary>
        Child,

        /// <summary>
        /// 下一级子节点
        /// </summary>
        NextChild,

        /// <summary>
        /// 父节点和当前
        /// </summary>
        ParentAndCurrent,

        /// <summary>
        /// 当前节点和子节点
        /// </summary>
        CurrentAndChild,

        /// <summary>
        /// 父节点 当前节点 子节点
        /// </summary>
        ParentAndCurrentAndChild,

        /// <summary>
        /// 父节点和子节点 (不包含当前节点)
        /// </summary>
        ParentAndChild
    }
}
