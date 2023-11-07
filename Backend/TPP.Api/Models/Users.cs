using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace TPP.Api.Models;

public class GetUsersItemResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsOnline { get; set; }

    public int TotalPositiveCount { get; set; }
    public int TotalNegativeCount { get; set; }
}

public class GetUsersRequest
{
    public OrderTypeEnum OrderType { get; set; } = OrderTypeEnum.None;
}

public enum OrderTypeEnum
{
    None = 0,
    ByOnline = 1,
    ByPositive = 2,
    ByNegative = 3
}