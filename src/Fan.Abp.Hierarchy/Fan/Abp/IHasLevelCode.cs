namespace Fan.Abp
{
    /// <summary>
    /// TODO 换个更好的位置
    /// </summary>
    /// <typeparam name="TLevelCode"></typeparam>
    public interface IHasLevelCode<out TLevelCode>
    {
        /// <summary>
        /// 层级编码
        /// </summary>
        TLevelCode LevelCode { get; }
    }

    public interface IHasLevelCode : IHasLevelCode<string>
    {

    }
}
