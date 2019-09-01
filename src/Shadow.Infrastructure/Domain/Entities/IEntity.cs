using System;

namespace Shadow.Infrastructure.Domain.Entities
{
    /// <summary>
    /// 实体，主键类型为 <see cref="int"/>
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }

    /// <summary>
    /// 实体，主键类型为 <see cref="long"/>
    /// </summary>
    public interface IEntityOfLong : IEntity<long>
    {

    }

    /// <summary>
    /// 实体，主键类型为 <see cref="string"/>
    /// </summary>
    public interface IEntityOfString : IEntity<string>
    {

    }

    /// <summary>
    /// 实体，主键类型为 <see cref="Guid"/>
    /// </summary>
    public interface IEntityOfGuid : IEntity<Guid>
    {

    }

    /// <summary>
    /// 实体
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体主键</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 实体主键
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// 检查是否是临时的实体
        /// </summary>
        /// <returns></returns>
        bool IsTransient();
    }
}
